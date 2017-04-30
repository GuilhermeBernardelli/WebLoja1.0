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
    <div align="center" class="auto-style6">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="160%" CssClass="auto-style5" Width="550px" HorizontalAlign="Center">
        <br />
        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
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
        &nbsp;&nbsp;<asp:Button ID="btnUsuarios" runat="server" class="button" OnClick="btnUsuario_Click" Text="Usuários" style="margin-top:8px;" Height="27px"/>
&nbsp;
        <asp:Button ID="btnGestao" runat="server" class="button"  Text="Gestão Sist." OnClick="btnGestao_Click" style="margin-top:8px;" Height="27px"/>
        <br />
        <br />
        </asp:Panel>
        </div>
    
</asp:Content> 