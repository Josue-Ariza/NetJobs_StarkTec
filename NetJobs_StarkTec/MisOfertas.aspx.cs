using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NetJobs_StarkTec
{
    public partial class MisOfertas : System.Web.UI.Page
    {
        // Cadena de conexión a la base de datos
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Si no es una solicitud postback, se carga la lista de ofertas
            if (!IsPostBack)
            {
                CargarMisOfertas();
            }
        }

        // Método para cargar las ofertas del usuario desde la base de datos
        private void CargarMisOfertas()
        {
            // Conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener las ofertas del usuario por su ID
                string query = "SELECT OfertaID, Titulo, Descripcion, Requisitos, Ubicacion, Salario FROM Ofertas WHERE UsuarioID = @UsuarioID";

                // Comando SQL con parámetros para evitar inyecciones SQL
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    // Añadimos el ID del usuario como parámetro
                    cmd.Parameters.AddWithValue("@UsuarioID", ObtenerUsuarioId());

                    // Abrimos la conexión con la base de datos
                    connection.Open();

                    // Ejecutamos la consulta y obtenemos los resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Asignamos los resultados al Repeater para mostrar los datos
                        rptMisOfertas.DataSource = reader;
                        rptMisOfertas.DataBind();
                    }
                }
            }
        }

        // Método para obtener el ID del usuario desde la sesión
        private int ObtenerUsuarioId()
        {
            // Recupera el ID del usuario de la sesión
            return (int)Session["IdUsuario"];
        }

        // Evento que maneja los comandos de los botones en el Repeater (editar o eliminar)
        protected void rptMisOfertas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Si el comando es "Eliminar", eliminamos la oferta
            if (e.CommandName == "Delete")
            {
                int ofertaID = Convert.ToInt32(e.CommandArgument); // Obtiene el ID de la oferta
                EliminarOferta(ofertaID); // Elimina la oferta de la base de datos
                CargarMisOfertas(); // Recarga las ofertas después de eliminar
            }
            // Si el comando es "Editar", redirige a la página de actualización de la oferta
            else if (e.CommandName == "Edit")
            {
                int ofertaID = Convert.ToInt32(e.CommandArgument); // Obtiene el ID de la oferta

                // Guardar el OfertaID en la sesión para que esté disponible en la página de edición
                Session["OfertaID"] = ofertaID;

                // Redirige a la página de actualización sin pasar el OfertaID en la URL
                Response.Redirect("ActualizarOferta.aspx");
            }
        }

        // Método para eliminar una oferta de la base de datos
        private void EliminarOferta(int ofertaID)
        {
            try
            {
                // Conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Consulta SQL para eliminar la oferta por su ID
                    string query = "DELETE FROM Ofertas WHERE OfertaID = @OfertaID";

                    // Comando SQL con parámetros
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OfertaID", ofertaID); // Añade el ID de la oferta a eliminar

                        // Abrimos la conexión con la base de datos
                        connection.Open();

                        // Ejecutamos el comando para eliminar la oferta
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                // En caso de error (por ejemplo, si la oferta tiene notificaciones asociadas), mostramos un mensaje flotante
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "var mensaje = document.createElement('div');" +
                    "mensaje.className = 'mensaje-flotante';" +
                    "mensaje.textContent = 'No se puede eliminar porque tienes notificaciones acerca de esta oferta';" +
                    "document.body.appendChild(mensaje);" +
                    "setTimeout(() => { mensaje.remove(); }, 4000);", true);
            }
        }
    }
}
