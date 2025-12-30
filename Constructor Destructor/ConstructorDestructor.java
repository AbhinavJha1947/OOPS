// Constructor and Destructor (Finalizer) Demo in Java

class Resource implements AutoCloseable {
    private String name;

    // Default Constructor
    public Resource() {
        this.name = "Default Resource";
        System.out.println("Resource created: " + name);
    }

    // Parameterized Constructor
    public Resource(String name) {
        this.name = name;
        System.out.println("Resource created: " + name);
    }

    // Copy Constructor (Manual implementation)
    public Resource(Resource other) {
        this.name = other.name + " (Copy)";
        System.out.println("Resource copied from: " + other.name);
    }

    @Override
    public void close() {
        System.out.println("Resource closed (Resource cleanup): " + name);
    }

    public void use() {
        System.out.println("Using resource: " + name);
    }
}

public class ConstructorDestructor {
    public static void main(String[] args) {
        System.out.println("--- Java Constructor Demo ---");

        // 1. Default constructor
        try (Resource r1 = new Resource()) {
            r1.use();
        }

        // 2. Parameterized constructor
        try (Resource r2 = new Resource("MyCustomResource")) {
            r2.use();

            // 3. Copy constructor (Nested to keep scope clear or separate)
            try (Resource r3 = new Resource(r2)) {
                r3.use();
            }
        }

        // 4. Try-with-resources (Modern way of "Destruction" in Java)
        System.out.println("\n--- Try-with-resources Demo ---");
        try (Resource autoResource = new Resource("AutoCleanResource")) {
            autoResource.use();
        } // autoResource.close() called automatically here

        System.out.println("\nEnd of main");
    }
}
