using System;

namespace Polymorphism
{
    class Calculator
    {
        // 1. Compile-time Polymorphism (Method Overloading)
        public int Add(int a, int b)
        {
            return a + b;
        }

        public double Add(double a, double b)
        {
            return a + b;
        }
    }

    class Animal
    {
        public virtual void MakeSound()
        {
            Console.WriteLine("Animal makes a sound");
        }
    }

    class Dog : Animal
    {
        // 2. Run-time Polymorphism (Method Overriding)
        public override void MakeSound()
        {
            Console.WriteLine("Dog barks");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Test Overloading
            Calculator calc = new Calculator();
            Console.WriteLine("Sum (int): " + calc.Add(5, 10));
            Console.WriteLine("Sum (double): " + calc.Add(5.5, 10.5));

            // Test Overriding
            Animal myAnimal = new Dog(); // Upcasting
            myAnimal.MakeSound(); // Calls Dog's method at runtime
        }
    }
}
