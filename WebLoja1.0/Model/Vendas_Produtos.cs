//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Vendas_Produtos
    {
        public int id { get; set; }
        public Nullable<int> id_venda { get; set; }
        public Nullable<int> num_item { get; set; }
        public Nullable<int> id_produto { get; set; }
        public int quantidade { get; set; }
    
        public virtual Produtos Produtos { get; set; }
        public virtual Vendas Vendas { get; set; }
    }
}
