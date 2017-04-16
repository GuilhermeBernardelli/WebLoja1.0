<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebLoja1._0.View.index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CSS/StyleSheet.css" rel="stylesheet" /> 
    <style type="text/css">
    .auto-style5 {
        background-color: lightyellow;
        margin-left: 0px;
        min-width: 300px;
        height: 90%;
        max-height: 268px;

    }
    
        
    </style>
     <img class="image" src="../Image/LgAlemão.png" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <asp:Panel ID="pnlPrincipal" runat="server" CssClass="auto-style5" Width="50%" HorizontalAlign="Center" Height="80%">
        <br />
        &nbsp;<asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Height="10%" Width="90%" Font-Size="X-Large"></asp:Label>
        <br />
        <br />
        <br />
        &nbsp;<asp:Label ID="lblLogin" runat="server" Height="10%" Text="Login :"></asp:Label>
&nbsp;
        <asp:TextBox ID="txtLogin" runat="server" Height="10%" Width="50%"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblSenha" runat="server" Height="10%" Text="Senha :"></asp:Label>
        &nbsp;
        <asp:TextBox ID="txtSenha" runat="server" Width="50%" CausesValidation="True" TextMode="Password" ValidateRequestMode="Enabled" Height="10%"></asp:TextBox>
        <br />
        <asp:Label ID="lblAlerta" runat="server" ForeColor="Red" Visible="False" Height="10%"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Entrar" class="button" width="30%" height="10%" Enabled="true" OnClick="btnLogin_Click"/>
       
        &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnCadastrar" width="30%" height="10%" runat="server" Text="Cadastrar" class="button" Enabled="False" OnClick="btnCadastrar_Click"/>
        <br />
        <asp:Label ID="lblEspaco" runat="server" Visible="False"></asp:Label>
        <br />
        </asp:Panel>
        
        </div>
    
</asp:Content>

