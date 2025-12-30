# Composition vs Inheritance

## Overview

**"Favor composition over inheritance"** is one of the fundamental principles in object-oriented design. This principle suggests that code reuse through composition (has-a relationship) is often better than inheritance (is-a relationship).

## Inheritance

**Definition**: A class inherits properties and behavior from a parent class.

### Advantages
- Code reuse
- Polymorphism
- Clear "is-a" relationship
- Easy to understand for simple hierarchies

### Disadvantages
- **Tight Coupling**: Subclass depends on parent implementation
- **Fragile Base Class**: Changes to parent can break subclasses
- **Limited Flexibility**: Cannot change inheritance at runtime
- **Multiple Inheritance Problems**: Diamond problem in languages that support it
- **Violates Encapsulation**: Subclass depends on parent internals

### Java Example

```java
// Inheritance approach
class Vehicle {
    public void start() {
        System.out.println("Vehicle starting");
    }
}

class Car extends Vehicle {
    public void drive() {
        start(); // Inherited method
        System.out.println("Car driving");
    }
}
```

**Problem**: What if we need a car that can fly? We'd need to change the entire hierarchy.

## Composition

**Definition**: A class contains instances of other classes as members.

### Advantages
- **Loose Coupling**: Components are independent
- **Flexibility**: Can change behavior at runtime
- **Better Encapsulation**: Implementation details are hidden
- **No Diamond Problem**: Avoid multiple inheritance issues
- **Easier Testing**: Components can be mocked/tested independently

### Disadvantages
- More code to write initially
- Can be more complex for simple cases

### Java Example

```java
// Composition approach
interface Engine {
    void start();
}

class GasEngine implements Engine {
    public void start() {
        System.out.println("Gas engine starting");
    }
}

class ElectricEngine implements Engine {
    public void start() {
        System.out.println("Electric engine starting");
    }
}

class Car {
    private Engine engine; // Composition
    
    public Car(Engine engine) {
        this.engine = engine;
    }
    
    public void start() {
        engine.start();
    }
    
    // Can change engine at runtime
    public void setEngine(Engine engine) {
        this.engine = engine;
    }
}

// Usage
Car gasCar = new Car(new GasEngine());
Car electricCar = new Car(new ElectricEngine());
```

## Real-World Comparison

### Bad: Inheritance Explosion

```java
// Using inheritance leads to class explosion
class Bird { }
class FlyingBird extends Bird { }
class NonFlyingBird extends Bird { }
class SwimmingBird extends Bird { }
class FlyingSwimmingBird extends FlyingBird { } // Problem!
```

### Good: Composition

```java
// Using composition
interface FlyBehavior {
    void fly();
}

interface SwimBehavior {
    void swim();
}

class CanFly implements FlyBehavior {
    public void fly() {
        System.out.println("Flying");
    }
}

class CannotFly implements FlyBehavior {
    public void fly() {
        System.out.println("Cannot fly");
    }
}

class CanSwim implements SwimBehavior {
    public void swim() {
        System.out.println("Swimming");
    }
}

class Bird {
    private FlyBehavior flyBehavior;
    private SwimBehavior swimBehavior;
    
    public Bird(FlyBehavior flyBehavior, SwimBehavior swimBehavior) {
        this.flyBehavior = flyBehavior;
        this.swimBehavior = swimBehavior;
    }
    
    public void performFly() {
        flyBehavior.fly();
    }
    
    public void performSwim() {
        if (swimBehavior != null) {
            swimBehavior.swim();
        }
    }
}

// Usage - Very flexible!
Bird duck = new Bird(new CanFly(), new CanSwim());  // Flies and swims
Bird penguin = new Bird(new CannotFly(), new CanSwim());  // Swims only
Bird sparrow = new Bird(new CanFly(), null);  // Flies only
```

## Strategy Pattern (Composition)

```java
interface PaymentStrategy {
    void pay(int amount);
}

class CreditCardPayment implements PaymentStrategy {
    public void pay(int amount) {
        System.out.println("Paid " + amount + " using Credit Card");
    }
}

class PayPalPayment implements PaymentStrategy {
    public void pay(int amount) {
        System.out.println("Paid " + amount + " using PayPal");
    }
}

class ShoppingCart {
    private PaymentStrategy paymentStrategy;
    
    public void setPaymentStrategy(PaymentStrategy strategy) {
        this.paymentStrategy = strategy;
    }
    
    public void checkout(int amount) {
        paymentStrategy.pay(amount);
    }
}

// Usage - Can change payment method at runtime
ShoppingCart cart = new ShoppingCart();
cart.setPaymentStrategy(new CreditCardPayment());
cart.checkout(100);

cart.setPaymentStrategy(new PayPalPayment());
cart.checkout(200);
```

## When to Use Each

### Use Inheritance When:
- There's a clear "is-a" relationship
- Subclass truly specializes the parent class
- You want polymorphism
- The hierarchy is stable and unlikely to change
- Example: `Dog is-a Animal`, `Circle is-a Shape`

### Use Composition When:
- You need "has-a" or "uses-a" relationship
- You want flexibility to change behavior at runtime
- You want to combine multiple behaviors
- The relationship might change over time
- Example: `Car has-a Engine`, `Bird uses-a FlyBehavior`

## Comparison Table

| Aspect | Inheritance | Composition |
|--------|-------------|-------------|
| **Relationship** | Is-a | Has-a |
| **Coupling** | Tight | Loose |
| **Flexibility** | Fixed at compile-time | Changeable at runtime |
| **Complexity** | Simple for small hierarchies | More code initially |
| **Reusability** | Code reuse through hierarchy | Code reuse through components |
| **Testing** | Hard to mock parent | Easy to mock components |
| **Maintenance** | Fragile, changes propagate | Robust, changes isolated |

## Best Practices

1. **Favor composition over inheritance** as a default choice
2. **Use inheritance sparingly** only for true is-a relationships
3. **Program to interfaces** not implementations
4. **Keep inheritance hierarchies shallow** (max 2-3 levels)
5. **Consider final classes** to prevent unintended inheritance
6. **Use composition for cross-cutting concerns** (logging, caching, etc.)

## Summary

- **Inheritance** creates tight coupling and rigid hierarchies
- **Composition** provides flexibility and loose coupling
- **Modern design** favors composition with interfaces
- **Use the right tool** for the right job—not all inheritance is bad!
