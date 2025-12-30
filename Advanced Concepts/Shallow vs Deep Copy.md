# Shallow Copy vs Deep Copy

Understanding how objects are copied is critical for memory management and avoiding bugs where changing one object accidentally affects another.

---

## 1. Shallow Copy
A shallow copy creates a new object, but inserts **references** into it to the objects found in the original.
*   **Primitives**: Values are copied.
*   **Objects**: **References (Memory Addresses)** are copied. The new object points to the *same* inner objects as the original.

### The Problem
If you modify a shared mutable object inside the copy, the original object is also affected.

```text
[Original Object] ----> [Shared Data] <---- [Shallow Copy]
```

### Java Example (Shallow Copy)
```java
class Address {
    String city;
    Address(String city) { this.city = city; }
}

class User implements Cloneable {
    String name;
    Address address; // Mutable object

    public User(String name, Address address) {
        this.name = name;
        this.address = address;
    }

    // Default clone() does a Shallow Copy
    @Override
    protected Object clone() throws CloneNotSupportedException {
        return super.clone();
    }
}

// Usage
User u1 = new User("John", new Address("NY"));
User u2 = (User) u1.clone(); // Shallow Copy

u2.address.city = "LA"; 
// u1.address.city is NOW "LA" too! (Bad)
```

---

## 2. Deep Copy
A deep copy creates a new object and **recursively copies** all objects found in the original.
*   The new object is completely independent of the original.
*   Modifying one does not affect the other.

```text
[Original Object] ----> [Data A]
[Deep Copy]       ----> [Data B (Copy of A)]
```

### Java Example (Deep Copy)
To achieve this, you must manually clone/copy inner objects.

```java
@Override
protected Object clone() throws CloneNotSupportedException {
    User deepCopy = (User) super.clone();
    // Manually copy the mutable inner object
    deepCopy.address = new Address(this.address.city);
    return deepCopy;
}

// Usage
User u1 = new User("John", new Address("NY"));
User u2 = (User) u1.clone(); // Deep Copy

u2.address.city = "LA"; 
// u1.address.city remains "NY". (Good)
```

---

## key Difference Table

| Feature | Shallow Copy | Deep Copy |
| :--- | :--- | :--- |
| **Speed** | Fast (Just copies references) | Slow (Recursively copies data) |
| **Dependency** | Copy depends on original | Copy is independent |
| **Default In** | Java `clone()`, C++ Copy Constructor | Must be implemented manually |
| **Use Case** | Read-only data | Mutable data structure |
