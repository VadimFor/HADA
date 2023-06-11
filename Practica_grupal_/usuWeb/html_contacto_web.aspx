<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="html_contacto_web.aspx.cs" Inherits="usuWeb.html_contacto_web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <!--/ Intro Single star /-->
  <section class="intro-single">
    <div class="container">
      <div class="row">
        <div class="col-md-12 col-lg-8">
          <div class="title-single-box">
            <h1 class="title-single">Contacta con nosotros</h1>
          </div>
        </div>
        <div class="col-md-12 col-lg-4">
          <nav aria-label="breadcrumb" class="breadcrumb-box d-flex justify-content-lg-end">
            <ol class="breadcrumb">
              <li class="breadcrumb-item">
                <a href="/Default.aspx">Inicio</a>
              </li>
              <li class="breadcrumb-item active" aria-current="page">
                Contacto
              </li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </section>
  <!--/ Intro Single End /-->

  <!--/ Contact Star /-->
  <section class="contact">
    <div class="container">
      <div class="row">
        <div class="col-sm-12">
          <div class="contact-map box">
            <div id="map" class="contact-map">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3129.2530658798764!2d-0.4939436848081682!3d38.34312437965993!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd62364c13e5b9e3%3A0xa64c1d4466be6e54!2sAv.%20Maisonnave%2C%2033%2C%2003003%20Alicante%20(Alacant)%2C%20Alicante!5e0!3m2!1ses!2ses!4v1650980568343!5m2!1ses!2ses"
                    width="100%" height="450" frameborder="0" style="border:0" allowfullscreen></iframe>               
            </div>
          </div>
        </div>
        <div class="col-sm-12 section-t8">
          <div class="row">
            <div class="col-md-7">
              <form class="form-a contactForm" action="" method="post" role="form">
                <div id="errormessage"></div>
                <div class="row">
                  <div class="col-md-6 mb-3">
                    <div class="form-group">
                        <asp:TextBox ID="name" type="text" runat="server" class="form-control form-control-lg form-control-a" placeholder="Nombre"></asp:TextBox>
                      <div class="validation"></div>
                    </div>
                  </div>
                  <div class="col-md-6 mb-3">
                    <div class="form-group">
                        <asp:TextBox ID="email" type="text" runat="server" class="form-control form-control-lg form-control-a" placeholder="Email"></asp:TextBox>
                      <div class="validation"></div>
                    </div>
                  </div>
                  <div class="col-md-12 mb-3">
                    <div class="form-group">
                        <asp:TextBox ID="asunto" type="text" runat="server" class="form-control form-control-lg form-control-a" placeholder="Asunto"></asp:TextBox>
                      <div class="validation"></div>
                    </div>
                  </div>
                  <div class="col-md-12 mb-3">
                    <div class="form-group">
                        <asp:TextBox ID="cuerpo" runat="server" TextMode="MultiLine" Rows="8" class="form-control form-control-lg form-control-a" placeholder="Mensaje"></asp:TextBox>
                      <div class="validation"></div>
                    </div>
                  </div>
                  <div class="col-md-12">
                    <asp:Button type="textarea" Text="Enviar" onClick="enviarMensaje" class="btn btn-a" runat="server"/>
                  </div>
                    <div class="col-md-12">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>
                </div>
              </form>
            </div>
            <div class="col-md-5 section-md-t3">
              <div class="icon-box section-b2">
                <div class="icon-box-icon">
                  <span class="ion-ios-paper-plane"></span>
                </div>
                <div class="icon-box-content table-cell">
                  <div class="icon-box-title">
                    <h4 class="icon-title">Saludanos</h4>
                  </div>
                  <div class="icon-box-content">
                    <p class="mb-1">Email.
                      <span class="color-a">sanvihouses@gmail.com</span>
                    </p>
                    <p class="mb-1">Teléfono.
                      <span class="color-a">+34 956 945 234</span>
                    </p>
                  </div>
                </div>
              </div>
              <div class="icon-box section-b2">
                <div class="icon-box-icon">
                  <span class="ion-ios-pin"></span>
                </div>
                <div class="icon-box-content table-cell">
                  <div class="icon-box-title">
                    <h4 class="icon-title">Encuentranos en </h4>
                  </div>
                  <div class="icon-box-content">
                    <p class="mb-1">
                      Av. Maisonnave, 33, 03003 Alicante (Alacant)
                      <br> España
                    </p>
                  </div>
                </div>
              </div>
              <div class="icon-box">
                <div class="icon-box-icon">
                  <span class="ion-ios-redo"></span>
                </div>
                <div class="icon-box-content table-cell">
                  <div class="icon-box-title">
                    <h4 class="icon-title">Redes Sociales</h4>
                  </div>
                  <div class="icon-box-content">
                    <div class="socials-footer">
                      <ul class="list-inline">
                        <li class="list-inline-item">
                            <asp:LinkButton CssClass="fa fa-pinterest" href="https://pin.it/6wx12ce" runat="server"></asp:LinkButton>
                        </li>
                        <li class="list-inline-item">
                            <asp:LinkButton CssClass="fa fa-twitter" href="https://twitter.com/SanviHouses" runat="server"></asp:LinkButton>
                        </li>
                        <li class="list-inline-item">
                            <asp:LinkButton CssClass="fa fa-instagram" href="https://www.instagram.com/sanvihouses/" runat="server"></asp:LinkButton>

                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
  <!--/ Contact End /-->
</asp:Content>
