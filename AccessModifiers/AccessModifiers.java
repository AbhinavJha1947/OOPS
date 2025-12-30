/**
 * Java Access Modifiers Demonstration
 * 
 * This file demonstrates all access modifiers in Java:
 * - public
 * - private  
 * - protected
 * - default (package-private)
 */

package AccessModifiers;

// Public class - accessible from anywhere
public class AccessModifiers {
    
    // Public field - accessible from anywhere
    public String publicField = "Public field";
    
    // Private field - accessible only within this class
    private String privateField = "Private field";
    
    // Protected field - accessible within package and subclasses
    protected String protectedField = "Protected field";
    
    // Default (package-private) field - accessible within package only
    String defaultField = "Default field";
    
    // Public constructor
    public AccessModifiers() {
        System.out.println("AccessModifiers object created");
    }
    
    // Public method - accessible from anywhere
    public void publicMethod() {
        System.out.println("Public method called");
        // Can access all fields within the class
        System.out.println(publicField);
        System.out.println(privateField);
        System.out.println(protectedField);
        System.out.println(defaultField);
    }
    
    // Private method - accessible only within this class
    private void privateMethod() {
        System.out.println("Private method called");
    }
    
    // Protected method - accessible within package and subclasses
    protected void protectedMethod() {
        System.out.println("Protected method called");
    }
    
    // Default method - accessible within package only
    void defaultMethod() {
        System.out.println("Default method called");
    }
    
    // Public method that calls private method
    public void callPrivateMethod() {
        privateMethod(); // OK - within same class
    }
}

// Subclass to demonstrate protected access
class SubClass extends AccessModifiers {
    public void testAccess() {
        // Can access public, protected, and default members
        System.out.println(publicField);      // OK
        System.out.println(protectedField);   // OK
        System.out.println(defaultField);     // OK (same package)
        // System.out.println(privateField);  // ERROR - private
        
        publicMethod();      // OK
        protectedMethod();   // OK
        defaultMethod();     // OK (same package)
        // privateMethod();  // ERROR - private
    }
}

// Class in same package to demonstrate package access
class SamePackageClass {
    public void testAccess() {
        AccessModifiers obj = new AccessModifiers();
        
        // Can access public, protected, and default members
        System.out.println(obj.publicField);      // OK
        System.out.println(obj.protectedField);   // OK (same package)
        System.out.println(obj.defaultField);     // OK (same package)
        // System.out.println(obj.privateField);  // ERROR - private
        
        obj.publicMethod();      // OK
        obj.protectedMethod();   // OK (same package)
        obj.defaultMethod();     // OK (same package)
        // obj.privateMethod();  // ERROR - private
    }
}

// Demonstration class
class AccessModifiersDemo {
    public static void main(String[] args) {
        System.out.println("=== Access Modifiers Demonstration ===\n");
        
        AccessModifiers obj = new AccessModifiers();
        
        // Accessing public members
        System.out.println("Public field: " + obj.publicField);
        obj.publicMethod();
        
        // Can call private method through public method
        obj.callPrivateMethod();
        
        // Accessing protected and default (same package)
        System.out.println("\nProtected field: " + obj.protectedField);
        obj.protectedMethod();
        
        System.out.println("\nDefault field: " + obj.defaultField);
        obj.defaultMethod();
        
        // Testing with subclass
        System.out.println("\n=== Subclass Access ===");
        SubClass subObj = new SubClass();
        subObj.testAccess();
        
        // Testing with same package class
        System.out.println("\n=== Same Package Access ===");
        SamePackageClass samePackage = new SamePackageClass();
        samePackage.testAccess();
    }
}

/**
 * Real-world example: BankAccount with proper encapsulation
 */
class BankAccount {
    // Private fields - implementation details hidden
    private String accountNumber;
    private double balance;
    private String accountHolderName;
    
    // Public constructor
    public BankAccount(String accountNumber, String name, double initialBalance) {
        this.accountNumber = accountNumber;
        this.accountHolderName = name;
        this.balance = initialBalance;
    }
    
    // Public methods - public API
    public void deposit(double amount) {
        if (validateAmount(amount)) {
            balance += amount;
            logTransaction("Deposit", amount);
        }
    }
    
    public boolean withdraw(double amount) {
        if (validateAmount(amount) && balance >= amount) {
            balance -= amount;
            logTransaction("Withdrawal", amount);
            return true;
        }
        return false;
    }
    
    public double getBalance() {
        return balance;
    }
    
    public String getAccountNumber() {
        return accountNumber;
    }
    
    // Private helper methods - internal implementation
    private boolean validateAmount(double amount) {
        return amount > 0;
    }
    
    private void logTransaction(String type, double amount) {
        System.out.println(type + ": $" + amount + " | New balance: $" + balance);
    }
    
    // Protected method - for subclasses
    protected void applyInterest(double rate) {
        double interest = balance * rate;
        balance += interest;
        System.out.println("Interest applied: $" + interest);
    }
}

// Subclass demonstrating protected access
class SavingsAccount extends BankAccount {
    private double interestRate;
    
    public SavingsAccount(String accountNumber, String name, double initialBalance, double rate) {
        super(accountNumber, name, initialBalance);
        this.interestRate = rate;
    }
    
    public void addMonthlyInterest() {
        // Can call protected method from parent
        applyInterest(interestRate);
    }
}
