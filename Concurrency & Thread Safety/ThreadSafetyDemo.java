// Shared resource that is NOT thread-safe
class UnsafeCounter {
    private int count = 0;

    public void increment() {
        count++; // Not atomic! Read -> Modify -> Write
    }

    public int getCount() {
        return count;
    }
}

// Shared resource that IS thread-safe
class SafeCounter {
    private int count = 0;
    private final Object lock = new Object();

    // Method 1: Synchronized method
    public synchronized void incrementSyncMethod() {
        count++;
    }

    // Method 2: Synchronized block (Fine-grained locking)
    public void incrementSyncBlock() {
        synchronized (lock) {
            count++;
        }
    }

    public int getCount() {
        return count;
    }
}

public class ThreadSafetyDemo {
    public static void main(String[] args) throws InterruptedException {
        System.out.println("--- Java Concurrency & Thread Safety Demo ---");

        // 1. Demonstrate Race Condition
        UnsafeCounter unsafeCounter = new UnsafeCounter();
        runThreads(unsafeCounter::increment);
        System.out.println("Unsafe Counter Value (Expected 2000): " + unsafeCounter.getCount());
        System.out.println("NOTE: If value < 2000, a Race Condition occurred.\n");

        // 2. Demonstrate Thread Safety
        SafeCounter safeCounter = new SafeCounter();
        runThreads(safeCounter::incrementSyncMethod);
        System.out.println("Safe Counter Value (Expected 2000): " + safeCounter.getCount());
    }

    // Helper to run two threads that increment a counter 1000 times each
    private static void runThreads(Runnable incrementTask) throws InterruptedException {
        Thread t1 = new Thread(() -> {
            for (int i = 0; i < 1000; i++)
                incrementTask.run();
        });

        Thread t2 = new Thread(() -> {
            for (int i = 0; i < 1000; i++)
                incrementTask.run();
        });

        t1.start();
        t2.start();

        t1.join();
        t2.join();
    }
}
