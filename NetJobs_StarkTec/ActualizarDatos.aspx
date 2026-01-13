
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActualizarDatos.aspx.cs" Inherits="NetJobs_StarkTec.ActualizarDatos" %>
<asp:Content ID="Content2" ContentPlaceHolderID="title" runat="server">
    Actualizar datos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Body" runat="server">
    <div class="form-container">
        <h2 class="form-title">Actualizar Datos</h2>

       

        <!-- Sección de Experiencia Laboral -->
        <div class="section">
            <h3 class="section-title">Experiencia Laboral</h3>
            <div class="form-group">
                <asp:TextBox ID="txtEmpresa" class="form-control" runat="server" placeholder="Nombre de la Empresa"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtAño" class="form-control" runat="server" placeholder="Año"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtPuesto" class="form-control" runat="server" placeholder="Puesto"></asp:TextBox>
            </div>
            <asp:Button ID="btnAgregarExperiencia" runat="server" Text="Agregar Experiencia" CssClass="btn btn-primary" OnClick="btnAgregarExperiencia_Click" />

            <!-- Mensaje de confirmación al agregar experiencia -->
            <asp:Label ID="lblMensajeExperiencia" runat="server" CssClass="text-success"></asp:Label>
        </div>

       

        <!-- Sección de Formación Académica -->
        <div class="section">
            <h3 class="section-title">Formación Académica</h3>
            <div class="form-group">
                <asp:TextBox ID="txtInstitucion" class="form-control" runat="server" placeholder="Ingrese el nombre de la institución"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtTitulo" class="form-control" runat="server" placeholder="Ingrese el título obtenido"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtAñoGraduacion" class="form-control" runat="server" placeholder="Ingrese el año de graduación"></asp:TextBox>
            </div>
            <asp:Button ID="btnAgregarFormacion" runat="server" Text="Agregar Formación Académica" CssClass="btn btn-primary" OnClick="btnAgregarFormacion_Click" />

            <!-- Mensaje de confirmación al agregar formación -->
            <asp:Label ID="lblMensajeFormacion" runat="server" CssClass="text-success"></asp:Label>
        </div>
         <!-- Sección de Habilidades -->
 <div class="section">
     <h3 class="section-title">Habilidades</h3>
     <div class="form-group">
         <asp:TextBox ID="txtHabilidades" class="form-control" runat="server" TextMode="MultiLine" Rows="4" placeholder="Describa sus habilidades"></asp:TextBox>
     </div>
 </div>
         <!-- Sección para subir la foto de perfil -->
 <div class="section">
     <h3 class="section-title">Foto de Perfil</h3>
     <div class="form-group">
         <asp:Label ID="lblFotoPerfil" runat="server" Text="Seleccione una foto de perfil:"></asp:Label>
         <asp:FileUpload ID="fuFotoPerfil" runat="server" CssClass="form-control" />
     </div>
 </div>
        <!-- Botones de acción -->
        <div class="button-group">
            <asp:Button ID="btnAceptar" class="btn btn-primary" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
            <asp:Button ID="btnVolver" class="btn btn-cancel" runat="server" PostBackUrl="~/MiPerfil.aspx" Text="Volver a mi perfil" />
        </div>
         <asp:Label ID="lblMensajeGeneral" runat="server" CssClass="text-success"></asp:Label>
     
    </div>
</asp:Content>
