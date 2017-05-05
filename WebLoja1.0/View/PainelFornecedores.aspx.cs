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
    public partial class PainelFornecedores : System.Web.UI.Page
    {
        List<Fornecedores> listaFornecedores = new List<Fornecedores>();
        List<Estados> listaEstados = new List<Estados>();
        List<Cidades> listaCidades = new List<Cidades>();
        Fornecedores fornecedor = new Fornecedores();
        Cidades cidade = new Cidades();

        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;

        static bool flagNovo = true;
        static int id_fornecedor = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Cadastro e pesquisa de fornecedores";

            if (!IsPostBack)
            {
                preencherRadioListEstado();
                
                //validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);
                    //lblUser.Text = user.nome;
                    if (!teste.ValidaUser(id, perfil) || user.num_perfil == 3)
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

        private void preencherRadioListEstado()
        {
            ddlEstado.SelectedIndex = 0;
            
            listaEstados = controle.pesquisaGeralEstdos();
            ddlEstado.DataSource = listaEstados;
            ddlEstado.DataTextField = "estado";
            ddlEstado.DataValueField = "id";
            ddlEstado.DataBind();            
        }

        protected void btnNovo_Click(object sender, EventArgs e)
        {
            txtResponsavel.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;
            txtEndereço.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            ddlEstado.Enabled = true;
            btnSalvar.Enabled = true;
            btnPesquisar.Enabled = false;
            btnNovo.Enabled = false;
            flagNovo = true;
            chkStatus.Enabled = true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            chkStatus.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            txtNome.Enabled = true;
            txtCnpj.Enabled = true;
            txtResponsavel.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;
            txtEndereço.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            ddlEstado.Enabled = true;
            ddlCidade.Enabled = true;
            preencherRadioList(ddlEstado.SelectedIndex);
            ddlCidade.SelectedValue = controle.pesquisaFornecedorCpnj(txtCnpj.Text).id_Cidade.ToString();
            flagNovo = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            if (!txtCnpj.Text.All(char.IsNumber) && txtCnpj.Text.Length > 13)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Cnpj\" deve conter somente números e ao menos 14 caracteres.');", true);
            }            
            else if (!txtTelefone.Text.All(char.IsNumber) || txtTelefone.Text.Length < 7)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Telefone\" deve conter somente números e no minimo 8 caracteres.');", true);
            }
            else if (!txtCelular.Text.All(char.IsNumber) || txtTelefone.Text.Length < 8)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Celular\" deve conter somente números e no minimo 9 caracteres.');", true);
            }

            else
            {
                if (validaCampos() && flagNovo)
                {
                    fornecedor = new Fornecedores();
                    controle.salvarFornecedor(fornecedor);
                    fornecedor.nome = txtNome.Text.ToUpper().Trim();
                    fornecedor.contato = txtResponsavel.Text.ToUpper().Trim();
                    fornecedor.cnpj = txtCnpj.Text.Trim();
                    fornecedor.endereço = txtEndereço.Text.ToUpper().Trim();
                    fornecedor.numeral = txtNumero.Text.Trim();
                    fornecedor.bairro = txtBairro.Text.ToUpper().Trim();
                    fornecedor.telefone = txtTelefone.Text.Trim();
                    fornecedor.celular = txtCelular.Text.Trim();
                    fornecedor.id_Cidade = ddlCidade.SelectedIndex;
                    fornecedor.status = Convert.ToInt32(chkStatus.Checked);
                    controle.salvaAtualiza();
                    btnLimpar_Click(sender, e);
                }

                else if (validaCampos() && !flagNovo)
                {
                    fornecedor = new Fornecedores();
                    fornecedor = controle.pesquisaFornecedorById(id_fornecedor);
                    fornecedor.nome = txtNome.Text.ToUpper().Trim();
                    fornecedor.contato = txtResponsavel.Text.ToUpper().Trim();
                    fornecedor.cnpj = txtCnpj.Text.Trim();
                    fornecedor.endereço = txtEndereço.Text.ToUpper().Trim();
                    fornecedor.numeral = txtNumero.Text.Trim();
                    fornecedor.bairro = txtBairro.Text.ToUpper().Trim();
                    fornecedor.telefone = txtTelefone.Text.Trim();
                    fornecedor.celular = txtCelular.Text.Trim();
                    fornecedor.status = Convert.ToInt32(chkStatus.Checked);

                    cidade = controle.pesquisaCidade(ddlCidade.SelectedItem.Text);

                    fornecedor.id_Cidade = cidade.id;

                    controle.salvaAtualiza();
                    btnLimpar_Click(sender, e);
                }

                else
                {
                    lblAlertaPreenchimento.Visible = true;
                    lblAlertaPreenchimento.Text = "O preenchimento dos campos é obrigatório";
                }
            }
        }

        private bool validaCampos()
        {
            if (txtNome.Text.Equals("") || txtCnpj.Text.Equals("") || txtEndereço.Text.Equals("") || txtNumero.Text.Equals("") || txtTelefone.Text.Equals("") || ddlCidade.SelectedIndex == 0)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        private void limpaForm()
        {
            txtNome.Text = "";
            txtResponsavel.Text = "";
            txtCnpj.Text = "";
            txtEndereço.Text = "";
            txtNumero.Text = "";
            ddlCidade.SelectedIndex = -1;
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtBairro.Text = "";
            chkStatus.Checked = false;
            ddlCidade.SelectedIndex = -1;
            ddlEstado.SelectedIndex = -1;
            lblAlerta.Visible = false;
            lblAlertaPreenchimento.Visible = false;
            PanelPesquisa.Visible = false;
        }

        protected void btnSair_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            lblAlerta.Visible = false;
            PanelPesquisa.Visible = true;

            if (!txtNome.Text.Equals(""))
            {
                listaFornecedores = controle.pesquisaFornecedoresCompleta(txtNome.Text);
                carregaListaFornecedor(listaFornecedores);
            }

            else if (!txtCnpj.Text.Equals(""))
            {
                listaFornecedores = controle.pesquisaFornecedoresCompleta(txtCnpj.Text);
                carregaListaFornecedor(listaFornecedores);
            }

            else
            {
                lblAlerta.Visible = true;
                lblAlerta.Text = "É necessário ao menos colocar um dos paramêtros (CNPJ ou Nome) para pesquisa.";
            }      
            
        }

        private void carregaListaFornecedor(List<Fornecedores> listaFornecedores)
        {
            rblFornecedores.DataSource = listaFornecedores;
            rblFornecedores.DataTextField = "nome";
            rblFornecedores.DataValueField = "id";
            rblFornecedores.DataBind();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se SelectedIndex = 1, cadastrar nova cidade
            if(ddlCidade.SelectedIndex == 1)
            {
                ddlCidade.SelectedIndex = 0;
                panelCidade.Visible = true;
                PanelFornecedor.Visible = false;
            }
            
        }

        private void preencherRadioList(int idEstado)
        {
            listaCidades = controle.pesquisaCidadesPorEstado(idEstado);            
            ddlCidade.DataSource = listaCidades;
            ddlCidade.DataTextField = "cidade";
            ddlCidade.DataValueField = "id";
            ddlCidade.DataBind();
            ddlCidade.SelectedIndex = -1;
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEstado.SelectedIndex == 0)
            {
                ddlCidade.Enabled = false;
                ddlCidade.DataSource = null;
                ddlCidade.DataBind();
            }

            else
            {
                preencherRadioList(ddlEstado.SelectedIndex);
                ddlCidade.Enabled = true;                
            }
        }

        protected void btnExcluirCidade_Click(object sender, EventArgs e)
        {
            if (txtNovaCidade.Text.Equals(""))
            {
                lblAlertaCidade.Visible = true;
                lblAlertaCidade.Text = "O campo acima deve estar preenchido com o nome da cidade a ser excluida";
            }

            else
            {
                cidade = controle.pesquisaCidade(txtNovaCidade.Text);

                if (cidade == null)
                {
                    lblAlertaCidade.Visible = true;
                    lblAlertaCidade.Text = "Para exclusão de cidade a mesma deve existir na base de dados";
                }

                else
                {
                    controle.excluirCidade(cidade);
                    btnVoltar_Click(sender, e);
                }

            }

        }

        protected void btnSalvarCidade_Click(object sender, EventArgs e)
        {
            if (txtNovaCidade.Text.Equals(""))
            {
                lblAlertaCidade.Visible = true;
                lblAlertaCidade.Text = "O campo acima deve estar preenchido com o nome da cidade a ser incluida";
            }

            else
            {
                cidade = controle.pesquisaCidade(txtNovaCidade.Text);

                if (cidade == null)
                {
                    cidade = new Cidades();
                    controle.salvarCidade(cidade);
                    cidade.cidade = txtNovaCidade.Text;
                    cidade.id_Estado = ddlEstado.SelectedIndex;                    
                    controle.salvaAtualiza();
                    btnVoltar_Click(sender, e);
                }

                else
                {
                    lblAlertaCidade.Visible = true;
                    lblAlertaCidade.Text = "Para inclusão de cidade a mesma não deve existir na base de dados";
                }

            }

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            panelCidade.Visible = false;
            PanelFornecedor.Visible = true;
            txtNovaCidade.Text = "";
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            limpaForm();
            btnPesquisar.Enabled = true;
            btnNovo.Enabled = true;
            btnEditar.Enabled = false;
            chkStatus.Enabled = false;
            btnSalvar.Enabled = false;
            btnVoltar.Enabled = true;
            txtNome.Enabled = true;
            txtCnpj.Enabled = true;
            txtResponsavel.Enabled = false;
            txtTelefone.Enabled = false;
            txtCelular.Enabled = false;
            txtEndereço.Enabled = false;
            txtNumero.Enabled = false;
            txtBairro.Enabled = false;
            ddlCidade.Enabled = false;
            ddlEstado.Enabled = false;
        }

        protected void rblFornecedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            fornecedor = new Fornecedores();
            fornecedor = controle.pesquisaFornecedorById(Convert.ToInt32(rblFornecedores.SelectedValue));
            id_fornecedor = fornecedor.id;
            txtNome.Text = fornecedor.nome;
            txtCnpj.Text = fornecedor.cnpj;
            txtEndereço.Text = fornecedor.endereço;
            txtBairro.Text = fornecedor.bairro;
            txtCelular.Text = fornecedor.celular;
            txtNumero.Text = fornecedor.numeral;
            txtResponsavel.Text = fornecedor.contato;
            txtTelefone.Text = fornecedor.telefone;
            ddlEstado.SelectedIndex = fornecedor.Cidades.id_Estado;
            chkStatus.Checked = Convert.ToBoolean(fornecedor.status);
            preencherRadioList(ddlEstado.SelectedIndex);

            ddlCidade.SelectedItem.Text = fornecedor.Cidades.cidade;

            PanelPesquisa.Visible = false;
            btnPesquisar.Enabled = false;
            btnNovo.Enabled = false;
            btnEditar.Enabled = true;
            chkStatus.Enabled = false;
            txtNome.Enabled = false;
            txtCnpj.Enabled = false;

        }
    }
}