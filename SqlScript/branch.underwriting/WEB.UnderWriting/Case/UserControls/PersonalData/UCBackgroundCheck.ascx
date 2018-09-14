﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCBackgroundCheck.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.PersonalData.UCBackgroundCheck" %>
<%@ Register Src="~/Case/UserControls/PersonalData/BackgroundCheckLink.ascx" TagPrefix="uc1" TagName="BackgroundCheckLink" %>

<ul class="secundario" style="display: none;">
    <li class="bgcheck">
        <uc1:BackgroundCheckLink runat="server" ID="BackgroundCheckLink" />
        <fieldset>
            <legend>Result General</legend>
            <div class=" wd20 mR-2-p fl mB22">
                <label class="label">
                    Reason:</label>
                <asp:TextBox ID="ReasonTxt" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
                <label class="label">
                    User:</label>
                <asp:TextBox ID="UserTxt" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
                <label class="label">
                    Results</label>
                <asp:TextBox ID="ResultsTxt" class="bgRJ-osc txtBL" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
                <label class="label">
                    Date:</label>
                <asp:TextBox ID="DateTxt" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
            </div>
            <div class=" wd78 fl mB22">
                <label class="label">
                    Comments:</label>
                <asp:TextBox ID="CommentsTxt" runat="server" TextMode="MultiLine" class="textarea_coment ReadOnly" ReadOnly="true"></asp:TextBox>
            </div>
        </fieldset>

        <%--         <div class="tbl-bgcheck wd100">
          <asp:GridView ID="gvBackGroundCheck" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="2"
                DataKeyNames="DocCategoryId,DocTypeId,DocumentId" OnPageIndexChanging="gvBackGroundCheck_OnPageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="File Name" HeaderStyle-CssClass="gradient_gris">
                        <ItemTemplate>
                            <div class="mL"><%# Eval("DocumentName")%></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Submitted Date" HeaderStyle-CssClass="gradient_gris">
                        <ItemTemplate>
                            <div><%# Eval("FileCreationDate")%></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PDF Files" HeaderStyle-CssClass="gradient_gris">
                        <ItemTemplate>
                            <div class="alignC">
                                <asp:LinkButton ID="BackgroundCheckPdfBTN" runat="server" CssClass="pdf_ico" OnClick="BackgroundCheckPdfBTN_Click">
                                </asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <div class="pagination">
                        <p>
                            Page
                            <asp:Literal ID="indexPage" runat="server" />
                            of
                            <asp:Literal ID="totalPage" runat="server" />
                            (<asp:Literal ID="totalItems" runat="server" />
                            items)
                        </p>
                        <asp:LinkButton runat="server" CssClass="rewd" CommandName="Page" CommandArgument="First" ID="firstButton" />
                        <asp:LinkButton runat="server" CssClass="prev" CommandName="Page" CommandArgument="Prev" ID="prevButton" />

                        <asp:LinkButton runat="server" CssClass="next" CommandName="Page" CommandArgument="Next" ID="nextButton" />
                        <asp:LinkButton runat="server" CssClass="fwrd" CommandName="Page" CommandArgument="Last" ID="lastButton" />
                    </div>
                </PagerTemplate>
            </asp:GridView>
          
        </div>--%>
        <%--        <div class="tbl-bgcheck wd100">
            <asp:GridView ID="gvBGCLinks" runat="server" AutoGenerateColumns="false"  AllowPaging="true" PageSize="2" OnPageIndexChanging="gvBGCLinks_OnPageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="URL" HeaderStyle-CssClass="gradient_gris">
                        <ItemTemplate>
                            <div class="mL">
                                <a onclick="window.open('<%# Eval("StepExtraInfoDesc")%>', '_blank')"><%# Eval("StepExtraInfoDesc")%> </a>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="gradient_gris">
                        <ItemTemplate>
                            <div>
                                <div class='<%#  Boolean.Parse(Eval("Match").ToString())?  "SlideMatch" : "SlidePending"  %>'></div>
                                <%# Boolean.Parse(Eval("Match").ToString())?  "Matched" : "Related" %>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <div class="pagination">
                        <p>
                            Page
                            <asp:Literal ID="indexPage" runat="server" />
                            of
                            <asp:Literal ID="totalPage" runat="server" />
                            (<asp:Literal ID="totalItems" runat="server" />
                            items)
                        </p>
                        <asp:LinkButton runat="server" CssClass="rewd" CommandName="Page" CommandArgument="First" ID="firstButton" />
                        <asp:LinkButton runat="server" CssClass="prev" CommandName="Page" CommandArgument="Prev" ID="prevButton" />

                        <asp:LinkButton runat="server" CssClass="next" CommandName="Page" CommandArgument="Next" ID="nextButton" />
                        <asp:LinkButton runat="server" CssClass="fwrd" CommandName="Page" CommandArgument="Last" ID="lastButton" />
                    </div>
                </PagerTemplate>
            </asp:GridView>
        </div>--%>
    </li>
</ul>