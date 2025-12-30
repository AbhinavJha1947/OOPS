# OOPS Interview Questions & Answers

## Table of Contents
1.  [What is Object-Oriented Programming?](#1-what-is-object-oriented-programming)
2.  [Difference between Class and Object?](#2-difference-between-class-and-object)
3.  [What are the 4 Pillars of OOPS?](#3-what-are-the-4-pillars-of-oops)
4.  [What is Encapsulation?](#4-what-is-encapsulation)
5.  [What is Abstraction? vs Information Hiding](#5-what-is-abstraction-vs-information-hiding)
6.  [Difference between Abstract Class and Interface?](#6-difference-between-abstract-class-and-interface)
7.  [What is Inheritance? Types?](#7-what-is-inheritance-types)
8.  [Diamond Problem in Inheritance?](#8-diamond-problem-in-inheritance)
9.  [What is Polymorphism? Overloading vs Overriding](#9-what-is-polymorphism-overloading-vs-overriding)
10. [What is a Constructor? Types?](#10-what-is-a-constructor-types)
11. [What is `static` keyword?](#11-what-is-static-keyword)
12. [Difference between `this` and `super`?](#12-difference-between-this-and-super)
13. [Association vs Aggregation vs Composition?](#13-association-vs-aggregation-vs-composition)
14. [Shallow Copy vs Deep Copy?](#14-shallow-copy-vs-deep-copy)
15. [What is a Destructor / Finalizer?](#15-what-is-a-destructor--finalizer)
16. [What is Thread Safety?](#16-what-is-thread-safety)
17. [What is a Race Condition?](#17-what-is-a-race-condition)
18. [What is a Deadlock?](#18-what-is-a-deadlock)
19. [Explain Access Modifiers (Public, Private, Protected, Internal)](#19-explain-access-modifiers-public-private-protected-internal)
20. [What is Dependency Injection?](#20-what-is-dependency-injection)
21. [Coupling vs Cohesion?](#21-coupling-vs-cohesion)
22. [What is the Law of Demeter?](#22-what-is-the-law-of-demeter)
23. [Is Java Pass by Value or Reference?](#23-is-java-pass-by-value-or-reference)
24. [Composition vs Inheritance?](#24-composition-vs-inheritance)

---

### 1. What is Object-Oriented Programming?
OOPS is a programming paradigm based on the concept of "objects", which contain data (attributes) and code (methods). It aims to implement real-world entities like inheritance, hiding, polymorphism, etc., in programming.

[Back to Top](#table-of-contents)

### 2. Difference between Class and Object?
| Class | Object |
| :--- | :--- |
| Blueprint or Template | Instance of the Class |
| Logical entity, no memory allocated | Physical entity, takes up memory |
| Declared once | Created multiple times |

[Back to Top](#table-of-contents)

### 3. What are the 4 Pillars of OOPS?
1.  **Encapsulation**: Wrapping data and methods together.
2.  **Abstraction**: Hiding implementation details.
3.  **Inheritance**: Acquiring properties of another class.
4.  **Polymorphism**: Ability to take multiple forms.

[Back to Top](#table-of-contents)

### 4. What is Encapsulation?
Encapsulation is the mechanism of wrapping the data (variables) and code acting on the data (methods) together as a single unit. In encapsulation, the variables of a class will be hidden from other classes (`private`), and can be accessed only through the methods of their current class (`public` getters/setters).

[Back to Top](#table-of-contents)

### 5. What is Abstraction? vs Information Hiding
Abstraction is selecting data from a larger pool to show only relevant details to the object. It helps in reducing programming complexity and effort.
*   **Abstraction**: Hides *implementation details* (How it works).
*   **Encapsulation**: Hides *internal state/data* (Information Hiding).

[Back to Top](#table-of-contents)

### 6. Difference between Abstract Class and Interface?
| Feature | Abstract Class | Interface |
| :--- | :--- | :--- |
| **Methods** | Can have abstract and non-abstract methods | Only abstract methods (prior to Java 8/C# 8) |
| **Inheritance** | Single inheritance (`extends`) | Multiple inheritance (`implements`) |
| **Variables** | Can have `final`, `non-final`, `static`, `non-static` | Variables are implicitly `public static final` (Constants) |
| **Constructor** | Can have constructors | Cannot have constructors |

[Back to Top](#table-of-contents)

### 7. What is Inheritance? Types?
Inheritance is a mechanism where one class acquires the properties and behaviors of another class.
**Types**:
1.  **Single**: A -> B
2.  **Multilevel**: A -> B -> C
3.  **Hierarchical**: A -> B, A -> C
4.  **Multiple**: A -> B, C -> B (Not supported directly in Java/C# classes)
5.  **Hybrid**: Combination of above.

[Back to Top](#table-of-contents)

### 8. Diamond Problem in Inheritance?
It occurs in **Multiple Inheritance** when Class D inherits from Class B and Class C, which both inherit from Class A. If Class A has a method `foo()`, and both B and C override it, D doesn't know which version of `foo()` to call.
> 🔗 [Read detailed article](Advanced%20Concepts/Diamond%20Problem.md)
*   **Solution**: Java/C# avoids this by not supporting multiple inheritance of classes. They use Interfaces to achieve similar functionality without the ambiguity. C++ uses `virtual` inheritance.

[Back to Top](#table-of-contents)

### 9. What is Polymorphism? Overloading vs Overriding
Polymorphism means "many forms".
*   **Compile-time (Overloading)**: Same method name, different parameters. Resolved by compiler.
*   **Run-time (Overriding)**: Same method name and parameters in Parent and Child class. Resolved at runtime.

[Back to Top](#table-of-contents)

### 10. What is a Constructor? Types?
A block of code similar to a method called when an instance of an object is created.
*   **Default Constructor**: No args.
*   **Parameterized Constructor**: With args.
*   **Copy Constructor**: Creates an object using another object of the same class.

[Back to Top](#table-of-contents)

### 11. What is `static` keyword?
Indicates that a member (variable or method) belongs to the **type** itself, rather than to a specific instance of that type. Only one instance of a static variable exists for the class.

[Back to Top](#table-of-contents)

### 12. Difference between `this` and `super`?
*   `this`: Refers to the **current object** instance. Used to access current class members.
*   `super`: Refers to the **parent class** instance. Used to call parent class constructors or methods.

[Back to Top](#table-of-contents)

### 13. Association vs Aggregation vs Composition?
All three represent relationships between objects.
*   **Association**: General "uses-a" relationship using references/methods. (Bank and Customer).
*   **Aggregation**: Specialized "weak" association. "Has-a". Child can exist independently of Parent. (Department and Teacher).
*   **Composition**: Specialized "strong" association. "Part-of". Child **cannot** exist without Parent. (House and Room).

[Back to Top](#table-of-contents)

### 14. Shallow Copy vs Deep Copy?
> 🔗 [Read detailed article](Advanced%20Concepts/Shallow%20vs%20Deep%20Copy.md)
*   **Shallow Copy**: Creates a new object but copies reference pointers to elements. If the original object's heap data changes, the copy changes too.
*   **Deep Copy**: Creates a new object and recursively copies all data pointed to by the original object. The copy is fully independent.

[Back to Top](#table-of-contents)

### 15. What is a Destructor / Finalizer?
A method called automatically when an object is destroyed or garbage collected.
*   **C++ Destructor (`~Class`)**: Deterministic. Called when object goes out of scope or is deleted. Used for RAII.
*   **Java/C# Finalizer**: Non-deterministic. Called by Garbage Collector before reclaiming memory. Not recommended for resource cleanup (use `AutoCloseable`/`IDisposable`).

[Back to Top](#table-of-contents)

### 16. What is Thread Safety?
Thread safety ensures that shared data structures/code are accessed by multiple threads in a way that prevents race conditions, data corruption, or unexpected behavior. Achieved using Synchronization, Locks, or Atomic operations.

[Back to Top](#table-of-contents)

### 17. What is a Race Condition?
A specific bug where the output of a process is unexpectedly dependent on the varying timing or order of execution of concurrent threads. Often occurs when two threads read-modify-write shared data simultaneously.

[Back to Top](#table-of-contents)

### 18. What is a Deadlock?
A situation where two or more threads are blocked forever, waiting for each other to release a resource.
*   **Example**: Thread A holds Lock 1 and waits for Lock 2. Thread B holds Lock 2 and waits for Lock 1.

[Back to Top](#table-of-contents)

### 19. Explain Access Modifiers (Public, Private, Protected, Internal)
*   **Public**: Accessible from anywhere.
*   **Private**: Accessible only within the same class.
*   **Protected**: Accessible within the same class and child classes (Inheritance).
*   **Internal (C#) / Default (Java)**: Accessible within the same Assembly (C#) or Package (Java).

[Back to Top](#table-of-contents)

### 20. What is Dependency Injection?
A design pattern where an object's dependencies are provided (injected) externally rather than created internally. This promotes Loose Coupling and easier testing.
> 🔗 [Read detailed article](Advanced%20Concepts/Dependency%20Injection%20(DI).md)
*   **Types**: Constructor Injection, Setter Injection, Interface Injection.


[Back to Top](#table-of-contents)

### 21. Coupling vs Cohesion?
*   **Cohesion**: How focused a class is. We want **High Cohesion** (Class does one thing well).
*   **Coupling**: How dependent a class is on others. We want **Low Coupling** (Classes are independent).
> 🔗 [Read detailed article](Advanced%20Concepts/Coupling%20vs%20Cohesion.md)

[Back to Top](#table-of-contents)

### 22. What is the Law of Demeter?
Also known as the "Principle of Least Knowledge". It states that a module should not know about the innards of the objects it manipulates.
*   **Rule**: Talk only to your immediate friends. Avoid "Train Wreck" code like `getA().getB().getC().doSomething()`.
> 🔗 [Read detailed article](Advanced%20Concepts/Law%20of%20Demeter.md)

[Back to Top](#table-of-contents)

### 23. Is Java Pass by Value or Reference?
**Java is aways Pass by Value.**
*   For objects, Java passes the **value of the reference** (copy of the memory address).
*   This means you can modify the object's data, but you cannot swap or reassign the original reference itself.
> 🔗 [Read detailed article](Advanced%20Concepts/Pass%20by%20Value%20vs%20Reference.md)

[Back to Top](#table-of-contents)

### 24. Composition vs Inheritance?
**Composition over Inheritance** is a design principle that argues for creating complex objects by composing specific behaviors (has-a) rather than inheriting them (is-a).
*   **Why?**: Inheritance creates tight coupling (Fragile Base Class). Composition provides flexibility to change behavior at runtime.
> 🔗 [Read detailed article](Advanced%20Concepts/Composition%20vs%20Inheritance.md)

[Back to Top](#table-of-contents)
