using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RepositorioEvaluacion : RepositorioBase<Evaluaciones>
    {
        public override Evaluaciones Buscar(int id)
        {
            Evaluaciones Evaluaciones = new Evaluaciones();
            Contexto db = new Contexto();
            try
            {

                Evaluaciones = db.evaluaciones.Include(x => x.Detalle)
                    .Where(x => x.EvaluacionId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

            return Evaluaciones;
        }
        public override bool Modificar(Evaluaciones evaluacion)
        {
            var Anterior = Buscar(evaluacion.EvaluacionId);
            bool paso = false;

            try
            {
                using (Contexto contexto = new Contexto())
                {
                    foreach (var item in Anterior.Detalle.ToList())
                    {
                        if (!evaluacion.Detalle.Exists(d => d.DetalleId == item.DetalleId))
                        {
                            contexto.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                    contexto.SaveChanges();
                }
                foreach (var item in evaluacion.Detalle)
                {
                    var estado = item.DetalleId > 0 ? EntityState.Unchanged : EntityState.Added;
                    _contexto.Entry(item).State = estado;
                }
                _contexto.Entry(evaluacion).State = EntityState.Modified;
                if (_contexto.SaveChanges() > 0)
                    paso = true;
            }
            catch
            {
                throw;
            }
            return paso;
        }
    }
}