﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using WSVirtualOffice.Models.businesslogic;
using WSVirtualOffice.Models.wsdata;


namespace WSVirtualOffice.Models.blinsurance
{
    public class IMaintermfixedLS
    {

        public enum CALCTYPES
        {
            PREMIUMAMOUNT = 1,
            INSURANCEAMOUNT = 2
        }

        private IMortalityfixed[] primarymortdatatable;
        private IMortalityfixed[] othermortdatatable;

        private ICanceldata[] canceldata;
        private double[] vivodata;
        private double[] remaindata;


        public CALCTYPES tocalculate;

        private int numberofyears;
        private int retirementnoofyears;
        private int defermentnoofyears;
        private double annuityamount;
        private double insuredamount;
        private double premiumcost;

        private Boolean isrefund = false;

        private double periodicpremiumamount;
        //private double annualizedpremiumamount;
        private String investmentprofile;
        private int age;
        private String gender;
        private String smoker;
        private String country;
        private String activityrisktype;
        private String healthrisktype;
        private String initialcontribution;
        private double initialcontributionamount;

        private double cumulativepremium;

        private double calcminimumpremium;
        private double calcinsuredamount;
        private double calcyearlypremium;


        private double calcaveragecommission;
        private double calctotalcommission;

        private double calckfactor;
        private double calccuotac;
        private double calcoverage;

        private String productcode;
        private char plantypecode;
        private char calculatetypecode;

        private char frequencytypecode;
        private int frequencytypevalue;
        private double frequencytypepenalty;

        private char investmentprofilecode;
        private char gendercode;
        private char smokercode;
        private int countryno;

        private double activityrisktypevalue;
        private double healthrisktypevalue;

        private char initialcontributioncode;

        private double growthrate;
        private double costoffunds;
        private double netgrowthrate;




        //private String initialcontribution;
        //public double initialcontributionamount;


        // start rider definition

        private String rideradb;
        private double rideradbamount;

        private String riderterm;
        private int ridertermuntilage;
        private double ridertermamount;

        private String rideracdb;


        private String riderci;
        private double riderciamount;

        //public String rideroir;
        private double rideroiramount;
        private int oirage;
        private String oirgender;
        private String oirsmoker;
        private int oiruntilage;
        private String oiractivityrisk;
        private String oirhealthrisk;

        private char oirgendercode;
        private char oirsmokercode;

        private double oiractivityrisktypevalue;
        private double oirhealthrisktypevalue;

        public char rideradbcode = 'N';
        public char ridertermcode = 'N';
        public char rideracdbcode = 'N';
        public char ridercicode = 'N';
        public char rideroircode = 'N';

        public double regularinsurancecost;
        public double repaycostnomargin;
        public double repaycost;
        
        public double rideradbcost;
        public double ridertermcost;
        public double rideracdbcost;
        public double ridercicost;
        public double rideroircost;



        private int Adbmaxage;
        private double Adbfactor;



        public double countryrisk;
        private ICommissiondata[] commissiondata = null;

        private ISurrenderpenaltydata[] surrenderpenaltydata = null;

        public double[] varpremiumamount = null;
        public double[] varinsuredamount = null;
        

        public char varpremiumcode = 'N';
        public char varannuitycode = 'N';
        
        
        private double precision = 2.0;
        private double insuranceprecision = 5.0;


        // rule values
        public int insurancemaxage;
        private int GSMinimumcontributionperiod;
        public double GSMaximumpremiumamount;
        public double GSMinimumpremiumamount;
        public double GSMaximuminsuranceamount;
        public double GSMinimuminsuranceamount;
        public double Minimumpremiumamount;
        public double Minimuminsuredamount;
        public double Maximumpremiumamount;
        public double Maximuminsuredamount;


        public double Targetoverage;
        public double Targetdiscountfactor;
        public double Minimumpremiumdiscountfactor;
        public int Retirementpartialtominprimamultiple;
        public double Insuranceoverage;
        public double Smokeroverage;
        public double Premiumreserve;
        public int Targetcontributionperiod;
        public double Monthlyfeevalue;
        public double Monthlyfeevalueyear;
        public double Loadchargepercent;
        public double Maxageoffsetfordefaultmoratlity;
        public double Maxagefordefaultmoratlity;
        public double Maxmortalityvalue;
        public int Malemortalityoffset;

        public double Targetrate;
        public double Targetfactor;
        public double Regularmargin;
        public double Savingsmargin;
        public double Ridersmargin;
        public double Minimumfirstagetarget;
        public double Minimumfirstbaseprice;
        public double Minimumsecondbaseprice;
        public int insuranceunderage;
        public double Extratermpenalty;


        public double Adminfixed;
        public double Baseinterestrate;
        public double Commission_cost;

        public double Minimumridercost;

        public int Rescatestartyear;
        public double Rescatepercentage;


        public int Maxage;


        public double surrendercharge;
        public double partialsurrendercharge;
        public double surrenderexcesspenalty;

        public double LoanInterestRate;
        public double LoanPrincipalGrowthRate;
        public Boolean LoanPrincipalGrowInvestRate;

        public double InterestLoanRate;
        public double LoanAssetRate;
        public Boolean IsLoanRate;


        public Boolean isOpeningbalance = false;
        public int planyearstart;
        public double openingbalanceamount = 0.0;

        private double calculatedtargetamount;
        private double calculatedinsurancefactor;
        private char clascode;

        public IMaintermfixedLS(long customerplanno)
        {
            DataVOUniversalDataContext newdb = Program.getDbConnection();
            try
            {
                DateTime[] dt1 = new DateTime[10];
                dt1[0] = DateTime.Now;

                customerPlandet custplan = (from item in newdb.customerPlandets
                                            where item.customerPlanno == customerplanno
                                            select item).SingleOrDefault();

                customerdet cust = (from item in newdb.customerdets
                                    where item.customerno == custplan.customerno
                                    select item).SingleOrDefault();

                dt1[1] = DateTime.Now;
                //this.product = product;
                this.productcode = custplan.productcode;
                this.clascode = custplan.@class;
                setRuledata();

                isrefund = Productdata.isRefund(this.productcode);

                dt1[2] = DateTime.Now;

                //this.plantype = plantype;
                this.plantypecode = custplan.plantypecode;

                //this.contributiontype = contributiontype;

                this.numberofyears = Int32.Parse(custplan.contributionperiod.ToString());

                this.calculatetypecode = custplan.calculatetypecode;

                //this.frequencytype = frequencytype;
                this.frequencytypecode = custplan.frequencytypecode;
                //this.frequencytype = Frequencytypes.getfrequencytype(custplan.frequencytypecode.Value);
                this.frequencytypevalue = Frequencytypes.getfrequencytypevaluefromcode(this.frequencytypecode);
                this.frequencytypepenalty = Frequencytypes.getfrequencytypepenaltyfromcode(this.frequencytypecode, this.productcode);

                //this.investmentprofile = investmentprofile;
                this.investmentprofilecode = custplan.investmentprofilecode;// Invprofiledata.getInvestmentprofilecode(this.investmentprofile);

                this.age = Int32.Parse(cust.Age.ToString());

                //this.gender = gender;
                this.gendercode = cust.gendercode;

                //this.smoker = smoker;
                this.smokercode = cust.Smoker;

                //this.country = country;
                this.countryno = custplan.countryno;

                //this.activityrisktype = activityrisktype;
                this.activityrisktypevalue = Activityrisktypes.getActivityriskvalue(custplan.activityrisktypeno);

                //this.healthrisktype = healthrisktype;
                this.healthrisktypevalue = Healthrisktypes.getHealthriskvalue(custplan.healthrisktypeno);

                this.growthrate = Rules.getRulevaluedouble(Rules.ADMIN_FIXED, this.productcode, this.clascode);

                // calculating mortality data
                dt1[3] = DateTime.Now;

                int maxage = Rules.getRulevalueint(Rules.MAX_AGE, this.productcode, this.clascode);
                //mortalitydata = illustrator1.businesslogic.Mortalityrates.getMortalitydata(this.productcode, Ridertypes.Primary, this.age, this.gendercode, this.smokercode);
                dt1[4] = DateTime.Now;


                this.costoffunds = Rules.getRulevaluedouble(Rules.FUND_COST, this.productcode, this.clascode);
                netgrowthrate = (this.growthrate - this.costoffunds);

                countryrisk = Countries.getCountryriskvalue(custplan.countryno);

                this.insuredamount = Numericdata.getDoublevalue(custplan.insuredamount.ToString());
                this.calcinsuredamount = this.insuredamount;



                this.periodicpremiumamount = double.Parse(custplan.premiumamount.ToString());
                double penaltypercent = this.frequencytypepenalty;
                double netperiodicpayment = periodicpremiumamount / (1 + penaltypercent);
                //MessageBox.Show(netperiodicpayment.ToString());
                double periodicgrowthrate = Math.Pow((1 + this.growthrate), 1.0 / this.frequencytypevalue * 1.0) - 1;
                //MessageBox.Show(periodicgrowthrate.ToString());
                //annualizedpremiumamount = calculatepv(periodicgrowthrate, this.frequencytypevalue, netperiodicpayment);
                this.calcyearlypremium = calculatepv(periodicgrowthrate, this.frequencytypevalue, netperiodicpayment);


                this.annuityamount = double.Parse(custplan.annuityamount.ToString());
                //this.calcyearlypremium = double.Parse(custplan.annuityamount.ToString());

                dt1[5] = DateTime.Now;
                // calculating commission data
                commissiondata = Commissions.getCommissiondata(this.productcode, 0, this.numberofyears);// new ICommissiondata[this.Maxage - age + 1];
                dt1[6] = DateTime.Now;

                surrenderpenaltydata = Surrenderpenaties.getSurrenderpenaltydata(this.productcode, 0); //new ISurrenderpenaltydata[this.Maxage - age + 1];


                dt1[7] = DateTime.Now;


                this.setInitialcontribution(
                    (custplan.initialcontribution.ToString().Equals("0.0")) ? "No" : "Yes",
                    custplan.initialcontribution.ToString());//// = ddlinitialcontribution.Text;


                this.retirementnoofyears = Int32.Parse(custplan.retirementperiod.ToString());
                this.defermentnoofyears = Int32.Parse(custplan.defermentperiod.ToString());

                dt1[8] = DateTime.Now;



                if (this.smokercode == 'Y')
                {
                    calcoverage = this.Smokeroverage;
                }
                else
                {
                    calcoverage = this.Insuranceoverage;
                }

                if (custplan.rideradb != null)
                {
                    this.setRideradbdata(Booleandata.getBooleanstring(custplan.rideradb), custplan.rideradbamount.ToString());
                    this.rideradbcost = double.Parse(custplan.rideradbcost.ToString());
                }

                if (custplan.rideracdb != null)
                {
                    this.setRideracdbdata(Booleandata.getBooleanstring(custplan.rideracdb));
                    this.rideracdbcost = double.Parse(custplan.rideracdbcost.ToString());
                }
                if (custplan.riderci != null)
                {
                    this.setRidercidata(Booleandata.getBooleanstring(custplan.riderci), custplan.riderciamount.ToString());
                    this.ridercicost = double.Parse(custplan.ridercicost.ToString());
                }

                if (custplan.riderterm != null)
                {
                    int temptermnumyears=0;
                    if (custplan.termcontributiontypecode == 'U')
                    {
                        temptermnumyears = custplan.ridertermuntilage - this.age + 1;
                    }
                    else if (custplan.termcontributiontypecode == 'C')
                    {
                        temptermnumyears = this.numberofyears;
                    }
                    else if (custplan.termcontributiontypecode == Contributiontypes.NUMBEROFYEARS)
                    {
                        temptermnumyears = custplan.ridertermuntilage;
                    }
                    if (temptermnumyears > this.numberofyears)
                    {
                        temptermnumyears = this.numberofyears;
                    }


                    this.setRidertermdata(Booleandata.getBooleanstring(custplan.riderterm), custplan.ridertermamount.ToString(), temptermnumyears.ToString());
                    this.ridertermcost = double.Parse(custplan.ridertermcost.ToString());
                }






                if (custplan.rideroir != null)
                {
                    if (custplan.rideroir == 'Y')
                    {
                        customerplanpartnerinsurancedet othins = (from item in newdb.customerplanpartnerinsurancedets
                                                                  where item.customerplanno == custplan.customerPlanno
                                                                  select item).SingleOrDefault();

                        int tempoiryears = 0;
                        if (othins.contributiontypecode == 'U')
                        {
                            tempoiryears = othins.untilage - othins.age.Value + 1;
                        }
                        else if (othins.contributiontypecode == 'C')
                        {
                            tempoiryears = this.numberofyears;
                        }
                        else if (othins.contributiontypecode == Contributiontypes.NUMBEROFYEARS)
                        {
                            tempoiryears = othins.untilage;
                        }

                        if (tempoiryears > this.numberofyears)
                        {
                            tempoiryears = this.numberofyears;
                        }


                        this.setOirdata(Booleandata.getBooleanstring(custplan.rideroir), othins.rideroiramount.ToString(),
                            othins.age.ToString(),
                            Genders.getgender(othins.gendercode.Value), Booleandata.getBooleanstring(othins.smoker),
                            tempoiryears.ToString(),
                            Activityrisktypes.getActivityrisktype(othins.activityrisktypeno.Value),
                            Healthrisktypes.getHealthrisktype(othins.healthrisktypeno.Value));
                        //this.rideroircost = double.Parse(othins.rideroircost.ToString());
                    }
                }

                //this.calculatedinsurancefactor = this.Insurancefactor;

                this.primarymortdatatable = Mortalityrates.getMortalitydatafixedterm(this.productcode, this.gendercode, Ridertypes.Primary, this.smokercode, this.healthrisktypevalue, this.activityrisktypevalue, this.countryrisk, custplan.@class);
                this.othermortdatatable = Mortalityrates.getMortalitydatafixedterm(this.productcode, this.oirgendercode, Ridertypes.Primary, this.oirsmokercode, this.oirhealthrisktypevalue, this.oiractivityrisktypevalue, this.countryrisk, custplan.@class);
                //this.othermortdatatable = Mortalityrates.getMortalitydatafixedterm(this.productcode, this.gendercode, Ridertypes.Primary, this.healthrisktypevalue, this.smokercode);



                //calckfactor = this.getKfactor();

                if (this.smokercode == 'Y')
                {
                    this.calcoverage = this.Smokeroverage;
                }
                else
                {
                    this.calcoverage = this.Insuranceoverage;
                }


                if (this.isrefund == true)
                {
                    canceldata = Productdata.getProductcanceldata(this.productcode, this.retirementnoofyears);
                    setVivodata();
                    setRemaindata();
                }


                ITermassumedata asdata = new ITermassumedata();

                if (this.calculatetypecode == Calculatetypes.INSUREDAMOUNT)
                {
                    this.tocalculate = CALCTYPES.INSURANCEAMOUNT;
                    asdata.premiumamount = this.calcyearlypremium;
                    double assumedinsuranceamount = goalseek(asdata, this.GSMinimuminsuranceamount, this.GSMaximuminsuranceamount);
                    this.calcinsuredamount = assumedinsuranceamount;
                    this.insuredamount = assumedinsuranceamount;
                    calculatedtargetamount = getTargetamount();
                }
                else if (this.calculatetypecode == Calculatetypes.PREMIUMAMOUNT)
                {
                    this.tocalculate = CALCTYPES.PREMIUMAMOUNT;
                    asdata.insuranceamount = this.insuredamount;
                    double assumedpremiumamount = goalseek(asdata, this.GSMinimumpremiumamount, this.GSMaximumpremiumamount);
                    //double assumedpremiumamount = goalseek(asdata, 1737.01, 1737.01);
                    this.calcyearlypremium = assumedpremiumamount;
                    this.periodicpremiumamount = this.calculatedPeriodicPremiumAmount();
                    calculatedtargetamount = getTargetamount();
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                //newdb.Dispose();
            }
        }

        public IMaintermfixedLS(WSCustomer cust, WSCustomerPlan custplan, List<WSRider> riderslist, WSCustomerPlanPartner partnerins)
        {
            DataVOUniversalDataContext newdb = Program.getDbConnection();
            try
            {
                DateTime[] dt1 = new DateTime[10];
                dt1[0] = DateTime.Now;

               

                dt1[1] = DateTime.Now;
                //this.product = product;
                this.productcode = custplan.productcode;
                this.clascode = custplan.classcode.ToCharArray()[0];

                setRuledata();

                isrefund = Productdata.isRefund(this.productcode);

                dt1[2] = DateTime.Now;

                //this.plantype = plantype;
                this.plantypecode = 'S';

                //this.contributiontype = contributiontype;

                this.numberofyears = Int32.Parse(custplan.contributionperiod.ToString());

                this.calculatetypecode = custplan.calculatetypecode.ToCharArray()[0];

                //this.frequencytype = frequencytype;
                this.frequencytypecode = custplan.frequencytypecode.ToCharArray()[0];
                //this.frequencytype = Frequencytypes.getfrequencytype(custplan.frequencytypecode.Value);
                this.frequencytypevalue = Frequencytypes.getfrequencytypevaluefromcode(this.frequencytypecode);
                this.frequencytypepenalty = Frequencytypes.getfrequencytypepenaltyfromcode(this.frequencytypecode, this.productcode);

                //this.investmentprofile = investmentprofile;
                this.investmentprofilecode = custplan.investmentprofilecode.ToCharArray()[0];// Invprofiledata.getInvestmentprofilecode(this.investmentprofile);

                this.age = Int32.Parse(cust.Age.ToString());

                //this.gender = gender;
                this.gendercode = cust.gendercode.ToCharArray()[0];

                //this.smoker = smoker;
                this.smokercode = cust.Smoker.ToCharArray()[0];

                //this.country = country;
                this.countryno = custplan.countryno;

                //this.activityrisktype = activityrisktype;
                this.activityrisktypevalue = Activityrisktypes.getActivityriskvalue(custplan.activityrisktypeno);

                //this.healthrisktype = healthrisktype;
                this.healthrisktypevalue = Healthrisktypes.getHealthriskvalue(custplan.healthrisktypeno);

                this.growthrate = Rules.getRulevaluedouble(Rules.ADMIN_FIXED, this.productcode, this.clascode);

                // calculating mortality data
                dt1[3] = DateTime.Now;

                int maxage = Rules.getRulevalueint(Rules.MAX_AGE, this.productcode, this.clascode);
                //mortalitydata = illustrator1.businesslogic.Mortalityrates.getMortalitydata(this.productcode, Ridertypes.Primary, this.age, this.gendercode, this.smokercode);
                dt1[4] = DateTime.Now;


                this.costoffunds = Rules.getRulevaluedouble(Rules.FUND_COST, this.productcode, this.clascode);
                netgrowthrate = (this.growthrate - this.costoffunds);

                countryrisk = Countries.getCountryriskvalue(custplan.countryno);

                this.insuredamount = Numericdata.getDoublevalue(custplan.insuredamount.ToString());
                this.calcinsuredamount = this.insuredamount;



                this.periodicpremiumamount = double.Parse(custplan.premiumamount.ToString());
                double penaltypercent = this.frequencytypepenalty;
                double netperiodicpayment = periodicpremiumamount / (1 + penaltypercent);
                //MessageBox.Show(netperiodicpayment.ToString());
                double periodicgrowthrate = Math.Pow((1 + this.growthrate), 1.0 / this.frequencytypevalue * 1.0) - 1;
                //MessageBox.Show(periodicgrowthrate.ToString());
                //annualizedpremiumamount = calculatepv(periodicgrowthrate, this.frequencytypevalue, netperiodicpayment);
                this.calcyearlypremium = calculatepv(periodicgrowthrate, this.frequencytypevalue, netperiodicpayment);


                this.annuityamount = double.Parse(custplan.annuityamount.ToString());
                //this.calcyearlypremium = double.Parse(custplan.annuityamount.ToString());

                dt1[5] = DateTime.Now;
                // calculating commission data
                commissiondata = Commissions.getCommissiondata(this.productcode, 0, this.numberofyears);// new ICommissiondata[this.Maxage - age + 1];
                dt1[6] = DateTime.Now;

                surrenderpenaltydata = Surrenderpenaties.getSurrenderpenaltydata(this.productcode, 0); //new ISurrenderpenaltydata[this.Maxage - age + 1];


                dt1[7] = DateTime.Now;


                this.setInitialcontribution(
                    (custplan.initialcontributionamount.ToString().Equals("0.0")) ? "No" : "Yes",
                    custplan.initialcontributionamount.ToString());//// = ddlinitialcontribution.Text;


                this.retirementnoofyears = custplan.insuranceperiod;
               // this.retirementnoofyears = Int32.Parse(custplan.retirementperiod.ToString());
                this.defermentnoofyears = Int32.Parse(custplan.defermentperiod.ToString());

                dt1[8] = DateTime.Now;



                if (this.smokercode == 'Y')
                {
                    calcoverage = this.Smokeroverage;
                }
                else
                {
                    calcoverage = this.Insuranceoverage;
                }
                WSRider rideradb = null;
                WSRider riderterm = null;

                if (riderslist != null)
                {
                    foreach (WSRider rider in riderslist)
                    {
                        if (rider.ridertypecode.Equals(WSRider.RIDERADB))
                        {
                            rideradb = rider;
                        }
                        else if (rider.ridertypecode.Equals(WSRider.RIDERTERM))
                        {
                            riderterm = rider;
                        }
                    }


                }

                if (rideradb != null)
                {
                    this.setRideradbdata("Yes", rideradb.amount.ToString());
                    this.rideradbcost = 0;//= double.Parse(custplan.rideradbcost.ToString());
                }

                //if (custplan.rideracdb != null)
                //{
                //    this.setRideracdbdata(Booleandata.getBooleanstring(custplan.rideracdb));
                //    this.rideracdbcost = double.Parse(custplan.rideracdbcost.ToString());
                //}
                //if (custplan.riderci != null)
                //{
                //    this.setRidercidata(Booleandata.getBooleanstring(custplan.riderci), custplan.riderciamount.ToString());
                //    this.ridercicost = double.Parse(custplan.ridercicost.ToString());
                //}

                if (riderterm != null)
                {
                    int temptermnumyears = 0;
                    if (riderterm.type == "U")
                    {
                        temptermnumyears = riderterm.term - this.age + 1;
                    }
                    else if (riderterm.type == "C")
                    {
                        temptermnumyears = this.numberofyears;
                    }
                    else if (riderterm.type == Contributiontypes.NUMBEROFYEARS.ToString())
                    {
                        temptermnumyears = riderterm.term;
                    }
                    if (temptermnumyears > this.numberofyears)
                    {
                        temptermnumyears = this.numberofyears;
                    }


                    this.setRidertermdata("Yes", riderterm.amount.ToString(), temptermnumyears.ToString());
                    this.ridertermcost = 0;
                }



                if (partnerins != null)
                {

                    int tempoiryears = 0;
                    if (partnerins.contributiontype == "U")
                    {
                        tempoiryears = partnerins.term - partnerins.age + 1;
                    }
                    else if (partnerins.contributiontype == "C")
                    {
                        tempoiryears = this.numberofyears;
                    }
                    else if (partnerins.contributiontype == Contributiontypes.NUMBEROFYEARS.ToString())
                    {
                        tempoiryears = partnerins.term;
                    }

                    if (tempoiryears > this.numberofyears)
                    {
                        tempoiryears = this.numberofyears;
                    }


                    this.setOirdata("Yes", partnerins.amount.ToString(),
                        partnerins.age.ToString(),
                        Genders.getgender(partnerins.gendercode.ToCharArray()[0]), Booleandata.getBooleanstring(partnerins.smoker.ToCharArray()[0]),
                        tempoiryears.ToString(),
                        Activityrisktypes.getActivityrisktype(partnerins.activityrisktypeno),
                        Healthrisktypes.getHealthrisktype(partnerins.healthrisktypeno));
                    //this.rideroircost = double.Parse(othins.rideroircost.ToString());

                }

                //this.calculatedinsurancefactor = this.Insurancefactor;

                this.primarymortdatatable = Mortalityrates.getMortalitydatafixedterm(this.productcode, this.gendercode, Ridertypes.Primary, this.smokercode, this.healthrisktypevalue, this.activityrisktypevalue, this.countryrisk, custplan.classcode.ToCharArray()[0]);
                this.othermortdatatable = Mortalityrates.getMortalitydatafixedterm(this.productcode, this.oirgendercode, Ridertypes.Primary, this.oirsmokercode, this.oirhealthrisktypevalue, this.oiractivityrisktypevalue, this.countryrisk, custplan.classcode.ToCharArray()[0]);
                //this.othermortdatatable = Mortalityrates.getMortalitydatafixedterm(this.productcode, this.gendercode, Ridertypes.Primary, this.healthrisktypevalue, this.smokercode);



                //calckfactor = this.getKfactor();

                if (this.smokercode == 'Y')
                {
                    this.calcoverage = this.Smokeroverage;
                }
                else
                {
                    this.calcoverage = this.Insuranceoverage;
                }


                if (this.isrefund == true)
                {
                    canceldata = Productdata.getProductcanceldata(this.productcode, this.retirementnoofyears);
                    setVivodata();
                    setRemaindata();
                }


                ITermassumedata asdata = new ITermassumedata();

                if (this.calculatetypecode == Calculatetypes.INSUREDAMOUNT)
                {
                    this.tocalculate = CALCTYPES.INSURANCEAMOUNT;
                    asdata.premiumamount = this.calcyearlypremium;
                    double assumedinsuranceamount = goalseek(asdata, this.GSMinimuminsuranceamount, this.GSMaximuminsuranceamount);
                    this.calcinsuredamount = assumedinsuranceamount;
                    this.insuredamount = assumedinsuranceamount;
                    calculatedtargetamount = getTargetamount();
                }
                else if (this.calculatetypecode == Calculatetypes.PREMIUMAMOUNT)
                {
                    this.tocalculate = CALCTYPES.PREMIUMAMOUNT;
                    asdata.insuranceamount = this.insuredamount;
                    double assumedpremiumamount = goalseek(asdata, this.GSMinimumpremiumamount, this.GSMaximumpremiumamount);
                    //double assumedpremiumamount = goalseek(asdata, 1737.01, 1737.01);
                    this.calcyearlypremium = assumedpremiumamount;
                    this.periodicpremiumamount = this.calculatedPeriodicPremiumAmount();
                    calculatedtargetamount = getTargetamount();
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                //newdb.Dispose();
            }
        }

      
        public void setRideradbdata(String rideradb, String rideradbamountstr)
        {
            this.rideradb = rideradb;
            this.rideradbcode = Booleandata.getBooleanvalue(rideradb);
            if (Program.isdecimal(rideradbamountstr) == true)
            {
                this.rideradbamount = double.Parse(rideradbamountstr);
            }
            else
            {
                this.rideradbamount = 0.0;
            }

        }

        public void setRideracdbdata(String rideracdb)
        {
            this.rideracdb = rideracdb;
            this.rideracdbcode = Booleandata.getBooleanvalue(rideracdb);
        }

        public void setRidercidata(String riderci, String riderciamountstr)
        {
            this.ridercicode = Booleandata.getBooleanvalue(riderci);
            if (this.ridercicode == 'Y')
            {
                if (Program.isdecimal(riderciamountstr) == true)
                {
                    this.riderciamount = double.Parse(riderciamountstr);
                }
                else
                {
                    this.riderciamount = 0.0;
                }
            }
            else
            {
                this.riderciamount = 0.0;
            }
        }



        public void setRidertermdata(String riderterm, String ridertermamountstr, String ridertermuntilagestr)
        {
            this.riderterm = riderterm;
            this.ridertermcode = Booleandata.getBooleanvalue(riderterm);
            if (this.ridertermcode == 'Y')
            {
                if (Program.isdecimal(ridertermamountstr) == true)
                {
                    this.ridertermamount = double.Parse(ridertermamountstr);
                }
                else
                {
                    this.ridertermamount = 0.0;
                }
                if (Program.isinteger(ridertermuntilagestr) == true)
                {
                    this.ridertermuntilage = Int32.Parse(ridertermuntilagestr);
                }
                else
                {
                    this.ridertermuntilage = 0;
                }
            }
            else
            {
                this.ridertermamount = 0.0;// double.Parse(ridertermamountstr);
                this.ridertermuntilage = 0;
            }
        }


        public void setOirdata(String rideroir, String rideroiramountstr, String oiragestr, String oirgender, String oirsmoker, String oiruntilagestr, String oiractivityrisk, String oirhealthrisk)
        {
            rideroircode = Booleandata.getBooleanvalue(rideroir);

            if (this.rideroircode == 'Y')
            {
                if (Program.isdecimal(rideroiramountstr) == true)
                {
                    this.rideroiramount = double.Parse(rideroiramountstr);
                }
                else
                {
                    this.rideroiramount = 0.0;
                }

                if (Program.isinteger(oiragestr) == true)
                {
                    this.oirage = Int32.Parse(oiragestr);
                }
                else
                {
                    this.oirage = 0;
                }

                this.oirgender = oirgender;
                this.oirsmoker = oirsmoker;

                if (Program.isinteger(oiruntilagestr) == true)
                {
                    this.oiruntilage = Int32.Parse(oiruntilagestr);
                }
                else
                {
                    this.oiruntilage = 0;
                }

                this.oiractivityrisk = oiractivityrisk;
                this.oirhealthrisk = oirhealthrisk;

                this.ridercicode = Booleandata.getBooleanvalue(rideroir);
                this.oirgendercode = Genders.getgendercode(oirgender);
                this.oirsmokercode = Booleandata.getBooleanvalue(oirsmoker);
                this.oiractivityrisktypevalue = Activityrisktypes.getActivityriskvalue(Activityrisktypes.getActivityrisktypeno(this.productcode, oiractivityrisk));
                this.oirhealthrisktypevalue = Healthrisktypes.getHealthriskvalue(Healthrisktypes.getHealthrisktypeno(this.productcode, oirhealthrisk));


            }
            else
            {
                this.rideroiramount = 0;
                this.oirage = 0;
            }


        }


        


        
        

        


        

        public void setInitialcontribution(String initialcontributionstr, String initialcontributionamountstr)
        {
            initialcontributioncode = Booleandata.getBooleanvalue(initialcontributionstr);
            if (this.initialcontributioncode == 'Y')
            {
                if (Program.isdecimal(initialcontributionamountstr) == true)
                {
                    this.initialcontributionamount = double.Parse(initialcontributionamountstr);
                }
                else
                {
                    this.initialcontributionamount = 0.0;
                }
            }
            else
            {
                this.initialcontributionamount = 0.0;
            }

        }


        public void setRuledata()
        {
            insurancemaxage = Rules.getRulevalueint(Rules.INSURANCE_MAX_AGE, this.productcode,this.clascode);
            GSMaximumpremiumamount = Rules.getRulevaluedouble(Rules.GS_MAXIMUM_PREMIUM_AMOUNT, this.productcode, this.clascode);
            GSMinimumpremiumamount = Rules.getRulevaluedouble(Rules.GS_MINIMUM_PREMIUM_AMOUNT, this.productcode, this.clascode);
            GSMaximuminsuranceamount = Rules.getRulevaluedouble(Rules.GS_MAXIMUM_INSURED_AMOUNT, this.productcode, this.clascode);
            GSMinimuminsuranceamount = Rules.getRulevaluedouble(Rules.GS_MINIMUM_INSURED_AMOUNT, this.productcode, this.clascode);

            Minimumpremiumamount = Rules.getRulevaluedouble(Rules.MINIMUM_YEARLY_PREMIUM, this.productcode, this.clascode);
            Minimuminsuredamount = Rules.getRulevaluedouble(Rules.MINIMUM_INSURED_AMT, this.productcode, this.clascode);
            Maximuminsuredamount = Rules.getRulevaluedouble(Rules.MAXIMUM_INSURED_AMT, this.productcode, this.clascode);
            Targetoverage = Rules.getRulevaluedouble(Rules.TARGET_OVERAGE, this.productcode, this.clascode);
            Premiumreserve = Rules.getRulevaluedouble(Rules.PREMIUM_RESERVE, this.productcode, this.clascode);
            Targetcontributionperiod = Rules.getRulevalueint(Rules.TARGET_CONTRIBUTION_PERIOD, this.productcode, this.clascode);
            Monthlyfeevalue = Rules.getRulevaluedouble(Rules.MONTHLY_FEE, this.productcode, this.clascode);
            Monthlyfeevalueyear = this.Monthlyfeevalue * 12;
            Loadchargepercent = Rules.getRulevaluedouble(Rules.LOAD_CHARGE, this.productcode, this.clascode);
            GSMinimumcontributionperiod = Rules.getRulevalueint(Rules.GS_MINIMUM_CONTRIBUTION_PERIOD, this.productcode, this.clascode);
            Maxage = Rules.getRulevalueint(Rules.MAX_AGE, this.productcode, this.clascode);
            //Minimumpremiumtotargetfactor = Rules.getRulevaluedouble(Rules.MINIMUM_PREMIUM_TO_TARGET_FACTOR, this.productcode);
            Targetdiscountfactor = Rules.getRulevaluedouble(Rules.TARGET_DISCOUNT_FACTOR, this.productcode, this.clascode);

            Minimumpremiumdiscountfactor = Rules.getRulevaluedouble(Rules.MINIMUM_PREMIUM_DISCOUNT_FACTOR, this.productcode, this.clascode);
            Retirementpartialtominprimamultiple = Rules.getRulevalueint(Rules.RETIREMENT_PARTIAL_MINIMUM_PREMIUM_MULTIPLE, this.productcode, this.clascode);

            surrendercharge = Rules.getRulevaluedouble(Rules.SURRENDER_CHARGE, this.productcode, this.clascode);
            partialsurrendercharge = Rules.getRulevaluedouble(Rules.PARTIAL_SURRENDER_CHARGE, this.productcode, this.clascode);
            surrenderexcesspenalty = Rules.getRulevaluedouble(Rules.SURRENDER_EXCESS_PERCENT, this.productcode, this.clascode);


            Extratermpenalty = Rules.getRulevaluedouble(Rules.EXTRA_TERM_PENALTY, this.productcode, this.clascode);


            this.Adbmaxage = Rules.getRulevalueint(Rules.ADB_MAX_AGE, this.productcode, this.clascode);
            this.Adbfactor = Rules.getRulevaluedouble(Rules.ADB_FACTOR, this.productcode, this.clascode);
            this.Minimumridercost = Rules.getRulevaluedouble(Rules.MINIMUM_RIDER_COST, this.productcode, this.clascode);

            this.LoanInterestRate = Rules.getRulevaluedouble(Rules.LOAN_INTEREST_RATE, this.productcode, this.clascode);
            this.LoanPrincipalGrowthRate = Rules.getRulevaluedouble(Rules.LOAN_PRINCIPAL_GROWTH_RATE, this.productcode, this.clascode);
            this.LoanPrincipalGrowInvestRate = Rules.getRulevalueboolean(Rules.LOAN_PRINCIPAL_GROW_INVEST_RATE, this.productcode, this.clascode);

            this.InterestLoanRate = Rules.getRulevaluedouble(Rules.INTEREST_LOAN_RATE, this.productcode, this.clascode);
            this.LoanAssetRate = Rules.getRulevaluedouble(Rules.LOAN_ASSET_RATE, this.productcode, this.clascode);
            this.IsLoanRate = Rules.getRulevalueboolean(Rules.IS_LOAN_RATE, this.productcode, this.clascode);

            this.Maxageoffsetfordefaultmoratlity = Rules.getRulevalueint(Rules.MAX_AGE_OFFSET_FOR_DEFAULT_MORTALITY, this.productcode, this.clascode);
            this.Maxagefordefaultmoratlity = Rules.getRulevalueint(Rules.MAX_AGE_FOR_DEFAULT_MORTALITY, this.productcode, this.clascode);
            this.Maxmortalityvalue = Rules.getRulevaluedouble(Rules.MAX_MORTALITY_VALUE, this.productcode, this.clascode);
            this.Malemortalityoffset = Rules.getRulevalueint(Rules.MALE_MORTALITY_OFFSET, this.productcode, this.clascode);

            this.Adminfixed = Rules.getRulevaluedouble(Rules.ADMIN_FIXED, this.productcode, this.clascode);
            this.Baseinterestrate = Rules.getRulevaluedouble(Rules.BASE_INTEREST, this.productcode, this.clascode);
            this.Commission_cost = Rules.getRulevaluedouble(Rules.COMMISSION_COST, this.productcode, this.clascode);

            this.Insuranceoverage = Rules.getRulevaluedouble(Rules.INSURANCE_OVERAGE, this.productcode, this.clascode);
            this.Smokeroverage = Rules.getRulevaluedouble(Rules.SMOKER_OVERAGE, this.productcode, this.clascode);

            this.Rescatestartyear = Rules.getRulevalueint(Rules.RESCATE_STARTYEAR, this.productcode, this.clascode);

            this.Rescatepercentage = Rules.getRulevaluedouble(Rules.RESCATE_PERCENTAGE, this.productcode, this.clascode);


            this.Targetrate = Rules.getRulevaluedouble(Rules.TARGET_RATE, this.productcode, this.clascode);

            this.Targetfactor = Rules.getRulevaluedouble(Rules.TARGET_FACTOR, this.productcode, this.clascode);



            this.Regularmargin = Rules.getRulevaluedouble(Rules.REGULAR_MARGIN, this.productcode, this.clascode);
            this.Savingsmargin = Rules.getRulevaluedouble(Rules.SAVINGS_MARGIN, this.productcode, this.clascode);
            this.Ridersmargin = Rules.getRulevaluedouble(Rules.RIDERS_MARGIN, this.productcode, this.clascode);
            this.Minimumfirstagetarget = Rules.getRulevaluedouble(Rules.MINIMUM_FIRST_AGE_TARGET, this.productcode, this.clascode);
            this.Minimumfirstbaseprice = Rules.getRulevaluedouble(Rules.MINIMUM_FIRST_BASE_PRICE, this.productcode, this.clascode);
            this.Minimumsecondbaseprice = Rules.getRulevaluedouble(Rules.MINIMUM_SECOND_BASE_PRICE, this.productcode, this.clascode);
            this.insuranceunderage = Rules.getRulevalueint(Rules.INSURANCE_UNDERAGE, this.productcode, this.clascode);


        }




        public double calculatepv(double growthrate, int frequency, double amount)
        {
            double netamount = amount;
            for (int i = 1; i < frequency; i++)
            {
                netamount = netamount + amount * (1.0 / Math.Pow((1 + growthrate), i));

            }
            return netamount;

        }

        public double Yearlypremiumamount
        {
            get
            {
                double penaltypercent = this.frequencytypepenalty;
                double netperiodicpayment = periodicpremiumamount / (1 + penaltypercent);
                double periodicgrowthrate = Math.Pow((1 + this.growthrate), 1.0 / this.frequencytypevalue * 1.0) - 1;
                this.calcyearlypremium = calculatepv(periodicgrowthrate, this.frequencytypevalue, netperiodicpayment);
                return calcyearlypremium;
            }
        }

        public double goalseek(ITermassumedata asdata, double minamount, double maxamount)
        {
            //calculateAccountvalueonretirement(
            //            
            if (this.tocalculate == CALCTYPES.PREMIUMAMOUNT)
            {
                if (this.isrefund == false)
                {
                    regularinsurancecost = getAnnualpremiumregular(this.insuredamount);
                    rideradbcost = getAnnualadbpremiumregular();
                    ridertermcost = getAnnualaddlpremiumregular();
                    rideroircost = getAnnualoirpremiumregular();
                    double temppremium = regularinsurancecost + rideradbcost + ridertermcost + rideroircost+this.repaycost;
                    return temppremium;
                }
                else
                {
                    regularinsurancecost = getAnnualpremiumregular(this.insuredamount);
                    rideradbcost = getAnnualadbpremiumregular();
                    ridertermcost = getAnnualaddlpremiumregular();
                    rideroircost = getAnnualoirpremiumregular();

                    double midamount = (minamount + maxamount) / 2;
                    asdata.repayamount = midamount;
                    asdata.premiumamount = midamount / (1 - this.Ridersmargin) + regularinsurancecost + rideradbcost + ridertermcost + rideroircost;

                    double difference = calculateAccountvalue(asdata);
                    if (Math.Abs(difference) <= 2)
                    {
                        this.repaycostnomargin = midamount;
                        this.repaycost = midamount / (1 - this.Ridersmargin);
                        double temppremium = regularinsurancecost + rideradbcost + ridertermcost + rideroircost+repaycost;
                        return temppremium;
                    }
                    else if (Math.Abs(minamount - maxamount) <= 1)
                    {                        
                        this.repaycostnomargin = midamount;
                        this.repaycost = midamount / (1 - this.Ridersmargin);
                        double temppremium = regularinsurancecost + rideradbcost + ridertermcost + rideroircost + repaycost;
                        return temppremium;
                    }
                    else
                    {
                        if (difference > 0)
                        {
                            return goalseek(asdata, midamount, maxamount);
                        }
                        else
                        {
                            return goalseek(asdata, minamount, midamount);
                        }
                    }
                }                
            }
            else if (this.tocalculate == CALCTYPES.INSURANCEAMOUNT)
            {
                if (this.isrefund == false)
                {
                    double midamount = (maxamount + minamount) / 2.0;
                    asdata.insuranceamount = midamount;
                    //double tempnetpreaftermargin = Netpremiumaftermargin(asdata.insuranceamount);
                    double tempregularinsurancecost = getAnnualpremiumregular(asdata.insuranceamount);
                    double temprideradbcost = getAnnualadbpremiumregular();
                    double tempridertermcost = getAnnualaddlpremiumregular();
                    double temprideroircost = getAnnualoirpremiumregular();                    
                    double temppremium = tempregularinsurancecost + temprideradbcost + tempridertermcost + temprideroircost;

                    if (Math.Abs(temppremium - this.calcyearlypremium) <= this.precision)
                    {
                        regularinsurancecost = tempregularinsurancecost;
                        rideradbcost = temprideradbcost;
                        ridertermcost = tempridertermcost;
                        rideroircost = temprideroircost;
                        return asdata.insuranceamount;
                    }
                    else if ((maxamount - minamount) <= insuranceprecision)
                    {
                        regularinsurancecost = tempregularinsurancecost;
                        rideradbcost = temprideradbcost;
                        ridertermcost = tempridertermcost;
                        rideroircost = temprideroircost;
                        return asdata.insuranceamount;
                    }
                    else
                    {
                        if (temppremium > this.calcyearlypremium)
                        {
                            return goalseek(asdata, minamount, midamount);
                        }
                        else
                        {
                            return goalseek(asdata, midamount, maxamount);
                        }
                    }
               }
                else
                {
                    double midamount = (maxamount + minamount) / 2.0;
                    asdata.insuranceamount = midamount;
                    //double tempnetpreaftermargin = Netpremiumaftermargin(asdata.insuranceamount);
                    double tempregularinsurancecost = getAnnualpremiumregular(asdata.insuranceamount);
                    double temprideradbcost = getAnnualadbpremiumregular();
                    double tempridertermcost = getAnnualaddlpremiumregular();
                    double temprideroircost = getAnnualoirpremiumregular();
                    double temprepaycost = asdata.premiumamount - (tempregularinsurancecost + temprideradbcost + tempridertermcost + temprideroircost);
                    double temprepaycostnomargin = temprepaycost * (1 - this.Ridersmargin);
                    asdata.repayamount = temprepaycostnomargin;

                    if(temprepaycostnomargin>0)
                    {
                        double difference = calculateAccountvalue(asdata);
                        if (Math.Abs(difference) <= 2)
                        {
                            this.repaycostnomargin = temprepaycostnomargin;
                            this.repaycost = temprepaycost;
                            double tempinsuranceamount = midamount;
                            return tempinsuranceamount;
                        }
                        else if (Math.Abs(minamount - maxamount) <= 10)
                        {
                            this.repaycostnomargin = temprepaycostnomargin;
                            this.repaycost = temprepaycost;
                            double tempinsuranceamount = midamount;
                            return tempinsuranceamount;
                        }
                        else
                        {
                            if (difference > 0)
                            {
                                return goalseek(asdata, minamount, midamount);
                            }
                            else
                            {
                                return goalseek(asdata, midamount, maxamount);
                            }
                        }                        
                    }
                    else 
                    {
                        return goalseek(asdata, minamount, midamount);
                    }                                        
                }                
            }
            else
            {
                return 0.0;
            }
        }

        /*
        private double goalseekRepay(ITermassumedata asdata,double minamount,double maxamount)
        {
            double tempregularinsurancecost = getAnnualpremiumregular(asdata.insuranceamount);
            double temprideradbcost = getAnnualadbpremiumregular();
            double tempridertermcost = getAnnualaddlpremiumregular();
            double temprideroircost = getAnnualoirpremiumregular();
            double midamount = (minamount + maxamount) / 2;
            asdata.repayamount = midamount;
            double temppremiumamount = midamount / (1 - this.Ridersmargin) + regularinsurancecost + rideradbcost + ridertermcost + rideroircost;

            if (Math.Abs(temppremiumamount - asdata.premiumamount) <= this.premiumcost)
            {
                double difference = calculateAccountvalue(asdata);
                if (Math.Abs(difference) <= 2)
                {
                    this.repaycostnomargin = midamount;
                    this.repaycost = midamount / (1 - this.Ridersmargin);
                    double temppremium = regularinsurancecost + rideradbcost + ridertermcost + rideroircost + repaycost;
                    return temppremium;
                }
                else if (Math.Abs(minamount - maxamount) <= 1)
                {
                    this.repaycostnomargin = midamount;
                    this.repaycost = midamount / (1 - this.Ridersmargin);
                    double temppremium = regularinsurancecost + rideradbcost + ridertermcost + rideroircost + repaycost;
                    return temppremium;
                }
                else
                {
                    if (difference > 0)
                    {
                        return goalseek(asdata, midamount, maxamount);
                    }
                    else
                    {
                        return goalseek(asdata, minamount, midamount);
                    }
                }
            }            
        }
        */
        
        public double getGrowthrate(int sno)
        {            
            return this.netgrowthrate;            
        }


        
        public double getVariableperiodicPremiumamount(int sno)
        {   
            double adbyearlypremium=0.0;
            double oiryearlypremium=0.0;
            double addlyearlypremium=0.0;

            int adbyears = (this.Adbmaxage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.Adbmaxage - this.age + 1);
            int oiryears = oiruntilage;// (this.oiruntilage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.oiruntilage - this.oirage + 1);
            int addlyears = this.ridertermuntilage;// (this.ridertermuntilage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.ridertermuntilage - this.age + 1);

            if(sno==1)
            {                
                return periodicpremiumamount;
            }
            else
            {
                if (sno <= adbyears)
                {
                    adbyearlypremium = this.rideradbcost;
                }
                if (sno <= addlyears)
                {
                    addlyearlypremium = this.ridertermcost;
                }
                if (sno <= oiryears)
                {
                    oiryearlypremium = this.rideroircost;
                }

                double tempyearlypremiumamount = this.regularinsurancecost + adbyearlypremium + addlyearlypremium + oiryearlypremium+this.repaycost;

                double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
                return Math.Ceiling(-Financial.Pmt(netperiodicrate,frequencytypevalue, tempyearlypremiumamount,0,DueDate.BegOfPeriod) * (1.0 + frequencytypepenalty));                

            }

        }


        public double getperiodicPremiumamountforcalc(int sno)
        {
            double adbyearlypremium = 0.0;
            double oiryearlypremium = 0.0;
            double addlyearlypremium = 0.0;

            int adbyears = (this.Adbmaxage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.Adbmaxage - this.age + 1);
            int oiryears = this.oiruntilage;// - this.age + 1) > this.numberofyears ? this.numberofyears : (this.oiruntilage - this.age + 1);
            int addlyears = this.ridertermuntilage;//(this.ridertermuntilage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.ridertermuntilage - this.age + 1);

            if (sno == 1)
            {
                return periodicpremiumamount;
            }
            else
            {
                if (sno <= adbyears)
                {
                    adbyearlypremium = this.rideradbcost;
                }
                if (sno <= addlyears)
                {
                    addlyearlypremium = this.ridertermcost;
                }
                if (sno <= oiryears)
                {
                    oiryearlypremium = this.rideroircost;
                }

                double tempyearlypremiumamount = this.regularinsurancecost + adbyearlypremium + addlyearlypremium + oiryearlypremium + this.repaycost;

                double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
                return Math.Ceiling(-Financial.Pmt(netperiodicrate, frequencytypevalue, tempyearlypremiumamount, 0, DueDate.BegOfPeriod) * (1.0 + frequencytypepenalty));

            }

        }


        public double getVariableisuranceamount(int sno)
        {
            double adbyearlyamount = 0.0;
            double oiryearlyamount = 0.0;
            double addlyearlyamount = 0.0;

            int adbyears = (this.Adbmaxage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.Adbmaxage - this.age + 1);
            int oiryears = this.oiruntilage;// (this.oiruntilage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.oiruntilage - this.age + 1);
            int addlyears = this.ridertermuntilage;// (this.ridertermuntilage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.ridertermuntilage - this.age + 1);

            
            if (sno <= adbyears)
            {
                adbyearlyamount = this.rideradbamount;
            }
            if (sno <= addlyears)
            {
                addlyearlyamount = this.ridertermamount;
            }
            if (sno <= oiryears)
            {
                oiryearlyamount = this.rideroiramount;
            }
            double tempyearlyinsuredamount = this.insuredamount + addlyearlyamount ;

            return tempyearlyinsuredamount;                
            

        }

        
        public double getVariableRiderperiodicPremiumamount(int sno)
        {
            double adbyearlypremium = 0.0;
            double oiryearlypremium = 0.0;
            double addlyearlypremium = 0.0;

            int adbyears = (this.Adbmaxage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.Adbmaxage - this.age + 1);
            int oiryears = oiruntilage;// (this.oiruntilage - this.oirage + 1) > this.numberofyears ? this.numberofyears : (this.oiruntilage - this.oirage + 1);
            int addlyears = this.ridertermuntilage;// (this.ridertermuntilage - this.age + 1) > this.numberofyears ? this.numberofyears : (this.ridertermuntilage - this.age + 1);

            if (sno <= adbyears)
            {
                adbyearlypremium = this.rideradbcost;
            }
            else
            {
                adbyearlypremium = 0;
            }
            if (sno <= addlyears)
            {
                addlyearlypremium = this.ridertermcost;
            }
            else
            {
                addlyearlypremium=0;
            }
            if (sno <= oiryears)
            {
                oiryearlypremium = this.rideroircost;
            }
            else
            {
                oiryearlypremium = 0;
            }
            double tempyearlyoirpremiumamount = adbyearlypremium + addlyearlypremium + oiryearlypremium;
            double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
            return Math.Ceiling(-Financial.Pmt(netperiodicrate, frequencytypevalue, tempyearlyoirpremiumamount, 0, DueDate.BegOfPeriod) * (1.0 + frequencytypepenalty));

        }



        public Termillusdata[] getIllustration()
        {           
            
            double accumulatedpremium = 0.0;
            double temppremiumamount;
            double tempyearlypremiumamount;
            double tempriderpremiumamount;

            Termillusdata[] tempillusdata = new Termillusdata[this.retirementnoofyears];
            for (int i = 0; i < retirementnoofyears; i++)
            {
                temppremiumamount = periodicpremiumamount;
                tempyearlypremiumamount = temppremiumamount * this.frequencytypevalue;

                if (i == 0)
                {
                    tempyearlypremiumamount=tempyearlypremiumamount+this.initialcontributionamount;
                }
                



                tempriderpremiumamount = getVariableRiderperiodicPremiumamount(i+1) * this.frequencytypevalue;
                //tempriderpremiumamount = getVariableRiderperiodicPremiumamount(i + 1) * this.frequencytypevalue;

                accumulatedpremium = accumulatedpremium + tempyearlypremiumamount;
                    
                tempillusdata[i] = new Termillusdata();
                tempillusdata[i].Sno = i + 1;
                tempillusdata[i].Age =this.age+ i ;
                tempillusdata[i].Accumulatedpremium = accumulatedpremium;
                tempillusdata[i].Insuredamount = getVariableisuranceamount(i+1);
                tempillusdata[i].Regularpremium = tempyearlypremiumamount - getVariableRiderperiodicPremiumamount(0) * this.frequencytypevalue;
                tempillusdata[i].Riderpremium = tempriderpremiumamount;
                tempillusdata[i].Totalbenefitamount = getVariableisuranceamount(i+1);
                tempillusdata[i].Totalpremium = tempillusdata[i].Regularpremium + tempriderpremiumamount;

            }

            return tempillusdata;
        }



        public Termillusdata[] getIllustrationtwo()
        {

            double accumulatedpremium = 0.0;
            double temppremiumamount;
            double tempyearlypremiumamount;
            double tempriderpremiumamount;


            int numyears = this.Maxage - (this.age + this.retirementnoofyears)+1;

            if (numyears > 0)
            {

                Termillusdata[] tempillusdata = new Termillusdata[numyears];

                for (int i = 0; i < numyears; i++)
                {
                    temppremiumamount = (primarymortdatatable[this.age + this.retirementnoofyears + i ].mortalityvalue * this.insuredamount / 1000) * (1 / (1 - this.Regularmargin))*(1+Extratermpenalty);
                    tempyearlypremiumamount = temppremiumamount;
                    tempriderpremiumamount = 0;

                    accumulatedpremium = accumulatedpremium + tempyearlypremiumamount;

                    tempillusdata[i] = new Termillusdata();
                    tempillusdata[i].Sno = i + 1+this.numberofyears;
                    tempillusdata[i].Age = this.age + i + this.retirementnoofyears;
                    tempillusdata[i].Accumulatedpremium = accumulatedpremium;
                    tempillusdata[i].Insuredamount = this.insuredamount;
                    tempillusdata[i].Regularpremium = tempyearlypremiumamount - tempriderpremiumamount;
                    tempillusdata[i].Riderpremium = tempriderpremiumamount;
                    tempillusdata[i].Totalbenefitamount = this.insuredamount;
                    tempillusdata[i].Totalpremium = tempyearlypremiumamount;

                }

                return tempillusdata;
            }
            else
            {
                return null;
            }
        }


        public double calculatedPeriodicPremiumAmount()
        {
            if (this.tocalculate == CALCTYPES.PREMIUMAMOUNT)
            {
                //double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
                double temppremiumamount = rideradbcost + ridertermcost + rideroircost + regularinsurancecost+this.repaycost;
                //return Math.Round((-Financial.Pmt(netperiodicrate, this.frequencytypevalue, temppremiumamount, 0, DueDate.BegOfPeriod)) * (1.0 + frequencytypepenalty),0);
                //return Math.Ceiling(calculatePMT(temppremiumamount, frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));
                return temppremiumamount / frequencytypevalue;
            }
            else
            {
                return this.periodicpremiumamount;
            }
        }


        public double calculatedYearlyPeriodicPremiumAmount()
        {
            if (this.tocalculate == CALCTYPES.PREMIUMAMOUNT)
            {
                //double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
                double temppremiumamount = rideradbcost + ridertermcost + rideroircost + regularinsurancecost + this.repaycost;
                //return Math.Round((-Financial.Pmt(netperiodicrate, this.frequencytypevalue, temppremiumamount, 0, DueDate.BegOfPeriod)) * (1.0 + frequencytypepenalty),0);
                //return Math.Ceiling(calculatePMT(temppremiumamount, frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));
                return temppremiumamount / frequencytypevalue;
            }
            else
            {
                return this.periodicpremiumamount;
            }
        }



        private double calculatePMT(double paymentamount, int payments, double rate)
        {
            double netrate = 0.0;
            for (int i = 0; i < payments; i++)
            {
                netrate = netrate + (1.0 / (Math.Pow((1 + rate), i)));
            }
            return paymentamount * (1.0 / netrate);
        }

        public double Calcaveragepremium
        {
            get
            {
                if (varpremiumcode == 'N')
                {
                    return this.calcyearlypremium;
                }
                else
                {
                    double totpremium = 0.0;
                    for (int i = 0; i < varpremiumamount.Length; i++)
                    {
                        totpremium = totpremium + varpremiumamount[i];
                    }
                    return totpremium / varpremiumamount.Length;
                }

            }

        }

        
        

        public double getTargetamountFE()
        {
            double temptargetamount = calculatedtargetamount;
            return temptargetamount;
        }

        public double getInsuredamountFE()
        {            
            return calcinsuredamount;
        }

        

        private double Actualpremiumpaid
        {
            get
            {
                return periodicpremiumamount * frequencytypevalue;
            }
        }




        public double getMinimumInsurancecost(double tempinsuranceamount)
        {   
            //double mincost=0;
            double partone=(this.age<this.Minimumfirstagetarget?this.Minimumfirstbaseprice:this.Minimumsecondbaseprice)+
                (tempinsuranceamount/1000.0-(this.age<=this.insuranceunderage?50:100))*.25;
            double parttwo=(this.rideroiramount!=0?(this.oirage<this.Minimumfirstagetarget?this.Minimumfirstbaseprice:this.Minimumsecondbaseprice)+
                this.rideroiramount/1000-(this.age<=this.insuranceunderage?50:100)*.25:0);
            return Math.Max(partone, parttwo);
                        
        }

        

        private double getTotalpremiumpaid()
        {
            double totalpremiumpaid = 0.0;
            totalpremiumpaid =this.Yearlypremiumamount;// totalpremiumpaid+this.Yearlypremiumamount*this.retirementnoofyears;
            //totalpremiumpaid=totalpremiumpaid+(this.rideradbcost+
            return totalpremiumpaid;
        }

        

        public double getTotalminimumoirpremiumregular()
        {
            int oiryears = this.oiruntilage;// Math.Min(this.oiruntilage - this.oirage + 1, this.numberofyears);
            double totoirinsurancecost = this.getTotaloirinsurancecost();
            return totoirinsurancecost / (1 - this.Ridersmargin) - ((othermortdatatable[this.oirage].nx - othermortdatatable[this.oirage + oiryears].nx) / othermortdatatable[this.oirage].ddx) * this.Minimumridercost;
        }

        public double getTotalminimumaddlpremiumregular()
        {
            int addlyears = this.ridertermuntilage;// Math.Min(this.ridertermuntilage - this.age + 1, this.numberofyears);
            double totaddlinsurancecost = this.getTotaladdlinsurancecost();
            return totaddlinsurancecost / (1 - this.Ridersmargin) - ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + addlyears].nx) / primarymortdatatable[this.age].ddx) * this.Minimumridercost;
            
        }

        public double getTotalminimumadbpremium()
        {
            
            int adbyears = Math.Min(this.Adbmaxage - this.age + 1, this.numberofyears);
            double totadbcost = this.getTotaladbridercost();
            return totadbcost/ (1 - this.Ridersmargin) - ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + adbyears].nx) / primarymortdatatable[this.age].ddx)*this.Minimumridercost;
            
        }

        

        public double getPeriodicoirpremiumregular()
        {            
            double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
            return Math.Ceiling(calculatePMT(getNetoirpremiumaftermargin(), frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));            
        }

        public double getPeriodicaddlpremiumregular()
        {            
            double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
            return Math.Ceiling(calculatePMT(getNetaddlpremiumaftermargin(), frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));            
        }

        public double getPeriodicadbpremiumregular()
        {            
            double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
            return Math.Ceiling(calculatePMT(getNetadbpremiumaftermargin(), frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));            
        }

        public double getAnnualadbpremiumregular()
        {         
            return getPeriodicadbpremiumregular() * this.frequencytypevalue;         
        }


        public double getAnnualaddlpremiumregular()
        {                            
            return getPeriodicaddlpremiumregular() * this.frequencytypevalue;            
        }

        public double getAnnualoirpremiumregular()
        {            
            return getPeriodicoirpremiumregular() * this.frequencytypevalue;            
        }        

        public double getNetoirpremiumaftermargin()
        {
            double tempnetoirinsurancecost = getNetoirinsurancecost();
            if (this.rideroircode == 'Y')
            {
                if (this.Minimumridercost > (tempnetoirinsurancecost / (1 - this.Ridersmargin)))
                {
                    return this.Minimumridercost;
                }
                else
                {
                    return tempnetoirinsurancecost / (1 - this.Ridersmargin);
                }
            }
            else
            {
                return 0.0;
            }                
            
        }

        public double getNetadbpremiumaftermargin()
        {
            double tempnetadbridercost=getNetadbridercost();
            if (this.rideradbcode == 'Y')
            {
                if (this.Minimumridercost > (tempnetadbridercost / (1 - this.Ridersmargin)))
                {
                    return this.Minimumridercost;
                }
                else
                {
                    return tempnetadbridercost / (1 - this.Ridersmargin);
                }
            }
            else
            {
                return 0.0;
            }
            
        }

        public double getNetaddlpremiumaftermargin()
        {
            double tempnetaddlinsurancecost = getNetaddlinsurancecost();
            if (this.ridertermcode == 'Y')
            {
                if (this.Minimumridercost > (tempnetaddlinsurancecost / (1 - this.Ridersmargin)))
                {
                    return this.Minimumridercost;
                }
                else
                {
                    return tempnetaddlinsurancecost / (1 - this.Ridersmargin);
                }
            }
            else
            {
                return 0.0;
            }                               
            
        }


        

        public double getNetoirinsurancecost()
        {            
            if (this.rideroircode == 'Y')
            {
                int oiryears = this.oiruntilage;// Math.Min(this.oiruntilage - this.oirage + 1, this.numberofyears);
                return this.getTotaloirinsurancecost() * (1 / ((othermortdatatable[this.oirage].nx - othermortdatatable[this.oirage + oiryears].nx) / othermortdatatable[this.oirage].ddx));
            }
            else
            {
                return 0.0;
            }                
            
        }

        public double getNetadbridercost()
        {
            if (this.rideradbcode == 'Y')
            {
                int adbyears = Math.Min(this.Adbmaxage - this.age + 1, this.numberofyears);
                return this.getTotaladbridercost() * (1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + adbyears].nx) / primarymortdatatable[this.age].ddx));
            }
            else
            {
                return 0.0;
            }                            
        }

        public double getNetaddlinsurancecost()
        {   
            if (ridertermcode == 'Y')
            {
                int addltermyears = this.ridertermuntilage; //Math.Min(this.ridertermuntilage - this.age + 1, this.numberofyears);
                return this.getTotaladdlinsurancecost() * (1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + addltermyears].nx) / primarymortdatatable[this.age].ddx));
            }
            else
            {
                return 0.0;
            }
        }



        public double getTotaloirinsurancecost()
        {
            if (this.rideroircode == 'Y')
            {
                int oiryears = this.oiruntilage;// Math.Min(this.oiruntilage - this.oirage + 1, this.numberofyears);
                double totoirinsuredcost = 0.0;
                for (int i = 1; i <= oiryears; i++)
                {
                    if (i == 1)
                    {
                        totoirinsuredcost = totoirinsuredcost + this.rideroiramount * othermortdatatable[this.oirage].mortalityvalue * (1 / 1000.0) * (1 / (1 + this.netgrowthrate));
                    }
                    else
                    {
                        totoirinsuredcost = totoirinsuredcost + this.rideroiramount * (othermortdatatable[this.oirage + i - 1].dx / othermortdatatable[this.oirage].lx) * Math.Pow((1 / (1 + this.netgrowthrate)), i);
                    }
                }
                return totoirinsuredcost;// *(1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + this.numberofyears].nx) / primarymortdatatable[this.age].ddx));
            }
            else
            {
                return 0.0;
            }                
         
        }


        
        public double getTotaladdlinsurancecost()
        {            
            if (this.ridertermcode == 'Y')
            {
                int addltermyears = this.ridertermuntilage;// - this.age + 1, this.numberofyears);
                double totaddlinsuredcost = 0.0;
                for (int i = 1; i <= addltermyears; i++)
                {
                    if (i == 1)
                    {
                        totaddlinsuredcost = totaddlinsuredcost + this.ridertermamount * primarymortdatatable[this.age].mortalityvalue * (1 / 1000.0) * (1 / (1 + this.netgrowthrate));
                    }
                    else
                    {
                        totaddlinsuredcost = totaddlinsuredcost + this.ridertermamount * (primarymortdatatable[this.age + i - 1].dx / primarymortdatatable[this.age].lx) * Math.Pow((1 / (1 + this.netgrowthrate)), i);
                    }
                }
                return totaddlinsuredcost;// *(1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + this.numberofyears].nx) / primarymortdatatable[this.age].ddx));
            }
            else
            {
                return 0.0;
            }
                
            
        }



        public double getTotaladbridercost()
        {   
            if (this.rideradbcode == 'Y')
            {
                int adbyears = Math.Min(this.Adbmaxage - this.age + 1, this.numberofyears);
                double totadbcost = 0.0;
                for (int i = 1; i <= adbyears; i++)
                {
                    totadbcost = totadbcost + this.rideradbamount * (1 / 1000.0) * this.Adbfactor * Math.Pow((1 / (1 + this.netgrowthrate)), i);
                }
                return totadbcost;// *(1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + this.numberofyears].nx) / primarymortdatatable[this.age].ddx));
            }
            else
            {
                return 0.0;
            }                            
        }

        public double getExcessinitialnormal(double tempinsuranceamount)
        {
            double tempexcessinitialnormal = (getTotalInsurancecost(tempinsuranceamount)/(1-this.Regularmargin))>this.initialcontributionamount?this.initialcontributionamount-(getTotalInsurancecost(tempinsuranceamount)/(1-this.Regularmargin)):0;
            return tempexcessinitialnormal;
        }


        public double getExcessinitialriders(double tempinsuranceamount)
        {
            double tempexcessinitialriders = ((getExcessinitialnormal(tempinsuranceamount) * (1 - this.Regularmargin)) < this.initialcontributionamount) ? this.initialcontributionamount - (getTotalInsurancecost(tempinsuranceamount) / (1 - this.Ridersmargin)) : 0;
            return tempexcessinitialriders;
        }

        public double getTotalridercost(double tempinsuranceamount)
        {
            double tempadbrideramount = getTotaladbridercost();
            double tempaddlrideramount = getTotaladdlinsurancecost();
            double tempoirrideramount = getTotaloirinsurancecost();

            return (tempadbrideramount+tempaddlrideramount+tempoirrideramount-getExcessinitialriders(tempinsuranceamount)*(1-this.Ridersmargin));
            
        }

        public double getNetridercost(double tempinsuranceamount)
        {

            double tempadbrideramount = getTotaladbridercost();
            double tempaddlrideramount = getTotaladdlinsurancecost();
            double tempoirrideramount = getTotaloirinsurancecost();


            double tempnetadbrideramount = getNetadbridercost();
            double tempnetaddlrideramount = getNetaddlinsurancecost();
            double tempnetoirrideramount = getNetoirinsurancecost();

            if ((tempnetadbrideramount + tempnetaddlrideramount + tempnetoirrideramount) == 0)
            {
                return 0.0;
            }
            else
            {
                return (tempnetadbrideramount + tempnetaddlrideramount + tempnetoirrideramount) * (getTotalridercost(tempinsuranceamount) / (tempadbrideramount + tempaddlrideramount + tempoirrideramount));
            }
            
        }


        public double getNetriderpremiumaftermargin(double tempinsuranceamount)
        {
            double tempnetadbaftermargin = getNetadbpremiumaftermargin();
            double tempnetaddlaftermargin = getNetaddlpremiumaftermargin();
            double tempnetoiraftermargin = getNetoirpremiumaftermargin();

            return tempnetadbaftermargin + tempnetaddlaftermargin + tempnetoiraftermargin;            

        }

        public double getPeriodicriderpremiumaftermargin(double tempinsuranceamount)
        {
            //double tempperiodicadbaftermargin = getPeriodicadbpremiumregular();
            //double tempperiodicaddlaftermargin = getPeriodicaddlpremiumregular();
            //double tempperiodicoiraftermargin = getPeriodicoirpremiumregular();

            double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
            return Math.Ceiling(calculatePMT(getNetriderpremiumaftermargin(tempinsuranceamount), frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));
        }

        public double getAnnualriderpremiumaftermargin(double tempinsuranceamount)
        {
            return getPeriodicriderpremiumaftermargin(tempinsuranceamount) * this.frequencytypevalue;
        }

        public double getTotalminimumriderpremium(double tempinsuranceamount)
        {
            return (getTotalminimumadbpremium() + getTotalminimumaddlpremiumregular() + getTotalminimumoirpremiumregular());
        }

        /*
        public double getAnnualriderpremiumaftermargin(double tempinsuranceamount)
        {
            return getPeriodicriderpremiumaftermargin(tempinsuranceamount) * this.frequencytypevalue;
        }*/

        public double getTotalannualpremium(double tempinsuranceamount)
        {
            return (getAnnualpremiumregular(tempinsuranceamount)+ getAnnualadbpremiumregular()+getAnnualaddlpremiumregular()+getAnnualoirpremiumregular());
        }

        public double getTotalannualriderpremium(double tempinsuranceamount)
        {
            return getPeriodicriderpremiumaftermargin(tempinsuranceamount) * this.frequencytypevalue;
        }

        public double getTargetamount()
        {            
            double calctemptarget = 0.0;
            calctemptarget = Math.Min(Financial.Pmt(this.Targetrate, this.Targetcontributionperiod, Financial.PV(this.Targetrate, this.numberofyears, getTotalannualpremium(this.insuredamount) - getTotalannualriderpremium(this.insuredamount)+this.repaycost, 0, DueDate.BegOfPeriod) + ((this.isrefund==false)? 0.5 * Financial.PV(this.Targetrate, this.numberofyears, (getTotalannualriderpremium(this.insuredamount)), 0, DueDate.BegOfPeriod):0) - this.initialcontributionamount, 0, DueDate.BegOfPeriod), 0.95 * (this.periodicpremiumamount * this.frequencytypevalue) + this.getCommissionableinicial());
            return Math.Round(calctemptarget * this.Targetfactor,0);            
        }

        public double Calcyearlypremiumamount(double tempinsuranceamount)
        {            
            return (getAnnualpremiumregular(tempinsuranceamount) + this.getAnnualadbpremiumregular() + this.getAnnualaddlpremiumregular() + this.getAnnualoirpremiumregular());            
        }

        public double Totalminimumpremiumregular(double tempinsuranceamount)
        {
            double totinsurancecost = getTotalInsurancecost(tempinsuranceamount);
            return totinsurancecost / (1 - this.Regularmargin) - ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + this.numberofyears].nx) / primarymortdatatable[this.age].ddx) * getMinimumInsurancecost(tempinsuranceamount);

        }

        public double getAnnualpremiumregular(double tempinsuranceamount)
        {
            return getPeriodicpremiumregular(tempinsuranceamount) * this.frequencytypevalue;        
        }

        public double getPeriodicpremiumregular(double tempinsuranceamount)
        {
            double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
            return Math.Ceiling(calculatePMT(Netpremiumaftermargin(tempinsuranceamount), frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));
        }

        public double Netpremiumaftermargin(double tempinsuranceamount)
        {            
            if (getMinimumInsurancecost(tempinsuranceamount) > (getNetinsurancecost(tempinsuranceamount) / (1 - this.Regularmargin)))
            {
                return this.getMinimumInsurancecost(tempinsuranceamount);
            }
            else
            {
                return getNetinsurancecost(tempinsuranceamount)/ (1 - this.Regularmargin);
            }            
        }

        public double getNetinsurancecost(double tempinsuranceamount)
        {            
            return getTotalInsurancecost(tempinsuranceamount) * (1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + this.numberofyears].nx) / primarymortdatatable[this.age].ddx));            
        }

        public double getTotalInsurancecost(double tempinsuranceamount)
        {
            double totinsuredcost = 0.0;
            for (int i = 1; i <= this.retirementnoofyears; i++)
            {
                if (i == 1)
                {
                    totinsuredcost = totinsuredcost + tempinsuranceamount * primarymortdatatable[this.age].mortalityvalue * (1 / 1000.0) * (1 / (1 + this.netgrowthrate));
                }
                else
                {
                    totinsuredcost = totinsuredcost + tempinsuranceamount * (primarymortdatatable[this.age + i - 1].dx / primarymortdatatable[this.age].lx) * Math.Pow((1 / (1 + this.netgrowthrate)), i);
                }
            }
            if (this.initialcontributioncode == 'N')
            {
                return totinsuredcost;
            }
            else
            {
                return (totinsuredcost-this.initialcontributionamount*(1-this.Regularmargin));
            }
            // *(1 / ((primarymortdatatable[this.age].nx - primarymortdatatable[this.age + this.numberofyears].nx) / primarymortdatatable[this.age].ddx));
        }

        private double getCommissionableinicial()
        {
            return Financial.Pmt(this.Targetrate, this.Targetcontributionperiod, this.initialcontributionamount, 0, DueDate.BegOfPeriod);
        }

        private void setVivodata()
        {
            vivodata = new double[this.retirementnoofyears];
            for (int i = 0; i < this.retirementnoofyears; i++)
            {
                vivodata[i] = primarymortdatatable[i + this.age].lx / primarymortdatatable[this.age].lx;
            }
            
        }

        private double calculateAccountvalue(ITermassumedata asdata)
        {
            double accountvalue = 0;
            double accumulatedpremium= 0;
            double netaccumulatedpremium = 0;
            double difference = 0;

            for (int i = 0; i < this.retirementnoofyears; i++)
            {
                if (i == 0)
                {
                    accumulatedpremium = accumulatedpremium + this.initialcontributionamount + asdata.premiumamount;
                }
                else
                {
                    accumulatedpremium = accumulatedpremium + asdata.premiumamount;
                }
                netaccumulatedpremium = accumulatedpremium * remaindata[i];

                accountvalue =(1+this.netgrowthrate)* (accountvalue + asdata.repayamount);
                difference = netaccumulatedpremium - accountvalue;
            }
            return difference;

        }



        private void setRemaindata()
        {
            if (this.isrefund == true)
            {
                remaindata = new double[this.retirementnoofyears + 1];
                for (int i = 0; i < this.retirementnoofyears; i++)
                {
                    if (i == 0)
                    {
                        remaindata[i] = 1;
                    }
                    else
                    {
                        remaindata[i] = (remaindata[i - 1] - canceldata[i].cancelpercent * remaindata[i - 1]) * (vivodata[i] / vivodata[i - 1]);
                    }
                    //remaindata[i] = primarymortdatatable[i + this.age].lx / primarymortdatatable[this.age].lx;
                }
            }
            

        }


        public double getAnnualizedPeriodicPremium()
        {
            if (this.frequencytypevalue == 1)
            {
                return this.calcyearlypremium;
            }
            else
            {

                //double netperiodicrate = Math.Pow((1 + growthrate), 1.0 / frequencytypevalue) - 1;
                double temppremiumamount = rideradbcost + ridertermcost + rideroircost + regularinsurancecost + this.repaycost;
                //return Math.Round((-Financial.Pmt(netperiodicrate, this.frequencytypevalue, temppremiumamount, 0, DueDate.BegOfPeriod)) * (1.0 + frequencytypepenalty),0);
                //return Math.Ceiling(calculatePMT(temppremiumamount, frequencytypevalue, netperiodicrate) * (1.0 + frequencytypepenalty));
                //return temppremiumamount / frequencytypevalue;

                return this.calcyearlypremium;// *(1 / (1 + this.frequencytypepenalty));
            }            
        }



        public double Ridertermamount
        {
            get
            {
                return this.ridertermamount;
            }
        }




    }

}