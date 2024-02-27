namespace WebApplication8.Models
{
    public class Server
    {
        public Server(int id, string name, int modelId, int osId, int years)
        {
            Id = id;
            Name = name;
            ModelId = modelId;
            OSId = osId;
            Years = years;
        }

        public Server()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public string Modelname { get; set; } = "";
        public int OSId { get; set; }
        public string OSName { get; set; } = "";
        public int Years {  get; set; }
    }
}
