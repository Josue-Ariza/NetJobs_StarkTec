using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace NetJobs_StarkTec
{
    public partial class MiPerfil : Page
    {
        // Cadena de conexión a la base de datos, obtenida desde el archivo de configuración
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si es la primera vez que se carga la página (no es un postback)
            if (!IsPostBack)
            {
                CargarPerfil(); // Llama a la función para cargar el perfil del usuario
            }
        }

        // Método para cargar todos los datos del perfil del usuario
        private void CargarPerfil()
        {
            // Verifica si la sesión contiene el IdUsuario (usuario autenticado)
            if (Session["IdUsuario"] != null)
            {
                // Convierte el IdUsuario de la sesión a un entero
                int usuarioId = Convert.ToInt32(Session["IdUsuario"]);

                // Abre la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Llama a los métodos para cargar diferentes secciones del perfil
                    CargarFotoPerfil(connection, usuarioId);
                    CargarExperienciaLaboral(connection, usuarioId);
                    CargarHabilidades(connection, usuarioId);
                    CargarEducacion(connection, usuarioId);
                }
            }
            else
            {
                // Si no hay sesión activa, redirige al usuario a la página de inicio
                Response.Redirect("~/Inicio.aspx");
            }
        }

        // Método para cargar la foto de perfil del usuario desde la base de datos
       
        private void CargarFotoPerfil(SqlConnection connection, int usuarioId)
        {
            string query = "SELECT FotoPerfilUrl FROM Usuarios WHERE IdUsuario = @IdUsuario";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@IdUsuario", usuarioId);
                object resultado = cmd.ExecuteScalar();

                string fotoUrl = resultado != null && !string.IsNullOrWhiteSpace(resultado.ToString())
                                 ? resultado.ToString()
                                 : "~/Imagenes/pefil.png"; // Imagen por defecto

                imgPerfil.ImageUrl = fotoUrl;
            }
        }


        // Método para cargar la experiencia laboral del usuario desde la base de datos
        private void CargarExperienciaLaboral(SqlConnection connection, int usuarioId)
        {
            // Consulta SQL para obtener la experiencia laboral del usuario
            string query = "SELECT Empresa, Puesto, Año FROM ExperienciaLaboral WHERE UsuarioID = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Añade el parámetro de UsuarioID a la consulta
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);

                // Ejecuta la consulta y usa un lector para leer los datos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Enlaza los datos del lector al Repeater de experiencia laboral
                    rptExperiencia.DataSource = reader;
                    rptExperiencia.DataBind(); // Realiza el enlace de datos
                }
            }
        }

        // Método para cargar las habilidades del usuario desde la base de datos
        private void CargarHabilidades(SqlConnection connection, int usuarioId)
        {
            // Consulta SQL para obtener las habilidades del usuario
            string query = "SELECT Descripcion FROM Habilidades WHERE UsuarioID = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Añade el parámetro de UsuarioID a la consulta
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);

                // Ejecuta la consulta y usa un lector para leer los datos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Enlaza los datos del lector al Repeater de habilidades
                    rptHabilidades.DataSource = reader;
                    rptHabilidades.DataBind(); // Realiza el enlace de datos
                }
            }
        }

        // Método para cargar la formación académica del usuario desde la base de datos
        private void CargarEducacion(SqlConnection connection, int usuarioId)
        {
            // Consulta SQL para obtener la educación del usuario
            string query = "SELECT Institucion, Titulo, AñoGraduacion FROM FormacionAcademica WHERE UsuarioID = @UsuarioID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                // Añade el parámetro de UsuarioID a la consulta
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioId);

                // Ejecuta la consulta y usa un lector para leer los datos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // Enlaza los datos del lector al Repeater de educación
                    rptEducacion.DataSource = reader;
                    rptEducacion.DataBind(); // Realiza el enlace de datos
                }
            }
        }
    }
}
