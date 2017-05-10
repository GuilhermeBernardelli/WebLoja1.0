using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebLoja1._0.Model
{
    public class Repository : DbContext
    {
        DatabaseEntities dataEntity = new DatabaseEntities();

        public void salvaAlteracao()
        {
            dataEntity.SaveChanges();
        }

        public void salvarNovoUsuario(Usuarios usuario)
        {
            dataEntity.Usuarios.Add(usuario);
        }

        public void excluirUsuarioExistente(Usuarios usuario)
        {
            dataEntity.Usuarios.Remove(usuario);
        }

        public Usuarios pesquisaSimplesUser(string valor)
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.nome.Equals(valor))
                    select usuarios).SingleOrDefault();
        }

        public Usuarios pesquisaUserById(int Id)
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.id == Id)
                    select usuarios).SingleOrDefault();
        }

        public List<Usuarios> pesquisaUsuariosInvalidos()
        {
            return (from usuarios in dataEntity.Usuarios
                    where (usuarios.status == 0)
                    select usuarios).ToList();
        }

        public Produtos pesquisaProdutoByNome(string pesquisa)
        {
            return (from produtos in dataEntity.Produtos
                    where (produtos.desc_produto.Equals(pesquisa))
                    select produtos).SingleOrDefault();
        }

        public void salvarNovoProduto(Produtos produto)
        {
            dataEntity.Produtos.Add(produto);
        }

        public Produtos pesquisaProdutoByCodigo(string codigo)
        {
            return(from produtos in dataEntity.Produtos
                    where (produtos.cod_produto.Equals(codigo))
                    select produtos).SingleOrDefault();
        }

        public List<Estoque> pesquisaEstoqueOrdened()
        {
            return (from estoque in dataEntity.Estoque
                    where estoque.Produtos.status == 1
                    orderby estoque.qnt_atual
                    select estoque).ToList();
        }

        public List<Tipos_Movimentacao> pesquisaTiposMov()
        {
            List<Tipos_Movimentacao> lista = (from tipoMov in dataEntity.Tipos_Movimentacao
                                              where tipoMov.mostrar == true
                                              orderby tipoMov.descricao
                                              select tipoMov).ToList();

            List<Tipos_Movimentacao> listaAux = new List<Tipos_Movimentacao>();
            
            for (int i = 0; i < lista.Count; i++)
            {
                if(i == 0)
                {
                    listaAux.Add(lista[i]);
                }
                else if (!lista[i].descricao.Equals(lista[i - 1].descricao))
                {
                    listaAux.Add(lista[i]);
                }
               
            }

            return listaAux;
        }

        public List<Tipos_Movimentacao> pesquisaTiposMovimentoByDesc(string busca)
        {

            List<Tipos_Movimentacao> lista = (from tipos in dataEntity.Tipos_Movimentacao
                                              where tipos.descricao.Equals(busca)
                                              && tipos.mostrar == true
                                              orderby tipos.sub_tipo
                                              select tipos).ToList();

            List<Tipos_Movimentacao> listaAux = new List<Tipos_Movimentacao>();

            for (int i = 0; i < lista.Count; i++)
            {
                if (i == 0)
                {
                    listaAux.Add(lista[i]);
                }
                else if (!lista[i].sub_tipo.Equals(lista[i - 1].sub_tipo))
                {
                    listaAux.Add(lista[i]);
                }

            }

            return listaAux;
        }

        public Produtos pesquisaProdutoById(int pesquisa)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.id == pesquisa)
                    select produto).SingleOrDefault();
        }

        public List<Tipos_Movimentacao> pesquisaSubTiposMovimentoByDesc(string busca)
        {
            return (from tipos in dataEntity.Tipos_Movimentacao
                    where tipos.sub_tipo.Equals(busca)
                    orderby tipos.forma_pag
                    select tipos).ToList();
        }

        public List<Produtos> pesquisaProdutoByNomeId(string busca)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.desc_produto.Contains(busca)
                    || produto.cod_produto == busca)
                    select produto).ToList();
        }

        public Gerenciamento pesquisaGerenciamento(int pesquisa)
        {
            return (from gerencia in dataEntity.Gerenciamento
                    where gerencia.Id == pesquisa
                    select gerencia).SingleOrDefault();
        }

        public List<Produtos> pesquisaProdutos()
        {
            return (from produto in dataEntity.Produtos
                    where produto.status == 1
                    select produto).ToList();
        }

        public List<Estados> pesquisaEstados()
        {
            return (from estados in dataEntity.Estados
                    orderby estados.id
                    select estados).ToList();
        }

        public List<Movimentos> pesquisaMovimentoIntervalo(DateTime dtInicio, DateTime dtFim)
        {
            return (from movimento in dataEntity.Movimentos
                    where movimento.data >= dtInicio
                    && movimento.data <= dtFim
                    select movimento).ToList();
        }

        public List<Cidades> pesquisaCidadesByEstado(int pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where (cidade.id_Estado == (pesquisa)
                    || cidade.id_Estado == 0)
                    orderby cidade.cidade
                    select cidade).ToList();
        }

        public int pesquisaMovimentoID(string desc, string tipo, string form)
        {
            if (form != null)
            {
                return (from movimento in dataEntity.Tipos_Movimentacao
                        where (movimento.descricao.Equals(desc)
                        && movimento.sub_tipo.Equals(tipo)
                        && movimento.forma_pag.Equals(form))
                        select movimento.id).SingleOrDefault();
            }
            else
            {
                return (from movimento in dataEntity.Tipos_Movimentacao
                        where (movimento.descricao.Equals(desc)
                        && movimento.sub_tipo.Equals(tipo))
                        select movimento.id).SingleOrDefault();
            }
            
        }

        public Cidades pesquisaCidadeByName(string pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where (cidade.cidade.Equals(pesquisa))                    
                    select cidade).SingleOrDefault();
        }

        public void salvarNovaCidade(Cidades cidade)
        {
            dataEntity.Cidades.Add(cidade);
        }

        public void removerCidade(Cidades cidade)
        {
            dataEntity.Cidades.Remove(cidade);
        }

        public List<Vendas> pesquisaVendas()
        {
            return (from venda in dataEntity.Vendas               
                    select venda).ToList();
        }

        public List<Pagamentos> pesquisaPagamentosTotais()
        {
            return (from pagamento in dataEntity.Pagamentos
                    where pagamento.tipoPag != null
                    orderby pagamento.dataPagamento
                    select pagamento).ToList();
        }

        public List<Movimentos> pesquisaMovimentosTotais()
        {
            return (from movimento in dataEntity.Movimentos
                    orderby movimento.id_tipo
                    select movimento).ToList();
        }

        public Clientes pesquisaClienteId(int id)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.id == (id))
                    select cliente).SingleOrDefault();
        }

        public Tipos_Movimentacao pesquisaTipoMovById(int? id_tipo)
        {
            return (from tipos in dataEntity.Tipos_Movimentacao
                    where tipos.id == (id_tipo)
                    select tipos).SingleOrDefault();
        }

        public void salvarNovoCliente(Clientes cliente)
        {
            dataEntity.Clientes.Add(cliente);
        }

        public List<Clientes> pesquisaClienteByCpfOrNome(string pesquisa)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.cpf.Equals(pesquisa)
                    || (cliente.nome.Contains(pesquisa)
                    && cliente.status == 1))        
                    select cliente).ToList();
        }

        public Clientes pesquisaClienteByCpf(string cpf)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.cpf.Equals(cpf))
                    select cliente).SingleOrDefault();
        }

        public List<Produtos> pesquisaProdutosValidoByName(string pesquisa)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.desc_produto.Contains(pesquisa)
                    && produto.status == 1)
                    orderby produto.desc_produto
                    select produto).ToList();
        }

        public void salvarNovoPagamento(Pagamentos pagamento)
        {
            dataEntity.Pagamentos.Add(pagamento);
        }

        public void salvarNovoMovimento(Movimentos movimento)
        {
            dataEntity.Movimentos.Add(movimento);
        }

        public void salvarNovoProdutoVendido(Vendas_Produtos prodVendido)
        {
            dataEntity.Vendas_Produtos.Add(prodVendido);
        }

        public Vendas pesquisaVendabyID(int valor)
        {
            return (from venda in dataEntity.Vendas
                    where (venda.id == valor)
                    select venda).SingleOrDefault();
        }

        public Estoque pesquisaEstoqueByProdID(int valor)
        {
            return (from estoque in dataEntity.Estoque
                    where (estoque.id_produto == valor)
                    select estoque).SingleOrDefault();
        }

        public void salvarNovoEstoque(Estoque estoque)
        {
            dataEntity.Estoque.Add(estoque);
        }

        public bool pesquisaIdPagamento(Pagamentos pagamento)
        {
            List<Pagamentos> pag = (from pagamentos in dataEntity.Pagamentos
                                    where (pagamentos.id == (pagamento.id))
                                    select pagamentos).ToList();
            if(pag.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void salvarNovaVenda(Vendas venda)
        {
            dataEntity.Vendas.Add(venda);
        }

        public Fornecedores pesquisaFornecedoresByCnpj(string cnpj)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.cnpj.Equals(cnpj))
                    select fornecedor).SingleOrDefault();
        }

        public List<Fornecedores> pesquisaFornecedoresByNomeCnpjOfStatus(string pesquisa)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.cnpj.Equals(pesquisa)
                    || (fornecedor.nome.Contains(pesquisa)
                    && fornecedor.status == 1))
                    select fornecedor).ToList();
        }

        public Fornecedores pesquisaFornecedoresID(int pesquisa)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.id == (pesquisa))
                    select fornecedor).SingleOrDefault();
        }

        public List<Fornecedores> pesquisaFornecedoresByNomeCnpj(string pesquisa)
        {
            return (from fornecedor in dataEntity.Fornecedores
                    where (fornecedor.cnpj.Equals(pesquisa)
                    || fornecedor.nome.Contains(pesquisa))
                    && fornecedor.status == 1
                    select fornecedor).ToList();
        }

        public void salvarNovoFornecedor(Fornecedores fornecedor)
        {
            dataEntity.Fornecedores.Add(fornecedor);
        }

    }
}