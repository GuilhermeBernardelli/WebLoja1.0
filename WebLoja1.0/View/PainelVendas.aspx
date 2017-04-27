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
        <asp:Panel ID="pnlCliente" runat="server" Height="100px" Width="1178px" GroupingText="Dados do Cliente" Visible="False" style="margin-left:22px;" Font-Bold="True" Font-Names="Arial" HorizontalAlign="Left">

            
            <br />

            
            <br />
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
                <asp:Button ID="btnVender" runat="server" class="button" Enabled="False" Height="42px" OnClick="btnVender_Click" style="margin-top:17px; margin-bottom:12px;" Text="Concluir Venda" Width="90%" />
                <br />
                <asp:Button ID="btnCorrigir" runat="server" class="button" Enabled="False" Height="42px" OnClick="btnCorrigir_Click" Text="Remover Produtos" Width="90%" style="margin-bottom:12px;"/>
                <asp:Button ID="btnRemover" runat="server" class="button" Height="42px" OnClick="btnRemover_Click" Text="Remover Selecionados" Visible="False" Width="90%" style="margin-bottom:12px;"/>
                <br />
                <asp:Button ID="btnCliente" runat="server" class="button" Height="42px" OnClick="btnCliente_Click" Text="Associar Cliente" Width="90%" style="margin-bottom:12px;" CausesValidation="False" TabIndex="5"/>
                <br />
                <asp:Button ID="btnDesconto" runat="server" class="button" Height="42px" Text="Ceder Desconto" Width="90%" style="margin-bottom:12px;"/>
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
                <asp:TextBox ID="txtTotalVista" runat="server" BackColor="#ffff99" Enabled="False" Font-Bold="True" Font-Size="22pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#000066" Height="35px" style="text-align:center; vertical-align:middle;" Width="95%">0,00</asp:TextBox>
                <br />
                <br />
                &nbsp;<asp:Label ID="lblTotalFinal" runat="server" Font-Bold="True" Font-Size="Large" Height="25px" Text="Total  Final : " Width="119px"></asp:Label>
                <asp:TextBox ID="txtTotalFinal" runat="server" BackColor="#ffff99" Enabled="False" Font-Bold="True" Font-Size="22pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#CC0000" Height="35px" style="text-align:center; vertical-align:middle;" Width="95%">0,00</asp:TextBox>
            </asp:Panel>
            
        </asp:Panel>
        <asp:Panel ID="pnlAdjacente" runat="server" Height="136px" Width="1200px">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" Text="Código Produto : "></asp:Label>
            <asp:TextBox ID="txtCodigo" runat="server" Width="427px" CssClass="auto-style6" Height="23px" BackColor="#FFFF99" Font-Size="Large"></asp:TextBox>
            <%-- &nbsp;<asp:Label ID="lblButton" runat="server" BackColor="Silver" BorderColor="Silver" BorderStyle="Outset" Font-Bold="True" Height="18px" Text="OK" Width="25px"></asp:Label>--%>
            
            &nbsp;&nbsp;<asp:Label ID="lblQuantidade" runat="server" Font-Bold="True" Text="Quantidade :"></asp:Label>
            &nbsp;<asp:TextBox ID="txtQuantidade" runat="server" Height="25px" Width="66px" BackColor="#FFFF99" Font-Size="Large"></asp:TextBox>
            &nbsp;<asp:Button ID="btnCodigo" runat="server" BackColor="Silver" BorderStyle="Outset" Font-Bold="True" Height="25px" OnClick="btnCodigo_Click" Text="OK" Width="35px" TabIndex="100" ViewStateMode="Enabled" ValidateRequestMode="Enabled" />
            &nbsp;<asp:Button ID="btnCancel" runat="server" BackColor="Silver" BorderStyle="Outset" Font-Bold="True" Height="25px" OnClick="btnCancel_Click" Text="X" Width="35px" />
            <br />
            &nbsp;<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPesquisa" runat="server" Font-Bold="True" Text="Pesquisar Produtos : "></asp:Label>
            <asp:TextBox ID="txtPesquisaProd" runat="server" Height="20px" Width="404px" BackColor="#FFFF99"></asp:TextBox>
            <asp:DropDownList ID="ddlPesquisaProd" runat="server" Height="26px" Visible="False" Width="409px" AutoPostBack="True" OnSelectedIndexChanged="ddlPesquisaProd_SelectedIndexChanged" BackColor="#FFFF99">
            </asp:DropDownList>
            &nbsp;<asp:ImageButton ID="btnPesquisa" runat="server" Height="22px" ImageAlign="Middle" Width="22px" img src="../Image/lupa.png" BackColor="Silver" BorderColor="Silver" BorderStyle="Outset" CssClass="auto-style6" OnClick="btnPesquisa_Click"/>
        </asp:Panel>
        </asp:Panel>
        </div>
    
</asp:Content>
