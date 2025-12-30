#include <iostream>
#include <string>
#include <vector>

using namespace std;

// Class demonstrating Constructors, Destructors, and RAII
class Resource {
private:
    string name;
    int* data; // Pointer to simulate dynamic resource management

public:
    // 1. Default Constructor
    Resource() {
        name = "Default Resource";
        data = new int(0);
        cout << "[Constructor] Default created: " << name << endl;
    }

    // 2. Parameterized Constructor
    Resource(string n) {
        name = n;
        data = new int(0);
        cout << "[Constructor] Created: " << name << endl;
    }

    // 3. Member Initializer List (Preferred in C++)
    Resource(string n, int value) : name(n), data(new int(value)) {
        cout << "[Constructor] Created with value: " << name << " (" << *data << ")" << endl;
    }

    // 4. Copy Constructor
    // Essential when class manages raw pointers (Deep Copy)
    Resource(const Resource& other) {
        name = other.name + " (Copy)";
        data = new int(*other.data); // Deep copy of data
        cout << "[Copy Constructor] Copied from: " << other.name << endl;
    }

    // 5. Destructor
    // Automatically called when object goes out of scope
    ~Resource() {
        cout << "[Destructor] Cleaning up: " << name << endl;
        delete data; // Prevent memory leak
    }

    void use() const {
        cout << "Using resource: " << name << " [Data: " << *data << "]" << endl;
    }
};

void createScope() {
    cout << "\n--- Entering Scope ---" << endl;
    Resource scoped("ScopedResource"); // Created here
    scoped.use();
    cout << "--- Exiting Scope ---" << endl;
} // ScopedResource destroyed here automatically

int main() {
    cout << "=== C++ Constructor & Destructor (RAII) Demo ===" << endl;

    // Default
    Resource* r1 = new Resource(); // Heap allocation
    r1->use();

    // Parameterized
    Resource r2("StackResource"); // Stack allocation (Preferred)
    r2.use();

    // Copy
    Resource r3(r2);
    r3.use();

    // RAII Demonstration
    createScope();

    // cleanup heap object manualy
    cout << "\nDeleting heap resource..." << endl;
    delete r1; // Destructor called

    cout << "\nEnd of Main" << endl;
    return 0;
}
