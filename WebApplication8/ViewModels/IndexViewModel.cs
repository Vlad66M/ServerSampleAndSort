using WebApplication8.Models;
namespace WebApplication8.ViewModels
{
    
    public class IndexViewModel
    {
        public IEnumerable<Server> ListServer { get; set; } = new List<Server>();
        public IEnumerable<ServerModel> ListServerModel { get; set; } = new List<ServerModel>();
        public IEnumerable<OS> ListOS { get; set; } = new List<OS>();

        public int LastOSSelectedId { get; set; } = -1;
        public int LastServerModelSelectedId { get; set; } = -1;
        public string LastSort {  get; set; } = string.Empty;
        public string Up { get; set; }

    }
}
