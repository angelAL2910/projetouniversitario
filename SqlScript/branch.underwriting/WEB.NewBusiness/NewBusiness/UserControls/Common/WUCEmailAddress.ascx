﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCEmailAddress.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.WUCEmailAddress" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:UpdatePanel runat="server" ID="udpEmailAddress" ClientIDMode="Static" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
    <ContentTemplate>
        <asp:Panel runat="server" class="col-1-3" ID="frmEmailAddress" ClientIDMode="Static" >
            <div class="fondo_blanco ">
                <div class="titulos_azules" id="titulos_azulesAddress"><span class="email"></span><strong><asp:Literal runat="server" ID="ltEmailAddress">E-MAIL ADDRESS</asp:Literal> </strong></div>
                <div class="content_fondo_blanco fix_height">
                    <asp:Panel runat="server"
                        ID="pnForm" DefaultButton="btnAdd">
                        <div class="grupos de_2">
                            <div style="width: 40%">
                                <label class="label red" runat="server" ID="EmailType">E-mail Type</label>
                                <div class="wrap_select">
                                    <asp:DropDownList runat="server" ID="cbxEmailType" validation="Required" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </div>
                                <!--wrap_select-->
                            </div>
                            <div style="width: 10%;">
                                <label class="label" style="margin: 0 auto; text-align: center" runat="server" ID="Primary">Primary</label>
                                <asp:CheckBox runat="server" ID="chkIsPrimary" />
                            </div>
                            <div class="float_right">
                                <div class="boton_wrapper amarillo float_right">
                                    <span class="add"></span>
                                    <asp:Button ID="btnAdd" CssClass="boton" Text="Add" runat="server" OnClientClick="return validateForm('frmEmailAddress')" OnClick="btnAdd_Click" />
                                </div>
                                <!--boton_wrapper-->
                            </div>
                        </div>
                        <!--grupos-->
                        <div class="grupos de_1">
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <label class="label" runat="server" ID="EmailAddress">E-mail Address</label>
                                            <asp:TextBox runat="server" ID="txtEmailAddress" AllowEnter="true" Validation="Required" inputType="Email" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!--grupos-->
                    </asp:Panel>
                    <div class="grid_wrap margin_t_10 gris">
                        <dx:ASPxGridView ID="gvCommonEmailAddress" runat="server"
                            KeyFieldName="CommunicationTypeId; DirectoryDetailId; DirectoryId"
                            EnableCallBacks="False"
                            AutoGenerateColumns="False"
                            Styles-FocusedRow-CssClass="Transparente"
                            Width="100%" OnRowCommand="gvCommonEmailAddress_RowCommand" OnPageIndexChanged="gvCommonEmailAddress_PageIndexChanged" OnPreRender="gvCommonEmailAddress_PreRender">
                            <SettingsPager PageSize="3">
                            </SettingsPager>
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="TYPE" FieldName="DirectoryTypeDesc" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn Caption="ADDRESS" FieldName="EmailAdress" VisibleIndex="1">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="PRIMARY" VisibleIndex="5">
                                    <DataItemTemplate>
                                        <asp:Label runat="server" CssClass='<%# Eval("imgClassIsPrimary") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="EDIT" VisibleIndex="6">
                                    <DataItemTemplate>
                                        <asp:Button ID="lnkEditEmail" runat="server" CommandName="Modify" CssClass="edit_file"></asp:Button>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="DELETE" VisibleIndex="6">
                                    <DataItemTemplate>
                                        <asp:Button ID="lnkDeleteEmail" runat="server" CommandName="Delete" CssClass="delete_file" OnClientClick="return DlgConfirm(this)" />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowSelectSingleRowOnly="true" AllowSort="False" />
                        </dx:ASPxGridView>
                    </div>
                    <!--grid_wrap-->
                </div>
                <!--content_fondo_blanco-->
            </div>
            <!--fondo_blanco-->
        </asp:Panel>
        <!--col-1-3-->
        <asp:HiddenField ID="hdnCurrentSession" runat="server" Value="" />
        <asp:HiddenField ID="hdnTotalEmail" runat="server" Value="" ClientIDMode="Static" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvCommonEmailAddress" />
    </Triggers>
</asp:UpdatePanel>

