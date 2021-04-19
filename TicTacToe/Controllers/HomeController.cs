using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe_API;

namespace TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get_Next_Play(string player, int row, int col, string pos)
        {
            int poss = (Convert.ToInt32(pos) + 1);
            string id = "#" + poss;
            List<Player_Move> player2_move = TicTacToe_API.Program.Return_Position(player, row, col, id);
            return Json(player2_move);
        }


        // Get winner of game and pass  to API  
        [HttpGet]
        public JsonResult Game_Over(string winner)
        {
            bool End = TicTacToe_API.Program.End_Game(winner);
            return Json(End);
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
