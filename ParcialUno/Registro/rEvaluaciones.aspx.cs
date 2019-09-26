using BLL;
using Entidades;
using pAnalisisMD.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParcialUno.Registro
{
    public partial class rEvaluaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                EvaluacionIdTextBox.Text = "0";
                Estudiante.Text = string.Empty;
                CategoriaTextBox.Text = "0";
                ValorTextBox.Text = string.Empty;
                LogradoTextBox.Text = "0";
                ViewState["Evaluaciones"] = new Evaluaciones();
                BindGrid();

            }

        }

        private void Limpiar()
        {
            Estudiante.Text = string.Empty;
            CategoriaTextBox.Text = "0";
            ValorTextBox.Text = "0";
            LogradoTextBox.Text = "0";
            DetalleGridView.DataSource = null;
            DetalleGridView.DataBind();
        }

        public Evaluaciones LlenaClase()
        {
            Evaluaciones evaluacion = new Evaluaciones();
            evaluacion = (Evaluaciones)ViewState["Evaluaciones"];
            evaluacion.EvaluacionId = Convert.ToInt32(EvaluacionIdTextBox.Text);
            evaluacion.Estudiante = Estudiante.Text;
            evaluacion.Fecha = Convert.ToDateTime(FechaTextBox.Text);
            return evaluacion;
        }





        private void LlenaCampo(Evaluaciones evaluacion)
        {

            ((Evaluaciones)ViewState["Evaluacion"]).Detalle = evaluacion.Detalle;
            EvaluacionIdTextBox.Text = evaluacion.EvaluacionId.ToString();
            Estudiante.Text = evaluacion.Estudiante;
            FechaTextBox.Text = evaluacion.Fecha.ToString();

            this.BindGrid();
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            RepositorioBase<Evaluaciones> db = new RepositorioBase<Evaluaciones>();
            Evaluaciones evaluacion = db.Buscar(Convert.ToInt32(EvaluacionIdTextBox.Text));
            return (evaluacion != null);

        }


        protected void BindGrid()
        {
            if (ViewState["Evaluacion"] != null)
            {
                DetalleGridView.DataSource = ((Evaluaciones)ViewState["Evaluacion"]).Detalle;
                DetalleGridView.DataBind();
            }
        }



        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            bool paso = false;
            RepositorioBase<Evaluaciones> Repositorio = new RepositorioBase<Evaluaciones>();
            Evaluaciones evaluacion = new Evaluaciones();

            evaluacion = LlenaClase();

            if (Utils.ToInt(EvaluacionIdTextBox.Text) == 0)
            {
                paso = Repositorio.Guardar(evaluacion);
                Limpiar();
            }
            else
            {
                paso = Repositorio.Modificar(evaluacion);
            }

            if (paso)
            {
               Utils.ShowToastr(this.Page, "Guardao!!", "Guardado", "info");
                return;
            }
            else
            {
                Utils.ShowToastr(this.Page, "Error!!", "error");
            }
        }
        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {


            RepositorioBase<Evaluaciones> Repositorio = new RepositorioBase<Evaluaciones>();

            var ev = Repositorio.Buscar(Utils.ToInt(EvaluacionIdTextBox.Text));
            if (ev != null)
            {
                if (Repositorio.Eliminar(Utils.ToInt(EvaluacionIdTextBox.Text)))
                {
                    Utils.ShowToastr(this.Page, "Eliminado!!", "Eliminado");
                    Limpiar();
                }
                else
                    Utils.ShowToastr(this.Page, "Error!!", "error");
            }
            else
                Utils.ShowToastr(this.Page, "Eliminado!!", "Eliminado");

        }

        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            Evaluaciones evaluacion = new Evaluaciones();

            evaluacion = (Evaluaciones)ViewState["Evaluaciones"];


            evaluacion.Detalle.Add(new Entidades.EvaluacionesDetalle(CategoriaTextBox.Text, Convert.ToDecimal(ValorTextBox.Text), Convert.ToDecimal(LogradoTextBox.Text), Convert.ToDecimal(Perdido.Text)));

            ViewState["Evaluaciones"] = evaluacion;

            this.BindGrid();

        }
    }
}