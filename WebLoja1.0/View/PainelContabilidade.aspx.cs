using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLoja1._0.Control;
using WebLoja1._0.Model;

namespace WebLoja1._0.View
{
    public partial class PainelContabilidade : System.Web.UI.Page
    {
        public struct listas
        {
            public List<string> titulos;
            public List<decimal> valores;
        }

        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;        

        static Movimentos movimentacao = new Movimentos();
        static List<Tipos_Movimentacao> listaTipoMov, listaSubTipoMov, listaFormaPag = new List<Tipos_Movimentacao>();
        static List<Movimentos> movimentos = new List<Movimentos>();
        static listas ativoCirc, atvNaoCirc, passivoCirc, passNaoCirc, patrLiq = new listas();
        static decimal despesas = 0, retirada = 0, salarios = 0, impostosRecolher = 0, caixa = 0, banco = 0, outrasPagar = 0, estoque = 0,
            fornecedor = 0, materialTrabalho = 0, creditoCliente = 0, outrasPagarLP = 0, outrosReceberLP = 0, maquinas = 0, veiculo = 0,
            ferramenta = 0, movel = 0, imovel = 0, lucro = 0, outrosReceber = 0, abertCapital = 0;

        static string descricao = null, subTipo = null, formaPg = null;

        protected void Page_Load(object sender, EventArgs e)
        {

            lblMensagem.Text = "Balanço Patrimonial e D.R.E.";

            if (!IsPostBack)
            {
                try
                {
                    //validação de acesso
                    id = Convert.ToInt32(Session["id"]);
                    perfil = Convert.ToInt32(Session["perfil"]);

                    if (id != 0 || perfil != 0)
                    {
                        user = controle.pesquisaUserId(id);
                        lblUser.Text = user.nome;
                        if (!teste.ValidaUser(id, perfil) || perfil > 2)
                        {
                            Response.Redirect("~/View/AcessoIndevido.aspx");
                        }
                    }

                    else
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }

                    carregaDdlMovimento();
                    txtDtInicio.Text = DateTime.Parse("01-01-2017").ToShortDateString();
                    txtDtFim.Text = DateTime.Today.AddYears(5).ToShortDateString();
                    btnMostrar_Click(sender, e);
                }
                catch
                {
                    Response.Redirect("~/View/AcessoIndevido.aspx");
                }
            }

        }

        private void carregaDdlMovimento()
        {
            try
            {
                listaTipoMov = controle.pesquisaTiposMovimento();

                ddlMovimento.Items.Clear();

                ddlMovimento.Items.Add("Selecione um dos itens");
                ddlMovimento.Items[0].Text = "Selecione um dos itens";
                int x = 1;
                foreach (Tipos_Movimentacao value in listaTipoMov)
                {
                    ddlMovimento.Items.Add("desc");
                    ddlMovimento.Items[x].Value = value.descricao.ToString();
                    ddlMovimento.Items[x].Text = listaTipoMov[x - 1].descricao;
                    x++;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na busca pelos tipos de movimento, tente novamente.');", true);
            }
        }

        protected void ddlMovimento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                descricao = ddlMovimento.SelectedValue.ToString();

                ddlOrigem.Enabled = true;

                listaSubTipoMov = controle.pesquisaTiposMovimento(ddlMovimento.SelectedValue.ToString());

                ddlOrigem.Items.Clear();

                ddlOrigem.Items.Add("Selecione um dos itens");
                ddlOrigem.Items[0].Text = "Selecione um dos itens";
                int x = 1;
                foreach (Tipos_Movimentacao value in listaSubTipoMov)
                {
                    ddlOrigem.Items.Add("desc");
                    ddlOrigem.Items[x].Value = value.sub_tipo.ToString();
                    ddlOrigem.Items[x].Text = listaSubTipoMov[x - 1].sub_tipo + " - " + listaSubTipoMov[x - 1].direcao;
                    x++;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na busca por sub tipos, por favor, tente novamente.');", true);
            }
        }

        protected void ddlOrigem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                subTipo = ddlOrigem.SelectedValue.ToString();

                listaFormaPag = controle.pesquisaSubTipoMov(ddlOrigem.SelectedValue.ToString());

                if (listaFormaPag.Count == 1)
                {
                    txtData.Enabled = true;
                    txtDescricao.Enabled = true;
                    txtMovValor.Enabled = true;
                    btnIncluir.Enabled = true;
                }
                else
                {
                    ddlForma.Enabled = true;

                    ddlForma.Items.Clear();

                    ddlForma.Items.Add("Selecione um dos itens");
                    ddlForma.Items[0].Text = "Selecione um dos itens";
                    int x = 1;
                    foreach (Tipos_Movimentacao value in listaFormaPag)
                    {
                        ddlForma.Items.Add("desc");
                        ddlForma.Items[x].Value = value.forma_pag.ToString();
                        ddlForma.Items[x].Text = listaFormaPag[x - 1].forma_pag;
                        x++;
                    }
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na busca por formas de pagamento, por favor, tente novamente.');", true);
            }
        }

        protected void ddlForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                formaPg = ddlForma.SelectedValue.ToString();

                txtData.Enabled = true;
                txtDescricao.Enabled = true;
                txtMovValor.Enabled = true;
                btnIncluir.Enabled = true;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na atribuição de forma de pagamento, por favor, tente novamente.');", true);
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                movimentacao = new Movimentos();
                controle.salvarMovimento(movimentacao);
                if (validaCampos())
                {
                    movimentacao.data = Convert.ToDateTime(txtData.Text);
                    movimentacao.desc = txtDescricao.Text;
                    movimentacao.id_tipo = controle.pesquisaCompletaIDTipoMov(descricao, subTipo, formaPg);
                    movimentacao.valor = Convert.ToDecimal(txtMovValor.Text);
                    controle.salvaAtualiza();

                    btnLimparMov_Click(sender, e);
                    btnMostrar_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('É obrigatório o preenchimento dos campos para a inclusão de uma nova movimentação.');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na inserção, por favor, tente novamente.');", true);
            }            
        }

        private bool validaCampos()
        {
            DateTime result;
            if (txtData.Text.Equals(""))
            {
                txtData.Text = DateTime.Today.ToShortDateString();
            }
            if (!DateTime.TryParse(txtData.Text, out result))
            {
                return false;
            }
            else if(txtData.Text.Equals("") || txtDescricao.Text.Equals("") || txtMovValor.Text.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnLimparMov_Click(object sender, EventArgs e)
        {
            txtData.Text = "";
            txtData.Enabled = false;
            txtDescricao.Text = "";
            txtDescricao.Enabled = false;
            txtMovValor.Text = "";
            txtMovValor.Enabled = false;
            ddlForma.Enabled = false;
            ddlForma.SelectedIndex = -1;
            ddlOrigem.Enabled = false;
            ddlOrigem.SelectedIndex = -1;
            ddlMovimento.SelectedIndex = -1;
            btnIncluir.Enabled = false;

        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                movimentos = new List<Movimentos>();
                movimentos = controle.pesquisaMovPeriodo(Convert.ToDateTime(txtDtInicio.Text), Convert.ToDateTime(txtDtFim.Text));

                preencheDataList(movimentos);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na busca por movimentos financeiros nabase de dados, por favor, tente novamente.');", true);
            }
            
        }

        private void preencheDataList(List<Movimentos> lista)
        {
            try
            {
                despesas = 0; retirada = 0; salarios = 0; impostosRecolher = 0; caixa = 0; banco = 0; outrasPagar = 0; estoque = 0;
                fornecedor = 0; materialTrabalho = 0; creditoCliente = 0; outrasPagarLP = 0; outrosReceberLP = 0; maquinas = 0; veiculo = 0;
                ferramenta = 0; movel = 0; imovel = 0; lucro = 0; outrosReceber = 0; abertCapital = 0;

                foreach (Movimentos value in lista)
                {
                    value.Tipos_Movimentacao = controle.pesquisaTiposMovimentoId(value.id_tipo);

                    if (value.Tipos_Movimentacao.descricao.Equals("Contas a Pagar"))
                    {

                        despesas = despesas + Convert.ToDecimal(value.valor);
                        lucro = lucro - Convert.ToDecimal(value.valor);

                    }
                    else if (value.Tipos_Movimentacao.descricao.Equals("Retirada"))
                    {

                        retirada = retirada + Convert.ToDecimal(value.valor);
                        lucro = lucro - Convert.ToDecimal(value.valor);

                    }
                    else if (value.Tipos_Movimentacao.descricao.Equals("Salário"))
                    {

                        salarios = salarios + Convert.ToDecimal(value.valor);
                        lucro = lucro - Convert.ToDecimal(value.valor);

                    }
                    else if (value.Tipos_Movimentacao.descricao.Equals("Impostos a Recolher"))
                    {
                        impostosRecolher = impostosRecolher + Convert.ToDecimal(value.valor);
                        lucro = lucro - Convert.ToDecimal(value.valor);
                    }
                    else if (value.Tipos_Movimentacao.descricao.Equals("Pagamento"))
                    {
                        if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Salario"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                salarios = salarios - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                salarios = salarios - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Pro-Labore"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                retirada = retirada - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                retirada = retirada - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Fornecedor"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                fornecedor = fornecedor - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                fornecedor = fornecedor - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    fornecedor = fornecedor - Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Despesas"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                despesas = despesas - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                despesas = despesas - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Contas"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                outrasPagar = outrasPagar - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                outrasPagar = outrasPagar - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Contas LP"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                outrasPagarLP = outrasPagarLP - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                outrasPagarLP = outrasPagarLP - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pag. Impostos"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                impostosRecolher = impostosRecolher - Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                impostosRecolher = impostosRecolher - Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Rec. Duplicatas"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                outrosReceber = outrosReceber - Convert.ToDecimal(value.valor);
                                caixa = caixa + Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                outrosReceber = outrosReceber - Convert.ToDecimal(value.valor);
                                banco = banco + Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Rec. Duplicatas LP"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                outrosReceberLP = outrosReceberLP - Convert.ToDecimal(value.valor);
                                caixa = caixa + Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                outrosReceberLP = outrosReceberLP - Convert.ToDecimal(value.valor);
                                banco = banco + Convert.ToDecimal(value.valor);
                            }
                        }
                    }
                    else if (value.Tipos_Movimentacao.descricao.Equals("Compra"))
                    {

                        if (value.Tipos_Movimentacao.sub_tipo.Equals("Estoque"))
                        {
                            estoque = estoque + Convert.ToDecimal(value.valor);
                            fornecedor = fornecedor + Convert.ToDecimal(value.valor);
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Material de Escritório"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                materialTrabalho = materialTrabalho + Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                materialTrabalho = materialTrabalho + Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    materialTrabalho = materialTrabalho + Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                                else
                                {
                                    materialTrabalho = materialTrabalho + Convert.ToDecimal(value.valor);
                                    outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                                }
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Maquinário"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                maquinas = maquinas + Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                maquinas = maquinas + Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    maquinas = maquinas + Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                                else
                                {
                                    maquinas = maquinas + Convert.ToDecimal(value.valor);
                                    outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                                }
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Veículo"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                veiculo = veiculo + Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                veiculo = veiculo + Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    veiculo = veiculo + Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                                else
                                {
                                    veiculo = veiculo + Convert.ToDecimal(value.valor);
                                    outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                                }
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Móveis"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                movel = movel + Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                movel = movel + Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    movel = movel + Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                                else
                                {
                                    movel = movel + Convert.ToDecimal(value.valor);
                                    outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                                }
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Imóvel"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                imovel = imovel + Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                imovel = imovel + Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    imovel = imovel + Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                                else
                                {
                                    imovel = imovel + Convert.ToDecimal(value.valor);
                                    outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                                }
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Ferramentas"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                ferramenta = ferramenta + Convert.ToDecimal(value.valor);
                                caixa = caixa - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                ferramenta = ferramenta + Convert.ToDecimal(value.valor);
                                banco = banco - Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("Prazo"))
                            {
                                if (value.data <= DateTime.Today)
                                {
                                    ferramenta = ferramenta + Convert.ToDecimal(value.valor);
                                    banco = banco - Convert.ToDecimal(value.valor);
                                }
                                else
                                {
                                    ferramenta = ferramenta + Convert.ToDecimal(value.valor);
                                    outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                                }
                            }
                        }
                    }

                    else if (value.Tipos_Movimentacao.descricao.Equals("Venda"))
                    {
                        if (value.Tipos_Movimentacao.sub_tipo.Equals("Clientes Longo Prazo"))
                        {
                            if (value.data > DateTime.Today.AddYears(1))
                            {
                                lucro = lucro + Convert.ToDecimal(value.valor);
                                outrosReceberLP = outrosReceberLP + Convert.ToDecimal(value.valor);
                            }
                            else if (value.data <= DateTime.Today)
                            {
                                outrosReceber = outrosReceber - Convert.ToDecimal(value.valor);
                                banco = banco + Convert.ToDecimal(value.valor);
                            }
                            else
                            {
                                outrosReceberLP = outrosReceberLP - Convert.ToDecimal(value.valor);
                                outrosReceber = outrosReceber + Convert.ToDecimal(value.valor);
                            }

                        }
                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Estoque"))
                        {
                            lucro = lucro - Convert.ToDecimal(value.valor);
                            estoque = estoque - Convert.ToDecimal(value.valor);
                        }
                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Clientes Curto Prazo"))
                        {
                            if (value.data <= DateTime.Today)
                            {
                                outrosReceber = outrosReceber - Convert.ToDecimal(value.valor);
                                banco = banco + Convert.ToDecimal(value.valor);
                            }
                            else
                            {
                                lucro = lucro + Convert.ToDecimal(value.valor);
                                outrosReceber = outrosReceber + Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Clientes à Vista"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                lucro = lucro + Convert.ToDecimal(value.valor);
                                caixa = caixa + Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                lucro = lucro + Convert.ToDecimal(value.valor);
                                banco = banco + Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Venda de Créditos ao Cliente"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                creditoCliente = creditoCliente + Convert.ToDecimal(value.valor);
                                caixa = caixa + Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                creditoCliente = creditoCliente + Convert.ToDecimal(value.valor);
                                banco = banco + Convert.ToDecimal(value.valor);
                            }
                        }

                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Pré-Paga"))
                        {
                            if (value.Tipos_Movimentacao.forma_pag.Equals("Dinheiro"))
                            {
                                creditoCliente = creditoCliente - Convert.ToDecimal(value.valor);
                                lucro = lucro + Convert.ToDecimal(value.valor);
                            }
                            else if (value.Tipos_Movimentacao.forma_pag.Equals("TEF"))
                            {
                                creditoCliente = creditoCliente - Convert.ToDecimal(value.valor);
                                lucro = lucro + Convert.ToDecimal(value.valor);
                            }
                        }

                    }

                    else if (value.Tipos_Movimentacao.descricao.Equals("Outras Despesas a Pagar"))
                    {
                        if (value.data > DateTime.Today.AddYears(1))
                        {
                            lucro = lucro - Convert.ToDecimal(value.valor);
                            outrasPagarLP = outrasPagarLP + Convert.ToDecimal(value.valor);
                        }
                        else if (value.data <= DateTime.Today)
                        {
                            outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                            lucro = lucro - Convert.ToDecimal(value.valor);
                        }
                        else
                        {
                            outrasPagarLP = outrasPagarLP - Convert.ToDecimal(value.valor);
                            outrasPagar = outrasPagar + Convert.ToDecimal(value.valor);
                        }
                    }

                    else if (value.Tipos_Movimentacao.descricao.Equals("Outros a Receber"))
                    {
                        if (value.data > DateTime.Today.AddYears(1))
                        {
                            lucro = lucro + Convert.ToDecimal(value.valor);
                            outrosReceberLP = outrosReceberLP + Convert.ToDecimal(value.valor);
                        }
                        else if (value.data <= DateTime.Today)
                        {
                            outrosReceber = outrosReceber - Convert.ToDecimal(value.valor);
                            banco = banco + Convert.ToDecimal(value.valor);
                        }
                        else
                        {
                            outrosReceberLP = outrosReceberLP - Convert.ToDecimal(value.valor);
                            outrosReceber = outrosReceber + Convert.ToDecimal(value.valor);
                        }
                    }

                    else if (value.Tipos_Movimentacao.descricao.Equals("Movimento Interno"))
                    {
                        if (value.Tipos_Movimentacao.sub_tipo.Equals("Pró-Labore => Lucro"))
                        {
                            retirada = retirada - Convert.ToDecimal(value.valor);
                            lucro = lucro + Convert.ToDecimal(value.valor);
                        }
                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Caixa => Banco"))
                        {
                            caixa = caixa - Convert.ToDecimal(value.valor);
                            banco = banco + Convert.ToDecimal(value.valor);
                        }
                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Banco => Caixa"))
                        {
                            caixa = caixa + Convert.ToDecimal(value.valor);
                            banco = banco - Convert.ToDecimal(value.valor);
                        }
                    }

                    else if (value.Tipos_Movimentacao.descricao.Equals("Abertura de Capital"))
                    {
                        if (value.Tipos_Movimentacao.sub_tipo.Equals("Capital Social Integralizado"))
                        {
                            abertCapital = abertCapital + Convert.ToDecimal(value.valor);
                            banco = banco + Convert.ToDecimal(value.valor);
                        }
                        else if (value.Tipos_Movimentacao.sub_tipo.Equals("Capital Social Não Integralizado"))
                        {
                            abertCapital = abertCapital + Convert.ToDecimal(value.valor);
                            outrosReceberLP = outrosReceberLP + Convert.ToDecimal(value.valor);
                        }
                    }
                }

                passivoCirc.titulos = new List<string>();
                passivoCirc.valores = new List<decimal>();
                passNaoCirc.titulos = new List<string>();
                passNaoCirc.valores = new List<decimal>();
                ativoCirc.titulos = new List<string>();
                ativoCirc.valores = new List<decimal>();
                atvNaoCirc.titulos = new List<string>();
                atvNaoCirc.valores = new List<decimal>();
                patrLiq.titulos = new List<string>();
                patrLiq.valores = new List<decimal>();

                if (despesas != 0)
                {
                    passivoCirc.titulos.Add("Despesas...............................................................");
                    passivoCirc.valores.Add(Convert.ToDecimal(despesas.ToString("0.00")));
                }

                if (impostosRecolher != 0)
                {
                    passivoCirc.titulos.Add("Imposto a Recolher.....................................................");
                    passivoCirc.valores.Add(Convert.ToDecimal(impostosRecolher.ToString("0.00")));
                }

                if (fornecedor != 0)
                {
                    passivoCirc.titulos.Add("Fornecedores...........................................................");
                    passivoCirc.valores.Add(Convert.ToDecimal(fornecedor.ToString("0.00")));
                }

                if (outrasPagar != 0)
                {
                    passivoCirc.titulos.Add("Contas a Pagar.........................................................");
                    passivoCirc.valores.Add(Convert.ToDecimal(outrasPagar.ToString("0.00")));
                }

                if (salarios != 0)
                {
                    passivoCirc.titulos.Add("Remuneração.............................................................");
                    passivoCirc.valores.Add(Convert.ToDecimal(salarios.ToString("0.00")));
                }

                if (creditoCliente != 0)
                {
                    passivoCirc.titulos.Add("Créditos de Clientes....................................................");
                    passivoCirc.valores.Add(Convert.ToDecimal(creditoCliente.ToString("0.00")));
                }

                if (outrasPagarLP != 0)
                {
                    passNaoCirc.titulos.Add("Contas a Pagar Longo Prazo..............................................");
                    passNaoCirc.valores.Add(Convert.ToDecimal(outrasPagarLP.ToString("0.00")));
                }

                if (abertCapital != 0)
                {
                    patrLiq.titulos.Add("Capital Social..............................................................");
                    patrLiq.valores.Add(Convert.ToDecimal(abertCapital.ToString("0.00")));
                }

                if (lucro != 0)
                {
                    patrLiq.titulos.Add("Lucro Liquido...............................................................");
                    patrLiq.valores.Add(Convert.ToDecimal(lucro.ToString("0.00")));
                }

                if (retirada != 0)
                {
                    patrLiq.titulos.Add("Pró-Labore..................................................................");
                    patrLiq.valores.Add(Convert.ToDecimal(retirada.ToString("0.00")));
                }

                if (outrosReceberLP != 0)
                {
                    atvNaoCirc.titulos.Add("Duplicatas à Receber Longo Prazo.........................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(outrosReceberLP.ToString("0.00")));
                }

                if (materialTrabalho != 0)
                {
                    atvNaoCirc.titulos.Add("Material de Escritório...................................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(materialTrabalho.ToString("0.00")));
                }

                if (ferramenta != 0)
                {
                    atvNaoCirc.titulos.Add("Ferramentas de Trabalho..................................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(ferramenta.ToString("0.00")));
                }

                if (movel != 0)
                {
                    atvNaoCirc.titulos.Add("Mobília..................................................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(movel.ToString("0.00")));
                }

                if (veiculo != 0)
                {
                    atvNaoCirc.titulos.Add("Veículo..................................................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(veiculo.ToString("0.00")));
                }

                if (maquinas != 0)
                {
                    atvNaoCirc.titulos.Add("Maquinas.................................................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(maquinas.ToString("0.00")));
                }

                if (imovel != 0)
                {
                    atvNaoCirc.titulos.Add("Imóvel / Inv. Construção.................................................");
                    atvNaoCirc.valores.Add(Convert.ToDecimal(imovel.ToString("0.00")));
                }

                if (caixa != 0)
                {
                    ativoCirc.titulos.Add("CAIXA.....................................................................");
                    ativoCirc.valores.Add(Convert.ToDecimal(caixa.ToString("0.00")));
                }

                if (banco != 0)
                {
                    ativoCirc.titulos.Add("BANCO....................................................................");
                    ativoCirc.valores.Add(Convert.ToDecimal(banco.ToString("0.00")));
                }

                if (estoque != 0)
                {
                    ativoCirc.titulos.Add("Material no Estoque......................................................");
                    ativoCirc.valores.Add(Convert.ToDecimal(estoque.ToString("0.00")));
                }

                if (outrosReceber != 0)
                {
                    ativoCirc.titulos.Add("Duplicatas à Receber......................................................");
                    ativoCirc.valores.Add(Convert.ToDecimal(outrosReceber.ToString("0.00")));
                }

                lbAtivoCirculante.DataSource = ativoCirc.titulos;
                lbAtivoCirculante.DataBind();

                lbValorAtivoCirculante.DataSource = ativoCirc.valores;
                lbValorAtivoCirculante.DataBind();

                lbAtvNCirc.DataSource = atvNaoCirc.titulos;
                lbAtvNCirc.DataBind();

                lbVlAtvNCirc.DataSource = atvNaoCirc.valores;
                lbVlAtvNCirc.DataBind();

                lbPassivoCirculante.DataSource = passivoCirc.titulos;
                lbPassivoCirculante.DataBind();

                lbValorPassivoCirculante.DataSource = passivoCirc.valores;
                lbValorPassivoCirculante.DataBind();

                lbPassNCirc.DataSource = passNaoCirc.titulos;
                lbPassNCirc.DataBind();

                lbVlPassNCirc.DataSource = passNaoCirc.valores;
                lbVlPassNCirc.DataBind();

                lbPatrimonio.DataSource = patrLiq.titulos;
                lbPatrimonio.DataBind();

                lbValorPatrimonio.DataSource = patrLiq.valores;
                lbValorPatrimonio.DataBind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Falha na conversão dos movimentos, por favor, tente novamente.');", true);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            txtDtInicio.Text = "";
            txtDtFim.Text = "";
        }
       
    }
}