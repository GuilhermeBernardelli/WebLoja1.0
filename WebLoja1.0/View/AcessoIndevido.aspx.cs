using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebLoja1._0.View
{
    public partial class AcessoIndevido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Acesso Indevido !";
            lblAlerta.Text = "Tentativa de acesso indevido ou incorreto, realize o login novamente.";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/index.aspx");
        }
    }
}