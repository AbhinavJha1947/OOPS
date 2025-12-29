// Abstract Class
abstract class Shape {
    // Abstract method (does not have a body)
    public abstract void draw();
    
    // Regular method
    public void commonFunction() {
        System.out.println("This is a shape.");
    }
}

class Circle extends Shape {
    public void draw() {
        System.out.println("Drawing Circle...");
    }
}

class Rectangle extends Shape {
    public void draw() {
        System.out.println("Drawing Rectangle...");
    }
}

public class Abstraction {
    public static void main(String[] args) {
        // Shape s = new Shape(); // Error: Cannot instantiate abstract class
        
        Shape s1 = new Circle();
        Shape s2 = new Rectangle();
        
        s1.commonFunction();
        s1.draw();
        
        s2.commonFunction();
        s2.draw();
    }
}
