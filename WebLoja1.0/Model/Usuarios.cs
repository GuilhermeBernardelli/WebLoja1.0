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
    
    public partial class Usuarios
    {
        public Usuarios()
        {
            this.Vendas = new HashSet<Vendas>();
        }
    
        public int id { get; set; }
        public Nullable<int> registro { get; set; }
        public string nome { get; set; }
        public string senha { get; set; }
        public int num_perfil { get; set; }
        public int status { get; set; }
    
        public virtual Perfis Perfis { get; set; }
        public virtual ICollection<Vendas> Vendas { get; set; }
    }
}
