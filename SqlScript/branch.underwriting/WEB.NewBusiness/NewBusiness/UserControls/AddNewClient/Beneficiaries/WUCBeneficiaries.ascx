﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCBeneficiaries.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Beneficiaries.WUCBeneficiaries" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:UpdatePanel ID="upBeneficiaries" runat="server">
    <ContentTemplate>
        <asp:Panel runat="server" ID="pnBeneficiaries" ClientIDMode="Static" CssClass="fondo_blanco pnBeneficiaries">
            <div class="titulos_azules">
                <span class="insured"></span><strong>
                    <asp:Label ID="lblBeTitle" ClientIDMode="Static" runat="server" Text="MAIN BENEFICIARIES"></asp:Label>
                </strong>
            </div>
            <div runat="server" style="border: none">
                <div class="content_fondo_blanco fix_height">
                    <div class="float_right">
                        <div class="boton_wrapper verde float_right mB" style="display:none">
                            <span class="search"></span>
                            <asp:Button runat="server" ID="btnSearch" CssClass="boton" Text="Search Contact" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="margin_t_10">
                        <div class="grupos de_5 small">
                            <div>
                                <label class="label red" runat="server" id="FirstName">First Name:</label>
                                <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" alphabetical="alphabetical"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label" runat="server" id="MiddleName">Middle Name:</label>
                                <asp:TextBox ID="txtMiddleName" runat="server" ClientIDMode="Static" alphabetical="alphabetical"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="LastName">Last Name:</label>
                                <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" alphabetical="alphabeticalLastName"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label" runat="server" id="SecondLastName">2nd Last Name:</label>
                                <asp:TextBox ID="txtSecondLastName" runat="server" ClientIDMode="Static" alphabetical="alphabetical"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="DateOftBirth">Date of Birth:</label>
                                <asp:TextBox ID="txtBEDateofBirth" runat="server" alt="validateFutureDate"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="Relationship">Relationship:</label>
                                <asp:DropDownList ID="ddlRelationship" ClientIDMode="Static" runat="server"></asp:DropDownList>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="Percentage">Percentage:</label>
                                <asp:TextBox ID="txtPercentage" runat="server" ClientIDMode="Static" decimal="decimal" data-inputmask="'alias': 'decimal', 'groupSeparator': ',', 'autoGroup': true, 'allowMinus': false, repeat: 3,'allowPlus': false, 'digits': 2"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="ID">Documento:</label>
                                <div class="wrap_select">
                                    <asp:DropDownList runat="server" ID="cbxIDType" validation="Required">
                                    </asp:DropDownList>
                                </div>

                                <asp:TextBox ID="txtIDNo" runat="server" ClientIDMode="Static" AllowEnter="true" data-inputmask="'clearMaskOnLostFocus': true,'showTooltip': false"></asp:TextBox>
                            </div>


                            <div class="wd40">
                                <div class="fl">
                                    <label class="label mT3" runat="server" id="Document">Document:</label>
                                </div>
                                <div class="browse_up fr" style="margin-top: -3px;">
                                    <dx:ASPxUploadControl ID="fuBenediciarieFile" ClientInstanceName="fuBenediciarieFile" runat="server"
                                        ShowAddRemoveButtons="false" ShowUploadButton="false" ShowClearFileSelectionButton="false" UploadMode="Auto" ShowProgressPanel="false" BrowseButton-Text="Upload ID"
                                        ValidationSettings-AllowedFileExtensions=".pdf" OnFileUploadComplete="fuBenediciarieFile_FileUploadComplete">
                                        <ClientSideEvents TextChanged="uploadFileChange" FileUploadComplete="uploadFileComplete" FileUploadStart="upFileStartUpload" />
                                    </dx:ASPxUploadControl>
                                </div>
                            </div>
                        </div>

                        <div class="grupos">
                            <asp:Panel ID="pnlReplacing" ClientIDMode="Static" runat="server" CssClass="wd20">
                                <label class="label" runat="server" id="Replacing">Replacing:</label>
                                <asp:DropDownList ID="ddlReplacing" ClientIDMode="Static" CssClass="wrap_select" runat="server"></asp:DropDownList>
                            </asp:Panel>

                            <div class="float_right">
                                <div class="boton_wrapper amarillo float_right">
                                    <span class="add"></span>
                                    <asp:Button ID="btnAdd" ClientIDMode="Static" Text="Add" OnClick="btnAdd_Click" OnClientClick="return ValidateBeneficiaries(this);" runat="server" />
                                </div>
                            </div>
                            <div class="float_right">
                                <div class="boton_wrapper gris float_right">
                                    <span class="borrar"></span>
                                    <asp:Button ID="btnBEClear" runat="server" ClientIDMode="Static" Text="Clear" OnClick="btnBEClear_Click" />
                                </div>
                            </div>

                        </div>
                        <!--//.head -->
                        <asp:Panel runat="server" ID="pnGvBeneficiaries" class="grid_wrap margin_t_10 mB" Style="overflow-x: auto">
                            <asp:GridView ID="gvBeneficiaries" DataKeyNames="FirstName,MiddleName,FirstLastName,SecondLastName,ContactId,ContactRoleTypeId,RelationshipToOwnerId,DocumentTypeId,DocumentId,DocumentCategoryId,SeqNo,PrimaryBeneficiaryId,ContactIdType"
                                runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="gvResult" OnPreRender="Translate_PreRender">
                                <Columns>
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name" HeaderStyle-CssClass="c1" AccessibleHeaderText="FirstNameLabel">
                                        <HeaderStyle CssClass="c1" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MiddleName" HeaderText="Middle Name" HeaderStyle-CssClass="c2" AccessibleHeaderText="MiddleNameLabel">
                                        <HeaderStyle CssClass="c2" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FirstLastName" HeaderText="First LastName" HeaderStyle-CssClass="c3" AccessibleHeaderText="LastNameLabel">
                                        <HeaderStyle CssClass="c3" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SecondLastName" HeaderText="SecondLastName" HeaderStyle-CssClass="c3" AccessibleHeaderText="SecondLastNameLabel">
                                        <HeaderStyle CssClass="c3" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Date Of Birth" HeaderStyle-CssClass="c4" AccessibleHeaderText="DateofBirthLabel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDob" runat="server" Text='<%# ((DateTime)Eval("Dob")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="c4" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Relationship" HeaderStyle-CssClass="c5" AccessibleHeaderText="RelationshipLabel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRelationshipDesc" runat="server" Text='<%# Eval("RelationshipDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="c5" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tipo" HeaderStyle-CssClass="c6" AccessibleHeaderText="ContactIdTypeDesc">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactIdTypeDesc" runat="server" Text='<%#  Eval("ContactIdTypeDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="c6" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID#" HeaderStyle-CssClass="c6" AccessibleHeaderText="ID#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblContactID" runat="server" Text='<%#  Eval("ContactMainId") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="c6" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="%" HeaderStyle-CssClass="c7">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBenefitsPercent" runat="server" Text='<%#Eval("BenefitsPercentF")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="c7" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Replacing" AccessibleHeaderText="ReplacingLabel">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblReplacing" Text='<%#Eval("ReplacingBeneficiaryFullName") %>'>                                               
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID File" HeaderStyle-CssClass="c8" AccessibleHeaderText="IdDocLabel">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnFile" runat="server" CssClass="pdf_file" OnClick="btnFile_Click" Visible='<%#Eval("DocumentExists") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="c8" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit" AccessibleHeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="edit_file" OnClick="btnEdit_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" AccessibleHeaderText="DeleteLabel">
                                        <ItemTemplate>
                                            <asp:Button ID="btnRemove" runat="server" CssClass="delete_file" OnClientClick="return DlgConfirm(this);" ClientIDMode="Static" OnClick="btnRemove_Click"></asp:Button>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <%= RESOURCE.UnderWriting.NewBussiness.Resources.NodataDisplay%>
                                </EmptyDataTemplate>
                                <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="gradient_azul" />
                            </asp:GridView>
                        </asp:Panel>
                    </div>
                    <div class="float_right">
                        <div class="boton_wrapper verde float_right mB" style="display:none">
                            <span class="search"></span>
                            <asp:Button runat="server" ID="btnSearchCompany" CssClass="boton" Text="Search Contact" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="margin_t_10">
                        <div class="grupos de_5 small">
                            <div>
                                <label class="label red" runat="server" id="EntityName">Entity Name:</label>
                                <asp:TextBox ID="txtEntityName" runat="server" ClientIDMode="Static" alphabetical="alphabetical"></asp:TextBox>

                                <label class="label red" runat="server" id="Percentage2">Percentage:</label>
                                <asp:TextBox ID="txtEntityPercentage" runat="server" ClientIDMode="Static" decimal="decimal" data-inputmask="'alias': 'decimal', 'groupSeparator': ',', 'autoGroup': true, 'allowMinus': false, 'allowPlus': false, 'digits': 2"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="IncorpotarionDate">Incorporation Date:</label>
                                <asp:TextBox ID="txtBEIncorporationDate" runat="server" alt="validateFutureDate"></asp:TextBox>

                                <label class="label" runat="server" id="ID2">ID#:</label>
                                <asp:TextBox ID="txtEntityIDNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div>
                                <label class="label red" runat="server" id="Type">Type:</label>

                                <asp:DropDownList ID="ddlEntityType" ClientIDMode="Static" runat="server"></asp:DropDownList>

                                <asp:Panel ID="pnlReplacingCompany" ClientIDMode="Static" runat="server">
                                    <label class="label" runat="server" id="Replacing2">Replacing:</label>
                                    <asp:DropDownList ID="ddlReplacingCompany" CssClass="wrap_select" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                </asp:Panel>
                            </div>

                            <div class="box wd40">
                                <div class="fl wd60">
                                    <label class="label" runat="server" id="Document2">Document:</label>
                                </div>
                                <div class="browse_up fr" style="margin-top: -3px;">
                                    <dx:ASPxUploadControl ID="fuBenediciarieFileCompany" ClientInstanceName="fuBenediciarieFileCompany" runat="server"
                                        ShowAddRemoveButtons="false" ShowUploadButton="false" ShowClearFileSelectionButton="false" UploadMode="Auto" ShowProgressPanel="false" BrowseButton-Text="Upload ID"
                                        ValidationSettings-AllowedFileExtensions=".pdf" OnFileUploadComplete="fuBenediciarieFile_FileUploadComplete">
                                        <ClientSideEvents TextChanged="uploadFileChange" FileUploadComplete="uploadFileCompanyComplete" FileUploadStart="upCOFileStartUpload" />
                                    </dx:ASPxUploadControl>
                                </div>
                            </div>
                        </div>

                        <div class="grupos">
                            <div class="float_right">
                                <div class="boton_wrapper amarillo float_right">
                                    <span class="add"></span>
                                    <asp:Button ID="btnBECompanyAdd" runat="server" ClientIDMode="Static" Text="Add" OnClick="btnBECompanyAdd_Click" OnClientClick="return ValidateBeneficiariesCompany(this);" />
                                </div>
                            </div>
                            <div class="float_right">
                                <div class="boton_wrapper gris float_right">
                                    <span class="borrar"></span>
                                    <asp:Button ID="btnBECompanyClear" runat="server" ClientIDMode="Static" Text="Clear" OnClick="btnBECompanyClear_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnGvCompany" class="grid_wrap margin_t_10" Style="overflow-x: auto">
                        <asp:GridView
                            ID="gvBeneficiariesCompany"
                            DataKeyNames="InstitutionalName,ContactId,ContactRoleTypeId,OccupationId,DocumentTypeId,DocumentId,DocumentCategoryId,SeqNo,PrimaryBeneficiaryId"
                            runat="server"
                            AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True"
                            CssClass="gvResult"
                            OnPreRender="Translate_PreRender">
                            <Columns>
                                <asp:BoundField DataField="InstitutionalName" HeaderText="Entity Name" HeaderStyle-CssClass="c1" AccessibleHeaderText="EntityName" />
                                <asp:TemplateField HeaderText="Incorporation Date" HeaderStyle-CssClass="c2" AccessibleHeaderText="IncorporationDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDob" runat="server" Text='<%#((DateTime)Eval("Dob")).ToString("MM/dd/yyyy") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="c3" AccessibleHeaderText="TypeLabel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMiddleName" runat="server" Text='<%#Eval("OccupationDesc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c3" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%" HeaderStyle-CssClass="c4" AccessibleHeaderText="">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBenefitsPercent" runat="server" Text='<%#Eval("BenefitsPercentF")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c4" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID#" HeaderStyle-CssClass="c5" AccessibleHeaderText="ID#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactID" runat="server" Text='<%#Eval("ContactMainId") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c5" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Replacing" AccessibleHeaderText="ReplacingLabel">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReplacing" runat="server" Text='<%#Eval("ReplacingBeneficiaryFullName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ID File" HeaderStyle-CssClass="c6" AccessibleHeaderText="IdDocLabel">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnBECompanyFile" runat="server" CssClass="pdf_file" OnClick="btnBECompanyFile_Click" Visible='<%#Eval("DocumentExists") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c6" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="c7" AccessibleHeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnBECompanyEdit" runat="server" CssClass="edit_file" OnClick="btnBECompanyEdit_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c7" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="c8" AccessibleHeaderText="DeleteLabel">
                                    <ItemTemplate>
                                        <asp:Button ID="btnBECompanyRemove" runat="server" CssClass="delete_file" OnClientClick="return DlgConfirm(this);" OnClick="btnBECompanyRemove_Click"></asp:Button>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="c8" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%= RESOURCE.UnderWriting.NewBussiness.Resources.NodataDisplay%>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle ForeColor="Red" HorizontalAlign="Center" />
                            <HeaderStyle CssClass="gradient_azul" />
                        </asp:GridView>
                    </asp:Panel>

                    <div class="grupos de_1">
                        <div>
                            <label class="label" runat="server" id="SpecialInstructions">SPECIAL INSTRUCTIONS</label>
                            <asp:TextBox ID="txtSpecialInstructions" runat="server" ClientIDMode="Static" TextMode="MultiLine" Height="150px"></asp:TextBox>
                        </div>
                    </div>

                    <asp:HiddenField ID="hdnIsEdit" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdnEditIndex" ClientIDMode="Static" runat="server" />
                    <asp:TextBox ID="hdnUploadedPDFPath" ClientIDMode="Static" runat="server" Style="display: none;" />

                    <asp:HiddenField ID="hdnIsCompany" ClientIDMode="Static" runat="server" Value="false" />
                    <asp:HiddenField ID="hdnIsEditCompany" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdnEditIndexCompany" ClientIDMode="Static" runat="server" />
                    <asp:TextBox ID="hdnUploadedPDFPathCompany" ClientIDMode="Static" runat="server" Style="display: none;" />

                    <asp:HiddenField ID="hdnBeneficiarieType" ClientIDMode="Static" runat="server" />

                    <asp:HiddenField ID="hdnBeneficiarieTypeID" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hdnIsInsured" ClientIDMode="Static" runat="server" Value="false" />

                    <asp:HiddenField ID="HFContactId" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="HFContactRoleIDType" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="HFIsEditPop" ClientIDMode="Static" runat="server" Value="false" />
                </div>
            </div>

        </asp:Panel>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="gvBeneficiariesCompany" />
        <asp:AsyncPostBackTrigger ControlID="gvBeneficiaries" />
    </Triggers>
</asp:UpdatePanel>
