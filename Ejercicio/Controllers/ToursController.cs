using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoLogin.Models;
using ProyectoLogin.Models.ViewModels;
using ProyectoLogin.Repos;

namespace ProyectoLogin.Controllers
{
    [Authorize]
    public class ToursController : Controller
    {
        private readonly Context _ctx;
        private readonly GestorReservas _gestorReservas;

        public ToursController(Context ctx)
        {
            _ctx = ctx;
            _gestorReservas = new GestorReservas(ctx);

        }

        public IActionResult ListadoTours()
        {
            List<Tour> tours = _ctx.GestorReservas.MostrarTours();
            return View(tours);
        }

        [HttpGet]
        public IActionResult UpsertTour(int idTour)
        {
            Tour tour = new Tour();

            if (idTour != 0)
            {

                tour = tour.MostrarInformacion(idTour, _ctx);
            }
            tour.FechaInicio = DateTime.Now.Date;
            tour.FechaFin = DateTime.Now.Date.AddDays(30);
            return View(tour);
        }

        [HttpPost]
        public IActionResult UpsertTour(Tour tour)
        {
            if (tour.Id == 0)
            {
                _gestorReservas.AgregarTour(tour);
                
            }
            else
            {
                _gestorReservas.EditarTour(tour);
            }

            _ctx.SaveChanges();

            return RedirectToAction("ListadoTours", "Tours");
        }

        [HttpGet]
        public IActionResult EliminarTour(int idTour)
        {
            Tour tour = new Tour();

            if (idTour != 0)
            {
                tour = tour.MostrarInformacion(idTour, _ctx);
            }

            return View(tour);
        }

        [HttpPost]
        public IActionResult EliminarTour(Tour tour)
        {
            _gestorReservas.EliminarTour(tour);
            _ctx.SaveChanges();

            return RedirectToAction("ListadoTours", "Tours");
        }
    }
}