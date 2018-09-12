﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPaymentInformation.ascx.cs" Inherits="WEB.UnderWriting.Case.UserControls.Payments.UCPaymentInformation" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>
<%@ Register Src="~/Case/UserControls/Common/UCPdfViewer.ascx" TagPrefix="uc1" TagName="UCPdfViewer" %>


<ul class="secundario">
	<li class="payinfo">
		<asp:UpdatePanel runat="server" ID="udpPayment" RenderMode="Block">
			<ContentTemplate>
				<div class=" wd100 fl mB mR-1-p">
					<div class=" boxes mR-2-p">
						<label class="label">
							ANNUAL PREMIUM:</label>
						<asp:TextBox ID="AnnualPremiumTxt" runat="server" class=" bgAM ReadOnly" ReadOnly="true" ClientIDMode="Static" Style="text-align: right"></asp:TextBox>
						<label class="label">
							PERIODIC PREMIUM:</label>
						<asp:TextBox ID="PeriodicPremiumTxt" runat="server" CssClass="ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
						<label class="label">
							Min Monthly premium:</label>
						<asp:TextBox ID="MinMonthlyPremiumTxt" runat="server" CssClass="ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
						<div id="divPreAut" runat="server">
							<label class="label">
								Premium:</label>
							<asp:TextBox ID="TextBox1" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
							<label class="label">
								Automatic payments:</label>
							<asp:TextBox ID="TextBox2" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
						</div>
					</div>
					<div class=" boxes mR-2-p">
						<label class="label">
							Payment frequency:</label>
						<!--<input name="name" type="text" class=" bgAM">-->
						<asp:TextBox ID="PaymentFrequencyTxt" runat="server" class="bgAM ReadOnly" ReadOnly="true"></asp:TextBox>
						<div class="dvPaymentEntity">
							<label class="label">
								Min Annual premium:</label>
							<asp:TextBox ID="MinAnnualPremiumTxt" runat="server" CssClass="ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
						</div>
						<label class="label">
							Policy Year:</label>
						<asp:TextBox ID="PolicyYearTxt" runat="server" class="ReadOnly" ReadOnly="true"></asp:TextBox>

						<div id="divfreMet" runat="server">
							<label class="label">
								Frequency:</label>
							<asp:TextBox ID="TextBox3" runat="server" class="ReadOnly" ReadOnly="true"></asp:TextBox>
							<label class="label">
								Method:</label>
							<asp:TextBox ID="TextBox4" runat="server" class="ReadOnly" ReadOnly="true"></asp:TextBox>
						</div>
					</div>
					<div class=" boxes mR-2-p">
						<label class="label">
							Premium Received:</label>
						<asp:TextBox ID="PremiumReceivedTxt" runat="server" class="bgAM ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
						<label class="label">
							Initial Contribution:</label>
						<asp:TextBox ID="InitialContributionTxt" runat="server" class="bgAM ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
						<div class=" wd100 fl">
							<div class=" wd100 fl">
								<label class="label txtBold em1-1">
									Dates And Periods</label>
							</div>
							<asp:Repeater runat="server" ID="Block1MonthsRepeater">
								<ItemTemplate>
									<div class=" boxi mR-1-p">
										<label class="label">
											<%# Eval("PayMonth") %>:</label>
										<asp:TextBox ID="MonthValueTxt" runat="server" CssClass="ReadOnly" Text='<%# Eval("PayDate") %>' ReadOnly="true"></asp:TextBox>
									</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>
					</div>
					<div class=" boxes">
						<div class="dvPaymentEntity">
							<label class="label">
								Account Value:</label>
							<asp:TextBox ID="AccountValueTxt" runat="server" CssClass="ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
						</div>
						<label class="label">
							Effective Date:</label>
						<asp:TextBox ID="EfectiveDateTxt" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
						<div class=" wd100 fl">
							<div class=" wd100 fl">
								<label class="label txtBold em1-1 hg12">
								</label>
								<asp:Repeater runat="server" ID="Block2MonthsRepeater">
									<ItemTemplate>
										<div class=" boxi mR-1-p">
											<label class="label">
												<%# Eval("PayMonth") %>:</label>
											<asp:TextBox ID="MonthValueTxt" runat="server" Text='<%# Eval("PayDate") %>' CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
										</div>
									</ItemTemplate>
								</asp:Repeater>
							</div>
						</div>
					</div>
				</div>
				<!--Bloque 3   -->
				<div class=" wd100 fl mB22">
					<div class=" wd100 fl">
						<label class="label txtBold em1-1">
							Payment Plan:</label>
					</div>
					<div class=" boxes mR-2-p">
						<label class="label">
							Payment Plan</label>
						<asp:TextBox ID="PaymentPlan" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
						<label class="label">
							Amount</label>
						<asp:TextBox ID="AmountTxt" runat="server" CssClass="ReadOnly" ReadOnly="true" Style="text-align: right"></asp:TextBox>
					</div>
					<div class=" boxes mR-2-p">
						<div class=" wd49 mR-2-p fl">
							<label class="label">
								Start Date:</label>
							<asp:TextBox ID="StartDateTxt" runat="server" class="ReadOnly" ReadOnly="true"></asp:TextBox>
						</div>
						<div class=" wd49 fl">
							<label class="label">
								End Date:</label>
							<asp:TextBox ID="EndDate" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
						</div>
						<div class=" wd100 fl">
							<label class="label">
								Payment frequency</label>
							<asp:TextBox ID="PaymentFrequencyPlanTxt" runat="server" CssClass="ReadOnly" ReadOnly="true"></asp:TextBox>
						</div>
					</div>
					<div class=" boxes mR-2-p">
					</div>
					<div class=" boxes">
					</div>
				</div>
				<!-- //Bloque 3     -->
				<!-- Bloque 4     -->
				<div class=" wd100 fl mB20 lh19">
					<div class="fl sub_ttl mT10 wd20 txtBold">
						PAYMENT DETAIL:
					</div>
					<div class="fl mT10 pdR alignR">
						OWNER OTHER POLICES:
					</div>
					<div class="fl wd20">
						<asp:DropDownList ID="OwnerOtherPolicuesDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OwnerOtherPolicuesDDL_SelectedIndexChanged"></asp:DropDownList>
					</div>
					<div class="fl mT10 wd9 pdR alignR">
						VIEW:
					</div>
					<div class="fl wd20">
						<asp:DropDownList ID="ViewDDL" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ViewDDL_SelectedIndexChanged"></asp:DropDownList>
					</div>
				</div>
				<!-- //Bloque 4     -->
				<div class="tbl row data_G pay_d">

					<asp:GridView ID="gvPaymentInfo" DataKeyNames="DocumentCategoryId,DocumentTypeId,DocumentId,PolicyNo"
						runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="gvPaymentInfo_PageIndexChanging">
						<Columns>

							<asp:TemplateField HeaderText="Illustration No" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c1" ItemStyle-HorizontalAlign="left" ItemStyle-CssClass="c1">
								<ItemTemplate>
									<asp:Label ID="lblPolicyNo" runat="server" Text='<%# Eval("PolicyNo") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Due Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c2" ItemStyle-CssClass="c2">
								<ItemTemplate>
									<asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("DueDate")==null? "" : DateTime.Parse(Eval("DueDate").ToString()).ToString("MM/dd/yyyy") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>

							<asp:TemplateField HeaderText="Posted Date" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c4" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c4">
								<ItemTemplate>
									<asp:Label ID="lblPaidDate" runat="server" Text='<%#Eval("PaidDate")==null? "" : DateTime.Parse(Eval("PaidDate").ToString()).ToString("MM/dd/yyyy") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>

							<asp:TemplateField HeaderText="Due Amount" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c5" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c5">
								<ItemTemplate>
									<asp:Label ID="lblDueAmount" runat="server" Text='<%#Eval("DueAmount")==null? "" : Decimal.Parse(Eval("DueAmount").ToString()).ToString("N2") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>

							<asp:TemplateField HeaderText="Posted Amount" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c6" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c6">
								<ItemTemplate>
									<asp:Label ID="lblPaidAmount" runat="server" Text='<%#Eval("PaidAmount")==null? "" : Decimal.Parse(Eval("PaidAmount").ToString()).ToString("N2") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>

							<asp:TemplateField HeaderText="Payment" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c6" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c6">
								<ItemTemplate>
									<asp:Label ID="lblPaymentSourceDesc" runat="server" Text='<%# Eval("PaymentSourceDesc") %>'></asp:Label>
								</ItemTemplate>
							</asp:TemplateField>

							<asp:TemplateField HeaderText="Pdf Files" HeaderStyle-HorizontalAlign="center" HeaderStyle-CssClass="c7" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="c7">
								<ItemTemplate>
									<asp:Button ID="LoadPdfFileBTN" runat="server" class="pdf_ico" Visible='<%# ((bool)Eval("HasDocument") ? true: false) %>' OnClick="LoadPdfFileBTN_Click"></asp:Button>
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

						<PagerStyle CssClass="RCFooterPad" />
					</asp:GridView>


				</div>

				<uc1:UCPdfViewer runat="server" ID="UCPdfViewer" Width="880px" Height="743px" margin-top="20px" margin-bottom="20px" />
				<asp:HiddenField runat="server" ID="hfPolicy" ClientIDMode="Static" />
			</ContentTemplate>
			<Triggers>
				<asp:AsyncPostBackTrigger ControlID="gvPaymentInfo" />
			</Triggers>
		</asp:UpdatePanel>
	</li>
</ul>
