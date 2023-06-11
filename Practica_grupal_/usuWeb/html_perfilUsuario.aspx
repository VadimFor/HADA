<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario.Master" AutoEventWireup="true" CodeBehind="html_perfilUsuario.aspx.cs" Inherits="usuWeb.html_perfilUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    
    <!--/ Intro Single star /-->
  <section class="intro-single">
    <div class="container">
      <div class="row">
        <div class="col-md-12 col-lg-8">
          <div class="title-single-box">
            <h1 class="title-single">Perfil de Usuario</h1>
          </div>
        </div>
        <div class="col-md-12 col-lg-4">
          <nav aria-label="breadcrumb" class="breadcrumb-box d-flex justify-content-lg-end">
            <ol class="breadcrumb">
              <li class="breadcrumb-item">
                <a href="/Default.aspx">Inicio</a>
              </li>
              <li class="breadcrumb-item active" aria-current="page">
                Perfil
              </li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </section>
  <!--/ Intro Single End /-->

  <!--/ Agent Single Star /-->
  <section class="agent-single">
    <div class="container">
      <div class="row">
        <div class="col-sm-12">
          <div class="row">
            <div class="col-md-6">
              <div class="agent-avatar-box">
                <img ID="foto_perfil" src="/imagenes/profile.jpg" class="agent-avatar img-fluid" runat="server" >
              </div>
            </div>
            <div class="col-md-5 section-md-t3">
              <div class="agent-info-box">
                
                <div class="agent-content mb-3">
                  
                  <div class="info-agents color-a">
                     <p>
                        <strong> Nombre:</strong> <asp:Label ID="usuario_nombre" class="color-text-a" runat="server" Text="nombre"></asp:Label>
                     </p>
                     <p>
                        <strong> Apellido:</strong> <asp:Label ID="usuario_apellido" class="color-text-a" runat="server" Text="Apellido"></asp:Label>
                     </p>
                     <p>
                        <strong> NIF:</strong> <asp:Label ID="usuario_nif" class="color-text-a" runat="server" Text="N34362761Y"></asp:Label>
                     </p>
                     <p>
                        <strong> Telefono:</strong> <asp:Label ID="usuario_telefono" class="color-text-a" runat="server" Text="602 24 24 24"></asp:Label>
                     </p>
                     <p>
                        <strong> Email:</strong> <asp:Label ID="usuario_email" class="color-text-a" runat="server" Text="email@email.com"></asp:Label>
                     </p>
                    
                  </div>

                </div>
                  <div><strong>MENÚ OPCIONES</strong></div>

                  <div> 
                      <a > Cambiar foto perfil:</a>
                      <asp:FileUpload ID="SubirImagenes" runat="server"  AllowMultiple="false" /> 
                     <asp:Label ID="listaArchivosSubidos"  runat="server" /> 
                     <asp:Button ID="UploadButton" Text="Confirmar nueva foto" OnClick="onCambiarImagenPerfil" runat="server"></asp:Button>
                  </div>  

                  <div style="padding-top:20px;">   </div>
                  <div><asp:Button text="Eliminar Usuario" onClick="onBorrar" ID="buttom_Borrar" runat="server" /></div>
              </div>
            </div>
          </div>
        </div>
          </div>
        </div>
      </section>
    </asp:Content>
