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
    public partial class PainelVendas : System.Web.UI.Page
    {
        //Variaveis de validação       
        Valida teste = new Valida();
        static int id;
        static int perfil = 0;

        //Variaveis de tipos da base de dados
        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        static Clientes cliente = new Clientes();
        List<Clientes> ListaClientes = new List<Clientes>();
        static Produtos produto = new Produtos();
        static List<Produtos> ListaProd = new List<Produtos>();
        List<Produtos> PesquisaProd = new List<Produtos>();
        static Vendas venda = new Vendas();
        static Pagamentos pagamento = new Pagamentos();

        //variaveis internas
        static List<int> QntProd = new List<int>();
        static bool clienteCadastrado = false;
        static bool notaPaulista = false;
        static string nfpCpf = null;
        static string nfpCnpj = null;
        static double icmsTotal;
        static Color corPadrao;

        //variaveis associadas as regras de negócio, deverão ser editaveis por meio de base de dados gerencial

        //aliquota percentual de ICMS vigente
        static double percICMS = 18;
        //desconto automatico, respectivamente Valor para conceção, percentual e limite máximo de desconto
        static int autoDescVal = 200;
        static double autoDescPerc = 7;
        static double maxDescPerc = 10;
        //desconto manual
        static double descManual = 0;
        //taxa de juros para parcelamento  
        static double jurosPrazo3x = 2.25;
        static double jurosPrazo4x = 4.3;
        static double jurosPrazo5x = 5.45;
        static double jurosPrazo6x = 6.75;
        static double jurosPrazo7x = 7.85;
        static double jurosPrazo8x = 8;
        static double jurosPrazo9x = 9.15;
        static double jurosPrazo10x = 10.25;
        static double jurosPrazo11x = 11.3;
        static double jurosPrazo12x = 12.45;
        //taxa de juros para parcelamento com cheque
        static double jurosCheque2x = 3;
        static double jurosCheque3x = 4.25;
        static double jurosCheque4x = 5.5;
        static double jurosCheque5x = 6.75;
        static double jurosCheque6x = 8;
        static double jurosCheque7x = 9.25;
        static double jurosCheque8x = 10.5;
        static double jurosCheque9x = 11.75;
        static double jurosCheque10x = 13;
        static double jurosCheque11x = 14.25;
        static double jurosCheque12x = 15.5;

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlPrincipal.DefaultButton = btnCodigo.ID;
            txtCodigo.Focus();

            if (!IsPostBack)
            {
                corPadrao = pnlPrincipal.BackColor;

                txtDesconto.Text = "0";

                produto = new Produtos();
                ListaProd = new List<Produtos>();

                //coversão de inteiro para percentual
                autoDescPerc = autoDescPerc / 100;
                maxDescPerc = maxDescPerc / 100;
                percICMS = percICMS / 100;

                //atribuição de texto as label's de titulo dos paineis
                lblPanelDescontos.Text = "Atribuir Desconto ao Total";
                lblMensagem.Text = "  Sistema de vendas - Alemão da Construção";

                /*/validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);
                    lblUser.Text = "Operador : " + user.nome;
                    if (!teste.ValidaUser(id, perfil) || user.num_perfil != 1)
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }
                    
                }*/
            }
        }

        protected void btnCodigo_Click(object sender, EventArgs e)
        {
            //Verifica se o campo com o código do produto foi digitado
            if (txtCodigo.Text.Equals(""))
            {

            }
            //em caso positivo armazena o produto referente ao código ou nulo
            else
            {               
                produto = controle.pesquisaProdutoCod(txtCodigo.Text);
            }

            //Verifica se existe um produto valido referente ao código
            if (produto == null)
            {
                //em caso negativo limpa os campos e informa o usuário
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('O código inserido não corresponde a nenhum produto cadastrado, tente novamente, ou solicite o cadastro deste produto');", true);
                txtCodigo.Text = "";
                txtQuantidade.Text = "";
            }
            else
            {
                btnCorrigir.Enabled = true;
                btnVender.Enabled = true;

                //validação de inserção repetida de produto
                bool existe = false;
                //validação de quantidade
                int qntTemp = 0;
                bool esgotado = false;

                if (txtQuantidade.Text.Equals(""))
                {
                    qntTemp = 1;                    
                }
                else
                {
                    qntTemp = Convert.ToInt32(txtQuantidade.Text);
                }

                if (produto.qnt_estoque < qntTemp)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Desculpe, no momento possuímos " + produto.qnt_estoque + " unidade(s) de " + produto.desc_produto + ".');", true);
                }
                else
                {
                    for (int i = 0; i < ListaProd.Count; i++)
                    {
                        if (ListaProd[i].id == produto.id)
                        {
                            if (produto.qnt_estoque < QntProd[i] + qntTemp)
                            {
                                esgotado = true;
                                qntTemp = QntProd[i];
                            }
                            else
                            {
                                if (txtQuantidade.Text.Equals(""))
                                {
                                    QntProd[i] = QntProd[i] + 1;
                                    qntTemp = 1;
                                    icmsTotal = icmsTotal + Convert.ToDouble(-produto.ICMS_pago * qntTemp);

                                }
                                else
                                {
                                    QntProd[i] = QntProd[i] + Convert.ToInt32(txtQuantidade.Text);
                                    qntTemp = Convert.ToInt32(txtQuantidade.Text);
                                    icmsTotal = icmsTotal + Convert.ToDouble(-produto.ICMS_pago * qntTemp);

                                }
                                existe = true;
                            }
                        }
                    }
                    if (esgotado)
                    {
                        esgotado = false;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Desculpe, no momento possuímos " + (produto.qnt_estoque - qntTemp) + " unidade(s) de " + produto.desc_produto + ".');", true);
                    }
                    else
                    {
                        if (!existe)
                        {
                            ListaProd.Add(produto);
                            QntProd.Add(qntTemp);
                            icmsTotal = icmsTotal + Convert.ToDouble(-produto.ICMS_pago * qntTemp);
                        }

                        txtSubTotal.Text = (Convert.ToDecimal(txtSubTotal.Text) + qntTemp * Convert.ToDecimal(produto.preco_venda)).ToString("0.00");

                        if (descManual > 0)
                        {
                            txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * descManual))).ToString("0.00");
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtSubTotal.Text) > autoDescVal)
                            {
                                txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * autoDescPerc))).ToString("0.00");
                            }
                            else
                            {
                                txtTotalVista.Text = txtSubTotal.Text;
                            }
                        }
                        preencheDataListVendas(ListaProd);
                        AdicionaCheckListVendas(ListaProd);
                        txtCodigo.Text = "";
                        txtQuantidade.Text = "";
                    }
                }
            }
        }

        private void preencheDataListVendas(List<Produtos> ListaProd)
        {
            dlProdVenda.DataSource = ListaProd;
            dlProdVenda.DataTextField = "desc_produto";
            dlProdVenda.DataValueField = "id";
            dlProdVenda.DataBind();
            dlProdValor.DataSource = ListaProd;
            dlProdValor.DataTextField = "preco_venda";
            dlProdValor.DataValueField = "id";
            dlProdValor.DataBind();
            dlProdQnt.DataSource = QntProd;
            dlProdQnt.DataBind();
            for (int i = 0; i < ListaProd.Count; i++)
            {
                string texto = "";
                if (i < 9)
                {
                    texto = "0" + (i + 1).ToString() + " - " + ListaProd[i].desc_produto;                                        
                }

                else
                {
                    texto = (i + 1).ToString() + " - " + ListaProd[i].desc_produto;                                        
                }
                for (int j = 0; j < 300; j++)
                {
                    texto = texto + ".";
                }
                
                dlProdVenda.Items[i].Text = texto;
                dlProdQnt.Items[i].Text = "Qnt " + QntProd[i].ToString() + "..............";
                dlProdValor.Items[i].Text = (QntProd[i] * Convert.ToDecimal(dlProdValor.Items[i].Text)).ToString("0.00");
            }

        }

        private void AdicionaCheckListVendas(List<Produtos> ListaProd)
        {            
            chkProdVenda.DataSource = ListaProd;
            chkProdVenda.DataTextField = "desc_produto";
            chkProdVenda.DataValueField = "id";
            chkProdVenda.DataBind();

            for (int i = 0; i < ListaProd.Count; i++)
            {
                string texto = "";
                if (i < 9)
                {
                    texto = "0" + (i + 1).ToString() + " - " + ListaProd[i].desc_produto;
                }

                else
                {
                    texto = (i + 1).ToString() + " - " + ListaProd[i].desc_produto;
                }                
                texto = texto + " ....... Qnt : ";                
                texto = texto + QntProd[i].ToString() + " ....... Valor : " + Convert.ToDecimal(dlProdValor.Items[i].Text).ToString("0.00");
                chkProdVenda.Items[i].Text = texto;                        
            }

        }

        protected void btnCorrigir_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Selecione os produtos a serem removidos e clique em \"Remover Selecionados\".');", true);

                dlProdVenda.Visible = false;
                dlProdValor.Visible = false;
                dlProdQnt.Visible = false;
                chkProdVenda.Visible = true;
                pnlRelacaoProdutos.Visible = true;
                btnCorrigir.Visible = false;
                btnRemover.Visible = true;

                txtCodigo.Enabled = false;
                txtQuantidade.Enabled = false;
                txtPesquisaProd.Enabled = false;
                btnCodigo.Enabled = false;
                btnPesquisa.Enabled = false;

                btnVender.Enabled = false;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Não foi possivel realizar a instrução');", true);
            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            dlProdVenda.Visible = true;
            dlProdValor.Visible = true;
            dlProdQnt.Visible = true;
            chkProdVenda.Visible = false;
            pnlRelacaoProdutos.Visible = false;
            btnCorrigir.Visible = true;
            btnRemover.Visible = false;

            txtCodigo.Enabled = true;
            txtQuantidade.Enabled = true;
            txtPesquisaProd.Enabled = true;
            btnCodigo.Enabled = true;
            btnPesquisa.Enabled = true;
            btnVender.Enabled = true;

            bool teste = false;
            for (int i = 0; i < ListaProd.Count; i++)
            {
                if (teste)
                {
                    teste = false;
                    i--;
                }
                if (chkProdVenda.Items[i].Selected)
                {
                    txtSubTotal.Text = (Convert.ToDecimal(txtSubTotal.Text) - QntProd[i] * Convert.ToDecimal(ListaProd[i].preco_venda)).ToString("0.00");
                    ListaProd.RemoveAt(i);
                    QntProd.RemoveAt(i);

                    if (descManual > 0)
                    {
                        txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * descManual))).ToString("0.00");
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtSubTotal.Text) > autoDescVal)
                        {
                            txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * autoDescPerc))).ToString("0.00");
                        }
                        else
                        {
                            txtTotalVista.Text = txtSubTotal.Text;
                        }
                    }

                    teste = true;
                }                                
            }                        
            if(ListaProd.Count == 0)
            {
                btnVender.Enabled = false;
                btnCorrigir.Enabled = false;
            }
            preencheDataListVendas(ListaProd);
            AdicionaCheckListVendas(ListaProd);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }

        protected void btnCliente_Click(object sender, EventArgs e)
        {
            clienteCadastrado = true;
            pnlCliente.Visible = true;
            btnCliente.Enabled = false;
            lblEspaçoButton.Visible = false;
            pnlPrincipal.DefaultButton = btnPesquisaClientes.ID;
        }

        protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
        {            
            //pesquisa e preenchimento de drop down list
            PesquisaProd = controle.pesquisaProdutosValidoNome(txtPesquisaProd.Text);

            if (PesquisaProd.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Não existem produtos com a expressão \"" + txtPesquisaProd.Text + "\".');", true);
                txtPesquisaProd.Text = "";
            }
            else
            {
                btnPesquisa.Visible = false;
                txtPesquisaProd.Visible = false;
                ddlPesquisaProd.Visible = true;

                ddlPesquisaProd.Items.Clear();

                ddlPesquisaProd.Items.Add("Selecione um dos itens");
                ddlPesquisaProd.Items[0].Text = "Selecione um dos itens";
                int x = 1;
                foreach (Produtos value in PesquisaProd)
                {
                    ddlPesquisaProd.Items.Add("desc_produto");
                    ddlPesquisaProd.Items[x].Value = value.id.ToString();
                    ddlPesquisaProd.Items[x].Text = PesquisaProd[x - 1].desc_produto + " - R$" + Convert.ToDecimal(PesquisaProd[x - 1].preco_venda).ToString("0.00");
                    x++;
                }
            }

        }

        protected void ddlPesquisaProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantidade.Focus();
            pnlPrincipal.DefaultButton = btnCodigo.ID;
            produto = controle.pesquisaProdutoId(Convert.ToInt32(ddlPesquisaProd.SelectedValue));
            txtCodigo.Text = produto.cod_produto;
            btnPesquisa.Visible = true;
            ddlPesquisaProd.Visible = false;
            txtPesquisaProd.Visible = true;
            txtPesquisaProd.Text = "";
        }

        protected void btnPesquisaClientes_Click(object sender, ImageClickEventArgs e)
        {
            lblEspaçoButton.Visible = true;
            txtPesquisaCliente.Visible = false;
            btnPesquisaClientes.Visible = false;
            ddlClientes.Visible = true;

            ListaClientes = controle.pesquisaClientesCompleta(txtPesquisaCliente.Text);

            ddlClientes.Items.Clear();
            ddlClientes.Items.Add("Selecione o cliente");
            ddlClientes.Items[0].Text = "Selecione o cliente";
            int x = 1;

            foreach (Clientes value in ListaClientes)
            {
                ddlClientes.Items.Add("nome");
                ddlClientes.Items[x].Value = value.id.ToString();
                ddlClientes.Items[x].Text = ListaClientes[x - 1].nome + " - CPF/CNPJ : " + ListaClientes[x - 1].cpf;
                x++;
            }
        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cliente = controle.pesquisaClienteById(Convert.ToInt32(ddlClientes.SelectedValue));                   
            ddlClientes.Visible = false;
            txtPesquisaCliente.Visible = true;
            txtPesquisaCliente.Enabled = false;
            txtPesquisaCliente.Text = "";
            txtCliente.Text = cliente.nome;
            txtCpf.Text = cliente.cpf;
            txtCpf.Enabled = false;
            txtRg.Text = cliente.rg;
            txtContato.Text = cliente.contato;
            txtTelefone.Text = cliente.telefone;
            txtCelular.Text = cliente.celular;
            txtTelefone.Text = cliente.telefone;
            txtEndereco.Text = cliente.endereço;
            txtNumero.Text = cliente.numeral;
            txtBairro.Text = cliente.bairro;
            txtCreditos.Text = Convert.ToDecimal(cliente.creditos).ToString("0.00");
            txtCidade.Text = cliente.Cidades.cidade + " / " + cliente.Cidades.Estados.sigla;
        }

        protected void btnCancelCliente_Click(object sender, EventArgs e)
        {
            clienteCadastrado = false;
            btnPesquisaClientes.Visible = true;
            btnCliente.Enabled = true;
            pnlCliente.Visible = false;
            ddlClientes.Visible = false;
            txtPesquisaCliente.Visible = true;
            txtPesquisaCliente.Enabled = true;
            txtPesquisaCliente.Text = "";
            txtCliente.Text = "";
            txtCpf.Text = "";
            txtRg.Text = "";
            txtContato.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtTelefone.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtCreditos.Text = "";
            txtCidade.Text = "";
        }

        protected void btnDesconto_Click(object sender, EventArgs e)
        {
            txtDesconto.Text = descManual.ToString();
            pnlDescontos.Visible = true;
            pnlBasico.Visible = false;
            pnlAdjacente.Visible = false;
            pnlCliente.Visible = false;                        
        }

        protected void btnAceitaDesc_Click(object sender, EventArgs e)
        {
            descManual = Convert.ToDouble(txtDesconto.Text) / 100;
            txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * descManual))).ToString("0.00");
            pnlDescontos.Visible = false;
            pnlBasico.Visible = true;
            pnlAdjacente.Visible = true;
            if (clienteCadastrado)
            {
                pnlCliente.Visible = true;
            }
        }

        protected void btnCancelDesc_Click(object sender, EventArgs e)
        {
            txtDesconto.Text = "0";
            pnlDescontos.Visible = false;
            pnlBasico.Visible = true;
            pnlAdjacente.Visible = true;
            if (clienteCadastrado)
            {
                pnlCliente.Visible = true;
            }
            
        }

        protected void btnVender_Click(object sender, EventArgs e)
        {
            pnlPagamento.Visible = true;
            pnlAdjacente.Visible = false;
            pnlBasico.Visible = false;
            pnlCliente.Visible = false;

            if (clienteCadastrado)
            {
                btnNotaPaulista.Enabled = false;
            }

            lblValorVista.Text = txtTotalVista.Text;
            lblValorCheque.Text = txtSubTotal.Text;
            lblValorPrazo.Text = txtSubTotal.Text;

            lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 2).ToString("0.00");
            lblValorParcelaCheque.Text = lblValorCheque.Text;
            ddlQntCheques.SelectedIndex = 0;
            ddlParcelas.SelectedIndex = 0;
        }

        protected void rdbVista_CheckedChanged(object sender, EventArgs e)
        {
            pnlPrazo.BackColor = Color.LightGray;
            pnlVista.BackColor = corPadrao;
            pnlCheque.BackColor = Color.LightGray;
            btnAceitarPag.Enabled = true;

            ddlParcelas.Enabled = false;
            ddlFormaVista.Enabled = true;
            btnReceber.Enabled = false;
            txtRecebido.Enabled = false;           
            ddlQntCheques.Enabled = false;
            txtPrimeiroCheque.Enabled = false;
            txtUltimoCheque.Enabled = false;
        }

        protected void rdbPrazo_CheckedChanged(object sender, EventArgs e)
        {
            pnlPrazo.BackColor = corPadrao;
            pnlVista.BackColor = Color.LightGray;
            pnlCheque.BackColor = Color.LightGray;
            btnAceitarPag.Enabled = true;

            ddlParcelas.Enabled = true;
            ddlFormaVista.Enabled = false;
            btnReceber.Enabled = false;
            txtRecebido.Enabled = false;
            ddlQntCheques.Enabled = false;
            txtPrimeiroCheque.Enabled = false;
            txtUltimoCheque.Enabled = false;
        }

        protected void rdbCheque_CheckedChanged(object sender, EventArgs e)
        {
            pnlPrazo.BackColor = Color.LightGray;
            pnlVista.BackColor = Color.LightGray;
            pnlCheque.BackColor = corPadrao;
            btnAceitarPag.Enabled = true;

            ddlParcelas.Enabled = false;
            ddlFormaVista.Enabled = false;
            btnReceber.Enabled = false;
            txtRecebido.Enabled = false;

            ddlQntCheques.Enabled = true;
            lblNumPrimeiro.Visible = true;
            txtPrimeiroCheque.Enabled = true;
            txtUltimoCheque.Enabled = true;
            txtPrimeiroCheque.Visible = true;
            if(ddlQntCheques.SelectedIndex > 0)
            {
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
        }

        protected void btnReceber_Click(object sender, EventArgs e)
        {
            lblValorTroco.Text = (Convert.ToDecimal(txtRecebido.Text) - Convert.ToDecimal(lblValorVista.Text)).ToString("0.00");
        }

        protected void btnContinuarCompra_Click(object sender, EventArgs e)
        {
            pnlPagamento.Visible = false;
            pnlAdjacente.Visible = true;
            pnlBasico.Visible = true;

            if (clienteCadastrado)
            {
                pnlCliente.Visible = true;
            }
        }

        protected void btnAceitarPag_Click(object sender, EventArgs e)
        {
            if (rdbVista.Checked)
            {
                if (Convert.ToDecimal(txtRecebido.Text) < Convert.ToDecimal(lblValorVista.Text))
                {

                }
                else
                {
                    venda = new Vendas();
                    controle.salvarVenda(venda);
                    venda.ICMS = icmsTotal + (percICMS * Convert.ToDouble(lblValorVista.Text));
                    venda.cfp = txtCpf.Text;
                    controle.salvaAtualiza();
                }
            }

            else if(rdbPrazo.Checked)
            {


            }

            else if (rdbCheque.Checked)
            {


            }
        }

        protected void btnCancelaPag_Click(object sender, EventArgs e)
        {
            Response.Flush();
            Response.Redirect("~/View/PainelVendas.aspx");
        }

        protected void ddlParcelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlParcelas.SelectedValue.Equals("2"))
            {
                lblValorPrazo.Text = Convert.ToDecimal(txtSubTotal.Text).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 2).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("3"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo3x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 3).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("4"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo4x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 4).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("5"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo5x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 5).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("6"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo6x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 6).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("7"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo7x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 7).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("8"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo8x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 8).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("9"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo9x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 9).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("10"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo10x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 10).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("11"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo11x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 11).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("12"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosPrazo12x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 12).ToString("0.00");
            }
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            txtQuantidade.Focus();
        }

        protected void ddlFormaVista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Dinheiro
            if (ddlFormaVista.SelectedIndex == 1)
            {
                lblRecebido.Visible = true;
                btnReceber.Visible = true;
                txtRecebido.Visible = true;
                btnReceber.Enabled = true;
                txtRecebido.Enabled = true;
                lblTroco.Visible = true;
                lblValorTroco.Visible = true;
            }
            //Cartão
            else if(ddlFormaVista.SelectedIndex == 2)
            {
                lblRecebido.Visible = true;
                btnReceber.Visible = true;
                txtRecebido.Visible = true;
                btnReceber.Enabled = true;
                txtRecebido.Enabled = true;
                lblTroco.Visible = false;
                lblValorTroco.Visible = false;
            }
            //Créditos
            else if (ddlFormaVista.SelectedIndex == 3)
            {
                lblRecebido.Visible = true;
                btnReceber.Visible = true;
                txtRecebido.Visible = true;
                btnReceber.Enabled = true;
                txtRecebido.Enabled = true;
                lblTroco.Visible = false;
                lblValorTroco.Visible = false;
            }
        }

        protected void ddlQntCheques_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlQntCheques.SelectedValue.Equals("1"))
            {
                lblValorCheque.Text = Convert.ToDecimal(txtSubTotal.Text).ToString("0.00");
                lblValorParcelaCheque.Text = lblValorCheque.Text;
                lblNumUltimo.Visible = false;
                txtUltimoCheque.Visible = false;
            }
            if (ddlQntCheques.SelectedValue.Equals("2"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque2x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 2).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("3"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque3x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 3).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("4"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque4x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 4).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("5"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque5x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 5).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("6"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque6x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 6).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("7"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque7x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 7).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("8"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque8x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 8).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("9"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque9x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 9).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("10"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque10x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 10).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("11"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque11x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 11).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("12"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(txtSubTotal.Text) + (Convert.ToDecimal(txtSubTotal.Text) * Convert.ToDecimal(jurosCheque12x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 12).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
        }

        protected void btnNotaPaulista_Click(object sender, EventArgs e)
        {            
            btnNotaPaulista.Enabled = false;
            pnlPagamento.Visible = false;
            pnlCpfCnpj.Visible = true;
            lblCpfCnpj.Text = "Inclusão de CPF/CNPJ para NF Paulista";
        }

        protected void rdbCpf_CheckedChanged(object sender, EventArgs e)
        {
            if (clienteCadastrado)
            {
                txtNpCpf.Text = txtCpf.Text;
            }
            txtNpCpf.Enabled = true;
            txtNpCnpj.Enabled = false;
        }

        protected void rdbCnpj_CheckedChanged(object sender, EventArgs e)
        {
            if (clienteCadastrado)
            {
                txtNpCnpj.Text = txtCpf.Text;
            }
            txtNpCpf.Enabled = false;
            txtNpCnpj.Enabled = true;
        }

        protected void btnOkNp_Click(object sender, EventArgs e)
        {
            notaPaulista = true;
            nfpCnpj = txtNpCnpj.Text;
            nfpCpf = txtNpCpf.Text;
            pnlPagamento.Visible = true;
            pnlCpfCnpj.Visible = false;
        }

        protected void btnCancNp_Click(object sender, EventArgs e)
        {
            pnlPagamento.Visible = true;
            txtNpCpf.Enabled = false;
            txtNpCnpj.Enabled = false;
            pnlCpfCnpj.Visible = false;
            txtNpCnpj.Text = "";
            txtNpCpf.Text = "";
            rdbCnpj.Checked = false;
            rdbCpf.Checked = false;
        }
    }
}