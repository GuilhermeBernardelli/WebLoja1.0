<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="AcessoIndevido.aspx.cs" Inherits="WebLoja1._0.View.AcessoIndevido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
    .auto-style5 {
        background-color: lightyellow;
        margin-left: 0px;
    }
        
    </style>

    <link href="../CSS/StyleSheet.css" rel="stylesheet" />   
    <img class="image" src="../Image/LgAlemão.png" /> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="80%" CssClass="auto-style5" Width="60%" HorizontalAlign="Center">
        <br />
        &nbsp;<asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <br />
        <br />        
        <br />
        <asp:Label ID="lblAlerta" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        &nbsp;</asp:Panel>
        </div>
    
</asp:Content> 
