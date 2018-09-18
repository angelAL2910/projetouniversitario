﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSI.Cobranza.LogicLayer
{
    public class ResultLogic<T> : BaseResultLogic<T> where T : class
    {
        public IEnumerable<T> dataResult { get; set; }        
    }
}
