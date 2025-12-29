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
