using InfoJobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace InfoJobs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly infojobsContext _infojobsContext;

        public HomeController(ILogger<HomeController> logger, infojobsContext infojobsContext)
        {
            _logger = logger;
            _infojobsContext = infojobsContext;
        }

        public IActionResult Index()
        {

            var trabajosMasSolicitados = (from t in _infojobsContext.trabajos
                                          select new
                                          {
                                              id = t.id_trabajo,
                                              trabajo = t.nombre,
                                              empresa = t.nombre_empresa,
                                              salario = t.salario,
                                              imagen = t.imagen
                                          }).ToList();
            ViewData["TrabajosTop"] = trabajosMasSolicitados;

            return View();
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

        // Para mostrar la vista propia de cada empleo
        // GET: Empleo/id
        public async Task<IActionResult> Empleo(int? id)
        {
            if (id == null || _infojobsContext.trabajos == null)
            {
                return NotFound();
            }

            var trabajos = await _infojobsContext.trabajos
                .FirstOrDefaultAsync(t => t.id_trabajo == id);
            if (trabajos == null)
            {
                return NotFound();
            }

            return View(trabajos);
        }
    }
}