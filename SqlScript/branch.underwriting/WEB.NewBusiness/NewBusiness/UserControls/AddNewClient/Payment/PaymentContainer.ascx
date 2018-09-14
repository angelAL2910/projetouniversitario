﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentContainer.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Payment.PaymentContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/WUCPaymentInformation.ascx" TagPrefix="uc1" TagName="WUCPaymentInformation" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/WUCFormOfPayment.ascx" TagPrefix="uc1" TagName="WUCFormOfPayment" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Payment/WUCPayment.ascx" TagPrefix="uc1" TagName="WUCPayment" %>

<div class="titulos_sin_accion02"></div>
<div class="fondo_gris" id="dvPayments" style="display: none">
    <div class="col-1-1">
        <div class="content_fondo_gris">
            <div class="col-1-3" id="dvPaymentInformation">
                <uc1:WUCPaymentInformation runat="server" ID="WUCPaymentInformation" />
            </div>
            <!--col-1-3-->

            <div class="col-1-3">
                <uc1:WUCFormOfPayment runat="server" ID="WUCFormOfPayment" />
            </div>
            <!--col-1-3-->

            <div class="col-1-3">
                <uc1:WUCPayment runat="server" ID="WUCPayment" />
            </div>
            <!--col-1-3-->

        </div>
        <!--content_fondo_gris-->
    </div>

    <asp:UpdatePanel runat="server" ID="udpUpdate">
        <ContentTemplate>
            <div class="col-1-1">
                <div class="barra_sub_menu">
                    <div class="grupos de_2_b last">
                        <div class="grupos de_2">
                            <div>
                                <div class="btn_celeste">
                                    <span class="see_ilustracion"></span>
                                    <asp:Button runat="server" ID="btnSubmitSTL" CssClass="boton" Text="SUBMIT TO DATA REVIEW" OnClick="btnSubmitSTL_Click" OnClientClick="return ConfirmSubmitToSTL(this)" />
                                </div>
                                <!--btn_celeste-->
                            </div>
                        </div>
                        <!--grupos-->
                    </div>
                    <!--grupos-->
                </div>
                <!--barra_sub_menu-->
            </div>
            <!--col-1-1-->
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>