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
    public partial class Inicial : System.Web.UI.Page
    {
        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;        

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Sistema do Alemão da Construção 1.0";

            if (!IsPostBack)
            {
                //validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);
                    lblUser.Text = user.nome;
                    if (!teste.ValidaUser(id, perfil))
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }
                }

                else
                {
                    Response.Redirect("~/View/AcessoIndevido.aspx");
                }
                
                if(perfil > 1)
                {
                    btnUsuarios.Visible = false;
                    btnGestao.Visible = false;
                    btnRelatorios.Visible = false;

                    if (perfil > 2)
                    {
                        btnContabilidade.Visible = false;
                        btnFolhaPg.Visible = false;

                        if (perfil == 3)
                        {
                            btnFornecedores.Visible = false;
                            btnClientes.Visible = false;
                            btnProdutos.Visible = false;
                        }

                        if (perfil == 4)
                        {
                            btnVendas.Visible = false;
                        }
                    }
                }                
            }
        }

        protected void btnVendas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelVendas.aspx");
        }

        protected void lblProdutos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelProdutos.aspx");
        }

        protected void btnClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelClientes.aspx");
        }

        protected void btnFornecedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelFornecedores.aspx");
        }

        protected void btnUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelUsuarios.aspx");
        }

        protected void btnContabilidade_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelContabilidade.aspx");
        }

        protected void btnGestao_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelGestao.aspx");
        }

        protected void btnFolhaPg_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelFolhaPag.aspx");
        }

        protected void btnRelatorios_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelRelatorios.aspx");
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Logout Realizado com Sucesso !!!')", true);
            Response.RedirectPermanent("~/View/index.aspx");
        }
    }
}