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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center" style="height: 205px">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="160%" CssClass="auto-style5" Width="70%" HorizontalAlign="Center">
        <br />
        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="lblInfo" runat="server" Text="Usuario Logado :"></asp:Label>
&nbsp;<asp:Label ID="lblUser" runat="server"></asp:Label>
        <br />
        <br />
        &nbsp;
        <asp:Button ID="btnVendas" runat="server" class="button" OnClick="btnVendas_Click" Text="Vendas" />
        &nbsp;
        <asp:Button ID="lblProdutos" runat="server" class="button" OnClick="lblProdutos_Click" Text="Produtos" />
        &nbsp;
        <asp:Button ID="btnClientes" runat="server" class="button" OnClick="btnClientes_Click" Text="Clientes" />
        &nbsp;
        <asp:Button ID="btnFornecedores" runat="server" class="button" OnClick="btnFornecedores_Click" Text="Fornecedor" />
&nbsp;
        <asp:Button ID="btnUsuarios" runat="server" class="button" OnClick="btnUsuario_Click" Text="Usuários" />
        <br />
        <br />
        </asp:Panel>
        </div>
    
</asp:Content> 