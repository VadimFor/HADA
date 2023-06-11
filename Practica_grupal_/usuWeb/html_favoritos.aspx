<%@ Page Title="Sanvi Houses Favorito" Language="C#" MasterPageFile="~/Usuario.Master" AutoEventWireup="true" CodeBehind="html_favoritos.aspx.cs" Inherits="usuWeb.html_favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="idScriptManager"> 
    </asp:ScriptManager>
    <section class="intro-single">
    <div class="container">
      <div class="row">
        <div class="col-md-12 col-lg-8">
          <div class="title-single-box">
            <h1 class="title-single">Favoritos</h1>
            <span class="color-text-a">Propiedades</span>
          </div>
        </div>
        <div class="col-md-12 col-lg-4">
          <nav aria-label="breadcrumb" class="breadcrumb-box d-flex justify-content-lg-end">
            <ol class="breadcrumb">
              <li class="breadcrumb-item">
                <a href="/">Default</a>
              </li>
              <li class="breadcrumb-item active" aria-current="page">
                Favoritos
              </li>
            </ol>
          </nav>
        </div>
      </div>
    </div>
  </section>
  <!--/ Intro Single End /-->

  <!--/ Property Grid Star /-->
  <asp:UpdatePanel runat="server" ID="idUpdatePanel">  
  <ContentTemplate>
  <section class="property-grid grid">
    <div class="container">
      <asp:Panel CssClass="row" runat="server" ID="Panel1">
        <asp:ListView 
            ID="ListView1"
            runat="server"
            >
            <LayoutTemplate>

                    <div id="ItemPlaceholder" runat="server">
                    </div>
            </LayoutTemplate>
            <ItemTemplate>
               <div runat="server" class="col-md-4">
                <div runat="server" class="card-box-a card-shadow">
                    <div runat="server" class="img-box-a">
                         <asp:Image id="Image1" CssClass= "img-a img-fluid" runat="server"
                           AlternateText="Image text"
                           ImageAlign="left"
                           ImageUrl='<%# Eval("ENPropiedad.img1")%>'/>
                    </div>
                    <div runat="server" class="card-overlay">
                        <div runat="server" class="card-overlay-a-content">
                        <div runat="server" class="card-header-a">
                            <h2 runat="server" class="card-title-a">
                            <a runat="server" href="#">
                                <br /> <%# Eval("Desc")%></a>
                            </h2>
                        </div>
                        <div runat="server" class="card-body-a">
                            <div runat="server" class="price-box d-flex">
                            <span runat="server" class="price-a"><%# Eval("Precio")%> €</span>
                            </div>
                            <asp:LinkButton ID="linkbutton" OnClick="verAnuncio" CommandArgument='<%#Eval("id")%>' runat="server" CssClass="a link-a" Text="Click here to view" ></asp:LinkButton>
                             <!--
                            <a runat="server" href="html_anuncio.aspx?id=" class="link-a">Click here to view

                            <span runat="server" class="ion-ios-arrow-forward"></span>
                            </a> -->
                        </div>
                        <div runat="server" class="card-footer-a">
                            <ul runat="server" class="card-info d-flex justify-content-around">
                            <li runat="server">
                                <h4 runat="server" class="card-info-title">Area</h4>
                                <span runat="server"><%# Eval("ENPropiedad.Metros")%>m
                                <sup runat="server">2</sup>
                                </span>
                            </li>
                            <li runat="server">
                                <h4 runat="server" class="card-info-title">Código postal</h4>
                                <span runat="server" ><%# Eval("ENPropiedad.Cod_postal")%></span>
                            </li>  
                                <li runat="server">
                                <asp:LinkButton  ID="favs" runat="server" OnClick="eliminarFav" CommandArgument='<%#Eval("id")%>' Text="Quitar">
                                   <i class="fa fa-star" ></i>
                                </asp:LinkButton></span>
                               </li>
                            </ul>
                        </div>
                        </div>
                    </div>
                </div>
               </div>
            </ItemTemplate>
        </asp:ListView>
      </asp:Panel>      
      <div class="row">
        <div class="col-sm-12">
          <nav class="pagination-a">
            <ul class="pagination justify-content-end">
              <li class="page-item">                
                    <asp:Button runat="server" ID="idAnterior" OnClick="anteriorPag" 
                        CssClass="page-link" Text ="Anterior"/>                                 
              </li>              
              <li class="page-item next">                
                    <asp:Button runat="server" ID="idSiguiente" OnClick="siguientePag" 
                        CssClass="page-link" Text ="Siguiente"/>                  
                
              </li>
            </ul>
          </nav>
        </div>
      </div>
    </div>
  </section>
      <asp:TextBox ID="desdeID" style ="visibility:hidden" runat="server"></asp:TextBox>
      <asp:TextBox ID="cargadoID" style ="visibility:hidden" runat="server"></asp:TextBox>
      <asp:TextBox ID="hastaID" style ="visibility:hidden" runat="server"></asp:TextBox>
  </ContentTemplate>
  <Triggers>
        <asp:AsyncPostBackTrigger ControlID="idAnterior" EventName="Click" />
  </Triggers>
  <Triggers>
        <asp:AsyncPostBackTrigger ControlID="idSiguiente" EventName="Click" />
  </Triggers>
  </asp:UpdatePanel>
  <%--Control UpdateProgress, AJAX Loader mientras se espera la respuesta asíncrona--%>
<asp:UpdateProgress runat="server" ID="idUpdateProgress" 
                    AssociatedUpdatePanelID="idUpdatePanel" DynamicLayout="true">
    <ProgressTemplate>

        <%--AQUÍ EL TEXTO O IMAGEN AJAX LOADER--%>

    </ProgressTemplate>
</asp:UpdateProgress>
    
</asp:Content>
