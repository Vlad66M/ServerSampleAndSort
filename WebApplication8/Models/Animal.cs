namespace WebApplication8.Models
{
    public class Animal
    {
        public Animal(int id, string name, string description, int count)
        {
            Id = id;
            Name = name;
            Description = description;
            Count = count;
        }


        public Animal(int id, string name)
        {
            Id = id;
            Name = name;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Count { get; set; }

        public string PrintInfo() => $"{Id}, {Name} ({Count} Тысяч)";



    }

}
