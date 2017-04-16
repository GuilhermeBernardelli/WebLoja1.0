<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelUsuarios.aspx.cs" Inherits="WebLoja1._0.View.PainelUsuarios" %>
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
    <asp:Panel ID="pnlPrincipal" runat="server" Height="80%" CssClass="auto-style5" Width="60%" style="overflow-x:scroll;">
        <br />
        &nbsp;<asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        
    
        <asp:Panel ID="pnlCadastro" runat="server" Height="215px" Visible="False" Width="553px" CssClass="button" >
            <asp:Label ID="lblNome" runat="server" Text="Nome : "></asp:Label>
            <asp:TextBox ID="txtNome" runat="server" Width="488px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblSenha" runat="server" Text="Senha : "></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" Width="185px" MaxLength="8" TextMode="Password"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Label ID="lblConfirma" runat="server" Text="Confirmação : "></asp:Label>
            <asp:TextBox ID="txtConfirma" runat="server" Width="185px" MaxLength="8" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Panel ID="pnlPerfil" runat="server" GroupingText="Selecione o perfil : " HorizontalAlign="Left">
                &nbsp;
                <asp:RadioButton ID="rdbAdmin" runat="server" GroupName="radioPerfil" Text="Administrador" OnCheckedChanged="rdbAdmin_CheckedChanged" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdbGerente" runat="server" GroupName="radioPerfil" Text="Gerente" OnCheckedChanged="rdbGerente_CheckedChanged" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdbCaixa" runat="server" GroupName="radioPerfil" Text="Caixa" OnCheckedChanged="rdbCaixa_CheckedChanged" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdbOperador" runat="server" GroupName="radioPerfil" Text="Operador" OnCheckedChanged="rdbOperador_CheckedChanged" />
            </asp:Panel>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnConfirma" runat="server" CssClass="button" Text="Confirmar" OnClick="btnConfirma_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancela" runat="server" CssClass="button" Text="Cancelar" OnClick="btnCancela_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:Label ID="lblAlertaCadastro" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            </asp:Panel>
        <asp:Panel ID="pnlValida" runat="server" Height="218px" Visible="False" Width="553px" style="overflow-x:scroll;">
            <asp:Label ID="lblLista" runat="server" Text="Selecione Usuário para Validação : "></asp:Label>
            &nbsp;<asp:RadioButtonList ID="rblUsuarios" runat="server">
            </asp:RadioButtonList>
        </asp:Panel>
    
    </asp:Panel>
    </div>
</asp:Content> 
