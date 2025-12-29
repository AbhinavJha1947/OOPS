package Encapsulations;

class Student {
    // Private attributes
    private String name;
    private int age;

    // Setter for name
    public void setName(String name) {
        this.name = name;
    }

    // Getter for name
    public String getName() {
        return name;
    }

    // Setter for age with validation
    public void setAge(int age) {
        if (age > 0) {
            this.age = age;
        } else {
            System.out.println("Age cannot be negative or zero.");
        }
    }

    // Getter for age
    public int getAge() {
        return age;
    }
}

public class Encapsulation {
    public static void main(String[] args) {
        Student s = new Student();

        // s.name = "John"; // Error: name is private

        s.setName("John Doe");
        s.setAge(20);

        System.out.println("Name: " + s.getName());
        System.out.println("Age: " + s.getAge());

        s.setAge(-5); // Testing validation
    }
}
