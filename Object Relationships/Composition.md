# Composition

## Definition

**Composition** is a strong form of association that represents a "part-of" relationship where the child cannot exist independently of the parent. It represents **strong ownership** and a tight lifecycle binding.

## Key Characteristics

- **Part-Of Relationship**: The child is an integral part of the parent
- **Dependent Lifecycle**: Child's lifecycle is bound to the parent's lifecycle
- **Strong Ownership**: Parent exclusively owns and controls the child
- **Death Relationship**: When the parent is destroyed, all children are also destroyed
- **Exclusive Ownership**: A child belongs to exactly one parent

## Real-World Examples

1. **House and Rooms**: Rooms are part of a house; they cannot exist without the house
2. **Car and Engine**: An engine is part of a car; destroying the car destroys the engine
3. **Human and Heart**: A heart is part of a human body; it cannot exist independently
4. **Book and Pages**: Pages are part of a book; they don't make sense without the book
5. **Computer and Motherboard**: The motherboard is an integral part of the computer

## Composition vs Aggregation

| Feature | Composition | Aggregation |
|---------|-------------|-------------|
| **Relationship** | Part-of (strong) | Has-a (weak) |
| **Ownership** | Strong, exclusive | Weak, shared |
| **Lifecycle** | Dependent | Independent |
| **Child Existence** | Cannot exist without parent | Can exist without parent |
| **Deletion** | Parent deletion destroys children | Parent deletion doesn't affect children |
| **Example** | House-Room | Department-Teacher |

## Code Examples

### Java Implementation

```java
// Room class - cannot exist without House
class Room {
    private String name;
    private double area;
    
    public Room(String name, double area) {
        this.name = name;
        this.area = area;
        System.out.println("Room '" + name + "' created");
    }
    
    public String getName() {
        return name;
    }
    
    public double getArea() {
        return area;
    }
    
    public void displayInfo() {
        System.out.println("  - " + name + ": " + area + " sq ft");
    }
}

// House class - owns rooms (composition)
class House {
    private String address;
    private List<Room> rooms; // Composition: Rooms are part of House
    
    public House(String address) {
        this.address = address;
        this.rooms = new ArrayList<>();
        System.out.println("House created at: " + address);
        
        // Create rooms as part of house construction
        rooms.add(new Room("Living Room", 300.0));
        rooms.add(new Room("Bedroom", 200.0));
        rooms.add(new Room("Kitchen", 150.0));
    }
    
    // Add a new room during house lifetime
    public void addRoom(String name, double area) {
        rooms.add(new Room(name, area));
    }
    
    public void displayHouse() {
        System.out.println("\nHouse at " + address);
        System.out.println("Rooms:");
        for (Room room : rooms) {
            room.displayInfo();
        }
    }
    
    // When house is destroyed, rooms are automatically destroyed
    // Java's garbage collector handles this
}

// Usage Example
class CompositionDemo {
    public static void main(String[] args) {
        House myHouse = new House("123 Main Street");
        myHouse.addRoom("Garage", 400.0);
        myHouse.displayHouse();
        
        // When myHouse goes out of scope, all rooms are destroyed too
        myHouse = null;
        System.gc(); // Suggest garbage collection
        System.out.println("\nHouse destroyed, all rooms destroyed too");
    }
}
```

### C# Implementation

```csharp
using System;
using System.Collections.Generic;

// Engine class - cannot exist without Car
public class Engine {
    public string Type { get; private set; }
    public int Horsepower { get; private set; }
    
    public Engine(string type, int horsepower) {
        Type = type;
        Horsepower = horsepower;
        Console.WriteLine($"Engine created: {type} ({horsepower} HP)");
    }
    
    public void Start() {
        Console.WriteLine($"{Type} engine started");
    }
    
    public void Stop() {
        Console.WriteLine($"{Type} engine stopped");
    }
}

// Wheel class
public class Wheel {
    public string Size { get; private set; }
    
    public Wheel(string size) {
        Size = size;
        Console.WriteLine($"Wheel created: {size}");
    }
}

// Car class - owns engine and wheels (composition)
public class Car {
    public string Model { get; private set; }
    private Engine engine; // Composition: Engine is part of Car
    private List<Wheel> wheels; // Composition: Wheels are part of Car
    
    public Car(string model, string engineType, int horsepower) {
        Model = model;
        Console.WriteLine($"\nCar '{model}' being built...");
        
        // Create engine as part of car construction
        engine = new Engine(engineType, horsepower);
        
        // Create wheels as part of car construction
        wheels = new List<Wheel>();
        for (int i = 1; i <= 4; i++) {
            wheels.Add(new Wheel("18 inch"));
        }
        
        Console.WriteLine($"Car '{model}' built successfully\n");
    }
    
    public void Start() {
        Console.WriteLine($"Starting {Model}...");
        engine.Start();
    }
    
    public void Stop() {
        Console.WriteLine($"Stopping {Model}...");
        engine.Stop();
    }
    
    public void DisplayInfo() {
        Console.WriteLine($"\nCar: {Model}");
        Console.WriteLine($"Engine: {engine.Type} ({engine.Horsepower} HP)");
        Console.WriteLine($"Wheels: {wheels.Count} x {wheels[0].Size}");
    }
}

// Usage Example
class Program {
    static void Main() {
        Car myCar = new Car("Toyota Camry", "V6", 268);
        myCar.DisplayInfo();
        myCar.Start();
        myCar.Stop();
        
        Console.WriteLine("\n--- When car is destroyed, engine and wheels are destroyed too ---");
        myCar = null;
        GC.Collect();
    }
}
```

### C++ Implementation

```cpp
#include <iostream>
#include <string>
#include <vector>
using namespace std;

// Page class - cannot exist without Book
class Page {
private:
    int pageNumber;
    string content;
    
public:
    Page(int num, string cont) : pageNumber(num), content(cont) {
        cout << "Page " << pageNumber << " created" << endl;
    }
    
    ~Page() {
        cout << "Page " << pageNumber << " destroyed" << endl;
    }
    
    int getPageNumber() const { return pageNumber; }
    string getContent() const { return content; }
    
    void displayPage() const {
        cout << "  Page " << pageNumber << ": " << content.substr(0, 50);
        if (content.length() > 50) cout << "...";
        cout << endl;
    }
};

// Book class - owns pages (composition)
class Book {
private:
    string title;
    string author;
    vector<Page*> pages; // Composition: Pages are part of Book
    
public:
    Book(string t, string a) : title(t), author(a) {
        cout << "\nBook '" << title << "' by " << author << " being created..." << endl;
        
        // Create pages as part of book construction
        pages.push_back(new Page(1, "Chapter 1: Introduction to the story..."));
        pages.push_back(new Page(2, "Chapter 2: The adventure begins..."));
        pages.push_back(new Page(3, "Chapter 3: Rising action and challenges..."));
        
        cout << "Book created with " << pages.size() << " pages\n" << endl;
    }
    
    // Add a new page
    void addPage(string content) {
        int pageNum = pages.size() + 1;
        pages.push_back(new Page(pageNum, content));
    }
    
    void displayBook() const {
        cout << "\n=== " << title << " ===" << endl;
        cout << "Author: " << author << endl;
        cout << "Pages:" << endl;
        for (const auto& page : pages) {
            page->displayPage();
        }
    }
    
    // Destructor - destroys all pages when book is destroyed
    ~Book() {
        cout << "\nBook '" << title << "' being destroyed..." << endl;
        for (auto page : pages) {
            delete page; // Delete all pages (composition)
        }
        pages.clear();
        cout << "Book destroyed along with all its pages" << endl;
    }
};

// Usage Example
int main() {
    {
        Book* novel = new Book("The Great Adventure", "John Doe");
        novel->addPage("Chapter 4: The climax of the story...");
        novel->displayBook();
        
        cout << "\n--- Deleting book ---" << endl;
        delete novel; // This will destroy the book and all its pages
    }
    
    cout << "\nProgram ending..." << endl;
    return 0;
}
```

## UML Representation

In UML diagrams, composition is represented by a **filled diamond** on the parent side:

```
House ◆────── Room
Car   ◆────── Engine
Book  ◆────── Page
```

## When to Use Composition

Use composition when:
- The child is an integral part of the parent
- The child cannot exist meaningfully without the parent
- The child's lifecycle should be controlled by the parent
- You want strong encapsulation
- The relationship is clearly "part-of" rather than "has-a"

## Benefits

1. **Strong Encapsulation**: Internal parts are hidden from the outside world
2. **Clear Ownership**: Unambiguous lifecycle management
3. **Simplified Memory Management**: Parent controls child lifecycle
4. **Design Integrity**: Ensures that parts don't exist in invalid states
5. **Code Reusability**: Favor composition over inheritance principle

## Design Principle: Favor Composition Over Inheritance

Composition is often preferred over inheritance because:
- **Flexibility**: Easier to change behavior at runtime
- **Loose Coupling**: Reduces tight coupling between classes
- **Multiple Behaviors**: Can combine multiple behaviors easily
- **No Diamond Problem**: Avoids multiple inheritance issues
- **Better Encapsulation**: Implementation details are hidden

### Example: Composition vs Inheritance

```java
// Bad: Using inheritance
class Bird {
    void fly() { }
}

class Penguin extends Bird {
    @Override
    void fly() {
        throw new UnsupportedOperationException("Penguins can't fly!");
    }
}

// Good: Using composition
interface FlyBehavior {
    void fly();
}

class CanFly implements FlyBehavior {
    public void fly() {
        System.out.println("Flying high!");
    }
}

class CannotFly implements FlyBehavior {
    public void fly() {
        System.out.println("Cannot fly");
    }
}

class Bird {
    private FlyBehavior flyBehavior; // Composition
    
    public Bird(FlyBehavior fb) {
        this.flyBehavior = fb;
    }
    
    public void performFly() {
        flyBehavior.fly();
    }
}

class Sparrow extends Bird {
    public Sparrow() {
        super(new CanFly());
    }
}

class Penguin extends Bird {
    public Penguin() {
        super(new CannotFly());
    }
}
```

## Common Pitfalls

1. **Confusion with Aggregation**: Understanding when to use composition vs aggregation
2. **Over-composition**: Creating too many small components
3. **Memory Leaks**: In C++, forgetting to delete composed objects
4. **Circular Dependencies**: Avoiding circular composition relationships
5. **Deep Nesting**: Too many levels of composition can complicate code

## Best Practices

1. **Create in Constructor**: Initialize composed objects in the parent's constructor
2. **Destroy in Destructor**: Clean up composed objects in the parent's destructor (C++)
3. **Use Private Members**: Keep composed objects private for strong encapsulation
4. **Document Relationships**: Clearly document composition relationships
5. **Consider Smart Pointers**: In C++, use `unique_ptr` for automatic cleanup
6. **Immutability**: Make composed objects immutable where possible
7. **Defensive Copying**: Be careful when exposing composed objects
