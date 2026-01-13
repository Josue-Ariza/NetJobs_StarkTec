<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="NetJobs_StarkTec.Login" %>
<!-- Declaración de la página de ASP.NET con configuración de lenguaje, vinculación de eventos automáticos 
y archivo de código subyacente -->

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Metadatos de la página y referencias a estilos y scripts -->
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <title>Iniciar sesión</title>
</head>
<body style="background: #333;">
    <!-- Fondo gris oscuro para toda la página -->
    <form id="form1" runat="server">
        <!-- Formulario de ASP.NET que contiene todos los elementos de la página -->
        <div>
            <hr />
            <div class="container">
                <div class="row">
                    <!-- Encabezado principal con título -->
                    <div class="col-sm-12 col-md-12">
                        <center>
                            <h1 class="Papyrus bold-text text-center text-white">Bienvenido a NetJobs</h1> 
                            <!-- Mensaje de bienvenida en texto blanco -->
                            <br />
                        </center>
                    </div>

                    <!-- Espaciador para diseño responsivo -->
                    <div class="col-sm-1 col-md-1">&nbsp;</div>

                    <!-- Panel izquierdo: Formulario de inicio de sesión -->
                    <div class="col-sm-4 col-md-4 card subtle card-custom">
                        <br /> 
                        <center>
                            <!-- Imagen de usuario como avatar -->
                            <img src="Imagenes/Usuario.gif" class="rounded-circle" width="150" height="150"/>
                            <br/><br/>
                            <!-- Campo de entrada para el correo electrónico -->
                            <div class="form-group">
                                <asp:TextBox ID="txtCorreo" class="form-control" runat="server" 
                                             autofocus="" placeholder="Ingrese su correo" required=""></asp:TextBox>
                            </div><br/> 
                            <!-- Campo de entrada para la contraseña -->
                            <div class="form-group">
                                <asp:TextBox ID="txtPass" class="form-control" runat="server" 
                                             placeholder="Ingrese su contraseña" TextMode="Password" required=""></asp:TextBox>
                            </div><br/>
                            <!-- Botón para iniciar sesión -->
                            <asp:Button ID="btnIngrear" class="btn btn-success" runat="server" 
                                        Text="Ingresar" OnClick="btnIngrear_Click" />
                            <br/><br/>
                            <!-- Etiqueta para mostrar mensajes de error o éxito -->
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="red"></asp:Label>
                        </center>
                        <br/>
                    </div>

                    <!-- Panel derecho: Carrusel informativo -->
                    <div class="col-sm-6 col-md-6 card bg bg-primary">
                        <center>
                            <div id="textCarousel" class="carousel slide" data-bs-ride="carousel">
                                <!-- Indicadores del carrusel -->
                                <div class="carousel-indicators">
                                    <button type="button" data-bs-target="#textCarousel" data-bs-slide-to="0" 
                                            class="active" aria-current="true" aria-label="Slide 1"></button>
                                    <button type="button" data-bs-target="#textCarousel" data-bs-slide-to="1" 
                                            aria-label="Slide 2"></button>
                                    <button type="button" data-bs-target="#textCarousel" data-bs-slide-to="2" 
                                            aria-label="Slide 3"></button>
                                </div>

                                <!-- Contenido del carrusel -->
                                <div class="carousel-inner">
                                    <div class="carousel-item active">
                                        <div class="d-flex justify-content-center align-items-center" style="height: 300px;">
                                            <!-- Primer mensaje del carrusel -->
                                            <h4 class="text-center text-white">Regístrate para disfrutar de <br />nuestros excelentes servicios.</h4>
                                        </div>
                                    </div>
                                    <div class="carousel-item">
                                        <div class="d-flex justify-content-center align-items-center" style="height: 300px;">
                                            <!-- Segundo mensaje del carrusel -->
                                            <h4 class="text-center text-white">Tenemos las mejores oportunidades <br />laborales a tan solo un clic.</h4>
                                        </div>
                                    </div>
                                    <div class="carousel-item">
                                        <div class="d-flex justify-content-center align-items-center" style="height: 300px;">
                                            <!-- Tercer mensaje del carrusel -->
                                            <h4 class="text-center text-white">NetJobs ha sido creado por <br />StarTec para facilitar a las <br />personas encontrar empleo</h4>
                                        </div>
                                    </div>
                                </div>

                                <!-- Botones de navegación del carrusel -->
                                <button class="carousel-control-prev" type="button" data-bs-target="#textCarousel" data-bs-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Anterior</span>
                                </button>
                                <button class="carousel-control-next" type="button" data-bs-target="#textCarousel" data-bs-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="visually-hidden">Siguiente</span>
                                </button>
                            </div>
                            <!-- Botón para redirigir a la página de registro -->
                            <a href="Registrarse.aspx" class="btn btn-outline-light">Registrarse</a>
                        </center>
                    </div>

                    <!-- Espaciador para diseño responsivo -->
                    <div class="col-sm-1 col-md-1">&nbsp;</div>
                </div>
            </div>
            <hr />
        </div>
    </form>
</body>
</html>
