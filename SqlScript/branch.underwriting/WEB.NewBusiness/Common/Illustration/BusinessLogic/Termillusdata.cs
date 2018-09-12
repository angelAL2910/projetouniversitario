﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.NewBusiness.Common.Illustration.BusinessLogic
{
    public class Termillusdata
    {
        private int sno;
        private int age;
        private double regularpremium;
        private double riderpremium;
        private double totalpremium;
        private double accumulatedpremium;
        private double totalbenefitamount;


        private double commission;
        private double costofinsurance;
        private double insuredamount;



        public String[] getDataarray()
        {
            String[] str1 = null;
            str1 = new String[7];
            str1[0] = sno.ToString();
            str1[1] = age.ToString();

            if (regularpremium > 0)
                str1[2] = String.Format("{0:N0}", regularpremium);

            if (riderpremium > 0)
                str1[3] = String.Format("{0:N0}", riderpremium);

            if (totalpremium > 0)
                str1[4] = String.Format("{0:N0}", totalpremium);

            if (accumulatedpremium > 0)
                str1[5] = String.Format("{0:N0}", accumulatedpremium);

            if (totalbenefitamount > 0)
                str1[6] = String.Format("{0:N0}", totalbenefitamount);

            return str1;
        }


        public String[] getDatacolumns()
        {
            String[] str1 = null;// new String[numscenarios * 3 + 3];            
            str1 = new String[9];
            str1[0] = "Sno";
            str1[1] = "Age";
            str1[2] = "Regularpremium";
            str1[3] = "riderpremium";
            str1[4] = "totalpremium";
            str1[5] = "accumulatedpremium";
            str1[6] = "deathbenefit";

            return str1;
        }


        public int Sno
        {
            get { return this.sno; }
            set { this.sno = value; }
        }

        public int Age
        {
            get { return this.age; }
            set { this.age = value; }
        }

        public double Regularpremium
        {
            get { return this.regularpremium; }
            set { this.regularpremium = value; }
        }

        public double Riderpremium
        {
            get { return this.riderpremium; }
            set { this.riderpremium = value; }
        }

        public double Totalpremium
        {
            get { return this.totalpremium; }
            set { this.totalpremium = value; }
        }

        public double Accumulatedpremium
        {
            get { return this.accumulatedpremium; }
            set { this.accumulatedpremium = value; }
        }

        public double Totalbenefitamount
        {
            get { return this.totalbenefitamount; }
            set { this.totalbenefitamount = value; }
        }

        public double Commission
        {
            get { return this.commission; }
            set { this.commission = value; }
        }

        public double Costofinsurance
        {
            get { return this.costofinsurance; }
            set { this.costofinsurance = value; }
        }

        public double Insuredamount
        {
            get { return this.insuredamount; }
            set { this.insuredamount = value; }
        }


    }
}