# Thread Safety

## Definition

**Thread Safety** is a property of code that guarantees safe execution by multiple threads simultaneously without causing unexpected behavior, data corruption, or race conditions. A thread-safe program ensures that shared data is accessed and modified correctly when multiple threads are running concurrently.

## Why Thread Safety Matters

In multi-threaded applications, multiple threads may access and modify shared resources simultaneously. Without proper synchronization, this can lead to:
- **Race Conditions**: Unpredictable results due to timing of thread execution
- **Data Corruption**: Inconsistent or invalid data states
- **Deadlocks**: Threads waiting indefinitely for each other
- **Performance Issues**: Poor resource utilization

## Key Concepts

### 1. **Atomicity**
Operations that complete entirely or not at all, without interruption.

### 2. **Visibility**
Changes made by one thread are visible to other threads.

### 3. **Ordering**
Operations execute in the expected order across threads.

## Thread-Unsafe Example

```java
// NOT thread-safe!
class Counter {
    private int count = 0;
    
    public void increment() {
        count++; // This is NOT atomic! (read, modify, write)
    }
    
    public int getCount() {
        return count;
    }
}

// Problem: Multiple threads calling increment() can cause lost updates
// Thread 1: reads count=0
// Thread 2: reads count=0
// Thread 1: writes count=1
// Thread 2: writes count=1
// Result: count=1 (should be 2!)
```

## Techniques for Thread Safety

### 1. Synchronization (Locks)

#### Java Implementation

````java
// Thread-safe using synchronized keyword
class SynchronizedCounter {
    private int count = 0;
    
    // Method-level synchronization
    public synchronized void increment() {
        count++;
    }
    
    public synchronized int getCount() {
        return count;
    }
    
    // Block-level synchronization
    public void incrementBlock() {
        synchronized(this) {
            count++;
        }
    }
}

// Usage
class SyncDemo {
    public static void main(String[] args) throws InterruptedException {
        SynchronizedCounter counter = new SynchronizedCounter();
        
        // Create 10 threads
        Thread[] threads = new Thread[10];
        for (int i = 0; i < 10; i++) {
            threads[i] = new Thread(() -> {
                for (int j = 0; j < 1000; j++) {
                    counter.increment();
                }
            });
            threads[i].start();
        }
        
        // Wait for all threads to complete
        for (Thread t : threads) {
            t.join();
        }
        
        System.out.println("Final count: " + counter.getCount()); // 10000
    }
}
```

#### C# Implementation

```csharp
using System;
using System.Threading;

// Thread-safe using lock statement
public class SynchronizedCounter {
    private int count = 0;
    private readonly object lockObject = new object();
    
    public void Increment() {
        lock (lockObject) {
            count++;
        }
    }
    
    public int GetCount() {
        lock (lockObject) {
            return count;
        }
    }
}

// Usage
class Program {
    static void Main() {
        var counter = new SynchronizedCounter();
        var threads = new Thread[10];
        
        for (int i = 0; i < 10; i++) {
            threads[i] = new Thread(() => {
                for (int j = 0; j < 1000; j++) {
                    counter.Increment();
                }
            });
            threads[i].Start();
        }
        
        foreach (var thread in threads) {
            thread.Join();
        }
        
        Console.WriteLine($"Final count: {counter.GetCount()}"); // 10000
    }
}
```

#### C++ Implementation

```cpp
#include <iostream>
#include <thread>
#include <mutex>
#include <vector>

class SynchronizedCounter {
private:
    int count;
    std::mutex mtx; // Mutex for synchronization
    
public:
    SynchronizedCounter() : count(0) {}
    
    void increment() {
        std::lock_guard<std::mutex> lock(mtx); // RAII-style lock
        count++;
    }
    
    int getCount() {
        std::lock_guard<std::mutex> lock(mtx);
        return count;
    }
};

int main() {
    SynchronizedCounter counter;
    std::vector<std::thread> threads;
    
    // Create 10 threads
    for (int i = 0; i < 10; i++) {
        threads.push_back(std::thread([&counter]() {
            for (int j = 0; j < 1000; j++) {
                counter.increment();
            }
        }));
    }
    
    // Wait for all threads
    for (auto& thread : threads) {
        thread.join();
    }
    
    std::cout << "Final count: " << counter.getCount() << std::endl; // 10000
    
    return 0;
}
```

### 2. Atomic Operations

#### Java - AtomicInteger

```java
import java.util.concurrent.atomic.AtomicInteger;

class AtomicCounter {
    private AtomicInteger count = new AtomicInteger(0);
    
    public void increment() {
        count.incrementAndGet(); // Atomic operation
    }
    
    public int getCount() {
        return count.get();
    }
}
```

#### C# - Interlocked

```csharp
using System.Threading;

public class AtomicCounter {
    private int count = 0;
    
    public void Increment() {
        Interlocked.Increment(ref count); // Atomic operation
    }
    
    public int GetCount() {
        return Interlocked.CompareExchange(ref count, 0, 0); // Atomic read
    }
}
```

#### C++ - std::atomic

```cpp
#include <atomic>

class AtomicCounter {
private:
    std::atomic<int> count{0};
    
public:
    void increment() {
        count.fetch_add(1); // Atomic operation
    }
    
    int getCount() {
        return count.load();
    }
};
```

### 3. Immutability

```java
// Immutable objects are inherently thread-safe
public final class ImmutablePerson {
    private final String name;
    private final int age;
    
    public ImmutablePerson(String name, int age) {
        this.name = name;
        this.age = age;
    }
    
    public String getName() {
        return name;
    }
    
    public int getAge() {
        return age;
    }
    
    // No setters - object cannot be modified after creation
}
```

### 4. Thread-Local Storage

```java
// Each thread gets its own copy
public class ThreadLocalExample {
    private static ThreadLocal<Integer> threadId = ThreadLocal.withInitial(() -> 0);
    
    public static void setThreadId(int id) {
        threadId.set(id);
    }
    
    public static int getThreadId() {
        return threadId.get();
    }
}
```

### 5. Concurrent Collections

```java
import java.util.concurrent.*;

class ConcurrentCollectionsExample {
    // Thread-safe collections
    private ConcurrentHashMap<String, Integer> map = new ConcurrentHashMap<>();
    private CopyOnWriteArrayList<String> list = new CopyOnWriteArrayList<>();
    private BlockingQueue<String> queue = new LinkedBlockingQueue<>();
    
    public void addToMap(String key, Integer value) {
        map.put(key, value); // Thread-safe
    }
    
    public void addToList(String item) {
        list.add(item); // Thread-safe
    }
}
```

## Thread Safety Levels

### 1. **Immutable**
Objects that cannot be modified after creation (highest safety).

### 2. **Thread-Safe**
Objects that can be safely used by multiple threads without external synchronization.

### 3. **Conditionally Thread-Safe**
Thread-safe for most operations, but some require external synchronization.

### 4. **Not Thread-Safe**
Objects that require external synchronization for concurrent use.

## Common Patterns

### 1. Double-Checked Locking (Singleton Pattern)

```java
public class Singleton {
    private static volatile Singleton instance;
    
    private Singleton() {}
    
    public static Singleton getInstance() {
        if (instance == null) { // First check (no locking)
            synchronized (Singleton.class) {
                if (instance == null) { // Second check (with locking)
                    instance = new Singleton();
                }
            }
        }
        return instance;
    }
}
```

### 2. Read-Write Locks

```java
import java.util.concurrent.locks.ReadWriteLock;
import java.util.concurrent.locks.ReentrantReadWriteLock;

class Cache {
    private Map<String, String> data = new HashMap<>();
    private ReadWriteLock lock = new ReentrantReadWriteLock();
    
    public String read(String key) {
        lock.readLock().lock(); // Multiple readers allowed
        try {
            return data.get(key);
        } finally {
            lock.readLock().unlock();
        }
    }
    
    public void write(String key, String value) {
        lock.writeLock().lock(); // Exclusive write access
        try {
            data.put(key, value);
        } finally {
            lock.writeLock().unlock();
        }
    }
}
```

### 3. Producer-Consumer Pattern

```java
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

class ProducerConsumer {
    private BlockingQueue<String> queue = new LinkedBlockingQueue<>(10);
    
    class Producer implements Runnable {
        public void run() {
            try {
                for (int i = 0; i < 100; i++) {
                    queue.put("Item " + i); // Blocks if queue is full
                    System.out.println("Produced: Item " + i);
                }
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }
    
    class Consumer implements Runnable {
        public void run() {
            try {
                while (true) {
                    String item = queue.take(); // Blocks if queue is empty
                    System.out.println("Consumed: " + item);
                }
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
    }
}
```

## Best Practices

1. **Minimize Shared State**: Reduce shared mutable data
2. **Use Immutable Objects**: Prefer immutable objects when possible
3. **Use Thread-Safe Collections**: Leverage built-in concurrent collections
4. **Prefer Atomic Operations**: Use atomic classes for simple operations
5. **Lock Minimally**: Hold locks for the shortest time possible
6. **Avoid Nested Locks**: Prevent potential deadlocks
7. **Document Thread Safety**: Clearly document thread safety guarantees
8. **Use Higher-Level Constructs**: Prefer executors and concurrent utilities
9. **Test Thoroughly**: Use stress tests and tools to detect concurrency issues

## Common Pitfalls

1. **Forgetting to Synchronize**: Not protecting shared mutable state
2. **Over-Synchronization**: Synchronizing too much, hurting performance
3. **Incorrect Lock Scope**: Locking too little or too much code
4. **Deadlocks**: Circular dependencies on locks
5. **Visibility Issues**: Changes not visible across threads
6. **Compound Operations**: Treating non-atomic operations as atomic
7. **Broken ThreadLocal**: Not cleaning up ThreadLocal variables

## Performance Considerations

| Technique | Performance | Use Case |
|-----------|-------------|----------|
| **No Synchronization** | Fastest | Immutable objects, thread-local data |
| **Atomic Operations** | Very Fast | Simple counters, flags |
| **Lock-Free Algorithms** | Fast | Advanced scenarios |
| **Read-Write Locks** | Good | Many reads, few writes |
| **Synchronized Blocks** | Moderate | General purpose |
| **Concurrent Collections** | Good | Thread-safe collections |

## Testing Thread Safety

```java
import java.util.concurrent.*;

class ThreadSafetyTester {
    public static void testCounter(Counter counter, int threads, int iterations) 
            throws InterruptedException {
        ExecutorService executor = Executors.newFixedThreadPool(threads);
        CountDownLatch latch = new CountDownLatch(threads);
        
        for (int i = 0; i < threads; i++) {
            executor.submit(() -> {
                for (int j = 0; j < iterations; j++) {
                    counter.increment();
                }
                latch.countDown();
            });
        }
        
        latch.await();
        executor.shutdown();
        
        int expected = threads * iterations;
        int actual = counter.getCount();
        
        if (expected == actual) {
            System.out.println("✓ Thread-safe: " + actual + " = " + expected);
        } else {
            System.out.println("✗ NOT thread-safe: " + actual + " ≠ " + expected);
        }
    }
}
```

## Summary

Thread safety is critical for correct multi-threaded applications. Key strategies include:
- Using synchronization (locks)
- Atomic operations
- Immutable objects
- Thread-local storage
- Concurrent collections

Choose the right technique based on your specific requirements for correctness, performance, and simplicity.
