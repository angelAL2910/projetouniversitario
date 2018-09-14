﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using WEB.UnderWriting.Common;
using Entity.UnderWriting.Entities;

namespace WEB.UnderWriting.Case.UserControls.PersonalData
{
    public partial class BackgroundCheckLink :   UC, IUC
    {
        //Lazy<BackgroundCheck.Logic.MATCH_LINKS.BussinesObject> obj_MATCH_LINKS = new Lazy<Logic.MATCH_LINKS.BussinesObject>();
        //public UCCaseInformations UCCaseInformations { get; set; }

        void IUC.Translator(string Lang)
        {
            throw new NotImplementedException();
        }

        void IUC.edit()
        {
            throw new NotImplementedException();
        }

        void IUC.FillData() {
            throw new NotImplementedException();
        }

        void IUC.clearData()
        {
            throw new NotImplementedException();
        }

        void IUC.save()
        {
            throw new NotImplementedException();
        }

        void IUC.readOnly(bool x)
        {
            throw new NotImplementedException();
        }
         

        private int PageSize
        {
            get { return Session["PageSizeInter"] == null ? 4 : (int)Session["PageSizeInter"]; }
            set { Session["PageSizeInter"] = value; }
        }

        private int ActivePageIndex
        {
            get { return Session["ActivePageIndex"] == null ? 4 : (int)Session["ActivePageIndex"]; }
            set { Session["ActivePageIndex"] = value; }
        }

        private List<Policy.BackGroundCheckLink> Match_Links
        {
            get { return Session["Match_Links"] == null ? new List<Policy.BackGroundCheckLink>() : (List<Policy.BackGroundCheckLink>)Session["Match_Links"]; }
            set { Session["Match_Links"] = value; }
        }

        private int CASE_HISTORICAL_ID
        {
            get { return Session["CASE_HISTORICAL_ID"] == null ? 0 : int.Parse(Session["CASE_HISTORICAL_ID"].ToString()); }
            set { Session["CASE_HISTORICAL_ID"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            int contactid = Service.Contact_Id;
            var SummaryRoleDDL = (DropDownList)Page.Master.FindControl("Left").FindControl("Left").FindControl("SummaryRoleDDL");

            if (SummaryRoleDDL.Items.Count > 0)
            {
                contactid = int.Parse(SummaryRoleDDL.SelectedValue.Split('|')[0]);
            }

            FillGrid(contactid);
        } 

        public void FillGrid(int contactid)
        { 

            div_pag.Visible = true;

            var Parameters = new Policy.BackGroundCheckLink
            {
                Corp_Id = Service.Corp_Id,
                Region_Id = Service.Region_Id,
                Country_Id = Service.Country_Id,
                Domesticreg_Id = Service.Domesticreg_Id,
                State_Prov_Id = Service.State_Prov_Id,
                City_Id = Service.City_Id,
                Office_Id = Service.Office_Id,
                Case_Seq_No = Service.Case_Seq_No,
                Hist_Seq_No = Service.Hist_Seq_No,
                Contact_Id = contactid 
            };

            var Match_Links_local = Services.PolicyManager.Bg_Get_Match_Links(Parameters).ToList();

            Match_Links = Match_Links_local;
            
            Match_Links_local = Match_Links_local.Where(r => r.Contact_Id == Parameters.Contact_Id).ToList();

            var c = (PageSize * (ActivePageIndex - 1)) + 1;

            ActivePageIndex = 1;

            //if (ActivePageIndex == 0)
            //{
            //    ActivePageIndex = 1;
            //}

            if (Match_Links_local.Count < c)
                ActivePageIndex = ActivePageIndex - 1;

            if (Match_Links_local.Any())
            {
                var Result = MemoryListPager(Match_Links_local, PageSize, ActivePageIndex);
                gv_InternetImages.DataSource = Result;
                gv_InternetImages.DataBind();
            }
            else
            {
                gv_InternetImages.DataSource = null;
                gv_InternetImages.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int contactid = Service.Contact_Id;
            var SummaryRoleDDL = (DropDownList)Page.Master.FindControl("Left").FindControl("Left").FindControl("SummaryRoleDDL");

            if (SummaryRoleDDL.Items.Count > 0)
            {
                contactid = int.Parse(SummaryRoleDDL.SelectedValue.Split('|')[0]);
            }

            var data = Services.PolicyManager.Bg_Get_Match_Links(new Policy.BackGroundCheckLink
            {
                Corp_Id = Service.Corp_Id,
                Region_Id = Service.Region_Id,
                Country_Id = Service.Country_Id,
                Domesticreg_Id = Service.Domesticreg_Id,
                State_Prov_Id = Service.State_Prov_Id,
                City_Id = Service.City_Id,
                Office_Id = Service.Office_Id,
                Case_Seq_No = Service.Case_Seq_No,
                Hist_Seq_No = Service.Hist_Seq_No,
                Contact_Id = contactid
            }).ToList().Where(x => x.Link_Url == txtURL.Text);
            
            if (data.Any())
            {
                var msj = "This URL is already on the list, please try with another one.";
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "CustomDialogMessageEx('" + msj + "', 500, 150, true, 'URL already added');", true);
                return;
            } 

            string Operation = EnumHelper.GetDescription(Tools.SettingOperations.Insert);
            Services.PolicyManager.Bg_Set_Match_Links(new Policy.BackGroundCheckLink
            {
                Operation = Operation,
                Corp_Id = Service.Corp_Id,
                Region_Id = Service.Region_Id,
                Country_Id = Service.Country_Id,
                Domesticreg_Id = Service.Domesticreg_Id,
                State_Prov_Id = Service.State_Prov_Id,
                City_Id = Service.City_Id,
                Office_Id = Service.Office_Id,
                Case_Seq_No = Service.Case_Seq_No,
                Hist_Seq_No = Service.Hist_Seq_No,
                Contact_Id = contactid,
                Link_Url = txtURL.Text,
                userid = Service.Underwriter_Id
            }); 

            txtURL.Text = string.Empty;
            FillGrid(contactid);
        }
          
        protected void lnk_DelDoc_Click(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
            var cORP_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cORP_ID"].ToString());
            var rEGION_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["rEGION_ID"].ToString());
            var cOUNTRY_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cOUNTRY_ID"].ToString());
            var dOMESTICREG_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["dOMESTICREG_ID"].ToString());
            var sTATE_PROV_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["sTATE_PROV_ID"].ToString());
            var cITY_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cITY_ID"].ToString());
            var oFFICE_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["oFFICE_ID"].ToString());
            var cASE_SEQ_NO = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cASE_SEQ_NO"].ToString());
            var hIST_SEQ_NO = int.Parse(gv_InternetImages.DataKeys[rowIndex]["hIST_SEQ_NO"].ToString());            
            var cONTACT_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cONTACT_ID"].ToString());
            var lINK_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["lINK_ID"].ToString());

           string Operation = EnumHelper.GetDescription(Tools.SettingOperations.Delete);

            Services.PolicyManager.Bg_Set_Match_Links(new Policy.BackGroundCheckLink
            {
                Operation = Operation,
                Corp_Id = cORP_ID,
                Region_Id = rEGION_ID,
                Country_Id = cOUNTRY_ID,
                Domesticreg_Id = dOMESTICREG_ID,
                State_Prov_Id = sTATE_PROV_ID,
                City_Id = cITY_ID,
                Office_Id = oFFICE_ID,
                Case_Seq_No = cASE_SEQ_NO,
                Hist_Seq_No = hIST_SEQ_NO,
                Contact_Id = cONTACT_ID,
                Link_Id = lINK_ID,
                userid = Service.Underwriter_Id
            });

            FillGrid(cONTACT_ID); 
        }

        public IEnumerable<Policy.BackGroundCheckLink> MemoryListPager(List<Policy.BackGroundCheckLink> Files, int Size, int currentPage)
        {
            int numberOfPages = (Files.Count / PageSize) + (Files.Count % PageSize == 0 ? 0 : 1);
            //Las listas son base 0 Por tal razon le restamos 1
            int pageIndex = (currentPage - 1) * Size;
            Session["LastPage"] = numberOfPages;
            IEnumerable<Policy.BackGroundCheckLink> itemsOnThisPage;
            lt_FirstPage.Text = Convert.ToString(currentPage);
            lt_lastPage.Text = Convert.ToString(numberOfPages);
            lt_items.Text = Convert.ToString(Files.Count);
            if (numberOfPages == 1 || numberOfPages == 0)
            {
                lt_FirstPage.Text = "1";
                lt_lastPage.Text = "1";
                lnk_next.Enabled = false;
                lnk_GoLast.Enabled = false;
                lnk_next.CssClass = "next_dis";
                lnk_GoLast.CssClass = "fwrd_dis";
                lnk_GoFirst.Enabled = false;
                lnk_prev.Enabled = false;
                lnk_GoFirst.CssClass = "rewd_dis";
                lnk_prev.CssClass = "prev_dis";
                if (numberOfPages == 0)
                {
                    div_pag.Visible = false;
                }
                else
                { div_pag.Visible = true; }
            }
            else if (numberOfPages > 1 & numberOfPages > currentPage)
            {
                div_pag.Visible = true;
                lnk_next.Enabled = true;
                lnk_GoLast.Enabled = true;
                lnk_next.CssClass = "next";
                lnk_GoLast.CssClass = "fwrd";
            }

            itemsOnThisPage = Files.Skip(pageIndex).Take(Size);
            ActivePageIndex = currentPage;

            return itemsOnThisPage;
        }

        protected void lnk_GoFirst_Click(object sender, EventArgs e)
        {
            lnk_GoFirst.Enabled = false;
            lnk_prev.Enabled = false;
            lnk_GoFirst.CssClass = "rewd_dis";
            lnk_prev.CssClass = "prev_dis";
            lnk_next.Enabled = true;
            lnk_GoLast.Enabled = true;
            lnk_next.CssClass = "next";
            lnk_GoLast.CssClass = "fwrd";
            var Result = MemoryListPager(Match_Links, PageSize, 1);
            gv_InternetImages.DataSource = Result;
            gv_InternetImages.DataBind();
        }

        protected void lnk_prev_Click(object sender, EventArgs e)
        {
            int Page = int.Parse(lt_FirstPage.Text);



            int page2 = Page - 1;
            if (page2 == 1)
            {
                lnk_GoFirst.Enabled = false;
                lnk_prev.Enabled = false;
                lnk_GoFirst.CssClass = "rewd_dis";
                lnk_prev.CssClass = "prev_dis";
                lnk_next.Enabled = true;
                lnk_GoLast.Enabled = true;
                lnk_next.CssClass = "next";
                lnk_GoLast.CssClass = "fwrd";
                var Result = MemoryListPager(Match_Links, PageSize, page2);
                gv_InternetImages.DataSource = Result;
                gv_InternetImages.DataBind();
            }
            else
            {
                lnk_next.Enabled = true;
                lnk_GoLast.Enabled = true;
                lnk_next.CssClass = "next";
                lnk_GoLast.CssClass = "fwrd";
                lnk_GoFirst.Enabled = true;
                lnk_prev.Enabled = true;
                lnk_GoFirst.CssClass = "rewd";
                lnk_prev.CssClass = "prev";
                var Result = MemoryListPager(Match_Links, PageSize, page2);
                gv_InternetImages.DataSource = Result;
                gv_InternetImages.DataBind();
            }



        }

        protected void lnk_next_Click(object sender, EventArgs e)
        {

            int Page = int.Parse(lt_FirstPage.Text);
            int page2 = Page + 1;
            int LastPage = int.Parse(Session["LastPage"].ToString());

            if (page2 == LastPage)
            {
                lnk_next.Enabled = false;
                lnk_GoLast.Enabled = false;
                lnk_next.CssClass = "next_dis";
                lnk_GoLast.CssClass = "fwrd_dis";
                lnk_GoFirst.Enabled = true;
                lnk_prev.Enabled = true;
                lnk_GoFirst.CssClass = "rewd";
                lnk_prev.CssClass = "prev";
                var Result = MemoryListPager(Match_Links, PageSize, page2);
                gv_InternetImages.DataSource = Result;
                gv_InternetImages.DataBind();
            }
            else
            {
                lnk_next.Enabled = true;
                lnk_GoLast.Enabled = true;
                lnk_next.CssClass = "next";
                lnk_GoLast.CssClass = "fwrd";
                lnk_GoFirst.Enabled = true;
                lnk_prev.Enabled = true;
                lnk_GoFirst.CssClass = "rewd";
                lnk_prev.CssClass = "prev";

                var Result = MemoryListPager(Match_Links, PageSize, page2);
                gv_InternetImages.DataSource = Result;
                gv_InternetImages.DataBind();
            }
        }

        protected void lnk_GoLast_Click(object sender, EventArgs e)
        {
            // lnk_next.Enabled = false;
            lnk_GoLast.Enabled = false;
            lnk_next.CssClass = "next_dis";
            lnk_GoLast.CssClass = "fwrd_dis";
            lnk_GoFirst.Enabled = true;
            lnk_prev.Enabled = true;
            lnk_GoFirst.CssClass = "rewd";
            lnk_prev.CssClass = "prev";
            int LastPage = int.Parse(Session["LastPage"].ToString());
            var Result = MemoryListPager(Match_Links, PageSize, LastPage);
            gv_InternetImages.DataSource = Result;
            gv_InternetImages.DataBind();
        }

        protected void imgStatus_Click(object sender, EventArgs e)
        {
            var rowIndex = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            var cORP_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cORP_ID"].ToString());
            var rEGION_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["rEGION_ID"].ToString());
            var cOUNTRY_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cOUNTRY_ID"].ToString());
            var dOMESTICREG_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["dOMESTICREG_ID"].ToString());
            var sTATE_PROV_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["sTATE_PROV_ID"].ToString());
            var cITY_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cITY_ID"].ToString());
            var oFFICE_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["oFFICE_ID"].ToString());
            var cASE_SEQ_NO = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cASE_SEQ_NO"].ToString());
            var hIST_SEQ_NO = int.Parse(gv_InternetImages.DataKeys[rowIndex]["hIST_SEQ_NO"].ToString());
            var cONTACT_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["cONTACT_ID"].ToString());
            var lINK_ID = int.Parse(gv_InternetImages.DataKeys[rowIndex]["lINK_ID"].ToString());
            var lINK_URL = gv_InternetImages.DataKeys[rowIndex]["lINK_URL"].ToString();
            var Matched = !Boolean.Parse(gv_InternetImages.DataKeys[rowIndex]["Matched"].ToString());

            string Operation = EnumHelper.GetDescription(Tools.SettingOperations.Update);

            Services.PolicyManager.Bg_Set_Match_Links(new Policy.BackGroundCheckLink
            {
                Operation = Operation,
                Corp_Id = cORP_ID,
                Region_Id = rEGION_ID,
                Country_Id = cOUNTRY_ID,
                Domesticreg_Id = dOMESTICREG_ID,
                State_Prov_Id = sTATE_PROV_ID,
                City_Id = cITY_ID,
                Office_Id = oFFICE_ID,
                Case_Seq_No = cASE_SEQ_NO,
                Hist_Seq_No = hIST_SEQ_NO,
                Contact_Id = cONTACT_ID,
                Link_Id = lINK_ID,
                Link_Url = lINK_URL,
                Matched = Matched,
                userid = Service.Underwriter_Id
            });

            FillGrid(cONTACT_ID);
        } 
         
    }
}