#include <iostream>
#include <string>
using namespace std;

// Class Definition
class Car {
public:
    // Attributes
    string brand;
    string model;
    int year;

    // Method
    void displayInfo() {
        cout << "Brand: " << brand << ", Model: " << model << ", Year: " << year << endl;
    }
};

int main() {
    // Creating an Object of Car
    Car myCar1;
    
    // Setting attribute values
    myCar1.brand = "Toyota";
    myCar1.model = "Corolla";
    myCar1.year = 2022;

    // Creating another Object
    Car myCar2;
    myCar2.brand = "Honda";
    myCar2.model = "Civic";
    myCar2.year = 2023;

    // Calling method
    cout << "Car 1 Info:" << endl;
    myCar1.displayInfo();

    cout << "Car 2 Info:" << endl;
    myCar2.displayInfo();

    return 0;
}
