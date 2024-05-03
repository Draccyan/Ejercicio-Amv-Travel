using Microsoft.EntityFrameworkCore;
using ProyectoLogin.Models;
using ProyectoLogin.Models.ViewModels;

namespace ProyectoLogin.Repos
{
    public interface IGestorReservas
    {
        void AgregarTour(Tour tour);
        void EditarTour(Tour tour);
        void EliminarTour(Tour tour);
        List<Tour> MostrarTours();
        void ReservarTour(Reserva reserva);
        void EditarReserva(Reserva reserva);
        void EliminarReserva(Reserva reserva);
        List<Reserva> MostrarReservas();
    }
    public class GestorReservas : IGestorReservas
    {
        private readonly Context _ctx;

        public GestorReservas(Context ctx)
        {
            _ctx = ctx;
        }

        public void AgregarTour(Tour tour)
        {
            _ctx.Tour.Add(tour);
            
        }


        public void EditarTour(Tour tour)
        {

            _ctx.Tour.Update(tour);

        }

        
        public void EliminarTour(Tour tour)
        {
            _ctx.Tour.Remove(tour);
        }

        public List<Reserva> MostrarReservas()
        {
            var reservas = _ctx.Reserva.Include(c => c.oTour).ToList();
            foreach(var reserva in reservas)
            {
                reserva.FechaReserva = Convert.ToDateTime(reserva.FechaReserva.Date.ToString("dd/MM/yyyy"));
            }
            return reservas;
        }

        public List<Tour> MostrarTours()
        {
            var tours = _ctx.Tour.ToList();
            foreach (var tour in tours)
            {
                tour.FechaInicio = tour.FechaInicio.Date;
                tour.FechaFin = tour.FechaFin.Date;
            }
            return tours;
        }

        public void ReservarTour(Reserva reserva)
        {
            _ctx.Reserva.Add(reserva);
        }

        public void EditarReserva(Reserva reserva)
        {
            _ctx.Reserva.Update(reserva);
        }

        public void EliminarReserva(Reserva reserva)
        {
            _ctx.Reserva.Remove(reserva);
        }

    }
}