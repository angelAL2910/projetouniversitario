﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCNewRisk.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.PolicyPlanData.UCNewRisk" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/Case/UserControls/PolicyPlanData/UCNewCredit.ascx" TagPrefix="uc1" TagName="UCNewCredit" %>

<div class="cont_risk_popup ">
    <asp:UpdatePanel ID="upNewRisk" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlRiderType" runat="server" CssClass="wd100 fl mB20 lh27">
                <label class="wd16-1 fl pdR alignR">RIDER TYPE:</label>
                <asp:DropDownList runat="server" ID="ddlPopNRiderType" CssClass="wd83-7 fl m0" ClientIDMode="Static"></asp:DropDownList>
            </asp:Panel>

            <div class="wd100 fl mB">
                <span class="label ttl"><strong>RATE REASON</strong></span>
            </div>

            <div class="wd100 fl mB25 lh27">
                <label class="wd16-3 fl pdR alignR">RISK TYPE:</label>
                <asp:DropDownList runat="server" ID="ddlPopNRiskType" CssClass="wd83-7 fl m0" ClientIDMode="Static" OnSelectedIndexChanged="ddlPopNRiskType_SelectedIndexChanged" ></asp:DropDownList>
            </div>

            <div class="wd100 fl mB25 lh27">
                <label class="wd16-3 fl pdR alignR">CATEGORY:</label>
                <asp:DropDownList runat="server" ID="ddlPopNRiskCategory" CssClass="wd83-7 fl m0" ClientIDMode="Static" OnSelectedIndexChanged="ddlPopNRiskCategory_SelectedIndexChanged" ></asp:DropDownList>
            </div>

                <div class="wd100 fl mB25 lh27 prl">
                    <label class="wd16-3 fl pdR alignR">CONDITION TYPE:</label>
                    <asp:DropDownList runat="server" ID="ddlPopNRiskConditionType" CssClass="wd75 fr m0" Visible ="false"  ClientIDMode="Static" OnSelectedIndexChanged="ddlPopNRiskConditionType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                     <%-- Bmarroquin 04-03-2017 cambio el campo CONDITION TYPE ahora es abierto  --%>
                    <asp:TextBox ID="txtPopNRiskConditionType" runat="server" ClientIDMode="Static" CssClass="wd83-7 fl m0 hg140" ></asp:TextBox>
                </div>


            <div class="wd100 fl mB25 lh27 prl">
                <label class="wd16-3 fl pdR alignR">REASON:</label>
                <%--  <dx:ASPxComboBox ID="ddlPopNRiskReason" ClientInstanceName="ddlPopNRiskReason" ClientEnabled="true" runat="server" CssClass="wd83-7 fl m0 select_dev" DropDownStyle="DropDown" IncrementalFilteringMode="Contains"
                    TextField="Text" ValueField="Value" OnSelectedIndexChanged="ddlPopNRiskReason_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" />--%>
                <asp:DropDownList ID="ddlPopNRiskReason" runat="server" CssClass="wd83-7 fl m0 select_dev" TextField="Text" ValueField="Value"
                    OnSelectedIndexChanged="ddlPopNRiskReason_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" Visible ="false" />
                 <%-- Bmarroquin 04-03-2017 cambio el campo CONDITION TYPE ahora es abierto  --%>
                 <asp:TextBox ID="txtPopNRiskReason" runat="server" ClientIDMode="Static" CssClass="wd83-7 fl m0 hg140" ></asp:TextBox>
            </div>

            <div class="wd100 fl mB10 lh27 prl">
                <%-- Bmarroquin 04-03-2017 cambio el Label --%>
                <label class="wd16-3 fl pdR alignR">COMMENTS:</label>
                <asp:TextBox ID="txtPopNRiskSuggestedRaiting" name ="txtPopNRiskSuggestedRaiting" runat="server" CssClass="wd83-7 fl m0 hg150" ClientIDMode="Static" TextMode="MultiLine" /> <%-- Bmarroquin 04-03-2017 se le quita el ReadOnly--%>
            </div>

            <asp:Panel ID="pnlPopNRiskAddReason" CssClass="wd100 fl hg35 clear mB10" runat="server" Visible="false">
                <div class="boton_wrapper gradient_AM_btn bdrAM txtNG-f fr mR">
                    <span class="add"></span>
                    <asp:Button ID="btnPopNRiskAddReason" runat="server" ClientIDMode="Static" CssClass="boton" Text="ADD REASON" OnClick="btnPopNRiskAddReason_Click"></asp:Button>
                </div>
            </asp:Panel>

            <%-- Bmarroquin 04-03-2017 Oculto el grid  --%>
            <div class="data_G tbl em1 mB25" style="display:none;">
                <asp:GridView ID="gvPopNRisks" DataKeyNames="RiskGroupId, RiskDetId, RiskTypeId, PageId, GridId, ElementId, ColumnId"
                    runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" ClientIDMode="Static">
                    <Columns>
                        <asp:TemplateField HeaderText="Risk Type">
                            <ItemTemplate>
                                <asp:Label ID="RiskType" runat="server" Text='<%# Eval("RiskTypeDesc ") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="Category" runat="server" Text='<%# Eval("CategoryDesc ") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Condition Type">
                            <ItemTemplate>
                                <asp:Label ID="ConditionType" runat="server" Text='<%# Eval("ConditionTypeDesc ") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="Reason" runat="server" Text='<%# Eval("ReasonDesc ") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remove">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="upNewRiskGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnRemove" runat="server" CssClass="delete_file" OnClientClick="return DlgConfirm(this);" ClientIDMode="Static" OnClick="btnRemove_Click"></asp:Button>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnRemove" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No data to display
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                    <HeaderStyle CssClass="gradient_azul" />
                </asp:GridView>
            </div>

            <div class="wd100 fl mB2 clear">
                <%-- Bmarroquin 04-03-2017 Oculto el div  --%>
                <div class="RiskTRaiting wd23-5 fl mR-2-p radio pdR-20" style="display:none;">
                    <asp:RadioButton ID="rBtnPopNRiskTableRating" runat="server" ClientIDMode="Static"
                        GroupName="mt" OnClick="EnableRatingDDL(this);" TextAlign="Left"></asp:RadioButton>
                    <label for="rBtnPopNRiskTableRating" class="fl radio_in" alt="ddlPopNRiskTableRating">TABLE RATING</label>
                    <asp:DropDownList runat="server" ID="ddlPopNRiskTableRating" CssClass="mT10 fl" ClientIDMode="Static" TextField="Text" ValueField="Value" Visible="false"></asp:DropDownList>

                </div>
                <%-- Bmarroquin 04-03-2017 Oculto el div  --%>
                <div class="RiskPThousand wd23-5 fl mR-2-p radio pdR-20" style="display:none;">
                    <asp:RadioButton ID="rBtnPopNRiskPerThousand" runat="server" ClientIDMode="Static"
                        GroupName="mt" OnClick="EnableRatingDDL(this);" TextAlign="Left"></asp:RadioButton>
                    <label for="rBtnPopNRiskPerThousand" class="fl radio_in" alt="ddlPopNRiskPerThousand">
                        PER THOUSAND</label>
                    <asp:DropDownList runat="server" ID="ddlPopNRiskPerThousand" CssClass="mT10 fl" ClientIDMode="Static" TextField="Text" ValueField="Value"  Visible="false"></asp:DropDownList>
                </div>
 
                <div class="wd23-5 fl mR-2-p">
                    <span class="fl mB">TABLE RATING:</span>
                    <asp:TextBox ID="txtPopNRiskTableRating" runat="server" ClientIDMode="Static" CssClass="IntegerFormat" MaxLengt="8" Visible="false"></asp:TextBox> <%-- Bmarroquin 07-03-2017 Cambio de Text Box a DropDownList  --%>
                    <asp:DropDownList runat="server" ID="ddlTableRatingRisk" CssClass="mT2" ClientIDMode="Static" TextField="Text" ValueField="Value"></asp:DropDownList>
                </div>
                <div class="wd23-5 fl">
                    <span class="fl mB">PER THOUSAND:</span>
                    <asp:TextBox ID="txtPopNRiskPerThousand" runat="server" ClientIDMode="Static" CssClass="DecimalFormat" MaxLengt="8"></asp:TextBox>
                </div>

                <div class="wd23-5 fl mR-2-p">
                    <span class="fl mB">DURATION (YEARS):</span>
                    <asp:TextBox ID="txtPopNRiskDuration" runat="server" ClientIDMode="Static" CssClass="DecimalOneDigit" MaxLengt="4"></asp:TextBox>
                </div>
                <div class="wd23-5 fl">
                    <span class="fl mB">YEARS TO RECONSIDER:</span>
                    <asp:TextBox ID="txtPopNRiskReconsider" runat="server" ClientIDMode="Static" CssClass="DecimalOneDigit" MaxLengt="4"></asp:TextBox>
                </div>
            </div>
            <div class="wd100 fl hg35 clear">
                <div class="boton_wrapper gradient_vd2 bdrVd2 txtNG-f fr mR">
                    <span class="save"></span>
                    <asp:Button ID="btnPopNRiskAdd" runat="server" ClientIDMode="Static" CssClass="boton" Text="Save" OnClick="btnPopNRiskAdd_Click" OnClientClick="return ValidateNewRisk();"></asp:Button>
                </div>
            </div>

            <asp:HiddenField ID="hdnPopNRiskFillDrop" Value="" runat="server" />
            <asp:HiddenField ID="hdnNRIsAditional" Value="false" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnNRContactId" runat="server" ClientIDMode="Static" Value="false" />
            <asp:HiddenField ID="hdnNRContactRoleTypeId" runat="server" ClientIDMode="Static" Value="false" />

            <asp:HiddenField ID="hdnNRAge" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnNRSex" runat="server" ClientIDMode="Static" />

            <asp:HiddenField ID="hdnNRIsEdit" runat="server" ClientIDMode="Static" />
             <%-- Bmarroquin 05-03-2017 Agrego 2 Nuevos HiddenField  --%>
            <asp:HiddenField ID="hdnRiksID" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnSequenceRef" runat="server" ClientIDMode="Static" />
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPopNRiskCategory" />
            <asp:AsyncPostBackTrigger ControlID="ddlPopNRiskConditionType" />
            <asp:AsyncPostBackTrigger ControlID="ddlPopNRiskType" />
            <asp:AsyncPostBackTrigger ControlID="ddlPopNRiskReason" />
            <asp:AsyncPostBackTrigger ControlID="gvPopNRisks" />
        </Triggers>
    </asp:UpdatePanel>
</div>


