using System;

namespace Keywords
{
    class Parent
    {
        public string name = "Parent";
        
        public Parent()
        {
            Console.WriteLine("Parent Constructor");
        }
    }

    sealed class Child : Parent // 'sealed' prevents inheritance from Child
    {
        public string name = "Child";
        public static int count = 0; // Static variable

        public Child() : base() // Call Parent constructor
        {
            count++;
            Console.WriteLine("Child Constructor");
        }

        public void Display()
        {
            // 'this' refers to current class attribute
            Console.WriteLine("Current Name: " + this.name);
            
            // 'base' refers to parent class attribute
            Console.WriteLine("Parent Name: " + base.name);
            
            Console.WriteLine("Total Children Created: " + count);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Child c1 = new Child();
            c1.Display();

            Child c2 = new Child();
            c2.Display();

            // Accessing static variable via Class
            Console.WriteLine("Final Count: " + Child.count);
        }
    }
}
