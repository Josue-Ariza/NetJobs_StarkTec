using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Configuration;
using System.Data;

namespace NetJobs_StarkTec
{
    public partial class ActualizarDatos : Page
    {
        // Cadena de conexión a la base de datos (asegúrate de que esté configurada en Web.config)
        private string connectionString = ConfigurationManager.ConnectionStrings["NetJobs"].ConnectionString;

        // Evento que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // Este método no carga información existente del usuario en esta versión
        }

        // Evento para agregar experiencia laboral
        protected void btnAgregarExperiencia_Click(object sender, EventArgs e)
        {
            // Verifica que los campos no estén vacíos
            if (!string.IsNullOrEmpty(txtEmpresa.Text) &&
                !string.IsNullOrEmpty(txtAño.Text) &&
                !string.IsNullOrEmpty(txtPuesto.Text))
            {
                try
                {
                    // Establece la conexión a la base de datos
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Ejecuta el procedimiento almacenado para insertar la experiencia laboral
                        using (SqlCommand cmd = new SqlCommand("InsertarExperienciaLaboral", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado

                            // Agrega los parámetros para el procedimiento almacenado
                            cmd.Parameters.AddWithValue("@UsuarioID", Session["IdUsuario"]); // Obtiene el ID del usuario desde la sesión
                            cmd.Parameters.AddWithValue("@NombreEmpresa", txtEmpresa.Text);
                            cmd.Parameters.AddWithValue("@Año", txtAño.Text);
                            cmd.Parameters.AddWithValue("@Puesto", txtPuesto.Text);

                            // Ejecuta el procedimiento
                            cmd.ExecuteNonQuery();

                            // Muestra mensaje de éxito
                            lblMensajeExperiencia.Text = "Experiencia laboral agregada correctamente.";
                            lblMensajeExperiencia.CssClass = "text-success";
                        }
                    }

                    // Limpiar los campos después de agregar la experiencia
                    txtEmpresa.Text = "";
                    txtAño.Text = "";
                    txtPuesto.Text = "";
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si algo sale mal
                    lblMensajeExperiencia.Text = "Error al agregar la experiencia laboral: " + ex.Message;
                    lblMensajeExperiencia.CssClass = "text-danger";
                }
                lblMensajeExperiencia.Visible = true;
            }
            else
            {
                // Si algún campo está vacío, muestra un mensaje de advertencia
                lblMensajeExperiencia.Text = "Por favor, complete todos los campos de experiencia laboral.";
                lblMensajeExperiencia.CssClass = "text-danger";
                lblMensajeExperiencia.Visible = true;
            }
        }

        // Evento para agregar formación académica
        protected void btnAgregarFormacion_Click(object sender, EventArgs e)
        {
            // Verifica que los campos no estén vacíos
            if (!string.IsNullOrEmpty(txtInstitucion.Text) &&
                !string.IsNullOrEmpty(txtTitulo.Text) &&
                !string.IsNullOrEmpty(txtAñoGraduacion.Text))
            {
                try
                {
                    // Establece la conexión a la base de datos
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Ejecuta el procedimiento almacenado para insertar la formación académica
                        using (SqlCommand cmd = new SqlCommand("InsertarFormacionAcademica", connection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure; // Indica que es un procedimiento almacenado

                            // Agrega los parámetros para el procedimiento almacenado
                            cmd.Parameters.AddWithValue("@UsuarioID", Session["IdUsuario"]); // Obtiene el ID del usuario desde la sesión
                            cmd.Parameters.AddWithValue("@Institucion", txtInstitucion.Text);
                            cmd.Parameters.AddWithValue("@Titulo", txtTitulo.Text);
                            cmd.Parameters.AddWithValue("@AñoGraduacion", txtAñoGraduacion.Text);

                            // Ejecuta el procedimiento
                            cmd.ExecuteNonQuery();

                            // Muestra mensaje de éxito
                            lblMensajeFormacion.Text = "Formación académica agregada correctamente.";
                            lblMensajeFormacion.CssClass = "text-success";
                        }
                    }

                    // Limpiar los campos después de agregar la formación
                    txtInstitucion.Text = "";
                    txtTitulo.Text = "";
                    txtAñoGraduacion.Text = "";
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si algo sale mal
                    lblMensajeFormacion.Text = "Error al agregar la formación académica: " + ex.Message;
                    lblMensajeFormacion.CssClass = "text-danger";
                }
                lblMensajeFormacion.Visible = true;
            }
            else
            {
                // Si algún campo está vacío, muestra un mensaje de advertencia
                lblMensajeFormacion.Text = "Por favor, complete todos los campos de formación académica.";
                lblMensajeFormacion.CssClass = "text-danger";
                lblMensajeFormacion.Visible = true;
            }
        }

        // Evento para el botón de aceptar (actualizar los datos)
        // Evento para el botón de aceptar (actualizar los datos)
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Establece la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Variable para almacenar la ruta de la foto de perfil
                    string filePath = string.Empty;

                    // Si el usuario ha subido una foto, se guarda en la carpeta "Imagenes"
                    if (fuFotoPerfil.HasFile)
                    {
                        filePath = "~/Imagenes/" + fuFotoPerfil.FileName;
                        fuFotoPerfil.SaveAs(Server.MapPath(filePath));
                    }
                    else
                    {
                        // Si no se ha subido una nueva foto, mantiene la foto actual en la base de datos
                        // Obtén la foto actual desde la base de datos si es necesario
                        string queryFotoActual = "SELECT FotoPerfilUrl FROM Usuarios WHERE IdUsuario = @UsuarioID";
                        using (SqlCommand cmdFotoActual = new SqlCommand(queryFotoActual, connection))
                        {
                            cmdFotoActual.Parameters.AddWithValue("@UsuarioID", Session["IdUsuario"]);
                            var result = cmdFotoActual.ExecuteScalar();
                            filePath = result != DBNull.Value ? result.ToString() : string.Empty; // Si hay foto, la mantiene, si no, deja la cadena vacía
                        }
                    }

                    // Actualiza la foto de perfil del usuario en la tabla Usuarios
                    string queryUsuarios = "UPDATE Usuarios SET FotoPerfilUrl = @FotoPerfil WHERE IdUsuario = @UsuarioID";
                    using (SqlCommand cmdUsuarios = new SqlCommand(queryUsuarios, connection))
                    {
                        cmdUsuarios.Parameters.AddWithValue("@UsuarioID", Session["IdUsuario"]);
                        cmdUsuarios.Parameters.AddWithValue("@FotoPerfil", filePath);

                        cmdUsuarios.ExecuteNonQuery();
                    }

                    // Llama al procedimiento almacenado para insertar las habilidades del usuario
                    using (SqlCommand cmdHabilidades = new SqlCommand("InsertarHabilidades", connection))
                    {
                        cmdHabilidades.CommandType = CommandType.StoredProcedure;
                        cmdHabilidades.Parameters.AddWithValue("@UsuarioID", Session["IdUsuario"]);
                        cmdHabilidades.Parameters.AddWithValue("@Habilidades", txtHabilidades.Text);

                        cmdHabilidades.ExecuteNonQuery();
                    }

                    // Muestra un mensaje de éxito si todo ha salido bien
                    lblMensajeGeneral.Text = "Datos actualizados correctamente.";
                    lblMensajeGeneral.CssClass = "text-success";
                }
            }
            catch (Exception ex)
            {
                // Muestra un mensaje de error si ocurre un problema
                lblMensajeGeneral.Text = "Error al actualizar los datos: " + ex.Message;
                lblMensajeGeneral.CssClass = "text-danger";
            }
            lblMensajeGeneral.Visible = true;
        }

    }
}
