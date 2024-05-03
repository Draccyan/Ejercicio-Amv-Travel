using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoLogin.Models;
using ProyectoLogin.Models.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ProyectoLogin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Context _ctx;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger, Context ctx)
        {
            _ctx = ctx;
            _logger = logger;

        }

        public IActionResult Index()
        {
            //ClaimsPrincipal claimuser = HttpContext.User;
            //string nombreUsuario = "";

            //if (claimuser.Identity.IsAuthenticated)
            //{
            //    nombreUsuario = claimuser.Claims.Where(c => c.Type == ClaimTypes.Name)
            //        .Select(c => c.Value).SingleOrDefault();
            //}

            //ViewData["nombreUsuario"] = nombreUsuario;

            
            List<Reserva> lista = _ctx.GestorReservas.MostrarReservas();
            
            return View(lista);
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

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Inicio");
        }


        [HttpGet]
        public IActionResult Reserva_Detalle(int idReserva)
        {
            ReservaVM oReservaVM = new ReservaVM()
            {
                oReserva = new Reserva(),
                oListaTour = _ctx.Tour.Select(tour => new SelectListItem()
                {
                    Text = tour.Nombre,
                    Value = tour.Id.ToString()
                }).ToList()
            };

            if (idReserva != 0)
            {
                oReservaVM.oReserva = oReservaVM.oReserva.MostrarInformacion(idReserva, _ctx);
            }
            oReservaVM.oReserva.FechaReserva = Convert.ToDateTime(DateTime.Now.Date.ToString("dd/MM/yyyy"));
            return View(oReservaVM);
        }

        [HttpPost]
        public IActionResult Reserva_Detalle(ReservaVM oReservaVM)
        {
            if (oReservaVM.oReserva.Id == 0)
            {
                _ctx.GestorReservas.ReservarTour(oReservaVM.oReserva);

            }
            else
            {
                _ctx.GestorReservas.EditarReserva(oReservaVM.oReserva);
            }

            _ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int idReserva)
        {
            Reserva reserva = new Reserva();

            if (idReserva != 0)
            {

                reserva = reserva.MostrarInformacion(idReserva, _ctx);
            }
            //Reserva oReserva = _ctx.Reserva.Include(c => c.oTour).FirstOrDefault(e => e.Id == idReserva);
            //var oReserva = oReserva.MostrarInformacion(idReserva, _ctx);

            return View(reserva);
        }

        [HttpPost]
        public IActionResult Eliminar(Reserva oReserva)
        {
            _ctx.Reserva.Remove(oReserva);
            _ctx.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}