using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyDemo
{
    // Shared resource that is NOT thread-safe
    class UnsafeCounter
    {
        public int Count { get; private set; } = 0;

        public void Increment()
        {
            Count++; // Not atomic
        }
    }

    // Shared resource that IS thread-safe
    class SafeCounter
    {
        public int Count { get; private set; } = 0;
        private readonly object _lock = new object();

        // Method 1: Locking
        public void IncrementLock()
        {
            lock (_lock)
            {
                Count++;
            }
        }

        // Method 2: Interlocked (Atomic operations - High Performance)
        public void IncrementAtomic()
        {
            Interlocked.Increment(ref UnsafeCountField);
        }
        // Helper for Ref
        public int UnsafeCountField = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- C# Concurrency & Thread Safety Demo ---");

            // 1. Unsafe Counter (Race Condition)
            UnsafeCounter unsafeCounter = new UnsafeCounter();
            RunThreads(() => unsafeCounter.Increment());
            Console.WriteLine($"Unsafe Counter Value (Expected 2000): {unsafeCounter.Count}");

            // 2. Safe Counter (Locking)
            SafeCounter safeCounter = new SafeCounter();
            RunThreads(() => safeCounter.IncrementLock());
            Console.WriteLine($"Safe Counter (Lock) Value (Expected 2000): {safeCounter.Count}");
            
            // 3. Safe Counter (Interlocked)
            RunThreads(() => safeCounter.IncrementAtomic());
            Console.WriteLine($"Safe Counter (Atomic) Value (Expected 2000): {safeCounter.UnsafeCountField}");
        }

        static void RunThreads(Action incrementAction)
        {
            Thread t1 = new Thread(() =>
            {
                for (int i = 0; i < 1000; i++) incrementAction();
            });

            Thread t2 = new Thread(() =>
            {
                for (int i = 0; i < 1000; i++) incrementAction();
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();
        }
    }
}
