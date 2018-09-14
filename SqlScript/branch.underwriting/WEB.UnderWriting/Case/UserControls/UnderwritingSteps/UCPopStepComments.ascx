﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPopStepComments.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.UnderwritingSteps.UCPopStepComments" %>
<asp:UpdatePanel ID="upUSStepComments" runat="server">
    <ContentTemplate>
        <div class="step_minfo">
            <div class="cont">
                <div class="mensaje_steps wd100 ">
                    <div class="p_txt wd100 ">
                        <asp:Repeater ID="gvUSComments" runat="server">
                            <ItemTemplate>
                                <span><strong>USER:</strong> <%# Eval("OriginatedByName") %> | <strong>DATE:</strong> <%# Eval("Date") %> | <strong>TIME:</strong> <%# Eval("Time") %></span>
                                <%--<p class="titulo_steps">1. Analisis de Sangre y Examen de Orina - Vital Lab</p>--%>
                                <p class="mB20"><%# Eval("NoteDesc") %></p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="wd100 fl hg35">
                        <asp:Panel ID="pnlBtnCloseStep" runat="server" ClientIDMode="Static" CssClass="boton_wrapper gradient_RJ bdrRJ fr">
                            <span class="equis"></span>
                            <asp:Button ID="btnCloseStep" runat="server" CssClass="boton" Text="Close Step" OnClientClick="return ShowUSAddComment('close');" />
                        </asp:Panel>
                        <asp:Panel ID="pnlBtnVoidStep" runat="server" ClientIDMode="Static" CssClass="boton_wrapper gradient_RJ bdrRJ fr mR">
                            <span class="decline"></span>
                            <asp:Button ID="btnVoidStep" runat="server" CssClass="boton" Text="Void Step" OnClientClick="return ShowUSAddComment('void');" />
                        </asp:Panel>
                        <asp:Panel ID="pnlBtnAddComment" runat="server" ClientIDMode="Static" CssClass="boton_wrapper gradient_AM_btn bdrAM txtNG-f fr mR">
                            <span class="add"></span>
                            <asp:Button ID="btnUSAddComment" ClientIDMode="Static" CssClass="boton" runat="server" OnClientClick="return ShowUSAddComment('comment');" Text="Add Comment" />
                        </asp:Panel>
                    </div>
                    <div id="steps_comentario" style="display: none;">
                        <div id="ddlforBackgroundCheck" style="display: none;">
                            <strong>Result:</strong>
                            <asp:DropDownList ID="ddlResultBgCheck" runat="server">
                                <asp:ListItem Value="true"> Matched</asp:ListItem>
                                <asp:ListItem Value="false"> Not Matched</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <asp:TextBox ID="txtUSCNewComment" ClientIDMode="Static" runat="server" TextMode="MultiLine" Style="float: left;" CssClass="wd100 hg180 mB" placeholder="Add Comment Void Step..."> </asp:TextBox>
                        <div class="wd100 fl hg35 m0">
                            <div class="boton_wrapper gradient_vd2 bdrVd2 fl mR">
                                <span class="save"></span>
                                <asp:Button ID="btnUSCSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="boton" OnClick="btnUSCSave_Click" OnClientClick="return ValidateUSSaveComment();" />
                            </div>
                            <div class="boton_wrapper gradient_RJ bdrRJ fl">
                                <span class="equis"></span>
                                <input class="boton" onclick="HideUSAddComment();" type="button" value="Cancel">
                            </div>
                        </div>
                    </div>
                    <!--// Botones -->
                </div>
                <div class="wd100 fl hg35">
                    <div class="boton_wrapper gradient_RJ bdrRJ fr">
                        <span class="equis"></span>
                        <input class="boton" type="button" value="Close" onclick="CloseUSCommentsPop();">
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnStepId" runat="server" />
        <asp:HiddenField ID="hdnStepCaseNo" runat="server" />
        <asp:HiddenField ID="hdnStepTypeId" runat="server" />
        <asp:HiddenField ID="hdnSaveCommentSender" runat="server" ClientIDMode="Static" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnUSCSave" />
        <asp:AsyncPostBackTrigger ControlID="btnVoidStep" />
        <asp:AsyncPostBackTrigger ControlID="btnCloseStep" />
    </Triggers>
</asp:UpdatePanel>