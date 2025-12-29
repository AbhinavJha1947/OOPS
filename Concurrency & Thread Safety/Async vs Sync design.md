# Async vs Sync Design

## Definition

- **Synchronous (Sync)**: Operations execute sequentially, blocking until completion
- **Asynchronous (Async)**: Operations execute concurrently, non-blocking, using callbacks or promises

## Synchronous Design

### Characteristics
- Simple and predictable
- Easy to debug and understand
- Blocks thread while waiting
- Can lead to poor scalability

### Java Example

```java
public class SynchronousExample {
    public String fetchData(String url) {
        // Blocks until data is fetched
        return httpClient.get(url);
    }
    
    public void processData() {
        String data1 = fetchData("http://api1.com"); // Waits
        String data2 = fetchData("http://api2.com"); // Waits
        String data3 = fetchData("http://api3.com"); // Waits
        
        // Total time = T1 + T2 + T3
        process(data1, data2, data3);
    }
}
```

## Asynchronous Design

### Characteristics
- Non-blocking, better resource utilization
- More complex to implement
- Better scalability
- Can execute operations concurrently

### Java - CompletableFuture

```java
import java.util.concurrent.CompletableFuture;

public class AsynchronousExample {
    public CompletableFuture<String> fetchDataAsync(String url) {
        return CompletableFuture.supplyAsync(() -> httpClient.get(url));
    }
    
    public void processDataAsync() {
        CompletableFuture<String> future1 = fetchDataAsync("http://api1.com");
        CompletableFuture<String> future2 = fetchDataAsync("http://api2.com");
        CompletableFuture<String> future3 = fetchDataAsync("http://api3.com");
        
        // All requests execute concurrently
        CompletableFuture.allOf(future1, future2, future3)
            .thenAccept(v -> {
                String data1 = future1.join();
                String data2 = future2.join();
                String data3 = future3.join();
                
                // Total time ≈ max(T1, T2, T3)
                process(data1, data2, data3);
            });
    }
}
```

### C# - async/await

```csharp
using System.Threading.Tasks;

public class AsynchronousExample {
    public async Task<string> FetchDataAsync(string url) {
        return await httpClient.GetStringAsync(url);
    }
    
    public async Task ProcessDataAsync() {
        // Start all requests concurrently
        Task<string> task1 = FetchDataAsync("http://api1.com");
        Task<string> task2 = FetchDataAsync("http://api2.com");
        Task<string> task3 = FetchDataAsync("http://api3.com");
        
        // Wait for all to complete
        await Task.WhenAll(task1, task2, task3);
        
        string data1 = task1.Result;
        string data2 = task2.Result;
        string data3 = task3.Result;
        
        Process(data1, data2, data3);
    }
}
```

### C++ - std::async

```cpp
#include <future>
#include <string>

class AsynchronousExample {
public:
    std::string fetchData(const std::string& url) {
        // Simulate HTTP request
        return httpClient.get(url);
    }
    
    void processDataAsync() {
        // Launch async tasks
        auto future1 = std::async(std::launch::async, 
            &AsynchronousExample::fetchData, this, "http://api1.com");
        auto future2 = std::async(std::launch::async,
            &AsynchronousExample::fetchData, this, "http://api2.com");
        auto future3 = std::async(std::launch::async,
            &AsynchronousExample::fetchData, this, "http://api3.com");
        
        // Wait for results
        std::string data1 = future1.get();
        std::string data2 = future2.get();
        std::string data3 = future3.get();
        
        process(data1, data2, data3);
    }
};
```

## Patterns

### 1. Callbacks

```java
public void fetchDataWithCallback(String url, Callback callback) {
    new Thread(() -> {
        String data = httpClient.get(url);
        callback.onSuccess(data);
    }).start();
}

interface Callback {
    void onSuccess(String data);
    void onError(Exception e);
}
```

### 2. Promises/Futures

```java
CompletableFuture<String> future = CompletableFuture.supplyAsync(() -> {
    return expensiveOperation();
});

future.thenAccept(result -> {
    System.out.println("Result: " + result);
});
```

### 3. Async/Await (C#)

```csharp
public async Task<int> CalculateAsync() {
    await Task.Delay(1000); // Non-blocking wait
    return 42;
}
```

## When to Use

**Use Synchronous**:
- Simple, short operations
- Single-threaded applications
- Easy debugging is priority
- Operations must complete in order

**Use Asynchronous**:
- I/O-bound operations (network, disk)
- Long-running operations
- Need to maximize throughput
- Building scalable services
- UI applications (keep UI responsive)

## Performance Comparison

| Operation Type | Sync Time | Async Time |
|----------------|-----------|------------|
| 3 sequential API calls (1s each) | 3 seconds | ~1 second |
| 10 database queries (100ms each) | 1 second | ~100ms |

## Best Practices

1. **Don't block async code**: Avoid `.wait()` or `.get()` in async context
2. **Use async all the way**: Don't mix sync and async unnecessarily
3. **Handle errors**: Always handle exceptions in async code
4. **Configure thread pools**: Set appropriate thread pool sizes
5. **Avoid async for CPU-bound work**: Use parallel processing instead
