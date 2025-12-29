# Immutable Objects

## Definition

An **immutable object** is an object whose state cannot be modified after it is created. All fields are final and set only once during construction. Immutable objects are inherently thread-safe because their state cannot change.

## Benefits

1. **Thread Safety**: No synchronization needed for immutable objects
2. **Simplicity**: Easier to understand and reason about
3. **Caching**: Can be safely cached and reused
4. **Security**: Cannot be modified by untrusted code
5. **No Side Effects**: Methods don't change object state

## Creating Immutable Classes

### Java

```java
public final class ImmutablePerson {
    private final String name;
    private final int age;
    private final Address address; // Must also be immutable
    
    public ImmutablePerson(String name, int age, Address address) {
        this.name = name;
        this.age = age;
        // Defensive copy to prevent external modification
        this.address = new Address(address);
    }
    
    public String getName() {
        return name;
    }
    
    public int getAge() {
        return age;
    }
    
    public Address getAddress() {
        // Return defensive copy
        return new Address(address);
    }
    
    // No setters!
}

public final class Address {
    private final String street;
    private final String city;
    
    public Address(String street, String city) {
        this.street = street;
        this.city = city;
    }
    
    public Address(Address other) {
        this.street = other.street;
        this.city = other.city;
    }
    
    public String getStreet() { return street; }
    public String getCity() { return city; }
}
```

### C#

```csharp
public sealed class ImmutablePerson {
    public string Name { get; }
    public int Age { get; }
    public DateTime BirthDate { get; }
    
    public ImmutablePerson(string name, int age, DateTime birthDate) {
        Name = name;
        Age = age;
        BirthDate = birthDate;
    }
    
    // Immutable update - returns new instance
    public ImmutablePerson WithAge(int newAge) {
        return new ImmutablePerson(Name, newAge, BirthDate);
    }
}

// C# 9+: Records (immutable by default)
public record Person(string Name, int Age, DateTime BirthDate);
```

### C++

```cpp
class ImmutablePerson {
private:
    const std::string name;
    const int age;
    
public:
    ImmutablePerson(const std::string& n, int a)
        : name(n), age(a) {}
    
    std::string getName() const { return name; }
    int getAge() const { return age; }
    
    // No setters!
    // Copy constructor and assignment can be defaulted or deleted
};
```

## Built-in Immutable Classes

### Java
- `String`
- `Integer`, `Long`, `Double` (wrapper classes)
- `BigInteger`, `BigDecimal`
- `LocalDate`, `LocalDateTime`

### C#
- `string`
- `DateTime`, `TimeSpan`
- Value types (structs)

## String Immutability Example

```java
String str1 = "Hello";
String str2 = str1.concat(" World"); // Creates NEW string

System.out.println(str1); // "Hello" (unchanged)
System.out.println(str2); // "Hello World" (new object)
```

## Guidelines for Immutability

1. **Make class final**: Prevent subclasses from adding mutable state
2. **Make all fields final**: Ensures single assignment
3. **Make all fields private**: Prevent direct access
4. **No setters**: Don't provide methods that modify state
5. **Defensive copies**: Copy mutable objects in constructor and getters
6. **Return new instances**: For "modification" methods, return new objects

## Best Practices

1. Prefer immutable objects for value types
2. Use immutable objects in multi-threaded environments
3. Consider builder pattern for complex immutable objects
4. Document immutability in class Javadoc/comments
