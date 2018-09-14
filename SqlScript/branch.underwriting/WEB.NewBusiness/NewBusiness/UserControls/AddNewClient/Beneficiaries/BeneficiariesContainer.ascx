﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BeneficiariesContainer.ascx.cs" Inherits="WEB.NewBusiness.NewBusiness.UserControls.Beneficiaries.BeneficiariesContainer" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Beneficiaries/WUCMainInsured.ascx" TagPrefix="uc1" TagName="WUCMainInsured" %>
<%@ Register Src="~/NewBusiness/UserControls/AddNewClient/Beneficiaries/WUCAdditionalInsured.ascx" TagPrefix="uc1" TagName="WUCAdditionalInsured" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:UpdatePanel runat="server" ID="udpBeneficiariesContainer" ClientIDMode="Static">
    <ContentTemplate>
        <asp:MultiView runat="server" ID="mtBeneficiariesMain" ActiveViewIndex="0">
            <asp:View runat="server" ID="vPlanesVida">
                <div class="accordion_tabulado">
                    <ul class="principal" id="accBeneficiaries">
                        <!--principal-->
                        <li><a href="#item1" id="current" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonBeneficiaries');">
                            <asp:Literal runat="server" ID="MainInsured">MAIN INSURED</asp:Literal>
                            <span>
                                <!--icono-->
                            </span></a>
                            <!--secundario / contendio-->
                            <ul>
                                <li>
                                    <uc1:WUCMainInsured runat="server" ID="WUCMainInsured" />
                                </li>
                            </ul>
                        </li>

                        <li><a href="#item2" lnk="lnk" onclick="setCurrentAccordeon(this,'#hfMenuAccordeonBeneficiaries');" style="display: <%= ObjServices.HasInsured ? "block":"none"%>">
                            <asp:Literal runat="server" ID="AdditionalInsured">ADDITIONAL INSURED</asp:Literal>
                            <span>
                                <!--icono-->
                            </span></a>
                            <!--secundario / contendio-->
                            <ul>
                                <li>
                                    <uc1:WUCAdditionalInsured runat="server" ID="WUCAdditionalInsured" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </asp:View>

            <asp:View runat="server" ID="vPlanesFunerarios">
                <asp:UpdatePanel ID="upMainBeneficiaries" runat="server">
                    <ContentTemplate>
                        <div class="titulos_sin_accion">
                            <asp:Literal runat="server" ID="MainBeneficiaries">CLAIMANTS BENEFICIARIES</asp:Literal>
                        </div>
                        <div class="fondo_gris">
                            <div class="col-1-1">
                                <div class="fondo_blanco fix_height">
                                    <div class="titulos_azules">
                                        <span class="address"></span><strong>
                                            <asp:Literal runat="server" ID="BeneficiariesInformation">BENEFICIARIES INFORMATION</asp:Literal></strong>
                                    </div>
                                    <div class="content_fondo_blanco">
                                        <div class="float_right">
                                            <div class="boton_wrapper verde float_right">
                                                <span class="search"></span>
                                                <asp:Button runat="server" ID="btnSearch" CssClass="boton" Text="Search Contact" OnClick="btnSearch_Click"/>
                                            </div>
                                            <!--boton_wrapper-->
                                        </div>
                                        <div class="col-1-1">
                                            <asp:Panel runat="server" ID="pnFormBeneficiaries" class="grupos de_4" ClientIDMode="Static">
                                                <div>
                                                    <label class="label red" runat="server" id="FirtName">First Name</label>
                                                    <asp:TextBox runat="server" ID="txtFirstName" validation="Required"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <label class="label" runat="server" id="MiddleName">Middle Name</label>
                                                    <asp:TextBox runat="server" ID="txtMiddleName"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <label class="label red" runat="server" id="LastName">Last Name</label>
                                                    <asp:TextBox runat="server" ID="txtLastName" validation="Required"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <label class="label" runat="server" id="SecondLastName">2nd Last Name</label>
                                                    <asp:TextBox runat="server" ID="txtSecondLastName"></asp:TextBox>
                                                </div>

                                                <div>
                                                    <label class="label  red" runat="server" id="DateOfBirth">Date of Birth</label>
                                                    <asp:TextBox runat="server" ID="txtDateOfBirth" ClientIDMode="Static" CssClass="datepicker" validation="Required" alt="validateFutureDate" ></asp:TextBox><%--maxDate="-3m"--%>
                                                </div>
                                                <div>
                                                    <label class="label" runat="server" id="Gender">Gender</label>
                                                    <div class="wrap_select">
                                                        <asp:DropDownList runat="server" ID="ddlGender" ClientIDMode="Static">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <!--wrap_select-->
                                                </div>
                                                <div>
                                                    <label class="label red" runat="server" id="Relationship">Relationship</label>
                                                    <div class="wrap_select">
                                                        <asp:DropDownList runat="server" ID="ddlRelationship" validation="Required">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <!--wrap_select-->
                                                </div>
                                                <div>
                                                    <label class="label" runat="server" id="ID">ID#</label>
                                                    <asp:TextBox runat="server" ID="txtID"></asp:TextBox>
                                                </div>
                                            </asp:Panel>
                                            <!--grupos-->
                                            <div class="grupos">
                                                <div class="float_right">
                                                    <div class="browse_up fr" style="margin-top: 15px;">
                                                        <dx:ASPxUploadControl ID="fuMainBenediciarieFile" ClientInstanceName="fuMainBenediciarieFile" runat="server"
                                                            ShowAddRemoveButtons="false" ShowUploadButton="false" ShowClearFileSelectionButton="false" UploadMode="Auto" ShowProgressPanel="false" BrowseButton-Text="Upload ID"
                                                            ValidationSettings-AllowedFileExtensions=".pdf" OnFileUploadComplete="fuMainBenediciarieFile_FileUploadComplete">
                                                            <ClientSideEvents TextChanged="uploadFileChange" FileUploadComplete="uploadFileContainerComplete" />
                                                        </dx:ASPxUploadControl>
                                                    </div>
                                                    <!--boton_wrapper-->
                                                </div>

                                                <div class="float_right">
                                                    <div class="boton_wrapper gris float_right">
                                                        <span class="borrar"></span>
                                                        <asp:Button runat="server" CssClass="boton" Text="Clear" ID="btnClear" OnClick="btnClear_Click" />
                                                    </div>
                                                    <!--boton_wrapper-->
                                                </div>

                                                <div class="float_right">
                                                    <div class="boton_wrapper amarillo float_right">
                                                        <span class="add"></span>
                                                        <asp:Button runat="server" CssClass="boton" Text="Add" ID="btnAdd" OnClick="btnAdd_Click" OnClientClick="return validateForm('pnFormBeneficiaries')" />
                                                    </div>
                                                    <!--boton_wrapper-->
                                                </div>
                                            </div>
                                            <!--grupos-->
                                            <asp:panel runat="server"  ID="pnGvBeneficiaries" class="grid_wrap margin_t_10 gris">
                                                <dx:ASPxGridView ID="gvBeneficiaries" runat="server"
                                                    EnableCallBacks="False"
                                                    KeyFieldName="ContactId;ContactRoleTypeId;RelationshipToOwnerId;DocumentTypeId;DocumentId;DocumentCategoryId;SeqNo;ContactMainId"
                                                    AutoGenerateColumns="False" OnPreRender="Translate_PreRender"
                                                    Width="100%" ClientIDMode="Static" OnRowCommand="gvBeneficiaries_RowCommand">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn Caption="FIRST NAME" FieldName="FirstName" VisibleIndex="0">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="MIDDLE NAME" FieldName="MiddleName" VisibleIndex="1">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="LAST NAME" FieldName="FirstLastName" VisibleIndex="2">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="2ND LAST NAME" FieldName="SecondLastName" VisibleIndex="3">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataDateColumn Caption="DATE OF BIRTH" FieldName="Dob" VisibleIndex="4">
                                                            <PropertiesDateEdit DisplayFormatString="MM/dd/yyyy">
                                                            </PropertiesDateEdit>
                                                        </dx:GridViewDataDateColumn>
                                                        <dx:GridViewDataTextColumn Caption="RELATIONSHIP" FieldName="RelationshipDesc" VisibleIndex="5">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ContactMainId" VisibleIndex="6">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="7" Caption="ID File">
                                                            <DataItemTemplate>
                                                                <asp:Button runat="server" ID="btnFile" CommandName="PdfView" CssClass="pdf_file" Visible='<%# Eval("DocumentExists") %>' />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="EDIT" VisibleIndex="8" Width="1%">
                                                            <DataItemTemplate>
                                                                <asp:Button runat="server" ID="btnEditar" CommandName="Modify" CssClass="edit_file" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn FieldName="DELETE" VisibleIndex="9" Width="1%">
                                                            <DataItemTemplate>
                                                                <asp:UpdatePanel runat="server" ID="udpDelete">
                                                                    <ContentTemplate>
                                                                        <asp:Button runat="server" ID="btnDelete" CssClass="delete_file" CommandName="Delete" OnClientClick="return DlgConfirm(this)" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnDelete" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>                                                                    
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsBehavior AllowSelectSingleRowOnly="true" AllowSort="False" />
                                                </dx:ASPxGridView>
                                            </asp:panel>
                                            <!--grid_wrap-->
                                        </div>
                                        <!--col-1-1-->
                                    </div>
                                    <!--content_fondo_blanco-->
                                </div>
                                <!--fondo_blanco-->
                            </div>
                            <!--col-1-1-->
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:View>
        </asp:MultiView>
        <asp:HiddenField runat="server" ID="hfMenuAccordeonBeneficiaries" Value="" ClientIDMode="Static" />
        <asp:HiddenField runat="server" ID="hdnCountBeneficiaries" Value="0" ClientIDMode="Static" />
        <asp:TextBox ID="hdnUploadedPDFPath" ClientIDMode="Static" runat="server" Style="display: none;" />
        <asp:Button runat="server" ID="btnRefresh" ClientIDMode="Static"/>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
    </Triggers>
</asp:UpdatePanel>