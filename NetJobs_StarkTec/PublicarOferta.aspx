<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PublicarOferta.aspx.cs" Inherits="NetJobs_StarkTec.PublicarOferta" %>


<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
    Publicar oferta
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">
    <!-- Contenedor principal para la sección de publicación de ofertas -->
    <div class="publicar-oferta-container">
        <h2>Publicar Oferta de Trabajo</h2>
        <div class="form-container">
            <!-- Campo para el título del trabajo -->
            <div class="form-group">
                <label for="txtTitulo">Título del Trabajo:</label>
                <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control" placeholder="Ej. Desarrollador Web"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvTitulo" 
                    runat="server" 
                    ControlToValidate="txtTitulo" 
                    ErrorMessage="El título es obligatorio." 
                    CssClass="text-danger" 
                    Display="Dynamic" />
            </div>

            <!-- Campo para la descripción del trabajo -->
            <div class="form-group">
                <label for="txtDescripcion">Descripción del Trabajo:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Escribe una descripción del trabajo"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvDescripcion" 
                    runat="server" 
                    ControlToValidate="txtDescripcion" 
                    ErrorMessage="La descripción es obligatoria." 
                    CssClass="text-danger" 
                    Display="Dynamic" />
            </div>

            <!-- Campo para los requisitos del trabajo -->
            <div class="form-group">
                <label for="txtRequisitos">Requisitos:</label>
                <asp:TextBox ID="txtRequisitos" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control" placeholder="Escribe los requisitos del trabajo"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvRequisitos" 
                    runat="server" 
                    ControlToValidate="txtRequisitos" 
                    ErrorMessage="Los requisitos son obligatorios." 
                    CssClass="text-danger" 
                    Display="Dynamic" />
            </div>

            <!-- Campo para la ubicación o empresa -->
            <div class="form-group">
                <label for="txtUbicacion">Ubicación o empresa:</label>
                <asp:TextBox ID="txtUbicacion" runat="server" CssClass="form-control" placeholder="Ej. San Salvador, El Salvador"></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvUbicacion" 
                    runat="server" 
                    ControlToValidate="txtUbicacion" 
                    ErrorMessage="La ubicación es obligatoria." 
                    CssClass="text-danger" 
                    Display="Dynamic" />
            </div>

            <!-- Campo para el salario -->
            <div class="form-group">
                <label for="txtSalario">Salario ($):</label>
                <asp:TextBox ID="txtSalario" runat="server" CssClass="form-control" placeholder="Ej. 1200 "></asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvSalario" 
                    runat="server" 
                    ControlToValidate="txtSalario" 
                    ErrorMessage="El salario es obligatorio." 
                    CssClass="text-danger" 
                    Display="Dynamic" />
                <asp:CustomValidator 
                    ID="cvSalario" 
                    runat="server" 
                    ControlToValidate="txtSalario" 
                    ErrorMessage="Por favor, ingresa un salario válido (número positivo)." 
                    OnServerValidate="cvSalario_ServerValidate" 
                    CssClass="text-danger" 
                    Display="Dynamic" />
                <br />
            </div>

            <!-- Botón para publicar la oferta -->
            <div class="form-group">
                <asp:Button ID="btnPublicar" runat="server" CssClass="btn btn-primary" Text="Publicar Oferta" OnClick="btnPublicar_Click" CausesValidation="true" />
            </div>
        </div>
    </div>
</asp:Content>
