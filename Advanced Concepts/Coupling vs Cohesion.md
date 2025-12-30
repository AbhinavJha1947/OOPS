# Coupling vs Cohesion

In software design, **Coupling** and **Cohesion** are two key metrics used to determine the quality of your code structure. The golden rule of OOPS design is:
> **Target High Cohesion and Low Coupling.**

---

## 1. Cohesion (Internal)
Cohesion is the degree to which the elements inside a module (class) belong together. It measures "how focused" a class is.

*   **High Cohesion (Good)**: The class does **one well-defined thing**. All methods and fields are related to that single purpose.
    *   *Example*: A `MathUtils` class containing only math functions (`add`, `subtract`, `sqrt`).
*   **Low Cohesion (Bad)**: The class does **too many unrelated things**. It usually violates the Single Responsibility Principle.
    *   *Example*: A `Utility` class that handles logging, database connections, and email formatting all in one place.

### Real-world Analogy
*   **High Cohesion**: A **Swiss Army Knife** is useful, but a dedicated **Chef's Knife** is better for cooking. A Chef's Knife has "high cohesion" regarding cutting. A garbage bin has "low cohesion" because it contains random, unrelated trash.

---

## 2. Coupling (External)
Coupling is the degree of **dependency** between different classes. It measures "how sticky" your code is.

*   **Loose Coupling (Good)**: Classes are independent. Changing one class does not break others. Dependencies are often managed via Interfaces (Abstraction).
    *   *Example*: Relying on `List<String>` (Interface) instead of `ArrayList<String>` (Implementation).
*   **Tight Coupling (Bad)**: Classes are highly dependent on each other's internal details. Changing one class forces you to change many others.
    *   *Example*: Class A directly modifying a public field of Class B.

### Code Example

#### ❌ Tight Coupling (Bad)
```java
class Engine {
    public void start() {}
}

class Car {
    // Car creates the Engine directly. 
    // If we want to change to "ElectricEngine", we must modify Car.
    Engine engine = new Engine(); 
    
    void drive() {
        engine.start();
    }
}
```

#### ✅ Loose Coupling (Good)
```java
interface Engine {
    void start();
}

class GasEngine implements Engine {
    public void start() { /* ... */ }
}

class ElectricEngine implements Engine {
    public void start() { /* ... */ }
}

class Car {
    private Engine engine;
    
    // Dependency Injection: Engine is passed in.
    // Car doesn't care if it's Gas or Electric.
    public Car(Engine engine) {
        this.engine = engine;
    }
    
    void drive() {
        engine.start();
    }
}
```

---

## Summary Matrix

| State | Result | Verdict |
| :--- | :--- | :--- |
| **High Cohesion, Low Coupling** | Modular, Easy to test, Reusable | 🏆 **Excellent** |
| **High Cohesion, High Coupling** | Focused but hard to reuse/test | ⚠️ **Acceptable but risky** |
| **Low Cohesion, Low Coupling** | Confusing, disorganized code | ⚠️ **Poor** |
| **Low Cohesion, High Coupling** | "Spaghetti Code", Fragile, Unmaintainable | ❌ **Terrible** |
