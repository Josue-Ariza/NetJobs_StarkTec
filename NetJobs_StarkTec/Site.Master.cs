using System; 
using System.Data.SqlClient; 
using System.Configuration; 
using System.Web.UI; 

namespace NetJobs_StarkTec
{
    public partial class Site : MasterPage
    {
        // Cadena de conexión a la base de datos, obtenida desde el archivo Web.config
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Evento que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si no es una recarga de página (PostBack)
            if (!IsPostBack)
            {
                CargarDatosUsuario(); // Llama al método para cargar los datos del usuario
            }
        }

        // Método para cargar los datos del usuario desde la base de datos
        private void CargarDatosUsuario()
        {
            // Verifica si hay un usuario autenticado almacenado en la sesión
            if (Session["IdUsuario"] != null)
            {
                // Obtiene el ID del usuario desde la sesión
                int usuarioId = Convert.ToInt32(Session["IdUsuario"]);

                // Abre una conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); // Abre la conexión

                    // Carga la foto de perfil del usuario
                    CargarFotoPerfil(connection, usuarioId);

                    // Carga el nombre completo del usuario
                    CargarNombreUsuario(connection, usuarioId);
                }
            }
            else
            {
                // Redirige al usuario a la página de inicio de sesión si no está autenticado
                Response.Redirect("~/Login.aspx");
            }
        }

        // Método para cargar la foto de perfil del usuario desde la base de datos
        private void CargarFotoPerfil(SqlConnection connection, int usuarioId)
        {
            // Consulta para obtener la URL de la foto de perfil
            string query = "SELECT FotoPerfilUrl FROM Usuarios WHERE IdUsuario = @IdUsuario";

            // Ejecuta la consulta SQL
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Parámetro para evitar inyección SQL
                cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);

                // Ejecuta la consulta y obtiene el resultado
                object resultado = cmd.ExecuteScalar();

                // Verifica si la foto existe; si no, asigna una imagen por defecto
                string fotoUrl = resultado != null && !string.IsNullOrWhiteSpace(resultado.ToString())
                                 ? resultado.ToString()
                                 : "~/Imagenes/pefil.png"; // Imagen por defecto

                // Asigna la URL de la imagen al control de la interfaz
                imgUserPhoto.ImageUrl = fotoUrl;
            }
        }

        // Método para cargar el nombre completo del usuario desde la base de datos
        private void CargarNombreUsuario(SqlConnection connection, int usuarioId)
        {
            // Consulta para obtener el nombre completo del usuario
            string query = "SELECT NombreCompleto FROM Usuarios WHERE IdUsuario = @IdUsuario";

            // Ejecuta la consulta SQL
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Parámetro para evitar inyección SQL
                cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);

                // Ejecuta la consulta y obtiene el resultado
                object resultado = cmd.ExecuteScalar();

                // Asigna el nombre al label; si no hay nombre, muestra "Usuario Desconocido"
                lblUserName.Text = resultado != null ? resultado.ToString() : "Usuario Desconocido";
            }
        }
    }
}
