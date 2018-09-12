﻿<%@ Page Language="C#" MasterPageFile="~/Business.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="IllustrationsVehicle.aspx.cs" Inherits="WEB.NewBusiness.NewBusiness.Pages.IllustrationsVehicle" %><%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCIllustrationInformation.ascx" TagPrefix="uc1" TagName="UCIllustrationInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCInsuredInformation.ascx" TagPrefix="uc1" TagName="UCInsuredInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCVehiclesInformation.ascx" TagPrefix="uc1" TagName="UCVehiclesInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCPoliciesInformation.ascx" TagPrefix="uc1" TagName="UCPoliciesInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/Illustration/UCPopupChangeStatusSaveNotes.ascx" TagPrefix="uc1" TagName="UCPopupChangeStatusSaveNotes" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCIllustrationsInformation.ascx" TagPrefix="uc1" TagName="UCIllustrationsInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCPopupApplyDiscount.ascx" TagPrefix="uc1" TagName="UCPopupApplyDiscount" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCPopupApplySurcharge.ascx" TagPrefix="uc1" TagName="UCPopupApplySurcharge" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCPopupVehicleTagConditioned.ascx" TagPrefix="uc1" TagName="UCPopupVehicleTagConditioned" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCDocuments.ascx" TagPrefix="uc1" TagName="UCDocuments" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCHealthView.ascx" TagPrefix="uc1" TagName="UCHealthView" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCRequiredInformation.ascx" TagPrefix="uc1" TagName="UCRequiredInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCWorkflow.ascx" TagPrefix="uc1" TagName="UCWorkflow" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationAlliedLines/UCAlliedLinesDetail.ascx" TagPrefix="uc1" TagName="UCAlliedLinesDetail" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/UCFacultyPosition.ascx" TagPrefix="uc1" TagName="UCFacultyPosition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../Content/illustrationvehicle.css" rel="stylesheet" />
    <script src="../../Scripts/JSPages/Illustration/IllustrationList.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
    </script>
    <div id="divIllustrationVehicle">
        <div id="divHeader" class="row_A head2 cotizacion">
            <div class="boxes_step col-9 fl">
                <div class="boxes">
                    <asp:LinkButton runat="server" ID="lnkIllustrations" ClientIDMode="Static" OnClick="lnkIllustrations_Click" OnClientClick="BeginRequestHandler(this)"> <div class="box_btn activo cursorpointer">
                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.Illustrations %><i class="arr_ico"></i>
                        </div>
                    </asp:LinkButton>
                    <!--activo_ck-->
                    <div class="box_btn bg_grd_vd cursorpointer"><%=RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationDetail %></div>
                    <div runat="server" class="box_btn bg_grd_vd cursorpointer" id="pnTabSelected"></div>
                </div>
            </div>
        </div>
        <div class="wrapper wrapper-fluid box_shw agent_cont">
            <!-- Contenedor -->
            <div class="row blue cotizacion">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                    <ContentTemplate>
                        <uc1:UCIllustrationInformation runat="server" ID="ucIllustrationInformation" />
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="agent_fbox blue" style="padding-right: 0px;">
                    <div class="row_A cont_tabs mB">
                        <ul id="ulTabs" class="tabs" data-ks-tabs>
                            <li class="">
                                <asp:LinkButton runat="server" ID="lnkIllustration" ClientIDMode="Static" OnClientClick="BeginRequestHandler()" OnClick="ManageTab"> <%=RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationSummary %></asp:LinkButton>
                                <i class="arr_ico"></i></li>
                            <li class="">
                                <asp:LinkButton runat="server" ID="lnkPolicy" ClientIDMode="Static" OnClientClick="BeginRequestHandler()" OnClick="ManageTab"><%=RESOURCE.UnderWriting.NewBussiness.Resources.PolicyHistoric %></asp:LinkButton>
                            </li>
                            <li class="">
                                <asp:LinkButton runat="server" ID="lnkIllustrationHistoric" OnClientClick="BeginRequestHandler()" ClientIDMode="Static" OnClick="ManageTab"><%=RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationHistoric2 %></asp:LinkButton>
                            </li>
                            <li class="">
                                <asp:LinkButton runat="server" ID="lnkRequired" ClientIDMode="Static" OnClientClick="BeginRequestHandler()" OnClick="ManageTab"><%=RESOURCE.UnderWriting.NewBussiness.Resources.RequiredLabel %></asp:LinkButton>
                            </li>
                            <li class="" runat="server" id="liFacultative" visible="false">
                                <asp:LinkButton runat="server" ID="lnkFacultative" ClientIDMode="Static" OnClientClick="BeginRequestHandler()" OnClick="ManageTab"><%=RESOURCE.UnderWriting.NewBussiness.Resources.FacultyPosition %> </asp:LinkButton>
                            </li>
                            <li class="">
                                <asp:LinkButton runat="server" ID="lnkWorkflow" ClientIDMode="Static" OnClientClick="BeginRequestHandler()" OnClick="ManageTab"><%=RESOURCE.UnderWriting.NewBussiness.Resources.Workflow %></asp:LinkButton>
                            </li>
                        </ul>

                        <div id="divInsuredAmount" class="cont_gnl tab_pane_container">
                            <uc1:UCInsuredInformation runat="server" ID="ucInsuredInformation" />

                            <asp:MultiView runat="server" ID="mtvTabs" ActiveViewIndex="0">
                                <asp:View ID="vCotizacion" runat="server">
                                    <article id="r_cotizacion">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                            <ContentTemplate>
                                                <asp:MultiView runat="server" ID="mtvMaster" ActiveViewIndex="0">
                                                    <asp:View runat="server" ID="vVehicleDetail">
                                                        <uc1:UCVehiclesInformation runat="server" ID="ucVehiclesInformation" />
                                                    </asp:View>
                                                    <asp:View runat="server" ID="vHealthDetail">
                                                        <uc1:UCHealthView runat="server" ID="ucHealthView" />
                                                    </asp:View>
                                                    <asp:View runat="server" ID="vAlliedLinesDetail">
                                                        <uc1:UCAlliedLinesDetail runat="server" ID="UCAlliedLinesDetail" />
                                                    </asp:View>
                                                </asp:MultiView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </article>
                                </asp:View>
                                <asp:View ID="vPoliza" runat="server">
                                    <article id="h_poliza">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                            <ContentTemplate>
                                                <uc1:UCPoliciesInformation runat="server" ID="ucPoliciesInformation" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </article>
                                </asp:View>
                                <asp:View ID="vIllustration" runat="server">
                                    <article id="h_illustration">
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                            <ContentTemplate>
                                                <uc1:UCIllustrationsInformation runat="server" ID="ucIllustrationsInformation" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </article>
                                </asp:View>
                                <asp:View ID="vRequired" runat="server">
                                    <article id="h_requiredinformation">
                                        <uc1:UCRequiredInformation runat="server" ID="ucRequiredInformation" />
                                    </article>
                                </asp:View>
                                <asp:View runat="server" ID="vFacultative">
                                    <article id="h_facultative">
                                        <uc1:UCFacultyPosition runat="server" ID="UCFacultyPosition" />
                                    </article>
                                </asp:View>
                                <asp:View ID="vWorkflow" runat="server">
                                    <article id="h_workflow">
                                        <uc1:UCWorkflow runat="server" ID="ucWorkflow" />
                                    </article>
                                </asp:View>
                            </asp:MultiView>

                            <asp:Button runat="server" ID="btnIllustration" ClientIDMode="Static" OnClick="btnIllustration_Click" Style="display: none;" />
                            <asp:Button runat="server" ID="btnPolicy" ClientIDMode="Static" OnClick="btnPolicy_Click" Style="display: none;" />
                            <asp:Button runat="server" ID="btnIllustrationHistoric" OnClick="btnIllustrationHistoric_Click" ClientIDMode="Static" Style="display: none;" />
                            <asp:Button runat="server" ID="btnWorkflow" OnClick="btnWorkflow_Click" ClientIDMode="Static" Style="display: none;" />

                            <asp:UpdatePanel runat="server" class="row_A mT10">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnLinksButtons">
                                        <asp:LinkButton runat="server" ID="btnSendToCore" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="btnSendToCore_Click" OnClientClick="return DlgConfirm(this)">
                                           <%=RESOURCE.UnderWriting.NewBussiness.Resources.ApproveBySubscription %> 
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" CssClass="col-2 fl button button-blue alignC embossed" ID="btnInspection"
                                            ClientIDMode="Static" OnClick="btnInspection_Click" Visible="false">
                                         <%=RESOURCE.UnderWriting.NewBussiness.Resources.Inspection %> 
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkBlackList" CssClass="col-2 fl button button-blue alignC embossed" OnClick="lnkBlackList_Click">
                                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.BlackList %>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkDataCredito" CssClass="col-2 fl button button-blue alignC embossed" OnClick="lnkDataCredito_Click">
                                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.DataCreditoLabel %>
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnDeclinedByClient" CssClass="col-2 fl button button-red alignC embossed"
                                            ClientIDMode="Static" OnClick="btnDeclinedByClient_Click">
                                              <%=RESOURCE.UnderWriting.NewBussiness.Resources.btnChangeStatusCotizacion %>                                              
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnSubscription" Visible="false" ClientIDMode="Static" CssClass="col-2 fl button button-green alignC embossed"
                                            OnClientClick="return ChangeIllustrationStatus(this);">
                                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.SendToSubscription %>                                                                                          
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnReopenIllustration" ClientIDMode="Static" CssClass="col-2 fl button button-green alignC embossed"
                                            OnClick="btnReopenIllustration_Click">
                                             <%=RESOURCE.UnderWriting.NewBussiness.Resources.ReopenIllustration %>    
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnDeclinedBySubscription" Visible="false" CssClass="col-2 fl button button-red alignC embossed"
                                            ClientIDMode="Static" OnClientClick="return IllustrationDeniedClick()" OnClick="btnDeclinedBySubscription_Click">                                         
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" CssClass="col-2 fl button button-blue alignC embossed" Text="Apply Discount"
                                            ID="btnOpenApplyDiscount" OnClientClick="BeginRequestHandler(); return true;" ClientIDMode="Static" OnClick="btnOpenApplyDiscount_Click">
                                             <%=RESOURCE.UnderWriting.NewBussiness.Resources.ApplyDiscount %>                                                  
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" CssClass="col-2 fl button button-blue alignC embossed" Text="Apply Discount"
                                            ID="btnOpenApplySurcharge" OnClientClick="BeginRequestHandler(); return true;" ClientIDMode="Static" OnClick="btnOpenApplySurcharge_Click">
                                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.ApplySurcharge %>   
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnSeeIllustration" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="btnSeeIllustration_Click">                                        
                                         
                                         </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnForeignStatusNotification" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="btnForeignStatusNotification_Click" OnPreRender="btnForeignStatusNotification_PreRender">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.ForeignStatusNotification %>   
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnCondicionado" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="btnCondicionado_Click">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.Conditioned %>   
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnCondPart" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="btnCondPart_Click">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.ParticularsConditions %>   
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkEndorsementClarifyingDepre" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="lnkEndorsementClarifyingDepre_Click">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.EndorsementClarifyingDepre %>   
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="lnkEndorsementOfDeductibleApplication" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="lnkEndorsementClarifyingDepre_Click">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.EndorsementOfDeductibleApplication %>   
                                        </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="lnkEndorsementClarifyingEconovida" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClick="lnkEndorsementClarifyingEconovida_Click">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.EndorsementClarifyingEconoVida %>
                                        </asp:LinkButton>

                                        <asp:LinkButton runat="server" ID="btnOnBase" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClientClick="return DlgConfirm(this);" OnClick="btnOnBase_Click">
                                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.Generatetheindexfile %>   
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnOnBaseKCO" Visible="false" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClientClick="return DlgConfirm(this);" OnClick="btnOnBaseKCO_Click">
                                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.GeneratetheindexfileKCO %>   
                                        </asp:LinkButton>
                                        <asp:Button runat="server" ID="btnGenerateFileError" ClientIDMode="Static" Style="display: none" OnClick="btnGenerateFileError_Click" />
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnOpenApplySurcharge" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSeeIllustration" />
                                    <asp:AsyncPostBackTrigger ControlID="btnOpenApplyDiscount" />
                                    <asp:AsyncPostBackTrigger ControlID="btnReopenIllustration" />
                                    <asp:AsyncPostBackTrigger ControlID="btnDeclinedByClient" />
                                    <asp:AsyncPostBackTrigger ControlID="btnInspection" />
                                    <asp:AsyncPostBackTrigger ControlID="btnForeignStatusNotification" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSendToCore" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCondicionado" />
                                    <asp:AsyncPostBackTrigger ControlID="btnCondPart" />
                                    <asp:AsyncPostBackTrigger ControlID="btnOnBase" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkEndorsementOfDeductibleApplication" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkEndorsementClarifyingDepre" />
                                    <asp:AsyncPostBackTrigger ControlID="lnkEndorsementClarifyingEconovida" />
                                    <asp:PostBackTrigger ControlID="btnGenerateFileError" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                        <!--// END .tab_pane_container-->
                    </div>
                </div>
            </div>
            <!--// Contenedor -->
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnCurrentTab" Value="lnkIllustration" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdnValidate" Value="true" ClientIDMode="Static" />

    <uc1:UCPopupChangeStatusSaveNotes runat="server" ID="ucPopupChangeStatusSaveNotes" />
    <uc1:UCPopupApplySurcharge runat="server" ID="ucPopupApplySurcharge" />
    <uc1:UCPopupVehicleTagConditioned runat="server" ID="UCPopupVehicleTagConditioned" />

    <asp:ModalPopupExtender ID="mpeTransunion" PopupControlID="pnTransunion" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopTransunion" BehaviorID="popupBhvrTransunion" runat="server"></asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="pnTransunion" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
        <div class="pop_up_wrapper" style="width: 1034px; height: 960px; overflow-x: hidden; overflow-y: hidden">
            <!--escriben por style el ancho que desean-->
            <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                <ul>
                    <li style="position: absolute; left: 41%; top: 31%;">
                        <asp:Label ID="lblTitle" ClientIDMode="Static" runat="server"></asp:Label>
                    </li>
                    <li style="top: 13%;">
                        <input type="button" id="close_pop_up" class="cls_pup" onclick="ClosePopTransunion();" />
                    </li>
                </ul>
            </div>
            <!--titulos_azules-->
            <iframe runat="server" id="ifrmTransunion" clientidmode="Static" style="height: 920px; padding: 0;"></iframe>
            <!--content_fondo_blanco-->
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" Value="false" ID="hdnPopTransunion" ClientIDMode="Static" />

    <asp:ModalPopupExtender
        BackgroundCssClass="ModalBackgroud3" BehaviorID="popupBhvr" DropShadow="false" ID="mpeApplyDiscount" PopupControlID="pnlApplyDiscount" runat="server" TargetControlID="hdnApplyDiscount">
    </asp:ModalPopupExtender>
    <%-- Este panel se utiliza para contener el UC de aplicar descuentos --%>
    <asp:Panel runat="server" ID="pnlApplyDiscount" CssClass="modalPopup recargoApply" Style="display: none;">
        <uc1:UCPopupApplyDiscount runat="server" ID="ucPopupApplyDiscount" />
    </asp:Panel>

    <asp:HiddenField runat="server" Value="false" ID="hdnApplyDiscount" ClientIDMode="Static" />

    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="udpQuotationPrev">
        <ContentTemplate>
            <asp:ModalPopupExtender ID="ModalPopupPDFViewer" PopupControlID="pdfUploadPanel" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPreviewPDF" BehaviorID="popupBhvrQuotation" runat="server"></asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pdfUploadPanel" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
                <div class="pop_up_wrapper" style="width: 1189px; height: 752px; overflow-x: hidden; overflow-y: hidden">
                    <!--escriben por style el ancho que desean-->
                    <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                        <ul>
                            <li style="position: absolute; left: 41%; top: 31%;">
                                <asp:Label ID="ltTypeDoc2" ClientIDMode="Static" runat="server"><%=RESOURCE.UnderWriting.NewBussiness.Resources.PrintPdfHeader %></asp:Label>
                            </li>
                            <li style="top: 13%;">
                                <input type="button" id="close_pop_up" class="cls_pup" onclick="ClosePopQuotationPrev()" />
                            </li>
                        </ul>
                    </div>
                    <!--titulos_azules-->
                    <PdfViewer:PdfViewer ID="pdfViewerMyPreviewPDF" CssClass="PdfViewer" runat="server" Height="712" Width="1186"
                        ClientIDMode="Static" ShowScrollbars="true" ShowToolbarMode="Show">
                    </PdfViewer:PdfViewer>
                    <!--content_fondo_blanco-->
                </div>
            </asp:Panel>
            <asp:HiddenField runat="server" Value="false" ID="hdnShowPreviewPDF" ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnOpenMoreInformationPanel" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnDiscountRole" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnCorpId" />

</asp:Content>
