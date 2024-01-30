using System.Collections.Generic;
using System;

public interface IEventListener<T> {
    void HandleEvent(T eventData);
}

public class EventDispatcher {
    public static EventDispatcher Instance => _instance ??= new();
    private static EventDispatcher _instance;

    private readonly Dictionary<Type, object> _events;

    public EventDispatcher() {
        _events = new();
    }

    public void Subscribe<T>(IEventListener<T> listener) {
        var type = typeof(T);

        if (!_events.ContainsKey(type)) {
            _events[type] = new List<IEventListener<T>>();
        }

        ((List<IEventListener<T>>) _events[type]).Add(listener);
    }

    public void Unsubscribe<T>(IEventListener<T> listener) {
        var type = typeof(T);

        if (_events.ContainsKey(type)) {
            ((List<IEventListener<T>>) _events[type]).Remove(listener);
        }
    }

    public void Dispatch<T>(T signal) {
        var type = typeof(T);

        if (_events.TryGetValue(type, out var listenersObj) && listenersObj is List<IEventListener<T>> listeners) {
            foreach (var listener in listeners) {
                listener.HandleEvent(signal);
            }
        }
    }
}

// Use this when Unity supports `dynamic` keyword
/*using System.Collections.Generic;
using System;
using VVT;

public class EventDispatcher {

	public static EventDispatcher Instance => _instance ??= new();
	private static EventDispatcher _instance;
	
	private readonly Dictionary<Type, dynamic> _events;

	public EventDispatcher() {
		_events = new();
	}

	public void Subscribe<T>(Action<T> callback) {
		var type = typeof(T);

		if (!_events.ContainsKey(type)) {
			if (!_events.TryAdd(type, null)) {
				Logs.LogError($"Failed to register action {callback.Method.Name} to type {type.Name}");
			}
		}

		_events[type] += callback;
	}

	public void Unsubscribe<T>(Action<T> callback) {
		var type = typeof(T);

		if (_events.ContainsKey(type)) {
			_events[type] -= callback;
		}
	}

	public void Dispatch<T>(T signal) {
		var type = typeof(T);

		if (!_events.ContainsKey(type)) {
			return;
		}

		_events[type](signal);
	}
}*/