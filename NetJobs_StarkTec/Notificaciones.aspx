<%@ Page Title="Notificaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="NetJobs_StarkTec.Notificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Notificaciones
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <!-- Puedes agregar aquí los estilos adicionales o scripts si es necesario -->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <div class="notificaciones-container">
        <!-- Contenido principal -->
        <div class="notificaciones-content">
            <h2>Notificaciones de aplicantes</h2>

            <div class="notification-list">
                <!-- Repeater para mostrar las aplicaciones -->
                   <asp:Repeater ID="rptAplicaciones" runat="server">
        <ItemTemplate>
            <div class="notification-item">
                <h4>Aplicación a tu oferta: <asp:Label ID="lblOfertaTitulo" runat="server" Text='<%# Eval("TituloOferta") %>'></asp:Label></h4>
                <p>El usuario <asp:Label ID="lblUsuarioAplicante" runat="server" Text='<%# Eval("NombreUsuario") %>'></asp:Label> ha aplicado.</p>
                <span class="notification-date"><%# Eval("FechaAplicacion", "Fecha en la que aplicó: {0:dd/MM/yyyy}") %></span>
                <br />
                <asp:LinkButton ID="lnkDescargarCV" runat="server" CommandArgument='<%# Eval("CVRuta") %>' Text="Descargar CV" OnClick="lnkDescargarCV_Click" CssClass="download-link"></asp:LinkButton>
                <asp:LinkButton ID="lnkVerPerfil" runat="server" CommandArgument='<%# Eval("UsuarioID") %>' Text="Ver Perfil del aplicante" OnClick="lnkVerPerfil_Click" CssClass="view-profile-link"></asp:LinkButton>
                <!-- Botón Recibida -->
                <asp:Button ID="btnRecibida" runat="server" Text="Recibida" CommandArgument='<%# Eval("AplicacionID") %>' OnClick="btnRecibida_Click" CssClass="btn-recibida" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
            </div>
        </div>
    </div>

    
</asp:Content>
