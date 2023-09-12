using System.Collections.Generic;
using UnityEngine.Assertions;
using System;

namespace VVT {

    /// <summary> 
    /// Use this class to get and register VamVamTemplate services.
    /// </summary>
    public sealed class Services : VVTSystem {

        public static Services Instance => _instance ??= new Services();

        protected override string Prefix { get; set; } = "Service Locator : ";

        private readonly Dictionary<Type, object> _services = new();
        private static Services _instance;

        private Services() { }

        public void RegisterService<T>(T service) {
            var serviceType = typeof(T);

            Assert.IsFalse(_services.ContainsKey(serviceType), $"Service {Logs.Colorize(serviceType.Name, LogColor.Aqua)} already registered");

            _services.Add(serviceType, service);
            Logs.SystemLog(Prefix, $"Service {Logs.Colorize(serviceType.Name, LogColor.Aqua)} registered");
        }

        public void UnRegisterService<T>() {
            var serviceType = typeof(T);

            Assert.IsTrue(_services.ContainsKey(serviceType), $"Service {Logs.Colorize(serviceType.Name, LogColor.Orange)} not registered");

            _services.Remove(serviceType);
            Logs.SystemLog(Prefix, $"Service {Logs.Colorize(serviceType.Name, LogColor.Orange)} un-registered");
        }

        public T GetService<T>() {
            var type = typeof(T);

            if (!_services.TryGetValue(type, out var service)) {
                Logs.LogError (
                    $"Failed to get {Logs.Colorize(type.Name, LogColor.Orange)} Service, try getting the service from Start() method. " 
                    + $"{Logs.Colorize("Note", LogColor.Lime)}: if you're creating a new Service and it can't be referenced in the Awake() method, " 
                    + "change the execution order of your concrete service implementation to be prior."
                );
            }
            
            return (T) service;     // * This cast will never fail because T, is the same T as the RegisterService method
        }

    }
}