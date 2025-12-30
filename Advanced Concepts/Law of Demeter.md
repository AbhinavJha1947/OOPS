# Law of Demeter (LoD)

Also known as the **Principle of Least Knowledge**. It is a design guideline to develop software with loosely coupled components.

## The Rule: "Talk only to your immediate friends"
A method `m` of an object `O` should only invoke methods of:
1.  The object `O` itself.
2.  The parameters passed to `m`.
3.  Any objects created within `m`.
4.  Direct component objects of `O` (fields).

**It should NOT invoke methods of objects returned by other methods.**

---

## ❌ Violation (Train Wreck Pattern)
Accessing a property of a property of a property...
```java
// Bad: Paperboy reaches into Customer's Wallet to get Payment
public void collectPayment(Customer customer) {
    double payment = customer.getWallet().getTotalMoney(); // VIOLATION
    // We are talking to 'Wallet', which is a stranger (friend of Customer).
    // If Customer changes structure (e.g., uses BankAccount instead of Wallet), this code breaks.
}
```

## ✅ Correct Approach
Ask the object to do the work for you.
```java
public void collectPayment(Customer customer) {
    double payment = customer.getPayment(10.00); // GOOD
    // We only talk to Customer. We don't care how they get the money.
}

// Inside Customer class
public double getPayment(double amount) {
    return this.wallet.takeMoney(amount); 
    // Customer talks to Wallet. This is allowed (Wallet is a field).
}
```

## Benefits of LoD
1.  **Loose Coupling**: Your class depends on fewer other classes.
2.  **Maintainability**: Changes in the internal structure of dependencies (like removing `Wallet`) don't break your code.
3.  **Testability**: Easier to mock `Customer` than to mock `Customer + Wallet + Money`.
