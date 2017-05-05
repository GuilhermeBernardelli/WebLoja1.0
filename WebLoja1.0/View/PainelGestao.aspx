 <%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelGestao.aspx.cs" Inherits="WebLoja1._0.View.PainelGestao" %>
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
    <style type="text/css">
        .auto-style5 {
            height: 355px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            height: 569px;
            width: 600px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            height: 482px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            height: 523px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <div class="auto-style5" >
    <asp:Panel ID="pnlPrincipal" runat="server" Height="700px" Width="550px" HorizontalAlign="Center">
        <br />
        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="pnlBasico" runat="server" Height="384px">
            <asp:Panel ID="pnlBase" runat="server" Height="100%" Width="30%" style="float:left;" GroupingText="Ajustes Padrão">
                <br />
                <asp:Label ID="lblIcms" runat="server" Text="Taxa ICMS (%)"></asp:Label>
                <br />
                <asp:TextBox ID="txtIcms" runat="server" Width="90%" style="margin-bottom:17px;" Height="18px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblComissao" runat="server" Text="Comissão (%)" ></asp:Label>
                <asp:TextBox ID="txtComissao" runat="server" Width="90%" style="margin-bottom:17px;" Height="18px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblAutoDesc" runat="server" Text="Desc. automático(%)"></asp:Label>
                <asp:TextBox ID="txtAutoDesc" runat="server" Width="90%" style="margin-bottom:17px;" Height="18px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblAutoDescValor" runat="server" Text="Desc. Automático R$"></asp:Label>
                <asp:TextBox ID="txtAutoDescValor" runat="server" Width="90%" style="margin-bottom:17px;" Height="18px" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblMaxDescPerc" runat="server" Text="Desc. Máximo (%)"></asp:Label>
                <asp:TextBox ID="txtMaxDescPerc" runat="server" Width="90%" Height="18px" style="margin-bottom: 8px;" Enabled="False"></asp:TextBox>
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlCheque" runat="server" Height="100%" Width="35%" style="float:right;" GroupingText="Juros Pag. Cheques">
                <br />
                <asp:Label ID="lblCheque2x" runat="server" Text="2x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque2x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque3x" runat="server" Text="3x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque3x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque4x" runat="server" Text="4x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque4x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque5x" runat="server" Text="5x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque5x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque6x" runat="server" Text="6x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque6x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque7x" runat="server" Text="7x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque7x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque8x" runat="server" Text="8x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque8x" runat="server" Width="57px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque9x" runat="server" Text="9x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque9x" runat="server" Width="57px" style="margin-bottom:8px;  margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque10x" runat="server" Text="10x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque10x" runat="server" Width="57px" style="margin-bottom:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque11x" runat="server" Text="11x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque11x" runat="server" Width="57px" style="margin-bottom:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblCheque12x" runat="server" Text="12x Cheque : "></asp:Label>
                <asp:TextBox ID="txtCheque12x" runat="server" Width="57px" style="margin-bottom:10px;" Enabled="False"></asp:TextBox>
            </asp:Panel>
            <asp:Panel ID="pnlPrazo" runat="server" Height="100%" Width="35%" style="float:right;" GroupingText="Juros Pag. Prazo">
                <br />
                <asp:Label ID="lblPrazo3x" runat="server" Text="3x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo3x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo4x" runat="server" Text="4x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo4x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo5x" runat="server" Text="5x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo5x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo6x" runat="server" Text="6x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo6x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo7x" runat="server" Text="7x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo7x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo8x" runat="server" Text="8x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo8x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo9x" runat="server" Text="9x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo9x" runat="server" Width="60px" style="margin-bottom:8px; margin-left:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo10x" runat="server" Text="10x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo10x" runat="server" Width="60px" style="margin-bottom:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo11x" runat="server" Text="11x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo11x" runat="server" Width="60px" style="margin-bottom:8px;" Enabled="False"></asp:TextBox>
                <br />
                <asp:Label ID="lblPrazo12x" runat="server" Text="12x Prazo :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtPrazo12x" runat="server" Width="60px" style="margin-bottom:3px;" Enabled="False"></asp:TextBox>
                <br />
                <br />
                <br />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlButtons" runat="server" Height="71px" HorizontalAlign="Center">
            <br />
            <asp:Button ID="btnEditar" runat="server" Text="Editar" class="button" OnClick="btnEditar_Click" Width="90px"/>
            &nbsp;
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" class="button" OnClick="btnSalvar_Click" Width="90px" Enabled="False"/>
            &nbsp;
            <asp:Button ID="btnSair" runat="server" Text="Voltar" class="button" OnClick="btnSair_Click" Width="90px"/>
        </asp:Panel>
        </asp:Panel>
        
        </div>
    </div>
</asp:Content> 