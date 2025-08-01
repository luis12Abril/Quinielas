﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quinielas.Shared.Entites
{
    public class Country
    {
        public int id { get; set; }

        [MaxLength(100)] 
        [Required]
        public string Name { get; set; } = null!;
    }
}
