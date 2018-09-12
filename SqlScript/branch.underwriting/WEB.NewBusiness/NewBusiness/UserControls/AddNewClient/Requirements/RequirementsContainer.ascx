﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequirementsContainer.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Requirements.RequirementsContainer" %>
<%@ Register Assembly="DevExpress.Web.v14.2" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Requirements/UCRequirements.ascx" TagPrefix="uc1" TagName="Requirements" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Requirements/UCRequirementPdfPopUp.ascx" TagPrefix="uc1" TagName="UCRequirementPdfPopUp" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Requirements/WUCPDFViewerReq.ascx" TagPrefix="uc1" TagName="WUCPDFViewerReq" %>

<asp:UpdatePanel runat="server" ID="udpUpdate" ClientIDMode="Static">
    <ContentTemplate>
        <style>
            /*Popup Control*/
            .modalBackground {
                background-color: #707070;
                filter: alpha(opacity=70);
                opacity: 0.7;
            }

            .modalPopup {
                background-color: white;
                padding: 3px;
                width: auto;
                z-index: 0;
            }

            .popupControl {
                background-color: #00477F;
                position: absolute;
                visibility: hidden;
                border-style: solid;
                border-color: Black;
                border-width: 2px;
                font: Arial;
            }

            .CustomTextBox, .CustomTextBox input[type='text'] {
                padding: 0px;
                border-width: 0px;
            }

                .CustomTextBox input[type='text'] {
                    opacity: 0;
                    filter: alpha(opacity=0);
                }

            .alignRigth {
                float: right;
            }
        </style>

        <asp:Button ID="btnShowPopup" Style="display: none" runat="server" Text="ShowPopup" />
        <asp:Button ID="btnViewFile" Style="display: none" runat="server" Text="ViewFile" />
        <asp:HiddenField runat="server" ID="RequirementsValues" ClientIDMode="Static" />

        <%-- Bmarroquin 10-04-2017 se crea panel para ocultar todos los controles en DR--%>
        <asp:Panel runat="server" ID="pnlDocumentosRequeridosNB" ClientIDMode="Static">

            <div class="titulos_sin_accion">
                <asp:Literal runat="server" ID="ltRequirements"> REQUIREMENTS </asp:Literal>
            </div>
            <div class="col-1-2">
                <asp:DataList ID="LstViewGeneralDocuments" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                    DataKeyField="Key" OnItemDataBound="ItemDataBound">
                    <ItemTemplate>
                        <div class="fondo_blanco fix_height">
                            <div class="titulos_azules"><span class="insured"></span><strong><%# RESOURCE.UnderWriting.NewBussiness.Resources.ResourceManager.GetString(DataBinder.Eval(Container.DataItem, "RequirementCatDesc").ToString().ToUpper()) %></strong></div>
                            <div class="content_fondo_blanco">
                                <uc1:Requirements runat="server" ID="UcRequirements" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="col-1-2">
                <asp:DataList ID="LstViewMedicalRequiments" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                    DataKeyField="Key" OnItemDataBound="ItemDataBound">
                    <ItemTemplate>
                        <div class="fondo_blanco fix_height">
                            <div class="titulos_azules"><span class="insured"></span><strong><%# RESOURCE.UnderWriting.NewBussiness.Resources.ResourceManager.GetString(DataBinder.Eval(Container.DataItem, "RequirementCatDesc").ToString().ToUpper()) %></strong></div>
                            <div class="content_fondo_blanco">
                                <uc1:Requirements runat="server" ID="UcRequirements" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="col-1-2">
                <asp:DataList ID="LstViewFinancialRequiments" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                    DataKeyField="Key" OnItemDataBound="ItemDataBound">
                    <ItemTemplate>
                        <div class="fondo_blanco fix_height">
                            <div class="titulos_azules"><span class="insured"></span><strong><%# RESOURCE.UnderWriting.NewBussiness.Resources.ResourceManager.GetString(DataBinder.Eval(Container.DataItem, "RequirementCatDesc").ToString().ToUpper()) %></strong></div>
                            <div class="content_fondo_blanco">
                                <uc1:Requirements runat="server" ID="UcRequirements" />
                            </div>
                        </div>

                    </ItemTemplate>
                </asp:DataList>
            </div>
            <div class="col-1-2">
                <asp:DataList ID="LstViewOccupationSport" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                    DataKeyField="Key" OnItemDataBound="ItemDataBound">
                    <ItemTemplate>
                        <div class="fondo_blanco fix_height">
                            <div class="titulos_azules"><span class="insured"></span><strong><%# RESOURCE.UnderWriting.NewBussiness.Resources.ResourceManager.GetString(DataBinder.Eval(Container.DataItem, "RequirementCatDesc").ToString().ToUpper()) %></strong></div>
                            <div class="content_fondo_blanco">
                                <uc1:Requirements runat="server" ID="UcRequirements" />
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:DataList>
            </div>

            <div id="ValidationMessage" style="display: none">
                <span id="ValidationMessageSpan"></span>
            </div>
            <p>
                &nbsp;
            </p>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" PopupControlID="pdfUploadPanel" TargetControlID="btnShowPopup" BehaviorID="popupBhvr" runat="server"></asp:ModalPopupExtender>
            <asp:Panel runat="server" ID="pdfUploadPanel" CssClass="modalPopup" Style="display: none">
                <uc1:UCRequirementPdfPopUp runat="server" ID="UCRequirementPdfPopUp" />
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hdnCase" ClientIDMode="Static" Value="" />
            <asp:HiddenField runat="server" ID="RequirementPath" ClientIDMode="Static" Value="" />
            <asp:Panel runat="server" ID="panelViewPdf" ClientIDMode="Static" Style="padding-top: 30px; display: none;">
            </asp:Panel>
            <!--grid_wrap-->
            <asp:HiddenField runat="server" ID="hdnShowPopRequirementPDF" Value="false" ClientIDMode="Static" />
            <div id="dvPopPDFRequirements" style="display: none">
                <uc1:WUCPDFViewerReq runat="server" ID="WUCPDFViewerReq" />
            </div>

            <asp:Panel runat="server" ID="pnSave" class="col-1-1">
                <div class="barra_sub_menu">
                    <div class="grupos de_5">
                        <div>
                            <div class="btn_celeste">
                                <span class="see_ilustracion"></span>
                                <asp:Button runat="server" CssClass="boton" Text="SEND TO REVIEW" ID="btnSendToReview" OnClick="btnSendToReview_Click" OnClientClick="return ConfirmReadyToReview(this)" />
                            </div>
                            <!--btn_celeste-->
                        </div>
                    </div>
                    <!--grupos-->
                </div>
                <!--barra_sub_menu-->
            </asp:Panel>


        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
