<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="usuWeb.Default" %>

<%--<!--Título de la página web -->
<title>Sanvi Houses Homepage</title>

<!--Link del archivo css -->
<link rel= "Stylesheet" type="text/css" href="css/PRINCIPAL.css" /> --%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <div class="intro intro-carousel">
    <div id="carousel" class="owl-carousel owl-theme">
        <asp:ListView 
            ID="ListView1"
            runat="server"  ObjectDataSource="InmoData"
            >
            <LayoutTemplate>
                    <div id="ItemPlaceholder" runat="server">
                    </div>
            </LayoutTemplate>
            <ItemTemplate>
      <div class="carousel-item-a intro-item bg-image" style="background-image: url('<%#Eval("img1").ToString()%>')" >
        <div class="overlay overlay-a"></div>
        <div class="intro-content display-table">
          <div class="table-cell">
            <div class="container">
              <div class="row">
                <div class="col-lg-8">
                  <div class="intro-body">
                    <p class="intro-title-top">
                       </p>
                      <h1 class="intro-title mb-4">
                          <asp:Label CssClass="color-b" Text='<%# Eval("cod_postal")%>' runat="server"></asp:Label>
                           <br>  <asp:Label Text='<%# Eval("direccion")%>' runat="server"></asp:Label>
                      </h1>
                    <p class="intro-subtitle intro-price">
                       <asp:LinkButton onClick="verAnuncio" CommandArgument='<%#Eval("anuncioId")%>' runat="server" CssClass="a price-a" href='<%# String.Format("/html_anuncio.aspx?id={0}", Eval("anuncioId")) %>' >€
                           <asp:Label Text='<%# Eval("precio")%>' runat="server"></asp:Label>
                       </asp:LinkButton>
                    </p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
                </ItemTemplate>
                </asp:ListView>
     </div>
  </div>

</asp:Content>