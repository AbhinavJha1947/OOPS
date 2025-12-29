#include <iostream>
#include <string>
using namespace std;

class Example {
public:
    string name;
    static int count; // Static variable declaration

    Example(string name) {
        this->name = name; // 'this' is a pointer to the current object
        count++;
    }

    // Const member function: cannot modify object state
    void display() const {
        cout << "Name: " << this->name << endl;
        // name = "New Name"; // Error: Cannot modify in const function
    }

    static void showCount() {
        cout << "Total Objects: " << count << endl;
    }
};

// Initialize static variable
int Example::count = 0;

int main() {
    Example e1("Object 1");
    e1.display();

    Example e2("Object 2");
    e2.display();

    // Call static method
    Example::showCount();

    return 0;
}
