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

- **Class responsibilities (Single Responsibility):**

- **Proper naming:**

- **Visibility (private/protected/public):**

- **Immutable vs Mutable objects:**

- **DTO vs Entity vs Value Object:**

- **Object Relationships:**

Association

Aggregation

Composition

Dependency

Cardinality (1-1, 1-many, many-many)

📌 Interviewers often ask:

“Why did you choose composition over inheritance?”

## Syntax Overview
```text
class ClassName {
    // attributes
    // methods
}

// Creating an object
ClassName objectName = new ClassName();
```
