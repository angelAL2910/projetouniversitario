﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Data
{

    /// <summary>
    /// Coverage
    /// </summary>
    public class CoverageWS
    {

        public int Id { get; set; }

        public bool IsLaw { get; set; }

        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public ICollection<DeductibleValues> Deductibles { get; set; }

    }
}