﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCFormOfPayment.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Payment.WUCFormOfPayment" %>
<%@ Register Src="~/NewBusiness/UserControls/Common/WUCGridPaginator.ascx" TagPrefix="uc1" TagName="WUCGridPaginator" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerACH.ascx" TagPrefix="uc1" TagName="UCContainerACH" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerCC.ascx" TagPrefix="uc1" TagName="UCContainerCC" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerTransfer.ascx" TagPrefix="uc1" TagName="UCContainerTransfer" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCUploadDocumentOfPayments.ascx" TagPrefix="uc1" TagName="UCUploadDocumentOfPayments" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCCash.ascx" TagPrefix="uc1" TagName="UCCash" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerCash.ascx" TagPrefix="uc1" TagName="UCContainerCash" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCBasicPayment.ascx" TagPrefix="uc1" TagName="UCBasicPayment" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerBasicPayment.ascx" TagPrefix="uc1" TagName="UCContainerBasicPayment" %>
 <%--Bmarroquin 11-02-2017 a raiz de tropicalizacion de ESA, se incorpora los controles abajo expuestos para procesar los pagos de ESA--%>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerCargoAutomatico.ascx" TagPrefix="uc1" TagName="UCContainerCargoAutomatico" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerTarjetasPagos.ascx" TagPrefix="uc1" TagName="UCContainerTarjetasPagos" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerPagoNPE.ascx" TagPrefix="uc1" TagName="UCContainerPagoNPE" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/UCContainerPagoCheque.ascx" TagPrefix="uc1" TagName="UCContainerPagoCheque" %>

<asp:UpdatePanel ID="udpFormOfPayment" runat="server" ClientIDMode="Static">
    <ContentTemplate>
        <div class="fix_height">
            <!--nivela los altos importante-->
            <div class="fondo_blanco fix_height">
                <div class="titulos_azules"><span class="payment"></span><strong runat="server" id="FormOfPayment">FORM OF PAYMENT</strong></div>
                <asp:MultiView ID="MVPaymentForm" runat="server" ActiveViewIndex="0">
                    <asp:View ID="VContainerACH" runat="server">
                        <uc1:UCContainerACH runat="server" ID="UCContainerACH" />
                    </asp:View>
                    <asp:View ID="VContainerCC" runat="server">
                        <uc1:UCContainerCC runat="server" ID="UCContainerCC" />
                    </asp:View>
                    <asp:View ID="VContainerTransfer" runat="server">
                        <uc1:UCContainerTransfer runat="server" ID="UCContainerTransfer" />
                    </asp:View>
                    <asp:View ID="VContainerCash" runat="server">
                        <uc1:UCContainerCash runat="server" ID="UCContainerCash" />
                    </asp:View>
                    <asp:View ID="VContainerBasicPayment" runat="server">
                        <uc1:UCContainerBasicPayment runat="server" ID="UCContainerBasicPayment" />
                    </asp:View>
                    <%--Bmarroquin 11-02-2017 a raiz de tropicalizacion de ESA, se incorpora los controles abajo expuestos para procesar los pagos de ESA--%>
                    <asp:View ID="VCargoAutomatico" runat="server">
                        <uc1:UCContainerCargoAutomatico runat="server" ID="UCContainerCargoAutomatico" />
                    </asp:View>
                    <asp:View ID="VTarjetaPagos" runat="server">
                        <uc1:UCContainerTarjetasPagos runat="server" ID="UCContainerTarjetasPagos" />
                    </asp:View>
                    <asp:View ID="VPagoNPE" runat="server">
                        <uc1:UCContainerPagoNPE runat="server" ID="UCContainerPagoNPE" />
                    </asp:View>
                    <asp:View ID="VPagoCheque" runat="server">
                        <uc1:UCContainerPagoCheque runat="server" ID="UCContainerPagoCheque" />
                    </asp:View>                    
                </asp:MultiView>
                <div class="grupos de_1">
                    <div>
                        <table id="tblUpload" style="position: relative; left: 6px; width: 98.13%">
                            <tbody>
                                <tr>
                                    <td width="80%">
                                        <div>
                                            <label class="label" runat="server" id="Upload">Upload</label>
                                            <asp:TextBox ID="txtPath" runat="server" Text="" label="FileUploadRequiredMessage" changeMessage="true" Enabled="false" validation="Required"></asp:TextBox>
                                        </div>
                                    </td>
                                    <td>
                                        <div>
                                            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                                <asp:View ID="vActive" runat="server">
                                                    <div class="boton_wrapper azul float_right" style="position: relative; top: 3px;">
                                                        <span class="upload"></span>
                                                        <asp:Button ID="btnAttach" CssClass="boton" runat="server" Text="Attach" OnClick="Button1_Click" />
                                                    </div>
                                                </asp:View>

                                                <asp:View ID="vInactive" runat="server">
                                                    <div class="boton_wrapper inactive float_right">
                                                        <span class="process_inactive"></span>
                                                        <asp:Button CssClass="aspNetDisabled boton" Text="Attach" runat="server" ID="btnProcessPayment2" Enabled="false" disabled="disabled" />
                                                    </div>
                                                </asp:View>
                                            </asp:MultiView>
                                            <!--boton_wrapper-->
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="content_fondo_blanco">
                    <uc1:UCUploadDocumentOfPayments runat="server" ID="UCUploadDocumentOfPayments" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>