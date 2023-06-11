<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="html_propiedad.aspx.cs" Inherits="usuWeb.html_propiedad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="idScriptManager"> 
    </asp:ScriptManager>
    <section class="intro-single">
    <div class="container">
      <div class="row">
        <div class="col-md-12 col-lg-8">
          <div class="title-single-box">
            <h1 class="title-single">Propiedades</h1>
          </div>
        </div>
        <div class="col-md-12 col-lg-4">
          <nav aria-label="breadcrumb" class="breadcrumb-box d-flex justify-content-lg-end">
            <ol class="breadcrumb">
              <li class="breadcrumb-item">
                <a href="/Default.aspx">Inicio</a>
              </li>
              <li class="breadcrumb-item active" aria-current="page">
                Propiedades
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
         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="elegirCat"  ObjectDataSource="InmoData" DataSourceID="SqlDataSource1" DataTextField="tipo" DataValueField="tipo" AppendDataBoundItems="true">
            <Items>
              <asp:ListItem Enabled="true" Text="Todas" Value="Todas" ></asp:ListItem>
            </Items>
         </asp:DropDownList>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:InmoData %>" SelectCommand="SELECT [tipo] FROM [Categoria]"></asp:SqlDataSource>
                
      <asp:Panel CssClass="row" runat="server" ID="Panel1">
        <asp:ListView 
            ID="ListView1"
            runat="server" OnPagePropertiesChanging="ListView1_PagePropertiesChanging" OnItemDataBound="ListView1_ItemDataBound"
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
                           ImageUrl='<%#Eval("img1")%>'/>
                    </div>
                    <div runat="server" class="card-overlay">
                        <div runat="server" class="card-overlay-a-content">
                        <div runat="server" class="card-header-a">
                            <h2 runat="server" class="card-title-a">
                                 <asp:Label runat="server" Text='<%# Eval("cod_postal") %>' />
                                <br />  <asp:Label runat="server" Text='<%# Eval("direccion") %>' />
                            </h2>
                        </div>
                        <div runat="server" class="card-body-a">
                            <div runat="server" class="price-box d-flex">
                            <span runat="server" class="price-a">€ <asp:Label runat="server" Text='<%# Eval("precio") %>' /></span>
                            </div>
                            <asp:LinkButton ID="linkbutton" OnClick="verAnuncio" CommandArgument='<%#Eval("id")%>' runat="server" CssClass="a link-a" Text="Click here to view" ></asp:LinkButton>
                        </div>
                        <div runat="server" class="card-footer-a">
                            <ul runat="server" class="card-info d-flex justify-content-around">
                            <li runat="server">
                                <h4 runat="server" class="card-info-title">Area</h4>
                                <span runat="server">  <asp:Label runat="server" Text='<%# Eval("metros") %>' />m
                                <sup runat="server">2</sup>
                                </span>
                            </li>
                                
                               <li runat="server">
                                   <h4 runat="server"  class="card-info-title" ><asp:Literal id="lbl_favs" runat=server Text="Añadir a favoritos"/></h4>
                                <asp:LinkButton  ID="favs" runat="server" CommandArgument='<%#Eval("id")%>' OnClick="anyadirFavoritos">
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
              <asp:DataPager ID="DataPager1" PagedControlID="ListView1" PageSize="3" runat="server">  
                    <Fields>  
                        <asp:NumericPagerField 
                            ButtonType="Link"
                            /> </Fields>
                </asp:DataPager>
            </ul>
          </nav>
        </div>
      </div>
    </div>
  </section>
  </ContentTemplate>
 
  </asp:UpdatePanel>
  <%--Control UpdateProgress, AJAX Loader mientras se espera la respuesta asíncrona--%>
<asp:UpdateProgress runat="server" ID="idUpdateProgress" 
                    AssociatedUpdatePanelID="idUpdatePanel" DynamicLayout="true">
    <ProgressTemplate>

        <%--AQUÍ EL TEXTO O IMAGEN AJAX LOADER--%>

    </ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>
