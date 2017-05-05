<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelProdutos.aspx.cs" Inherits="WebLoja1._0.View.PainelProdutos" %>
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
        .auto-style5 {}
    </style>
    <style type="text/css">
        .auto-style5 {}
    </style>
    <style type="text/css">
        .auto-style5 {
            color: darkblue;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-top: 0px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            width: 395px;
        }
        .auto-style6 {
            width: 106px;
            height: 228px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            height: 298px;
            width: 80%;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            width: 717px;
            height: 181px;
        }
        .auto-style6 {
            float: left;
            width: 524px;
            height: 190px;
        }
        .auto-style7 {
            float: left;
            width: 191px;
            height: 189px;
        }
        .auto-style8 {
            width: 717px;
            height: 185px;
        }
        .auto-style9 {
            width: 717px;
            height: 188px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-left: 3px;
        }
        .auto-style6 {
            margin-left: 3px;
            margin-top: 8px;
        }
        .auto-style7 {
            margin-top: 9px;
        }
        .auto-style8 {
            margin-top: 8px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-top: 12px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center" style="height: 782px">
        <asp:Panel ID="pnlPrincipal" runat="server" Height="775px" CssClass="auto-style5" Width="770px" HorizontalAlign="Center" >
            <br />
            &nbsp;<asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
            <br />
            <br />
            <div align="center">
            <div  align="center" class="auto-style9">
                <%-- style="overflow-x:scroll;" --%>
            

                <div style="max-height:153px; height: 458px;" class="auto-style8">
                    <div class="auto-style7">
                        <asp:Panel ID="PanelProduto" runat="server" Height="195px" Width="180px" HorizontalAlign="Left">
                            <asp:Label ID="imgProduto" runat="server" BorderColor="#000099" BorderStyle="Solid" BorderWidth="3px" Height="186px" Width="174px" style="float:left"> <img src="../Image/temp.jpg" width="100%" height="100%"/></asp:Label>
                        </asp:Panel>
                    </div>
                    <div align="left" class="auto-style6" style="width: 515px; height: 202px;">
                        <asp:Label ID="lblNome" runat="server" Text="Produto :"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtNome" runat="server" Width="438px" Enabled="False" MaxLength="30" Height="17px"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblCodigo" runat="server" Text="Código :"></asp:Label>
                        &nbsp;&nbsp;<asp:TextBox ID="txtCodigo" runat="server" Width="291px" Enabled="False" style="margin-top:10px;" MaxLength="13" Height="17px"></asp:TextBox>
                        &nbsp;&nbsp;<asp:Label ID="lblUnd" runat="server" Text="Und :"></asp:Label>
                        &nbsp;<asp:DropDownList ID="ddlMedida" runat="server" Height="26px" Width="90px" Enabled="False">
                            <asp:ListItem>UNIDADE</asp:ListItem>
                            <asp:ListItem>CAIXA</asp:ListItem>
                            <asp:ListItem>PACOTE</asp:ListItem>
                        </asp:DropDownList>
                        <br /> <asp:Label ID="lblCusto" runat="server" Text="Custo :"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCusto" runat="server" Enabled="False" Width="81px" MaxLength="8" Height="17px" style="margin-top:10px; margin-left:2px;"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblIcms" runat="server" Text="ICMS (R$) :"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtIcms" runat="server" Enabled="False" Width="81px" MaxLength="8" Height="17px"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPreco" runat="server" Text="Preço :"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtPreco" runat="server" Width="81px" Enabled="False" MaxLength="8" Height="17px"></asp:TextBox>
                        
                            
                        <br />
                        
                        <asp:Label ID="lblFornecedor" runat="server" Text="Fornecedor :"></asp:Label>
                        &nbsp;&nbsp;<asp:DropDownList ID="ddlFornecedores" runat="server" AutoPostBack="True" DataTextField="Nome" DataValueField="Id" Height="27px" Width="418px" Enabled="False" style="margin-top:10px;">
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="lblQnt" runat="server" Text="Qnt Atual :"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtQnt" runat="server" Enabled="False" Width="79px" style="margin-top:10px;" Height="17px" MaxLength="3"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMinimo" runat="server" Text="Qnt Min : "></asp:Label>
                        &nbsp;<asp:TextBox ID="txtMinimo" runat="server" Enabled="False" Height="17px" Width="79px" MaxLength="3"></asp:TextBox>
                        &nbsp; &nbsp;<asp:Label ID="lblMaximo" runat="server" Text="Qnt Max : "></asp:Label>
                        &nbsp;<asp:TextBox ID="txtMaximo" runat="server" Enabled="False" Height="17px" Width="79px" MaxLength="3"></asp:TextBox>
                        <br />
                        <asp:Label ID="lblNumLocal" runat="server" Text="Localização :  Nº"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtNumLocal" runat="server" Enabled="False" style="margin-top:10px;" Width="30px" MaxLength="2" Height="17px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblLetraLocal" runat="server" Text="Sigla :"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtLetraLocal" runat="server" Enabled="False" style="margin-top:10px;" Width="30px" MaxLength="2" Height="17px"></asp:TextBox>
                        &nbsp;<asp:Label ID="lblRefLocal" runat="server" Text="Ref. :"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtRefLocal" runat="server" Enabled="False" style="margin-top:10px;" Width="210px" Height="17px" MaxLength="50"></asp:TextBox>
                        <br />
                    </div>
                    <asp:FileUpload ID="Upload" runat="server" BackColor="White" CssClass="auto-style6" Font-Size="XX-Small" Height="20px" Width="626px" Enabled="False" OnDataBinding="btnImage_Click" style="float:left;"/>
                    &nbsp;<asp:Button ID="btnImage" runat="server" Font-Size="XX-Small" Height="21px" OnClick="btnImage_Click" Text="Upload" Width="72px" Enabled="False" CssClass="auto-style8" style="float:left; margin-left: 3px; "/>
                    &nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>

                <br />

                <br />
                <br />

                <br />
                <asp:Panel ID="Butons" runat="server" Height="69px" Width="100%" Style="margin-top: 0px" CssClass="auto-style5">
                    <asp:Label ID="lblAlerta" runat="server" Font-Bold="True" Visible="False"></asp:Label>

                    <br />
                    <asp:Button ID="btnLimpar" runat="server" class="button" Text="Limpar" OnClick="btnLimpar_Click" Height="26px" Width="90px" CssClass="auto-style5" />
                &nbsp;
                <asp:Button ID="btnPesquisar" runat="server" class="button" Height="26px" OnClick="btnPesquisar_Click" Text="Pesquisar" Width="90px" />
                    &nbsp;
            <asp:Button ID="btnNovo" runat="server" class="button" Text="Novo" OnClick="btnNovo_Click" Height="26px" Width="90px" />
                    &nbsp;
            <asp:Button ID="btnEditar" runat="server" class="button" Text="Alterar" OnClick="btnEditar_Click" Enabled="False" Height="26px" Width="90px" />
                    &nbsp;
            <asp:Button ID="btnExcluir" runat="server" class="button" Text="Excluir" OnClick="btnExcluir_Click" Enabled="False" Height="26px" Width="90px" />
                    &nbsp;
                <asp:Button ID="btnSalvar" runat="server" class="button" Text="Salvar" OnClick="btnSalvar_Click" Enabled="False" Height="26px" Width="90px" />
                    &nbsp;
                <asp:Button ID="btnVoltar" runat="server" Text="Voltar" class="button" OnClick="btnVoltar_Click" Height="26px" Width="90px" />
                     <asp:Label ID="Label1" runat="server" Width="7px"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="panelPesquisa" runat="server" Height="49px" Visible="False">
                    <asp:Label ID="lblPesquisa" runat="server" Text="Pesquisar por :"></asp:Label>
                    &nbsp;<asp:TextBox ID="txtPesquisa" runat="server" Width="423px"></asp:TextBox>
                    &nbsp;<asp:ImageButton ID="btnBusca" runat="server" src="../Image/lupa.png"  OnClick="btnBusca_Click" Height="20px" Width="20px" />
                </asp:Panel>
            

                <br />
            

            <br />
                </div>
                
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                
                <br />
                <br />
                
            <asp:Label ID="lblListaTitulo" runat="server" Text="Relação de Produtos" Font-Bold="True" Font-Size="Large"></asp:Label>
            <br />
            <br />             
                <div align="center" style="overflow-y: scroll; overflow-x: scroll; width: 90%; height: auto; max-height: 300px">
                    <asp:Panel ID="pnlGridView" runat="server" Width="100%" Height ="280px">
                    <asp:GridView ID="gvlProdutos" runat="server" AutoGenerateColumns="False" CellPadding="4" OnSelectedIndexChanged="gvlProdutos_SelectedIndexChanged" Width="100%" DataKeyNames="desc_produto" DataMember="DefaultView" ViewStateMode="Enabled" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" Height="130px">
                        <Columns>
                            <asp:CommandField ButtonType="Button" SelectText="Selec." ShowSelectButton="True" />
                            <asp:BoundField DataField="cod_produto" HeaderText="Código" SortExpression="cod_produto" />
                            <asp:BoundField DataField="desc_produto" HeaderText="Produto" SortExpression="desc_produto" />
                            <asp:BoundField DataField="preco_compra" HeaderText="Compra" SortExpression="preco_compra" />
                            <asp:BoundField DataField="preco_venda" HeaderText="Venda" SortExpression="preco_venda" />
                            <asp:BoundField DataField="Estoque.qnt_atual" HeaderText="Qnt" SortExpression="Estoque.qnt_atual" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="LightGrey" Font-Bold="True" ForeColor="Black" />
                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                        <SortedDescendingHeaderStyle BackColor="#002876" />
                    </asp:GridView>
                    <%--asp:SqlDataSource ID="SqlDataSourceProd" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT prod.cod_produto, prod.desc_produto, prod.preco_compra, prod.preco_venda, est.qnt_atual FROM [Produtos] prod, [Estoque] est WHERE prod.status = 1 AND prod.id = est.id_produto;"></--asp:SqlDataSource--%>
                    </asp:Panel>
                
            </div>
                     
            </asp:Panel>
    </div>
</asp:Content>
