﻿
using Declarative.Invoicing.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Declarative.Invoicing.Logic
{
    public class BaseManager
    {
        private readonly BaseRepository baseRepository;

        public BaseManager()
        {
            baseRepository = new BaseRepository();
        }
    }
}
