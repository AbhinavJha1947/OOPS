# Race Conditions

## Definition

A **race condition** occurs when two or more threads access shared data concurrently, and at least one thread modifies the data, leading to unpredictable and incorrect results. The outcome depends on the relative timing of thread execution.

## Example of Race Condition

```java
public class RaceConditionExample {
    private int counter = 0;
    
    public void increment() {
        counter++; // NOT atomic! Has 3 steps:
                   // 1. Read counter
                   // 2. Add 1
                   // 3. Write counter
    }
    
    public static void main(String[] args) throws InterruptedException {
        RaceConditionExample example = new RaceConditionExample();
        
        Thread t1 = new Thread(() -> {
            for (int i = 0; i < 10000; i++) {
                example.increment();
            }
        });
        
        Thread t2 = new Thread(() -> {
            for (int i = 0; i < 10000; i++) {
                example.increment();
            }
        });
        
        t1.start();
        t2.start();
        t1.join();
        t2.join();
        
        System.out.println("Counter: " + example.counter);
        // Expected: 20000, Actual: varies (e.g., 18234)
    }
}
```

## Solutions

### 1. Synchronization

```java
public class SynchronizedSolution {
    private int counter = 0;
    
    public synchronized void increment() {
        counter++;
    }
}
```

### 2. Atomic Variables

```java
import java.util.concurrent.atomic.AtomicInteger;

public class AtomicSolution {
    private AtomicInteger counter = new AtomicInteger(0);
    
    public void increment() {
        counter.incrementAndGet();
    }
}
```

### 3. Lock

```java
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;

public class LockSolution {
    private int counter = 0;
    private Lock lock = new ReentrantLock();
    
    public void increment() {
        lock.lock();
        try {
            counter++;
        } finally {
            lock.unlock();
        }
    }
}
```

## Best Practices

1. Identify and protect shared mutable state
2. Use thread-safe data structures
3. Minimize shared data
4. Prefer immutable objects
5. Use atomic operations for simple counters
