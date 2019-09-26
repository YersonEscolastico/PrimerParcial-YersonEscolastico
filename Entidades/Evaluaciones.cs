using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Evaluaciones
    {
        [Key]

        public int EvaluacionId { get; set; }

        public string Estudiante { get; set; }
        public DateTime Fecha { get; set; }
        public List<EvaluacionesDetalle> Detalle { get; set; }

        public Evaluaciones()
        {
            EvaluacionId = 0;
            Estudiante = string.Empty;
            Fecha = DateTime.Now;
            Detalle = new List<EvaluacionesDetalle>();
        }
    public void AgregarDetalle(string categorias, decimal valor, decimal logrado, decimal perdido)
        {
            this.AgregarDetalle(categorias, valor, logrado,perdido);
        }      
    }
}
