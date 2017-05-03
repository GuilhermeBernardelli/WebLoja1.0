using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebLoja1._0.Model;
using WebLoja1._0.Control;
using System.IO;
using System.Drawing;
using System.Web.UI.WebControls;

namespace WebLoja1._0.View
{
    public partial class PainelProdutos : System.Web.UI.Page
    {
        Usuarios user = new Usuarios();
        Valida teste = new Valida();
        static int id;
        static int perfil;

        Controle controle = new Controle();
        static List<Produtos> listaProdutos = new List<Produtos>();
        static List<Fornecedores> listaFornecedores = new List<Fornecedores>();
        static Produtos produto = new Produtos();

        static Byte[] bytes;
        static System.Drawing.Image img;
        static bool flagNovo = true;
        int i;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Catálogo de Produtos";
                             
            if (!IsPostBack)
            {                
                listaCompleta();

                /*/validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);

                    if (!teste.ValidaUser(id, perfil))
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }
                }

                else
                {
                    Response.Redirect("~/View/AcessoIndevido.aspx");
                }*/

                limpaImg();  
                btnLimpar_Click(sender, e);
                listaFornecedores = controle.pesquisaFornecedores("");
                ddlFornecedores.DataSource = listaFornecedores;
                ddlFornecedores.DataTextField = "nome";
                ddlFornecedores.DataValueField = "id";
                ddlFornecedores.DataBind();
            }

        }

        private void listaCompleta()
        {
            listaProdutos = controle.pesquisaGeralProd();            

            for (i = 0; i < gvlProdutos.Rows.Count; i++)
            {
                if (i < listaProdutos.Count)
                {
                    gvlProdutos.Rows[i].Cells[1].Text = listaProdutos[i].cod_produto;
                    gvlProdutos.Rows[i].Cells[2].Text = listaProdutos[i].desc_produto;
                    gvlProdutos.Rows[i].Cells[3].Text = listaProdutos[i].preco_compra.ToString("0.00");
                    gvlProdutos.Rows[i].Cells[4].Text = listaProdutos[i].preco_venda.ToString("0.00");
                    gvlProdutos.Rows[i].Cells[5].Text = listaProdutos[i].qnt_estoque.ToString();

                    if (listaProdutos[i].estoque_minimo >= listaProdutos[i].qnt_estoque)
                    {
                        gvlProdutos.Rows[i].ForeColor = Color.Red;
                        gvlProdutos.Rows[i].Font.Bold = true;
                    }
                    else
                    {
                        gvlProdutos.Rows[i].ForeColor = Color.Blue;
                        gvlProdutos.Rows[i].Font.Bold = false;
                    }
                    gvlProdutos.Rows[i].RowState = DataControlRowState.Normal;
                    gvlProdutos.Rows[i].Visible = true;
                }
                else
                {
                    gvlProdutos.Rows[i].Visible = false;
                }

            }

        }

        private void limpaImg()
        {

            img = new Bitmap(@"C:\Users\Bernardelli\Documents\Visual Studio 2015\Projects\WebLoja1.0\WebLoja1.0\Image\Branco.jpg", true);
            img.Save("C:\\Users\\Bernardelli\\Documents\\Visual Studio 2015\\Projects\\WebLoja1.0\\WebLoja1.0\\Image\\temp.jpg");
            Response.Flush();

        }   
        
        protected void gvlProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlPrincipal.DefaultButton = btnSalvar.ID;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            produto = controle.pesquisaProdutoNome(gvlProdutos.SelectedValue.ToString());
            txtCodigo.Text = produto.cod_produto.ToString();
            txtCusto.Text = produto.preco_compra.ToString("0.00");
            txtNome.Text = produto.desc_produto;
            txtQnt.Text = produto.qnt_estoque.ToString();
            txtPreco.Text = produto.preco_venda.ToString("0.00");
            txtMinimo.Text = produto.estoque_minimo.ToString();
            txtIcms.Text = Convert.ToDecimal(produto.ICMS_pago).ToString("0.00");
            ddlMedida.SelectedItem.Text = produto.und_medida;
            ddlFornecedores.SelectedValue = produto.id_fornecedor.ToString();
            //converter bytes para image
            //img = produto.image;

        }
        
        protected void btnNovo_Click(object sender, EventArgs e)
        {
            flagNovo = true;
            txtCodigo.Enabled = true;
            txtCusto.Enabled = true;
            txtNome.Enabled = true;
            txtPesquisa.Enabled = true;
            txtPreco.Enabled = true;
            txtIcms.Enabled = true;
            txtQnt.Enabled = true;
            txtMinimo.Enabled = true;
            ddlFornecedores.Enabled = true;
            ddlMedida.Enabled = true;
            btnPesquisar.Enabled = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
            btnSalvar.Enabled = true;
            Upload.Enabled = true;
            btnImage.Enabled = true;            
        }

        private bool validaCampos()
        {
            if(!txtCodigo.Text.Equals("") && !txtCusto.Text.Equals("") && !txtNome.Text.Equals("") && !txtPreco.Text.Equals("") && !txtQnt.Text.Equals("") && !txtMinimo.Text.Equals(""))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        protected void btnImage_Click(object sender, EventArgs e)
        {

            if (Upload.HasFile)
            {
                string filename = Upload.PostedFile.FileName;
                string filePath = Path.GetFileName(filename);
                Stream fs = Upload.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                
                //colocar image na label
                img.Save("C:\\Users\\Bernardelli\\Documents\\Visual Studio 2015\\Projects\\WebLoja1.0\\WebLoja1.0\\Image\\temp.jpg");

            }

        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

            if (validaCampos() && flagNovo)
            {
                lblAlerta.Visible = false;
                produto = new Produtos();
                controle.salvarProduto(produto);
                produto.cod_produto = txtCodigo.Text;
                produto.preco_compra = Convert.ToDouble(txtCusto.Text);
                produto.preco_venda = Convert.ToDouble(txtPreco.Text);
                produto.ICMS_pago = Convert.ToDouble(txtIcms.Text);
                produto.desc_produto = txtNome.Text;
                produto.estoque_minimo = Convert.ToInt32(txtMinimo.Text);
                produto.id_fornecedor = Convert.ToInt32(ddlFornecedores.SelectedValue);
                produto.qnt_estoque = Convert.ToInt32(txtQnt.Text);
                produto.und_medida = ddlMedida.SelectedItem.Text;
                produto.imagem = bytes;
                produto.status = 1;
                controle.salvaAtualiza();
            }

            else if (validaCampos() && !flagNovo)
            {
                int id = produto.id;
                produto = new Produtos();
                produto = controle.pesquisaProdutoId(id);
                lblAlerta.Visible = false;
                produto.cod_produto = txtCodigo.Text;
                produto.preco_compra = Convert.ToDouble(txtCusto.Text);
                produto.preco_venda = Convert.ToDouble(txtPreco.Text);
                produto.ICMS_pago = Convert.ToDouble(txtIcms.Text);
                produto.estoque_minimo = Convert.ToInt32(txtMinimo.Text);
                produto.desc_produto = txtNome.Text;
                produto.id_fornecedor = Convert.ToInt32(ddlFornecedores.SelectedValue);
                produto.qnt_estoque = Convert.ToInt32(txtQnt.Text);
                produto.und_medida = ddlMedida.SelectedItem.Text;
                produto.imagem = bytes;
                controle.salvaAtualiza();
            }

            else
            {
                lblAlerta.Visible = true;
                lblAlerta.Text = "É necessário o preenchimento de todos os campos";
            }
            limpaForm();
            listaCompleta();
        }

        private void limpaForm()
        {
            lblAlerta.Visible = false;
            txtCodigo.Enabled = false;
            txtCusto.Enabled = false;
            txtNome.Enabled = false;
            txtPreco.Enabled = false;
            txtMinimo.Enabled = false;
            txtIcms.Enabled = false;
            txtQnt.Enabled = false;
            ddlFornecedores.Enabled = false;
            ddlMedida.Enabled = false;
            btnPesquisar.Enabled = true;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            Upload.Enabled = false;
            btnImage.Enabled = false;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodigo.Text = "";
            txtCusto.Text = "";
            txtNome.Text = "";
            txtIcms.Text = "";
            txtPreco.Text = "";
            txtMinimo.Text = "";
            txtQnt.Text = "";
            ddlFornecedores.SelectedIndex = -1;
            ddlMedida.SelectedIndex = -1;
            txtPesquisa.Text = "";
            panelPesquisa.Visible = false;
            limpaImg();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/Inicial.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            pnlPrincipal.DefaultButton = btnSalvar.ID;
            flagNovo = false;
            txtCodigo.Enabled = true;
            txtCusto.Enabled = true;
            txtNome.Enabled = true;
            txtPreco.Enabled = true;
            txtIcms.Enabled = true;
            txtMinimo.Enabled = true;
            txtQnt.Enabled = true;
            ddlFornecedores.Enabled = true;
            ddlMedida.Enabled = true;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            Upload.Enabled = true;
            btnImage.Enabled = true;
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            id = produto.id;
            produto = new Produtos();
            produto = controle.pesquisaProdutoId(id);
            produto.status = 0;
            controle.salvaAtualiza();            
            limpaForm();
            listaCompleta();            
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            btnLimpar_Click(sender, e);
            panelPesquisa.Visible = true;
            btnPesquisar.Enabled = false;
            btnLimpar.TabIndex = 2;
            txtPesquisa.Focus();
            pnlPrincipal.DefaultButton = btnBusca.ID;
        }
        
        protected void btnBusca_Click(object sender, ImageClickEventArgs e)
        {
            if (txtPesquisa.Text.Length < 3)
            {
                lblListaTitulo.Text = "É necessário ao menos 3 caracteres para realizar uma busca";
            }

            else
            {
                if (controle.pesquisaProdutosNomeId(txtPesquisa.Text).Count > 0)
                {
                    listaProdutos = controle.pesquisaGeralProd();

                    for (i = 0; i < listaProdutos.Count; i++)
                    {
                        if (gvlProdutos.Rows[i].Cells[2].Text.Contains(txtPesquisa.Text) )
                        {
                            gvlProdutos.Rows[i].Visible = true;
                            lblListaTitulo.Text = "Relação de produtos que contém a expressão \"" + txtPesquisa.Text + "\" ";
                        }

                        else if (gvlProdutos.Rows[i].Cells[1].Text.Equals(txtPesquisa.Text))
                        {
                            gvlProdutos.Rows[i].Visible = true;
                            lblListaTitulo.Text = "Produto com o código: " + txtPesquisa.Text;
                        }

                        else
                        {
                            gvlProdutos.Rows[i].Visible = false;
                        }

                    }                    
                    txtPesquisa.Text = "";
                    Response.Flush();
                }

                else
                {
                    lblListaTitulo.Text = "Não existem produtos com o código ou expressão \"" + txtPesquisa.Text + "\" ";
                    txtPesquisa.Text = "";
                }

            }

        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            flagNovo = true;
            limpaForm();            

            int i = 0;

            listaProdutos = controle.pesquisaGeralProd();

            for (i = 0; i < listaProdutos.Count; i++)
            {                
                gvlProdutos.Rows[i].Visible = true;
            }

            lblListaTitulo.Text = "Relação de produtos";
        }
        
    }
}