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
        static Usuarios user = new Usuarios();
        static Clientes cliente = new Clientes();
        List<Clientes> ListaClientes = new List<Clientes>();
        static Produtos produto = new Produtos();
        static List<Produtos> ListaProd = new List<Produtos>();
        List<Produtos> PesquisaProd = new List<Produtos>();
        static Vendas venda = new Vendas();
        static Pagamentos pagamento = new Pagamentos();
        static Vendas_Produtos prodVendido = new Vendas_Produtos();
        static Gerenciamento gerencia = new Gerenciamento();

        //variaveis internas
        static List<int> QntProd = new List<int>();
        static bool clienteCadastrado = false;
        static bool notaPaulista = false;
        static string nfpCpf = null;
        static string nfpCnpj = null;
        static double icmsTotal;
        static Color corPadrao;
        bool existe = false;
        int qntTemp = 0;
        bool esgotado = false;
        static bool pessoaFisica = true;
        static double valorDevido = 0.00;
        static bool entrada = false;
        //desconto manual
        static double descManual = 0;


        //variaveis associadas as regras de negócio, deverão ser editaveis por meio de base de dados gerencial        

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlPrincipal.DefaultButton = btnCodigo.ID;
            txtCodigo.Focus();

            if (!IsPostBack)
            {
                corPadrao = pnlPrincipal.BackColor;
                txtDesconto.Text = "0";

                situacaoInicial();
                Response.Clear();

                produto = new Produtos();
                ListaProd = new List<Produtos>();
                gerencia = controle.pesquisaGerenciamento(1);

                //atribuição de texto as label's de titulo dos paineis
                lblPanelDescontos.Text = "Atribuir Desconto ao Total";
                lblMensagem.Text = "  Sistema de vendas - Alemão da Construção";

                //validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);
                    lblUser.Text = user.nome;
                    if (!teste.ValidaUser(id, perfil) || user.num_perfil == 4)
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
                existe = false;
                //validação de quantidade
                qntTemp = 0;
                esgotado = false;

                if (txtQuantidade.Text.Equals(""))
                {
                    qntTemp = 1;                    
                }
                else
                {
                    qntTemp = Convert.ToInt32(txtQuantidade.Text);
                }

                if (produto.Estoque.qnt_atual < qntTemp)
                {
                    txtQuantidade.Text = "";
                    txtCodigo.Text = "";
                    txtCodigo.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Desculpe, no momento possuímos " + produto.Estoque.qnt_atual + " unidade(s) de " + produto.desc_produto + ".');", true);                    
                }
                else
                {
                    for (int i = 0; i < ListaProd.Count; i++)
                    {
                        if (ListaProd[i].id == produto.id)
                        {
                            if (produto.Estoque.qnt_atual < QntProd[i] + qntTemp)
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
                                }
                                else
                                {
                                    QntProd[i] = QntProd[i] + Convert.ToInt32(txtQuantidade.Text);
                                    qntTemp = Convert.ToInt32(txtQuantidade.Text);                                    
                                }
                                existe = true;
                            }
                        }
                    }
                    if (esgotado)
                    {
                        esgotado = false;
                        txtCodigo.Text = "";
                        txtQuantidade.Text = "";
                        pnlPrincipal.DefaultButton = btnCodigo.ID;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Desculpe, no momento possuímos " + (produto.Estoque.qnt_atual - qntTemp) + " unidade(s) de " + produto.desc_produto + ".');", true);
                    }
                    else
                    {
                        if (!existe)
                        {
                            ListaProd.Add(produto);
                            QntProd.Add(qntTemp);                            
                        }

                        txtSubTotal.Text = (Convert.ToDecimal(txtSubTotal.Text) + qntTemp * Convert.ToDecimal(produto.preco_venda)).ToString("0.00");

                        if (descManual > 0)
                        {
                            txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * descManual))).ToString("0.00");
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtSubTotal.Text) > gerencia.autoDescValor)
                            {
                                txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * gerencia.autoDescPerc))).ToString("0.00");
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
            icmsTotal = 0;
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
                icmsTotal = icmsTotal + Convert.ToDouble(-ListaProd[i].ICMS_pago * QntProd[i]);
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
                        if (Convert.ToDecimal(txtSubTotal.Text) > gerencia.autoDescValor)
                        {
                            txtTotalVista.Text = (Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * gerencia.autoDescPerc))).ToString("0.00");
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
            //lblEspaçoButton.Visible = false;
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
                    ddlPesquisaProd.Items[x].Text = PesquisaProd[x - 1].desc_produto + " - Compra R$" + Convert.ToDecimal(PesquisaProd[x - 1].preco_compra).ToString("0.00") + " - Venda R$" + Convert.ToDecimal(PesquisaProd[x - 1].preco_venda).ToString("0.00");
                    x++;
                }
            }

        }

        protected void ddlPesquisaProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtQuantidade.Focus();            
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
            //lblEspaçoButton.Visible = true;
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
            txtPesquisaCliente.Text = "";
            txtCliente.Text = cliente.nome;
            txtCpf.Text = cliente.cpf;
            if (cliente.pessoa_fisica == 1)
            {
                rdbFisica.Checked = true;
            }
            else
            {
                rdbJuridica.Checked = true;
            }
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
            if (gerencia.maxDescPerc < descManual + gerencia.autoDescPerc)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Os descontos cedidos excedem o desconto máximo de "+ gerencia.maxDescPerc + ", solicite liberação a um administrador.');", true);
            }
            else
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

                //valorDevido = Convert.ToDouble(txtSubTotal.Text);
                venda = new Vendas();
                controle.salvarVenda(venda);
                venda.data_Venda = DateTime.Today;
                venda.id_Cliente = cliente.id;
                venda.id_Usuario = user.id;
                if (clienteCadastrado)
                {
                    if (pessoaFisica)
                    {
                        venda.cnpj = "";
                        venda.cpf = cliente.cpf;
                    }
                    else
                    {
                        venda.cnpj = cliente.cpf;
                        venda.cpf = "";
                    }
                }
                venda.desconto = Convert.ToInt32(txtDesconto.Text);
                venda.comissao = 0;
                venda.ICMS = icmsTotal;
                venda.valor_Venda = 0;
                controle.salvaAtualiza();
            }
        }

        protected void rdbVista_CheckedChanged(object sender, EventArgs e)
        {
            valorDevido = Convert.ToDouble(lblValorVista.Text);
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
            valorDevido = Convert.ToDouble(lblValorPrazo.Text);
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
            valorDevido = Convert.ToDouble(lblValorCheque.Text);
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
            if (txtRecebido.Text.Equals(""))
            {

            }
            else if (Convert.ToDecimal(txtRecebido.Text) > Convert.ToDecimal(lblValorVista.Text))
            {
                lblValorTroco.Text = (Convert.ToDecimal(txtRecebido.Text) - Convert.ToDecimal(lblValorVista.Text)).ToString("0.00");
            }                 
        }

        protected void btnContinuarCompra_Click(object sender, EventArgs e)
        {
            btnRemover.Enabled = false;
            pnlPagamento.Visible = false;
            pnlAdjacente.Visible = true;
            pnlBasico.Visible = true;

            if (clienteCadastrado)
            {
                pnlCliente.Visible = true;
            }

            if (rdbVista.Checked)
            {
                txtTotalVista.Text = valorDevido.ToString("0.00");
                txtSubTotal.Text = (valorDevido + valorDevido * 0.07).ToString("0.00");
            }
            else
            {
                txtSubTotal.Text = valorDevido.ToString("0.00");
            }
        }

        protected void btnAceitarPag_Click(object sender, EventArgs e)
        {
            try
            {                                
                string saldo;

                if (rdbVista.Checked)
                {
                    pagamento = new Pagamentos();
                    controle.salvarPagamento(pagamento);
                    pagamento.valorTotal = Convert.ToDecimal(lblValorVista.Text);

                    if (ddlFormaVista.SelectedIndex == 3)
                    {
                        if (clienteCadastrado)
                        {
                            if (cliente.creditos < valorDevido)
                            {
                                pagamento.numParcela = 0;
                                saldo = (valorDevido - Convert.ToDouble(cliente.creditos)).ToString("0,00");
                                pagamento.valorParcela = Convert.ToDecimal(cliente.creditos);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Créditos debitados insuficientes! Saldo a receber de R$" + saldo + ".');", true);
                                cliente.creditos = 0.00;
                                valorDevido = Convert.ToDouble(saldo);
                                pagamento.tipoPag = "Entrada";
                                venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                                venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));                       
                                venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);
                                entrada = true;
                            }
                            else
                            {
                                pagamento.numParcela = 1;
                                pagamento.valorParcela = Convert.ToDecimal(valorDevido);
                                pagamento.tipoPag = "Vista";
                                cliente.creditos = cliente.creditos - valorDevido;
                                venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                                venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                                venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Créditos debitados! Seu saldo atual é de R$" + cliente.creditos + ".');", true);
                                valorDevido = 0;
                                entrada = false;
                            }
                            pagamento.numChequePrimeiro = "";
                            pagamento.numChequeUltimo = "";
                            pagamento.qntParcelas = 1;
                            pagamento.id_venda = venda.id;
                            pagamento.dataPagamento = DateTime.Today;
                            pagamento.formaPag = ddlFormaVista.SelectedValue;
                            controle.salvaAtualiza();

                            lblValorVista.Text = valorDevido.ToString("0.00");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Para a utilização de créditos é necessário associar cliente!!!');", true);
                        }
                    }

                    else if (ddlFormaVista.SelectedIndex == 1)
                    {
                        if (Convert.ToDecimal(txtRecebido.Text) < Convert.ToDecimal(valorDevido))
                        {
                            pagamento.numParcela = 0;
                            saldo = (valorDevido - Convert.ToDouble(txtRecebido.Text)).ToString("0.00");
                            pagamento.valorParcela = Convert.ToDecimal(txtRecebido.Text);                                                        
                            valorDevido = Convert.ToDouble(saldo);                           
                            pagamento.tipoPag = "Entrada";
                            venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                            venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                            venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Valor recebido insuficientes! Saldo a receber de R$" + saldo + ".');", true);
                            entrada = true;
                        }
                        else
                        {
                            pagamento.numParcela = 1;
                            pagamento.valorParcela = Convert.ToDecimal(valorDevido);
                            pagamento.tipoPag = "Vista";
                            lblValorTroco.Text = (Convert.ToDouble(txtRecebido.Text) - valorDevido).ToString("0.00");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Valor recebido pelo sistema de R$" + Convert.ToDecimal(txtRecebido.Text).ToString("0.00") + ", Troco devido de R$" + lblValorTroco.Text + ".');", true);
                            valorDevido = 0;
                            venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                            venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                            venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);
                            entrada = false;
                        }
                        pagamento.numChequePrimeiro = "";
                        pagamento.numChequeUltimo = "";
                        pagamento.qntParcelas = 1;
                        pagamento.id_venda = venda.id;
                        pagamento.dataPagamento = DateTime.Today;
                        pagamento.formaPag = ddlFormaVista.SelectedValue;
                        controle.salvaAtualiza();
                        txtRecebido.Text = "0,00";
                        lblValorVista.Text = valorDevido.ToString("0.00");
                    }

                    else if (ddlFormaVista.SelectedIndex == 2)
                    {
                        if (Convert.ToDecimal(txtRecebido.Text) < Convert.ToDecimal(valorDevido))
                        {
                            pagamento.numParcela = 0;
                            saldo = (valorDevido - Convert.ToDouble(txtRecebido.Text)).ToString("0,00");
                            pagamento.valorParcela = Convert.ToDecimal(txtRecebido.Text);
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Valor recebido insuficientes! Saldo a receber de R$" + saldo + ".');", true);
                            cliente.creditos = 0.00;
                            valorDevido = Convert.ToDouble(saldo);
                            pagamento.tipoPag = "Entrada";
                            venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                            venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                            venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);
                            entrada = true;
                        }
                        else
                        {
                            pagamento.numParcela = 1;
                            pagamento.valorParcela = Convert.ToDecimal(valorDevido);
                            pagamento.tipoPag = "Vista";
                            lblValorTroco.Text = (Convert.ToDouble(txtRecebido.Text) - valorDevido).ToString("0.00");
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Valor recebido pelo sistema de R$" + Convert.ToDecimal(txtRecebido.Text).ToString("0.00") + ".');", true);
                            valorDevido = 0;
                            venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                            venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                            venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);
                            entrada = false;
                        }
                        pagamento.numChequePrimeiro = "";
                        pagamento.numChequeUltimo = "";
                        pagamento.qntParcelas = 1;
                        pagamento.id_venda = venda.id;
                        pagamento.dataPagamento = DateTime.Today;
                        pagamento.formaPag = ddlFormaVista.SelectedValue;
                        controle.salvaAtualiza();

                        lblValorVista.Text = valorDevido.ToString("0.00");
                    }

                    if (valorDevido == 0)
                    {

                        for (int i = 0; i < ListaProd.Count; i++)
                        {
                            prodVendido = new Vendas_Produtos();
                            controle.salvaProdutosVendidos(prodVendido);
                            prodVendido.id_venda = venda.id;
                            prodVendido.id_produto = ListaProd[i].id;
                            prodVendido.num_item = i + 1;
                            prodVendido.quantidade = QntProd[i];
                            produto = controle.pesquisaProdutoId(ListaProd[i].id);
                            produto.Estoque.qnt_atual = produto.Estoque.qnt_atual - QntProd[i];
                            controle.salvaAtualiza();
                        }                       
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Venda efetuada com sucesso!')", true);
                        situacaoInicial();
                    }

                    else
                    {
                        lblValorVista.Text = valorDevido.ToString("0.00");
                        lblValorPrazo.Text = ((valorDevido * 0.07) + valorDevido).ToString("0.00");
                        lblValorCheque.Text = ((valorDevido * 0.07) + valorDevido).ToString("0.00");

                        lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 2).ToString("0.00");
                        lblValorParcelaCheque.Text = lblValorCheque.Text;
                        ddlQntCheques.SelectedIndex = 0;
                        ddlParcelas.SelectedIndex = 0;

                        controle.salvaAtualiza();
                    }
                }
                
                else if (rdbPrazo.Checked)
                {
                    for (int i = 0; i < Convert.ToInt32(ddlParcelas.SelectedValue); i++)
                    {
                        pagamento = new Pagamentos();
                        controle.salvarPagamento(pagamento);

                        pagamento.numChequePrimeiro = "";
                        pagamento.numChequeUltimo = "";
                        pagamento.numParcela = i + 1;
                        pagamento.qntParcelas = Convert.ToInt32(ddlParcelas.SelectedValue);
                        pagamento.valorTotal = Convert.ToDecimal(lblValorPrazo.Text);
                        pagamento.id_venda = venda.id;

                        if (entrada)
                        {
                            pagamento.tipoPag = "Entrada + Parcelas";
                            pagamento.dataPagamento = DateTime.Today.AddMonths(i + 1);
                        }
                        else if (i == 0)
                        {
                            pagamento.tipoPag = "Parcelado";
                            pagamento.dataPagamento = DateTime.Today;
                        }
                        else
                        {
                            pagamento.tipoPag = "Parcelado";
                            pagamento.dataPagamento = DateTime.Today.AddMonths(i);
                        }
                        pagamento.valorParcela = Convert.ToDecimal(lblValorParcela.Text);
                        pagamento.formaPag = "Cartão Crédito";

                        venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                        venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                        venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);

                        controle.salvaAtualiza();

                    }

                    entrada = false;

                    for (int i = 0; i < ListaProd.Count; i++)
                    {
                        prodVendido = new Vendas_Produtos();
                        controle.salvaProdutosVendidos(prodVendido);
                        prodVendido.id_venda = venda.id;
                        prodVendido.id_produto = ListaProd[i].id;
                        prodVendido.num_item = i + 1;
                        prodVendido.quantidade = QntProd[i];
                        produto = controle.pesquisaProdutoId(ListaProd[i].id);
                        produto.Estoque.qnt_atual = produto.Estoque.qnt_atual - QntProd[i];
                        controle.salvaAtualiza();
                    }
                    
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Registrado o recebimento em " + ddlParcelas.SelectedValue + " parcelas de R$" + lblValorParcela.Text + ".');", true);
                    situacaoInicial();
                }

                else if (rdbCheque.Checked)
                {

                    for (int i = 0; i < Convert.ToInt32(ddlQntCheques.SelectedValue); i++)
                    {
                        pagamento = new Pagamentos();
                        controle.salvarPagamento(pagamento);
                        pagamento.numChequePrimeiro = txtPrimeiroCheque.Text;
                        pagamento.numChequeUltimo = txtUltimoCheque.Text;
                        pagamento.numParcela = i + 1;
                        pagamento.qntParcelas = Convert.ToInt32(ddlQntCheques.SelectedValue);
                        pagamento.valorTotal = Convert.ToDecimal(lblValorCheque.Text);
                        pagamento.id_venda = venda.id;

                        if (Convert.ToInt32(ddlQntCheques.SelectedValue) > 1)
                        {
                            if(entrada)
                            {
                                pagamento.tipoPag = "Entrada + Parcelas";
                                pagamento.dataPagamento = DateTime.Today.AddMonths(i + 1);
                            }
                            else if (i == 0)
                            {
                                pagamento.tipoPag = "Parcelado";
                                pagamento.dataPagamento = DateTime.Today;
                            }
                            else
                            {
                                pagamento.tipoPag = "Parcelado";
                                pagamento.dataPagamento = DateTime.Today.AddMonths(i);
                            }
                            
                            pagamento.valorParcela = Convert.ToDecimal(lblValorParcelaCheque.Text);
                            pagamento.formaPag = "Cheques";

                        }
                        else
                        {
                            pagamento.dataPagamento = DateTime.Today;
                            pagamento.tipoPag = "Vista";
                            pagamento.valorParcela = Convert.ToDecimal(lblValorCheque.Text);
                            pagamento.formaPag = "Cheques";
                        }
                        venda.ICMS = venda.ICMS + (Convert.ToDouble(pagamento.valorParcela) * gerencia.percIcms);
                        venda.comissao = venda.comissao + (pagamento.valorParcela * Convert.ToDecimal(gerencia.comissao));
                        venda.valor_Venda = venda.valor_Venda + Convert.ToDouble(pagamento.valorParcela);                       
                        controle.salvaAtualiza();
                    }

                    entrada = false;   
                    
                    for (int i = 0; i < ListaProd.Count; i++)
                    {
                        prodVendido = new Vendas_Produtos();
                        controle.salvaProdutosVendidos(prodVendido);
                        prodVendido.id_venda = venda.id;
                        prodVendido.id_produto = ListaProd[i].id;
                        prodVendido.num_item = i + 1;
                        prodVendido.quantidade = QntProd[i];
                        produto = controle.pesquisaProdutoId(ListaProd[i].id);
                        produto.Estoque.qnt_atual = produto.Estoque.qnt_atual - QntProd[i];
                        controle.salvaAtualiza();
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Pagamento efetuado, recebido R$" + (Convert.ToDecimal(lblValorCheque.Text)).ToString("0.00") + ", parcelado em " + ddlQntCheques.SelectedValue + " cheques de R$" + lblValorParcelaCheque.Text + ".');", true);
                    situacaoInicial();

                }
                if (notaPaulista)
                {
                    if (nfpCpf.Length > nfpCnpj.Length)
                    {
                        venda.cpf = nfpCpf;
                    }
                    else
                    {
                        venda.cpf = nfpCnpj;
                    }
                    controle.salvaAtualiza();
                }
                
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert(' Houve algum problema ao incluir os dados, tente novamente.');", true);
            }
        }

        protected void btnCancelaPag_Click(object sender, EventArgs e)
        {
            situacaoInicial();
        }

        private void situacaoInicial()
        {
            Response.Flush();
            pnlBasico.Visible = true;
            pnlAdjacente.Visible = true;
            pnlPagamento.Visible = false;
            txtBairro.Text = "";
            txtCelular.Text = "";
            txtCidade.Text = "";
            txtCliente.Text = "";
            txtCodigo.Text = "";
            txtContato.Text = "";
            txtCpf.Text = "";
            txtCreditos.Text = "";
            txtDesconto.Text = "0";
            txtEndereco.Text = "";
            txtNpCnpj.Text = "";
            txtNpCpf.Text = "";
            txtNumero.Text = "";
            txtPesquisaCliente.Text = "";
            txtPesquisaProd.Text = "";
            txtPrimeiroCheque.Text = "";
            txtQuantidade.Text = "";
            txtRecebido.Text = "";
            txtRg.Text = "";
            txtSubTotal.Text = "0,00";
            txtTelefone.Text = "";
            txtTotalVista.Text = "0,00";
            txtUltimoCheque.Text = "";
            lblValorVista.Text = "0,00";
            lblValorPrazo.Text = "0,00";
            lblValorCheque.Text = "0,00";
            lblValorParcela.Text = "0,00";
            lblValorParcelaCheque.Text = "0,00";
            lblTroco.Text = "Troco : ";
            lblValorTroco.Text = "0,00";
            ddlClientes.SelectedIndex = -1;
            ddlFormaVista.SelectedIndex = -1;
            ddlParcelas.SelectedIndex = -1;
            ddlPesquisaProd.SelectedIndex = -1;
            ddlQntCheques.SelectedIndex = -1;
            dlProdVenda.Items.Clear();
            dlProdValor.Items.Clear();
            dlProdQnt.Items.Clear();
            venda = new Vendas();
            produto = new Produtos();
            cliente = new Clientes();
            ListaProd = new List<Produtos>();
            QntProd = new List<int>();
            clienteCadastrado = false;
            notaPaulista = false;
            nfpCpf = null;
            nfpCnpj = null;
            existe = false;            
            qntTemp = 0;
            esgotado = false;
            pessoaFisica = true;
            rdbCheque.Checked = false;
            rdbVista.Checked = false;
            rdbPrazo.Checked = false;
            lblNumUltimo.Visible = false;
            txtUltimoCheque.Visible = false;
            lblRecebido.Visible = false;
            txtRecebido.Visible = false;
            btnReceber.Visible = false;
            lblTroco.Visible = false;
            lblValorTroco.Visible = false;
            btnVender.Enabled = false;
            btnCorrigir.Enabled = false;
            btnCliente.Enabled = true;
            btnAceitarPag.Enabled = false;
            btnNotaPaulista.Enabled = true;
            btnContinuarCompra.Enabled = true;
            btnPesquisaClientes.Visible = true;
            pnlPrazo.BackColor = Color.LightGray;
            pnlVista.BackColor = Color.LightGray;
            pnlCheque.BackColor = Color.LightGray;
        }

        protected void ddlParcelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlParcelas.SelectedValue.Equals("2"))
            {
                lblValorPrazo.Text = Convert.ToDecimal(valorDevido).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 2).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("3"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo3x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 3).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("4"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo4x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 4).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("5"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo5x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 5).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("6"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo6x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 6).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("7"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo7x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 7).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("8"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo8x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 8).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("9"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo9x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 9).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("10"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo10x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 10).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("11"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo11x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 11).ToString("0.00");
            }
            else if (ddlParcelas.SelectedValue.Equals("12"))
            {
                lblValorPrazo.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosPrazo12x / 100))).ToString("0.00");
                lblValorParcela.Text = (Convert.ToDecimal(lblValorPrazo.Text) / 12).ToString("0.00");
            }
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnCodigo_Click(sender, e);
            //txtQuantidade.Focus();
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
                lblValorCheque.Text = Convert.ToDecimal(valorDevido).ToString("0.00");
                lblValorParcelaCheque.Text = lblValorCheque.Text;
                lblNumUltimo.Visible = false;
                txtUltimoCheque.Visible = false;
            }
            if (ddlQntCheques.SelectedValue.Equals("2"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque2x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 2).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("3"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque3x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 3).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("4"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque4x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 4).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("5"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque5x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 5).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("6"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque6x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 6).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("7"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque7x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 7).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("8"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque8x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 8).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("9"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque9x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 9).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("10"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque10x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 10).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("11"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque11x / 100))).ToString("0.00");
                lblValorParcelaCheque.Text = (Convert.ToDecimal(lblValorCheque.Text) / 11).ToString("0.00");
                lblNumUltimo.Visible = true;
                txtUltimoCheque.Visible = true;
            }
            else if (ddlQntCheques.SelectedValue.Equals("12"))
            {
                lblValorCheque.Text = (Convert.ToDecimal(valorDevido) + (Convert.ToDecimal(valorDevido) * Convert.ToDecimal(gerencia.jurosCheque12x / 100))).ToString("0.00");
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
            if (rdbCpf.Checked)
            {
                nfpCnpj = "";
                nfpCpf = txtNpCpf.Text;
            }
            else if (rdbCnpj.Checked)
            {
                nfpCnpj = txtNpCnpj.Text;
                nfpCpf = "";
            }
            
            pnlPagamento.Visible = true;
            pnlCpfCnpj.Visible = false;
            btnNotaPaulista.Enabled = false;
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

        protected void rdbFisica_CheckedChanged(object sender, EventArgs e)
        {
            lblCpf.Visible = true;
            lblCnpj.Visible = false;
            pessoaFisica = true;
        }

        protected void rdbJuridica_CheckedChanged(object sender, EventArgs e)
        {
            lblCpf.Visible = false;
            lblCnpj.Visible = true;
            pessoaFisica = false;
        }

        protected void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Focus();
            pnlPrincipal.DefaultButton = btnCodigo.ID;
        }

        protected void txtPesquisaProd_TextChanged(object sender, EventArgs e)
        {
            txtCodigo.Focus();
            pnlPrincipal.DefaultButton = btnCodigo.ID;
        }
    }
}