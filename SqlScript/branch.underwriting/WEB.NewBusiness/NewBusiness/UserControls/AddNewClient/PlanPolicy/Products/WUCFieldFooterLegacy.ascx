﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFieldFooterLegacy.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.PlanPolicy.Products.WUCFieldFooterLegacy" %>
<asp:UpdatePanel runat="server" ID="udpFieldFooter">
    <ContentTemplate>

        <asp:Panel runat="server" ID="pnFooter" class="col-1-1" ClientIDMode="Static">
            <div class="barra_azul_celeste">
                <div class="grupos de_7 alingtoend">
                    <div>
                        <label class="label" runat="server" id="TotalInsuredAmount">Total Insured Amount</label>
                        <asp:TextBox ID="txtInsuredAmount" runat="server" ClientIDMode="Static" TabIndex="0" decimal="decimal"  AllowEnter="false" validation="Required"></asp:TextBox>
                    </div>                   
                    <div>
                        <label class="label" runat="server" id="PeriodicPremium">Periodic Premium</label>
                        <asp:TextBox ID="txtPeriodicPremium" onchange="GetAnnualPremium('txtPeriodicPremium','ddlFrequencyofPayment')" AllowEnter="false" runat="server" ClientIDMode="Static" TabIndex="3" decimal="decimal"  validation="Required"></asp:TextBox>
                    </div>                    
                    <div>
                        <label class="label" runat="server" id="TargetAnnualPremium">Target Annual Premium</label>
                        <asp:TextBox ID="txtTargetAnnualPremium" runat="server" ClientIDMode="Static" decimal="decimal" TabIndex="4"  AllowEnter="false" validation="Required"></asp:TextBox>
                    </div>
                    <div>
                        <label class="label" runat="server" id="MinimumAnnualPremium">Minimum Annual Premium</label>
                        <asp:TextBox ID="txtMinimumAnnualPremium" runat="server" ClientIDMode="Static" TabIndex="5" decimal="decimal" AllowEnter="false" validation="Required"></asp:TextBox>
                    </div>
                     <div>
                        <label class="label" runat="server" id="AnnualPremium">Annual Premium</label>
                        <asp:TextBox ID="txtAnnualPremium" Enabled="false"  runat="server" ClientIDMode="Static" decimal="decimal" AllowEnter="false" TabIndex="1"  validation="Required"></asp:TextBox>
                    </div>
                    <div>
                        <label class="label" runat="server" id="SelectiveTax">Selective Tax Insurance</label>
                        <asp:TextBox ID="txtSelectiveTax" runat="server" TabIndex="3" ClientIDMode="Static" ReadOnly="true" AllowEnter="false" decimal="decimal" ></asp:TextBox>
                    </div>
                    <div>
                        <label class="label" runat="server" id="AnnualPremiumWithTax">Annual Premium With Tax</label>
                        <asp:TextBox ID="txtAnnualPremiumWithTax" runat="server" TabIndex="3" ClientIDMode="Static" ReadOnly="true" AllowEnter="false" decimal="decimal" ></asp:TextBox>
                    </div>
                </div>
                <!--grupos-->
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="hdAnnualPremium" runat="server"  ClientIDMode="Static"  />