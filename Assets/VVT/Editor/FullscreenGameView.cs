/*
MIT License

Copyright (c) 2021 Chillu

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

#if UNITY_EDITOR

using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Little utility for opening a "Game" view in fullscreen. Will be opened on whatever Unity thinks is the "main"
/// monitor at the moment (or last position?). If for some reason event breaks, fullscreen windows can be closed via Alt+F4.
/// </summary>
/// <remarks>
/// Confirmed to work in Unity 2020. May work in earlier and later versions. No promises.
/// <para> Unity will automatically make a new game window if we're on a double monitor setup that we don't want (extra rendering time) </para>
/// <para> So just make that window display ex. "Display 3", so it won't render anything </para>
/// </remarks>
[InitializeOnLoad]
public static class FullscreenGameView {

    private static readonly Type GameViewType = Type.GetType("UnityEditor.GameView,UnityEditor");

    private static readonly PropertyInfo ShowToolbarProperty =
        GameViewType.GetProperty("showToolbar", BindingFlags.Instance | BindingFlags.NonPublic);

    private static readonly object FalseObject = false; // Only box once. This is a matter of principle.
    private static EditorWindow Instance;

    private static readonly bool InFullscreen = true;

	public static bool UseFullScreen = false;

	[MenuItem("VamVam/System/Toggle Fullscreen play mode")]
	public static void ToggleFullScreen() {
		Debug.Log("VVT Editor: Play mode Fullscreen: " + (InFullscreen ? "Enabled" : "Disabled"));
		UseFullScreen = !UseFullScreen;
	}

    static FullscreenGameView()
    {
        EditorApplication.playModeStateChanged -= ToggleFullScreen;
        if (!InFullscreen)
            return;
        EditorApplication.playModeStateChanged += ToggleFullScreen;
    }

    [MenuItem("Window/General/Game (Fullscreen) %#&2", priority = 2)]
    public static void Toggle() {
        ToggleFullScreen(PlayModeStateChange.EnteredPlayMode);
    }

    public static void ToggleFullScreen(PlayModeStateChange playModeStateChange) {
		if (!UseFullScreen) return;
		
        if (playModeStateChange == PlayModeStateChange.EnteredEditMode || playModeStateChange == PlayModeStateChange.ExitingEditMode) {
            CloseGameWindow();
            return;
        }

        if (GameViewType == null) {
            Debug.LogError("GameView type not found.");
            return;
        }

        if (ShowToolbarProperty == null) {
            Debug.LogWarning("GameView.showToolbar property not found.");
        }

        switch (playModeStateChange) {

            case PlayModeStateChange.ExitingPlayMode:
                return;
            case PlayModeStateChange.EnteredPlayMode: //Used to toggle
                if (CloseGameWindow())
                    return;
                break;
        }

        Instance = (EditorWindow) ScriptableObject.CreateInstance(GameViewType);

        ShowToolbarProperty?.SetValue(Instance, FalseObject);

        var desktopResolution = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
        var fullscreenRect = new Rect(Vector2.zero, desktopResolution);

        Instance.ShowPopup();
        Instance.position = fullscreenRect;
        Instance.Focus();
    }

    private static bool CloseGameWindow() {
        if (Instance != null) {
            Instance.Close();
            Instance = null;
            return true;
        }

        return false;
    }
}
#endif