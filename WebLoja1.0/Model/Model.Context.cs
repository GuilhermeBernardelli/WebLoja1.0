﻿//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebLoja1._0.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseEntities : DbContext
    {
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Cidades> Cidades { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Estados> Estados { get; set; }
        public DbSet<Fornecedores> Fornecedores { get; set; }
        public DbSet<Perfis> Perfis { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Vendas> Vendas { get; set; }
        public DbSet<Vendas_Produtos> Vendas_Produtos { get; set; }
        public DbSet<Produtos> Produtos { get; set; }
    }
}
