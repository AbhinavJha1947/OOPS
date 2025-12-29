// Class Definition
class Car {
    // Attributes
    String brand;
    String model;
    int year;

    // Method
    public void displayInfo() {
        System.out.println("Brand: " + brand + ", Model: " + model + ", Year: " + year);
    }
}

public class ClassAndObject {
    public static void main(String[] args) {
        // Creating an Object of Car
        Car myCar1 = new Car();

        // Setting attribute values
        myCar1.brand = "Toyota";
        myCar1.model = "Corolla";
        myCar1.year = 2022;

        // Creating another Object
        Car myCar2 = new Car();
        myCar2.brand = "Honda";
        myCar2.model = "Civic";
        myCar2.year = 2023;

        // Calling method
        System.out.println("Car 1 Info:");
        myCar1.displayInfo();

        System.out.println("Car 2 Info:");
        myCar2.displayInfo();
    }
}
