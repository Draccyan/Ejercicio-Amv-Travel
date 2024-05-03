using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoLogin.Models
{
    [Table("Tour")]
    public class Tour
    {
        public Tour()
        {
            Reservas = new HashSet<Reserva>();
        }

        [Column("Id", TypeName = "int")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("Nombre", TypeName = "varchar")]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [Column("Destino", TypeName = "varchar")]
        [MaxLength(100)]
        public string Destino { get; set; }
        [Column("FechaInicio", TypeName = "datetime")]
        public DateTime FechaInicio { get; set; }
        [Column("FechaFin", TypeName = "datetime")]
        public DateTime FechaFin { get; set; }
        [Column("Precio", TypeName = "decimal")]
        public decimal? Precio { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }


        // Método para mostrar información de un tour por su nombre
        public Tour MostrarInformacion(int idTour, Context ctx)
        {
            // Utilizamos LINQ para buscar el tour por su nombre
            var tour = ctx.Tour.FirstOrDefault(t => t.Id == idTour);

            if (tour != null)
            {
                tour.FechaInicio = tour.FechaInicio.Date;
                tour.FechaFin = tour.FechaFin.Date;
                return tour;
            }
            else
            {
                throw new InvalidOperationException($"No se encontró el tour '{idTour}'.");
            }
        }
    }

}
