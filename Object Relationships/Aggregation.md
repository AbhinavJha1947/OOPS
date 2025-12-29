# Aggregation

## Definition

**Aggregation** is a specialized form of Association that represents a "has-a" relationship where the child can exist independently of the parent. It represents a **weak ownership** relationship.

## Key Characteristics

- **Has-A Relationship**: One object contains or owns another
- **Independent Lifecycle**: The contained object can exist without the container
- **Weak Ownership**: The parent doesn't control the child's lifecycle
- **Part-Whole Relationship**: Represents parts that make up a whole
- **Shared Ownership**: Multiple parents can share the same child object

## Real-World Examples

1. **Department and Teacher**: A department has teachers, but teachers can exist without the department
2. **Library and Books**: A library has books, but books can exist independently
3. **Team and Players**: A team has players, but players exist even if the team disbands
4. **University and Students**: A university has students, but students exist independently

## Aggregation vs Composition

| Feature | Aggregation | Composition |
|---------|-------------|-------------|
| **Ownership** | Weak (has-a) | Strong (part-of) |
| **Lifecycle** | Independent | Dependent |
| **Child Existence** | Can exist without parent | Cannot exist without parent |
| **Example** | Department-Teacher | House-Room |
| **Deletion** | Deleting parent doesn't delete child | Deleting parent deletes child |

## Code Examples

### Java Implementation

```java
// Teacher class - can exist independently
class Teacher {
    private String name;
    private String subject;
    
    public Teacher(String name, String subject) {
        this.name = name;
        this.subject = subject;
    }
    
    public String getName() {
        return name;
    }
    
    public String getSubject() {
        return subject;
    }
    
    public void teach() {
        System.out.println(name + " is teaching " + subject);
    }
}

// Department class - has teachers (aggregation)
class Department {
    private String deptName;
    private List<Teacher> teachers; // Aggregation: Teachers exist independently
    
    public Department(String deptName) {
        this.deptName = deptName;
        this.teachers = new ArrayList<>();
    }
    
    // Add existing teacher to department
    public void addTeacher(Teacher teacher) {
        teachers.add(teacher);
        System.out.println(teacher.getName() + " added to " + deptName);
    }
    
    // Remove teacher from department (teacher still exists)
    public void removeTeacher(Teacher teacher) {
        teachers.remove(teacher);
        System.out.println(teacher.getName() + " removed from " + deptName);
    }
    
    public void listTeachers() {
        System.out.println("Teachers in " + deptName + ":");
        for (Teacher teacher : teachers) {
            System.out.println("- " + teacher.getName() + " (" + teacher.getSubject() + ")");
        }
    }
}

// Usage Example
class AggregationDemo {
    public static void main(String[] args) {
        // Create teachers independently
        Teacher t1 = new Teacher("Dr. Smith", "Mathematics");
        Teacher t2 = new Teacher("Prof. Johnson", "Physics");
        
        // Create department and add teachers
        Department mathDept = new Department("Mathematics Department");
        mathDept.addTeacher(t1);
        mathDept.addTeacher(t2);
        
        mathDept.listTeachers();
        
        // Remove teacher from department
        mathDept.removeTeacher(t1);
        
        // Teacher still exists and can work independently
        t1.teach(); // Dr. Smith is still functional
    }
}
```

### C# Implementation

```csharp
using System;
using System.Collections.Generic;

// Book class - exists independently
public class Book {
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    
    public Book(string title, string author, string isbn) {
        Title = title;
        Author = author;
        ISBN = isbn;
    }
    
    public void DisplayInfo() {
        Console.WriteLine($"{Title} by {Author} (ISBN: {ISBN})");
    }
}

// Library class - has books (aggregation)
public class Library {
    public string Name { get; set; }
    private List<Book> books; // Aggregation: Books exist independently
    
    public Library(string name) {
        Name = name;
        books = new List<Book>();
    }
    
    // Add existing book to library
    public void AddBook(Book book) {
        books.Add(book);
        Console.WriteLine($"Added '{book.Title}' to {Name}");
    }
    
    // Remove book from library (book still exists)
    public void RemoveBook(Book book) {
        books.Remove(book);
        Console.WriteLine($"Removed '{book.Title}' from {Name}");
    }
    
    public void ListBooks() {
        Console.WriteLine($"\nBooks in {Name}:");
        foreach (var book in books) {
            Console.WriteLine($"- {book.Title} by {book.Author}");
        }
    }
}

// Usage Example
class Program {
    static void Main() {
        // Create books independently
        Book book1 = new Book("1984", "George Orwell", "978-0451524935");
        Book book2 = new Book("To Kill a Mockingbird", "Harper Lee", "978-0061120084");
        
        // Create library and add books
        Library cityLibrary = new Library("City Central Library");
        cityLibrary.AddBook(book1);
        cityLibrary.AddBook(book2);
        
        cityLibrary.ListBooks();
        
        // Remove book from library
        cityLibrary.RemoveBook(book1);
        
        // Book still exists independently
        book1.DisplayInfo(); // Book is still functional
    }
}
```

### C++ Implementation

```cpp
#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
using namespace std;

// Player class - exists independently
class Player {
private:
    string name;
    int jerseyNumber;
    string position;
    
public:
    Player(string n, int num, string pos) 
        : name(n), jerseyNumber(num), position(pos) {}
    
    string getName() const { return name; }
    int getJerseyNumber() const { return jerseyNumber; }
    string getPosition() const { return position; }
    
    void displayInfo() const {
        cout << name << " (#" << jerseyNumber << ") - " << position << endl;
    }
    
    void practice() {
        cout << name << " is practicing..." << endl;
    }
};

// Team class - has players (aggregation)
class Team {
private:
    string teamName;
    vector<Player*> players; // Aggregation: Players exist independently
    
public:
    Team(string name) : teamName(name) {}
    
    // Add existing player to team
    void addPlayer(Player* player) {
        players.push_back(player);
        cout << player->getName() << " joined " << teamName << endl;
    }
    
    // Remove player from team (player still exists)
    void removePlayer(Player* player) {
        auto it = find(players.begin(), players.end(), player);
        if (it != players.end()) {
            players.erase(it);
            cout << player->getName() << " left " << teamName << endl;
        }
    }
    
    void listPlayers() const {
        cout << "\nPlayers in " << teamName << ":" << endl;
        for (const auto& player : players) {
            cout << "- ";
            player->displayInfo();
        }
    }
    
    // Destructor doesn't delete players (aggregation)
    ~Team() {
        cout << teamName << " disbanded, but players still exist" << endl;
        // Note: We don't delete players here because they exist independently
    }
};

// Usage Example
int main() {
    // Create players independently
    Player* p1 = new Player("Michael Jordan", 23, "Shooting Guard");
    Player* p2 = new Player("LeBron James", 6, "Small Forward");
    Player* p3 = new Player("Kobe Bryant", 24, "Shooting Guard");
    
    {
        // Create team and add players
        Team bulls("Chicago Bulls");
        bulls.addPlayer(p1);
        bulls.addPlayer(p2);
        bulls.listPlayers();
        
        // Remove a player
        bulls.removePlayer(p2);
        
        // Team goes out of scope here
    }
    
    // Players still exist and can practice
    p1->practice();
    p2->practice();
    p3->practice();
    
    // Clean up players manually
    delete p1;
    delete p2;
    delete p3;
    
    return 0;
}
```

## UML Representation

In UML diagrams, aggregation is represented by a **hollow diamond** on the parent side:

```
Department ◇────── Teacher
Library    ◇────── Book
Team       ◇────── Player
```

## When to Use Aggregation

Use aggregation when:
- The child object can exist independently of the parent
- Multiple parents can share the same child object
- The relationship is "has-a" but not "owns"
- You want to model a collection or grouping relationship
- The child's lifecycle is not controlled by the parent

## Benefits

1. **Flexibility**: Objects can be shared among multiple containers
2. **Reusability**: Child objects can be reused in different contexts
3. **Independence**: Objects maintain their own lifecycle
4. **Memory Efficiency**: Same object can be referenced by multiple parents

## Common Pitfalls

1. **Memory Management**: In languages like C++, be careful not to double-delete shared objects
2. **Confusion with Composition**: Understand when to use aggregation vs composition
3. **Circular References**: Be cautious with bidirectional aggregation relationships
4. **Null Checks**: Always check if aggregated objects exist before using them

## Best Practices

1. **Use Smart Pointers**: In C++, use `shared_ptr` for automatic memory management
2. **Clear Documentation**: Clearly document ownership and lifecycle expectations
3. **Consider Composition**: If the child should not exist without the parent, use composition instead
4. **Immutability**: Consider making aggregated collections immutable where appropriate
5. **Defensive Copying**: Be careful when exposing aggregated collections publicly
