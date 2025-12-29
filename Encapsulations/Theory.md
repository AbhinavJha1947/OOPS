# Encapsulation in Object-Oriented Programming

## Definition
**Encapsulation** is the mechanism of wrapping the data (variables) and code acting on the data (methods) together as a single unit. In encapsulation, the variables of a class will be hidden from other classes, and can be accessed only through the methods of their current class. Therefore, it is also known as **data hiding**.

## Real-World Analogy
Consider a **Capsule**. The medicine is encapsulated inside the capsule wrapper. You cannot access the medicine directly; you have to consume the capsule.
Another example is a **Bank Account**. You cannot directly access the money in the bank vault. You interact with the bank teller or ATM (methods) to deposit or withdraw money (data).

## Key Concepts
- **Data Hiding:** The internal state of an object is protected from external interference.
- **Access Modifiers:** Keywords used to restrict access to the members of a class (e.g., `private`, `public`, `protected`).
- **Getters and Setters:** Public methods used to access (get) and update (set) private variables.

## Advantages
- **Control over data:** You can control what data is stored in the fields (e.g., preventing negative values for age).
- **Data Hiding:** The user will have no idea about the inner implementation of the class.
- **Flexibility:** You can make the variables of the class read-only or write-only.
