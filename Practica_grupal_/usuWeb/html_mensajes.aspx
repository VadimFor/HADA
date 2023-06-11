<%@ Page Title="Sanvi Houses Mensajes" Language="C#" MasterPageFile="~/Usuario.Master" AutoEventWireup="true" CodeBehind="html_mensajes.aspx.cs" Inherits="usuWeb.html_mensajes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <br />
    <br />
    <br />

    <%--    DROPDOWNLIST PARA ELEGIR CONVERSACIÓN--%>
        <div align="center">
            <asp:DropDownList runat="server" ID="ElegirConversacion" DataSourceID="SqlDataSource1" DataTextField="usu_receptor" DataValueField="usu_receptor" OnSelectedIndexChanged="ElegirConversacion_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                <asp:ListItem runat="server">Elige una conversación...</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:InmoData %>' SelectCommand="SELECT DISTINCT [usu_receptor] 
FROM [Mensaje] 
WHERE ([usu_emisor] = @usu_iniciado) 
union
SELECT DISTINCT [usu_emisor] 
FROM [Mensaje] 
WHERE ([usu_receptor] = @usu_iniciado)">
            <SelectParameters>
                <asp:Parameter Name="usu_iniciado" Type="String"></asp:Parameter>
            </SelectParameters>
        </asp:SqlDataSource>

    <%--MENSAJES--%>

    <center>
    <section id="comment-section" class="comment-section">
    <asp:TextBox type="text" id="EntradaMensaje" CssClass="input-comentario" placeholder="Escribe tu mensaje aquí..." runat="server"></asp:TextBox>
    <asp:Button ID="EnviarMensajeButton" CssClass="publicar-comentario" Text="Enviar mensaje" runat="server" OnClick="EnviarMensaje"></asp:Button>
        <br />
        <asp:Panel runat="server" Visible="false" ID="AvisoPanel">
        <asp:Label runat="server" ID="Aviso" Text="No hay ninguna conversación!"></asp:Label>
            <br />
            <br />
        </asp:Panel>
        <asp:Repeater ID="ListaMensajes" runat="server" DataSourceID="SqlDataSource2">
            <ItemTemplate>
                <%--SI usu_emisor = usuarioId ALIGN RIGHT--%>
                <asp:Placeholder ID="Placeholder1" runat="server" visible='<%#Eval("usu_emisor").ToString()==ViewState["user"].ToString()%>'>
                    <asp:Label ID="Texto" Text='<% #Eval("texto")%>' runat="server" CssClass="price-a negro"></asp:Label>
                    <asp:Label ID="Label2" Text="TÚ" runat="server" CssClass="price-a negro"></asp:Label>
                    <br />
                    <br />
                </asp:Placeholder>
                <%--SI usu_receptor = usuarioId ALIGN LEFT--%>
                <asp:Placeholder runat="server" visible='<%#Eval("usu_receptor").ToString()==ViewState["user"].ToString()%>'>
                    <asp:Label ID="Label3" Text='<% #Eval("usu_emisor") %>' runat="server" CssClass="price-a negro"></asp:Label>
                    <asp:Label ID="Label1" Text='<% #Eval("texto") %>' runat="server" CssClass="price-a negro"></asp:Label>
                    <br />
                    <br />
                </asp:Placeholder>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource runat="server" ID="SqlDataSource2" ConnectionString='<%$ ConnectionStrings:InmoData %>' SelectCommand="SELECT [texto], [usu_emisor], [usu_receptor] FROM [Mensaje] 
            WHERE (([usu_emisor] = @usu_iniciado) OR ([usu_receptor] = @usu_iniciado) ) AND (([usu_emisor] = @usu_elegido) OR ([usu_receptor] = @usu_elegido) )
            ORDER BY [id] ASC">
            <SelectParameters>
                <asp:Parameter Name="usu_iniciado" Type="String"></asp:Parameter>
                <asp:ControlParameter DefaultValue="" Name="usu_elegido" Type="String" ControlID="ElegirConversacion" PropertyName="SelectedValue"/>
            </SelectParameters>
        </asp:SqlDataSource>
    </section>
    </center>

</asp:Content>
