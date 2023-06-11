<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="html_registrarse.aspx.cs" Inherits="usuWeb.html_registrarse1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <!--Título de la página -->
    <title>Sanvi Houses</title>
        
    <!--Link del archivo css -->
    <link rel= "Stylesheet" type="text/css" href="css/iniciarSesion.css" />

    <!--Google Fonts -->
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@100&display=swap" rel="stylesheet" />

    <style>
        /* If you like this, be sure to ❤️ it. */
.wrapper {
  height: 100vh;
  /* This part is important for centering the content */
  display: flex;
  align-items: center;
  justify-content: center;
  /* End center */
  background: -webkit-linear-gradient(to right, #834d9b, #d04ed6);
  background: linear-gradient(to right, #834d9b, #d04ed6);
}

.wrapper a {
  display: inline-block;
  text-decoration: none;
  padding: 15px;
  background-color: #fff;
  border-radius: 3px;
  text-transform: uppercase;
  color: #585858;
  font-family: 'Roboto', sans-serif;
}

.modal {
  visibility: hidden;
  opacity: 0;
  position: absolute;
  top: 0;
  right: 0;
  bottom: 0;
  left: 0;
  display: flex;
  align-items: center;
  justify-content: center;
  background: rgba(77, 77, 77, .7);
  transition: all .4s;
}

.modal:target {
  visibility: visible;
  opacity: 1;
}

.modal__content {
  border-radius: 4px;
  position: relative;
  width: 500px;
  max-width: 90%;
  background: #fff;
  padding: 1em 2em;
  z-index:999;
}

.modal__footer {
  text-align: right;
  a {
    color: #585858;
  }
  i {
    color: #d02d2c;
  }
}
.modal__close {
  position: absolute;
  top: 10px;
  right: 10px;
  color: #585858;
  text-decoration: none;
}
    </style>
</head>

<body>
                    <form id="form1" runat="server" style="height: 100%;">
    <div class='container' style="height:100%">
      <div class='window' style="height:760px">
        <div class='overlay' style="height:760px"></div>
        <div class='content'>
          <div class='welcome'>Registrarse</div>

          <div class='input-fields'>
            <asp:TextBox ID ="NombreTextBox" TextMode="SingleLine" runat="server" placeholder="Nombre" CssClass="input-line full-width"></asp:TextBox>
            <asp:TextBox ID ="ApellidoTextBox" TextMode="SingleLine" runat="server" placeholder="Apellido" CssClass="input-line full-width"></asp:TextBox>
            <asp:TextBox ID ="DNITextBox" TextMode="SingleLine" runat="server" placeholder="DNI/NIE/NIF" CssClass="input-line full-width"></asp:TextBox>
            <asp:TextBox ID ="TelefonoTextBox" TextMode="SingleLine" runat="server" placeholder="Telefono" CssClass="input-line full-width"></asp:TextBox>
            <asp:TextBox ID ="EmailTextBox" TextMode="Email" runat="server" placeholder="Email" CssClass="input-line full-width"></asp:TextBox>
            <asp:TextBox ID ="PasswordTextBox" TextMode="Password" runat="server" placeholder="Contraseña" CssClass="input-line full-width"></asp:TextBox>
            <strong >NOTA: La contraseña debe tener almenos: 1 mayúscula, 1 minúscula, 1 valor numérico y 1 carácter especial.</strong>

          </div>
   
            <div class='spacing'>o <span class='highlight'><a href="/html_IniciarSesion.aspx" >inicializar sesión</a></span></div>

          <div><asp:Button ID="RegistrarseButton" CssClass ="ghost-round full-width" runat="server" Text="Registrarse" OnClick="EventoRegistro" /></div>
        </div>
      </div>
    </div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript" language="javascript">
        function Func(message) {
            window.location = '#demo-modal'
            document.querySelector('#messageError').textContent = message;
        }
    </script>

    <div id="demo-modal" class="modal">
    <div class="modal__content">
        <h1>Error</h1>

        <p id="messageError">
        </p>

        <a style ="color:black" href ="#" class="modal__close">&times;</a>
    </div>
</div>




</form>



</body>
</html>