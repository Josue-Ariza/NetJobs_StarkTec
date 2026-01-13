using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NetJobs_StarkTec
{
    public partial class Notificaciones : Page
    {
        // Cadena de conexión a la base de datos, obtenida desde el archivo de configuración (web.config)
        private string cone = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)  // Verifica si es la primera vez que se carga la página (no es un postback)
            {
                CargarAplicaciones();  // Carga las aplicaciones (notificaciones) desde la base de datos
            }
        }

        // Método que maneja el clic en el botón "Ver Perfil"
        protected void lnkVerPerfil_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;  // Captura el botón que fue clickeado
            string usuarioID = btn.CommandArgument;  // Obtiene el ID del usuario desde el atributo CommandArgument

            // Guarda el ID del aplicante en la sesión para usarlo más adelante
            Session["IdAplicante"] = usuarioID;

            // Redirige al perfil del aplicante (otra página)
            Response.Redirect("PerfilAplicante.aspx");
        }

        // Método que maneja el clic en el botón "Descargar CV"
        protected void lnkDescargarCV_Click(object sender, EventArgs e)
        {
            LinkButton lnkDescargarCV = (LinkButton)sender;  // Captura el botón de descarga
            string cvRuta = lnkDescargarCV.CommandArgument;  // Obtiene la ruta del CV desde el atributo CommandArgument

            // Verifica si la ruta del archivo no está vacía
            if (!string.IsNullOrEmpty(cvRuta))
            {
                string filePath = Server.MapPath(cvRuta);  // Convierte la ruta relativa a una ruta física en el servidor

                // Verifica si el archivo existe en el servidor
                if (File.Exists(filePath))
                {
                    Response.Clear();  // Limpia la respuesta previa
                    Response.ContentType = "application/pdf";  // Establece el tipo de contenido como PDF
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));  // Establece el nombre de archivo para la descarga
                    Response.TransmitFile(filePath);  // Transmite el archivo al cliente
                    Response.End();  // Finaliza la respuesta
                }
                else
                {
                    // Muestra un mensaje de alerta si el archivo no se encuentra
                    Response.Write("<script>alert('El archivo no se encuentra en el servidor.');</script>");
                }
            }
            else
            {
                // Muestra un mensaje de alerta si no se encontró la ruta del archivo
                Response.Write("<script>alert('No se encontró la ruta del archivo.');</script>");
            }
        }

        // Método privado para cargar las aplicaciones desde la base de datos
        private void CargarAplicaciones()
        {
            // Obtiene el ID del usuario que está publicando las ofertas
            int idUsuarioPublicador = Convert.ToInt32(Session["IdUsuario"]);

            // Lista para almacenar las notificaciones de aplicaciones
            List<ApplicationNotification> aplicaciones = new List<ApplicationNotification>();

            // Consulta SQL para obtener las aplicaciones de los usuarios que se han postulado a las ofertas
            string query = @"
                SELECT a.AplicacionID, o.Titulo AS TituloOferta, u.NombreCompleto AS NombreUsuario, 
                       a.FechaAplicacion, a.CVRuta, u.IdUsuario AS UsuarioID
                FROM Aplicaciones a
                JOIN Ofertas o ON a.OfertaID = o.OfertaID
                JOIN Usuarios u ON a.UsuarioID = u.IdUsuario
                WHERE o.UsuarioID = @IdUsuarioPublicador";

            // Conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(cone))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdUsuarioPublicador", idUsuarioPublicador);  // Parámetro para filtrar las aplicaciones por el publicador
                conn.Open();  // Abre la conexión a la base de datos

                // Ejecuta la consulta y lee los datos
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())  // Itera sobre cada fila del resultado
                    {
                        // Añade los resultados a la lista de aplicaciones
                        aplicaciones.Add(new ApplicationNotification
                        {
                            AplicacionID = Convert.ToInt32(reader["AplicacionID"]),  // Obtiene el ID de la aplicación
                            TituloOferta = reader["TituloOferta"].ToString(),  // Obtiene el título de la oferta
                            NombreUsuario = reader["NombreUsuario"].ToString(),  // Obtiene el nombre del usuario que aplicó
                            FechaAplicacion = Convert.ToDateTime(reader["FechaAplicacion"]),  // Obtiene la fecha de la aplicación
                            CVRuta = reader["CVRuta"].ToString(),  // Obtiene la ruta del CV
                            UsuarioID = Convert.ToInt32(reader["UsuarioID"])  // Obtiene el ID del usuario que aplicó
                        });
                    }
                }
            }

            // Asigna los datos de las aplicaciones al control Repeater para mostrarlas en la página
            rptAplicaciones.DataSource = aplicaciones;
            rptAplicaciones.DataBind();
        }

        // Método para eliminar una notificación de la base de datos
        protected void btnRecibida_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;  // Captura el botón que fue clickeado
            int aplicacionID = Convert.ToInt32(btn.CommandArgument);  // Obtiene el ID de la aplicación desde el atributo CommandArgument

            try
            {
                // Conexión a la base de datos para eliminar la aplicación
                using (SqlConnection conn = new SqlConnection(cone))
                {
                    string query = "DELETE FROM Aplicaciones WHERE AplicacionID = @AplicacionID";  // Consulta SQL para eliminar la aplicación
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AplicacionID", aplicacionID);  // Parámetro para especificar qué aplicación eliminar
                        conn.Open();  // Abre la conexión
                        cmd.ExecuteNonQuery();  // Ejecuta la consulta de eliminación
                    }
                }

                // Recarga las aplicaciones después de eliminar una
                CargarAplicaciones();
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre una excepción
                Response.Write("<script>alert('Ocurrió un error al eliminar la notificación.')</script>");
            }
        }
    }

    // Clase que representa una notificación de aplicación
    public class ApplicationNotification
    {
        public int AplicacionID { get; set; }  // ID de la aplicación
        public string TituloOferta { get; set; }  // Título de la oferta de empleo
        public string NombreUsuario { get; set; }  // Nombre del usuario que aplicó
        public DateTime FechaAplicacion { get; set; }  // Fecha de la aplicación
        public string CVRuta { get; set; }  // Ruta del archivo CV
        public int UsuarioID { get; set; }  // ID del usuario que aplicó
    }
}
