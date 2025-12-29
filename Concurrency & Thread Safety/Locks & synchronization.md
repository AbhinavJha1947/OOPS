# Locks & Synchronization

## Definition

**Locks** and **Synchronization** are mechanisms used to control access to shared resources in multi-threaded programs. They ensure that only one thread can access a critical section of code at a time, preventing race conditions and data corruption.

## Types of Locks

### 1. Mutex (Mutual Exclusion)
### 2. Semaphore
### 3. Read-Write Lock
### 4. Spin Lock
### 5. Reentrant Lock

## 1. Mutex (Mutual Exclusion Lock)

A **mutex** allows only one thread to access a resource at a time.

### Java - synchronized keyword

```java
public class MutexExample {
    private int sharedResource = 0;
    private final Object lock = new Object();
    
    // Method-level synchronization
    public synchronized void incrementMethod() {
        sharedResource++;
    }
    
    // Block-level synchronization
    public void incrementBlock() {
        synchronized(lock) {
            sharedResource++;
        }
    }
    
    // Static synchronization (class-level lock)
    public static synchronized void staticMethod() {
        // Only one thread can execute this across all instances
    }
}
```

### C# - lock statement

```csharp
public class MutexExample {
    private int sharedResource = 0;
    private readonly object lockObject = new object();
    
    public void Increment() {
        lock (lockObject) {
            sharedResource++;
        }
    }
    
    // Using Mutex class explicitly
    private static Mutex mutex = new Mutex();
    
    public void IncrementWithMutex() {
        mutex.WaitOne(); // Acquire lock
        try {
            sharedResource++;
        } finally {
            mutex.ReleaseMutex(); // Release lock
        }
    }
}
```

### C++ - std::mutex

```cpp
#include <mutex>
#include <thread>

class MutexExample {
private:
    int sharedResource = 0;
    std::mutex mtx;
    
public:
    // Using lock_guard (RAII)
    void incrementLockGuard() {
        std::lock_guard<std::mutex> lock(mtx);
        sharedResource++;
    } // Automatically unlocks when lock goes out of scope
    
    // Using unique_lock (more flexible)
    void incrementUniqueLock() {
        std::unique_lock<std::mutex> lock(mtx);
        sharedResource++;
        lock.unlock(); // Can manually unlock
        // Do other work...
        lock.lock(); // Can relock
    }
    
    // Using scoped_lock (C++17, multiple mutexes)
    void incrementScopedLock() {
        std::scoped_lock lock(mtx);
        sharedResource++;
    }
};
```

## 2. Semaphore

A **semaphore** controls access to a resource with a limited number of permits.

### Java - Semaphore

```java
import java.util.concurrent.Semaphore;

public class SemaphoreExample {
    // Allow up to 3 threads to access resource simultaneously
    private Semaphore semaphore = new Semaphore(3);
    
    public void accessResource() {
        try {
            semaphore.acquire(); // Acquire a permit
            System.out.println(Thread.currentThread().getName() + " accessing resource");
            
            // Simulate work
            Thread.sleep(2000);
            
            System.out.println(Thread.currentThread().getName() + " releasing resource");
        } catch (InterruptedException e) {
            e.printStackTrace();
        } finally {
            semaphore.release(); // Release the permit
        }
    }
    
    public static void main(String[] args) {
        SemaphoreExample example = new SemaphoreExample();
        
        // Create 10 threads, but only 3 can access resource at once
        for (int i = 0; i < 10; i++) {
            new Thread(example::accessResource, "Thread-" + i).start();
        }
    }
}
```

### C# - SemaphoreSlim

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

public class SemaphoreExample {
    private SemaphoreSlim semaphore = new SemaphoreSlim(3, 3); // 3 concurrent threads
    
    public async Task AccessResourceAsync() {
        await semaphore.WaitAsync(); // Acquire permit
        try {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} accessing resource");
            await Task.Delay(2000); // Simulate work
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} releasing resource");
        } finally {
            semaphore.Release(); // Release permit
        }
    }
}
```

### C++ - std::counting_semaphore (C++20)

```cpp
#include <semaphore>
#include <thread>
#include <iostream>

class SemaphoreExample {
private:
    std::counting_semaphore<3> semaphore{3}; // Max 3 threads
    
public:
    void accessResource() {
        semaphore.acquire(); // Acquire permit
        std::cout << "Thread accessing resource\n";
        std::this_thread::sleep_for(std::chrono::seconds(2));
        std::cout << "Thread releasing resource\n";
        semaphore.release(); // Release permit
    }
};
```

## 3. Read-Write Lock

Allows multiple readers OR one writer (readers-writer lock).

### Java - ReadWriteLock

```java
import java.util.concurrent.locks.ReadWriteLock;
import java.util.concurrent.locks.ReentrantReadWriteLock;
import java.util.HashMap;
import java.util.Map;

public class ReadWriteLockExample {
    private Map<String, String> cache = new HashMap<>();
    private ReadWriteLock rwLock = new ReentrantReadWriteLock();
    
    // Multiple threads can read simultaneously
    public String read(String key) {
        rwLock.readLock().lock();
        try {
            return cache.get(key);
        } finally {
            rwLock.readLock().unlock();
        }
    }
    
    // Only one thread can write at a time
    public void write(String key, String value) {
        rwLock.writeLock().lock();
        try {
            cache.put(key, value);
        } finally {
            rwLock.writeLock().unlock();
        }
    }
}
```

### C# - ReaderWriterLockSlim

```csharp
using System.Threading;
using System.Collections.Generic;

public class ReadWriteLockExample {
    private Dictionary<string, string> cache = new Dictionary<string, string>();
    private ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
    
    public string Read(string key) {
        rwLock.EnterReadLock(); // Multiple readers allowed
        try {
            return cache.ContainsKey(key) ? cache[key] : null;
        } finally {
            rwLock.ExitReadLock();
        }
    }
    
    public void Write(string key, string value) {
        rwLock.EnterWriteLock(); // Exclusive write access
        try {
            cache[key] = value;
        } finally {
            rwLock.ExitWriteLock();
        }
    }
}
```

### C++ - std::shared_mutex (C++17)

```cpp
#include <shared_mutex>
#include <map>
#include <string>

class ReadWriteLockExample {
private:
    std::map<std::string, std::string> cache;
    mutable std::shared_mutex rwMutex;
    
public:
    // Multiple threads can read simultaneously
    std::string read(const std::string& key) const {
        std::shared_lock<std::shared_mutex> lock(rwMutex);
        auto it = cache.find(key);
        return it != cache.end() ? it->second : "";
    }
    
    // Only one thread can write at a time
    void write(const std::string& key, const std::string& value) {
        std::unique_lock<std::shared_mutex> lock(rwMutex);
        cache[key] = value;
    }
};
```

## 4. Reentrant Lock

A lock that can be acquired multiple times by the same thread.

### Java - ReentrantLock

```java
import java.util.concurrent.locks.ReentrantLock;

public class ReentrantLockExample {
    private ReentrantLock lock = new ReentrantLock();
    
    public void outerMethod() {
        lock.lock();
        try {
            System.out.println("Outer method");
            innerMethod(); // Can reacquire the lock
        } finally {
            lock.unlock();
        }
    }
    
    public void innerMethod() {
        lock.lock(); // Same thread can acquire again
        try {
            System.out.println("Inner method");
        } finally {
            lock.unlock();
        }
    }
    
    // Try lock with timeout
    public void tryLockExample() {
        boolean acquired = false;
        try {
            acquired = lock.tryLock(1, TimeUnit.SECONDS);
            if (acquired) {
                // Do work
            } else {
                System.out.println("Could not acquire lock");
            }
        } catch (InterruptedException e) {
            e.printStackTrace();
        } finally {
            if (acquired) {
                lock.unlock();
            }
        }
    }
}
```

### C++ - std::recursive_mutex

```cpp
#include <mutex>

class ReentrantLockExample {
private:
    std::recursive_mutex mtx; // Can be locked multiple times by same thread
    
public:
    void outerMethod() {
        std::lock_guard<std::recursive_mutex> lock(mtx);
        std::cout << "Outer method\n";
        innerMethod(); // Same thread can acquire again
    }
    
    void innerMethod() {
        std::lock_guard<std::recursive_mutex> lock(mtx);
        std::cout << "Inner method\n";
    }
};
```

## 5. Spin Lock

A lock that causes a thread to wait in a loop ("spin") while repeatedly checking if the lock is available.

### C++ - std::atomic_flag

```cpp
#include <atomic>
#include <thread>

class SpinLock {
private:
    std::atomic_flag flag = ATOMIC_FLAG_INIT;
    
public:
    void lock() {
        while (flag.test_and_set(std::memory_order_acquire)) {
            // Spin (busy-wait)
        }
    }
    
    void unlock() {
        flag.clear(std::memory_order_release);
    }
};

// Usage
SpinLock spinLock;
void criticalSection() {
    spinLock.lock();
    // Critical code
    spinLock.unlock();
}
```

## Lock Comparison

| Lock Type | Use Case | Performance | Fairness |
|-----------|----------|-------------|----------|
| **Mutex** | General purpose, exclusive access | Medium | Fair |
| **Semaphore** | Limited concurrent access, resource pooling | Medium | Fair |
| **Read-Write Lock** | Many reads, few writes | High for reads | Fair |
| **Reentrant Lock** | Recursive calls, advanced features | Medium | Fair |
| **Spin Lock** | Very short critical sections | Low overhead | Not fair |

## Best Practices

1. **Always Release Locks**: Use try-finally or RAII to ensure locks are released
2. **Minimize Lock Scope**: Hold locks for the shortest time possible
3. **Avoid Nested Locks**: Prevent potential deadlocks
4. **Use Appropriate Lock Type**: Choose the right lock for your use case
5. **Prefer Higher-Level Constructs**: Use concurrent collections when possible
6. **Document Locking Strategy**: Clearly document which locks protect which data
7. **Lock Ordering**: Always acquire locks in the same order
8. **Avoid Lock Contention**: Reduce the number of threads competing for the same lock

## Common Pitfalls

1. **Forgetting to Unlock**: Causes threads to hang indefinitely
2. **Deadlock**: Circular dependencies on locks
3. **Lock Contention**: Too many threads competing for the same lock
4. **Priority Inversion**: High-priority thread waiting for low-priority thread
5. **Starvation**: Thread never acquires lock due to unfair scheduling

## Advanced: Lock-Free Programming

```java
import java.util.concurrent.atomic.AtomicReference;

// Lock-free stack using CAS (Compare-And-Swap)
public class LockFreeStack<T> {
    private static class Node<T> {
        T value;
        Node<T> next;
        
        Node(T value) {
            this.value = value;
        }
    }
    
    private AtomicReference<Node<T>> head = new AtomicReference<>();
    
    public void push(T value) {
        Node<T> newNode = new Node<>(value);
        Node<T> oldHead;
        
        do {
            oldHead = head.get();
            newNode.next = oldHead;
        } while (!head.compareAndSet(oldHead, newNode)); // CAS operation
    }
    
    public T pop() {
        Node<T> oldHead;
        Node<T> newHead;
        
        do {
            oldHead = head.get();
            if (oldHead == null) return null;
            newHead = oldHead.next;
        } while (!head.compareAndSet(oldHead, newHead)); // CAS operation
        
        return oldHead.value;
    }
}
```

## Summary

- **Mutex**: Exclusive access, one thread at a time
- **Semaphore**: Limited concurrent access, counting permits
- **Read-Write Lock**: Optimize for read-heavy workloads
- **Reentrant Lock**: Allow same thread to reacquire lock
- **Spin Lock**: Efficient for very short critical sections

Choose the appropriate synchronization mechanism based on your specific concurrency requirements.
