﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DReviewContainer.ascx.cs" Inherits="WEB.NewBusiness.DReview.UserControl.DReview.DReviewContainer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCPersonalInformation.ascx" TagPrefix="uc1" TagName="WUCPersonalInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCAddress.ascx" TagPrefix="uc1" TagName="WUCAddress" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCPhoneNumber.ascx" TagPrefix="uc1" TagName="WUCPhoneNumber" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCEmailAddress.ascx" TagPrefix="uc1" TagName="WUCEmailAddress" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCBackgroundInformation.ascx" TagPrefix="uc1" TagName="WUCBackgroundInformation" %>
<%@ Register Src="~/DReview/UserControl/PlanPolicyContainer.ascx" TagPrefix="uc1" TagName="PlanPolicyContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCIdentification.ascx" TagPrefix="uc1" TagName="WUCIdentification" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/HealthDeclaration/HealthDeclarationContainer.ascx" TagPrefix="uc1" TagName="HealthDeclarationContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Requirements/RequirementsContainer.ascx" TagPrefix="uc1" TagName="RequirementsContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/PaymentContainer.ascx" TagPrefix="uc1" TagName="PaymentContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Beneficiaries/BeneficiariesContainer.ascx" TagPrefix="uc1" TagName="BeneficiariesContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/UCNotesComments.ascx" TagPrefix="uc1" TagName="UCNotesComments" %>
<%@ Register Src="~/DReview/UserControl/DReview/WUCCompareEdit.ascx" TagPrefix="uc1" TagName="WUCCompareEdit" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/ContactsInfo/WUCCompanyInfo.ascx" TagPrefix="uc1" TagName="WUCCompanyInfo" %>
<%@ Register Src="~/DReview/UserControl/DReview/WUCPopRejectToReadyReview.ascx" TagPrefix="uc1" TagName="WUCPopRejectToReadyReview" %>
<%@ Register Src="~/DReview/UserControl/DReview/WUCAddNewNotePopup.ascx" TagPrefix="uc1" TagName="WUCAddNewNotePopup" %>
<%@ Register Src="~/DReview/UserControl/DReview/WUCAddFollowUpComment.ascx" TagPrefix="uc1" TagName="WUCAddFollowUpComment" %>
<%@ Register Src="~/DReview/UserControl/DReview/WUCPopMergeCases.ascx" TagPrefix="uc1" TagName="WUCPopMergeCases" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCPersonalInformationLegal.ascx" TagPrefix="uc1" TagName="WUCPersonalInformationRepLegal" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCIdentificationLegal.ascx" TagPrefix="uc1" TagName="WUCIdentificationLegal" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCEmailAddressLegal.ascx" TagPrefix="uc1" TagName="WUCEmailAddressLegal" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCBackgroundInformationLegal.ascx" TagPrefix="uc1" TagName="WUCBackgroundInformationLegal" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCPhoneNumberLegal.ascx" TagPrefix="uc1" TagName="WUCPhoneNumberLegal" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCAddressLegal.ascx" TagPrefix="uc1" TagName="WUCAddressLegal" %>
<%@ Register Src="~/DReview/UserControl/Compliance/UCCompliance.ascx" TagPrefix="uc1" TagName="WUCompliance" %>

<asp:UpdatePanel runat="server" ID="udpDataReview">
    <ContentTemplate>
        <asp:MultiView runat="server" ID="mtvDataReview" ActiveViewIndex="0">
            <asp:View runat="server" ID="vDataReview">
                <div class="grid grid-pad">
                    <div class="col-1-1">
                        <div class="titulos_sin_accion02" runat="server" id="DataReview">DATA REVIEW</div>
                        <div class="fondo_gris">
                            <div class="col-1-1">
                                <div class="content_fondo_gris">
                                    <dx:ASPxGridView ID="gvDataReview"
                                        KeyFieldName="CorpId;
                                                    RegionId;
                                                    CountryId;
                                                    DomesticregId;
                                                    StateProvId;
                                                    CityId;
                                                    OfficeId; 
                                                    CaseSeqNo;
                                                    HistSeqNo;
                                                    InsuredContactId;
                                                    OwnerContactId;
                                                    AgentId;
                                                    CompanyDesc;
                                                    PolicyNo;
                                                    ProductDesc;
                                                    UserName;
                                                    InsuredFullName;
                                                    OwnerFullName;
                                                    DesignatedPensionerContactId;
                                                    RelationshiptoAgent;
                                                    RelationshiptoOwner;
                                                    StudentContactId;
                                                    AddInsuredContactId;
                                                    CountryDesc;                                                    
                                                    OfficeDesc;
                                                    AgentFullName;
                                                    Exception;
                                                    SubmitDate;
                                                    PaymentId;
                                                    DaysPending;
                                                    IsReview"
                                        runat="server" ClientIDMode="Static"
                                        EnableCallBacks="False" Width="100%" DataSourceID="LinqDS" AutoGenerateColumns="False" OnRowCommand="gvDataReview_RowCommand"
                                        OnPreRender="gvDataReview_PreRender">
                                        <Settings ShowFilterRow="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="false" />
                                        <ClientSideEvents EndCallback="$('#gvDataReview_DXFREditorcol14').hide()" />
                                        <Columns>
                                            <dx:GridViewDataCheckColumn Caption="" VisibleIndex="0" Width="35px">
                                                <DataItemTemplate>
                                                    <asp:CheckBox ID="chkSelectedPolicy" runat="server" Checked="false" />
                                                </DataItemTemplate>
                                            </dx:GridViewDataCheckColumn>
                                            <dx:GridViewDataColumn Caption="SUBMIT" VisibleIndex="1" Width="4%" Name="Submit">
                                                <DataItemTemplate>
                                                    <asp:Button runat="server" CommandName="merge" CssClass='<%# bool.Parse(Eval("IsReview").ToString()) ? ((Eval("isComplianceOK") != null && bool.Parse(Eval("isComplianceOK").ToString())) ? "submit_cases" : "reject_file") : (Eval("isComplianceOK") != null ? (bool.Parse(Eval("isComplianceOK").ToString()) ? "":"reject_file") : "") %>' OnClientClick='<%# bool.Parse(Eval("IsReview").ToString()) ?  ((Eval("isComplianceOK") != null && bool.Parse(Eval("isComplianceOK").ToString()))? "return true" : "return false") : "return false" %>' ToolTip='<%# bool.Parse(Eval("IsReview").ToString())? ((Eval("isComplianceOK") != null && bool.Parse(Eval("isComplianceOK").ToString())) ? "" : "Favor revisar el tab de cumplimiento"): (Eval("isComplianceOK") != null ? (bool.Parse(Eval("isComplianceOK").ToString()) ? "":"Favor revisar el tab de cumplimiento") : "") %>' />
                                                </DataItemTemplate>
                                                <Settings AllowHeaderFilter="False" AllowSort="False" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="CompanyDesc" Caption="COMPANY" VisibleIndex="2" Name="Company" />
                                            <dx:GridViewDataTextColumn FieldName="PolicyNo" Caption="POLICY" VisibleIndex="3" Name="PolicyLabel" />
                                            <dx:GridViewDataTextColumn FieldName="ProductDesc" Caption="PLAN" VisibleIndex="4" Name="PlanLabel" />
                                            <dx:GridViewDataTextColumn FieldName="InsuredFullName" Caption="INSURED NAME" VisibleIndex="5" Name="InsuredName" />
                                            <dx:GridViewDataTextColumn FieldName="CountryDesc" Caption="COUNTRY" VisibleIndex="6" Name="CountryLabel" />
                                            <dx:GridViewDataTextColumn FieldName="OfficeDesc" Caption="OFFICE" VisibleIndex="7" Name="Office" />
                                            <dx:GridViewDataTextColumn FieldName="AgentFullName" Caption="AGENT NAME" VisibleIndex="8" Name="AGENT" />
                                            <dx:GridViewDataTextColumn FieldName="Exception" Caption="EXCEPTION" VisibleIndex="9" Name="Exception" />
                                            <dx:GridViewDataDateColumn FieldName="SubmitDate" Caption="SUBMITTED DATE" VisibleIndex="10" Name="SubmitDate" Width="10%">
                                                <PropertiesDateEdit DisplayFormatString="">
                                                </PropertiesDateEdit>
                                            </dx:GridViewDataDateColumn>
                                            <dx:GridViewDataTextColumn FieldName="DaysPending" Caption="DAYS PENDING" VisibleIndex="11" Name="DaysPending" />
                                            <dx:GridViewDataColumn Caption="COMPARE EDIT" VisibleIndex="12" Width="7%" Name="COMPAREEDIT">
                                                <DataItemTemplate>
                                                    <asp:Button runat="server" ID="btnCompareData" CssClass="compare_edit" CommandName="compare" />
                                                </DataItemTemplate>
                                                <Settings AllowHeaderFilter="False" AllowSort="False" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="NOTE" VisibleIndex="13" Width="4%" Name="NOTE">
                                                <DataItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="udpAddNote">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="btnAddNote" CssClass="add_note" CommandName="addnote" OnClientClick="BeginRequestHandler()" />
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnAddNote" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>

                                                </DataItemTemplate>
                                                <Settings AllowHeaderFilter="False" AllowSort="False" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="REVIEW" VisibleIndex="14" Width="4%" Name="REVIEW" FieldName="IsReview">
                                                <DataItemTemplate>
                                                    <asp:Button runat="server" CssClass='<%# bool.Parse(Eval("IsReview").ToString())?"check_file":"" %>' OnClientClick="return false" />
                                                </DataItemTemplate>
                                                <Settings AllowHeaderFilter="False" AllowSort="True" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="COMMENT" VisibleIndex="15" Width="7%" Name="Comment">
                                                <DataItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="udpComment">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" CssClass="coment_file" CommandName="Comment" ID="btnComment" OnClientClick="BeginRequestHandler()"></asp:Button>
                                                            <span class="globoNB"><%# Eval("CommentCount") %></span>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnComment" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </DataItemTemplate>
                                                <Settings AllowHeaderFilter="False" AllowSort="False" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="REJECT" VisibleIndex="16" Width="7%" Name="Reject">
                                                <DataItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="udpReject">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" CssClass="reject_file" CommandName="reject" ID="btnReject" OnClientClick="BeginRequestHandler()"></asp:Button>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="btnReject" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </DataItemTemplate>
                                                <Settings AllowHeaderFilter="False" AllowSort="False" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="AgentLegalContactId" Caption="RepresentanteLegal" Name="RepresentanteLegal" VisibleIndex="17" Width="0px" />
                                        </Columns>
                                        <SettingsBehavior ColumnResizeMode="NextColumn" />
                                        <Settings VerticalScrollableHeight="600" VerticalScrollBarMode="Visible" />
                                        <SettingsPager PageSize="20" AlwaysShowPager="true">
                                            <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                                        </SettingsPager>
                                        <SettingsPopup>
                                            <HeaderFilter Height="200" />
                                        </SettingsPopup>
                                        <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                    </dx:ASPxGridView>
                                    <dx:LinqServerModeDataSource ID="LinqDS" OnSelecting="LinqDS_Selecting" runat="server" DefaultSorting="SubmitDate" />

                                    <dx:ASPxGridView ID="gvFakeGridView" runat="server" OnPreRender="gvDataReview_PreRender" EnableCallBacks="False" Width="100%" AutoGenerateColumns="False" Visible="false"
                                        KeyFieldName="CompanyDesc; 
                                                          PolicyNo;  
                                                          ProductDesc;  
                                                          InsuredFullName;  
                                                          CountryDesc;  
                                                          OfficeDesc;  
                                                          AgentFullName;  
                                                          Exception;  
                                                          SubmitDate;  
                                                          DaysPending;">
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="CompanyDesc" Caption="COMPANY" VisibleIndex="1" Name="Company" />
                                            <dx:GridViewDataTextColumn FieldName="PolicyNo" Caption="POLICY" VisibleIndex="2" Name="PolicyLabel" />
                                            <dx:GridViewDataTextColumn FieldName="ProductDesc" Caption="PLAN" VisibleIndex="3" Name="PlanLabel" />
                                            <dx:GridViewDataTextColumn FieldName="InsuredFullName" Caption="INSURED NAME" VisibleIndex="4" Name="InsuredName" />
                                            <dx:GridViewDataTextColumn FieldName="CountryDesc" Caption="COUNTRY" VisibleIndex="5" Name="CountryLabel" />
                                            <dx:GridViewDataTextColumn FieldName="OfficeDesc" Caption="OFFICE" VisibleIndex="6" Name="Office" />
                                            <dx:GridViewDataTextColumn FieldName="AgentFullName" Caption="AGENT NAME" VisibleIndex="7" Name="AGENT" />
                                            <dx:GridViewDataTextColumn FieldName="Exception" Caption="EXCEPTION" VisibleIndex="8" Name="Exception" />
                                            <dx:GridViewDataTextColumn FieldName="SubmitDate" Caption="SUBMITTED DATE" VisibleIndex="9" Name="SubmitDate" />
                                            <dx:GridViewDataTextColumn FieldName="DaysPending" Caption="DAYS PENDING" VisibleIndex="10" Name="DaysPending" />
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="gvFakeGridView"></dx:ASPxGridViewExporter>
                                    <!--grid_wrap-->
                                </div>
                                <!--content_fondo_gris-->
                            </div>
                            <!--col-1-1-->
                        </div>
                        <!--fondo gris-->
                    </div>
                    <!--col-1-1-->

                    <div class="col-1-1">
                        <div class="barra_sub_menu">
                            <div class="grupos de_2_b last">
                                <div class="grupos de_2">
                                    <div>
                                        <div class="btn_celeste">
                                            <span class="iconos_botones_azules_virtualoffice"></span>
                                            <asp:Button runat="server" CssClass="boton" Text="EXPORT" ID="btnExport" OnClick="btnExport_Click" OnClientClick="return ConfirmPrintList('gvDataReview');" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>
                                </div>
                            </div>
                            <!--grupos-->
                        </div>
                        <!--barra_sub_menu-->
                    </div>
                    <!--col-1-1-->
                </div>
            </asp:View>
            <asp:View runat="server" ID="vCompareData">
                <div style="float: left; display: block">
                    <asp:Button runat="server" ID="btnviewPdf" CssClass="view_file" OnClick="viewPdf_Click" OnClientClick="BeginRequestHandler()" />
                </div>
                <!--wrapper de las columnas-->
                <div class="grid grid-pad">
                    <ul class="ttl_polis">
                        <li>
                            <asp:Literal runat="server" ID="ltCompareDataFor" Text="COMPARE DATA FOR:"></asp:Literal>
                            <asp:Label runat="server" ID="ltPolicy"> </asp:Label>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="ltOfficeLabel" Text="OFFICE:"></asp:Literal>
                            <asp:Label runat="server" ID="ltOffice"></asp:Label>
                        </li>
                        <li>
                            <div class="input_label50">
                                <asp:Literal runat="server" ID="ltAgentNameLabel" Text="Agent Name:"></asp:Literal>
                                <div class="wrap_select">
                                    <asp:DropDownList runat="server" ID="ddlAgent" AutoPostBack="true" OnSelectedIndexChanged="ddlAgent_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="lblInsuredName" Text="INSURED NAME:"></asp:Literal>
                            <asp:Label runat="server" ID="lblInsured"> </asp:Label>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="ltViewing" Text="VIEWING:"></asp:Literal>
                            <asp:Label runat="server" ID="CurrentTab"> </asp:Label>
                        </li>
                    </ul>

                    <div class="fondo_blanco">
                        <div class="col-1-2">
                            <div class="fondo_gris fix_height">
                                <div class="content_fondo_blanco">
                                    <PdfViewer:PdfViewer ID="pdfViewer" runat="server" ClientIDMode="Static" ShowScrollbars="true" ShowToolbarMode="Show" Width="880" Height="1178"></PdfViewer:PdfViewer>
                                </div>
                                <!--content_fondo_blanco-->
                            </div>
                            <!--fondo gris-->
                            <div class="grupos de_4">
                                <div>
                                    <label class="label" runat="server" id="Document">Document</label>
                                    <div class="wrap_select">
                                        <asp:DropDownList runat="server" ID="ddlDocument" AutoPostBack="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <!--wrap_select-->
                                </div>
                                <div class="label_check margin_t_15">
                                    <asp:CheckBox runat="server" ID="chkIsReview" AutoPostBack="true" OnCheckedChanged="chkIsReview_CheckedChanged" /><label class="label" runat="server" id="Reviewed">Reviewed</label>
                                </div>
                                <div>
                                </div>
                                <div>
                                    <label class="label" runat="server" id="AllDocuments">All Documents</label>
                                    <div class="wrap_select">
                                        <asp:DropDownList runat="server" ID="ddlAllDocuments" AutoPostBack="true" OnSelectedIndexChanged="ddlAllDocuments_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <!--wrap_select-->
                                </div>
                            </div>
                            <!--grupos-->
                        </div>
                        <!--col-1-2-->

                        <div class="col-1-2">
                            <div class="fondo_gris fix_height scroll" id="dvScroll">
                                <asp:MultiView runat="server" ID="vTabs" ActiveViewIndex="0">
                                    <asp:View runat="server" ID="vClientOrOwnerInfo">
                                        <asp:Panel runat="server" ClientIDMode="Static" ID="dvClientOrOwnerInfo" Visible="false">
                                            <div class="accordion_tabulado">
                                                <ul class="principal" id="ClientInfo">
                                                    <li>
                                                        <a href="#" id="current" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonClientInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCPersonalInformation runat="server" ID="WUCPersonalInformation" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonClientInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCAddress runat="server" ID="WUCAddress" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonClientInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCBackgroundInformation runat="server" ID="WUCBackgroundInformation" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonClientInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCPhoneNumber runat="server" ID="WUCPhoneNumber" />
                                                            </li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonClientInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCEmailAddress runat="server" ID="WUCEmailAddress" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonClientInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCIdentification runat="server" ID="WUCIdentification" />
                                                            </li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>

                                        </asp:Panel>
                                        <asp:Panel runat="server" ClientIDMode="Static" ID="dvCompany" Visible="false">

                                            <div class="accordion_tabulado">
                                                <ul class="principal" id="CompanyInfo">
                                                    <li class="trigger shown open">
                                                        <div class="col-1-1">
                                                            <uc1:WUCCompanyInfo runat="server" ID="WUCCompanyInfo" />
                                                        </div>
                                                    </li>
                                                    <%--         <div class="col-1-1">
                                                <uc1:WUCAddress runat="server" ID="WUCAddress1" />
                                            </div>--%>
                                                    <%--      <div class="col-1-1">
                                                <uc1:WUCPhoneNumber runat="server" ID="WUCPhoneNumber1" />
                                            </div>--%>
                                                    <%--    <div class="col-1-1">
                                                <uc1:WUCEmailAddress runat="server" ID="WUCEmailAddress1" />
                                            </div>--%>
                                                </ul>
                                                <div class="titulos_sin_accion" style="width: 100%">
                                                    <asp:Literal runat="server" ID="Rep_Legal">REPRESENTANTE LEGAL</asp:Literal>
                                                    <span style="float: right;">
                                                        <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkAgentLegalIsSameAsInsured" AutoPostBack="true" onclick="BeginRequestHandler()" OnCheckedChanged="chkAgentLegalIsSameAsInsured_CheckedChanged" />
                                                        <label runat="server" id="AgentLegalisSameAsInsured" class="label">Agent Legal is same as Insured</label>
                                                    </span>
                                                </div>

                                                <ul class="" id="AgentInfo">
                                                    <li>
                                                        <a href="#" id="currentAgents" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonAgentInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCPersonalInformationRepLegal runat="server" ID="WUCPersonalInformationRepLegal" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonAgentInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCAddressLegal runat="server" ID="WUCAddressLegal" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonAgentInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCBackgroundInformationLegal ID="WUCBackgroundInformationLegal" runat="server" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonAgentInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCPhoneNumberLegal runat="server" ID="WUCPhoneNumberRepLegal" />
                                                            </li>
                                                        </ul>
                                                    </li>
                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonAgentInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCEmailAddressLegal runat="server" ID="WUCEmailAddressLegal" />
                                                            </li>
                                                        </ul>
                                                    </li>

                                                    <li>
                                                        <a href="#" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonAgentInfo');"><span></span></a>
                                                        <ul>
                                                            <li class="row">
                                                                <uc1:WUCIdentificationLegal runat="server" ID="WUCIdentificationLegal" />
                                                            </li>
                                                        </ul>
                                                    </li>
                                                </ul>
                                            </div>
                                        </asp:Panel>
                                    </asp:View>
                                    <asp:View runat="server" ID="vPlanPolicy">
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <uc1:PlanPolicyContainer runat="server" ID="PlanPolicyContainer" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:View>
                                    <asp:View runat="server" ID="vBeneficiaries">
                                        <uc1:BeneficiariesContainer runat="server" ID="BeneficiariesContainer" />
                                    </asp:View>
                                    <asp:View runat="server" ID="vQuestionaries">
                                        <uc1:HealthDeclarationContainer runat="server" ID="HealthDeclarationContainer" />
                                    </asp:View>
                                    <asp:View runat="server" ID="vRequirements">
                                        <uc1:RequirementsContainer runat="server" ID="RequirementsContainer" />
                                    </asp:View>
                                    <asp:View runat="server" ID="vPayment">
                                        <uc1:PaymentContainer runat="server" ID="PaymentContainer" />
                                    </asp:View>
                                    <asp:View runat="server" ID="VCompliance">
                                        <asp:Panel runat="server" ID="pnlCompliance" ClientIDMode="Static">
                                            <div class="titulos_sin_accion">
                                                <asp:Literal runat="server" ID="litHeaderCumplimiento"> CUMPLIMIENTO </asp:Literal>
                                            </div>
                                            <div class="col-1-1">
                                                <div class="fondo_blanco fix_height">
                                                    <div class="content_fondo_blanco">
                                                        <uc1:WUCompliance runat="server" ID="WUCompliance" />
                                                    </div>
                                                </div>
                                            </div>
                                            <dx:ASPxButton ID="btnRefreshStatus" runat="server" Text="REFRESCAR ESTADOS" RenderMode="Button" Image-IconID="actions_refresh_16x16gray"  OnClick="btnRefreshStatus_Click" Font-Bold="true" CssClass="alignRigth"></dx:ASPxButton>
                                        </asp:Panel>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                            <div id="dvFooterBoton">
                                <!--fondo gris-->
                                <div class="grupos de_4 margin_t_15" id="ButtonTabsContainer">
                                    <div style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="CLIENT INFO" ID="btnClientInfo" CommandArgument="1" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>

                                    <div style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="OWNER INFO" ID="btnOwnerInfo" CommandArgument="2" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>

                                    <div style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="PLAN / POLICY" ID="btnPlanPolicy" CommandArgument="3" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>

                                    <asp:Panel runat="server" ID="pnButtonBeneficiaries" Style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="BENEFICIARIES" ID="btnBeneficiaries" CommandArgument="4" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </asp:Panel>

                                    <div style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="QUESTIONNAIRES" ID="btnQuestionaries" CommandArgument="7" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>

                                    <div style="min-width: 150px; display: none">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="REQUIREMENTS" ID="btnRequirements" CommandArgument="5" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>
                                    <div style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="PAYMENT" ID="btnPayment" CommandArgument="6" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>
                                    <div style="min-width: 150px;">
                                        <div class="btn_bevel blue">
                                            <asp:Button runat="server" CssClass="boton" Text="COMPLIANCE" ID="btnCompliance" CommandArgument="8" ClientIDMode="Static" OnClientClick="return validacionesTab(this)" OnClick="ManageTabs" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>

                                </div>
                                <!--grupos-->
                                <div class="grupos">
                                    <div style="min-width: 162px;" class="float_left margin_t_15">
                                        <div class="btn_bevel green">
                                            <span class="save_celeste"></span>
                                            <asp:Button runat="server" CssClass="boton" ID="btnSave" Text="SAVE" OnClick="btnSave_Click" OnClientClick="BeginRequestHandler(this);return validacionesTab(this);" />
                                        </div>
                                        <!--btn_celeste-->
                                    </div>
                                    <div class="float_right">
                                        <div class="boton_wrapper gris">
                                            <span class="prev_icon_dis"></span>
                                            <asp:Button runat="server" CssClass="boton" ID="btnBackToCompare" Text="Back to Data Review" OnClick="btnBackToCompare_Click" />
                                        </div>
                                    </div>
                                    <div class="float_right">
                                        <div class="boton_wrapper amarillo">
                                            <span class="add"></span>
                                            <asp:Button runat="server" CssClass="boton" ID="btnAddFollowUpComment" Text="Add Follow Up Comment" OnClick="btnAddFollowUpComment_Click" />
                                        </div>
                                    </div>
                                </div>
                                <!--grupos-->
                            </div>
                        </div>
                        <!--col-1-2-->
                    </div>
                </div>
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField runat="server" ID="hdnCurrentTab" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnRememberScroll" runat="server" Value="0" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnShowPopReject" runat="server" Value="false" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnShowCompareEdit" runat="server" Value="false" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnShowMergeCases" runat="server" Value="false" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnShowAddNewNote" runat="server" Value="false" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnShowPopComment" ClientIDMode="Static" Value="false" />
        <asp:HiddenField runat="server" ID="hdnShowAddFollowUpComment" ClientIDMode="Static" Value="false" />
        <asp:HiddenField runat="server" ID="hdnIsCompanyOwner" ClientIDMode="Static" Value="false" />
        <asp:HiddenField runat="server" ID="fixheightIntervalID" ClientIDMode="Static" Value="" />
        <asp:HiddenField runat="server" ID="hfMenuAccordeonClientInfo" ClientIDMode="Static" Value="ClientInfo|0" />
        <asp:HiddenField runat="server" ID="hfMenuAccordeonAgentInfo" ClientIDMode="Static" />
        <!--fondo_blanco-->
        <asp:ModalPopupExtender ID="mpeCompareEdit" PopupControlID="PopCompareEdit" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowCompareEdit" BehaviorID="popupBhvr1" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="PopCompareEdit" CssClass="modalPopup" Style="display: none">
            <uc1:WUCCompareEdit runat="server" ID="WUCCompareEdit" />
        </asp:Panel>

        <asp:ModalPopupExtender ID="mpeMergeCases" PopupControlID="PopMergeCases" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowMergeCases" BehaviorID="popupBhvr2" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="PopMergeCases" ClientIDMode="Static" CssClass="modalPopup" Style="display: none">
            <uc1:WUCPopMergeCases runat="server" ID="WUCPopMergeCases" />
        </asp:Panel>

        <asp:HiddenField runat="server" ID="hdnShowPopViewPDF" ClientIDMode="Static" Value="false" />

        <asp:ModalPopupExtender ID="ModalPopupPdfViewer" PopupControlID="PopPdfViewer" DropShadow="false" TargetControlID="hdnShowPopViewPDF" BehaviorID="popupBhvr3333" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="PopPdfViewer" ClientIDMode="Static" Style="display: none; border: 1px solid black;">
            <div class="titulos_azules" style="width: 100%;">
                <ul>
                    <li style="position: absolute; left: 41%; top: 31%;">
                        <asp:Literal ID="ltDocumentView" Text="" ClientIDMode="Static" runat="server"></asp:Literal>
                    </li>
                    <li style="top: 13%;">
                        <input type="button" id="close_pop_up" class="cls_pup" onclick="$('#hdnShowPopViewPDF').val('false'); $find('popupBhvr3333').hide()" />
                    </li>
                </ul>
            </div>
            <PdfViewer:PdfViewer ID="pdfViewerPopup" runat="server" ClientIDMode="Static" ShowScrollbars="true" ShowToolbarMode="Show" Width="880" Height="589"></PdfViewer:PdfViewer>
        </asp:Panel>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
        <asp:PostBackTrigger ControlID="btnviewPdf" />
    </Triggers>
</asp:UpdatePanel>

<div id="dvAddFollowUpComment" style="display: none">
    <uc1:WUCAddFollowUpComment runat="server" ID="WUCAddFollowUpComment" />
</div>

<div id="dvCommentReject" style="display: none">
    <uc1:WUCPopRejectToReadyReview runat="server" ID="WUCPopRejectToReadyReview" />
</div>

<div id="dvAddNote" style="display: none">
    <uc1:WUCAddNewNotePopup runat="server" ID="WUCAddNewNotePopup" />
</div>

<div id="dvPopAddComment" style="display: none; padding: 0">
    <uc1:UCNotesComments runat="server" ID="UCNotesComments" />
</div>