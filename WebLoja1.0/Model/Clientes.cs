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
    
    public partial class Clientes
    {
        public Clientes()
        {
            this.Vendas = new HashSet<Vendas>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string contato { get; set; }
        public string telefone { get; set; }
        public string celular { get; set; }
        public string endereço { get; set; }
        public string numeral { get; set; }
        public string bairro { get; set; }
        public int id_Cidade { get; set; }
        public Nullable<int> pessoa_fisica { get; set; }
        public int status { get; set; }
        public Nullable<double> creditos { get; set; }
        public string recado { get; set; }
        public string email { get; set; }
    
        public virtual Cidades Cidades { get; set; }
        public virtual ICollection<Vendas> Vendas { get; set; }
    }
}
