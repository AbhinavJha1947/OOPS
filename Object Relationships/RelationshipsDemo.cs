using System;
using System.Collections.Generic;

namespace ObjectRelationships
{
    // 1. Association
    // Logic: Driver uses a Car.
    public class Car
    {
        public string Model { get; set; }
        public Car(string model) { Model = model; }
    }

    public class Driver
    {
        public string Name { get; set; }
        public Driver(string name) { Name = name; }

        public void Drive(Car car)
        {
            Console.WriteLine($"{Name} is driving a {car.Model}");
        }
    }

    // 2. Aggregation
    // Logic: Team has Players. Players survive if Team is dissolved.
    public class Player
    {
        public string Name { get; set; }
        public Player(string name) { Name = name; }
    }

    public class Team
    {
        public string TeamName { get; set; }
        private List<Player> _players = new List<Player>();

        public Team(string name) { TeamName = name; }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
            Console.WriteLine($"Added {player.Name} to {TeamName}");
        }
    }

    // 3. Composition
    // Logic: Order has LineItems. LineItems make no sense without Order.
    public class LineItem
    {
        public string Product { get; set; }
        public LineItem(string product) { Product = product; }
    }

    public class Order
    {
        private List<LineItem> _items = new List<LineItem>();

        public void AddItem(string product)
        {
            // Item created within the Order
            _items.Add(new LineItem(product));
        }

        public void ShowOrder()
        {
            Console.WriteLine("Order contains:");
            foreach (var item in _items) Console.WriteLine($" - {item.Product}");
        }
        // When Order is garbage collected, _items is also collected
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Object Relationships Demo ===\n");

            // Association
            Driver d = new Driver("Alice");
            Car c = new Car("Tesla");
            d.Drive(c);

            // Aggregation
            Player p1 = new Player("Bob");
            Team team = new Team("Wildcats");
            team.AddPlayer(p1);
            // p1 still exists if team is nullified.

            // Composition
            Order order = new Order();
            order.AddItem("Laptop");
            order.AddItem("Mouse");
            order.ShowOrder();
        }
    }
}
