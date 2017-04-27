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
    public partial class PainelVendas : System.Web.UI.Page
    {
        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil = 0;

        Produtos produto = new Produtos();
        static List<Produtos> ListaProd = new List<Produtos>();
        List<Produtos> PesquisaProd = new List<Produtos>();
        static List<int> QntProd = new List<int>();

        //variaveis associadas as regras de negócio
        //desconto automatico, respectivamente Valor para conceção e percentual
        static int autoDescVal = 200;
        static double autoDescPerc = 7;

        protected void Page_Load(object sender, EventArgs e)
        {
            pnlPrincipal.DefaultButton = btnCodigo.ID;

            if (!IsPostBack)
            {
                //coversão de inteiro para percentual
                autoDescPerc = autoDescPerc / 100;

                lblMensagem.Text = "  Sistema de vendas - Alemão da Construção";
                produto = new Produtos();
                ListaProd = new List<Produtos>();
                /*/validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);
                    //lblUser.Text = user.nome;
                    if (!teste.ValidaUser(id, perfil) || user.num_perfil != 1)
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }
                    
                }*/
                
            }
        }

        protected void btnCodigo_Click(object sender, EventArgs e)
        {
            //inserir validação de existencia de produto
            produto = controle.pesquisaProdutoCod(txtCodigo.Text);            

            btnCorrigir.Enabled = true;
            btnVender.Enabled = true;
            int qntTemp = 0;
            //validação de inserção repetida de produto
            bool existe = false;

            for(int i = 0;i < ListaProd.Count; i++)
            {
                if(ListaProd[i].id == produto.id)
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
            if (!existe)
            { 
                ListaProd.Add(produto);
                if (txtQuantidade.Text.Equals(""))
                {
                    QntProd.Add(1);
                    qntTemp = 1;
                }
                else
                {
                    QntProd.Add(Convert.ToInt32(txtQuantidade.Text));
                    qntTemp = Convert.ToInt32(txtQuantidade.Text);
                }
            }            
            
            txtSubTotal.Text = (Convert.ToDecimal(txtSubTotal.Text) + qntTemp * Convert.ToDecimal(produto.preco_venda)).ToString();
            if(Convert.ToDecimal(txtSubTotal.Text) > autoDescVal)
            {
                txtTotalVista.Text = ((Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * autoDescPerc))) + Convert.ToDecimal("0,00")).ToString();
            }
            else
            {
                txtTotalVista.Text = txtSubTotal.Text;
            }

            preencheDataListVendas(ListaProd);
            AdicionaCheckListVendas(ListaProd);
            txtCodigo.Text = "";
            txtQuantidade.Text = "";
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
                    texto = "0" + (i + 1).ToString() + "-" + ListaProd[i].desc_produto;                                        
                }

                else
                {
                    texto = (i + 1).ToString() + "-" + ListaProd[i].desc_produto;                                        
                }
                for (int j = 0; j < 300; j++)
                {
                    texto = texto + ".";
                }
                
                dlProdVenda.Items[i].Text = texto;
                dlProdQnt.Items[i].Text = "Qnt " + QntProd[i].ToString() + "..............";
                dlProdValor.Items[i].Text = (QntProd[i] * Convert.ToDecimal(dlProdValor.Items[i].Text) + Convert.ToDecimal("0,00")).ToString();
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
                    texto = "0" + (i + 1).ToString() + "-" + ListaProd[i].desc_produto;
                }

                else
                {
                    texto = (i + 1).ToString() + "-" + ListaProd[i].desc_produto;
                }                
                texto = texto + " ....... Qnt : ";                
                texto = texto + QntProd[i].ToString() + " ....... Valor : " + (Convert.ToDecimal(dlProdValor.Items[i].Text) + Convert.ToDecimal("0,00")).ToString();
                chkProdVenda.Items[i].Text = texto;                        
            }

        }

        protected void btnVender_Click(object sender, EventArgs e)
        {

        }

        protected void btnCorrigir_Click(object sender, EventArgs e)
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
            btnCancel.Enabled = false;
            btnCodigo.Enabled = false;
            btnPesquisa.Enabled = false;

            btnVender.Enabled = false;
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
            btnCancel.Enabled = true;
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
                    txtSubTotal.Text = (Convert.ToDecimal(txtSubTotal.Text) - QntProd[i] * Convert.ToDecimal(ListaProd[i].preco_venda)).ToString();
                    ListaProd.RemoveAt(i);
                    QntProd.RemoveAt(i);

                    if (Convert.ToDecimal(txtSubTotal.Text) > autoDescVal)
                    {
                        txtTotalVista.Text = ((Convert.ToDecimal(Convert.ToDouble(txtSubTotal.Text) - (Convert.ToDouble(txtSubTotal.Text) * autoDescPerc))) + Convert.ToDecimal("0,00")).ToString();
                    }

                    else
                    {
                        txtTotalVista.Text = txtSubTotal.Text;
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
            pnlCliente.Visible = true;
        }

        protected void btnPesquisa_Click(object sender, ImageClickEventArgs e)
        {
            btnPesquisa.Visible = false;            
            txtPesquisaProd.Visible = false;
            ddlPesquisaProd.Visible = true;
            //pesquisa e preenchimento de drop down list
            PesquisaProd = controle.pesquisaProdutosValidoNome(txtPesquisaProd.Text);
            
            ddlPesquisaProd.Items.Clear();

            ddlPesquisaProd.Items.Add("Selecione um dos itens");
            ddlPesquisaProd.Items[0].Text = "Selecione um dos itens";
            int x = 1;
            foreach (Produtos value in PesquisaProd)
            {
                ddlPesquisaProd.Items.Add("desc_produto");
                ddlPesquisaProd.Items[x].Value = value.id.ToString();
                ddlPesquisaProd.Items[x].Text = PesquisaProd[x - 1].desc_produto + "- R$" + (Convert.ToDecimal(PesquisaProd[x - 1].preco_venda) + Convert.ToDecimal("0,00"));
                x++;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtQuantidade.Text = "";
        }

        protected void ddlPesquisaProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            produto = controle.pesquisaProdutoId(Convert.ToInt32(ddlPesquisaProd.SelectedValue));
            txtCodigo.Text = produto.cod_produto;
            btnPesquisa.Visible = true;
            ddlPesquisaProd.Visible = false;
            txtPesquisaProd.Visible = true;
            txtPesquisaProd.Text = "";
        }
    }
}