<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="Inicial.aspx.cs" Inherits="WebLoja1._0.View.Inicial" %>
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
        .auto-style5 {}
    </style>
    <style type="text/css">
        .auto-style5 {
            height: 205px;
        }
        .auto-style6 {
            height: 223px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center" class="auto-style6" style="height: 278px">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="101%" CssClass="auto-style5" Width="550px" HorizontalAlign="Center">
        <asp:Button ID="btnCancelCliente" runat="server" BackColor="#CC0000" BorderColor="White" Font-Bold="True" ForeColor="White" Height="21px" OnClick="btnSair_Click"  style="float:right; margin-bottom:15px;" Text="X" Width="27px" />
        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large" Width="100%"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="lblInfo" runat="server" Text="Usuario Logado : "></asp:Label>
&nbsp;<asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnVendas" runat="server" class="button" OnClick="btnVendas_Click" Text="Vendas" Height="27px"/>
        &nbsp;
        <asp:Button ID="btnProdutos" runat="server" class="button" OnClick="lblProdutos_Click" Text="Produtos" Height="27px"/>
        &nbsp;
        <asp:Button ID="btnClientes" runat="server" class="button" OnClick="btnClientes_Click" Text="Clientes" Height="27px"/>
        &nbsp;
        <asp:Button ID="btnFornecedores" runat="server" class="button" OnClick="btnFornecedores_Click" Text="Fornecedor" Height="27px"/>
        <br />
        <asp:Button ID="btnContabilidade" runat="server" class="button" OnClick="btnContabilidade_Click" Text="Contabil." style="margin-top:8px;" Height="27px"/>
        &nbsp;&nbsp;<asp:Button ID="btnFolhaPg" runat="server" class="button" Height="27px" OnClick="btnFolhaPg_Click" style="margin-top:8px;" Text="Folha Pag." />
        &nbsp;&nbsp;<asp:Button ID="btnUsuarios" runat="server" class="button" Height="27px" OnClick="btnUsuario_Click" style="margin-top:8px;" Text="Usuários" />
&nbsp;&nbsp;<asp:Button ID="btnGestao" runat="server" class="button" Height="27px" OnClick="btnGestao_Click" style="margin-top:8px;" Text="Gestão Sist." />
        <br />
        <asp:Button ID="btnRelatorios" runat="server" class="button" Height="27px" style="margin-top:8px;" Text="Relatórios" OnClick="btnRelatorios_Click" />
        &nbsp;&nbsp;<asp:Button ID="btn3" runat="server" class="button" Height="27px" style="margin-top:8px;" Text="Reserva" Visible="False" />
&nbsp;
        <asp:Button ID="btn4" runat="server" class="button" Height="27px" style="margin-top:8px;" Text="Reserva" Visible="False" />
        &nbsp;
        <asp:Button ID="btn5" runat="server" class="button" Height="27px" style="margin-top:8px;" Text="Reserva" Visible="False" />
        </asp:Panel>
        </div>
    
</asp:Content> 