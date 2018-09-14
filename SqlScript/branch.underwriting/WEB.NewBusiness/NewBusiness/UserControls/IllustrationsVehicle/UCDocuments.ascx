﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCDocuments.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCDocuments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCRequirementPdfPopUp.ascx" TagPrefix="uc1" TagName="UCRequirementPdfPopUp" %>

<asp:UpdatePanel ID="udpRequirementsDoc" runat="server" RenderMode="Block" class="reqVehiculo">
    <ContentTemplate>
        <div class=" tbl data_Gpl">
            
            <div class="tbl data_Gpl gvVehiculos">
                <dx:ASPxGridView ID="gvRequirement" runat="server"
                    EnableCallBacks="False"
                    AutoGenerateColumns="False" Width="100%"
                    OnRowCommand="gvRequirement_RowCommand"
                    OnPreRender="gvRequirement_PreRender"
                    OnPageIndexChanged="gvRequirement_PageIndexChanged"
                    OnHtmlRowCreated="gvRequirement_HtmlRowCreated"
                    OnAfterPerformCallback="gvRequirement_AfterPerformCallback">
                    <Columns>
                        <dx:GridViewDataColumn Caption="Obligatorio" FieldName="IsMandatory" Name="IsMandatoryLabel">
                            <DataItemTemplate>
                                <span class="hasdocument_<%#Eval("IsMandatory")%>">
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Valiado" FieldName="IsValid" Name="ValidLabel">
                            <DataItemTemplate>
                                <span class="hasdocument_<%#Eval("IsValid")==null?false:Eval("IsValid")%> hideme"></span>
                                <span class="ShowDate_<%#Eval("IsValid")==null?false:Eval("IsValid")%>"><%# Convert.ToDateTime(Eval("ValidByDate")).ToShortDateString() %></span>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="false" AllowSort="true" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn Caption="Subido por" FieldName="UploadByUserName" Name="Submittedby">
                            <Settings AllowHeaderFilter="false" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Validado por" FieldName="ValidByUserName" Name="Validatedby">
                            <Settings AllowHeaderFilter="false" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Documento Requerido" FieldName="RequirementTypeDesc" Name="DocumentLabel">
                            <Settings AllowHeaderFilter="false" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn Caption="Subir / Ver Documento" Name="uploadViewDocument" Width="100" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:Button runat="server" CssClass="k" CommandName="Upload" ID="btnUpload" Visible="true"></asp:Button>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Eliminar" Name="DeleteLabel" Width="100">
                            <DataItemTemplate>
                                <div style="text-align: center">
                                    <asp:Button runat="server" CssClass="delete_file" CommandName="Delete" ID="btnDelete" OnClientClick="return DlgConfirm(this)"></asp:Button>
                                </div>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                    </Columns>
                    <Settings VerticalScrollableHeight="500" VerticalScrollBarMode="Hidden" />
                    <SettingsPager PageSize="100000000" AlwaysShowPager="false">
                        <PageSizeItemSettings Visible="false" ShowAllItem="true" />
                    </SettingsPager>
                    <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                </dx:ASPxGridView>
            </div>
        </div>

        <asp:Button runat="server" ID="btnSaveDocument" ClientIDMode="Static" OnClick="btnSaveDocument_Click" Style="display: none;" />
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="udpPrev">
            <ContentTemplate>
                <asp:ModalPopupExtender ID="MPopPDFViewer" PopupControlID="pdfView" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPDF" BehaviorID="popupBhvrPDFView" runat="server" />
                <asp:Panel runat="server" ID="pdfView" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
                    <div class="pop_up_wrapper" style="width: 1189px; height: 791px; overflow-x: hidden; overflow-y: hidden; background-color: #525659;">
                        <!--escriben por style el ancho que desean-->
                        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                            <ul>
                                <li style="position: absolute; top: 31%;">
                                    <asp:Label ID="ltTypeDoc2" ClientIDMode="Static" runat="server"></asp:Label>
                                </li>
                                <li style="top: 13%;">
                                    <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: 0px;" onclick="ClosePopQuotation()" />
                                </li>
                            </ul>
                        </div>
                        <!--titulos_azules-->
                        <PdfViewer:PdfViewer ID="pdfViewerMyPreviewPDF" CssClass="PdfViewer" runat="server" Height="712" Width="1186"
                            ClientIDMode="Static" ShowScrollbars="true" ShowToolbarMode="Show">
                        </PdfViewer:PdfViewer>
                        <!--content_fondo_blanco-->
                        <asp:Panel runat="server" ID="pnChkValidate" class="button button-orange checko embossed" Style="position: relative; top: 5px;">
                            <table style="width: 98px; margin-left: 15px;">
                                <tr>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkValidateDoc" AutoPostBack="true" OnCheckedChanged="chkValidateDoc_CheckedChanged" /></td>
                                    <td style="font-weight: bold"><%=RESOURCE.UnderWriting.NewBussiness.Resources.ValidLabel%></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:HiddenField runat="server" Value="false" ID="hdnShowPDF" ClientIDMode="Static" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="updUploadFile">
            <ContentTemplate>
                <asp:ModalPopupExtender ID="mpeUploadFile" PopupControlID="pnlShowUploadFile" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowUploadFile" BehaviorID="popupBhvrUploadFile" runat="server" />
                <asp:Panel runat="server" ID="pnlShowUploadFile" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
                    <uc1:UCRequirementPdfPopUp runat="server" ID="UCRequirementPdfPopUp" />
                </asp:Panel>
                <asp:HiddenField runat="server" Value="false" ID="hdnShowUploadFile" ClientIDMode="Static" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField runat="server" ID="hdnTransunion" ClientIDMode="Static" />
        <asp:Button runat="server" ID="btnRefreshList" ClientIDMode="Static" Style="display: none" OnClick="btnRefreshList_Click"></asp:Button>
        <asp:Button runat="server" ID="btnDownload" ClientIDMode="Static" Style="display: none" OnClick="btnDownload_Click"></asp:Button>
        <asp:HiddenField runat="server" ID="hdndReq" ClientIDMode="Static" />
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnDownload" />
        <asp:PostBackTrigger ControlID="btnSaveDocument" />
        <asp:AsyncPostBackTrigger ControlID="gvRequirement" />
    </Triggers>
</asp:UpdatePanel>