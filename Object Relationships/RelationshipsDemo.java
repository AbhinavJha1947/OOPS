import java.util.ArrayList;
import java.util.List;

// 1. Association: "Uses-a" relationship (Banks and Customers)
// Independent lifecycles.
class Bank {
    private String name;

    public Bank(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }
}

class Customer {
    private String name;

    public Customer(String name) {
        this.name = name;
    }

    // Association happens here
    public void openAccount(Bank bank) {
        System.out.println(this.name + " is opening an account at " + bank.getName());
    }
}

// 2. Aggregation: "Has-a" relationship (Weak ownership)
// Department has Teachers. Teachers can exist without Department.
class Teacher {
    private String name;

    public Teacher(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }
}

class Department {
    private String name;
    private List<Teacher> teachers; // Aggregation

    public Department(String name) {
        this.name = name;
        this.teachers = new ArrayList<>();
    }

    public void addTeacher(Teacher teacher) {
        teachers.add(teacher);
    }
}

// 3. Composition: "Part-of" relationship (Strong ownership)
// House has Rooms. Rooms cannot exist without House.
class Room {
    private String type;

    public Room(String type) {
        this.type = type;
    }

    public void describe() {
        System.out.println("  - " + type);
    }
}

class House {
    private List<Room> rooms; // Composition

    public House() {
        this.rooms = new ArrayList<>();
        // Rooms are created *inside* the House
        // If House is destroyed, Rooms are also destroyed (conceptually)
        rooms.add(new Room("Living Room"));
        rooms.add(new Room("Kitchen"));
    }

    public void showRooms() {
        System.out.println("House contains:");
        for (Room r : rooms)
            r.describe();
    }
}

public class RelationshipsDemo {
    public static void main(String[] args) {
        System.out.println("--- Association Demo ---");
        Bank bank = new Bank("Chase");
        Customer customer = new Customer("John");
        customer.openAccount(bank); // Association

        System.out.println("\n--- Aggregation Demo ---");
        Teacher t1 = new Teacher("Mr. Smith");
        // Teacher exists independently
        Department math = new Department("Math");
        math.addTeacher(t1); // Added to department
        System.out.println(t1.getName() + " is part of Math dept, but exists independently.");

        System.out.println("\n--- Composition Demo ---");
        // We don't create Room independently here
        House myHouse = new House();
        myHouse.showRooms();
        // Rooms are bound to myHouse lifecycle
    }
}
