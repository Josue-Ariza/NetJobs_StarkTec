<%@ Page Title="Aplicar a Oferta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AplicarOferta.aspx.cs" Inherits="NetJobs_StarkTec.AplicarOferta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Aplicar a Oferta
</asp:Content>





<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <!-- Título principal de la página -->
    <h2 align="center">APLICAR A OFERTA: </h2>

    <!-- Sección que muestra los detalles de la oferta a la que el usuario puede aplicar -->
    <div class="oferta-details">
        <!-- Etiquetas que se llenarán dinámicamente con los datos de la oferta -->
        <h3><asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h3>
        <p><strong>Descripción:</strong> <asp:Label ID="lblDescripcion" runat="server" Text=""></asp:Label></p>
        <p><strong>Requisitos:</strong> <asp:Label ID="lblRequisitos" runat="server" Text=""></asp:Label></p>
        <p><strong>Ubicación o empresa:</strong> <asp:Label ID="lblUbicacion" runat="server" Text=""></asp:Label></p>
        <p><strong>Salario: $</strong> <asp:Label ID="lblSalario" runat="server" Text=""></asp:Label></p>
    </div>

    <!-- Formulario donde el usuario puede subir su CV y postularse a la oferta -->
    <div class="formulario-aplicar">
        <!-- Campo para subir el CV en formato PDF -->
        <div class="form-group">
            <label for="fileCV">Subir CV (PDF):</label>
            <asp:FileUpload ID="fileCV" runat="server" Accept="application/pdf" CssClass="file-upload" />
        </div>
        <!-- Botón que permite al usuario aplicar a la oferta -->
        <div class="form-group">
            <asp:Button ID="btnAplicar" runat="server" Text="Aplicar" OnClick="btnAplicar_Click" CssClass="btn-aplicar" />
        </div>
    </div>
</asp:Content>
