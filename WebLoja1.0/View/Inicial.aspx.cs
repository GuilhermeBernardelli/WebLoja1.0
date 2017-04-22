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
            lblMensagem.Text = "Sistema do Alemão da Construção";

            if (!IsPostBack)
            {
                /*/validação de acesso
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
                }*/
                
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
    }
}