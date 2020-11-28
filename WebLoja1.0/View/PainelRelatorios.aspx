<%@ Page Title="" Language="C#" MasterPageFile="~/View/Padrao.Master" AutoEventWireup="true" CodeBehind="PainelRelatorios.aspx.cs" Inherits="WebLoja1._0.View.PainelRelatorios" %>
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
            height: 287px;
        }
    </style>
    <style type="text/css">
        .auto-style5 {
            margin-top: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div align="center">
    <asp:Panel ID="pnlPrincipal" runat="server" Height="1000px" CssClass="auto-style5" Width="1000px" HorizontalAlign="Center">
        <asp:Button ID="btnCancelCliente" runat="server" BackColor="#CC0000" BorderColor="White" Font-Bold="True" ForeColor="White" Height="21px" OnClick="btnSair_Click"  style="float:right; margin-bottom:15px;" Text="X" Width="27px" />
        <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Size="XX-Large" Width="100%"></asp:Label>
        <br />
        <br />
        <br />
        <asp:RadioButton ID="rbEstoque" Text="Estoque" runat="server" Checked="True" Font-Bold="True" Font-Size="Large" GroupName="groupRelatorio" OnCheckedChanged="rbEstoque_CheckedChanged" AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbVendas" Text="Vendas" runat="server" Font-Bold="True" Font-Size="Large" GroupName="groupRelatorio" OnCheckedChanged="rbVendas_CheckedChanged" AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbCliente" Text="Clientes" runat="server" Font-Bold="True" Font-Size="Large" GroupName="groupRelatorio" OnCheckedChanged="rbCliente_CheckedChanged" AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbPagamento" Text="Pagamentos" runat="server" Font-Bold="True" Font-Size="Large" GroupName="groupRelatorio" OnCheckedChanged="rbPagamento_CheckedChanged" AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbMovimentacao" Text="Mov. Financeiras" runat="server" Font-Bold="True" Font-Size="Large" GroupName="groupRelatorio" OnCheckedChanged="rbMovimentacao_CheckedChanged" AutoPostBack="True" />
        <br />
        <br />
        <asp:Panel ID="pnlEsgotadoVazio" runat="server" Visible="False" GroupingText="Estoque - Produtos Esgotados" HorizontalAlign="Left" Height="160px" Font-Bold="True" Font-Size="Large">
            <asp:Label ID="lblEsgVazio" runat="server" Font-Bold="True" Font-Size="X-Large" Width="100%" Text="Não existem itens esgotados."></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlEstoqueEsgotado" runat="server" GroupingText="Estoque - Produtos Esgotados" Height="456px" Font-Bold="True" Font-Size="Larger">
            <asp:GridView ID="gdvEsgotados" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="1" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Size="Medium" Height="10px">
                <Columns>
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlAcabandoVazio" runat="server" Visible="False" GroupingText="Estoque - Produtos Abaixo do Limite" HorizontalAlign="Left" Height="160px" CssClass="auto-style5" Font-Bold="True" Font-Size="Large">
            <asp:Label ID="lblAcabVazio" runat="server" Font-Bold="True" Font-Size="X-Large" Width="100%" Text="Não existem itens Abaixo dos limites."></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlEstoqueAcabando" runat="server" GroupingText="Estoque - Produtos Abaixo do Limite" Height="419px" Font-Bold="True" Font-Size="Larger">
            <asp:GridView ID="gdvAcabando" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="1" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Size="Medium" Height="10px">
                <Columns>
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlVendas" runat="server">
            <asp:Panel ID="pnlVendasGrid" runat="server" GroupingText="Vendas - Acumulado Mensal (Últimos 12 Meses)" Height="419px" Font-Bold="True" Font-Size="Larger">
            <asp:GridView ID="gdvVendas" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="1" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Size="Medium" Height="10px">
                <Columns>
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
        </asp:Panel>
        </asp:Panel>
                <asp:Panel ID="pnlClientes" runat="server">
            <asp:Panel ID="pnlGdvClientes" runat="server" GroupingText="Clientes Ativos - Lista Completa" Height="419px" Font-Bold="True" Font-Size="Larger">
            <asp:GridView ID="gdvClientes" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="1" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Size="X-Small" Height="10px">
                <Columns>
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
                <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
        </asp:Panel>
        </asp:Panel>
                <asp:Panel ID="pnlPagamentos" runat="server">
            <asp:Panel ID="pnlGdvPagamentos" runat="server" GroupingText="Pagamentos - Acumulado e Futuro (12 Meses Anteriores e 12 Meses Próximos)" Height="419px" Font-Bold="True" Font-Size="Larger">
            <asp:GridView ID="gdvPagamentos" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="1" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Size="Medium" Height="10px">
                <Columns>
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
        </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlMovimento" runat="server">
            <asp:Panel ID="pnlGdvMovimento" runat="server" GroupingText="Movimentação Financeira - Realizadas e a Realizar (12 Meses Anteriores e 5 Próximos Anos)" Height="419px" Font-Bold="True" Font-Size="Larger">
            <asp:GridView ID="gdvMovimento" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="1" AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Size="Medium" Height="10px">
                <Columns>
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                    <asp:BoundField />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Center" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
            <br />
            <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            <br />
            <br />
        </asp:Panel>
        </asp:Panel>
        </asp:Panel>
        </div>    
</asp:Content> 
