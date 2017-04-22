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
        List<Usuarios> ListaUser = new List<Usuarios>();

        //static bool flagNovo = true;

        protected void Page_Load(object sender, EventArgs e)
        {          
            if (!IsPostBack)
            {
                lblMensagem.Text = "Cadastro e validação de Usuários";

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
                        validar_usuarios();
                    }
                }

                else
                {
                    cadastrar_usuario();
                    //validação temporaria
                    //validar_usuarios();
                }

            }

        }

        public void cadastrar_usuario()
        {
            lblMensagem.Text = "Cadastrar novo Usuário";
            pnlCadastro.Visible = true;
        }

        //public void validar_usuarios(Usuarios user)
        public void validar_usuarios()
        {
            carregaLista();
            lblMensagem.Text = "Validação de Usuários";
            pnlValida.Visible = true;
            pnlUser.Visible = false;
        }

        private void carregaLista()
        {
            ListaUser = new List<Usuarios>();
            ListaUser = controle.usuariosInvalidos();

            if (ListaUser.Count == 0)
            {
                lblAlertaValida.Text = "Não existem usuários pendentes de validação, pressione \"Cancelar\".";
                lblAlertaValida.Visible = true;
            }
            else
            {
                lblAlertaValida.Visible = false;
                rblUsuarios.DataSource = ListaUser;
                rblUsuarios.DataTextField = "nome";
                rblUsuarios.DataValueField = "id";
                rblUsuarios.DataBind();

                for (int i = 0; i < ListaUser.Count; i++)
                {
                    string perfil = "";

                    if (ListaUser[i].num_perfil == 1)
                    {
                        perfil = "Administrador";
                    }

                    else if (ListaUser[i].num_perfil == 2)
                    {
                        perfil = "Gerente";
                    }

                    else if (ListaUser[i].num_perfil == 3)
                    {
                        perfil = "Caixa";
                    }

                    else if (ListaUser[i].num_perfil == 4)
                    {
                        perfil = "Operador";
                    }

                    rblUsuarios.Items[i].Text = ListaUser[i].nome + " - " + perfil;
                }
            }
                       
        }

        protected void btnConfirma_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                user = new Usuarios();
                controle.salvarUsuario(user);
                user.nome = txtNome.Text;
                user.senha = txtSenha.Text;
                user.num_perfil = novoPerfil;
                controle.salvaAtualiza();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Usuário "+txtNome.Text+" salvo com sucesso !!!');", true);
                Response.Redirect("~/View/index.aspx");
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

        protected void btnCancelaValida_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }

        protected void btnCancela_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/index.aspx");
        }

        protected void rdbAdmin_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 1;
            btnConfirma.Enabled = true;
        }

        protected void rdbGerente_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 2;
            btnConfirma.Enabled = true;
        }

        protected void rdbCaixa_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 3;
            btnConfirma.Enabled = true;
        }

        protected void rdbOperador_CheckedChanged(object sender, EventArgs e)
        {
            novoPerfil = 4;
            btnConfirma.Enabled = true;
        }

        protected void rblUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConfirmaValido.Enabled = true;
            lblLista.Visible = false;
            rblUsuarios.Visible = false;
            pnlUser.Visible = true;

            user = controle.pesquisaUserId(Convert.ToInt32(rblUsuarios.SelectedValue));
            txtNomeValida.Text = user.nome;
            if(user.num_perfil == 1)
            {
                rdbAdminVal.Checked = true;
            }
            else if (user.num_perfil == 2)
            {
                rdbGerenteVal.Checked = true;
            }
            else if (user.num_perfil == 3)
            {
                rdbCaixaVal.Checked = true;
            }
            else if (user.num_perfil == 4)
            {
                rdbOperadorVal.Checked = true;
            }
        }

        protected void btnConfirmaValido_Click(object sender, EventArgs e)
        {
            if (txtRegistro.Text.Equals(""))
            {
                lblAlertaValida.Text = "É necessário a inclusão de um registro para validação de novo usuário";
                lblAlertaValida.Visible = true;
            }
            else if (!txtRegistro.Text.All(char.IsNumber))
            {
                lblAlertaValida.Text = "O campo registro é exclusivamente numérico";
                lblAlertaValida.Visible = true;
            }
            else
            {
                lblAlertaValida.Visible = false;
                user = controle.pesquisaUserId(Convert.ToInt32(rblUsuarios.SelectedValue));
                user.registro = Convert.ToInt32(txtRegistro.Text);
                user.status = 1;
                controle.salvaAtualiza();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alerta", "alert('Usuário " + txtNomeValida.Text + " esta habilitado no sistema.');", true);
                Response.Redirect("~/View/Inicial.aspx");
            }

        }
        
    }

}