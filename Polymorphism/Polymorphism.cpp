#include <iostream>
using namespace std;

class Calculator {
public:
    // 1. Compile-time Polymorphism (Function Overloading)
    int add(int a, int b) {
        return a + b;
    }

    double add(double a, double b) {
        return a + b;
    }
};

class Animal {
public:
    // Virtual function for Run-time Polymorphism
    virtual void makeSound() {
        cout << "Animal makes a sound" << endl;
    }
};

class Dog : public Animal {
public:
    // 2. Run-time Polymorphism (Method Overriding)
    void makeSound() override {
        cout << "Dog barks" << endl;
    }
};

int main() {
    // Test Overloading
    Calculator calc;
    cout << "Sum (int): " << calc.add(5, 10) << endl;
    cout << "Sum (double): " << calc.add(5.5, 10.5) << endl;

    // Test Overriding
    Animal* myAnimal = new Dog(); // Upcasting using pointer
    myAnimal->makeSound(); // Calls Dog's method at runtime

    delete myAnimal; // Clean up memory
    return 0;
}
