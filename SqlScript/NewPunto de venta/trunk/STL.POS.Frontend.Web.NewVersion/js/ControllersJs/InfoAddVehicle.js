﻿InitializeVehicle = function () {
    InitializeChosen();

    //Agregando clase requerido(que pone un borde rojo) a los campos que son requeridos
    applyEventsRequiredFields();
    //

    $('.effectiveDateStart').datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: new Date()
    });

    $('.effectiveDateEnd').datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: new Date()
    });


    var hdnIsInclusion = $("#hdnIsInclusion").val();

    var d = $.datepicker.formatDate(getCurrentDateFormat(), moment(GlobalcurrentStartDateSelected, getCurrentDateMomentFormat()).toDate());
    $('.effectiveDateStart').val(d);

    if (hdnIsInclusion == "S") {
        var hdnVigencyEndDate = $("#hdnVigencyEndDate").val();
        var hdnVigencyStartDate = $("#hdnVigencyStartDate").val();
        d = $.datepicker.formatDate(getCurrentDateFormat(), moment(hdnVigencyEndDate).toDate());
        d2 = $.datepicker.formatDate(getCurrentDateFormat(), moment(hdnVigencyStartDate).toDate());
        $('.effectiveDateEnd').val(d);
        $(".effectiveDateEnd").datepicker("destroy");
        $('.effectiveDateStart').val(d2);
        $('.effectiveDateStart').datepicker('option', 'maxDate', d);

    } else {
        d = $.datepicker.formatDate(getCurrentDateFormat(), moment(GlobalcurrentEndDateSelected, getCurrentDateMomentFormat()).toDate());
        $('.effectiveDateEnd').val(d);
        $('.effectiveDateEnd').datepicker('option', 'maxDate', d);
        //$('.effectiveDateStart').datepicker('option', 'maxDate', d);
    }

    activateDatePickers();

    frmVehicleAddValidations();

    $("#ddlPaymentFreq").change(function () {
        var $this = $(this);
        $("#PaymentFreq").val($this.val());
    });

    $("#btnSendToGlobal").off("click");
    $("#btnSendToGlobal").click(function () {

        var _isValidToPassNextStep = isValidToPassNextStep();
        if (_isValidToPassNextStep == false) {
            return _isValidToPassNextStep;
        }       

        var qid = parseInt($('#quotationID').val());

        if (qid > 0) {

            $("#btnSaveVehicleAndNext").trigger('click');
            
            if ($("#frmVehicle").valid()) {
                
                $.ajax({
                    url: "/Home/SendQuotToGlobal",
                    type: "POST",
                    data: { quotationID: qid },
                    cache: false,
                    success: function (data, textStatus, jqXHR) {

                        if (data.success) {

                            //reemplazar div y mostrar partialview
                            $.ajax({
                                url: "/Home/FinalStepToInbox",
                                data: { quotationID: qid },
                                contentType: 'application/html; charset=utf-8',
                                type: 'GET',
                                dataType: 'html',
                                async: false,
                                success: function (html) {

                                    $("#dvHeaderOption").hide();
                                    $("#dvContainer").html(html);

                                    changeOptionFilter();
                                },
                                error: function (req, status, error) {
                                }
                            });

                            window.history.replaceState({}, '', '/Home/Index');
                            var isfinanced = $("#isFinancedPolicy").val() == "True";
                            //showSucess([data.message], "Cotización", (!isfinanced) ? function () { location.reload(); } : null, false);
                            showSucess([data.message], "Cotización", null, false);
                        }
                        else {
                            showError([data.message], "Enviando Cotizacion a la Bandeja de Auto");
                        }
                    },
                    error: function (data, textStatus, jqXHR) {
                        if (data.messageError) {
                            showError([data.messageError]);
                        } else {
                            var textError = data + " " + textStatus + " " + jqXHR;
                            showError([textError]);
                        }
                    }
                });
            }
        }

        return false;
    });


    $("#ddlColor").trigger("change");
    $("#ddlPaymentFreq").trigger("change");
    $("#ddlDriver").trigger("change");
    $("#chassis").trigger("focusout");
    $("#placa").trigger("focusout");

    $(".continueWithAgent").show();


    var hdnTotalVehicles = parseInt($('#hdnTotalVehicles').val());
    var hdnTotalVehiclesCompleted = parseInt($('#hdnTotalVehiclesCompleted').val());

    if (hdnTotalVehicles == 1) {

        if ($("#btnSendToGlobal").length > 0) {
            $("#btnSendToGlobal").show();
        }
        else if ($(".btnSendToPaymentCheckOut").length > 0) {
            $(".btnSendToPaymentCheckOut").show();
        }
    }
    else {
        if (hdnTotalVehiclesCompleted >= hdnTotalVehicles) {
            if ($("#btnSendToGlobal").length > 0) {
                $("#btnSendToGlobal").show();
            }
            else if ($(".btnSendToPaymentCheckOut").length > 0) {
                $(".btnSendToPaymentCheckOut").show();
            }
        }
        else {
            if ($("#btnSendToGlobal").length > 0) {
                $("#btnSendToGlobal").hide();
            }
            else if ($(".btnSendToPaymentCheckOut").length > 0) {
                $(".btnSendToPaymentCheckOut").hide();
            }
        }
    }
};

SaveVehicleAndNext = function () {
    $("#btnSubmitForm").click();
}

function ViewFinancedContract() {
    var QuotationId = $("#quotationID").val();
    showFinancedContractTH(QuotationId, function (data) {
        window.open(data, '_blank');
    });
}

function showFinancedContractTH(QuotationId, CallBack) {
    $.ajax({
        url: "/Home/ShowFinancedContractTH",
        data: JSON.stringify({ QuotationId: QuotationId }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        beforeSend: function () {

        },
        success: CallBack,
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
        }
    });
};

function frmVehicleAddValidations() {

    $("#frmVehicle").validate(
        {
            rules: {
                plate: {
                    required: true
                },
                chassis: {
                    required: true
                },
                ddlColor: {
                    required: true
                },
                ddlPaymentFreq: {
                    required: true
                },
                ddlDriver: {
                    required: true
                },
                StartDate: {
                    required: true
                },
                EndDate: {
                    required: true
                }
                //
            },
            messages: {
                plate: {
                    required: 'La Placa es requerida.'
                },
                chassis: {
                    required: 'El Chasis es requerido.'
                },
                ddlColor: {
                    required: 'El Color es requerido.'
                },
                ddlPaymentFreq: {
                    required: 'La Frequencia de Pago es requerida.'
                },
                ddlDriver: {
                    required: 'La Conductor Principal es requerido.'
                },
                StartDate: {
                    required: 'La Fecha Inicio de Vigencia es requerida.'
                },
                EndDate: {
                    required: 'La Fecha Fin de Vigencia es requerida.'
                }
            }
        });
}

function checkChassisOrPlateLaw() {

    var _isValidToPassNextStep = isValidToPassNextStep();
    if (_isValidToPassNextStep == false) {
        return _isValidToPassNextStep;
    }

    var qid = parseInt($('#quotationID').val());
    var pass = false;

    if (qid > 0) {

        if ($("#frmVehicle").valid()) {

            $("#btnSaveVehicleAndNext").trigger('click');

            $.ajax({
                url: "/Home/CheckChassisPlateLawProducts",
                type: "POST",
                data: { quotationID: qid },
                cache: false,
                async: false,
                success: function (data, textStatus, jqXHR) {

                    if (data.success) {
                        pass = true;
                    }
                    else {
                        showError([data.message], "Revisando Chasis y/o Placa");
                    }
                },
                error: function (data, textStatus, jqXHR) {
                    if (data.messageError) {
                        showError([data.messageError]);
                    } else {
                        var textError = data + " " + textStatus + " " + jqXHR;
                        showError([textError]);
                    }
                }
            });
        }
    }

    return pass;
}

function activateDatePickers() {
    var hdnIsInclusion = $("#hdnIsInclusion").val();

    $(".effectiveDateStart").change(function () {

        var $this = $(this);
        var makeDate = $this.val().replace('.', '');//getCorrectDateFormat($this.val());

        var neDat = $.datepicker.formatDate(getCurrentDateFormat(), moment(makeDate, getCurrentDateMomentFormat()).toDate());
        //var neDatMax = getNewDateYear(makeDate);

        var neDatMax = $.datepicker.formatDate(getCurrentDateFormat(), moment(makeDate, getCurrentDateMomentFormat()).add(12, 'months').startOf('day').toDate());

        if (hdnIsInclusion != "S") {
            $('.effectiveDateEnd').datepicker('option', 'minDate', neDat);
            $('.effectiveDateEnd').datepicker('option', 'maxDate', neDatMax);//Un Ano a partir de la Fecha Seleccionada
        }

        var startDate = moment(neDat, getCurrentDateMomentFormat());
        var endDate = moment(startDate).add(12, 'months').startOf('day');

        if (hdnIsInclusion != "S") {

            var d = $.datepicker.formatDate(getCurrentDateFormat(), moment(endDate, getCurrentDateMomentFormat()).toDate());
            $('.effectiveDateEnd').val(d);
            $('.effectiveDateStart').datepicker('option', 'maxDate', d);

            var requoting = CheckEffectiveDatesLessThanYear();

            if (requoting) {
                CallRequoting();
            }
        }
    });

    if (hdnIsInclusion != "S") {

        $(".effectiveDateEnd").change(function () {

            var $this = $(this);
            var makeDate = $this.val().replace('.', '');//getCorrectDateFormat($this.val());


            var startDate = moment(makeDate, getCurrentDateMomentFormat());

            var neDat = $.datepicker.formatDate(getCurrentDateFormat(), moment(startDate, getCurrentDateMomentFormat()).toDate());
            $('.effectiveDateStart').datepicker('option', 'maxDate', neDat);

            var requoting = CheckEffectiveDatesLessThanYear();

            if (requoting) {
                CallRequoting();
            }
        });
    }
}

function CheckEffectiveDatesLessThanYear() {

    var efdt = $('.effectiveDateStart').val() !== undefined ? $('.effectiveDateStart').val() : "";
    var efdf = $('.effectiveDateEnd').val() !== undefined ? $('.effectiveDateEnd').val() : "";

    if (efdt !== "" && efdf != "") {

        var makeDateT = efdt; //getCorrectDateFormat(efdt);
        var makeDateF = efdf; //getCorrectDateFormat(efdf);

        var dt = moment(makeDateT, getCurrentDateMomentFormat());//.toDate();
        var df = moment(makeDateF, getCurrentDateMomentFormat());//.toDate();

        //var diff = df.diff(dt, 'years'); // 1

        GlobalcurrentStartDateSelected = moment(makeDateT, getCurrentDateMomentFormat()).format(getCurrentDateMomentFormat()); //(moment(dt, getCurrentDateTimeMomentFormat()).format(getCurrentDateTimeMomentFormat()));
        GlobalcurrentEndDateSelected = moment(makeDateF, getCurrentDateMomentFormat()).format(getCurrentDateMomentFormat());//(moment(df, getCurrentDateTimeMomentFormat()).format(getCurrentDateTimeMomentFormat()));

        return true;
    }
    return false;
}

function isValidToPassNextStep() {

    var hdnTotalVehicles = parseInt($('#hdnTotalVehicles').val());
    var hdnTotalVehiclesCompleted = parseInt($('#hdnTotalVehiclesCompleted').val());
    var hdnTotalVehiclesCompleted2 = $('#hdnTotalVehiclesCompleted2').val();

    if (hdnTotalVehicles > 1) {

        if (hdnTotalVehiclesCompleted2 == "0") {

            if (hdnTotalVehiclesCompleted < hdnTotalVehicles) {
                showError(['Debe completar la informaciones de todos los Vehículos.'], "Completar la información Vehículos");
                return false;
            }
        }
    }
}