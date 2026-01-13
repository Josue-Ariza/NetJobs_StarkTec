using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

namespace NetJobs_StarkTec
{
    public partial class Inicio : System.Web.UI.Page
    {
        // Método que se ejecuta cuando la página se carga
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Carga las ofertas cuando la página se carga por primera vez
                CargarOfertas();
            }
        }

        // Método para cargar las ofertas desde la base de datos
        private void CargarOfertas()
        {
            // Obtiene la cadena de conexión desde el archivo web.config
            string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

            // Obtiene el ID del usuario actual desde la sesión
            int idUsuarioActual = Convert.ToInt32(Session["IdUsuario"]);

            // Crea una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL que obtiene las ofertas, excluyendo las del usuario actual
                string query = @"
                    SELECT 
                        o.OfertaID,
                        o.Titulo, 
                        o.Descripcion, 
                        o.Requisitos, 
                        o.Ubicacion, 
                        o.Salario, 
                        o.fechaPublicacion, 
                        u.NombreCompleto AS UsuarioNombre 
                    FROM 
                        Ofertas o
                    INNER JOIN 
                        Usuarios u ON o.UsuarioID = u.IdUsuario
                    WHERE 
                        o.UsuarioID <> @IdUsuarioActual";

                // Crea el comando SQL y le agrega el parámetro para evitar inyecciones SQL
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuarioActual", idUsuarioActual);

                try
                {
                    // Abre la conexión y ejecuta el comando
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Carga los datos obtenidos en una tabla de datos
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Asocia los datos del DataTable al Repeater para mostrarlos en la interfaz
                    rptOfertas.DataSource = dataTable;
                    rptOfertas.DataBind();
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si algo sale mal al cargar las ofertas
                    Response.Write("<script>alert('Error al cargar las ofertas: " + ex.Message + "');</script>");
                }
            }
        }

        // Método que se ejecuta cuando se realiza una búsqueda de ofertas
        protected void BuscarOfertas(object sender, EventArgs e)
        {
            // Obtiene la palabra clave desde el formulario, eliminando espacios innecesarios
            string palabraClave = Request.Form["search-input"]?.Trim();

            // Si el campo de búsqueda está vacío, recarga todas las ofertas
            if (string.IsNullOrEmpty(palabraClave))
            {
                CargarOfertas();
                return;
            }

            // Obtiene la cadena de conexión desde el archivo web.config
            string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

            // Obtiene el ID del usuario actual desde la sesión
            int idUsuarioActual = Convert.ToInt32(Session["IdUsuario"]);

            // Crea una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Consulta SQL que permite buscar ofertas por palabra clave
                string query = @"
                    SELECT 
                        o.OfertaID,
                        o.Titulo, 
                        o.Descripcion, 
                        o.Requisitos, 
                        o.Ubicacion, 
                        o.Salario, 
                        o.fechaPublicacion, 
                        u.NombreCompleto AS UsuarioNombre 
                    FROM 
                        Ofertas o
                    INNER JOIN 
                        Usuarios u ON o.UsuarioID = u.IdUsuario
                    WHERE 
                        o.UsuarioID <> @IdUsuarioActual
                        AND 
                        (o.Titulo LIKE @PalabraClave 
                        OR o.Descripcion LIKE @PalabraClave
                        OR o.Requisitos LIKE @PalabraClave
                        OR o.Ubicacion LIKE @PalabraClave)";

                // Crea el comando SQL y le agrega los parámetros para evitar inyecciones SQL
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IdUsuarioActual", idUsuarioActual);
                command.Parameters.AddWithValue("@PalabraClave", "%" + palabraClave + "%");

                try
                {
                    // Abre la conexión y ejecuta el comando
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    // Carga los datos obtenidos en una tabla de datos
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    // Asocia los datos del DataTable al Repeater para mostrarlos en la interfaz
                    rptOfertas.DataSource = dataTable;
                    rptOfertas.DataBind();
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si algo sale mal al buscar las ofertas
                    Response.Write("<script>alert('Error al buscar ofertas: " + ex.Message + "');</script>");
                }
            }
        }

        // Método que se ejecuta cuando se hace clic en el botón "Aplicar"
        protected void btnAplica_Click(object sender, EventArgs e)
        {
            // Obtiene el ID de la oferta desde el CommandArgument del botón
            string ofertaID = ((Button)sender).CommandArgument;

            // Almacena el ID de la oferta en la sesión para usarlo en la página de aplicación
            Session["OfertaID"] = ofertaID;

            // Redirige a la página de aplicación de la oferta
            Response.Redirect("AplicarOferta.aspx");
        }
    }
}

