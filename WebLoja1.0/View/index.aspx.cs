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
    public partial class index : System.Web.UI.Page
    {
        Controle controle = new Controle();
        Usuarios user;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Bem vindo ao sistema do Alemão da Construção";
            //Acesso Forçado durante testes
            //REMOVER APÓS CONCLUSÃO
            Session["id"] = 1;
            Session["perfil"] = 1;
            Response.Redirect("Inicial.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text.Equals(""))
            {
                lblAlerta.Text = "Digite um valor válido para Login e Senha";
                lblAlerta.Visible = Visible;
            }
            else
            {
                user = controle.pesquisaUserLogin(txtLogin.Text.Trim().ToUpper());
                if (user == null)
                {
                    lblAlerta.Text = "Digite um valor válido para Login e Senha";
                    lblAlerta.Visible = Visible;
                }
                else
                {
                    if (txtSenha.Text == user.senha)
                    {
                        Session["id"] = user.id;
                        Session["perfil"] = user.num_perfil;
                        Response.Redirect("Inicial.aspx");                       
                    }

                    else
                    {
                        lblAlerta.Text = "Digite um valor válido para Login e Senha";
                        lblAlerta.Visible = Visible;
                    }
                }
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/PainelUsuarios.aspx");
        }
    }

}