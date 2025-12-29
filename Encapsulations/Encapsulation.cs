using System;

namespace EncapsulationExample {
    class Student {
        // Private field
        private string name;
        private int age;

        // Public Property for Name
        public string Name {
            get { return name; }
            set { name = value; }
        }

        // Public Property for Age with validation
        public int Age {
            get { return age; }
            set {
                if (value > 0) {
                    age = value;
                } else {
                    Console.WriteLine("Age cannot be negative or zero.");
                }
            }
        }
    }

    class Program {
        static void Main(string[] args) {
            Student s = new Student();
            
            // s.name = "John"; // Error: name is private
            
            // Using Properties
            s.Name = "John Doe";
            s.Age = 20;
            
            Console.WriteLine($"Name: {s.Name}");
            Console.WriteLine($"Age: {s.Age}");
            
            s.Age = -5; // Testing validation
        }
    }
}
