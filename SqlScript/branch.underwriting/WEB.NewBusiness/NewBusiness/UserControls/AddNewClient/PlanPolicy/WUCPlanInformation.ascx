﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPlanInformation.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.PlanPolicy.WUCPlanInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCEduplan.ascx" TagPrefix="uc1" TagName="UCEduplan" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCHorizon.ascx" TagPrefix="uc1" TagName="UCHorizon" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCScholar.ascx" TagPrefix="uc1" TagName="UCScholar" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCCompassIndex.ascx" TagPrefix="uc1" TagName="UCCompassIndex" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCLegacy.ascx" TagPrefix="uc1" TagName="UCLegacy" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCLightHouse.ascx" TagPrefix="uc1" TagName="UCLightHouse" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCSentinel.ascx" TagPrefix="uc1" TagName="UCSentinel" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCAxy.ascx" TagPrefix="uc1" TagName="UCAxy" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/UCPopPerzonalizedProfile.ascx" TagPrefix="uc1" TagName="UCPopPerzonalizedProfile" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCBasicPlan.ascx" TagPrefix="uc1" TagName="UCBasicPlan" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCFunerarios.ascx" TagPrefix="uc1" TagName="UCFunerarios" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCSelect.ascx" TagPrefix="uc1" TagName="UCSelect" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCElite.ascx" TagPrefix="uc1" TagName="UCElite" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCFortis.ascx" TagPrefix="uc1" TagName="UCFortis" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCSerenity.ascx" TagPrefix="uc1" TagName="UCSerenity" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCAsistenciaalViajerohasta90dias.ascx" TagPrefix="uc1" TagName="UCAsistenciaalViajerohasta90dias" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCAsistenciaalViajeroAnual-30diascontinuos.ascx" TagPrefix="uc1" TagName="UCAsistenciaalViajeroAnual30diascontinuos" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/UCAsistenciaalViajeroAnual-60díascontinuos.ascx" TagPrefix="uc1" TagName="UCAsistenciaalViajeroAnual60díascontinuos" %>
          

<asp:UpdatePanel runat="server" ID="udpPlanInformation" RenderMode="Inline" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:MultiView ID="mvSelectControl" runat="server" ActiveViewIndex="0">
            <asp:View ID="VBasicPlan" runat="server">
                <uc1:UCBasicPlan runat="server" ID="UCBasicPlan" />
            </asp:View>
            <asp:View ID="VHorizon" runat="server">
                <uc1:UCHorizon runat="server" ID="UCHorizon" />
            </asp:View>
            <asp:View ID="VEduplan" runat="server">
                <uc1:UCEduplan runat="server" ID="UCEduplan" />
            </asp:View>
            <asp:View ID="VScholar" runat="server">
                <uc1:UCScholar runat="server" ID="UCScholar" />
            </asp:View>
            <asp:View ID="VCompassIndex" runat="server">
                <uc1:UCCompassIndex runat="server" ID="UCCompassIndex" />
            </asp:View>
            <asp:View ID="VLegacy" runat="server">
                <uc1:UCLegacy runat="server" ID="UCLegacy" />
            </asp:View>
            <asp:View ID="VLightHouse" runat="server">
                <uc1:UCLightHouse runat="server" ID="UCLightHouse" />
            </asp:View>
            <asp:View ID="VSentinel" runat="server">
                <uc1:UCSentinel runat="server" ID="UCSentinel" />
            </asp:View>
            <asp:View ID="VAxy" runat="server">
                <uc1:UCAxy runat="server" ID="UCAxy" />
            </asp:View>
            <asp:View ID="VFunerarios" runat="server">
                <uc1:UCFunerarios runat="server" ID="UCFunerarios" />
            </asp:View>
            <asp:View ID="VSelect" runat="server">
                <uc1:UCSelect runat="server" ID="UCSelect" />
            </asp:View>
            <asp:View ID="VElite" runat="server">
                <uc1:UCElite runat="server" ID="UCElite" />
            </asp:View>
            <asp:View ID="VFortis" runat="server">
                <uc1:UCFortis runat="server" id="UCFortis" />
            </asp:View>
            <asp:View ID="VSerenity" runat="server">
                <uc1:UCSerenity runat="server" id="UCSerenity" />
            </asp:View>
            <asp:View ID="VAsistencia90dias" runat="server">
                <uc1:UCAsistenciaalViajerohasta90dias runat="server" id="UCAsistenciaalViajerohasta90dias" />
            </asp:View>
            <asp:View ID="VAsistencia30dias" runat="server">
                <uc1:UCAsistenciaalViajeroAnual30diascontinuos runat="server" id="UCAsistenciaalViajeroAnual30diascontinuos" />
            </asp:View>
            <asp:View ID="VAsistencia60dias" runat="server">
                <uc1:UCAsistenciaalViajeroAnual60díascontinuos runat="server" id="UCAsistenciaalViajeroAnual60díascontinuos" />
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField runat="server" ID="hdnContactRoleID" Value="-1" ClientIDMode="Static" />
        <asp:HiddenField ID="hfSelectControls" runat="server" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnShowPopPersonalizeInvstProf" ClientIDMode="Static" />
        <%--Bmarroquin 07-04-2017 Fix Issue no se pierda el numero de cotizacion de IllusData--%>
        <asp:HiddenField runat="server" ID="hdnNumCotizacionIllusData" Value="-1" ClientIDMode="Static" />

    </ContentTemplate>
</asp:UpdatePanel>
<div id="popPersonalizeInvestProf" style="display: none">
    <uc1:UCPopPerzonalizedProfile runat="server" ID="UCPopPerzonalizedProfile" />
</div>