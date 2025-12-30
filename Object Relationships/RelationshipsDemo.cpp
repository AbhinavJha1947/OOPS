#include <iostream>
#include <string>
#include <vector>

using namespace std;

// 1. Association
class Car {
public:
    string model;
    Car(string m) : model(m) {}
};

class Driver {
public:
    string name;
    Driver(string n) : name(n) {}

    void drive(Car* car) { // Uses a pointer/reference to Car
        cout << name << " is driving " << car->model << endl;
    }
};

// 2. Aggregation (Weak ownership uses pointers)
class Professor {
public:
    string name;
    Professor(string n) : name(n) {}
};

class University {
public:
    string name;
    vector<Professor*> professors; // Holds pointers. Does NOT own memory strictly.

    University(string n) : name(n) {}

    void addProfessor(Professor* p) {
        professors.push_back(p);
    }
    // Destructor does NOT delete professors
};

// 3. Composition (Strong ownership)
class Engine {
public:
    string type;
    Engine(string t) : type(t) {
        cout << "  [Engine created]" << endl;
    }
    ~Engine() {
        cout << "  [Engine destroyed]" << endl;
    }
};

class Airplane {
private:
    Engine* engine; // Composition via pointer for explicit control, or object member
public:
    Airplane() {
        cout << "Airplane created." << endl;
        engine = new Engine("Jet Engine");
    }

    ~Airplane() {
        // Airplane is responsible for destroying its parts
        delete engine;
        cout << "Airplane destroyed." << endl;
    }
};

int main() {
    cout << "=== C++ Object Relationships Demo ===" << endl;

    // Association
    Car car("Mustang");
    Driver driver("Dave");
    driver.drive(&car);

    // Aggregation
    cout << "\n--- Aggregation ---" << endl;
    Professor* p1 = new Professor("Dr. Jones");
    {
        University u("Tech University");
        u.addProfessor(p1);
        cout << "University has " << p1->name << endl;
    } // University destroyed here
    cout << "University destroyed, but " << p1->name << " still exists." << endl;
    delete p1; // Manual cleanup of independent object

    // Composition
    cout << "\n--- Composition ---" << endl;
    {
        Airplane plane;
        // Engine is created inside
    } // Plane destroyed, Engine destroyed automatically

    return 0;
}
