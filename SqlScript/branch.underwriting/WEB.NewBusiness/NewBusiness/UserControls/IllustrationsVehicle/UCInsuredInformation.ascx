﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCInsuredInformation.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCInsuredInformation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCContactEditForm.ascx" TagPrefix="uc1" TagName="UCContactEditForm" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCPaymentAgreement.ascx" TagPrefix="uc1" TagName="UCPaymentAgreement" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/UCFacultyPosition.ascx" TagPrefix="uc1" TagName="UCFacultyPosition" %>

<asp:UpdatePanel runat="server" ID="udpInsuredInformation" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="cont_gnl tab_pane_container rcomp mT25">
            <div class="round_blue "><%=RESOURCE.UnderWriting.NewBussiness.Resources.ClientProfile %></div>
            <div class="col-4 fl">
                <asp:Panel CssClass="label_plus_input par" ID="pnlClientName" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.ClientName %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtClientName" ClientIDMode="Static"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnlDateofBirth" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.DateofBirthLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtDateofBirth"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnlEmailAddress" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.EmailAddressLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtEmail"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnlGender" runat="server" ClientIDMode="Static">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.GenderLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="TxtGender"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnlInvoiceType" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.NcfType %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtInvoiceType"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlMaritalStatus" runat="server" Visible="false" ClientIDMode="Static">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.MaritalStatusLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtMaritalStatus"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlProductNameA" Visible="false" runat="server">
                    <span>Producto</span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtProductNameA"></asp:TextBox>
                </asp:Panel>
            </div>
            <div class="col-4 fl">
                <asp:Panel CssClass="label_plus_input par" ID="pnlPhoneNumber" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.PhoneNumberLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPhone"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnlCellPhone" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.CellPhone %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtCellPhone"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par inputDoble" ID="pnlIDNumber" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.IDNumberLabel %></span>
                    <div>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtIdentification"></asp:TextBox>
                        <asp:TextBox runat="server" ReadOnly="true" ID="txtIdentificationType"></asp:TextBox>
                        <asp:Panel runat="server" Visible="false" ID="pnExpDate" CssClass="label_plus_input par row_A">
                            <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Expiration_Date%></span>
                            <asp:TextBox runat="server" ID="txtExpDate" ReadOnly="true"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnPaymentFreq" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.PaymentFrequency %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPaymentFreq"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlMonthlyIncome" Visible="false" runat="server">
                    <span>Ingreso Mensual</span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtMonthlyIncome"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlPostalCode" Visible="false" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.PostalCodeLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtPostalCode"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlOccupation" Visible="false" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.OccupationLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtOccupation"></asp:TextBox>
                </asp:Panel>
            </div>
            <div class="col-4 fl">
                <asp:Panel CssClass="label_plus_input par" ID="pnlCitizenship" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Citizenship %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtCitizenship"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par" ID="pnlHomeAddress" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.HomeAddressLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtAddress"></asp:TextBox>
                </asp:Panel>

                <asp:Panel CssClass="label_plus_input par" ID="pnlCountry" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.CountryLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtCountry"></asp:TextBox>
                    <div class="col-6 fr">
                        <asp:LinkButton runat="server" ID="btnEditContact" OnPreRender="btnEditContact_PreRender" ClientIDMode="Static" Style="float: right !important; width: 250px;" CssClass="button button-green alignC embossed" OnClick="btnEditContact_Click">
                             <%=RESOURCE.UnderWriting.NewBussiness.Resources.EditContactInformation %>    
                        </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPaymentAgreement" OnPreRender="lnkPaymentAgreement_PreRender" ClientIDMode="Static" Style="float: right !important; width: 250px; margin-top: 3px;" CssClass="button button-green alignC embossed" OnClick="lnkPaymentAgreement_Click">
                             <%=RESOURCE.UnderWriting.NewBussiness.Resources.PaymentAgreement %>    
                        </asp:LinkButton>
                    </div>
                </asp:Panel>

                <asp:Panel CssClass="label_plus_input par health" ID="pnlCity" Visible="false" runat="server">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.CityLabel %></span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtCity"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlSelectedDeductible" Visible="false" runat="server">
                    <span>Deductible Seleccionado</span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtSelectedDeductible"></asp:TextBox>
                </asp:Panel>
                <asp:Panel CssClass="label_plus_input par health" ID="pnlOccupationDescription" Visible="false" runat="server">
                    <span>Describa</span>
                    <asp:TextBox runat="server" ReadOnly="true" ID="txtOccupationDescription"></asp:TextBox>
                </asp:Panel>
            </div>
            <asp:Panel runat="server" ID="pnMoreInformation" Visible="false">
                <div class="row_A" style="cursor: pointer;">
                    <span id="spaMoreInf" onclick="ShowMore(this)">[+]</span>
                    <%=RESOURCE.UnderWriting.NewBussiness.Resources.MoreInformation %>
                </div>
                <fieldset class="row_A" id="fsetTransunion" style="display: none">
                    <legend><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.ContactDetailsForTransunion %></span></legend>
                    <div class="row_A">
                        <div class="col-4 fl">
                            <div class="label_plus_input par">
                                <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.CellPhone %></span>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtCelular"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4 fl">
                            <div class="label_plus_input par">
                                <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.HomePhone %></span>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtCasa"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-4 fl">
                            <div class="label_plus_input par">
                                <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.WorkPhone %></span>
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtTrabajo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="label_plus_input row_A direcc">
                            <div class="col-4 fl">
                                <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.AddresessLabel%></span>
                            </div>
                            <div class="dir">
                                <asp:TextBox runat="server" TextMode="MultiLine" ReadOnly="true" ID="txtDireccion" CssClass="mB20"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                </fieldset>
            </asp:Panel>
        </div>
        <asp:ModalPopupExtender ID="mpeContactEditPop" PopupControlID="pnContactEditPop" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPopContactEdit" BehaviorID="popupBhvrContactEditPop" runat="server"></asp:ModalPopupExtender>

        <asp:Panel runat="server" ID="pnContactEditPop" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper" style="width: 1233px; height: 100%; overflow-x: hidden; overflow-y: hidden">
                <!--l-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 36%; top: 31%;">
                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.EditContactInformation %> 
                        </li>
                        <li style="top: 13%;">
                            <input type="button" class="cls_pup" style="background-color: transparent; border-color: transparent;" onclick="closePopEditContact();" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:UCContactEditForm runat="server" ID="UCContactEditForm" />
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>
        <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnShowPopContactEdit" />

        <asp:ModalPopupExtender ID="mpePaymentAgreementPop" PopupControlID="pnPaymentAgreementPop" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPopPaymentAgreementPop" BehaviorID="popupBhvrPaymentAgreementPop" runat="server"></asp:ModalPopupExtender>

        <asp:Panel runat="server" ID="pnPaymentAgreementPop" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper" style="width: 541px; height: 465px; overflow-x: hidden; overflow-y: hidden">
                <!--l-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li style="position: absolute; left: 40%; top: 31%;">
                            <%=RESOURCE.UnderWriting.NewBussiness.Resources.PaymentAgreement %> 
                        </li>
                        <li style="top: 13%;">
                            <input type="button" class="cls_pup" style="background-color: transparent; border-color: transparent;" onclick="closePopPaymentAgreement()" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:UCPaymentAgreement runat="server" ID="UCPaymentAgreement" />
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>

        <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnShowPopPaymentAgreementPop" />
        <asp:Button runat="server" ID="btnCerrarPOP" Style="display: none" ClientIDMode="Static" OnClick="btnCerrarPOP_Click" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnEditContact" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="lnkPaymentAgreement" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>