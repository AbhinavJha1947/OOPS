# Class and Object in Object-Oriented Programming

## Definition

### Class
A **Class** is a user-defined data type that acts as a blueprint for creating objects. It defines a set of properties (attributes) and methods (behaviors) that are common to all objects of that type. A class does not occupy memory until an object is instantiated.

### Object
An **Object** is an instance of a class. It is a real-world entity that has identity, state (attributes), and behavior (methods). When an object is created, memory is allocated.

## Real-World Analogy
Think of a **House Blueprint** (Class) and the **Actual House** (Object).
- The blueprint defines the structure (rooms, doors, windows). You can't live in the blueprint.
- The house is built based on the blueprint. You can build multiple houses from the same blueprint. Each house has its own address and specific furniture, but they share the same structure defined by the blueprint.

## Key Concepts
- **Attributes (Fields/Properties):** Variables within a class that store data (state).
- **Methods (Functions):** Functions within a class that perform actions (behavior).
- **Instantiation:** The process of creating an object from a class.

## Class Design

### 1. Class Responsibilities (Single Responsibility Principle - SRP)
A class should have **one, and only one, reason to change**. This means a class should focus on a single task or responsibility.
-   **Cohesion**: A class should have **High Cohesion** (all methods/fields relate to a central purpose).
-   **Coupling**: Classes should have **Low Coupling** (dependencies between classes should be minimized).
*   *Bad Example*: `User` class handles database logic, email sending, and validation.
*   *Good Example*: `User` handles data, `UserRepository` handles database, `EmailService` handles emails.

### 2. Proper Naming
Clear naming is critical for maintainability.
-   **Classes**: Nouns, PascalCase (e.g., `Customer`, `BankAccount`). Avoid vague names like `Manager` or `Processor` if possible.
-   **Methods**: Verbs/Actions, camelCase (e.g., `calculateTotal()`, `isValid()`).
-   **Booleans**: Prefix with `is`, `has`, `can` (e.g., `isActive`, `hasAccess`).

### 3. Visibility (Access Modifiers)
Control who can access your class members to ensure **Encapsulation**.
-   **private**: Only visible within the class. (Default choice for fields).
-   **protected**: Visible to package and subclasses.
-   **public**: Visible everywhere. (Use sparingly, mainly for API methods).
-   **package-private (default)**: Visible only within the same package.

### 4. Immutable vs Mutable Objects
-   **Mutable**: State can change after creation.
    -   *Example*: `StringBuilder`, typical JavaBean with setters.
-   **Immutable**: State **cannot** change after creation.
    -   *How*: Make class `final`, all fields `private final`, no setters.
    -   *Benefits*: Thread-safe by default, excellent for HashKeys, no side effects.
    -   *Example*: `String`, `Integer`, `BigDecimal`.

### 5. DTO vs Entity vs Value Object
-   **Entity**: An object defined by its **Identity** (ID). two entities with different IDs are different, even if data is same.
    -   *Example*: `User` (ID: 101), `Order` (ID: 5002). Mutable.
-   **Value Object (VO)**: An object defined by its **Value**. No identity. Two VOs with same values are considered equal.
    -   *Example*: `Money($10)`, `GPSLocation(x,y)`, `Color(Red)`. Should be Immutable.
-   **DTO (Data Transfer Object)**: A simple object used to transfer data between processes/layers.
    -   *Characteristics*: No business logic, just getters/setters (or public fields), often serializable.

### 6. Object Relationships
How objects interact with each other. [See detailed examples here](../Object%20Relationships/Dependency.md).

-   **Association ("Uses-a")**:
    -   Objects have independent lifecycles.
    -   *Example*: `Teacher` and `Student`. A teacher has students, but removing a teacher doesn't destroy the students.

-   **Aggregation ("Has-a")**:
    -   A specialized form of Association. Parent has Child, but Child can exist independently.
    -   *Example*: `Department` and `Employee`. If Department closes, Employees still exist.

-   **Composition ("Part-of")**:
    -   Strong ownership. Child **cannot** exist without Parent.
    -   *Example*: `House` and `Room`. If House is destroyed, Rooms are gone.

-   **Dependency**:
    -   A temporary relationship where one class uses another (e.g., as a parameter).
    -   *Example*: `Driver` depends on `Car` only while driving.

#### Cardinality
Describes the numerical relationship between entities:
-   **1-to-1**: One User has One Profile.
-   **1-to-Many**: One Team has Many Players.
-   **Many-to-Many**: Students and Courses (One student takes many courses; One course has many students).

---

### 📌 Interview Question: "Why did you choose Composition over Inheritance?"
**Answer**:
"I prefer **Composition over Inheritance** because it provides more flexibility and avoids the 'Fragile Base Class' problem."
1.  **Flexibility**: You can change behavior at runtime (Dependency Injection), whereas inheritance is fixed at compile-time.
2.  **Loose Coupling**: Changes in the parent class don't automatically break the child class.
3.  **Encapsulation**: Inheritance breaks encapsulation because the subclass depends on the internal details of the parent.
4.  **"Is-a" vs "Has-a"**: Inheritance is only for strict "Is-a" relationships. If unsure, "Has-a" (Composition) is safer.

## Syntax Overview
```text
class ClassName {
    // attributes
    // methods
}

// Creating an object
ClassName objectName = new ClassName();
```
