﻿using Entity.UnderWriting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WEB.ConfirmationCall.Common;
using WEB.ConfirmationCall.Infrastructure.Providers;

namespace WEB.ConfirmationCall.UserControls.ConfirmationCall
{
    public partial class UCNotes : UC
    {

        #region metodos

        public IEnumerable<Entity.UnderWriting.Entities.Step.Note> DataNotes
        {
            get
            {
                var data = Session["UCNotes.DataNotes"];
                return data != null ? (IEnumerable<Entity.UnderWriting.Entities.Step.Note>)Session["UCNotes.DataNotes"] : null;
            }
            set
            {
                Session["UCNotes.DataNotes"] = value;
            }
        }

        public object GetNotes(IEnumerable<Entity.UnderWriting.Entities.Step.Note> data)
        {
            return data;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (_services.Current_Contact_Id > 0)
                    {
                        //Session["ModificarAddres"] = "0";
                        FillNotes();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region LLenarDropbox

        public void CommentType()
        {
            var lista = _services.oDropDownManager.GetDropDownByType(new DropDown.Parameter
            {
                DropDownType = "ConfirmationCallNoteType",
                CorpId = _services.Corp_Id,
                CompanyId = UserDataProvider.CompanyId,
                ProjectId = UserDataProvider.ProjectId,
                LanguageId = UserDataProvider.LanguageId
            });
            DrpCommType.DataSource = lista.Where(o => o.NoteTypeId != 17);
            DrpCommType.DataTextField = "NoteTypeDesc";
            DrpCommType.DataValueField = "NoteTypeId";
            DrpCommType.DataBind();
        }

        #endregion


        #region MetodosdelGrid

        public void FillNotes()
        {
            //Utility.ClearAll(this.Controls);
            Session["noteId"] = -1;
            int? contactTypeId = _services.Current_Contact_Type_Id == WEB.ConfirmationCall.Common.ContactType.None ? null : (int?)_services.Current_Contact_Type_Id;
            //var ListaNotes = new UCNotes()._Inote.GetAll(1,1,28,3,1,1,62,1084,1).ToList();
            DataNotes = _services.oStepManager.GetAllNotes(new Entity.UnderWriting.Entities.Step
            {
                //Key
                CorpId = _services.Corp_Id,
                RegionId = _services.Region_Id,
                CountryId = _services.Country_Id,
                DomesticregId = _services.Domesticreg_Id,
                StateProvId = _services.State_Prov_Id,
                CityId = _services.City_Id,
                OfficeId = _services.Office_Id,
                CaseSeqNo = _services.Case_Seq_No,
                HistSeqNo = _services.Hist_Seq_No,
                ContactId = _services.Current_Contact_Id,
                ContactRoleTypeId = contactTypeId,
                StepCaseNo = _services.Step_Case_No,
                StepId = _services.Step_Id,
                StepTypeId = _services.Step_Type_Id,
                LanguageId = UserDataProvider.LanguageId

            });
            GrdNotes.DataBind();
            CommentType();
            TxtSubJect.Text = "";
            TxtDescripcion.Text = "";
            TxtDescripcionAnterior.Text = "";
            BtnAddNotes.Text = "Add";
            BtnAddNotesDiv.Attributes["class"] = "boton_wrapper amarillo float_right";
            BtnAddNotesSpan.Attributes["class"] = "add";
        }

        public void SaveNote()
        {
            int? contactTypeId = 0;
            try
            {
                if ((TxtSubJect.Text != "") && (TxtDescripcion.Text != ""))
                {
                    contactTypeId = _services.Current_Contact_Type_Id == WEB.ConfirmationCall.Common.ContactType.None ? null : (int?)_services.Current_Contact_Type_Id;
                    if (Session["noteId"].ToInt() == -1)
                    {
                        //Saving 
                        _services.oStepManager.InsertNote(new Entity.UnderWriting.Entities.Step.Note
                        {
                            //Key
                            CorpId = _services.Corp_Id,
                            RegionId = _services.Region_Id,
                            CountryId = _services.Country_Id,
                            DomesticregId = _services.Domesticreg_Id,
                            StateProvId = _services.State_Prov_Id,
                            CityId = _services.City_Id,
                            OfficeId = _services.Office_Id,
                            CaseSeqNo = _services.Case_Seq_No,
                            HistSeqNo = _services.Hist_Seq_No,
                            ContactId = _services.Current_Contact_Id,
                            ContactRoleTypeId = contactTypeId,
                            StepCaseNo = _services.Step_Case_No,
                            StepId = _services.Step_Id,
                            StepTypeId = _services.Step_Type_Id,
                            NoteTypeId = DrpCommType.SelectedItem.Value.ToInt(),
                            NoteName = TxtSubJect.Text,
                            NoteDesc = TxtDescripcion.Text,
                            OriginatedBy = UserDataProvider.UserId.ToInt(),
                            UserId = UserDataProvider.UserId.ToInt()
                        });


                        Alertify.Alert(RESOURCE.UnderWriting.ConfirmationCall.Resources.NoteSaveSucessfully, this);

                    }
                    else
                    {
                        //Updateding 
                        _services.oStepManager.UpdateNote(new Entity.UnderWriting.Entities.Step.Note
                        {
                            //Key
                            CorpId = _services.Corp_Id,
                            RegionId = _services.Region_Id,
                            CountryId = _services.Country_Id,
                            DomesticregId = _services.Domesticreg_Id,
                            StateProvId = _services.State_Prov_Id,
                            CityId = _services.City_Id,
                            OfficeId = _services.Office_Id,
                            CaseSeqNo = _services.Case_Seq_No,
                            HistSeqNo = _services.Hist_Seq_No,
                            ContactId = _services.Current_Contact_Id,
                            ContactRoleTypeId = contactTypeId,
                            StepCaseNo = _services.Step_Case_No,
                            StepId = _services.Step_Id,
                            StepTypeId = _services.Step_Type_Id,
                            NoteId = Session["noteId"].ToInt(),
                            NoteTypeId = DrpCommType.SelectedItem.Value.ToInt(),
                            NoteName = TxtSubJect.Text,
                            NoteDesc = TxtDescripcion.Text,
                            OriginatedBy = UserDataProvider.UserId.ToInt(),
                            UserId = UserDataProvider.UserId.ToInt()
                        });

                        Alertify.Alert(RESOURCE.UnderWriting.ConfirmationCall.Resources.NoteSaveSucessfully, this);
                    }

                    FillNotes();

                  
                }
                else
                {

                    Alertify.Alert(RESOURCE.UnderWriting.ConfirmationCall.Resources.ReqSubjetNoteAndDescripcion, this);

                }

            }
            catch (Exception ex)
            {

                string parameter = "CorpId:" + _services.Corp_Id + ",RegionId:" + _services.Region_Id + "CountryId:" + _services.Country_Id + ",DomesticregId:" + _services.Domesticreg_Id + ",StateProvId:" +
                    _services.State_Prov_Id + ",CityId:" + _services.City_Id + ",OfficeId:" + _services.Office_Id + ", CaseSeqNo:" + _services.Case_Seq_No + ",HistSeqNo:" + _services.Hist_Seq_No +
                    ",ContactId:" + _services.Current_Contact_Id + ",ContactRoleTypeId:" + contactTypeId + ",StepCaseNo" + _services.Step_Case_No + ",StepId:" + _services.Step_Id + ",StepTypeId:" +
                    _services.Step_Type_Id + ",NoteTypeId:" + DrpCommType.SelectedItem.Value.ToInt() + ",NoteName:" + TxtSubJect.Text + ",NoteDesc:" + TxtDescripcion.Text + ",OriginatedBy:" +
                    UserDataProvider.UserId.ToInt() + ",UserId:" + UserDataProvider.UserId.ToInt() + "";

                ConfirmationCallLog.Log("SaveNote", ex.Message, parameter, UserDataProvider.UserId.ToInt(), Request.ServerVariables["SERVER_NAME"].ToString());
            }

        }

        #endregion

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Translate();
        }

        void Translate()
        {

            Utility.TranslateColumnsAspxGrid(this.GrdNotes);
            //
            GrdNotes.SettingsText.EmptyDataRow = RESOURCE.UnderWriting.ConfirmationCall.Resources.RowEmpty;
            //
            BtnAddNotes.Text = RESOURCE.UnderWriting.ConfirmationCall.Resources.Add;
            BtnCancelNotes.Text = RESOURCE.UnderWriting.ConfirmationCall.Resources.Cancel;
        }


        protected void GrdNotes_RowCommand1(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            try
            {
                Session["noteId"] = e.KeyValue.ToString().Split('|')[1];
                Session["directoryDetailId"] = e.KeyValue.ToString().Split('|')[2];
                switch (((Button)e.CommandSource).CommandName)
                {
                    case "Edit":
                        string SourceSystem = (string.IsNullOrWhiteSpace(e.KeyValue.ToString().Split('|')[5]) ? "" : e.KeyValue.ToString().Split('|')[5]);

                        if (SourceSystem == "Underwriting" || SourceSystem == "DataReview" || SourceSystem == " ")
                        {
                            BtnAddNotesDiv.Visible = false;
                        }
                        else
                        {
                            BtnAddNotesDiv.Visible = true;
                        }

                        TxtSubJect.Text = e.KeyValue.ToString().Split('|')[2].ToString();
                        TxtDescripcion.Text = "";
                        TxtDescripcionAnterior.Text = e.KeyValue.ToString().Split('|')[3].ToString();
                        DrpCommType.SelectedValue = e.KeyValue.ToString().Split('|')[4].ToString();
                        BtnAddNotes.Text = "Save";
                        BtnAddNotesDiv.Attributes["class"] = "boton_wrapper verde float_right";
                        BtnAddNotesSpan.Attributes["class"] = "save";
                        break;
                    case "Delete":
                        //EliminaNote(int.Parse(UserDataProvider.CorpId), int.Parse(Session["directoryId"].ToString()), int.Parse(Session["directoryDetailId"].ToString()));
                        break;

                    default:
                        Session["ModificarNote"] = "0";
                        break;
                }
            }

            catch (Exception ex)
            {


            }

        }

        #region Events

        protected void dsNotes_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["data"] = DataNotes;
        }

        protected void GrdNotes_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GrdNotes.FocusedRowIndex = -1;
        }

        protected void GrdNotes_PageIndexChanged(object sender, EventArgs e)
        {
            GrdNotes.FocusedRowIndex = -1;
        }

        protected void GrdNotes_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            GrdNotes.FocusedRowIndex = -1;
        }
        #endregion

        protected void BtnCancelNotes_Click(object sender, EventArgs e)
        {
            BtnAddNotesDiv.Visible = true;
            FillNotes();
        }

        protected void BtnAddNotes_Click(object sender, EventArgs e)
        {
            SaveNote();
        }
    }
}