﻿var lang = "";
var grdName = "#gvIllustration";

function CallExecuteOnCloseEvent() { }

function GetDoc() {
    var html = $("#dvDocTransunion").html();
}

function RequestPage(urlPage) {
    BeginRequestHandler();
    $.ajax({
        url: urlPage,
        type: "post",
        asyc: false,
        dataType: "html",
        beforeSend: function () {
            //BeginRequestHandler();
        },
        success: function (data) {
            var dReq = $("#hdndReq").val();
            GenerateDocTransunion(data, dReq, function () { $("#btnRefreshList").click(); });
        }
    });
}

function pageLoad() {
    var Tds = $("#gvVehicle").find(".dxgvFooter_DevEx").find("td");
    setEndorsementBeneficaryAutoComplete();
    setAgentsAutoComplete();
    setOccupationAutoComplete();
    $("#dvOcupacion,#dvAutoCompleteEndorsementBen").find("span[role='status']").hide();
    var totalInsuredAmount = $("#hdntotalInsuredAmount").val();
    var totalDeductible = $("#hdntotalDeductible").val();
    var totalPremiumAmount = $("#hdntotalPremiumAmount").val();
    var tdTotalLabel = Tds.eq(11);
    var tdTotalInsuredAmount = Tds.eq(14);
    var tdTotalPremiumAmount = Tds.eq(15);
    var cssStyle = { "text-align": "center", "font-weight": "bold" };

    tdTotalLabel.text("TOTAL");
    tdTotalLabel.css(cssStyle);
    tdTotalInsuredAmount.text(totalInsuredAmount);
    tdTotalInsuredAmount.css(cssStyle);
    tdTotalPremiumAmount.text(totalPremiumAmount);
    tdTotalPremiumAmount.css(cssStyle);

    Configutations();

    $(".dxWeb_pcCloseButton_DevEx").css("display", "none");

    var CurrentTab = $("#hdnCurrentTab").val();
    $("#ulTabs li").removeClass("open");
    var liSelected = $("#" + CurrentTab).parent().addClass("open");

    lang = $("#hdnLang").val();
    setESCAPEkey();
    SetDatePicker();

    $("#pnTBSiniestralidad,#pnFactura,#AmortizationTablePanel,#popBlackList,#pnPepPop,#pnPropertyDetail,#pnTransportDetail,#pnBailDetail,#pnPaymentAgreementPop,#pnTransunion,#popCoverage,#pdfUploadPanel,#pnlShowUploadFile,#pnContactEditPop,#pnVehicleEditForm,#popEndoso,#popInspectionAddress,#pdfView,#pnShowPDF,#popCoverage,#AssignIllustrations").draggable({
        handle: ".titulos_azules"
    }).find(".titulos_azules").css("cursor", "move");

    var checkMark = "<div> <input type='checkbox' id='chkAll'/> </div>";

    var $Grid = $(grdName);

    var td = $Grid.find("tr[id*='DXHeadersRow']").find("td:first");
    td.html("");
    td.append(checkMark);

    var Tds = $Grid.find("tr[id*='DXHeadersRow']:first").find("td.dxgvHeader_DevEx");
    var tdView = Tds.eq(1).find("table").find("tr").find("td:first");
    if (tdView != null)
        tdView.addClass("view_fileHeader");

    var tdInspecction = Tds.eq(2).find("table").find("tr").find("td:first");
    if (tdInspecction != null)
        tdInspecction.addClass("inspection_fileHeader");

    var tdRequiredDocs = Tds.eq(3).find("table").find("tr").find("td:first");
    if (tdRequiredDocs != null)
        tdRequiredDocs.addClass("requiredDocs_fileHeader");

    var tdRequiredDocs = Tds.eq(3).find("table").find("tr").find("td:first");
    if (tdRequiredDocs != null)
        tdRequiredDocs.addClass("requiredDocs_fileHeader");

    var tdFinalCleareance = Tds.eq(4).find("table").find("tr").find("td:first");
    if (tdFinalCleareance != null)
        tdFinalCleareance.addClass("FinancialCleareans_fileHeader");

    setClickCheckBoxGridView(grdName, 'chkAll');

    $(grdName).find("input[id='chkAll']").click(function () {
        SelectAll(this, grdName);
    });

    getTabSelected();

    setFirstGroupTabs();

    $('#chkAll').click(function () {
        SelectAll(this, ".gvResult");
    });

    ChangeIllustrationStatus();

    $(".edit_file").click(function () {
        BeginRequestHandler();
    });

    $("#accordeonFilterIllustration").accordion({
        collapsible: true
    });

    setCurrentAccordeonForIndex("#hdnAccordeonIllustrationList");
    // setCurrentAccordeonForIndex("#hdnAccordeonFiltersList");

    if ($("#lnkRequired").length > 0 && $("#lnkRequired").parent().hasClass("open") && !$("#lnkDocuments").parent().hasClass("open")) {
        $("#lnkDocuments").parent().click();
    }

    if (document.getElementById("txtBusinessLine") && document.getElementById("txtBusinessLine").value == "Auto")
        HideHealthItems();

    configurePivotColumn();

    hideHasdocument_True();

    $(".dxpc-closeBtn").click(function myfunction() {
        $("#txtReasonPending").val("");
    });

    formatBrowseButton();

    $('.dxBB').append('<span class="upload"/>');

    DropText();

    $("#gvIllustration,#gvCoverages,#gvPOSCotizaciones").find('.dxgvDataRow_DevEx').each(function () {

        $this = $(this);

        $this.find("td").each(function () {
            $td = $(this);
            var txt = $td.text().trim();
            if (txt != "")
                $td.attr("title", txt);
        });
    });

    $("#drpStatusQuotation").change(function () {
        if ($(this).val() == "DeclinedByClient") {
            $("#lblReason").show();
            $($("#drpReasonDeclined").parent()).show();
        }
    });

    var isMissingInspection = $("#hdnMissingInspection").val() == "true";

    if (isMissingInspection) {
        $("#drpStatusQuotation").val("MissingInspection");
        $("#hdnMissingInspection").val("false");
    }

    accounting.settings = {
        currency: {
            symbol: "$",   // default currency symbol is '$'
            format: "%s%v", // controls output: %s = symbol, %v = value/number (can be object: see below)
            decimal: ".",  // decimal point separator
            thousand: ",",  // thousands separator
            precision: 2   // decimal places
        },
        number: {
            precision: 0,  // default precision on numbers is 0
            thousand: ","  // thousands separator
        }
    }

    var ohdnOpenMoreInformationPanel = $("#hdnOpenMoreInformationPanel");
    isOpenMoreInformationPanel = ohdnOpenMoreInformationPanel.val() == "true";
    var ofsetTransunion = $('#fsetTransunion');
    var obj = $("#spaMoreInf");

    if (isOpenMoreInformationPanel) {
        $(obj).text("[-]");
        ohdnOpenMoreInformationPanel.val("true");
        ofsetTransunion.fadeIn(300);
    } else {
        $(obj).text("[+]");
        ohdnOpenMoreInformationPanel.val("false");
        ofsetTransunion.fadeOut(300);
    }

    var val = $("#ddlIdType").val();
    if (val == "5")
        $("#txtIDExpDate").removeAttr("validation");
    else
        $("#txtIDExpDate").attr("validation", "Required");

    $("#ddlIdType").on("change", function () {
        var $this = $(this);
        if ($this.val() == "5")
            $("#txtIDExpDate").removeAttr("validation");
        else
            $("#txtIDExpDate").attr("validation", "Required");
    });

    $('.punt').each(function () { var c = accounting.formatNumber($(this).text()); $(this).text(c); });

    var ddlIdType = $("#ddlIdType");
    IdChange(ddlIdType, "txtIDNumber");

    var ddlRepresentativeIdentificationType = $("#ddlRepresentativeIdentificationType");
    IdChange(ddlRepresentativeIdentificationType, "txtRepresentativeIdentification");

    ddlRepresentativeIdentificationType.change(function () {
        var $this = $(this);
        IdChange($this, "txtRepresentativeIdentification");
    });

    ddlIdType.change(function () {
        var $this = $(this);
        IdChange($this, "txtIDNumber");
    });

    $("#gvCoveragesIL_groupcol0").find("td:first").css("width", "200px");

    var GroupingHeader = $("#gvCoveragesIL").find(".dxgvGroupRow_DevEx");
    GroupingHeader.each(function () {
        var $tdTarget = $(this).find("td").eq(1);
        var text = $.trim($tdTarget.text().split(":")[1]);
        $tdTarget.text(text);
    });

    $("#popCoverage").find("tr[id*='gvCoveragesIL_DXGroupRowExp']").each(function () {
        var $this = $(this);
        $this.after("<tr class='gradient_azul cober_pp'>");
        var $tr = $this.next();
        $tr.append("<td> <span>Cobertura</span></td>");
        $tr.append("<td> <span>Limite</span></td>");
        //$tr.append("<td> <span>Porc. coaseguro</span></td>");
        $tr.append("<td> <span>Coaseguro</span></td>");
        $tr.append("<td> <span>Deducible</span></td>");
        var isVisible = $("#hdnHideOrShowPrimeAndRate").val() == "true";
        if (isVisible) {
            $tr.append("<td> <span>Prima</span></td>");
            $tr.append("<td> <span>Tasa</span></td>");
        }
    });

    $(".dxgvGroupPanel_DevEx").remove();
    $('#gvCoveragesIL_DXMainTable').find("tbody").find("tr:first").remove();


    //Grid de Coberturas del Facultativo
    $("#gvCoverages").find(".dxgvGroupRow_DevEx").each(function () {
        var $tdTarget = $(this).find("td").eq(1);
        var text = $.trim($tdTarget.text().split(":")[1]);
        $tdTarget.text(text);
    });

    calculatePercentaje();

    $("#gvCoverages").find(".dxgvDataRow_DevEx").each(function () {
        var $Row = $(this);
        var $Inputs = $Row.find(".valGet");
        var $InputTotal = $Row.find(".valTotal");

        var SumatoryPerc = 0;

        $Inputs.each(function () {
            var $this = $(this);
            SumatoryPerc += parseFloat($this.val());
        });

        $InputTotal.val(SumatoryPerc.toFixed(2));
    });

    var $Inputs = $("#gvCoverages").find(".dxgvDataRow_DevEx").find(".valGet");

    $Inputs.focus(function () {
        $(this).select();
    });

    $Inputs.blur(function () {
        var $this = $(this);
        var $Tr = $(this).parents().eq(1);
        var $Total = $Tr.find(".valTotal");
        var SumatoryPerc = 0;

        $Tr.find(".valGet").each(function () {
            var $this = $(this);
            SumatoryPerc += parseFloat($this.val());
        });

        if (SumatoryPerc > 100) {
            $this.val("0");
            CustomDialogMessage("El porciento total no debe ser mayor que 100");
            return false;
        }

        $Total.val(SumatoryPerc.toFixed(2));
    });

    $('#gvContrato').find("input.PayDateLimit").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        minDate: new Date(),
    });

    setPorcentajeDescuento();

    $("input.readOnlyCCVT").each(function () {
        var $this = $(this);
        $this.mouseover(function () {
            CustomToolTips($this, $this.attr("alt"), null, null);
        });
    });

    var isCompany = $("#hdnIsCompany").val() == "true";

    $("#spanPep").mouseover(function () {
        $this = $(this);
        $this.css("cursor", "pointer");
        getToolTipMessage('ToolTipPEP', function (data) {
            msj = data.d;
            CustomToolTips("#spanPep", msj, null, 5500, 'classic', 9);
        });
    });

    $("#spanBene").mouseover(function () {
        $this = $(this);
        $this.css("cursor", "pointer");
        getToolTipMessage(isCompany ? 'ToolTipBenFinalCompania' : 'ToolTipBenFinalPersona', function (data) {
            msj = data.d;
            CustomToolTips("#spanBene", msj, null, 5500, 'classic', 9);
        });
    });

    $("#spanManagerName").mouseover(function () {
        $this = $(this);
        $this.css("cursor", "pointer");
        getToolTipMessage('ToolTipManagerName', function (data) {
            msj = data.d;
            CustomToolTips("#spanManagerName", msj, null, null, 'classic', 9);
        });
    });

    $("body").find(".dxgvHeader_DevEx").find("table").find("tbody").find("tr").find("td:first").attr("style", "line-height:11px !important");

    $("input[data-mask]").each(function () {
        var $this = $(this);
        var mask = $this.attr('data-mask');
        $this.inputmask(mask);
    });


    var dvMap = document.getElementById('map');
    //--Obtener la direccion que esta en los hiddenfields
    var Long = $("#hdnlongitudSelectedVehicle").val();
    var Lat = $("#hdnlatitudelectedVehicle").val();
    if (Long != "0" || Lat != "0") {
        var myLatLng = new google.maps.LatLng(Lat, Long);
        searchAddressLatLon(myLatLng);
        initializeSubmitMap(dvMap, myLatLng, true, true, true);
    } else {
        initializeSubmitMap(dvMap, null, true, true, true);
        var $elementToPutAddress = $("#ltSelectedAddressOnMap");
        if ($elementToPutAddress != undefined)
            $elementToPutAddress.text("Dirección seleccionada en el mapa : N/A ");
    }

    var $InputGrid = $("#gvIllustration tr.dxgvFilterRow_DevEx").find("input.dxeEditArea_DevEx");

    $InputGrid.on({
        mouseover: function () {
            var $this = $(this);
            CustomToolTips($this, "Para ejecutar el filtro pulse enter", "top", null, null, null);
        },
        keydown: function (event) {
            var $this = $(this);
            //console.log(event.which);
            if ((event.which == 8 || event.which == 46) && $this.val() == "")
                aspxGVScheduleCommand('gvIllustration', ['ClearFilter'], 0);
        },
        keypress: function (event) {
            //console.log(event.which);
            var $this = $(this);
            if ((event.which == 8 || event.which == 46) && $this.val() == "")
                aspxGVScheduleCommand('gvIllustration', ['ClearFilter'], 0);
        }
    });

    var $txtFilterFrom = $("#bodyContent_WUCIllustrationsList_txtFrom");
    var $txtFilterTo = $("#bodyContent_WUCIllustrationsList_txtTo");
    var $drpPeriod = $("#bodyContent_WUCIllustrationsList_drpPeriod");

    $drpPeriod.change(function () {
        var $this = $(this);
        if ($this.val() != "8") {            
            $txtFilterFrom.attr("disabled", "disabled");
            $txtFilterTo.attr("disabled", "disabled");
            $txtFilterFrom.css("background-color", "antiquewhite");
            $txtFilterTo.css("background-color", "antiquewhite")
        } else {
            $txtFilterFrom.removeAttr("disabled");
            $txtFilterTo.removeAttr("disabled");
            $txtFilterFrom.removeAttr("style");
            $txtFilterTo.removeAttr("style");
        }
    });

    var OptionSelected = $drpPeriod.val();  

    if (OptionSelected != "8") {
        $txtFilterFrom.attr("disabled", "disabled");
        $txtFilterTo.attr("disabled", "disabled");
        $txtFilterFrom.css("background-color", "antiquewhite");
        $txtFilterTo.css("background-color", "antiquewhite")
    }
}

function VerifyCompleteMask() {

    var $InputsToValidate = $("input[validateCompleteMask='validateCompleteMask']");
    var oInputs = []
    var oRecordInput;

    $InputsToValidate.each(function () {
        var $this = $(this);
        var isComplete = $this.inputmask("isComplete");

        if (!isComplete && $this.val() != "") {
            oRecordInput = {}
            oRecordInput.InputId = $this.attr("id");
            oInputs.push(oRecordInput);
        }
    });

    if (oInputs.length > 0) {
        var msg = "";
        for (var i = 0; i < oInputs.length; i++) {
            var obj = $("#" + oInputs[i].InputId);
            var label = getLabelField(obj);
            msg += "El campo " + label + " no esta completo <br>";
        }

        CustomDialogMessage(msg, null);
        return false;
    }

    result = validateForm('dvVehicleContactForm');

    return (result) ? true : false;

}

function DisplayResultSaveFacultative(ButtonData, Grid) {
    $("#" + Grid).find("input[data='" + ButtonData + "']").click();
}

function ConfirmationCallConfirmation(obj) {
    DlgConfirmWithFuncCallBackExt(obj, "", null, 200, function () { $(obj).prop("checked", true); }, null, "ConfirmationCallConfirmation");
    return false;
}

function IdChange($this, objIdTxt) {

    var $txt = $('#' + objIdTxt);
    switch ($this.val()) {
        case "1":
        case "3":
            $txt.inputmask("999-9999999-9");
            break;
        case "5":
            $txt.inputmask("9-9999999-9");
            break;
        default:
            $txt.inputmask('remove');
            break;
    }
}

function ShowMore(obj) {
    var ofsetTransunion = $('#fsetTransunion');
    var isVisible = ofsetTransunion.css("display") == "block";
    var ohdnOpenMoreInformationPanel = $("#hdnOpenMoreInformationPanel");

    if (isVisible) {
        $(obj).text("[+]");
        ohdnOpenMoreInformationPanel.val("false");
        ofsetTransunion.fadeOut(300);
    }
    else {
        $(obj).text("[-]");
        ohdnOpenMoreInformationPanel.val("true");
        ofsetTransunion.fadeIn(300);
    }
}

function rAll(text, busca, reemplaza) {
    while (text.toString().indexOf(busca) != -1)
        text = text.toString().replace(busca, reemplaza);
    return text;
}

function ConfiguraMontos() {
    var valView = $("#bodyContent_WUCIllustrationsList_ddlEmisionesViewBy").val();

    if (valView != undefined) {
        $(".dxpgCell_DevEx").each(function () {
            var $this = $(this);
            var unformat = '';
            var format = '';

            if (valView == "0" || valView == "4") {
                format = accounting.formatNumber($this.text().replace('$', '').replace('€', ''));
                $this.text(format);
            }
            else if (valView == "1" || valView == "2" || valView == "3") {
                //unformat = accounting.unformat($this.text().replace('$', '').replace('€', '').trim());
                //format = accounting.formatMoney(unformat);
                //$this.text(format);
                var tt = $this.text();
                if (tt < 1) { tt = '$' + tt; }
                $this.text(tt);
            }
            //else if (valView == "3") {
            //    unformat = accounting.unformat($this.text().replace('$', '').replace('€', '').trim());
            //    format = accounting.formatMoney(unformat);
            //    $this.text(format);
            //}
        });

        if (valView == "3") {
            $(".dxpgCell_DevEx").each(function () {
                var $this = $(this);
                var unformat = '';
                var format = '';
                var cellIndex = $this[0].cellIndex;
                if (cellIndex % 3 == 0) {
                    var $celdas = $('.dxpgCell_DevEx');
                    $celdas.each(function (i, v) {
                        var ri = i + 1;
                        var mul = 3;
                        if (ri % mul == 0) {
                            var annualPremium = 0.00;
                            var insuredAmount = 0.00;
                            var rate = 0.000;

                            annualPremium = accounting.unformat($($celdas[ri - 3]).text().replace('$', ''), '.');
                            insuredAmount = accounting.unformat($($celdas[ri - 2]).text().replace('$', ''), '.');

                            if (annualPremium >= 1 && insuredAmount >= 1) {
                                rate = annualPremium / insuredAmount;
                                $($celdas[ri - 1]).text(accounting.toFixed(rate, 3) + '%');
                            }
                            else
                                $($celdas[ri - 1]).text('0.000%');
                        }
                    });
                }
            });
        }
    }
}

function ConfiguraValores() {
    var valView = $("#bodyContent_WUCIllustrationsList_ddlPerformanceViewBy").val();
    if (valView != undefined) {
        $(".dxpgCell_DevEx").each(function () {
            var $this = $(this);
            var unformat = '';
            var format = '';

            format = accounting.formatNumber(($this.text().replace('$', '').replace('€', '')));
            $this.text(format);
        });

        $(".dxpgCell_DevEx").each(function () {
            var $this = $(this);

            var cellIndex = $this[0].cellIndex;
            if (cellIndex % 3 == 0) {
                var $celdas = $('.dxpgCell_DevEx');
                $celdas.each(function (i, v) {
                    var ri = i + 1;
                    var mul = 3;
                    if (ri % mul == 0) {
                        var cantidad = 0;
                        var tiempo = 0;
                        var rendimiento = 0.00;

                        cantidad = accounting.unformat($($celdas[ri - 3]).text().replace('$', ''), '.');
                        tiempo = accounting.unformat($($celdas[ri - 2]).text().replace('$', ''), '.');

                        if (cantidad >= 1 && tiempo >= 1) {
                            rendimiento = tiempo / cantidad;
                            $($celdas[ri - 1]).text(accounting.toFixed(rendimiento, 2));
                        }
                        else
                            $($celdas[ri - 1]).text('0.00');
                    }
                });
            }
        });
    }
}

function DropText() {
    //Activar/Desactivar txtMontoDescuento en Popup de descuentos
    $('#ddlPorcentajeDescuento').on('change', function () {
        var selectedIndex = $("select[id='ddlPorcentajeDescuento'] option:selected").index();
        if (selectedIndex === 0)
            $('#txtMontoDescuento').removeAttr('disabled');
        else
            $('#txtMontoDescuento').attr('disabled', 'disabled');
    });

    //Activar/Desactivar ddlPorcentajeDescuento en Popup de descuentos
    var lastMontoDescuento = '';
    $('#txtMontoDescuento').on('change keyup paste mouseup keypress', function (e) {
        if (e.which == 13)
            return false;

        $('#txtCalculatedAmount').val($(this).val());

        setInterval(function () {
            if ($('#txtMontoDescuento').val() != lastMontoDescuento) {
                lastMontoDescuento = $('#txtMontoDescuento').val();
                if (parseFloat(lastMontoDescuento) > 0) {
                    $('#ddlPorcentajeDescuento').attr('disabled', 'disabled');
                }
                else
                    $('#ddlPorcentajeDescuento').removeAttr('disabled');
            }
        }, 500);
    });

}

function ValidateSendToCore(sender, grid) {

    var TotalCheck = CountCheck('#' + grid);

    if (TotalCheck == 0) {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Warning" : "Advertencia", "gridSelectionValidation");
        return false;
    }
    return DlgConfirm(sender);
}

function ReturnToQuotationInbox() {
    $('#lnkIllustrations').click();
}

function ClosePopPropertyDetail() {
    $('#hdnPopPropertyDetail').val('false');
    $("#popupBhvrPropertyDetail_backgroundElement").hide();
    $find('popupBhvrPropertyDetail').hide();
}

function ClosePopFinalBeneficiary() {

    $('#hdnShowFinalBeneficiaryPop').val('false');
    $("#popupBhvrFinalBeneficiaryPop_backgroundElement").hide();
    $find('popupBhvrFinalBeneficiaryPop').hide();

    if ($('#hdnHasPep').val() == 'true') {
        var ItemSelectedNo = "{\"CorpId\":1,\"AgentId\":2}";
        //$('#bodyContent_ucInsuredInformation_UCContactEditForm_ddlPep').val('{"CorpId":1, "AgentId":2}');
        $('#bodyContent_ucInsuredInformation_UCContactEditForm_ddlPep').val(ItemSelectedNo);
        __doPostBack('bodyContent_ucInsuredInformation_UCContactEditForm_btnVerPEPS', '');
    }
}

function ClosePopPep() {
    $('#hdnShowPepPop').val('false');
    $("#popupBhvrPepPop_backgroundElement").hide();
    $find('popupBhvrPepPop').hide();
}

function ClosePopBlackList() {
    $('#hdnPopBlackList').val('false');
    $("#popupBlackList_backgroundElement").hide();
    $find('popupBlackList').hide();
}

function ClosePopFacultyPop() {
    $('#hdnPopFacultyPosition').val('false');
    $("#popupBhvrFacultyPosition_backgroundElement").hide();
    $find('popupBhvrFacultyPosition').hide();
}

function ClosePopAirPlaneDetail() {
    $('#hdnPopAirPlaneDetail').val('false');
    $("#popupBhvrAirPlaneDetail_backgroundElement").hide();
    $find('popupBhvrAirPlaneDetail').hide();
}

function ClosePopNavyDetail() {
    $('#hdnPopNavyDetail').val('false');
    $("#popupBhvrNavyDetail_backgroundElement").hide();
    $find('popupBhvrNavyDetail').hide();
}

function ClosePopBailDetail() {
    $('#hdnPopBailDetail').val('false');
    $("#popupBhvrBailDetail_backgroundElement").hide();
    $find('popupBhvrBailDetail').hide();
}

function ClosePopTransportDetail() {
    $('#hdnPopTransportDetail').val('false');
    $("#popupBhvrTransportDetail_backgroundElement").hide();
    $find('popupBhvrTransportDetail').hide();
}

function CloseBailCoverage() {
    $('#hdnBailCoverages').val('false');
    $("#BailupBhvrnavyCoverages_backgroundElement").hide();
    $find('BailupBhvrnavyCoverages').hide();
}

function CloseTransportCoverage() {
    $('#hdnTransportCoverages').val('false');
    $("#TransportupBhvrnavyCoverages_backgroundElement").hide();
    $find('TransportupBhvrnavyCoverages').hide();
}

function ClosePopRequirement() {
    $('#hdnShowUploadFile').val('false');
    $("#popupBhvrUploadFile_backgroundElement").hide();
    $('#txtPath').val('');
    $find('popupBhvrUploadFile').hide();
}

function validaCumplimiento() {
    $("#btnValidateCumplimiento").click();
}

function closePopEditContact() {
    DlgConfirmWithFuncCallBack(null, "Si ha hecho algun cambio y no lo guardo, al cerrar los cambio se perderan, Seguro que desea cerrar?", null, 140, function () { closePOPContactEdit() }, function () { });
}

function ClosePopTBSiniestralidad() {
    $('#hdnPopTBSiniestralidad').val('false');
    $("#popupBhvrTBSiniestralidad_backgroundElement").hide();
    $find('popupBhvrTBSiniestralidad').hide();
}

function closePOPContactEdit() {
    $("#popupBhvrContactEditPop_backgroundElement").hide();
    var pop = $find('popupBhvrContactEditPop');
    if (pop != null) {
        $find('popupBhvrContactEditPop').hide();
        $("#hdnShowPopContactEdit").val('false');
    }
    //$("#btnCerrarPOP").click();
}

function closePopPaymentAgreement() {
    $('#hdnShowPopPaymentAgreementPop').val('false');
    $("#popupBhvrPaymentAgreementPop_backgroundElement").hide();
    $find('popupBhvrPaymentAgreementPop').hide();
}

function CloseFileUpload() {
    $("#popupBhvrUploadFile_backgroundElement").css("display", "none");
    $('#pnlShowUploadFile').hide()
};

function ClosePopCoverage() {
    $('#hdnPopCoverages').val('false');
    $("#popupBhvrPopCoverages_backgroundElement").hide();
    $find('popupBhvrPopCoverages').hide();
};

function CloseNavyCoverage() {
    $('#hdnnavyCoverages').val('false');
    $("#navyupBhvrnavyCoverages_backgroundElement").hide();
    $find('navyupBhvrnavyCoverages').hide();
}

function CloseAirCoverage() {
    $('#hdnAirCoverages').val('false');
    $("#airupBhvrairCoverages_backgroundElement").hide();
    $find('airupBhvrairCoverages').hide();
};

function ClosePopEndoso() {
    $('#txtSelectBenficiary,#txtBeneficiario,#txtRNC,#txtContactName,#txtPhoneNumber,#txtEmailAddress').val('');
    $('#txtMonto').val('0.00');
    $('#hdnEndosoPopup').val('false');
    $("#popupEndoso_backgroundElement").hide();
    $find('popupEndoso').hide();
};

function ClosePopInspectionAddress() {
    $('#hdnInspectionAddressPopup').val('false');
    $("#popupInspectionAddress_backgroundElement").css("display", "none");
    $find('popupInspectionAddress').hide();
};

function ClosePopVehicleForm() {
    $('#hdnPopVehicleEditForm').val('false');
    $("#popupBhvr1VehicleEditForm_backgroundElement").css("display", "none");
    $find('popupBhvr1VehicleEditForm').hide();
}

function ClosePopVehicleEditForm() {
    $('#hdnPopVehicleEditForm').val('false');
    $("#popupBhvrTransunion_backgroundElement").css("display", "none");
    $find('popupBhvrTransunion').hide();
}

function ClosePopTransunion() {
    $('#hdnShowPDF').val('false');
    $("#popupBhvrTransunion_backgroundElement").css("display", "none");
    $find('popupBhvrTransunion').hide();
}

function ClosePop() {
    $('#hdnPopTransunion').val('false');
    $("#popupBhvr1PDF_backgroundElement").css("display", "none");
    $find('popupBhvr1PDF').hide();
};

function ClosePopQuotation() {
    $('#hdnShowPreviewPDF').val('false');
    $("#popupBhvrPDFView_backgroundElement").css("display", "none");
    $find('popupBhvrPDFView').hide();
};

function ClosePopQuotationPrev() {
    $('#hdnShowPreviewPDF').val('false');
    $("#popupBhvrQuotation_backgroundElement").css("display", "none");
    $find('popupBhvrQuotation').hide();
};

function ClosePopDiscount() {
    $("#hdnApplyDiscount").val('false');
    $("#hdnShowDiscount").val('false');
    $("#popupBhvr1Discount_backgroundElement").css("display", "none");
    $find('popupBhvr1Discount').hide();
}

function ClosePopCoverageView() {
    $('#hdnShowPopCoverage').val('false');
    $find('popupBhvr1').hide();
};

function closePopPrintInvoice() {
    $("#hdnShowpoppnPrintingInvoice").val('false');
    $("#popupBhvrShowPoppnPrintingInvoice_backgroundElement").css("display", "none");
    $find('popupBhvrShowPoppnPrintingInvoice').hide();
}

function ClosePopFactura() {
    $("#hdnFactura").val('false');
    $("#popupBhvrFactura_backgroundElement").css("display", "none");
    $find('popupBhvrFactura').hide();
}

$(document).ready(function () {
    $("#ulTabs li:first-child").addClass("open");
    $("#ulTabsseg li:first-child").addClass("open");
    $("#divsuplementos").hide();
    hideHasdocument_True();
    calculatePercentaje();
});

function hideHasdocument_True() {
    if ($(".hasdocument_True.hideme").length > 0) {
        $(".hasdocument_True.hideme").hide();
    } else {
        $(".hasdocument_True.hideme").show();
    }
}

function changeTabSelected2() {
    $("#ulTabsseg li:nth-child(2)").addClass("open");
    $("#divsuplementos").show();
}

function HideHealthItems() {
    $(".health").hide();
}

function HideAutoItems() {
    $(".auto").hide();
}

function changeTabSelected1() {
    $("#divsuplementos").hide();
    $("#ulTabsseg li:nth-child(2)").removeClass("open");
    $("#ulTabsseg li:first-child").addClass("open");
}

function ChangeIllustrationStatus(btn) {

    var hdnButtonSelected = document.getElementById("hdnButtonSelected");
    if ((btn == null || btn == undefined) && (hdnButtonSelected == null || hdnButtonSelected.value == "")) return;

    if (btn == null || btn == undefined)
        btn = document.getElementById(hdnButtonSelected.value);
    else
        hdnButtonSelected.value = btn.id;

    var status = btn.getAttribute("data-Status");
    var divReasonDeclined = document.getElementById("divReasonDeclined");
    var txtReasonPending = document.getElementById("txtReasonPending");
    var lblStatusMessage = document.getElementById("lblStatusMessage");
    var lblReason = document.getElementById("lblReason");
    var lblNoteReason = document.getElementById("lblNoteReason");

    document.getElementById("hdnIllustrationStatus").value = status;

    var classNameDivListPolicies = "";

    if (status == "PendingByClient" || status == "Submitted") {
        $([txtReasonPending, lblReason]).show();
        $(txtReasonPending).attr("validation", "Required");
        $("#drpReasonDeclined").removeAttr("validation");
        $(divReasonDeclined, lblNoteReason).hide();
        classNameDivListPolicies = "divListPoliciesWithTextArea";
    }
    else if (status == "DeclinedBySubscription") {
        $([divReasonDeclined, lblReason, lblNoteReason, txtReasonPending]).show();
        $("#drpReasonDeclined").attr("validation", "Required");
        classNameDivListPolicies = "divListPoliciesWithDropDownTextArea";
    }
    else if (status == "DeclinedByClient") {
        $([divReasonDeclined, lblReason, lblNoteReason, txtReasonPending]).show();

        $("#drpStatusQuotation").attr("validation", "Required");

        classNameDivListPolicies = "divListPoliciesWithDropDownTextArea";

        var tab = btn.getAttribute("data-tab");
        document.getElementById("hdnTab").value = tab;

    }
    else {
        $([txtReasonPending, divReasonDeclined, lblReason]).hide();
        $("#drpReasonDeclined").removeAttr("validation");
        $(txtReasonPending).removeAttr("validation");
        classNameDivListPolicies = "divListPolicies";
        $([lblNoteReason, txtReasonPending]).show();
    }

    $("#divListPolicies").removeClass().addClass(classNameDivListPolicies);


    lblStatusMessage.innerText = btn.getAttribute("data-StatusMessage");
    var val = FillTblListPolicies("divListPolicies");

    if (val)
        ppcChangeStatusIllustrations.Show();

    return false;
}

function filldrop(tab) {

    $.ajax({
        type: "POST",
        url: "~/Pages/Illustrations.aspx/StatusQuotation",
        data: '{tab: "' + tab + '" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        }
    });
}

function OnSuccess(response) {
    alert(response.d);
}

function setTabQoutation(btn) {

    var $but = $(btn);

    if ($but !== null) {
        var tab = $but.attr("data-tab");
        $("#hdnTab").val(tab);
    }
}

function ChangeIllustrationStatusClick() {
    var isValidForm = validateForm("frmStatusToSubmit");
    if (isValidForm)
        BeginRequestHandler();
    return isValidForm;
}

function BackToIllustrationList(Message, Title) {
    var funcCallBack = function () {
        $('div.box_btn.activo.cursorpointer').click()
    };
    CustomDialogMessageWithCallBack(Message, funcCallBack, Title, funcCallBack, null);
}

function ClosePopup() {
    document.getElementById("hdnButtonSelected").value = document.getElementById("hdnIllustrationStatus").value = "";
    $("#drpStatusQuotation").val("-1");
    $("#drpInspectors").val("-1");
    $("#divInspectors").hide();
    $("#txtReasonPending").val();
}

function OpenApplyDiscount() {
    document.getElementById("txtPopupDiscountIllustrationNo").value = document.getElementById("txtIllustrationNo").value;
    document.getElementById("txtPopupDiscountInsuredName").value = document.getElementById("txtClientName").value;
    $('#hdnApplyDiscount').val('true');
}

function SaveApplyDiscountSucessfully() {
    ppcApplyDiscount.Hide();
    CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Save Sucessfully" : "Guardado Satisfactoriamente", "SaveSucessfully");
}

function OpenApplySurcharge() {
    document.getElementById("txtPopupSurchargeIllustrationNo").value = document.getElementById("txtIllustrationNo").value;
    document.getElementById("txtPopupSurchargeInsuredName").value = document.getElementById("txtClientName").value;
    ppcApplySurcharge.Show();
}

function SaveApplySurchargeSucessfully() {
    ppcApplySurcharge.Hide();
    CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Save Sucessfully" : "Guardado Satisfactoriamente", "SaveSucessfully");
}

function OpenPopupTagConditioned(headerText) {
    ppcVehicleTagConditioned.SetHeaderText(headerText);
    ppcVehicleTagConditioned.Show();
}

function openFileUpload() {
    $('#fldUploadDocument').click();
}

function FileUpload_onChange(FileUploadID) {
    var Upload_File = $(FileUploadID);
    var btnFileUp = $('#btnSaveDocument');  //var btnFileUp = $get('btnSaveDocument');
    var myfile = Upload_File.val();

    var size = $(FileUploadID)[0].files[0].size;

    var hdnRequiredFileSize = $("#RequiredFileSize").val();

    if (myfile != '' && myfile != null) {
        if (myfile.toLowerCase().indexOf("pdf") < 1) {
            alert('File type is not supported.');
            return false;
        }
        else if (size > hdnRequiredFileSize) {
            CustomDialogMessage(null, "fileLargeMessage");
            return false;
        }
        else
            btnFileUp.click();
    }
}

function IllustrationListRowDblClick(s, e) {
    if (document.getElementById("hdnTabSelected").value != 'lnkPuntoVentaTab') {
        document.getElementById("hdnSelectedRowVisibleIndex").value = e.visibleIndex;
        document.getElementById("btnSelectedRow").click();
    }
}

function IllustrationListRowClick(s, e) {
    if (document.getElementById("hdnTabSelected").value == 'lnkPuntoVentaTab') {
        $(".dxgvFocusedRow_DevEx").addClass("dxgvDataRow_DevEx");
        $(".dxgvFocusedRow_DevEx").removeClass("dxgvFocusedRow_DevEx");
    }
}

function closePopAssignIllustration() {

    $('#hdnShowPopAssignIllustration').val('false');
    $("#popupBhvrShowPopAssignIllustration_backgroundElement").css("display", "none");
    $find('popupBhvrShowPopAssignIllustration').hide();
}

function closePopLocateQuotFlat() {

    $('#hdnShowpopLocateQuotFlat').val('false');
    $("#popupBhvrShowPopLocateQuotFlat_backgroundElement").css("display", "none");
    $find('popupBhvrShowPopLocateQuotFlat').hide();
}

function ValidateCheck(Grid, messageKey) {
    var result = true;
    var gvIllustration = $(Grid);

    var CheckCount = CountCheck(gvIllustration);

    if (CheckCount == 0) {
        CustomDialogMessage(null, 'gridSelectionValidation');
        result = false;
    }

    if (CheckCount > 1) {
        result = false;
        CustomDialogMessage(null, messageKey);
    }
    return result;
}

function KeepOpenDocumentRequired() {
    $("#lnkRequired").parent().click();
    $("#lnkDocuments").parent().click();
}

function OpenAssignIllustrations() {
    var val = FillTblListPolicies("divAssignListPolicies");
    if (val) {
        $('#drpAssignIllustrationsSubscribers').val(0);
        ppcAssignIllustrations.Show();
    }
    return false;
}

var wasCheckedFromOuterSide = false;

function FillTblListPolicies(divListName) {
    var lstPolicies = [];
    if (document.getElementById("gvIllustration") != null) {
        var lstCheckboxSelected = $("#gvIllustration_DXMainTable :checkbox:checked");

        if (lstCheckboxSelected.length == 0 && wasCheckedFromOuterSide == false) {
            CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Warning" : "Advertencia", "gridSelectionValidation");
            wasCheckedFromOuterSide = false;
            return false;
        } else if (lstCheckboxSelected.length == 0 && wasCheckedFromOuterSide == true) {
            wasCheckedFromOuterSide = false;
            ppcChangeStatusIllustrations.Hide();
            return false;
        } else { wasCheckedFromOuterSide = true; }

        lstPolicies = GetListPolicies(lstCheckboxSelected);
    } else {
        wasCheckedFromOuterSide = false;

        var txtIllustrationNo = document.getElementById("txtIllustrationNo");
        if (txtIllustrationNo == null) return false;
        lstPolicies.push(txtIllustrationNo.value);
    }

    $("#" + divListName + " table tbody tr").remove();

    var tbodyLstPolicies = $("#" + divListName + " table tbody");

    var PolizasSeleccionadas = '';

    for (var i = 0; i < lstPolicies.length; i++) {
        var policyNo = lstPolicies[i];

        PolizasSeleccionadas += policyNo + '|';

        var trPolicy = $('<tr class="dxgvDataRow_DevEx"/>');
        var tdPolicy = $('<td class="dxgv"/>');
        tdPolicy.text(policyNo);
        trPolicy.append(tdPolicy);
        tbodyLstPolicies.append(trPolicy);
    }

    if (document.getElementById("hdnPolizasSeleccionadas") != null) {
        if (PolizasSeleccionadas.length > 0) {
            document.getElementById("hdnPolizasSeleccionadas").value = PolizasSeleccionadas;
        }
    }

    return true;
}

function ShowNotes() {
    $("#txtNotes").attr('Validation', 'Required');
    setTimeout('ppcNotes.Show()', 10);
}

function GetListPolicies(lstCheckboxSelected) {
    var lstPolicies = [];

    for (var i = 0; i < lstCheckboxSelected.length; i++) {
        var checkboxSelected = lstCheckboxSelected[i];
        var trParent = $(checkboxSelected).parent().parent();
        lstPolicies.push(trParent.find("td:nth-child(6)").text());
    }
    return lstPolicies;
}

function gvRecargosRowDblClick(s, e) {
    document.getElementById("hdnSelectedRowVisibleIndex").value = e.visibleIndex;
    document.getElementById("btnSelectedRow").click();
}

function gvDescuentosCustomButtonClick(s, e) {
    if (e.buttonID != 'btnDelete') return;

    document.getElementById("hdnSelectedRowVisibleIndex").value = e.visibleIndex;
}

function OpenAllSurcharges() {
    ppcAllSurcharges.Show();
}

function getTabSelected() {
    var CurrentTab = $("#hdnTabSelected").val();
    $("#MenuTabs li").removeClass("active");
    $("#" + CurrentTab).parent().addClass("active");
}

ConfirmPrintList = function (sender) {

    var TotalCheck = CountCheck('#gvIllustration');

    if (TotalCheck == 0) {
        CustomDialogMessageEx(null, 500, 150, true, lang == "en" ? "Warning" : "Advertencia", "gridSelectionValidation");
        return false;
    }
};

function ddlInspectors(visible) {

    $('#drpDocumentos').removeAttr('validation');
    $('#drpReasonDeclined').removeAttr('validation');

    var ddlValue;

    if (visible) {
        document.getElementById("divVehiclesToInspection").style.display = "block";
        document.getElementById("divDocumentos").style.display = "none";
        $('#drpInspectors').attr('validation', 'Required');
        $(".cbkVehicle").attr("validation", "Required");

        ddlValue = $("#drpStatusQuotation").val();
    }
    else {
        document.getElementById("divVehiclesToInspection").style.display = "none";
        document.getElementById("divDocumentos").style.display = "none";

        $('#drpInspectors').removeAttr('validation');
        $(".cbkVehicle").removeAttr('validation');

        ddlValue = '';
    }

    document.getElementById("hdnIllustrationStatus").value = ddlValue;
    $('#btnDeclinedByClient').attr('data-Status', ddlValue);
}

function ddlDocumentos(visible) {

    $('#drpInspectors').removeAttr('validation');
    $(".cbkVehicle").removeAttr('validation');
    $('#drpReasonDeclined').removeAttr('validation');

    var ddlValue;

    if (visible) {
        document.getElementById("divVehiclesToInspection").style.display = "none";
        document.getElementById("divDocumentos").style.display = "block";
        $('#drpDocumentos').attr('validation', 'Required');
        ddlValue = $("#drpStatusQuotation").val();
    }
    else {

        document.getElementById("divVehiclesToInspection").style.display = "none";
        document.getElementById("divDocumentos").style.display = "none";

        $('#drpDocumentos').removeAttr('validation');

        ddlValue = '';
    }

    document.getElementById("hdnIllustrationStatus").value = ddlValue;
    $('#btnDeclinedByClient').attr('data-Status', ddlValue);
}

function ddlReasonDeclined(visible) {

    $('#drpInspectors').removeAttr('validation');
    $(".cbkVehicle").removeAttr('validation');
    $('#drpDocumentos').removeAttr('validation');

    var ddlValue;

    if (visible) {
        document.getElementById("divVehiclesToInspection").style.display = "none";
        document.getElementById("divDocumentos").style.display = "none";
        document.getElementById("divReason").style.display = "block";

        $("#drpReasonDeclined").attr("validation", "Required");

        ddlValue = $("#drpStatusQuotation").val();
    }
    else {
        document.getElementById("divVehiclesToInspection").style.display = "none";
        document.getElementById("divDocumentos").style.display = "none";
        $("#drpReasonDeclined").removeAttr('validation');

        ddlValue = '';
    }

    document.getElementById("hdnIllustrationStatus").value = ddlValue;
    $('#btnDeclinedByClient').attr('data-Status', ddlValue);
}

function ddlOtherStatus() {
    var ddlValue = $("#drpStatusQuotation").val();
    document.getElementById("hdnIllustrationStatus").value = ddlValue;
    $('#btnDeclinedByClient').attr('data-Status', ddlValue);
}

function StatusQuotationOnChange() {
    $("#drpStatusQuotation").on("change", function () {
        var v = $(this).val();

        $('#drpInspectors').removeAttr('validation');
        $('#drpDocumentos').removeAttr('validation');

        if (v == "DeclinedByClient") {
            document.getElementById("divReason").style.display = "block";
            document.getElementById("divVehiclesToInspection").style.display = "none";
            document.getElementById("divDocumentos").style.display = "none";

            $("#drpReasonDeclined").attr("validation", "Required");
            $('#drpReasonDeclined').val('-1');
        }
        else if (v == "MissingInspection") {
            document.getElementById("divVehiclesToInspection").style.display = "block";
            document.getElementById("divReason").style.display = "none";
            document.getElementById("divDocumentos").style.display = "none";

            $('#drpInspectors').attr('validation', 'Required');
            $('#drpInspectors').val('-1');
            $(".cbkVehicle").attr("validation", "Required");
            $("#btnExceuteStatusQuotationChanged").click();
        }
        else {

            if (v == "MissingDocuments") {
                document.getElementById("divDocumentos").style.display = "block";
                $('#drpDocumentos').attr('validation', 'Required');
                $('#drpDocumentos').val('-1');
            } else
                document.getElementById("divDocumentos").style.display = "none";

            document.getElementById("divReason").style.display = "none";
            document.getElementById("divVehiclesToInspection").style.display = "none";

            $("#drpReasonDeclined").removeAttr("validation");
            $(".cbkVehicle").removeAttr("validation");
        }

        document.getElementById("hdnIllustrationStatus").value = v;
    });
}

function calculatePercentaje() {
    $('#ddlPorcentajeDescuento').on('change', function () {

        var selectedIndex = $("select[id='ddlPorcentajeDescuento'] option:selected").val();
        var json = JSON.parse(selectedIndex);
        var prime = parseFloat($("#hdnCalculoPorcentaje").val());

        if (json != null) {

            var percentaje = parseFloat(json.DiscountRuleValue);
            var result = (prime * percentaje);


            $("#txtCalculatedAmount").val(isNaN(result) ? "0.00" : result);
        }
    });
}

function setFirstGroupTabs() {
    var tab = $("#hdnTabGroup").val();
    $('* [id*="ulGroupTabs"] li').removeClass("active");
    $('* [id*="' + tab + '"]').addClass("active");
}

function setIsPep(IsPep) {
    if (IsPep != null) {
        $('#hdnHasPep').val(IsPep);
    } else {
        $('#hdnHasPep').val('false');
    }
}

function ValidateTabForDeterminatePrintInvoce() {
    var result = true;
    var TabSelected = $("#hdnTabSelected").val();
    var IsApproveBySuscription = TabSelected == "lnkApprovedBySubscription";

    if (IsApproveBySuscription)
        result = ValidateCheck('#gvIllustration', 'Youcanonlyassignquote');

    return result;
}

