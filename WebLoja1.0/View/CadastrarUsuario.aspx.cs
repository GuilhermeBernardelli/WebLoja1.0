using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebLoja1._0.Control;

namespace WebLoja1._0.View
{
    public partial class CadastrarUsuario : System.Web.UI.Page
    {
        Valida teste = new Valida();
        static int id;
        static int perfil;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);
                
                if (!teste.ValidaUser(id, perfil))
                {
                    Response.Redirect("~/View/AcessoIndevido.aspx");
                }
            }
        }
    }
}