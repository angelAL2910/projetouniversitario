﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCVehiclesInformation.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.IllustrationsVehicle.UCVehiclesInformation" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="PdfViewerAspNet" Namespace="PdfViewer4AspNet" TagPrefix="PdfViewer" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/UCEndosoCesion.ascx" TagPrefix="uc1" TagName="UCEndosoCesion" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/WUCVechicleEditForm.ascx" TagPrefix="uc1" TagName="WUCVechicleEditForm" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCBlackListValidation.ascx" TagPrefix="uc1" TagName="WUCBlackListValidation" %>
<%@ Register Src="~/NewBusiness/UserControls/IllustrationsVehicle/WUCTBSiniestraldidad.ascx" TagPrefix="uc1" TagName="WUCTBSiniestraldidad" %>

<div class="cont_gnl tab_pane_container rcomp mT25">
    <div class="round_blue "><%=RESOURCE.UnderWriting.NewBussiness.Resources.InsuredRisk %></div>   
    <div class="reqVehiculo">
        <div class="tbl data_Gpl gvVehiculos rasegu">                      
            <dx:ASPxGridView 
                    ID="gvVehicle" 
                    runat="server" 
                    EnableCallBacks="False" 
                    KeyFieldName="InsuredVehicleId;VehicleUniqueId;Registry;Chassis;VehicleValue" 
                    ClientIDMode="Static" 
                    AutoGenerateColumns="false" 
                    Width="100%" 
                    OnRowCommand="gvVehicle_RowCommand" 
                    OnPreRender="gvVehicle_PreRender" 
                    OnPageIndexChanged="gvVehicle_PageIndexChanged" OnHtmlRowCreated="gvVehicle_HtmlRowCreated">
                    <Columns>
                        <dx:GridViewDataColumn Caption="Editar" Name="Edit" Visible="true">
                            <DataItemTemplate>                                    
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">                                 
                                            <asp:LinkButton 
                                                runat="server" 
                                                ID="lnkEdit" 
                                                CommandName="EditVehicle" CssClass="myedit_file">
                                            </asp:LinkButton>                                                 
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkEdit" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataTextColumn Caption="Marca" FieldName="MakeDesc" Name="Make">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Modelo" FieldName="ModelDesc" Name="Model">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Año" FieldName="Year" Name="Year" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Placa" FieldName="Registry" Name="Registry" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Chasis" FieldName="Chassis" Name="VehicleChasis" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Color" FieldName="Color" Name="VehicleColor" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Uso Principal" FieldName="UsageDesc" Name="PrincipalUse" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Capacidad" FieldName="VehicleCapacity" Name="CapacityLabel">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tipo Combustible" FieldName="fuelTypeDesc" Name="FuelTypeLabel">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Estacionamiento" FieldName="StoredDesc" Name="Parking" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Conductor" FieldName="Driver" Name="Driver" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Plan" FieldName="PlanName" Name="PlanLabel" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn Caption="Deducible" FieldName="Deductible" Name="Deductible" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>   
                        <dx:GridViewDataTextColumn Caption="Monto Asegurado" FieldName="InsuredAmount" Name="InsuredAmount" CellStyle-HorizontalAlign="Right">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>                                       
                        <dx:GridViewDataTextColumn Caption="Prima sin Impuesto" FieldName="PremiumAmount" Name="PremiumWithoutTax" CellStyle-HorizontalAlign="Right">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Tasa" FieldName="Rate" Name="Rate" CellStyle-HorizontalAlign="Right">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>                                                     
                        <dx:GridViewDataTextColumn Caption="Siniestraldad Modelo" FieldName="SIModelo" Name="SIModelo" CellStyle-HorizontalAlign="Center">
                            <Settings AllowHeaderFilter="False" AllowSort="true" />
                        </dx:GridViewDataTextColumn>    
                        <dx:GridViewDataColumn Caption="Tabla Siniestralidad" Name="TBSiniestralidad" Width="100">
                            <DataItemTemplate>                                    
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">                                 
                                            <asp:LinkButton 
                                                runat="server" 
                                                ID="lnkTBSiniestralidad" 
                                                CommandName="TBSiniestralidad" CssClass="view_file" >
                                            </asp:LinkButton> 
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkTBSiniestralidad" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn> 
                        <dx:GridViewDataColumn Caption="Marbete" Name="Tag" Width="100">
                            <DataItemTemplate>                                    
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">                                 
                                            <asp:LinkButton 
                                                runat="server" 
                                                ID="lnkTag" 
                                                CommandName="tag" 
                                                Enabled='<%# Eval("EnableTag") %>' 
                                                CssClass='<%# Eval("CssClassMarbete")%>'></asp:LinkButton> 
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkTag" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>  
                        <dx:GridViewDataColumn Caption="Ver Inspeccion" Name="Inspection" Width="100">
                            <DataItemTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <asp:CheckBox 
                                                Checked='<%# Eval("Inspection") %>' 
                                                Enabled="false" 
                                                ID="chkInspection" 
                                                runat="server" 
                                                Visible='<%# Eval("VisibleChkInspection") %>' />
                                            <asp:LinkButton 
                                                CommandName="Inspeccion" 
                                                CssClass='<%# Eval("CssClassInspection") %>' 
                                                Enabled='<%# Eval("Inspection") %>' 
                                                ID="lnkInspeccion" 
                                                runat="server" 
                                                Visible='<%# Eval("VisibleLnkInspeccion") %>'></asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkInspeccion" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>                          
                        <dx:GridViewDataColumn Name="InspectionAddress" Caption="Direccion de Inspeccion" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:UpdatePanel ID="updInspectionAddress" runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton 
                                                runat="server" 
                                                ID="lnkInspectionAddress" 
                                                CommandName="InspectionAddress" 
                                                Enabled='<%# Eval("EnabledInspectionAddress") %>' 
                                                CssClass='<%# Eval("CssInspectionAddress") %>'></asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkInspectionAddress" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>    
                        <dx:GridViewDataColumn Caption="Cobertura" Name="Coverage" Width="100">
                            <DataItemTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton runat="server" 
                                                ID="lnkCoverage" 
                                                CommandName="Coverage"
                                                CssClass="view_file"></asp:LinkButton>  
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkCoverage" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Endoso" Name="Endorsement" Width="100">
                            <DataItemTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton 
                                                runat="server" 
                                                ID="lnkEndosoCesion" 
                                                Enabled='<%# Eval("available") %>' 
                                                Visible='<%# Eval("HasOwnDamage") %>' 
                                                CommandName="Endoso" 
                                                CssClass='<%# Eval("ClassEndoso")%>'></asp:LinkButton> 
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkEndosoCesion" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Endoso aclaratorio" Name="EndorsementClarifying" Width="100">
                            <DataItemTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton 
                                                CommandName="EndosoAclaratorio" 
                                                CssClass='<%# Eval("CssClassEndorsementClarifying")%>' 
                                                Enabled='<%#Eval("EndorsementClarifying") %>' 
                                                ID="lnkEndosoAclaratorio" 
                                                runat="server"></asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkEndosoAclaratorio" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="Endoso de cesion de derecho" Name="EndorsementOfTransferOfRight" Width="100">
                            <DataItemTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div style="text-align: center">
                                            <asp:LinkButton 
                                                CommandName="EndosoCesionDerecho" 
                                                ID="lnkEndosoCesionDerecho" 
                                                CssClass='<%# Eval("CssClassEndorsementOfTransferOfRight")%>' 
                                                Enabled='<%# Eval("availableEndorsementOfTransferOfRight") %>' 
                                                runat="server"></asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkEndosoCesionDerecho" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                            <Settings AllowHeaderFilter="False" AllowSort="False" />
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Name="BlackList"> 
                            <DataItemTemplate>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton 
                                                runat="server" 
                                                ID="lnkBlackList" 
                                                CommandName="BlackList" 
                                                CssClass='view_file'></asp:LinkButton>   
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkBlackList" EventName="Click"/>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsPager PageSize="2147483647" AlwaysShowPager="true">
                        <PageSizeItemSettings Visible="false" ShowAllItem="true" />
                    </SettingsPager>
                    <SettingsBehavior AllowSelectSingleRowOnly="false" AllowSort="False" AllowFocusedRow="false" />
                    <Settings ShowFooter="True" />
                </dx:ASPxGridView>                                             
        </div>
    </div>  
</div>   

<asp:ModalPopupExtender ID="mpeVehicleEditForm" PopupControlID="pnVehicleEditForm" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopVehicleEditForm" BehaviorID="popupBhvr1VehicleEditForm" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="pnVehicleEditForm" CssClass="modalPopup" ClientIDMode="Static" style="display: none;">              
    <div class="pop_up_wrapper" style="width: 1000px; height: 276px; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 31%;">
                    <asp:Label ID="Label1" ClientIDMode="Static" runat="server"></asp:Label>
                </li>
                <li style="top: 13%;">
                    <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent;border: 0;" onclick="ClosePopVehicleForm();"/>
                </li>
            </ul>
        </div>                                                                     
        <!--titulos_azules-->
        <uc1:WUCVechicleEditForm runat="server" id="WUCVechicleEditForm" />
        <!--content_fondo_blanco-->
    </div>
    <asp:HiddenField runat="server" ID="hdnPopVehicleEditForm" Value="false" />
</asp:Panel> 
<asp:ModalPopupExtender ID="ModalPopupShowPDF" PopupControlID="pnShowPDF" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPDF" BehaviorID="popupBhvr1PDF" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="pnShowPDF" CssClass="modalPopup" ClientIDMode="Static" style="display: none;">              
    <div class="pop_up_wrapper" style="width: 1189px; height: 752px; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 31%;">
                    <asp:Label ID="ltTypeDoc2" ClientIDMode="Static" runat="server"><%=RESOURCE.UnderWriting.NewBussiness.Resources.PrintPdfHeader %></asp:Label>
                </li>
                <li style="top: 13%;">
                    <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent;border: 0;" onclick="ClosePop();"/>
                </li>
            </ul>
        </div>
        <!--titulos_azules-->
        <PdfViewer:PdfViewer 
            ID="pdfViewerMyPreviewPDF" 
            CssClass="PdfViewer" 
            runat="server" 
            Height="712" 
            Width="1186"
            ClientIDMode="Static" 
            ShowScrollbars="true" 
            ShowToolbarMode="Show">
        </PdfViewer:PdfViewer>   
        <!--content_fondo_blanco-->
    </div>
</asp:Panel>

<asp:ModalPopupExtender ID="ModalPopupCoverage" PopupControlID="popCoverage" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnShowPopCoverage" BehaviorID="popupBhvr1" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="popCoverage" CssClass="modalPopup" ClientIDMode="Static" style="display:none">
    <div class="pop_up_wrapper tbl data_Gpl coberageT" style="width: 1105px; border: 1px solid #000; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 22%;">
                    <asp:Literal ID="Title" ClientIDMode="Static" runat="server"></asp:Literal>
                </li>
                <li style="top: 13%;">
                    <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopCoverageView()" />
                </li>
            </ul>
        </div>
        <!--titulos_azules-->
        <asp:UpdatePanel runat="server" ID="udpCoverages" RenderMode="Block" style="overflow-y: auto; ">
            <ContentTemplate>
                <asp:Panel runat="server" ID="pnThirDamage"  CssClass="cont_gnl mT25">
                    <div class="round_blue"><%=RESOURCE.UnderWriting.NewBussiness.Resources.ThirdDamageLabel%></div>
                    <%--<input id="btnGetTime" style="display:none;" type="button" value="Show Current Time" onclick="ShowCurrentTime()" />--%>
                    <table>
                        <tbody>
                            <asp:Repeater runat="server" ID="RepeaterCoveragesThirdDamage" >
                                <HeaderTemplate>
                                    <table id="tblVehicles" class="tblBottom">
                                        <thead>
                                            <tr class="gradient_azul trHeader">
                                               <%-- <th align="left" style="display:none;"></th>--%>
                                                <th align="left" class="c1"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Coverage%></span></th>
                                                <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Limit%></span></th>     
                                                <% if (ObjServices.IsViewPrimeAndRateCot)
                                                   { %>
                                                <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Premium%></span></th>                                                 
                                                <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Rate%></span></th> 
                                                <% } %>
                                               <%-- <th align="center" class="c3"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Status%></span></th>   --%>                                    
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                       <%-- <td align="left" style="display:none;">
                                            <asp:HiddenField runat="server" Value='<%# Bind("DatoJson") %>' ID="hdnCoberturaPopup" ClientIDMode="Static" />
                                        </td>--%>
                                        <td align="left" style="padding-left: 21px"; class="c1"><%# WEB.NewBusiness.Common.Utility.Capitalize(Eval("CoverageDesc").ToString(), ' ') %></td>
                                        <td align="center" class="c2"><%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0}",Eval("CoverageLimit")) %></td>   
                                        <% if (ObjServices.IsViewPrimeAndRateCot)
                                           { %>
                                        <td align="center" class="c2"><%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0.00}",Eval("UnitaryPrice")) %></td>                                         
                                        <td align="center" class="c2"><%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0.000000}",Eval("PremiumPercentage")) %></td>  
                                         <% } %>
                                       <%-- <td style="text-align: center;">
                                            <asp:CheckBox runat="server" Checked='<%# Bind("Activo") %>' CssClass="pruebaActivo" ID="activoCheck"  />
                                        </td>--%>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>            
                        </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>   
                </asp:Panel>

                <asp:Panel runat="server" ID="pnSelfDamage" CssClass="cont_gnl mT25">
                    <div class="round_blue"><%=RESOURCE.UnderWriting.NewBussiness.Resources.SelfDamageLabel%></div>
                    <table>
                        <tbody>
                            <asp:Repeater runat="server" ID="RepeaterCoveragesSelfDamage">
                                <HeaderTemplate>
                                    <table id="tblVehicles" class="tblBottom">
                                        <thead>
                                            <tr class="gradient_azul trHeader">
                                                <th align="left" class="c1"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Coverage%></span></th>
                                                <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Limit%></span></th>
                                                <th align="center" class="c3"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Deductible%></span></th> 
                                                <th align="center" class="c3"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.MinDeductible%></span></th> 

                                                <% if (ObjServices.IsViewPrimeAndRateCot)
                                                   { %>
                                                   <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Premium%></span></th>                                                 

                                                <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Rate%></span></th>                                            
                                                <% } %>
                                            </tr>      
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" style="padding-left: 21px;" class="c1"><%#WEB.NewBusiness.Common.Utility.Capitalize(Eval("CoverageDesc").ToString(),' ') %></td>
                                        <td align="center" class="c2"><%#string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0}",Eval("CoverageLimit")) %></td>
                                        <td align="center" class="c3">                                
                                             <%# Eval("DeductiblePercentage") %>  
                                        </td>
                                        <td align="center" class="c3">                                
                                             <%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0.00}",Eval("DeductibleAmount")) %>  
                                        </td>  
                                        <% if (ObjServices.IsViewPrimeAndRateCot)
                                           { %>
                                        <td align="center" class="c2"><%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0.00}",Eval("UnitaryPrice")) %></td>  
                                       
                                        <td align="center" class="c2"><%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0.000000}",Eval("PremiumPercentage")) %></td>                                   
                                         <% } %>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>          
                        </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnServices" runat="server" CssClass="cont_gnl mT25">
                    <div class="round_blue"><%=RESOURCE.UnderWriting.NewBussiness.Resources.AdditionalServviceLabel%></div>
                    <table>
                        <tbody>
                            <asp:Repeater runat="server" ID="RepeaterCoveragesAdditional">
                                <HeaderTemplate>
                                    <table id="tblVehicles" class="tblBottom">
                                        <thead>
                                            <tr class="gradient_azul trHeader">
                                                <th align="left" class="c1"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Coverage%></span></th> 
                                                <th align="center" class="c2"><span><%=RESOURCE.UnderWriting.NewBussiness.Resources.Premium%></span></th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td align="left" class="c1" style="padding-left: 21px;"><%# WEB.NewBusiness.Common.Utility.Capitalize(Eval("CoverageDesc").ToString(),' ') %></td>                                   
                                         <td align="center" class="c2"><%# string.Format(System.Globalization.CultureInfo.InvariantCulture,"{0:#,0.00}",Eval("UnitaryPrice")) %></td>                                         
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tbody>            
                        </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </asp:Panel>
                <div style="clear:both;"></div>
               <%-- <div style="float: right;">
                    <div style="margin-top: 10px; width: 315px;">
                        <asp:LinkButton runat="server" ID="btnSaveCoverage" ClientIDMode="Static" CssClass="col-6 fl button button-red alignC embossed" OnClick="btnSaveCoverage_Click">
                                        <%=RESOURCE.UnderWriting.NewBussiness.Resources.Save %>   
                        </asp:LinkButton>

                         <a href="#" class="col-6 fl button button-blue alignC embossed"  onclick="ClosePopCoverageView()"><%=RESOURCE.UnderWriting.NewBussiness.Resources.Cancel %>   </a>

                    </div>
                </div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--content_fondo_blanco-->
    </div>
</asp:Panel>
<asp:ModalPopupExtender ID="ModalPopupEndoso" PopupControlID="popEndoso" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnEndosoPopup" BehaviorID="popupEndoso" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="popEndoso" CssClass="modalPopup" ClientIDMode="Static" style="display:none">
    <div class="pop_up_wrapper tbl data_Gpl coberageT" style="width: 800px; height: 452px;border: 1px solid #000; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 18%;">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.EndosoTitulo%></span>                  
                </li>
                <li style="top: 13%;">
                    <input type="button" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopEndoso();"/>
                </li>
            </ul>
        </div>
        <!--titulos_azules-->
        <uc1:UCEndosoCesion runat="server" id="UCEndosoCesion" />
        <!--content_fondo_blanco-->
    </div>
</asp:Panel>  
<asp:ModalPopupExtender ID="ModalPopupBlackList" PopupControlID="popBlackList" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopBlackList" BehaviorID="popupBlackList" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="popBlackList" CssClass="modalPopup" ClientIDMode="Static" style="display:none">
    <div class="pop_up_wrapper tbl data_Gpl coberageT" style="width: 800px; height: 340px;border: 1px solid #000; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 18%;">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.BlackList%></span>                  
                </li>
                <li style="top: 13%;">
                    <input type="button" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopBlackList();"/>
                </li>
            </ul>
        </div>
        <!--titulos_azules-->
        <uc1:WUCBlackListValidation runat="server" id="WUCBlackListValidation" />
        <!--content_fondo_blanco-->
        <asp:HiddenField runat="server" Value="false" ID="hdnPopBlackList" ClientIDMode="Static" />
    </div>
</asp:Panel>  


<asp:ModalPopupExtender ID="ModalPopupTBSiniestralidad" PopupControlID="pnTBSiniestralidad" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnPopTBSiniestralidad" BehaviorID="popupBhvrTBSiniestralidad" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="pnTBSiniestralidad" CssClass="modalPopup" ClientIDMode="Static" style="display: none;">              
    <div class="pop_up_wrapper" style="width: 750px; height: 434px; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 31%;">
                    <asp:Label ID="Label2" ClientIDMode="Static" runat="server"> Tabla de Siniestralidad</asp:Label>
                </li>
                <li style="top: 13%;">
                    <input type="button" id="close_pop_up" class="cls_pup" style="background-color: transparent;border: 0;" onclick="ClosePopTBSiniestralidad();"/>
                </li>
            </ul>
        </div>                                                                     
        <!--titulos_azules-->
        <uc1:WUCTBSiniestraldidad runat="server" id="WUCTBSiniestraldidad" />
        <!--content_fondo_blanco-->
    </div>
    <asp:HiddenField runat="server" ID="hdnPopTBSiniestralidad" ClientIDMode="Static" Value="false" />
</asp:Panel>


<asp:ModalPopupExtender ID="ModalPopupInspectionAddress" PopupControlID="popInspectionAddress" DropShadow="false" BackgroundCssClass="ModalBackgroud3" TargetControlID="hdnInspectionAddressPopup" BehaviorID="popupInspectionAddress" runat="server"></asp:ModalPopupExtender>
<asp:Panel runat="server" ID="popInspectionAddress" CssClass="modalPopup" ClientIDMode="Static" style="display:none">
    <div class="pop_up_wrapper tbl data_Gpl coberageT" style="width: 800px; height: 750px;border: 1px solid #000; overflow-x: hidden; overflow-y: hidden">
        <!--escriben por style el ancho que desean-->
        <div class="titulos_azules head_contengridproxi PreviewPDF" style="width: 100%;">
            <ul>
                <li style="position: absolute; left: 41%; top: 18%;">
                    <span><%=RESOURCE.UnderWriting.NewBussiness.Resources.VehicleInspectionAddress%></span>
                </li>
                <li style="top: 13%;">
                    <input type="button" class="cls_pup" style="background-color: transparent; border: none;" onclick="ClosePopInspectionAddress();"/>
                </li>
            </ul>
        </div>
        <!--titulos_azules-->
        <!--content_fondo_blanco-->
        <div id="divInspectionAddress">
            <div style="height: 15px;"></div>
            <div class="">
                <label class="label titulo"><%=RESOURCE.UnderWriting.NewBussiness.Resources.AddressLabel %>:</label>
                <asp:TextBox 
                    ClientIDMode="Static" 
                    Columns="20" 
                    ID="txtInspectionAddress" 
                    Rows="5" 
                    runat="server"                     
                    validation='Required' TextMode="MultiLine"></asp:TextBox>                                       
                 </div>  
             </div> 
                    <div ID="ltSelectedAddressOnMap" style="font-size: 16px;color: #065456;">Ubicación seleccionada en el mapa :</div>                    
               <div>                   
                    <div id="dvInputSearch" style="display:none;"></div>                                                          
                <div id="map" style="height: 368px">
                   
                </div>
                </div>
               <div class="row_A mT20 mB15">
                   <div class="col-8 fl"><asp:CheckBox runat="server" ID="chkCloneAllAddress" CssClass="fl mR"/><span class="fl">Usar la misma ubicación para todos los vehiculos</span> </div>
                   <div class="col-4 fl">
                         <input 
                    type="button" 
                    name="btnCancelar" 
                    id="btnCancelar" 
                    value='<%=RESOURCE.UnderWriting.NewBussiness.Resources.Cancel %>' 
                    class="fr button button-red alignC embossed "
                    onclick="ClosePopInspectionAddress();" />
                
                <asp:Button 
                    runat="server" 
                    ID="btnSaveAddress" 
                    CssClass="mR fr button button-blue alignC embossed " 
                    OnClientClick="return validateForm('divInspectionAddress')" 
                    OnClick="btnSaveAddress_Click" 
                    Text="Save Address" /> 
                   </div>
                                  
               
                   </div>                     
              
    </div>
      
</asp:Panel>
<asp:HiddenField runat="server" ID="hdnlongitudSelectedVehicle" ClientIDMode="Static" Value="0" />
<asp:HiddenField runat="server" ID="hdnlatitudelectedVehicle" ClientIDMode="Static" Value="0" />
<asp:HiddenField runat="server" Value="0" ID="hdntotalInsuredAmount" ClientIDMode="Static" />
<asp:HiddenField runat="server" Value="0" ID="hdntotalDeductible" ClientIDMode="Static" />
<asp:HiddenField runat="server" Value="0" ID="hdntotalPremiumAmount" ClientIDMode="Static" /> 
<asp:HiddenField runat="server" Value="false" ID="hdnShowPDF" ClientIDMode="Static" />
<asp:HiddenField runat="server" Value="false" ID="hdnEndosoPopup" ClientIDMode="Static" />
<asp:HiddenField runat="server" Value="false" ID="hdnShowPopCoverage" ClientIDMode="Static" />
<asp:HiddenField runat="server" Value="false" ID="hdnInspectionAddressPopup" ClientIDMode="Static" />      