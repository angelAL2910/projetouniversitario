﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCProperty.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationAlliedLines.Products.UCProperty" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationAlliedLines/UCPropertyDetail.ascx" TagPrefix="uc1" TagName="UCPropertyDetail" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationAlliedLines/Products/UCCoverages.ascx" TagPrefix="uc1" TagName="UCCoverages" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationAlliedLines/UCEndosoCesionAlliedLines.ascx" TagPrefix="uc1" TagName="UCEndosoCesionAlliedLines" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCBlackListValidation.ascx" TagPrefix="uc1" TagName="WUCBlackListValidation" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <dx:ASPxGridView
            ID="gvPropertyDetail"
            runat="server"
            EnableCallBacks="False"
            KeyFieldName="UniquePropertyId;InsuredAmount"
            ClientIDMode="Static"                                                                         
            AutoGenerateColumns="false"
            Width="100%"
            OnRowCommand="gvAlliedLinesDetail_RowCommand"
            OnPreRender="gvPropertyDetail_PreRender">
            <Columns>
                <dx:GridViewDataColumn Caption="Editar" Name="View" Visible="true" VisibleIndex="0">
                    <DataItemTemplate>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton
                                        runat="server"
                                        ID="lnkEdit"
                                        CommandName="EditProperty" CssClass="view_file">
                                    </asp:LinkButton>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkEdit" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </DataItemTemplate>
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataColumn>    
                <dx:GridViewDataTextColumn FieldName="TypeCoverage" Name="TypeCoverageLabel" Caption="TypeCoverageLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="1">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PropertyBuildTypeDesc" Name="PropertyBuildTypeDescLabel" Caption="PropertyBuildTypeDescLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="1">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="electricsystem" Name="electricsystemLabel" Caption="electricsystemLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="1">
                    <Settings AllowHeaderFilter="False" AllowSort="False" /> 
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="fireProtectionequipment" Name="fireProtectionequipmentLabel" Caption="fireProtectionequipmentLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="1">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BusinessTypeDesc" Name="BusinessLabel" Caption="Negocio" CellStyle-HorizontalAlign="Center" VisibleIndex="1">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="ActivfityTypeDesc" FieldName="ActivfityTypeDesc" Name="ActivfityTypeDescLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="2">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Address" FieldName="AddressStreetFull" Name="AddressLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="3">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Edification" FieldName="EdificationValueF" Name="EdificationValueLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="4">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Machinery" FieldName="MachineryValueF" Name="MachineryValueLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="5">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Furniture And Equipment" FieldName="FurnitureAndEquipmentValueF" Name="FurnitureAndEquipmentValueLabel" CellStyle-HorizontalAlign="Center" Width="140px" VisibleIndex="6">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Stock Value" FieldName="StockValueF" Name="StockValueLabelF" CellStyle-HorizontalAlign="Center" VisibleIndex="7">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Value Object And Art Value" Visible="false" FieldName="ValueObjectAndArtValueF" Name="ValueObjectAndArtValueLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="8">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="pól. Princ." FieldName="PolicyNoMain" Name="PolicyNumberMain" Visible="false" CellStyle-HorizontalAlign="Center" VisibleIndex="9">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Plan" FieldName="ProductDesc" Name="PlanLabel" CellStyle-HorizontalAlign="Center" VisibleIndex="10">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Monto Asegurado" FieldName="InsuredAmountF" Name="InsuredAmountLabel2" CellStyle-HorizontalAlign="Center" VisibleIndex="11">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Deducible" FieldName="DeductiblePercentageF" Name="Deductible" CellStyle-HorizontalAlign="Center" Visible="false" VisibleIndex="12">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Prima sin Impuesto" FieldName="PremiumAmountF" Name="PremiumWithoutTax" CellStyle-HorizontalAlign="Center" Width="120px" VisibleIndex="13">
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Inspeccion" Name="Inspected" Width="30px" VisibleIndex="14">
                    <DataItemTemplate>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <asp:CheckBox
                                        Checked='<%# Eval("Inspected") %>'
                                        Enabled="false"
                                        ID="chkInspection"
                                        runat="server"
                                        Visible='<%# !Convert.ToBoolean(Eval("VisibleLnkInspeccion")) %>' />
                                    <%--Visible='<%# !Convert.ToBoolean(Eval("Inspected")) %>' />--%>
                                    <asp:LinkButton
                                        CommandName="Inspeccion"
                                        CssClass='<%# Eval("CssClassInspected") %>'
                                        Enabled='<%# Eval("Inspected") %>'
                                        ID="lnkInspeccion"
                                        runat="server"
                                        Visible='<%# Eval("VisibleLnkInspeccion") %>'></asp:LinkButton>
                                    <%--Visible='<%# Eval("Inspected") %>'></asp:LinkButton>--%>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkInspeccion" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </DataItemTemplate>
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="<br />Cobertura" Name="Coverage" Width="30px" HeaderStyle-CssClass="clearFix" VisibleIndex="15">
                    <DataItemTemplate>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton runat="server" ID="lnkCoverage" CommandName="Coverage" CssClass="view_file"></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkCoverage" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </DataItemTemplate>
                    <CellStyle HorizontalAlign="Center" VerticalAlign="Middle"></CellStyle>
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Endoso" Name="Endorsement" Width="30px" VisibleIndex="16">
                    <DataItemTemplate>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton
                                        runat="server"
                                        ID="lnkEndosoCesion"
                                        CommandName="Endoso"
                                        CssClass='<%# Eval("ClassEndoso")%>'></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkEndosoCesion" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </DataItemTemplate>
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Endoso Aclaratorio" Name="EndorsementClarifying" Width="130px" VisibleIndex="17">
                    <DataItemTemplate>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="text-align: center">
                                    <asp:LinkButton
                                        CommandName="EndosoAclaratorio"
                                        CssClass='<%# Eval("CssClassEndorsementClarifying")%>'
                                        Enabled='<%#Eval("EndorsementClarifying") %>'
                                        ID="lnkEndosoAclaratorio"
                                        runat="server"></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkEndosoAclaratorio" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </DataItemTemplate>
                    <Settings AllowHeaderFilter="False" AllowSort="False" />
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Name="BlackList">
                    <DataItemTemplate>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:LinkButton
                                    runat="server"
                                    ID="lnkBlackList"
                                    CommandName="BlackList"
                                    CssClass='view_file'></asp:LinkButton>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkBlackList" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </DataItemTemplate>
                </dx:GridViewDataColumn>
            </Columns>
            <SettingsPager PageSize="20" AlwaysShowPager="true">
                <PageSizeItemSettings Visible="false" ShowAllItem="true" />
            </SettingsPager>
            <SettingsBehavior AllowSelectSingleRowOnly="false" AllowSort="False" AllowFocusedRow="false" />
            <Settings ShowFooter="True" />
        </dx:ASPxGridView>

        <asp:ModalPopupExtender ID="ModalPopupCoverage" PopupControlID="popCoverage" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopCoverages" BehaviorID="popupBhvrPopCoverages" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="popCoverage" ClientIDMode="Static">
            <div class="pop_up_wrapper" style="width: 936px; height: 775px; overflow-x: hidden; overflow-y: hidden">
                <!--escriben por style el ancho que desean-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 41%; top: 31%;">
                            <asp:Label ID="Label1" ClientIDMode="Static" runat="server" Text="COBERTURAS"></asp:Label>
                        </li>
                        <li style="top: 13%;">
                            <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopCoverage();" />
                        </li>
                    </ul>
                </div>
                <!--content_fondo_blanco-->
                <uc1:UCCoverages runat="server" ID="UCCoverages" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpePropertyDetail" PopupControlID="pnPropertyDetail" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopPropertyDetail" BehaviorID="popupBhvrPropertyDetail" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="pnPropertyDetail" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper" style="width: 1529px; height: 800px; overflow-x: hidden; overflow-y: hidden">
                <!--escriben por style el ancho que desean-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 41%; top: 31%;">
                            <asp:Label ID="lblTitle" ClientIDMode="Static" runat="server"></asp:Label>
                        </li>
                        <li style="top: 13%;">
                            <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopPropertyDetail();" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:UCPropertyDetail runat="server" ID="UCPropertyDetail" />
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupEndoso" PopupControlID="popEndoso" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnEndosoPopup" BehaviorID="popupEndoso" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="popEndoso" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper tbl data_Gpl coberageT" style="width: 800px; height: 452px; border: 1px solid #000; overflow-x: hidden; overflow-y: hidden">
                <!--escriben por style el ancho que desean-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 41%; top: 18%;">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.EndosoTitulo%></span>
                        </li>
                        <li style="top: 13%;">
                            <input type="button" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopEndoso();" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:UCEndosoCesionAlliedLines runat="server" ID="UCEndosoCesionAlliedLines" />
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="ModalPopupShowPDF" PopupControlID="pnShowPDF" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPDF" BehaviorID="popupBhvr1PDF" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="pnShowPDF" CssClass="modalPopup" ClientIDMode="Static" Style="display: none;">
            <div class="pop_up_wrapper" style="width: 1189px; height: 752px; overflow-x: hidden; overflow-y: hidden">
                <!--escriben por style el ancho que desean-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 41%; top: 31%;">
                            <asp:Label ID="ltTypeDoc2" ClientIDMode="Static" runat="server"><%=RESOURCE.UnderWriting.NewBussiness.Resources.PrintPdfHeader %></asp:Label>
                        </li>
                        <li style="top: 13%;">
                            <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: 0;" onclick="ClosePop();" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <PdfViewer:PdfViewer
                    ID="pdfViewerMyPreviewPDF"
                    CssClass="PdfViewer"
                    runat="server"
                    Height="712"
                    Width="1186"
                    ClientIDMode="Static"
                    ShowScrollbars="true"
                    ShowToolbarMode="Show">
                </PdfViewer:PdfViewer>
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>

        <asp:ModalPopupExtender ID="ModalPopupBlackList" PopupControlID="popBlackList" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopBlackList" BehaviorID="popupBlackList" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="popBlackList" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper tbl data_Gpl coberageT" style="width: 800px; height: 340px; border: 1px solid #000; overflow-x: hidden; overflow-y: hidden">
                <!--escriben por style el ancho que desean-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 41%; top: 18%;">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.BlackList%></span>
                        </li>
                        <li style="top: 13%;">
                            <input type="button" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopBlackList();" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:WUCBlackListValidation runat="server" ID="WUCBlackListValidation" />
                <!--content_fondo_blanco-->
                <asp:HiddenField runat="server" Value="false" ID="hdnPopBlackList" ClientIDMode="Static" />
            </div>
        </asp:Panel>
        <asp:HiddenField runat="server" Value="0" ID="hdntotalInsuredAmount" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnPopPropertyDetail" Value="false" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnPopCoverages" Value="false" ClientIDMode="Static" />
        <asp:HiddenField runat="server" Value="false" ID="hdnEndosoPopup" ClientIDMode="Static" />
        <asp:HiddenField runat="server" Value="false" ID="hdnShowPDF" ClientIDMode="Static" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvPropertyDetail" />
    </Triggers>
</asp:UpdatePanel>
