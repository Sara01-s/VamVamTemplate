using UnityEngine;
using System;

namespace VVT {

    [Serializable]
    public struct Optional<T> {

        [SerializeField] private bool _enabled;
        [SerializeField] private T _value;

        public Optional(T initialValue) {
            _enabled = true;
            _value = initialValue;
        }

        public readonly void IfPresent(Action<T> action) {
            if (_value != null) {
                action(_value);
            }
        }

        public readonly T OrElse(T elseValue) {
            if (_value == null) {
                return elseValue;
            }

            return _value;
        }

        public readonly T OrElseThrow(Exception exception) {
            if (_value == null) {
                throw exception;
            }

            return _value;
        }

        public readonly bool Enabled => _enabled;
        public T Value { readonly get => _value; set => _value = value; }

    }
    
}

