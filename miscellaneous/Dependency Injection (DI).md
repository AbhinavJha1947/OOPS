# Dependency Injection (DI)

## Definition

**Dependency Injection** is a design pattern where an object's dependencies are provided (injected) from the outside rather than created internally. This promotes loose coupling, testability, and adherence to the Dependency Inversion Principle.

## Without Dependency Injection (Tightly Coupled)

```java
// BAD: Tight coupling
public class UserService {
    private EmailService emailService = new EmailService(); // Hard-coded dependency
    private UserRepository repository = new MySQLUserRepository(); // Hard-coded dependency
    
    public void registerUser(String email) {
        repository.save(new User(email));
        emailService.sendWelcomeEmail(email);
    }
}

// Problems:
// 1. Cannot change EmailService implementation
// 2. Hard to test (cannot mock dependencies)
// 3. Violates Dependency Inversion Principle
```

## With Dependency Injection (Loosely Coupled)

```java
// GOOD: Loose coupling through DI
public class UserService {
    private final IEmailService emailService;
    private final IUserRepository repository;
    
    // Dependencies injected through constructor
    public UserService(IEmailService emailService, IUserRepository repository) {
        this.emailService = emailService;
        this.repository = repository;
    }
    
    public void registerUser(String email) {
        repository.save(new User(email));
        emailService.sendWelcomeEmail(email);
    }
}

// Benefits:
// 1. Can easily swap implementations
// 2. Easy to test with mocks
// 3. Follows Dependency Inversion Principle
```

## Types of Dependency Injection

### 1. Constructor Injection (Recommended)

Dependencies are provided through the constructor.

```java
public class OrderService {
    private final IPaymentProcessor paymentProcessor;
    private final IInventoryService inventoryService;
    
    // Constructor Injection
    public OrderService(IPaymentProcessor paymentProcessor, IInventoryService inventoryService) {
        this.paymentProcessor = paymentProcessor;
        this.inventoryService = inventoryService;
    }
    
    public void processOrder(Order order) {
        inventoryService.reserve(order.getItems());
        paymentProcessor.process(order.getAmount());
    }
}
```

**Advantages**:
- Mandatory dependencies are explicit
- Immutable objects (final fields)
- Easy to test
- Compile-time safety

### 2. Setter Injection (Property Injection)

Dependencies are provided through setter methods.

```java
public class ReportGenerator {
    private IDataSource dataSource;
    private IFormatter formatter;
    
    // Setter Injection
    public void setDataSource(IDataSource dataSource) {
        this.dataSource = dataSource;
    }
    
    public void setFormatter(IFormatter formatter) {
        this.formatter = formatter;
    }
    
    public String generateReport() {
        Data data = dataSource.fetch();
        return formatter.format(data);
    }
}
```

**Advantages**:
- Optional dependencies
- Can change dependencies at runtime

**Disadvantages**:
- Object may be in incomplete state
- Not immutable

### 3. Method Injection (Parameter Injection)

Dependencies are provided as method parameters.

```java
public class DocumentProcessor {
    public void processDocument(Document doc, IValidator validator) {
        if (validator.validate(doc)) {
            // Process document
        }
    }
}
```

**Use Case**: When dependency varies per method call.

## Inversion of Control (IoC) Containers

IoC containers automatically manage object creation and dependency injection.

### Java - Spring Framework

```java
// Define interfaces
public interface IEmailService {
    void send(String to, String message);
}

// Implementation
@Component
public class SmtpEmailService implements IEmailService {
    public void send(String to, String message) {
        System.out.println("Sending email to: " + to);
    }
}

// Service with dependencies
@Service
public class UserService {
    private final IEmailService emailService;
    
    @Autowired // Spring automatically injects dependency
    public UserService(IEmailService emailService) {
        this.emailService = emailService;
    }
    
    public void registerUser(String email) {
        emailService.send(email, "Welcome!");
    }
}

// Configuration
@Configuration
public class AppConfig {
    @Bean
    public IEmailService emailService() {
        return new SmtpEmailService();
    }
}
```

### C# - .NET Core DI

```csharp
// Interface
public interface IEmailService {
    void Send(string to, string message);
}

// Implementation
public class SmtpEmailService : IEmailService {
    public void Send(string to, string message) {
        Console.WriteLine($"Sending email to: {to}");
    }
}

// Service
public class UserService {
    private readonly IEmailService _emailService;
    
    public UserService(IEmailService emailService) {
        _emailService = emailService;
    }
    
    public void RegisterUser(string email) {
        _emailService.Send(email, "Welcome!");
    }
}

// Program.cs - Register services
var builder = WebApplication.CreateBuilder(args);

// Register dependencies
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();
```

## Service Lifetimes

### 1. Transient
New instance created every time it's requested.

```csharp
services.AddTransient<IEmailService, SmtpEmailService>();
```

**Use Case**: Lightweight, stateless services.

### 2. Scoped
One instance per request/scope.

```csharp
services.AddScoped<IUserRepository, UserRepository>();
```

**Use Case**: Per-request services (e.g., database context).

### 3. Singleton
Single instance for the application lifetime.

```csharp
services.AddSingleton<IConfigurationService, ConfigurationService>();
```

**Use Case**: Shared state, expensive to create.

## Benefits of Dependency Injection

1. **Loose Coupling**: Classes depend on abstractions, not concrete implementations
2. **Testability**: Easy to mock dependencies for unit testing
3. **Maintainability**: Changes to dependencies don't require changes to dependent classes
4. **Flexibility**: Easy to swap implementations
5. **Separation of Concerns**: Creation logic separated from business logic
6. **Configuration**: Easily configure different implementations for different environments

## Testing with DI

```java
// Production code
public class OrderService {
    private final IPaymentProcessor paymentProcessor;
    
    public OrderService(IPaymentProcessor paymentProcessor) {
        this.paymentProcessor = paymentProcessor;
    }
    
    public boolean placeOrder(Order order) {
        return paymentProcessor.process(order.getAmount());
    }
}

// Test code
@Test
public void testOrderPlacement() {
    // Mock dependency
    IPaymentProcessor mockProcessor = mock(IPaymentProcessor.class);
    when(mockProcessor.process(any())).thenReturn(true);
    
    // Inject mock into service
    OrderService service = new OrderService(mockProcessor);
    
    // Test
    boolean result = service.placeOrder(new Order(100));
    assertTrue(result);
    verify(mockProcessor).process(100);
}
```

## Dependency Inversion Principle (SOLID)

**Definition**: High-level modules should not depend on low-level modules. Both should depend on abstractions.

```java
// BAD: High-level depends on low-level
public class ReportService {
    private MySQLDatabase database = new MySQLDatabase(); // Concrete dependency
}

// GOOD: Both depend on abstraction
public interface IDatabase {
    Data query(String sql);
}

public class MySQLDatabase implements IDatabase {
    public Data query(String sql) { /* MySQL implementation */ }
}

public class ReportService {
    private final IDatabase database; // Abstract dependency
    
    public ReportService(IDatabase database) {
        this.database = database;
    }
}
```

## Best Practices

1. **Prefer Constructor Injection** for required dependencies
2. **Depend on Abstractions** (interfaces/abstract classes) not concrete classes
3. **Keep Dependencies Minimal** - follow Single Responsibility Principle
4. **Use IoC Containers** for complex applications
5. **Document Dependencies** clearly in class documentation
6. **Avoid Service Locator Pattern** - prefer explicit injection
7. **Register by Interface** not by concrete type
8. **Avoid Circular Dependencies** - indicates design problem

## Common Pitfalls

1. **Over-injection**: Too many constructor parameters (usually > 5)
2. **Hidden Dependencies**: Using static methods or service locators
3. **New Keyword Everywhere**: Creating dependencies with `new` instead of injecting
4. **Wrong Lifetime**: Using singleton for stateful services
5. **Circular Dependencies**: Class A depends on B, B depends on A

## Summary

- **DI** is a pattern where dependencies are provided externally
- **Constructor Injection** is preferred for required dependencies
- **IoC Containers** automate dependency management
- **Benefits** include loose coupling, testability, and flexibility
- **Follow** SOLID principles, especially Dependency Inversion
