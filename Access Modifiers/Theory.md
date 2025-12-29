# Access Modifiers

## Definition

**Access Modifiers** (also called access specifiers) are keywords that set the accessibility of classes, methods, and other members. They control which parts of your program can access the members of a class.

## Purpose

- **Encapsulation**: Hide implementation details
- **Security**: Prevent unauthorized access
- **Maintainability**: Control what can be changed from outside
- **Design**: Define public API vs internal implementation

## Access Modifiers by Language

### Java Access Modifiers

| Modifier | Class | Package | Subclass | World |
|----------|-------|---------|----------|-------|
| **public** | ✓ | ✓ | ✓ | ✓ |
| **protected** | ✓ | ✓ | ✓ | ✗ |
| **default** (no modifier) | ✓ | ✓ | ✗ | ✗ |
| **private** | ✓ | ✗ | ✗ | ✗ |

### C# Access Modifiers

| Modifier | Description |
|----------|-------------|
| **public** | Accessible from anywhere |
| **private** | Accessible only within the same class |
| **protected** | Accessible within the same class and derived classes |
| **internal** | Accessible within the same assembly |
| **protected internal** | Accessible within same assembly OR derived classes |
| **private protected** | Accessible within same assembly AND derived classes |

### C++ Access Modifiers

| Modifier | Description |
|----------|-------------|
| **public** | Accessible from anywhere |
| **private** | Accessible only within the same class |
| **protected** | Accessible within the same class and derived classes |

## 1. Public

Accessible from anywhere.

### Java
```java
public class Person {
    public String name; // Accessible anywhere
    
    public void introduce() { // Accessible anywhere
        System.out.println("Hello, I'm " + name);
    }
}

// Usage from another class/package
Person p = new Person();
p.name = "John"; // OK - public field
p.introduce(); // OK - public method
```

### C#
```csharp
public class Person {
    public string Name { get; set; } // Public property
    
    public void Introduce() {
        Console.WriteLine($"Hello, I'm {Name}");
    }
}
```

### C++
```cpp
class Person {
public:
    std::string name;
    
    void introduce() {
        std::cout << "Hello, I'm " << name << std::endl;
    }
};
```

## 2. Private

Accessible only within the same class.

### Java
```java
public class BankAccount {
    private double balance; // Only accessible within this class
    
    private void auditLog(String message) {
        // Internal helper method
        System.out.println("Audit: " + message);
    }
    
    public void deposit(double amount) {
        balance += amount; // Can access private field
        auditLog("Deposited " + amount); // Can call private method
    }
    
    public double getBalance() {
        return balance; // Can access private field
    }
}

// Usage
BankAccount account = new BankAccount();
account.deposit(100); // OK
// account.balance = 1000000; // ERROR - private field
// account.auditLog("test"); // ERROR - private method
```

### C#
```csharp
public class BankAccount {
    private double _balance; // Private field
    
    private void AuditLog(string message) {
        Console.WriteLine($"Audit: {message}");
    }
    
    public void Deposit(double amount) {
        _balance += amount;
        AuditLog($"Deposited {amount}");
    }
    
    public double Balance => _balance; // Public read-only property
}
```

### C++
```cpp
class BankAccount {
private:
    double balance;
    
    void auditLog(const std::string& message) {
        std::cout << "Audit: " << message << std::endl;
    }
    
public:
    void deposit(double amount) {
        balance += amount;
        auditLog("Deposited");
    }
    
    double getBalance() const {
        return balance;
    }
};
```

## 3. Protected

Accessible within the same class and subclasses.

### Java
```java
public class Animal {
    protected String species; // Accessible to subclasses
    
    protected void move() {
        System.out.println(species + " is moving");
    }
}

public class Dog extends Animal {
    public void bark() {
        species = "Canine"; // OK - can access protected field
        move(); // OK - can call protected method
        System.out.println("Woof!");
    }
}

// Usage
Dog dog = new Dog();
dog.bark(); // OK
// dog.species = "Feline"; // ERROR - protected (outside package)
```

### C#
```csharp
public class Animal {
    protected string Species { get; set; }
    
    protected void Move() {
        Console.WriteLine($"{Species} is moving");
    }
}

public class Dog : Animal {
    public void Bark() {
        Species = "Canine"; // OK - can access protected property
        Move(); // OK - can call protected method
        Console.WriteLine("Woof!");
    }
}
```

### C++
```cpp
class Animal {
protected:
    std::string species;
    
    void move() {
        std::cout << species << " is moving\n";
    }
};

class Dog : public Animal {
public:
    void bark() {
        species = "Canine"; // OK - can access protected member
        move(); // OK - can call protected method
        std::cout << "Woof!\n";
    }
};
```

## 4. Default / Package-Private (Java)

Accessible within the same package only (no modifier in Java).

### Java
```java
// File: com/example/Employee.java
package com.example;

class Employee { // Default (package-private) class
    String name; // Default field
    
    void work() { // Default method
        System.out.println(name + " is working");
    }
}

// File: com/example/Company.java
package com.example;

public class Company {
    public void hire() {
        Employee emp = new Employee(); // OK - same package
        emp.name = "John"; // OK - same package
        emp.work(); // OK - same package
    }
}

// File: com/other/Main.java
package com.other;

public class Main {
    public void test() {
        // Employee emp = new Employee(); // ERROR - different package
    }
}
```

## 5. Internal (C#)

Accessible within the same assembly.

### C#
```csharp
// Assembly A
internal class DatabaseHelper {
    internal void Connect() {
        Console.WriteLine("Connecting to database");
    }
}

public class DataService {
    public void FetchData() {
        var helper = new DatabaseHelper(); // OK - same assembly
        helper.Connect(); // OK - same assembly
    }
}

// Assembly B (different project)
public class ExternalClass {
    public void Test() {
        // var helper = new DatabaseHelper(); // ERROR - different assembly
    }
}
```

## 6. Protected Internal (C#)

Accessible within same assembly OR from derived classes.

### C#
```csharp
// Assembly A
public class BaseClass {
    protected internal int Value { get; set; }
}

public class SameAssemblyClass {
    public void Access() {
        var obj = new BaseClass();
        obj.Value = 10; // OK - same assembly
    }
}

// Assembly B
public class DerivedClass : BaseClass {
    public void Access() {
        Value = 20; // OK - derived class
    }
}

public class OtherClass {
    public void Access() {
        var obj = new BaseClass();
        // obj.Value = 30; // ERROR - different assembly and not derived
    }
}
```

## When to Use Each

### Public
- Public API
- Methods clients need to call
- Properties that should be accessible

### Private
- Implementation details
- Internal helper methods
- Fields (encapsulation)
- Constants used only within the class

### Protected
- Methods that subclasses should override
- Fields that subclasses need to access
- Designed for inheritance

### Default/Package-Private (Java)
- Classes used only within a package
- Internal implementation classes

### Internal (C#)
- Classes used only within an assembly
- Implementation details of a library

## Best Practices

1. **Least Privilege Principle**: Use the most restrictive access level possible
2. **Encapsulation**: Make fields private, expose through properties/methods
3. **Public API**: Keep public API minimal and stable
4. **Protected for Extension**: Use protected for members meant to be overridden
5. **Avoid Public Fields**: Use properties instead (except constants)
6. **Document Public Members**: All public members should be documented
7. **Consider Internal**: Use internal for implementation details in C#

## Common Patterns

### Encapsulation Pattern
```java
public class Person {
    private String name; // Private field
    private int age;
    
    // Public getter
    public String getName() {
        return name;
    }
    
    // Public setter with validation
    public void setName(String name) {
        if (name != null && !name.isEmpty()) {
            this.name = name;
        }
    }
    
    public int getAge() {
        return age;
    }
    
    public void setAge(int age) {
        if (age > 0 && age < 150) {
            this.age = age;
        }
    }
}
```

### Protected Template Method Pattern
```java
public abstract class DataProcessor {
    // Public template method
    public final void process() {
        loadData();
        processData();
        saveData();
    }
    
    // Protected methods for subclasses to override
    protected abstract void loadData();
    protected abstract void processData();
    protected abstract void saveData();
}
```

## Summary

- **Public**: Accessible everywhere - use for public API
- **Private**: Accessible only in same class - use for implementation details
- **Protected**: Accessible in same class and subclasses - use for inheritance
- **Default/Package-Private**: Accessible in same package (Java)
- **Internal**: Accessible in same assembly (C#)
- **Use least restrictive access** needed for proper encapsulation
