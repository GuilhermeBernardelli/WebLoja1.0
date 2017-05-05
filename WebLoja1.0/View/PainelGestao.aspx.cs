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
    public partial class PainelGestao : System.Web.UI.Page
    {
        
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;

        Controle controle = new Controle();
        static Gerenciamento gerencia = new Gerenciamento();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                lblMensagem.Text = "Gerenciamento e Controle";
                preencheCampos();

                //validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);
                    if (!teste.ValidaUser(id, perfil) || user.num_perfil > 1)
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

        private void preencheCampos()
        {
            gerencia = controle.pesquisaGerenciamento(1);

            txtAutoDesc.Text = (100 * gerencia.autoDescPerc).ToString() + "%";
            txtAutoDescValor.Text = "R$" + gerencia.autoDescValor.ToString();
            txtComissao.Text = (100 * gerencia.comissao).ToString() + "%";
            txtIcms.Text = (100 * gerencia.percIcms).ToString() + "%";
            txtMaxDescPerc.Text = (100 * gerencia.maxDescPerc).ToString() + "%";

            txtPrazo3x.Text = (100 * gerencia.jurosPrazo3x).ToString() + "%";
            txtPrazo4x.Text = (100 * gerencia.jurosPrazo4x).ToString() + "%";
            txtPrazo5x.Text = (100 * gerencia.jurosPrazo5x).ToString() + "%";
            txtPrazo6x.Text = (100 * gerencia.jurosPrazo6x).ToString() + "%";
            txtPrazo7x.Text = (100 * gerencia.jurosPrazo7x).ToString() + "%";
            txtPrazo8x.Text = (100 * gerencia.jurosPrazo8x).ToString() + "%";
            txtPrazo9x.Text = (100 * gerencia.jurosPrazo9x).ToString() + "%";
            txtPrazo10x.Text = (100 * gerencia.jurosPrazo10x).ToString() + "%";
            txtPrazo11x.Text = (100 * gerencia.jurosPrazo11x).ToString() + "%";
            txtPrazo12x.Text = (100 * gerencia.jurosPrazo12x).ToString() + "%";

            txtCheque2x.Text = (100 * gerencia.jurosCheque2x).ToString() + "%";
            txtCheque3x.Text = (100 * gerencia.jurosCheque3x).ToString() + "%";
            txtCheque4x.Text = (100 * gerencia.jurosCheque4x).ToString() + "%";
            txtCheque5x.Text = (100 * gerencia.jurosCheque5x).ToString() + "%";
            txtCheque6x.Text = (100 * gerencia.jurosCheque6x).ToString() + "%";
            txtCheque7x.Text = (100 * gerencia.jurosCheque7x).ToString() + "%";
            txtCheque8x.Text = (100 * gerencia.jurosCheque8x).ToString() + "%";
            txtCheque9x.Text = (100 * gerencia.jurosCheque9x).ToString() + "%";
            txtCheque10x.Text = (100 * gerencia.jurosCheque10x).ToString() + "%";
            txtCheque11x.Text = (100 * gerencia.jurosCheque11x).ToString() + "%";
            txtCheque12x.Text = (100 * gerencia.jurosCheque12x).ToString() + "%";
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            txtAutoDesc.Text = (100 * gerencia.autoDescPerc).ToString();
            txtAutoDescValor.Text = gerencia.autoDescValor.ToString();
            txtComissao.Text = (100 * gerencia.comissao).ToString();
            txtIcms.Text = (100 * gerencia.percIcms).ToString();
            txtMaxDescPerc.Text = (100 * gerencia.maxDescPerc).ToString();

            txtPrazo3x.Text = (100 * gerencia.jurosPrazo3x).ToString();
            txtPrazo4x.Text = (100 * gerencia.jurosPrazo4x).ToString();
            txtPrazo5x.Text = (100 * gerencia.jurosPrazo5x).ToString();
            txtPrazo6x.Text = (100 * gerencia.jurosPrazo6x).ToString();
            txtPrazo7x.Text = (100 * gerencia.jurosPrazo7x).ToString();
            txtPrazo8x.Text = (100 * gerencia.jurosPrazo8x).ToString();
            txtPrazo9x.Text = (100 * gerencia.jurosPrazo9x).ToString();
            txtPrazo10x.Text = (100 * gerencia.jurosPrazo10x).ToString();
            txtPrazo11x.Text = (100 * gerencia.jurosPrazo11x).ToString();
            txtPrazo12x.Text = (100 * gerencia.jurosPrazo12x).ToString();

            txtCheque2x.Text = (100 * gerencia.jurosCheque2x).ToString();
            txtCheque3x.Text = (100 * gerencia.jurosCheque3x).ToString();
            txtCheque4x.Text = (100 * gerencia.jurosCheque4x).ToString();
            txtCheque5x.Text = (100 * gerencia.jurosCheque5x).ToString();
            txtCheque6x.Text = (100 * gerencia.jurosCheque6x).ToString();
            txtCheque7x.Text = (100 * gerencia.jurosCheque7x).ToString();
            txtCheque8x.Text = (100 * gerencia.jurosCheque8x).ToString();
            txtCheque9x.Text = (100 * gerencia.jurosCheque9x).ToString();
            txtCheque10x.Text = (100 * gerencia.jurosCheque10x).ToString();
            txtCheque11x.Text = (100 * gerencia.jurosCheque11x).ToString();
            txtCheque12x.Text = (100 * gerencia.jurosCheque12x).ToString();

            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;

            txtAutoDesc.Enabled = true;
            txtAutoDescValor.Enabled = true;
            txtComissao.Enabled = true; 
            txtIcms.Enabled = true; 
            txtMaxDescPerc.Enabled = true; 

            txtPrazo3x.Enabled = true; 
            txtPrazo4x.Enabled = true; 
            txtPrazo5x.Enabled = true; 
            txtPrazo6x.Enabled = true; 
            txtPrazo7x.Enabled = true; 
            txtPrazo8x.Enabled = true; 
            txtPrazo9x.Enabled = true; 
            txtPrazo10x.Enabled = true; 
            txtPrazo11x.Enabled = true; 
            txtPrazo12x.Enabled = true; 

            txtCheque2x.Enabled = true; 
            txtCheque3x.Enabled = true; 
            txtCheque4x.Enabled = true; 
            txtCheque5x.Enabled = true; 
            txtCheque6x.Enabled = true; 
            txtCheque7x.Enabled = true; 
            txtCheque8x.Enabled = true; 
            txtCheque9x.Enabled = true; 
            txtCheque10x.Enabled = true; 
            txtCheque11x.Enabled = true; 
            txtCheque12x.Enabled = true; 
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            gerencia = new Gerenciamento();
            gerencia = controle.pesquisaGerenciamento(1);

            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;

            txtAutoDesc.Enabled = false;
            txtAutoDescValor.Enabled = false;
            txtComissao.Enabled = false;
            txtIcms.Enabled = false;
            txtMaxDescPerc.Enabled = false;

            txtPrazo3x.Enabled = false;
            txtPrazo4x.Enabled = false;
            txtPrazo5x.Enabled = false;
            txtPrazo6x.Enabled = false;
            txtPrazo7x.Enabled = false;
            txtPrazo8x.Enabled = false;
            txtPrazo9x.Enabled = false;
            txtPrazo10x.Enabled = false;
            txtPrazo11x.Enabled = false;
            txtPrazo12x.Enabled = false;

            txtCheque2x.Enabled = false;
            txtCheque3x.Enabled = false;
            txtCheque4x.Enabled = false;
            txtCheque5x.Enabled = false;
            txtCheque6x.Enabled = false;
            txtCheque7x.Enabled = false;
            txtCheque8x.Enabled = false;
            txtCheque9x.Enabled = false;
            txtCheque10x.Enabled = false;
            txtCheque11x.Enabled = false;
            txtCheque12x.Enabled = false;

            gerencia.autoDescPerc = Convert.ToDouble(txtAutoDesc.Text) / 100;
            gerencia.autoDescValor = Convert.ToDecimal(Convert.ToDecimal(txtAutoDescValor.Text).ToString("0.00"));
            gerencia.comissao = Convert.ToDouble(txtComissao.Text) / 100;
            gerencia.percIcms = Convert.ToDouble(txtIcms.Text) / 100;
            gerencia.maxDescPerc = Convert.ToDouble(txtMaxDescPerc.Text) / 100;

            gerencia.jurosPrazo3x = Convert.ToDouble(txtPrazo3x.Text) / 100;
            gerencia.jurosPrazo4x = Convert.ToDouble(txtPrazo4x.Text) / 100;
            gerencia.jurosPrazo5x = Convert.ToDouble(txtPrazo5x.Text) / 100;
            gerencia.jurosPrazo6x = Convert.ToDouble(txtPrazo6x.Text) / 100;
            gerencia.jurosPrazo7x = Convert.ToDouble(txtPrazo7x.Text) / 100;
            gerencia.jurosPrazo8x = Convert.ToDouble(txtPrazo8x.Text) / 100;
            gerencia.jurosPrazo9x = Convert.ToDouble(txtPrazo9x.Text) / 100;
            gerencia.jurosPrazo10x = Convert.ToDouble(txtPrazo10x.Text) / 100;
            gerencia.jurosPrazo11x = Convert.ToDouble(txtPrazo11x.Text) / 100;
            gerencia.jurosPrazo12x = Convert.ToDouble(txtPrazo12x.Text) / 100;

            gerencia.jurosCheque2x = Convert.ToDouble(txtCheque2x.Text) / 100;
            gerencia.jurosCheque3x = Convert.ToDouble(txtCheque3x.Text) / 100;
            gerencia.jurosCheque4x = Convert.ToDouble(txtCheque4x.Text) / 100;
            gerencia.jurosCheque5x = Convert.ToDouble(txtCheque5x.Text) / 100;
            gerencia.jurosCheque6x = Convert.ToDouble(txtCheque6x.Text) / 100;
            gerencia.jurosCheque7x = Convert.ToDouble(txtCheque7x.Text) / 100;
            gerencia.jurosCheque8x = Convert.ToDouble(txtCheque8x.Text) / 100;
            gerencia.jurosCheque9x = Convert.ToDouble(txtCheque9x.Text) / 100;
            gerencia.jurosCheque10x = Convert.ToDouble(txtCheque10x.Text) / 100;
            gerencia.jurosCheque11x = Convert.ToDouble(txtCheque11x.Text) / 100;
            gerencia.jurosCheque12x = Convert.ToDouble(txtCheque12x.Text) / 100;

            controle.salvaAtualiza();

            preencheCampos();
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }
    }
}