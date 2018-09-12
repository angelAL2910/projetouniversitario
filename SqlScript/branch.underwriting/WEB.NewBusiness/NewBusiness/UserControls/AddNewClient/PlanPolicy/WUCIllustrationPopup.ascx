﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCIllustrationPopup.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.PlanPolicy.WUCIllustrationPopup" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<asp:UpdatePanel ID="udpIllustration" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 31%;">
                    <asp:Literal ID="ltIllustraciones" Text="Illustration List" ClientIDMode="Static" runat="server"></asp:Literal>
                </li>
                <li style="top: 13%;">
                    <input type="button" id="close_pop_up" class="cls_pup" onclick="ClosePopIllustration()" />
                </li>
            </ul>
        </div>
        <div class="cont_bl">
            <!--titulos_azules-->
            <dx:ASPxGridView ID="gvIllustration"
                runat="server" ClientIDMode="Static" KeyFieldName="CustomerPlanNo;Product;DispIllustrationNo;DateUpdated"
                EnableCallBacks="False" DataSourceID="LinqDS" Width="100%" AutoGenerateColumns="False"
                OnFocusedRowChanged="gvIllustration_FocusedRowChanged" OnAfterPerformCallback="gvIllustration_AfterPerformCallback" OnProcessColumnAutoFilter="gvIllustration_ProcessColumnAutoFilter">                
                <Settings ShowFilterRow="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="false"/>
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="InsuredName" Name="InsuredName" Caption="NAME / LASTNAME" Settings-AutoFilterCondition="Contains" />
                    <dx:GridViewDataTextColumn FieldName="Product" Name="PlanName" Caption="PLAN NAME" Settings-AutoFilterCondition="Contains" />
                    <dx:GridViewDataTextColumn FieldName="DispIllustrationNo" Name="ILLUSTRATIONNUMBERLabel" Caption="ILLUSTRATION NUMBER" Settings-AutoFilterCondition="Contains" />
                    <dx:GridViewDataTextColumn FieldName="IllustrationStatus" Name="Status" Caption="STATUS" Settings-AutoFilterCondition="Contains" />
                    <dx:GridViewDataTextColumn FieldName="DateUpdated" Name="LastUpdateLabel" Caption="LAST UPDATE">
                        <PropertiesTextEdit DisplayFormatString="MM/dd/yyyy">
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <Settings ShowGroupPanel="True" />
                <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />
                <Settings VerticalScrollableHeight="400" VerticalScrollBarMode="Visible" />
                <SettingsPager PageSize="50" AlwaysShowPager="true">
                    <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                </SettingsPager>
                <SettingsPopup>
                    <HeaderFilter Height="200" />
                </SettingsPopup>
                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
            </dx:ASPxGridView>
            <dx:LinqServerModeDataSource OnSelecting="LinqDS_Selecting" ID="LinqDS" runat="server" DefaultSorting="DateUpdated" />
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvIllustration" />
    </Triggers>
</asp:UpdatePanel>
