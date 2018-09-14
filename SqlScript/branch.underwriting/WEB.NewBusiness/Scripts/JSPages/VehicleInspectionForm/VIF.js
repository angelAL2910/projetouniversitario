﻿var bcIF = 'bodyContent_InspectionForm_',
    formIG = 'bodyContent_InspectionForm_informacionesGenerales1_',
    formOI = 'bodyContent_InspectionForm_otrasInformaciones1_',
    _fixheightInspeccion;

function pageLoad() {
    setUserAgent();

    $('.pop_up_wrapper').hide();

    setAccordeaons();

    $('#VehicleOperationCheck, #VCPPE, #VCPPI, #VCPPO, #VA1, #VA2, #VehicleUpholstery, #VehicleSecuritySystems, #VehicleComplement, #VIGL, #VIGR').find("tr:nth-child(odd)").css("background-color", "#ffffff");
    $('#VehicleOperationCheck, #VCPPE, #VCPPI, #VCPPO, #VA1, #VA2, #VehicleUpholstery, #VehicleSecuritySystems, #VehicleComplement, #VIGL, #VIGR').find("tr:nth-child(even)").css("background-color", "#e1f0d5");

    $('a[rel="prettyPhoto[pp_gal]"]').prettyPhoto({
        animation_speed: 'fast',
        modal: true,
        theme: 'light_rounded',
        slideshow: 5000,
        autoplay_slideshow: false,
        social_tools: false
    });

    $('.onlyNumbers').keypress(function (event) {
        return isNumber(event, this)
    });

    $('#acc1 li').fadeIn(2);

    $('.file-upload').on('click', function () {
        this.value = null;
    });

    $('.file-upload').on('change.bs.fileinput', function (event) {
        event.preventDefault();
        event.stopPropagation();
        setTimeout(function () {

            var base64string = '',
                documentdesc = '',
                documentname = '';

            $('div.fileinput-preview.thumbnail img').each(function (index) {
                if (!$(this).attr('thn')) {
                    base64string = $(this).attr('src');
                    documentdesc = $(this).parent('div.fileinput-preview.thumbnail').attr('documentdesc');
                    documentname = $(this).parent('div.fileinput-preview.thumbnail').attr('documentname');
                    $(this).attr('thn', 'thn');
                    $(this).parent().parent().attr('href', base64string);
                    $(this).parent().parent().attr('imagecar', documentname);
                    return;
                }
            });

            $('.label.errmsgvif').each(function (index) {
                if ($(this).attr('imagecar') == documentname) {
                    $(this).text('');
                    return false;
                }
            });

            savePhoto(base64string, documentdesc, documentname, event);

        }, 600);

        return false;
    });

    $("#txtTelefono").mask("(999) 999-9999");

    $("#txtTelefono").on("blur", function () {
        var last = $(this).val().substr($(this).val().indexOf("-") + 1);

        if (last.length == 5) {
            var move = $(this).val().substr($(this).val().indexOf("-") + 1, 1);

            var lastfour = last.substr(1, 4);

            var first = $(this).val().substr(0, 9);

            $(this).val(first + move + '-' + lastfour);
        }
    });

    var $div = document.getElementById("map");

    var $hdnLongitud = $("#hdnLongitudVEH");
    var $hdnLatitud = $("#hdnLatitudVEH");

    var sLongitud = $hdnLongitud.val();
    var sLatitud = $hdnLatitud.val();
    if (sLatitud != "0" && sLongitud != "0") {
        var LatLng = new google.maps.LatLng(sLatitud, sLongitud);
        initializeSubmitMap($div, LatLng);
    } else {
        var DireccionInspeccion = $("#txtDireccionInspeccion").val();
        searchAddress("Republica Dominicana, " + DireccionInspeccion, function (latlon) {
            var lat = latlon.lat;
            var lon = latlon.lng;
            $hdnLongitud.val(lat);
            $hdnLatitud.val(lon);
            initializeSubmitMap($div, latlon, false, true, false);
        });
    }

    //Call method to create Signature Panel
    $("#signatureparent").jSignature({
        //color: "#194E92",
        lineWidth: 3,
        width: '100%',
        height: 200
        //background-color:"#0f0"
    });

    //$("#signatureparent").jSignature('disable');

}

function DisabledSections(disabled) {
    if (disabled == 'true') {
        $("[id*=rb], [id*=txt], [type=file], #" + bcIF + "btnSave, #" + bcIF + "btnClean, #ddlVersion, #ddlTransmision, #ddlClase, #ddlTraccion, #ddlMileageKilometer").attr('disabled', 'disabled');
    } else {
        $("[id*=rb], [type=file], #" + formOI + "txtDictamenDanos, #" + formOI + "txtMontoRD, #" + formOI + "txtHoraCulminacion, #" + bcIF + "btnSave, #" + bcIF + "btnClean, #ddlVersion, #ddlTransmision, #ddlClase, #ddlTraccion, #ddlMileageKilometer").removeAttr('disabled');
        $('#' + formIG + 'txtModelo, #' + formIG + 'txtAno, #' + formIG + 'txtColor, #' + formIG + 'txtUso, #' + formIG + 'txtPlaca, #' + formIG + 'txtTipo, #' + formIG + 'txtChasis').attr('disabled', 'disabled');

        $("#" + formIG + "txtKilometraje").removeAttr('disabled');
        $("#" + formIG + "txtCilindros").removeAttr('disabled');
        $("#" + formIG + "txtCapacidad").removeAttr('disabled');
    }

    $("#txtUsuarioInspeccion").removeAttr('disabled');

    return false;
}

function cleanAll(informaciones_generales) {
    BeginRequestHandler();

    var date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ':' + seconds + ' ' + ampm;

    $('#' + formIG + 'txtHoraInicio').val(strTime);

    if (informaciones_generales != 'false') {
        $('#' + formIG + 'hdnModeloId').val('');
        $('#' + formIG + 'txtModelo').val('');
        $('#' + formIG + 'txtAno').val('');
        $('#' + formIG + 'txtColor').val('');
        $('#' + formIG + 'txtCilindros').val('');
        $('#' + formIG + 'txtPlaca').val('');
        $('#' + formIG + 'txtTipo').val('');
        $('#' + formIG + 'txtUso').val('');
        $('#' + formIG + 'txtKilometraje').val('');
        $('#' + formIG + 'txtCapacidad').val('');
        $('#' + formIG + 'txtChasis').val('');

        $('#txtTelefono').val('');
        $('#txtCorreoElectronico').val('');

        $('#ddlClase').val('0');
        $('#ddlVersion').val('0');
        $('#ddlTransmision').val('0');
        $('#ddlTraccion').val('0');
        $('#ddlMileageKilometer').val('0');
    }

    $('input:radio[id="' + formIG + 'rbSi"]').prop('checked', false);
    $('input:radio[id="' + formIG + 'rbNo"]').prop('checked', false);

    //Verificacion de Informaciones Generales
    $('input:radio[id^="' + formIG + 'VerificacionInformacionesGenerales_rb"]').prop('checked', false);

    //Tipo de Combustible
    //$('input:radio[id^="' + bcIF + 'tipoCombustible1_rb"]').prop('checked', false);

    //Verificacion Funcionamiento Vehiculo
    $('input:radio[id^="' + bcIF + 'verificacionFuncionamiento1_rb"]').prop('checked', false);

    //Verificacion Partes Fisicas
    $('input:radio[id^="' + bcIF + 'verificacionPartesFisicas1_rb"]').prop('checked', false);

    //Accesorios y Tapiceria
    $('input:radio[id^="' + bcIF + 'accesoriosTapiceria1_rb"]').prop('checked', false);

    //Sistemas de Seguridad y Complementos
    $('input:radio[id^="' + bcIF + 'sistemasSeguridadComplementos1_rb"]').prop('checked', false);

    //Fotos
    $('[id^="bodyContent_InspectionForm_Fotos1_"] img').css('height', '159px').css('width', '318px').prop('src', '').attr('imagealign', 'AbsMiddle').attr('imageCar', 'car01.png');

    //Otras Informaciones
    $('#' + formOI + 'txtDictamenDanos').val('');
    $('#txtUsuarioInspeccion').val('');
    $('input:radio[id^="' + formOI + '"]').prop('checked', false);

    $('.timePicker').timepicker({ 'step': 1, 'timeFormat': 'h:i A' }).timepicker('setTime', new Date());
    $('.onlyNumbers').keypress(function (event) { return isNumber(event, this); });

    EndRequestHandler();
}

function saveAll(mode) {
    BeginRequestHandler();

    if (mode == 'auto')
        clearInterval(auto_save_interval_id);

    var informacionesGenerales = new Object(),
        verificacionInformacionesGenerales = [],
        tipoCombustible = new Object(),
        funcionamiento = [],
        partesFisicas = new Object(),
        accesoriosTapiceria = new Object(),
        seguridadComplementos = new Object(),
        otrasInformaciones = new Object();

    verificacionInformacionesGenerales = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    tipoCombustible = {
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewDetailId: -1,
        checked: false,
        erase: true
    };

    funcionamiento = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewOptionDesc: '',
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    partesFisicas.Exterior = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewOptionDesc: '',
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    partesFisicas.Interior = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewOptionDesc: '',
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    partesFisicas.Otros = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewOptionDesc: '',
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    accesoriosTapiceria.Accesorios = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewOptionDesc: '',
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    accesoriosTapiceria.Tapiceria = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewOptionDesc: '',
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    seguridadComplementos.Seguridad = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    seguridadComplementos.Complementos = [{
        reviewGroupId: -1,
        reviewClassId: -1,
        reviewItemId: -1,
        reviewOptionId: -1,
        reviewDetailId: -1,
        checked: false,
        erase: true
    }];

    otrasInformaciones = {
        DictamenDanos: '',
        MontoRD: '',
        Impronta: '',
        SomeData: '',
        Sucursal: '',
        InspeccionNo: '',
        HoraCulminacion: '',
        SolicitanteInspeccion: '',
        DocumentoIdentidad: '',
        InspectorSuggestsAcceptRisk: false,
        UsuarioInspeccion: ''
    };

    var oMessage = {};
    var arrayMessage = [];

    if ($('#ddlMarca option:selected').val() == '0') {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheGeneralInformation';
        arrayMessage.push(oMessage);

    } else if ($('#ddlMarca option:selected').val() != '0' &&
                (
                    $('#' + formIG + 'txtCilindros').val() == '0' ||
                    $('#' + formIG + 'txtKilometraje').val() == '0' ||
                    $('#' + formIG + 'txtCapacidad').val() == '0' ||
                    $('#ddlVersion option:selected').val() == '0' ||
                    $('#ddlTransmision option:selected').val() == '0' ||
                    $('#ddlClase option:selected').val() == '0' ||
                    $('#ddlTraccion option:selected').val() == '0' ||
                    $('#ddlMileageKilometer option:selected').val() == '0' ||
                    (
                        !($('input:radio[id="' + formIG + 'rbSi"]').prop('checked')) &&
                        !($('input:radio[id="' + formIG + 'rbNo"]').prop('checked'))
                    )
                )
              ) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheGeneralInformation';
        arrayMessage.push(oMessage);
    }


    var capacidad = $('#' + formIG + 'txtCapacidad').val();
    if (!isInteger(capacidad)) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'CapacityShouldNotContainDecimal';
        arrayMessage.push(oMessage);

        EndRequestHandler();
        var titlex = $("#hdnLang").val() == "en" ? "Warning" : "Alerta";
        CustomDialogMessageExVIF(null, null, null, true, titlex, 'CapacityShouldNotContainDecimal');
        return false;
    }

    var cilindros = $('#' + formIG + 'txtCilindros').val();
    if (!isInteger(cilindros)) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'CylindersShouldNotContainDecimal';
        arrayMessage.push(oMessage);

        EndRequestHandler();
        var titlex = $("#hdnLang").val() == "en" ? "Warning" : "Alerta";
        CustomDialogMessageExVIF(null, null, null, true, titlex, 'CylindersShouldNotContainDecimal');
        return false;
    }

    var kilometraje = $('#' + formIG + 'txtKilometraje').val();
    if (!isInteger(kilometraje)) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'MileageShouldNotContainDecimal';
        arrayMessage.push(oMessage);

        EndRequestHandler();
        var titlex = $("#hdnLang").val() == "en" ? "Warning" : "Alerta";
        CustomDialogMessageExVIF(null, null, null, true, titlex, 'MileageShouldNotContainDecimal');
        return false;
    }

    var autoSaveMode = mode == 'auto' ? true : false;

    informacionesGenerales = {
        NumeroCotizacion: $('#' + formIG + 'txtNumeroCotizacion').val(),
        Pais: $('#' + formIG + 'txtPais').val(),
        Provincia: $('#' + formIG + 'txtProvincia').val(),
        Ciudad: $('#' + formIG + 'txtCiudad').val(),
        Fecha: $('#' + formIG + 'txtFecha').val().replace('.', ''),
        HoraInicio: $('#' + formIG + 'txtHoraInicio').val(),
        Inspector: $("#drpInspectors option:selected").text(),
        Asegurado: $('#' + formIG + 'txtAsegurado').val(),
        Intermediario: $('#' + formIG + 'txtIntermediario').val(),
        Marca: Number($('#ddlMarca option:selected').val().split('|')[0]),
        Modelo: Number($('#' + formIG + 'hdnModeloId').val()),
        Ano: Number($('#' + formIG + 'txtAno').val()),
        Color: $('#' + formIG + 'txtColor').val(),
        VersionId: Number($('#ddlVersion option:selected').val()),
        TransmisionId: Number($('#ddlTransmision option:selected').val()),
        TraccionId: Number($('#ddlTraccion option:selected').val()),
        ClaseId: Number($('#ddlClase option:selected').val()),
        Cilindros: Number(cilindros),
        Placa: $('#' + formIG + 'txtPlaca').val(),
        Tipo: $('#' + formIG + 'txtTipo').val(),
        Uso: $('#' + formIG + 'txtUso').val(),
        MileageKilometer: Number($('#ddlMileageKilometer option:selected').val()),
        Kilometraje: Number(kilometraje),
        Capacidad: Number(capacidad),
        MatriculaDocumentoLegalBL: $('input:radio[id="' + formIG + 'rbSi"]').prop('checked'),
        Chasis: $('#' + formIG + 'txtChasis').val(),
        AutoSaveMode: autoSaveMode,
        Inspeccionado: autoSaveMode ? false : true,
        Telefono: $('#txtTelefono').val(),
        CorreoElectronico: $('#txtCorreoElectronico').val(),
        InspectorID: $('#drpInspectors').val()
    };

    $('*[id^="' + formIG + 'VerificacionInformacionesGenerales_rb"]').each(function (index) {
        if ($(this).prop('checked')) {
            verificacionInformacionesGenerales.push({
                reviewGroupId: -1,
                reviewClassId: -1,
                reviewItemId: $(this).data('reviewItemId'),
                reviewOptionId: -1,
                reviewDetailId: -1,
                checked: $(this).data('reviewOptionDesc') == 'Si',
                erase: false
            });
        }
    });

    if (verificacionInformacionesGenerales.length < 12) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheVerificationOfGeneralInformation';
        arrayMessage.push(oMessage);
    }

    $('*[id^="' + bcIF + 'tipoCombustible1_rb"]').each(function (index) {
        if ($(this).prop('checked')) {
            tipoCombustible = {
                reviewGroupId: $(this).data('reviewGroupId'),
                reviewClassId: $(this).data('reviewClassId'),
                reviewItemId: $(this).data('reviewItemId'),
                reviewOptionId: $(this).data('reviewOptionId'),
                reviewDetailId: $(this).data('reviewDetailId'),
                checked: true,
                erase: false
            };
        }
    });

    if (!tipoCombustible.checked) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheFuelType';
        arrayMessage.push(oMessage);
    }

    $('*[id^="' + bcIF + 'verificacionFuncionamiento1_rb"]').each(function (index) {
        if ($(this).prop('checked')) {
            funcionamiento.push({
                reviewGroupId: $(this).data('reviewGroupId'),
                reviewClassId: $(this).data('reviewClassId'),
                reviewItemId: $(this).data('reviewItemId'),
                reviewOptionId: $(this).data('reviewOptionId'),
                reviewOptionDesc: $(this).data('reviewOptionDesc'),
                reviewDetailId: $(this).data('reviewDetailId'),
                checked: true,
                erase: false
            });
        }
    });

    if (funcionamiento.length < 9) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheOperationCheck';
        arrayMessage.push(oMessage);
    }

    $('*[id^="' + bcIF + 'verificacionPartesFisicas1_rb"]').each(function (index) {
        if ($(this).prop('checked')) {
            if ($(this).data('reviewClassDesc') == 'Exterior') {
                partesFisicas.Exterior.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewOptionDesc: $(this).data('reviewOptionDesc'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            } else if ($(this).data('reviewClassDesc') == 'Interior') {
                partesFisicas.Interior.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewOptionDesc: $(this).data('reviewOptionDesc'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            } else if ($(this).data('reviewClassDesc') == 'Otros') {
                partesFisicas.Otros.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewOptionDesc: $(this).data('reviewOptionDesc'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            }
        }
    });

    if ((partesFisicas.Exterior.length < 20) || (partesFisicas.Interior.length < 7) || (partesFisicas.Otros.length < 6)) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteThePhysicalParts';
        arrayMessage.push(oMessage);
    }

    $('*[id^="' + bcIF + 'accesoriosTapiceria1_rb"]').each(function (index) {
        if ($(this).prop('checked')) {
            if ($(this).data('reviewClassDesc') == 'Accesorios') {
                accesoriosTapiceria.Accesorios.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewOptionDesc: $(this).data('reviewOptionDesc'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            } else if ($(this).data('reviewClassDesc') == 'Tapiceria') {
                accesoriosTapiceria.Tapiceria.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewOptionDesc: $(this).data('reviewOptionDesc'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            }
        }
    });

    if (accesoriosTapiceria.Accesorios.length < 18 || accesoriosTapiceria.Tapiceria.length < 8) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheAccessoriesUpholstery';
        arrayMessage.push(oMessage);
    }

    $('*[id^="' + bcIF + 'sistemasSeguridadComplementos1_rb"]').each(function (index) {
        if ($(this).prop('checked')) {
            if ($(this).data('reviewClassDesc') == 'Seguridad') {
                seguridadComplementos.Seguridad.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            } else if ($(this).data('reviewClassDesc') == 'Complementos') {
                seguridadComplementos.Complementos.push({
                    reviewGroupId: $(this).data('reviewGroupId'),
                    reviewClassId: $(this).data('reviewClassId'),
                    reviewItemId: $(this).data('reviewItemId'),
                    reviewOptionId: $(this).data('reviewOptionId'),
                    reviewDetailId: $(this).data('reviewDetailId'),
                    checked: true,
                    erase: false
                });
            }
        }
    });

    if (seguridadComplementos.Seguridad.length < 9 || seguridadComplementos.Complementos.length < 9) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheSecuritySystemsComplement';
        arrayMessage.push(oMessage);
    }

    var count = 0;
    $('*[rel="prettyPhoto[pp_gal]"]').each(function (index) {
        if ($(this).attr('imageCar') == 'car01.png') {
            count += 1;
        }
    });

    if (count > 0) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteThePhotos';
        arrayMessage.push(oMessage);
    }


    if ($('#txtUsuarioInspeccion').val() == '') {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustIndicateInspectionUserName';
        arrayMessage.push(oMessage);
    }

    if (
            $('#' + formOI + 'txtDictamenDanos').val() == '' ||
            $('#' + formOI + 'txtDireccionInspeccion').val() == '' ||
            (
                !($('input:radio[id="' + formOI + 'rbSi"]').prop('checked')) &&
                !($('input:radio[id="' + formOI + 'rbNo"]').prop('checked'))
            )
       ) {
        oMessage = {};
        oMessage.ErrorType = "VehicleInspectionForm";
        oMessage.Field = 'YouMustCompleteTheOtherInformations';
        arrayMessage.push(oMessage);
    }

    var partialSave = arrayMessage.length > 0;
    informacionesGenerales.PartialSave = partialSave;

    //Verificar si se escribio una firma
    var HasSign = $("#signatureparent > div").eq(1).find("img:first").css("display") == "none";

    otrasInformaciones = {
        DictamenDanos: $('#' + formOI + 'txtDictamenDanos').val(),
        Sucursal: $('#' + formOI + 'txtSucursal').val(),
        HoraCulminacion: $('#' + formOI + 'txtHoraCulminacion').val(),
        InspectorSuggestsAcceptRisk: $('input:radio[id="' + formOI + 'rbSi"]').prop('checked'),
        UsuarioInspeccion: $('#txtUsuarioInspeccion').val(),
        FirmaCiente: HasSign ? SetImageDigitalSign() : ""
    };

    var dataToSend = {
        ValoresJson: $('#hdnValoresJSON').val(),
        InformacionesGenerales: informacionesGenerales,
        VerificacionInformacionesGenerales: verificacionInformacionesGenerales,
        TipoCombustible: tipoCombustible,
        Funcionamiento: funcionamiento,
        PartesFisicas: partesFisicas,
        AccesoriosTapiceria: accesoriosTapiceria,
        SeguridadComplementos: seguridadComplementos,
        OtrasInformaciones: otrasInformaciones
    };

    $.ajax({
        type: "POST",
        url: "VehicleInspectionForm.aspx/saveAll",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend),
        dataType: "json",
        beforeSend: function () {
            BeginRequestHandler();
        },
        success: function (result) {
            setTimeout(function () {
                var msg = result.d.replace('"', ''),
                    saved = msg.indexOf('Exception') == -1,
                    title = $("#hdnLang").val() == "en" ? "Information" : "Información";

                $('#hdnSaved').val(saved);

                var ms = saved ? msg : null;
                var er = !saved ? msg : null;

                if (mode != 'auto') {
                    EndRequestHandler();

                    CustomDialogMessageWithCallBack(er, function () { }, title, null, ms);
                    $('div.ui-dialog-buttonset button').addClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only').attr('role', 'button').attr('aria-disabled', 'false').html('<span class="ui-button-text">OK</span>');

                    //if (arrayMessage.length == 0) {
                    //    cleanAll();
                    //    $('#ddlMarca').val(0);
                    //    DisabledSections('true');
                    //}
                }
            }, 100);

            $.ajax({
                type: "POST",
                url: "VehicleInspectionForm.aspx/InspectionCompleted",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(dataToSend),
                dataType: "json",
                beforeSend: function () {
                    BeginRequestHandler();
                },
                success: function (result) {
                    if (result.d) {
                        var missingInspection = $('#hdnMissingInspection').val().toString().trim().toLowerCase() == "true";
                        if (missingInspection) {
                            $("#btnSendToSubscription").removeAttr("disabled");
                            $("#signatureparent").jSignature('enable');
                        }
                    }
                    else {
                        $("#btnSendToSubscription").attr("disabled", "disabled");
                        $("#signatureparent").jSignature('disable');
                    }
                },
                error: function (result) {
                    EndRequestHandler();
                    var titlex = $("#hdnLang").val() == "en" ? "We're sorry, an unexpected error has occurred." : "Lo sentimos, ha ocurrido un error inesperado.";
                    $('#hdnSaved').val('false');
                    CustomDialogMessageExVIF(result.responseText, null, null, true, titlex, null);
                }
            });

        },
        error: function (result) {
            EndRequestHandler();
            var titlex = $("#hdnLang").val() == "en" ? "We're sorry, an unexpected error has occurred." : "Lo sentimos, ha ocurrido un error inesperado.";
            $('#hdnSaved').val('false');
            CustomDialogMessageExVIF(result.responseText, null, null, true, titlex, null);
        }
    });

    $('.timePicker').timepicker({ 'step': 1, 'timeFormat': 'h:i A' }).timepicker('setTime', new Date());
    $('.onlyNumbers').keypress(function (event) { return isNumber(event, this); });

    return false;
}

function SetImageDigitalSign() {
    var dataBase64Png = $("#signatureparent").jSignature('getData');
    var $imgSignatureImage = $("#imgSignatureImage");
    $imgSignatureImage.attr('src', dataBase64Png);

    return dataBase64Png;
}

function saveDraft() {
    return saveAll('auto');
}

function isNumber(evt, element) {

    var charCode = (evt.which) ? evt.which : event.keyCode

    if (
        (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // "-" CHECK MINUS, AND ONLY ONE.
        (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // "." CHECK DOT, AND ONLY ONE.
        (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function PhotoReviews() {
    var dataToSend = { ValoresJson: $('#hdnValoresJSON').val() };

    $.ajax({
        type: "POST",
        url: "VehicleInspectionForm.aspx/PhotoReviews",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend),
        dataType: "json",
        beforeSend: function () {
            BeginRequestHandler();
        },
        success: function (result) {
            if (result.d.length > 0) {
                BeginRequestHandler();

                $('.boton_descargar_foto').removeAttr('disabled');

                var fotos = [];
                fotos = result.d;

                $('div.fileinput-preview.thumbnail img').each(function (index) {
                    var foto = fotos[index];
                    if (foto != undefined) {
                        var fotoId = foto.imgId + '1';
                        $(this).attr('thn', 'thn');
                        $(this).attr('align', 'absmiddle');
                        $(this).css('height', '159px').css('width', '318px');
                    }
                });

                for (var i = 0; i < fotos.length; i++) {
                    var foto = fotos[i];
                    var fotoId = foto.imgId + '1';

                    $('a[rel="prettyPhoto[pp_gal]"]').each(function (index) {
                        if ($(this).attr('id') == foto.lnkId) {
                            $(this).attr('href', foto.Base64String);
                            $(this).attr('imageCar', foto.DocumentName);
                        }
                    });

                    $('div.fileinput-preview.thumbnail').each(function (index) {
                        if ($(this).attr('id') == foto.lnkId) {
                            $(this).attr('documentdesc', foto.DocumentDesc);
                            $(this).attr('documentname', foto.DocumentName);
                            $(this).attr('thn', 'thn');
                        }
                    });

                    $('div.fileinput-preview.thumbnail img').each(function (index) {
                        if ($(this).attr('id') == fotoId && $(this).parent().parent().attr('imageCar') == foto.DocumentName) {
                            $(this).attr('src', foto.Base64String).css('display', 'block');
                            $(this).attr('imageCar', foto.DocumentName);
                        }
                    });
                }
            }
            else {
                $('.boton_descargar_foto').attr('disabled', 'disabled');
            }

            EndRequestHandler();
        },
        error: function (result) {
            EndRequestHandler();
            $('.boton_descargar_foto').attr('disabled', 'disabled');
            var titlex = $("#hdnLang").val() == "en" ? "We're sorry, an unexpected error has occurred." : "Lo sentimos, ha ocurrido un error inesperado.";
            CustomDialogMessageExVIF(result.responseText, null, null, true, titlex, null);
        }
    });
    return false;
}

function OptionsReviews(registrationDocument) {

    if (registrationDocument != null) {
        var checked = registrationDocument == 'true' ? true : false;
        $('input:radio[id="' + formIG + 'rbSi"]').prop('checked', checked);
        $('input:radio[id="' + formIG + 'rbNo"]').prop('checked', !checked);
    }

    var dataToSend = { ValoresJson: $('#hdnValoresJSON').val() };

    $.ajax({
        type: "POST",
        url: "VehicleInspectionForm.aspx/OptionsReviews",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend),
        dataType: "json",
        beforeSend: function () {
            BeginRequestHandler();
        },
        success: function (result) {
            BeginRequestHandler();

            var datos = result.d;

            if (datos.VIGCount > 0) {
                var verificacionInformacionesGenerales = new Object();

                verificacionInformacionesGenerales = datos.VerificacionInformacionesGenerales;
                $('*[id^="' + formIG + 'VerificacionInformacionesGenerales_rb"]').each(function (index) {
                    for (var i = 0; i < verificacionInformacionesGenerales.length; i++) {
                        if (($(this).data('reviewOptionDesc') == 'Si') &&
                            ($(this).data('reviewItemId') == verificacionInformacionesGenerales[i].ReviewItemId) &&
                            verificacionInformacionesGenerales[i].Checked) {
                            $(this).prop('checked', true);
                        } else if (($(this).data('reviewOptionDesc') == 'No') &&
                                   ($(this).data('reviewItemId') == verificacionInformacionesGenerales[i].ReviewItemId) &&
                                   !verificacionInformacionesGenerales[i].Checked) {
                            $(this).prop('checked', true);
                        }
                    }
                });
            }

            if (datos.Count > 0) {
                var tipoCombustible = new Object(),
                    funcionamiento = [],
                    partesFisicas = new Object(),
                    accesoriosTapiceria = new Object(),
                    seguridadComplementos = new Object();

                tipoCombustible = datos.TipoCombustible;
                funcionamiento = datos.Funcionamiento;
                partesFisicas = datos.PartesFisicas;
                accesoriosTapiceria = datos.AccesoriosTapiceria;
                seguridadComplementos = datos.SeguridadComplementos;

                $('input:radio[id^="' + bcIF + 'tipoCombustible1_rb"]').each(function (index) {
                    if (($(this).data('reviewGroupId') == tipoCombustible.ReviewGroupId) &&
                        ($(this).data('reviewOptionId') == tipoCombustible.ReviewOptionId) &&
                        ($(this).data('reviewClassId') == tipoCombustible.ReviewClassId) &&
                        ($(this).data('reviewItemId') == tipoCombustible.ReviewItemId)) {
                        $(this).prop('checked', true);
                        $(this).attr("data-review-detail-id", tipoCombustible.ReviewDetailId)
                    }
                });

                for (var i = 0; i < funcionamiento.length; i++) {
                    $('input:radio[id^="' + bcIF + 'verificacionFuncionamiento1_rb"]').each(function (index) {
                        if (($(this).data('reviewGroupId') == funcionamiento[i].ReviewGroupId) &&
                            ($(this).data('reviewOptionId') == funcionamiento[i].ReviewOptionId) &&
                            ($(this).data('reviewClassId') == funcionamiento[i].ReviewClassId) &&
                            ($(this).data('reviewItemId') == funcionamiento[i].ReviewItemId)) {
                            $(this).prop('checked', true);
                            $(this).attr("data-review-detail-id", funcionamiento[i].ReviewDetailId)
                        }
                    });
                }

                $('input:radio[id^="' + bcIF + 'verificacionPartesFisicas1_rb"]').each(function (index) {
                    if ($(this).data('reviewClassDesc') == 'Exterior') {
                        for (var i = 0; i < partesFisicas.Exterior.length; i++) {
                            if (($(this).data('reviewGroupId') == partesFisicas.Exterior[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == partesFisicas.Exterior[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == partesFisicas.Exterior[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == partesFisicas.Exterior[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", partesFisicas.Exterior[i].ReviewDetailId)
                            }
                        }
                    } else if ($(this).data('reviewClassDesc') == 'Interior') {
                        for (var i = 0; i < partesFisicas.Interior.length; i++) {
                            if (($(this).data('reviewGroupId') == partesFisicas.Interior[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == partesFisicas.Interior[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == partesFisicas.Interior[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == partesFisicas.Interior[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", partesFisicas.Interior[i].ReviewDetailId)
                            }
                        }
                    } else if ($(this).data('reviewClassDesc') == 'Otros') {
                        for (var i = 0; i < partesFisicas.Otros.length; i++) {
                            if (($(this).data('reviewGroupId') == partesFisicas.Otros[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == partesFisicas.Otros[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == partesFisicas.Otros[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == partesFisicas.Otros[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", partesFisicas.Otros[i].ReviewDetailId)
                            }
                        }
                    }
                });

                $('input:radio[id^="' + bcIF + 'accesoriosTapiceria1_rb"]').each(function (index) {
                    if ($(this).data('reviewClassDesc') == 'Accesorios') {
                        for (var i = 0; i < accesoriosTapiceria.Accesorios.length; i++) {
                            if (($(this).data('reviewGroupId') == accesoriosTapiceria.Accesorios[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == accesoriosTapiceria.Accesorios[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == accesoriosTapiceria.Accesorios[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == accesoriosTapiceria.Accesorios[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", accesoriosTapiceria.Accesorios[i].ReviewDetailId)
                            }
                        }
                    } else if ($(this).data('reviewClassDesc') == 'Tapiceria') {
                        for (var i = 0; i < accesoriosTapiceria.Tapiceria.length; i++) {
                            if (($(this).data('reviewGroupId') == accesoriosTapiceria.Tapiceria[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == accesoriosTapiceria.Tapiceria[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == accesoriosTapiceria.Tapiceria[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == accesoriosTapiceria.Tapiceria[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", accesoriosTapiceria.Tapiceria[i].ReviewDetailId)
                            }
                        }
                    }
                });

                $('input:radio[id^="' + bcIF + 'sistemasSeguridadComplementos1_rb"]').each(function (index) {
                    if ($(this).data('reviewClassDesc') == 'Seguridad') {
                        for (var i = 0; i < seguridadComplementos.Seguridad.length; i++) {
                            if (($(this).data('reviewGroupId') == seguridadComplementos.Seguridad[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == seguridadComplementos.Seguridad[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == seguridadComplementos.Seguridad[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == seguridadComplementos.Seguridad[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", seguridadComplementos.Seguridad[i].ReviewDetailId)
                            }
                        }
                    } else if ($(this).data('reviewClassDesc') == 'Complementos') {
                        for (var i = 0; i < seguridadComplementos.Complementos.length; i++) {
                            if (($(this).data('reviewGroupId') == seguridadComplementos.Complementos[i].ReviewGroupId) &&
                                ($(this).data('reviewOptionId') == seguridadComplementos.Complementos[i].ReviewOptionId) &&
                                ($(this).data('reviewClassId') == seguridadComplementos.Complementos[i].ReviewClassId) &&
                                ($(this).data('reviewItemId') == seguridadComplementos.Complementos[i].ReviewItemId)) {
                                $(this).prop('checked', true);
                                $(this).attr("data-review-detail-id", seguridadComplementos.Complementos[i].ReviewDetailId)
                            }
                        }
                    }
                });
            }
            EndRequestHandler();
        },
        error: function (result) {
            EndRequestHandler();
            var titlex = $("#hdnLang").val() == "en" ? "We're sorry, an unexpected error has occurred." : "Lo sentimos, ha ocurrido un error inesperado.";
            CustomDialogMessageExVIF(result.responseText, null, null, true, titlex, null);
        }
    });

    return false;
}

function OtherInformation() {
    var dataToSend = { ValoresJson: $('#hdnValoresOIJSON').val() };

    $.ajax({
        type: "POST",
        url: "VehicleInspectionForm.aspx/OtherInformation",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend),
        dataType: "json",
        beforeSend: function () {
            BeginRequestHandler();
        },
        success: function (result) {
            if (result.d !== undefined) {
                BeginRequestHandler();
                var formOI = 'bodyContent_InspectionForm_otrasInformaciones1_';

                $('#' + formOI + 'txtDictamenDanos').val(result.d.DictamenDanos);
                $('#txtUsuarioInspeccion').val(result.d.UsuarioInspeccion);
                $('#' + formOI + 'txtSucursal').val(result.d.Sucursal);
                $('#' + formOI + 'txtHoraCulminacion').val(result.d.HoraCulminacion);
                if (result.d.InspectorSuggestsAcceptRisk !== null) {
                    $('#' + formOI + 'rbSi').prop('checked', result.d.InspectorSuggestsAcceptRisk);
                    $('#' + formOI + 'rbNo').prop('checked', !result.d.InspectorSuggestsAcceptRisk);
                }

                var $imgSignatureImage = $("#imgSignatureImage");
                var $pnSignature = $("#pnSignature");
                if (result.d.FirmaCiente != '') {
                    $imgSignatureImage.attr('src', result.d.FirmaCiente);
                    $imgSignatureImage.show();
                    $pnSignature.hide();
                } else {
                    $pnSignature.show();
                    $imgSignatureImage.hide();
                }
            }
            EndRequestHandler();
        },
        error: function (result) {
            EndRequestHandler();
            var titlex = $("#hdnLang").val() == "en" ? "We're sorry, an unexpected error has occurred." : "Lo sentimos, ha ocurrido un error inesperado.";
            CustomDialogMessageExVIF(result.responseText, null, null, true, titlex, null);
        }
    });
    return false;
}

function SetSucursal(txt) {
    $('#' + formOI + 'txtSucursal').val(txt).attr('disabled', 'disabled');
    return false;
}
function SetUserInspector(txt) {
    $("#txtUsuarioInspeccion").val(txt);
    return false;
}
function SetHoraInicio(txt) {
    $('#' + formIG + 'txtHoraInicio').val(txt).attr('disabled', 'disabled');
    return false;
}

function isInteger(value) {
    return Math.floor(value) == value && $.isNumeric(value);
}

function savePhoto(base64string, documentdesc, documentname, event) {
    if (base64string != '' && documentdesc != '' && documentname != '') {

        BeginRequestHandler();

        var informacionesGenerales = new Object(),
            otrasInformaciones = new Object();

        var capacidad = Math.floor($('#' + formIG + 'txtCapacidad').val()),
            cilindros = Math.floor($('#' + formIG + 'txtCilindros').val()),
            kilometraje = Math.floor($('#' + formIG + 'txtKilometraje').val());

        informacionesGenerales = {
            NumeroCotizacion: $('#' + formIG + 'txtNumeroCotizacion').val(),
            Pais: $('#' + formIG + 'txtPais').val(),
            Provincia: $('#' + formIG + 'txtProvincia').val(),
            Ciudad: $('#' + formIG + 'txtCiudad').val(),
            Fecha: $('#' + formIG + 'txtFecha').val().replace('.', ''),
            HoraInicio: $('#' + formIG + 'txtHoraInicio').val(),
            Inspector: $('#' + formIG + 'txtInspector').val(),
            Asegurado: $('#' + formIG + 'txtAsegurado').val(),
            Intermediario: $('#' + formIG + 'txtIntermediario').val(),
            Marca: Number($('#ddlMarca option:selected').val().split('|')[0]),
            Modelo: Number($('#' + formIG + 'hdnModeloId').val()),
            Ano: Number($('#' + formIG + 'txtAno').val()),
            Color: $('#' + formIG + 'txtColor').val(),
            VersionId: Number($('#ddlVersion option:selected').val()),
            TransmisionId: Number($('#ddlTransmision option:selected').val()),
            TraccionId: Number($('#ddlTraccion option:selected').val()),
            ClaseId: Number($('#ddlClase option:selected').val()),
            Cilindros: Number(cilindros),
            Placa: $('#' + formIG + 'txtPlaca').val(),
            Tipo: $('#' + formIG + 'txtTipo').val(),
            Uso: $('#' + formIG + 'txtUso').val(),
            MileageKilometer: Number($('#ddlMileageKilometer option:selected').val()),
            Kilometraje: Number(kilometraje),
            Capacidad: Number(capacidad),
            MatriculaDocumentoLegalBL: $('input:radio[id="' + formIG + 'rbSi"]').prop('checked'),
            Chasis: $('#' + formIG + 'txtChasis').val(),
            AutoSaveMode: false,
            Inspeccionado: false,
            Telefono: $('#txtTelefono').val(),
            CorreoElectronico: $('#txtCorreoElectronico').val()
        };

        otrasInformaciones = {
            DictamenDanos: $('#' + formOI + 'txtDictamenDanos').val(),
            Sucursal: $('#' + formOI + 'txtSucursal').val(),
            HoraCulminacion: $('#' + formOI + 'txtHoraCulminacion').val(),
            InspectorSuggestsAcceptRisk: $('input:radio[id="' + formOI + 'rbSi"]').prop('checked'),
            UsuarioInspeccion: $('#txtUsuarioInspeccion').val()
        };

        var dataToSend = {
            ValoresJson: $('#hdnValoresJSON').val(),
            InformacionesGenerales: informacionesGenerales,
            photo: {
                Base64String: base64string,
                DocumentDesc: documentdesc.toUpperCase(),
                DocumentName: documentname
            },
            OtrasInformaciones: otrasInformaciones
        };

        $.ajax({
            type: "POST",
            url: "VehicleInspectionForm.aspx/savePhoto",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(dataToSend),
            dataType: "json",
            beforeSend: function () {
                BeginRequestHandler();
            },
            success: function (result) {
                EndRequestHandler();
                if (result.d.length > 0) {
                    var msg = result.d.replace('"', ''),
                        saved = msg == 'saved';
                    if (!saved) {
                        setTimeout(function () {
                            savePhotoTryAgain(dataToSend, documentname, event);
                        }, 1200);
                    }
                }
            },
            error: function (result) {
                setTimeout(function () {
                    savePhotoTryAgain(dataToSend, documentname, event);
                }, 1200);
            }
        });
    }
    return false;
}

function savePhotoTryAgain(dataToSend, documentname, event) {
    event.preventDefault();
    event.stopPropagation();

    BeginRequestHandler();

    $.ajax({
        type: "POST",
        url: "VehicleInspectionForm.aspx/savePhoto",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToSend),
        dataType: "json",
        beforeSend: function () {
            BeginRequestHandler();
        },
        success: function (result) {
            EndRequestHandler();
            if (result.d.length > 0) {
                var msg = result.d.replace('"', ''),
                    saved = msg == 'saved';
                if (!saved) {
                    $('.errmsgvif').each(function (index) {
                        if ($(this).attr('imagecar') == documentname) {
                            $(this).html(msg);
                            return false;
                        }
                    });
                }
                else {
                    $('.img-vif').each(function (index) {
                        if ($(this).attr('documentname') == documentname) {
                            $(this).css('display', 'block');
                            return false;
                        }
                    });
                }
            }
        },
        error: function (result) {
            EndRequestHandler();
            var titlex = $("#hdnLang").val() == "en" ? "We're sorry, an unexpected error has occurred." : "Lo sentimos, ha ocurrido un error inesperado.";
            var msg = JSON.parse(result.responseText).d;
            $('#spnMessages[imagecar="' + documentname + '"]').html(msg);
            CustomDialogMessageExVIF(msg, null, null, true, titlex, null);
        }
    });
    return false;
}

function fillDrops(versionId, classId, transmissionTypeId, wheelDriveTypeId, mileageKilometerId) {
    $('#ddlVersion').val(versionId);
    $('#ddlTransmision').val(transmissionTypeId);
    $('#ddlClase').val(classId);
    $('#ddlTraccion').val(wheelDriveTypeId);
    $('#ddlMileageKilometer').val(mileageKilometerId);
}

function fillInformacionVehiculo(modelo, ano, color, uso, placa, tipoVehiculo, chasis, cilindros, odometro, capacidad) {
    $('#' + formIG + 'txtModelo').val(modelo);
    $('#' + formIG + 'txtAno').val(ano);
    $('#' + formIG + 'txtColor').val(color);
    $('#' + formIG + 'txtUso').val(uso);
    $('#' + formIG + 'txtPlaca').val(placa);
    $('#' + formIG + 'txtTipo').val(tipoVehiculo);
    $('#' + formIG + 'txtChasis').val(chasis);
    $('#' + formIG + 'txtCilindros').val(cilindros);
    $('#' + formIG + 'txtKilometraje').val(odometro);
    $('#' + formIG + 'txtCapacidad').val(capacidad);
}

isMobileBrowser = function () {
    var isMobile = {
        Android: function () {
            return navigator.userAgent.match(/Android/i);
        },
        BlackBerry: function () {
            return navigator.userAgent.match(/BlackBerry/i);
        },
        iOS: function () {
            return navigator.userAgent.match(/iPhone|iPad|iPod/i);
        },
        Opera: function () {
            return navigator.userAgent.match(/Opera Mini/i);
        },
        Windows: function () {
            return navigator.userAgent.match(/IEMobile/i);
        },
        any: function () {
            return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
        }
    };
    return isMobile.any();
}

setUserAgent = function () {
    if (isMobileBrowser) {
        window.navigator.__defineGetter__('userAgent', function () {
            return 'Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36';
        });
    }
}

DlgConfirmWithCallBackInspectionForm = function (obj, Message, pwidth, pheight, Func, FuncNo, key) {
    if ($('#' + bcIF + 'btnSave').attr('disabled')) { return; }

    var divToCreate = "<div id='dvConfirmDialog'></div>";

    $("body").append(divToCreate);

    var divCreated = $("#dvConfirmDialog");

    //Botones en Ingles
    var pButtonsEn = {
        "Yes": function () {
            if (Func != null)
                Func();
            __doPostBack($(obj).attr('name'), '');
            $(divCreated).dialog("close");
        },
        "No": function () {
            if (FuncNo != null)
                FuncNo();
            $(divCreated).dialog("close");
        }
    };

    //Botenes en español
    var pButtonsEs = {
        "Si": function () {
            if (Func != null)
                Func();
            __doPostBack($(obj).attr('name'), '');
            $(divCreated).dialog("close");
        },
        "No": function () {
            if (FuncNo != null)
                FuncNo();
            $(divCreated).dialog("close");
        }
    };

    $(divCreated).dialog(
    {
        draggable: true,
        height: pheight == null ? 126 : pheight,
        width: pwidth == null ? 402 : pwidth,
        show: {
            effect: "puff",
            duration: 260
        },
        hide: {
            effect: "puff",
            duration: 260
        },
        buttons: $("#hdnLang").val() == "en" ? pButtonsEn : pButtonsEs,
        title: $("#hdnLang").val() == "en" ? "Confirmation" : "Confirmación",
        autoOpen: false,
        //position: { my: "center", at: "center", of: $("body") },
        position: { my: "center", at: "center", of: $($("#containerMessage").val()) },
        modal: true,
        open: function () {
            AddPopIframe();
            RelocatePops();
        },
        create: function () {
            $(".ui-dialog-buttons").css("z-index", "13000");
            $(".ui-dialog-titlebar > span").css("height", "25px");
            $(".ui-dialog-titlebar > span").css("line-height", "24px");
            $(".ui-dialog-titlebar > span").css({ "color": "white", "text-align": "center", "width": "100%" });
            $(".ui-dialog-titlebar > button").css("background", "transparent");
            $(".ui-button-icon-primary").css("display", "none");
            $(".ui-dialog-titlebar > button").css("background-image", "url('../../Content/images/close_pop_up.png')");
            $(".ui-dialog-titlebar > button").css("border", "0");
            $(".ui-dialog-titlebar > button").mouseover(function () {
                $(this).removeClass("ui-state-hover");
            });
            $(".ui-dialog-titlebar > button").css({ "width": "24px", "height": "24px" });
            $(".ui-dialog-titlebar").addClass("HeaderPopup");

            var ys = $("#hdnLang").val() == "en" ? 'Yes' : 'Si'

            $($('div.ui-dialog-buttonset button').addClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only').attr('role', 'button').attr('aria-disabled', 'false')[0]).html('<span class="ui-button-text">' + ys + '</span>');
            $($('div.ui-dialog-buttonset button').addClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only').attr('role', 'button').attr('aria-disabled', 'false')[1]).html('<span class="ui-button-text">No</span>');
        },
        close: function () {
            $(divCreated).remove();
        }
    }).dialog("open");

    //Traductor
    getTranslate(key, function (data) {
        var DialogMessage = Message == "" || Message == null ? data.d : Message;
        divCreated.html(DialogMessage);
    });

    return false;
};

DlgConfirmInspectionForm = function (obj, Message, pwidth, pheight) {

    var divToCreate = "<div id='dvConfirmDialog'></div>";

    $("body").append(divToCreate);

    var divCreated = $("#dvConfirmDialog");

    //Botones en Ingles
    var pButtonsEn = {
        "Yes": function () {
            $('#hdnClean').val('true');

            if (!$(obj).is("a"))
                __doPostBack(obj.name, '');
            else {
                BeginRequestHandler();
                if (!$.browser.mozilla)
                    eval($(obj).prop("pathname"));
                else
                    eval($(obj).prop("href"));
            }

            $(divCreated).dialog("close");
        },
        "No": function () {
            $('#hdnClean').val('false');
            $(divCreated).dialog("close");
        }
    };

    //Botenes en español
    var pButtonsEs = {
        "Si": function () {
            $('#hdnClean').val('true');

            if (!$(obj).is("a"))
                __doPostBack(obj.name, '');
            else {
                BeginRequestHandler();
                if (!$.browser.mozilla)
                    eval($(obj).prop("pathname"));
                else
                    eval($(obj).prop("href"));
            }

            $(divCreated).dialog("close");
        },
        "No": function () {
            $('#hdnClean').val('false');
            $(divCreated).dialog("close");
        }
    };

    $(divCreated).dialog(
    {
        draggable: true,
        height: pheight == null ? 150 : pheight,
        width: pwidth == null ? 402 : pwidth,
        show: {
            effect: "puff",
            duration: 260
        },
        hide: {
            effect: "puff",
            duration: 260
        },
        buttons: $("#hdnLang").val() == "en" ? pButtonsEn : pButtonsEs,
        title: $("#hdnLang").val() == "en" ? "Confirmation" : "Confirmación",
        autoOpen: false,
        position: { my: "center", at: "center", of: $("#containerMessage") != null ? $($("#containerMessage").val()) : "body" },
        modal: true,
        open: function () {
            AddPopIframe();
            RelocatePops();
        },
        create: function () {
            $(".ui-dialog-buttons").css("z-index", "13000");
            $(".ui-dialog-titlebar > span").css("height", "25px");
            $(".ui-dialog-titlebar > span").css("line-height", "24px");
            $(".ui-dialog-titlebar > span").css({ "color": "white", "text-align": "center", "width": "100%" });
            $(".ui-dialog-titlebar > button").css("background", "transparent");
            $(".ui-button-icon-primary").css("display", "none");
            $(".ui-dialog-titlebar > button").css("background-image", "url('../../Content/images/close_pop_up.png')");
            $(".ui-dialog-titlebar > button").css("border", "0");
            $(".ui-dialog-titlebar > button").mouseover(function () {
                $(this).removeClass("ui-state-hover");
            });

            $(".ui-dialog-titlebar > button").css({ "width": "24px", "height": "24px" });
            $(".ui-dialog-titlebar").addClass("HeaderPopup");

            var ys = $("#hdnLang").val() == "en" ? 'Yes' : 'Si'
            $($('div.ui-dialog-buttonset button').addClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only').attr('role', 'button').attr('aria-disabled', 'false')[0]).html('<span class="ui-button-text">' + ys + '</span>');
            $($('div.ui-dialog-buttonset button').addClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only').attr('role', 'button').attr('aria-disabled', 'false')[1]).html('<span class="ui-button-text">No</span>');

            // $("body").prepend($(".ui-dialog"));

        },
        close: function () {
            $(divCreated).remove();
        }
    }).dialog("open");

    var key = "ActionTaskWarning";

    //Traductor
    getTranslate(key, function (data) {
        var DialogMessage = Message == "" || Message == null ? data.d : Message;
        divCreated.html(DialogMessage);
    });

    return false;
};

CustomDialogMessageExVIF = function (Message, pwidth, pheight, isModal, titlex, key) {

    $("#dvMessage").remove();

    pcloseText = $("#hdnLang").val() == "en" ? "Close" : "Cerrar";

    var divToCreate = "<div id='dvMessage'></div>";

    $("body").append(divToCreate);

    var divCreated = $("#dvMessage");

    if (key != null) {
        if (key != "jsonMessage") {
            //Traductor
            getTranslate(key, function (data) {
                divCreated.html(data.d);
            });
        } else {
            //Procesar mensaje json
            getTranslate(Message, function (data) {
                divCreated.html(data.d);
            });
        }
    }
    else
        divCreated.html(Message);

    titles = (titlex == null) ? "" : titlex;
    isModal = (isModal == null) ? true : isModal;
    pheight = pheight == null ? 'auto' : pheight;
    pwidth = pwidth == null ? 350 : pwidth;

    $(divCreated).dialog({
        title: titles,
        autoOpen: false,
        resizable: false,
        position: { my: "center", at: "center", of: $("#containerMessage") != null ? $($("#containerMessage").val()) : "body" },
        height: pheight,
        width: pwidth,
        modal: isModal,
        closeText: pcloseText,
        show: {
            effect: "puff",
            duration: 260
        },
        hide: {
            effect: "puff",
            duration: 260
        },
        buttons: {
            "OK": function () {
                $(this).dialog("close");
            }
        },
        create: function () {
            $(".ui-dialog-buttons").css("z-index", "13000");
            $(".ui-dialog-titlebar > span").css("height", "25px");
            $(".ui-dialog-titlebar > span").css("line-height", "24px");
            $(".ui-dialog-titlebar > span").css({ "color": "white", "text-align": "center", "width": "100%", "font-family": "Arial, Helvetica, sans-serif" });
            $(".ui-dialog-titlebar > button").css("background", "transparent");
            $(".ui-button-icon-primary").css("display", "none");
            $(".ui-dialog-titlebar > button").css("background-image", "url('../../Content/images/close_pop_up.png')");
            $(".ui-dialog-titlebar > button").css("border", "0");
            $(".ui-dialog-titlebar > button").mouseover(function () {
                $(this).removeClass("ui-state-hover");
            });

            $(".ui-dialog-titlebar > button").css({ "width": "24px", "height": "24px" });
            $(".ui-dialog-titlebar").addClass("HeaderPopup");

            $('div.ui-dialog-buttonset button').addClass('ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only').attr('role', 'button').attr('aria-disabled', 'false').html('<span class="ui-button-text">OK</span>');
        },
        close: function () {
            $(divCreated).remove();
        },
        open: function () {
            $(divCreated).html(Message);
            AddPopIframe();
            RelocatePops();
        }
    }).dialog("open");

};

function BackToIllustrationList2(Message, Title) {
    var funcCallBack = function () {
        location.href = "Illustrations.aspx";
    };
    CustomDialogMessageWithCallBack(Message, funcCallBack, Title, funcCallBack, null);
}