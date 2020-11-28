<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelContabilidade.aspx.cs" Inherits="WebLoja1._0.View.PainelContabilidade" %>
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
    <div align="center" style="align-items:center; height:700px;">
        <asp:Panel ID="pnlPrincipal" runat="server" Height="680px" CssClass="auto-style5" Width="1100px" HorizontalAlign="Center">
            <asp:Button ID="btnCancelCliente" runat="server" BackColor="#CC0000" BorderColor="White" Font-Bold="True" ForeColor="White" Height="21px" OnClick="btnSair_Click" Style="float: right; margin-bottom: 15px;" Text="X" Width="27px" />
            <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large" Width="65%" CssClass="auto-style5" Height="45px" Style="float: left;"></asp:Label>
            &nbsp;<asp:Label ID="lblInfo" runat="server" Text="Usuario : " Style="float: left; margin-top: 25px;"></asp:Label>
            <asp:Label ID="lblUser" runat="server" Font-Bold="True" Font-Size="Large" Style="float: left; margin-top: 25px;"></asp:Label>
            <br />
      
            <asp:Panel ID="pnlBalanco" runat="server" BackColor="#ccccee" Height="600px" Style="margin-top:25px; float:left;" Width="60%" GroupingText="Balanço Patrimonial" CssClass="auto-style5">
                <div style="height:7%; width:50%; float:left; background-color:darkgreen; color:white; font-weight:bold; text-align:center;">ATIVO CIRCULANTE</div>
                <div style="height:7%; width:50%; float:right; background-color:darkred; color:white; font-weight:bold; text-align:center;">PASSIVO CIRCULANTE</div>                 
                <asp:ListBox ID="lbAtivoCirculante" runat="server" Width="38%" Height="300px" style="float:left;"></asp:ListBox>
                <asp:ListBox ID="lbValorAtivoCirculante" runat="server" Width="12%" Height="300px" style="float:left;"></asp:ListBox>
                <asp:ListBox ID="lbValorPassivoCirculante" runat="server" Width="12%" Height="235px" style="float:right;"></asp:ListBox>
                <asp:ListBox ID="lbPassivoCirculante" runat="server" Width="38%" Height="235px" style="float:right;"></asp:ListBox>
                               
                                        
                <div style="height:7%; width:50%; float:right; background-color:darkred; color:white; font-weight:bold; text-align:center;">PASSIVO NÃO CIRCULANTE</div>
                <asp:ListBox ID="lbVlPassNCirc" runat="server" Width="12%" Height="136px" style="float:right;"></asp:ListBox>
                <asp:ListBox ID="lbPassNCirc" runat="server" Width="38%" Height="136px" style="float:right;"></asp:ListBox>
                
                <div style="height:7%; width:50%; float:left; background-color:darkgreen; color:white; font-weight:bold; text-align:center;">ATIVO NÃO CIRCULANTE</div>                                                                           
                <asp:ListBox ID="lbAtvNCirc" runat="server" Width="38%" Height="228px" style="float:left;"></asp:ListBox>
                <asp:ListBox ID="lbVlAtvNCirc" runat="server" Width="12%" Height="228px" style="float:left;"></asp:ListBox>

                <div style="height:7%; width:50%; float:right; background-color:darkblue; color:white; font-weight:bold; text-align:center;">PATRIMÔNIO LIQUIDO</div>                                                                           
                <asp:ListBox ID="lbValorPatrimonio" runat="server" Width="12%" Height="140px" style="float:right;"></asp:ListBox> 
                <asp:ListBox ID="lbPatrimonio" runat="server" Width="38%" Height="140px" style="float:right;"></asp:ListBox>                          
               
            </asp:Panel>
            <asp:Panel ID="pnlDre" runat="server" BackColor="#ccccee" Height="550px" Style="margin-top: 25px; float: left;" Width="60%" GroupingText="Demonstrativo de Resultado do Exercício" CssClass="auto-style5" Visible="False">
            </asp:Panel>
            <asp:Panel ID="pnlControles" runat="server" Height="488px" Style="float: right; margin-top:25px" Width="39%" HorizontalAlign="Left" >
                <asp:Label ID="lblSubTitulo" runat="server" Text="Definição da Interface :" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                <asp:RadioButton ID="rdbBP" runat="server" style="float:right" Text="Balanço Patrimonial (B.P.)" Width="220px" Checked="True" GroupName="Opção"/>  
                <br />
                <br />
                <asp:RadioButton ID="rdbDRE" runat="server" style="float:right" Text="Resultado Exercício (D.R.E.)" Width="220px" GroupName="Opção"/>
                <br />
                <br />
                <asp:Label ID="lblPeriodo" runat="server" Text="Período :" Font-Bold="True" Font-Size="Large"></asp:Label>
                <br />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:Label ID="lblInicio" runat="server" Text="Data Início :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtDtInicio" runat="server" TextMode="Date" Width="94px"></asp:TextBox>
                &nbsp;<asp:Label ID="lblFim" runat="server" Text="até Data :"></asp:Label>
                &nbsp;<asp:TextBox ID="txtDtFim" runat="server" TextMode="Date" Width="94px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnMostrar" runat="server" Text="Mostrar" class="button" style="margin-left:120px; margin-top:12px;" Width="90px" OnClick="btnMostrar_Click"/>
                &nbsp;&nbsp;
                <asp:Button ID="btnLimpar" runat="server" Text="Limpar" class="button" Width="90px" OnClick="btnLimpar_Click" />
                <br />
                <br />
                <asp:Panel ID="pnlMovimento" runat="server" Height="255px" Width="100%" style="margin-top:12px; margin-right:6px; float:right;" GroupingText="Nova Movimentação">
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo :"></asp:Label>
                    <asp:DropDownList ID="ddlMovimento" runat="server" AutoPostBack="True" style="margin-top:12px; margin-bottom:12px;" Height="29px" Width="278px" OnSelectedIndexChanged="ddlMovimento_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;<asp:Label ID="lblDireção" runat="server" Font-Bold="True">Entrada</asp:Label>
                    <br />
                    &nbsp;<asp:Label ID="lblOrigem" runat="server" Text="Origem :"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlOrigem" runat="server" AutoPostBack="True" Height="29px"  Width="390px" Enabled="False" OnSelectedIndexChanged="ddlOrigem_SelectedIndexChanged" style="margin-bottom:12px;">
                    </asp:DropDownList>
                    <br />
                    &nbsp;<asp:Label ID="lblForma" runat="server" Text="Forma Pag. :"></asp:Label>
                    &nbsp;<asp:DropDownList ID="ddlForma" runat="server" AutoPostBack="True" Enabled="False" Height="29px" OnSelectedIndexChanged="ddlForma_SelectedIndexChanged" Width="390px">
                    </asp:DropDownList>
                    <br />
                    &nbsp;<asp:Label ID="lblValor" runat="server" Text="Valor (R$) :"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtMovValor" runat="server" Width="58px" style="margin-top:12px; margin-bottom:18px;" Enabled="False"></asp:TextBox>
                    &nbsp;<asp:Label ID="lblData" runat="server" Text="Data Movimento :"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtData" runat="server" style="margin-top:12px;" Width="100px" Enabled="False"></asp:TextBox>
                    <br />
                    &nbsp;<asp:Label ID="lblDesc" runat="server" Text="Descrição :"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtDescricao" runat="server" Width="385px" Height="44px" Rows="3" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                    <br />
                    &nbsp;<br />
                    <asp:Button ID="btnIncluir" runat="server" Text="Incluir" Width="90px" class="button" style="margin-left:105px; margin-top:4px;" Enabled="False" OnClick="btnIncluir_Click"/>
                    <asp:Button ID="btnLimparMov" runat="server" Text="Limpar" Width="90px" class="button" style="margin-left:25px" OnClick="btnLimparMov_Click"/>
                    
                    <br />
                    
                    <br />
                </asp:Panel>
                <br />
            </asp:Panel>
            &nbsp;<br />
            <br />
        </asp:Panel>
    </div>
    
</asp:Content> 