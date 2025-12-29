# Important OOPS Keywords

This section covers essential keywords used in Object-Oriented Programming (Java, C#, C++) to control behavior, scope, and inheritance.

## 1. `static` (Java, C#, C++)
*   **Definition**: Belongs to the class rather than an instance of the class. A single copy is shared among all objects.
*   **Usage**: Constants, utility methods, counters.
*   **Access**: Can be accessed without creating an object (e.g., `ClassName.method()`).

## 2. `this` (Java, C#, C++)
*   **Definition**: A reference variable that refers to the **current object**.
*   **Usage**:
    *   To differentiate between class attributes and parameters with the same name.
    *   To call another constructor in the same class (Constructor Chaining).
    *   To return the current object instance.

## 3. `super` (Java) / `base` (C#)
*   **Definition**: A reference variable used to refer to the **immediate parent class** object.
*   **Usage**:
    *   To call the parent class's constructor.
    *   To access parent class methods or fields that are hidden/overridden by the child class.

## 4. `final` (Java) / `sealed` (C#) / `const` (C++)
*   **Java `final`**:
    *   Variable: Value cannot be changed (constant).
    *   Method: Cannot be overridden.
    *   Class: Cannot be inherited.
*   **C# `sealed`**:
    *   Class: Cannot be inherited.
    *   Method: Cannot be overridden (used with `override`).
*   **C++ `const`**:
    *   Variable: Value cannot be changed.
    *   Member Function: Cannot modify the object's state.

## 5. `abstract`
*   Used to declare abstract classes and methods (discussed in Abstraction).

Delegates / Events (in C# specifically)
Inner / Nested Classes
Partial Classes
Sealed / Final Classes
Virtual & Override Keywords
Anonymous Classes / Objects
