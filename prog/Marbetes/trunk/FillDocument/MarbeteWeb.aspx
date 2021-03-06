﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MarbeteWeb.aspx.cs" Inherits="MarbeteWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Statetrust.Framework.Web" Namespace="Statetrust.Framework.Web.WebParts.UserControls.UserProfile"
    TagPrefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <!--no dejar estos metas-->
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>State Trust | Impresión de Marbetes</title>
    <!--en este orden-->
    <link rel="stylesheet" type="text/css" href="Styles/reset.css" />
    <link href="Styles/dtl.css" rel="stylesheet" type="text/css" />
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/kickstart.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/Loading.css" />
    <link href="cssSecurity/cssSecurity.css" rel="stylesheet" />
    <!---->
    <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
   <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.23/jquery-ui.js"></script>--%>
    <script src="cssSecurity/js/jquery-1.11.3.min.js" type="text/javascript"></script>
    <link href="cssSecurity/js/jquery-ui-1.10.3.custom.css" rel="stylesheet" type="text/css" />
    <script src="cssSecurity/js/jquery-ui-1.10.3.custom.js" type="text/javascript"></script>
</head>
<body id="mybody" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="st-container" class="st-container" style="overflow-y: auto;">
        <div id="menu-2">
            <div class="st-menu st-effect-2" id="scroll_1">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <uc1:STFCUserProfile runat="server" ID="STFCUserProfile1">
                        </uc1:STFCUserProfile>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <!--st-menu st-effect-2-->
        </div>
        <div class="st-pusher">
            <div class="st-content">
                <div id="st-trigger-effects">
                    <button data-effect="st-effect-2" id="btnLoadProfile" class="show_profile show_profile_es"
                        runat="server" onclick="GetUserProfileData(true);">
                    </button>
                </div>
                <div id="divpreloader" class="preloader" style="display: none">
                    <span class="preloader-gif"></span>
                </div>
                <div class="contenedor cajera_md">
                    <div class="cabecera">
                        <div class="logo_atl">
                        </div>
                        <span class="ttl_cj">Documento Marbete <b class="welTXT">Bienvenid@,
                            <asp:Literal ID="ltlUserName" runat="server"></asp:Literal></b></span>
                    </div>
                    <!--cabecera-->
                    <div class="contenido">
                        <div class="marbete_doc">
                            <div class="bdrRadius20 az">
                                <%-- <div class="preloader" style="display: none">
                                    <span class="preloader-gif"></span>
                                </div>--%>
                                <form action="" id="frmPDF">
                                <asp:Button ID="btnLogin" runat="server" Text="Salir" CssClass="btn_out" OnClick="btnLogin_Click" />
                                <div runat="server" id="dvAtras" visible="false" class="back_btn">
                                    <asp:LinkButton ID="btnAtras" runat="server" CssClass="back_btn" Text="Volver Atras"
                                        OnClick="btnAtras_Click" />
                                    <i class="flecha_iz"></i></asp:LinkButton>
                                </div>
                                <%--<script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
                                <script src="http://code.jquery.com/ui/1.11.1/jquery-ui.min.js"></script>
                                <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.1/themes/smoothness/jquery-ui.css" />--%>
                                <script src="JS/Site.js" type="text/javascript"></script>
                                <script src="JS/JSLoading.js" type="text/javascript"></script>
                                <asp:UpdatePanel ID="upMarbete" runat="server">
                                    <ContentTemplate>
                                        <div style="overflow-x: auto; border-bottom: 2px solid #525659;">
                                            <iframe id="iframePDF" clientidmode="Static" runat="server" style="height: 350px;
                                                padding: 0px; margin-bottom: 0;" frameborder="0"></iframe>
                                        </div>
                                        <div class="col-7 fl mT15">
                                            <asp:DropDownList ID="ddlPolicies" ClientIDMode="Static" onchange="SetCombine(this)"
                                                runat="server" CssClass="row_A">
                                                <asp:ListItem Text="-- Todas las Secciones --" Selected="True" Value="0">                      
                                                </asp:ListItem>
                                                <asp:ListItem Text="Sección 1" Value="1">                      
                                                </asp:ListItem>
                                                <asp:ListItem Text="Sección 2" Value="2">                     
                                                </asp:ListItem>
                                                <asp:ListItem Text="Sección 3" Value="3">                      
                                                </asp:ListItem>
                                                <asp:ListItem Text="Sección 4" Value="4">                      
                                                </asp:ListItem>
                                                <asp:ListItem Text="Sección 5" Value="5">                      
                                                </asp:ListItem>
                                                <asp:ListItem Text="Sección 6" Value="6"> 
                                                </asp:ListItem>
                                                <asp:ListItem Text="Combinar Secciones" Value="7"> 
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-5 fl mT boxCheck90">
                                            <!-- Check 90 Dias -->
                                            <div class="row_B ">
                                                <div class="pdL5 mT10 ">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="cb90Days" runat="server" ClientIDMode="Static" />
                                                        <label for="cb90Days">
                                                        </label>
                                                    </div>
                                                    <span>Emitir a 90 Días</span>
                                                </div>
                                                <div class="pdL5 mT10 ">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="cbFull" AutoPostBack="true" OnCheckedChanged="cbFull_OnCheckedChanged"
                                                            Onclick="AlertToUser();" runat="server" Checked="false" ClientIDMode="Static" />
                                                        <label for="cbFull">
                                                        </label>
                                                    </div>
                                                    <span>Emitir para seguro Full</span>
                                                    <asp:HiddenField ID="hdnIsFull" runat="server" ClientIDMode="Static" Value="false" />
                                                </div>
                                            </div>
                                            <!-- Check Seis -->
                                            <ul class="seisCheck">
                                                <li data-marbete="Marbete1" style="display: none">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="marbete1" runat="server" ClientIDMode="Static" />
                                                        <label for="marbete1">
                                                        </label>
                                                    </div>
                                                    <span>Sección 1</span> </li>
                                                <li data-marbete="Marbete2" style="display: none">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="marbete2" runat="server" ClientIDMode="Static" />
                                                        <label for="marbete2">
                                                        </label>
                                                    </div>
                                                    <span>Sección 2</span> </li>
                                                <li data-marbete="Marbete3" style="display: none">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="marbete3"  runat="server" ClientIDMode="Static" />
                                                        <label for="marbete3">
                                                        </label>
                                                    </div>
                                                    <span>Sección 3</span> </li>
                                                <li data-marbete="Marbete4" style="display: none">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="marbete4" runat="server" ClientIDMode="Static" />
                                                        <label for="marbete4">
                                                        </label>
                                                    </div>
                                                    <span>Sección 4</span> </li>
                                                <li data-marbete="Marbete5" style="display: none">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="marbete5" runat="server" ClientIDMode="Static" />
                                                        <label for="marbete5">
                                                        </label>
                                                    </div>
                                                    <span>Sección 5</span> </li>
                                                <li data-marbete="Marbete6" style="display: none">
                                                    <div class="check_lb fl mR">
                                                        <asp:CheckBox ID="marbete6" runat="server" ClientIDMode="Static" />
                                                        <label for="marbete6">
                                                        </label>
                                                    </div>
                                                    <span>Sección 6</span> </li>
                                            </ul>
                                        </div>
                                        <div id="boxPolicies" style="margin-left: 6px" class="row_B">
                                            <asp:TextBox ID="txtPolicies" PlaceHolder="ej:1-00-000000,2-00-000000" CssClass=""
                                                ClientIDMode="Static" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="row mT10">
                                            <div class="col-4
                                        fl">
                                                &nbsp;
                                            </div>
                                            <div class="col-4 fl">
                                                <asp:Button ID="btnFillPDF" runat="server" Text="" OnClientClick='return ValidatePolicies()'
                                                    OnClick="btnFillPDF_Click" CssClass="button
                                        button-green embossed col-6 fl alignC" />
                                                <asp:Button ID="btnClear" runat="server" Text="Limpiar" OnClick="btnClear_Click"
                                                    CssClass="button button-blue embossed col-6
                                        fl alignC" />
                                            </div>
                                            <div class="col-4 fl">
                                                &nbsp;
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="ddlPolicies" />
                                        <asp:PostBackTrigger ControlID="btnClear" />
                                        <asp:PostBackTrigger ControlID="btnFillPDF" />
                                        <asp:PostBackTrigger ControlID="btnAtras" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div   id="dialog1"  title="Dialog Title" hidden="hidden">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    
                                        &times;
                                    </button>
                                </div>
                                </form>
                            </div>
                        </div>
                    </div>
                    <!--contenido-->
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
