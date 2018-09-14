﻿using DevExpress.Data.Linq;
using DevExpress.Web.ASPxPivotGrid;
using RESOURCE.UnderWriting.NewBussiness;
using System;
using System.Linq;
using WEB.NewBusiness.Common;
using WEB.NewBusiness.Common.Statistics;

namespace WEB.NewBusiness.NewBusiness.UserControls.Statistics
{
    public partial class UCEmisiones : UC, IUC
    {
        int currentPeriodYear = 0,
            currentPeriodData = 0;

        Period.Periods currentPeriod = 0;
        public int ReportType
        {
            get
            {
                return ViewState["ReportType"].ToInt();
            }
            set
            {
                ViewState["ReportType"] = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Translator(string.Empty);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void dsEmisiones_Selecting(object sender, LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "Statistics_Id";
            e.QueryableSource = GetEmisiones();
        }

        private IQueryable GetEmisiones()
        {
            var periodQuantity = 13;

            currentPeriod = (Period.Periods)ViewState["period"];
            currentPeriodYear = Convert.ToInt32(ViewState["year"]);
            currentPeriodData = Convert.ToInt32(ViewState["periodData"]);

            var from = DateTime.Now;
            if ((currentPeriod == Period.Periods.SeasonalMonth ||
                 currentPeriod == Period.Periods.SeasonalQuarter ||
                 currentPeriod == Period.Periods.SeasonalSemestral) &&
                currentPeriodData != 0)
                from = new DateTime(DateTime.Now.Year, currentPeriodData, 1);

            if (currentPeriod == Period.Periods.Last3Month)
                periodQuantity = 3;
            else if (currentPeriod == Period.Periods.Last6Month)
                periodQuantity = 6;

            var lstPeriods = from.GetPeriodsDate(currentPeriod, periodQuantity);
            from = lstPeriods.Min(o => o.StartDate);
            var to = lstPeriods.Max(o => o.EndDate);
            var pivotQuarter = currentPeriodData.ToString().PadLeft(2, '0') + "-" + Utility.GetQuarterFromMonth(currentPeriodData);

            string pivotSemestral = "";
            if (currentPeriod == Period.Periods.SeasonalSemestral)
            {
                if (currentPeriodData >= 1 && currentPeriodData <= 6)
                {
                    pivotSemestral = "01" + "-" + Utility.GetSemestralFromMonth(currentPeriodData);
                    to = new DateTime(to.Year, 6, 30);
                }
                else if (currentPeriodData >= 7 && currentPeriodData <= 12)
                {
                    pivotSemestral = "02" + "-" + Utility.GetSemestralFromMonth(currentPeriodData);
                    to = new DateTime(to.Year, 12, 31);
                }
            }

            if ((currentPeriodYear > 0) && (currentPeriod != Period.Periods.Monthly && currentPeriod != Period.Periods.Last3Month && currentPeriod != Period.Periods.Last6Month))
                from = new DateTime(to.Year - (currentPeriodYear - 1), 1, 1);

            StatisticsEntities statistics = new StatisticsEntities();
            var query = statistics.VW_GET_QUO_PL_POLICY_STATISTICS_BYMONTH.Where(o => o.Date >= from && o.Date <= to && o.Request_Type_Id== ReportType);
            
            if (currentPeriod == Period.Periods.SeasonalMonth)
                query = query.Where(o => o.Month == currentPeriodData);

            if (currentPeriod == Period.Periods.SeasonalQuarter)
                query = query.Where(o => o.PivotQuarter == pivotQuarter);

            if (currentPeriod == Period.Periods.SeasonalSemestral)
                query = query.Where(o => o.PivotSemestral == pivotSemestral);

            return query.ToArray().AsQueryable();
        }

        public void Search(Utility.StatisticsEmisionesViewBy pivot, Period.Periods period, int periodData, int year)
        {
            pnlEmisionesCantidad.Visible =
            pnlEmisionesPrima.Visible =
            pnlEmisionesSumaAsegurada.Visible =
            pnlEmisionesTasa.Visible =
            pnlEmisionesCantidadVehiculos.Visible = false;

            currentPeriodYear = year;
            currentPeriod = period;
            currentPeriodData = periodData;

            ViewState["period"] = period;
            ViewState["year"] = year;
            ViewState["periodData"] = periodData;
            ViewState["pivot"] = pivot;

            switch (pivot)
            {
                case Utility.StatisticsEmisionesViewBy.Count:
                    pnlEmisionesCantidad.Visible = true;
                    ConfigureGrid(pgEmisionesCantidad, period);
                    pgEmisionesCantidad.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.AnnualPremium:
                    pnlEmisionesPrima.Visible = true;
                    ConfigureGrid(pgEmisionesPrima, period);
                    pgEmisionesPrima.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.Insured_Amount:
                    pnlEmisionesSumaAsegurada.Visible = true;
                    ConfigureGrid(pgEmisionesSumaAsegurada, period);
                    pgEmisionesSumaAsegurada.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.Rate:
                    pnlEmisionesTasa.Visible = true;
                    ConfigureGrid(pgEmisionesTasa, period);
                    pgEmisionesTasa.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.VehicleCount:
                    pnlEmisionesCantidadVehiculos.Visible = true;
                    ConfigureGrid(pgEmisionesCantidadVehiculos, period);
                    pgEmisionesCantidadVehiculos.DataBind();
                    break;
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
        }

        private void ConfigureGrid(ASPxPivotGrid grd, Period.Periods period)
        {
            grd.ClientSideEvents.EndCallback = Utility.GetJSPivotEndCallbackClickHandler(grd.ClientID);
            grd.FieldValueDisplayText += pivotGrid_FieldValueDisplayText;

            this.ExcecuteJScript("ConfiguraMontos();");

            #region Period
            if (period == Period.Periods.Yearly)
            {
                #region Yearly
                if (grd.Fields["PivotMonth"] != null)
                    grd.Fields.Remove(grd.Fields["PivotMonth"]);

                if (grd.Fields["PivotQuarter"] != null)
                    grd.Fields.Remove(grd.Fields["PivotQuarter"]);

                if (grd.Fields["PivotSemestral"] != null)
                    grd.Fields.Remove(grd.Fields["PivotSemestral"]);
                #endregion
            }
            else if (period == Period.Periods.Quarterly || period == Period.Periods.SeasonalQuarter)
            {
                #region Quarterly
                if (grd.Fields["PivotQuarter"] != null)
                    grd.Fields.Remove(grd.Fields["PivotQuarter"]);

                MonthTemplate.AddQuarterToPivot(grd);

                if (grd.Fields["PivotMonth"] != null)
                    grd.Fields.Remove(grd.Fields["PivotMonth"]);

                if (grd.Fields["PivotSemestral"] != null)
                    grd.Fields.Remove(grd.Fields["PivotSemestral"]);
                #endregion
            }
            else if (period == Period.Periods.Monthly || period == Period.Periods.SeasonalMonth)
            {
                #region Monthly
                if (grd.Fields["PivotMonth"] != null)
                    grd.Fields.Remove(grd.Fields["PivotMonth"]);

                MonthTemplate.AddMonthToPivot(grd);
                if (grd.Fields["PivotQuarter"] != null)
                    grd.Fields.Remove(grd.Fields["PivotQuarter"]);

                if (grd.Fields["PivotSemestral"] != null)
                    grd.Fields.Remove(grd.Fields["PivotSemestral"]);
                #endregion
            }
            else if (period == Period.Periods.Semestral || period == Period.Periods.SeasonalSemestral)
            {
                #region Semestral
                if (grd.Fields["PivotSemestral"] != null)
                    grd.Fields.Remove(grd.Fields["PivotSemestral"]);

                MonthTemplate.AddSemestralToPivot(grd);

                if (grd.Fields["PivotMonth"] != null)
                    grd.Fields.Remove(grd.Fields["PivotMonth"]);

                if (grd.Fields["PivotQuarter"] != null)
                    grd.Fields.Remove(grd.Fields["PivotQuarter"]);
                #endregion
            }
            else if (period == Period.Periods.Last3Month || period == Period.Periods.Last6Month)
            {
                #region Last3Month || Last6Month
                if (grd.Fields["PivotMonth"] != null)
                    grd.Fields.Remove(grd.Fields["PivotMonth"]);

                MonthTemplate.AddMonthToPivot(grd);

                if (grd.Fields["PivotQuarter"] != null)
                    grd.Fields.Remove(grd.Fields["PivotQuarter"]);

                if (grd.Fields["PivotSemestral"] != null)
                    grd.Fields.Remove(grd.Fields["PivotSemestral"]);
                #endregion
            }
            else
            {
                #region Month
                if (grd.Fields["PivotMonth"] != null)
                    grd.Fields.Remove(grd.Fields["PivotMonth"]);

                MonthTemplate.AddMonthToPivot(grd);

                if (grd.Fields["PivotQuarter"] != null)
                    grd.Fields.Remove(grd.Fields["PivotQuarter"]);

                if (grd.Fields["PivotSemestral"] != null)
                    grd.Fields.Remove(grd.Fields["PivotSemestral"]);
                #endregion
            }
            #endregion

            #region Caption
            foreach (var field in grd.Fields)
            {
                var item = (PivotGridField)field;
                string caption = string.Empty;

                switch (item.FieldName)
                {
                    case "Bl_Desc": caption = Resources.StatisticsLineOfBusiness; break;
                    case "Office_Desc": caption = Resources.StatisticsOffice; break;
                    case "Distribution_Desc": caption = Resources.StatisticsSalesChannel; break;
                    case "Agent_FullName_DirectorSenior": caption = Resources.SeniorDirector; break;
                    case "Agent_FullName_DirectorComercial": caption = Resources.CommercialDirector; break;
                    case "Agent_FullName_GerenteComercial": caption = Resources.CommercialManager; break;
                    case "Agent_FullName_Comercial": caption = Resources.BusinessAgent; break;
                    case "Agent_Name": caption = Resources.StatisticsAgent; break;
                    case "Product_Type_Desc": caption = Resources.Product; break;
                    case "Annual_Premium": caption = Resources.AnnualPremium; break;
                    case "Count": caption = Resources.Count; break;
                    case "Insured_Amount": caption = Resources.Insured_Amount; break;
                    case "Rate": caption = Resources.Rate; break;
                    case "Policy_No": caption = Resources.PolicyLabel; break;
                    case "Vehicle_Count": caption = Resources.VehicleCount; break;
                }

                item.Caption = caption;
            }
            #endregion
        }

        private void pivotGrid_FieldValueDisplayText(object sender, PivotFieldDisplayTextEventArgs e)
        {
            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal)            
                e.DisplayText = Resources.StatisticsGrandTotal;            

            if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.Total)            
                e.DisplayText = e.DisplayText.Replace("Total", "TOTAL");            
        }

        public void Translator(string Lang)
        {
            Utility.StatisticsEmisionesViewBy pivot = (Utility.StatisticsEmisionesViewBy)ViewState["pivot"];
            Period.Periods period = (Period.Periods)ViewState["period"];

            switch (pivot)
            {
                case Utility.StatisticsEmisionesViewBy.Count:
                    pnlEmisionesCantidad.Visible = true;
                    ConfigureGrid(pgEmisionesCantidad, period);
                    pgEmisionesCantidad.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.AnnualPremium:
                    pnlEmisionesPrima.Visible = true;
                    ConfigureGrid(pgEmisionesPrima, period);
                    pgEmisionesPrima.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.Insured_Amount:
                    pnlEmisionesSumaAsegurada.Visible = true;
                    ConfigureGrid(pgEmisionesSumaAsegurada, period);
                    pgEmisionesSumaAsegurada.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.Rate:
                    pnlEmisionesTasa.Visible = true;
                    ConfigureGrid(pgEmisionesTasa, period);
                    pgEmisionesTasa.DataBind();
                    break;
                case Utility.StatisticsEmisionesViewBy.VehicleCount:
                    pnlEmisionesCantidadVehiculos.Visible = true;
                    ConfigureGrid(pgEmisionesCantidadVehiculos, period);
                    pgEmisionesCantidadVehiculos.DataBind();
                    break;
            }
        }

        public void ReadOnlyControls(bool isReadOnly)
        {
            throw new NotImplementedException();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void edit()
        {
            throw new NotImplementedException();
        }

        public void FillData()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public void ClearData()
        {
            throw new NotImplementedException();
        }
    }
}