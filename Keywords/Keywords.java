package Keywords;

class Parent {
    String name = "Parent";

    Parent() {
        System.out.println("Parent Constructor");
    }
}

class Child extends Parent {
    String name = "Child";
    static int count = 0; // Static variable
    final int MAX_LIMIT = 100; // Final (Constant)

    Child() {
        super(); // Call Parent constructor
        count++; // Increment static counter
        System.out.println("Child Constructor");
    }

    void display() {
        // 'this' refers to current class attribute
        System.out.println("Current Name: " + this.name);

        // 'super' refers to parent class attribute
        System.out.println("Parent Name: " + super.name);

        System.out.println("Total Children Created: " + count);
    }
}

public class Keywords {
    public static void main(String[] args) {
        Child c1 = new Child();
        c1.display();

        Child c2 = new Child();
        c2.display();

        // Accessing static variable directly via Class
        System.out.println("Final Count: " + Child.count);
    }
}
