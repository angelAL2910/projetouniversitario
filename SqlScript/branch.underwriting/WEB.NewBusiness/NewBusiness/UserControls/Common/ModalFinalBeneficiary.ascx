﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalFinalBeneficiary.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Common.ModalFinalBeneficiary" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:UpdatePanel runat="server" style="width: 100%" RenderMode="Block" ID="udpFinalBeneficiary" ClientIDMode="Static">
    <ContentTemplate>
        <div class="select_buy1">
            <div class="box_SP row_B">
                <div class="ttl">
                    <asp:ImageButton runat="server" ID="btnAdd" ImageUrl="~/Content/images/add_icon.png" OnClick="btnAdd_Click" CssClass="fl" Style="top: -6px; position: relative;" /><asp:Label Style="font-family: initial;" runat="server" ID="lblAgregarContrato" Text="Agregar"></asp:Label>
                </div>
                <div class="boxCont">
                    <dx:ASPxGridView
                        ID="gvFinalBeneficiary"
                        runat="server"
                        EnableCallBacks="False"
                        AutoGenerateColumns="false"
                        Width="100%"
                        KeyFieldName="RecordIndex;Status"
                        OnRowCommand="gvPEP_RowCommand">
                        <Columns>
                            <dx:GridViewDataColumn Caption="" CellStyle-HorizontalAlign="Center" Width="100">
                                <DataItemTemplate>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="grupos de_2" style="text-align: center">
                                                <asp:Panel runat="server" ID="pnDelete">
                                                    <asp:LinkButton runat="server" ID="btnDelete" CssClass="delete_file" CommandName="Delete" OnClientClick="return DlgConfirm(this);" />
                                                </asp:Panel>
                                                <div>
                                                    <asp:LinkButton runat="server" ID="btnCancel" CssClass="mycancel_file" CommandName="Cancel" Visible="false" />
                                                </div>
                                                <div>
                                                    <asp:LinkButton runat="server" ID="btnEditOrSave" CssClass="myedit_file" CommandName="Edit" OnClientClick="return validateForm('udpFinalBeneficiary');" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnEditOrSave" />
                                            <asp:AsyncPostBackTrigger ControlID="btnCancel" />
                                            <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Nombre Completo" Settings-AllowSort="true" Name="NameLabel" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:TextBox runat="server" ID="txtName" validation="Required" label="Nombre Completo" Visible="false" Text='<%#Eval("NombreCompleto")%>'></asp:TextBox>
                                    <asp:Literal runat="server" ID="ltName" Text='<%#Eval("NombreCompleto")%>' />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Porcentaje" Settings-AllowSort="true" Name="" CellStyle-HorizontalAlign="Center">
                                <DataItemTemplate>
                                    <asp:TextBox runat="server" Style="text-align: right" ID="txtPorcentaje" Text='<%#decimal.Parse(Eval("Porcentaje").ToString()).ToString("#,0.00",System.Globalization.CultureInfo.InvariantCulture)%>' Visible="false"></asp:TextBox>
                                    <asp:Literal runat="server" ID="ltPorcentaje" Text='<%#decimal.Parse(Eval("Porcentaje").ToString()).ToString("#,0.00",System.Globalization.CultureInfo.InvariantCulture)%>' />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Settings ShowFooter="True" />
                        <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Visible" />
                    </dx:ASPxGridView>

                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdd" />
    </Triggers>
</asp:UpdatePanel>
