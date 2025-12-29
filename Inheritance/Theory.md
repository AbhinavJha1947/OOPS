# Inheritance in Object-Oriented Programming

## What is Inheritance?
Inheritance is a fundamental concept in Object-Oriented Programming (OOP) where a new class (subclass or derived class) inherits properties and behaviors (fields and methods) from an existing class (superclass or base class).

It establishes an **"Is-A"** relationship between classes.  
*Example:* A **Car** *is a* **Vehicle**.

## Why use Inheritance?
1.  **Code Reusability**: You can reuse fields and methods of the existing class without rewriting them.
2.  **Method Overriding**: You can provide a specific implementation of a method that is already defined in the superclass.
3.  **Extensibility**: New functionality can be added to existing classes without modifying them.

## Types of Inheritance
1.  **Single Inheritance**: A class inherits from only one superclass.
2.  **Multilevel Inheritance**: A chain of inheritance (e.g., A -> B -> C).
3.  **Hierarchical Inheritance**: Multiple classes inherit from a single superclass.
4.  **Multiple Inheritance**: A class inherits from multiple superclasses. (Supported in C++ directly, supported in Java/C# via Interfaces).
5.  **Hybrid Inheritance**: A combination of two or more types of inheritance.

## Syntax Overview

### Java
```java
class Superclass { }
class Subclass extends Superclass { }
```

### C#
```csharp
class Superclass { }
class Subclass : Superclass { }
```

### C++
```cpp
class Superclass { };
class Subclass : public Superclass { };
```
