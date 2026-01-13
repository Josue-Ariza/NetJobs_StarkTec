using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace NetJobs_StarkTec
{
    public partial class Registrarse : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos, obtenida desde el archivo web.config
        private string cone = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Este evento se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // Desactiva la validación discreta de JavaScript en el cliente, para evitar errores de validación
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        // Este evento se ejecuta cuando el usuario hace clic en el botón de registrar
        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Se obtienen los valores ingresados en los controles de la interfaz de usuario
            string correo = txtCorreo.Text;
            string nombreCompleto = txtNom.Text;
            string dui = txtDUI.Text;
            string contra = txtPass.Text;
            string telefono = txtTel.Text;

            // Hashear la contraseña antes de almacenarla en la base de datos
            string hashedPassword = HashPassword(contra);

            // Validar si el DUI ya existe en la base de datos
            using (SqlConnection con = new SqlConnection(cone))
            {
                SqlCommand checkDuiCmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE DUI = @DUI", con);
                checkDuiCmd.Parameters.AddWithValue("@DUI", dui);

                try
                {
                    con.Open();
                    int count = (int)checkDuiCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // Mostrar mensaje si el DUI ya existe
                        lblMensaje.Text = "El DUI ya está registrado. Intente con otro.";
                        lblMensaje.ForeColor = System.Drawing.Color.Red;
                        return; // Detener la ejecución del registro
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Ocurrió un error al verificar el DUI: " + ex.Message;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }

            // Registro del usuario si el DUI no existe
            using (SqlConnection con = new SqlConnection(cone))
            {
                SqlCommand cmd = new SqlCommand("RegistrarUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DUI", dui);
                cmd.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", correo);
                cmd.Parameters.AddWithValue("@Contrasena", hashedPassword);
                cmd.Parameters.AddWithValue("@Telefono", telefono);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMensaje.Text = "Registro exitoso.";
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Ocurrió un error: " + ex.Message;
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
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
