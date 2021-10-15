using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CadastroTempus.Models
{
    public class dadosCliente
    {

       
        
        [Key]
        public string TxtCpf { get; set; }

        public string Nome { get; set; }

        public DateTime DtaNascimento { get; set; }

        public DateTime DtaCadastro { get; set; }


        public decimal Renda { get; set; }

        
    }
}