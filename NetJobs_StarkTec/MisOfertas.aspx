<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisOfertas.aspx.cs" Inherits="NetJobs_StarkTec.MisOfertas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Mis ofertas
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
   <h2 align="center">Mis ofertas publicadas</h2> <!-- Título centrado de la sección -->

<div class="mis-ofertas-container"> <!-- Contenedor para las ofertas publicadas -->

    <!-- Repeater para mostrar las ofertas publicadas -->
    <asp:Repeater ID="rptMisOfertas" runat="server" OnItemCommand="rptMisOfertas_ItemCommand">
        <ItemTemplate> <!-- Plantilla de ítem del Repeater -->
            <div class="card"> <!-- Contenedor de cada oferta, con estilo de tarjeta -->
                
                <!-- Muestra el título de la oferta -->
                <h3><%# Eval("Titulo") %></h3>

                <!-- Muestra la descripción de la oferta -->
                <p><strong>Descripción:</strong> <%# Eval("Descripcion") %></p>

                <!-- Muestra los requisitos de la oferta -->
                <p><strong>Requisitos:</strong> <%# Eval("Requisitos") %></p>

                <!-- Muestra la ubicación o empresa de la oferta -->
                <p><strong>Ubicación o empresa:</strong> <%# Eval("Ubicacion") %></p>

                <!-- Muestra el salario de la oferta, formateado como moneda -->
                <p><strong>Salario:</strong> <%# Eval("Salario", "{0:C}") %></p>

                <!-- Contenedor de botones de acción -->
                <div class="btn-container">
                    <!-- Botón para editar la oferta -->
                    <asp:Button ID="btnEdit" runat="server" Text="Actualizar" 
                        CommandName="Edit" CommandArgument='<%# Eval("OfertaID") %>' CssClass="btn" />
                    
                    <!-- Botón para eliminar la oferta -->
                    <asp:Button ID="btnDelete" runat="server" Text="Eliminar" 
                        CommandName="Delete" CommandArgument='<%# Eval("OfertaID") %>' CssClass="btn btn-danger" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</div>

</asp:Content>
