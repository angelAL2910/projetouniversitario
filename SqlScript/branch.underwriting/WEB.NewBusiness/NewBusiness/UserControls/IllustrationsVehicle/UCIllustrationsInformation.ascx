﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCIllustrationsInformation.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCIllustrationsInformation" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>    
<div class="row">
    <div class="cont_gnl tab_pane_container rcomp mT25">
        <div class="round_blue "><%=RESOURCE.UnderWriting.NewBussiness.Resources.ExistingIllustrations%></div>
        <div class="reqVehiculo">
            <div class="tbl data_Gpl gvVehiculos rasegu">
                <dx:ASPxGridView
                    ID="gvIllustrations"
                    runat="server"
                    EnableCallBacks="False"
                    KeyFieldName="PolicyKey;"
                    ClientIDMode="Static"
                    AutoGenerateColumns="false"
                    Width="100%"
                    OnRowCommand="gvIllustrations_RowCommand"
                    OnPreRender="gvIllustrations_PreRender"
                    OnPageIndexChanged="gvIllustrations_PageIndexChanged">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Compañía" FieldName="Company" Name="Company">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Cotización" FieldName="Illustration" Name="ILLUSTRATIONLABEL">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Línea de Negocio" FieldName="BusinessLine" Name="LineofBusinessLabel">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Monto Prima" FieldName="PremiumAmount" Name="PremiumAmount">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha de Emisión" FieldName="EffectiveDate" Name="EmmisionDate" Width="8%">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Fecha de Vencimiento" FieldName="CreateDate" Name="StatusChangeDate" Width="8%">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Estado" FieldName="Status" Name="Status" Width="8%">
                            <Settings AllowHeaderFilter="False" AllowSort="True" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn Caption="Notas" Name="Notes" Width="5%">
                            <DataItemTemplate>
                                <div style="text-align: center">
                                    <asp:Button runat="server" CssClass="edit_file" ID="btnNote"></asp:Button>
                                </div>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsPager PageSize="20" AlwaysShowPager="true">
                        <PageSizeItemSettings Visible="false" ShowAllItem="true" />
                    </SettingsPager>
                    <SettingsBehavior AllowSelectSingleRowOnly="false" AllowSort="false" AllowFocusedRow="false" />
                    <Settings ShowFooter="true" />
                </dx:ASPxGridView>
            </div>
        </div>
    </div>
</div>