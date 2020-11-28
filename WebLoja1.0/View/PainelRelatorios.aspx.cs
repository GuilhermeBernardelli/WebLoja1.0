using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLoja1._0.Control;
using WebLoja1._0.Model;

namespace WebLoja1._0.View
{
    public partial class PainelRelatorios : System.Web.UI.Page
    {
        public struct vendasAcumulado
        {
            public string mês;
            public int quantidade;
            public decimal valorTotal;
            public decimal icmsTotal;
            public decimal comissaoTotal;
        };
        public struct pagAcumulado
        {
            public string mês;
            public decimal valorTotal;
            public int qntTotal;
            public int qntFormaCartao;
            public int qntFormaDinheiro;
            public int qntFormaCréditos;
            public int qntFormaCheque;
            public int qntTipoVista;
            public int qntTipoParcelado;
        };
        public struct movAcumulado
        {
            public string mês;
            public string descricao;
            public string subtipo;
            public decimal valor;
            public string direcao;
        };
        Controle controle = new Controle();
        List<Estoque> listaEstoque = new List<Estoque>();
        List<Clientes> listaClientes = new List<Clientes>();
        static vendasAcumulado lista = new vendasAcumulado();
        static List<Vendas> listaVendas = new List<Vendas>();
        static List<vendasAcumulado> listaVendasMês = new List<vendasAcumulado>();
        static pagAcumulado pagMes = new pagAcumulado();
        static List<Pagamentos> listaPagamentos = new List<Pagamentos>();
        static List<pagAcumulado> listaPagMes = new List<pagAcumulado>();
        static movAcumulado movMes = new movAcumulado();
        static List<Movimentos> listaMovimentos = new List<Movimentos>();
        static List<movAcumulado> listaMovMes = new List<movAcumulado>();

        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;

        static int esgotado = 0, acabando = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    lblMensagem.Text = "Sistemas de Relatórios";
                    rbEstoque_CheckedChanged(sender, e);

                    //validação de acesso
                    id = Convert.ToInt32(Session["id"]);
                    perfil = Convert.ToInt32(Session["perfil"]);

                    if (id != 0 || perfil != 0)
                    {
                        user = controle.pesquisaUserId(id);
                        if (!teste.ValidaUser(id, perfil) || user.num_perfil == 3)
                        {
                            Response.Redirect("~/View/AcessoIndevido.aspx");
                        }
                    }

                    else
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }

                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Falha na validação de usuário, por favor, tente novamente.');", true);
            }
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }

        private void limpaRelatórios()
        {
            esgotado = 0;
            acabando = 0;
            pnlEstoqueEsgotado.Visible = false;
            pnlEstoqueAcabando.Visible = false;
            pnlAcabandoVazio.Visible = false;
            pnlEsgotadoVazio.Visible = false;
            pnlVendas.Visible = false;
            pnlClientes.Visible = false;
            pnlClientes.Visible = false;
            pnlPagamentos.Visible = false;
            pnlMovimento.Visible = false;
        }

        protected void rbEstoque_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                limpaRelatórios();

                pnlEstoqueEsgotado.Visible = true;
                pnlEstoqueAcabando.Visible = true;

                listaEstoque = controle.pesquisaEstoqueRelatorio();

                gdvEsgotados.DataSource = listaEstoque;

                gdvEsgotados.Columns[0].HeaderText = "Produto";
                gdvEsgotados.Columns[0].ItemStyle.Width = 300;
                gdvEsgotados.Columns[1].HeaderText = "Código";
                gdvEsgotados.Columns[1].ItemStyle.Width = 250;
                gdvEsgotados.Columns[2].HeaderText = "Qnt.";
                gdvEsgotados.Columns[2].ItemStyle.Width = 60;
                gdvEsgotados.Columns[3].HeaderText = "Und.";
                gdvEsgotados.Columns[3].ItemStyle.Width = 90;
                gdvEsgotados.Columns[4].HeaderText = "Qnt. Min.";
                gdvEsgotados.Columns[4].ItemStyle.Width = 60;
                gdvEsgotados.Columns[5].HeaderText = "Qnt. Max.";
                gdvEsgotados.Columns[5].ItemStyle.Width = 60;
                gdvEsgotados.Columns[6].HeaderText = "Local Nº";
                gdvEsgotados.Columns[6].ItemStyle.Width = 90;
                gdvEsgotados.Columns[7].HeaderText = "Sigla";
                gdvEsgotados.Columns[7].ItemStyle.Width = 60;
                gdvEsgotados.Columns[8].HeaderText = "Referência";
                gdvEsgotados.Columns[8].ItemStyle.Width = 100;

                gdvEsgotados.DataBind();

                for (int i = 0; i < listaEstoque.Count; i++)
                {

                    if (listaEstoque[i].qnt_atual == 0)
                    {
                        esgotado++;
                        GridViewRow newLine = new GridViewRow(gdvEsgotados.Rows.Count + 1, gdvEsgotados.Rows.Count + 1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                        gdvEsgotados.Controls.Add(newLine);

                        gdvEsgotados.Rows[i].Cells[0].Text = listaEstoque[i].Produtos.desc_produto;
                        gdvEsgotados.Rows[i].Cells[1].Text = listaEstoque[i].Produtos.cod_produto;
                        gdvEsgotados.Rows[i].Cells[2].Text = listaEstoque[i].qnt_atual.ToString();
                        gdvEsgotados.Rows[i].Cells[3].Text = listaEstoque[i].Produtos.und_medida;
                        gdvEsgotados.Rows[i].Cells[4].Text = listaEstoque[i].qnt_minima.ToString();
                        gdvEsgotados.Rows[i].Cells[5].Text = listaEstoque[i].qnt_maxima.ToString();
                        gdvEsgotados.Rows[i].Cells[6].Text = listaEstoque[i].num_local.ToString();
                        gdvEsgotados.Rows[i].Cells[7].Text = listaEstoque[i].letra_local;
                        gdvEsgotados.Rows[i].Cells[8].Text = listaEstoque[i].ref_local;

                        gdvEsgotados.Rows[i].RowState = DataControlRowState.Normal;
                        gdvEsgotados.Rows[i].Visible = true;
                    }
                    else
                    {
                        gdvEsgotados.Rows[i].Visible = false;
                    }

                }
                if (esgotado == 0)
                {
                    pnlEstoqueEsgotado.Visible = false;
                    pnlEsgotadoVazio.Visible = true;
                }

                gdvAcabando.DataSource = listaEstoque;

                gdvAcabando.Columns[0].HeaderText = "Produto";
                gdvAcabando.Columns[0].ItemStyle.Width = 300;
                gdvAcabando.Columns[1].HeaderText = "Código";
                gdvAcabando.Columns[1].ItemStyle.Width = 250;
                gdvAcabando.Columns[2].HeaderText = "Qnt.";
                gdvAcabando.Columns[2].ItemStyle.Width = 60;
                gdvAcabando.Columns[3].HeaderText = "Und.";
                gdvAcabando.Columns[3].ItemStyle.Width = 90;
                gdvAcabando.Columns[4].HeaderText = "Qnt. Min.";
                gdvAcabando.Columns[4].ItemStyle.Width = 60;
                gdvAcabando.Columns[5].HeaderText = "Qnt. Max.";
                gdvAcabando.Columns[5].ItemStyle.Width = 60;
                gdvAcabando.Columns[6].HeaderText = "Local Nº";
                gdvAcabando.Columns[6].ItemStyle.Width = 90;
                gdvAcabando.Columns[7].HeaderText = "Sigla";
                gdvAcabando.Columns[7].ItemStyle.Width = 60;
                gdvAcabando.Columns[8].HeaderText = "Referência";
                gdvAcabando.Columns[8].ItemStyle.Width = 100;

                gdvAcabando.DataBind();

                for (int i = 0; i < listaEstoque.Count; i++)
                {

                    if (listaEstoque[i].qnt_minima > listaEstoque[i].qnt_atual && listaEstoque[i].qnt_atual > 0)
                    {
                        acabando++;
                        GridViewRow newLine = new GridViewRow(gdvAcabando.Rows.Count + 1, gdvAcabando.Rows.Count + 1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                        gdvAcabando.Controls.Add(newLine);

                        gdvAcabando.Rows[i].Cells[0].Text = listaEstoque[i].Produtos.desc_produto;
                        gdvAcabando.Rows[i].Cells[1].Text = listaEstoque[i].Produtos.cod_produto;
                        gdvAcabando.Rows[i].Cells[2].Text = listaEstoque[i].qnt_atual.ToString();
                        gdvAcabando.Rows[i].Cells[3].Text = listaEstoque[i].Produtos.und_medida;
                        gdvAcabando.Rows[i].Cells[4].Text = listaEstoque[i].qnt_minima.ToString();
                        gdvAcabando.Rows[i].Cells[5].Text = listaEstoque[i].qnt_maxima.ToString();
                        gdvAcabando.Rows[i].Cells[6].Text = listaEstoque[i].num_local.ToString();
                        gdvAcabando.Rows[i].Cells[7].Text = listaEstoque[i].letra_local;
                        gdvAcabando.Rows[i].Cells[8].Text = listaEstoque[i].ref_local;

                        gdvAcabando.Rows[i].RowState = DataControlRowState.Normal;
                        gdvAcabando.Rows[i].Visible = true;
                    }
                    else
                    {
                        gdvAcabando.Rows[i].Visible = false;
                    }

                }
                if (acabando == 0)
                {
                    pnlEstoqueAcabando.Visible = false;
                    pnlAcabandoVazio.Visible = true;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Falha na pesquisa de materiais no estoque, por favor, tente novamente.');", true);
            }
        }

        protected void rbVendas_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                limpaRelatórios();

                pnlVendas.Visible = true;

                listaVendas = controle.pesquisaVendasGeral();

                listaVendasMês = novaListaMês(listaVendas);

                gdvVendas.DataSource = listaVendasMês;

                gdvVendas.Columns[0].HeaderText = "Mês";
                gdvVendas.Columns[0].ItemStyle.Width = 160;
                gdvVendas.Columns[1].HeaderText = "Quantidade";
                gdvVendas.Columns[1].ItemStyle.Width = 120;
                gdvVendas.Columns[2].HeaderText = "Valor Total (R$)";
                gdvVendas.Columns[2].ItemStyle.Width = 220;
                gdvVendas.Columns[3].HeaderText = "Imposto Devido (R$)";
                gdvVendas.Columns[3].ItemStyle.Width = 220;
                gdvVendas.Columns[4].HeaderText = "Comissão Devida (R$)";
                gdvVendas.Columns[4].ItemStyle.Width = 220;

                gdvVendas.DataBind();

                for (int i = 0; i < listaVendasMês.Count; i++)
                {

                    GridViewRow newLine = new GridViewRow(gdvVendas.Rows.Count + 1, gdvVendas.Rows.Count + 1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                    gdvVendas.Controls.Add(newLine);

                    gdvVendas.Rows[i].Cells[0].Text = listaVendasMês[i].mês.ToString();
                    gdvVendas.Rows[i].Cells[1].Text = listaVendasMês[i].quantidade.ToString();
                    gdvVendas.Rows[i].Cells[2].Text = "R$ " + listaVendasMês[i].valorTotal.ToString("0.00");
                    gdvVendas.Rows[i].Cells[3].Text = "R$ " + listaVendasMês[i].icmsTotal.ToString("0.00");
                    gdvVendas.Rows[i].Cells[4].Text = "R$ " + listaVendasMês[i].comissaoTotal.ToString("0.00");

                    gdvVendas.Rows[i].RowState = DataControlRowState.Normal;
                    gdvVendas.Rows[i].Visible = true;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Falha na pesquisa de vendas realizadas, por favor, tente novamente.');", true);
            }
        }

        private List<vendasAcumulado> novaListaMês(List<Vendas> listaVendas)
        {

            listaVendasMês = new List<vendasAcumulado>();
            int j = 0, y;
            y = DateTime.Today.Year - 1;
            j = DateTime.Today.AddMonths(-11).Month;
            do
            {
                vendasAcumulado lista = new vendasAcumulado();

                for (int i = 0; i < listaVendas.Count; i++)
                {

                    if (listaVendas[i].data_Venda > DateTime.Today.AddMonths(-11))
                    {
                        if (listaVendas[i].data_Venda.Month.Equals(j))
                        {
                            lista.mês = listaVendas[i].data_Venda.Month.ToString() + "/" + listaVendas[i].data_Venda.Year.ToString();
                            lista.quantidade++;
                            lista.comissaoTotal = lista.comissaoTotal + Convert.ToDecimal(listaVendas[i].comissao);
                            lista.icmsTotal = lista.icmsTotal + Convert.ToDecimal(listaVendas[i].ICMS);
                            lista.valorTotal = lista.valorTotal + Convert.ToDecimal(listaVendas[i].valor_Venda);

                        }
                    }
                }

                if (lista.mês != null)
                {
                    listaVendasMês.Add(lista);
                }

                if (j == 12)
                {
                    j = 1;
                    y++;
                }
                else
                {
                    j++;
                }

            } while (j != DateTime.Today.Month + 1);
            return listaVendasMês;

        }

        protected void rbCliente_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                limpaRelatórios();

                pnlClientes.Visible = true;
                pnlClientes.Visible = true;

                listaClientes = controle.pesquisaClientesCompleta("");

                gdvClientes.DataSource = listaClientes;

                gdvClientes.Columns[0].HeaderText = "Nome";
                gdvClientes.Columns[0].ItemStyle.Width = 280;
                gdvClientes.Columns[1].HeaderText = "Endereço";
                gdvClientes.Columns[1].ItemStyle.Width = 180;
                gdvClientes.Columns[2].HeaderText = "Nº";
                gdvClientes.Columns[2].ItemStyle.Width = 20;
                gdvClientes.Columns[3].HeaderText = "Bairro";
                gdvClientes.Columns[3].ItemStyle.Width = 90;
                gdvClientes.Columns[4].HeaderText = "Cidade";
                gdvClientes.Columns[4].ItemStyle.Width = 60;
                gdvClientes.Columns[5].HeaderText = "Telefone";
                gdvClientes.Columns[5].ItemStyle.Width = 60;
                gdvClientes.Columns[6].HeaderText = "Celular";
                gdvClientes.Columns[6].ItemStyle.Width = 60;
                gdvClientes.Columns[7].HeaderText = "Outro Tel.";
                gdvClientes.Columns[7].ItemStyle.Width = 60;
                gdvClientes.Columns[8].HeaderText = "Email";
                gdvClientes.Columns[8].ItemStyle.Width = 120;
                gdvClientes.Columns[9].HeaderText = "Créditos";
                gdvClientes.Columns[9].ItemStyle.Width = 30;

                gdvClientes.DataBind();

                for (int i = 0; i < listaClientes.Count; i++)
                {
                    GridViewRow newLine = new GridViewRow(gdvClientes.Rows.Count + 1, gdvClientes.Rows.Count + 1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                    gdvClientes.Controls.Add(newLine);

                    gdvClientes.Rows[i].Cells[0].Text = listaClientes[i].nome;
                    gdvClientes.Rows[i].Cells[1].Text = listaClientes[i].endereço;
                    gdvClientes.Rows[i].Cells[2].Text = listaClientes[i].numeral;
                    gdvClientes.Rows[i].Cells[3].Text = listaClientes[i].bairro;
                    gdvClientes.Rows[i].Cells[4].Text = listaClientes[i].Cidades.cidade;
                    gdvClientes.Rows[i].Cells[5].Text = listaClientes[i].telefone;
                    gdvClientes.Rows[i].Cells[6].Text = listaClientes[i].celular;
                    gdvClientes.Rows[i].Cells[7].Text = listaClientes[i].recado;
                    gdvClientes.Rows[i].Cells[8].Text = listaClientes[i].email;
                    gdvClientes.Rows[i].Cells[9].Text = Convert.ToDecimal(listaClientes[i].creditos).ToString("0.00");

                    gdvClientes.Rows[i].RowState = DataControlRowState.Normal;
                    gdvClientes.Rows[i].Visible = true;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Falha no levantamento de clientes cadastrados, por favor, tente novamente.');", true);
            }
        }

        protected void rbPagamento_CheckedChanged(object sender, EventArgs e)
        {

            limpaRelatórios();

            pnlPagamentos.Visible = true;

            listaPagamentos = controle.pesquisaPagamentosGeral();
            listaPagMes = novaListaPagMês(listaPagamentos);

            gdvPagamentos.DataSource = listaPagMes;

            gdvPagamentos.Columns[0].HeaderText = "Mês";
            gdvPagamentos.Columns[0].ItemStyle.Width = 120;
            gdvPagamentos.Columns[1].HeaderText = "Qnt. Total";
            gdvPagamentos.Columns[1].ItemStyle.Width = 100;
            gdvPagamentos.Columns[2].HeaderText = "Qnt. à Vista";
            gdvPagamentos.Columns[2].ItemStyle.Width = 100;
            gdvPagamentos.Columns[3].HeaderText = "Qnt. Parcelado";
            gdvPagamentos.Columns[3].ItemStyle.Width = 100;
            gdvPagamentos.Columns[4].HeaderText = "Qnt a Dinheiro";
            gdvPagamentos.Columns[4].ItemStyle.Width = 100;
            gdvPagamentos.Columns[5].HeaderText = "Qnt. Pré-Paga";
            gdvPagamentos.Columns[5].ItemStyle.Width = 100;
            gdvPagamentos.Columns[6].HeaderText = "Qnt. no Cartão";
            gdvPagamentos.Columns[6].ItemStyle.Width = 100;
            gdvPagamentos.Columns[7].HeaderText = "Qnt. no Cheque";
            gdvPagamentos.Columns[7].ItemStyle.Width = 100;
            gdvPagamentos.Columns[8].HeaderText = "Valor Total (R$)";
            gdvPagamentos.Columns[8].ItemStyle.Width = 120;

            gdvPagamentos.DataBind();

            for (int i = 0; i < listaPagMes.Count; i++)
            {

                GridViewRow newLine = new GridViewRow(gdvPagamentos.Rows.Count + 1, gdvPagamentos.Rows.Count + 1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                gdvPagamentos.Controls.Add(newLine);

                gdvPagamentos.Rows[i].Cells[0].BackColor = Color.LightGray;
                gdvPagamentos.Rows[i].Cells[2].BackColor = Color.LightGray;
                gdvPagamentos.Rows[i].Cells[3].BackColor = Color.LightGray;
                gdvPagamentos.Rows[i].Cells[8].BackColor = Color.LightGray;

                gdvPagamentos.Rows[i].Cells[0].Text = listaPagMes[i].mês.ToString();
                gdvPagamentos.Rows[i].Cells[1].Text = listaPagMes[i].qntTotal.ToString();
                gdvPagamentos.Rows[i].Cells[2].Text = listaPagMes[i].qntTipoVista.ToString();
                gdvPagamentos.Rows[i].Cells[3].Text = listaPagMes[i].qntTipoParcelado.ToString();
                gdvPagamentos.Rows[i].Cells[4].Text = listaPagMes[i].qntFormaDinheiro.ToString();
                gdvPagamentos.Rows[i].Cells[5].Text = listaPagMes[i].qntFormaCréditos.ToString();
                gdvPagamentos.Rows[i].Cells[6].Text = listaPagMes[i].qntFormaCartao.ToString();
                gdvPagamentos.Rows[i].Cells[7].Text = listaPagMes[i].qntFormaCheque.ToString();
                gdvPagamentos.Rows[i].Cells[8].Text = "R$ " + listaPagMes[i].valorTotal.ToString();

                gdvPagamentos.Rows[i].RowState = DataControlRowState.Normal;
                gdvPagamentos.Rows[i].Visible = true;
            }
        }
   
        private List<pagAcumulado> novaListaPagMês(List<Pagamentos> listaPagamentos)
        {
            listaPagMes = new List<pagAcumulado>();
            int j = 0, y;
            y = DateTime.Today.Year - 1;
            j = DateTime.Today.Month;
            do
            {
                pagAcumulado pagMes = new pagAcumulado();

                for (int i = 0; i < listaPagamentos.Count; i++)
                {
                    if (listaPagamentos[i].dataPagamento > DateTime.Today.AddMonths(-11))
                    {
                        if (listaPagamentos[i].dataPagamento.Value.Month.Equals(j) && listaPagamentos[i].dataPagamento.Value.Year.Equals(y))
                        {
                            if (listaPagamentos[i].formaPag.Equals("Cartão Crédito"))
                            {
                                pagMes.qntFormaCartao++;
                                pagMes.qntTipoParcelado++;
                            }
                            else if (listaPagamentos[i].formaPag.Equals("Cheques"))
                            {
                                pagMes.qntFormaCheque++;

                                if (listaPagamentos[i].tipoPag.Equals("Vista"))
                                {
                                    pagMes.qntTipoVista++;
                                }
                                else if (listaPagamentos[i].tipoPag.Equals("Entrada + Parcelas") || listaPagamentos[i].tipoPag.Equals("Parcelado"))
                                {
                                    pagMes.qntTipoParcelado++;
                                }
                            }
                            else if (listaPagamentos[i].formaPag.Equals("cartao"))
                            {
                                pagMes.qntFormaCartao++;

                                if (listaPagamentos[i].tipoPag.Equals("Entrada"))
                                {
                                    pagMes.qntTipoVista++;
                                }
                                else if (listaPagamentos[i].tipoPag.Equals("Vista"))
                                {
                                    pagMes.qntTipoParcelado++;
                                }
                            }
                            else if (listaPagamentos[i].formaPag.Equals("creditos"))
                            {
                                pagMes.qntFormaCréditos++;

                                if (listaPagamentos[i].tipoPag.Equals("Entrada"))
                                {
                                    pagMes.qntTipoParcelado++;
                                }
                                else if (listaPagamentos[i].tipoPag.Equals("Vista"))
                                {
                                    pagMes.qntTipoVista++;
                                }
                            }

                            else if (listaPagamentos[i].formaPag.Equals("dinheiro"))
                            {
                                pagMes.qntFormaDinheiro++;
                                pagMes.qntTipoVista++;
                            }

                            pagMes.mês = listaPagamentos[i].dataPagamento.Value.Month.ToString() + "/" + listaPagamentos[i].dataPagamento.Value.Year.ToString();
                            pagMes.qntTotal++;
                            pagMes.valorTotal = pagMes.valorTotal + Convert.ToDecimal(listaPagamentos[i].valorParcela);
                        }
                    }
                }
                if (pagMes.mês != null)
                {
                    listaPagMes.Add(pagMes);
                }

                if (j == 12)
                {
                    j = 1;
                    y++;
                }
                else
                {
                    j++;
                }
            } while (j != DateTime.Today.Month || y != (DateTime.Today.Year + 1));
            return listaPagMes;
        }

        protected void rbMovimentacao_CheckedChanged(object sender, EventArgs e)
        {
            limpaRelatórios();

            pnlMovimento.Visible = true;

            listaMovimentos = controle.pesquisaMovimentosGeral();
            listaMovMes = novaListaMovMês(listaMovimentos);
            gdvMovimento.DataSource = listaMovMes;

            //gdvMovimento = new GridView();
            //pnlGdvMovimento.Controls.Add(gdvMovimento);            

            gdvMovimento.Columns[0].HeaderText = "Mês";
            gdvMovimento.Columns[0].ItemStyle.Width = 150;
            gdvMovimento.Columns[1].HeaderText = "Descrição";
            gdvMovimento.Columns[1].ItemStyle.Width = 250;
            gdvMovimento.Columns[2].HeaderText = "Tipo";
            gdvMovimento.Columns[2].ItemStyle.Width = 250;
            gdvMovimento.Columns[3].HeaderText = "Direção";
            gdvMovimento.Columns[3].ItemStyle.Width = 250;
            gdvMovimento.Columns[4].HeaderText = "Valor";
            gdvMovimento.Columns[4].ItemStyle.Width = 150;

            gdvMovimento.DataBind();

            for (int i = 0; i < listaMovMes.Count; i++)
            {

                GridViewRow newLine = new GridViewRow(gdvMovimento.Rows.Count + 1, gdvMovimento.Rows.Count + 1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                gdvMovimento.Controls.Add(newLine);

                gdvMovimento.Rows[i].Cells[0].Text = listaMovMes[i].mês;
                gdvMovimento.Rows[i].Cells[1].Text = listaMovMes[i].descricao;
                gdvMovimento.Rows[i].Cells[2].Text = listaMovMes[i].subtipo;
                gdvMovimento.Rows[i].Cells[3].Text = listaMovMes[i].direcao;
                gdvMovimento.Rows[i].Cells[4].Text = "R$ " + listaMovMes[i].valor.ToString();

                gdvMovimento.Rows[i].RowState = DataControlRowState.Normal;
                gdvMovimento.Rows[i].Visible = true;
            }


        }

        private movAcumulado acumuladoApagar(List<Movimentos> listaMovimentos)
        {
            
            movAcumulado movMes = new movAcumulado();

 
            
            return movMes;
        }    

        private List<movAcumulado> novaListaMovMês(List<Movimentos> listaMovimentos)
        {
            listaMovMes = new List<movAcumulado>();
            int j = 0, y;
            y = DateTime.Today.Year - 1;
            j = DateTime.Today.Month;
            do
            {
                movAcumulado movMes = new movAcumulado();

                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Contas a Pagar"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Água, Luz, Telefone, ..."))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Contas a Pagar"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Multas"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Abertura de Capital"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Capital Social Integralizado"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Abertura de Capital"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Capital Social Não Integralizado"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Estoque"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Material de Escritório"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Maquinário"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Veículo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Ferramentas"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Móveis"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Compra"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Imóvel"))
                                {
                                    {
                                        movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                        movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                        movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                        movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                        movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                    }
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Impostos a Recolher"))
                            {
                                movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Movimento Interno"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pró-Labore => Lucro"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Movimento Interno"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Caixa => Banco"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Movimento Interno"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Banco => Caixa"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Outras Despesas a Pagar"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Investimentos Longo Prazo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Outras Despesas a Pagar"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Investimento Curto Prazo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Outros a Receber"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Recebimento Longo Prazo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }

                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Outros a Receber"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Recebimento Curto Prazo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Pro-Labore"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Salario"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Despesas"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Contas"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Fornecedor"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Impostos"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Duplicatas"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Duplicatas LP"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Pagamento"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pag. Contas LP"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Retirada"))
                            {
                                movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Salário"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Funcionários"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Salário"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Ajudantes"))
                                {
                                    {
                                        movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                        movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                        movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                        movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                        movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                    }
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Venda"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Clientes Curto Prazo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Venda"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Clientes Longo Prazo"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Venda"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Clientes à Vista"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Venda"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Venda de Créditos ao Cliente"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Venda"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Pré-Paga"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }
                for (int i = 0; i < listaMovimentos.Count; i++)
                {
                    if (listaMovimentos[i].data > DateTime.Today.AddMonths(-11))
                    {
                        if (listaMovimentos[i].data.Value.Month.Equals(j) && listaMovimentos[i].data.Value.Year.Equals(y))
                        {
                            if (listaMovimentos[i].Tipos_Movimentacao.descricao.Equals("Venda"))
                            {
                                if (listaMovimentos[i].Tipos_Movimentacao.sub_tipo.Equals("Estoque"))
                                {
                                    movMes.descricao = listaMovimentos[i].Tipos_Movimentacao.descricao;
                                    movMes.direcao = listaMovimentos[i].Tipos_Movimentacao.direcao;
                                    movMes.subtipo = listaMovimentos[i].Tipos_Movimentacao.sub_tipo;
                                    movMes.mês = listaMovimentos[i].data.Value.Month.ToString() + "/" + listaMovimentos[i].data.Value.Year.ToString();
                                    movMes.valor = movMes.valor + Convert.ToDecimal(listaMovimentos[i].valor);
                                }
                            }
                        }
                    }
                }
                if (movMes.mês != null && movMes.valor != 0)
                {
                    listaMovMes.Add(movMes);
                    movMes.valor = 0;
                }                          

                if (j == 12)
                {
                    j = 1;
                    y++;
                }
                else
                {
                    j++;
                }
            } while (j != DateTime.Today.Month || y != (DateTime.Today.Year + 6));
            return listaMovMes;
        }
    }
}