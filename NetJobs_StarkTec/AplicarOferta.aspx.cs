using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NetJobs_StarkTec
{
    public partial class AplicarOferta : System.Web.UI.Page
    {
        private int ofertaId;

        // Evento Page_Load se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificamos si hay un ID de oferta pasado por sesión
            if (Session["OfertaID"] != null)
            {
                // Obtenemos el ID de la oferta desde la sesión
                ofertaId = Convert.ToInt32(Session["OfertaID"]);

                if (!IsPostBack)
                {
                    // Cargar los detalles de la oferta
                    CargarDetallesOferta(ofertaId);
                }
            }
            else
            {
                // En caso de que no haya ID de oferta en la sesión, redirigir o mostrar error
                Response.Redirect("Inicio.aspx");
            }
        }

        // Método para cargar los detalles de la oferta desde la base de datos
        private void CargarDetallesOferta(int ofertaId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para obtener los detalles de la oferta
                string query = @"
                    SELECT 
                        Titulo, 
                        Descripcion, 
                        Requisitos, 
                        Ubicacion, 
                        Salario 
                    FROM 
                        Ofertas 
                    WHERE 
                        OfertaID = @OfertaID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OfertaID", ofertaId);

                try
                {
                    // Abrimos la conexión y ejecutamos la consulta
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Si se encontraron filas (resultados), se llenan las etiquetas con los detalles
                    if (reader.HasRows)
                    {
                        reader.Read();

                        lblTitulo.Text = reader["Titulo"].ToString();
                        lblDescripcion.Text = reader["Descripcion"].ToString();
                        lblRequisitos.Text = reader["Requisitos"].ToString();
                        lblUbicacion.Text = reader["Ubicacion"].ToString();
                        lblSalario.Text = reader["Salario"].ToString();
                    }
                    else
                    {
                        // Si no se encuentra la oferta, redirige a la página de inicio
                        Response.Redirect("Inicio.aspx");
                    }
                }
                catch (Exception ex)
                {
                    // Manejamos el error en caso de que haya un problema con la consulta
                    Response.Write("<script>alert('Error al cargar la oferta: " + ex.Message + "');</script>");
                }
            }
        }

        // Lógica del evento de clic en el botón "Aplicar"
        protected void btnAplicar_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un archivo
            if (fileCV.HasFile)
            {
                // Verificar si el archivo es un PDF
                if (Path.GetExtension(fileCV.FileName).ToLower() == ".pdf")
                {
                    // Definir la ruta en el servidor donde se guardará el archivo
                    string archivoNombre = Path.GetFileName(fileCV.FileName);
                    string rutaArchivo = "~/ArchivosAplicaciones/" + archivoNombre;

                    // Guardar el archivo en el servidor
                    fileCV.SaveAs(Server.MapPath(rutaArchivo));

                    // Verificar si el usuario está autenticado
                    if (Session["IdUsuario"] != null)
                    {
                        int usuarioID = Convert.ToInt32(Session["IdUsuario"]);

                        // Guardar la aplicación en la base de datos
                        bool guardado = GuardarAplicacion(ofertaId, usuarioID, rutaArchivo);

                        if (guardado)
                        {
                            // Mostrar un mensaje de éxito
                            Response.Write("<script>alert('¡Aplicación enviada con éxito!');</script>");
                        }
                        else
                        {
                            // Si hubo un problema al guardar la aplicación, mostrar mensaje de error
                            Response.Write("<script>alert('Hubo un problema al guardar la aplicación.');</script>");
                        }
                    }
                    else
                    {
                        // Si el usuario no está autenticado, redirigir al login
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    // Si el archivo no es un PDF, mostrar mensaje de error
                    Response.Write("<script>alert('Por favor, sube un archivo PDF');</script>");
                }
            }
            else
            {
                // Si no se ha seleccionado ningún archivo, mostrar mensaje de error
                Response.Write("<script>alert('Por favor, selecciona un archivo PDF');</script>");
            }
        }

        // Método para guardar la aplicación en la base de datos
        private bool GuardarAplicacion(int ofertaID, int usuarioID, string rutaArchivo)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL para insertar la aplicación en la base de datos
                string query = @"
                    INSERT INTO Aplicaciones (OfertaID, UsuarioID, FechaAplicacion, Estado, CVRuta) 
                    VALUES (@OfertaID, @UsuarioID, GETDATE(), 'Pendiente', @CVRuta)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OfertaID", ofertaID);
                command.Parameters.AddWithValue("@UsuarioID", usuarioID);
                command.Parameters.AddWithValue("@CVRuta", rutaArchivo);

                try
                {
                    // Abrir la conexión y ejecutar la consulta de inserción
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    // Si hay un error al guardar, mostrar el mensaje de error
                    Response.Write("<script>alert('Error al enviar la aplicación: " + ex.Message + "');</script>");
                    return false;
                }
            }
        }
    }
}

