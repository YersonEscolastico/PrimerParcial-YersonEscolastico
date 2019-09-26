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
    public class RepositorioEvaluacion:RepositorioBase<Evaluaciones>
    {
  
        public override bool Guardar(Evaluaciones evaluacion)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Set<Evaluaciones>().Add(evaluacion) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


        public override bool Modificar(Evaluaciones evaluacion)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(evaluacion).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }


        public override bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {


                var eliminar = db.evaluaciones.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }
    }
}

