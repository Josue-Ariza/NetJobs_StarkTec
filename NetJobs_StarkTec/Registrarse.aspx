<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="NetJobs_StarkTec.Registrarse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
<title>Registrarse</title>
</head>
<body style="background: #333"> <!-- Establece el color de fondo de la página a gris oscuro -->
    <form id="form1" runat="server"> <!-- Define un formulario en ASP.NET que se ejecuta en el servidor -->
        <div>
            <hr /> <!-- Línea horizontal que separa contenido -->
            <div class="container"> <!-- Contenedor principal para los elementos del formulario -->
                <div class="row"> <!-- Fila de la rejilla de Bootstrap -->
                    <div class="col-sm-12 col-md-12"> <!-- Columna que ocupa todo el ancho en pantallas pequeñas y medianas -->
                        <center> <!-- Centra el contenido dentro de la página -->
                            <h1 class="Papyrus bold-text text-center text-white">Registrate en NetJobs</h1> <!-- Título de bienvenida -->
                            <br /> <!-- Salto de línea -->
                        </center>
                    </div>
                    <div class="col-sm-4 col-md-4">&nbsp;</div> <!-- Columna vacía para espaciar el formulario -->
                    <div class="col-sm-4 col-md-4 card subtle card-custom"> <!-- Columna con tarjeta personalizada para el formulario -->
                        <br /> 
                        <center> <!-- Centra el contenido dentro de la columna -->
                            <img src="Imagenes/Usuario.gif" class="rounded-circle" width="85" height="85"/> <!-- Imagen circular de usuario -->
                            <br/> <br/>

                            <!-- Campo para el DUI -->
                            <div class="form-group">
                                <asp:TextBox ID="txtDUI" class="form-control" runat="server" placeholder="Ingrese DUI"></asp:TextBox> <!-- Campo de texto para DUI -->
                                <asp:RequiredFieldValidator ID="rfvDUI" runat="server" ControlToValidate="txtDUI" ErrorMessage="El campo DUI es obligatorio." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator> <!-- Validador requerido para DUI -->
                                <asp:RegularExpressionValidator ID="revDUI" runat="server" ControlToValidate="txtDUI" ErrorMessage="Formato DUI incorrecto (00000000-0)." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d{8}-\d{1}$"></asp:RegularExpressionValidator> <!-- Validador de expresión regular para el formato de DUI -->
                            </div> <br/>

                            <!-- Campo para el Nombre Completo -->
                            <div class="form-group">
                                <asp:TextBox ID="txtNom" class="form-control" runat="server" placeholder="Ingrese su nombre completo"></asp:TextBox> <!-- Campo de texto para el nombre -->
                                <asp:RequiredFieldValidator ID="rfvNom" runat="server" ControlToValidate="txtNom" ErrorMessage="El campo Nombre Completo es obligatorio." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator> <!-- Validador requerido para el nombre completo -->
                            </div><br/>

                            <!-- Campo para el Correo -->
                            <div class="form-group">
                                <asp:TextBox ID="txtCorreo" class="form-control" runat="server" placeholder="Ingrese su correo"></asp:TextBox> <!-- Campo de texto para el correo -->
                                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="El campo Correo es obligatorio." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator> <!-- Validador requerido para el correo -->
                                <asp:RegularExpressionValidator ID="revCorreo" runat="server" ControlToValidate="txtCorreo" ErrorMessage="Formato de correo incorrecto." CssClass="text-danger" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$"></asp:RegularExpressionValidator> <!-- Validador de expresión regular para el formato de correo -->
                            </div><br/>

                            <!-- Campo para la Contraseña -->
                            <div class="form-group">
                                <asp:TextBox ID="txtPass" TextMode="Password" class="form-control" runat="server" placeholder="Ingrese su contraseña"></asp:TextBox> <!-- Campo de texto para la contraseña -->
                                <asp:RequiredFieldValidator ID="rfvPass" runat="server" ControlToValidate="txtPass" ErrorMessage="El campo Contraseña es obligatorio." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator> <!-- Validador requerido para la contraseña -->
                            </div><br/>

                            <!-- Campo para Repetir la Contraseña -->
                            <div class="form-group">
                                <asp:TextBox ID="txtPass2" TextMode="Password" class="form-control" runat="server" placeholder="Repita su contraseña"></asp:TextBox> <!-- Campo de texto para repetir la contraseña -->
                                <asp:RequiredFieldValidator ID="rfvPass2" runat="server" ControlToValidate="txtPass2" ErrorMessage="Debe repetir su contraseña." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator> <!-- Validador requerido para repetir la contraseña -->
                                <asp:CompareValidator ID="cvPass" runat="server" ControlToValidate="txtPass2" ControlToCompare="txtPass" ErrorMessage="Las contraseñas no coinciden." CssClass="text-danger" Display="Dynamic"></asp:CompareValidator> <!-- Validador para comparar las contraseñas -->
                            </div><br/>

                            <!-- Campo para el Teléfono -->
                            <div class="form-group">
                                <asp:TextBox ID="txtTel" class="form-control" runat="server" placeholder="Ingrese su teléfono"></asp:TextBox> <!-- Campo de texto para el teléfono -->
                                <asp:RequiredFieldValidator ID="rfvTel" runat="server" ControlToValidate="txtTel" ErrorMessage="El campo Teléfono es obligatorio." CssClass="text-danger" Display="Dynamic"></asp:RequiredFieldValidator> <!-- Validador requerido para el teléfono -->
                                <asp:RegularExpressionValidator ID="revTel" runat="server" ControlToValidate="txtTel" ErrorMessage="Formato Teléfono incorrecto (0000-0000)." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d{4}-\d{4}$"></asp:RegularExpressionValidator> <!-- Validador de expresión regular para el formato del teléfono -->
                            </div><br/>

                            <!-- Botones de acción: Registrar y Cancelar -->
                            <asp:Button ID="btnRegistrar" class="btn btn-success" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" /> &nbsp; &nbsp; <!-- Botón de registro -->
                            <asp:Button ID="btnCancelar" class="btn btn-danger" runat="server" Text="Cancelar" /> <!-- Botón de cancelar -->
                            <br /><br/>
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Blue"></asp:Label> <!-- Label para mostrar mensajes -->
                            <a href="Login.aspx" class="btn btn-link text-blue" >Iniciar sesión.</a> <!-- Enlace para redirigir a la página de inicio de sesión -->
                            <br/><br/><br/>
                        </center>
                        <br/>
                    </div>
                    <div class="col-sm-4 col-md-4">&nbsp;</div> <!-- Columna vacía para espaciar -->
                </div>
            </div>
            <hr /> <!-- Línea horizontal final -->
        </div>
    </form>
</body>
</html>

