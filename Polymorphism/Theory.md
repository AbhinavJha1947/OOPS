# Polymorphism in Object-Oriented Programming

## What is Polymorphism?
Polymorphism means "many forms". In OOP, it allows objects to be treated as instances of their parent class rather than their actual class. It enables a single interface to represent different underlying forms (data types).

## Types of Polymorphism

### 1. Compile-time Polymorphism (Static Binding)
Also known as **Method Overloading**.
*   **Definition**: Multiple methods in the same class have the same name but different parameters (number, type, or order).
*   **Resolution**: The compiler determines which method to call at compile time.

### 2. Run-time Polymorphism (Dynamic Binding)
Also known as **Method Overriding**.
*   **Definition**: A subclass provides a specific implementation of a method that is already defined in its superclass.
*   **Resolution**: The method to be executed is determined at runtime based on the object instance.

## Key Differences

| Feature | Method Overloading | Method Overriding |
| :--- | :--- | :--- |
| **Time of Resolution** | Compile-time | Run-time |
| **Location** | Same class | Different classes (Inheritance) |
| **Parameters** | Must be different | Must be same |
| **Return Type** | Can be different | Must be same (or covariant) |
| **Keywords** | None specifically | `@Override` (Java), `override` (C#), `virtual` (C++) |

## Real-world Analogy
*   **Overloading**: A person can "speak". If they speak to a friend, they use casual language. If they speak to a boss, they use formal language. The action "speak" changes based on the context (parameters).
*   **Overriding**: A generic "Animal" makes a sound. A "Dog" overrides this to "Bark". A "Cat" overrides this to "Meow".
