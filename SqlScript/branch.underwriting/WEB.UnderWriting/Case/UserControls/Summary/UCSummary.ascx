﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCSummary.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.Summary.UCSummary" %>
<ul class="secundario">
    <li class="summ">
        <!-- 1era tabla -->
        <div class="wd100 fl">
            <label class=" label fl">PERSONAL DATA BY ROLE ILLUSTRATION #: [<asp:Literal ID="ltrPolicyNumber" runat="server" />]</label>
            <label class=" label fr"><asp:Literal ID="ltrIsPreApproved" runat="server" />CASE PRE-APPROVED</label>
        </div>
        <div class="tbl data_G tbl-1 wd100 mB20">

            <asp:GridView ID="gvPersonalData"
                runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="gvPersonalData_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Role" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c1" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c1">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name / Last Name" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c2" ItemStyle-CssClass="c2" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ContactFullName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c4" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c4">
                        <ItemTemplate>
                            <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Marital Status" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c5" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c5">
                        <ItemTemplate>
                            <asp:Label ID="lblMaritalStatus" runat="server" Text='<%# Eval("MaritalStatusDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Country Of Birth" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c6" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c6">
                        <ItemTemplate>
                            <asp:Label ID="lblCountryOfBirth" runat="server" Text='<%# Eval("CountryOfBirth") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Country Of Residence" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c6" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c6">
                        <ItemTemplate>
                            <asp:Label ID="lblCountryOfBirth" runat="server" Text='<%# Eval("CountryOfResidence") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Compliance" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c7" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c7">
                        <ItemTemplate>
                            <asp:Label ID="lblCompliance" runat="server" Text='<%# Eval("Compliance") %>'></asp:Label>
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


                <EmptyDataTemplate>
                    No data to display
                </EmptyDataTemplate>
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                <HeaderStyle CssClass="gradient_azul" />
                <PagerStyle CssClass="paginationTD" />
            </asp:GridView>



        </div>
        <!--// 1era tabla -->

        <!-- 2da tabla -->
        <div class="wd100 fl lh27">
            <label class="label mT0">ILLUSTRATION DATA BY ROLE [<asp:Literal ID="ltrEntityName" runat="server" />]</label>
            <asp:DropDownList runat="server" ID="ddlPolicyData" CssClass="fr wd30 m0" AutoPostBack="true" OnSelectedIndexChanged="ddlPolicyData_SelectedIndexChanged" DataTextField="Text" DataValueField="Value" />
        </div>
        <div class="tbl data_G tbl-2 wd100 mB20">

            <asp:GridView ID="gvPolicyData"
                runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="gvPolicyData_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Role" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c1" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c1">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%# Eval("RoleDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Illustration #" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c4" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c4">
                        <ItemTemplate>
                            <asp:Label ID="lblGender" runat="server" Text='<%# Eval("PolicyNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plan Name" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c5" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c5">
                        <ItemTemplate>
                            <asp:Label ID="lblMaritalStatus" runat="server" Text='<%# Eval("PlanName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plan Type" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c6" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c6">
                        <ItemTemplate>
                            <asp:Label ID="lblCountryOfBirth" runat="server" Text='<%# Eval("PlanType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Efective Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c3" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c3">
                        <ItemTemplate>
                            <asp:Label ID="lblAge" runat="server" Text='<%# Eval("EffectiveDate") != null ? DateTime.Parse(Eval("EffectiveDate").ToString()).ToString("MM/dd/yyyy"):"" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Risk Class" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c7" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c7" >
                        <ItemTemplate>
                            <asp:Label ID="lblCompliance" runat="server" Text='<%# Eval("RiskClass") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Benefit Amount" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c7" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="c8">
                        <ItemTemplate>
                            <asp:Label ID="lblCompliance" runat="server" Text='<%# Eval("BenefitAmount") == null? "0.00" : Decimal.Parse(Eval("BenefitAmount").ToString()).ToString("N2") %>'></asp:Label>
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

                <EmptyDataTemplate>
                    No data to display
                </EmptyDataTemplate>
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                <HeaderStyle CssClass="gradient_azul" />
                 <PagerStyle CssClass="paginationTD" />
            </asp:GridView>


            <!--pagination-->
        </div>
        <!--// 2da tabla -->

        <!-- 3era tabla -->
        <div class="wd100 fl lh27">
            <label class="label mT0">ALL REQUIREMENTS RECEIVED ILLUSTRATION #: [<asp:Literal ID="ltrRequirementPolicy" runat="server" />]</label>

            <asp:DropDownList runat="server" ID="ddlRequirementType" CssClass="fr wd30 m0" AutoPostBack="true" OnSelectedIndexChanged="ddlRequirementType_SelectedIndexChanged" DataTextField="Text" DataValueField="Value" />
            <%--         <select class="fr wd30 m0">
                <option>All Requirements</option>
                <option>General Documents Requirements</option>
                <option>Medical Requirements</option>
                <option>Financial Requirements</option>
                <option>Sports/Activity Requirements</option>
            </select>--%>
        </div>
        <div class="tbl data_G tbl-3 wd100 mB20">

            <asp:GridView ID="gvAllRequirements" DataKeyNames="ContactId,RequirementDocId,RequirementId,RequirementTypeId,RequirementCatId"
                runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="gvAllRequirements_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Requirement" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c1" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c1">
                        <ItemTemplate>
                            <asp:Label ID="lblRequirementTypeDesc" runat="server" Text='<%# Eval("RequirementTypeDesc") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date Received" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c2" ItemStyle-CssClass="c2" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
							<asp:Label ID="lblReceivedDate" runat="server" Text='<%# Eval("ReceivedDate") != null ? DateTime.Parse(Eval("ReceivedDate").ToString()).ToString("MM/dd/yyyy") : string.Empty %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="View PDF" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c3" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c3">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnkPdfFile" CssClass="pdf_ico" OnClick="lnkPdfFile_Click" Visible='<%# Eval("RequirementDocID") == null? false: true %>' />
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


                <EmptyDataTemplate>
                    No data to display
                </EmptyDataTemplate>
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />

                <HeaderStyle CssClass="gradient_azul" />
                 <PagerStyle CssClass="paginationTD" />
            </asp:GridView>



            <!--pagination-->
        </div>
        <!--// 3era tabla -->

        <!-- 4ta tabla -->
        <div class="wd100 fl lh27">
            <label class="label mT0">ALL PAYMENTS RECEIVED ILLUSTRATION #: [<asp:Literal ID="ltrPaymentPolicy" runat="server" />]</label>
            <asp:DropDownList runat="server" ID="ddlAllPayments" CssClass="fr wd15 m0" AutoPostBack="true" OnSelectedIndexChanged="ddlAllPayments_SelectedIndexChanged" DataTextField="Text" DataValueField="Value" />
            <%--            <select class="fr wd15 m0">
                <option>S451245</option>
                <option>S321247</option>
                <option>All Policies</option>
            </select>--%>
        </div>
        <div class="tbl data_G tbl-4 wd100">

            <asp:GridView ID="gvAllPayments"
                runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="gvAllPayments_PageIndexChanging">
                <Columns>

                    <asp:TemplateField HeaderText="Policy #" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c1" ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="c1">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%# Eval("PolicyNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c2" ItemStyle-CssClass="c2" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
							<asp:Label ID="lbDueDate" runat="server" Text='<%# Eval("DueDate") != null ? DateTime.Parse(Eval("DueDate").ToString()).ToString("MM/dd/yyyy") : string.Empty %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Due Amount" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c3" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="c3">
                        <ItemTemplate>
                            <asp:Label ID="lblDueAmount" runat="server" Text='<%# Eval("DueAmount") == null? "0.00" : Decimal.Parse(Eval("DueAmount").ToString()).ToString("N2")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Posted Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c4" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c4">
                        <ItemTemplate>
							<asp:Label ID="lblPaidDate" runat="server" Text='<%# Eval("PaidDate") != null ? DateTime.Parse(Eval("PaidDate").ToString()).ToString("MM/dd/yyyy") : string.Empty %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Posted Amount" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c5" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="c5">
                        <ItemTemplate>
                            <asp:Label ID="lblPaidAmount" runat="server" Text='<%# Eval("PaidAmount") == null? "0.00" : Decimal.Parse(Eval("PaidAmount").ToString()).ToString("N2") %>'></asp:Label>
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

                <EmptyDataTemplate>
                    No data to display
                </EmptyDataTemplate>
                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                <HeaderStyle CssClass="gradient_azul" />
                 <PagerStyle CssClass="paginationTD" />
            </asp:GridView>

            <!--pagination-->
        </div>
        <!--// 4ta tabla -->

    </li>
</ul>