<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelFornecedores.aspx.cs" Inherits="WebLoja1._0.View.PainelFornecedores" %>
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
            height: 271px;
        }
    </style>
    <style type="text/css">
        .auto-style6 {
            height: 313px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            left: 0px;
            top: 3px;
            height: 48px;
        }
        .auto-style6 {
            left: 0px;
            top: 0px;
            height: 317px;
        }
        .auto-style7 {
            left: 0px;
            top: 0px;
            height: 535px;
            width: 80%;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            height: 435px;
        }
        .auto-style6 {
            height: 458px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center" class="auto-style6" width="60%" style="height: 656px">
    <div align="center" class="auto-style7" >
    <asp:Panel ID="pnlPrincipal" runat="server" Height="661px" CssClass="auto-style5" width="750px" style="overflow-x: scroll; overflow-y: scroll; margin-left: 0px;">
        <br />
        <asp:Panel ID="PanelFornecedor" runat="server" Width="718px" Height="69%" CssClass="auto-style5">
            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Label ID="lblNome" runat="server" Text="Nome Fornecedor: "></asp:Label>
            <asp:TextBox ID="txtNome" runat="server" Width="478px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblCnpj" runat="server" Text="CNPJ/CPF: "></asp:Label>
            <asp:TextBox ID="txtCnpj" runat="server" Width="255px"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lblResponsavel" runat="server" Text="Responsável: "></asp:Label>
            <asp:TextBox ID="txtResponsavel" runat="server" Width="160px" Enabled="False"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblTelefone" runat="server" Text="Telefone : "></asp:Label>
            <asp:TextBox ID="txtTelefone" runat="server" Width="125px" Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lblCelular" runat="server" Text="Celular : "></asp:Label>
            <asp:TextBox ID="txtCelular" runat="server" Width="125px" Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:Label ID="lblRecado" runat="server" Text="Recado : "></asp:Label>
            <asp:TextBox ID="txtRecado" runat="server" Enabled="False" Width="125px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblEndereco" runat="server" Text="Endereço: "></asp:Label>
            <asp:TextBox ID="txtEndereço" runat="server" Enabled="False" Width="535px"></asp:TextBox>
            &nbsp;<br /> <br />
            &nbsp;<asp:Label ID="lblNumero" runat="server" Text="Nº: "></asp:Label>
            <asp:TextBox ID="txtNumero" runat="server" Width="86px" Enabled="False"></asp:TextBox>
&nbsp;
            <asp:Label ID="lblBairro" runat="server" Text="Bairro: "></asp:Label>
            <asp:TextBox ID="txtBairro" runat="server" Width="339px" Enabled="False"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:CheckBox ID="chkStatus" runat="server" Text="Ativo: " TextAlign="Left" />
            <br />
            <br />
            <asp:Label ID="lblEstado" runat="server" Text="Estado: "></asp:Label>
            <asp:DropDownList ID="ddlEstado" runat="server" Height="22px" Width="244px" AutoPostBack="True" Enabled="False" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp;
            <asp:Label ID="lblCidade" runat="server" Text="Cidade: "></asp:Label>
            <asp:DropDownList ID="ddlCidade" runat="server" Height="22px" Width="244px" OnSelectedIndexChanged="ddlCidade_SelectedIndexChanged" Enabled="False" AutoPostBack="True">
                <asp:ListItem>* Selecionar Cidade</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Label ID="lblAlertaPreenchimento" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnVoltar0" runat="server" class="button" OnClick="btnLimpar_Click" Text="Limpar" Height="26px" Width="90px" />
            &nbsp;
            <asp:Button ID="btnPesquisar" runat="server" class="button" OnClick="btnPesquisar_Click" Text="Pesquisar" Height="26px" Width="90px" />
            &nbsp;
            <asp:Button ID="btnNovo" runat="server" class="button" OnClick="btnNovo_Click" Text="Novo" Height="26px" Width="90px" />
            &nbsp;
            <asp:Button ID="btnEditar" runat="server" class="button" OnClick="btnEditar_Click" Text="Alterar" Enabled="False" Height="26px" Width="90px" />
            &nbsp;
            <asp:Button ID="btnSalvar" runat="server" class="button" OnClick="btnSalvar_Click" Text="Salvar" Enabled="False" Height="26px" Width="90px" />
            &nbsp;
            <asp:Button ID="btnVoltar" runat="server" class="button" OnClick="btnSair_Click" Text="Sair" Height="26px" Width="90px" />
            <br />
            <br />
            <asp:Panel ID="PanelPesquisa" runat="server" CssClass="auto-style5" Height="50px" Width="587px" Visible="False">
                <div align="left">
                    <asp:RadioButtonList ID="rblFornecedores" runat="server" Height="21px" AutoPostBack="True" OnSelectedIndexChanged="rblFornecedores_SelectedIndexChanged">
                    </asp:RadioButtonList>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblAlerta" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
                </div>
            </asp:Panel>
        </asp:Panel>
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<br />
        
        <asp:Panel ID="panelCidade" runat="server" Height="159px" Width="57%" Visible="False" >
            <br />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Large" Text="Incluir Cidade"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="txtNovaCidade" runat="server" Width="380px"></asp:TextBox>
            <br />
            <asp:Label ID="lblAlertaCidade" runat="server" Font-Bold="True" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Button ID="btnExcluirCidade" runat="server" class="button" Height="26px" OnClick="btnExcluirCidade_Click" Text="Excluir" Width="90px" />
            &nbsp;
            <asp:Button ID="btnSalvarCidade" runat="server" class="button" Height="26px" OnClick="btnSalvarCidade_Click" Text="Salvar" Width="90px" />
            &nbsp;
            <asp:Button ID="btnCancelar" runat="server" class="button" OnClick="btnVoltar_Click" Text="Voltar" />
        </asp:Panel>
        
        </asp:Panel>
        </div>
    </div>
</asp:Content> 
