<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActualizarOferta.aspx.cs" Inherits="NetJobs_StarkTec.ActualizarOferta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    
    <!-- Contenedor principal de la sección de actualización de oferta -->
<div class="publicar-oferta-container">
    <h2>Actualizar oferta</h2> <!-- Título de la sección -->

    <!-- Contenedor del formulario -->
    <div class="form-container">
        
        <!-- Campo para el título del trabajo -->
        <div class="form-group">
            <label for="txtTitulo">Título del Trabajo:</label>
            <!-- TextBox para ingresar el título del trabajo, se usa CssClass para aplicar estilos -->
            <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej. Desarrollador Web"></asp:TextBox>
        </div>

        <!-- Campo para la descripción del trabajo -->
        <div class="form-group">
            <label for="txtDescripcion">Descripción del Trabajo:</label>
            <!-- TextBox de múltiples líneas (TextMode="MultiLine") para la descripción -->
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Escribe una descripción del trabajo"></asp:TextBox>
        </div>

        <!-- Campo para los requisitos del trabajo -->
        <div class="form-group">
            <label for="txtRequisitos">Requisitos:</label>
            <!-- TextBox de múltiples líneas (TextMode="MultiLine") para los requisitos -->
            <asp:TextBox ID="txtRequisitos" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Escribe los requisitos del trabajo"></asp:TextBox>
        </div>

        <!-- Campo para la ubicación o empresa -->
        <div class="form-group">
            <label for="txtUbicacion">Ubicación o empresa:</label>
            <!-- TextBox para ingresar la ubicación o empresa -->
            <asp:TextBox ID="txtUbicacion" runat="server" CssClass="form-control" placeholder="Ej. San Salvador, El Salvador"></asp:TextBox>
        </div>

        <!-- Campo para el salario -->
        <div class="form-group">
            <label for="txtSalario">Salario ($):</label>
            <!-- TextBox para ingresar el salario -->
            <asp:TextBox ID="txtSalario" runat="server" CssClass="form-control" placeholder="Ej. 1200"></asp:TextBox>
            
            <!-- Validador personalizado para asegurarse de que el salario sea un número positivo -->
            <asp:CustomValidator ID="cvSalario" runat="server" ControlToValidate="txtSalario" 
                ErrorMessage="Por favor, ingresa un salario válido (número positivo)." 
                OnServerValidate="cvSalario_ServerValidate" CssClass="text-danger" Display="Dynamic"></asp:CustomValidator>
            <br />
        </div>

        <!-- Botón para actualizar la oferta -->
        <div class="form-group">
            <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="btnActualizar_Click" />
        </div>
    </div>
   
</div>

</asp:Content>
