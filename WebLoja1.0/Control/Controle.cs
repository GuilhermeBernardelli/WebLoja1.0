using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebLoja1._0.Model;

namespace WebLoja1._0.Control
{
    public class Controle
    {
        Repository dbRepository = new Repository();
        public void salvaAtualiza()
        {
            dbRepository.salvaAlteracao();
        }

        public void salvarUsuario(Usuarios usuario)
        {
            dbRepository.salvarNovoUsuario(usuario);
        }

        public void excluiUsuario(Usuarios usuario)
        {
            dbRepository.excluirUsuarioExistente(usuario);
        }

        public Usuarios pesquisaUserLogin(string user)
        {
            string pesquisa = user;

            return dbRepository.pesquisaSimplesUser(pesquisa);
        }

        public List<Usuarios> usuariosInvalidos()
        {            
            return dbRepository.pesquisaUsuariosInvalidos();
        }

        public Produtos pesquisaProdutoNome(string selectedValue)
        {
            string pesquisa = selectedValue;

            return dbRepository.pesquisaProdutoByNome(pesquisa);
        }

        public List<Estados> pesquisaGeralEstdos()
        {
            return dbRepository.pesquisaEstados();
        }

        public void salvarFornecedor(Fornecedores fornecedor)
        {
            dbRepository.salvarNovoFornecedor(fornecedor);
        }

        public Usuarios pesquisaUserId(int user)
        {
            int pesquisa = user;

            return dbRepository.pesquisaUserById(pesquisa);
        }

        public Produtos pesquisaProdutoCod(string cod)
        {
            string codigo = cod;

            return dbRepository.pesquisaProdutoByCodigo(codigo);
        }

        public List<Estoque> pesquisaEstoqueRelatorio()
        {
            return dbRepository.pesquisaEstoqueOrdened();
        }

        public List<Tipos_Movimentacao> pesquisaTiposMovimento()
        {
            return dbRepository.pesquisaTiposMov();
        }

        public void salvarProduto(Produtos produto)
        {
            dbRepository.salvarNovoProduto(produto);
        }

        public Produtos pesquisaProdutoId(int id)
        {
            int pesquisa = id;

            return dbRepository.pesquisaProdutoById(pesquisa);
        }

        public List<Produtos> pesquisaProdutosNomeId(string valor)
        {
            string busca = valor;

            return dbRepository.pesquisaProdutoByNomeId(busca);
        }

        public List<Produtos> pesquisaGeralProd()
        {
            return dbRepository.pesquisaProdutos();
        }

        public List<Tipos_Movimentacao> pesquisaTiposMovimento(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.pesquisaTiposMovimentoByDesc(busca);
        }

        public Gerenciamento pesquisaGerenciamento(int id)
        {
            int pesquisa = id;

            return dbRepository.pesquisaGerenciamento(pesquisa);
        }

        public List<Fornecedores> pesquisaFornecedores(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.pesquisaFornecedoresByNomeCnpj(busca);
        }        

        public Cidades pesquisaCidade(string cidade)
        {
            string pesquisa = cidade;

            return dbRepository.pesquisaCidadeByName(pesquisa);
        }

        public List<Cidades> pesquisaCidadesPorEstado(int idEstado)
        {
            int pesquisa = idEstado;

            return dbRepository.pesquisaCidadesByEstado(pesquisa);
        }

        public List<Tipos_Movimentacao> pesquisaSubTipoMov(string valor)
        {
            string busca = valor;

            return dbRepository.pesquisaSubTiposMovimentoByDesc(busca);
        }

        public void salvarCidade(Cidades cidade)
        {
            dbRepository.salvarNovaCidade(cidade);
        }

        public void excluirCidade(Cidades cidade)
        {
            dbRepository.removerCidade(cidade);
        }

        public Fornecedores pesquisaFornecedorById(int Id)
        {
            int valor = Id;

            return dbRepository.pesquisaFornecedoresID(valor);
        }

        public List<Movimentos> pesquisaMovPeriodo(DateTime txtDtInicio, DateTime txtDtFim)
        {
            DateTime dtInicio = txtDtInicio;
            DateTime dtFim = txtDtFim;

            return dbRepository.pesquisaMovimentoIntervalo(dtInicio, dtFim);
        }

        public Fornecedores pesquisaFornecedorCpnj(string pesquisa)
        {
            string cnpj = pesquisa;

            return dbRepository.pesquisaFornecedoresByCnpj(cnpj);
        }

        public List<Fornecedores> pesquisaFornecedoresCompleta(string pesquisa)
        {
            string busca = pesquisa;

            return dbRepository.pesquisaFornecedoresByNomeCnpjOfStatus(busca);
        }

        public Clientes pesquisaClienteCpf(string pesquisa)
        {
            string cpf = pesquisa;

            return dbRepository.pesquisaClienteByCpf(cpf);
        }

        public void salvarCliente(Clientes cliente)
        {
            dbRepository.salvarNovoCliente(cliente);
        }

        public Clientes pesquisaClienteById(int selectedIndex)
        {
            int id = selectedIndex;

            return dbRepository.pesquisaClienteId(id);
        }

        public int pesquisaCompletaIDTipoMov(string descricao, string subTipo, string formaPg)
        {
            string desc = descricao;
            string tipo = subTipo;
            string form = formaPg;

            return dbRepository.pesquisaMovimentoID(desc, tipo, form);
        }

        public List<Clientes> pesquisaClientesCompleta(string busca)
        {
            string pesquisa = busca;

            return dbRepository.pesquisaClienteByCpfOrNome(pesquisa);
        }

        public List<Produtos> pesquisaProdutosValidoNome(string busca)
        {
            string pesquisa = busca;

            return dbRepository.pesquisaProdutosValidoByName(pesquisa);
        }

        public void salvarVenda(Vendas venda)
        {
            dbRepository.salvarNovaVenda(venda);
        }

        public void salvarPagamento(Pagamentos pagamento)
        {
            dbRepository.salvarNovoPagamento(pagamento);
        }

        public void salvaProdutosVendidos(Vendas_Produtos prodVendido)
        {
            dbRepository.salvarNovoProdutoVendido(prodVendido);
        }

        public bool pesquisaPagamentoId(Pagamentos pagamento)
        {
            return dbRepository.pesquisaIdPagamento(pagamento);
        }

        public Vendas pesquisaVendaID(int id)
        {
            int valor = id;

            return dbRepository.pesquisaVendabyID(valor);
        }

        public void salvarEstoque(Estoque estoque)
        {
            dbRepository.salvarNovoEstoque(estoque);
        }

        public Estoque pesquisaProdEstoqueId(int id)
        {
            int valor = id;

            return dbRepository.pesquisaEstoqueByProdID(valor);
        }

        public void salvarMovimento(Movimentos movimento)
        {
            dbRepository.salvarNovoMovimento(movimento);
        }

        public Tipos_Movimentacao pesquisaTiposMovimentoId(int? id_tipo)
        {
            return dbRepository.pesquisaTipoMovById(id_tipo);
        }

        public List<Vendas> pesquisaVendasGeral()
        {
            return dbRepository.pesquisaVendas();
        }

        public List<Pagamentos> pesquisaPagamentosGeral()
        {
            return dbRepository.pesquisaPagamentosTotais();
        }

        public List<Movimentos> pesquisaMovimentosGeral()
        {
            return dbRepository.pesquisaMovimentosTotais();
        }
    }
}