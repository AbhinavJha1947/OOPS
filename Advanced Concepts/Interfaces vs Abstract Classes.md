# Interfaces vs Abstract Classes

## Definitions

**Interface**: A contract that defines what a class must do, but not how it does it. Contains method signatures without implementation (traditionally).

**Abstract Class**: A class that cannot be instantiated and may contain both abstract methods (without implementation) and concrete methods (with implementation).

## Key Differences

| Feature | Interface | Abstract Class |
|---------|-----------|----------------|
| **Methods** | Only abstract methods (pre-Java 8/C# 8) | Can have both abstract and concrete methods |
| **Multiple Inheritance** | A class can implement multiple interfaces | A class can extend only one abstract class |
| **Fields** | Only `public static final` constants | Can have instance variables with any access modifier |
| **Constructors** | Cannot have constructors | Can have constructors |
| **Access Modifiers** | Methods are implicitly `public` | Methods can have any access modifier |
| **Implementation** | No implementation (traditionally) | Can provide partial implementation |
| **Usage** | "Can-do" relationship | "Is-a" relationship |
| **When to Use** | Define capabilities across unrelated classes | Share code among related classes |

## Java Examples

### Interface

```java
// Interface - defines what to do
public interface Flyable {
    void fly(); // Abstract method
    void land(); // Abstract method
    
    // Java 8+: Default method
    default void glide() {
        System.out.println("Gliding...");
    }
    
    // Java 8+: Static method
    static void checkWeather() {
        System.out.println("Weather is good for flying");
    }
    
    // Constants (implicitly public static final)
    int MAX_ALTITUDE = 40000;
}

// Multiple interface implementation
public class Airplane implements Flyable, Drivable {
    @Override
    public void fly() {
        System.out.println("Airplane flying");
    }
    
    @Override
    public void land() {
        System.out.println("Airplane landing");
    }
    
    @Override
    public void drive() {
        System.out.println("Airplane taxiing");
    }
}
```

### Abstract Class

```java
// Abstract class - provides partial implementation
public abstract class Animal {
    private String name; // Instance variable
    private int age;
    
    // Constructor
    public Animal(String name, int age) {
        this.name = name;
        this.age = age;
    }
    
    // Concrete method (with implementation)
    public void sleep() {
        System.out.println(name + " is sleeping");
    }
    
    // Abstract method (must be implemented by subclass)
    public abstract void makeSound();
    
    // Concrete method using abstract method
    public void wakeUp() {
        System.out.println(name + " waking up");
        makeSound();
    }
    
    public String getName() {
        return name;
    }
}

// Single inheritance only
public class Dog extends Animal {
    public Dog(String name, int age) {
        super(name, age);
    }
    
    @Override
    public void makeSound() {
        System.out.println("Woof! Woof!");
    }
}
```

## C# Examples

### Interface

```csharp
// Interface
public interface IPaymentProcessor {
    void ProcessPayment(decimal amount);
    bool ValidatePayment(decimal amount);
    
    // C# 8+: Default implementation
    decimal CalculateFee(decimal amount) {
        return amount * 0.02m;
    }
}

// Multiple interface implementation
public class CreditCardProcessor : IPaymentProcessor, IRefundable {
    public void ProcessPayment(decimal amount) {
        Console.WriteLine($"Processing credit card payment: ${amount}");
    }
    
    public bool ValidatePayment(decimal amount) {
        return amount > 0 && amount < 10000;
    }
    
    public void ProcessRefund(decimal amount) {
        Console.WriteLine($"Refunding: ${amount}");
    }
}
```

### Abstract Class

```csharp
// Abstract class
public abstract class Vehicle {
    public string Model { get; set; }
    protected int speed;
    
    // Constructor
    public Vehicle(string model) {
        Model = model;
        speed = 0;
    }
    
    // Concrete method
    public void Start() {
        Console.WriteLine($"{Model} is starting");
    }
    
    // Abstract method
    public abstract void Accelerate();
    
    // Virtual method (can be overridden)
    public virtual void Stop() {
        speed = 0;
        Console.WriteLine($"{Model} stopped");
    }
}

public class Car : Vehicle {
    public Car(string model) : base(model) { }
    
    public override void Accelerate() {
        speed += 10;
        Console.WriteLine($"Car accelerating, speed: {speed}");
    }
}
```

## C++ Examples

### Interface (Pure Abstract Class)

```cpp
// C++ doesn't have interfaces, but we can create pure abstract classes
class IShape {
public:
    virtual double getArea() const = 0; // Pure virtual function
    virtual double getPerimeter() const = 0;
    virtual void draw() const = 0;
    virtual ~IShape() = default; // Virtual destructor
};

class Circle : public IShape {
private:
    double radius;
    
public:
    Circle(double r) : radius(r) {}
    
    double getArea() const override {
        return 3.14159 * radius * radius;
    }
    
    double getPerimeter() const override {
        return 2 * 3.14159 * radius;
    }
    
    void draw() const override {
        std::cout << "Drawing circle\n";
    }
};
```

### Abstract Class

```cpp
class Animal {
protected:
    std::string name;
    int age;
    
public:
    Animal(const std::string& n, int a) : name(n), age(a) {}
    
    // Concrete method
    void sleep() const {
        std::cout << name << " is sleeping\n";
    }
    
    // Pure virtual (abstract) method
    virtual void makeSound() const = 0;
    
    // Virtual destructor
    virtual ~Animal() = default;
    
    std::string getName() const { return name; }
};

class Cat : public Animal {
public:
    Cat(const std::string& name, int age) : Animal(name, age) {}
    
    void makeSound() const override {
        std::cout << "Meow!\n";
    }
};
```

## When to Use

### Use Interface When:
- You want to define a contract for unrelated classes
- You need multiple inheritance of type
- You want to specify capabilities (can-do relationship)
- You don't have shared implementation
- Examples: `Comparable`, `Serializable`, `Closeable`

### Use Abstract Class When:
- You want to share code among closely related classes
- You need to declare non-static or non-final fields
- You need constructors
- You have common implementation to share
- You want to provide default behavior
- Examples: `InputStream`, `AbstractList`, `HttpServlet`

## Modern Features (Java 8+, C# 8+)

### Java 8+ Interface Enhancements

```java
public interface SmartDevice {
    // Abstract method
    void turnOn();
    
    // Default method (provides default implementation)
    default void updateFirmware() {
        System.out.println("Updating firmware...");
    }
    
    // Static method
    static void printManufacturer() {
        System.out.println("Generic Smart Device");
    }
    
    // Java 9+: Private method
    private void log(String message) {
        System.out.println("Log: " + message);
    }
}
```

### C# 8+ Default Interface Members

```csharp
public interface ILogger {
    void Log(string message);
    
    // Default implementation
    void LogError(string error) {
        Log($"ERROR: {error}");
    }
    
    // Properties with default implementation
    string LogLevel => "INFO";
}
```

## Real-World Analogy

**Interface**: Like a contract or job description
- "Must be able to fly"
- "Must  be able to swim"
- Doesn't specify HOW to do it

**Abstract Class**: Like a template or base implementation
- "All animals eat and sleep (common behavior)"
- "Each animal makes its own sound (specific behavior)"
- Provides SOME implementation, requires MORE

## Best Practices

1. **Prefer interfaces** for defining types and contracts
2. **Use abstract classes** when you have shared implementation
3. **Keep interfaces focused** - Interface Segregation Principle
4. **Don't use abstract classes** just to enforce method implementation
5. **Use marker interfaces** sparingly (empty interfaces like `Serializable`)
6. **Document abstract methods** clearly for implementers

## Summary

- **Interfaces** define contracts (what to do)
- **Abstract classes** provide templates (what and how to do)
- **Interfaces** support multiple inheritance
- **Abstract classes** allow code sharing
- **Modern languages** blur the distinction with default methods
- **Choose based on** whether you're defining a contract or sharing code
