#include <iostream>
#include <string>

/**
 * C++ Access Modifiers Demonstration
 * 
 * This file demonstrates all access modifiers in C++:
 * - public
 * - private
 * - protected
 */

// Public class demonstrating access modifiers
class AccessModifiers {
public:
    // Public members - accessible from anywhere
    std::string publicField;
    
    // Public constructor
    AccessModifiers() : publicField("Public field"), 
                       privateField("Private field"),
                       protectedField("Protected field") {
        std::cout << "AccessModifiers object created\n";
    }
    
    // Public method
    void publicMethod() {
        std::cout << "Public method called\n";
        // Can access all members within the class
        std::cout << "  " << publicField << "\n";
        std::cout << "  " << privateField << "\n";
        std::cout << "  " << protectedField << "\n";
    }
    
    // Public method that calls private method
    void callPrivateMethod() {
        privateMethod(); // OK - within same class
    }
    
protected:
    // Protected members - accessible within class and derived classes
    std::string protectedField;
    
    void protectedMethod() {
        std::cout << "Protected method called\n";
    }
    
private:
    // Private members - accessible only within this class
    std::string privateField;
    
    void privateMethod() {
        std::cout << "Private method called\n";
    }
};

// Derived class to demonstrate protected access
class DerivedClass : public AccessModifiers {
public:
    void testAccess() {
        std::cout << "\n=== Derived Class Access ===\n";
        
        // Can access public and protected members
        std::cout << "Public field: " << publicField << "\n";        // OK
        std::cout << "Protected field: " << protectedField << "\n";  // OK
        // std::cout << privateField << "\n";                         // ERROR - private
        
        publicMethod();      // OK
        protectedMethod();   // OK
        // privateMethod();  // ERROR - private
    }
};

// Real-world example: BankAccount with proper encapsulation
class BankAccount {
private:
    // Private members - implementation details hidden
    std::string accountNumber;
    double balance;
    std::string accountHolderName;
    
    // Private helper methods
    bool validateAmount(double amount) const {
        return amount > 0;
    }
    
    void logTransaction(const std::string& type, double amount) {
        std::cout << type << ": $" << amount 
                  << " | New balance: $" << balance << "\n";
    }
    
protected:
    // Protected method - for derived classes
    void applyInterest(double rate) {
        double interest = balance * rate;
        balance += interest;
        std::cout << "Interest applied: $" << interest << "\n";
    }
    
public:
    // Public constructor
    BankAccount(const std::string& accNum, const std::string& name, double initialBalance)
        : accountNumber(accNum), accountHolderName(name), balance(initialBalance) {}
    
    // Public methods - public API
    void deposit(double amount) {
        if (validateAmount(amount)) {
            balance += amount;
            logTransaction("Deposit", amount);
        }
    }
    
    bool withdraw(double amount) {
        if (validateAmount(amount) && balance >= amount) {
            balance -= amount;
            logTransaction("Withdrawal", amount);
            return true;
        }
        return false;
    }
    
    // Public getters
    double getBalance() const {
        return balance;
    }
    
    std::string getAccountNumber() const {
        return accountNumber;
    }
    
    std::string getAccountHolderName() const {
        return accountHolderName;
    }
};

// Derived class demonstrating protected access
class SavingsAccount : public BankAccount {
private:
    double interestRate;
    
public:
    SavingsAccount(const std::string& accNum, const std::string& name, 
                   double initialBalance, double rate)
        : BankAccount(accNum, name, initialBalance), interestRate(rate) {}
    
    void addMonthlyInterest() {
        // Can call protected method from parent
        applyInterest(interestRate);
    }
};

// Demonstration of inheritance and access control
class Vehicle {
public:
    Vehicle(const std::string& m) : model(m) {
        std::cout << "Vehicle created: " << model << "\n";
    }
    
    void start() {
        std::cout << model << " starting...\n";
    }
    
protected:
    std::string model;
    
    void engineSound() {
        std::cout << "Engine sound\n";
    }
    
private:
    void internalDiagnostics() {
        std::cout << "Running diagnostics...\n";
    }
};

class Car : public Vehicle {
public:
    Car(const std::string& m) : Vehicle(m) {}
    
    void accelerate() {
        std::cout << model << " accelerating\n";  // Can access protected member
        engineSound();                             // Can call protected method
        // internalDiagnostics();                  // ERROR - private
    }
};

// Main demonstration
int main() {
    std::cout << "=== C++ Access Modifiers Demonstration ===\n\n";
    
    // Testing AccessModifiers class
    AccessModifiers obj;
    
    std::cout << "\nPublic field: " << obj.publicField << "\n";
    obj.publicMethod();
    
    // Can call private method through public method
    obj.callPrivateMethod();
    
    // Testing with derived class
    DerivedClass derived;
    derived.testAccess();
    
    // Real-world example
    std::cout << "\n=== Bank Account Example ===\n";
    BankAccount account("12345", "John Doe", 1000.0);
    account.deposit(500);
    account.withdraw(200);
    std::cout << "Final balance: $" << account.getBalance() << "\n";
    
    std::cout << "\n=== Savings Account Example ===\n";
    SavingsAccount savings("67890", "Jane Smith", 5000.0, 0.05);
    savings.deposit(1000);
    savings.addMonthlyInterest();
    std::cout << "Final balance: $" << savings.getBalance() << "\n";
    
    // Testing Vehicle inheritance
    std::cout << "\n=== Vehicle Inheritance Example ===\n";
    Car myCar("Toyota Camry");
    myCar.start();
    myCar.accelerate();
    
    return 0;
}
