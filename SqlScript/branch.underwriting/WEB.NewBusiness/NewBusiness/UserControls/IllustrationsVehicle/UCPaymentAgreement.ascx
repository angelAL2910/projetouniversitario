﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPaymentAgreement.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCPaymentAgreement" %>
<asp:UpdatePanel runat="server" ID="udpPaymentAgreement" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel runat="server" ID="dvPaymentAgreementForm" ClientIDMode="Static" Style="padding: 10px">
            <div style="height: 15px;"></div>
            <div class="">
                <div class="label_plus_input par">
                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.AnnualPremium %></span>
                    <asp:TextBox runat="server" ID="txtAnnualPremiun" decimal="decimal" Enabled="false"></asp:TextBox>
                </div>
                <div class="label_plus_input par sl">
                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.PaymentFrequency %></span>
                    <asp:DropDownList runat="server" ID="ddlPaymentFreq" validation="Required" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentFreq_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <asp:Panel runat="server" ID="pnDataPayment">
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Discount %></span>
                        <asp:TextBox runat="server" ID="txtDiscount" Style="text-align: left;" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.TotalPremiumDiscounts %></span>
                        <asp:TextBox runat="server" ID="txtPremiumWithDesc" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Tax %></span>
                        <asp:TextBox runat="server" ID="txtTax" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.TotalPremium %></span>
                        <asp:TextBox runat="server" ID="txtTotalPremium" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par sl">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.MinimunAmount %> (<%= RESOURCE.UnderWriting.NewBussiness.Resources.initial %>) </span>
                        <asp:TextBox runat="server" ID="txtPorcMinimum" Style="width: 25% !important;" Enabled="false"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMinimunAmount" Style="width: 25% !important;" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.AgreementAmount %></span>
                        <asp:TextBox runat="server" ID="txtAgreementAmount" Style="text-align: left;" validation="Required" decimal="decimal"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.AmountQuotas %></span>
                        <asp:TextBox runat="server" ID="txtAmomuntQuotas" Style="text-align: left;" validation="Required" decimal="decimal"></asp:TextBox>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnDataPayment2">
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Tax %></span>
                        <asp:TextBox runat="server" ID="txtTaxFinanced" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                     <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.TotalPremium %></span>
                        <asp:TextBox runat="server" ID="txtTotalPrimeFinanced" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                    <div class="label_plus_input par">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.MonthlyPayment %></span>
                        <asp:TextBox runat="server" ID="txtMonthlyPayment" Style="text-align: left;" decimal="decimal" Enabled="false"></asp:TextBox>
                    </div>
                </asp:Panel>
            </div>
            <div style="bottom: 7px; position: absolute; width: 96%;">
                <asp:Panel runat="server" ID="pnCancelPaymentAgreement" Visible="false" Style="float: left;">
                    <div class="fl" style="margin-left: 10px;">
                        <asp:LinkButton runat="server" CssClass="button button-green alignC embossed" ID="lnkCancelPaymentAgreement" OnClick="lnkCancelPaymentAgreement_Click" OnClientClick="return DlgConfirm(this);">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CancelPaymentAgreement %></span>
                        </asp:LinkButton>
                    </div>
                </asp:Panel>
                <div style="float: right;">
                    <div class="fl" style="margin-left: 10px;">
                        <asp:LinkButton runat="server" CssClass="button button-green alignC embossed" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="return validateForm('dvPaymentAgreementForm');">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Save %></span>
                        </asp:LinkButton>
                    </div>

                    <div class="fl" style="margin-left: 10px;">
                        <input type="button" class="button button-red alignC embossed" value="<%= RESOURCE.UnderWriting.NewBussiness.Resources.Cancel %>" onclick="closePopPaymentAgreement()" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>