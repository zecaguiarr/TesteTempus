﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace CadastroTempus.Models
{
    public class context
    {

        public DbSet<dadosCliente> Dados { get; set; }
    }
}