﻿using System.Collections.Generic;
using System.Linq;
using DATA.UnderWriting.Data;
using DATA.UnderWriting.Repositories.Base;
using Entity.UnderWriting.Entities;

namespace DATA.UnderWriting.Repositories.Global
{
    public class StepRepository : GlobalRepository
    {
        public StepRepository(GlobalEntityDataModel globalModel, GlobalEntities globalModelExtended) : base(globalModel, globalModelExtended) { }

        public virtual IEnumerable<Step> GetAll(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId)
        {
            IEnumerable<Step> result;
            IEnumerable<SP_GET_UW_STEPS_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, stepTypeId);

            result = temp
                .Select(s => new Step
                {
                    CorpId = s.Corp_Id,
                    RegionId = s.Region_Id,
                    CountryId = s.Country_Id,
                    DomesticregId = s.Domesticreg_Id,
                    StateProvId = s.State_Prov_Id,
                    CityId = s.City_Id,
                    OfficeId = s.Office_Id,
                    CaseSeqNo = s.Case_Seq_No,
                    HistSeqNo = s.Hist_Seq_No,
                    StepTypeId = s.Step_Type_Id,
                    StepId = s.Step_Id,
                    StepCaseNo = s.Step_Case_No,
                    ProcessId = s.Process_Id,
                    StatusTypeId = s.Status_Type_Id,
                    StatusId = s.Status_Id,
                    CommunicationTypeId = s.Communication_Type_Id,
                    CallId = s.Call_Id,
                    Initiated = s.Initiated,
                    Completed = s.Completed,
                    Current = s.Current,
                    Standard = s.Standard,
                    CurrentIsRed = s.CurrentIsRed,
                    ProcessStatus = s.ProcessStatus.ConvertToNoNullable(1),
                    StatusCatalogDesc = s.Status_Catalog_Desc,
                    StatusTypeDesc = s.Status_Type_Desc,
                    DateSent = s.Date_Sent,
                    CallregNotehistoryid = s.Callreg_Notehistoryid,
                    HistoryId = s.History_Id,
                    StartDate = s.Start_Date,
                    EndDate = s.End_Date,
                    Duration = s.Duration,
                    RecordingFile = s.Recording_File,
                    StepDesc = s.Step_Desc,
                    StepCode = s.Step_Code,
                    StepTypeDesc = s.Step_Type_Desc,
                    HasCall = s.HasCall.ConvertToNoNullable(),
                    NoteDesc = s.Note_Desc,
                    Closable = s.Closable.ConvertToNoNullable(),
                    Voidable = s.Voidable.ConvertToNoNullable(),
                    UserId = s.UserId,
                    UserName = s.UserName
                })
                .ToArray();

            return
                result;
        }
        
        public virtual IEnumerable<Step.Note> GetAllNotes(Step step)
        {
            IEnumerable<Step.Note> result;
            IEnumerable<SP_GET_UW_STEPS_NOTES_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS_NOTES(
                    step.CorpId,
                    step.RegionId,
                    step.CountryId,
                    step.DomesticregId,
                    step.StateProvId,
                    step.CityId,
                    step.OfficeId,
                    step.CaseSeqNo,
                    step.HistSeqNo,
                    step.StepTypeId,
                    step.StepId,
                    step.StepCaseNo,
                    step.ContactId,
                    step.ContactRoleTypeId,
                    step.LanguageId
                );

            result = temp
                .Select(sn => new Step.Note
                {
                    CorpId = sn.Corp_Id,
                    RegionId = sn.Region_Id,
                    CountryId = sn.Country_Id,
                    DomesticregId = sn.Domesticreg_Id,
                    StateProvId = sn.State_Prov_Id,
                    CityId = sn.City_Id,
                    OfficeId = sn.Office_Id,
                    CaseSeqNo = sn.Case_Seq_No,
                    HistSeqNo = sn.Hist_Seq_No,
                    StepTypeId = sn.Step_Type_Id,
                    StepId = sn.Step_Id,
                    StepCaseNo = sn.Step_Case_No,
                    NoteId = sn.Note_Id,
                    ContactId = sn.Contact_Id,
                    ContactRoleTypeId = sn.Contact_Role_Type_Id,
                    NoteTypeId = sn.Note_Type_Id,
                    ReferenceId = sn.Reference_Id,
                    NoteName = sn.Note_Name,
                    DateAdded = sn.Date_Added,
                    DateModified = sn.Date_Modified,
                    OriginatedBy = sn.Originated_By,
                    OriginatedByName = sn.Originated_By_Name,
                    NoteDesc = sn.Note_Desc,
                    UnderwriterId = sn.Underwriter_Id,
                    UnderwriterName = sn.UnderwriterName,
                    SourceSystem = sn.SourceSystem,
                    NoteTypeDesc = sn.Note_Type_Desc
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Step.Workflow> GetWorkflow(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo)
        {
            IEnumerable<Step.Workflow> result;
            IEnumerable<SP_GET_UW_STEPS_WORKFLOW_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS_WORKFLOW(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo);
            
            result = temp
                .Select(w => new Step.Workflow
                {
                    CorpId = w.Corp_Id.ConvertToNoNullable(),
                    RegionId = w.Region_Id.ConvertToNoNullable(),
                    CountryId = w.Country_Id.ConvertToNoNullable(),
                    DomesticregId = w.Domesticreg_Id.ConvertToNoNullable(),
                    StateProvId = w.State_Prov_Id.ConvertToNoNullable(),
                    CityId = w.City_Id.ConvertToNoNullable(),
                    OfficeId = w.Office_Id.ConvertToNoNullable(),
                    CaseSeqNo = w.Case_Seq_No.ConvertToNoNullable(),
                    HistSeqNo = w.Hist_Seq_No.ConvertToNoNullable(),
                    StepTypeId = w.Step_Type_Id,
                    StepId = w.Step_Id,
                    StepCaseNo = w.Step_Case_No,
                    Opened = w.Opened,
                    Closed = w.Closed,
                    ProcessStatus = w.ProcessStatus.ConvertToNoNullable(),
                    StepDesc = w.Step_Desc,
                    Stage = w.Stage.ConvertToNoNullable(),
                    Order = w.Order.ConvertToNoNullable(),
                    WasSentToReinsurance = w.WasSenttoReinsurance.ConvertToNoNullable()
                })
                .ToArray();

            return
                result;
        }

        public virtual IEnumerable<Policy.Call> GetAllCall(Step step)
        {
            IEnumerable<Policy.Call> result;
            IEnumerable<SP_GET_UW_STEPS_CALL_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS_CALL(
                step.CorpId,
                step.RegionId,
                step.CountryId,
                step.DomesticregId,
                step.StateProvId,
                step.CityId,
                step.OfficeId,
                step.CaseSeqNo,
                step.HistSeqNo,
                step.StepTypeId,
                step.StepId,
                step.StepCaseNo
                );

            result = temp
                .Select(c => new Policy.Call
                {
                    CorpId = c.Corp_Id,
                    RegionId = c.Region_Id,
                    CountryId = c.Country_Id,
                    DomesticregId = c.Domesticreg_Id,
                    StateProvId = c.State_Prov_Id,
                    CityId = c.City_Id,
                    OfficeId = c.Office_Id,
                    CaseSeqNo = c.Case_Seq_No,
                    HistSeqNo = c.Hist_Seq_No,
                    CommunicationTypeId = c.Communication_Type_Id,
                    CallId = c.Call_Id,
                    StepTypeId = c.Step_Type_Id,
                    StepId = c.Step_Id,
                    StepCaseNo = c.Step_Case_No,
                    ContactId = c.Contact_Id,
                    ContactRoleTypeId = c.Contact_Role_Type_Id,
                    OriginatedByName = c.OriginatedByName,
                    StartDate = c.Start_Date,
                    EndDate = c.End_Date,
                    Duration = c.Duration,
                    RecordingFile = c.Recording_File,
                    OriginatedBy = c.Originated_By,
                    CallerIdNumber = c.CallerId_Number,
                    CallerIdName = c.CallerId_Name,
                    OutboundNumber = c.Outbound_Number
                })
                .ToArray();

            return
                result;
        }
        public virtual int DeleteCall(Policy.Call call)
        {
            int result;
            IEnumerable<SP_DELETE_UW_STEPS_CALL_Result> temp;

            temp = globalModel.SP_DELETE_UW_STEPS_CALL(
                    call.CorpId,
                    call.RegionId,
                    call.CountryId,
                    call.DomesticregId,
                    call.StateProvId,
                    call.CityId,
                    call.OfficeId,
                    call.CaseSeqNo,
                    call.HistSeqNo,
                    call.CommunicationTypeId,
                    call.CallId,
                    call.StepTypeId,
                    call.StepId,
                    call.StepCaseNo,
                    call.UserId
                );
            result = -1;

            return
                result;
        }

        public virtual int InsertStepComment(Step step)
        {
            int result;
            IEnumerable<SP_SET_UW_STEPS_UNDERWRITING_CALL_COMMENTS_Result> temp;

            temp = globalModel.SP_SET_UW_STEPS_UNDERWRITING_CALL_COMMENTS(
                    step.CorpId,
                    step.RegionId,
                    step.CountryId,
                    step.DomesticregId,
                    step.StateProvId,
                    step.CityId,
                    step.OfficeId,
                    step.CaseSeqNo,
                    step.HistSeqNo,
                    step.StepTypeId,
                    step.StepId,
                    step.StepCaseNo,
                    step.UserId
                );
            result = -1;

            return
                result;
        }

        public virtual int Set(Step.NewStep newStep)
        {
            int result;
            IEnumerable<SP_SET_UW_STEPS_Result> temp;

            temp = globalModel.SP_SET_UW_STEPS(
                    newStep.CorpId,
                    newStep.RegionId,
                    newStep.CountryId,
                    newStep.DomesticregId,
                    newStep.StateProvId,
                    newStep.CityId,
                    newStep.OfficeId,
                    newStep.CaseSeqNo,
                    newStep.HistSeqNo,
                    newStep.StepTypeId,
                    newStep.StepId,
                    newStep.StepCaseNo,
                    newStep.ProcessDate,
                    newStep.Note,
                    newStep.Automatic,
                    newStep.UserId,
                    true
                )
                .ToArray();

            result = temp != null && temp.Any()
                ? temp.First().Step_Case_No.ConvertToNoNullable()
                : -1;

            return
                result;
        }
        public virtual int SetProcess(Step.Process process)
        {
            int result;
            IEnumerable<SP_SET_UW_STEPS_PROCESSES_Result> temp;

            temp = globalModel.SP_SET_UW_STEPS_PROCESSES(
                        process.CorpId,
                        process.RegionId,
                        process.CountryId,
                        process.DomesticregId,
                        process.StateProvId,
                        process.CityId,
                        process.OfficeId,
                        process.CaseSeqNo,
                        process.HistSeqNo,
                        process.StepTypeId,
                        process.StepId,
                        process.StepCaseNo,
                        process.ProcessId,
                        process.StatusTypeId,
                        process.StatusId,
                        process.ProcessDate,
                        process.UserId
                );
            result = -1;

            return
                result;
        }
        public virtual int SetNote(Step.Note note)
        {
            int result;
            IEnumerable<SP_INSERT_UW_STEPS_NOTES_Result> temp;

            temp = globalModel.SP_INSERT_UW_STEPS_NOTES(
                        note.CorpId,
                        note.RegionId,
                        note.CountryId,
                        note.DomesticregId,
                        note.StateProvId,
                        note.CityId,
                        note.OfficeId,
                        note.CaseSeqNo,
                        note.HistSeqNo,
                        note.StepTypeId,
                        note.StepId,
                        note.StepCaseNo,
                        note.NoteId,
                        note.ContactId,
                        note.ContactRoleTypeId,
                        note.NoteTypeId,
                        note.ReferenceId,
                        note.NoteName,
                        note.DateAdded,
                        note.DateModified,
                        note.OriginatedBy,
                        note.NoteDesc,
                        note.UnderwriterId,
                        note.UserId
                );
            result = -1;

            return
                result;
        }

        public virtual IEnumerable<Step.ExtraInfo> GetStepExtraInfo(Policy.Parameter policyParameter)
        {
            IEnumerable<Step.ExtraInfo> result;
            IEnumerable<SP_GET_UW_STEPS_EXTRA_INFO_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS_EXTRA_INFO(
                    policyParameter.CorpId,
                    policyParameter.RegionId,
                    policyParameter.CountryId,
                    policyParameter.DomesticregId,
                    policyParameter.StateProvId,
                    policyParameter.CityId,
                    policyParameter.OfficeId,
                    policyParameter.CaseSeqNo,
                    policyParameter.HistSeqNo
                );

            result = temp
                .Select(ei => new Step.ExtraInfo
                {
                    CorpId = ei.Corp_Id,
                    RegionId = ei.Region_Id,
                    CountryId = ei.Country_Id,
                    DomesticRegId = ei.Domesticreg_Id,
                    StateProvId = ei.State_Prov_Id,
                    CityId = ei.City_Id,
                    OfficeId = ei.Office_Id,
                    CaseSeqNo = ei.Case_Seq_No,
                    HistSeqNo = ei.Hist_Seq_No,
                    StepTypeId = ei.Step_Type_Id,
                    StepId = ei.Step_Id,
                    StepCaseNo = ei.Step_Case_No,
                    StepExtraInfoTypeId = ei.Step_Extra_Info_Type_Id,
                    StepExtraInfoTypeDesc = ei.Step_Extra_Info_Type_Desc,
                    StepExtraInfoId = ei.Step_Extra_Info_Id,
                    DocTypeId = ei.Doc_Type_Id,
                    DocCategoryId = ei.Doc_Category_Id,
                    DocCategoryDesc = ei.Doc_Category_Desc,
                    DocumentId = ei.Document_Id,
                    DocumentDesc = ei.Document_Desc,
                    DocumentName = ei.Document_Name,
                    FileCreationDate = ei.File_Creation_Date,
                    StepExtraInfoDesc = ei.Step_Extra_Info_Desc,
                    StepExtraInfoStatusId = ei.Step_Extra_Info_Status_Id,
                    StepExtraInfoStatusDesc = ei.Step_Extra_Info_Status_Desc
                })
                .ToArray();

            return
                result;
        }
        public virtual int SetExtraInfo(Step.ExtraInfo extraInfo)
        {
            int result;
            IEnumerable<SP_SET_UW_STEPS_EXTRA_INFO_Result> temp;

            temp = globalModel.SP_SET_UW_STEPS_EXTRA_INFO(
                        extraInfo.CorpId,
                        extraInfo.RegionId,
                        extraInfo.CountryId,
                        extraInfo.DomesticRegId,
                        extraInfo.StateProvId,
                        extraInfo.CityId,
                        extraInfo.OfficeId,
                        extraInfo.CaseSeqNo,
                        extraInfo.HistSeqNo,
                        extraInfo.StepTypeId,
                        extraInfo.StepId,
                        extraInfo.StepCaseNo,
                        extraInfo.StepExtraInfoTypeId,
                        extraInfo.StepExtraInfoId,
                        extraInfo.DocTypeId,
                        extraInfo.DocCategoryId,
                        extraInfo.DocumentId,
                        extraInfo.StepExtraInfoDesc,
                        extraInfo.StepExtraInfoStatusId,
                        extraInfo.UserId
                );
            result = -1;

            return
                result;
        }

        public virtual IEnumerable<Step> GetAll_byStepId(int coprId, int regionId, int countryId, int domesticRegId, int stateProvId, int cityId
          , int officeId, int caseSeqNo, int histSeqNo, int stepTypeId, int stepId)
        {
            IEnumerable<Step> result;
            IEnumerable<SP_GET_UW_STEPS_ID_Result> temp;

            temp = globalModel.SP_GET_UW_STEPS_ID(coprId, regionId, countryId, domesticRegId, stateProvId, cityId, officeId, caseSeqNo, histSeqNo, stepTypeId, stepId);

            result = temp
                .Select(s => new Step
                {
                    CorpId = s.Corp_Id,
                    RegionId = s.Region_Id,
                    CountryId = s.Country_Id,
                    DomesticregId = s.Domesticreg_Id,
                    StateProvId = s.State_Prov_Id,
                    CityId = s.City_Id,
                    OfficeId = s.Office_Id,
                    CaseSeqNo = s.Case_Seq_No,
                    HistSeqNo = s.Hist_Seq_No,
                    StepTypeId = s.Step_Type_Id,
                    StepId = s.Step_Id,
                    StepCaseNo = s.Step_Case_No,
                    ProcessId = s.Process_Id,
                    StatusTypeId = s.Status_Type_Id,
                    StatusId = s.Status_Id,
                    CommunicationTypeId = s.Communication_Type_Id,
                    CallId = s.Call_Id,
                    Initiated = s.Initiated,
                    Completed = s.Completed,
                    Current = s.Current,
                    Standard = s.Standard,
                    CurrentIsRed = s.CurrentIsRed,
                    ProcessStatus = s.ProcessStatus.ConvertToNoNullable(1),
                    StatusCatalogDesc = s.Status_Catalog_Desc,
                    StatusTypeDesc = s.Status_Type_Desc,
                    DateSent = s.Date_Sent,
                    CallregNotehistoryid = s.Callreg_Notehistoryid,
                    HistoryId = s.History_Id,
                    StartDate = s.Start_Date,
                    EndDate = s.End_Date,
                    Duration = s.Duration,
                    RecordingFile = s.Recording_File,
                    StepDesc = s.Step_Desc,
                    StepCode = s.Step_Code,
                    StepTypeDesc = s.Step_Type_Desc,
                    HasCall = s.HasCall.ConvertToNoNullable(),
                    NoteDesc = s.Note_Desc,
                    Closable = s.Closable.ConvertToNoNullable(),
                    Voidable = s.Voidable.ConvertToNoNullable(),
                    UserId = s.UserId,
                    UserName = s.UserName
                })
                .ToArray();

            return
                result;
        }

    }
}
