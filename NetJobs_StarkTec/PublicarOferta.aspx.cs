using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NetJobs_StarkTec
{
    public partial class PublicarOferta : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos obtenida desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Este método se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Desactiva la validación discreta y usa validación explícita en el servidor
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        // Este método se ejecuta cuando el usuario hace clic en el botón "Publicar Oferta"
        protected void btnPublicar_Click(object sender, EventArgs e)
        {
            // Verifica si los controles de validación en la página son válidos
            if (Page.IsValid)
            {
                // Llama al método para guardar la oferta en la base de datos
                GuardarOferta();
            }
        }

        // Método que guarda la oferta de trabajo en la base de datos
        private void GuardarOferta()
        {
            // Abre una nueva conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar una nueva oferta de trabajo en la base de datos
                string query = "INSERT INTO Ofertas (Titulo, Descripcion, Requisitos, Ubicacion, Salario, UsuarioID) VALUES (@Titulo, @Descripcion, @Requisitos, @Ubicacion, @Salario, @UsuarioID)";

                // Crea un comando SQL para ejecutar la consulta
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Asocia los valores de los controles del formulario a los parámetros de la consulta
                    cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@Requisitos", txtRequisitos.Text);
                    cmd.Parameters.AddWithValue("@Ubicacion", txtUbicacion.Text);
                    cmd.Parameters.AddWithValue("@Salario", decimal.Parse(txtSalario.Text)); // Convierte el salario a decimal
                    cmd.Parameters.AddWithValue("@UsuarioID", ObtenerUsuarioId()); // Obtiene el ID del usuario logueado

                    // Abre la conexión y ejecuta el comando SQL para insertar los datos
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            // Redirige al usuario a la página de inicio después de guardar la oferta
            Response.Redirect("~/Inicio.aspx");
        }

        // Este método obtiene el ID del usuario logueado desde la sesión
        private int ObtenerUsuarioId()
        {
            // Obtiene el ID del usuario almacenado en la sesión
            return (int)Session["IdUsuario"];
        }

        // Método de validación del salario para asegurarse de que sea un número positivo
        protected void cvSalario_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            // Intenta convertir el valor ingresado a un decimal y valida que sea mayor que 0
            if (decimal.TryParse(args.Value, out decimal salario) && salario > 0)
            {
                args.IsValid = true; // Si la validación es exitosa, marca como válido
            }
            else
            {
                args.IsValid = false; // Si la validación falla, marca como inválido
            }
        }
    }
}
