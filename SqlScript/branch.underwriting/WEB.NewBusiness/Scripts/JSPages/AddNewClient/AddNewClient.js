﻿

function EnlazarEventos() {
    $(document).ready(function () {         

        $('#txtOccupation').on('blur', function () {
            var OccupationId = $.trim($("#hdnOccupationId").val());
            if (OccupationId == "") {
                $("#txtOccupation").val("");
                $("#hdnOccupationGroupId").val("");
                $("#txtProfession").val("");
                changeBorderColor($("#txtProfession"));
            };
        });

        $('#txtOccupation_Legal').on('blur', function () {
            var OccupationId = $.trim($("#hdnOccupationId_Legal").val());
            if (OccupationId == "") {
                $("#txtOccupation_Legal").val("");
                $("#hdnOccupationGroupId_Legal").val("");
                $("#txtProfession_Legal").val("");
                changeBorderColor($("#txtProfession_Legal"));
            };
        });

        var IDMainBenef = eval($('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_cbxIDType').val());
        var ctrlIDMainBenef = $("input[name='ctl00$bodyContent$BeneficiariesContainer$WUCMainInsured$WUCMainBeneficiaries$txtIDNo']");
        if (IDMainBenef == 5) { var $txt = ctrlIDMainBenef.inputmask("999-99999-9"); }
        else if (IDMainBenef == 1 || IDMainBenef == 3) { var $txt = ctrlIDMainBenef.inputmask("999-9999999-9"); }
        else { var $txt = ctrlIDMainBenef.inputmask("remove"); }


        var IDContMainBenef = eval($('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_cbxIDType').val());
        var ctrlIDContMainBenef = $("input[name='ctl00$bodyContent$BeneficiariesContainer$WUCMainInsured$WUCMainBeneficiaries$txtIDNo']");
        if (IDContMainBenef == 5) { var $txt = ctrlIDContMainBenef.inputmask("999-99999-9"); }
        else if (IDContMainBenef == 1 || IDContMainBenef == 3) { var $txt = ctrlIDContMainBenef.inputmask("999-9999999-9"); }
        else { var $txt = ctrlIDContMainBenef.inputmask("remove"); }
    });
};

ConfirmReadyToReview = function (sender) {

    return DlgConfirm(sender);
};

ConfirmSubmitToSTL = function (sender) {
    return DlgConfirm(sender);
};

var lang = "";
DiscardPadecimiento = function () {
    $('#hfPadecimiento').val('false');
    $find('popupPadecimiento').hide();
};

ClosePopIllustration = function () {
    $find('popupIllustrations').hide();
}


function pageLoad() {
    try {
        MostrarListaPEP = function (item) {
            if ($(item).attr('namekey') == "ISPARENTPOLITICAL") {
                
                if ($(item).attr('disabled') == undefined) {

                    var ryes = $(item).parent().find('input').val() == 'rbYes';  
                    ShowParentPolitical(ryes, $(item).attr('sectionid'));
                } 
            }
        };

        function ShowParentPolitical(result,Section) {

            if (result) {
                $('#' + Section).removeClass('campos');
                $('#' + Section).addClass('camposShow');
                $('#' + Section).css('width', 'auto');
                $('#frmBackGroundInformation').height('0%');
            } else {
                $('#' + Section).removeClass('camposShow');
                $('#' + Section).addClass('campos');
            }

            myFixHeight();
        }
         
        $(function () {


            if ($('input[id=bodyContent_ContactsInfoContainer_WUCBackgroundInformation_repeaterQuestion_rbYes_3]:checked').val() == 'rbYes') {
                //Mostrar el panel de contacto
                ShowParentPolitical(true, 'ISPARENTPOLITICAL_section');
            }

            if ($('input[id=bodyContent_PlanPolicyContainer_WUCDesignatedPensionerInformation_repeaterQuestion_rbYes_3]:checked').val() == 'rbYes') {
                ShowParentPolitical(true, 'ISPARENTPOLITICAL_Designated');
            }

            if ($('input[id=bodyContent_ContactsInfoContainer_WUCBackgroundInformationLegal_repeaterQuestion_rbYes_3]:checked').val() == 'rbYes') { 
                ShowParentPolitical(true, 'ISPARENTPOLITICAL_legal');
            }


        });
        
        setMaskToControls();
        var hdnisFuneral = $("#hfisFuneral");

        var isFuneral = (hdnisFuneral.val() == "Luminis" ||
                         hdnisFuneral.val() == "LuminisVIP" ||
                         hdnisFuneral.val() == "Exequium" ||
                         hdnisFuneral.val() == "ExequiumVIP") && hdnisFuneral.val() != "";

        $("#pnPadecimiento,#pnIllustrations,#pdfUploadPanel").draggable({
            handle: ".titulos_azules"
        }).find(".titulos_azules").css("cursor", "move");

        var hdnRefreshBeneficiariesTab = $("#hdnRefreshBeneficiariesTab");

        $(".gvResult > thead").addClass("azules headgrid");
        $(".gvResult > tbody").addClass("datagrid");

        lang = $("#hdnLang").val();

        //Cambiar a español todos los datePickers
        if (lang == "es") {
            $.datepicker.regional['es'] = {
                closeText: 'Cerrar',
                prevText: '<Ant',
                nextText: 'Sig>',
                currentText: 'Hoy',
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
                dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
                weekHeader: 'Sm',
                dateFormat: 'mm/dd/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };

            $.datepicker.setDefaults($.datepicker.regional['es']);
        }


        sessionTimeout = $("#hdnTimeOut").val();

        var fieldSetDisabled = "<fieldset style='border:none;padding: 0; margin: 0;' disabled='disabled'></fieldset>";
        var isReadOnly = $("#hdnIsReadOnly").val() == "true";

        //Cambiar de Tab
        var CurrentTab = $("#hdnCurrentTabAddNewClient").val().split('|')[0];

        if (CurrentTab != null && CurrentTab != "") {
            var HideOrShow1 = (CurrentTab == "lnkOwnerInfo" && !$("#chkOwnerIsSameAsInsured")[0].checked && !$("#chkIsCompany")[0].checked) ? "block" : "none";
            var HideOrShow2 = (CurrentTab == "lnkRequirements" || CurrentTab == "lnkBeneficiaries" || CurrentTab == "lnkPayment") ? "none" : "";
            var HideOrShow3 = (CurrentTab == "lnkPlanPolicy") ? "none" : "block";

            $("#OnlyOwnerInfo").css("display", HideOrShow1);
            $("#client_searchbox").css("display", HideOrShow2);
            $("#titulos_azules,#titulos_azulesAddress").css("display", HideOrShow3);

            //PAYMENTS
            if (CurrentTab == "lnkPayment") {

                var trs = $("#bodyContent_PaymentContainer_WUCPayment_gvPayment").find("table:first").find("tr");

                for (var x = 1; x < trs.length; x++)
                    $(trs[x]).css("text-align", "right");

                var dropFormOfPayment = $("#bodyContent_PaymentContainer_WUCFormOfPayment_UCContainerBasicPayment_UCBasicPayment_ddlFormofPayment").val();
                $("#tblUpload > tbody > tr > td").css("width", "77%");

                if (dropFormOfPayment == "-1")
                    $($("#tblUpload").parents()[1]).hide();
                else
                    $($("#tblUpload").parents()[1]).show();

                $("input[type='submit']").each(function () {
                    if ($(this).attr("alt") != null) {
                        $(this).mouseover(function () {
                            ShowToolTips($(this), 'top', true, null);
                        });
                    }
                });

                setABAautoComplete();

                $("#divSave").prepend($("#tblUpload"));
                var el = $(".dxgvFooter_DevEx");
                var html = el.find("td").eq(2).text($("#hdntotalPay").val());
                el.find("td").eq(0).html("<span style='font-weight:bold'>Total<span/>");
                $("#bodyContent_PaymentContainer_WUCFormOfPayment_UCContainerACH_UCACHOneTime_txtOriginationDate").addClass("datepicker");

                if ($("#gvFormOfPayment").find("[class='view_file']").length > 0) {
                    var texto = lang == "en" ? "VIEW" : "VER";
                    $($("#gvFormOfPayment").find("table")[3]).find("td:first").text(texto);
                }
                var $body = $("body");

                if ($body.find("[id*='ddlFormofPayment']").hasClass("aspNetDisabled")) {
                    $body.find("input[id*='txtOriginationDate']").wrap(fieldSetDisabled);
                }

                $("#frmPay").find(".datepicker").each(function () {
                    if ($(this).prop("readonly"))
                        $(this).prop("disabled", true);
                });

                $("#dvPayments").fadeIn(500);
            }
                //END PAYMENTS
            else
                //PLAN POLICY
                if (CurrentTab == "lnkPlanPolicy") {

                    $("#bodyContent_PlanPolicyContainer_WUCDesignatedPensionerInformation_pnDependents").addClass('grid_wrap margin_t_10 gris');

                    setCurrentAccordeonForIndex("#hfMenuAccordeon");
                    if (!$("#btnPprofile").prop("disabled")) {
                        $("#btnPprofile").removeClass("buttonDisabled");
                    }

                    $("#btnPprofile").mouseover(function () {
                        var $this = $(this);
                        CustomToolTips($this, $this.attr("alt"), "top", 3000);
                    });


                    if ($("#frmAdditionalsIndured").hasClass("aspNetDisabled"))
                        $("#frmAdditionalsIndured").addClass("grupos de_4");

                    if (isReadOnly) {
                        $("#QuestionHaveDesignatedPensioner").parent().wrap("<fieldset style='border:none' disabled='disabled'></fieldset>");
                        $("#frmIDOccupation").wrap(fieldSetDisabled);
                        $(".BGIDesignatedPensioner").wrap(fieldSetDisabled);
                        $("#dvHomeaddress").wrap(fieldSetDisabled);
                        $("#dvBusinessAddress").wrap(fieldSetDisabled);
                    }

                    $("#udpPhoneNumber div:first,#udpEmailAddress div:first").removeClass("col-1-3");
                    $("#udpPhoneNumber div:first > div,#udpEmailAddress div:first > div").css("border", "none");
                    $("#udpPhoneNumberLegal div:first,#udpEmailAddressLegal div:first").removeClass("col-1-3");
                    $("#udpPhoneNumberLegal div:first > div,#udpEmailAddressLegal div:first > div").css("border", "none");
                    $(".ui-slider-tabs-content-container").css("height", "387px");

                    //Calculo del Itbis
                    GetAnnualPremium('txtPeriodicPremium', 'ddlFrequencyofPayment');
                    $("#txtAnnualPremium").attr("disabled", "true");
                    //$("#txtFraccionamiento").attr("disabled", "true"); //mavelar 4/27/17
                    $("#txtReturnofPremium").attr("disabled", "true");
                    $("#txtSelectiveTax").attr("disabled", "true");
                    $("#txtAnnualPremium").attr("disabled", "true")
                    $("#txtAnnualPremiumWithTax").attr("disabled", "true");

                    if ($("#frmPhoneNumbers").css("display") == "none")
                        $("#frmPhoneNumbers").fadeIn(500);

                    if ($("#fEmailAddress").css("display") == "none")
                        $("#fEmailAddress").fadeIn(500);

                    if ($("#dvAdditionalsInsured").css("display") == "none")
                        $("#dvAdditionalsInsured").fadeIn(100);
                    //Bmarroquin 20-01-2017  cambio como parte de la tropicalizacion en ESA, el monto de suma aseguradora total es el mismo de vida Basico
                    $("#txtMontoAseguradoCorBasica").val($("#txtInsuredAmount").val());
                    //Bmarroquin 27-03-2017 Mejora para que se actualice automaticamente el valor de suma Asegurada de GF en base al 10% de suma asegurada de Vida Basico
                    $("#ddlAdditionalTermInsurance").change(ChangeGastosFunerarios);
                    $("#txtMontoAseguradoCorBasica").blur(ChangeGastosFunerarios);
                } //END PLAN POLICY
                else
                    //BENEFICIARIOS
                    if (CurrentTab == 'lnkBeneficiaries') {
                        $("#bodyContent_BeneficiariesContainer_fuMainBenediciarieFile_TextBox0_FakeInput").css("display", "none");
                        $("#bodyContent_BeneficiariesContainer_fuMainBenediciarieFile_TextBox0").css("width", "0");
                        $("#bodyContent_BeneficiariesContainer_fuMainBenediciarieFile_TextBox0").css("padding", "0");
                        setCurrentAccordeonForIndex("#hfMenuAccordeonBeneficiaries");

                        if (hdnRefreshBeneficiariesTab.val() == "false")
                            $(".pnBeneficiaries").fadeIn();
                        $("#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_cbxIDType").change(function () {

                            var IDMainBenefIns = eval($('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_cbxIDType').val());
                            var ctrlIDMainBenefIns = $("input[name='ctl00$bodyContent$BeneficiariesContainer$WUCMainInsured$WUCMainBeneficiaries$txtIDNo']");
                            if (IDMainBenefIns == 5) { var $txt = ctrlIDMainBenefIns.inputmask("999-99999-9"); }
                            else if (IDMainBenefIns == 1 || IDMainBenefIns == 3) { var $txt = ctrlIDMainBenefIns.inputmask("999-9999999-9"); }
                            else { var $txt = ctrlIDMainBenefIns.inputmask("remove"); }
                        });
                        $("#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_cbxIDType").change(function () {
                            var IDMainBenefInsCnt = eval($('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_cbxIDType').val());
                            var ctrlIDMainBenefInsCont = $("input[name='ctl00$bodyContent$BeneficiariesContainer$WUCMainInsured$WUCContingentBeneficiaries$txtIDNo']");
                            if (IDMainBenefInsCnt == 5) { var $txt = ctrlIDMainBenefInsCont.inputmask("999-99999-9"); }
                            else if (IDMainBenefInsCnt == 1 || IDMainBenefInsCnt == 3) { var $txt = ctrlIDMainBenefInsCont.inputmask("999-9999999-9"); }
                            else { var $txt = ctrlIDMainBenefInsCont.inputmask("remove"); }
                        });
                    }//END BENEFICIARIOS
                    else
                        //CLIENT INFO
                        if (CurrentTab == "lnkClientInfo") {
                            $("#frmPhoneNumbers").show();
                            if ($("#frmEmailAddress").css("display") == "none") {
                                $("#frmEmailAddress").fadeIn(500);
                            }
                            $("#dvrepeaterClientIsA").find("input:checkbox").each(function () {
                                if ($(this)[0].checked)
                                    checkBoxes($(this)[0]);
                            });

                            $("#dvrepeaterHasACloseRelationShipWithA").find("input:checkbox").each(function () {
                                if ($(this)[0].checked)
                                    checkBoxes($(this)[0]);
                            });
                        }
                            //END CLIENT INFO
                        else
                            //OWNER INFO
                            if (CurrentTab == "lnkOwnerInfo") {

                                $("#frmPhoneNumbers").show();

                                $("#frmPhoneNumbersLegal").show();

                                $("#frmHomeAddress").css("display", $("#chkIsCompany").prop("checked") ? "none" : "block");

                                $("#dvOwnerInformation").fadeIn(500);

                                if ($("#frmEmailAddress").css("display") == "none") {
                                    $("#frmEmailAddress").fadeIn(500);
                                }

                                if (!$("#chkIsCompany").prop("checked")) {
                                    $("#dvrepeaterClientIsA").find("input:checkbox").each(function () {
                                        if ($(this)[0].checked)
                                            checkBoxes($(this)[0]);
                                    });

                                    $("#dvrepeaterHasACloseRelationShipWithA").find("input:checkbox").each(function () {
                                        if ($(this)[0].checked)
                                            checkBoxes($(this)[0]);
                                    });

                                }
                                //END ONER INFO
                            } else
                                //HEALTH DECLARATION
                                if (CurrentTab == "lnkHealthDeclaration") {

                                    //Verificar si los questionarios estan completos
                                    $("#gvHealthDeclaration > tbody > tr").each(function () {
                                        $(this).find("td").each(function () {
                                            try {
                                                var Index = $(this).find("[hidcheck]").attr("hidcheck");
                                                var YesOrNo = $(this).find("[hidcheck]").find("input:checked").val().split("|")[0];
                                                if (YesOrNo == "NO")
                                                    $("#gvHealthDeclaration").find("[hid='" + Index + "']").hide();
                                                else {
                                                    var oDivAnswerContainer = $("#gvHealthDeclaration").find("[hid='" + Index + "']");

                                                    var textcheck = oDivAnswerContainer.find("[textcheck='textcheck']");
                                                    if (textcheck != null && textcheck.length > 0) {
                                                        textcheck.each(function () {
                                                            var checkBox = $(this).find("input:checkbox");
                                                            hideCheckTexbox(checkBox);
                                                        });
                                                    }

                                                    var dropdawncheck = oDivAnswerContainer.find("[dropdawncheck='dropdawncheck']");
                                                    if (dropdawncheck != null && dropdawncheck.length > 0) {
                                                        dropdawncheck.each(function () {
                                                            var checkBox = $(this).find("input:checkbox");
                                                            hideCheckTexbox(checkBox);
                                                        });
                                                    }

                                                    oDivAnswerContainer.show();
                                                }
                                            } catch (e) {
                                                $("#gvHealthDeclaration").find("[hid='" + Index + "']").hide();
                                            }

                                        });
                                    });

                                    if ($("#isViewTab").val() == "false") {
                                        $("#gvHealthDeclaration").fadeIn(1200);
                                        $("#isViewTab").val("true");
                                    } else $("#gvHealthDeclaration").fadeIn(400);

                                    changeTabHealth();

                                    if (isReadOnly)
                                        //Envolver las preguntas en un fieldset deshabilitado
                                        $(".pregunta").wrap("<fieldset style='border:none' disabled='disabled'></fieldset>");

                                    //Pregunta 18
                                    //$("body").find("[hid='18']").find("input[type='text']").removeAttr("decimal").attr("decimal", "decimal3");

                                    var selector = "[hid='5'],[hid='6'],[hid='8'],[hid='11'],[hid='12'],[hid='13'],[hid='14'],[hid='15'],[hid='16'],[hid='19'],[hid='21'],[hid='22'],[hid='23'],[hid='24'],[hid='25']";

                                    $("body").find(selector).find("input[class*='datepicker']").attr("alt", "validateFutureDate");

                                    //Validar la pregunta #1
                                    var Question1 = $("body").find("[hid = '1']").attr("id", "frmQuestion1");
                                    var TextBox = Question1.find("input:text:first");

                                    var radioChecked = Question1.find("input:radio:checked");

                                    TextBox.attr("Min-Value", (radioChecked.attr("id") == "rblRadioButtonList_0") ? "5" : "10");

                                    Question1.find("input[type='radio']").click(function () {
                                        if ($(this).attr("id") == 'rblRadioButtonList_0')
                                            TextBox.attr("Min-Value", "5");
                                        else if ($(this).attr("id") == 'rblRadioButtonList_1')
                                            TextBox.attr("Min-Value", "10");
                                    });

                                    var sel = "[hid='16'],[hid='8'],[hid='15'],[hid='23'],[hid='22'],[hid='24'],[hid='25']";

                                    $("body").find(sel).find("label").each(function () {
                                        var $this = $(this);
                                        if ($this.text().indexOf('Telephone') != -1 || $this.text().indexOf('phone') != -1 || $this.text() == "Telefono" || $this.text() == "Teléfono") {
                                            $this.next().attr("data-inputmask", "'alias': 'integer','rightAlign': false,'clearMaskOnLostFocus': true,'allowMinus': false, 'allowPlus': false");
                                            $this.next().attr("maxlength", "16");
                                        }
                                    });

                                    if (isFuneral) {
                                        //Pregunta #1 solo aplica para planes funerarios
                                        var QuestionOne = $("body").find("[hid = '1']");

                                        var pCheckBoxes = QuestionOne.find("input:checkbox");
                                        var ptextareas = QuestionOne.find("textarea");

                                        ptextareas.hide();
                                        ptextareas.removeAttr("validation");

                                        var textA0 = $(ptextareas[0]);
                                        var textA1 = $(ptextareas[1]);
                                        var textA2 = $(ptextareas[2]);

                                        if ($(pCheckBoxes[0]).prop("checked")) {
                                            textA0.show();
                                            if (!$(pCheckBoxes[1]).prop("checked"))
                                                $($(pCheckBoxes[1]).parents()[3]).removeAttr("validatecheckboxlist");

                                            if (!$(pCheckBoxes[2]).prop("checked"))
                                                $($(pCheckBoxes[2]).parents()[3]).removeAttr("validatecheckboxlist");
                                        } else
                                            $(textA0.parent()).hide();


                                        if ($(pCheckBoxes[1]).prop("checked")) {
                                            textA1.show();
                                            if (!$(pCheckBoxes[0]).prop("checked"))
                                                $($(pCheckBoxes[0]).parents()[3]).removeAttr("validatecheckboxlist");

                                            if (!$(pCheckBoxes[2]).prop("checked"))
                                                $($(pCheckBoxes[2]).parents()[3]).removeAttr("validatecheckboxlist");
                                        }
                                        else
                                            $(textA1.parent()).hide();

                                        if ($(pCheckBoxes[2]).prop("checked")) {
                                            textA2.show();
                                            if (!$(pCheckBoxes[0]).prop("checked"))
                                                $($(pCheckBoxes[0]).parents()[3]).removeAttr("validatecheckboxlist");

                                            if (!$(pCheckBoxes[1]).prop("checked"))
                                                $($(pCheckBoxes[1]).parents()[3]).removeAttr("validatecheckboxlist");
                                        }
                                        else
                                            $(textA2.parent()).hide();

                                        myFixHeight();

                                        pCheckBoxes.each(function () {
                                            var $this = $(this);
                                            $this.click(function () {
                                                var ParentChk0 = $($(pCheckBoxes[0]).parents()[3]);
                                                var ParentChk1 = $($(pCheckBoxes[1]).parents()[3]);
                                                var ParentChk2 = $($(pCheckBoxes[2]).parents()[3]);

                                                var cb0 = !$(pCheckBoxes[0]).prop("checked");
                                                var cb1 = !$(pCheckBoxes[1]).prop("checked");
                                                var cb2 = !$(pCheckBoxes[2]).prop("checked");

                                                var value = $this.attr("value");

                                                if (value == "4") {
                                                    if ($this.prop("checked")) {
                                                        textA0.parent().fadeIn(300);
                                                        textA0.attr("validation", "Required");

                                                        //Quitar la validacion                                                        
                                                        ParentChk1.removeAttr("validatecheckboxlist");
                                                        ParentChk1.removeAttr("style");
                                                        ParentChk2.removeAttr("validatecheckboxlist");
                                                        ParentChk2.removeAttr("style");
                                                        textA0.fadeIn(300);
                                                    }
                                                    else {
                                                        textA0.parent().hide();
                                                        textA0.val("");
                                                        textA0.removeAttr("validation");
                                                        textA0.removeAttr("style");
                                                        ParentChk0.removeAttr("validatecheckboxlist");

                                                        if (cb1)
                                                            ParentChk1.removeAttr("validatecheckboxlist");

                                                        if (cb2)
                                                            ParentChk2.removeAttr("validatecheckboxlist");

                                                        if (cb1 && cb2) {
                                                            ParentChk0.attr("validatecheckboxlist", "validatecheckboxlist");
                                                            ParentChk1.attr("validatecheckboxlist", "validatecheckboxlist");
                                                            ParentChk2.attr("validatecheckboxlist", "validatecheckboxlist");
                                                        }
                                                    }
                                                }

                                                if (value == "7") {
                                                    if ($this.prop("checked")) {
                                                        textA1.parent().fadeIn(300);
                                                        textA1.attr("validation", "Required");
                                                        //Quitar la validacion
                                                        ParentChk0.removeAttr("validatecheckboxlist");
                                                        ParentChk0.removeAttr("style");
                                                        ParentChk2.removeAttr("validatecheckboxlist");
                                                        ParentChk2.removeAttr("style");
                                                        textA1.fadeIn(300);
                                                    }
                                                    else {
                                                        textA1.parent().hide();
                                                        textA1.val("");
                                                        textA1.removeAttr("validation");
                                                        textA1.removeAttr("style");
                                                        ParentChk1.removeAttr("validatecheckboxlist");

                                                        if (cb0)
                                                            ParentChk0.removeAttr("validatecheckboxlist");

                                                        if (cb2)
                                                            ParentChk2.removeAttr("validatecheckboxlist");

                                                        if (cb0 && cb2) {
                                                            ParentChk0.attr("validatecheckboxlist", "validatecheckboxlist");
                                                            ParentChk1.attr("validatecheckboxlist", "validatecheckboxlist");
                                                            ParentChk2.attr("validatecheckboxlist", "validatecheckboxlist");
                                                        }

                                                    }
                                                }


                                                if (value == "10") {
                                                    if ($this.prop("checked")) {
                                                        textA2.parent().fadeIn(300);
                                                        textA2.attr("validation", "Required");
                                                        //Quitar la validacion
                                                        ParentChk0.removeAttr("validatecheckboxlist");
                                                        ParentChk0.removeAttr("style");
                                                        ParentChk1.removeAttr("validatecheckboxlist");
                                                        ParentChk1.removeAttr("style");
                                                        textA2.fadeIn(300);
                                                    }
                                                    else {
                                                        textA2.parent().hide();
                                                        textA2.val("");
                                                        textA2.removeAttr("validation");
                                                        textA2.removeAttr("style");
                                                        ParentChk2.removeAttr("validatecheckboxlist");

                                                        if (cb0)
                                                            ParentChk0.removeAttr("validatecheckboxlist");

                                                        if (cb1)
                                                            ParentChk1.removeAttr("validatecheckboxlist");

                                                        if (cb0 && cb1) {
                                                            ParentChk0.attr("validatecheckboxlist", "validatecheckboxlist");
                                                            ParentChk1.attr("validatecheckboxlist", "validatecheckboxlist");
                                                            ParentChk2.attr("validatecheckboxlist", "validatecheckboxlist");
                                                        }
                                                    }
                                                }

                                                myFixHeight();
                                            });
                                        });

                                        //==============================================================================================
                                    }

                                    //Pregunta 4 del cuestionario INSURANCE INFORMATION PREVIOUS (OR CURRENT) 
                                    var vhdnCurrentTabHealth = $("#hdnCurrentTabHealth").val();
                                    if (vhdnCurrentTabHealth == "lnkInformacionSeguroAnteriorActual") {
                                        var Pregunta4 = $("body").find("[hid='4']");
                                        var Radios = Pregunta4.find("input:radio");

                                        var Rdo = Pregunta4.find("input:radio[checked='checked']");

                                        //Esconder los Text Area
                                        Pregunta4.find("#divDrop").css("display", "none");

                                        if (Rdo.val() == "5") {
                                            //Esconder los Text Area
                                            Pregunta4.find("#divDrop").css("display", "none");
                                        } else
                                            if (Rdo.val() == "4") {
                                                Pregunta4.find("#divDrop").fadeIn(400);
                                            }

                                        //asignar en el evento click de los radio button
                                        Radios.click(function () {
                                            var $this = $(this);
                                            var Pregunta4 = $("body").find("[hid='4']");
                                            //Si
                                            if ($this.val() == "4")
                                                Pregunta4.find("#divDrop").fadeIn(400);
                                            else
                                                //No
                                                if ($this.val() == "5") {
                                                    Pregunta4.find("#divDrop").find("textarea").val("");
                                                    Pregunta4.find("#divDrop").hide();
                                                }

                                            myFixHeight();
                                        });
                                    }

                                    //Pregunta #22
                                    $("body").find("[hid='22']").find("[id*='rblRadioButtonList']").each(function () {
                                        var $this = $(this);
                                        $this.attr("id", $this.attr("id") + "22");
                                    });

                                    $("body").find("[hid='22']").find("[for*='rblRadioButtonList']").each(function () {
                                        var $this = $(this);
                                        $this.attr("for", $this.attr("for") + "22");
                                    });

                                    //Pregunta 3
                                    var Label = (lang == "en") ? "Family Relationship" : "Relación Familiar";
                                    $($("body").find("[hid='3']").find("span")[1]).text(Label);

                                    //Validacion especial para la pregunta #5   
                                    var textAreas = $("body").find("[hid='5']").find("textarea");
                                    var datePickers = $("body").find("[hid='5']").find(".datepicker");

                                    //Nonprescription Medicines
                                    var textAreaNonprescriptionMedicines = $(textAreas[0]);
                                    var datePickersNonprescriptionMedicinesStart = $(datePickers[0]);
                                    var datePickersNonprescriptionMedicinesEnd = $(datePickers[1]);

                                    datePickersNonprescriptionMedicinesEnd.removeAttr('validation');

                                    //Drugs/Alcohol Treatment
                                    var textAreaDrugs_AlcoholTreatment = $(textAreas[1]);
                                    var datePickersDrugs_AlcoholTreatmentStart = $(datePickers[2]);
                                    var datePickersDrugs_AlcoholTreatmentEnd = $(datePickers[3]);

                                    datePickersDrugs_AlcoholTreatmentEnd.removeAttr('validation');

                                    if (
                                         textAreaNonprescriptionMedicines.val() != "" &&
                                         datePickersNonprescriptionMedicinesStart.val() != ""
                                       ) {
                                        textAreaDrugs_AlcoholTreatment.removeAttr("validation");
                                        datePickersDrugs_AlcoholTreatmentStart.removeAttr("validation");
                                    }

                                    if (
                                          textAreaDrugs_AlcoholTreatment.val() != "" &&
                                          datePickersDrugs_AlcoholTreatmentStart.val() != ""
                                        ) {
                                        textAreaNonprescriptionMedicines.removeAttr("validation");
                                        datePickersNonprescriptionMedicinesStart.removeAttr("validation");
                                    }

                                    Configutations();
                                } //END HEALTH DECLARATION

            ChangeTab(CurrentTab);

            if (CurrentTab == "lnkClientInfo" || CurrentTab == "lnkOwnerInfo") {
                var $geSpans = $('.checkInToSpan');
                var $geChecks = $geSpans.find('input[type="checkbox"]');

                $geChecks.addClass('refresh_height');

                $geChecks.on('change', function () {
                    checkBoxes(this);
                });
            }
        }

        if (!isReadOnly) {
            $("#txtClientSearch").mouseover(function () {
                var message = $("#chkClientorOwnerAlreadyinContacts").parent().find("label:first").html();
                $(this).attr("alt", message);
                $(this).mouseover(function () {
                    ShowToolTips(this, 'left', true, "ui-le-frog");
                });
            });

            $("#chkClientorOwnerAlreadyinContacts").click(function () {
                $("#txtClientSearch").val("");
            });
        }

        $(".dxWeb_pcCloseButton").hide();

        $("#hdnDateOfBirthBefore").val($("#tb_WUC_PI_DateBirth").val());

        $("#hdnDateOfBirthBefore_Legal").val($("#tb_WUC_PI_DateBirth_Legal").val());

        $("#fuRequirementFile_Browse0").css({ "background": "transparent", "border": "none" });

        $("#tb_WUC_PI_DateBirth").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: '-3m',
            minDate: '-65y',
            defaultDate: '-3m',
            yearRange: "c-100:c+100",
            dateFormat: 'mm-dd-yy',
            onSelect: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            },
            onClose: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            }, beforeShow: function () {
                SavePosDatePicker(this);
            }
        }).inputmask('mm/dd/yyyy');

        $("#tb_WUC_PI_DateBirth_Legal").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: '-3m',
            minDate: '-65y',
            defaultDate: '-3m',
            yearRange: "c-100:c+100",
            dateFormat: 'mm-dd-yy',
            onSelect: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            },
            onClose: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            }, beforeShow: function () {
                SavePosDatePicker(this);
            }
        }).inputmask('mm/dd/yyyy');

        $("#TDateOfBirth").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: '-3m',
            minDate: '-65y',
            defaultDate: '-3m',
            yearRange: "c-100:c+100",
            dateFormat: 'mm-dd-yy',
            onSelect: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            },
            onClose: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            }, beforeShow: function () {
                SavePosDatePicker(this);
            }
        }).inputmask('mm/dd/yyyy');

        $("#txtDateOfBirthDesignatedPensioner").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: '-3m',
            minDate: '-65y',
            defaultDate: '-3m',
            yearRange: "c-100:c+100",
            dateFormat: 'mm-dd-yy',
            onSelect: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            },
            onClose: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            }, beforeShow: function () {
                SavePosDatePicker(this);
            }
        }).inputmask('mm/dd/yyyy');
        //Bmarroquin 02-11-2017 agrego datePicker para la fecha de vencimiento de la tarjeta en Pagos
        //$("#txtFecVenTarjeta").datepicker({
        $(".datePicker_2").datepicker({
            changeMonth: true,
            changeYear: true,
            showButtonPanel: true,
            dateFormat: 'mm-yy',
            onClose: function (dateText, inst) {
                $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
            }
        });
        if ($("#hdnShowPopIllustration").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvPopIllustration",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "ILLUSTRATION SEARCH" : "BÚSQUEDA DE ILUSTRACIÓN",
                pmodal: true,
                presizable: true,
                OnCLose: function () { $("#hdnShowPopIllustration").val("false"); }
            });
        }

        if ($("#hdnShowPopRequirementPDF").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvPopPDFRequirements",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: "",
                pmodal: true,
                presizable: true,
                OnCLose: function () { $("#hdnShowPopRequirementPDF").val("false"); }
            });
        }

        if ($("#hdnShowPopPersonalizeInvstProf").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#popPersonalizeInvestProf",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "PERSONALIZED INVESTMENT PROFILE" : "PERFIL DE INVERSIÓN PERSONALIZADO",
                pmodal: true,
                presizable: true,
                OnCLose: function () { $("#hdnShowPopPersonalizeInvstProf").val("false"); }
            });
        }

        if (!$("#btn_WCU_A_CopyHomeAddress").hasClass("boton")) {
            $("#btn_WCU_A_CopyHomeAddress").addClass("boton");
        }

        $("body").find("a[alt='Disabled']").each(function () {
            $(this).click(function () {
                CustomDialogMessageEx($(this).html() + lang == "en" ? " tab is disabled" : " tab esta deshabilitado", null, null, true, "Warning");
                EndRequestHandler();
                return false;
            });
        });

        $('.dxBB').append('<span class="upload"></span>');


        $(".st-content-inner").scrollTop($("#hdnPosScroll").val());

        if ($("#tb_WUC_PI_DateBirth").val() != "") {
            $("#tb_WUC_PI_Age").val($("#hdnAge").val());
            $("#txtAge").val($("#hdnAge").val());
        }

        if ($("#tb_WUC_PI_DateBirth_Legal").val() != "") {
            $("#tb_WUC_PI_Age_Legal").val($("#hdnAge_Legal").val());
            $("#txtAge").val($("#hdnAge_Legal").val());
        }

        setClickCheckBoxGridView("#gvCasesInProcess");


        $("#tb_WCU_A_HomeAddress").keydown(function () {

            $("#hdnAddressChanges").val("true");
        });

        $("#tb_WCU_A_HomePostalCode").keydown(function () {

            $("#hdnAddressChanges").val("true");
        });

        SetUploadersText();

        $("#frmIdentifications").find('select,input').removeAttr("onkeydown");
        $("#frmIdentifications_Legal").find('select,input').removeAttr("onkeydown");
        $("#frmPhoneNumbers").find('select,input').removeAttr("onkeydown");
        $("#frmPhoneNumbersLegal").find('select,input').removeAttr("onkeydown");
        $("#frmEmailAddress").find('select,input').removeAttr("onkeydown");
        $("#frmEmailAddress_Legal").find('select,input').removeAttr("onkeydown");

        if ($("#hdnShowPopClientInfoSearch").val() == "true") {
            var SearchClientOrOwnerpop = popSearchClientOrOwner.GetWindowByName('ClientOwnerSearch');
            popSearchClientOrOwner.ShowWindow(SearchClientOrOwnerpop);

            if (CurrentTab == "lnkOwnerInfo")
                SearchClientOrOwnerpop.SetHeaderText(lang == "en" ? 'Owner Search' : 'Búsqueda del Propietario');
            else
                if (CurrentTab == "lnkClientInfo")
                    SearchClientOrOwnerpop.SetHeaderText(lang == "en" ? 'Client Search' : 'Búsqueda del Cliente');
                else
                    if (CurrentTab == "lnkPlanPolicy" || CurrentTab == "lnkBeneficiaries")
                        SearchClientOrOwnerpop.SetHeaderText(lang == "en" ? 'Contact Search' : 'Búsqueda del Contacto');


            $(".dxWeb_pcCloseButton").css("display", "none");
            $(".dxpc-content").css("padding", "0");
        }
        else
            $(".dxWeb_pcCloseButton,.dxWeb_pcCloseButton_DevEx").click();

        formatBrowseButton();

        fixheight();

        setOccupationTypeAutoComplete();
        setOccupationAutoComplete();
        setPositionnAutoComplete();
        setPositionnAutoCompleteDesignated();
        setPositionnAutoCompletelegal();
        setOccupationAutoComplete2();
        setOccupationTypeAutoComplete2();
        divScroll();

        SetDatePicker();

        if (isReadOnly)
            $(".datepicker").datepicker('disable');

        $("#ddlInvestmentProfile").change(function () {
            BeginRequestHandler();
        });

        Configutations();
        //$("#txtExpDate,#txtExpirationDate").datepicker("option", "minDate", "+65d"); //Lgonzalez 07-02-17
        $("#txtFecVenTarjeta").datepicker("option", "minDate", "0"); //Bmarroquin 11-02-2017 
        if ($("#hdnClickBussinessAddress").val() == "true") {
            $('#lnkBusinessAddress').click();
            $("#hdnClickBussinessAddress").val("false");
        }

        if (CurrentTab == "lnkBeneficiaries") {
            if (hdnRefreshBeneficiariesTab.val() == "true" || $("#hdnPendingRefreshBeneficiariesTab").val() == "true") {
                hdnRefreshBeneficiariesTab.val("false");
                $("#hdnPendingRefreshBeneficiariesTab").val("false");
                $("#btnRefresh").click();
            }
        }
        $("#bodyContent_ContactsInfoContainer_WUCIdentification_cbxIDType").change(function () {
            var IDIdentification = eval($('#bodyContent_ContactsInfoContainer_WUCIdentification_cbxIDType').val());
            var ctrlIDIdentification = $('#bodyContent_ContactsInfoContainer_WUCIdentification_txtIDNumber');
            if (IDIdentification == 5) { var $txt = ctrlIDIdentification.inputmask("999-99999-9"); }
            else if (IDIdentification == 1 || IDIdentification == 3) { var $txt = ctrlIDIdentification.inputmask("999-9999999-9"); }
            else if (IDIdentification == 2) { var $txt = ctrlIDIdentification.inputmask("remove"); }
            else { var $txt = ctrlIDIdentification.inputmask("remove"); }

            var $this = $(this);
            if ($this.val() != "1" && $this.val() != "3") {
                $("#txtExpDate").removeAttr("validation");
            }
            else {
                $("#txtExpDate").attr("validation", "Required");
            }
        });

        /*******************VALIDACION PARA REPRESENTANTE LEGAL mavelar 03/23/2017     *******************/
        $("#bodyContent_ContactsInfoContainer_WUCIdentificationLegal_cbxIDType_Legal").change(function () {
            var $this = $(this);
            if ($this.val() != "1" && $this.val() != "3") {
                $("#txtExpDate_Legal").removeAttr("validation");
            }
            else {
                $("#txtExpDate_Legal").attr("validation", "Required");
            }
        });

        /*******************FIN VALIDACION PARA REPRESENTANTE LEGAL mavelar 03/23/2017     *******************/
    }
    catch (e) {
        //CustomDialogMessage(e.message);
        console.log(e.message);
    }
}

function setMaskToControls() {
    $(".NumTarjetaFormat").inputmask("integer", { rightAlign: false, allowPlus: false, allowMinus: false });
}

//Bmarroquin 27-03-2017 Mejora para que se actualice automaticamente el valor de suma Asegurada de GF en base al 10% de suma asegurada de Vida Basico
function ChangeGastosFunerarios() {
    var ddlGastosFunerariosValue = $("#ddlAdditionalTermInsurance").val();
    if (ddlGastosFunerariosValue == "1") {
        $("#txtAdditionalTermInsuranceInsuredAmount").attr("validation", "Required").removeAttr("disabled");
        var lIntSumaAsegVidaBasico = $("#txtMontoAseguradoCorBasica").val();
        if (lIntSumaAsegVidaBasico !== undefined && lIntSumaAsegVidaBasico != "") {
            //Bmarroquin 06-04-2017 correccion a Issue pa q no se pierda cuando la cantidad es >= a 1 Millon, se usa La Regular Expression
            lIntSumaAsegVidaBasico = parseFloat(lIntSumaAsegVidaBasico.replace(/,/g, '')).toFixed(2);
            if (lIntSumaAsegVidaBasico > 0) {
                var lNum_SumaAsegGF = 0.00;
                lNum_SumaAsegGF = lIntSumaAsegVidaBasico * 0.10;
                //Verificar minimos y maximos 
                if (lNum_SumaAsegGF < 1000.00) {
                    lNum_SumaAsegGF = 0.00;
                }
                if (lNum_SumaAsegGF > 2500.00) {
                    lNum_SumaAsegGF = 2500;
                }
                $("#txtAdditionalTermInsuranceInsuredAmount").val(lNum_SumaAsegGF);
            }
        }
        //Fin Mejora Bmarroquin 27-03-2017
    }
    else {
        $("#txtAdditionalTermInsuranceInsuredAmount").removeAttr("validation").attr("disabled", "disabled");
        $("#txtAdditionalTermInsuranceInsuredAmount").val('0.00');
    }
}
//Fin Mejora Bmarroquin 27-03-2017
myFixHeight = function () {
    var intervalo = setInterval("fixheight();", 100);
    setTimeout(function () { clearInterval(intervalo) }, 400);
}


function ClosePopFinalBeneficiary() {
    $('#hdnShowFinalBeneficiaryPop').val('false');
    $("#popupBhvrFinalBeneficiaryPop_backgroundElement").hide();
    $find('popupBhvrFinalBeneficiaryPop').hide(); 
}

function OnGridViewSelectionChanged(sender) {
    sender.UnselectRows();
}

validateQuestionarie = function () {
    var result = true;
    ///Validaciones para campos relacionados a detalles medicos (Doctor)
    var anyActive = false;
    $('#gvHealthDeclaration input[value="SI|1"]').each(function () {
        var $this = $(this);
        if ($(this).prop('checked')) {
            anyActive = true;
        }
    });
    if (anyActive) {
        $("#bodyContent_HealthDeclarationContainer_ddlMDCountry").attr("validation", "Required");
        $("#bodyContent_HealthDeclarationContainer_ddlMDStateProvince").attr("validation", "Required");
        $("#bodyContent_HealthDeclarationContainer_ddlMDCity").attr("validation", "Required");
    }
    else {
        $("#bodyContent_HealthDeclarationContainer_ddlMDCountry").attr("validation", "");
        $("#bodyContent_HealthDeclarationContainer_ddlMDStateProvince").attr("validation", "");
        $("#bodyContent_HealthDeclarationContainer_ddlMDCity").attr("validation", "");
    }


    //Validar el formulario del primer tab
    result = validateForm("PNDetalle");

    if (result) {
        var RowsCollection = $("#gvHealthDeclaration > tbody > tr");
        var QuestionArray = new Array();

        RowsCollection.each(function () {
            var CellCollection = $(this).find("td");
            CellCollection.each(function () {
                try {

                    var Index = $(this).find("[hidcheck]").attr("hidcheck");
                    if (Index != undefined) {

                        var YesOrNo = $(this).find("[hidcheck]").find("input:checked").val().split("|")[0];

                        var ActualTD = $(this);

                        if (YesOrNo == "SI") {
                            var MasterSelector = "[validation='CheckCombo'],[validation='Required'],[validateCheckboxList='validateCheckboxList'],[RadiolistRequired='RadiolistRequired'],table";
                            $("#gvHealthDeclaration").find("[hid='" + Index + "']").find(MasterSelector).each(function () {

                                if (Index == 1) {
                                    result = validateForm("frmQuestion1");
                                    if (!result)
                                        QuestionArray.push(Index);
                                }

                                //Validacion especial para la pregunta #5 
                                if (Index == 5) {
                                    var textAreas = $("body").find("[hid='5']").find("textarea");
                                    var datePickers = $("body").find("[hid='5']").find(".datepicker");

                                    //Nonprescription Medicines
                                    var textAreaNonprescriptionMedicines = $(textAreas[0]);
                                    var datePickersNonprescriptionMedicinesStart = $(datePickers[0]);
                                    var datePickersNonprescriptionMedicinesEnd = $(datePickers[1]);

                                    //Drugs/Alcohol Treatment
                                    var textAreaDrugs_AlcoholTreatment = $(textAreas[1]);
                                    var datePickersDrugs_AlcoholTreatmentStart = $(datePickers[2]);
                                    var datePickersDrugs_AlcoholTreatmentEnd = $(datePickers[3]);

                                    if (
                                         textAreaNonprescriptionMedicines.val() != "" &&
                                         datePickersNonprescriptionMedicinesStart.val() != "" &&
                                         datePickersNonprescriptionMedicinesEnd.val() != ""
                                       ) {
                                        textAreaDrugs_AlcoholTreatment.removeAttr("validation");
                                        datePickersDrugs_AlcoholTreatmentStart.removeAttr("validation");
                                        datePickersDrugs_AlcoholTreatmentEnd.removeAttr("validation");
                                    }


                                    if (
                                          textAreaDrugs_AlcoholTreatment.val() != "" &&
                                          datePickersDrugs_AlcoholTreatmentStart.val() != "" &&
                                          datePickersDrugs_AlcoholTreatmentEnd.val() != ""
                                        ) {
                                        textAreaNonprescriptionMedicines.removeAttr("validation");
                                        datePickersNonprescriptionMedicinesStart.removeAttr("validation");
                                        datePickersNonprescriptionMedicinesEnd.removeAttr("validation");
                                    }
                                }

                                //Validar dropdown
                                if ($(this).is("select")) {
                                    if ($(this).val() == "-1") {
                                        result = false;
                                        QuestionArray.push(Index);
                                        $(this).parent().css("border", "1px solid rgb(192, 61, 190)");
                                        $(this).change(function () {
                                            changeBorderColor($(this));
                                        });
                                    }
                                }

                                //Validacion de DropDownCheck
                                var IsDropDownCheck = ($(this).find("[dropdawncheck]").length > 0);
                                if (IsDropDownCheck) {
                                    if ($(this).is("table")) {
                                        var ListCheck = $($(this).parents()[2]).find("[dropdawncheck]").find("input:checkbox");
                                        var CountCheck = getCountCheck(ListCheck);
                                        if (CountCheck == 0) {
                                            ActualTD.find("div:first").css("border", "1px solid rgb(192, 61, 190)");
                                            result = false;
                                            QuestionArray.push(Index);
                                            ListCheck.click(function () {
                                                ActualTD.find("div:first").css("border", "0px");
                                            });
                                        } else {
                                            //Verificar si hay al menos un checkcbox del dropdown que esta seleccionado chequeado
                                            ListCheck.each(function () {
                                                var oCheckbox = $(this);
                                                if (oCheckbox.prop("checked")) {
                                                    var Container = $(oCheckbox.parents()[4]).find("tr")[1];
                                                    //Verificar si en el dropdown hay al menos un checkbox
                                                    var ListCheckBox = $(Container).find("input:checkbox");
                                                    var Count = getCountCheck(ListCheckBox);
                                                    if (Count == 0) {
                                                        var DropSelect = $(Container).find("td > div > div > div");
                                                        DropSelect.css("border", "1px solid rgb(192, 61, 190)");
                                                        $(ListCheckBox).click(function () {
                                                            DropSelect.css("border", "");
                                                            DropSelect.css({ "display": "inline-block", "position": "relative", "width": "195px" });
                                                        });
                                                        QuestionArray.push(Index);
                                                        result = false;
                                                    }
                                                }
                                            });
                                        }
                                    }
                                }

                                //Validacion de textcheck
                                var Istextcheck = ($(this).find("[textcheck]").length > 0);
                                if (Istextcheck) {
                                    if ($(this).is("table")) {
                                        var ListCheck = $($(this).parents()[2]).find("[textcheck]").find("input:checkbox");
                                        var CountCheck = getCountCheck(ListCheck);
                                        if (CountCheck == 0) {
                                            ActualTD.find("div:first").css("border", "1px solid rgb(192, 61, 190)");
                                            result = false;
                                            QuestionArray.push(Index);
                                            ListCheck.click(function () {
                                                ActualTD.find("div:first").css("border", "0px");
                                            });
                                        }
                                    }
                                }

                                if ($(this).is("div") && $(this).attr("validation") == "CheckCombo") {
                                    var oListChk = $(this).find("input:checkbox");
                                    var c = getCountCheck(oListChk);
                                    if (c == 0) {
                                        result = false;
                                        var oC = $(this).find("div:first");
                                        oC.css("border", "1px solid rgb(192, 61, 190)");

                                        oC.find("input:checkbox").click(function () {
                                            oC.css("border", "");
                                            oC.css({ "display": "inline-block", "position": "relative", "width": "195px" });
                                        });

                                        QuestionArray.push(Index);
                                    }
                                }

                                if ($(this).hasClass("dxgvControl_DevEx") || $(this).hasClass("dxgvControl")) {
                                    var totalRows = parseInt($(this).parent().parent().parent().find("input:hidden[id*='HiddenFieldGrid']").val());
                                    if (totalRows <= 0) {
                                        result = false;
                                        $(this).css("border", "1px solid rgb(192, 61, 190)");
                                        QuestionArray.push(Index);
                                    }
                                }

                                if ($(this).is("input[type='text']") && $(this).attr("validation") != null) {
                                    if ($(this).val() == "") {
                                        result = false;
                                        QuestionArray.push(Index);
                                        $(this).css("border", "1px solid rgb(192, 61, 190)");
                                        $(this).keydown(function () {
                                            changeBorderColor(this);
                                        });
                                    }
                                }

                                if ($(this).is("textarea") && $(this).attr("validation") != null) {
                                    if ($(this).val() == "") {
                                        result = false;
                                        QuestionArray.push(Index);
                                        $(this).css("border", "1px solid rgb(192, 61, 190)");
                                        $(this).keydown(function () {
                                            changeBorderColor(this);
                                        });
                                    }
                                }

                                if ($(this).attr("RadiolistRequired") != null) {
                                    var container = $(this);
                                    var counter = CountCheckEx($(this), 'radio');
                                    if (counter == 0) {
                                        result = false;
                                        QuestionArray.push(Index);
                                        $(this).css("border", "1px solid rgb(192, 61, 190)");
                                        $(this).find("input:radio").click(function () {
                                            if ($(this).prop("checked")) {
                                                changeBorderColor(container);
                                            }
                                        });
                                    }
                                }

                                if ($(this).attr("validateCheckboxList") != null) {
                                    var container = $(this);
                                    var counter = CountCheckEx($(this), 'checkbox');
                                    if (counter == 0) {
                                        result = false;
                                        QuestionArray.push(Index);
                                        $(this).css("border", "1px solid rgb(192, 61, 190)");
                                        $(this).find("input:checkbox").click(function () {
                                            if ($(this).prop("checked")) {
                                                changeBorderColor(container);
                                            }
                                        });
                                    }
                                }
                            });
                        }
                    }

                 } catch (e) {
                     console.log(e.message);
                 }
            });
        });

        if (!result) {
            var Questions = QuestionArray.unique().join();
            var ErrorMessage = {};
            var ArrMessage = [];
            ErrorMessage.ErrorType = "QuestionsAnsweredValidation";
            ErrorMessage.Field = Questions;
            ArrMessage.push(ErrorMessage);
            CustomDialogMessageEx(JSON.stringify(ArrMessage), null, null, true, lang == "en" ? "Validation Summary" : "Resumen de validación", "jsonMessage");
        }
    }

    if (result) {
        //Validar que todas las preguntas esten marcadas en tab correspondiente
        var tds = $("#gvHealthDeclaration > tbody > tr > td");
        var countQuestions = tds.find("[hidcheck]").length;
        var countResponses = tds.find("[hidcheck]").find("input:checked").length;
        if (countResponses < countQuestions) {
            result = false;
            var oErrorMessage = {};
            var ArrMess = [];
            oErrorMessage.ErrorType = "QuestionariesValidation";
            ArrMess.push(oErrorMessage);
            CustomDialogMessageEx(JSON.stringify(ArrMess), null, null, true, lang == "en" ? "Validation Summary" : "Resumen de validación", "jsonMessage");
        }
    }
    return result;
};

getDropdownCheck = function (selector) {
    var DropDownHideArray = new Array();

    $("#gvHealthDeclaration").find(selector).each(function () {
        DropDownHideArray.push($(this).attr("dropdawnhide"));
    });

    return DropDownHideArray.unique();
};

getCheckBoxList = function (selector) {
    return $("#gvHealthDeclaration").find(selector).find("input:checkbox");
}

setColorCheckBox = function (ListCheck) {
    ListCheck.each(function () {
        var obj = $($(this).parents()[1]);
        obj.css("border", "1px solid red");
    });
};

getCountCheck = function (ListCheck) {
    var count = 0;
    ListCheck.each(function () {
        if ($(this).prop("checked"))
            count++;
    });
    return count;
};

hideOrShow = function (index, sender) {

    var el = $(sender).attr("id");
    var obj = $("#" + el);

    var gvHealthDeclaration = $("#gvHealthDeclaration");

    if (obj.val().split("|")[0] == "SI") {
        gvHealthDeclaration.find("[hid='" + index + "']").fadeIn(300);
    } else
        if (obj.val().split("|")[0] == "NO") {
            gvHealthDeclaration.find("[hid='" + index + "']").find("input,textarea,select").each(function () {

                var $this = $(this);

                if ($this.is("input:text,textarea")) {
                    $this.val("");
                }

                if ($this.is("input:radio,input:checkbox")) {
                    if ($this.prop("checked"))
                        $(this).click();
                }

                if ($(this).is("select")) {
                    $($(this).find("option")[0]).attr('selected', 'selected');
                }
            });

            gvHealthDeclaration.find("[hid='" + index + "']").parent().css("border", "0px");
            gvHealthDeclaration.find("[hid='" + index + "']").hide();

            var Pregunta4 = $("body").find("[hid='" + index + "']");
            Pregunta4.find("#divDrop").find("textarea").val("");
        }

    myFixHeight();
};

hideCheckTexbox = function (sender) {
    var oCheckBox = $(sender);
    if (oCheckBox.prop("checked")) {
        var oDiv = $($(oCheckBox.parents()[5]).find("tr")[1]).find("div:first");
        oDiv.find("input:text").attr("validation", "Required");
        oDiv.fadeIn(400);
    }
    else {
        var oDiv = $($(oCheckBox.parents()[5]).find("tr")[1]).find("div:first");
        oDiv.find("input:text").val("");
        oDiv.find("input:text").removeAttr("validation", "Required");
        oDiv.hide();
    }

    var intervalo = setInterval("fixheight();", 100);
    setTimeout(function () { clearInterval(intervalo) }, 400);
};

changeTabHealth = function () {
    var Tab = $("#hdnCurrentTabHealth").val();
    //Limpiar
    $("#MenuTabsH li").removeClass("active");

    vTab = $('#' + Tab);

    vTab.parent().addClass("active");
};

function validateAgent() {
    var isReadOnly = $("#hdnIsReadOnly").val() == "true";
    if (isReadOnly) return false;
    var oddl_P_ANC_AgentName = $("#ddl_P_ANC_AgentName");

    if (oddl_P_ANC_AgentName.val() == "-1") {
        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "AgentValidation");
        return false;
    } else
        __doPostBack($('#btnCallSearchOwnerInfo').attr('name'), '');
}

ClearTextBox = function (sender) {
    var txt1 = $(sender).parent().find("input[type='text']:first");
    var txt2 = $($(sender).parent().find("input[type='text']")[1]);

    if ($(sender).children("input").prop("checked")) {
        txt1.focus();
    }
    else {
        txt1.val("");
        if (txt2)
            txt2.val("");
    }
    var labels = $(sender).parent().find("label");

    $(labels[1]).css("color", "#111");
    $(labels[2]).css("color", "#111");
};

CallPopup = function () {
    if (!$("#chkClientAlredy")[0].checked) {

        if ($("#ddl_P_ANC_AgentName > option").length > 0) {
            if ($("#ddl_P_ANC_AgentName").val() == "-1") {
                CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "AgentValidation");
                EndRequestHandler();
                return false;
            }
        } else {
            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "AgentValidation");
            EndRequestHandler();
            return false;
        }

        __doPostBack($('#btnCallSearchClientInfo').attr('name'), '');
    }
};

validacionesTab = function (sender) {

    BeginRequestHandler();

    var result = true;

    var hdnCurrentTabAddNewClient = $("#hdnCurrentTabAddNewClient").val();

    var CurrentTab = hdnCurrentTabAddNewClient.split('|')[0];
    var NoOrdenCurrent = parseInt(hdnCurrentTabAddNewClient.split('|')[1]);
    var NoOrdenNext = parseInt($(sender).attr("alt"));
    if (NoOrdenNext >= NoOrdenCurrent) {
        var ddl_Office = $("#ddl_Office");
        if (ddl_Office != null) {
            //Validar que el dropdown de las oficinas este lleno
            if (ddl_Office.val() == "-1") {
                CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "OfficeValidation");
                EndRequestHandler();
                return false;
            }
        }

        //Validar que el dropdown de los agentes este lleno
        if ($("#ddl_P_ANC_AgentName").val() == "-1") {
            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "AgentValidation");
            EndRequestHandler();
            return false;
        }

        //Validar que el dropdown de la relacion con el asegurado este lleno
        if ($("#ddl_P_ANC_Relationship").val() == "-1") {
            var key = CurrentTab == "lnkClientInfo" ? "RelationshipWithInsuredValidation" : "RelationshipWithOwnerValidation";
            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", key);
            EndRequestHandler();
            return false;
        }

        //**** Validaciones para la compañia *****// 
        if (CurrentTab == "lnkHealthDeclaration") {
            result = validateQuestionarie();
            EndRequestHandler();
        } else
            if (CurrentTab == "lnkOwnerInfo") {
                if ($("#chkIsCompany")[0].checked) {

                    result = (validateForm("frmCompany"));

                    if (result) {
                        //*************VALUDACIONES PARA COMPAñIA Y REPRESENTANTE LEGAL************//

                        result = (validateForm("frmPersonalInformationLegal"));

                        //validar Business Address de la compañia
                        if (($("#tb_WCU_A_BusinessAddress").val().trim() == "") || ($("#ddl_WUC_A_BusinessCountry").val() == null || $("#ddl_WUC_A_BusinessCountry").val() == "-1") || ($("#ddl_WUC_A_BusinessState").val() == null || $("#ddl_WUC_A_BusinessState").val() == "-1")
                        || ($("#ddl_WUC_A_BusinessCity").val() == null || $("#ddl_WUC_A_BusinessCity").val() == "-1")) {
                            result = validateForm("frmbusinessAddress");
                        }

                        //validar Home Address para representante legal
                        if (($("#tb_WCU_A_HomeAddress_Legal").val().trim() == "") || ($("#ddl_WUC_A_HomeCountry_Legal").val() == null || $("#ddl_WUC_A_HomeCountry_Legal").val() == "-1") || ($("#ddl_WUC_A_HomeState_Legal").val() == null || $("#ddl_WUC_A_HomeState_Legal").val() == "-1")
                        || ($("#ddl_WUC_A_HomeCity_Legal").val() == null || $("#ddl_WUC_A_HomeCity_Legal").val() == "-1")) {
                            result = validateForm("frmHomeAddress_Legal");
                        }
                        //else if (($("#tb_WCU_A_BusinessAddress_Legal").val().trim() == "") || ($("#ddl_WUC_A_BusinessCountry_Legal").val() == null || $("#ddl_WUC_A_BusinessCountry_Legal").val() == "-1") || ($("#ddl_WUC_A_BusinessState_Legal").val() == null || $("#ddl_WUC_A_BusinessState_Legal").val() == "-1")
                        //    || ($("#ddl_WUC_A_BusinessCity_Legal").val() == null || $("#ddl_WUC_A_BusinessCity_Legal").val() == "-1")) {
                        //    result = validateForm("frmbusinessAddress_Legal");
                        //}

                        //telefono de Representante
                        var TotalPhone = parseInt($("#hdnTotalPhonesLegal").val());
                        //telefono compañia
                        var TotalPhoneCompany = parseInt($("#hdnTotalPhones").val());
                        if (TotalPhoneCompany <= 0) {
                            EndRequestHandler();
                            var EntityCompany = "PhoneNumberOwnerValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", EntityCompany);
                        }

                        if (TotalPhone <= 0) {
                            EndRequestHandler();
                            var Entity = "PhoneNumberAgentLegalValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }

                        //email compañia
                        var TotalEmnail = parseInt($("#hdnTotalEmail").val());
                        if (TotalEmnail <= 0) {
                            EndRequestHandler();
                            var Entity = "EmailOwnerValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }
                        var TotalEmail = parseInt($("#hdnTotalEmail_Legal").val());
                        if (TotalEmail <= 0) {
                            EndRequestHandler();
                            var Entity = "EmailOwnerLegalValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }

                        var TotalIDCompanyLegal = parseInt($("#hdnTotalIdentification_Legal").val());
                        if (TotalIDCompanyLegal <= 0) {
                            EndRequestHandler();
                            var Entity = "IdentificationOwnerLegalValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }

                        // FIN*************VALUDACIONES PARA EL REPRESENTANTE LEGAL************//
                    }
                } else {
                    //Validar el formulario de Personal Information para el Owner
                    result = (validateForm("frmPersonalInformation"));

                    if (result) {
                        //var PersonalIncome = $("#tb_WUC_PI_PersonalIncome");
                        //var YearLyFamilyIncome = $("#tb_WUC_PI_YearLyFamilyIncome");

                        //var valPersonalIncome = parseFloat(replaceAll(",", "", $("#tb_WUC_PI_PersonalIncome").val()));
                        //var valYearLyFamilyIncome = parseFloat(replaceAll(",", "", $("#tb_WUC_PI_YearLyFamilyIncome").val()));

                        //if (valPersonalIncome > valYearLyFamilyIncome) {
                        //    EndRequestHandler();
                        //    result = false;
                        //    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "PersonalIncomeValidation");
                        //}
                        }

                    var oMessage;
                    var ArrayMessage = [];
                    var validationSummary = "";

                    //Validar el formulario de BackGroundInformation  Grupo 1
                    $("#frmGrupo1").find("input[type='checkbox']").each(function () {
                        var Chk = $(this)[0];
                        var TxtBox = $($(this).parents()[1]).find("input[type='text']");

                        if (Chk.checked && $.trim(TxtBox.val()) == "") {
                            var Field = TxtBox.parent().find("label:first");
                            Field.css("color", "red");
                            TxtBox.focus();
                            oMessage = {};
                            oMessage.ErrorType = "Required";
                            oMessage.Field = Field.html();
                            ArrayMessage.push(oMessage);
                            $(TxtBox).keydown(function () {
                                if ($(this).val() != "")
                                    $(this).parent().find("label:first").css("color", "#111");

                            });

                            EndRequestHandler();
                        }
                    });


                    //Validar el formulario de BackGroundInformation  Grupo 2
                    $("#frmGrupo2").find("input[type='checkbox']").each(function () {
                        var Chk = $(this)[0];

                        var txt = $($(this).parents()[1]).find("input[type='text']");

                        var TxtBox1 = $(txt[0]);
                        var TxtBox2 = $(txt[1]);

                        if (Chk.checked && ($.trim(TxtBox1.val()) == "" || $.trim(TxtBox2.val()) == "")) {
                            if ($.trim(TxtBox1.val()) == "" && $.trim(TxtBox2.val()) != "") {

                                var Field = TxtBox1.parent().find("label:first");
                                Field.css("color", "red");
                                TxtBox1.focus();
                                oMessage = {};
                                oMessage.ErrorType = "Required";
                                oMessage.Field = Field.html();
                                ArrayMessage.push(oMessage);
                            } else
                                if ($.trim(TxtBox2.val()) == "" && $.trim(TxtBox1.val()) != "") {
                                    var Field = TxtBox2.parent().find("label:first");
                                    Field.css("color", "red");
                                    TxtBox2.focus();
                                    oMessage = {};
                                    oMessage.ErrorType = "Required";
                                    oMessage.Field = Field.html();
                                    ArrayMessage.push(oMessage);
                                }
                                else {
                                    var Field1 = TxtBox1.parent().find("label:first");
                                    var Field2 = TxtBox2.parent().find("label:first");
                                    Field1.css("color", "red");
                                    Field2.css("color", "red");
                                    TxtBox1.focus();
                                    oMessage = {};
                                    oMessage.ErrorType = "Required";
                                    oMessage.Field = Field1.html();
                                    ArrayMessage.push(oMessage);

                                    oMessage = {};
                                    oMessage.ErrorType = "Required";
                                    oMessage.Field = Field2.html();
                                    ArrayMessage.push(oMessage);
                                }

                            $(TxtBox1).keydown(function () {
                                if ($.trim($(this).val()) != "")
                                    $(this).parent().find("label:first").css("color", "#111");
                            });

                            $(TxtBox2).keydown(function () {
                                if ($(this).val() != "")
                                    $(this).parent().find("label:first").css("color", "#111");
                            });

                            EndRequestHandler();
                        }
                    });
                         
                    var oJsonMessage = JSON.stringify(ArrayMessage);

                    var Title = lang == "en" ? "Validation Summary" : "Resumen de Validación";

                    //Mostrar el summary de validaciones
                    if (ArrayMessage.length > 0) {
                        CustomDialogMessageEx(oJsonMessage, 500, null, true, Title, "jsonMessage");
                        result = false;
                    }

                    //====== Validaciones de Address ====================================================================//

                    if (result) {
                        //validar Home Address
                        if ($("#tb_WCU_A_HomeAddress").val().trim() != "" ||
                           ($("#ddl_WUC_A_HomeCountry").val() != null && $("#ddl_WUC_A_HomeCountry").val() != "-1") ||
                           ($("#ddl_WUC_A_HomeState").val() != null && $("#ddl_WUC_A_HomeState").val() != "-1") ||
                           ($("#ddl_WUC_A_HomeCity").val() != null && $("#ddl_WUC_A_HomeCity").val() != "-1")
                           ) {
                            result = validateForm("frmHomeAddress");

                        } else if ($("#tb_WCU_A_BusinessAddress").val().trim() != "" ||
                                ($("#ddl_WUC_A_BusinessCountry").val() != null && $("#ddl_WUC_A_BusinessCountry").val() != "-1") ||
                                ($("#ddl_WUC_A_BusinessState").val() != null && $("#ddl_WUC_A_BusinessState").val() != "-1") ||
                                ($("#ddl_WUC_A_BusinessCity").val() != null && $("#ddl_WUC_A_BusinessCity").val() != "-1")
                            ) {
                            //validar Business Address
                            result = validateForm("frmbusinessAddress");

                        } else {
                            var Entity = CurrentTab != "lnkOwnerInfo" ? "AddressInsuredValidation" : "AddressOwnerValidation";
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                            EndRequestHandler();
                            result = false;
                        }
                    }

                    //====== Validaciones de Address ====================================================================//

                    if (result) {

                        var TotalPhone = parseInt($("#hdnTotalPhones").val());
                        if (TotalPhone <= 0) {
                            EndRequestHandler();
                            var Entity = CurrentTab != "lnkOwnerInfo" ? "PhoneNumberInsuredValidation" : "PhoneNumberOwnerValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }
                    }

                    if ($("#STFCUserProfile1_drpCompanyProfile option:selected").val() != "2") {
                        var TotalEmnail = parseInt($("#hdnTotalEmail").val());

                        if (TotalEmnail <= 0) {
                            EndRequestHandler();
                            var Entity = CurrentTab != "lnkOwnerInfo" ? "EmailInsuredValidation" : "EmailOwnerValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }
                    }

                    if (result) {

                        var TotalID = parseInt($("#hdnTotalIdentification").val());
                        if (TotalID <= 0) {
                            EndRequestHandler();
                            var Entity = CurrentTab != "lnkOwnerInfo" ? "IdentificationInsuredValidation" : "IdentificationOwnerValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }
                    }
                }

            } else if (CurrentTab == "lnkPlanPolicy") {

                if ($("#hdnValidateFormDesignatedPensionerOrAddicionalInsured").val() == "true") {
                    //vAdditionalInsured 
                    var isViewAdditonalInsured = ($("#hdnCurrentView").val() == "vAdditionalInsured");
                    var hasDependent = $("#bodyContent_PlanPolicyContainer_WUCDesignatedPensionerInformation_pnDependents").css("display") == "block";

                    if (!isViewAdditonalInsured && !hasDependent) {
                        result = validateDesignatedPensionerOrAdditionalInsured();
                    }
                    else {
                        var CountAdditionalAdd = $("#hdnCountAdditionalsInsured").val();

                        if (CountAdditionalAdd <= 0) {
                            EndRequestHandler();
                            result = false;
                            var msjKey = !hasDependent ? "AdditionalInsuredValidation" : "DependentsValidation";
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", msjKey);
                        }
                    }
                }

                if (result)
                    result = (validateForm("frmPlan"));

                if (result)
                    result = validateForm("pnFooter");
                EndRequestHandler();
            }
                //**** End Validaciones para la compañia *****// 
            else if (CurrentTab == "lnkClientInfo" || CurrentTab == "lnkOwnerInfo") {
                //Validar la edad

                if (result)
                    //Validar el formulario de Personal Information
                    result = (validateForm("frmPersonalInformation"));

                if (result) {
                    //var PersonalIncome = $("#tb_WUC_PI_PersonalIncome");
                    //var YearLyFamilyIncome = $("#tb_WUC_PI_YearLyFamilyIncome");

                    //var valPersonalIncome = parseFloat(replaceAll(",", "", $("#tb_WUC_PI_PersonalIncome").val()));
                    //var valYearLyFamilyIncome = parseFloat(replaceAll(",", "", $("#tb_WUC_PI_YearLyFamilyIncome").val()));

                    //if (valYearLyFamilyIncome < valPersonalIncome) {
                    //    EndRequestHandler();
                    //    result = false;
                    //    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", 'PersonalIncomeValidation');
                    //}
                }

                if (result) {

                    var TotalPhone = parseInt($("#hdnTotalPhones").val());

                    if (TotalPhone <= 0) {
                        EndRequestHandler();
                        var Entity = CurrentTab != "lnkOwnerInfo" ? "PhoneNumberInsuredValidation" : "PhoneNumberOwnerValidation";
                        result = false;
                        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                    }
                }

                if (result) {

                    if ($("#STFCUserProfile1_drpCompanyProfile option:selected").val() != "2") {
                        var TotalEmnail = parseInt($("#hdnTotalEmail").val());

                        if (TotalEmnail <= 0) {
                            EndRequestHandler();
                            var Entity = CurrentTab != "lnkOwnerInfo" ? "EmailInsuredValidation" : "EmailOwnerValidation";
                            result = false;
                            CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        }
                    }
                }

                if (result) {

                    var TotalID = parseInt($("#hdnTotalIdentification").val());
                    if (TotalID <= 0) {
                        EndRequestHandler();
                        var Entity = CurrentTab != "lnkOwnerInfo" ? "IdentificationInsuredValidation" : "IdentificationOwnerValidation";
                        result = false;
                        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                    }
                }

                var oMessage;
                var ArrayMessage = [];

                var validationSummary = "";

                //Validar el formulario de BackGroundInformation  Grupo 1
                $("#frmGrupo1").find("input[type='checkbox']").each(function () {
                    var Chk = $(this)[0];
                    var TxtBox = $($(this).parents()[1]).find("input[type='text']");

                    if (Chk.checked && $.trim(TxtBox.val()) == "") {
                        var Field = TxtBox.parent().find("label:first");
                        Field.css("color", "red");
                        TxtBox.focus();
                        oMessage = {};
                        oMessage.ErrorType = "Required";
                        oMessage.Field = Field.html();
                        ArrayMessage.push(oMessage);
                        $(TxtBox).keydown(function () {
                            if ($(this).val() != "")
                                $(this).parent().find("label:first").css("color", "#111");

                        });

                        EndRequestHandler();
                    }
                });


                //Validar el formulario de BackGroundInformation  Grupo 2
                $("#frmGrupo2").find("input[type='checkbox']").each(function () {
                    var Chk = $(this)[0];

                    var TxtBox1 = $($(this).parent().parent().find("input[type='text']")[0]);
                    var TxtBox2 = $($(this).parent().parent().find("input[type='text']")[1]);

                    if (Chk.checked && ($.trim(TxtBox1.val()) == "" || $.trim(TxtBox2.val()) == "")) {
                        if ($.trim(TxtBox1.val()) == "" && $.trim(TxtBox2.val()) != "") {

                            var Field = TxtBox1.parent().find("label:first");
                            Field.css("color", "red");
                            TxtBox1.focus();
                            oMessage = {};
                            oMessage.ErrorType = "Required";
                            oMessage.Field = Field.html();
                            ArrayMessage.push(oMessage);
                        } else
                            if ($.trim(TxtBox2.val()) == "" && $.trim(TxtBox1.val()) != "") {
                                var Field = TxtBox2.parent().find("label:first");
                                Field.css("color", "red");
                                TxtBox2.focus();
                                oMessage = {};
                                oMessage.ErrorType = "Required";
                                oMessage.Field = Field.html();
                                ArrayMessage.push(oMessage);
                            }
                            else {
                                var Field1 = TxtBox1.parent().find("label:first");
                                var Field2 = TxtBox2.parent().find("label:first");
                                Field1.css("color", "red");
                                Field2.css("color", "red");
                                TxtBox1.focus();
                                oMessage = {};
                                oMessage.ErrorType = "Required";
                                oMessage.Field = Field1.html();
                                ArrayMessage.push(oMessage);

                                oMessage = {};
                                oMessage.ErrorType = "Required";
                                oMessage.Field = Field2.html();
                                ArrayMessage.push(oMessage);
                            }

                        $(TxtBox1).keydown(function () {
                            if ($.trim($(this).val()) != "")
                                $(this).parent().find("label:first").css("color", "#111");
                        });

                        $(TxtBox2).keydown(function () {
                            if ($(this).val() != "")
                                $(this).parent().find("label:first").css("color", "#111");
                        });

                        EndRequestHandler();
                    }
                });

                //Validar el formulario de BackGroundInformation  Grupo 3
                var div = $('#divContainerRepeterQuestion .radio input');

                var realcant = div.length / 2;
                var QuestionNotAnswer = 0;
                for (var i = 0; i < realcant; i++) {
                    var idyes = 'bodyContent_ContactsInfoContainer_WUCBackgroundInformation_repeaterQuestion_rbYes_' + i;
                    var idno = 'bodyContent_ContactsInfoContainer_WUCBackgroundInformation_repeaterQuestion_rbNo_' + i;

                    if ($('input[id=' + idyes + ']:checked').val() === undefined && $('input[id=' + idno + ']:checked').val() === undefined) {
                       QuestionNotAnswer++;
                    }
                }

                if (QuestionNotAnswer > 0) {

                    oMessage = {};
                    oMessage.ErrorType = "ComplementaryInformationincomplete";
                    ArrayMessage.push(oMessage);

                    EndRequestHandler();
                }

                //====== Validaciones de Address ====================================================================//
                if (result) {
                    //validar Home Address
                    if ($("#tb_WCU_A_HomeAddress").val().trim() != "" ||
                       ($("#ddl_WUC_A_HomeCountry").val() != null && $("#ddl_WUC_A_HomeCountry").val() != "-1") ||
                       ($("#ddl_WUC_A_HomeState").val() != null && $("#ddl_WUC_A_HomeState").val() != "-1") ||
                       ($("#ddl_WUC_A_HomeCity").val() != null && $("#ddl_WUC_A_HomeCity").val() != "-1")
                       ) {
                        result = validateForm("frmHomeAddress");

                    } else if ($("#tb_WCU_A_BusinessAddress").val().trim() != "" ||
                            ($("#ddl_WUC_A_BusinessCountry").val() != null && $("#ddl_WUC_A_BusinessCountry").val() != "-1") ||
                            ($("#ddl_WUC_A_BusinessState").val() != null && $("#ddl_WUC_A_BusinessState").val() != "-1") ||
                            ($("#ddl_WUC_A_BusinessCity").val() != null && $("#ddl_WUC_A_BusinessCity").val() != "-1")
                        ) {
                        //validar Business Address
                        result = validateForm("frmbusinessAddress");

                    } else {
                        var Entity = CurrentTab != "lnkOwnerInfo" ? "AddressInsuredValidation" : "AddressOwnerValidation";
                        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                        EndRequestHandler();
                        result = false;
                    }
                }

                var oJsonMessage = JSON.stringify(ArrayMessage);

                var Title = lang == "en" ? "Validation Summary" : "Resumen de Validación";

                //Mostrar el summary de validaciones
                if (ArrayMessage.length > 0) {
                    CustomDialogMessageEx(oJsonMessage, 500, null, true, Title, "jsonMessage");
                    result = false;
                }
            }
    }
    return result;
};

validateDesignatedPensionerOrAdditionalInsured = function () {
    var result = (validateForm("frmDesignatedPensioner"));

    if (result)
        result = (validateForm("frmIDOccupation"));


    return result;

};

validateTotalAmount = function (obj) {
    var returnVal = true;

    $('.barra_azul_celeste').find('input:text').each(function () {
        if (parseFloat(replaceAll(",", "", $(this).val())) > parseFloat(replaceAll(",", "", obj.val())))
            returnVal = false;
    })

    return returnVal;
}

validateFormIdentification = function (form) {

    var resultado = validateForm(form);
    return resultado;
};

validateFormEmailAddress = function (sender, form) {

    var resultado = validateForm(form);
    return resultado;
};

validateFormPhoneNumbers = function (sender, form) {

    var resultado = validateForm(form);
    return resultado;
};

validateFormCitizenContact = function (sender, form) {

    var resultado = validateForm(form);

    if (true) {

        return resultado;
    } else {

        return resultado;
    }

};

validateFormIdentification_Legal = function (form) {
    var resultado = validateForm(form);
    return resultado;
};

validateFormEmailAddress_Legal = function (sender, form) {
    var resultado = validateForm(form);
    return resultado;
};

validateFormPhoneNumbers_Legal = function (sender, form) {
    var resultado = validateForm(form);
    return resultado;
};

calculateAge = function (birthday) {
    var now = new Date();
    var anio = birthday.substring(6);
    var mes = birthday.substring(0, 2);
    var dia =birthday.substring(3, 5); 
    //var past = new Date(birthday.substring(6), birthday.substring(3, 5), birthday.substring(0, 2));
    /*var past = new Date(anio, mes - 1, dia);
    var nowYear = now.getFullYear();
    var pastYear = past.getFullYear(); //past.getFullYear();
    var age = nowYear - pastYear;*/
    // cogemos los valores actuales
    var fecha_hoy = new Date();
    var ahora_ano = now.getYear();
    var ahora_mes = now.getMonth();
    var ahora_dia = now.getDate();
    // realizamos el calculo
    var edad = (ahora_ano + 1900) - anio;
    if (ahora_mes < (mes - 1)) {
        edad--;
    }
    if (((mes - 1) == ahora_mes) && (ahora_dia < dia)) {
        edad--;
    }
    if (edad > 1900) {
        edad -= 1900;
    }
    return edad;
};

CallExecuteOnCloseEvent = function (sender, selectedDate) {

    //Cambiar de Tab
    var CurrentTab = $("#hdnCurrentTabAddNewClient").val().split('|')[0];

    if (selectedDate != "") {
        if (CurrentTab == "lnkHealthDeclaration") {

            var Dtp12 = $("body").find("[hid='12']").find("input.datepicker");
            var Dtp14 = $("body").find("[hid='14']").find("input.datepicker");

            var FromTxt12 = $(Dtp12[0]);
            var Totxt12 = $(Dtp12[1]);

            var FromTxt14 = $(Dtp14[0]);
            var Totxt14 = $(Dtp14[1]);

            validateDateRange(FromTxt12, Totxt12, sender);
            validateDateRange(FromTxt14, Totxt14, sender);

            //Pregunta 5            
            var datePickers = $("body").find("[hid='5']").find(".datepicker");

            var datePickersNonprescriptionMedicinesStart = $(datePickers[0]);
            var datePickersNonprescriptionMedicinesEnd = $(datePickers[1]);

            var datePickersDrugs_AlcoholTreatmentStart = $(datePickers[2]);
            var datePickersDrugs_AlcoholTreatmentEnd = $(datePickers[3]);

            validateDateRange(datePickersNonprescriptionMedicinesStart, datePickersNonprescriptionMedicinesEnd, sender);
            validateDateRange(datePickersDrugs_AlcoholTreatmentStart, datePickersDrugs_AlcoholTreatmentEnd, sender);
            //end Pregunta 5

        }
        var fecha = new Date();

        if ($(sender).attr('id') == "tb_WUC_PI_DateBirth" ||
        $(sender).attr('id') == 'txtDateOfBirthDesignatedPensioner' ||
        $(sender).attr('id') == 'TDateOfBirth'
            
        || $(sender).attr('id') == 'tb_WUC_PI_DateBirth_Legal')
            {
            var BeforeVal = $("#hdnDateOfBirthBefore").val();

            var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();
            var message = lang == 'en' ? 'The date of birth can not be future, the date "' + selectedDate + '" is a future date.'
            : 'La fecha de nacimiento no puede ser futura, la fecha "' + selectedDate + '" es una fecha futura.';
            pDate = pDate.ToDate('mm/dd/yyyy');
            if (selectedDate.ToDate('mm/dd/yyyy') > pDate) {
                //Clean Controls
                $(sender).val("");

                if ($(sender).attr('id') == 'tb_WUC_PI_DateBirth_Legal') {
                    $("#hdnAge_Legal").val("");
                    $("#tb_WUC_PI_Age_Legal").val("");
                }
                else {

                    $("#hdnAge").val("");
                    $("#tb_WUC_PI_Age").val("");

                }

                $("#txtAge").val("");

                $(sender).focus();
                CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                return false;
            }
            //var age = selectedDate.YearDiff("", "dd/mm/yyyy");//Lgonzalez 13-02-17
            var age = calculateAge(selectedDate);
            if ($(sender).attr('id') == "tb_WUC_PI_DateBirth" && age < 18) {
                $(sender).val("");
                $(sender).focus();
                CustomDialogMessageEx('La edad de la persona no puede ser menor a 18 años', null, null, true, "Warning");
                return false;
            }

            if ($(sender).attr('id') == "tb_WUC_PI_DateBirth_Legal" && age < 18) {
                $(sender).val("");
                $(sender).focus();
                CustomDialogMessageEx('La edad de la persona no puede ser menor a 18 años', null, null, true, "Warning");
                return false;
            }

            if (age < 18 && $(sender).attr('id') == 'TDateOfBirth')
                $("#TIdentification").removeAttr('validation');
            else
                $("#TIdentification").attr('validation', 'Required');

            if ($(sender).attr('id') == 'tb_WUC_PI_DateBirth_Legal') {
                $("#hdnAge_Legal").val(age);
                $("#tb_WUC_PI_Age_Legal").val(age);
                $("#txtAge").val(age);
                $("#ddl_WUC_PI_MaritalStatus_Legal").focus();
            }
            else {

                $("#hdnAge").val(age);
                $("#tb_WUC_PI_Age").val(age);
                $("#txtAge").val(age);
                $("#ddl_WUC_PI_MaritalStatus").focus();

            }

        } else
            if ($(sender).attr('id') == 'txtExpDate' ||
                $(sender).attr('id') == 'txtExpirationDate') {

                var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();
                console.log($(sender).attr('id') + ' ' +pDate);
                var FechaSeleccionada = selectedDate.split("/");

                var FechaFinal = FechaSeleccionada[0] + "/" + FechaSeleccionada[1] + "/" + FechaSeleccionada[2];

                var Days = pDate.DayDiff(FechaFinal, 'mm/dd/yyyy');

                if (Days < 61) {
                    $(sender).val("");
                    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "DocExpirationDate");
                }
            } else if ($(sender).attr('id') == 'txtRegistrationDate') {

                var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();

                if (selectedDate.ToDate('mm/dd/yyyy') > pDate.ToDate('mm/dd/yyyy')) {
                    $(sender).val("");
                    $(sender).focus();
                    var message = lang == 'en' ? 'The date of registration of the company should not be a future date, the date "' + selectedDate + '" is a future date.'
                                     : 'La fecha de registro de la empresa no puede ser una fecha futura, la fecha "' + selectedDate + '" es una fecha futura.';
                    CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                }
            } else if ($(sender).attr("alt") == 'validateFutureDateofBirth' ||
                       $(sender).attr("alt") == 'validateFutureDate') {

                var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();

                if (selectedDate.ToDate('mm/dd/yyyy') > pDate.ToDate('mm/dd/yyyy')) {
                    $(sender).val("");
                    $(sender).focus();

                    var message = lang == 'en' ? 'The date can not be future, the date "' + selectedDate + '" is a future date.'
                                      : 'La fecha no puede ser futura, la fecha "' + selectedDate + '" es una fecha futura.';
                    CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                }
            }
    }
}

CloseSearchClientOrOwner = function () {
    var SearchClientOrOwnerpop = popSearchClientOrOwner.GetWindowByName('ClientOwnerSearch');
    popSearchClientOrOwner.ShowWindow(SearchClientOrOwnerpop);
    $("#hdnShowPopClientInfoSearch").val("false");
    popSearchClientOrOwner.Hide();
};

ChangeTab = function (Tab) {

    //Limpiar
    $("#MenuTabs li").removeClass("encurso");

    vTab = $('#' + Tab);

    vTab.parent().addClass("encurso");
};

//// BENEFICIARIES TAB  -  Ing. José Mejía ////
function ValidateBeneficiaries(sender) {
    /*******************Para el Tab de Benefificario ------- Beneficiarios  Principales -------mavelar 03/09/2017     *******************/
    //RNC
    var idDocument = eval($('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_cbxIDType').val());
    var ctrlID = $("input[name='ctl00$bodyContent$BeneficiariesContainer$WUCMainInsured$WUCMainBeneficiaries$txtIDNo']");
    if (idDocument == 5) {
        var $txt = ctrlID.inputmask("999-99999-9");
        var value = ctrlID.val();
        var num = value.match(/\d+/g).join("");
        if (num.length < 9) {
            CustomDialogMessageEx('El RNC debe tener 9 digitos.', null, null, true, lang == "en" ? "Warning" : "Advertencia");
            return false;
        }
    }
    //CEDULA
    else if (idDocument == 1 || idDocument == 3) {
        var $txt = ctrlID.inputmask("999-9999999-9");
        var value = ctrlID.val();
        var num = value.match(/\d+/g).join("");
        if (num.length != 11) {
            CustomDialogMessageEx('La cedula de identidad debe tener 11 digitos.', null, null, true, lang == "en" ? "Warning" : "Advertencia");
            return false;
        }
    }
    else {
        var $txt = ctrlID.inputmask("remove");
    }
    var idDocumentContingent = eval($('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_cbxIDType').val());
    var ctrlIDContingent = $("input[name='ctl00$bodyContent$BeneficiariesContainer$WUCMainInsured$WUCContingentBeneficiaries$txtIDNo']");
    //RNC
    if (idDocumentContingent == 5) {
        var $txt = ctrlIDContingent.inputmask("999-99999-9");
        var value = ctrlIDContingent.val();
        var num = value.match(/\d+/g).join("");
        if (num.length < 9) {
            CustomDialogMessageEx('El RNC debe tener 9 digitos.', null, null, true, lang == "en" ? "Warning" : "Advertencia");
            return false;
        }
    }
    //DUI
    else if (idDocumentContingent == 1) {
        var $txt = ctrlIDContingent.inputmask("999-9999999-9");
        var value = ctrlIDContingent.val();
        var num = value.match(/\d+/g).join("");
        if (num.length < 9) {
            CustomDialogMessageEx('La cedula de identidad debe tener 11 digitos.', null, null, true, lang == "en" ? "Warning" : "Advertencia");
            return false;
        }
    }
    else {
        var $txt = ctrlIDContingent.inputmask("remove");
    }
    /*******************Para el Tab de Benefificario ------- Beneficiarios  Contingenciales -------mavelar 03/09/2017     *******************/
    var clase = $(sender).attr("class").split(" ")[1];

    //Para Buscar los controles a validar, esto procedimiento es porque no se pueden 
    //buscar por nombre ya que el control se repite varias veces en la misma pagina.
    var txtFirstName = GetElementByClassAndId('txtFirstName', clase, 'input[type="text"]').val().trim();
    var txtLastName = GetElementByClassAndId('txtLastName', clase, 'input[type="text"]').val().trim();
    var txtPercentage = GetElementByClassAndId('txtPercentage', clase, 'input[type="text"]').val().trim();
    var ddlRelationship = GetElementByClassAndId('ddlRelationship', clase, 'select').prop("selectedIndex");

    //Para Buscar el Datepicker del Control que llamo el Evento.
    var arrayTxt = $("[name$=txtBEDateofBirth]");
    var txtBEDateofBirth = null;

    for (var x = 0; x <= arrayTxt.length; x++) {
        if ($(arrayTxt[x]).hasClass(clase)) {
            txtBEDateofBirth = $(arrayTxt[x]).val();
            break;
        }
    }

    if (ddlRelationship > 0) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'RelationShipValidation');
        return false;
    }

    if (txtFirstName != '') {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'NameValidation');
        return false;
    }

    if (txtLastName != '') {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'LastNameValidation');
        return false;
    }

    if (txtBEDateofBirth != '') {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'BirthDateValidation');
        return false;
    }

    if (txtBEDateofBirth.IsDate('mm/dd/yyyy')) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Invalid Required Fields' : 'Campos inválidos vacios', 'validBirthDateValidation');
        return false;
    }

    if (txtPercentage != '' && txtPercentage > 0) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'validPercentageValidation');
        return false;
    }

    if (txtPercentage < 101) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Wrong Percentage" : "Porcentaje incorrecto", 'Percentage100Validation');
        return false;
    }

    return true;
}

function ValidateBeneficiariesCompany(sender) {
    var clase = $(sender).attr("class").split(" ")[1];

    //Para Buscar los controles a validar, esto procedimiento es porque no se pueden 
    //buscar por nombre ya que el control se repite varias veces en la misma pagina.
    var txtEntityName = GetElementByClassAndId('txtEntityName', clase, 'input[type="text"]').val().trim();
    var txtEntityPercentage = GetElementByClassAndId('txtEntityPercentage', clase, 'input[type="text"]').val().trim();
    var ddlEntityType = GetElementByClassAndId('ddlEntityType', clase, 'select').val();

    //Para Buscar el Datepicker del Control que llamo el Evento.
    var arrayTxt = $("[name$=txtBEIncorporationDate]");
    var txtBEIncorporationDate = null;

    for (var x = 0; x <= arrayTxt.length; x++) {
        if ($(arrayTxt[x]).hasClass(clase)) {
            txtBEIncorporationDate = $(arrayTxt[x]).val();
            break;
        }
    }


    if (txtBEIncorporationDate != '') {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'IncorporationDateValidation');
        return false;
    }

    if (txtBEIncorporationDate.IsDate('mm/dd/yyyy')) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Invalid Required Fields' : 'Campos inválidos vacios', 'validIncorporationDateValidation');
        return false;
    }

    if (txtEntityName != '') {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'EntityNameValidation');
        return false;
    }

    if (txtEntityPercentage != '' && txtEntityPercentage > 0) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'validPercentageValidation');
        return false;
    }

    if (txtEntityPercentage < 101) {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Wrong Percentage" : "Porcentaje incorrecto", 'validPercentageValidation');
        return false;
    }

    if (ddlEntityType != '' && ddlEntityType != '-1') {
        returnVal = true;
    }
    else {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? 'Empty Required Fields' : 'Campos requeridos vacios', 'EntityTypeValidation');
        return false;
    }

    return true;
}

function upCOFileStartUpload(sender, event) {
    var clase = sender.mainElement.attributes.class.value.split(" ")[1];
    BEDisableInputsFile(sender, true, clase, true);
}

function uploadFileChange(sender, event) {
    sender.Upload();
}

function uploadFileContainerComplete(sender, event) {
    var obj = JSON.parse(event.callbackData);
    if (obj.file == "")
        showAlert(obj.error);
    else {

        var hdnUploadedPDFPath = $('#hdnUploadedPDFPath');

        if (hdnUploadedPDFPath != null) {
            var FileName = obj.file.split("~~")[1];
            hdnUploadedPDFPath.val(obj.file);

            if (sender.uniqueID == "ctl00$bodyContent$BeneficiariesContainer$fuMainBenediciarieFile") {
                $("#bodyContent_BeneficiariesContainer_fuMainBenediciarieFile_Browse0 span").removeClass("upload");
                $("#bodyContent_BeneficiariesContainer_fuMainBenediciarieFile_Browse0 span").addClass("pdf_b");
            }

        }
    }
}

function uploadFileComplete(sender, event) {
    var obj = JSON.parse(event.callbackData);
    if (obj.file == "")
        showAlert(obj.error);
    else {
        var clase = sender.mainElement.attributes.class.value.split(" ")[1];
        if (clase != null) {
            var hdnUploadedPDFPath = GetElementByClassAndId('hdnUploadedPDFPath', clase, 'input[type="text"]');

            if (hdnUploadedPDFPath != null) {
                var FileName = obj.file.split("~~")[1];
                hdnUploadedPDFPath.val(obj.file);

                FileName = "..\\TempFiles\\" + FileName;
                setIntervalFileName(FileName, clase, 0);
            }
        }
    }

    BEDisableInputsFile(sender, false, clase, false);
}

function upFileStartUpload(sender, event) {
    var clase = sender.mainElement.attributes.class.value.split(" ")[1];
    BEDisableInputsFile(sender, true, clase, false);
}

BEDisableInputsFile = function (sender, val, className, isCompany) {
    var AddButton = GetElementByClassAndId((!isCompany ? 'btnAdd' : 'btnBECompanyAdd'), className, 'input[type="submit"]');
    var ClearButton = GetElementByClassAndId((!isCompany ? 'btnBEClear' : 'btnBECompanyClear'), className, 'input[type="submit"]');

    $(AddButton).prop('disabled', val);
    $(ClearButton).prop('disabled', val);
    $(sender).prop('disabled', val);
    $(sender).prop('enabled', !val);
}

function uploadFileCompanyComplete(sender, event) {
    var obj = JSON.parse(event.callbackData);
    if (obj.file == "")
        showAlert(obj.error);
    else {
        var clase = sender.mainElement.attributes.class.value.split(" ")[1];
        if (clase != null) {
            var hdnUploadedPDFPathCompany = GetElementByClassAndId('hdnUploadedPDFPathCompany', clase, 'input[type="text"]');

            if (hdnUploadedPDFPathCompany != null) {
                var FileName = obj.file.split("~~")[1];
                hdnUploadedPDFPathCompany.val(obj.file);

                FileName = "..\\TempFiles\\" + FileName;
                setIntervalFileName(FileName, clase, 1);
            }
        }
    }

    BEDisableInputsFile(sender, false, clase, true);
}

//Usado para setear un intervalo, para mostrar el nombre del documento, ya que al subirlo, no se queda en el textbox.
function setIntervalFileName(fileName, className, isCompany) {
    var myVar = setInterval(function () {
        if (isCompany == 1) {
            switch (className) {
                case 'MP':
                    $('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
                case 'MC':
                    $('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AP':
                    $('#bodyContent_BeneficiariesContainer_WUCAdditionalInsured_WUCMainBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AC':
                    $('#bodyContent_BeneficiariesContainer_WUCAdditionalInsured_WUCContingentBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
            }
        }
        else {
            switch (className) {
                case 'MP':
                    $('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
                case 'MC':
                    $('#bodyContent_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AP':
                    $('#bodyContent_BeneficiariesContainer_WUCAdditionalInsured_WUCMainBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AC':
                    $('#bodyContent_BeneficiariesContainer_WUCAdditionalInsured_WUCContingentBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
            }
        }

        clearInterval(myVar);
    }, 100);
}

function SetUploadersText() {
    //Person
    var FileName = "";
    var hdnUploadedPDFPathMC = GetElementByClassAndId('hdnUploadedPDFPath', 'MC', 'input[type="text"]');
    var hdnUploadedPDFPathAP = GetElementByClassAndId('hdnUploadedPDFPath', 'AP', 'input[type="text"]');
    var hdnUploadedPDFPathAC = GetElementByClassAndId('hdnUploadedPDFPath', 'AC', 'input[type="text"]');
    var hdnUploadedPDFPathMP = GetElementByClassAndId('hdnUploadedPDFPath', 'MP', 'input[type="text"]');

    if (hdnUploadedPDFPathMC != null) {
        FileName = $(hdnUploadedPDFPathMC).val();
        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'MC', 0);
        }
    }

    if (hdnUploadedPDFPathAP != null) {
        FileName = $(hdnUploadedPDFPathAP).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'AP', 0);
        }
    }

    if (hdnUploadedPDFPathAC != null) {
        FileName = $(hdnUploadedPDFPathAC).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'AC', 0);
        }
    }

    if (hdnUploadedPDFPathMP != null) {
        FileName = $(hdnUploadedPDFPathMP).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'MP', 0);
        }
    }


    //Company
    var hdnUploadedPDFPathCompanyMC = GetElementByClassAndId('hdnUploadedPDFPathCompany', 'MC', 'input[type="text"]');
    var hdnUploadedPDFPathCompanyAP = GetElementByClassAndId('hdnUploadedPDFPathCompany', 'AP', 'input[type="text"]');
    var hdnUploadedPDFPathCompanyAC = GetElementByClassAndId('hdnUploadedPDFPathCompany', 'AC', 'input[type="text"]');
    var hdnUploadedPDFPathCompanyMP = GetElementByClassAndId('hdnUploadedPDFPathCompany', 'MP', 'input[type="text"]');

    if (hdnUploadedPDFPathCompanyMC != null) {
        FileName = $(hdnUploadedPDFPathCompanyMC).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'MC', 1);
        }
    }

    if (hdnUploadedPDFPathCompanyAP != null) {

        FileName = $(hdnUploadedPDFPathCompanyAP).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'AP', 1);
        }
    }

    if (hdnUploadedPDFPathCompanyAC != null) {
        FileName = $(hdnUploadedPDFPathCompanyAC).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'AC', 1);
        }
    }

    if (hdnUploadedPDFPathCompanyMP != null) {
        FileName = $(hdnUploadedPDFPathCompanyMP).val();

        if (FileName.trim() != "") {
            FileName = "..\\TempFiles\\" + FileName.split("~~")[1];
            setIntervalFileName(FileName, 'MP', 1);
        }
    }

}

function ClosePaymentPDFPop() {
    $find('popupBhvr').hide();
    return false;
}

checkBoxes = function (sender) {
    var $leCheckClicked = $(sender);
    var $leDivToShowOrHide = $leCheckClicked.parent().parent().find('div');

    if ($leCheckClicked.is(':checked')) {
        $leDivToShowOrHide.removeClass('campos');
        $leDivToShowOrHide.addClass('camposShow');
    } else {
        $leDivToShowOrHide.removeClass('camposShow');
        $leDivToShowOrHide.addClass('campos');
    }

    fixheight();
};

function validateHomeAddress() {
    if ($("#tb_WCU_A_HomeAddress").val().trim() == "" ||
         ($("#ddl_WUC_A_HomeCountry").val() == null && $("#ddl_WUC_A_HomeCountry").val() == "-1") ||
         ($("#ddl_WUC_A_HomeState").val() == null && $("#ddl_WUC_A_HomeState").val() == "-1") ||
         ($("#ddl_WUC_A_HomeCity").val() == null && $("#ddl_WUC_A_HomeCity").val() == "-1")
         ) {
        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", 'HomeAddressValidation');
        return false;
    }

    return true;
}
//// END BENEFICIARIES TAB ////

var textSeparator = ";";
var Currentlistbox;

function OnListBoxSelectionChanged(listBox, args) {
    Currentlistbox = listBox;
    if (args.index == 0)
        args.isSelected ? Currentlistbox.SelectAll() : Currentlistbox.UnselectAll();
    UpdateSelectAllItemState();
    UpdateText();
}

function UpdateSelectAllItemState() {
    IsAllSelected() ? Currentlistbox.SelectIndices([0]) : Currentlistbox.UnselectIndices([0]);
}

function IsAllSelected() {
    var selectedDataItemCount = Currentlistbox.GetItemCount() - (Currentlistbox.GetItem(0).selected ? 0 : 1);
    return Currentlistbox.GetSelectedItems().length == selectedDataItemCount;
}

function UpdateText() {
    var selectedItems = Currentlistbox.GetSelectedItems();
    //Currentlistbox.SetText(GetSelectedItemsText(selectedItems));
}

function SynchronizeListBoxValues(dropDown, args) {
    Currentlistbox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = GetValuesByTexts(texts);
    Currentlistbox.SelectValues(values);
    UpdateSelectAllItemState();
    UpdateText(dropDown); // for remove non-existing texts
}

function GetSelectedItemsText(items) {
    var texts = [];
    for (var i = 0; i < items.length; i++)
        if (items[i].index != 0)
            texts.push(items[i].text);
    return texts.join(textSeparator);
}

function GetValuesByTexts(texts) {
    var actualValues = [];
    var item;
    for (var i = 0; i < texts.length; i++) {
        item = checkListBox.FindItemByText(texts[i]);
        if (item != null)
            actualValues.push(item.value);
    }
    return actualValues;
}

function PPCalcTotalPercent() {
    var totalPercent = 0.00;

    $('.PDPercentage').each(function () {
        totalPercent += parseFloat($(this).val());
    });

    return totalPercent.toFixed(2);
}

function SetPPTotalPercent(obj) {
    if (PPCalcTotalPercent() > 100) {
        $(obj).focus();
        $(obj).val('0.00');
        var msj = lang == "en" ? "'Error: Total Percentage can\'t be greater than 100%.'" : "Error: Porcentaje total no puede ser superior al 100%.";
        CustomDialogMessageEx(msj, 500, 150, true, lang == "en" ? "Wrong Percentage" : "Porcentaje incorrecto");
    }
    $('#txtPPTotalPercent').val(PPCalcTotalPercent() + '%');
}

function ValidatePerProfileTotal() {
    var returnVal = true;
    returnVal = validateForm("frmPersonalized");

    if (returnVal) {

        if (PPCalcTotalPercent() != 100) {
            returnVal = false;
            var msj = lang == "en" ? 'Error: Total Percentage must be 100%.' : "Error: Porcentaje total debe ser del 100%.";
            CustomDialogMessageEx(msj, 500, 150, true, lang == "en" ? "Wrong Percentage" : "Porcentaje incorrecto");
        }
    }
    return returnVal;
}

function SelectedTwoDropDownValues(sourceDrop, destinatDrop) {

    var selectedValue = sourceDrop.value;
    var destinationDrop = document.getElementById(destinatDrop);// This should perform dynamically.
    destinationDrop.value = selectedValue;
    window.__doPostBack(destinationDrop.id);
}
  
//Dirson Breton
function CalculateItbis(val) {
    var result = 0;

    var valor = val.replace(/,/g, "");
    var Tax = document.getElementById("HDFItbis");
    var itbis = parseFloat(Tax.value);

    var valorcal = parseFloat(valor);

    result = valorcal * itbis;

    return result;
}

function ValItbis(valor) {
    var SelectiveTax = document.getElementById("txtSelectiveTax");

    if (SelectiveTax != null)
        SelectiveTax.value = CalculateItbis(valor.toString());
}

function CalculateAnnualPrimium(PeriodicPremiumvalue, ddlFrequencyvalue) {
    var periodicpremium = parseFloat(PeriodicPremiumvalue.replace(/,/g, ""));
    var HDFMultiploAnual = document.getElementById("HDFMultiploAnual");
    var MultiploAnual = parseFloat(HDFMultiploAnual.value);

    var annualPremium = 0;

    switch (ddlFrequencyvalue) {
        case "1"://Trimestral
            annualPremium = (periodicpremium * 4) - ((periodicpremium * 4) * MultiploAnual);
            break;
        case "2"://Mensual
            annualPremium = (periodicpremium * 12) - ((periodicpremium * 12) * MultiploAnual);
            break;
        case "3"://Anual
            annualPremium = (periodicpremium * 1) - ((periodicpremium * 1) * MultiploAnual);
            break;
        case "4"://Semestral
            annualPremium = (periodicpremium * 2) - ((periodicpremium * 2) * MultiploAnual);
            break;
    }

    return annualPremium;
}

function GetAnnualPremium(PeriodicPremiumvalueID, ddlFrequencyID) {

    var txtAnnualPremium = document.getElementById('txtAnnualPremium');

    var txtAnnualPremiumWithTax = document.getElementById('txtAnnualPremiumWithTax');

    var hdAnnualPremium = document.getElementById('hdAnnualPremium');

    var frequency = document.getElementById(ddlFrequencyID);

    var periodicpremium = document.getElementById(PeriodicPremiumvalueID)

    var TaxPct = document.getElementById("HDFItbis");

    var itbisPct = parseFloat(TaxPct.value);

    var TtlAnualpremium = 0.00;
    if (frequency != null && periodicpremium != null) {
        var PeriodicPremiumvalue = periodicpremium.value;
        var ddlFrequencyvalue = frequency.value;
        TtlAnualpremium = CalculateAnnualPrimium(PeriodicPremiumvalue.toString(), ddlFrequencyvalue);
    }

    ValItbis(TtlAnualpremium);
    if (txtAnnualPremium != null && txtAnnualPremiumWithTax != null) {
        txtAnnualPremium.value = TtlAnualpremium.toString();
        txtAnnualPremiumWithTax.value = TtlAnualpremium; 
    }


    ///Para Retorno de prima
    var RopAmount = 0.00;
    var txtAmount = document.getElementById('bodyContent_PlanPolicyContainer_WUCPlanInformation_UCSentinel_txtAmount');
    var ddlContributionPeriod = document.getElementById('bodyContent_PlanPolicyContainer_WUCPlanInformation_UCSentinel_ddlContributionPeriod');
    var InitialContribution = 0.00;

    if (txtAmount != null) {
        if (txtAmount.value != "")
            InitialContribution = parseFloat(txtAmount.value.replace(/[^0-9\.]/g, ''));
    }

    if ($('#bodyContent_PlanPolicyContainer_WUCPlanInformation_UCSentinel_ddlProductName').val() != undefined) {
        
        var JsonValue = $.parseJSON($('#bodyContent_PlanPolicyContainer_WUCPlanInformation_UCSentinel_ddlProductName').val());

        if (JsonValue.Product == 9 || JsonValue.Product == 10) {

            if (ddlContributionPeriod != null) {
                var frecuencia = 0;
                switch (frequency.value) {
                    case '3': //anual
                        frecuencia = 1;
                        break;
                    case '2': //mensual
                        frecuencia = 12;
                        break;
                    case '1': //trimestral
                        frecuencia = 4;
                        break;
                    case '4': //semestral
                        frecuencia = 2;
                        break;
                }

                var ContributionPeriod = ddlContributionPeriod.value;
                var PorcentajeDevolver = 0.0;

                switch (true) {
                    case (ContributionPeriod == 10):
                        PorcentajeDevolver = 0.50;
                        break;
                    case (ContributionPeriod == 11):
                        PorcentajeDevolver = 0.55;
                        break;
                    case (ContributionPeriod == 12):
                        PorcentajeDevolver = 0.60;
                        break;
                    case (ContributionPeriod == 13):
                        PorcentajeDevolver = 0.65;
                        break;
                    case (ContributionPeriod == 14):
                        PorcentajeDevolver = 0.70;
                        break;
                    case (ContributionPeriod == 15):
                        PorcentajeDevolver = 0.75;
                        break;
                    case (ContributionPeriod == 16):
                        PorcentajeDevolver = 0.80;
                        break;
                    case (ContributionPeriod == 17):
                        PorcentajeDevolver = 0.85;
                        break;
                    case (ContributionPeriod == 18):
                        PorcentajeDevolver = 0.90;
                        break;
                    case (ContributionPeriod == 19):
                        PorcentajeDevolver = 0.95;
                        break;
                    case (ContributionPeriod > 19 && ContributionPeriod <= 30):
                        PorcentajeDevolver = 1.00;
                        break;
                    case (ContributionPeriod < 10):
                    default:
                        PorcentajeDevolver = 0.0;
                        break;
                }

                //El retorno de la prima ahora se calculara con sus valores decimales
                RopAmount = (InitialContribution + (ContributionPeriod * (TtlAnualpremium))) * PorcentajeDevolver;

                var txtReturnofPremium = document.getElementById('txtReturnofPremium');
                txtReturnofPremium.value = RopAmount;
            }
        }

        var clientID = document.getElementById('Hidden1'); 
        clientID.value = RopAmount;
    }
}