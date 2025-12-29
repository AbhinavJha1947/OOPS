package Inheritance;

// Base Class (Parent)
class Vehicle {
    protected String brand = "Generic Vehicle";

    public void honk() {
        System.out.println("Tuut, tuut!");
    }
}

// Derived Class (Child)
class Car extends Vehicle {
    private String modelName = "Mustang";

    public void displayInfo() {
        // Accessing inherited field
        System.out.println("Brand: " + brand);
        System.out.println("Model: " + modelName);
    }
}

public class Inheritance {
    public static void main(String[] args) {
        Car myCar = new Car();

        // Calling method from Child class
        myCar.displayInfo();

        // Calling inherited method from Parent class
        myCar.honk();
    }
}
