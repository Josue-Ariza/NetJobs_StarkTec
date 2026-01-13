using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace NetJobs_StarkTec
{
    public partial class ActualizarOferta : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["OfertaID"] != null)
                {
                    int ofertaID = (int)Session["OfertaID"];
                    CargarOferta(ofertaID);
                }
                else
                {
                    // Manejar el caso en que no se proporciona un OfertaID válido.
                    Response.Redirect("MisOfertas.aspx");
                }
            }

            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        private void CargarOferta(int ofertaID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Titulo, Descripcion, Requisitos, Ubicacion, Salario FROM Ofertas WHERE OfertaID = @OfertaID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OfertaID", ofertaID);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtTitulo.Text = reader["Titulo"].ToString();
                            txtDescripcion.Text = reader["Descripcion"].ToString();
                            txtRequisitos.Text = reader["Requisitos"].ToString();
                            txtUbicacion.Text = reader["Ubicacion"].ToString();
                            txtSalario.Text = reader["Salario"].ToString();
                        }
                        else
                        {
                            // Manejar el caso en que no se encuentra la oferta.
                            Response.Redirect("MisOfertas.aspx");
                        }
                    }
                }
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (Session["OfertaID"] == null)
            {
                // Si la sesión se pierde, redirigir al usuario
                Response.Redirect("MisOfertas.aspx");
                return;
            }

            int ofertaID = (int)Session["OfertaID"];

            // Verifica que el salario es válido antes de proceder
            if (Page.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Ofertas SET Titulo = @Titulo, Descripcion = @Descripcion, Requisitos = @Requisitos, Ubicacion = @Ubicacion, Salario = @Salario WHERE OfertaID = @OfertaID";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OfertaID", ofertaID);
                        cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                        cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                        cmd.Parameters.AddWithValue("@Requisitos", txtRequisitos.Text);
                        cmd.Parameters.AddWithValue("@Ubicacion", txtUbicacion.Text);
                        cmd.Parameters.AddWithValue("@Salario", txtSalario.Text);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Redirige a la lista de ofertas después de actualizar
                Response.Redirect("MisOfertas.aspx");
            }
        }

        protected void cvSalario_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Validar que el salario ingresado sea un número positivo.
            decimal result;
            args.IsValid = decimal.TryParse(args.Value, out result) && result > 0;
        }
    }
}
