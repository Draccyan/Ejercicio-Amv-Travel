using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProyectoLogin.Models
{
    [Table("Reserva")]
    public class Reserva
    {
        [Column("Id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Cliente", TypeName = "varchar")]
        [MaxLength(100)]
        public string Cliente { get; set; }
        [Column("FechaReserva", TypeName = "datetime")]
        public DateTime FechaReserva { get; set; }
        [Column("TourId", TypeName = "int")]
        public int TourId { get; set; }
        public virtual Tour? oTour { get; set; }


        // Método para mostrar información de una reserva por su id
        public Reserva MostrarInformacion(int idReserva, Context ctx)
        {
            // Utilizamos LINQ para buscar el tour por su nombre
            var reserva = ctx.Reserva.Include(c => c.oTour).FirstOrDefault(e => e.Id == idReserva);

            if (reserva != null)
            {
                reserva.FechaReserva = reserva.FechaReserva.Date;
                return reserva;
            }
            else
            {
                throw new InvalidOperationException($"No se encontró la reserva nro '{idReserva}'.");
            }
        }

    }

}
