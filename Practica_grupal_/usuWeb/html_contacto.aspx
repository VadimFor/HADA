<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Usuario.Master" CodeBehind="html_contacto.aspx.cs" Inherits="usuWeb.html_contacto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<!--/ Intro Single star /-->
  <section class="intro-single">
    <div class="container">
      <div class="row">
        <div class="col-md-12 col-lg-8">
          <div class="title-single-box">
            <h1 class="title-single">Contacto del agente</h1>
          </div>
        </div>
        <div class="col-md-12 col-lg-4">
          <nav aria-label="breadcrumb" class="breadcrumb-box d-flex justify-content-lg-end">
            <ol class="breadcrumb">
              <li class="breadcrumb-item">
                <a href="/Default.aspx">Inicio</a>
              </li>
                <li class="breadcrumb-item">
                <a href="/html_propiedad.aspx">Propiedades</a>
              </li>
              <li class="breadcrumb-item active" aria-current="page">
                <asp:Label id="Label1" runat="server"></asp:Label>
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
      <div>
        <div class="col-sm-12">
          <div>
           
            <div class="col-md-5 section-md-t3">
              <div class="agent-info-box">
                <div class="agent-title">
                  <div class="title-box-d">
                    <h3 class="title-d">
                        <asp:Label id="Label2" runat="server"></asp:Label>
                  </div>
                </div>
                <div class="agent-content mb-3">
                  
                  <div class="info-agents color-a">
                    <p>
                      <strong>Teléfono: </strong>
                        <asp:Label id="Label3" runat="server" class="color-text-a"></asp:Label>
                    </p>
                    <p>
                      <strong>Email: </strong>
                         <asp:Label id="Label4" runat="server" class="color-text-a"></asp:Label>
                    </p>
                      <p>
                           <asp:TextBox ID="cuerpo" runat="server" TextMode="MultiLine" Rows="8" class="form-control form-control-lg form-control-a" placeholder="Consultar"></asp:TextBox>
                      </p>
                      <p>
                           <asp:Button type="textarea" Text="Enviar mensaje" onClick="enviarMensaje" class="btn btn-a" runat="server"/>
                      </p>
                      <p>
                          <asp:Label ID="Label5" runat="server"></asp:Label>
                      </p>
                  </div>
                 
                </div>
                </div>
              </div>
            </div>
          </div>
        </div>
    </div>
  </section>
  <!--/ Agent Single End /-->

    </asp:Content>