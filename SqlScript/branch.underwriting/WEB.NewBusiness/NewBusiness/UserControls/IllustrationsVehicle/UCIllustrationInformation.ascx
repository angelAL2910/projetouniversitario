﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCIllustrationInformation.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCIllustrationInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCSeeDiscount.ascx" TagPrefix="uc1" TagName="UCSeeDiscount" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>

<div class="agent_fbox">
    <div class="row_A cont_tabs mB">
        <div class="cont_gnl tab_pane_container mT10">
            <!--<a class="expand" href="#"></a>-->
            <div class="cont_gnl tab_pane_container rcomp mT25">
                <div class="round_blue "><%=RESOURCE.UnderWriting.NewBussiness.Resources.Summary %></div>
                <div class="col-4 fl">
                    <% if (txtIllustrationNoTemp.Visible) %>
                    <% { %>
                    <div class="label_plus_input par inputDoble">
                        <% }
                       else
                       { %>
                        <div class="label_plus_input par">
                            <% } %>
                            <span><%= WEB.NewBusiness.Common.Utility.Capitalize(RESOURCE.UnderWriting.NewBussiness.Resources.QuotationOrPolicyNumberLabel) %></span>
                            <div>
                                <asp:TextBox runat="server" ReadOnly="true" Visible="false" ID="txtIllustrationNoTemp" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtIllustrationNo" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>

                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.IllustrationDate %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtIllustrationDate"></asp:TextBox>
                        </div>

                        <div class="label_plus_input par">
                            <span><%=WEB.NewBusiness.Common.Utility.Capitalize(RESOURCE.UnderWriting.NewBussiness.Resources.Office) %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtOffice"></asp:TextBox>
                        </div>

                        <div class="label_plus_input par inputDoble view_bt">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Discount %>s</span>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtDiscount" Style="width: 60% !important;"></asp:TextBox>
                                    <asp:LinkButton runat="server" ID="btnSeeDiscount" ClientIDMode="Static" OnClick="btnSeeIllustration_Click" OnPreRender="btnSeeDiscount_PreRender" Visible="true">                                        
                                    </asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSeeDiscount" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="label_plus_input par">
                            <span>Fecha Inicio vigencia</span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtEffectiveDate"></asp:TextBox>
                        </div>

                        <div class="label_plus_input par inputDoble view_bt">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Financed%></span>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox runat="server" ReadOnly="true" ID="txtFinanced" Style="width: 60% !important;"></asp:TextBox>
                                    <asp:LinkButton runat="server" ID="lnkContractView" CssClass="view_file" ClientIDMode="Static" OnClick="lnkContractView_Click" OnPreRender="lnkContractView_PreRender">                                        
                                    </asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkContractView" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <asp:Panel runat="server" ID="pnLoanNumber" class="label_plus_input par">
                            <span>No. Prestamo KCO</span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtLoanNumber"></asp:TextBox>
                        </asp:Panel>


                        <div class="label_plus_input par inputDoble" runat="server" id="dvCouponInfo" visible="false">
                            <span>Código Promocional / Porcentaje Descuento</span>
                            <div>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtCouponCode"></asp:TextBox>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtCouponDiscount"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-4 fl">    
                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.InsuredAmount %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtInsuredAmount"></asp:TextBox>
                        </div>

                        <div class="label_plus_input par">
                            <asp:Literal runat="server" ID="ltTotalPremiumWithoutTax"></asp:Literal>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtTotalPremium" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtProratedPremium" Visible="false" ClientIDMode="Static"></asp:TextBox>
                        </div>

                        <asp:Panel runat="server" ID="pnTasa" class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Rate %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtTasa"></asp:TextBox>
                        </asp:Panel>

                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.LineofBusinessLabel %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtBusinessLine" ClientIDMode="Static"></asp:TextBox>
                        </div>

                        <div class="label_plus_input par inputDoble view_bt">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Surcharge %></span>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ReadOnly="true" ID="txtSurcharge" runat="server" Style="width: 60% !important;"></asp:TextBox>
                                    <asp:LinkButton
                                        aria-describedby="ui-tooltip-4"
                                        ClientIDMode="Static"
                                        ID="btnSeeSurcharge"
                                        OnClick="btnSeeSurcharge_Click"
                                        OnPreRender="btnSeeSurcharge_PreRender"
                                        runat="server"></asp:LinkButton>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSeeSurcharge" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.EndDate %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtExpirationDate"></asp:TextBox>
                        </div>
                        <div class="label_plus_input par" runat="server" id="dvApplyDays" visible="false">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.DaysApplyLabel %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtApplyDays"></asp:TextBox>
                        </div>
                        <div class="label_plus_input par">
                            <asp:LinkButton runat="server" ID="btnRecreateLoanKCO" OnClick="btnRecreateLoanKCO_Click" Visible="false" ClientIDMode="Static" CssClass="col-2 fl button button-blue alignC embossed" OnClientClick="return DlgConfirm(this);">
                                   Recrear la Solicitud de prestamo
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="col-4 fl">
                        <div class="label_plus_input par pc">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Channel %></span>
                            <div>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtChannel"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnAgent" class="label_plus_input par inputDoble pc">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.AGENT + (ObjServices.Bandeja=="Auto" && AccidentRateVisible ? " | " + RESOURCE.UnderWriting.NewBussiness.Resources.AccidentRate:"") %></span>
                            <div>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtComercialAgent"></asp:TextBox>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtAgentAccidentRate" />
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnVendor" class="label_plus_input par inputDoble pc">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.VendorName +  (ObjServices.Bandeja=="Auto" && AccidentRateVisible? " | " + RESOURCE.UnderWriting.NewBussiness.Resources.AccidentRate:"")%></span>
                            <div>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtAgent"></asp:TextBox>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtVendorAccidentRate" />
                            </div>
                        </asp:Panel>
                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Status %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtStatus"></asp:TextBox>
                        </div>
                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.FinancialClearance %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtFinancialClearance" Style="width: 30% !important;" OnPreRender="txtFinancialClearance_PreRender"></asp:TextBox>
                            <asp:Image runat="server" ID="imgRiesgo" Style="width: 28px; height: 28px;" />
                        </div>
                        <div class="label_plus_input par">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.BlackList %></span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtBlackList" Style="width: 30% !important;" OnPreRender="txtBlackList_PreRender"></asp:TextBox>
                            <asp:Image runat="server" ID="ImgBlackList" Style="width: 28px; height: 28px;" />
                        </div>
                        <div class="label_plus_input par">
                            <span>Nivel de Riesgo</span>
                            <asp:TextBox runat="server" ReadOnly="true" ID="txtRiskLevel" Style="width: 30% !important;"></asp:TextBox>
                            <asp:Image runat="server" ID="imgRiskLevel" Style="width: 28px; height: 28px;" />
                        </div>
                    </div>
                </div>
            </div>
            <!--// END .tab_pane_container-->
        </div>
    </div>

    <asp:ModalPopupExtender ID="ModalPopupSeeDiscount" PopupControlID="pnShowDiscount" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowDiscount" BehaviorID="popupBhvr1Discount" runat="server"></asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="pnShowDiscount" CssClass="modalPopup" ClientIDMode="Static" Style="display: none;">
        <div class="pop_up_wrapper" style="width: 1002px; height: auto; overflow-x: hidden; overflow-y: hidden">
            <!--escriben por style el ancho que desean-->
            <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                <ul>
                    <li style="position: absolute; top: 31%;">
                        <asp:Label ID="ltTypeDoc2" ClientIDMode="Static" runat="server"><%=RESOURCE.UnderWriting.NewBussiness.Resources.Discount %></asp:Label>
                    </li>
                    <li style="top: 13%;">
                        <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: 0;" onclick="ClosePopDiscount();" />
                    </li>
                </ul>
            </div>
            <!--titulos_azules-->
            <uc1:UCSeeDiscount runat="server" ID="UCSeeDiscount" />
            <!--content_fondo_blanco-->
        </div>
    </asp:Panel>
    <asp:HiddenField runat="server" Value="false" ID="hdnShowDiscount" ClientIDMode="Static" />

    <%--<asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="udpQuotationPrev">
        <ContentTemplate>--%>
    <asp:ModalPopupExtender ID="ModalPopupAmortizationTable" PopupControlID="AmortizationTablePanel" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPopAmortizationTable" BehaviorID="popupBhvrQuotation" runat="server"></asp:ModalPopupExtender>
    <asp:Panel runat="server" ID="AmortizationTablePanel" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
        <div class="pop_up_wrapper" style="width: 1189px; height: 752px; overflow-x: hidden; overflow-y: hidden">
            <!--escriben por style el ancho que desean-->
            <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                <ul>
                    <li style="position: absolute; left: 41%; top: 31%;"></li>
                    <li style="top: 13%;">
                        <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopQuotationPrev()" />
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
    <asp:HiddenField runat="server" Value="false" ID="hdnShowPopAmortizationTable" ClientIDMode="Static" />
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</div>