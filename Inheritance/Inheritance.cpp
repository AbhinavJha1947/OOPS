#include <iostream>
#include <string>

// Base Class
class Vehicle {
public:
    std::string brand = "Generic Vehicle";

    void honk() {
        std::cout << "Tuut, tuut!\n";
    }
};

// Derived Class
class Car : public Vehicle {
public:
    std::string modelName = "Mustang";
};

int main() {
    Car myCar;

    // Accessing inherited attribute
    std::cout << "Brand: " << myCar.brand << "\n";
    std::cout << "Model: " << myCar.modelName << "\n";

    // Calling inherited method
    myCar.honk();

    return 0;
}
