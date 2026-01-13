using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

namespace NetJobs_StarkTec
{
    public partial class Login : System.Web.UI.Page
    {
        // Cadena de conexión obtenida del archivo de configuración (Web.config)
        private string cone = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Evento que se ejecuta cuando la página carga por primera vez
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta al hacer clic en el botón "Ingresar"
        protected void btnIngrear_Click(object sender, EventArgs e)
        {
            // Obtiene los valores ingresados por el usuario en los campos de correo y contraseña
            string correo = txtCorreo.Text;
            string contraseña = txtPass.Text;

            // Hashear la contraseña ingresada por el usuario
            string hashedPassword = HashPassword(contraseña);

            // Conexión a la base de datos para verificar las credenciales
            using (SqlConnection conn = new SqlConnection(cone))
            {
                // Abre la conexión a la base de datos
                conn.Open();

                // Crea un comando SQL para verificar si las credenciales existen en la tabla Usuarios
                SqlCommand cmd = new SqlCommand("SELECT IdUsuario FROM Usuarios WHERE Correo = @Correo AND Contraseña = @Contraseña", conn);

                // Agrega los parámetros al comando SQL para prevenir inyecciones SQL
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contraseña", hashedPassword);  // Usamos la contraseña hasheada

                // Ejecuta el comando y obtiene el resultado (ID del usuario si las credenciales son correctas)
                object result = cmd.ExecuteScalar();
                if (result != null) // Si el resultado no es nulo, las credenciales son válidas
                {
                    // Almacena el ID del usuario en la sesión para su uso posterior
                    Session["IdUsuario"] = result;
                    // Opcionalmente, almacena también el correo electrónico en la sesión
                    Session["UserEmail"] = correo;

                    // Redirige al usuario a la página de inicio
                    Response.Redirect("Inicio.aspx");
                }
                else
                {
                    // Si las credenciales son incorrectas, muestra un mensaje de error en la etiqueta lblMensaje
                    lblMensaje.Text = "Credenciales incorrectas";
                }
            }
        }

        // Función para aplicar el hashing a la contraseña utilizando SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseña a un arreglo de bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el arreglo de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
