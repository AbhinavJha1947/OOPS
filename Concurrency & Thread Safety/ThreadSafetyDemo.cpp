#include <iostream>
#include <thread>
#include <vector>
#include <mutex> // For mutex
#include <atomic> // For atomic

using namespace std;

// 1. Unsafe Counter (Race Condition)
class UnsafeCounter {
public:
    int count = 0;
    void increment() {
        count++; // Read-Modify-Write is not atomic
    }
};

// 2. Safe Counter using Mutex
class SafeCounterOnlyMutex {
public:
    int count = 0;
    mutex mtx;

    void increment() {
        // lock_guard automatically locks when created and unlocks when destroyed (RAII)
        lock_guard<mutex> lock(mtx);
        count++;
    }
};

// 3. Safe Counter using Atomics
class SafeCounterAtomic {
public:
    atomic<int> count{0};

    void increment() {
        count++; // Atomic operation
    }
};

void runThreadsUnsafe(UnsafeCounter& counter) {
    auto task = [&counter]() {
        for (int i = 0; i < 1000; ++i) {
            counter.increment();
        }
    };

    thread t1(task);
    thread t2(task);

    t1.join();
    t2.join();
}

void runThreadsSafe(SafeCounterOnlyMutex& counter) {
    auto task = [&counter]() {
        for (int i = 0; i < 1000; ++i) {
            counter.increment();
        }
    };

    thread t1(task);
    thread t2(task);

    t1.join();
    t2.join();
}

void runThreadsAtomic(SafeCounterAtomic& counter) {
    auto task = [&counter]() {
        for (int i = 0; i < 1000; ++i) {
            counter.increment();
        }
    };

    thread t1(task);
    thread t2(task);

    t1.join();
    t2.join();
}

int main() {
    cout << "--- C++ Concurrency & Thread Safety Demo ---" << endl;

    // Unsafe
    UnsafeCounter unsafeObj;
    runThreadsUnsafe(unsafeObj);
    cout << "Unsafe Counter Value (Expected 2000): " << unsafeObj.count << endl;

    // Safe (Mutex)
    SafeCounterOnlyMutex safeObj;
    runThreadsSafe(safeObj);
    cout << "Safe Counter (Mutex) Value (Expected 2000): " << safeObj.count << endl;

    // Safe (Atomic)
    SafeCounterAtomic atomicObj;
    runThreadsAtomic(atomicObj);
    cout << "Safe Counter (Atomic) Value (Expected 2000): " << atomicObj.count.load() << endl;

    return 0;
}
