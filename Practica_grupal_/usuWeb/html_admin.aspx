<%@ Page Title="Página Administrador" Language="C#" MaintainScrollPositionOnPostBack="true" MasterPageFile="~/Usuario.Master" AutoEventWireup="true" CodeBehind="html_admin.aspx.cs" Inherits="usuWeb.html_verUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section style="padding-top:100px;">  
        
        <asp:Button ID="btnVerUsuarios" runat="server" Text="Ver Usuarios" OnClick="mostrarUsuarios_click"  />
         <asp:Button ID="btnVerPropiedades" runat="server" Text="Ver Propiedades" OnClick="mostrarPropiedades_click" />

        <asp:GridView EnableViewState="False" ID="GridView1" runat="server"  class="table table-striped table-bordered" position="auto" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
            <Columns>

               <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />

            </Columns>

        </asp:GridView>

        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" OnClick="guardarCambios_click"/>


		</section>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<script defer  src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
<script defer  src="https://cdn.datatables.net/buttons/1.2.2/js/dataTables.buttons.min.js"></script>
<script defer  src="https://cdn.datatables.net/1.10.12/js/dataTables.bootstrap.min.js"></script>
<script defer  src="https://cdn.datatables.net/buttons/1.2.2/js/buttons.bootstrap.min.js"></script>
      <script id="rendered-js" type="module">
          $(document).ready(function () {
              document.title = 'Usuarios';
              $(".table-striped").DataTable(
                  {
                      "dom": '<"dt-buttons"Bf><"clear">lirtp',
                      "paging": true,
                      "autoWidth": true,
                      "buttons": []
                  });
          });
      </script>

    <script>
        function cargarEstadisticas(mañana, tarde, noche) {
            document.querySelector('#mañanaE').textContent = mañana + " %";
            document.querySelector('#tardeE').textContent = tarde + " %";
            document.querySelector('#nocheE').textContent = noche + " %";
            document.querySelector('#mañanaDIV').style.height = mañana + "%";
            document.querySelector('#tardeDIV').style.height = tarde + "%";
            document.querySelector('#nocheDIV').style.height = noche + "%";
        }
    </script>

    <div class="board">
        <div class="titulo_grafica">
            <h3 class="t_grafica">Usuario por Horario</h3>
        </div>
        <div class="sub_board">
            <div class="sep_board"></div>
            <div class="cont_board">
                <div class="graf_board">
                    <div class="barra">
                        <div class="sub_barra b1" id="mañanaDIV">
                            <div id="mañanaE" class="tag_g"></div>
                            <div class="tag_leyenda">Mañana</div>
                        </div>
                    </div>
                    <div class="barra">
                        <div class="sub_barra b2 " id="tardeDIV">
                            <div id="tardeE" class="tag_g"></div>
                            <div class="tag_leyenda">Tarde</div>
                        </div>
                    </div>
                    <div class="barra">
                        <div class="sub_barra b3 " id="nocheDIV">
                            <div id="nocheE" class="tag_g"></div>
                            <div class="tag_leyenda">Noche</div>
                        </div>
                    </div>
                </div>
                <div class="tag_board">
                    <div class="sub_tag_board">
                        <div>100</div>
                        <div>90</div>
                        <div>80</div>
                        <div>70</div>
                        <div>60</div>
                        <div>50</div>
                        <div>40</div>
                        <div>30</div>
                        <div>20</div>
                        <div>10</div>
                    </div>
                </div>
           </div> 
            <div class="sep_board"></div>
       </div>
    </div>

</asp:Content> 