﻿var lang = "";

DiscardPadecimiento = function () {
    $('#hfPadecimiento').val('false');
    $find('popupPadecimiento').hide();
};

myFixHeight = function () {
    var intervalo = setInterval("fixheight();", 100);
    setTimeout(function () { clearInterval(intervalo) }, 400);
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

        function ShowParentPolitical(result, Section) {

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
            
            if ($('input[id=bodyContent_DReviewContainer_WUCBackgroundInformation_repeaterQuestion_rbYes_3]:checked').val() == 'rbYes') {
                //Mostrar el panel de contacto
                ShowParentPolitical(true, 'ISPARENTPOLITICAL_section');
            }

            if ($('input[id=bodyContent_WUCView_ContactsInfoContainer_WUCBackgroundInformation_repeaterQuestion_rbYes_3]:checked').val() == 'rbYes') {
                //Mostrar el panel de contacto
                ShowParentPolitical(true, 'ISPARENTPOLITICAL_Designated');
            }

            if ($('input[id=bodyContent_ContactsInfoContainer_WUCBackgroundInformationLegal_repeaterQuestion_rbYes_3]:checked').val() == 'rbYes') {
                ShowParentPolitical(true, 'ISPARENTPOLITICAL_legal');
            }

        });

        var hdnRefreshBeneficiariesTab = $("#hdnRefreshBeneficiariesTab");

        var popupBhvr3333_backgroundElement = $('#popupBhvr3333_backgroundElement');
        if ($('#hdnShowPopViewPDF').val() == 'true' &&
            popupBhvr3333_backgroundElement != null)
            popupBhvr3333_backgroundElement.css('z-index', '-10000');

        $("#gvCompareEdit_DXFREditorcol4,#gvDataReview_DXFREditorcol14").hide();

        $("#pnModalPopupPDFViewer,#PopMergeCases,#bodyContent_DReviewContainer_PopCompareEdit,#PopPdfViewer").draggable({
            handle: ".titulos_azules"
        }).find(".titulos_azules").css("cursor", "move");

        lang = $("#hdnLang").val();

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
                // dateFormat: 'dd/mm/yy',
                firstDay: 1,
                isRTL: false,
                showMonthAfterYear: false,
                yearSuffix: ''
            };

            $.datepicker.setDefaults($.datepicker.regional['es']);
        }

        setCurrentAccordeonForIndex("#hfMenuAccordeonBeneficiaries");

        var ScrollMaster = $($("#gridMaster").find(".dxgvCSD")[0]);

        ScrollMaster.scroll(function () {
            $("#hdnPosScrollMasterGrid").val($(this).scrollTop());
        });

        ScrollMaster.scrollTop($("#hdnPosScrollMasterGrid").val());

        $("#pdfViewer").find("embed").attr("width", "890px");

        sessionTimeout = $("#hdnTimeOut").val();

        var isView = ($("#hdnIsView").val() == "true");

        $(".dxgvHSDC").css("width", "98.91%");

        $("#bodyContent_WUCView_ContactsInfoContainer_WUCSaveTab_udpSave").hide();
        $('#bodyContent_DReviewContainer_PaymentContainer_udpUpdate').hide();

        //CheckBoxes del grid master
        $(".CheckMasterGrid > input:checkbox").click(function () {
            $this = $(this);

            var idSender = $this.attr("id");

            var idSenderName = $this.attr("name");

            var currentRowSelect = $this.parents()[2];

            var RealId = idSenderName.substr(idSenderName.lastIndexOf('$') + 1, idSenderName.length);

            var NextTr = $(currentRowSelect).next();

            var isNext = NextTr.find("td:first").hasClass("dxgvIndentCell");

            if ((isNext && RealId == "chkSameAsSelected" && NextTr.find("input:checkbox").length == 0) || (!isNext && RealId == "chkSameAsSelected")) {
                $(idSender).prop("checked", false);
                return false;
            }

            var isCheck = $(currentRowSelect).find("input:checkbox:checked").length > 1;

            //Solo uno debe estar chequeado
            if (isCheck) {
                $($this.parents()[2]).find("input[type='checkbox']").each(function () {
                    if (idSender != $(this).attr("id"))
                        $(this).prop("checked", false);
                });
            }
            //End                   

            if (isNext) {
                var Chks = NextTr.find("input:checkbox");

                if (RealId == "chkNewClient") {
                    //Deshabilitar todos los checkbox del grid detalle                
                    Chks.prop("checked", false);
                    if ($(this).prop("checked"))
                        Chks.attr("disabled", "disabled");
                    else
                        Chks.removeAttr("disabled");
                } else {
                    //habilitar todos los checkbox del grid detalle                                
                    Chks.removeAttr("disabled");
                }
            }

            SetCheckGrid();
        });

        //CheckBoxes del grid detalle
        $(".detailcheckGrid > input:checkbox").click(function () {

            $this = $(this);
            var idSender = $this.attr("id");

            var tbody = $this.parents()[3];

            var isCheck = $(tbody).find("input:checkbox:checked").length > 1;

            if ($this.prop("checked")) {
                var RowPrev = $this.parents().eq(10).prev();
                var chk = RowPrev.find("input:checkbox[id*='chkSameAsSelected']");
                chk.prop("checked", true);
            }

            //Solo uno debe estar chequeado
            if (isCheck) {
                $(tbody).find("input[type='checkbox']").each(function () {
                    var $this = $(this);
                    if (idSender != $this.attr("id"))
                        $this.prop("checked", false);
                });
            }
            //End

            SetCheckGrid();
        });

        //Seleccion de los checkboxes del grid master
        if ($("#CheckIds").length > 0 && $("#CheckIds").val() != "") {
            var CheckIdsCheck = $("#CheckIds").val().split(",");
            for (var i = 0; i < CheckIdsCheck.length; i++) {
                var $this = $("#" + CheckIdsCheck[i]);
                if ($this.attr("id").indexOf('chkNewClient') > -1)
                    $("#" + CheckIdsCheck[i]).click();
                else
                    $this.prop("checked", true);
            }
        }

        var fieldSetDisabled = "<fieldset style='border:none;padding: 0; margin: 0;' disabled></fieldset>";

        var CurrentTab = $("#hdnCurrentTab").val();
        ChangeTab(CurrentTab);

        var currentTabMenuSup = $("#hdnCurrentTabSup").val();
        ChangeTabMenuSup(currentTabMenuSup);

        var currentTabCasesNotSubmitted = $("#hdnCurrentTabCasesNoSubmitted").val();
        ChangeTabCasesNotSubmitted(currentTabCasesNotSubmitted);

        CurrentTabMenuTabHistoricalCases = $("#hdnCurrentTabHistoricalCases").val();
        ChangeTabMenuTabHistoricalCases(CurrentTabMenuTabHistoricalCases);

        var CurrentTabView = $("#hdnCurrentTabView").val();

        ChangeTabView(CurrentTabView);

        if (CurrentTabView == "lnkClientInfo" || CurrentTabView == "lnkOwnerInfo") {
            var isCompany = $("#hdnIsCompanyOwnerView").val() == "true";

            $("#frmHomeAddress").css("display", (CurrentTabView == "lnkOwnerInfo" && isCompany) ? "none" : "block");

            if (isCompany)
                $("#dvOwnerInformation").fadeIn(500);

            $("#dvrepeaterClientIsA").find("input:checkbox").each(function () {
                if ($(this)[0].checked) {
                    checkBoxes($(this)[0]);
                }
            });

            $("#dvrepeaterHasACloseRelationShipWithA").find("input:checkbox").each(function () {
                if ($(this)[0].checked) {
                    checkBoxes($(this)[0]);
                }
            });
        }

        var hdnCurrentTabHistoricalCases = $("#hdnCurrentTabHistoricalCases").val();

        var HideOrShow = (CurrentTab == "btnOwnerInfo") ? "block" : "none";

        $("#OnlyOwnerInfo").css("display", HideOrShow);            

        if (CurrentTab == "btnClientInfo" || CurrentTab == "btnOwnerInfo") {

            $("#frmHomeAddress").css("display", (CurrentTab == "btnOwnerInfo" && $("#hdnIsCompanyOwner").val() == "true") ? "none" : "block");

            if ($("#hdnIsCompanyOwner").val() == "true")
                $("#dvOwnerInformation").fadeIn(500);

            $("#dvrepeaterClientIsA").find("input:checkbox").each(function () {
                if ($(this)[0].checked) {
                    checkBoxes($(this)[0]);
                }
            });

            $("#dvrepeaterHasACloseRelationShipWithA").find("input:checkbox").each(function () {
                if ($(this)[0].checked) {
                    checkBoxes($(this)[0]);
                }
            });
        } else if (CurrentTab == "btnQuestionaries" || CurrentTabView == "lnkHealthDeclaration") {

            $("body").find("[ToolTipDR]").each(function () {

                $(this).mouseover(function () {
                    var $this = $(this);
                    var msj = $this.val();
                    if (msj != '') {
                        $this.jqxTooltip({ opacity: 1 });
                        CustomToolTips($this, msj, 'top', 3000);
                    }
                });
            });

            $(".barra_sub_menu").hide();

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

            var selector = "[hid='5'],[hid='6'],[hid='8'],[hid='11'],[hid='12'],[hid='13'],[hid='14'],[hid='15'],[hid='16'],[hid='19'],[hid='21'],[hid='22'],[hid='23'],[hid='24'],[hid='25']";

            $("body").find(selector).find("input[class*='datepicker']").attr("alt", "validateFutureDate");


            var sel = "[hid='16'],[hid='8'],[hid='15'],[hid='22'],[hid='24'],[hid='25']";

            $("body").find(sel).find("label").each(function () {
                var $this = $(this);
                if ($this.text().indexOf('Telephone') != -1 || $this.text().indexOf('phone') != -1 || $this.text() == "Telefono" || $this.text() == "Teléfono") {
                    $this.next().attr("data-inputmask", "'alias': 'integer','rightAlign': false,'clearMaskOnLostFocus': true,'allowMinus': false, 'allowPlus': false");
                    $this.next().attr("maxlength", "16");
                }
            });

            var hdnisFuneral = $("#hfisFuneral");

            var isFuneral = (hdnisFuneral.val() == "Luminis" ||
                            hdnisFuneral.val() == "LuminisVIP" ||
                            hdnisFuneral.val() == "Exequium" ||
                            hdnisFuneral.val() == "ExequiumVIP") && hdnisFuneral.val() != "";

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
                } else
                    $(textA1.parent()).hide();


                if ($(pCheckBoxes[2]).prop("checked")) {
                    textA2.show();
                    if (!$(pCheckBoxes[0]).prop("checked"))
                        $($(pCheckBoxes[0]).parents()[3]).removeAttr("validatecheckboxlist");

                    if (!$(pCheckBoxes[1]).prop("checked"))
                        $($(pCheckBoxes[1]).parents()[3]).removeAttr("validatecheckboxlist");
                } else
                    $(textA2.parent()).hide();

                var intervalo = setInterval("fixheight();", 100);
                setTimeout(function () { clearInterval(intervalo) }, 400);

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

                                if (cb1) {
                                    ParentChk1.removeAttr("validatecheckboxlist");
                                }
                                if (cb2) {
                                    ParentChk2.removeAttr("validatecheckboxlist");
                                }

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

                                if (cb0) {
                                    ParentChk0.removeAttr("validatecheckboxlist");
                                }
                                if (cb1) {
                                    ParentChk1.removeAttr("validatecheckboxlist");
                                }

                                if (cb0 && cb1) {
                                    ParentChk0.attr("validatecheckboxlist", "validatecheckboxlist");
                                    ParentChk1.attr("validatecheckboxlist", "validatecheckboxlist");
                                    ParentChk2.attr("validatecheckboxlist", "validatecheckboxlist");
                                }
                            }
                        }

                        var intervalo = setInterval("fixheight();", 100);
                        setTimeout(function () { clearInterval(intervalo) }, 400);
                    });
                });

                //==============================================================================================
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

            var intervalo = setInterval("fixheight();", 100);
            setTimeout(function () { clearInterval(intervalo) }, 400);

            Configutations();

        } else if (CurrentTab == "btnRequirements") {

        } else if (CurrentTab == "btnPayment" || CurrentTabView == "lnkPayment") {

            var trs = $("#bodyContent_DReviewContainer_PaymentContainer_WUCPayment_gvPayment").find("table:first").find("tr");

            for (var x = 1; x < trs.length; x++)
                $(trs[x]).css("text-align", "right");

            $("#tblUpload").hide();
            var udpFormOfPayment = $("#udpFormOfPayment");
            var $infoPayment = udpFormOfPayment.find('.fondo_blanco');
            var $infoProPay = $("#dvFPayment");

            udpFormOfPayment.wrap(fieldSetDisabled);

            var objcss = { "min-height": "585px", "max-height": "693px" };

            if (!isView) {
                $infoPayment.removeClass("fix_height");
                $infoProPay.removeClass("fix_height");
            } else {
                $("#dvPaymentInformation")
                    .find('.fondo_blanco')
                    .addClass("fix_height")
                    .css(objcss);

                $infoPayment
                    .addClass("fix_height")
                    .css(objcss);

                $infoProPay
                    .addClass("fix_height")
                    .css(objcss);
            }

            $("input[type='submit']").each(function () {
                var $this = $(this);
                if ($this.attr("alt") != null) {
                    $this.mouseover(function () {
                        ShowToolTips($(this), 'top', true, null);
                    });
                }
            });

            setABAautoComplete();

            $("#divSave").prepend($("#tblUpload"));

            var el = $(".dxgvFooter_DevEx");
            var html = el.find("td").eq(2).text($("#hdntotalPay").val());
            el.find("td").eq(0).html("<span style='font-weight:bold'>Total<span/>");

            if ($("#gvFormOfPayment").find("[class='view_file']").length > 0)
                $($("#gvFormOfPayment").find("table")[3]).find("td:first").text("View");

            $("#divSave").remove();

            $("#dvPayments").fadeIn(500);

        } else if (CurrentTab == "btnPlanPolicy" || CurrentTabView == "lnkPlanPolicy") {
            $(".barra_sub_menu").hide();

            var frmAdditionalsIndured = $("#frmAdditionalsIndured");
            if (frmAdditionalsIndured.hasClass("aspNetDisabled"))
                frmAdditionalsIndured.addClass("grupos de_4");

            if (!isView)
                $("#DReviewConainerBI").next().removeClass("col-1-2").addClass("clearfix");

            frmAdditionalsIndured.find(".datepicker").each(function () {
                var $this = $(this);
                $this.datepicker("option", "disabled", $this.hasClass("aspNetDisabled"));
            });

            $("#ddlInvestmentProfile").change(function () {
                BeginRequestHandler();
            });

            $("#dvPlanPolicyContainer").fadeIn(0);

            if (!$("#btnPprofile").prop("disabled")) {
                $("#btnPprofile").removeClass("buttonDisabled");
            }

            $("#btnPprofile").mouseover(function () {
                var $this = $(this);
                CustomToolTips($this, $this.attr("alt"), "top", 3000);
            });

            $("#titulos_azules,#titulos_azulesAddress").hide();
            $("#udpPhoneNumber div:first > div,#udpEmailAddress div:first > div").css("border", "none");

            //Calculo del Itbis
            GetAnnualPremium('txtPeriodicPremium', 'ddlFrequencyofPayment');
            $("#txtAnnualPremium").attr("disabled", "true");
            $("#txtSelectiveTax").attr("disabled", "true");
            $("#txtAnnualPremiumWithTax").attr("disabled", "true");

            var QuestionHaveDesignatedPensioner = $("#QuestionHaveDesignatedPensioner");
            if (QuestionHaveDesignatedPensioner != null) {

                var hdnIsrefreshDesignatedPensioner = ($("#hdnIsrefreshDesignatedPensioner").val() == "true");
                if (hdnIsrefreshDesignatedPensioner) {
                    var RadioSi = QuestionHaveDesignatedPensioner.find("input[id*='si']");
                    var RadioNo = QuestionHaveDesignatedPensioner.find("input[id*='no']");
                    if (RadioNo.prop("checked")) {
                        RadioSi.click();
                        RadioNo.click();
                        $("#hdnIsrefreshDesignatedPensioner").val("false");
                    }
                }

                if (isView)
                    $("#QuestionHaveDesignatedPensioner").wrap(fieldSetDisabled);

                if ($("#frmPhoneNumbers").css("display") == "none")
                    $("#frmPhoneNumbers").fadeIn(500);

                if ($("#fEmailAddress").css("display") == "none")
                    $("#fEmailAddress").fadeIn(500);

                if ($("#dvAdditionalsInsured").css("display") == "none")
                    $("#dvAdditionalsInsured").fadeIn(100);

                //var ddlProductName = $("body").find("select[name*='ddlProductName']");
                //var valueKey = JSON.parse(ddlProductName.val());
                //var isFuneral = valueKey.NameKey == "Luminis" ||
                //                valueKey.NameKey == "LuminisVIP" ||
                //                valueKey.NameKey == "Exequium" ||
                //                valueKey.NameKey == "ExequiumVIP";


                if (isFuneral) {
                    if ($("#isRefesh").val() == "true") {
                        $("#btnRefreshView").click();
                    }
                }
            }
        } else if (CurrentTab == "btnBeneficiaries" || CurrentTabView == "lnkBeneficiaries") {
            $("#bodyContent_DReviewContainer_BeneficiariesContainer_fuMainBenediciarieFile_TextBox0_FakeInput").css("display", "none");
            $("#bodyContent_DReviewContainer_BeneficiariesContainer_fuMainBenediciarieFile_TextBox0").css("width", "0");
            $("#bodyContent_DReviewContainer_BeneficiariesContainer_fuMainBenediciarieFile_TextBox0").css("padding", "0");
            setCurrentAccordeonForIndex("#hfMenuAccordeonBeneficiaries");
            if (hdnRefreshBeneficiariesTab.val() == "false")
                $(".pnBeneficiaries").fadeIn();
        }

        var checkMark = "<div> <input type='checkbox' id='chkAll'/> </div>";
        var GridsSelectors = "#gvDataReview,#gvApprovedCases,#gvRejectedCases";

        var td = $(GridsSelectors).find("tr[id*='DXHeadersRow']").find("td:first");
        td.html("");
        td.append(checkMark);

        setClickCheckBoxGridView(GridsSelectors, 'chkAll');

        $(GridsSelectors).find("input[id='chkAll']").click(function () {
            SelectAll(this, GridsSelectors);
        });

        //Esta configuracion solo aplica para cuando se esta en modo solo lectura
        if (!isView) { 
            $("#frmPersonalInformation,#frmAddress,#frmPhoneNumbers,#frmEmailAddress,#frmIdentifications,#frmPersonalInformationLegal,#frmAddress_Legal,#frmEmailAddress_Legal,#frmIdentifications_Legal,#frmPhoneNumbersLegal").removeClass('col-1-3');
            $("#frmPlan").parent().parent().removeClass('col-1-4-c').addClass("col-1-1"); 
            $("#pnDesignatedPensionerOrAddicionalInsured").parent().removeClass('col-3-4-c');
            $('#frmBackGroundInformation_Legal').parent().removeClass('col-1-3');
            $("#frmBackGroundInformation").parent().removeClass('col-1-3');
            $("#udpUpdate").find("[class='col-1-2']").removeClass('col-1-2');
            $("#dvPayments").find("[class='col-1-3']").css("margin-bottom", "10px").removeClass('col-1-3');
            $("#bodyContent_DReviewContainer_BeneficiariesContainer_WUCMainInsured_pnBeneficiaries,#pnBeneficiaries").find("div[class=col-1-2]").removeClass('col-1-2');
            $("#DReviewConainerBI").append($("#BackgroundInformationFieldSet"));
            $("#DReviewConainerBI").append($(".tbDesignatedPensionerInfo"));
            $("#pnDesignatedPensionerOrAddicionalInsured").find("div[class='col-1-2']").removeClass("col-1-2");
            $("#dvPaymentInformation").hide();
        } else if (isView && CurrentTabView == "lnkPlanPolicy") {
            $("#frmPhoneNumbers,#frmEmailAddress").removeClass('col-1-3');
            $(".BGIDesignatedPensioner").wrap(fieldSetDisabled);
            $("#dvHomeaddress").wrap(fieldSetDisabled);
            $("#dvBusinessAddress").wrap(fieldSetDisabled);
            $("#frmIDOccupation").wrap(fieldSetDisabled);
        } else if (isView && CurrentTabView == "lnkHealthDeclaration") {
            //Envolver las preguntas en un fieldset deshabilitado
            $(".pregunta").wrap(fieldSetDisabled);
        } else if (isView && CurrentTabView == "lnkBeneficiaries") {
            $("#udpBeneficiariesContainer").wrap(fieldSetDisabled);
            $(".edit_file").click(function () { return false; })
        }

        $(".dxucBrowseButton_DevEx").append("<span class='upload'></span>");

        $("#dvCopyHomeAddress").hide();
        $("#dvClientOrOwnerInfo").fadeIn(500);
        $("#frmPhoneNumbers").show();

        var $geSpans = $('.checkInToSpan');
        var $geChecks = $geSpans.find('input[type="checkbox"]');

        $geChecks.addClass('refresh_height');

        $geChecks.on('change', function () {
            checkBoxes(this);
        });

        if ($("#tb_WUC_PI_DateBirth").val() != "") {
            $("#tb_WUC_PI_Age").val($("#hdnAge").val());
            $("#txtAge").val($("#hdnAge").val());
        }


        if ($("#hdnShowPopClientInfoSearch").val() == "true") {
            var SearchClientOrOwnerpop = popSearchClientOrOwner.GetWindowByName('ClientOwnerSearch');
            popSearchClientOrOwner.ShowWindow(SearchClientOrOwnerpop);

            if (CurrentTab == "btnOwnerInfo")
                SearchClientOrOwnerpop.SetHeaderText(lang == "en" ? 'OWNER SEARCH' : 'Búsqueda del Propietario');
            else
                if (CurrentTab == "btnClientInfo")
                    SearchClientOrOwnerpop.SetHeaderText(lang == "en" ? 'CLIENT SEARCH' : 'Búsqueda del Cliente');
                else
                    if (CurrentTab == "btnPlanPolicy" || CurrentTab == "btnBeneficiaries")
                        SearchClientOrOwnerpop.SetHeaderText(lang == "en" ? 'Contact Search' : 'Búsqueda del Contacto');


            $(".dxWeb_pcCloseButton").css("display", "none");
            $(".dxpc-content").css("padding", "0");
        }
        else
            $(".dxWeb_pcCloseButton,.dxWeb_pcCloseButton_DevEx").click();

        if ($("#hdnPopAnswerPadecimiento").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvGridViewPopup",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "ADD CONDITION" : "AÑADIR CONDICIÓN",
                pmodal: true,
                presizable: true,
                OnCLose: function () { $("#hdnPopAnswerPadecimiento").val("false"); }
            });

            fixheight();
        }

        if ($("#hdnShowPopComment").val() != undefined) {
            if ($("#hdnShowPopComment").val().split('|')[0] == "true") {
                if ($("#hdnShowPopComment").val().split('|').length > 1)
                    if ($("#hdnShowPopComment").val().split('|')[1] == 'ReadOnly')
                        $('#dvPopAddComment').find('.amarillo').css('display', 'none');
                    else
                        $('#dvPopAddComment').find('.amarillo').css('display', 'block');

                JQueryPopup({
                    ElementIDorClass: "#dvPopAddComment",
                    pautoOpen: true,
                    pShowTitleBar: true,
                    pTitle: lang == "en" ? "COMMENTS" : "COMENTARIOS",
                    pmodal: true,
                    presizable: false,
                    OnCLose: function () { $("#hdnShowPopComment").val("false"); }
                });

                fixheight();
            }
        }

        if ($("#hdnShowPopViewPDF").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvPopViewPDF",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "VIEW PDF DOCUMENT" : "VISTA DEL DOCUMENTO PDF",
                pmodal: false,
                pwidth: 900,
                presizable: false,
                OnCLose: function () { $("#hdnShowPopViewPDF").val("false"); }
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
                pTitle: lang == "en" ? "PERSONALIZED INVESTMENT PROFILE" : "PERFIL DE INVERSIÓN PERSONALIZADA",
                pmodal: true,
                presizable: true,
                OnCLose: function () { $("#hdnShowPopPersonalizeInvstProf").val("false"); }
            });
        }

        if ($("#hdnShowAddNewNote").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvAddNote",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "ADD NEW NOTE" : "AÑADIR NUEVO NOTA",
                pmodal: true,
                presizable: false,
                OnCLose: function () { $("#hdnShowAddNewNote").val("false"); }
            });

            fixheight();
        }

        if ($("#hdnShowAddNewNoteRejectedCases").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvAddNoteComentsHistoricalCases",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "COMMENTS" : "COMENTARIOS",
                pmodal: true,
                presizable: false,
                OnCLose: function () { $("#hdnShowAddNewNoteRejectedCases").val("false"); }
            });
        }

        if ($("#hdnShowAddNewNoteApprovedCases").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvAddNoteComentsHistoricalCases",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "COMMENTS" : "COMENTARIOS",
                pmodal: true,
                presizable: false,
                OnCLose: function () { $("#hdnShowAddNewNoteApprovedCases").val("false"); }
            });
        }

        if ($("#hdnShowPopReject").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvCommentReject",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "REJECT CASE TO READY TO REVIEW" : "DEVOLVER CASO A LISTO PARA REVISAR",
                pmodal: true,
                presizable: false,
                OnCLose: function () { $("#hdnShowPopReject").val("false"); }
            });
        }

        if ($("#hdnShowAddFollowUpComment").val() == "true") {
            JQueryPopup({
                ElementIDorClass: "#dvAddFollowUpComment",
                pautoOpen: true,
                pShowTitleBar: true,
                pTitle: lang == "en" ? "ADD FOLLOW UP COMMENT" : "AGREGAR COMENTARIO DE SEGUIMIENTO",
                pmodal: true,
                presizable: false,
                OnCLose: function () { $("#hdnShowAddFollowUpComment").val("false"); }
            });
        }

        $("#dvScroll").scroll(function () {
            var ScrollTop = $(this).scrollTop();
            $("#hdnRememberScroll").val(ScrollTop);
        });

        $("#tb_WUC_PI_DateBirth").datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: '-3m',
            minDate: '-65y',
            defaultDate: '-3m',
            yearRange: "c-100:c+100",
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
        }).each(function () {
            $(this).inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy", "clearIncomplete": true });

            if (this.getAttribute("maxDate") != "" && this.getAttribute("maxDate") != null) {
                var maxDate = this.getAttribute("maxDate");
                $(this).datepicker("option", "maxDate", maxDate);
                $(this).datepicker("option", "defaultDate", maxDate);
            }
        });

        $(".RTRdatepickerFrom").datepicker({
            defaultDate: $("#hdnGetDate").val(),
            changeMonth: true,
            changeYear: true,
            maxDate: new Date(),
            yearRange: "c-100:c+100",
            onSelect: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            },
            onClose: function (selectedDate) {
                $(".RTRdatepickerTo").datepicker("option", "minDate", selectedDate);
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            }, beforeShow: function () {
                SavePosDatePicker(this);
            }
        }).inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy", "clearIncomplete": true });

        $(".RTRdatepickerTo").datepicker({
            defaultDate: $("#hdnGetDate").val(),
            changeMonth: true,
            changeYear: true,
            yearRange: "c-100:c+100",
            onSelect: function (selectedDate) {
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            },
            onClose: function (selectedDate) {
                var today = new Date();
                var sDate = new Date(selectedDate);
                $(".RTRdatepickerFrom").datepicker("option", "maxDate", selectedDate == "" ? today : (sDate > today ? today : sDate));
                changeBorderColor(this);
                CallExecuteOnCloseEvent(this, selectedDate);
            }, beforeShow: function () {
                SavePosDatePicker(this);
            }
        }).inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy", "clearIncomplete": true });

        $("#dvScroll").scrollTop($("#hdnRememberScroll").val());

        $(".gvResult > thead").addClass("headgrid");
        $(".gvResult > thead").addClass("azules");
        $(".gvResult > tbody").addClass("datagrid");

        $("#frmIdentifications").find('select,input').removeAttr("onkeydown");
        $("#frmPhoneNumbers").find('select,input').removeAttr("onkeydown");
        $("#frmEmailAddress").find('select,input').removeAttr("onkeydown");
        $(".pnBeneficiaries").fadeIn();
        Configutations();
        setOccupationTypeAutoComplete();
        setOccupationAutoComplete();
        setPositionnAutoComplete();
        SetDatePicker(); 
        setAccordeaons();

        //Esto solo aplica para Client Info y Owner Info
        $("#dvClientOrOwnerInfo").find(".fix_height").removeClass("fix_height").removeAttr("style");
        $("#dvClientOrOwnerInfo").find(".fix_height1").removeClass("fix_height1").removeAttr("style");

        var objLi = $("#ClientInfo > li");
        objLi.each(function () {
            var $this = $(this);
            var Title = $this.find("div.titulos_azules");
            var ClassSpan = Title.find("span:first").attr("class");
            var Lnk = $this.find("a:first");
            Lnk.append(Title);
        });
        setCurrentAccordeonForIndex("#hfMenuAccordeonClientInfo");
        //===============================================================================================
        fixheight();
        divScroll();

        if (CurrentTab != "btnPlanPolicy")
            $(".datepicker,.datepicker_03").datepicker("option", "disabled", isView);

        $("body").find("[id*='ddlCheckComboBox_B']").removeClass("datepicker_02");

        $("#bodyContent_DReviewContainer_WUCAddNewNotePopup_ddlCheckComboBox_I").attr("validation", "Required");

        $("#txtExpDate").datepicker("option", "minDate", "+65d");

        $("#bodyContent_ContactsInfoContainer_WUCIdentification_cbxIDType").change(function () {
            var $this = $(this);
            if ($this.val() == "6") {
                $("#txtExpDate").removeAttr("style");
                $("#txtExpDate").removeAttr("validation");
            }
            else
                $("#txtExpDate").attr("validation", "Required");

        });


        if (CurrentTab == "btnBeneficiaries" || CurrentTabView == "lnkBeneficiaries") {
            if (hdnRefreshBeneficiariesTab.val() == "true" || $("#hdnPendingRefreshBeneficiariesTab").val() == "true") {
                hdnRefreshBeneficiariesTab.val("false");
                $("#hdnPendingRefreshBeneficiariesTab").val("false");
                $("#btnRefresh").click();
            }
        }


    } catch (e) {
        console.log(e.message);
        //CustomDialogMessageEx("Error in js:" + e.message, null, null, true, "Error");
    }
}

SetCheckGrid = function () {
    var checkeados = [];
    $("#CheckIds").val("");

    $("#gridMaster").find("input:checkbox:checked").each(function () {
        checkeados.push($(this).attr("id"));
    });

    $("#CheckIds").val(checkeados.join(","));
}

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

function HidePopup() {
    $find("popupBhvr2").hide();
    $("#CheckIds").val("");
    $('#hdnSelectedContact').val("");
}

function ClosePaymentPDFPop() {
    $find('popupBhvr').hide();
    return false;
}

CloseSearchClientOrOwner = function () {
    var SearchClientOrOwnerpop = popSearchClientOrOwner.GetWindowByName('ClientOwnerSearch');
    popSearchClientOrOwner.ShowWindow(SearchClientOrOwnerpop);
    $("#hdnShowPopClientInfoSearch").val("false");
    popSearchClientOrOwner.Hide();
};

verifyChecksHeader = function () {

    var result = true;
    $("#gridMaster").find("tr[id*='gridMaster_DXDataRow']").each(function () {
        var $this = $(this);
        var Tds = $this.find("td");
        var chkNewClient = Tds.eq(9).find("input:checkbox");
        var chkSameAsSelected = Tds.eq(10).find("input:checkbox");

        if (!chkNewClient.prop("checked") && !chkSameAsSelected.prop("checked")) {
            result = false;
            return false;
        }
    });

    return result;
};

validateSubmitToUnderWriting = function (sender) {

    var SelectedContact = [];

    var result = verifyChecksHeader();

    if (result) {
        //Ahora recorremos el grid para ir buscando los conctacos seleccionados
        $("#gridMaster").find("tr[id*='gridMaster_DXDataRow']").each(function () {
            var CaseContact = {};
            var $this = $(this);
            var Tds = $this.find("td");
            var chkNewClient = Tds.eq(9).find("input:checkbox");
            var chkSameAsSelected = Tds.eq(10).find("input:checkbox");

            if (!chkNewClient.prop("checked")) {
                var NextTr = $this.next();
                var isNext = NextTr.find("td:first").hasClass("dxgvIndentCell");
                if (isNext) {

                    var chk = NextTr.find("input:checkbox:checked");

                    var chequeados = chk.length > 0;

                    if (chequeados == 0) {
                        result = false;
                        return false;
                    }
                    else {
                        CaseContact.currentContact = chkSameAsSelected.parent().attr("data");
                        CaseContact.NewContact = chk.parent().attr("data");
                        SelectedContact.push(CaseContact);
                    }
                } else {
                    result = false;
                    return false;
                }
            } else {
                CaseContact.currentContact = chkNewClient.parent().attr("data");
                CaseContact.NewContact = null;
                SelectedContact.push(CaseContact);
            }
        });
    }


    if (!result) {
        CustomDialogMessageEx(null, 350, 150, true, lang == "en" ? "Warning" : "Advertencia", 'AllRowsCheckValidation');
        return false;
    } else {
        $("#hdnSelectedContact").val(JSON.stringify(SelectedContact));

        result = DlgConfirm(sender, null, 350, 150);
    }

    return result;
};

ConfirmPrintList = function (grid) {

    var TotalCheck = CountCheck('#' + grid);

    if (TotalCheck == 0) {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Warning" : "Advertencia", "gridSelectionValidation");
        return false;
    }
};

CloseNotesCommentPop = function () {
    $($("#upNoteComments").parent()).dialog("close");
};

function CancelNotesCommentPop() {
    $('#txtNewComment').val("");
    $('#pnComment').toggle();
    RelocatePops();
    return false;
}

ValidateCommentNote = function () {
    if ($.trim($('#txtNewComment').val()) == "") {
        CustomDialogMessageEx(null, 350, 150, true, lang == "en" ? "Warning" : "Advertencia", 'CommentMessage');
        return false;
    }
};

function upCOFileStartUpload(sender, event) {
    var clase = sender.mainElement.attributes.class.value.split(" ")[1];
    BEDisableInputsFile(sender, true, clase, true);
}

function upFileStartUpload(sender, event) {
    var clase = sender.mainElement.attributes.class.value.split(" ")[1];
    BEDisableInputsFile(sender, true, clase, false);
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

BEDisableInputsFile = function (sender, val, className, isCompany) {
    var AddButton = GetElementByClassAndId((!isCompany ? 'btnAdd' : 'btnBECompanyAdd'), className, 'input[type="submit"]');
    var ClearButton = GetElementByClassAndId((!isCompany ? 'btnBEClear' : 'btnBECompanyClear'), className, 'input[type="submit"]');

    $(AddButton).prop('disabled', val);
    $(ClearButton).prop('disabled', val);
    $(sender).prop('disabled', val);
    $(sender).prop('enabled', !val);
};

function uploadFileContainerComplete(sender, event) {
    var obj = JSON.parse(event.callbackData);
    if (obj.file == "")
        showAlert(obj.error);
    else {

        var hdnUploadedPDFPath = $('#hdnUploadedPDFPath');

        if (hdnUploadedPDFPath != null) {
            var FileName = obj.file.split("~~")[1];
            hdnUploadedPDFPath.val(obj.file);
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

function uploadFileRequirementComplete(sender, event) {

    if (event != '') {

        $('#txtPath').val(event.callbackData.split('~~')[1]);
        if (event.callbackData != '') {
            $('#btnRequirementPreviewPDF').trigger('click');
            sender.ClearText();
        }

    }
}

function uploadFileChange(sender, event) {
    sender.Upload();
}

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
        }

    var intervalo = setInterval("fixheight();", 100);
    setTimeout(function () { clearInterval(intervalo) }, 400);
};

changeTabHealth = function () {
    var Tab = $("#hdnCurrentTabHealth").val();
    //Limpiar
    $("#MenuTabsH li").removeClass("active");

    vTab = $('#' + Tab);

    $(vTab).parent().addClass("active");
};

validacionesTab = function (sender) {

    var CurrentTab = $("#hdnCurrentTab").val();

    var result = true;

    //**** Validaciones para la compañia *****// 
    if (CurrentTab == "btnOwnerInfo") {
        if ($("#chkIsCompany").length > 0 && $("#chkIsCompany")[0].checked) {
            result = (validateForm("frmCompany"));

            if (result) {
                if ($("#tb_WCU_A_BusinessAddress").val().trim() != "" ||
                     ($("#ddl_WUC_A_BusinessCountry").val() != null && $("#ddl_WUC_A_BusinessCountry").val() != "-1") ||
                     ($("#ddl_WUC_A_BusinessState").val() != null && $("#ddl_WUC_A_BusinessState").val() != "-1") ||
                     ($("#ddl_WUC_A_BusinessCity").val() != null && $("#ddl_WUC_A_BusinessCity").val() != "-1")
                 ) {
                    //validar Business Address
                    result = validateForm("frmbusinessAddress");
                }
            }
        } else {
            //Validar el formulario de Personal Information
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
                var TxtBox = $(this).parent().parent().find("input[type='text']:first");

                if (Chk.checked && $.trim(TxtBox.val()) == "") {
                    var Field = TxtBox.parent().find("label:first");
                    Field.css("color", "red");
                    oMessage = {};
                    oMessage.ErrorType = "Required";
                    oMessage.Field = Field.html();
                    ArrayMessage.push(oMessage);
                    TxtBox.focus();
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
                        oMessage = {};
                        oMessage.ErrorType = "Required";
                        oMessage.Field = Field.html();
                        ArrayMessage.push(oMessage);
                        TxtBox1.focus();
                    } else
                        if ($.trim(TxtBox2.val()) == "" && $.trim(TxtBox1.val()) != "") {
                            var Field = TxtBox2.parent().find("label:first");
                            Field.css("color", "red");
                            oMessage = {};
                            oMessage.ErrorType = "Required";
                            oMessage.Field = Field.html();
                            ArrayMessage.push(oMessage);
                            TxtBox2.focus();
                        }
                        else {
                            var Field1 = TxtBox1.parent().find("label:first");
                            var Field2 = TxtBox2.parent().find("label:first");
                            Field1.css("color", "red");
                            Field2.css("color", "red");
                            oMessage = {};
                            oMessage.ErrorType = "Required";
                            oMessage.Field = Field1.html();
                            ArrayMessage.push(oMessage);

                            oMessage = {};
                            oMessage.ErrorType = "Required";
                            oMessage.Field = Field2.html();
                            ArrayMessage.push(oMessage);
                            TxtBox1.focus();
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
                    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "AddressValidation");
                    EndRequestHandler();
                    result = false;
                }
            }

            //====== Validaciones de Address ====================================================================//

            if (result) {

                var TotalPhone = parseInt($("#hdnTotalPhones").val());
                if (TotalPhone <= 0) {
                    EndRequestHandler();
                    var Entity = CurrentTab != "btnOwnerInfo" ? "PhoneNumberInsuredValidation" : "PhoneNumberOwnerValidation";
                    result = false;
                    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                }
            }

            if (result) {

                if ($("#STFCUserProfile1_drpCompanyProfile option:selected").val() != "2") {
                    var TotalEmnail = parseInt($("#hdnTotalEmail").val());

                    if (TotalEmnail <= 0) {
                        EndRequestHandler();
                        var Entity = CurrentTab != "btnOwnerInfo" ? "EmailInsuredValidation" : "EmailOwnerValidation";
                        result = false;
                        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                    }
                }
            }

            if (result) {

                var TotalID = parseInt($("#hdnTotalIdentification").val());
                if (TotalID <= 0) {
                    EndRequestHandler();
                    var Entity = CurrentTab != "btnOwnerInfo" ? "IdentificationInsuredValidation" : "IdentificationOwnerValidation";
                    result = false;
                    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                }
            }
        }

    } else if (CurrentTab == "btnPlanPolicy") {

        if ($("#hdnValidateFormDesignatedPensionerOrAddicionalInsured").val() == "true") {
            result = (validateForm("frmDesignatedPensioner"));

            if (result)
                result = (validateForm("frmIDOccupation"));

        }

        if (result)
            result = (validateForm("frmPlan"));

        if (result)
            result = validateForm("pnFooter");
        EndRequestHandler();

    }
    else if (CurrentTab == "btnQuestionaries") {
        result = validateQuestionarie();
        EndRequestHandler();

    } //**** End Validaciones para la compañia *****// 
    else if (CurrentTab == "btnClientInfo" || CurrentTab == "btnOwnerInfo") {
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
                var Entity = CurrentTab != "btnOwnerInfo" ? "PhoneNumberInsuredValidation" : "PhoneNumberOwnerValidation";
                result = false;
                CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
            }
        }

        if (result) {

            if ($("#STFCUserProfile1_drpCompanyProfile option:selected").val() != "2") {
                var TotalEmnail = parseInt($("#hdnTotalEmail").val());

                if (TotalEmnail <= 0) {
                    EndRequestHandler();
                    var Entity = CurrentTab != "btnOwnerInfo" ? "EmailInsuredValidation" : "EmailOwnerValidation";
                    result = false;
                    CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", Entity);
                }
            }
        }

        if (result) {

            var TotalID = parseInt($("#hdnTotalIdentification").val());
            if (TotalID <= 0) {
                EndRequestHandler();
                var Entity = CurrentTab != "btnOwnerInfo" ? "IdentificationInsuredValidation" : "IdentificationOwnerValidation";
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
            var TxtBox = $(this).parent().parent().find("input[type='text']:first");

            if (Chk.checked && $.trim(TxtBox.val()) == "") {
                var Field = TxtBox.parent().find("label:first");
                Field.css("color", "red");
                oMessage = {};
                oMessage.ErrorType = "Required";
                oMessage.Field = Field.html();
                ArrayMessage.push(oMessage);
                TxtBox.focus();
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
                    oMessage = {};
                    oMessage.ErrorType = "Required";
                    oMessage.Field = Field.html();
                    ArrayMessage.push(oMessage);
                    TxtBox1.focus();
                } else
                    if ($.trim(TxtBox2.val()) == "" && $.trim(TxtBox1.val()) != "") {
                        var Field = TxtBox2.parent().find("label:first");
                        Field.css("color", "red");
                        oMessage = {};
                        oMessage.ErrorType = "Required";
                        oMessage.Field = Field.html();
                        ArrayMessage.push(oMessage);
                        TxtBox2.focus();
                    }
                    else {
                        var Field1 = TxtBox1.parent().find("label:first");
                        var Field2 = TxtBox2.parent().find("label:first");
                        Field1.css("color", "red");
                        Field2.css("color", "red");
                        oMessage = {};
                        oMessage.ErrorType = "Required";
                        oMessage.Field = Field1.html();
                        ArrayMessage.push(oMessage);

                        oMessage = {};
                        oMessage.ErrorType = "Required";
                        oMessage.Field = Field2.html();
                        ArrayMessage.push(oMessage);
                        TxtBox1.focus();
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
            var idyes = 'bodyContent_DReviewContainer_WUCBackgroundInformation_repeaterQuestion_rbYes_' + i;
            var idno = 'bodyContent_DReviewContainer_WUCBackgroundInformation_repeaterQuestion_rbNo_' + i;

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
                var Entity = CurrentTab != "btnOwnerInfo" ? "AddressInsuredValidation" : "AddressOwnerValidation";
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

    return result;
};

ChangeTabMenuSup = function (Tab) {

    //Limpiar
    $("#menu li a").removeClass("active");

    vTab = $('#' + Tab);

    vTab.addClass("active");
};

ChangeTabMenuTabHistoricalCases = function (Tab) {
    //Limpiar
    $("#MenuTabHistoricalCases li").removeClass("active");

    vTab = $('#' + Tab);

    vTab.parent().addClass("active");
};

ChangeTabCasesNotSubmitted = function (Tab) {
    //Limpiar
    $("#menuTabsCasesNotSubmitted li").removeClass("active");

    vTab = $('#' + Tab);

    vTab.parent().addClass("active");
};

ChangeTabView = function (Tab) {
    //Limpiar
    $("#MenuTabsView li").removeClass("active");

    vTab = $('#' + Tab);

    vTab.parent().addClass("active");
};

ChangeTab = function (Tab) {

    //Limpiar
    $("#ButtonTabsContainer input:submit").parent().removeClass("bule_on");

    vTab = $('#' + Tab);

    vTab.parent().addClass("bule_on");
};

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

    //if ($('#txtTo').val() != null) {

    //    var FechaFrom =

    //    var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();

    //    var FechaSeleccionada = selectedDate.split("/");

    //    var FechaFinal = FechaSeleccionada[1] + "/" + FechaSeleccionada[0] + "/" + FechaSeleccionada[2];

    //    var Days = pDate.DayDiff(FechaFinal, 'mm/dd/yyyy');

    //    if (Days < 61) {
    //        $(sender).val("");
    //        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "DocExpirationDate");
    //    }

    //    return resultado;
    //} else {

        return resultado;
    //} 

};

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

    $($(sender).parent().find("label")[1]).css("color", "#111");
    $($(sender).parent().find("label")[2]).css("color", "#111");
};

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

//para validad las fechas de cumpleanos
function validateDateOfBirth(selectedDate) {

    var fecha = new Date();
    var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();
    pDate = pDate.ToDate('mm/dd/yyyy');

    if (selectedDate.ToDate('mm/dd/yyyy') > pDate) {
        var message = lang == "en" ? 'La fecha de nacimiento no puede ser futura, la fecha "' + selectedDate + '" es una fecha futura.' :
        CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
        return false;
    }
    return true;
}

CallExecuteOnCloseEvent = function (sender, selectedDate) {  
    var CurrentTab = $("#hdnCurrentTab").val();
    var $sender = $(sender);
    if (selectedDate != "") {

        if (CurrentTab == "btnQuestionaries") {

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
        if ($sender.val() != "") {
            if ($sender.attr('id') == "tb_WUC_PI_DateBirth" ||
                $sender.attr('id') == 'txtDateOfBirthDesignatedPensioner' ||
                $sender.attr('id') == 'txtDateOfBirth') {
                var BeforeVal = $("#hdnDateOfBirthBefore").val();

                var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();
                var message = lang == 'en' ? 'The date of birth can not be future, the date "' + selectedDate + '" is a future date.'
                                        : 'La fecha de nacimiento no puede ser futura, la fecha "' + selectedDate + '" es una fecha futura.';

                pDate = pDate.ToDate('mm/dd/yyyy');

                if (selectedDate.ToDate('mm/dd/yyyy') > pDate) {
                    //Clean Controls
                    $sender.val("");
                    $("#hdnAge").val("");
                    $("#tb_WUC_PI_Age").val("");
                    $("#txtAge").val("");

                    $sender.focus();
                    CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                    return false;
                }

                var age = selectedDate.YearDiff("", "mm/dd/yyyy");

                $("#tb_WUC_PI_Age").val(age);

                if (age.toString() != "false") {
                    $("#hdnAge").val(age);
                    $("#txtAge").val(age);
                }

                $("#ddl_WUC_PI_MaritalStatus").focus();
                $("#ddlGender").focus();

            } else
                if ($sender.attr('id') == 'txtExpDate' ||
                    $sender.attr('id') == 'txtExpirationDate') {

                    var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();

                    var FechaSeleccionada = selectedDate.split("/");

                    var FechaFinal = FechaSeleccionada[0] + "/" + FechaSeleccionada[1] + "/" + FechaSeleccionada[2];

                    var Days = pDate.DayDiff(FechaFinal, 'mm/dd/yyyy');

                    if (Days < 61) {
                        $sender.val("");
                        CustomDialogMessageEx(null, null, null, true, lang == "en" ? "Warning" : "Advertencia", "DocExpirationDate");
                    }
                } else if ($sender.attr('id') == 'txtRegistrationDate') {

                    var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();

                    if (selectedDate.ToDate('mm/dd/yyyy') > pDate.ToDate('mm/dd/yyyy')) {
                        $sender.val("");
                        $sender.focus();
                        var message = lang == 'en' ? 'The date of registration of the company should not be a future date, the date "' + selectedDate + '" is a future date.'
                                     : 'La fecha de registro de la empresa no puede ser una fecha futura, la fecha "' + selectedDate + '" es una fecha futura.';
                        CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                    }
                } else if ($sender.attr("alt") == 'validateFutureDateofBirth' ||
                           $sender.attr("alt") == 'validateFutureDate') {

                    var pDate = (fecha.getMonth() + 1) + "/" + fecha.getDate() + "/" + fecha.getFullYear();

                    if (selectedDate.ToDate('mm/dd/yyyy') > pDate.ToDate('mm/dd/yyyy')) {
                        $sender.val("");
                        $sender.focus();
                        var message = lang == 'en' ? 'The date can not be future, the date "' + selectedDate + '" is a future date.'
                                     : 'La fecha no puede ser futura, la fecha "' + selectedDate + '" es una fecha futura';
                        CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                    }
                }

            if ($sender.attr('id') == 'txtTo') {

                var startDate = new Date($('#txtFrom').val()); 

                if (selectedDate.ToDate('mm/dd/yyyy') < startDate) {
                    $sender.val("");
                    $sender.focus();
                    var message = lang == 'en' ? 'The date [to] can not be less  than the date [from]'
                                         : 'la fecha [hasta] no puede ser menor a la fecha [desde]';
                    CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                }


            }

        }
    }
}

//// BENEFICIARIES TAB  -  Ing. José Mejía ////
function ValidateBeneficiaries(sender) {
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

//Usado para setear un intervalo, para mostrar el nombre del documento, ya que al subirlo, no se queda en el textbox.
function setIntervalFileName(fileName, className, isCompany) {
    var myVar = setInterval(function () {
        if (isCompany == 1) {
            switch (className) {
                case 'MP':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
                case 'MC':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AP':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCAdditionalInsured_WUCMainBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AC':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCAdditionalInsured_WUCContingentBeneficiaries_fuBenediciarieFileCompany_TextBox0_FakeInput').val(fileName);
                    break;
            }
        }
        else {
            switch (className) {
                case 'MP':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCMainInsured_WUCMainBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
                case 'MC':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCMainInsured_WUCContingentBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AP':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCAdditionalInsured_WUCMainBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
                case 'AC':
                    $('#bodyContent_DReviewContainer_BeneficiariesContainer_WUCAdditionalInsured_WUCContingentBeneficiaries_fuBenediciarieFile_TextBox0_FakeInput').val(fileName);
                    break;
            }
        }

        clearInterval(myVar);
    }, 100);
}

function CheckSingleCheckbox(ob) {
    var grid = ob.parentNode.parentNode.parentNode;
    var inputs = grid.getElementsByTagName("input");
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].type == "checkbox") {
            if (ob.checked && inputs[i] != ob && inputs[i].checked) {
                inputs[i].checked = false;
            }
        }
    }
}

var textSeparator = ";";

function OnListBoxSelectionChanged(listBox, args) {
    if (args.index == 0)
        args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
    UpdateSelectAllItemState();
    UpdateText();
}
function UpdateSelectAllItemState() {
    IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
}
function IsAllSelected() {
    var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
    return checkListBox.GetSelectedItems().length == selectedDataItemCount;
}
function UpdateText() {
    var selectedItems = checkListBox.GetSelectedItems();
    checkComboBox.SetText(GetSelectedItemsText(selectedItems));
}
function SynchronizeListBoxValues(dropDown, args) {
    checkListBox.UnselectAll();
    var texts = dropDown.GetText().split(textSeparator);
    var values = GetValuesByTexts(texts);
    checkListBox.SelectValues(values);
    UpdateSelectAllItemState();
    UpdateText(); // for remove non-existing texts
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

setCurrentAccordeon = function (obj, hiddenfield) {
    var index = 0;

    var isOpen = $(obj).parent().find("ul:first").css("display") == "block";

    if (!isOpen) {
        $(obj).parent().parent().find("li").find("a[lnk='lnk']").removeAttr("alt");
        //Marcar el Objeto como abierto
        $(obj).attr("alt", "Open");

        var hrefArray = $(obj).parent().parent().find("li").find("a[lnk='lnk']");

        var divParent = $(obj).parent().parent().attr("id");

        for (var x = 0; x <= hrefArray.length - 1; x++) {
            if ($(hrefArray[x]).attr("alt") == "Open") {
                $(hiddenfield).val(divParent + "|" + x);
                break;
            }
        }
    }

    var intervalo = setInterval(fixheight, 100);
    setTimeout(function () { clearInterval(intervalo) }, 400);
};

setCurrentAccordeonForIndex = function (hiddenfield) {
    if ($(hiddenfield).val() != "" & $(hiddenfield).val() != null) {
        var divActiveAccordeon = $(hiddenfield).val().split("|")[0];
        var ActiveAccordeonIndex = $(hiddenfield).val().split("|")[1];

        $($("#" + divActiveAccordeon + " > li").find("a[lnk='lnk']")).attr("id", "");
        $($("#" + divActiveAccordeon + " > li").find("a[lnk='lnk']")[ActiveAccordeonIndex]).addClass("shown").addClass("open").attr("id", "current");
        $(ObjAcordeon).accordion({ initShow: "#current" });
    }
};

function uploadFileChange(sender, event) {
    sender.Upload();
}
//// END BENEFICIARIES TAB ////

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
    var frequency = document.getElementById(ddlFrequencyID);
    var periodicpremium = document.getElementById(PeriodicPremiumvalueID)
    var hdAnnualPremium = document.getElementById('hdAnnualPremium');

    var TatalAnualpremium = 0.00;
    if (frequency != null && periodicpremium != null) {
        var PeriodicPremiumvalue = periodicpremium.value;
        var ddlFrequencyvalue = frequency.value;
        TatalAnualpremium = CalculateAnnualPrimium(PeriodicPremiumvalue.toString(), ddlFrequencyvalue);
    }

    ValItbis(TatalAnualpremium);

    if (txtAnnualPremium != null && txtAnnualPremiumWithTax != null) {
        txtAnnualPremium.value = TatalAnualpremium.toString();
        txtAnnualPremiumWithTax.value = TatalAnualpremium + CalculateItbis(TatalAnualpremium.toString());
        hdAnnualPremium.value = txtAnnualPremium.value;
    }

    ///Para Retorno de prima
    var RopAmount = 0.00;
    var txtAmount = document.getElementById('bodyContent_DReviewContainer_PlanPolicyContainer_WUCPlanInformation_UCSentinel_txtAmount');
    var ddlContributionPeriod = document.getElementById('bodyContent_DReviewContainer_PlanPolicyContainer_WUCPlanInformation_UCSentinel_ddlContributionPeriod');
    var InitialContribution = 0.00;

    if (txtAmount != null) {
        if (txtAmount.value != "")
            InitialContribution = parseFloat(txtAmount.value.replace(/[^0-9\.]/g, ''));
    }

    if ($('#bodyContent_DReviewContainer_PlanPolicyContainer_WUCPlanInformation_UCSentinel_ddlProductName').val() != undefined) {

        var JsonValue = $.parseJSON($('#bodyContent_DReviewContainer_PlanPolicyContainer_WUCPlanInformation_UCSentinel_ddlProductName').val());

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
