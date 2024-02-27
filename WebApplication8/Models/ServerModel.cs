namespace WebApplication8.Models
{
    public class ServerModel
    {
        public ServerModel(int id, string producer, string model, int maxLoad, string description)
        {
            Id = id;
            Producer = producer;
            Model = model;
            MaxLoad = maxLoad;
            Description = description;
        }

        public int Id {  get; set; }
        public string Producer { get; set; }
        public string Model{ get; set; }
        public int MaxLoad {  get; set; }
        public string Description {  get; set; }
    }
}
