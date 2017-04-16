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

    public partial class PainelUsuarios : System.Web.UI.Page
    {                
        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil = 0;

        static int novoPerfil;

        static bool flagNovo = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Cadastro e validação de Usuários";

            if (!IsPostBack)
            {

                //validação de acesso
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
                    else
                    {
                        validar_usuarios(user);
                    }
                }

                else
                {
                    cadastrar_usuario();
                }

            }

        }

        public void cadastrar_usuario()
        {
            lblMensagem.Text = "Cadastrar novo Usuário";
            pnlCadastro.Visible = true;
        }

        public void validar_usuarios(Usuarios user)
        {
            lblMensagem.Text = "Validação de Usuários";
            pnlValida.Visible = true;
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                user = new Usuarios();
                user.nome = txtNome.Text;
                user.senha = txtSenha.Text;
                user.num_perfil = novoPerfil;
            }
        }

        private bool validaCampos()
        {
            if (txtNome.Text.Equals(""))
            {
                lblAlertaCadastro.Visible = true;
                lblAlertaCadastro.Text = "O campo nome não deve estar em branco";
                return false;
            }

            else if (txtSenha.Text.Equals(""))
            {
                lblAlertaCadastro.Visible = true;
                lblAlertaCadastro.Text = "O campo senha deve ser preenchido"; 
                return false;
            }

            else if (txtSenha.Text.Length != 8 || txtSenha.Text.All(char.IsDigit) || txtSenha.Text.All(char.IsLetter))
            {
                lblAlertaCadastro.Visible = true;
                lblAlertaCadastro.Text = "O campo senha deve conter 8 digitos, ser composto por letras e números";
                return false;
            }

            else if (!txtSenha.Text.Equals(txtConfirma.Text))
            {
                lblAlertaCadastro.Visible = true;
                lblAlertaCadastro.Text = "A senha e a confirmação não conhecidem";
                return false;
            }

            else if(novoPerfil == 0)
            {
                lblAlertaCadastro.Visible = true;
                lblAlertaCadastro.Text = "Deve ser selecionado o tipo de perfil do usuário";
                return false;
            }

            else
            {
                lblAlertaCadastro.Visible = false;
                return true;
            }
            
        }

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/index.aspx");
        }

        protected void rdbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 1;
        }

        protected void rdbGerente_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 2;
        }

        protected void rdbCaixa_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 3;
        }

        protected void rdbOperador_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 4;
        }
    }

}