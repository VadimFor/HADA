<%@ Page Title="" Language="C#" MasterPageFile="~/Usuario.Master" AutoEventWireup="true" CodeBehind="html_publicar.aspx.cs" Inherits="usuWeb.html_publicar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<body>

  <div class="click-closed"></div>

  <section class="form-publicar">
    <div class="container">
      <div class="row">
        <div class="col-sm-12 section-t8">
          <div class="row">
              <div id="errormessage"></div>
                <div class="row">

                  <div class="col-md-3 mb-3">
                    <div class="form-group">
                      <asp:DropDownList ID="Venta_Alquiler" cssclass="form-control form-control-lg form-control-a" AutoPostBack="true" OnSelectedIndexChanged="DDL_Venta_Alquiler" runat="server" >
                          <asp:ListItem Value="Venta" Text="Venta"/>
                          <asp:ListItem Value="Alquiler" Text="Alquiler"/>
                      </asp:DropDownList>
                    </div>
                  </div>

                  <div class="col-md-3 mb-3">
                    <div class="form-group">
                        <asp:DropDownList ID="categoria" CssClass="form-control form-control-lg form-control-a" runat="server" DataSourceID="CategoriaDDL" DataTextField="tipo" DataValueField="tipo">
                            <asp:ListItem Text="--Selecciona--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="CategoriaDDL" ConnectionString='<%$ ConnectionStrings:InmoData %>' SelectCommand="SELECT [tipo] FROM [Categoria]"></asp:SqlDataSource>
                    </div>
                  </div>

                  <div class="col-md-3 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="metros" CssClass="form-control form-control-lg form-control-a" placeholder="Metros" runat="server"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="metrosValidator" ValidationGroup="propiedad" runat="server" ControlToValidate="metros" ForeColor="Red" ErrorMessage="Campo obligatorio!"></asp:RequiredFieldValidator>--%>
                    </div>
                  </div>

                  <div class="col-md-3 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="codigo_postal" name="codigo_postal" CssClass="form-control form-control-lg form-control-a" placeholder="Codigo Postal" data-msg="Introduce un codigo postal valido" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="col-md-4 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="direccion" name="direccion" CssClass="form-control form-control-lg form-control-a" placeholder="Direccion" data-msg="Introduce una direccion valida" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="col-md-4 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="precio" name="precio" CssClass="form-control form-control-lg form-control-a" placeholder="Precio" data-msg="Introduce un precio valido" runat="server"></asp:TextBox>
                    </div>
                  </div>
                    
                  <div class="col-md-4 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="fianza" name="fianza" CssClass="form-control form-control-lg form-control-a" placeholder="Fianza" Visible="false" runat="server"></asp:TextBox>
                    </div>
                  </div>
                  
                  <div class="col-md-12 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="descripcion" name="descripcion" CssClass="form-control" data-rule="required" placeholder="Descripción" data-msg="Please write something for us" TextMode="MultiLine" Height="200" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <%--<div class="col-md-12 mb-3">
                    <asp:Button ID="subir_foto" CssClass="btn" Text="Subir foto" runat="server"></asp:Button>
                  </div>--%>

                  <div>  
                     <asp:FileUpload ID="SubirImagenes" runat="server"  AllowMultiple="true" />  
                        <%--<asp:Button ID="SubirFile" runat="server"  Text="Subir" /> --%> 
                     <asp:Label ID="listaArchivosSubidos" runat="server" />  
                  </div>  
    
                  <div class="col-md-12 mb-0">
                    <div class="icon-box section-b2">
                      <div class="icon-box-icon">
                        <span class="ion-ios-paper-plane"></span>
                      </div>
                      <div class="icon-box-content table-cell">
                        <div class="icon-box-title">
                          <h4 class="icon-title">Contact Info</h4>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div class="col-md-6 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="contacto_email" name="contacto_email" type="email" CssClass="form-control form-control-lg form-control-a" placeholder="Email de Contacto"  data-msg="Introduce una direccion email correcta" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="col-md-6 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="contacto_nombre" name="contacto_nombre" CssClass="form-control form-control-lg form-control-a" placeholder="Nombre" data-msg="Introduce un valor de nombre correcto" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="col-md-6 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="contacto_apellido" name="contacto_apellido" CssClass="form-control form-control-lg form-control-a" placeholder="Apellido" data-msg="Introduce un valor de apellido correcto" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="col-md-6 mb-3">
                    <div class="form-group">
                      <asp:TextBox ID="contacto_telefono" name="contacto_telefono" CssClass="form-control form-control-lg form-control-a" placeholder="Telefono" data-msg="Introduce un valor de metros correcto" runat="server"></asp:TextBox>
                    </div>
                  </div>

                  <div class="col-md-6 mb-3">
                    <asp:Button ID="publicar" CssClass="btn btn-a" OnClick="publicar_Click" text="Publicar" AutoPostBack="true" runat="server"></asp:Button>
                  </div>

                    <asp:Label ID="mensaje" runat="server"></asp:Label>
                </div>         
          </div>
        </div>
      </div>
    </div>
  </section>
    
    </body>
</asp:Content>