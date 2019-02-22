﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateTrustGlobal.ViewModels
{
    public class ALL_LOCATION
    {
        public int Country_Id { get; set; }
        public int State_Prov_Id { get; set; }
        public int City_Id { get; set; }
        public string City_Desc { get; set; }
        public int Global_Country_Id { get; set; }
        public string Global_Country_Desc { get; set; }
        public string Global_Country_Desc_EN { get; set; }
        public int Domesticreg_Id { get; set; }
        public string State_Prov_Desc { get; set; }
        public bool State_Prov_Status { get; set; }
        public string Estatus { get; set; }

    }
}