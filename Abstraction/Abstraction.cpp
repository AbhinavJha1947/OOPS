#include <iostream>
using namespace std;

// Abstract Class
class Shape {
public:
    // Pure Virtual Function
    virtual void draw() = 0; 
    
    // Concrete method
    void commonFunction() {
        cout << "This is a shape." << endl;
    }
};

class Circle : public Shape {
public:
    void draw() override {
        cout << "Drawing Circle..." << endl;
    }
};

class Rectangle : public Shape {
public:
    void draw() override {
        cout << "Drawing Rectangle..." << endl;
    }
};

int main() {
    // Shape* s = new Shape(); // Error: Cannot instantiate abstract class
    
    Shape* s1 = new Circle();
    Shape* s2 = new Rectangle();
    
    s1->commonFunction();
    s1->draw();
    
    s2->commonFunction();
    s2->draw();
    
    delete s1;
    delete s2;
    
    return 0;
}
