# Association

## Definition

**Association** represents a relationship between two separate classes that establishes a connection through their objects. It is a "uses-a" or "knows-a" relationship where objects can exist independently.

## Key Characteristics

- **Independent Lifecycle**: Objects can exist independently of each other
- **Bi-directional or Uni-directional**: Can be one-way or two-way relationship
- **No Ownership**: Neither class owns the other
- **Weak Relationship**: The weakest form of object relationship

## Types of Association

### 1. Unidirectional Association
One class knows about another class, but not vice versa.

**Example**: A `Customer` knows about their `Address`, but `Address` doesn't need to know about `Customer`.

### 2. Bidirectional Association
Both classes know about each other.

**Example**: A `Teacher` knows about their `Students`, and `Students` know about their `Teacher`.

## Real-World Examples

1. **Teacher and Student**: A teacher teaches students, and students learn from teachers
2. **Driver and Car**: A driver can drive a car, but both exist independently
3. **Customer and Order**: A customer places orders, but both can exist separately

## Code Examples

### Java Implementation

```java
// Unidirectional Association
class Address {
    private String street;
    private String city;
    
    public Address(String street, String city) {
        this.street = street;
        this.city = city;
    }
    
    public String getFullAddress() {
        return street + ", " + city;
    }
}

class Customer {
    private String name;
    private Address address; // Customer knows about Address
    
    public Customer(String name, Address address) {
        this.name = name;
        this.address = address;
    }
    
    public void displayInfo() {
        System.out.println("Customer: " + name);
        System.out.println("Address: " + address.getFullAddress());
    }
}

// Bidirectional Association
class Teacher {
    private String name;
    private List<Student> students; // Teacher knows about Students
    
    public Teacher(String name) {
        this.name = name;
        this.students = new ArrayList<>();
    }
    
    public void addStudent(Student student) {
        students.add(student);
        student.setTeacher(this); // Establishing bidirectional link
    }
}

class Student {
    private String name;
    private Teacher teacher; // Student knows about Teacher
    
    public Student(String name) {
        this.name = name;
    }
    
    public void setTeacher(Teacher teacher) {
        this.teacher = teacher;
    }
}
```

### C# Implementation

```csharp
// Unidirectional Association
public class Address {
    public string Street { get; set; }
    public string City { get; set; }
    
    public Address(string street, string city) {
        Street = street;
        City = city;
    }
    
    public string GetFullAddress() {
        return $"{Street}, {City}";
    }
}

public class Customer {
    public string Name { get; set; }
    public Address Address { get; set; } // Customer knows about Address
    
    public Customer(string name, Address address) {
        Name = name;
        Address = address;
    }
    
    public void DisplayInfo() {
        Console.WriteLine($"Customer: {Name}");
        Console.WriteLine($"Address: {Address.GetFullAddress()}");
    }
}

// Bidirectional Association
public class Teacher {
    public string Name { get; set; }
    public List<Student> Students { get; set; }
    
    public Teacher(string name) {
        Name = name;
        Students = new List<Student>();
    }
    
    public void AddStudent(Student student) {
        Students.Add(student);
        student.Teacher = this;
    }
}

public class Student {
    public string Name { get; set; }
    public Teacher Teacher { get; set; }
    
    public Student(string name) {
        Name = name;
    }
}
```

### C++ Implementation

```cpp
#include <iostream>
#include <string>
#include <vector>
using namespace std;

// Forward declaration for bidirectional association
class Student;

// Unidirectional Association
class Address {
private:
    string street;
    string city;
    
public:
    Address(string s, string c) : street(s), city(c) {}
    
    string getFullAddress() {
        return street + ", " + city;
    }
};

class Customer {
private:
    string name;
    Address* address; // Customer knows about Address
    
public:
    Customer(string n, Address* addr) : name(n), address(addr) {}
    
    void displayInfo() {
        cout << "Customer: " << name << endl;
        cout << "Address: " << address->getFullAddress() << endl;
    }
};

// Bidirectional Association
class Teacher {
private:
    string name;
    vector<Student*> students;
    
public:
    Teacher(string n) : name(n) {}
    
    void addStudent(Student* student);
    
    string getName() { return name; }
};

class Student {
private:
    string name;
    Teacher* teacher;
    
public:
    Student(string n) : name(n), teacher(nullptr) {}
    
    void setTeacher(Teacher* t) {
        teacher = t;
    }
    
    string getName() { return name; }
};

void Teacher::addStudent(Student* student) {
    students.push_back(student);
    student->setTeacher(this);
}
```

## Association vs Other Relationships

| Feature | Association | Aggregation | Composition |
|---------|-------------|-------------|-------------|
| **Relationship Type** | Uses-a | Has-a (weak) | Has-a (strong) |
| **Ownership** | No ownership | Weak ownership | Strong ownership |
| **Lifecycle** | Independent | Child can exist without parent | Child cannot exist without parent |
| **Example** | Teacher-Student | Department-Teacher | House-Room |

## When to Use Association

- When objects need to interact but remain independent
- When there's no clear ownership relationship
- When objects can exist and function separately
- When you need flexible, loosely-coupled design

## Best Practices

1. **Keep it Simple**: Don't create unnecessary associations
2. **Prefer Unidirectional**: Use bidirectional only when absolutely necessary
3. **Document Multiplicity**: Clearly define one-to-one, one-to-many, or many-to-many relationships
4. **Avoid Circular Dependencies**: Be careful with bidirectional associations to prevent circular references
5. **Use Interfaces**: Consider using interfaces to make associations more flexible
