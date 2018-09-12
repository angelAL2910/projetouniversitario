﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.Common.Header" %>
<%@ Register Src="~/Case/UserControls/Common/UCCasesGrid.ascx" TagPrefix="uc1" TagName="UCCasesGrid" %>
<asp:UpdatePanel runat="server" RenderMode="Block">
    <ContentTemplate>
        <div class="contenedor_tabs">
            <ul class="tabs_ttle dvddo_in_11" id="MenuCasesAllCases">
                <li class="active">
                    <asp:LinkButton runat="server" ID="lnkAllOpen" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> ALL OPEN </asp:LinkButton>
                    <i class="punt" runat="server" id="AllOpenCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkProcessing" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> PROCESSING </asp:LinkButton>
                    <i class="punt" runat="server" id="ProcsssingCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkAwaitingInfo" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> AWAITING INFO </asp:LinkButton>
                    <i class="punt" runat="server" id="AwatingCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkReinsurance" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> REINSURANCE </asp:LinkButton>
                    <i class="punt" runat="server" id="ReinsuranceCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkAlerts" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> ALERTS </asp:LinkButton>
                    <i class="punt" runat="server" id="AlertCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkShowExceptions" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> SHOW EXCEPTIONS </asp:LinkButton>
                    <i class="punt" runat="server" id="ShowExceptionsCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkRecent" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> RECENT </asp:LinkButton>
                    <i class="punt" runat="server" id="RecentCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkChanges" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> CHANGES </asp:LinkButton>
                    <i class="punt" runat="server" id="ChangesCountText"></i>
                </li>
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkSearchResults" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> SEARCH RESULTS </asp:LinkButton>
                    <i class="punt" runat="server" id="SearchResultCountText"></i>
                </li>
                 <li class="">
                    <asp:LinkButton runat="server" ID="lnkHistory" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> HISTORY </asp:LinkButton>
                    <i class="punt" runat="server" id="HistoryResultCountText"></i>
                </li>
                <%--MAVELAR  3/10/2017--%> 
                <li class="">
                    <asp:LinkButton runat="server" ID="lnkRejected" ClientIDMode="Static" OnClick="SelectTabAllCases" OnClientClick="ViewTabs();BeginRequestHandler();"> REJECTED </asp:LinkButton>
                    <i class="punt" runat="server" id="RejectedCasesCountText"></i>
                </li>
                <%--FIN MAVELAR  3/10/2017--%> 
            </ul>
            <div style="clear: both;">
            </div>
        </div>

        <div id="contenedorTabsMenuCasesAllCases">
            <asp:Panel runat="server" ID="dvGridAllOpen" CssClass="wd100 fl all_open" ClientIDMode="Static">
                <uc1:UCCasesGrid runat="server" ID="UCCasesGrid1" />
            </asp:Panel>
        </div>

        <asp:HiddenField ID="hfMenuCasesAllCases" ClientIDMode="Static" runat="server" />

    </ContentTemplate>
   <Triggers>
        <asp:AsyncPostBackTrigger ControlID="lnkAllOpen" />
        <asp:AsyncPostBackTrigger ControlID="lnkProcessing" />
        <asp:AsyncPostBackTrigger ControlID="lnkAwaitingInfo" />
        <asp:AsyncPostBackTrigger ControlID="lnkReinsurance" />
        <asp:AsyncPostBackTrigger ControlID="lnkAlerts" />
        <asp:AsyncPostBackTrigger ControlID="lnkShowExceptions" />
        <asp:AsyncPostBackTrigger ControlID="lnkRecent" />
        <asp:AsyncPostBackTrigger ControlID="lnkChanges" />
        <asp:AsyncPostBackTrigger ControlID="lnkSearchResults" />    
       <asp:AsyncPostBackTrigger ControlID="lnkHistory" /> 
       <%--MAVELAR  3/10/2017--%>      
       <asp:AsyncPostBackTrigger ControlID="lnkRejected" /> 
       <%--FIN MAVELAR  3/10/2017--%>         
    </Triggers>
</asp:UpdatePanel>
