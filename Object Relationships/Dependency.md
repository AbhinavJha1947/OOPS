# Dependency

## Definition

**Dependency** is the weakest form of relationship between classes where one class uses another class temporarily. It's a "uses-a" relationship where one class depends on another to perform its functionality, but doesn't store a reference to it as a member variable.

## Key Characteristics

- **Temporary Relationship**: The relationship exists only during method execution
- **No Member Variable**: The dependent object is not stored as a field
- **Weakest Coupling**: The loosest form of relationship
- **Usage-Based**: One class uses another through method parameters, local variables, or return types
- **No Ownership**: No lifecycle dependency between classes

## Types of Dependency

### 1. **Method Parameter Dependency**
The dependent class is passed as a method parameter.

### 2. **Local Variable Dependency**
The dependent class is created as a local variable within a method.

### 3. **Return Type Dependency**
A method returns an instance of the dependent class.

### 4. **Static Method Dependency**
Dependency through static method calls.

## Real-World Examples

1. **Driver and Car**: A driver uses a car to drive, but doesn't own it
2. **Printer and Document**: A printer uses a document to print, but doesn't store it
3. **EmailService and EmailMessage**: An email service uses email messages to send
4. **PaymentProcessor and CreditCard**: A payment processor uses credit card info temporarily

## Code Examples

### Java Implementation

```java
// Document class - used by Printer
class Document {
    private String content;
    private String fileName;
    
    public Document(String fileName, String content) {
        this.fileName = fileName;
        this.content = content;
    }
    
    public String getContent() {
        return content;
    }
    
    public String getFileName() {
        return fileName;
    }
}

// Printer class - depends on Document
class Printer {
    private String printerName;
    
    public Printer(String name) {
        this.printerName = name;
    }
    
    // Method parameter dependency
    public void print(Document document) {
        System.out.println("Printing on " + printerName + ":");
        System.out.println("File: " + document.getFileName());
        System.out.println("Content: " + document.getContent());
        System.out.println("Printing completed.\n");
    }
    
    // Local variable dependency
    public void printTestPage() {
        Document testDoc = new Document("test.txt", "Test Page Content");
        print(testDoc);
    }
}

// EmailMessage class
class EmailMessage {
    private String to;
    private String subject;
    private String body;
    
    public EmailMessage(String to, String subject, String body) {
        this.to = to;
        this.subject = subject;
        this.body = body;
    }
    
    public String getTo() { return to; }
    public String getSubject() { return subject; }
    public String getBody() { return body; }
}

// EmailService depends on EmailMessage
class EmailService {
    // Method parameter dependency
    public void sendEmail(EmailMessage message) {
        System.out.println("Sending email to: " + message.getTo());
        System.out.println("Subject: " + message.getSubject());
        System.out.println("Body: " + message.getBody());
        System.out.println("Email sent successfully!\n");
    }
    
    // Return type dependency
    public EmailMessage createWelcomeEmail(String recipient) {
        return new EmailMessage(
            recipient,
            "Welcome!",
            "Thank you for joining us."
        );
    }
}

// Usage Example
class DependencyDemo {
    public static void main(String[] args) {
        // Printer-Document dependency
        Printer printer = new Printer("HP LaserJet");
        Document doc = new Document("report.txt", "Annual Report 2024");
        printer.print(doc); // Document used temporarily
        
        // EmailService-EmailMessage dependency
        EmailService emailService = new EmailService();
        EmailMessage email = new EmailMessage(
            "user@example.com",
            "Hello",
            "This is a test email"
        );
        emailService.sendEmail(email); // EmailMessage used temporarily
    }
}
```

### C# Implementation

```csharp
using System;

// Logger class - used by various services
public class Logger {
    public void Log(string message) {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}

// Payment class
public class Payment {
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string CardNumber { get; set; }
    
    public Payment(decimal amount, string currency, string cardNumber) {
        Amount = amount;
        Currency = currency;
        CardNumber = cardNumber;
    }
}

// PaymentProcessor depends on Payment and Logger
public class PaymentProcessor {
    // Method parameter dependency on both Payment and Logger
    public bool ProcessPayment(Payment payment, Logger logger) {
        logger.Log($"Processing payment of {payment.Amount} {payment.Currency}");
        
        // Simulate payment processing
        bool success = ValidateCard(payment.CardNumber);
        
        if (success) {
            logger.Log("Payment successful");
        } else {
            logger.Log("Payment failed");
        }
        
        return success;
    }
    
    // Local variable dependency
    private bool ValidateCard(string cardNumber) {
        // Local dependency on Logger
        Logger validationLogger = new Logger();
        validationLogger.Log("Validating card: " + MaskCardNumber(cardNumber));
        
        // Simplified validation
        return cardNumber.Length == 16;
    }
    
    private string MaskCardNumber(string cardNumber) {
        if (cardNumber.Length < 4) return cardNumber;
        return "****-****-****-" + cardNumber.Substring(cardNumber.Length - 4);
    }
}

// Usage Example
class Program {
    static void Main() {
        PaymentProcessor processor = new PaymentProcessor();
        Payment payment = new Payment(99.99m, "USD", "1234567890123456");
        Logger logger = new Logger();
        
        // Payment and Logger are used temporarily
        bool result = processor.ProcessPayment(payment, logger);
        
        Console.WriteLine($"\nTransaction result: {(result ? "Success" : "Failed")}");
    }
}
```

### C++ Implementation

```cpp
#include <iostream>
#include <string>
#include <ctime>
using namespace std;

// Report class - used by ReportGenerator
class Report {
private:
    string title;
    string data;
    
public:
    Report(string t, string d) : title(t), data(d) {}
    
    string getTitle() const { return title; }
    string getData() const { return data; }
};

// FileWriter class - used by ReportGenerator
class FileWriter {
public:
    void writeToFile(const string& filename, const string& content) {
        cout << "Writing to file: " << filename << endl;
        cout << "Content: " << content << endl;
        cout << "File written successfully\n" << endl;
    }
};

// ReportGenerator depends on Report and FileWriter
class ReportGenerator {
public:
    // Method parameter dependency
    void generateReport(const Report& report, FileWriter& writer) {
        cout << "Generating report: " << report.getTitle() << endl;
        
        // Format the report
        string formattedContent = formatReport(report);
        
        // Use FileWriter temporarily to save
        writer.writeToFile(report.getTitle() + ".txt", formattedContent);
    }
    
    // Return type dependency
    Report createSalesReport() {
        // Local variable dependency
        time_t now = time(0);
        string timestamp = ctime(&now);
        
        return Report(
            "Sales Report",
            "Sales data for " + timestamp
        );
    }
    
private:
    // Local variable dependency
    string formatReport(const Report& report) {
        string formatted = "=== " + report.getTitle() + " ===\n";
        formatted += report.getData();
        formatted += "\n=========================";
        return formatted;
    }
};

// Usage Example
int main() {
    ReportGenerator generator;
    FileWriter writer;
    
    // Create a report
    Report salesReport = generator.createSalesReport();
    
    // Generate and save (dependencies used temporarily)
    generator.generateReport(salesReport, writer);
    
    // Another report
    Report monthlyReport("Monthly Summary", "Total sales: $50,000");
    generator.generateReport(monthlyReport, writer);
    
    return 0;
}
```

## UML Representation

In UML diagrams, dependency is represented by a **dashed arrow** pointing from the dependent class to the dependency:

```
Printer -----> Document
EmailService -----> EmailMessage
PaymentProcessor -----> Payment
```

## Dependency vs Other Relationships

| Feature | Dependency | Association | Aggregation | Composition |
|---------|------------|-------------|-------------|-------------|
| **Relationship** | Uses-a (temporary) | Uses-a | Has-a (weak) | Part-of (strong) |
| **Storage** | No member variable | Member variable | Member variable | Member variable |
| **Lifetime** | Method scope | Independent | Independent | Dependent |
| **Coupling** | Weakest | Weak | Medium | Strong |
| **UML Symbol** | Dashed arrow | Solid line | Hollow diamond | Filled diamond |

## When to Use Dependency

Use dependency when:
- You need to use a class temporarily within a method
- You don't need to store a reference to the dependent object
- You want the loosest possible coupling
- The relationship is purely functional, not structural
- You're following dependency injection principles

## Benefits

1. **Loose Coupling**: Minimal coupling between classes
2. **Flexibility**: Easy to change or replace dependencies
3. **Testability**: Easy to mock dependencies for testing
4. **Reusability**: Classes can be reused in different contexts
5. **Maintainability**: Changes to dependencies have minimal impact

## Dependency Injection

Dependency is closely related to the Dependency Injection (DI) design pattern:

```java
// Interface for loose coupling
interface IEmailSender {
    void send(String to, String message);
}

// Implementations
class SmtpEmailSender implements IEmailSender {
    public void send(String to, String message) {
        System.out.println("Sending via SMTP to: " + to);
    }
}

class MockEmailSender implements IEmailSender {
    public void send(String to, String message) {
        System.out.println("Mock: Email to " + to);
    }
}

// UserService depends on IEmailSender (injected)
class UserService {
    // Constructor injection
    public void registerUser(String email, IEmailSender emailSender) {
        System.out.println("Registering user: " + email);
        emailSender.send(email, "Welcome!");
    }
}
```

## Common Pitfalls

1. **Hidden Dependencies**: Using static methods creates hidden dependencies
2. **God Objects**: Too many dependencies indicate a class doing too much
3. **Circular Dependencies**: Class A depends on B, and B depends on A
4. **Tight Coupling**: Using concrete classes instead of interfaces
5. **Breaking Encapsulation**: Exposing too much to satisfy dependencies

## Best Practices

1. **Depend on Abstractions**: Use interfaces or abstract classes instead of concrete classes
2. **Dependency Injection**: Inject dependencies rather than creating them internally
3. **Keep Dependencies Minimal**: Follow the Single Responsibility Principle
4. **Use Interfaces**: Define contracts through interfaces for flexibility
5. **Constructor Injection**: Prefer constructor injection for required dependencies
6. **Method Injection**: Use method parameters for optional or variable dependencies
7. **Avoid Static Dependencies**: Static methods create hidden, hard-to-test dependencies
8. **Document Dependencies**: Clearly document what a class depends on

## Dependency Inversion Principle (DIP)

The Dependency Inversion Principle states:
- **High-level modules should not depend on low-level modules**. Both should depend on abstractions.
- **Abstractions should not depend on details**. Details should depend on abstractions.

```java
// Bad: High-level depends on low-level
class OrderProcessor {
    private MySQLDatabase database = new MySQLDatabase(); // Tight coupling
    
    public void processOrder(Order order) {
        database.save(order);
    }
}

// Good: Both depend on abstraction
interface IDatabase {
    void save(Object data);
}

class MySQLDatabase implements IDatabase {
    public void save(Object data) { /* MySQL specific code */ }
}

class OrderProcessor {
    private IDatabase database; // Depends on abstraction
    
    public OrderProcessor(IDatabase db) {
        this.database = db;
    }
    
    public void processOrder(Order order) {
        database.save(order);
    }
}
```

## Testing with Dependencies

Dependencies make testing easier through mocking:

```java
// Production code
class NotificationService {
    public void notifyUser(String userId, IEmailSender emailSender) {
        String email = getUserEmail(userId);
        emailSender.send(email, "You have a notification");
    }
    
    private String getUserEmail(String userId) {
        return userId + "@example.com";
    }
}

// Test code
class MockEmailSender implements IEmailSender {
    public List<String> sentEmails = new ArrayList<>();
    
    public void send(String to, String message) {
        sentEmails.add(to);
    }
}

// Test
void testNotification() {
    NotificationService service = new NotificationService();
    MockEmailSender mockSender = new MockEmailSender();
    
    service.notifyUser("user123", mockSender);
    
    assert mockSender.sentEmails.contains("user123@example.com");
}
```
