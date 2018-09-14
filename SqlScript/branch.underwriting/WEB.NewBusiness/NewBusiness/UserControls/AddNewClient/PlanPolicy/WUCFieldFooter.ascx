﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFieldFooter.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.PlanPolicy.WUCFieldFooter" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterAxys.ascx" TagPrefix="uc1" TagName="WUCFieldFooterAxys" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterEduplan.ascx" TagPrefix="uc1" TagName="WUCFieldFooterEduplan" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterHorizon.ascx" TagPrefix="uc1" TagName="WUCFieldFooterHorizon" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterScholar.ascx" TagPrefix="uc1" TagName="WUCFieldFooterScholar" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterCompassIndex.ascx" TagPrefix="uc1" TagName="WUCFieldFooterCompassIndex" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterLegacy.ascx" TagPrefix="uc1" TagName="WUCFieldFooterLegacy" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterLighthouse.ascx" TagPrefix="uc1" TagName="WUCFieldFooterLighthouse" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterSentinel.ascx" TagPrefix="uc1" TagName="WUCFieldFooterSentinel" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterFunerarios.ascx" TagPrefix="uc1" TagName="WUCFieldFooterFunerarios" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterElite.ascx" TagPrefix="uc1" TagName="WUCFieldFooterElite" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/PlanPolicy/Products/WUCFieldFooterSelect.ascx" TagPrefix="uc1" TagName="WUCFieldFooterSelect" %>
<asp:UpdatePanel runat="server" ID="udpFieldFooter">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnFooter" class="col-1-1">
            <asp:MultiView runat="server" ID="mtvFieldFooterProduct" ActiveViewIndex="0">
                 <asp:View runat="server" ID="VEmpty">                    
                </asp:View>
                <asp:View runat="server" ID="VHorizon">
                    <uc1:WUCFieldFooterHorizon runat="server" ID="WUCFieldFooterHorizon" />
                </asp:View>
                <asp:View runat="server" ID="VEduplan">
                    <uc1:WUCFieldFooterEduplan runat="server" ID="WUCFieldFooterEduplan" />
                </asp:View>
                <asp:View runat="server" ID="VScholar">
                    <uc1:WUCFieldFooterScholar runat="server" ID="WUCFieldFooterScholar" />
                </asp:View>
                <asp:View runat="server" ID="VCompassIndex">
                    <uc1:WUCFieldFooterCompassIndex runat="server" ID="WUCFieldFooterCompassIndex" />
                </asp:View>
                <asp:View runat="server" ID="VLegacy">
                    <uc1:WUCFieldFooterLegacy runat="server" ID="WUCFieldFooterLegacy" />
                </asp:View>
                <asp:View runat="server" ID="VLighthouse">
                    <uc1:WUCFieldFooterLighthouse runat="server" ID="WUCFieldFooterLighthouse" />
                </asp:View>
                <asp:View runat="server" ID="VSentinel">
                    <uc1:WUCFieldFooterSentinel runat="server" ID="WUCFieldFooterSentinel" />
                </asp:View>
                <asp:View runat="server" ID="VAxys">
                    <uc1:WUCFieldFooterAxys runat="server" ID="WUCFieldFooterAxys" />
                </asp:View>
                <asp:View runat="server" ID="VElite">
                    <uc1:WUCFieldFooterElite runat="server" id="WUCFieldFooterElite" />
                </asp:View>
                <asp:View runat="server" ID="VSelect">
                    <uc1:WUCFieldFooterSelect runat="server" id="WUCFieldFooterSelect" />
                </asp:View>
                <asp:View runat="server" ID="VFunerarios">
                    <uc1:WUCFieldFooterFunerarios runat="server" id="WUCFieldFooterFunerarios" />
                </asp:View>
            </asp:MultiView>

            <div class="barra_sub_menu">
                <div class="grupos de_6">
                    <div>
                        <div class="btn_celeste">
                            <span class="save_celeste"></span>
                            <asp:Button runat="server" CssClass="boton" alt="3" Text="Save" ID="btnSaveFooter" OnClientClick="BeginRequestHandler(this);return validacionesTab(this);"/>
                            <asp:Button ID="btnCalc" ClientIDMode="AutoID" runat="server" OnClick="btnCalc_Click" Text="Button" />
                        </div>
                        <asp:HiddenField ID="HDFItbis" runat="server" ClientIDMode="Static" />
                        <asp:HiddenField ID="HDFMultiploAnual" runat="server" ClientIDMode="Static" />
                        <!--btn_celeste-->
                    </div>
                </div>
                <!--grupos-->
            </div>
            <!--barra_sub_menu-->
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:HiddenField ID="Hidden1" ClientIDMode="Static" runat="server" Value="" />