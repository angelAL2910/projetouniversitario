﻿using Entity.Entities;
using STL.POS.Data.NewVersion.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STL.POS.Logic
{
    public class CoverageManager: BaseManager
    {
        public readonly CoverageRepository coverageRepository;
        public CoverageManager()
        {
            coverageRepository = new CoverageRepository();
        }

        public BaseEntity SetCoverageDetail(Coverage.Parameter parameter)
        {
            return coverageRepository.SetCoverageDetail(parameter);
        }


        public virtual CoverageExplication GetCoverageExplication(CoverageExplication.Parameter parameter)
        {
            return coverageRepository.GetCoverageExplication(parameter);
        }
    }
}