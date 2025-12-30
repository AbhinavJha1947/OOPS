using System;

namespace ConstructorDestructorDemo
{
    // A class demonstrating Constructors, Destructors, and IDisposable
    class Resource : IDisposable
    {
        public string Name { get; private set; }
        private bool _disposed = false;

        // 1. Default Constructor
        public Resource()
        {
            Name = "Default Resource";
            Console.WriteLine($"[Constructor] Resource created: {Name}");
        }

        // 2. Parameterized Constructor
        public Resource(string name)
        {
            Name = name;
            Console.WriteLine($"[Constructor] Resource created: {Name}");
        }

        // 3. Copy Constructor (C# doesn't have a strict copy constructor syntax like C++, but we can implement one)
        public Resource(Resource other)
        {
            Name = other.Name + " (Copy)";
            Console.WriteLine($"[Copy Constructor] Resource copied from: {other.Name}");
        }

        // 4. Constructor Chaining
        public Resource(string name, int id) : this(name + $" (ID: {id})")
        {
            Console.WriteLine($"[Chained Constructor] Added ID info.");
        }

        // Destructor (Finalizer)
        // Only called by GC if Dispose() was not called and suppression didn't happen
        ~Resource()
        {
            Console.WriteLine($"[Destructor/Finalizer] Resource being finalized: {Name}");
            Dispose(false);
        }

        // IDisposable Implementation
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Stop GC from calling the finalizer
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Clean up managed resources here
                    Console.WriteLine($"[Dispose] Cleaning up managed resources for: {Name}");
                }

                // Clean up unmanaged resources here
                Console.WriteLine($"[Dispose] Cleaning up unmanaged resources for: {Name}");
                _disposed = true;
            }
        }

        public void Use()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(Name);
            }
            Console.WriteLine($"Using resource: {Name}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# Constructor & Destructor Demo ===\n");

            // Using Default Constructor
            Resource r1 = new Resource();
            r1.Use();

            // Using Parameterized Constructor
            Resource r2 = new Resource("MyCustomResource");
            r2.Use();

            // Using Copy Constructor approach
            Resource r3 = new Resource(r2);
            r3.Use();

            // Using Chained Constructor
            Resource r4 = new Resource("ChainedResource", 42);
            r4.Use();

            Console.WriteLine("\n--- IDisposable (using statement) Demo ---");
            // The 'using' statement ensures Dispose is called automatically (Deterministic Finalization)
            using (Resource autoResource = new Resource("AutoResource"))
            {
                autoResource.Use();
            } // Dispose called here automatically

            Console.WriteLine("\n--- Destructor/Finalizer Demo ---");
            Console.WriteLine("Creating a resource without disposing it properly...");
            CreateAndForget();
            
            // Force Garbage Collection to demonstrate Finalizer (Not recommended in production)
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nEnd of Main");
        }

        static void CreateAndForget()
        {
            Resource forgotten = new Resource("ForgottenResource");
            // forgotten.Dispose() is NOT called
        }
    }
}
