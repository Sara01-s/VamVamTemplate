using UnityEngine;
using System;

namespace VamVam.Source.Utils {

    [Serializable]
    public struct Optional<T> {

        [SerializeField] private bool _enabled;
        [SerializeField] private T _value;

        public Optional(T initialValue) {
            _enabled = true;
            _value = initialValue;
        }

        public void IfPresent(Action<T> action) {
            if (_value != null) {
                action(_value);
            }
        }

        public T OrElse(T elseValue) {
            if (_value == null) {
                return elseValue;
            }

            return _value;
        }

        public T OrElseThrow(Exception exception) {
            if (_value == null) {
                throw exception;
            }

            return _value;
        }

        public readonly bool Enabled => _enabled;
        public T Value { get => _value; set => value = _value; }

    }
    
}

