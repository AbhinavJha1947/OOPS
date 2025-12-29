#include <iostream>
#include <string>
using namespace std;

class Student {
private:
    // Private attribute
    string name;
    int age;

public:
    // Setter for name
    void setName(string n) {
        name = n;
    }

    // Getter for name
    string getName() {
        return name;
    }

    // Setter for age with validation
    void setAge(int a) {
        if(a > 0) {
            age = a;
        } else {
            cout << "Age cannot be negative or zero." << endl;
        }
    }

    // Getter for age
    int getAge() {
        return age;
    }
};

int main() {
    Student s;
    
    // s.name = "John"; // Error: name is private
    
    s.setName("John Doe");
    s.setAge(20);
    
    cout << "Name: " << s.getName() << endl;
    cout << "Age: " << s.getAge() << endl;
    
    s.setAge(-5); // Testing validation
    
    return 0;
}
