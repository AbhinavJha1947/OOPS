# Pass by Value vs Pass by Reference

This is one of the most confusing and frequently asked interview topics, especially for Java developers.

---

## 1. Definitions

### Pass by Value
The method receives a **copy** of the variable's value.
*   Modifying the variable in the method **does NOT** affect the original variable.
*   **Java is ALWAYS Pass by Value.**

### Pass by Reference
The method receives a **reference (pointer/alias)** to the original variable in memory.
*   Modifying the variable in the method **DOES** affect the original variable.
*   **C++ and C# (with `ref`/`out`) support Pass by Reference.**

---

## 2. The Great Java "Gotcha"
If Java is "Pass by Value", why can I change an object's properties inside a method?

**Answer**: Because Java passes the **value of the reference** (the memory address), not the object itself.
*   You **CAN** modify the object the reference points to (e.g., `person.setName("New")`).
*   You **CANNOT** reassign the original reference to a new object.

### Java Example (The Swap Test)

```java
class Dog {
    String name;
    Dog(String name) { this.name = name; }
}

public class Test {
    // Tries to swap references
    public static void swap(Dog d1, Dog d2) {
        Dog temp = d1;
        d1 = d2;
        d2 = temp;
        System.out.println("Inside swap: d1=" + d1.name + ", d2=" + d2.name);
    }
    
    // Tries to modify attribute
    public static void modify(Dog d) {
        d.name = "Changed!";
    }

    public static void main(String[] args) {
        Dog a = new Dog("A");
        Dog b = new Dog("B");

        // 1. Swapping
        swap(a, b);
        System.out.println("After swap: a=" + a.name + ", b=" + b.name);
        // OUTPUT: a=A, b=B (SWAP FAILED!)
        // Reason: The method got COPIES of the references. 
        // We swapped the copies, not the originals.

        // 2. Modifying
        modify(a);
        System.out.println("After modify: a=" + a.name);
        // OUTPUT: a=Changed!
        // Reason: We followed the copy of the reference to the ACTUAL object and changed it.
    }
}
```

---

## 3. Comparison by Language

| Feature | Java | C++ | C# |
| :--- | :--- | :--- | :--- |
| **Default Behavior** | **Pass by Value** | **Pass by Value** | **Pass by Value** |
| **Primitives (int, bool)** | Copy of value | Copy of value | Copy of value |
| **Objects** | Copy of **Reference** | Copy of Object (Copy Constructor) | Copy of Reference |
| **True Pass by Reference?** | ❌ No | ✅ Yes (using `&` or pointers) | ✅ Yes (using `ref` keyword) |

### C++ Example (True Pass by Reference)
```cpp
void swap(int &a, int &b) { // & denotes reference
    int temp = a;
    a = b;
    b = temp;
}
// Calling swap(x, y) WILL actually swap x and y in the caller.
```
