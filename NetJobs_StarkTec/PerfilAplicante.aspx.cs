using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace NetJobs_StarkTec
{
    public partial class PerfilAplicante : Page
    {
        // Cadena de conexión para acceder a la base de datos
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // Si la página no es un PostBack (es decir, es la primera carga), se cargan los datos del perfil
            if (!IsPostBack)
            {
                CargarPerfil();
            }
        }

        // Método para cargar la información del perfil del aplicante
        private void CargarPerfil()
        {
            // Verifica si existe una sesión con el IdAplicante (es decir, si el aplicante está autenticado)
            if (Session["IdAplicante"] != null)
            {
                // Obtiene el Id del aplicante desde la sesión
                int aplicanteId = Convert.ToInt32(Session["IdAplicante"]);

                // Abre una conexión con la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Carga los diferentes datos del perfil del aplicante
                    CargarFotoPerfil(connection, aplicanteId);  // Foto de perfil
                    CargarNombreAplicante(connection, aplicanteId); // Nombre completo
                    CargarExperienciaLaboral(connection, aplicanteId); // Experiencia laboral
                    CargarHabilidades(connection, aplicanteId); // Habilidades
                    CargarEducacion(connection, aplicanteId); // Formación académica
                }
            }
            else
            {
                // Si no existe la sesión, redirige a la página de notificaciones
                Response.Redirect("~/Notificaciones.aspx");
            }
        }

        // Método para cargar la foto de perfil del aplicante
        private void CargarFotoPerfil(SqlConnection connection, int aplicanteId)
        {
            // Consulta SQL para obtener la URL de la foto de perfil del usuario
            string query = "SELECT FotoPerfilUrl FROM Usuarios WHERE IdUsuario = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", aplicanteId);
                object resultado = cmd.ExecuteScalar(); // Ejecuta la consulta y obtiene el resultado
                // Si la URL de la foto es nula, asigna una imagen predeterminada
                imgPerfil.ImageUrl = resultado != null ? resultado.ToString() : "~/Imagenes/perfil.png";
            }
        }

        // Método para cargar el nombre completo del aplicante
        private void CargarNombreAplicante(SqlConnection connection, int aplicanteId)
        {
            // Consulta SQL para obtener el nombre completo del usuario
            string query = "SELECT NombreCompleto FROM Usuarios WHERE IdUsuario = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", aplicanteId);
                object resultado = cmd.ExecuteScalar(); // Ejecuta la consulta y obtiene el resultado
                // Si el nombre no está disponible, asigna un valor predeterminado
                lblNombre.Text = resultado != null ? resultado.ToString() : "Nombre no disponible";
            }
        }

        // Método para cargar la experiencia laboral del aplicante
        private void CargarExperienciaLaboral(SqlConnection connection, int aplicanteId)
        {
            // Consulta SQL para obtener la experiencia laboral del aplicante
            string query = "SELECT Empresa, Puesto, Año FROM ExperienciaLaboral WHERE UsuarioID = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", aplicanteId);

                // Ejecuta la consulta y llena el Repeater con los datos obtenidos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    rptExperiencia.DataSource = reader;  // Asigna el resultado de la consulta al Repeater
                    rptExperiencia.DataBind(); // Realiza el binding de los datos al Repeater
                }
            }
        }

        // Método para cargar las habilidades del aplicante
        private void CargarHabilidades(SqlConnection connection, int aplicanteId)
        {
            // Consulta SQL para obtener las habilidades del aplicante
            string query = "SELECT Descripcion FROM Habilidades WHERE UsuarioID = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", aplicanteId);

                // Ejecuta la consulta y llena el Repeater con los datos obtenidos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    rptHabilidades.DataSource = reader;  // Asigna el resultado de la consulta al Repeater
                    rptHabilidades.DataBind(); // Realiza el binding de los datos al Repeater
                }
            }
        }

        // Método para cargar la formación académica del aplicante
        private void CargarEducacion(SqlConnection connection, int aplicanteId)
        {
            // Consulta SQL para obtener la formación académica del aplicante
            string query = "SELECT Institucion, Titulo, AñoGraduacion FROM FormacionAcademica WHERE UsuarioID = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UsuarioID", aplicanteId);

                // Ejecuta la consulta y llena el Repeater con los datos obtenidos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    rptEducacion.DataSource = reader;  // Asigna el resultado de la consulta al Repeater
                    rptEducacion.DataBind(); // Realiza el binding de los datos al Repeater
                }
            }
        }
    }
}
