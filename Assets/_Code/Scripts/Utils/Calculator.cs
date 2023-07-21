using UnityEngine;
namespace VamVam.Scripts.Utils {

    public interface ILog {
        void Log(string message);
        int  LogCount();
    }

    ///<summary> Example class for tests </summary>
    public class UnityLogAdapter : ILog {
        void ILog.Log(string message) {
            Debug.Log(message);
        }

        int ILog.LogCount() {
            return 22;  // * valor cualquiera
        }
    }

    public sealed class Calculator {

        private readonly ILog _log;

        public Calculator(ILog log) {
            _log = log;
        }

        public int Sum(int a, int b) {
            if (a < 0 || b < 0)
                throw new System.Exception();

            var result = a + b;

            _log.Log($"{a} + {b} = {result}");

            return result;
        }

    }
}