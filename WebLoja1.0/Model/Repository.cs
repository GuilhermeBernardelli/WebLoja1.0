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

        public Produtos pesquisaProdutoById(int pesquisa)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.id == pesquisa)
                    select produto).SingleOrDefault();
        }

        public List<Produtos> pesquisaProdutoByNomeId(string busca)
        {
            return (from produto in dataEntity.Produtos
                    where (produto.desc_produto.Contains(busca))
                    select produto).ToList();
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

        public List<Cidades> pesquisaCidadesByEstado(int pesquisa)
        {
            return (from cidade in dataEntity.Cidades
                    where (cidade.id_Estado == (pesquisa)
                    || cidade.id_Estado == 0)
                    orderby cidade.cidade
                    select cidade).ToList();
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

        public Clientes pesquisaClienteId(int id)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.id == (id))
                    select cliente).SingleOrDefault();
        }

        public void salvarNovoCliente(Clientes cliente)
        {
            dataEntity.Clientes.Add(cliente);
        }

        public List<Clientes> pesquisaClienteByCpfOrNome(string pesquisa)
        {
            return (from cliente in dataEntity.Clientes
                    where (cliente.cpf.Equals(pesquisa)
                    || cliente.nome.Contains(pesquisa))
                    && cliente.status == 1
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
                    where produto.desc_produto.Contains(pesquisa)
                    && produto.status == 1
                    select produto).ToList();
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
                    || fornecedor.nome.Contains(pesquisa))
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