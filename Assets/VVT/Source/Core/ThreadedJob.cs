// based on https://discussions.unity.com/t/unity3d-and-c-coroutines-vs-threading/57049/2
using System.Collections;

namespace VVT {
    
    public class ThreadedJob {

        private bool _isDone = false;
        private readonly object _handle = new();
        private System.Threading.Thread _thread = null;

        public bool IsDone {
            get {
                bool tmp;
                lock (_handle) {
                    tmp = _isDone;
                }
                return tmp;
            }
            set {
                lock (_handle) {
                    _isDone = value;
                }
            }
        }

        public virtual void Start() {
            _thread = new System.Threading.Thread(Run);
            _thread.Start();
        }

        public virtual void Abort() {
            _thread.Abort();
        }

        protected virtual void ThreadFunction() { }
        protected virtual void OnFinished() { }

        public virtual bool Update() {
            if (IsDone) {
                OnFinished();
                return true;
            }

            return false;
        }

        public IEnumerator WaitFor() {
            while(!Update())
                yield return null;
        }

        private void Run() {
            ThreadFunction();
            IsDone = true;
        }
    }
}
