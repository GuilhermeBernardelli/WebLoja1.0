<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelVendas.aspx.cs" Inherits="WebLoja1._0.View.PainelVendas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    .auto-style5 {
        background-color: lightyellow;
        margin-left: 0px;
    }
        
    </style>

    <link href="../CSS/StyleSheet.css" rel="stylesheet" />   
    <img class="image" src="../Image/LgAlemão.png" /> 
    <style type="text/css">
        .auto-style5 {
            width: 395px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-top: 8px;
        }
        .auto-style6 {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="90%" CssClass="auto-style5" Width="90%" HorizontalAlign="Left" style="overflow-x:scroll;">
        &nbsp;&nbsp;<asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
        &nbsp;&nbsp;
        <br />
        <asp:Panel ID="pnlCliente" runat="server" Height="191px" Width="1178px" GroupingText="Dados do Cliente" Visible="False" style="margin-left:22px;" Font-Bold="True" Font-Names="Arial" HorizontalAlign="Left">            
           
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPesquisaCliente" runat="server" Font-Bold="True" Text="Pesquisar Clientes : " Font-Size="Small"></asp:Label>
            <asp:TextBox ID="txtPesquisaCliente" runat="server" Height="14px" Width="354px" BackColor="#FFFF99"  style="margin-top:8px;"></asp:TextBox>
            <asp:DropDownList ID="ddlClientes" runat="server" Height="20px" Visible="False" Width="359px" AutoPostBack="True" BackColor="#FFFF99" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" style="margin-top:8px;">
            </asp:DropDownList>
            &nbsp;<asp:ImageButton ID="btnPesquisaClientes" runat="server" Height="16px" ImageAlign="Middle" Width="16px" img src="../Image/lupa.png" BackColor="Silver" BorderColor="Silver" BorderStyle="Outset" CssClass="auto-style6" OnClick="btnPesquisaClientes_Click" />
             <asp:Label ID="lblEspaçoButton" runat="server" Visible="False" Width="17px"></asp:Label>
            <asp:Label ID="lblEspaço" runat="server" Width="569px"></asp:Label>
            <asp:Button ID="btnCancelCliente" runat="server" BackColor="Silver" BorderStyle="Outset" Font-Bold="True" Height="21px" Text="X" Width="27px" OnClick="btnCancelCliente_Click" />
            <br />
            <br />
            &nbsp;&nbsp;&nbsp; <asp:Label ID="lblCliente" runat="server" Font-Bold="True" Text="Cliente : " Font-Size="Small"></asp:Label>
            <asp:TextBox ID="txtCliente" runat="server" BackColor="#FFFF99" Height="14px" Width="338px" Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lblCpf" runat="server" Font-Bold="True" Font-Size="Small" Text="CPF/CNPJ : "></asp:Label>
            <asp:TextBox ID="txtCpf" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="137px"></asp:TextBox>
&nbsp;
            <asp:Label ID="lblRg" runat="server" Font-Bold="True" Font-Size="Small" Text="RG : "></asp:Label>
            <asp:TextBox ID="txtRg" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="117px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblCreditos" runat="server" Font-Bold="True" Font-Size="Medium" Text="Créditos : "></asp:Label>
            <asp:TextBox ID="txtCreditos" runat="server" BackColor="#FFFF99" Enabled="False" Font-Bold="True" Font-Size="Large" Height="18px" Width="87px"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTelefone" runat="server" Font-Bold="True" Font-Size="Small" Text="Telefone : "></asp:Label>
            <asp:TextBox ID="txtTelefone" runat="server" BackColor="#FFFF99" Height="14px" Width="126px" Enabled="False"></asp:TextBox>
            &nbsp;&nbsp;<asp:Label ID="lblCelular" runat="server" Font-Bold="True" Font-Size="Small" Text="Celular : "></asp:Label>
            <asp:TextBox ID="txtCelular" runat="server" BackColor="#FFFF99" Height="14px" Width="126px" Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lblContato" runat="server" Font-Bold="True" Font-Size="Small" Text="Responsável : "></asp:Label>
            <asp:TextBox ID="txtContato" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="211px"></asp:TextBox>
            <br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEndereco" runat="server" Font-Bold="True" Font-Size="Small" Text="End : "></asp:Label>
            <asp:TextBox ID="txtEndereco" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="300px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Label ID="lblNumero" runat="server" Font-Bold="True" Font-Size="Small" Text="Nº : "></asp:Label>
            <asp:TextBox ID="txtNumero" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="37px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Label ID="lblBairro" runat="server" Font-Bold="True" Font-Size="Small" Text="Bairro : "></asp:Label>
            <asp:TextBox ID="txtBairro" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="226px"></asp:TextBox>
            &nbsp;&nbsp;<asp:Label ID="lblCidade" runat="server" Font-Bold="True" Font-Size="Small" Text="Cidade : "></asp:Label>
            <asp:TextBox ID="txtCidade" runat="server" BackColor="#FFFF99" Enabled="False" Height="14px" Width="256px"></asp:TextBox>
            <br />
            <br />

        </asp:Panel>
        <asp:Panel ID="pnlBasico" runat="server" Height="300px" Width="1200px" >
            <asp:Panel ID="pnlBorda" runat="server" Height="100%" HorizontalAlign="Center" Style="float: left;" Width="70%">
                <asp:ListBox ID="dlProdVenda" runat="server" Height="260px" Width="600px" BackColor="#ffff99" Font-Names="Arial" Font-Size="16pt" style="margin-top:2%; margin-left:2%;">
                </asp:ListBox>
                <asp:ListBox ID="dlProdQnt" runat="server" Height="260px" Width="100px" BackColor="#ffff99" Font-Names="Arial" Font-Size="16pt" style="margin-top:2%; text-align:center;">
                </asp:ListBox>
                <asp:ListBox ID="dlProdValor" runat="server" Height="260px" Width="100px" BackColor="#ffff99" Font-Names="Arial" Font-Size="16pt" style="margin-top:2%; text-align:right;">
                </asp:ListBox>
                <asp:Panel ID="pnlRelacaoProdutos" runat="server" BackColor="#ffff99" Height="260px" Width="800px" HorizontalAlign="Left" BorderWidth="1px" style="margin-top:2%; margin-left:3%; overflow-y:scroll;" BorderStyle="Inset" Visible="False" >                    
                    <asp:CheckBoxList ID="chkProdVenda" runat="server" Visible="False" Font-Names="Arial" Font-Size="16pt" AutoPostBack="True" >
                    </asp:CheckBoxList>
                </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlButtons" runat="server" Height="100%" HorizontalAlign="Center" style="float:left;" Width="15%">
                <asp:Button ID="btnVender" runat="server" class="button" Enabled="False" Height="42px" OnClick="btnVender_Click" style="margin-top:16px; margin-bottom:12px;" Text="Concluir Venda" Width="90%" />
                <br />
                <asp:Button ID="btnCorrigir" runat="server" class="button" Enabled="False" Height="42px" OnClick="btnCorrigir_Click" Text="Remover Produtos" Width="90%" style="margin-bottom:12px;"/>
                <asp:Button ID="btnRemover" runat="server" class="button" Height="42px" OnClick="btnRemover_Click" Text="Remover Selecionados" Visible="False" Width="90%" style="margin-bottom:12px;"/>
                <br />
                <asp:Button ID="btnCliente" runat="server" class="button" Height="42px" OnClick="btnCliente_Click" Text="Associar Cliente" Width="90%" style="margin-bottom:12px;" CausesValidation="False" TabIndex="5"/>
                <br />
                <asp:Button ID="btnDesconto" runat="server" class="button" Height="42px" Text="Ceder Desconto" Width="90%" style="margin-bottom:12px;" OnClick="btnDesconto_Click"/>
                <br />
                <asp:Button ID="btnVoltar" runat="server" class="button" Height="42px" OnClick="btnCancelar_Click" Text="Sair do Sistema" Width="90%" />
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlDisplay" runat="server" Height="100%" HorizontalAlign="Left" style="float:right;" Width="13%">
                <br />
                &nbsp;<asp:Label ID="lblSubTotal" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Sub - Total : " Width="119px"></asp:Label>
                <br />
                <asp:TextBox ID="txtSubTotal" runat="server" BackColor="#ffff99" Enabled="False" Font-Bold="True" Font-Size="22pt" ForeColor="#000066" Height="35px" style="text-align:center; vertical-align:middle;" Width="95%">0,00</asp:TextBox>
                <br />
                <br />
                &nbsp;<asp:Label ID="lblTotalVista" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Total à Vista : " Width="119px"></asp:Label>
                <asp:TextBox ID="txtTotalVista" runat="server" BackColor="#ffff99" Enabled="False" Font-Bold="True" Font-Size="22pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" Height="35px" style="text-align:center; vertical-align:middle;" Width="95%">0,00</asp:TextBox>
                <br />
                <br />
                &nbsp;</asp:Panel>            
        </asp:Panel>
        <asp:Panel ID="pnlAdjacente" runat="server" Height="95px" Width="1200px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" Text="Código Produto : "></asp:Label>
            <asp:TextBox ID="txtCodigo" runat="server" Width="427px" CssClass="auto-style6" Height="23px" BackColor="#FFFF99" Font-Size="Large" AutoPostBack="True" OnTextChanged="txtCodigo_TextChanged"></asp:TextBox>
            <%-- &nbsp;<asp:Label ID="lblButton" runat="server" BackColor="Silver" BorderColor="Silver" BorderStyle="Outset" Font-Bold="True" Height="18px" Text="OK" Width="25px"></asp:Label>--%>
            
            &nbsp;&nbsp;<asp:Label ID="lblQuantidade" runat="server" Font-Bold="True" Text="Quantidade :"></asp:Label>
            &nbsp;<asp:TextBox ID="txtQuantidade" runat="server" Height="25px" Width="66px" BackColor="#FFFF99" Font-Size="Large"></asp:TextBox>
            &nbsp;<asp:Button ID="btnCodigo" runat="server" BackColor="Silver" BorderStyle="Outset" Font-Bold="True" Height="25px" OnClick="btnCodigo_Click" Text="OK" Width="35px" TabIndex="100" ViewStateMode="Enabled" ValidateRequestMode="Enabled" />
            
            <br />
            &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPesquisa" runat="server" Font-Bold="True" Text="Pesquisar Produtos : "></asp:Label>
            <asp:TextBox ID="txtPesquisaProd" runat="server" Height="20px" Width="404px" BackColor="#FFFF99"></asp:TextBox>
            <asp:DropDownList ID="ddlPesquisaProd" runat="server" Height="26px" Visible="False" Width="409px" AutoPostBack="True" OnSelectedIndexChanged="ddlPesquisaProd_SelectedIndexChanged" BackColor="#FFFF99">
            </asp:DropDownList>
            &nbsp;<asp:ImageButton ID="btnPesquisa" runat="server" Height="22px" ImageAlign="Middle" Width="22px" img src="../Image/lupa.png" BackColor="Silver" BorderColor="Silver" BorderStyle="Outset" CssClass="auto-style6" OnClick="btnPesquisa_Click"/>
            <br />
            <asp:Label ID="lblUser" runat="server" Font-Size="Small" Font-Bold="True" style="float:right;" Text="Anônimo" Width="180px"/>   
            <asp:Label ID="lblOperador" runat="server" Font-Size="Small" style="float:right;" Text="Operador :" Width="65px" />         
        </asp:Panel>
        <div align="center">
        <asp:Panel ID="pnlDescontos" runat="server" Height="184px" Width="400px" HorizontalAlign="Center" Visible="False">
            &nbsp;&nbsp;<asp:Label ID="lblPanelDescontos" runat="server" Font-Bold="True" Font-Size="X-Large" ></asp:Label>
            <br />
            <br />
            &nbsp;&nbsp;<asp:Label ID="lblDesconto" runat="server" Font-Bold="True" Text="Conceder Desconto (%) : "></asp:Label>
            &nbsp;<asp:TextBox ID="txtDesconto" runat="server" Height="25px" Width="81px" BackColor="#FFFF99" Font-Size="Large"></asp:TextBox>            
            <br /><asp:Label ID="lblAviso1" runat="server" Font-Bold="True" Text="*Os descontos somente são aplicados para as compras pagas à vista" Font-Size="X-Small" ForeColor="Red"></asp:Label>
            <br /><asp:Label ID="lblAviso2" runat="server" Font-Bold="True" Text="**A inclusão de um desconto manual substitui os descontos automáticos" Font-Size="X-Small" ForeColor="Red"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnAceitaDesc" runat="server" class="button" Height="32px" Text="OK" Width="100px" CausesValidation="False" TabIndex="5" OnClick="btnAceitaDesc_Click"/>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelDesc" runat="server" class="button" Height="32px" Text="Cancelar" Width="100px" CausesValidation="False" TabIndex="5" OnClick="btnCancelDesc_Click"/>
        </asp:Panel>
        </div>
         <asp:Panel ID="pnlPagamento" runat="server" Height="505px" Width="1200px" Visible="False">
            
            <asp:Panel ID="pnlFormaPag" runat="server" GroupingText="Selecione a Forma de Pagamento" Width="1150px" style="margin-left:25px; margin-top:12px;" Font-Bold="True" Font-Size="Large" Height="420px" >
                <asp:Panel ID="pnlVista" runat="server" Height="390px" Width="33%" BackColor="LightGray" HorizontalAlign="Center" style="float:left" BorderStyle="Solid" BorderWidth="1px">
                    <br />
                    <asp:RadioButton ID="rdbVista" runat="server" Text="Pagamento à Vista" Font-Size="Large" GroupName="grpPagamento" OnCheckedChanged="rdbVista_CheckedChanged" AutoPostBack="True" />
                    <br />
                    <br />
                    <asp:Label ID="lblTotalFinalVista" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Total  Final : " Width="159px"></asp:Label>
                    <asp:Label ID="lblValorVista" runat="server" Font-Size="XX-Large" ForeColor="Red" Text="0,00" Width="140px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblFormaVista" runat="server" Text="Forma de Pagamento"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlFormaVista" runat="server" AutoPostBack="True" Font-Size="X-Large" Height="34px" Width="232px" OnSelectedIndexChanged="ddlFormaVista_SelectedIndexChanged" Enabled="False">
                        <asp:ListItem>Selecione a Forma</asp:ListItem>
                        <asp:ListItem Value="dinheiro">Dinheiro</asp:ListItem>
                        <asp:ListItem Value="cartao">Cartão</asp:ListItem>
                        <asp:ListItem Value="creditos">Créditos</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblRecebido" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Valor Recebido  : " Visible="False" Width="158px"></asp:Label>
                    <asp:TextBox ID="txtRecebido" runat="server" Enabled="False" Height="29px" Visible="False" Width="107px" Font-Bold="True" Font-Size="X-Large"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnReceber" runat="server" Text="OK" Enabled="False" Font-Bold="True" Font-Size="Large" OnClick="btnReceber_Click" Visible="False" />
                    <br />
                    <br />
                    <asp:Label ID="lblTroco" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Troco  : " Visible="False" Width="158px"></asp:Label>
                    <asp:Label ID="lblValorTroco" runat="server" Font-Bold="True" Font-Size="XX-Large" Text="0,00" Visible="False" Width="140px"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="pnlPrazo" runat="server" Height="390px" Width="33%" BackColor="LightGray" HorizontalAlign="Center" style="float:left" BorderStyle="Solid" BorderWidth="1px">
                    <br />
                    <asp:RadioButton ID="rdbPrazo" runat="server" Text="Pagamento à Prazo" Font-Size="Large" GroupName="grpPagamento" OnCheckedChanged="rdbPrazo_CheckedChanged" AutoPostBack="True" />
                    <br />
                    <br />
                    <asp:Label ID="lblTotalFinalPrazo" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Total  Final : " Width="159px"></asp:Label>
                    <asp:Label ID="lblValorPrazo" runat="server" Font-Size="XX-Large" ForeColor="Red" Text="0,00" Width="140px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblParcela" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Valor Parcelas : " Width="158px"></asp:Label>
                    <asp:Label ID="lblValorParcela" runat="server" Font-Size="XX-Large" Text="0,00" Width="140px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Nº de Parcelas"></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlParcelas" runat="server" AutoPostBack="True" Font-Bold="True" Font-Size="X-Large" Height="34px" Width="74px" Enabled="False" OnSelectedIndexChanged="ddlParcelas_SelectedIndexChanged">                        
                        <asp:ListItem Selected="True" Value="2">2 x</asp:ListItem>
                        <asp:ListItem Value="3">3 x</asp:ListItem>
                        <asp:ListItem Value="4">4 x</asp:ListItem>
                        <asp:ListItem Value="5">5 x</asp:ListItem>
                        <asp:ListItem Value="6">6 x</asp:ListItem>
                        <asp:ListItem Value="7">7 x</asp:ListItem>
                        <asp:ListItem Value="8">8 x</asp:ListItem>
                        <asp:ListItem Value="9">9 x</asp:ListItem>
                        <asp:ListItem Value="10">10 x</asp:ListItem>
                        <asp:ListItem Value="11">11 x</asp:ListItem>
                        <asp:ListItem Value="12">12 x</asp:ListItem>
                    </asp:DropDownList>
                </asp:Panel>
                <asp:Panel ID="pnlCheque" runat="server" Height="390px" Width="33%" BackColor="LightGray" HorizontalAlign="Center" BorderStyle="Solid" BorderWidth="1px" style="float:left">
                    <br />
                    <asp:RadioButton ID="rdbCheque" runat="server" Text="Pagamento com Cheques" Font-Size="Large" GroupName="grpPagamento" OnCheckedChanged="rdbCheque_CheckedChanged" AutoPostBack="True" />
                    <br />
                    <br />
                    <asp:Label ID="lblTotalFinalCartao" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Total  Final : " Width="159px"></asp:Label>
                    <asp:Label ID="lblValorCheque" runat="server" Font-Size="XX-Large" ForeColor="Red" Text="0,00" Width="140px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblParcelaCheque" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Valor Cheques : " Width="158px"></asp:Label>
                    <asp:Label ID="lblValorParcelaCheque" runat="server" Font-Size="XX-Large" Text="0,00" Width="140px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblQntCheques" runat="server" Text="Quantidade de Cheques : "></asp:Label>
                    <asp:DropDownList ID="ddlQntCheques" runat="server" AutoPostBack="True" Enabled="False" Font-Bold="True" Font-Size="X-Large" Height="34px" Width="56px" OnSelectedIndexChanged="ddlQntCheques_SelectedIndexChanged">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="11">11</asp:ListItem>
                        <asp:ListItem Value="12">12</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="lblNumPrimeiro" runat="server" Text="Nº Primeiro Cheque" Visible="False"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtPrimeiroCheque" runat="server" Visible="False" Width="90%"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lblNumUltimo" runat="server" Text="Nº Último Cheque" Visible="False"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtUltimoCheque" runat="server" Visible="False" Width="90%"></asp:TextBox>
                </asp:Panel>
            </asp:Panel>
            <div align="center">
                <br />
                <asp:Button ID="btnContinuarCompra" runat="server" class="button" Height="45px" Text="Editar Compra" Width="166px" OnClick="btnContinuarCompra_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnAceitarPag" runat="server" class="button" Height="45px" Text="Realizar Pagamento" Width="166px" OnClick="btnAceitarPag_Click" Enabled="False" />
                &nbsp;&nbsp;
                <asp:Button ID="btnNotaPaulista" runat="server" class="button" Height="45px" Text="Nota Paulista" Width="166px" OnClick="btnNotaPaulista_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelaPag" runat="server" class="button" Height="45px" OnClick="btnCancelaPag_Click" Text="Cancelar Compra" Width="166px" />
            </div>
        </asp:Panel>
        <div align="center">
            <asp:Panel ID="pnlCpfCnpj" runat="server" Height="200px" Width="400px" HorizontalAlign="Center" Visible="False">
                &nbsp;&nbsp;<asp:Label ID="lblCpfCnpj" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                <br />
                <br />
                <asp:RadioButton ID="rdbCpf" runat="server" GroupName="NotaPaulista" Text="CPF : " Width="80px" OnCheckedChanged="rdbCpf_CheckedChanged" />
                <asp:TextBox ID="txtNpCpf" runat="server" Height="16px" Width="210px" Font-Size="Large" Enabled="False"></asp:TextBox>
                <br />
                <br />
                <asp:RadioButton ID="rdbCnpj" runat="server" GroupName="NotaPaulista" Text="CNPJ : " Width="80px" OnCheckedChanged="rdbCnpj_CheckedChanged" />
                <asp:TextBox ID="txtNpCnpj" runat="server" Enabled="False" Font-Size="Large" Height="16px" Width="210px"></asp:TextBox>
                <br />
                <asp:Label ID="lblAviso3" runat="server" Font-Bold="True" Font-Size="X-Small" ForeColor="Red" Text="*Após o &quot;OK&quot; o sistema não mais habilita a opção de inclusão"></asp:Label>
                <br />
                <br />
                <asp:Button ID="btnOkNp" runat="server" class="button" Height="32px" Text="OK" Width="100px" CausesValidation="False" TabIndex="5" OnClick="btnOkNp_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancNp" runat="server" class="button" Height="32px" Text="Cancelar" Width="100px" CausesValidation="False" TabIndex="5" OnClick="btnCancNp_Click" />
            </asp:Panel>
        </div>
        </asp:Panel>
        </div>
    
</asp:Content>
