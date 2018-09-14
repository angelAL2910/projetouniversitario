﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCContactEditForm.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCContactEditForm" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/WUCPEPForm.ascx" TagPrefix="uc1" TagName="WUCPEPForm" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/WUCFinalBeneficary.ascx" TagPrefix="uc1" TagName="WUCFinalBeneficary" %>
<asp:UpdatePanel runat="server" ID="udpContactEditForm" UpdateMode="Conditional">
    <ContentTemplate>
        <div id="dvVehicleContactForm" style="padding: 10px">
            <div class="select_buy1">
                <div class="box_SP row_B">
                    <div class="ttl">Información del Contacto</div>
                    <div class="boxCont">
                        <div class="labelbox_50_fluid row_A">
                            <asp:Panel runat="server" ID="pnTypeOfPerson" class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.TypeOfPerson %></span>
                                <asp:DropDownList runat="server" ID="ddlTypeOfPerson" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeOfPerson_SelectedIndexChanged" validation="Required"></asp:DropDownList>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnPerson" Visible="true">
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.FirstNameLabel %></span>
                                    <asp:TextBox runat="server" ID="txtFirstName" Style="text-align: left;"></asp:TextBox>
                                </div>
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.LastNameLabel %></span>
                                    <asp:TextBox runat="server" ID="txtLastName" Style="text-align: left;"></asp:TextBox>
                                </div>
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.MiddleNameLabel %></span>
                                    <asp:TextBox runat="server" ID="txtMiddleName" Style="text-align: left;"></asp:TextBox>
                                </div>
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.SecondLastNameLabel %></span>
                                    <asp:TextBox runat="server" ID="txtSecondLastName" Style="text-align: left;"></asp:TextBox>
                                </div>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnCompany" Visible="false">
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CompanyNameLabel %></span>
                                    <asp:TextBox runat="server" ID="txtCompanyName" Style="text-align: left;"></asp:TextBox>
                                </div>
                            </asp:Panel>
                            <div class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.EmailAddressLabel %></span>
                                <asp:TextBox runat="server" ID="txtEmail" inputtype="Email" Style="text-align: left;"></asp:TextBox>
                            </div>
                            <div class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CellPhone %></span>
                                <asp:TextBox runat="server" ID="txtCellPhone" validateCompleteMask="validateCompleteMask" data-inputmask="'mask': '(999)999-9999'" Style="text-align: left;"></asp:TextBox>
                            </div>
                            <div class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.HomePhone %></span>
                                <asp:TextBox runat="server" ID="txtHomePhone" validateCompleteMask="validateCompleteMask" data-inputmask="'mask': '(999)999-9999'" Style="text-align: left;"></asp:TextBox>
                            </div>

                            <div class="label_plus_input par">
                                <span>Fax </span>
                                <asp:TextBox runat="server" ID="txtFax" validateCompleteMask="validateCompleteMask" data-inputmask="'mask': '(999)999-9999'" Style="text-align: left;"></asp:TextBox>
                            </div>
                            <asp:Panel runat="server" ID="pnDob" class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.DateofBirthLabel %></span>
                                <asp:TextBox runat="server" ID="txtDob" CssClass="datepicker" Style="text-align: left;"></asp:TextBox>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnPlaceOfBirth" class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.PlaceOfBirth %></span>
                                <asp:TextBox runat="server" ID="txtPlaceOfBirth" Style="text-align: left;"></asp:TextBox>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnGender" class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.GenderLabel %></span>
                                <asp:DropDownList runat="server" ID="ddlGender" ClientIDMode="Static" validation="Required"></asp:DropDownList>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnMaritalStatus" class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.MaritalStatusLabel %></span>
                                <asp:DropDownList runat="server" ID="ddlMaritalStatus" ClientIDMode="Static" validation="Required"></asp:DropDownList>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnHomeOwner" class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.HomeOwner %></span>
                                <asp:DropDownList runat="server" ID="ddlHomeOwner" ClientIDMode="Static">
                                </asp:DropDownList>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnDependencyCount" class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Numberofdependents %></span>
                                <asp:TextBox runat="server" ID="txtDependencyCount" number="number" Style="text-align: right;"></asp:TextBox>
                            </asp:Panel>
                            <div class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CountryofBirthLabel %></span>
                                <asp:DropDownList runat="server" ID="ddlCountryCitizenship" validation="Required"></asp:DropDownList>
                            </div>
                            <asp:Panel runat="server" ID="pnGridIds" Visible="false">
                                <asp:UpdatePanel runat="server" ID="udpIds" ClientIDMode="Static" RenderMode="Block" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="label_plus_input par" style="width: 1614px;">
                                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.IdentificationsLabel %></span>
                                        </div>
                                        <dx:ASPxGridView
                                            ID="gvIdentitication"
                                            runat="server"
                                            EnableCallBacks="False"
                                            ClientIDMode="Static"
                                            AutoGenerateColumns="false"
                                            KeyFieldName="ContactIdType;SeqNo"
                                            Width="100%" OnRowCommand="gvIdentitication_RowCommand" OnPreRender="gvIdentitication_PreRender">
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="" CellStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <div class="grupos de_2" style="text-align: center">
                                                                    <div>
                                                                        <asp:LinkButton runat="server" ID="btnEditOrSave" CssClass="myedit_file" CommandName="Edit" OnClientClick="return validateForm('udpIds');" />
                                                                    </div>
                                                                    <div>
                                                                        <asp:LinkButton runat="server" ID="btnCancel" Visible="false" CssClass="mycancel_file" CommandName="Cancel" />
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnEditOrSave" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Type" Name="IDTypeLabel" CellStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:DropDownList runat="server" ID="dropIdType" ClientIDMode="Static" Visible="false" validation="Required"></asp:DropDownList>
                                                        <asp:Literal runat="server" ID="ltTypeId" Text='<%#Eval("ContactIdTypeDescription")%>' />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Id Number" Name="IDNumberLabel" CellStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtId" Text='<%#Eval("Id")%>' validateCompleteMask='validateCompleteMask' validation="Required" label="Id" Visible="false" />
                                                        <asp:Literal runat="server" ID="ltId" Text='<%#Eval("Id")%>' />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn Caption="Expiration Date" Name="ExpirationDateLabel" CellStyle-HorizontalAlign="Center">
                                                    <DataItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtExpDate" CssClass="datepicker" Text='<%#Eval("ExpireDate") != null? ((DateTime)Eval("ExpireDate")).ToString("MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture):string.Empty%>' validation="Required" label="ExpireDate" Visible="false" />
                                                        <asp:Literal runat="server" ID="ltExpDate" Text='<%# Eval("ExpireDate") != null? ((DateTime)Eval("ExpireDate")).ToString("MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture):string.Empty%>' />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                            </Columns>
                                            <SettingsPager PageSize="1" AlwaysShowPager="true">
                                                <PageSizeItemSettings Visible="false" ShowAllItem="true" />
                                            </SettingsPager>
                                            <SettingsBehavior AllowSelectSingleRowOnly="false" AllowSort="False" AllowFocusedRow="false" />
                                            <Settings ShowFooter="True" />
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>

                            <asp:Panel runat="server" ID="pnID" Visible="true">
                                <div class="label_plus_input par sl">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.IDTypeLabel %></span>
                                    <asp:DropDownList runat="server" ID="ddlIdType" AutoPostBack="true" OnSelectedIndexChanged="ddlIdType_SelectedIndexChanged" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.ID %></span>
                                    <asp:TextBox runat="server" ID="txtIDNumber" validateCompleteMask='validateCompleteMask' ClientIDMode="Static" Style="text-align: left;"></asp:TextBox>
                                </div>
                                <asp:Panel runat="server" ID="pnExpDate" class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.ExpirationDate %></span>
                                    <asp:TextBox runat="server" ID="txtIDExpDate" ClientIDMode="Static" CssClass="datepicker" Style="text-align: left;"></asp:TextBox>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnRepresentante" Visible="false">
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.RepresentativeNameLabel %></span>
                                    <asp:TextBox runat="server" ID="txtRepresentativeName" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="label_plus_input par sl">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.RepresentativeIdentificationTypeLabel %></span>
                                    <asp:DropDownList runat="server" ID="ddlRepresentativeIdentificationType" ClientIDMode="Static"></asp:DropDownList>
                                </div>
                                <div class="label_plus_input par">
                                    <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.RepresentativeIdentificationLabel %></span>
                                    <asp:TextBox runat="server" validateCompleteMask='validateCompleteMask' ID="txtRepresentativeIdentification" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </asp:Panel>
                            <div class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.NcfType %></span>
                                <asp:DropDownList runat="server" ID="ddlNcfType" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <asp:Panel runat="server" ID="pnqtyEmployee" class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Quantityofemployees %></span>
                                <asp:TextBox runat="server" ID="txtQtyEmployee" number="number" Style="text-align: right;"></asp:TextBox>
                            </asp:Panel>
                            <div class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CountryLabel %></span>
                                <asp:DropDownList runat="server" ID="ddlCountry" validation="Required" AutoPostBack="true" Enabled="false" ClientIDMode="Static" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.StateProvinceLabel %></span>
                                <asp:DropDownList runat="server" ID="ddlState" validation="Required" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <div class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.municipality %></span>
                                <asp:DropDownList runat="server" ID="ddlMunicipality" validation="Required" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlMunicipality_SelectedIndexChanged"></asp:DropDownList>
                            </div>

                            <%--                            <div class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Economicactivity %></span>
                                <asp:TextBox runat="server" ID="txtEconomyActivity" ClientIDMode="Static"></asp:TextBox>
                            </div>--%>

                            <div class="label_plus_input par sl">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CityLabel %></span>
                                <asp:DropDownList runat="server" ID="ddlCity" validation="Required" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                            <div class="label_plus_input par">
                                <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.AddressLabel %></span>
                                <asp:TextBox runat="server" ID="txtAddress" validation="Required" TextMode="MultiLine" Style="font-weight: bold; text-align: left; height: 45px; width: 50%;"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel runat="server" ID="pnCreditCardInformation" class="select_buy1 col-4 fl">
                <div class="box_SP row_B">
                    <div class="ttl">Domiciliación</div>
                    <div class="boxCont">
                        <div class="row_B">
                            <asp:Panel runat="server" ID="pnDeseaDomiciliar" class="fl">
                                <span class="lb_azul  mL0">Domiciliar el pago?
                                    <asp:CheckBox runat="server" ID="chkDommiciliation" CssClass="fr mL10" ClientIDMode="Static" AutoPostBack="true" OnCheckedChanged="chkDommiciliation_CheckedChanged" /></span>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnIncluirInicial" class="fr">
                                <span class="lb_azul  mL0 ">Incluir el pago inicial?<asp:CheckBox runat="server" ID="chkInitialDomiciliation" CssClass="fr mL10" ClientIDMode="Static" /></span>
                            </asp:Panel>
                        </div>
                        <asp:Panel runat="server" ID="pnDomicliation" CssClass="clear">
                            <div class="label_plus_input par sl">
                                <span>Tipo de tarjeta</span>
                                <asp:DropDownList runat="server" ID="ddlCreditCardType" AutoPostBack="true" OnSelectedIndexChanged="ddlCreditCardType_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                            <asp:Panel runat="server" Visible="false" class="label_plus_input par">
                                <span>Número de Tarjeta Clave</span>
                                <asp:TextBox runat="server" ID="txtCreaditCardNumberKey" Style="text-align: left; background-color: #f9f9b5;" ReadOnly="true"></asp:TextBox>
                            </asp:Panel>
                            <div class="label_plus_input par">
                                <span>Número de Tarjeta</span>
                                <asp:TextBox runat="server" ID="txtCreditCardNumber" validateCompleteMask='validateCompleteMask' Style="text-align: left; background-color: #f9f9b5; width: 46% !important;" ReadOnly="true" placeholder="Numero de tarjeta"></asp:TextBox>
                                <asp:LinkButton runat="server" ID="lnkEditCreditCard" CssClass="myedit_file" Style="width: 30px; position: relative; left: 175px; top: 2px;" OnClick="lnkEditCreditCard_Click"></asp:LinkButton>
                            </div>
                            <div class="label_plus_input par sl inputDoble">
                                <span>Mes / Año de vencimiento</span>
                                <asp:DropDownList runat="server" ID="ddlYear" label="Año vencimiento" Style="position: relative; width: 24% !important; border: 1px solid #4472C4;" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlMonth" Enabled="false" validation="Required" label="Mes vencimiento" Style="position: relative; width: 24% !important; border: 1px solid #4472C4;"></asp:DropDownList>
                            </div>
                            <div class="label_plus_input par">
                                <span>Tarjetahabiente</span>
                                <asp:TextBox runat="server" ID="txtCardHolder" Style="text-align: left;"></asp:TextBox>
                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </asp:Panel>

            <asp:Button runat="server" ID="btnReadOnlyCreditCardNumber" ClientIDMode="Static" Style="display: none" />
            <asp:Button runat="server" ID="btnReadOnlyButton" ClientIDMode="Static" Style="display: none" />

            <div class="select_buy1 col-4 fl" id="frmCumplimiento">
                <div class="box_SP row_B">
                    <div class="ttl">Cumplimiento</div>
                    <div class="boxCont">
                        <div class="label_plus_input par sl">
                            <span>Posee calidad de PEP<i id="spanPep" title="" class="fa fa-question-circle" aria-hidden="true" style="font-size: 27px;"></i></span>
                            <asp:DropDownList runat="server" ID="ddlPep" AutoPostBack="true" validation="Required" OnSelectedIndexChanged="ddlPep_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="label_plus_input par sl">
                            <span>Beneficiario Final<i id="spanBene" title="" class="fa fa-question-circle" aria-hidden="true" style="font-size: 27px;"></i></span>
                            <asp:DropDownList runat="server" ID="ddlBeneFinal" AutoPostBack="true" validation="Required" OnSelectedIndexChanged="ddlBeneFinal_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="label_plus_input par sl">
                            <span>Actividad</span>
                            <asp:DropDownList runat="server" ID="ddlActividad" validation="Required"></asp:DropDownList>
                        </div>

                        <div class="label_plus_input par sl">
                            <span>Estructura de titularidad (SA, SRL, EIRL, etc)</span>
                            <asp:DropDownList runat="server" ID="ddlEstructuraTitularidad" validation="Required"></asp:DropDownList>
                        </div>
                        <div class="label_plus_input par sl">
                            <%--<span><%= RESOURCE.UnderWriting.NewBussiness.Resources.ManagerName %></span>--%>
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.ManagerName %><i id="spanManagerName" title="" class="fa fa-question-circle" aria-hidden="true" style="font-size: 27px;"></i></span>
                            <asp:TextBox runat="server" ID="txtManagerName" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtManagerName_TextChanged"></asp:TextBox>

                        </div>
                        <asp:Panel ID="pnAdminPepFormularyOptionsId" runat="server" CssClass="label_plus_input par sl" Visible="false">
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.AdminPepFormularyOptionsId %></span>
                            <asp:DropDownList runat="server" ID="ddlAdminPepFormularyOptionsId" AutoPostBack="true" OnSelectedIndexChanged="ddlAdminPepFormularyOptionsId_SelectedIndexChanged"></asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="pnAdminPepFormularyOptionsId2" runat="server" Visible="false">
                            <asp:LinkButton runat="server" CssClass="button button-orange alignC embossed" ID="lnkShowPepsManager" OnClick="lnkShowPepsManager_Click">
                                <span> Ver PEPS</span>
                            </asp:LinkButton>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div class="select_buy1 col-4 fl">
                <div class="box_SP row_B">
                    <div class="ttl">Información Laboral</div>
                    <div class="boxCont">
                        <div class="label_plus_input par" id="dvOcupacion">
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.OccupationLabel %></span>
                            <asp:TextBox runat="server" ID="txtOccupation" ClientIDMode="Static"></asp:TextBox>
                        </div>
                        <div class="label_plus_input par">
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.CompanyNameLabel %></span>
                            <asp:TextBox runat="server" ID="txtEmpresaLabora"></asp:TextBox>
                        </div>
                        <asp:Panel ID="divWorkAdress" runat="server" Visible="false" CssClass="label_plus_input par">
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.WorkAddress %></span>
                            <asp:TextBox runat="server" ID="txtWorkAddress" MaxLength="1000"></asp:TextBox>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="divWorkPhone" Visible="false" class="label_plus_input par">
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.WorkPhone %></span>
                            <asp:TextBox runat="server" ID="txtWorkPhone" validateCompleteMask="validateCompleteMask" data-inputmask="'mask': '(999)999-9999'" Style="text-align: left;"></asp:TextBox>
                        </asp:Panel>
                        <div class="label_plus_input par">
                            <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.YearlyPersonalIncomeLabel %></span>
                            <asp:TextBox runat="server" ID="txtIngresoAnual" decimal="decimal"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 fl mB">
                <asp:Panel runat="server" ID="pnGuardar" class="fr" Style="margin-left: 10px;">
                    <asp:LinkButton runat="server" CssClass="button button-green alignC embossed" ClientIDMode="Static" ID="btnGuardar" OnClick="btnGuardar_Click" OnClientClick="return VerifyCompleteMask();">
                        <span><%= RESOURCE.UnderWriting.NewBussiness.Resources.Save %></span>
                    </asp:LinkButton>
                </asp:Panel>

                <div class="fr" style="margin-left: 10px;">
                    <input type="button" class="button button-red alignC embossed" value="<%= RESOURCE.UnderWriting.NewBussiness.Resources.Cancel %>" onclick="closePopEditContact();" />
                </div>

                <div class="fr" style="margin-left: 10px;">
                    <asp:LinkButton runat="server" CssClass="button button-orange alignC embossed" ID="btnVerPEPS" OnClick="btnVerPEPS_Click" Visible="false">
                        <span> Ver PEPS</span>
                    </asp:LinkButton>
                </div>

                <div class="fr" style="margin-left: 10px;">
                    <asp:LinkButton runat="server" CssClass="button button-orange alignC embossed" ID="btnVerBeneficiariosFinales" OnClick="btnVerBeneficiariosFinales_Click" Visible="false">
                        <span> Ver Beneficiarios Finales</span>
                    </asp:LinkButton>
                </div>
                <div class="fr" style="margin-left: 10px;">
                    <asp:LinkButton runat="server" CssClass="button button-green alignC embossed" ClientIDMode="Static" ID="lnkDomiciliarPago" OnPreRender="lnkDomiciliarPago_PreRender" OnClick="lnkDomiciliarPago_Click" OnClientClick="return DlgConfirm(this);">
                        <span>Domiciliar pago</span>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <asp:ModalPopupExtender ID="mpePepPop" PopupControlID="pnPepPop" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPepPop" BehaviorID="popupBhvrPepPop" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="pnPepPop" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper" style="width: 1000px; height: 500px; overflow-x: hidden; overflow-y: hidden">
                <!--l-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li>Formulario PEP</li>
                        <li style="top: 13%;">
                            <input type="button" class="cls_pup" style="background-color: transparent; border-color: transparent;" onclick="ClosePopPep()" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:WUCPEPForm runat="server" ID="WUCPEPForm" />
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>
        <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnShowPepPop" />
        <asp:ModalPopupExtender ID="mpeFinalBeneficiary" PopupControlID="pnFinalBeneficiaryPop" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowFinalBenef" BehaviorID="popupBhvrFinalBeneficiaryPop" runat="server"></asp:ModalPopupExtender>
        <asp:Panel runat="server" ID="pnFinalBeneficiaryPop" CssClass="modalPopup" ClientIDMode="Static" Style="display: none">
            <div class="pop_up_wrapper" style="width: 1000px; height: 500px; overflow-x: hidden; overflow-y: hidden">
                <!--l-->
                <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
                    <ul>
                        <li>BENEFICIARIOS FINALES</li>
                        <li style="top: 13%;">
                            <input type="button" class="cls_pup" style="background-color: transparent; border-color: transparent;" onclick="ClosePopFinalBeneficiary()" />
                        </li>
                    </ul>
                </div>
                <!--titulos_azules-->
                <uc1:WUCFinalBeneficary runat="server" ID="WUCFinalBeneficary" />
                <!--content_fondo_blanco-->
            </div>
        </asp:Panel>
        <asp:Button runat="server" ID="btnValidateCumplimiento" Style="display: none" ClientIDMode="Static" OnClick="btnValidateCumplimiento_Click" />
        <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnShowFinalBenef" />
        <asp:HiddenField runat="server" Value="" ClientIDMode="Static" ID="hdnIsCompany" />
        <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnDataSaveCumplimiento" />
        <asp:HiddenField runat="server" Value="" ClientIDMode="Static" ID="hdnOccupationId" />
        <asp:HiddenField runat="server" Value="" ClientIDMode="Static" ID="hdnOccupationGroupId" />
        <asp:HiddenField runat="server" Value="false" ClientIDMode="Static" ID="hdnHasPep" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="ddlPep" />
        <asp:AsyncPostBackTrigger ControlID="ddlCountry" />
        <asp:AsyncPostBackTrigger ControlID="ddlState" />
        <asp:AsyncPostBackTrigger ControlID="ddlIdType" />
        <asp:AsyncPostBackTrigger ControlID="ddlRepresentativeIdentificationType" />
        <asp:AsyncPostBackTrigger ControlID="ddlMunicipality" />
        <asp:AsyncPostBackTrigger ControlID="chkDommiciliation" />
        <asp:AsyncPostBackTrigger ControlID="txtManagerName" />
    </Triggers>
</asp:UpdatePanel>