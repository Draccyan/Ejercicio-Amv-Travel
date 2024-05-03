using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoLogin.Models;

namespace ProyectoLogin.Models.ViewModels
{
    public class EmpleadoVM
    {
        public Empleado oEmpleado { get; set; }

        public List<SelectListItem> oListaCargo { get; set; }

    }

    public class ReservaVM
    {
        public Reserva oReserva { get; set; }

        public List<SelectListItem> oListaTour { get; set; }

    }
}
