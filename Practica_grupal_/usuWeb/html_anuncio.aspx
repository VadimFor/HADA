
<%@ Page Title="Sanvi Houses Anuncio" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="html_anuncio.aspx.cs" Inherits="usuWeb.html_anuncio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet">

    <br />
    <br />
    <br />
    <br />
  <section class="property-single nav-arrow-b">
    <div class="container">
      <div class="row">
        <div class="col-sm-12">
            <div id="property-single-carousel" style ="width: 600px; height:600px; left:250px;" class="owl-carousel owl-arrow gallery-property owl-loaded owl-drag">

          <div class="owl-stage-outer" style="height:500px">
              <div class="owl-stage" style="transform: translate3d(-1380px, 0px, 0px); transition: all 0s ease 0s; width: 4830px; height:500px">
              <div class="owl-item cloned" style="width: 690px;">
                  <div class="carousel-item-b" style="width: 600px; height: 600px; overflow: hidden; display: flex;">
              <img src=" " alt="" runat="server" id="img1" style="max-width: inherit; max-height: inherit; height: inherit; width: inherit; object-fit: cover;">
            </div></div>
            <%if (img2.Src != "")
                    {                                        %>
              <div class="owl-item cloned" style="width: 690px;">
                  <div class="carousel-item-b" style="width: 600px; height: 600px; overflow: hidden; display: flex;">
              <img src=" " alt="" runat="server" id="img2" style="max-width: inherit; max-height: inherit; height: inherit; width: inherit; object-fit: cover;">
            </div></div>
                                  <% } %>
              <%if (img3.Src != "")
                    {                                        %>
              <div class="owl-item active" style="width: 690px;">
                  <div class="carousel-item-b" style="width: 600px; height: 600px; overflow: hidden; display: flex;">
              <img src=" " alt="" runat="server" id="img3" style="max-width: inherit; max-height: inherit; height: inherit; width: inherit; object-fit: cover;">
            </div></div>
                                                <% } %>

            </div></div><div class="owl-nav"><button type="button" role="presentation" class="owl-prev"><i class="ion-ios-arrow-back" aria-hidden="true"></i></button><button type="button" role="presentation" class="owl-next"><i class="ion-ios-arrow-forward" aria-hidden="true"></i></button></div><div class="owl-dots"><button role="button" class="owl-dot active"><span></span></button><button role="button" class="owl-dot"><span></span></button><button role="button" class="owl-dot"><span></span></button></div></div>
                <div class="row justify-content-between">
            <div class="col-md-5 col-lg-4">
              <div class="property-price d-flex justify-content-center foo">
                <div class="card-header-c d-flex">
                  <div class="card-box-ico">
                    <span class="ion-money">€</span>
                  </div>
                  <div class="card-title-c align-self-center">
                      <asp:Label ID="PrecioLabel" runat="server" Text="" CssClass="title-c"></asp:Label>
                  </div>
                </div>
              </div>
              <div class="property-summary">
                <div class="row">
                  <div class="col-sm-12">
                    <div class="title-box-d section-t4">
                      <h3 class="title-d">Resumen</h3>
                    </div>
                  </div>
                </div>
                <div class="summary-list">
                  <ul class="list">
                      <li class="d-flex justify-content-between">
                          <asp:Label ID="EstadoLabel" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                      </li>
                    <li class="d-flex justify-content-between">
                        <asp:Label ID="FPLabel" runat="server" Font-Bold="true" ForeColor="Black"></asp:Label>
                        <span>
                            <asp:Label runat="server" ID="AlquilerLabel" Visible="false" Text=""></asp:Label>
                            <asp:Label runat="server" ID="VentaLabel" Visible="false" Text=""></asp:Label>
                         </span>
                    </li>
                    <li class="d-flex justify-content-between">
                      <strong>ID de la propiedad:</strong>
                       <span><asp:Label runat="server" ID="PropertyIdLabel" Text=""></asp:Label></span>
                    </li>
                    <li class="d-flex justify-content-between">
                      <strong>Dirección:</strong>
                       <span><asp:Label runat="server" ID="DireccionLabel" Text=""></asp:Label></span>
                    </li>
                    <li class="d-flex justify-content-between">
                      <strong>Categoría de propiedad:</strong>
                       <span><asp:Label runat="server" ID="CategoriaLabel" Text=""></asp:Label></span>
                    </li>
                    <li class="d-flex justify-content-between">
                      <strong>Área:</strong>
                        <span><asp:Label runat="server" ID="MetrosLabel" Text=""></asp:Label>m<sup>2</sup></span>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
            <div class="col-md-7 col-lg-7 section-md-t3">
              <div class="row">
                <div class="col-sm-12">
                  <div class="title-box-d">
                    <h3 class="title-d">Descripción de la Propiedad</h3>
                  </div>
                </div>
              </div>
              <div class="property-description">
                <p class="description color-text-a">
                    <asp:Label ID="DescriptionLabel" runat="server" Text="" CssClass="description color-text-a"></asp:Label>
                </p>
              </div>
              <div class="row section-t3">
                <div class="col-sm-12">
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-md-12">
          <div class="row section-t3">
            <div class="col-sm-12">
              <div class="title-box-d">
                <h3 class="title-d">Contactar Propietario</h3>
              </div>
                <div class="col-md-12">
                <asp:Button ID="Button1" runat="server" CssClass="btn btn-a" OnClick="ContactButton_Click" text="Contactar Propietario"/>
             </div>
            </div>
          </div>
        </div>
       </div>
        </div>
    </section>

    <br />
    <br />
    <br />

    <center>
            <%--                ENVIAR MENSAJE BUTTON--%>
        <asp:TextBox runat="server" ID="EnviarMensajeTextBox" CSSClass="input-comentario" type="text" placeholder="Envia un mensaje..."></asp:TextBox>
        <asp:Button runat="server" ID="EnviarMensajeButton" CssClass="publicar-comentario" Text="Enviar Mensaje (Al Anunciante)" OnClick="EnviarMensajeButton_Click" />
        <asp:Panel runat="server" ID="AvisoPanel" Visible="false">
        <asp:Label runat="server" ID="EnviarMensajeConfirmar" Text="Mensaje enviado!" Visible="false"></asp:Label>
        <asp:Label runat="server" ID="AvisoSesion" Text="No se puede enviar mensajes sin iniciar sesión!" Visible="false"></asp:Label>
        <asp:Label runat="server" ID="AvisoSinTexto" Text="No se puede enviar un mensaje vacío!" Visible="false"></asp:Label>
        <asp:Label runat="server" ID="AvisoSiMismo" Text="No se puede enviar un mensaje a sí mismo!" Visible="false"></asp:Label>
            <br />
            <br />
        </asp:Panel>
    </center>

    <%--COMENTARIOS--%>
    <center>
    <section id="comment-section" class="comment-section">
            <asp:TextBox type="text" id="EntradaComentario" CssClass="input-comentario" placeholder="Tienes un comentario?" runat="server"></asp:TextBox>
            <asp:Button ID="PublicarButton" CssClass="publicar-comentario" Text="Publicar Comentario" runat="server" OnClick="PublicarComentario"></asp:Button>
    </section>
    </center>

    <div class ="container" style ="margin-bottom:30px">
<div class="row" style="align-items: center; justify-content: center;">

    <% for(int i = 0; i<enComentario.lista.Count(); i++){ %>
    <div class="col-md-8">
        <div class="media g-mb-30 media-comment">
            <img class="d-flex g-width-50 g-height-50 rounded-circle g-mt-3 g-mr-15" src="https://bootdey.com/img/Content/avatar/avatar8.png" alt="Image Description">
            <div class="media-body u-shadow-v18 g-bg-secondary g-pa-30">
              <div class="g-mb-15">
                <h5 class="h5 g-color-gray-dark-v1 mb-0"><%= enComentario.lista[i].usuario.Split('@')[0] %></h5>
              </div>

              <p><%= enComentario.lista[i].texto %></p>

            </div>
        </div>
    </div>
      <% } %>
</div>
    </div>

</asp:Content>
