using System;

namespace AbstractionExample {
    // Abstract Class
    abstract class Shape {
        // Abstract method (does not have a body)
        public abstract void Draw();
        
        // Regular method
        public void CommonFunction() {
            Console.WriteLine("This is a shape.");
        }
    }

    class Circle : Shape {
        public override void Draw() {
            Console.WriteLine("Drawing Circle...");
        }
    }

    class Rectangle : Shape {
        public override void Draw() {
            Console.WriteLine("Drawing Rectangle...");
        }
    }

    class Program {
        static void Main(string[] args) {
            // Shape s = new Shape(); // Error: Cannot create an instance of the abstract class or interface
            
            Shape s1 = new Circle();
            Shape s2 = new Rectangle();
            
            s1.CommonFunction();
            s1.Draw();
            
            s2.CommonFunction();
            s2.Draw();
        }
    }
}
