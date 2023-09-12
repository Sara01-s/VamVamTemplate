// ----------------------------------------------------------------------------
// PriorityQueue class for use in Unity or Mono C# enviroments.
//
// Based on PriorityQueue implementation found in Red Blob Games' A* 
// Implementation compantion:
// http://www.redblobgames.com/pathfinding/a-star/implementation.html#csharp
// and
// https://gist.github.com/e-sarkis/716c4415254a22c2b2f9eb8d9df777f5
// ----------------------------------------------------------------------------
using System.Collections.Generic;

/// <summary>
/// A Queue class in which each item is associated with a float value
/// representing the item's priority. 
/// Dequeue and Peek functions return item with the best priority value (lowest float). 
/// </summary>
public sealed class PriorityQueue<T> {

    private readonly List<(T, float)> _elements = new();

    /// <summary>
    /// Return the total number of elements currently in the Queue.
    /// </summary>
    /// <returns>Total number of elements currently in Queue</returns>
    public int Count => _elements.Count;

    /// <summary>
    /// Add given item to Queue and assign item the given priority value.
    /// </summary>
    /// <param name="item">Item to be added.</param>
    /// <param name="priorityValue">Item priority value as Double.</param>
    public void Enqueue(T item, float priorityValue) {
        _elements.Add((item, priorityValue));
    }

    /// <summary>
    /// Return lowest priority value item and remove item from Queue.
    /// </summary>
    /// <returns>Queue item with lowest priority value.</returns>
    public T Dequeue() {
        int bestPriorityIndex = 0;

        for (int i = 0; i < _elements.Count; ++i) {
            if (_elements[i].Item2 < _elements[bestPriorityIndex].Item2) {
                bestPriorityIndex = i;
            }
        }

        T bestItem = _elements[bestPriorityIndex].Item1;
        _elements.RemoveAt(bestPriorityIndex);

        return bestItem;
    }

    /// <summary>
    /// Return lowest priority value item without removing item from Queue.
    /// </summary>
    /// <returns>Queue item with lowest priority value.</returns>
    public T Peek() {
        int bestPriorityIndex = 0;

        for (int i = 0; i < _elements.Count; ++i) {
            if (_elements[i].Item2 < _elements[bestPriorityIndex].Item2) {
                bestPriorityIndex = i;
            }
        }

        T bestItem = _elements[bestPriorityIndex].Item1;

        return bestItem;
    }
}
