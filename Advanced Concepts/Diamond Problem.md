# The Diamond Problem

The **Diamond Problem** is an ambiguity that arises in languages that support **Multiple Inheritance** (like C++). It occurs when two classes B and C inherit from A, and class D inherits from both B and C.

## The Diagram
```text
      A  (Grandparent)
     / \
    B   C  (Parents)
     \ /
      D  (Child)
```
If Class `A` has a method `start()`, and both `B` and `C` override it, which version does `D` inherit?

### C++ Example (The Problem)
```cpp
class A { void start() { cout << "A"; } };
class B : public A { void start() { cout << "B"; } };
class C : public A { void start() { cout << "C"; } };

class D : public B, public C { 
    // Ambiguity! 
    // D inherits two copies of 'start' (one from B, one from C).
};

D obj;
obj.start(); // Compiler Error: 'start' is ambiguous.
```

### Solution in C++ (Virtual Inheritance)
In C++, we use `virtual` inheritance to ensure only one instance of the base class `A` exists.
```cpp
class B : virtual public A { ... }
class C : virtual public A { ... }
```

---

## Java/C# Approach
Java and C# **do not support Multiple Inheritance of Classes** to specifically avoid this problem.
*   A class can only extend **one** parent class (`extends`).
*   A class can implement **multiple** interfaces (`implements`).

### The Diamond Problem with Default Methods (Java 8+)
Java 8 introduced `default` methods in interfaces, which reintroduced a variation of this problem.

```java
interface B {
    default void start() { System.out.println("B"); }
}

interface C {
    default void start() { System.out.println("C"); }
}

// COMPILER ERROR: class D inherits unrelated defaults for start() from types B and C
class D implements B, C {
    // Solution: You MUST override the method to resolve ambiguity
    @Override
    public void start() {
        B.super.start(); // Call B explicitly
        // OR
        System.out.println("D's own implementation");
    }
}
```
