using System;

namespace ClassObjectExample {
    // Class Definition
    class Car {
        // Attributes (Fields)
        public string brand;
        public string model;
        public int year;

        // Method
        public void DisplayInfo() {
            Console.WriteLine($"Brand: {brand}, Model: {model}, Year: {year}");
        }
    }

    class Program {
        static void Main(string[] args) {
            // Creating an Object of Car
            Car myCar1 = new Car();
            
            // Setting attribute values
            myCar1.brand = "Toyota";
            myCar1.model = "Corolla";
            myCar1.year = 2022;

            // Creating another Object
            Car myCar2 = new Car();
            myCar2.brand = "Honda";
            myCar2.model = "Civic";
            myCar2.year = 2023;

            // Calling method
            Console.WriteLine("Car 1 Info:");
            myCar1.DisplayInfo();

            Console.WriteLine("Car 2 Info:");
            myCar2.DisplayInfo();
        }
    }
}
