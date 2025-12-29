# Cardinality in Object Relationships

## Definition

**Cardinality** defines the numerical relationships between objects in an association, aggregation, or composition relationship. It specifies how many instances of one class can be associated with instances of another class.

## Types of Cardinality

### 1. One-to-One (1:1)
### 2. One-to-Many (1:N or 1:*)
### 3. Many-to-One (N:1 or *:1)
### 4. Many-to-Many (M:N or *:*)

## 1. One-to-One (1:1) Relationship

**Definition**: Each instance of Class A is associated with exactly one instance of Class B, and vice versa.

### Real-World Examples
- Person and Passport
- Country and Capital City
- Employee and Desk (in assigned seating)
- User and UserProfile

### Java Implementation

```java
// One-to-One: Person and Passport
class Passport {
    private String passportNumber;
    private String country;
    private Person owner; // Back reference
    
    public Passport(String number, String country) {
        this.passportNumber = number;
        this.country = country;
    }
    
    public void setOwner(Person person) {
        this.owner = person;
    }
    
    public String getPassportNumber() {
        return passportNumber;
    }
    
    public void displayInfo() {
        System.out.println("Passport: " + passportNumber + " (" + country + ")");
        if (owner != null) {
            System.out.println("Owner: " + owner.getName());
        }
    }
}

class Person {
    private String name;
    private Passport passport; // One person has one passport
    
    public Person(String name) {
        this.name = name;
    }
    
    public void assignPassport(Passport passport) {
        this.passport = passport;
        passport.setOwner(this); // Bidirectional relationship
    }
    
    public String getName() {
        return name;
    }
    
    public void displayInfo() {
        System.out.println("Person: " + name);
        if (passport != null) {
            System.out.println("Passport: " + passport.getPassportNumber());
        }
    }
}

// Usage
class OneToOneDemo {
    public static void main(String[] args) {
        Person john = new Person("John Doe");
        Passport passport = new Passport("A1234567", "USA");
        
        john.assignPassport(passport);
        john.displayInfo();
    }
}
```

## 2. One-to-Many (1:N) Relationship

**Definition**: Each instance of Class A can be associated with multiple instances of Class B, but each instance of Class B is associated with only one instance of Class A.

### Real-World Examples
- Department and Employees
- Customer and Orders
- Author and Books
- Parent and Children

### Java Implementation

```java
import java.util.*;

// One-to-Many: Department and Employees
class Employee {
    private String name;
    private String employeeId;
    private Department department; // Many employees belong to one department
    
    public Employee(String name, String id) {
        this.name = name;
        this.employeeId = id;
    }
    
    public void setDepartment(Department dept) {
        this.department = dept;
    }
    
    public String getName() {
        return name;
    }
    
    public void displayInfo() {
        System.out.println("Employee: " + name + " (ID: " + employeeId + ")");
        if (department != null) {
            System.out.println("Department: " + department.getName());
        }
    }
}

class Department {
    private String name;
    private List<Employee> employees; // One department has many employees
    
    public Department(String name) {
        this.name = name;
        this.employees = new ArrayList<>();
    }
    
    public void addEmployee(Employee emp) {
        employees.add(emp);
        emp.setDepartment(this);
    }
    
    public void removeEmployee(Employee emp) {
        employees.remove(emp);
        emp.setDepartment(null);
    }
    
    public String getName() {
        return name;
    }
    
    public void listEmployees() {
        System.out.println("\nDepartment: " + name);
        System.out.println("Employees (" + employees.size() + "):");
        for (Employee emp : employees) {
            System.out.println("  - " + emp.getName());
        }
    }
}

// Usage
class OneToManyDemo {
    public static void main(String[] args) {
        Department engineering = new Department("Engineering");
        
        Employee emp1 = new Employee("Alice", "E001");
        Employee emp2 = new Employee("Bob", "E002");
        Employee emp3 = new Employee("Charlie", "E003");
        
        engineering.addEmployee(emp1);
        engineering.addEmployee(emp2);
        engineering.addEmployee(emp3);
        
        engineering.listEmployees();
    }
}
```

### C# Implementation

```csharp
using System;
using System.Collections.Generic;

// One-to-Many: Author and Books
public class Book {
    public string Title { get; set; }
    public string ISBN { get; set; }
    public Author Author { get; set; } // Many books, one author
    
    public Book(string title, string isbn) {
        Title = title;
        ISBN = isbn;
    }
    
    public void DisplayInfo() {
        Console.WriteLine($"  - {Title} (ISBN: {ISBN})");
    }
}

public class Author {
    public string Name { get; set; }
    private List<Book> books; // One author, many books
    
    public Author(string name) {
        Name = name;
        books = new List<Book>();
    }
    
    public void AddBook(string title, string isbn) {
        Book book = new Book(title, isbn);
        book.Author = this;
        books.Add(book);
    }
    
    public void ListBooks() {
        Console.WriteLine($"\nAuthor: {Name}");
        Console.WriteLine($"Books ({books.Count}):");
        foreach (var book in books) {
            book.DisplayInfo();
        }
    }
}

// Usage
class Program {
    static void Main() {
        Author author = new Author("J.K. Rowling");
        author.AddBook("Harry Potter and the Philosopher's Stone", "978-0747532699");
        author.AddBook("Harry Potter and the Chamber of Secrets", "978-0747538493");
        author.AddBook("Harry Potter and the Prisoner of Azkaban", "978-0747542155");
        
        author.ListBooks();
    }
}
```

## 3. Many-to-Many (M:N) Relationship

**Definition**: Multiple instances of Class A can be associated with multiple instances of Class B, and vice versa.

### Real-World Examples
- Students and Courses
- Actors and Movies
- Tags and Blog Posts
- Doctors and Patients

### Java Implementation

```java
import java.util.*;

// Many-to-Many: Students and Courses
class Course {
    private String courseName;
    private String courseCode;
    private List<Student> enrolledStudents; // Many courses have many students
    
    public Course(String name, String code) {
        this.courseName = name;
        this.courseCode = code;
        this.enrolledStudents = new ArrayList<>();
    }
    
    public void enrollStudent(Student student) {
        if (!enrolledStudents.contains(student)) {
            enrolledStudents.add(student);
            student.addCourse(this); // Bidirectional
        }
    }
    
    public void removeStudent(Student student) {
        enrolledStudents.remove(student);
        student.removeCourse(this);
    }
    
    public String getCourseName() {
        return courseName;
    }
    
    public String getCourseCode() {
        return courseCode;
    }
    
    public void listStudents() {
        System.out.println("\nCourse: " + courseName + " (" + courseCode + ")");
        System.out.println("Enrolled Students (" + enrolledStudents.size() + "):");
        for (Student student : enrolledStudents) {
            System.out.println("  - " + student.getName());
        }
    }
}

class Student {
    private String name;
    private String studentId;
    private List<Course> courses; // Many students can take many courses
    
    public Student(String name, String id) {
        this.name = name;
        this.studentId = id;
        this.courses = new ArrayList<>();
    }
    
    void addCourse(Course course) {
        if (!courses.contains(course)) {
            courses.add(course);
        }
    }
    
    void removeCourse(Course course) {
        courses.remove(course);
    }
    
    public String getName() {
        return name;
    }
    
    public void listCourses() {
        System.out.println("\nStudent: " + name + " (ID: " + studentId + ")");
        System.out.println("Enrolled Courses (" + courses.size() + "):");
        for (Course course : courses) {
            System.out.println("  - " + course.getCourseName() + " (" + course.getCourseCode() + ")");
        }
    }
}

// Usage
class ManyToManyDemo {
    public static void main(String[] args) {
        // Create students
        Student alice = new Student("Alice", "S001");
        Student bob = new Student("Bob", "S002");
        Student charlie = new Student("Charlie", "S003");
        
        // Create courses
        Course math = new Course("Mathematics", "MATH101");
        Course physics = new Course("Physics", "PHYS101");
        Course chemistry = new Course("Chemistry", "CHEM101");
        
        // Enroll students in courses (many-to-many)
        math.enrollStudent(alice);
        math.enrollStudent(bob);
        
        physics.enrollStudent(alice);
        physics.enrollStudent(charlie);
        
        chemistry.enrollStudent(bob);
        chemistry.enrollStudent(charlie);
        
        // Display course enrollment
        math.listStudents();
        physics.listStudents();
        
        // Display student courses
        alice.listCourses();
        bob.listCourses();
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

// Forward declarations
class Movie;
class Actor;

// Many-to-Many: Actors and Movies
class Movie {
private:
    string title;
    int year;
    vector<Actor*> cast;
    
public:
    Movie(string t, int y) : title(t), year(y) {}
    
    void addActor(Actor* actor);
    
    string getTitle() const { return title; }
    int getYear() const { return year; }
    
    void listCast() const;
};

class Actor {
private:
    string name;
    vector<Movie*> movies;
    
public:
    Actor(string n) : name(n) {}
    
    void addMovie(Movie* movie) {
        if (find(movies.begin(), movies.end(), movie) == movies.end()) {
            movies.push_back(movie);
        }
    }
    
    string getName() const { return name; }
    
    void listMovies() const {
        cout << "\nActor: " << name << endl;
        cout << "Movies (" << movies.size() << "):" << endl;
        for (const auto& movie : movies) {
            cout << "  - " << movie->getTitle() << " (" << movie->getYear() << ")" << endl;
        }
    }
};

void Movie::addActor(Actor* actor) {
    if (find(cast.begin(), cast.end(), actor) == cast.end()) {
        cast.push_back(actor);
        actor->addMovie(this);
    }
}

void Movie::listCast() const {
    cout << "\nMovie: " << title << " (" << year << ")" << endl;
    cout << "Cast (" << cast.size() << "):" << endl;
    for (const auto& actor : cast) {
        cout << "  - " << actor->getName() << endl;
    }
}

// Usage
int main() {
    // Create actors
    Actor* leo = new Actor("Leonardo DiCaprio");
    Actor* kate = new Actor("Kate Winslet");
    Actor* tom = new Actor("Tom Hardy");
    
    // Create movies
    Movie* titanic = new Movie("Titanic", 1997);
    Movie* inception = new Movie("Inception", 2010);
    Movie* revenant = new Movie("The Revenant", 2015);
    
    // Cast actors in movies (many-to-many)
    titanic->addActor(leo);
    titanic->addActor(kate);
    
    inception->addActor(leo);
    inception->addActor(tom);
    
    revenant->addActor(leo);
    revenant->addActor(tom);
    
    // Display movie casts
    titanic->listCast();
    inception->listCast();
    
    // Display actor filmographies
    leo->listMovies();
    tom->listMovies();
    
    // Cleanup
    delete leo;
    delete kate;
    delete tom;
    delete titanic;
    delete inception;
    delete revenant;
    
    return 0;
}
```

## Cardinality Notation

### UML Notation
```
Class A ────── 1      1 ────── Class B     (One-to-One)
Class A ────── 1      * ────── Class B     (One-to-Many)
Class A ────── *      * ────── Class B     (Many-to-Many)
```

### Common Symbols
- `1` : Exactly one
- `0..1` : Zero or one
- `*` or `0..*` : Zero or more
- `1..*` : One or more
- `n..m` : Between n and m

## Database Implementation

### One-to-One
```sql
CREATE TABLE Person (
    PersonID INT PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Passport (
    PassportNumber VARCHAR(20) PRIMARY KEY,
    Country VARCHAR(50),
    PersonID INT UNIQUE, -- UNIQUE ensures 1:1
    FOREIGN KEY (PersonID) REFERENCES Person(PersonID)
);
```

### One-to-Many
```sql
CREATE TABLE Department (
    DeptID INT PRIMARY KEY,
    DeptName VARCHAR(100)
);

CREATE TABLE Employee (
    EmpID INT PRIMARY KEY,
    Name VARCHAR(100),
    DeptID INT, -- Foreign key, no UNIQUE (allows many)
    FOREIGN KEY (DeptID) REFERENCES Department(DeptID)
);
```

### Many-to-Many (Junction Table)
```sql
CREATE TABLE Student (
    StudentID INT PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Course (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(100)
);

-- Junction/Bridge table for many-to-many
CREATE TABLE Enrollment (
    StudentID INT,
    CourseID INT,
    EnrollmentDate DATE,
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
);
```

## Best Practices

1. **Choose Appropriate Cardinality**: Understand the business rules before deciding
2. **Use Collection Types**: Use `List`, `Set`, or `Map` for one-to-many and many-to-many
3. **Bidirectional Consistency**: Keep both sides of relationship synchronized
4. **Avoid Circular References**: Be careful with bidirectional relationships
5. **Lazy Loading**: Consider lazy loading for large collections
6. **Junction Tables**: Use junction tables for many-to-many in databases
7. **Cascading Operations**: Define cascade rules (delete, update) appropriately
8. **Null Checks**: Always check for null in optional relationships (0..1)

## Common Pitfalls

1. **Memory Leaks**: Bidirectional relationships can cause memory leaks if not managed properly
2. **Infinite Loops**: toString() or serialization can cause infinite loops in bidirectional relationships
3. **Inconsistent State**: Forgetting to update both sides of bidirectional relationship
4. **Performance**: Not considering performance implications of loading large collections
5. **Wrong Cardinality**: Misunderstanding business rules leads to wrong cardinality choice

## Summary Table

| Cardinality | Example | Implementation | Database |
|-------------|---------|----------------|----------|
| **1:1** | Person-Passport | Single reference in both | Foreign key with UNIQUE |
| **1:N** | Department-Employee | Collection in parent | Foreign key in child |
| **N:1** | Employee-Department | Single reference in child | Foreign key in child |
| **M:N** | Student-Course | Collections in both | Junction/Bridge table |
