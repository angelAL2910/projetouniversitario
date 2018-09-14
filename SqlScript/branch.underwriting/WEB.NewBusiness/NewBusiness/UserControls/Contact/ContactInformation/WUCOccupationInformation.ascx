﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCOccupationInformation.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Contact.ContactInformation.WUCOccupationInformation" %>
<asp:UpdatePanel runat="server" ID="udpOccupationInformation">
    <ContentTemplate>
        <div class="titulos_azules"><span class="icon_occupation"></span><strong runat="server" id="OccupationInformation">OCCUPATION INFORMATION</strong></div>
        <div class="content_fondo_blanco" id="frmPersonalInformation2">
            <div class="grupos de_2">
                <div>
                    <label class="label" runat="server" id="YearlyPersonalIncome">Yearly Personal Income</label>
                    <asp:TextBox runat="server" ID="txtPersonalIncome" decimal="decimal" ClientIDMode="Static" data-inputmask="'alias': 'decimal', 'groupSeparator': ',', 'autoGroup': true" />
                </div>
                <div>
                    <label class="label" runat="server" id="YearlyFamilyIncome">Yearly Family Income</label>
                    <asp:TextBox runat="server" ID="txtYearlyFamilyIncome" decimal="decimal" ClientIDMode="Static" data-inputmask="'alias': 'decimal', 'groupSeparator': ',', 'autoGroup': true" />
                </div>


                <div>
                    <label class="label" runat="server" id="Occupation">Occupation</label>
                    <div id="divOccupation">
                        <asp:TextBox runat="server" ID="txtOccupation" ClientIDMode="Static"></asp:TextBox>
                    </div>
                    <div class="wrap_select" style="display: none">
                        <asp:DropDownList runat="server" ID="ddl_WUC_PI_Occupation" ClientIDMode="Static"></asp:DropDownList>
                    </div>
                    <!--wrap_select-->
                </div>
                <div>
                    <label class="label" runat="server" id="OccupationType">Occupation Type</label>
                    <div>
                        <asp:TextBox runat="server" ID="txtProfession" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                    </div>
                    <!--wrap_select-->
                </div>
            </div>
            <!--grupos-->

            <div class="grupos de_1">
                <div>
                    <label class="label" runat="server" id="CompanyName">Company Name</label>
                    <asp:TextBox runat="server" ID="txtCompanyName" />
                </div>
            </div>
            <!--grupos-->

            <div class="grupos de_2">
                <div>
                    <label class="label" runat="server" id="LineofBusiness1">Line of Business 1</label>
                    <div class="wrap_select">
                        <asp:DropDownList runat="server" ID="ddl_BusinessLine2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddl_BusinessLine2_SelectedIndexChanged"></asp:DropDownList>
                        <asp:TextBox runat="server" ID="txt2ndLineOfBussines" Visible="false" />
                    </div>
                </div>
                <div>
                    <label class="label" runat="server" id="LineofBusiness2">Line of Business 2</label>
                    <div class="wrap_select">
                        <asp:DropDownList runat="server" ID="ddl_BusinessLine1" ClientIDMode="Static" AutoPostBack="false"></asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtFirstLineOfBussines" Visible="false" />
                    </div>
                </div>
            </div>
            <!--grupos-->

            <div class="grupos de_1">
                <div>
                    <label class="label" runat="server" id="TaskPerformed">Task Performed</label>
                    <asp:TextBox runat="server" ID="txtTaskPerformed" />
                </div>
            </div>
            <!--grupos-->

            <fieldset class="margin_t_15">
                <legend runat="server" id="LengthatWork">Length at Work:</legend>
                <div class="grupos de_2">
                    <div>
                        <label class="label" runat="server" id="Years">Years</label>
                        <div class="wrap_select">
                            <asp:DropDownList runat="server" ID="ddlYears">
                            </asp:DropDownList>
                        </div>
                        <!--wrap_select-->
                    </div>
                    <div>
                        <label class="label" runat="server" id="Months">Months</label>
                        <div class="wrap_select">
                            <asp:DropDownList runat="server" ID="ddlMonths">
                            </asp:DropDownList>
                        </div>
                        <!--wrap_select-->
                    </div>
                </div>
                <!--grupos-->
                <fieldset class="margin_t_15" id="AdditionalInformationContainer" style="display: none">
                </fieldset>
            </fieldset>
        </div>
        <asp:HiddenField ID="hdnOccupationId" runat="server" Value="" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnOccupationGroupId" runat="server" Value="" ClientIDMode="Static" />
    </ContentTemplate>      
</asp:UpdatePanel>