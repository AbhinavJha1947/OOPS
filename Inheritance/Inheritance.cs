using System;

namespace Inheritance
{
    // Base Class
    class Vehicle
    {
        public string brand = "Generic Vehicle";

        public virtual void Honk()
        {
            Console.WriteLine("Tuut, tuut!");
        }
    }

    // Derived Class
    class Car : Vehicle
    {
        public string modelName = "Mustang";

        public override void Honk()
        {
            Console.WriteLine("Beep, beep! (Car Honk)");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car myCar = new Car();

            Console.WriteLine("Brand: " + myCar.brand);
            Console.WriteLine("Model: " + myCar.modelName);

            // Calls the overridden method in Car
            myCar.Honk();
        }
    }
}
