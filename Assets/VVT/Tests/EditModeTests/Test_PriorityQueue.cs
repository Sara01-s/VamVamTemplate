using NUnit.Framework;

namespace VVT {

    public sealed class Test_PriorityQueue {

        [Test]
        public void Count_ReturnsPositiveNumber() {

            var pq = new PriorityQueue<int>();

            int count = pq.Count;

            Assert.GreaterOrEqual(count, 0);
            Assert.Less(count, float.MaxValue);
        }

        [Test]
        public void Dequeue_ReturnsItemWithLowestPriority() {
            var pq = new PriorityQueue<int>();

            pq.Enqueue(10, 5.0f);
            pq.Enqueue(6, 1.0f);
            pq.Enqueue(7, -60.0f); // Should dequeue this
            pq.Enqueue(8, 3.0f);

            int dequeued = pq.Dequeue();

            Assert.AreEqual(dequeued, 7);
        }

        [Test]
        public void Dequeue_RemovesItemWithLowestPriority() {
            var pq = new PriorityQueue<int>();

            pq.Enqueue(10, 5.0f);
            pq.Enqueue(6, 1.0f);
            pq.Enqueue(7, -60.0f); // Should dequeue this
            pq.Enqueue(8, 3.0f);

            int previousCount = pq.Count;
            _ = pq.Dequeue();

            Assert.AreEqual(pq.Count, previousCount - 1);
        }

        [Test]
        public void Peek_ReturnsItemWithoutRemovingIt() {
            var pq = new PriorityQueue<int>();

            pq.Enqueue(10, 5.0f);
            pq.Enqueue(6, 1.0f);
            pq.Enqueue(8, 3.0f);

            int prevCount = pq.Count;
            _ = pq.Peek();
            int currCount = pq.Count;

            Assert.AreEqual(currCount, prevCount);
        }

        [Test]
        public void Peek_ReturnsCorrectItem() {
            var pq = new PriorityQueue<int>();

            pq.Enqueue(10, 5.0f);
            pq.Enqueue(6, 1.0f);
            pq.Enqueue(7, -60.0f); // Should peek this
            pq.Enqueue(8, 3.0f);

            int item = pq.Peek();

            Assert.AreEqual(item, 7);
        }
    }
}
