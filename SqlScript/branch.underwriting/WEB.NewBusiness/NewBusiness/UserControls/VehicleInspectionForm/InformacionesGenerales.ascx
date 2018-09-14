﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InformacionesGenerales.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.VehicleInspectionForm.InformacionesGenerales" %>
<%@ Register Src="~/NewBusiness/UserControls/VehicleInspectionForm/VerificacionInformacionesGenerales.ascx" TagPrefix="uc1" TagName="VerificacionInformacionesGenerales" %>

<asp:UpdatePanel ID="updInformacionesGenerales" runat="server">
    <ContentTemplate>
        <div class="fondo_gris">
            <div class="grupos de_1" style="padding: 10px;">
                <div>
                    <div class="fondo_blanco">
                        <div class="titulos_azules"><strong><%= RESOURCE.UnderWriting.NewBussiness.Resources.QuotationInformation.ToUpper() %></strong></div>
                        <div style="padding: 10px;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="padding-top: 6px;">
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.QuotePolicy %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtNumeroCotizacion" ReadOnly="true" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.INSURED%>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtAsegurado" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.Country %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtPais" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.Province %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtProvincia" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.Town %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtMunicipio" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.City %> / <%= RESOURCE.UnderWriting.NewBussiness.Resources.Neighborhood %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtCiudad" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.PhoneNumberLabel %>:</span>
                                                    <asp:TextBox ClientIDMode="Static" ID="txtTelefono" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.EmailAddressLabel %>:</span>
                                                    <asp:TextBox ClientIDMode="Static" ID="txtCorreoElectronico" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="fondo_blanco" style="margin-top: 5px;">
                        <div class="titulos_azules"><strong><%= RESOURCE.UnderWriting.NewBussiness.Resources.InspectionInformation.ToUpper() %></strong></div>
                        <div style="padding: 10px;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="padding-top: 6px;">
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.InspectionDate %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtFecha" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.StartTime%>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtHoraInicio" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.Inspector %>:</span>
                                                    <%--<asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtInspector" runat="server"></asp:TextBox>--%>
                                                    <asp:DropDownList
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="drpInspectors"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>                                                     
                                                </div>
                                            </div>
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.InpectionRegisteredBy %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtRegistradaPor" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.IntermediaryName %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtIntermediario" runat="server"></asp:TextBox>
                                                </div>

                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.InspectionAddress %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" ID="txtDireccionInspeccion" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="fondo_blanco" style="margin-top: 5px;">
                        <div class="titulos_azules"><strong><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleInformation.ToUpper() %></strong></div>
                        <div style="padding: 10px;">
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="padding-top: 6px;">
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleBrand %>:</span>
                                                    <asp:DropDownList
                                                        AutoPostBack="true"
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="ddlMarca"
                                                        OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleModel %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtModelo" runat="server"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnModeloId" runat="server" />
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.Year %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtAno" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleVersion %>:</span>
                                                    <asp:DropDownList
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="ddlVersion"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleColor %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtColor" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleTransmission %>:</span>
                                                    <asp:DropDownList
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="ddlTransmision"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleKind %>:</span>
                                                    <asp:DropDownList
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="ddlClase"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleUse %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtUso" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleLicensePlate %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtPlaca" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleType %>:</span>
                                                    <asp:TextBox CssClass="input_disabled" Enabled="false" ID="txtTipo" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleTraction %>:</span>
                                                    <asp:DropDownList
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="ddlTraccion"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>                                                  
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleChasis %>:</span>
                                                    <asp:TextBox ID="txtChasis" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleCylinders %>:</span>
                                                    <asp:TextBox CssClass="onlyNumbers" ID="txtCilindros" runat="server" number="number"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleMileageKilometer %>:</span>
                                                    <asp:DropDownList
                                                        ClientIDMode="Static"
                                                        CssClass="combo_box"
                                                        DataValueField="Key"
                                                        DataTextField="Value"
                                                        ID="ddlMileageKilometer"
                                                        runat="server"
                                                        Style="padding-left: 4px; width: 50%;">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleMileage %>:</span>
                                                    <asp:TextBox CssClass="onlyNumbers" ID="txtKilometraje" runat="server" number="number"></asp:TextBox>
                                                </div>
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehiclePeopleCapacity %>:</span>
                                                    <asp:TextBox CssClass="onlyNumbers" ID="txtCapacidad" runat="server" number="number"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="grupos de_4">
                                                <div class="label_plus_input par">
                                                    <span style="font-size: 13px; padding-top: 3px;"><%= RESOURCE.UnderWriting.NewBussiness.Resources.VehicleRegistrationLegalDocumentBL %>:</span>
                                                    <div class="wd50 fl inspection_radio">
                                                        <div>
                                                            <table>
                                                                <tr>
                                                                    <td><%= RESOURCE.UnderWriting.NewBussiness.Resources.YesLabel %></td>
                                                                    <td>
                                                                        <asp:RadioButton GroupName="VehicleRegistrationLegalDocumentBL" ID="rbSi" runat="server" /></td>
                                                                    <td>
                                                                        <br />
                                                                    </td>
                                                                    <td><%= RESOURCE.UnderWriting.NewBussiness.Resources.NoLabel %></td>
                                                                    <td>
                                                                        <asp:RadioButton GroupName="VehicleRegistrationLegalDocumentBL" ID="rbNo" runat="server" /></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>   
                                    <tr>
                                        <td>
                                            Localizacion en el mapa
                                        </td>
                                    </tr>                                 
                                    <tr>
                                        <td>
                                            <div>
                                                <div id="dvInputSearch" style="display: none;"></div>
                                                <div id="map" style="height: 368px">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
                <div>
                    <uc1:VerificacionInformacionesGenerales runat="server" ID="VerificacionInformacionesGenerales" />
                </div>
            </div>
        </div>
        <asp:HiddenField ClientIDMode="Static" ID="hdnValoresJSON" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnValoresOIJSON" runat="server" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnClean" runat="server" Value="false" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnMissingInspection" runat="server" Value="false" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnLongitudVEH" runat="server" Value="0" />
        <asp:HiddenField ClientIDMode="Static" ID="hdnLatitudVEH" runat="server" Value="0" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlMarca" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="drpInspectors" EventName="SelectedIndexChanged" />
    </Triggers>
</asp:UpdatePanel>