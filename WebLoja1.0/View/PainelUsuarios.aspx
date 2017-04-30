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
    <style type="text/css">
        .auto-style5 {
            color: black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="80%" CssClass="auto-style5" Width="60%" style="overflow-x:scroll;">
        <br />
        &nbsp;<asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        
    
        <asp:Panel ID="pnlCadastro" runat="server" Height="217px" Visible="False" Width="553px" CssClass="auto-style5" HorizontalAlign="Center" >
            <asp:Label ID="lblNome" runat="server" Text="Nome : "></asp:Label>
            <asp:TextBox ID="txtNome" runat="server" Width="488px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblSenha" runat="server" Text="Senha : "></asp:Label>
            <asp:TextBox ID="txtSenha" runat="server" Width="185px" MaxLength="8" EnableViewState="False"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:Label ID="lblConfirma" runat="server" Text="Confirmação : "></asp:Label>
            <asp:TextBox ID="txtConfirma" runat="server" Width="185px" MaxLength="8" EnableViewState="False"></asp:TextBox>
            <br />
            <br />
            <asp:Panel ID="pnlPerfil" runat="server" GroupingText="Selecione o perfil : " HorizontalAlign="Left">
                &nbsp;
                <asp:RadioButton ID="rdbAdmin" runat="server" GroupName="radioPerfil" Text="Administrador" OnCheckedChanged="rdbAdmin_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdbGerente" runat="server" GroupName="radioPerfil" Text="Gerente" OnCheckedChanged="rdbGerente_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdbCaixa" runat="server" GroupName="radioPerfil" Text="Caixa" OnCheckedChanged="rdbCaixa_CheckedChanged" AutoPostBack="True" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdbOperador" runat="server" GroupName="radioPerfil" Text="Operador" OnCheckedChanged="rdbOperador_CheckedChanged" AutoPostBack="True" />
            </asp:Panel>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnConfirma" runat="server" class="button" Text="Confirmar" OnClick="btnConfirma_Click" Enabled="False" Height="26px" Width="90px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancela" runat="server" class="button" Text="Cancelar" OnClick="btnCancela_Click" Height="26px" Width="90px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <asp:Label ID="lblAlertaCadastro" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            </asp:Panel>
        <asp:Panel ID="pnlValida" runat="server" Height="275px" Visible="False" Width="553px" style="overflow-x:scroll;" HorizontalAlign="Left">
            <asp:Panel ID="pnlButtons" runat="server" Height="77px" HorizontalAlign="Center">
                <br />
                <asp:Button ID="btnConfirmaValido" runat="server" class="button" OnClick="btnConfirmaValido_Click" Text="Validar" Enabled="False" Height="26px" Width="90px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelaValido" runat="server" class="button" OnClick="btnCancelaValida_Click" Text="Cancelar" Height="26px" Width="90px" />
                <br />
                <asp:Label ID="lblAlertaValida" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            </asp:Panel>
            <asp:Panel ID="pnlUser" runat="server" Height="130px" Visible="False">
                <asp:Label ID="lblNomeValida" runat="server" Text="Nome : "></asp:Label>
                <asp:TextBox ID="txtNomeValida" runat="server" Enabled="False" Width="488px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="lblRegistro" runat="server" Text="Registro : "></asp:Label>
                <asp:TextBox ID="txtRegistro" runat="server" MaxLength="8" Width="185px"></asp:TextBox>
                <br />
                <br />
                <asp:Panel ID="pnlPerfilValidacao" runat="server" GroupingText="Selecione o perfil : " HorizontalAlign="Left">
                    &nbsp;
                    <asp:RadioButton ID="rdbAdminVal" runat="server" Enabled="False" GroupName="radioPerfil" OnCheckedChanged="rdbAdmin_CheckedChanged" Text="Administrador" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdbGerenteVal" runat="server" Enabled="False" GroupName="radioPerfil" OnCheckedChanged="rdbGerente_CheckedChanged" Text="Gerente" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdbCaixaVal" runat="server" Enabled="False" GroupName="radioPerfil" OnCheckedChanged="rdbCaixa_CheckedChanged" Text="Caixa" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rdbOperadorVal" runat="server" Enabled="False" GroupName="radioPerfil" OnCheckedChanged="rdbOperador_CheckedChanged" Text="Operador" />
                </asp:Panel>
            </asp:Panel>
            <br />
            <asp:Label ID="lblLista" runat="server" Text="Selecione Usuário para Validação : "></asp:Label>
            &nbsp;<asp:RadioButtonList ID="rblUsuarios" runat="server" Height="16px" OnSelectedIndexChanged="rblUsuarios_SelectedIndexChanged" Width="407px" AutoPostBack="True">
            </asp:RadioButtonList>
        </asp:Panel>
    
    </asp:Panel>
    </div>
</asp:Content> 
