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
    public partial class PainelClientes : System.Web.UI.Page
    {
        List<Clientes> listaClientes = new List<Clientes>();
        List<Estados> listaEstados = new List<Estados>();
        List<Cidades> listaCidades = new List<Cidades>();
        Clientes cliente = new Clientes();
        Cidades cidade = new Cidades();
        Controle controle = new Controle();
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;
        static bool flagNovo = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Cadastro e pesquisa de Clientes";

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
                    if (!teste.ValidaUser(id, perfil))
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
            txtCpf.Enabled = true;
            txtResponsavel.Enabled = true;
            txtTelefone.Enabled = true;
            txtCelular.Enabled = true;
            txtEndereço.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            ddlEstado.Enabled = true;
            ddlCidade.Enabled = true;
            preencherRadioList(ddlEstado.SelectedIndex);
            ddlCidade.SelectedValue = controle.pesquisaClienteCpf(txtCpf.Text).id_Cidade.ToString();
            flagNovo = false;
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaCampos() && flagNovo)
            {
                cliente = new Clientes();
                controle.salvarCliente(cliente);
                cliente.nome = txtNome.Text;
                cliente.contato = txtResponsavel.Text;
                cliente.cpf = txtCpf.Text;
                cliente.endereço = txtEndereço.Text;
                cliente.numeral = txtNumero.Text;
                cliente.bairro = txtBairro.Text;
                cliente.telefone = txtTelefone.Text;
                cliente.celular = txtCelular.Text;
                cliente.id_Cidade = Convert.ToInt32(ddlCidade.SelectedValue);
                cliente.status = Convert.ToInt32(chkStatus.Checked);
                controle.salvaAtualiza();
                btnLimpar_Click(sender, e);
            }

            else if (validaCampos() && !flagNovo)
            {
                cliente = controle.pesquisaClienteCpf(txtCpf.Text);
                cliente.nome = txtNome.Text;
                cliente.contato = txtResponsavel.Text;
                cliente.cpf = txtCpf.Text;
                cliente.endereço = txtEndereço.Text;
                cliente.numeral = txtNumero.Text;
                cliente.bairro = txtBairro.Text;
                cliente.telefone = txtTelefone.Text;
                cliente.celular = txtCelular.Text;
                cliente.status = Convert.ToInt32(chkStatus.Checked);

                cidade = controle.pesquisaCidade(ddlCidade.SelectedItem.Text);

                cliente.id_Cidade = cidade.id;

                controle.salvaAtualiza();
                btnLimpar_Click(sender, e);
            }

            else
            {
                lblAlertaPreenchimento.Visible = true;
                lblAlertaPreenchimento.Text = "O preenchimento dos campos é obrigatório";
            }
        }

        private bool validaCampos()
        {
            if (txtNome.Text.Equals("") || txtCpf.Text.Equals("") || txtEndereço.Text.Equals("") || txtNumero.Text.Equals("") || txtTelefone.Text.Equals("") || ddlCidade.SelectedIndex == 0)
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
            txtCpf.Text = "";
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
                listaClientes = controle.pesquisaClientesCompleta(txtNome.Text);
                carregaListaCliente(listaClientes);
            }

            else if (!txtCpf.Text.Equals(""))
            {
                listaClientes = controle.pesquisaClientesCompleta(txtCpf.Text);
                carregaListaCliente(listaClientes);
            }

            else
            {
                lblAlerta.Visible = true;
                lblAlerta.Text = "É necessário ao menos colocar um dos paramêtros (CNPJ ou Nome) para pesquisa.";
            }

        }

        private void carregaListaCliente(List<Clientes> listaClientes)
        {
            rblClientes.DataSource = listaClientes;
            rblClientes.DataTextField = "nome";
            rblClientes.DataValueField = "id";
            rblClientes.DataBind();
        }

        protected void ddlCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se SelectedIndex = 1, cadastrar nova cidade
            if (ddlCidade.SelectedIndex == 1)
            {
                ddlCidade.SelectedIndex = 0;
                panelCidade.Visible = true;
                PanelCliente.Visible = false;
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
                    cidade.id_Estado = Convert.ToInt32(ddlEstado.SelectedValue);
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
            PanelCliente.Visible = true;
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
            txtCpf.Enabled = true;
            txtResponsavel.Enabled = false;
            txtTelefone.Enabled = false;
            txtCelular.Enabled = false;
            txtEndereço.Enabled = false;
            txtNumero.Enabled = false;
            txtBairro.Enabled = false;
            ddlCidade.Enabled = false;
            ddlEstado.Enabled = false;
        }

        protected void rblClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            cliente = new Clientes();
            cliente = controle.pesquisaClienteById(rblClientes.SelectedIndex);
            txtNome.Text = cliente.nome;
            txtCpf.Text = cliente.cpf;
            txtEndereço.Text = cliente.endereço;
            txtBairro.Text = cliente.bairro;
            txtCelular.Text = cliente.celular;
            txtNumero.Text = cliente.numeral;
            txtResponsavel.Text = cliente.contato;
            txtTelefone.Text = cliente.telefone;
            ddlEstado.SelectedValue = cliente.Cidades.id_Estado.ToString();
            ddlCidade.SelectedValue = cliente.id_Cidade.ToString();
            chkStatus.Checked = Convert.ToBoolean(cliente.status);
            preencherRadioList(ddlEstado.SelectedIndex);

            //ddlCidade.SelectedItem.Text = cliente.Cidades.cidade;

            PanelPesquisa.Visible = false;
            btnPesquisar.Enabled = false;
            btnNovo.Enabled = false;
            btnEditar.Enabled = true;
            chkStatus.Enabled = false;
            txtNome.Enabled = false;
            txtCpf.Enabled = false;

        }
    }
}