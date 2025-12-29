package Polymorphism;

class Calculator {
    // 1. Compile-time Polymorphism (Method Overloading)
    public int add(int a, int b) {
        return a + b;
    }

    public double add(double a, double b) {
        return a + b;
    }
}

class Animal {
    public void makeSound() {
        System.out.println("Animal makes a sound");
    }
}

class Dog extends Animal {
    // 2. Run-time Polymorphism (Method Overriding)
    @Override
    public void makeSound() {
        System.out.println("Dog barks");
    }
}

public class Polymorphism {
    public static void main(String[] args) {
        // Test Overloading
        Calculator calc = new Calculator();
        System.out.println("Sum (int): " + calc.add(5, 10));
        System.out.println("Sum (double): " + calc.add(5.5, 10.5));

        // Test Overriding
        Animal myAnimal = new Dog(); // Upcasting
        myAnimal.makeSound(); // Calls Dog's method at runtime
    }
}
