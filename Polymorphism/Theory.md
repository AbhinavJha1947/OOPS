# Polymorphism in Object-Oriented Programming

## What is Polymorphism?
Polymorphism comes from Greek words: *Poly* (many) and *Morph* (forms). In programming, it allows a single interface (class/method) to represent different underlying forms (data types).
> **Core Concept**: An object can be treated as its specific type OR as its parent type, allowing flexible and reusable code.

---

## 1. Compile-time Polymorphism (Static Binding)
Also known as **Method Overloading** or **Early Binding**.
The compiler determines which method to call at **compile time** based on the method signature.

### How to Achieve Overloading?
The method name must be the same, but the **Signature must be different**.
1.  **By Number of Parameters**:
    ```java
    add(int a, int b)       // 2 params
    add(int a, int b, int c) // 3 params
    ```
2.  **By Type of Parameters**:
    ```java
    print(String s)   // String type
    print(int i)      // int type
    ```
3.  **By Order of Parameters**:
    ```java
    save(String name, int id)
    save(int id, String name)
    ```

### ⚠️ Important Rules & Pitfalls
*   **Return Type is Ignored**: You **cannot** overload a method just by changing the return type.
    ```java
    // ERROR: Duplicate method
    int getVal() { return 1; }
    String getVal() { return "1"; }
    ```
*   **Operator Overloading**: Some languages (C++, C#) allow standard operators (`+`, `-`, `*`) to be overloaded for custom classes (e.g., adding two `Vector` objects). Java does not support user-defined operator overloading.

---

## 2. Run-time Polymorphism (Dynamic Binding)
Also known as **Method Overriding** or **Late Binding**.
The method to be executed is determined at **runtime** based on the actual object instance type, not the reference type.

### Prerequisites (The "Three Rules")
1.  **Inheritance**: Must happen between a Parent and Child class.
2.  **Same Signature**: Method name, parameters, and return type must be the same.
3.  **Access**: The child method cannot have a *more restrictive* access modifier than the parent. (e.g., Public -> Protected is Illegal).

### How it Works (Upcasting)
The specific implementation that runs depends on the **object** created (`new Child()`), not the variable type (`Parent p`).

```java
class Animal {
    void speak() { System.out.println("Animal sound"); }
}
class Dog extends Animal {
    @Override // Annotation is good practice
    void speak() { System.out.println("Bark"); }
}

// Main
Animal myPet = new Dog(); // Upcasting
myPet.speak(); // Output: "Bark" (Resolved at Runtime)
```

### Language Nuances
*   **Java**: All non-static methods are `virtual` by default. You can override them unless they are `final`.
*   **C#**: You must explicitly mark the parent method as `virtual` and the child method as `override`.
*   **C++**: You must explicitly mark the parent method as `virtual`.

---

## Advanced Concepts

### 1. Covariant Return Types
When overriding, the return type doesn't have to be *exactly* the same; it can be a **subclass** of the original return type.
*   *Parent*: `Animal getAnimal()`
*   *Child*: `Dog getAnimal()` // Valid, because Dog is an Animal.

### 2. Method Hiding (Static Polymorphism)
**Static methods cannot be overridden**, they can only be **hidden**.
If a child class defines a static method with the same signature as the parent, it "hides" the parent's method regarding that specific class, but polymorphism does not apply.
```java
Parent p = new Child();
p.staticMethod(); // Calls PARENT's static method (Early Binding)
```

### 3. Binding Differences
| Feature | Static Binding (Early) | Dynamic Binding (Late) |
| :--- | :--- | :--- |
| **Methods** | `static`, `private`, `final`, Overloaded methods | `virtual`, Overridden methods |
| **Resolver** | Compiler | JVM / CLR (Runtime) |
| **Speed** | Faster (Structure known) | Slower (Lookup required) |
| **Flexibility** | Less Flexible | More Flexible |
