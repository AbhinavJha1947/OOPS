# Deadlocks

## Definition

A **deadlock** is a situation in concurrent programming where two or more threads are permanently blocked, each waiting for a resource that another thread holds. None of the threads can proceed, creating a circular dependency.

## Real-World Analogy

Imagine two people trying to cross a narrow bridge from opposite directions. Neither wants to back up, so both are stuck waiting for the other to move first. This is similar to a deadlock in programming.

## Coffman Conditions

For a deadlock to occur, **all four** of these conditions must be true simultaneously:

### 1. **Mutual Exclusion**
At least one resource must be held in a non-shareable mode (only one thread can use it at a time).

### 2. **Hold and Wait**
A thread holding at least one resource is waiting to acquire additional resources held by other threads.

### 3. **No Preemption**
Resources cannot be forcibly taken from a thread; they must be released voluntarily.

### 4. **Circular Wait**
A circular chain of threads exists where each thread holds a resource that the next thread in the chain is waiting for.

## Deadlock Example

### Java

```java
public class DeadlockExample {
    private static final Object lock1 = new Object();
    private static final Object lock2 = new Object();
    
    public static void main(String[] args) {
        // Thread 1: acquires lock1, then tries to acquire lock2
        Thread thread1 = new Thread(() -> {
            synchronized (lock1) {
                System.out.println("Thread 1: Holding lock1...");
                try { Thread.sleep(10); } catch (InterruptedException e) {}
                
                System.out.println("Thread 1: Waiting for lock2...");
                synchronized (lock2) {
                    System.out.println("Thread 1: Acquired lock2!");
                }
            }
        });
        
        // Thread 2: acquires lock2, then tries to acquire lock1  
        Thread thread2 = new Thread(() -> {
            synchronized (lock2) {
                System.out.println("Thread 2: Holding lock2...");
                try { Thread.sleep(10); } catch (InterruptedException e) {}
                
                System.out.println("Thread 2: Waiting for lock1...");
                synchronized (lock1) {
                    System.out.println("Thread 2: Acquired lock1!");
                }
            }
        });
        
        thread1.start();
        thread2.start();
        
        // Both threads will be deadlocked!
    }
}
```

### C#

```csharp
using System;
using System.Threading;

public class DeadlockExample {
    private static readonly object lock1 = new object();
    private static readonly object lock2 = new object();
    
    static void Main() {
        // Thread 1
        Thread thread1 = new Thread(() => {
            lock (lock1) {
                Console.WriteLine("Thread 1: Holding lock1...");
                Thread.Sleep(10);
                
                Console.WriteLine("Thread 1: Waiting for lock2...");
                lock (lock2) {
                    Console.WriteLine("Thread 1: Acquired lock2!");
                }
            }
        });
        
        // Thread 2
        Thread thread2 = new Thread(() => {
            lock (lock2) {
                Console.WriteLine("Thread 2: Holding lock2...");
                Thread.Sleep(10);
                
                Console.WriteLine("Thread 2: Waiting for lock1...");
                lock (lock1) {
                    Console.WriteLine("Thread 2: Acquired lock1!");
                }
            }
        });
        
        thread1.Start();
        thread2.Start();
        
        // Deadlock!
    }
}
```

### C++

```cpp
#include <iostream>
#include <thread>
#include <mutex>
#include <chrono>

std::mutex mutex1;
std::mutex mutex2;

void thread1Func() {
    std::lock_guard<std::mutex> lock1(mutex1);
    std::cout << "Thread 1: Holding mutex1...\n";
    std::this_thread::sleep_for(std::chrono::milliseconds(10));
    
    std::cout << "Thread 1: Waiting for mutex2...\n";
    std::lock_guard<std::mutex> lock2(mutex2);
    std::cout << "Thread 1: Acquired mutex2!\n";
}

void thread2Func() {
    std::lock_guard<std::mutex> lock2(mutex2);
    std::cout << "Thread 2: Holding mutex2...\n";
    std::this_thread::sleep_for(std::chrono::milliseconds(10));
    
    std::cout << "Thread 2: Waiting for mutex1...\n";
    std::lock_guard<std::mutex> lock1(mutex1);
    std::cout << "Thread 2: Acquired mutex1!\n";
}

int main() {
    std::thread t1(thread1Func);
    std::thread t2(thread2Func);
    
    t1.join();
    t2.join();
    
    // Deadlock!
    return 0;
}
```

## Deadlock Prevention Strategies

### 1. Lock Ordering

Always acquire locks in the same order across all threads.

#### Java - Fixed Order

```java
public class LockOrderingSolution {
    private static final Object lock1 = new Object();
    private static final Object lock2 = new Object();
    
    // Thread 1: lock1 -> lock2
    public void thread1() {
        synchronized (lock1) {
            System.out.println("Thread 1: Acquired lock1");
            synchronized (lock2) {
                System.out.println("Thread 1: Acquired lock2");
                // Do work
            }
        }
    }
    
    // Thread 2: lock1 -> lock2 (SAME ORDER!)
    public void thread2() {
        synchronized (lock1) {
            System.out.println("Thread 2: Acquired lock1");
            synchronized (lock2) {
                System.out.println("Thread 2: Acquired lock2");
                // Do work
            }
        }
    }
}
```

### 2. Try Lock with Timeout

Attempt to acquire a lock with a timeout, and back off if unsuccessful.

#### Java - tryLock

```java
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;
import java.util.concurrent.TimeUnit;

public class TryLockSolution {
    private Lock lock1 = new ReentrantLock();
    private Lock lock2 = new ReentrantLock();
    
    public void thread1() {
        while (true) {
            boolean acquired1 = false;
            boolean acquired2 = false;
            
            try {
                acquired1 = lock1.tryLock(50, TimeUnit.MILLISECONDS);
                if (!acquired1) {
                    continue; // Retry
                }
                
                acquired2 = lock2.tryLock(50, TimeUnit.MILLISECONDS);
                if (!acquired2) {
                    continue; // Retry
                }
                
                // Both locks acquired, do work
                System.out.println("Thread 1: Working with both locks");
                break;
                
            } catch (InterruptedException e) {
                e.printStackTrace();
            } finally {
                if (acquired2) lock2.unlock();
                if (acquired1) lock1.unlock();
            }
        }
    }
}
```

### 3. Lock All or Nothing

Acquire all required locks atomically or none at all.

#### C++ - std::lock

```cpp
#include <mutex>
#include <thread>

std::mutex mutex1;
std::mutex mutex2;

void safeFunction() {
    // Acquires both locks atomically - deadlock-free!
    std::lock(mutex1, mutex2);
    
    // Adopt ownership of already-locked mutexes
    std::lock_guard<std::mutex> lock1(mutex1, std::adopt_lock);
    std::lock_guard<std::mutex> lock2(mutex2, std::adopt_lock);
    
    // Do work with both locks
}

// C++17: Even simpler with scoped_lock
void safeFunctionCpp17() {
    std::scoped_lock lock(mutex1, mutex2); // Acquires both atomically
    // Do work
}
```

### 4. Banker's Algorithm

A deadlock-avoidance algorithm that ensures the system never enters an unsafe state.

```java
public class BankersAlgorithm {
    private int[] available;     // Available resources
    private int[][] maximum;     // Maximum resources needed
    private int[][] allocation;  // Currently allocated resources
    private int[][] need;        // Remaining resource needs
    
    public boolean isSafeState() {
        int n = allocation.length; // Number of processes
        int m = available.length;  // Number of resource types
        
        boolean[] finish = new boolean[n];
        int[] work = available.clone();
        
        int count = 0;
        while (count < n) {
            boolean found = false;
            for (int i = 0; i < n; i++) {
                if (!finish[i]) {
                    boolean canAllocate = true;
                    for (int j = 0; j < m; j++) {
                        if (need[i][j] > work[j]) {
                            canAllocate = false;
                            break;
                        }
                    }
                    
                    if (canAllocate) {
                        for (int j = 0; j < m; j++) {
                            work[j] += allocation[i][j];
                        }
                        finish[i] = true;
                        found = true;
                        count++;
                    }
                }
            }
            
            if (!found) {
                return false; // System is in unsafe state
            }
        }
        
        return true; // System is in safe state
    }
}
```

## Deadlock Detection

### 1. Thread Dump Analysis

#### Java - Thread Dump

```bash
jstack <pid>
```

Look for patterns like:
```
"Thread-1" waiting for monitor entry [0x...]
  waiting to lock <0x123> (a java.lang.Object)
  locked <0x456> (a java.lang.Object)

"Thread-2" waiting for monitor entry [0x...]
  waiting to lock <0x456> (a java.lang.Object)
  locked <0x123> (a java.lang.Object)
```

### 2. Programmatic Detection

```java
import java.lang.management.ManagementFactory;
import java.lang.management.ThreadMXBean;
import java.lang.management.ThreadInfo;

public class DeadlockDetector {
    public static void detectDeadlocks() {
        ThreadMXBean threadBean = ManagementFactory.getThreadMXBean();
        long[] deadlockedThreads = threadBean.findDeadlockedThreads();
        
        if (deadlockedThreads != null) {
            System.out.println("DEADLOCK DETECTED!");
            ThreadInfo[] threadInfos = threadBean.getThreadInfo(deadlockedThreads);
            
            for (ThreadInfo info : threadInfos) {
                System.out.println("Thread: " + info.getThreadName());
                System.out.println("Locked on: " + info.getLockName());
                System.out.println("Waiting for: " + info.getLockOwnerName());
                System.out.println();
            }
        } else {
            System.out.println("No deadlocks detected");
        }
    }
}
```

## Deadlock Recovery

1. **Process Termination**
   - Abort one or more threads to break the cycle
   - Choose victim based on priority, resource usage, etc.

2. **Resource Preemption**
   - Forcibly take resources from some threads
   - Rollback thread state and restart

3. **Timeout and Retry**
   - Detect timeout in lock acquisition
   - Release all locks and retry

## Real-World Example: Dining Philosophers Problem

```java
import java.util.concurrent.Semaphore;

public class DiningPhilosophers {
    static class Fork {
        private Semaphore semaphore = new Semaphore(1);
        
        public void pickUp() throws InterruptedException {
            semaphore.acquire();
        }
        
        public void putDown() {
            semaphore.release();
        }
    }
    
    static class Philosopher implements Runnable {
        private int id;
        private Fork leftFork;
        private Fork rightFork;
        
        public Philosopher(int id, Fork left, Fork right) {
            this.id = id;
            this.leftFork = left;
            this.rightFork = right;
        }
        
        public void run() {
            try {
                while (true) {
                    think();
                    eat();
                }
            } catch (InterruptedException e) {
                Thread.currentThread().interrupt();
            }
        }
        
        private void think() throws InterruptedException {
            System.out.println("Philosopher " + id + " is thinking");
            Thread.sleep((long) (Math.random() * 1000));
        }
        
        private void eat() throws InterruptedException {
            // Solution: Always pick up lower-numbered fork first
            Fork first = (id % 2 == 0) ? leftFork : rightFork;
            Fork second = (id % 2 == 0) ? rightFork : leftFork;
            
            first.pickUp();
            second.pickUp();
            
            System.out.println("Philosopher " + id + " is eating");
            Thread.sleep((long) (Math.random() * 1000));
            
            second.putDown();
            first.putDown();
        }
    }
}
```

## Best Practices

1. **Consistent Lock Ordering**: Always acquire locks in the same order
2. **Use Timeouts**: Implement timeouts when acquiring locks  
3. **Minimize Lock Scope**: Hold locks for the shortest time possible
4. **Avoid Nested Locks**: Reduce complexity by avoiding nested locking
5. **Use Higher-Level Constructs**: Prefer concurrent collections and executors
6. **Document Locking Strategy**: Clearly document lock ordering requirements
7. **Test Thoroughly**: Use stress testing to uncover potential deadlocks
8. **Monitor Production**: Use deadlock detection tools in production

## Common Pitfalls

1. **Inconsistent Lock Order**: Different threads acquiring locks in different orders
2. **Hidden Dependencies**: Calling external methods while holding locks
3. **Dynamic Lock Acquisition**: Acquiring locks based on runtime conditions
4. **Forgotten Unlock**: Not releasing locks in finally blocks or RAII
5. **Too Many Locks**: Overly fine-grained locking increases deadlock risk

## Tools for Deadlock Detection

- **Java**: `jstack`, `JConsole`, `VisualVM`
- **C#**: `WinDbg`, `dotMemory`, Visual Studio Debugger
- **C++**: `gdb`, `Valgrind` (helgrind), Thread Sanitizer

## Summary

**Prevention**:
- Lock ordering
- Lock timeout
- Atomic lock acquisition

**Detection**:
- Thread dumps
- Programmatic detection
- Monitoring tools

**Recovery**:
- Process termination
- Resource preemption
- Timeout and retry

The best approach is **prevention** through careful design and consistent lock ordering.
