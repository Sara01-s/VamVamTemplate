using System.Collections.Generic;
using UnityEngine.Assertions;
using System;

namespace VamVam.Source.Utils {

    /// <summary> 
    /// Use this class to get and register util services.
    /// </summary>
    public sealed class ServiceLocator {

        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());
        private readonly Dictionary<Type, object> _services;
        private const string CONSOLE_PREFIX = "Service Locator : ";


        private ServiceLocator() {
            _services = new Dictionary<Type, object>();
        }

        public void RegisterService<T>(T service) {
            var type = typeof(T);

            Assert.IsFalse(_services.ContainsKey(type), $"Service {type.Name} already registered");

            _services.Add(type, service);
            LogUtils.SystemLog(CONSOLE_PREFIX + $"Service {type.Name} registered");
        }

        public void UnRegisterService<T>() {
            var type = typeof(T);

            Assert.IsTrue(_services.ContainsKey(type), $"Service {type.Name} not registered");

            _services.Remove(type);
            LogUtils.SystemLog(CONSOLE_PREFIX + $"Service {type.Name} un-registered");
        }

        public T GetService<T>() {
            var type = typeof(T);

            if (!_services.TryGetValue(type, out var service)) {
                LogUtils.SystemLogError($"Failed to get {type.Name} Service, try getting the service from Start() method." + 
                                        "Note: if you're creating a new Service and it can't be referenced in the Awake() method," +
                                        "change the execution order of your service to be prior.");
            }

            return (T) service;     // This cast will never fail because is the same T as the RegisterService method
        }

    }
}