using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Evaluaciones
    {
        [Key]

        public int EvaluacionId { get; set; }

        public string Estudiante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalPerdido { get; set; }
        public List<EvaluacionesDetalle> Detalle { get; set; }


        public Evaluaciones(int evaluacionid, DateTime fecha, string estudiante, decimal totalPerdido)
        {
            EvaluacionId= evaluacionid;
            Fecha = fecha;
            Estudiante = estudiante;
            TotalPerdido = totalPerdido;
            Detalle = new List<EvaluacionesDetalle>();
        }
        public Evaluaciones()
        {
            EvaluacionId = 0;
            Estudiante = string.Empty;
            Fecha = DateTime.Now;
            TotalPerdido = 0;
            Detalle = new List<EvaluacionesDetalle>();
        }
    public void AgregarDetalle(int detalleID, int EvaluacionID,string categorias, decimal valor, decimal logrado, decimal perdido)
        {
            this.Detalle.Add(new EvaluacionesDetalle(detalleID,EvaluacionID,categorias, valor, logrado,perdido));
        }      
    }
}
