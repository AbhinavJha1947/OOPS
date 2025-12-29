using System;

namespace AccessModifiersDemo
{
    /// <summary>
    /// C# Access Modifiers Demonstration
    /// 
    /// This file demonstrates all access modifiers in C#:
    /// - public
    /// - private
    /// - protected
    /// - internal
    /// - protected internal
    /// - private protected
    /// </summary>
    public class AccessModifiers
    {
        // Public field - accessible from anywhere
        public string PublicField = "Public field";
        
        // Private field - accessible only within this class
        private string privateField = "Private field";
        
        // Protected field - accessible within class and derived classes
        protected string ProtectedField = "Protected field";
        
        // Internal field - accessible within same assembly
        internal string InternalField = "Internal field";
        
        // Protected internal - accessible within assembly OR derived classes
        protected internal string ProtectedInternalField = "Protected internal field";
        
        // Private protected (C# 7.2+) - accessible within assembly AND derived classes
        private protected string PrivateProtectedField = "Private protected field";
        
        // Public constructor
        public AccessModifiers()
        {
            Console.WriteLine("AccessModifiers object created");
        }
        
        // Public method - accessible from anywhere
        public void PublicMethod()
        {
            Console.WriteLine("Public method called");
            // Can access all fields within the class
            Console.WriteLine(PublicField);
            Console.WriteLine(privateField);
            Console.WriteLine(ProtectedField);
            Console.WriteLine(InternalField);
        }
        
        // Private method - accessible only within this class
        private void PrivateMethod()
        {
            Console.WriteLine("Private method called");
        }
        
        // Protected method - accessible within class and derived classes
        protected void ProtectedMethod()
        {
            Console.WriteLine("Protected method called");
        }
        
        // Internal method - accessible within same assembly
        internal void InternalMethod()
        {
            Console.WriteLine("Internal method called");
        }
        
        // Protected internal method
        protected internal void ProtectedInternalMethod()
        {
            Console.WriteLine("Protected internal method called");
        }
        
        // Private protected method
        private protected void PrivateProtectedMethod()
        {
            Console.WriteLine("Private protected method called");
        }
        
        // Public method that calls private method
        public void CallPrivateMethod()
        {
            PrivateMethod(); // OK - within same class
        }
    }
    
    // Derived class to demonstrate protected access
    public class DerivedClass : AccessModifiers
    {
        public void TestAccess()
        {
            // Can access public, protected, internal, protected internal, and private protected
            Console.WriteLine(PublicField);                    // OK
            Console.WriteLine(ProtectedField);                 // OK - derived class
            Console.WriteLine(InternalField);                  // OK - same assembly
            Console.WriteLine(ProtectedInternalField);         // OK - derived class OR same assembly
            Console.WriteLine(PrivateProtectedField);          // OK - derived class AND same assembly
            // Console.WriteLine(privateField);                // ERROR - private
            
            PublicMethod();                   // OK
            ProtectedMethod();                // OK - derived class
            InternalMethod();                 // OK - same assembly
            ProtectedInternalMethod();        // OK - derived class OR same assembly
            PrivateProtectedMethod();         // OK - derived class AND same assembly
            // PrivateMethod();               // ERROR - private
        }
    }
    
    // Same assembly class to demonstrate internal access
    public class SameAssemblyClass
    {
        public void TestAccess()
        {
            var obj = new AccessModifiers();
            
            // Can access public, internal, and protected internal members
            Console.WriteLine(obj.PublicField);                // OK
            Console.WriteLine(obj.InternalField);              // OK - same assembly
            Console.WriteLine(obj.ProtectedInternalField);     // OK - same assembly
            // Console.WriteLine(obj.ProtectedField);          // ERROR - not derived
            // Console.WriteLine(obj.PrivateProtectedField);   // ERROR - not derived
            // Console.WriteLine(obj.privateField);            // ERROR - private
            
            obj.PublicMethod();                // OK
            obj.InternalMethod();              // OK - same assembly
            obj.ProtectedInternalMethod();     // OK - same assembly
            // obj.ProtectedMethod();          // ERROR - not derived
            // obj.PrivateProtectedMethod();   // ERROR - not derived
            // obj.PrivateMethod();            // ERROR - private
        }
    }
    
    /// <summary>
    /// Real-world example: BankAccount with proper encapsulation
    /// </summary>
    public class BankAccount
    {
        // Private fields - implementation details hidden
        private string accountNumber;
        private decimal balance;
        private string accountHolderName;
        
        // Public properties with private setters
        public string AccountNumber => accountNumber;
        public decimal Balance => balance;
        public string AccountHolderName => accountHolderName;
        
        // Public constructor
        public BankAccount(string accountNumber, string name, decimal initialBalance)
        {
            this.accountNumber = accountNumber;
            this.accountHolderName = name;
            this.balance = initialBalance;
        }
        
        // Public methods - public API
        public void Deposit(decimal amount)
        {
            if (ValidateAmount(amount))
            {
                balance += amount;
                LogTransaction("Deposit", amount);
            }
        }
        
        public bool Withdraw(decimal amount)
        {
            if (ValidateAmount(amount) && balance >= amount)
            {
                balance -= amount;
                LogTransaction("Withdrawal", amount);
                return true;
            }
            return false;
        }
        
        // Private helper methods - internal implementation
        private bool ValidateAmount(decimal amount)
        {
            return amount > 0;
        }
        
        private void LogTransaction(string type, decimal amount)
        {
            Console.WriteLine($"{type}: ${amount:F2} | New balance: ${balance:F2}");
        }
        
        // Protected method - for derived classes
        protected void ApplyInterest(decimal rate)
        {
            decimal interest = balance * rate;
            balance += interest;
            Console.WriteLine($"Interest applied: ${interest:F2}");
        }
        
        // Internal method - for same assembly
        internal void AuditAccount()
        {
            Console.WriteLine($"Auditing account: {accountNumber}");
        }
    }
    
    // Derived class demonstrating protected access
    public class SavingsAccount : BankAccount
    {
        private decimal interestRate;
        
        public SavingsAccount(string accountNumber, string name, decimal initialBalance, decimal rate)
            : base(accountNumber, name, initialBalance)
        {
            this.interestRate = rate;
        }
        
        public void AddMonthlyInterest()
        {
            // Can call protected method from parent
            ApplyInterest(interestRate);
        }
    }
    
    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Access Modifiers Demonstration ===\n");
            
            var obj = new AccessModifiers();
            
            // Accessing public members
            Console.WriteLine("Public field: " + obj.PublicField);
            obj.PublicMethod();
            
            // Can call private method through public method
            obj.CallPrivateMethod();
            
            // Accessing internal members (same assembly)
            Console.WriteLine("\nInternal field: " + obj.InternalField);
            obj.InternalMethod();
            
            // Testing with derived class
            Console.WriteLine("\n=== Derived Class Access ===");
            var derived = new DerivedClass();
            derived.TestAccess();
            
            // Testing with same assembly class
            Console.WriteLine("\n=== Same Assembly Access ===");
            var sameAssembly = new SameAssemblyClass();
            sameAssembly.TestAccess();
            
            // Real-world example
            Console.WriteLine("\n=== Bank Account Example ===");
            var account = new BankAccount("12345", "John Doe", 1000m);
            account.Deposit(500);
            account.Withdraw(200);
            Console.WriteLine($"Final balance: ${account.Balance:F2}");
            
            var savings = new SavingsAccount("67890", "Jane Smith", 5000m, 0.05m);
            savings.AddMonthlyInterest();
        }
    }
}
