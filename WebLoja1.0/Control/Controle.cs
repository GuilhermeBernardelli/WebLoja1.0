using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public List<Clientes> pesquisaClientesCompleta(string busca)
        {
            string pesquisa = busca;

            return dbRepository.pesquisaClienteByCpfOrNome(pesquisa);
        }
    }
}