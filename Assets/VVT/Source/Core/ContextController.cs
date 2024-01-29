using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using System.Linq;
using System;

namespace VVT {

    [DefaultExecutionOrder(-50)]
    public sealed class ContextController : MonoBehaviour, IContextService {

        [field:SerializeField] public ContextInfo ContextsInfo { get; private set; }
        public event Action<Context, Context> OnContextChanged;
        public event Action<bool> OnPause;

		private Context _previousPauseContext = null;
		private List<Context> _allContexts;

        private void Awake      () => Services.Instance.RegisterService<IContextService>(this);
        private void OnDisable  () => Services.Instance.UnRegisterService<IContextService>();
		private void GetAllContexts() => _allContexts = Resources.LoadAll<Context>("Prefabs/Contexts").ToList();
		private void OnValidate () => GetAllContexts();

        private void Start() {
            Assert.IsNotNull(ContextsInfo, "Failed to initialize context info storage, please assign a \"Context info\" scriptable in the inspector");
			GetAllContexts();
        }

        public void UpdateGameContext(Context newContext) {
            ContextsInfo.CanToggleGamePause = newContext.AllowPause;
            ContextsInfo.PlayerHasControl = newContext.AllowControl;
            ContextsInfo.PreviousContext = ContextsInfo.CurrentContext;
            ContextsInfo.CurrentContext  = newContext;
			ContextsInfo.CursorLockState = newContext.CursorLockState;
			ContextsInfo.CursorVisibleState = newContext.CursorVisibleState;

			Cursor.lockState = ContextsInfo.CursorLockState;
			Cursor.visible = ContextsInfo.CursorVisibleState;

            Logs.SystemLog($"{Logs.Bold("New Game context")} : {newContext}");
            Logs.SystemLog($"{Logs.Bold("Previous Game context")} : {ContextsInfo.PreviousContext}");

            OnContextChanged?.Invoke(ContextsInfo.PreviousContext, newContext);
        }

        public void ToggleGamePause() {
			if (!ContextsInfo.CanToggleGamePause) return;

            ContextsInfo.GamePaused = !ContextsInfo.GamePaused;
            Time.timeScale = ContextsInfo.GamePaused ? 0.0f : 1.0f;

			// TODO - Find better way to do this
			var pauseContext = _allContexts.Find(context => context.name.ToLower().Contains("pause"));
			if (pauseContext == null) {
				Debug.LogWarning("Please create a context with which name contains \"Pause\" in it to be properly detected");
				return;
			}

			if (ContextsInfo.GamePaused) {
				_previousPauseContext = ContextsInfo.CurrentContext;
				UpdateGameContext(pauseContext);
                OnPause?.Invoke(true);
	            Logs.SystemLog("Game Paused", LogColor.Orange, bold: true);
			}
			else {
				UpdateGameContext(_previousPauseContext);
                OnPause?.Invoke(false);
	            Logs.SystemLog("Game Resumed", LogColor.Lime, bold: true);
			} 
        }

        public void QuitApplication() {
            Application.Quit();
        }

    }

	[Serializable]
	public sealed class ContextInfo {

        [Tooltip("Current game context handled by Vam Vam Template")]
        [field:SerializeField] public Context CurrentContext { get; set; }
        [Tooltip("Previous game context before context update")]
        [field:SerializeField] public Context PreviousContext { get; set; }
        [Tooltip("Can game pause be toggled in the current context?")]
        [field:SerializeField] public bool CanToggleGamePause { get; set; } = false;
        [Tooltip("Is player input read?")]
        [field:SerializeField] public bool PlayerHasControl { get; set; } = false;
        [Tooltip("Is game currently paused?")]
        [field:SerializeField] public bool GamePaused { get; set; } = false;
        [Tooltip("Cursor lock state used in the current context")]
		[field:SerializeField] public CursorLockMode CursorLockState { get; set; }
        [Tooltip("Is the cursor visible in the current context?")]
		[field:SerializeField] public bool CursorVisibleState { get; set; }
        
    }
}