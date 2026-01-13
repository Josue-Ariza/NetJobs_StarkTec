<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="NetJobs_StarkTec.Inicio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Inicio
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <!-- Encabezado de bienvenida -->
    <h1 class="welcome-text">Bienvenido a NetJobs</h1>

    <!-- Barra de búsqueda -->
    <div class="search-bar">
        <input type="text" 
               placeholder="Buscar ofertas, empresas o ubicaciones..." 
               class="search-input" 
               id="search-input" 
               name="search-input" 
               value='<%= Request.Form["search-input"] ?? "" %>' />
        <button class="search-btn" runat="server" onserverclick="BuscarOfertas">Buscar</button>
    </div>

    <!-- Sección para mostrar publicaciones hechas por otros usuarios -->
    <div class="publications">
        <div class="row">
            <!-- Repeater para iterar sobre las ofertas laborales disponibles -->
            <asp:Repeater ID="rptOfertas" runat="server">
                <ItemTemplate>
                    <!-- Contenedor para cada publicación -->
                    <div class="col-md-12">
                        <div class="card-large mb-3" style="width: 100%;">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Titulo") %></h5>
                                <p class="card-text"><%# Eval("Descripcion") %></p>
                                <p class="card-text"><strong>Requisitos:</strong> <%# Eval("Requisitos") %></p>
                                <p class="card-text"><strong>Ubicación o empresa:</strong> <%# Eval("Ubicacion") %></p>
                                <p class="card-text"><strong>Salario: $</strong> <%# Eval("Salario") %></p>
                                <p class="card-text">
                                    <small class="text-muted">Publicado por: <%# Eval("UsuarioNombre") %> el <%# Eval("fechaPublicacion", "{0:dd/MM/yyyy}") %></small>
                                </p>
                                <div style="text-align: right;">
                                    <asp:Button ID="btnAplica" runat="server" Text="Aplicar" CssClass="btn btn-secondary btn-sm" 
                                                OnClick="btnAplica_Click" CommandArgument='<%# Eval("OfertaID") %>' />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
