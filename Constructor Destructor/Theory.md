# Constructors and Destructors

## Constructors

### Definition
A **constructor** is a special method that is automatically called when an object is created. It initializes the object's state and allocates resources.

### Characteristics
- Same name as the class
- No return type (not even void)
- Called automatically when object is created
- Can be overloaded
- Cannot be inherited
- Cannot be virtual (in most languages)

## Types of Constructors

### 1. Default Constructor
A constructor with no parameters.

```java
public class Person {
    String name;
    int age;
    
    // Default constructor
    public Person() {
        name = "Unknown";
        age = 0;
    }
}
```

### 2. Parameterized Constructor
A constructor that accepts parameters.

```java
public class Person {
    String name;
    int age;
    
    // Parameterized constructor
    public Person(String name, int age) {
        this.name = name;
        this.age = age;
    }
}
```

### 3. Copy Constructor
Creates an object by copying another object of the same class.

```java
public class Person {
    String name;
    int age;
    
    // Copy constructor
    public Person(Person other) {
        this.name = other.name;
        this.age = other.age;
    }
}
```

## Constructor Chaining

Calling one constructor from another using `this()` or `super()`.

```java
public class Employee {
    String name;
    int id;
    double salary;
    
    // Constructor 1
    public Employee() {
        this("Unknown", 0, 0.0);  // Calls constructor 3
    }
    
    // Constructor 2
    public Employee(String name) {
        this(name, 0, 0.0);  // Calls constructor 3
    }
    
    // Constructor 3
    public Employee(String name, int id, double salary) {
        this.name = name;
        this.id = id;
        this.salary = salary;
    }
}
```

## Destructors / Finalizers

### Definition
A **destructor** (C++) or **finalizer** (Java, C#) is a special method called when an object is destroyed to clean up resources.

### C++ Destructor
```cpp
class Resource {
public:
    Resource() {
        // Allocate resources
        data = new int[100];
    }
    
    ~Resource() {  // Destructor
        // Clean up resources
        delete[] data;
    }
    
private:
    int* data;
};
```

### Java Finalizer (Deprecated)
```java
public class Resource {
    protected void finalize() throws Throwable {
        try {
            // Clean up resources
        } finally {
            super.finalize();
        }
    }
}
```

**Note**: Java finalizers are deprecated. Use try-with-resources or `AutoCloseable` instead.

### C# Finalizers
```csharp
public class Resource {
    ~Resource() {  // Finalizer
        // Clean up unmanaged resources
    }
}
```

## RAII (Resource Acquisition Is Initialization)

A C++ programming technique where resource allocation is tied to object lifetime.

```cpp
class File {
    FILE* handle;
    
public:
    File(const char* filename) {
        handle = fopen(filename, "r");  // Acquire resource
    }
    
    ~File() {
        if (handle) fclose(handle);  // Release resource
    }
};

// Usage
{
    File f("data.txt");
    // Use file
} // Destructor automatically closes file
```

## Constructor Rules

### Java
- If no constructor is defined, compiler provides a default constructor
- First line must be `this()` or `super()` (implicit if not specified)
- Constructors cannot be synchronized, static, final, or abstract

### C#
- If no constructor is defined, compiler provides a default constructor
- Can use constructor initializers `: base()` or `: this()`
- Constructors can be private (for singleton pattern)

### C++
- If no constructor is defined, compiler provides a default constructor
- Can use member initializer lists
- Destructors called in reverse order of construction

## Best Practices

1. **Initialize all fields** in constructors
2. **Use constructor chaining** to avoid code duplication
3. **Validate parameters** in constructors
4. **Prefer initialization lists** (C++) over assignment
5. **Don't call virtual methods** in constructors
6. **Use factory methods** for complex object creation
7. **Implement IDisposable/AutoCloseable** instead of finalizers
8. **Follow RAII** in C++ for automatic resource management

## Comparison Table

| Feature | Java | C# | C++ |
|---------|------|----| ----|
| **Syntax** | `ClassName()` | `ClassName()` | `ClassName()` |
| **Destructor** | Finalizer (deprecated) | Finalizer `~ClassName()` | Destructor `~ClassName()` |
| **Resource Cleanup** | try-with-resources | IDisposable pattern | RAII |
| **Constructor Chaining** | `this()`, `super()` | `: this()`, `: base()` | Member initializer list |
| **Default Constructor** | Auto-generated if none exists | Auto-generated if none exists | Auto-generated if none exists |
