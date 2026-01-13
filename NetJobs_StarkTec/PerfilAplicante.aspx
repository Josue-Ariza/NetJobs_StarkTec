<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerfilAplicante.aspx.cs" Inherits="NetJobs_StarkTec.PerfilAplicante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Aplicante
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="profile-container">
        <!-- Foto de perfil y nombre -->
        <div class="profile-photo-name">
            <div class="profile-photo">
                <asp:Image ID="imgPerfil" runat="server" alt="Foto de perfil del usuario" />
            </div>
            
        </div>

        <!-- Información del usuario -->
    
        <div class="profile-info">
            <!-- Tarjeta de experiencia laboral -->
            
                   <div class="profile-name">
                <asp:Label ID="lblNombre" runat="server" CssClass="profile-name-label" Text="Nombre del aplicante"></asp:Label>
                </div>
            <br />
            <div class="info-card">
                <h3>Experiencia Laboral</h3>
                <asp:Repeater ID="rptExperiencia" runat="server">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <asp:Label ID="lblExperiencia" runat="server" Text='<%# Eval("Puesto") + " en " + Eval("Empresa") + " (" + Eval("Año") + ")" %>'></asp:Label>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Tarjeta de habilidades -->
            <div class="info-card">
                <h3>Habilidades</h3>
                <asp:Repeater ID="rptHabilidades" runat="server">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <asp:Label ID="lblHabilidad" runat="server" Text='<%# Eval("Descripcion") %>'></asp:Label>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Tarjeta de educación -->
            <div class="info-card">
                <h3>Educación</h3>
                <asp:Repeater ID="rptEducacion" runat="server">
                    <ItemTemplate>
                        <ul>
                            <li>
                                <asp:Label ID="lblEducacion" runat="server" Text='<%# Eval("Titulo") + ", " + Eval("Institucion") + " (" + Eval("AñoGraduacion") + ")" %>'></asp:Label>
                            </li>
                        </ul>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Botón para volver a la página de notificaciones -->
            <asp:Button ID="btnBack" runat="server" Text="Volver" PostBackUrl="~/Notificaciones.aspx" CssClass="back-home-btn" />
        </div>
    </div>
</asp:Content>
