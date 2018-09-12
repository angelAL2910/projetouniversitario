﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPhoneNumberLegal.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Common.WUCPhoneNumberLegal" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<asp:UpdatePanel runat="server" ID="udpPhoneNumberLegal" ClientIDMode="Static" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
    <ContentTemplate>
        <asp:Panel runat="server" class="col-1-3" ID="frmPhoneNumbersLegal" ClientIDMode="Static" >
            <div class="fondo_blanco">
                <div class="titulos_azules" id="titulos_azules">
                    <span class="phone"></span><strong>
                        <asp:Literal runat="server" ID="ltPhoneNumbers">PHONE NUMBERS</asp:Literal></strong>
                </div>
                <div class="content_fondo_blanco fix_height">
                    <asp:Panel runat="server" ID="pnForm" DefaultButton="btnAdd">
                        <div class="grupos">
                            <div style="width: 50%">
                                <label class="label" runat="server" id="ltPhoneType">Phone Type</label>
                                <div class="wrap_select">
                                    <asp:DropDownList runat="server" ID="cbxPhoneTypeLegal" validation='Required'>
                                    </asp:DropDownList>
                                </div>
                                <!--wrap_select-->
                            </div>
                            <div>
                                <label class="label" style="margin: 0 auto; text-align: center" runat="server" id="Primary">Primary</label>
                                <asp:CheckBox runat="server" ID="chkIsPrimary" />
                            </div>
                            <div class="float_right">
                                <div class="boton_wrapper amarillo float_right" id="dvAddBtn" runat="server">
                                    <span class="add"></span>
                                    <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="boton" AllowEnter="true" OnClientClick="return validateFormPhoneNumbers(this,'frmPhoneNumbersLegal')" OnClick="btnAdd_Click" />
                                </div>
                                <!--boton_wrapper-->
                            </div>
                        </div>
                        <!--grupos-->
                        <div class="grupos de_4 small alingtoend">
                            <div>
                                <label class="label red" runat="server" id="CountryCode">Country Code</label>
                                <asp:TextBox ID="txtCountryCodeLegal" runat="server" AllowEnter="true" data-inputmask="'alias': 'integer','rightAlign': false,'clearMaskOnLostFocus': true,'allowMinus': false, 'allowPlus': false" ClientIDMode="Static" validation='Required' />
                            </div>
                            <div style="width=0;">
                                <label class="label red" runat="server" id="AreaCode">Area Code</label>
                                <asp:TextBox ID="txtCityCode" runat="server" ClientIDMode="Static" validation='Required' AllowEnter="true" data-inputmask="'alias': 'integer','rightAlign': false,'clearMaskOnLostFocus': true,'allowMinus': false, 'allowPlus': false"/>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="PhoneNumber">Phone Number</label>
                                <asp:TextBox ID="txtPhoneNumber" runat="server" phone="phone" ClientIDMode="Static" validation='Required' AllowEnter="true" validateLength="7" data-inputmask="'mask': '999-9999','clearMaskOnLostFocus': true,'showTooltip': true" Text="" />
                            </div>
                            <div>
                                <label class="label red" runat="server" id="Extension">Extension</label>
                                <asp:TextBox ID="txtExtension" runat="server" ClientIDMode="Static" AllowEnter="true" data-inputmask="'alias': 'integer','rightAlign': false,'clearMaskOnLostFocus': true,'allowMinus': false, 'allowPlus': false ,'repeat':6" />
                            </div>
                        </div>
                    </asp:Panel>
                    <!--grupos-->
                    <div class="grid_wrap margin_t_10 gris">
                        <dx:ASPxGridView ID="gvCommonPhoneLegal" runat="server"
                            KeyFieldName="DirectoryDetailId; DirectoryId"
                            EnableCallBacks="False"
                            AutoGenerateColumns="False"
                            SettingsPager-PageSize="15"
                            Width="100%" OnRowCommand="gvCommonPhoneLegal_RowCommand" OnPageIndexChanged="gvCommonPhoneLegal_PageIndexChanged" ClientIDMode="Static" OnPreRender="gvCommonPhoneLegal_PreRender">
                            <SettingsPager PageSize="3">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="TYPE" FieldName="Type" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="COUNTRY CODE" FieldName="CountryCode" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="AREA CODE" FieldName="AreaCode" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="PHONE NUMBER" FieldName="PhoneNumber" VisibleIndex="3">
                                    <PropertiesTextEdit DisplayFormatString="{0:###-####}"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="EXT" FieldName="Ext" VisibleIndex="4">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PRIMARY" VisibleIndex="5">
                                    <DataItemTemplate>
                                        <asp:Label runat="server" CssClass='<%# Eval("imgClassIsPrimary") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EDIT" VisibleIndex="6">
                                    <DataItemTemplate>
                                        <asp:Button runat="server" ID="btnEditar" CommandName="Modify" CssClass="edit_file" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DELETE" VisibleIndex="6">
                                    <DataItemTemplate>
                                        <asp:UpdatePanel ID="udpDelete" runat="server" OnUnload="UpdatePanel_Unload">
                                            <ContentTemplate>
                                                <asp:Button runat="server" ID="btnDelete" CssClass="delete_file" CommandName="Delete" OnClientClick='return DlgConfirm(this)' />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowSelectSingleRowOnly="true" AllowSort="False" />
                        </dx:ASPxGridView>
                    </div>
                    <!--grid_wrap-->
                </div>
            </div>
            <!--content_fondo_blanco-->
            <!--fondo_blanco-->
        </asp:Panel>
        <asp:HiddenField ID="hdnCurrentSessionLegal" runat="server" Value="" />
        <asp:HiddenField ID="hdnTotalPhonesLegal" runat="server" Value="" ClientIDMode="Static" />
        <!--col-1-3-->
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvCommonPhoneLegal" />
    </Triggers>
</asp:UpdatePanel>
