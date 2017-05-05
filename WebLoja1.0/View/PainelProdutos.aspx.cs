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
        static Estoque estoque = new Estoque();
        static Gerenciamento gerencia = new Gerenciamento();

        static Byte[] bytes;
        static System.Drawing.Image img;
        static bool flagNovo = true;
        int i;        

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMensagem.Text = "Catálogo de Produtos";
                                        
            if (!IsPostBack)
            {
                gerencia = controle.pesquisaGerenciamento(1);
                listaCompleta();

                //validação de acesso
                id = Convert.ToInt32(Session["id"]);
                perfil = Convert.ToInt32(Session["perfil"]);

                if (id != 0 || perfil != 0)
                {
                    user = controle.pesquisaUserId(id);

                    if (!teste.ValidaUser(id, perfil) || user.num_perfil == 3)
                    {
                        Response.Redirect("~/View/AcessoIndevido.aspx");
                    }
                }

                else
                {
                    Response.Redirect("~/View/AcessoIndevido.aspx");
                }

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
            
            gvlProdutos.DataSource = listaProdutos;
            gvlProdutos.DataBind();

            for (i = 0; i < gvlProdutos.Rows.Count; i++)
            {                                                    

                if (i < listaProdutos.Count)
                {
                    gvlProdutos.Rows[i].Cells[1].Text = listaProdutos[i].cod_produto;
                    gvlProdutos.Rows[i].Cells[2].Text = listaProdutos[i].desc_produto;
                    gvlProdutos.Rows[i].Cells[3].Text = listaProdutos[i].preco_compra.ToString("0.00");
                    gvlProdutos.Rows[i].Cells[4].Text = listaProdutos[i].preco_venda.ToString("0.00");
                    gvlProdutos.Rows[i].Cells[5].Text = listaProdutos[i].Estoque.qnt_atual.ToString();

                    if (listaProdutos[i].Estoque.qnt_minima >= listaProdutos[i].Estoque.qnt_atual)
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
            btnNovo.Enabled = false;
            btnPesquisar.Enabled = false;
            gvlProdutos.Enabled = false;
            pnlPrincipal.DefaultButton = btnSalvar.ID;
            btnExcluir.Enabled = true;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            produto = controle.pesquisaProdutoNome(gvlProdutos.SelectedValue.ToString());
            txtCodigo.Text = produto.cod_produto.ToString();
            txtCusto.Text = produto.preco_compra.ToString("0.00");
            txtNome.Text = produto.desc_produto;
            txtQnt.Text = produto.Estoque.qnt_atual.ToString();
            txtPreco.Text = produto.preco_venda.ToString("0.00");
            txtMinimo.Text = produto.Estoque.qnt_minima.ToString();
            txtMaximo.Text = produto.Estoque.qnt_maxima.ToString();
            txtLetraLocal.Text = produto.Estoque.letra_local;
            txtNumLocal.Text = produto.Estoque.num_local.ToString();
            txtRefLocal.Text = produto.Estoque.ref_local;
            txtIcms.Text = Convert.ToDecimal(produto.ICMS_pago).ToString("0.00");
            ddlMedida.SelectedItem.Text = produto.und_medida;
            ddlFornecedores.SelectedValue = produto.id_fornecedor.ToString();

            if (produto.imagem != null)
            {
                //converter bytes para image                
                System.Drawing.Image x = (Bitmap)((new ImageConverter()).ConvertFrom(produto.imagem));
                bytes = produto.imagem;
                img = x;
                img.Save("C:\\Users\\Bernardelli\\Documents\\Visual Studio 2015\\Projects\\WebLoja1.0\\WebLoja1.0\\Image\\temp.jpg");
            }            

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
            txtMaximo.Enabled = true;
            txtNumLocal.Enabled = true;
            txtLetraLocal.Enabled = true;
            txtRefLocal.Enabled = true;
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
            if(!txtCodigo.Text.Equals("") && !txtCusto.Text.Equals("") && !txtNome.Text.Equals("") && !txtPreco.Text.Equals("") && !txtIcms.Text.Equals("") && !txtQnt.Text.Equals("") && !txtMinimo.Text.Equals(""))
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
                //converter arquivo de imagem para byte array e image
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

            gvlProdutos.Enabled = true;

            //condicionais referente a regras de negócio
            //Icms inferior a 25% do preço de custo
            if (Convert.ToDouble(txtIcms.Text) > (Convert.ToDouble(txtCusto.Text) * 0.25))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"ICMS\" corresponde a mais de 25% do custo de compra, Edite o produto caso necessário.');", true);
            }
            //Lucro minimo de 30% sobre o custo
            if (Convert.ToDouble(txtPreco.Text) < (Convert.ToDouble(txtCusto.Text) + (Convert.ToDouble(txtCusto.Text) * gerencia.lucroMinimo)))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Preco\" está com uma taxa de lucro inferior ao determinado pela Administração.');", true);
            }

            //Condicionais para validação do preenchimento
            //campos preenchidos
            if (!validaCampos())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('Todos os campos devem ser preenchidos com valores válidos');", true);
            }
            //campo código numérico e com 13 digitos
            else if (txtCodigo.Text.All(char.IsLetter) || txtCodigo.Text.Length != 13)
            {
                txtCodigo.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Código\" deve ser exclusivamente numérico com 13 caracteres.');", true);
            }
            //campo de custo, icms e preço correspondendo a valor monetário
            else if (!txtCusto.Text.Any(char.IsNumber) || !txtCusto.Text.Any(char.IsPunctuation))
            {
                txtCusto.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Custo\" deve ser preenchido com o formato \"XX.XX\".');", true);
            }
            else if (!txtIcms.Text.Any(char.IsNumber) || !txtIcms.Text.Any(char.IsPunctuation))
            {
                txtIcms.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"ICMS\" deve ser preenchido com o formato \"XX.XX\".');", true);
            }
            else if (!txtPreco.Text.Any(char.IsNumber) || !txtPreco.Text.Any(char.IsPunctuation))
            {
                txtPreco.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Preço\" deve ser preenchido com o formato \"XX.XX\".');", true);
            }
            //quantidade numérica
            else if (!txtQnt.Text.All(char.IsNumber))
            {
                txtQnt.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Qnt\" deve ser preenchido com valores numéricos.');", true);
            }
            //preço superior ao custo
            else if (Convert.ToDouble(txtCusto.Text) >= Convert.ToDouble(txtPreco.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "message", "alert('O campo \"Preco\" obrigatóriamente deve ser maior que o \"Custo\".');", true);
            }

            //se flag novo = false, novo elemento
            else if (flagNovo)
            {

                txtCusto.Text = Convert.ToDecimal(txtCusto.Text).ToString("0.00");
                txtPreco.Text = Convert.ToDecimal(txtPreco.Text).ToString("0.00");
                txtIcms.Text = Convert.ToDecimal(txtIcms.Text).ToString("0.00");

                lblAlerta.Visible = false;

                produto = new Produtos();
                controle.salvarProduto(produto);
                produto.cod_produto = txtCodigo.Text.Trim();
                produto.preco_compra = Convert.ToDecimal(txtCusto.Text.Trim());
                produto.preco_venda = Convert.ToDecimal(txtPreco.Text.Trim());
                produto.ICMS_pago = Convert.ToDecimal(txtIcms.Text.Trim());
                produto.desc_produto = txtNome.Text.Trim().ToUpper();
                produto.id_fornecedor = Convert.ToInt32(ddlFornecedores.SelectedValue);
                produto.und_medida = ddlMedida.SelectedItem.Text;
                produto.imagem = bytes;
                produto.status = 1;
                controle.salvaAtualiza();

                GridViewRow newLine = new GridViewRow(gvlProdutos.Rows.Count + 1, gvlProdutos.Rows.Count + 1, DataControlRowType.DataRow, DataControlRowState.Normal);
                gvlProdutos.Controls.Add(newLine);

                estoque = new Estoque();
                controle.salvarEstoque(estoque);
                estoque.id_produto = produto.id;
                estoque.qnt_atual = Convert.ToInt32(txtQnt.Text.Trim());
                estoque.qnt_minima = Convert.ToInt32(txtMinimo.Text.Trim());
                estoque.qnt_maxima = Convert.ToInt32(txtMaximo.Text.Trim());
                estoque.num_local = Convert.ToInt32(txtNumLocal.Text.Trim());
                estoque.letra_local = txtLetraLocal.Text.Trim().ToUpper();
                estoque.ref_local = txtRefLocal.Text.Trim().ToUpper();
                controle.salvaAtualiza();

                limpaForm();
                listaCompleta();
            }

            //alteração de elemento existente na base de dados
            else if (!flagNovo)
            {
                txtCusto.Text = Convert.ToDecimal(txtCusto.Text).ToString("0.00");
                txtPreco.Text = Convert.ToDecimal(txtPreco.Text).ToString("0.00");
                txtIcms.Text = Convert.ToDecimal(txtIcms.Text).ToString("0.00");

                int id = produto.id;
                produto = new Produtos();
                produto = controle.pesquisaProdutoId(id);
                lblAlerta.Visible = false;
                produto.cod_produto = txtCodigo.Text;
                produto.preco_compra = Convert.ToDecimal(txtCusto.Text);
                produto.preco_venda = Convert.ToDecimal(txtPreco.Text);
                produto.ICMS_pago = Convert.ToDecimal(txtIcms.Text);
                produto.desc_produto = txtNome.Text;
                produto.id_fornecedor = Convert.ToInt32(ddlFornecedores.SelectedValue);
                produto.und_medida = ddlMedida.SelectedItem.Text;
                produto.imagem = bytes;

                estoque = new Estoque();
                estoque = controle.pesquisaProdEstoqueId(id);
                estoque.qnt_atual = Convert.ToInt32(txtQnt.Text);
                estoque.qnt_minima = Convert.ToInt32(txtMinimo.Text);
                estoque.qnt_maxima = Convert.ToInt32(txtMaximo.Text);
                estoque.num_local = Convert.ToInt32(txtNumLocal.Text);
                estoque.letra_local = txtLetraLocal.Text;
                estoque.ref_local = txtRefLocal.Text;
                controle.salvaAtualiza();

                limpaForm();
                listaCompleta();
            }
            
        }

        private void limpaForm()
        {
            gvlProdutos.Enabled = true;
            gvlProdutos.SelectedIndex = -1;
            lblAlerta.Visible = false;
            txtCodigo.Enabled = false;
            txtCusto.Enabled = false;
            txtNome.Enabled = false;
            txtPreco.Enabled = false;
            txtIcms.Enabled = false;
            txtQnt.Enabled = false;
            txtMinimo.Enabled = false;
            txtMaximo.Enabled = false;
            txtNumLocal.Enabled = false;
            txtLetraLocal.Enabled = false;
            txtRefLocal.Enabled = false;
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
            txtMaximo.Text = "";
            txtQnt.Text = "";
            txtNumLocal.Text = "";
            txtLetraLocal.Text = "";
            txtRefLocal.Text = "";
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
            txtMaximo.Enabled = true;
            txtNumLocal.Enabled = true;
            txtLetraLocal.Enabled = true;
            txtRefLocal.Enabled = true;
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
                if (controle.pesquisaProdutosNomeId(txtPesquisa.Text.ToUpper()).Count > 0)
                {
                    listaProdutos = controle.pesquisaGeralProd();

                    for (i = 0; i < listaProdutos.Count; i++)
                    {
                        if (gvlProdutos.Rows[i].Cells[2].Text.Contains(txtPesquisa.Text.ToUpper()))
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
                    listaCompleta();
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