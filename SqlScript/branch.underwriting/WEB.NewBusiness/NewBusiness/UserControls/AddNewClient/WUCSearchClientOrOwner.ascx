﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCSearchClientOrOwner.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.AddNewClient.Common.WUCSearchClientOrOwner" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<asp:UpdatePanel runat="server" ID="updSearchClientOrOwner" UpdateMode="Conditional" OnUnload="UpdatePanel_Unload">
    <ContentTemplate>
        <div class="pop_up_wrapper" style="border: none">
            <div class="content_fondo_blanco" style="padding: 0px;">
                <dx:ASPxGridView ID="gvSearch" runat="server"
                    KeyFieldName="ContactId;FirstName;LastName;ContactAgentLegalId"
                    EnableCallBacks="False"
                    AutoGenerateColumns="False"
                    Width="100%"
                    DataSourceID="LinqDS"
                    ClientIDMode="Static"
                    OnFocusedRowChanged="gvSearch_FocusedRowChanged" OnCustomColumnSort="gvSearch_CustomColumnSort" OnProcessColumnAutoFilter="gvSearch_ProcessColumnAutoFilter" OnAfterPerformCallback="gvSearch_AfterPerformCallback">
                    <SettingsPager PageSize="15">
                    </SettingsPager>
                    <ClientSideEvents RowClick="function(){$('#hdnISFiltering').val('false');}" />
                    <Settings ShowFilterRow="true" ShowFilterBar="Auto" ShowHeaderFilterButton="true" ShowFilterRowMenu="true" ShowFilterRowMenuLikeItem="false" VerticalScrollableHeight="800" />
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="INSTITUTION NAME" FieldName="FirstName" VisibleIndex="0">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="FIRST NAME" FieldName="FirstName" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="LAST NAME" FieldName="LastName" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ID NUMBER" FieldName="IdNumber" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="DateOfBirth" Caption="DATE OF BIRTH" CellStyle-HorizontalAlign="Center" VisibleIndex="4">
                            <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="COUNTRY OF RESIDENCE" FieldName="Country" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="LastUpdate" Caption="LAST UPDATE" CellStyle-HorizontalAlign="Center" VisibleIndex="6">
                            <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy"></PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                    </Columns>
                    <Settings VerticalScrollableHeight="400" VerticalScrollBarMode="Visible" />
                    <SettingsPager PageSize="15" NumericButtonCount="3" AlwaysShowPager="true">
                        <PageSizeItemSettings Visible="true" ShowAllItem="true" />
                    </SettingsPager>
                    <SettingsBehavior AllowFocusedRow="true" ProcessFocusedRowChangedOnServer="true" />
                </dx:ASPxGridView>
                <dx:LinqServerModeDataSource OnSelecting="LinqDS_Selecting" ID="LinqDS" runat="server" DefaultSorting="FirstName" />
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hdnISFiltering" Value="false" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnContactTypeId" Value="1" />
        <asp:HiddenField runat="server" ID="hdnKeyNameProduct" Value="" />
        <asp:HiddenField ID="HFIsConting" ClientIDMode="Static" runat="server" Value="true" />
        <asp:HiddenField ID="HFIsMain" ClientIDMode="Static" runat="server" Value="true" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvSearch" />
    </Triggers>
</asp:UpdatePanel>