using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text;
using WebApplication8.Models;
using WebApplication8.ViewModels;

namespace WebApplication8.Controllers
{




    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ApplicationContext db;

        List<OS> listOs;
        List<ServerModel> listServerModel;
        List<Server> listServer;




        public HomeController(ILogger<HomeController> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;

            var os1 = new OS(1, "Windows 10");
            var os2 = new OS(2, "Windows 11");
            var os3 = new OS(3, "Linux");

            listOs = new List<OS>()
            {
                os1,
                os2,
                os3
            };

            var sm1 = new ServerModel(1, "Producer1", "Model1", 1000, "");
            var sm2 = new ServerModel(2, "Producer2", "Model2", 2000, "");
            var sm3 = new ServerModel(3, "Producer3", "Model3", 3000, "");

            listServerModel = new List<ServerModel>()
            {
                sm1,
                sm2,
                sm3
            };

            listServer = new List<Server>()
            {
                new Server(1, "Server1", 1, 1, 1),
                new Server(2, "Server2", 1, 1, 12),
                new Server(3, "Server3", 2, 2, 10),
                new Server(4, "Server4", 1, 2, 3),
                new Server(5, "Server5", 2, 3, 2),
                new Server(6, "Server6", 1, 3, 1),
                new Server(7, "Server7", 3, 1, 5),
                new Server(8, "Server8", 1, 1, 1),
                new Server(9, "Server9", 1, 1, 1),
                new Server(10, "Server10", 3, 2, 1)
            };


        }






        public async Task<IActionResult> Index(int? osId, int? serverModelId, string order="", string up="")
        {

            var listOsModel = listOs;
            listOsModel.Insert(0, new OS(-1, "Все"));

            var listServerModelModel = listServerModel;
            listServerModelModel.Insert(0, new ServerModel(-1, "", "Все", 0, ""));

            


            IndexViewModel viewModel = new() { ListOS = listOsModel, ListServerModel = listServerModelModel, ListServer = listServer };
            viewModel.LastSort = order;
            viewModel.Up = up;
            /*if (up == "Up") { viewModel.Up = "Down"; }
            else viewModel.Up = "Up";*/
            string or = "";
            if (order == "NameSort")
            {
                if(viewModel.Up == "Up")
                {
                    or = "NameAsc";
                }
                else
                {
                    or = "NameDesc";
                }
            }
            if (order == "OSSort")
            {
                if (viewModel.Up == "Up")
                {
                    or = "OSAsc";
                }
                else
                {
                    or = "OSDesc";
                }
            }
            if (order == "ModelSort")
            {
                if (viewModel.Up == "Up")
                {
                    or = "ModelAsc";
                }
                else
                {
                    or = "ModelDesc";
                }
            }

            /*foreach (var s in listServer)
            {
                db.Servers.Add(s);
                db.SaveChanges();
            }*/
            /*string ord = "";

            switch (order)
            {
                case "NameAsc":
                        ord = "NameDesc";
                        break;
                case "NameDesc":
                    ord = "NameAsc";
                    break;
                case "OSAsc":
                    ord = "OSDesc";
                    break;
                case "OSDesc":
                    ord = "OSAsc";
                    break;
                case "ModelAsc":
                    ord = "ModelDesc";
                    break;
                case "ModelDesc":
                    ord = "ModelAsc";
                    break;
                case "":
                    ord = "NameAsc";
                    break;
            }
            order = ord;*/

            ViewBag.Sort = order;

            viewModel.ListServer = db.Servers;

            if (osId != null && osId > -1)
            {
                viewModel.ListServer = viewModel.ListServer.Where(s => s.OSId == osId);
                viewModel.LastOSSelectedId = osId??-1;
            }
            if (serverModelId != null && serverModelId > -1)
            {
                viewModel.ListServer = viewModel.ListServer.Where(s => s.ModelId == serverModelId);
                viewModel.LastServerModelSelectedId = serverModelId??-1;
            }

            foreach(Server s in viewModel.ListServer)
            {
                s.OSName = viewModel.ListOS.FirstOrDefault(os => os.Id == s.OSId).Name;
                s.Modelname = viewModel.ListServerModel.FirstOrDefault(sm => sm.Id == s.ModelId).Model;
            }

            switch (or)
            {
                case "NameAsc":
                    viewModel.ListServer = viewModel.ListServer.OrderBy(s => s.Name);
                    break;
                case "NameDesc":
                    viewModel.ListServer = viewModel.ListServer.OrderByDescending(s => s.Name);
                    break;
                case "OSAsc":
                    viewModel.ListServer = viewModel.ListServer.OrderBy(s => s.OSName);
                    break;
                case "OSDesc":
                    viewModel.ListServer = viewModel.ListServer.OrderByDescending(s => s.OSName);
                    break;
                case "ModelAsc":
                    viewModel.ListServer = viewModel.ListServer.OrderBy(s => s.Modelname);
                    break;
                case "ModelDesc":
                    viewModel.ListServer = viewModel.ListServer.OrderByDescending(s => s.Modelname);
                    break;
            }

            /*viewModel.ListServer = sortOrder switch
            {
                SortState.NameDesc => viewModel.ListServer.OrderByDescending(s => s.Name),

                SortState.NameAsc => viewModel.ListServer.OrderBy(s => s.Name),

                SortState.OSDesc => viewModel.ListServer.OrderByDescending(s => s.OSName),

                SortState.OSAsc => viewModel.ListServer.OrderBy(s => s.OSName),

                SortState.ModelDesc => viewModel.ListServer.OrderByDescending(s => s.Modelname),

                SortState.ModelAsc => viewModel.ListServer.OrderBy(s => s.Modelname)
            };*/

            return View(viewModel);
        }

        public IActionResult Create()
        {
            /*listOs.RemoveAt(0);
            listServerModel.RemoveAt(0);*/
            ViewBag.OSList = listOs;
            ViewBag.ServerModelsList = listServerModel;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Server server)
        {
            db.Servers.Add(server);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Server? server = db.Servers.FirstOrDefault(s => s.Id == id); //FirstOrDefaultAsync - не найдено, в данной версии проекта
                if (server != null)
                {
                    db.Servers.Remove(server);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

            }
            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.OSList = listOs;
            ViewBag.ServerModelsList = listServerModel;
            if (id != null)
            {
                Server? server = db.Servers.FirstOrDefault(s => s.Id == id); //FirstOrDefaultAsync - не найдено, в данной версии проекта
                if (server != null)
                {

                    return View(server);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Server server)
        {
            db.Servers.Update(server);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    

}