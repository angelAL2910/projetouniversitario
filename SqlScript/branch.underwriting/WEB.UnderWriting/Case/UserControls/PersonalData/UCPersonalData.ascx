﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPersonalData.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.PersonalData.UCPersonalData" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>

<ul class="secundario" style="display: block;">
    <li class="pdL-10 pdata">
        <asp:UpdatePanel ID="upPersonalData" runat="server">
            <ContentTemplate>
                <div class="col-1-2 mT5">
                    <div class="grupo_de_cuatro">
                        <ul class="list_campos">
                            <li class=" mR-2-p">
                                <label runat="server">
                                    ROLE:</label>
                                <asp:DropDownList runat="server" ID="RoleDDL" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="RoleDDL_SelectedIndexChanged"></asp:DropDownList>
                            </li>
                            <li class="">
                                <label runat="server">
                                    TYPE:</label>
                                <asp:TextBox ID="TypeTxt" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </li>
                            <div id="InsuredAgentLegal" runat="server">
                                <li class="mR-2-p">
                                    <label class="ReqField" runat="server">
                                        FIRST NAME:
                                    </label>
                                    <asp:TextBox ID="FirstNameTxt" data-group="OK" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:TextBox>
                                </li>
                                <li class="">
                                    <label runat="server">
                                        Middle Name:</label>
                                    <asp:TextBox ID="MiddleTxt" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </li>
                                <li class="mR-2-p">
                                    <label class="ReqField" runat="server">
                                        LAST NAME:</label>
                                    <asp:TextBox ID="LastNameTxt" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:TextBox>
                                </li>
                                <li class="">
                                    <label>
                                        2nd LAST NAME:</label>
                                    <asp:TextBox ID="SecondLastNameTxt" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </li>
                            </div>
                            <li class=" wd100">
                                <div id="updateCompanyinfo" runat="server">

                                    <label class="label ReqField" runat="server" id="companyName">Company Name:</label>
                                    <asp:TextBox ID="txtInstitutionalNameUpdate" runat="server"></asp:TextBox>

                                    <div class=" wd49 mR-2-p fl mT10 ">
                                        <label class="label" runat="server" id="SocietyType">Society Type</label>
                                        <asp:DropDownList ID="ddlSocietyType" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="wd49 fl mT10 ">
                                        <label class="label" runat="server" id="CompanyActivity">Commercial Activity</label>
                                        <asp:DropDownList ID="ddlCommercialActivity" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class=" wd49 mR-2-p fl mT10 ">
                                        <label class="label ReqField" runat="server" id="IDType">ID Type</label>
                                        <asp:DropDownList runat="server" ID="ddlIDType" validation="Required" onchange="SetBirthCertificate(this);"></asp:DropDownList>
                                    </div>
                                    <div class="wd49 fl mT10 ">
                                        <label class="label ReqField" runat="server" id="CompanyRNC">Company ID</label>
                                        <asp:TextBox ID="txtCompanyRNC" runat="server" ClientIDMode="Static" validation="Required" data-inputmask="'mask': '999-99999-9','clearMaskOnLostFocus': true,'showTooltip': true" />
                                    </div>
                                    <div class=" wd49 mR-2-p fl mT10 ">
                                        <label class="label ReqField" id="DateBirdth" runat="server">Date of Birth:</label>
                                        <label class="label ReqField" id="DateCompany" runat="server">Registration Date:</label>
                                        <asp:TextBox ID="DOBTxt" runat="server" CssClass="datepickerDOB validationElement" ClientIDMode="Static" alt="validateFutureDate"></asp:TextBox>
                                    </div>
                                    <div class="wd49 fl mT10 ">
                                        <label class="label" runat="server" id="RegistrationNumber">Registration Number</label>
                                        <asp:TextBox ID="txtRegistrationNumberDO" runat="server" ClientIDMode="Static" />
                                    </div>
                                    <div class="wd49 mR-2-p fl mT10 ">
                                        <label class="label" runat="server" id="SocietyFinalBeneficiary">Final Beneficiary</label>
                                        <asp:DropDownList ID="ddlSocietyFinalBeneficiary" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div id="EdadFuma" runat="server">
                                    <div class=" wd49 fl">
                                        <div class=" wd20 fl mR-2-p">
                                            <label class="label">
                                                Age:</label>
                                            <asp:TextBox ID="AgeTxt" runat="server" ClientIDMode="Static" CssClass="NFormat" MaxLength="3" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class=" wd34 fl mR-2-p">
                                            <label class="label">
                                                Near age:
                                            </label>
                                            <asp:TextBox ID="NearAgeTxt" runat="server" ClientIDMode="Static" CssClass="NFormat" MaxLength="3" ReadOnly="true"></asp:TextBox>
                                        </div>
                                        <div class=" wd42 fl">
                                            <label class="label ReqField">
                                                Smoker:</label>
                                            <asp:DropDownList ID="SmokerDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <div id="GeneroEstado" runat="server">
                                <li class="mR-2-p">
                                    <label class="ReqField">
                                        GENDER:</label>
                                    <asp:DropDownList ID="GenderDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                </li>
                                <li class="">
                                    <label class="ReqField">
                                        Marital Status:</label>
                                    <asp:DropDownList ID="MaritalStatusDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                </li>
                            </div>



                            <%--2016-01-29 | Marcos J. Perez--%>
                            <div id="divMeasurements" runat="server">
                                <li class="fl mR-1-p">
                                    <div class="wd100 fl">
                                        <div class="wd32 fl mR-1-p">
                                            <label class="label">
                                                WEIGHT:
											<asp:TextBox ID="txtWeight" runat="server" onblur="CalculoBMI()" Style="text-align: right;" ClientIDMode="Static" />
                                            </label>
                                        </div>
                                        <div class="wd32 fl mR-1-p">
                                            <label class="label">
                                                HEIGHT:
											<asp:TextBox ID="txtHeight" runat="server" onblur="CalculoBMI()" Style="text-align: right;" ClientIDMode="Static" />
                                            </label>
                                        </div>
                                        <div class="wd32 fl mR-1-p">
                                            <label class="label">
                                                BMI:</label>
                                            <asp:TextBox ID="txtBMI" runat="server" ReadOnly="true" Style="text-align: right;" ClientIDMode="Static" />
                                        </div>

                                        <%--2016-02-12 | Marcos J. Perez--%>
                                        <script type="text/javascript">
                                            function CalculoBMI() {
                                                var height = $('#<%= txtHeight.ClientID %>').val(),
													weight = $('#<%= txtWeight.ClientID %>').val(),
													bmi = BMI(height, weight, false);

                                                $('#<%= txtBMI.ClientID %>').val(bmi);
											}
                                        </script>
                                    </div>
                                </li>
                            </div>
                            <div id="InsuredAgentLegal2" runat="server">
                                <li class="mR-1-p" id="liCountryResidence">
                                    <label class="ReqField">
                                        Country of Residence:</label>
                                    <asp:DropDownList ID="CountryOfResidenceDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"
                                        OnSelectedIndexChanged="CountryOfResidenceDDL_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </li>
                                <div id="StateCity" runat="server">
                                    <li class="mR-1-p" id="liStateResidence">
                                        <label class="ReqField">
                                            State of Residence:</label>
                                        <asp:DropDownList ID="ProvinceOfResidenceDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"
                                            AutoPostBack="true" OnSelectedIndexChanged="ProvinceOfResidenceDDL_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </li>
                                    <li class="mR-1-p" id="liCityResidence">
                                        <label class="ReqField">
                                            City of Residence:</label>
                                        <asp:DropDownList ID="CityOfResidenceDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                    </li>
                                </div>
                                <li class="mR-2-p" id="liCountryCitizenship">
                                    <label>Country of Citizenship:</label>
                                    <asp:DropDownList ID="CountryOfCitizenShipDDL" runat="server" class="coc" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <asp:Button ID="AddCitizenshipCountry" runat="server" title="ADD COUNTRY" class="add_btn_redond tooltip fr" ClientIDMode="Static" OnClick="InsertCitizenCountry" OnClientClick="return ValidateCitizenshipCountry();" />
                                    <%--<button class="add_btn_redond tooltip fr" href="#" title="ADD COUNTRY" id="AddCitizenshipCountry"></button>--%>
                                </li>
                                <li class="">
                                    <label class="ReqField">
                                        Country of Birth:</label>
                                    <asp:DropDownList ID="CountryOfBirthDDL" runat="server" class="coc" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                </li>
                            </div>
                            <!-- Tabla -->
                            <li class=" wd100 mB20">
                                <div class="tbl tbl-1">
                                    <asp:GridView ID="gvCitizenShip" runat="server" border="0" CellSpacing="0" CellPadding="0"
                                        AutoGenerateColumns="false" DataKeyNames="ContactId,GlobalCountryId,CreateUser,ModifyUser">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Country of Citizenship" HeaderStyle-CssClass="gradient_azul">
                                                <ItemTemplate>
                                                    <div class="c1" runat="server" id="DVCountry"><%# Eval("GlobalCountryDesc") %></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gradient_azul">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Button runat="server" class=" delete_file" OnClick="DeleteCitizenCountry" OnClientClick="return DlgConfirm(this);" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </li>
                            <li class=" wd100 mB20">
                                <div class="tbl tbl-1">
                                    <asp:GridView ID="grdDocs" runat="server" border="0" CellPadding="0" AutoGenerateColumns="false" DataKeyNames="ContactIdTypeDescription,Id">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Document" HeaderStyle-CssClass="gradient_azul">
                                                <ItemTemplate>
                                                    <div class="c1" runat="server" id="DVDoc"><%# Eval("ContactIdTypeDescription") %></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No." HeaderStyle-CssClass="gradient_azul">
                                                <ItemTemplate>
                                                    <div class="c1" runat="server" id="DVDocNo"><%# Eval("Id") %></div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </li>
                            <!--// Tabla -->

                            <!-- Tabla 2 -->
                            <div class="dvQuestions">
                                <li class=" wd100">
                                    <div class="tbl-2">
                                        <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="false" ClientIDMode="Static">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <td align="left" class="c1">
                                                            <label>- <%# Eval("CitizenQuestDesc") %></label>
                                                        </td>
                                                        <td align="left" class="c2 radiogroup">
                                                            <ul class="radio ">
                                                                <li class="li_si">
                                                                    <asp:RadioButton
                                                                        ID="s1" runat="server"
                                                                        Checked='<%# ((bool?)Eval("CitizenQuestAnswer")).HasValue && (((bool?) Eval("CitizenQuestAnswer")).Value ? true : false) %>'
                                                                        data-question='<%# Eval("CitizenQuestId") %>' CssClass="CitizenQuesChk" />
                                                                    <label for="s1" class='<%# ((bool?)Eval("CitizenQuestAnswer")).HasValue ? ((bool?)Eval("CitizenQuestAnswer")).Value ?"fr mT5 radio_in radioSelect"  : "fr mT5 radio_in" : "fr mT5 radio_in" %>'>Yes</label>
                                                                    <asp:HiddenField ID="hdfRadio_si" runat="server" Value='<%# ((bool?)Eval("CitizenQuestAnswer")).HasValue && (((bool?)Eval("CitizenQuestAnswer")).Value ? true : false) %>' />
                                                                </li>
                                                                <li class="li_no">
                                                                    <asp:RadioButton
                                                                        ID="n1" runat="server"
                                                                        Checked='<%# ((bool?)Eval("CitizenQuestAnswer")).HasValue && !((bool?)Eval("CitizenQuestAnswer")).Value %>'
                                                                        CssClass="CitizenQuesChk" />
                                                                    <label for="n1" class='<%# ((bool?)Eval("CitizenQuestAnswer")).HasValue?((bool?)Eval("CitizenQuestAnswer")).Value ? "fr mT5 radio_in" : "fr mT5 radio_in radioSelect" : "fr mT5 radio_in" %>'>NO</label>
                                                                    <asp:HiddenField ID="hdfRadio_no" runat="server" Value='<%# ((bool?)Eval("CitizenQuestAnswer")).HasValue && !((bool?)Eval("CitizenQuestAnswer")).Value %>' />
                                                                </li>
                                                            </ul>
                                                        </td>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </li>
                            </div>
                        </ul>
                    </div>
                </div>

                <div class="col-1-2 mT5">
                    <div id="TabbedPanels2" class="TabbedPanels">
                        <ul class="TabbedPanelsTabGroup bdr0">
                            <div class="con_previewBTN2">
                                <asp:Button ID="UCPersonalDataPreviewBtn" CssClass="previewBTN" runat="server" Text="Preview" OnClick="UCPersonalDataPreviewBtn_Click" />
                            </div>
                            <asp:Repeater ID="BigPdfRepeater" runat="server">
                                <ItemTemplate>
                                    <li class="TabbedPanelsTab bdr0  last-child mR-8" tabindex="0" style="padding: 0; width: 27px;">
                                        <asp:Button ID="Index" runat="server" Text='<%# Container.ItemIndex + 1  %>' OnClick="LoadPdf_Click" data-PdfKey='<%# Eval("DocumentCategoryId") +"|"+ Eval("DocumentTypeId") +"|"+ Eval("DocumentId")%>' Style="padding: 4px 10px;" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div class="TabbedPanelsContentGroup">
                            <div class="TabbedPanelsContent TabbedPanelsContentVisible" style="display: block;">
                                <PdfViewer:PdfViewer CssClass="imgid" ID="pdfViewerPersonalData" runat="server" ClientIDMode="Static" ShowScrollbars="true"
                                    ShowToolbarMode="Show" ShowNavigationPanel="false">
                                    <PdfViewerPreferences />
                                </PdfViewer:PdfViewer>
                            </div>
                        </div>
                    </div>
                    <div class="grupo_de_cuatro">
                        <ul class="list_campos">
                            <%--Seccion de Tipo de documento Personal--%>
                            <li class=" wd100">
                                <div class=" wd49 fl mR-2-p">
                                    <label class="label mT10">ID Type:</label>
                                    <asp:DropDownList ID="IdtypeDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                    <label class="label mT22">
                                        ID Expiration Date:</label>

                                    <asp:TextBox ID="IdExpirationDateTxt" runat="server" class="datepicker" ClientIDMode="Static"></asp:TextBox>

                                </div>
                                <div class=" wd49 fl ">
                                    <%--<label class="label mT10">ID:</label>--%>
                                    <label class="label mT10">Identification Number:</label>
                                    <asp:TextBox ID="idTxt" runat="server" ClientIDMode="Static" data-inputmask="'clearMaskOnLostFocus': true,'showTooltip': true"></asp:TextBox>
                                    <label class="label mT22">ISSUED BY:</label>
                                    <asp:DropDownList ID="IssuedByDDL" runat="server" ClientIDMode="Static" CssClass="validationElement"></asp:DropDownList>
                                </div>
                            </li>
                            <%--Fin Seccion de Tipo de documento Personal--%>
                            <li class="titulo_list ">
                                <div class="wd49">
                                    <label>
                                        <strong>Background Check Results</strong></label>
                                    <asp:TextBox ID="BackgroundCheckTxt" runat="server" class="bgVD2 txtBL ReadOnly" ReadOnly="true"></asp:TextBox>
                                </div>
                            </li>
                            <li class="titulo_list ">
                                <%--mavelar 02/04/2017 --%>

                                <div class="wd49 mR-2-p fl">
                                    <%--CssClass="ReqField"--%>
                                    <asp:Label ID="lblRelationshipToOwner" runat="server" Text="Relationship to Owner:" Style="font-weight: bold;"></asp:Label>
                                    <%--CssClass="validationElement"--%>
                                    <asp:DropDownList ID="RelationshipOwnerDDL" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </div>


                                <div class="wd49 fl">
                                    <%--class="ReqField"--%>
                                    <label>
                                        Relationship to Agent:
                                    </label>
                                    <%--CssClass="validationElement"--%>
                                    <asp:DropDownList ID="RelationshipToAgentDDL" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </div>


                            </li>
                        </ul>
                    </div>
                </div>
                <div id="InfoCompany" runat="server">
                    <div class="list_addresses">
                        <div class="titulo_list ">
                            <div class="wd49">
                                <div class="mB10" style="font-size: 8pt;"><strong>COMPANY INFORMATION:</strong></div>
                            </div>
                        </div>
                        <div class=" wd100 mB fl">
                            <div class="fl wd32 mR-2-p">
                                <label class="label">
                                    Company Name:</label>
                                <asp:TextBox ID="txtInstitutionalName" runat="server"></asp:TextBox>
                            </div>

                            <div class="fl wd32">
                                <label class="label">
                                    Registration Date:</label>
                                <asp:TextBox ID="txtRegistrationDate" runat="server"></asp:TextBox>
                            </div>

                        </div>
                        <%--mavelar--%>
                        <div class=" wd100 mB fl" style="display: none;">

                            <div class="fl wd32 mR-2-p">
                                <label class="label">

                                    <%--Registration Number--%> Company Registration (NRF):</label>
                                <asp:TextBox ID="txtRegistrationNumber" runat="server"></asp:TextBox>
                            </div>
                            <div class="fl wd32">
                                <label class="label">
                                    Company NIT:</label>
                                <asp:TextBox ID="txtCompanyNIT" runat="server"></asp:TextBox>
                            </div>
                        </div>


                        <div class=" wd100 mB fl" style="display: none;">
                            <div class="fl wd32 mR-2-p">
                                <label class="label">
                                    Company Principal (Contact Person):</label>
                                <asp:TextBox ID="txtInstitutionalPrincipal" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="fl wd32 mR-2-p">
                                <label class="label">
                                    Country:</label>
                                <asp:TextBox ID="txtInstitutionalCountry" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="fl wd32">
                                <label class="label">
                                    Position Held at Company:</label>
                                <asp:TextBox ID="txtInstitutionalPositionAtCompany" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                            <%--fin mavelar--%>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="RoleDDL" />
                <asp:AsyncPostBackTrigger ControlID="CountryOfResidenceDDL" />
                <asp:AsyncPostBackTrigger ControlID="ProvinceOfResidenceDDL" />
            </Triggers>
        </asp:UpdatePanel>
    </li>
</ul>
<div class="clear"></div>