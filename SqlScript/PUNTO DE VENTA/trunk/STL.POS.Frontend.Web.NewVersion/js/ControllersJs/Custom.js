﻿var GlobalData = null;
var GlobalDataUsages = null;
var GlobalDataProductsByUsage = null;
var GlobalAllProducts = null;
var GlobalDataCoverages = null;
var GlobalDataProductLimits = null;
var GlobalDataDeductibleList = null;
var GlobalDataXVehicle = null;
var GlobalDataPopupSelectedServices = null;
var GLobalPopupAnnualTotal = null;
var GlobalpercentageToInsure = 100;
var AllVehicleDataToSave = null;
var GlobalSecuenciaVehicleSysflex = 0;
var vehicleRandomID = null;
var isEditingVehicle = false;
var firstTime = true;
var GlobalCurrentIsc = 16;
var hasServices = true;
var PercentByQtyVehicle = 0;
var isFlotilla = false;
var changedDateBirth = false;
var changedSex = false;
var changedForeingLicenceDriver = false;
var LoadVehicle = false;
var LoadVehicleCurrentID = 0;
var LoadVehicleCurrentSecuenciaVehicleSysflex = 0;
var LoadVehicleQuantity = 1;
var GlobalcurrentStartDateSelected = null;
var GlobalcurrentEndDateSelected = null;
var wasLoadVehicle = false;
var GlobalServicesSelected = null;
var loadSurcharge = null;
var GlobalAllowRequoting = true;
var GlobalAppMode = "";
var GlobalChangeForModeLey = false;
//-----------
var PaymentStatus = '';
var PaymentFromCardnet = '';
var PaymentSuccess = '';
var failInsentingQuotationOnSysFlexOrVO = '';
var PaymentMessage = '';
var PolicyNumber = '';
var AuthorizationCode = '';
var PolicyNumberPayment = '';
var connection = null;
var pathOpenfireHostname = '';
var chUsername = '';
var clientUsername = '';
var chatParams = {};
var GlobalIsMobile = false
var messageTO = '';
var messageId = 0;
var GlobalAgentDirectSales = false;
var AmountPayByClient = 0;
var GlobalEventFired = false;
var GlobalSaveVehicleOnly = true;
var GlobalVehicleDelete = false;
var GlobalOnlyForEmission = true;
var alreadyTrigger = false;
var isLoadingQuot = false;
var isEventAdded = false;

$(document).ready(function () {

    $('#headCober').addClass('disabledAccordionTab');
    $('#headVH').addClass('disabledAccordionTab');

    GetCurrentIsc();

    InitializeCustom();

    if (GlobalAppMode != "LEYMODE" && GlobalAppMode != "FULLMODE") {
        getAgentsList();
    }

    getCountriesByBl();
    getBlByCountry();
    redirectByBl();

    //getChatParameters();//Hasta que se decida si se usara el chat o no

    $(document).on('change', '#filtroHistorico', function () {

        InitializeChosen();

        window.history.replaceState({}, document.title, "/Home/Index");

        var selected = $(this).val();
        switch (selected) {
            case "2"://Historico de cotizaciones
                if ($('#IsCustomer').val() == 'false')
                    LoadQuotationSearch();
                else
                    $('#ppCustomerFilter').modal('show');
                break;
            case "3":
                LoadInclusion(false, 'INCLUSIONES');
                break;
            case "4":
                LoadInclusion(false, 'EXCLUSIONES');
                break;
            case "5":
                LoadInclusion(false, 'CAMBIOS');
                break;
            case "7":
                LoadCotRenov(false, 'Propuesta Recuperación');
                break;
            default://Nueva Cotizacin
                window.location.reload();
                break;
        }
    });

    $('#headVH').click(function (event) {

        var d = $(this);
        if (d.hasClass('disabledAccordionTab')) {
            showWarning(['Debe guardar las informaciones del Conductor Principal dando click al boton Siguiente.'], 'Guardar información conductor principal');
            $("#hdnChangedSomethingClient").val("S");
            return false;
        }
    });

    $('#headCober').click(function (event) {

        var d = $(this);
        if (d.hasClass('disabledAccordionTab')) {

            var headVH = $('#headVH').hasClass("disabledAccordionTab");

            if (isEditingVehicle) {
                showWarning(['Debe guardar los cambios realizados al Vehículo dando click al boton Editar Vehículo.'], 'Cambios Vehículo');
                return false;
            }
            else if (!headVH) {
                showWarning(['Debe agregar un Vehículo para poder ver esta sección.'], 'Agregar Vehículo');
                return false;
            } else {
                showWarning(['Debe guardar las informaciones del Conductor Principal dando click al boton Siguiente.'], 'Guardar información conductor principal');
            }
            $("#hdnChangedSomethingClient").val("S");
            return false;
        }
    });

    $('#headInfoCC').click(function (event) {
        var d = $(this);
        if (d.hasClass('disabledAccordionTab')) {

            if (isEditingVehicle) {
                showWarning(['Esta sección esta bloqueada hasta que termine los cambios al Vehículo dando click al boton Editar Vehículo.'], 'Cambios Vehículo');
                return false;
            }
        }
    });

    $(".continueWithAgent").hide();

    $("#ddlLogNewUsers").change(function (data) {
        var $this = $(this);

        $.ajax({
            url: "/Login/ReloginUser",
            async: false,
            data: {
                userdata: $this.val()
            },
            success: function (data) {

                if (data.success) {
                    location.reload();
                }
            }
        });

    });

    $("#chat_btn, #chat_btn_Main").click(function () {
        $('#ChatProfile').html('Buscando agentes...');
        $('#ChatProfilePleaseWait').html('Por favor Espere...');
        if ($('#chat_dtl').is(":visible") != true) {
            connect();
            CreateNewQueue();
            $("#chat_dtl").show();
        }
    });

    $("#close_Chat").click(function () {
        var result = CloseChatConnection();
        if (result) {
            ClearChat();
            $("#chat_dtl").hide();
        }
    });

    $("#call_btn").click(function () {
        getPhoneTypes();
        $('#ppCustomerCallback').modal('show');
    });

    $("#SendCallback").click(function () {
        return SendCallback();
    });

    $(document).on('change', '#PhoneType', function () {
        var selected = $(this).val();
        var $CountryCodeCallback = $('#CountryCodeCallback');
        var $CityCodeCallback = $('#CityCodeCallback');
        var $NumToCall = $('#NumToCall');

        $CityCodeCallback.prop('disabled', false);
        $NumToCall.prop('disabled', false);

        switch (selected) {
            case "1"://Telefono mobil
                $CountryCodeCallback.prop('disabled', false);
                break;
            case "2":
                $CountryCodeCallback.prop('disabled', true);
                break;
            default://Telefono mobil
                $CountryCodeCallback.val('');
                $CityCodeCallback.val('');
                $NumToCall.val('');

                $CountryCodeCallback.prop('disabled', true);
                $CityCodeCallback.prop('disabled', true);
                $NumToCall.prop('disabled', true);
                break;
        }
    });

    $("#sendMessage").click(function () {
        var $userMessage = $('#userMessage');
        var msg = $userMessage.val();
        if (msg == '') {
            return false;
        }

        if (messageTO == '' || messageTO == null) {
            showError(["En este momento no hay agentes conectados, por favor intente más tarde"], "No hay usuarios conectados");
            return;
        }
        AddMessage('user', msg);


        sendMsg(chUsername + pathOpenfireHostname, messageTO, msg, $("#hdnChatId").val());

    });
    $('#userMessage').keypress(function (e) {
        if (e.which == 13) {//valido si se tecleó la tecla <Enter>
            $('#sendMessage').click();//Ejecuto el evento clic del boton enviar mensaje
            return false;
        }
    });
    $("#closeCustomerCallback").click(function () {
        ClearCallBack(true);
    });

    function connect() {
        chUsername = $('#hdnDefaultUserChat').val();

        pathOpenfireHostname = $('#hdnPathOpenfireHostname').val();
        var jid = chUsername + pathOpenfireHostname + "/StateTrustChat-Web";

        $.ajax({
            url: "/Home/ValidateOpenFireUserSession",
            method: "POST",
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            async: false,
            data: JSON.stringify({ 'pUser': chUsername.toLowerCase() })
            , success: function (data) {
                if (data.name == null || data.name == '') {
                    if (jid != null && chUsername != null) {
                        connection.connect(jid, chUsername, onConnect);
                        $('#ChatProfile').html('Buscando agentes...');
                    }
                } else {
                    console.log('SESSION DETECTED');
                }
            }, error: function (e) {
                console.log('ERROR, NOT CONNECTED');
            }
        });
    }

    function onBroadcast(msg) {
        var to = $(msg).attr('to');
        var from = $(msg).attr('from');
        var elems = $(msg).find('body');
        var user = Strophe.getText(elems[0]);
        var fromUser = from.split("@", 1);
        console.log("BC FROM: " + fromUser);
        var toUser = to.split("@", 1);
        console.log("BC TO: " + toUser);
        var usxrArr = null;
        var usxr = null;
        var chxt = null;
        if ($(msg).find('StUser').length > 0) {
            console.log($(msg).find('StUser').length);
            if ($(msg).find('StUser').find('agentname').length > 0) {
                var StUserUserId = $(msg).find('StUser').find('agentname');
                console.log("StUserAgente: " + StUserUserId);
                usxrArr = Strophe.getText(StUserUserId[0]);
                console.log("usxrArr: " + usxrArr);
                usxr = usxrArr.split("@", 1);
                console.log("usxr: " + usxr);
            }
            var StUserChatId = $(msg).find('StUser').find('chatid');
            console.log("StUserChatId: " + StUserChatId);
            chxt = Strophe.getText(StUserChatId[0]);
            console.log("chxt: " + chxt);
        }
        var hlSubj = $(msg).find('subject');
        var subText = Strophe.getText(hlSubj[0]);
        var tempChatIdB = null;
        var tmpDomain = pathOpenfireHostname.substring(1, pathOpenfireHostname.length)

        console.log("DATA TO: " + to + ", FROM: " + from + ", fromUser: " + fromUser + ", clientUsername: " + clientUsername + ", ELEMs: " + elems + ", USER: " + user + ", frUSER: " + fromUser + ", toUSER: " + toUser + ", SUBJECT: " + subText);
        if (fromUser != null) {
            //								var user_one = fromUser.toLowerCase();
            var user_two = clientUsername + '';
            var user_wea = user_two.toLowerCase();
            //								console.log("DATA LOWER: " + user_one + " == " + user_two + "?");
            if (fromUser == user_wea) {
                if (user == "com.statetrust.atlantica.assigned") {
                    sendCustomMessage('headline', 'mobile_agents@broadcast.' + tmpDomain, chUsername + '' + pathOpenfireHostname, fromUser[0], '1', '2');
                }
                else if (user == "com.statetrust.atlantica.send_close_chat") {
                    // MODIFICAR PARA QUE MUESTRE MENSAJE DE
                    // CHAT CERRADO

                    $("#ch-" + clientUsername).remove();

                    changeStatus(pkChat, 'H');

                    $('#nomClientInfo').html("");
                    $('#paisClientInfo').html("");
                    $('#codClientInfo').html("");
                    $('#telClientInfo').html("");
                    $("#client-policies").DataTable().clear().draw();
                    // Bloqueando controles
                    $("#close-chat").attr("disabled", "disabled");
                    $("#send-message").attr("disabled", "true");
                    $("#typedMessage").attr("disabled", "true");

                    activees.delete(fromUser[0]);

                    var msgBubble = "<li class=\"left clearfix admin_chat\">"
                        + "<div class=\"bubble you\"><p> "
                        + "Este chat ha sido terminado por el cliente."
                        + "</p>"
                        + "<div class=\"chat_time pull-left\">"
                        + "</div></div></li>";

                    $('#ch-' + clientUsername).append(msgBubble);

                    // Desconectarse
                    alertify.alert().setting({
                        'label': 'Aceptar',
                        'title': 'Chat completado!',
                        'message': 'El usuario ' + clientUsername + ' ha terminado el chat. Puede salir de esta página',
                        'onok': function () { $("#chatDiv").html("<strong style='font-family: arial; color: red;'>Recuerde: Ya puede cerrar esta página.</strong>"); }
                    }).show();
                    disconnect();
                } else {
                    if (toUser[0] === fromUser[0]) {
                        console.log('EQUAL:' + fromUser[0] + ':' + toUser[0]);
                    } else {
                        console.log('NOTEQUAL:' + fromUser[0] + ':' + toUser[0]);
                        $("#u-" + user).remove();
                        $("#ch-" + user).remove();

                    }
                }
            }
        }
        return true;
    }

    //connect();

    function disconnect() {
        connection.flush();
        connection.disconnect();
        $('#ChatProfile').html('Desconectado');
    }

    function onConnect(status) {
        var str = '';
        IS_CONNECTED = false;
        if (status == Strophe.Status.CONNECTING) {
            console.log('Strophe is connecting.');
        } else if (status == Strophe.Status.CONNFAIL) {
            console.log('Strophe failed to connect.');
        } else if (status == Strophe.Status.DISCONNECTING) {
            console.log('Strophe is disconnecting.');
        } else if (status == Strophe.Status.DISCONNECTED) {
            console.log('Strophe is disconnected.');
        } else if (status == Strophe.Status.CONNECTED) {
            console.log('Strophe is connected.');
            setTimeout(function () {
            }, 2000);
            IS_CONNECTED = true;
            connection.send($pres());

            //connection.addHandler(onMessageNow, null, 'message','chat');
            connection.addHandler(onBroadcast, null, 'message', 'headline');
            connection.addHandler(onReceiveMessage, null, 'message', 'chat');
            //var msg = "El usuario " + chUsername + " necesita asistencia";

            //if ($('#chat_dtl').is(":visible") != true) {
            //    sendMsg("anon_014836005394839@atl-srv40.atlantica.do", "mobile_agents@broadcast.atl-srv40.atlantica.do", msg, $("#hdnChatId").val());
            //    AddMessage('Admin', "En breve le atendemos.");
            //}
        }
    }

    $(document).on('focusout', '#CouponCode', function () {

        var isLeyOrFullMode = (GlobalAppMode == "FULLMODE" || GlobalAppMode == "LEYMODE");

        var isEmpty = ($(this).val() === '');

        if (isLeyOrFullMode) {

            if (isEmpty) {

                var parentDiv = $("#contactForm").parent().parent();
                parentDiv.removeClass('requerido');
                parentDiv.addClass('requerido');

                removeErrorBorderClass($("#contactForm"), true);

                if ($("#contactForm").val() !== '') {
                    $("#contactForm").trigger('change');
                }
            }
        }

        ProcessCoupon($(this).val(), 0);
    });

    if ($("#hdnPaymentFromCardnet").val()) {
        ProcessResponsePaymentCardnet();
    }

    //Setiando Version de la app
    $("#spVersionApp").html("(App Ver. 35.0.0)");
    //

    $(document).on('click', '.coverageExplicationBig', function () {
        $(".nProcuctosbtn").trigger('click');
    });

    if (GlobalAppMode == "LEYMODE") {
        $("#CouponCode").trigger('focusout');
    }

    $(document).on("click", ".ShowOrNotExplication", function () {
        var $btn = $(this);
        var $li_elem = $btn.parent().parent();

        if ($btn.find('i.large').html() == "visibility_off") {

            $li_elem.find('div.notice').hide();
            $li_elem.find('a.coverageExplicationBig').hide();

            $btn.find('i.large').html("visibility");
        }
        else if ($btn.find('i.large').html() == "visibility") {

            $li_elem.find('div.notice').show();
            $li_elem.find('a.coverageExplicationBig').show();

            $btn.find('i.large').html("visibility_off");
        }
    });

    $(document).on("click", "#btnSendVerCode", function () {
        SendVerificationCode();

        GlobalEventFired = false;
    });

    $(document).on("focusout", "#VerificationCode", function () {
        if (!GlobalEventFired) {
            ValidateCode();
        }
    });

    $(document).on("keypress", "#VerificationCode", function (e) {
        if (e.which == 13 && !GlobalEventFired) {//valido si se tecleó la tecla <Enter>

            ValidateCode();
            //GlobalEventFired = true;
            return false;
        }
    });

    $(document).on("keypress", "#referredByPolicy", function (e) {
        if (e.which == 13 && !alreadyTrigger) {//valido si se tecleó la tecla <Enter>

            var v = $("#referredByPolicy").val();
            if (v) {
                getReferredInfoByPolicy(v);
            }
            return false;
        }
    });

    $(document).on('click', '#QuotTypeNormal', function () {

        var quotationID = $('#quotationID').val();

        if (quotationID !== '' && $('#QuotTypeNormal').is(':checked')) {
            setFlotillaMode(false);
            location.reload();
            return false;
        }

        $(".ac2").show();
        $(".ac3").show();
        $(".ac4").hide();

        $("#hdnAlreadyChecked").val('N');
    });

    $(document).on('click', '#QuotTypeFlotilla', function () {

        var $this = $(this);
        var xx = $("#hdnAlreadyChecked").val();
        if ($this.is(':checked') && xx === 'N') {
            $("#hdnAlreadyChecked").val('S');
        } else if ($this.is(':checked') && xx === 'S') {
            return true;
        }

        var quotationID = $('#quotationID').val();

        if (quotationID !== '' && $('#QuotTypeFlotilla').is(':checked') && isLoadingQuot !== true) {
            setFlotillaMode(true);
            location.reload();
            return false;
        }

        if (isEventAdded == false) {
            changesEvents();
        }

        //Agregando clase requerido(que pone un borde rojo) a los campos que son requeridos
        $('.putErrorBorderFlot').addClass('requerido');
        applyEventsRequiredFields(true);
        //

        $(".ac4").show();
        $(".ac2").hide();
        $(".ac3").hide();

        //Generando el RandomId
        vehicleRandomID = Math.floor((Math.random() * -20000) + (-1));

        assingRandomId();

        //Agregando evento al estacionamiento
        var $select_elem = $("#ddlStoreCar");
        $select_elem.empty();
        $select_elem.append('<option value=""></option>');

        var allStore = $("#ddlStoreCarHidden option");
        $.each(allStore, function (idx, obj) {
            $select_elem.append("<option value='" + obj.value + "'>" + obj.text + "</option>");
        });
        $select_elem.trigger("change.select2");
        //

        //Agregando evento a los recargos    
        $select_elem = $("#ddlSurchargePercent");
        $select_elem.empty();
        $select_elem.append('<option value="">Seleccione</option>');

        var allSurcharge = $("#ddlSurchargePercentHidden option");
        $.each(allSurcharge, function (idx, obj) {
            $select_elem.append("<option value='" + obj.value + "'>" + obj.text + "</option>");
        });
        $select_elem.trigger("change.select2");
        //        
        CollectData_Flot(vehicleRandomID);
    });

    //---NEW FLOTILLA PROCESS--- 

    $(document).on('click', '.btnNextVehicle', function () {

        vehicleRandomID = Math.floor((Math.random() * -20000) + (-1));

        var $this = $(this);
        var tbl = $("#tblAllVehicle");
        var $Tbody = tbl.find("tbody");
        var $trLast = $Tbody.find("tr:last");
        var newTr = $('<tr>').attr('id', 'trVehicle_' + vehicleRandomID).addClass('trVehicle').attr('data-vehiclerandomid', vehicleRandomID);
        newTr = generateNewRow(newTr);

        $trLast.after(newTr);

        generateDinamycEvents();

        $('div.table-responsive').animate({ scrollLeft: '0' }, 300);

        CollectData_Flot(vehicleRandomID);

        applyEventsRequiredFields(true);
    });

    $(document).on('click', '.btnDeleteVehicle', function () {

        var $this = $(this);
        var randomID = $this.data('vehiclerandomid');

        var trLen = $(".trVehicle").length;
        if (trLen == 1) {
            showWarning(['Debe existir al menos un vehículo']);
            return false;
        }

        var current = altFind(AllVehicleDataToSave, function (item) {
            return item.randomId == randomID
        });

        if (current != undefined) {
            AllVehicleDataToSave = AllVehicleDataToSave.filter(function (item) {
                return item.randomId != randomID
            });
        }

        var quotationCoreNumber = getQuotationCoreNumber();
        var vehicleID = current.Id;

        if (current.SecuenciaVehicleSysflex > 0) {
            $.ajax({
                url: '/Home/DeleteVehicleOnSysflex',
                type: 'POST',
                dataType: 'json',
                data: { SecuenciaVehicleSysflex: current.SecuenciaVehicleSysflex, quotationCoreNumber: quotationCoreNumber, vehicleID: vehicleID },
                async: false,
                success: function (data) {
                    if (data == "ERROR") {
                        showError(['A ocurrido un error Eliminando el Vehículo'], 'Eliminando Vehículo');
                    }
                }
            });
        }

        //remuevo el Vehículo de la seccion de Vehículos
        var tr = getHtmlElementByClass("trVehicle", randomID);
        tr.remove();

        GlobalVehicleDelete = true;
    });

    //---NEW FLOTILLA PROCESS---
});

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function InizitializeControls(callgetMainOptions) {

    $('.normalDatepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: new Date(),
        onSelect: function (dateText) {
            var $parent = $(this).parent();
            $parent.addClass("is-dirty");
            $parent.removeClass('requerido');
            $parent.removeClass('erarequerido');
            $parent.addClass('erarequerido');
        }
    });

    //Edad maxima para poder asegurar un Vehículo (18 anos)
    var currDate = moment(new Date());
    var minDate = currDate.add(18 * -1, 'years');
    //

    $('.dateOfBirth.datepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: "-80:+0",
        maxDate: new Date(minDate),
        onSelect: function (dateText) {
            var $parent = $(this).parent();
            $parent.addClass("is-dirty");
            $parent.removeClass('requerido');
            $parent.removeClass('erarequerido');
            $parent.addClass('erarequerido');

            if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                changedDateBirth = true;
                disableAccordionsSections(true, true);
            }
        }
    });

    /*Mascara para los telefonos*/
    $("[phonenumber = 'PhoneNumber10']").inputmask("1-999-999-9999");

    /*Mascara para valores decimales*/
    $("[decimal='decimal']").inputmask({ alias: 'decimal', groupSeparator: ',', autoGroup: true, repeat: 9, digits: 2, allowMinus: false, allowPlus: false, rightAlign: true });

    if (callgetMainOptions == undefined || callgetMainOptions == null) {
        getMainOptions();
    }
}

function InitializeChosen() {

    $('.chosen-select-deselect').select2({
        theme: "bootstrap",
        allowClear: true,
        minimumResultsForSearch: 10,
        width: "95%",
        language: {
            noResults: function (params) {
                return "No se han encontrado resultados.";
            }
        }
    });
}

function InitializeCustom(notSaveIp = false) {

    GlobalChangeForModeLey = false;
    GlobalEventFired = false;
    isEventAdded = false;

    InizitializeControls();

    $(document).ajaxStart(function () {
        BeginRequestHandler();
    });

    $(document).ajaxStop(function () {
        EndRequestHandler();
    });

    InitializeChosen();

    $.datepicker.setDefaults($.datepicker.regional["en"]);
    $.datepicker.setDefaults({
        dateFormat: getCurrentDateFormat()
    });
    //#EndRegion

    var currentStartDateSelected = moment(new Date()).format(getCurrentDateTimeMomentFormat());
    GlobalcurrentStartDateSelected = currentStartDateSelected;
    var endDate = moment(currentStartDateSelected, getCurrentDateTimeMomentFormat()).add(12, 'months').startOf('day');
    var currentEndDateSelected = (endDate.format(getCurrentDateTimeMomentFormat()));
    GlobalcurrentEndDateSelected = currentEndDateSelected;

    //#Region Jquery Validate
    $.validator.setDefaults({
        showErrors: function (errorMap, errorList) {
            if (errorList.length > 0) {
                var messages = $.map(errorList, function (item) { return item.message; });
                showError(messages, "Se han producido los siguientes errores:");
            }
        },
        ignore: ":hidden:not(.chosen-select-deselect)",//Le digo que no ignore los campos ocultos que tenga esa clase
        //ignore: [":not([readonly='readonly'])"], //Le digo que no ignore los campos ocultos que tenga esa clase
        onfocusout: false,
        onkeyup: false,
        onclick: false,
        onsubmit: true,
        focusInvalid: false
    });

    customValidationsMethods();

    frmClientInfoBasicValidations();
    frmVehicleInfoBasicValidations();

    //#EndRegion

    //Agregando clase requerido(que pone un borde rojo) a los campos que son requeridos
    $('.putErrorBorder').addClass('requerido');
    applyEventsRequiredFields();
    //

    if (GlobalAppMode != "LEYMODE" && GlobalAppMode != "FULLMODE") {
        //getAgentsList();
        getStorages(true);
        getRecargos(true);
    }
    else {
        getAllContactForm();
        if (GlobalAppMode === "FULLMODE") {
            getStorages();
        }
    }

    AllDropsChanges();

    /*Mascara para los tipo de identificaciones*/
    $(".optionLicence").change(function () {
        var opt = $(this);
        var numberLic = $("#IdentificationNumber");

        if (opt.is(":checked")) {
            switch (opt.attr("id")) {

                case "IdentificationTypeCed":
                case "IdentificationTypeCedLic":
                    numberLic.inputmask("999-9999999-9");
                    if ($("#Sex").val() == "Empresa") {
                        $("#Sex").val("");
                        $("#ForeignLicense").val("");
                    }
                    $("#Sex").removeAttr("disabled");
                    $("#Sex").trigger("chosen:updated");
                    $("#Sex").trigger("change");

                    $("#ForeignLicense").removeAttr("disabled");
                    if ($("#ForeignLicense").val() == "") {
                        $("#ForeignLicense").parent().removeClass("is-dirty");
                    }
                    $("#ForeignLicense").trigger("change");


                    if ($("#FirstSurname").prop('disabled')) {
                        $("#FirstSurname").removeAttr("disabled");
                    }

                    resetDateOfBirth(false);
                    break;
                case "IdentificationTypeCedRnc":
                    numberLic.inputmask("999-99999-9");
                    $("#Sex").val("Empresa");
                    $("#Sex").attr("disabled", "disabled");
                    $("#Sex").trigger("chosen:updated");
                    $("#Sex").trigger("change");

                    $("#ForeignLicense").val("No");
                    $("#ForeignLicense").attr("disabled", "disabled");
                    $("#ForeignLicense").parent().addClass("is-dirty");
                    $("#ForeignLicense").trigger("change");

                    $("#FirstSurname").val('');
                    $("#FirstSurname").parent().removeClass("is-dirty");
                    $("#FirstSurname").attr("disabled", "disabled");

                    resetDateOfBirth(true);
                    break;
                case "IdentificationTypeCedPasaport":
                    numberLic.inputmask("remove");
                    if ($("#Sex").val() == "Empresa") {
                        $("#Sex").val("");
                        $("#ForeignLicense").val("");
                    }
                    $("#Sex").removeAttr("disabled");
                    $("#Sex").trigger("chosen:updated");
                    $("#Sex").trigger("change");

                    $("#ForeignLicense").removeAttr("disabled");
                    if ($("#ForeignLicense").val() == "") {
                        $("#ForeignLicense").parent().removeClass("is-dirty");
                    }
                    $("#ForeignLicense").trigger("change");

                    if ($("#FirstSurname").prop('disabled')) {
                        $("#FirstSurname").removeAttr("disabled");
                    }

                    resetDateOfBirth(false);
                    break;
                default:
            }
        }
    });
    /**/

    $("#PhoneNumber").focusout(function () {
        $this = $(this);
        var parentDiv = $(this).parent();

        if ($this.val() != "") {

            parentDiv.addClass('is-dirty');

            if (GlobalAppMode == 'FULLMODE' || GlobalAppMode == 'LEYMODE') {

                if ($("#hdnCurrentPhoneNumber").val() === '') {
                    $("#hdnCurrentPhoneNumber").val($this.val());
                }
                else if ($("#hdnCurrentPhoneNumber").val() !== $this.val()) {

                    $(".btnSaveClientBasic").addClass('ui-state-disabled');
                    $(".btnSaveClientBasic").attr('disabled', 'disabled');

                    $("#btnSendVerCode").removeClass('ui-state-disabled');
                    $("#btnSendVerCode").removeAttr('disabled');

                    $("#VerificationCode").val('');
                }
            }

        } else {

            parentDiv.removeClass('is-dirty');

            if (GlobalAppMode == 'FULLMODE' || GlobalAppMode == 'LEYMODE') {

                $(".btnSaveClientBasic").addClass('ui-state-disabled');
                $(".btnSaveClientBasic").attr('disabled', 'disabled');

                $("#VerificationCode").val('');

                $("#btnSendVerCode").removeClass('ui-state-disabled');
                $("#btnSendVerCode").removeAttr('disabled');
            }
        }
    });

    $("#referredByPhoneNumber").focusout(function () {
        $this = $(this);
        var parentDiv = $(this).parent();

        if ($this.val() != "") {
            parentDiv.addClass('is-dirty');

        } else {

            parentDiv.removeClass('is-dirty');
        }
    });

    $("#IdentificationNumber").focusout(function () {
        $this = $(this);
        var parentDiv = $(this).parent();

        if ($this.val() != "") {
            parentDiv.addClass('is-dirty');
        } else {
            parentDiv.removeClass('is-dirty');
        }
    });

    $(".btnSaveClientBasic").click(function () {
        var agentSelected = $("#AgentList").val();

        if (agentSelected !== '') {
            var dob = $("#DateOfBirth").val();

            if (dob == '' && GlobalAppMode != 'LEYMODE') {

                showError(['La Fecha de Nacimiento es requerida.']);
                return false;
            }

            SaveBasicClientData();

            return false;
        } else {
            showError(['Debe seleccionar un Representante para poder continuar.'], 'Debe Seleccionar un Representante');
            return false;
        }
    });

    $("#AgentList").change(function () {
        var drop = $(this);

        $("#AgentSelected").val(drop.val());

        if (GlobalOnlyForEmission) {

            //Setiando el anterior agente seleccionado
            if ($("#oldAgentSelected").val() == '') {
                $("#oldAgentSelected").val(drop.val());
            }

            if (drop.val() != '') {

                //Si el intermediario es de venta directa
                var adata = JSON.parse(drop.val());
                if (adata != null) {
                    getChannelAgent(adata.NameId);
                }
                //

                ProcessCoupon($('#hdnCouponCode').val(), $('#quotationID').val());

                UpdateVehiclesByAgentChange();
            }
        }
    });

    GlobalIsMobile = IsAMobile();
    if (GlobalIsMobile) {
        $("#vehiclePrice").inputmask('remove');
        $(".vehiclePrice_Flot").inputmask('remove');
    } else {
        $("#vehiclePrice").inputmask({ 'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': false, 'prefix': '' });
        $(".vehiclePrice_Flot").inputmask({ 'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': false, 'prefix': '', 'positionCaretOnTab': false });
    }

    $("#vehiclePrice").focusout(function () {
        var $this = $(this);
        var parent = $($this.parent());

        if (parseFloat($this.val()) >= 0) {

            if (!parent.hasClass('is-dirty')) {
                parent.addClass("is-dirty");
            }
        } else {
            parent.removeClass("is-dirty");
        }

        if (isEditingVehicle) {
            updateGlobalDataProductLimits(vehicleRandomID);
        } else {
            // updateGlobalDataProductLimits();
        }
    });

    removeErrorBorderClass($("#vehiclePrice"));

    $(".qtyVehicles").focusout(function () {
        var v = $(this);
        var randomID = v.attr("data-vehiclerandomid");

        getRates(randomID);

        var parentDiv = v.parent().parent();
        if (v.val() == "") {
            if (parentDiv.hasClass('erarequerido')) {
                parentDiv.addClass('requerido');
                parentDiv.removeClass('erarequerido');
            }
        } else {
            parentDiv.removeClass('requerido');
            parentDiv.addClass('erarequerido');
        }
    });

    $(".deleteVehicle").click(function () {
        var randomid = $(this).attr("data-vehiclerandomid");
        removeVehicle(randomid);
    });

    $(".editVehicle").click(function () {
        var randomid = $(this).attr("data-vehiclerandomid");
        SetEditVehicle(randomid);
    });

    $(".servicesPopUp").click(function () {
        var $this = $(this);
        var randomID = $this.attr("data-vehiclerandomid");

        SetAdditionals(randomID);
        //mostrarpopup
        $("#addServicios").modal({ backdrop: 'static', keyboard: false, show: true });
        $(".saveServices").attr("data-vehiclerandomid", randomID);

        return false;
    });

    $(".optionLicence").trigger('change');

    //AllowSaveOnlyVehicle();

    var msg = getParameterByName("msg", window.location.href);

    var pass = true;
    if (msg !== '' && msg !== undefined && msg !== null) {
        showError(['La cotización solicitada no existe.'], 'Cotización');
        pass = false;
    }

    $("#QuotTypeNormal").trigger('click');

    var quotationID = GetURLParameter();
    if (pass) {
        loadQuotation(quotationID);
        $('#hdnEncrypQuotId').val(quotationID);
    }

    var QuotationNumber = $("#QuotationNumber").val();
    $("#spQuotationNumber").text(QuotationNumber);

    addVehicle();

    $(".RedirectToOtherApp").on("click", function () {

        var $this = $(this);
        var path = $this.data("path");
        var appname = $this.data("appname");
        var tab = "";

        $.ajax({
            url: "/Login/RedirectToOtherApp",
            data: { path: path, appname: appname, tab: tab },
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data.Status) {
                    location.href = data.UrlPath;
                } else {
                    showError([data.errormessage]);
                    return false;
                }
            }
        });
    });

    //getCountriesByBl();
    //getBlByCountry();
    //redirectByBl();

    componentHandler.upgradeAllRegistered();

    $("#btnCloseSection").click(function () {
        $('#headVH').find('a').trigger('click');
        return false;
    });

    if ((GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") && (notSaveIp == false)) {
        var m = getParameterByName("m", window.location.href);
        //Aqui proceso obtener la ip y guardar la aplicacion
        getClientIpInfo(m);
    }

}

function getCurrentDateFormat() {
    return 'dd-M-yy';
}

function showError(errorList, title) {

    var errorContainer = $("#ppError");
    var errorTitle = $("#errorTitle");
    var errorListContainer = $("#errorListContainer");

    if (title) {
        errorTitle.html(title);
    }

    errorListContainer.empty();
    if (errorList) {
        $.each(errorList, function (item, i) {
            errorListContainer.append('<li class="text-left">' + i + '</li>');
        });
    }
    errorContainer.modal({ backdrop: 'static', keyboard: false, show: true });
}

function showWarning(warningList, title) {

    var warningContainer = $("#ppWarning");
    var warningTitle = $("#warningTitle");
    var warningListContainer = $("#warningListContainer");

    if (title) {
        warningTitle.html(title);
    }

    warningListContainer.empty();
    if (warningList) {
        $.each(warningList, function (item, i) {
            warningListContainer.append('<li class="text-left">' + i + '</li>');
        });
    }
    //warningContainer.modal('show');
    warningContainer.modal({ backdrop: 'static', keyboard: false, show: true });
}

function showSucess(sucessList, title, okCallback, OcultarBoton) {

    var sucessContainer = $("#ppSucess");
    var sucessTitle = $("#sucessTitle");
    var sucessListContainer = $("#sucessListContainer");
    var okButton = $('#btnSucessOk');

    if (title) {
        sucessTitle.html(title);
    }

    sucessListContainer.empty();
    if (sucessList) {
        $.each(sucessList, function (item, i) {
            sucessListContainer.append('<li class="text-left">' + i + '</li>');
        });
    }

    if (OcultarBoton == false) {
        $("#RedirectToOtherApp").css("display", "block");
    } else {
        $("#RedirectToOtherApp").css("display", "none");
    }

    //cuando no haya usuario logueado no mostrar el boton de ir a la bandeja
    if ($("#hdnOnlyLoggedUsers").val() === "False" || $("#hdnOnlyLoggedUsers").val() === "false") {
        $("#RedirectToOtherApp").css("display", "none");
    }


    okButton.focus();

    if (okCallback) {
        okButton.unbind('click');
        okButton.click(function () {
            sucessContainer.modal('hide');
            okCallback();
        });
    }

    //sucessContainer.modal('show');
    sucessContainer.modal({ backdrop: 'static', keyboard: false, show: true });
}

function showInfo(infoList, title) {

    var infoContainer = $("#ppInfo");
    var infoTitle = $("#infoTitle");
    var infoListContainer = $("#infoListContainer");

    if (title) {
        infoTitle.html(title);
    }

    infoListContainer.empty();
    if (infoList) {
        $.each(infoList, function (item, i) {
            infoListContainer.append('<li class="text-left">' + i + '</li>');
        });
    }
    //infoContainer.modal('show');
    infoContainer.modal({ backdrop: 'static', keyboard: false, show: true });
}

function showQuestion(question, title, acceptAction, CancelAction) {

    var questionModalContainer = $("#ppQuestion");
    var questionTitle = $("#questionTitle");
    var questionContainer = $("#questionContainer");
    var okButton = $('#btnQuestionOk');
    var cancelButton = $('#btnQuestionCancel');

    if (title) {
        questionTitle.html(title);
    }

    questionContainer.empty();
    if (question) {
        questionContainer.html(question);
    }

    okButton.off("click");
    okButton.click(function () { questionModalContainer.modal('hide'); acceptAction(); });

    if (typeof CancelAction === "function") {
        cancelButton.off("click");
        cancelButton.click(function () { questionModalContainer.modal('hide'); CancelAction(); });
    } else {
        cancelButton.click(function () { questionModalContainer.modal('hide'); });
    }

    //questionModalContainer.modal('show');
    questionModalContainer.modal({ backdrop: 'static', keyboard: false, show: true });
}

function applyEventsRequiredFields(isFlotillaMode) {
    var form = $("#frmClientInfoBasic, #frmVehicleInfoBasic, #frmVehicle,#frmVehicleInclusion");

    var allDivsRequired = form.find('.putErrorBorder.requerido , .input-group.putErrorBorder.requeridoB');

    if (isFlotillaMode == true) {
        form = $("#frmVehicleInfoBasicFlotilla");
        allDivsRequired = form.find('.putErrorBorderFlot.requerido');
    }

    $.each(allDivsRequired, function (i, div) {

        var realDiv = $(div);
        var actualInputsInDiv = realDiv.find('input[type="text"], select, input[type="radio"]');

        $.each(actualInputsInDiv, function (i, element) {

            var realElement = $(element);

            if (realElement.is(':input[type="text"]')) {
                putClassRequeridoInputs(realElement);
            } else if (realElement.is('select')) {
                putClassRequeridoSelects(realElement);
            } else if (realElement.is(':input[type="radio"]')) {
                putClassRequeridoRadios(realElement);
            }
        });
    });
}

function putClassRequeridoInputs(input) {

    $(input).focusout(function () {
        $this = $(this);
        var parentDiv = $(this).parent();

        if ($this.val() == "") {

            if (parentDiv.hasClass('erarequerido')) {

                if (parentDiv.hasClass('requeridoB')) {
                    parentDiv.addClass('requeridoB');
                } else {
                    parentDiv.addClass('requerido');
                }
                parentDiv.removeClass('erarequerido');
            }

        } else {
            parentDiv.removeClass('requerido');
            parentDiv.removeClass('requeridoB');
            parentDiv.addClass('erarequerido');
        }
    });
}

function putClassRequeridoSelects(input) {

    $(input).change(function () {
        $this = $(this);
        var parentDiv = $(this).parent().parent();

        if (!parentDiv.hasClass('putErrorBorder')) {
            parentDiv = $(this).parent();
        }

        if ($this.val() == "" || $this.val() == "-1") {
            if (parentDiv.hasClass('erarequerido')) {
                parentDiv.addClass('requerido');
                parentDiv.removeClass('erarequerido');
            }
        } else {
            parentDiv.removeClass('requerido');
            parentDiv.addClass('erarequerido');
        }
    });
}

function putClassRequeridoRadios(input) {

    $(input).click(function () {
        $this = $(this);
        var parentDiv = $(this).parent().parent();

        if (!$this.is(":checked")) {
            if (parentDiv.hasClass('erarequerido')) {
                parentDiv.addClass('requerido');
                parentDiv.removeClass('erarequerido');
            }
        } else {
            parentDiv.removeClass('requerido');
            parentDiv.addClass('erarequerido');
        }
    });
}

function removeErrorBorderClass(input, isChosenSelect, isRadio) {

    var element = $(input);

    var parentDiv = isChosenSelect ? element.parent().parent() : isRadio ? element.parent().parent() : element.parent();

    if (element.val() == "") {
        if (parentDiv.hasClass('erarequerido')) {
            parentDiv.addClass('requerido');
            parentDiv.removeClass('erarequerido');
        }
    } else {
        parentDiv.removeClass('requerido');
        parentDiv.addClass('erarequerido');
    }
}

function frmClientInfoBasicValidations() {

    $("#frmClientInfoBasic").validate(
        {
            rules: {
                FirstName: {
                    required: true,
                    maxlength: 50
                },
                DateOfBirth: {
                    //required: true,
                    required: function () {
                        if (GlobalAppMode == "LEYMODE") {
                            return false;
                        }
                        return true;
                    },
                    dateFormat: true
                },
                Sex: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") {
                            return false;
                        }
                        return true;
                    }
                },
                ForeignLicense: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE") {
                            return false;
                        }
                        return true;
                    }
                },
                Email: {
                    email: true
                },
                IdentificationType: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE") {
                            return false;
                        }
                        return true;
                    }
                },
                contactForm: {
                    required: function () {
                        if (GlobalAppMode == "FULLMODE" || GlobalAppMode == "LEYMODE") {

                            var CouponCode = $('#CouponCode').val();
                            if (CouponCode !== '') {
                                return false;
                            }
                            return true;
                        }
                        return false;
                    }
                },
                PhoneNumber: {
                    required: function () {
                        if (GlobalAppMode == "FULLMODE" || GlobalAppMode == "LEYMODE") {
                            return true;
                        }
                        return false;
                    }
                }
            },
            messages: {
                FirstName: {
                    required: 'El Nombre es requerido.',
                },
                DateOfBirth: {
                    required: 'La Fecha de Nacimiento es requerida',
                    dateFormat: 'Debe ingresar una Fecha de Nacimiento válida',
                }, Sex: {
                    required: 'El Sexo es requerido.',
                }, ForeignLicense: {
                    required: 'La Licencia Extranjera es requerida.',
                },
                Email: {
                    email: 'El Email debe ser una dirección de correo electrónico válida'
                },
                IdentificationType: {
                    required: 'El Tipo de Identificación es requerida.',
                },
                contactForm: {
                    required: 'El campo Vía de Promoción es requerido.'
                },
                PhoneNumber: {
                    required: 'El campo Número de Celular es requerido.'
                }
            }
        });
}

function frmVehicleInfoBasicValidations() {

    $("#frmVehicleInfoBasic").validate(
        {
            rules: {
                VehicleDriver_Id: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") {
                            return false;
                        }
                        return true;
                    }
                },
                VehicleModel_Make_Id: {
                    required: true
                },
                VehicleModel_Model_Id: {
                    required: true
                },
                yearsAvaibles: {
                    required: true
                },
                VehicleType: {
                    required: true
                },
                VehicleYearsOld: {
                    required: true
                },
                Usages: {
                    required: true
                },
                StoreCar: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE") {
                            return false;
                        }
                        return true;
                    }
                },
                vehiclePrice: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE") {
                            return false;
                        }
                        return true;
                    }
                },
                fuelType: {
                    required: function () {
                        if (GlobalAppMode == "LEYMODE") {
                            return false;
                        }
                        return true;
                    }
                }
            },
            messages: {
                VehicleDriver_Id: {
                    required: 'El Conductor Principal es requerido.',
                },
                VehicleModel_Make_Id: {
                    required: 'La Marca es requerida.'
                },
                VehicleModel_Model_Id: {
                    required: 'El Modelo es requerido.',
                },
                yearsAvaibles: {
                    required: 'El Año es requerido.',
                },
                VehicleType: {
                    required: 'El Tipo Vehículo es requerido.'
                },
                VehicleYearsOld: {
                    required: 'El campo Nuevo / 0KM es requerido.'
                },
                Usages: {
                    required: 'El Uso Principal es requerido.'
                },
                StoreCar: {
                    required: 'El Estacionamiento es requerido.'
                },
                vehiclePrice: {
                    required: 'El Valor es requerido.'
                },
                fuelType: {
                    required: 'El Tipo Combustible es requerido.'
                }
            }
        });
}

function AllDropsChanges() {

    $("#VehicleModel_Make_Id").change(function (data) {
        var $this = $(this);
        getVehicleModels($this.val());

        if (isEditingVehicle) {

            $("#Usages").empty();
            $("#Usages").trigger("chosen:updated");
            $("#Usages").trigger("change");

            var element = $(".ddlAllProducts");
            if (element.length > 1) {
                $.each(element, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == vehicleRandomID) {
                        element = $(obj);
                    }
                });
            }
            element.empty();
            element.append('<option value="">Seleccionar</option>');
            element.trigger("chosen:updated");
            element.trigger("change");

            element = $(".Coverages");
            if (element.length > 1) {
                $.each(element, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == vehicleRandomID) {
                        element = $(obj);
                    }
                });
            }
            element.trigger("change");

            element = $(".Deducible");
            if (element.length > 1) {
                $.each(element, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == vehicleRandomID) {
                        element = $(obj);
                    }
                });
            }
            element.trigger("change");
        }
    });

    $("#VehicleModel_Model_Id").change(function () {
        var $this = $(this);

        var lastModelSelected = parseInt($("#lastModelSelected").val());
        lastModelSelected = isNaN(lastModelSelected) ? 1 : lastModelSelected;
        var modelIdActual = $this.val();

        if (lastModelSelected > 0) {

            if (lastModelSelected != modelIdActual) {

                if (modelIdActual == null || modelIdActual == "") {
                    modelIdActual = lastModelSelected;
                }

                $("#lastModelSelected").val(modelIdActual);

                var brand = $("#VehicleModel_Make_Id").val();
                var year = $("#yearsAvaibles").val();

                if (isEditingVehicle) {

                    $("#Usages").empty();
                    $("#Usages").trigger("chosen:updated");
                    $("#Usages").trigger("change");

                    var element = $(".ddlAllProducts");
                    if (element.length > 1) {
                        $.each(element, function (idx, obj) {
                            var randomid = $(obj).attr("data-vehiclerandomid");
                            if (randomid == vehicleRandomID) {
                                element = $(obj);
                            }
                        });
                    }
                    element.empty();
                    element.append('<option value="">Seleccionar</option>');

                    GlobalChangeForModeLey = false;

                    element.trigger("chosen:updated");
                    element.trigger("change");

                    element = $(".Coverages");
                    if (element.length > 1) {
                        $.each(element, function (idx, obj) {
                            var randomid = $(obj).attr("data-vehiclerandomid");
                            if (randomid == vehicleRandomID) {
                                element = $(obj);
                            }
                        });
                    }
                    element.trigger("change");

                    element = $(".Deducible");
                    if (element.length > 1) {
                        $.each(element, function (idx, obj) {
                            var randomid = $(obj).attr("data-vehiclerandomid");
                            if (randomid == vehicleRandomID) {
                                element = $(obj);
                            }
                        });
                    }
                    element.trigger("change");
                }

                GetProductsFromSysflex(brand, modelIdActual, year);

                if (modelIdActual) {
                    GetFuelType(null, brand, modelIdActual);
                }
            }
        }
    });

    $("#yearsAvaibles").change(function () {
        var $this = $(this);

        var year = $this.val();
        var brand = $("#VehicleModel_Make_Id").val();
        var model = $("#VehicleModel_Model_Id").val();

        if (isEditingVehicle) {

            var lastMakeSelected = $("#lastMakeSelected").val();
            if (lastMakeSelected == brand) {
                model = $("#lastModelSelected").val();
            }
        }

        GlobalDataProductLimits = null;

        //llamar metodo que me trae los productos de sysflex
        GetProductsFromSysflex(brand, model, year);
        enableVehicleOldCombo(year);
    });

    $("#VehicleType").change(function () {
        var $this = $(this);

        var selectedVehicleType = $this.val();

        UsagesListByVehicleType(selectedVehicleType);
    });

    $("#Usages").change(function () {

        var $this = $(this);

        var usageSelected = $this.val();

        $("#AddVehicle").removeAttr('disabled');
        $("#hdnInvalidUsage").val("N");

        var usage = GlobalDataUsages.filter(function (item) {
            return item.idUso == usageSelected;
        });

        if (usage.length > 0) {
            var UsageFound = usage[0];

            var allowed = UsageFound.allowed;
            var message = UsageFound.message;

            if (allowed == 2) {
                //mensaje error
                showError([message], 'Uso Principal');

                $("#AddVehicle").attr('disabled', 'disabled');

                $("#hdnInvalidUsage").val("S");
                return;
            }
            else if (allowed == 3) {
                //mensaje advertencia
                showWarning([message], 'Uso Principal');
                return;
            }

            ProductListByUsages(usageSelected);

            if (isEditingVehicle) {
                updateGlobalDataProductLimits(vehicleRandomID);
            } else {
                updateGlobalDataProductLimits();
            }
        }
    });

    $("#StoreCar").change(function () {
        var $this = $(this);
        if (isEditingVehicle) {
            getRates(vehicleRandomID);
        }
    });

    $(".ddlAllProducts").change(function () {

        var $this = $(this);
        var i = $this.attr('id');
        var selectedProductName = $("#" + i + " option:selected").text();
        var randomID = $this.attr("data-vehiclerandomid");

        if (ApplyToZero(randomID) == false && ($this.val() !== "")) {
            SetFieldsApplyToZero(false, randomID);
            return false;
        } else {
            SetFieldsApplyToZero(true, randomID);
        }


        if (GlobalChangeForModeLey == false) {
            currentCoveragesByUsage(selectedProductName, randomID);
        }

        var parentDiv = $this.parent();
        if ($this.val() == "") {
            if (parentDiv.hasClass('erarequerido')) {
                parentDiv.addClass('requerido');
                parentDiv.removeClass('erarequerido');
            }
        } else {
            parentDiv.removeClass('requerido');
            parentDiv.addClass('erarequerido');
        }
    });

    $(".SurchargePercent").change(function () {
        var $this = $(this);
        var randomID = $this.attr("data-vehiclerandomid");

        getRates(randomID);
    });

    $("#Sex").change(function () {
        var s = $(this);

        if (s.val() != '') {
            if (s.val() == "Empresa" && !$("#IdentificationTypeCedRnc").is(":checked")) {
                s.val("");
                s.trigger("chosen:updated");
                s.trigger("change");
            }

            if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                changedSex = true;
                disableAccordionsSections(true, true);
            } else {
                //disableAccordionsSections(false);
            }
        } else {
            if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                disableAccordionsSections(true, true);
            } else {
                //disableAccordionsSections(false);
            }
        }
    });

    $("#ForeignLicense").change(function () {
        var s = $(this);
        if (s.val() != '') {

            if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                changedForeingLicenceDriver = true;
                disableAccordionsSections(true, true);
            } else {
                //disableAccordionsSections(false);
            }
        } else {
            if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                disableAccordionsSections(true, true);
            } else {
                //disableAccordionsSections(false);
            }
        }
    });

    $("#contactForm").change(function (data) {
        var $this = $(this);

        //esto es porque si escriben un cupon tiene preferencia sobre el seleccionado

        if ($('#CouponCode').val() === '') {

            if ($this.val()) {
                var Coupon = $this.find(':selected').data('coupon');
                if (Coupon) {

                    $("#hdnDefaultCupon").val(Coupon);
                    ProcessCoupon(Coupon, 0);
                }
            }
            else if ($('#hdnCouponCode').val() !== '') {
                $('#hdnCouponCode').val('');
                $('#hdnCuponDiscount').val('');
                $('#hdnProspectoID').val("");

                //Hago esto para que me obligue a guardar el cupon cuando haya un vehiculo
                if (AllVehicleDataToSave !== null) {
                    changedSex = true;
                    disableAccordionsSections(true, true);
                }

                var NotIsLeyOrFullMode = (GlobalAppMode != "FULLMODE" && GlobalAppMode != "LEYMODE");

                if (NotIsLeyOrFullMode) {
                    $("#contactForm").parent().removeClass("requerido");
                }
            }
        }

        //Recomendacion de alguien
        if ($this.val() === '5') {
            $('#dvReferredBy').show();
        }
        else {
            $('#dvReferredBy').hide();
            cleanReferredInfo();
        }



    });
}

function getAgentsList() {

    $.ajax({
        url: "/Home/GetAgents",
        type: "POST",
        data: {},
        cache: false,
        async: false,
        success: function (json, textStatus, jqXHR) {

            var $select_elem = $("#AgentList");
            $select_elem.empty();
            $select_elem.append('<option value=""></option>');

            var qtyAgent = json.agents.length;
            var selec = "";
            $.each(json.agents, function (idx, obj) {
                var toJson = obj.jsonAgentTree;

                if (qtyAgent == 1) {
                    selec = "selected='selected'";
                }
                $select_elem.append("<option value='" + toJson + "' " + selec + ">" + obj.FullNameAll + "</option>");
                selec = "";
            });

            if (qtyAgent == 1) {
                $select_elem.attr("disabled", 'disabled');
            }
            $select_elem.trigger("chosen:updated");
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

function addVehicle() {

    $("#AddVehicle").click(function () {

        var va = $("#frmVehicleInfoBasic").valid();
        if (va) {

            var hdnDefaultClientWasSaved = $("#hdnDefaultClientWasSaved").val();
            //if (GlobalAppMode == "LEYMODE" && !isEditingVehicle && hdnDefaultClientWasSaved == "N") {
            //    SaveBasicClientData();
            //}

            if (isEditingVehicle) {
                $('#headCober').removeClass('disabledAccordionTab');
            }

            $('#headCober').removeClass('disabledAccordionTab');
            $('#selecCober').addClass("collapse show");
        } else {
            return false;
        }

        var newVehicle = null;
        var SecuenciaVehicleSysflex = 1;

        if (AllVehicleDataToSave != null && LoadVehicle == false) {

            newVehicle = altFind(AllVehicleDataToSave, function (item) {
                return item.randomId == vehicleRandomID;
            });

            var arr = AllVehicleDataToSave.sort(function (a, b) { return (a.SecuenciaVehicleSysflex > b.SecuenciaVehicleSysflex) ? 1 : ((b.SecuenciaVehicleSysflex > a.SecuenciaVehicleSysflex) ? -1 : 0); })
            var lastVeh = arr[arr.length - 1];

            if (lastVeh) {
                SecuenciaVehicleSysflex = (lastVeh.SecuenciaVehicleSysflex + 1);
            }
        }

        var qtyVehicles = LoadVehicleQuantity;

        if ($("#qtyVehicles").val() == "") {
            $("#qtyVehicles").val(qtyVehicles);
        }
        $("#qtyVehicles").parent().addClass("is-dirty");

        var $qtyVehicles_elem = $('.qtyVehicles');

        if ($qtyVehicles_elem.length > 1) {
            $.each($qtyVehicles_elem, function (idx, obj) {

                var randomid = $(obj).attr("data-vehiclerandomid");

                if (randomid == vehicleRandomID) {
                    $qtyVehicles_elem = $(obj);
                    qtyVehicles = $qtyVehicles_elem.val();
                } else {
                    qtyVehicles = $qtyVehicles_elem.val();
                }
            });
        } else {
            var randomid = $qtyVehicles_elem.attr("data-vehiclerandomid");
            if (randomid == vehicleRandomID) {
                qtyVehicles = $qtyVehicles_elem.val();
            } else {
                qtyVehicles = $qtyVehicles_elem.val();
            }
        }
        $qtyVehicles_elem.parent().addClass("is-dirty");

        var parentDiv = $qtyVehicles_elem.parent().parent();

        if ($qtyVehicles_elem.val() == "") {

            if (parentDiv.hasClass('erarequerido')) {
                parentDiv.addClass('requerido');
                parentDiv.removeClass('erarequerido');
            }
        } else {
            parentDiv.removeClass('requerido');
            parentDiv.addClass('erarequerido');
        }


        var usage = $("#Usages").val();
        var selectedVehicleType = $("#VehicleType").val();

        var brand = $("#VehicleModel_Make_Id").val();
        var model = $("#VehicleModel_Model_Id").val();
        var year = $("#yearsAvaibles").val();
        var vehiclePrice = parseFloat($("#vehiclePrice").val().replace(/,/g, ''));
        var selectedCoverage = $("#Coverages").val();
        var VehicleDriver = $("#VehicleDriver_Id").val();
        var VehicleYearsOld = $("#VehicleYearsOld").val();

        var VehicleFuelTypeId = $("#fuelType").val();
        var VehicleFuelTypeDesc = $("#fuelType option:selected").text();

        var makeName = $("#VehicleModel_Make_Id option:selected").text();
        var modelName = $("#VehicleModel_Model_Id option:selected").text();
        var store = $("#StoreCar").val();
        var StoreName = $("#StoreCar option:selected").text();
        var VehicleQuantity = LoadVehicle ? LoadVehicleQuantity : qtyVehicles;

        if (newVehicle == null || newVehicle == undefined) {

            vehicleRandomID = Math.floor((Math.random() * -20000) + (-1));
            hasServices = false;

            var newVehicle = {};
            newVehicle.randomId = vehicleRandomID;
            newVehicle.Id = LoadVehicle ? LoadVehicleCurrentID : null;
            newVehicle.SecuenciaVehicleSysflex = LoadVehicle ? LoadVehicleCurrentSecuenciaVehicleSysflex : SecuenciaVehicleSysflex;

            newVehicle.Year = year;
            newVehicle.VehiclePrice = vehiclePrice;
            newVehicle.SelectedVehicleTypeId = selectedVehicleType;
            newVehicle.SelectedVehicleTypeName = selectedVehicleType;
            newVehicle.UsageId = usage;
            newVehicle.VehicleModel_Make_Id = brand;
            newVehicle.VehicleMakeName = makeName;
            newVehicle.VehicleModel_Model_Id = model;
            newVehicle.VehicleModelName = modelName;
            newVehicle.VehicleDescription = makeName + " " + modelName;
            newVehicle.StoreId = store;
            newVehicle.StoreName = StoreName;
            newVehicle.Driver_Id = VehicleDriver;
            newVehicle.VehicleYearOld = VehicleYearsOld;
            newVehicle.VehicleQuantity = VehicleQuantity;

            newVehicle.SelectedVehicleFuelTypeId = VehicleFuelTypeId;
            newVehicle.SelectedVehicleFuelTypeDesc = VehicleFuelTypeDesc;

            newVehicle.GlobalDataUsages = GlobalDataUsages;
            newVehicle.GlobalData = GlobalData;
            newVehicle.GlobalDataProductsByUsage = GlobalDataProductsByUsage;
            newVehicle.GlobalAllProducts = GlobalAllProducts;

            var realVehicle = setVehicle(newVehicle);

            setAllVehicleDataToSave(realVehicle);

            if (firstTime) {

                //setiando randomid a los campos existentes
                var desc = realVehicle.VehicleDescription + " " + realVehicle.Year;
                $("#vehicleTitle").html(desc);
                $("#vehicleTitle").attr("data-vehiclerandomid", realVehicle.randomId);

                var apply = ApplyToZero(realVehicle.randomId);

                $("#VehicleValue").attr("data-vehiclerandomid", realVehicle.randomId);

                //Si aplica a 0 como valor, entonces mostrar 0 en el campo VehicleValue
                if (apply && vehiclePrice <= 1) {
                    $("#VehicleValue").html("$" + number_format("0", 2));
                }
                else if (!apply && vehiclePrice <= 1) {
                    vehiclePrice = "0";
                    $("#VehicleValue").html("$" + number_format(vehiclePrice, 2));
                }
                else {
                    $("#VehicleValue").html("$" + number_format(vehiclePrice, 2));
                }

                $("#ddlAllProducts").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#Coverages").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#Deducible").attr("data-vehiclerandomid", realVehicle.randomId);

                $("#SurchargePercent").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#qtyVehicles").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#btnDeleteVehicle").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#btnEditVehicle").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#servicesPopUp").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#dvparentSectionVehicle").attr("data-vehiclerandomid", realVehicle.randomId);

                $("#totalPrime").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#liflotillaDiscount").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#flotillaDiscountAmount").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#liPrimeFlotillaDiscount").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#PrimeFlotillaDiscountAmount").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#ISC").attr("data-vehiclerandomid", realVehicle.randomId);
                $("#totalToPay").attr("data-vehiclerandomid", realVehicle.randomId);

                $("#liCoverageExplication").attr("data-vehiclerandomid", realVehicle.randomId);

                $("#totalAllDiscount").attr("data-vehiclerandomid", realVehicle.randomId);

                firstTime = false;
            }
        } else {
            //Actualizando
            newVehicle.Year = year;
            newVehicle.VehiclePrice = vehiclePrice;
            newVehicle.SelectedVehicleTypeId = selectedVehicleType;
            newVehicle.SelectedVehicleTypeName = selectedVehicleType;
            newVehicle.UsageId = usage;
            newVehicle.VehicleModel_Make_Id = brand;
            newVehicle.VehicleMakeName = makeName;
            newVehicle.VehicleModel_Model_Id = model;
            newVehicle.VehicleModelName = modelName;
            newVehicle.VehicleDescription = makeName + " " + modelName;
            newVehicle.StoreId = store;
            newVehicle.StoreName = StoreName;
            newVehicle.Driver_Id = VehicleDriver;
            newVehicle.VehicleYearOld = VehicleYearsOld;
            newVehicle.VehicleQuantity = VehicleQuantity;

            newVehicle.SelectedVehicleFuelTypeId = VehicleFuelTypeId;
            newVehicle.SelectedVehicleFuelTypeDesc = VehicleFuelTypeDesc;

            newVehicle.GlobalDataUsages = GlobalDataUsages;
            newVehicle.GlobalData = GlobalData;
            newVehicle.GlobalDataProductsByUsage = GlobalDataProductsByUsage;
            newVehicle.GlobalAllProducts = GlobalAllProducts;

            var realVehicle = setVehicle(newVehicle);

            setAllVehicleDataToSave(realVehicle, "UPDATE");

            updateHtmlValueFromVehicle(newVehicle.randomId);
        }

        resetVehicleInfo();

        if (isEditingVehicle == false) {
            paintVehicles(AllVehicleDataToSave);
        }

        remove0kmIfIsNotNew(vehicleRandomID);

        if (GlobalAppMode == "LEYMODE") {
            //$(".ddlAllProducts").trigger('change');

            var $select_elemProd = $(".ddlAllProducts");

            if ($select_elemProd.length > 1) {
                GlobalChangeForModeLey = false;

                $.each($select_elemProd, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == vehicleRandomID) {
                        $select_elemProd = $(obj);
                    }
                });
            }
            $select_elemProd.trigger('change');
            GlobalChangeForModeLey = true;
        }


        if (LoadVehicle == false) {
            vehicleRandomID = 0;
        }

        if (isEditingVehicle) {
            isEditingVehicle = false;
            $('#headCober').removeClass('disabledAccordionTab');
            $('#headInfoCC').removeClass('disabledAccordionTab');
            $("#AddVehicle").html('<i class="material-icons">add</i> Agregar Vehículo');
            $("#lastDeducibleSelected").val("");
        }

        //Poniendole el marco de requerdo luego de agregar el Vehículo
        var elm = $(".addAgain");
        $.each(elm, function (idx, obj) {
            var l = $(this);

            if (!l.hasClass("vprice")) {
                if (l.hasClass('erarequerido')) {
                    l.removeClass("erarequerido");
                    l.addClass("requerido");
                }
            }
        });

        return false;
    });

}

function getYearAvaiblesList() {

    $.getJSON('/Home/GetVehicleAvailableYearsList', function (json) {
        var $select_elem = $("#yearsAvaibles");
        $select_elem.empty();
        $select_elem.append('<option value=""></option>');

        $.each(json.agents, function (idx, obj) {
            $select_elem.append('<option value="' + obj.NameId + '">' + obj.FullNameAll + '</option>');
        });
        $select_elem.trigger("chosen:updated");
    });
}

function getVehicleModels(brandID) {

    $.ajax({
        url: "/Home/getVehiclesModelsByBrands",
        dataType: 'json',
        async: isEditingVehicle,
        cache: false,
        data: {
            BrandID: brandID
        },
        success: function (json) {

            var $select_elem = $("#VehicleModel_Model_Id");
            $select_elem.empty();
            $select_elem.append('<option value=""></option>');

            $.each(json, function (idx, obj) {
                $select_elem.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
            });

            if (isEditingVehicle) {

                var lastMakeSelected = $("#lastMakeSelected").val();
                if (lastMakeSelected == brandID) {

                    var last = $("#lastModelSelected").val();
                    $select_elem.val(last);
                }

            }
            $select_elem.trigger("chosen:updated");
            $select_elem.trigger("change");
        }
    });
}

function loadQuotation(quotationID) {

    var isNumber = parseInt(quotationID);
    if (!isNaN(isNumber)) {
        return false;
    }

    $.getJSON('/Home/loadQuotatoin', { QuotationID: quotationID }, function (data) {

        if (data.error) {
            showError([data.error], 'Cotización no existe');
            window.history.replaceState({}, document.title, "/Home/Index");
            return false;
        }

        var modoFlotilla = false;
        isLoadingQuot = true;

        GlobalAllowRequoting = data.AllowRequoting;

        var quotData = data.quotData;

        if (quotData.QuotationNumber != null) {
            var lasQuotationStep = parseInt(quotData.LastStepVisited);

            modoFlotilla = (quotData.FlotillaMode === undefined || quotData.FlotillaMode === null) ? false : quotData.FlotillaMode;

            var actualDate = moment(new Date());
            var selectedDat = moment(quotData.StartDate);

            var result = getNewDateYear(actualDate);

            var isbefore = moment(actualDate).isBefore(selectedDat);
            var isSameYear = moment(actualDate).isSame(selectedDat, "year");
            var isSameMonth = moment(actualDate).isSame(selectedDat, "month");
            var isSameDay = moment(actualDate).isSame(selectedDat, "day");

            if (isbefore) {
                GlobalcurrentStartDateSelected = (moment(quotData.StartDate ? quotData.StartDate : new Date()).format(getCurrentDateTimeMomentFormat()));
                GlobalcurrentEndDateSelected = (moment(quotData.EndDate ? quotData.EndDate : result).format(getCurrentDateTimeMomentFormat()));
            }
            else if (isSameDay && isSameMonth && isSameYear) {
                GlobalcurrentStartDateSelected = (moment(quotData.StartDate ? quotData.StartDate : new Date()).format(getCurrentDateTimeMomentFormat()));
                GlobalcurrentEndDateSelected = (moment(quotData.EndDate ? quotData.EndDate : result).format(getCurrentDateTimeMomentFormat()));
            } else {
                GlobalcurrentStartDateSelected = (moment(new Date()).format(getCurrentDateTimeMomentFormat()));
                GlobalcurrentEndDateSelected = (moment(result).format(getCurrentDateTimeMomentFormat()));
            }

            $("#spQuotationNumber").html(quotData.QuotationNumber);

            //Hidden fields Cotizacion
            $("#quotationID").val(quotData.Id);
            $("#QuotationNumber").val(quotData.QuotationNumber);
            $("#QuotationCoreNumber").val(quotData.QuotationCoreNumber);
            $("#Financed").val(quotData.Financed);

            if (quotData.couponCode && (quotData.contactFormId == null || quotData.contactFormId <= 0)) {
                $('#CouponCode').val(quotData.couponCode);
                $('#CouponCode').parent().addClass('is-dirty');
            }

            $('#hdnCouponCode').val(quotData.couponCode);
            $('#hdnCuponDiscount').val(quotData.couponPercentageDiscount);
            $('#hdnProspectoID').val(quotData.CouponProspectId);
            //

            var agent = (quotData._agentQuotation != undefined || quotData._agentQuotation != null) ? quotData._agentQuotation.Username : "";
            $("#AgentList option").each(function () {
                var act = $(this).val();
                if (act != "") {

                    var tojson = JSON.parse(act);
                    //en esto me quesde
                    if (tojson != null) {

                        if (tojson.NameId == agent) {
                            $("#AgentList").val(act);
                            $("#oldAgentSelected").val(act);
                            return false;
                        }
                    }
                }
            });
            $("#AgentList").trigger("change.select2");

            //Driver Basic Info
            if (GlobalAppMode != "LEYMODE") {
                configAppMode();

                $.each(quotData._drivers, function (i, dri) {

                    if (dri.IsPrincipal) {

                        $("#driver").val(dri.Id);

                        $("#FirstName").val(dri.FirstName);
                        $("#FirstName").parent().addClass("is-dirty");
                        removeErrorBorderClass($("#FirstName"));

                        $("#FirstSurname").val(dri.FirstSurname);
                        $("#FirstSurname").parent().addClass("is-dirty");

                        $("#Sex").val(dri.Sex);
                        $("#Sex").trigger("chosen:updated");
                        removeErrorBorderClass($("#Sex"), true);

                        if (dri.ForeignLicense) {
                            $("#ForeignLicense").val("Si");
                        } else {
                            $("#ForeignLicense").val("No");
                        }
                        $("#ForeignLicense").parent().addClass("is-dirty");
                        removeErrorBorderClass($("#ForeignLicense"));

                        if (dri.IdentificationNumber !== '') {
                            $("#IdentificationNumber").val(dri.IdentificationNumber);
                            $("#IdentificationNumber").parent().addClass("is-dirty");
                        }

                        if (dri.Mobile !== '') {
                            $("#PhoneNumber").val(dri.Mobile);
                            $("#PhoneNumber").parent().addClass("is-dirty");
                            $("#PhoneNumber").focusout();
                        }

                        if (dri.Email !== '') {
                            $("#Email").val(dri.Email);
                            $("#Email").parent().addClass("is-dirty");
                        }

                        var check = $("input[name='IdentificationType']");
                        $.each(check, function (i, iden) {
                            var objCheck = $(iden);
                            if (objCheck.val() == dri.IdentificationType) {
                                objCheck.parent().addClass('is-checked');
                                objCheck.trigger('click');
                                removeErrorBorderClass(objCheck, false, true);
                            }
                        });

                        var realDob = moment(dri.DateOfBirth).format("DD-MMM-YYYY");
                        if (dri.IdentificationType !== "RNC") {
                            $("#DateOfBirth").val(realDob);
                            $("#DateOfBirth").parent().addClass("is-dirty");
                        } else {
                            $("#DateOfBirth").val("N/A");
                        }
                        removeErrorBorderClass($("#DateOfBirth"));
                    }
                });
                //

                //llenar el drop drivers con los drivers en la seccion de Vehículo
                var $select_elem = $("#VehicleDriver_Id");
                $select_elem.empty();
                $select_elem.append('<option value=""></option>');
                $.each(quotData._drivers, function (idx, obj) {
                    //Por ahora solo sera el conductor principal
                    if (obj.IsPrincipal) {
                        var driverNameFull = obj.FirstName + ' ' + obj.FirstSurname;
                        $select_elem.append("<option value='" + obj.Id + "'>" + driverNameFull + "</option>");
                    }
                });
                //

                AllVehicleDataToSave = null;

                if (GlobalAppMode != "FULLMODE") {
                    //Si el intermediario es de venta directa

                    if ($("#AgentList").val() !== '') {
                        var adata = JSON.parse($("#AgentList").val());
                        if (adata != null) {

                            getChannelAgent(adata.NameId);

                            if (quotData.contactFormId != null && quotData.contactFormId > 0) {
                                $("#contactForm").val(quotData.contactFormId);
                                $("#contactForm").trigger("change.select2");
                            }
                        }
                    }
                    //
                }
            }
            else {

                configAppMode();

                $('#hdnDefaultClientWasSaved').val("S");

                $.each(quotData._drivers, function (idx, obj) {
                    //Por ahora solo sera el conductor principal
                    if (obj.IsPrincipal) {
                        $('#hdnDefaultClientId').val(obj.Id);
                        $('#driver').val(obj.Id);

                        if (obj.PhoneNumber !== '') {
                            $("#PhoneNumber").val(obj.Mobile);
                            $("#PhoneNumber").parent().addClass("is-dirty");
                            $("#PhoneNumber").focusout();
                        }

                        if (obj.Email !== '') {
                            $("#Email").val(obj.Email);
                            $("#Email").parent().addClass("is-dirty");
                        }
                        return;
                    }
                });

                if (quotData._vehicles !== null) {
                    AllVehicleDataToSave = null;
                }
            }

            if (GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") {
                if (quotData.contactFormId != null && quotData.contactFormId > 0) {
                    $("#contactForm").val(quotData.contactFormId);
                    $("#contactForm").trigger("change");
                }
                else {

                    if ($('#CouponCode').val() !== '') {

                        $("#contactForm").val('');
                        $("#contactForm").trigger('change');

                        var parentDiv = $("#contactForm").parent().parent();
                        parentDiv.removeClass('requerido');
                    }
                }
                GetCode();
            }


            //Recomendacion de Alguien
            if (quotData.contactFormId == 5) {
                getQuotationReferredInfo($("#quotationID").val());
            }

            firstTime = true;
            var hasVehicle = false;
            var goToFirstStep = false;
            var qtyVehicleTot = 0;

            if (modoFlotilla == true) {
                $("#QuotTypeFlotilla").trigger('click');
                $('#headVH2').find('a').trigger('click');
            }

            if (quotData._vehicles) {
                qtyVehicleTot = quotData._vehicles.length;
            }

            //Vehículos
            $.each(quotData._vehicles, function (idx, obj) {
                var vehicle = obj;

                if (vehicle != undefined) {

                    if (modoFlotilla == true) {
                        disableAccordionsSections(false);

                        qtyVehicleTot = (qtyVehicleTot - 1);
                        loadDataVehicleFlotMode(vehicle, data, qtyVehicleTot);
                    }
                    else {
                        disableAccordionsSections(false);

                        //trayendo driver
                        $("#VehicleDriver_Id").val(vehicle.Driver_Id);
                        $("#VehicleDriver_Id").trigger("chosen:updated");
                        removeErrorBorderClass($("#VehicleDriver_Id"), true);

                        $("#lastModelSelected").val(vehicle.VehicleModel_Model_Id);

                        //Cascade
                        $("#VehicleModel_Make_Id").val(vehicle.VehicleModel_Make_Id);
                        $("#VehicleModel_Make_Id").trigger("chosen:updated");
                        $("#VehicleModel_Make_Id").trigger("change");
                        $("#lastMakeSelected").val(vehicle.VehicleModel_Make_Id);

                        $("#VehicleModel_Model_Id").val(vehicle.VehicleModel_Model_Id);
                        $("#VehicleModel_Model_Id").trigger("chosen:updated");
                        $("#VehicleModel_Model_Id").trigger("change");

                        $("#yearsAvaibles").val(vehicle.Year);
                        $("#yearsAvaibles").trigger("chosen:updated");
                        $("#yearsAvaibles").trigger("change");

                        $("#VehicleType").val(vehicle.SelectedVehicleTypeName);
                        $("#VehicleType").trigger("chosen:updated");
                        $("#VehicleType").trigger("change");


                        $("#Usages").val(vehicle.UsageId);
                        $("#Usages").trigger("chosen:updated");
                        $("#Usages").trigger("change");
                        //

                        $("#VehicleYearsOld").val(vehicle.VehicleYearOld);
                        $("#VehicleYearsOld").parent().addClass('is-dirty');
                        $("#VehicleYearsOld").trigger("change");

                        $("#vehiclePrice").val(vehicle.VehiclePrice);


                        $("#StoreCar").val(vehicle.StoreId);
                        $("#StoreCar").trigger("chosen:updated");
                        $("#StoreCar").trigger("change");

                        $("#fuelType").val(vehicle.SelectedVehicleFuelTypeId);
                        $("#fuelType").trigger("change");


                        LoadVehicle = true;
                        LoadVehicleCurrentID = vehicle.Id;
                        LoadVehicleCurrentSecuenciaVehicleSysflex = vehicle.SecuenciaVehicleSysflex;
                        wasLoadVehicle = true;

                        LoadVehicleQuantity = vehicle.VehicleQuantity;

                        $("#AddVehicle").trigger('click');

                        var randomID = vehicleRandomID;

                        hasVehicle = true;

                        GlobalSaveVehicleOnly = false;

                        if (!randomID) {
                            goToFirstStep = true;
                            return false;
                        }

                        GlobalServicesSelected = vehicle._services;

                        if (vehicle.SurChargePercentage > 0) {
                            getOptionsByValueOfDropWithClassAndSelect('.SurchargePercent', randomID, vehicle.SurChargePercentage, true);

                        }

                        var theText = vehicle.SelectedProductName;
                        getOptionsByTextOfDropWithClassAndSelect('.ddlAllProducts', randomID, theText);

                        theText = vehicle.SelectedCoverageName;
                        getOptionsByTextOfDropWithClassAndSelect('.Coverages', randomID, theText);

                        var pl = vehicle.vehicleProductLimits;

                        if (pl != undefined && pl != null) {
                            var dedu = pl.SelectedDeductibleName;

                            if (dedu != null && dedu != "") {
                                getOptionsByTextOfDropWithClassAndSelect('.Deducible', randomID, dedu);
                            }
                            else {
                                if (pl.SelectedDeductibleCoreId) {
                                    getOptionsByValueOfDropWithClassAndSelect('.Deducible', randomID, pl.SelectedDeductibleCoreId);
                                }
                            }
                        }

                        //Si no permite recotize, setiar mi objeto con las propiedades que vienen desde la db
                        if (!GlobalAllowRequoting) {
                            var vehicleActual = getAllDataVehicleByRandomID(vehicleRandomID);

                            var qtyVehicles = vehicle.VehicleQuantity;

                            var realTotalVehiclesForDiscountFlotilla = data.qtyMaxVehicles > 0 ? data.qtyMaxVehicles : qtyVehiclesByVehicleForRequoting();

                            var makeName = vehicle.VehicleMakeName;
                            var modelName = vehicle.ModelDesc;
                            var year = vehicle.Year;
                            var IsFacultative = false;
                            var AmountFacultative = 0;

                            var getActualAgentSelected = getOldAgentSelected();
                            var ActualAgentSelected = "";

                            if (getActualAgentSelected != null) {
                                ActualAgentSelected = getActualAgentSelected.NameId;
                            }

                            var _insuredAmount = insuredAmount(vehicleRandomID);

                            if (vehicleActual.GlobalDataProductLimits) {

                                if (vehicle.vehicleProductLimits.TpPrime != undefined) {

                                    var TpPrime = vehicle.vehicleProductLimits.TpPrime
                                    var SdPrime = vehicle.vehicleProductLimits.SdPrime
                                    var ServicesPrime = vehicle.vehicleProductLimits.ServicesPrime

                                    vehicleActual.GlobalDataProductLimits.TpPrime = TpPrime;
                                    vehicleActual.GlobalDataProductLimits.SdPrime = SdPrime;
                                    vehicleActual.GlobalDataProductLimits.ServicesPrime = ServicesPrime;

                                    var total = (SdPrime + TpPrime + ServicesPrime) * qtyVehicles;
                                    var iscPercentage = parseFloat(GlobalCurrentIsc);
                                    vehicleActual.GlobalDataProductLimits.TotalIsc = (total * (iscPercentage / 100));

                                    var selectedVehicleTypeId = vehicle.SelectedVehicleTypeId;

                                    var rateJson = vehicle.RateJson;
                                    var porcImpuesto = iscPercentage;

                                    var totalPrimePlusIsc = (total + vehicleActual.GlobalDataProductLimits.TotalIsc);
                                    var ISC = vehicleActual.GlobalDataProductLimits.TotalIsc;

                                    var _isLawProduct = isLawProduct(vehicleRandomID, false, vehicle.SelectedCoverageCoreId);
                                    var idCapacidad = getIdCapacidad(vehicleRandomID, false, vehicle.SelectedProductCoreId);
                                    var descCapacidad = getDescCapacidad(vehicleRandomID, false, vehicle.SelectedProductCoreId);


                                    var $elem = $(".totalPrime");

                                    if ($elem.length > 1) {
                                        $.each($elem, function (idx, obj) {
                                            var randomid = $(obj).attr("data-vehiclerandomid");
                                            if (randomid == vehicleRandomID) {
                                                $elem = $(obj);
                                                $elem.html("$" + number_format(total, 2));
                                            }
                                        });
                                    } else {
                                        $elem.html("$" + number_format(total, 2));
                                    }

                                    var $elem = $(".ISC");

                                    if ($elem.length > 1) {
                                        $.each($elem, function (idx, obj) {
                                            var randomid = $(obj).attr("data-vehiclerandomid");
                                            if (randomid == vehicleRandomID) {
                                                $elem = $(obj);
                                                $elem.html("$" + number_format(ISC, 2));
                                            }
                                        });
                                    } else {
                                        $elem.html("$" + number_format(ISC, 2));
                                    }

                                    var $elem = $(".totalToPay");

                                    if ($elem.length > 1) {
                                        $.each($elem, function (idx, obj) {
                                            var randomid = $(obj).attr("data-vehiclerandomid");
                                            if (randomid == vehicleRandomID) {
                                                $elem = $(obj);
                                                $elem.html("$" + number_format(totalPrimePlusIsc, 2));
                                            }
                                        });
                                    } else {
                                        $elem.html("$" + number_format(totalPrimePlusIsc, 2));
                                    }

                                    if (PercentByQtyVehicle == 0) {

                                        $.ajax({
                                            url: '/Home/GetPercentByQtyVehicle',
                                            type: 'POST',
                                            dataType: 'json',
                                            data: { qtyVehicles: realTotalVehiclesForDiscountFlotilla },
                                            async: false,
                                            success: function (data) {
                                                PercentByQtyVehicle = data;

                                                if (data > 0) {
                                                    isFlotilla = true;
                                                } else {
                                                    isFlotilla = false;
                                                }
                                            }
                                        });
                                    }

                                    var subRamo = vehicle.SelectedCoverageCoreId;

                                    //Solos los que no son de Ley
                                    if (!_isLawProduct) {

                                        IsFacultative = vehicle.IsFacultative;
                                        AmountFacultative = vehicle.AmountFacultative;
                                        var msjFacultative = 'El vehículo Marca: {0} Modelo: {1} Año: {2} Sobrepasa el valor máximo del reaseguro. El monto de la prima puede variar.';
                                        msjFacultative = msjFacultative.replace("{0}", makeName).replace("{1}", modelName).replace("{2}", year);

                                        if (IsFacultative) {
                                            showWarning([msjFacultative], 'Advertencia Reaseguro');
                                        }
                                    }
                                }

                                var totaPrimelbyVH = getTotalPrime(vehicleRandomID);

                                vehicleActual.isLawProduct = _isLawProduct;
                                vehicleActual.GlobalDataProductLimits = vehicleActual.GlobalDataProductLimits;
                                //vehicleActual.servicescoverages = arrayServiceCoverages;
                                //vehicleActual.limitSelfThirdsDamages = arraySelfAndThirdsDamage;

                                vehicleActual.VehicleDescription = makeName + " " + modelName;

                                vehicleActual.InsuredAmount = _insuredAmount;
                                vehicleActual.PercentageToInsure = GlobalpercentageToInsure;
                                vehicleActual.TotalPrime = totaPrimelbyVH;
                                vehicleActual.TotalIsc = (totaPrimelbyVH * (iscPercentage / 100));
                                vehicleActual.SelectedProductCoreId = vehicle.SelectedProductCoreId;
                                vehicleActual.SelectedProductName = vehicle.SelectedProductName;
                                vehicleActual.UsageId = vehicle.UsageId;
                                vehicleActual.UsageName = vehicle.UsageName;
                                vehicleActual.Quotation_Id = quotData.Id;
                                vehicleActual.SelectedVehicleTypeId = selectedVehicleTypeId;
                                vehicleActual.SelectedCoverageCoreId = vehicle.SelectedCoverageCoreId;
                                vehicleActual.SelectedCoverageName = vehicle.SelectedCoverageName;
                                vehicleActual.SurChargePercentage = vehicle.SurChargePercentage;
                                vehicleActual.RateJson = rateJson;
                                vehicleActual.SecuenciaVehicleSysflex = vehicle.SecuenciaVehicleSysflex;
                                vehicleActual.IsFacultative = IsFacultative
                                vehicleActual.AmountFacultative = AmountFacultative
                                vehicleActual.VehicleQuantity = qtyVehicles;
                                vehicleActual.selectedDeductible = vehicle.vehicleProductLimits.SelectedDeductibleCoreId;
                                vehicleActual.idCapacidad = idCapacidad;
                                vehicleActual.descCapacidad = descCapacidad;
                                vehicleActual.isSemifull = vehicle.SelectedCoverageName;
                                vehicleActual.actualAgentSelected = ActualAgentSelected;
                                vehicleActual.porcImpuesto = porcImpuesto;

                                vehicleActual.startDate = GlobalcurrentStartDateSelected;
                                vehicleActual.endDate = GlobalcurrentEndDateSelected;

                                vehicleActual.PercentByQtyVehicle = PercentByQtyVehicle > 0 ? PercentByQtyVehicle : 0;
                                vehicleActual.TotalByQtyVehicle = TotalByQtyVehicle();

                                vehicleActual.iscompletedVehicle = true;

                                var realVehicle = setVehicle(vehicleActual, vehicleRandomID);

                                setAllVehicleDataToSave(realVehicle, "UPDATE");
                            }
                        }
                        //

                        LoadVehicle = false;
                        LoadVehicleCurrentID = 0;
                        LoadVehicleCurrentSecuenciaVehicleSysflex = 0;
                        LoadVehicleQuantity = 1;
                        vehicleRandomID = 0;
                        wasLoadVehicle = false;
                        GlobalServicesSelected = null;
                        GlobalSaveVehicleOnly = true;
                    }
                }

                if (!GlobalAgentDirectSales) {
                    configAppMode();
                }
            });

            GlobalAllowRequoting = true;

            if (hasVehicle) {
                $('#headInfoCC').find('a').trigger('click');
                $('#infoVH').addClass("collapse show");

                if (!goToFirstStep) {
                    $('#selecCober').addClass("collapse show");
                }
                refreshDiscountSectionAllVehicle();
            }
            isLoadingQuot = false;

            if ($('#AllowRedirect').val() == 'true' && !goToFirstStep) {
                if (lasQuotationStep > 1) {
                    switch (lasQuotationStep) {
                        case 2: // redireccionar al summary
                            $('#AllowRedirect').val('false');
                            $('.returnStep2').click();
                            break;
                        case 3:
                            $('#AllowRedirect').val('false');
                            $('.returnStep3').click();
                            break;
                        case 4:
                            $('#AllowRedirect').val('false');
                            $('.returnStep4').click();
                            break;
                        case 5://Documentos Requeridos
                            $('#AllowRedirect').val('false');
                            $('.returnStep5').click();
                            break;
                        case 6://Pantalla Pago
                            $('#AllowRedirect').val('false');
                            $('.returnStep6').click();
                            break;
                        default:
                            break;
                    }
                }
            }
            //
        }
    });


}

function GetURLParameter() {
    var sPageURL = window.location.href;
    sPageURL = sPageURL.replace('#', '').replace("%3d", "=");
    var indexOfLastSlash = sPageURL.lastIndexOf("/");
    var returnValue = "";

    if (sPageURL.toLowerCase().indexOf('?m=') !== -1) {
        return 0;
    }

    if (indexOfLastSlash > 0 && sPageURL.length - 1 != indexOfLastSlash) {
        returnValue = sPageURL.substring(indexOfLastSlash + 1);

        if (returnValue.toLowerCase() == 'index') {
            return 0;
        }

        return returnValue;
    }
    else {
        return 0;
    }
}

function GetProductsFromSysflex(brand, model, year, isFlotillaMode, randomId) {

    if (brand > 0 && model > 0 && year > 0) {
        var AgentCode = "";

        if ($("#AgentList").val()) {
            var SelectedAgent = JSON.parse($("#AgentList").val());
            AgentCode = SelectedAgent.AgentCode;
        }

        $.ajax({
            url: "/Home/GetVehicleTypes_New",
            dataType: 'json',
            async: false,
            cache: false,
            data: {
                brandId: brand,
                modelId: model,
                vehicleYear: year,
                AgentCode: AgentCode
            },
            success: function (data) {
                $("#lastModelSelected").val(model);

                GlobalData = data;
                fillDropVehicleTypes(GlobalData, isFlotillaMode, randomId);
            }
        });
    }
    else {
        return [];
    }
}

function fillDropVehicleTypes(vehicleTypes, isFlotillaMode, randomId) {

    var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlVehicleType', randomId) : $("#VehicleType");
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');

    $.each(vehicleTypes, function (idx, obj) {
        $select_elem.append("<option value='" + obj.Name + "'>" + obj.Name + "</option>");
    });
    $select_elem.trigger("chosen:updated");
    $select_elem.trigger("change");
}

function UsagesListByVehicleType(selectedVehicleType, isFlotillaMode, randomId) {

    if (selectedVehicleType) {

        var usagebytype;

        var t = GlobalData.filter(function (item) {
            return item.Name == selectedVehicleType;
        });


        if (t != undefined) {
            usagebytype = t[0].NewUsages;
        }

        $.ajax({
            url: '/Home/GetUsageStates',
            dataType: 'json',
            async: isEditingVehicle,
            success: function (data) {

                $.each(data, function (idx, us) {

                    var obj = {
                        idUso: us.id,
                        descUso: us.name,
                        allowed: us.allowed,
                        message: us.message
                    }

                    usagebytype.push(obj);
                });
            }
        });

        GlobalDataUsages = usagebytype;

        var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlUsages', randomId) : $("#Usages");
        $select_elem.empty();
        $select_elem.append('<option value=""></option>');

        $.each(usagebytype, function (idx, obj) {
            $select_elem.append("<option value='" + obj.idUso + "'>" + obj.descUso + "</option>");
        });
        $select_elem.trigger("chosen:updated");
        $select_elem.trigger("change");
    }
    return [];
}

function enableVehicleOldCombo(vehicleYear, isFlotillaMode, randomId) {

    var VehicleYearsOld = isFlotillaMode == true ? getHtmlElementByClass('ddlVehicleYearsOld', randomId) : $("#VehicleYearsOld ");

    if (vehicleYear > 0) {

        var QtyYearsBack0KmVip = "";

        $.ajax({
            url: "/Home/GetQtyYearsBack0KmVip",
            dataType: 'json',
            async: isEditingVehicle,
            success: function (data) {
                QtyYearsBack0KmVip = data;
            }
        });


        if (!QtyYearsBack0KmVip) {
            QtyYearsBack0KmVip = 2;
        }
        var currentYear = new Date().getFullYear();


        if ((currentYear - QtyYearsBack0KmVip) <= vehicleYear && vehicleYear <= (currentYear + 1)) {

            //var isFirstLoading = self.isFirstLoading();
            var isDisabled = VehicleYearsOld.is("disabled");
            var lastvalue = $("#LastSelectedVehicleYearsOld").val();
            var actualvalue = VehicleYearsOld.val();


            if (/*isFirstLoading == false &&*/ lastvalue == actualvalue) {
                VehicleYearsOld.val("");
                $("#LastSelectedVehicleYearsOld").val(VehicleYearsOld.val());
            }

            VehicleYearsOld.parent().removeClass("is-dirty");
            VehicleYearsOld.removeAttr("disabled");
            VehicleYearsOld.trigger("change");
        }
        else {

            VehicleYearsOld.val("Usado");
            VehicleYearsOld.trigger("chosen:updated");
            VehicleYearsOld.parent().addClass("is-dirty");
            VehicleYearsOld.trigger("change");

            $("#LastSelectedVehicleYearsOld").val(VehicleYearsOld.val());
            //self.isFirstLoading(false);

            VehicleYearsOld.attr("disabled", "disabled");
        }
    }
    else {
        VehicleYearsOld.removeAttr("disabled");
        VehicleYearsOld.trigger("change");
    }
}

function getStorages(isFlotillaMode, randomId) {

    $.ajax({
        url: "/Home/GetStoreStates",
        dataType: 'json',
        async: false,
        success: function (data) {

            //var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlStoreCar', randomId) : $("#StoreCar");
            var $select_elem = $("#StoreCar");
            $select_elem.empty();
            $select_elem.append('<option value=""></option>');
            $.each(data, function (idx, obj) {
                $select_elem.append("<option value='" + obj.id + "'>" + obj.name + "</option>");
            });
            $select_elem.trigger("chosen:updated");

            if (isFlotillaMode == true) {

                $select_elem = $("#ddlStoreCarHidden");
                $select_elem.empty();
                $select_elem.append('<option value=""></option>');
                $.each(data, function (idx, obj) {
                    $select_elem.append("<option value='" + obj.id + "'>" + obj.name + "</option>");
                });
            }
        }
    });
}

function ProductListByUsages(usageSelected, isFlotillaMode, randomId) {

    if (usageSelected) {

        var UsageFound = GlobalDataUsages.filter(function (item) {
            return item.idUso == usageSelected;
        });

        if (UsageFound.length) {
            var currentUsageFound = UsageFound[0];

            var selectedVehicleType = isFlotillaMode == true ? getHtmlElementByClass('ddlVehicleType', randomId).val() : $("#VehicleType").val();

            var t = GlobalData.filter(function (item) {
                return item.Name == selectedVehicleType;
            });

            var AllProductListByUsages;

            if (t.length > 0) {
                AllProductListByUsages = t[0].ProductByUsages;
                GlobalAllProducts = t[0].Products;

                var arrProds = listProductsNotShow(GlobalAppMode);

                if (GlobalAppMode == "LEYMODE") {

                    //GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                    //    return item.Name.indexOf("DE LEY") != -1 || item.Name.indexOf("ULTRA") != -1;
                    //});

                    //AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                    //    return item.ProductoDescripcion.indexOf("DE LEY") != -1 || item.ProductoDescripcion.indexOf("ULTRA") != -1;
                    //});

                    GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                        return arrProds.indexOf(item.Name) != -1;
                    });

                    AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                        return arrProds.indexOf(item.ProductoDescripcion) != -1;
                    });
                }

                if (GlobalAppMode == "FULLMODE") {
                    //var arrProds = listProductsNotShow();

                    //GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                    //    return arrProds.indexOf(item.Name) == -1;
                    //});

                    //AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                    //    return arrProds.indexOf(item.ProductoDescripcion) == -1;
                    //});

                    GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                        return arrProds.indexOf(item.Name) != -1;
                    });

                    AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                        return arrProds.indexOf(item.ProductoDescripcion) != -1;
                    });
                }
            }

            var FilteredProductList = AllProductListByUsages.filter(function (item) {
                return item.UsoDescripcion.indexOf(currentUsageFound.descUso) != -1;
            });

            var $select_elem = $(".ddlAllProducts");

            if ($select_elem.length > 1) {
                $.each($select_elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == vehicleRandomID) {
                        $select_elem = $(obj);
                    }
                });
            } else {
                var hasValue = $select_elem.val();
                if (hasValue != "") {
                    if (isFlotillaMode != true) {
                        return;
                    }
                }
            }

            if (isFlotillaMode == true) {
                $select_elem = getHtmlElementByClass('ddlPlan', randomId);
            }

            if (FilteredProductList.length > 0) {

                var NewCurrentProductList = [];

                $.each(FilteredProductList, function (idx, plist) {

                    var prodsNews = altFind(GlobalAllProducts, function (currProd) {
                        return currProd.Name.indexOf(plist.ProductoDescripcion) != -1
                    });

                    if (prodsNews != undefined) {
                        NewCurrentProductList.push(prodsNews);
                    }
                });

                GlobalDataProductsByUsage = NewCurrentProductList;

                if (LoadVehicle == false && $select_elem.length == 1 && wasLoadVehicle == false) {
                    $select_elem.empty();
                    $select_elem.append('<option value="">Seleccionar</option>');
                    var firstValue = true;

                    $.each(NewCurrentProductList, function (idx, obj) {

                        if (firstValue && GlobalAppMode == "LEYMODE") {
                            $select_elem.append("<option value='" + obj.Id + "' selected='selected'>" + obj.Name + "</option>");
                            firstValue = false;
                        }
                        else {
                            $select_elem.append("<option value='" + obj.Id + "'>" + obj.Name + "</option>");
                        }
                    });
                }
            }
            else {
                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
            }
        }
    }
    return [];
}

function currentCoveragesByUsage(selectedProductName, randomID, isFlotillaMode) {

    if ((selectedProductName || GlobalAppMode == "LEYMODE")) {

        var vehicle = getAllDataVehicleByRandomID(randomID);

        var usage = vehicle.UsageId; //$("#Usages").val();
        var selectedVehicleType = vehicle.SelectedVehicleTypeName; //$("#VehicleType").val();        

        var vehGlobalDataUsages = vehicle.GlobalDataUsages;
        var vehGlobalData = vehicle.GlobalData;

        var UsageFound = altFind(vehGlobalDataUsages, function (item) {
            return item.idUso == usage
        });

        if (UsageFound != undefined) {

            var t = vehGlobalData.filter(function (item) {
                return item.Name == selectedVehicleType;
            });

            var AllCoveragesListByUsages;

            if (t.length > 0) {
                AllCoveragesListByUsages = t[0].CoveragesByUsages;

                if (GlobalAppMode == "LEYMODE") {

                    var newGlobalAllProductsLey = AllCoveragesListByUsages.filter(function (item) {
                        return item.ProductName.indexOf("DE LEY") != -1;
                    });

                    var newGlobalAllProductsUltra = AllCoveragesListByUsages.filter(function (item) {
                        return item.ProductName.indexOf("ULTRA") != -1;
                    });

                    AllCoveragesListByUsages = newGlobalAllProductsLey.concat(newGlobalAllProductsUltra);
                }
            }

            if (GlobalAppMode != "LEYMODE") {

                var FilteredCovList = AllCoveragesListByUsages.filter(function (item) {
                    return item.UsoDescripcion.indexOf(UsageFound.descUso) != -1 && item.ProductName == selectedProductName;
                });
            }
            else {

                var FilteredCovList = AllCoveragesListByUsages.filter(function (item) {
                    return item.UsoDescripcion.indexOf(UsageFound.descUso) != -1;
                });
            }

            GlobalDataCoverages = FilteredCovList;

            vehicle.GlobalDataCoverages = GlobalDataCoverages
            var realv = setVehicle(vehicle);
            setAllVehicleDataToSave(realv, "UPDATE");

            if (isFlotillaMode == true) {
                var $select_elem = getHtmlElementByClass('ddlCoverages', randomID);

                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
                $.each(GlobalDataCoverages, function (idx, obj) {
                    $select_elem.append("<option value='" + obj.Id + "' data-productname='" + obj.ProductName + "'>" + obj.Name + "</option>");
                });
                $select_elem.trigger('change');

                $select_elem = getHtmlElementByClass('ddlDeducible', randomID);
                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
                $select_elem.trigger('change');

            } else {

                $(".Coverages").off("change");
                $(".Coverages").change(function () {
                    var $this = $(this);
                    var randomID = $this.attr("data-vehiclerandomid");

                    updateGlobalDataProductLimits(randomID, $this.val());

                    showRechargeAllLawProducts($this.val(), randomID);

                    var $elem = $(".servicesPopUp");
                    if ($elem.length > 1) {
                        $.each($elem, function (idx, obj) {
                            var randomid = $(obj).attr("data-vehiclerandomid");
                            if (randomid == randomID) {
                                $elem = $(obj);
                            }
                        });
                    }

                    if ($this.val() !== '' && hasServices) {
                        $elem.removeAttr('disabled');
                    } else {
                        $elem.attr('disabled', 'disabled');
                    }

                    var parentDiv = $this.parent();
                    if ($this.val() == "") {
                        if (parentDiv.hasClass('erarequerido')) {
                            parentDiv.addClass('requerido');
                            parentDiv.removeClass('erarequerido');
                        }

                        var $li_elem = $(".liCoverageExplication");

                        if ($li_elem.length > 1) {
                            $.each($li_elem, function (idx, obj) {
                                var randomid = $(obj).attr("data-vehiclerandomid");
                                if (randomid == randomID) {
                                    $li_elem = $(obj);
                                }
                            });
                        }

                        $li_elem.hide();


                    } else {
                        parentDiv.removeClass('requerido');
                        parentDiv.addClass('erarequerido');
                    }

                    if (GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") {

                        var cov = $this.val();
                        if (cov) {

                            var prodname = $this.find(':selected').data('productname');

                            if (prodname) {

                                if (GlobalAppMode == "LEYMODE") {

                                    var $select_elemProd = $(".ddlAllProducts");

                                    if ($select_elemProd.length > 1) {
                                        $.each($select_elemProd, function (idx, obj) {
                                            var randomid = $(obj).attr("data-vehiclerandomid");
                                            if (randomid == randomID) {
                                                $select_elemProd = $(obj);
                                            }
                                        });
                                    }

                                    $("#" + $select_elemProd.attr('id') + " option").each(function () {
                                        if ($(this).text() == prodname) {
                                            $(this).attr('selected', 'selected');
                                        }
                                        else {
                                            if ($(this).attr('selected')) {
                                                $(this).removeAttr('selected');
                                            }
                                        }
                                    });

                                    GlobalChangeForModeLey = true;
                                    $select_elemProd.trigger('change');
                                }

                                //lleno div de descripcion covertura
                                var $li_elem = $(".liCoverageExplication");

                                if ($li_elem.length > 1) {
                                    $.each($li_elem, function (idx, obj) {
                                        var randomid = $(obj).attr("data-vehiclerandomid");
                                        if (randomid == randomID) {
                                            $li_elem = $(obj);
                                        }
                                    });
                                }

                                $.ajax({
                                    url: '/Home/GetCoverageExplication',
                                    type: 'POST',
                                    dataType: 'json',
                                    data: { coverageID: cov },
                                    success: function (result) {
                                        if (result.data) {
                                            $li_elem.show();
                                            $li_elem.find('.coveragetexthere').text(result.data.Description);
                                        } else {
                                            $li_elem.hide();
                                        }
                                    }
                                });
                                //
                            }
                        }
                    }
                });

                var $select_elem = $(".Coverages");
                if ($select_elem.length > 1) {
                    $.each($select_elem, function (idx, obj) {
                        var randomid = $(obj).attr("data-vehiclerandomid");
                        if (randomid == randomID) {
                            $select_elem = $(obj);
                        }
                    });
                }

                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
                $.each(GlobalDataCoverages, function (idx, obj) {
                    $select_elem.append("<option value='" + obj.Id + "' data-productname='" + obj.ProductName + "'>" + obj.Name + "</option>");
                });

                var $select_elem = $(".Deducible");
                if ($select_elem.length > 1) {
                    $.each($select_elem, function (idx, obj) {
                        var randomid = $(obj).attr("data-vehiclerandomid");
                        if (randomid == randomID) {
                            $select_elem = $(obj);
                        }
                    });
                }
                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
            }
        }
    } else {

        if (isFlotillaMode == true) {
            var $select_elem = getHtmlElementByClass('ddlCoverages', randomID);

            $select_elem.empty();
            $select_elem.append('<option value="">Seleccionar</option>');
            $select_elem.trigger('change');

            $select_elem = getHtmlElementByClass('ddlDeducible', randomID);
            $select_elem.empty();
            $select_elem.append('<option value="">Seleccionar</option>');


        }
        else {

            var $select_elem = $(".Coverages");
            if ($select_elem.length > 1) {
                $.each($select_elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == randomID) {
                        $select_elem = $(obj);
                    }
                });
            }
            $select_elem.empty();
            $select_elem.append('<option value="">Seleccionar</option>');

            $select_elem = $(".Deducible");
            if ($select_elem.length > 1) {
                $.each($select_elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == randomID) {
                        $select_elem = $(obj);
                    }
                });
            }
            $select_elem.empty();
            $select_elem.append('<option value="">Seleccionar</option>');
        }
    }
}

function updateGlobalDataProductLimits(randomID, realSelectedCoverage, isFlotillaMode) {

    var brand = "";
    var model = "";
    var year = "";
    var vehiclePrice = "";
    var selectedCoverage = "";
    var vehicle = null;

    if (randomID == undefined) {
        brand = isFlotillaMode == true ? getHtmlElementByClass('ddlMakes', randomID).val() : $("#VehicleModel_Make_Id option:selected").val();
        model = isFlotillaMode == true ? getHtmlElementByClass('ddlModels', randomID).val() : $("#VehicleModel_Model_Id option:selected").val();
        year = isFlotillaMode == true ? getHtmlElementByClass('ddlYears', randomID).val() : $("#yearsAvaibles option:selected").val();
        vehiclePrice = isFlotillaMode == true ? parseFloat(getHtmlElementByClass('vehiclePrice_Flot', randomID).val().replace(/,/g, '')) : parseFloat($("#vehiclePrice").val().replace(/,/g, ''));
        selectedCoverage = ""; //$("#Coverages option:selected").val();
    }
    else if (isEditingVehicle && randomID != undefined) {
        brand = $("#VehicleModel_Make_Id option:selected").val();
        model = $("#VehicleModel_Model_Id option:selected").val();
        year = $("#yearsAvaibles option:selected").val();
        vehiclePrice = parseFloat($("#vehiclePrice").val().replace(/,/g, ''));
        selectedCoverage = getOptionSelectedDropWithClass('.Coverages', randomID); //  $("#Coverages option:selected").val();
        vehicle = getAllDataVehicleByRandomID(randomID);
    }
    else {
        vehicle = getAllDataVehicleByRandomID(randomID);
        brand = vehicle.VehicleModel_Make_Id;
        model = vehicle.VehicleModel_Model_Id;
        year = vehicle.Year;
        vehiclePrice = vehicle.VehiclePrice;
        selectedCoverage = vehicle.SelectedCoverageCoreId == undefined ? realSelectedCoverage : (realSelectedCoverage != vehicle.SelectedCoverageCoreId ? realSelectedCoverage : vehicle.SelectedCoverageCoreId);
    }

    if (vehiclePrice == 0) {
        if (ApplyToZero(randomID, isFlotillaMode)) {
            vehiclePrice = 1;
        }
    }

    if (selectedCoverage && brand && year && vehiclePrice) {

        $.ajax({
            url: "/Home/GetCoverageDetailsOfVehicle",
            dataType: 'json',
            async: isEditingVehicle,
            data: {
                coverageCoreId: selectedCoverage,
                makeId: brand,
                modelId: model,
                vehiclePrice: vehiclePrice
            },
            success: function (data) {

                GlobalDataProductLimits = data.coverageLimits;
                GlobalDataDeductibleList = data.deductibles;

                if (vehicle != undefined) {

                    vehicle.GlobalDataProductLimits = GlobalDataProductLimits;

                    var ddlCov = getHtmlElementByClass('ddlCoverages', randomID);
                    var ddlCovTex = ddlCov.find("option:selected").text();


                    //chequeo si es un semifull
                    var SelectedCoverageName = isFlotillaMode == true ? ddlCovTex : getOptionSelectedTextDropWithClass(".Coverages", randomID);
                    var isSemifull = SelectedCoverageName.toLowerCase().indexOf('semi');
                    var issemi = (isSemifull != -1);

                    if (issemi) {
                        vehicle.GlobalDataDeductibleList = [];
                    } else {
                        vehicle.GlobalDataDeductibleList = GlobalDataDeductibleList;
                    }

                    var realVehicle = setVehicle(vehicle);
                    setAllVehicleDataToSave(realVehicle, "UPDATE");

                    var ServicesCoverages = vehicle.GlobalDataProductLimits.ServicesCoverages;

                    if (ServicesCoverages.length <= 0) {
                        hasServices = false;
                    } else {
                        hasServices = true;
                    }
                }

                getDeducibles(GlobalDataDeductibleList, randomID, isFlotillaMode);

                if (randomID !== undefined) {
                    SetAdditionals(randomID, isFlotillaMode);
                }

                if (selectedCoverage && GlobalAppMode == "LEYMODE") {

                    var $select_elemCov = $(".Coverages");

                    if ($select_elemCov.length > 1) {

                        $.each($select_elemCov, function (idx, obj) {

                            var randomid = $(obj).attr("data-vehiclerandomid");

                            if (randomid == randomID) {
                                $select_elemCov = $(obj);
                            }
                        });
                    }

                    var prodname = $select_elemCov.find(':selected').data('productname');

                    if (prodname) {

                        var $select_elemProd = $(".ddlAllProducts");

                        if ($select_elemProd.length > 1) {
                            $.each($select_elemProd, function (idx, obj) {
                                var randomid = $(obj).attr("data-vehiclerandomid");
                                if (randomid == randomID) {
                                    $select_elemProd = $(obj);
                                }
                            });
                        }

                        $("#" + $select_elemProd.attr('id') + " option").each(function () {
                            if ($(this).text() == prodname) {
                                $(this).attr('selected', 'selected');
                            }
                            else {
                                if ($(this).attr('selected')) {
                                    $(this).removeAttr('selected');
                                }
                            }
                        });

                        GlobalChangeForModeLey = true;
                        $select_elemProd.trigger('change');
                    }
                }

                if (isFlotillaMode == true) {

                    getRatesFlotMode(randomID);

                    isEditingVehicle = true;
                }

                if (!isEditingVehicle) {
                    getRates(randomID);
                }

                if (isFlotillaMode == true) {
                    isEditingVehicle = false;
                }
            }
        });
    }
    else {
        GlobalDataProductLimits = null;
        if (vehicle != undefined) {
            vehicle.GlobalDataProductLimits = null;
        }
    }
}

function getDeducibles(deductibleList, randomID, isFlotillaMode) {

    var vh = getAllDataVehicleByRandomID(randomID);

    if (isFlotillaMode == true) {
        //$(".ddlDeducible").off("change");
        //$(".ddlDeducible").change(function () { deducibleChange(this) });
    } else {
        $(".Deducible").off("change");
        $(".Deducible").change(function () {
            var $this = $(this);

            //$("#lastDeducibleSelected").val($this.val());
            var randomID = $this.attr("data-vehiclerandomid");
            //var realSelectedCoverage = getOptionSelectedDropWithClass(".Coverages", randomID);
            //updateGlobalDataProductLimits(randomID, realSelectedCoverage);

            if ($this.val() !== "") {
                getRates(randomID);
            }

            var parentDiv = $this.parent();
            if ($this.val() == "") {
                if (parentDiv.hasClass('erarequerido')) {
                    parentDiv.addClass('requerido');
                    parentDiv.removeClass('erarequerido');
                }
            } else {
                parentDiv.removeClass('requerido');
                parentDiv.addClass('erarequerido');
            }
        });
    }

    var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlDeducible', randomID) : $(".Deducible");

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }
    $select_elem.empty();
    $select_elem.append('<option value="">Seleccionar</option>');

    if ((vh != undefined && vh.GlobalDataDeductibleList.length > 0) || deductibleList.length > 0) {
        $select_elem.removeAttr('disabled');
    } else {
        $select_elem.attr('disabled', 'disabled');

        var parentDiv = $select_elem.parent();
        parentDiv.removeClass('requerido');
        parentDiv.addClass('erarequerido');
        return false;
    }

    var lastSelected = "";
    var exist = false;

    $.each(deductibleList, function (idx, obj) {
        lastSelected = parseInt($("#lastDeducibleSelected").val());
        lastSelected = !isNaN(lastSelected) ? lastSelected : "";

        if (lastSelected == obj.CoreId) {
            exist = true;
        }
        $select_elem.append("<option value='" + obj.CoreId + "'>" + obj.Name + "</option>");
    });

    var islaw = isLawProduct(randomID, null, null, isFlotillaMode);
    if (islaw) {
        exist = false;
        $select_elem.attr('disabled', 'disabled');
    } else {
        //chequeo si es un semifull
        var SelectedCoverageName = getOptionSelectedTextDropWithClass(".Coverages", randomID);
        var isSemifull = SelectedCoverageName.toLowerCase().indexOf('semi');
        var issemi = (isSemifull != -1);

        if (issemi) {
            exist = false;
            $select_elem.attr('disabled', 'disabled');
        }
    }

    if (isEditingVehicle) {
        exist = false;
    }

    if (exist) {
        $select_elem.val(lastSelected);
    }
}

function getRates(randomID, isRequoting) {

    var vehicle = getAllDataVehicleByRandomID(randomID);
    var allGood = false;
    if (vehicle != null) {

        if (vehicle.GlobalDataProductLimits) {

            vehicle.iscompletedVehicle = false;

            if (validateGetRates(randomID)) {

                if (!GlobalAllowRequoting) {
                    return;
                }

                var servicesIdList = [];

                var ServicesCoverages = vehicle.GlobalDataProductLimits.ServicesCoverages;

                if (ServicesCoverages) {
                    var allCoverages = new Array();
                    $.each(ServicesCoverages, function (idx, item) { $.each(item.Coverages, function (idx, sItem) { allCoverages.push(sItem) }); });
                    $.each(allCoverages.filter(function (idx, item) { return item.IsSelected; }), function (idx, item) { servicesIdList.push(item.CoverageDetailCoreId); });
                }

                var getQuotationNumberForRates = $("#quotationID").val();
                var getQuotationNumber = $("#QuotationNumber").val();


                isRequoting = (isRequoting == false || isRequoting == undefined) ? false : true;

                var _qtyVehiclesByVehicle = isRequoting == false ? qtyVehiclesByVehicle() : qtyVehiclesByVehicleForRequoting();

                var realTotalVehiclesForDiscountFlotilla = _qtyVehiclesByVehicle;

                var q = $(".qtyVehicles");
                if (q.length > 1) {
                    $.each(q, function (idx, obj) {
                        var randomid = $(obj).attr("data-vehiclerandomid");
                        if (randomid == randomID) {
                            q = $(obj);
                        }
                    });
                }

                var qtyVehicles = isRequoting == false ? q.val() : vehicle.VehicleQuantity;

                var quotationCoreNumber = $("#QuotationCoreNumber").val();

                //Original
                var NewAgentID = "";
                var NewAgent = getNewAgentSelected();

                if (NewAgent != null) {
                    NewAgentID = NewAgent.NameId;
                }

                var getActualAgentSelected = getOldAgentSelected();
                var ActualAgentSelected = "";

                if (getActualAgentSelected != null) {
                    ActualAgentSelected = getActualAgentSelected.NameId;
                }

                var principalDateOfBirth = isRequoting == false ? $("#DateOfBirth").val() : ((vehicle.driverdob == undefined || vehicle.driverdob == null) ? vehicle.principalDateOfBirth : vehicle.driverdob);
                var wasChangeDateBirth = false;
                if (changedDateBirth == true) {
                    wasChangeDateBirth = true;
                }

                var clientSex = isRequoting == false ? $("#Sex").val() : ((vehicle.driversex == undefined || vehicle.driversex == null) ? vehicle.principalSex : vehicle.driversex);
                var wasChangeClientSex = false;
                if (changedSex == true) {
                    wasChangeClientSex = true;
                }

                var getForeingLicenceDriver = isRequoting == false ? $("#ForeignLicense").val() : ((vehicle.driverforeignlicense == undefined || vehicle.driverforeignlicense == null) ? vehicle.principalrforeignlicense : vehicle.driverforeignlicense);

                var arraySelfAndThirdsDamage = [];
                var arrayServiceCoverages = [];

                if (vehicle.GlobalDataProductLimits.SelfDamagesCoverages) {
                    $.each(vehicle.GlobalDataProductLimits.SelfDamagesCoverages, function (idx, item) {

                        var AsociativeArraySelfAndThirdsDamage = {}
                        AsociativeArraySelfAndThirdsDamage["CoverageDetailCoreId"] = item.CoverageDetailCoreId;
                        AsociativeArraySelfAndThirdsDamage["Limit"] = item.Limit;
                        AsociativeArraySelfAndThirdsDamage["Name"] = item.Name;

                        arraySelfAndThirdsDamage.push(AsociativeArraySelfAndThirdsDamage);
                    });
                }

                if (vehicle.GlobalDataProductLimits.ThirdPartyCoverages) {
                    $.each(vehicle.GlobalDataProductLimits.ThirdPartyCoverages, function (idx, item) {

                        AsociativeArraySelfAndThirdsDamage = {}
                        AsociativeArraySelfAndThirdsDamage["CoverageDetailCoreId"] = item.CoverageDetailCoreId;
                        AsociativeArraySelfAndThirdsDamage["Limit"] = item.Limit;
                        AsociativeArraySelfAndThirdsDamage["Name"] = item.Name;

                        arraySelfAndThirdsDamage.push(AsociativeArraySelfAndThirdsDamage);
                    });
                }


                if (vehicle.GlobalDataProductLimits.ServicesCoverages) {
                    var allCoverages = new Array();
                    $.each(vehicle.GlobalDataProductLimits.ServicesCoverages, function (idx, item) {
                        $.each(item.Coverages, function (idx, sItem) {

                            var AsociativearrayServiceCoverages = {}
                            AsociativearrayServiceCoverages["CoverageDetailCoreId"] = sItem.CoverageDetailCoreId;
                            AsociativearrayServiceCoverages["Limit"] = sItem.Limit;
                            AsociativearrayServiceCoverages["Name"] = sItem.Name;
                            AsociativearrayServiceCoverages["isSelected"] = sItem.IsSelected;

                            arrayServiceCoverages.push(AsociativearrayServiceCoverages);
                        });
                    });
                }

                var limitSelfThirdJson = JSON.stringify(arraySelfAndThirdsDamage);
                var serviceCoberageJson = JSON.stringify(arrayServiceCoverages);

                var usage = vehicle.UsageId;
                var usageName = "";

                var UsageFound = altFind(vehicle.GlobalDataUsages, function (item) { return item.idUso == usage });

                if (UsageFound) {
                    usageName = UsageFound.descUso;

                    var allowed = UsageFound.allowed;
                    var message = UsageFound.message;

                    //No debe generar prima
                    if (allowed == 2) {
                        return;
                    }
                }

                var asyncOrNo = false; //self.parent.changeDate() ? false : true;

                var SecuenciaVehicleSysflex = vehicle.SecuenciaVehicleSysflex;

                var coverageCoreId = isRequoting == false ? getOptionSelectedDropWithClass(".Coverages", randomID) : vehicle.SelectedCoverageCoreId;
                var SelectedCoverageName = isRequoting == false ? getOptionSelectedTextDropWithClass(".Coverages", randomID) : vehicle.SelectedCoverageName;
                var isSemifull = SelectedCoverageName;
                var selectedVehicleType = vehicle.SelectedVehicleTypeId;


                var brand = vehicle.VehicleModel_Make_Id;
                var model = vehicle.VehicleModel_Model_Id;
                var makeName = vehicle.VehicleMakeName;
                var modelName = vehicle.VehicleModelName;

                var year = vehicle.Year;

                var selectedDeductible = isRequoting == false ? getOptionSelectedDropWithClass(".Deducible", randomID) : vehicle.selectedDeductible;
                var store = vehicle.StoreId;
                var storeName = vehicle.StoreName;
                var selectedSurchargePercent = isRequoting == false ? getOptionSelectedDropWithClass(".SurchargePercent", randomID) : vehicle.SurChargePercentage;


                var _insuredAmount = insuredAmount(randomID);

                var IsFacultative = false;
                var AmountFacultative = 0;

                var selectedVehicleTypeId = -1;
                var rateJson = "";
                var porcImpuesto = 0;

                var VehicleYearsOld = vehicle.VehicleYearsOld;
                var VehicleDriver = vehicle.Driver_Id;
                var vehiclePrice = vehicle.VehiclePrice;


                var SelectedProductCore = isRequoting == false ? getOptionSelectedDropWithClass(".ddlAllProducts", randomID) : vehicle.SelectedProductCoreId;
                var SelectedProductName = isRequoting == false ? getOptionSelectedTextDropWithClass(".ddlAllProducts", randomID) : vehicle.SelectedProductName;

                var _isLawProduct = isLawProduct(randomID, isRequoting, coverageCoreId);
                var idCapacidad = getIdCapacidad(randomID, isRequoting, SelectedProductCore);
                var descCapacidad = getDescCapacidad(randomID, isRequoting, SelectedProductCore);

                var z = GlobalcurrentStartDateSelected;
                var qq = GlobalcurrentEndDateSelected;

                var fuelTypeId = vehicle.SelectedVehicleFuelTypeId;
                var fuelTypeDesc = vehicle.SelectedVehicleFuelTypeDesc;

                if (GlobalAppMode == "LEYMODE") {
                    store = "6";//Casa
                }

                $.ajax({
                    url: '/Home/GetRates',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        coverageCoreId: coverageCoreId,
                        productId: selectedVehicleType,
                        brandId: brand,
                        modelId: model,
                        vehicleYear: year,
                        coveragePercent: GlobalpercentageToInsure,
                        startDate: GlobalcurrentStartDateSelected,
                        endDate: GlobalcurrentEndDateSelected,
                        insuredAmount: _insuredAmount,
                        servicesIdLstoreist: servicesIdList.join(),
                        deductibleId: selectedDeductible,
                        gender: clientSex,
                        principalDateOfBirth: principalDateOfBirth,
                        storageId: store,
                        percentSurCharge: selectedSurchargePercent,
                        QuotationNumberForRates: getQuotationNumberForRates,
                        LicenciaExtranjera: getForeingLicenceDriver,
                        qtyVehicles: qtyVehicles,
                        usage: usage,
                        secuencia: SecuenciaVehicleSysflex,
                        agentChangeSelected: NewAgentID,
                        quotationCore: quotationCoreNumber,
                        Esdeley: _isLawProduct,
                        idCapacidad: idCapacidad,
                        descCapacidad: descCapacidad,
                        coverages: serviceCoberageJson,
                        limitself: limitSelfThirdJson,
                        usagename: usageName,
                        isSemifull: isSemifull,
                        QuotationNumber: getQuotationNumber,
                        wasChangeDateBirth: wasChangeDateBirth,
                        wasChangeClientSex: wasChangeClientSex,
                        actualAgentSelected: ActualAgentSelected,
                        fuelTypeId: fuelTypeId,
                        fuelTypeDesc: fuelTypeDesc
                    },
                    async: isRequoting == false ? (LoadVehicle ? false : true) : false,//asyncOrNo,
                    success: function (data) {

                        var totalAllDiscountsByVH = 0;

                        if (data.messageError) {
                            showError([data.messageError], 'Error obteniendo la prima');
                            return false;
                        }

                        if (vehicle.GlobalDataProductLimits) {

                            if (data.TpPrime != undefined) {

                                vehicle.GlobalDataProductLimits.TpPrime = data.TpPrime;
                                vehicle.GlobalDataProductLimits.SdPrime = data.SdPrime;
                                vehicle.GlobalDataProductLimits.ServicesPrime = data.ServicesPrime;

                                var total = (data.SdPrime + data.TpPrime + data.ServicesPrime) * qtyVehicles;

                                //totalAllDiscountsByVH = getTotalDiscountByVHFlotilla(total);//Descuento Flotilla
                                //var totalMinusFlotillaDiscount = (total - totalAllDiscountsByVH);
                                //totalAllDiscountsByVH += getTotalDiscountByVHCoupon(totalMinusFlotillaDiscount);//Descuento Cupon

                                var iscPercentage = parseFloat(GlobalCurrentIsc);
                                vehicle.GlobalDataProductLimits.TotalIsc = (total * (iscPercentage / 100));

                                selectedVehicleTypeId = data.VehicleTypeId;

                                rateJson = data.jsonRates;
                                porcImpuesto = iscPercentage;

                                var totalPrimePlusIsc = (total + vehicle.GlobalDataProductLimits.TotalIsc);
                                var ISC = vehicle.GlobalDataProductLimits.TotalIsc;

                                /*var totalMinusDiscount = 0;

                                if (totalAllDiscountsByVH > 0) {

                                    totalMinusDiscount = (total - totalAllDiscountsByVH);

                                    ISC = (totalMinusDiscount * (iscPercentage / 100));

                                    totalPrimePlusIsc = totalMinusDiscount + ISC;
                                }*/

                                var $elem = $(".totalPrime");

                                if ($elem.length > 1) {
                                    $.each($elem, function (idx, obj) {
                                        var randomid = $(obj).attr("data-vehiclerandomid");
                                        if (randomid == randomID) {
                                            $elem = $(obj);
                                            $elem.html("$" + number_format(total, 2));
                                        }
                                    });
                                } else {
                                    $elem.html("$" + number_format(total, 2));
                                }

                                var $elem = $(".ISC");

                                if ($elem.length > 1) {
                                    $.each($elem, function (idx, obj) {
                                        var randomid = $(obj).attr("data-vehiclerandomid");
                                        if (randomid == randomID) {
                                            $elem = $(obj);
                                            $elem.html("$" + number_format(ISC, 2));
                                        }
                                    });
                                } else {
                                    $elem.html("$" + number_format(ISC, 2));
                                }

                                var $elem = $(".totalToPay");

                                if ($elem.length > 1) {
                                    $.each($elem, function (idx, obj) {
                                        var randomid = $(obj).attr("data-vehiclerandomid");
                                        if (randomid == randomID) {
                                            $elem = $(obj);
                                            $elem.html("$" + number_format(totalPrimePlusIsc, 2));
                                        }
                                    });
                                } else {
                                    $elem.html("$" + number_format(totalPrimePlusIsc, 2));
                                }

                                /*
                                var $elem = $(".totalAllDiscount");

                                if ($elem.length > 1) {
                                    $.each($elem, function (idx, obj) {
                                        var randomid = $(obj).attr("data-vehiclerandomid");
                                        if (randomid == randomID) {
                                            $elem = $(obj);
                                            $elem.html("$" + number_format(totalAllDiscountsByVH, 2));
                                        }
                                    });
                                } else {
                                    $elem.html("$" + number_format(totalAllDiscountsByVH, 2));
                                }
                                

                                $.ajax({
                                    url: '/Home/GetPercentByQtyVehicle',
                                    type: 'POST',
                                    dataType: 'json',
                                    data: { qtyVehicles: realTotalVehiclesForDiscountFlotilla },
                                    async: false,
                                    success: function (data) {
                                        PercentByQtyVehicle = data;

                                        if (data > 0) {
                                            isFlotilla = true;
                                        } else {
                                            isFlotilla = false;
                                        }

                                    }
                                });*/


                                var subRamo = coverageCoreId;
                                //Solos los que no son de Ley
                                if (!_isLawProduct) {

                                    /*Reaseguro*/
                                    $.ajax({
                                        url: '/Home/getMaximoReaseguroSubRamo_New',
                                        dataType: 'json',
                                        data: { SecuenciaVehicleSysflex: SecuenciaVehicleSysflex, quotationCoreNumber: quotationCoreNumber, make: makeName, model: modelName, year: year },
                                        async: false, //LoadVehicle ? false : true,
                                        success: function (data) {

                                            if (data.IsFacultative) {
                                                IsFacultative = data.IsFacultative;
                                                AmountFacultative = data.AmountFacultative;
                                                showWarning([data.message], 'Advertencia Reaseguro');
                                            } else {
                                                IsFacultative = false;
                                                AmountFacultative = 0;
                                            }
                                        }
                                    });
                                }
                            }

                            var totaPrimelbyVH = getTotalPrime(randomID);

                            vehicle.isLawProduct = _isLawProduct;
                            vehicle.GlobalDataProductLimits = vehicle.GlobalDataProductLimits;
                            vehicle.servicescoverages = arrayServiceCoverages;
                            vehicle.limitSelfThirdsDamages = arraySelfAndThirdsDamage;

                            vehicle.VehicleDescription = makeName + " " + modelName;

                            vehicle.InsuredAmount = _insuredAmount;
                            vehicle.PercentageToInsure = GlobalpercentageToInsure;
                            vehicle.TotalPrime = totaPrimelbyVH;
                            vehicle.TotalIsc = (totaPrimelbyVH * (iscPercentage / 100));
                            vehicle.SelectedProductCoreId = SelectedProductCore;
                            vehicle.SelectedProductName = SelectedProductName;
                            vehicle.UsageId = usage;
                            vehicle.UsageName = usageName;
                            vehicle.Quotation_Id = getQuotationNumberForRates;
                            vehicle.SelectedVehicleTypeId = selectedVehicleTypeId;
                            vehicle.SelectedCoverageCoreId = coverageCoreId;
                            vehicle.SelectedCoverageName = SelectedCoverageName;
                            vehicle.SurChargePercentage = selectedSurchargePercent == "" ? 0 : selectedSurchargePercent;
                            vehicle.RateJson = rateJson;
                            vehicle.SecuenciaVehicleSysflex = SecuenciaVehicleSysflex;
                            vehicle.IsFacultative = IsFacultative
                            vehicle.AmountFacultative = AmountFacultative
                            vehicle.VehicleQuantity = qtyVehicles;
                            vehicle.selectedDeductible = selectedDeductible;

                            vehicle.idCapacidad = idCapacidad;
                            vehicle.descCapacidad = descCapacidad;
                            vehicle.isSemifull = isSemifull;
                            vehicle.actualAgentSelected = ActualAgentSelected;
                            vehicle.porcImpuesto = porcImpuesto;
                            vehicle.principalDateOfBirth = principalDateOfBirth;
                            vehicle.principalSex = clientSex;
                            vehicle.principalrforeignlicense = getForeingLicenceDriver;

                            vehicle.startDate = GlobalcurrentStartDateSelected;
                            vehicle.endDate = GlobalcurrentEndDateSelected;

                            vehicle.PercentByQtyVehicle = PercentByQtyVehicle > 0 ? PercentByQtyVehicle : 0;
                            vehicle.TotalByQtyVehicle = TotalByQtyVehicle();

                            vehicle.iscompletedVehicle = true;

                            vehicle.totalAllDiscountsByVH = totalAllDiscountsByVH;

                            var realVehicle = setVehicle(vehicle, randomID);

                            setAllVehicleDataToSave(realVehicle, "UPDATE");

                            allGood = true;

                            if (GlobalSaveVehicleOnly && !isRequoting) {

                                SaveDataVehicleOnlyOne(randomID);

                                refreshDiscountSectionAllVehicle();
                            }

                            var IsDeclarativa = getExistAllDeclarativa();

                            if (!IsDeclarativa) {
                                showWarning(['No puede tener un plan DECLARATIVA con otro tipo de PLAN, favor revisar para poder continuar.']);

                                var vehle = getAllDataVehicleByRandomID(randomID);

                                if (vehle) {
                                    vehle.iscompletedVehicle = false;
                                    var realVehicle = setVehicle(vehle, randomID);
                                    setAllVehicleDataToSave(realVehicle, "UPDATE");
                                }

                                return false;
                            }
                        }
                    }
                });
            }
        }
    }

    return allGood;
}

function getRecargos(isFlotillaMode, randomId) {

    $.ajax({
        url: '/Home/GetSurchargePercentage',
        dataType: 'json',
        async: false,
        success: function (data) {
            //var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlSurchargePercent', randomId) : $("#SurchargePercent");
            var $select_elem = $("#SurchargePercent");
            $select_elem.empty();
            $select_elem.append('<option value="">Seleccione</option>');
            $.each(data, function (idx, obj) {
                $select_elem.append("<option value='" + obj.id + "'>" + obj.name + "</option>");
            });

            if (isFlotillaMode == true) {

                $select_elem = $("#ddlSurchargePercentHidden");
                $select_elem.empty();
                $.each(data, function (idx, obj) {
                    $select_elem.append("<option value='" + obj.id + "'>" + obj.name + "</option>");
                });
            }

            //$select_elem.trigger("change.select2");
        }
    });
}

function getRecargosForNewVehicles(dropid) {

    $.ajax({
        url: '/Home/GetSurchargePercentage',
        dataType: 'json',
        async: false,
        success: function (data) {
            var $select_elem = $("#" + dropid);
            $select_elem.empty();
            $select_elem.append('<option value="">Seleccione</option>');
            $.each(data, function (idx, obj) {
                $select_elem.append("<option value='" + obj.id + "'>" + obj.name + "</option>");
            });
            //$select_elem.trigger("change.select2");

        }
    });
}

function showRechargeAllLawProducts(selectedCoverage, randomID, isFlotillaMode) {

    var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlSurchargePercent', randomID) : $(".SurchargePercent");
    if ($select_elem.length > 1) {
        $.each($select_elem, function (idx, obj) {
            var randomid = $(obj).attr("data-vehiclerandomid");
            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    if (selectedCoverage) {

        var vehicle = getAllDataVehicleByRandomID(randomID);

        var prod = altFind(vehicle.GlobalDataCoverages, function (item) {
            return item.Id == selectedCoverage;
        });

        if (prod) {
            var whatis = prod.IsLaw;

            if (whatis == false) {
                //Chequeao que no sea un semifull
                var isSemifull = prod.Name.toLowerCase().indexOf('semi');
                var issemi = (isSemifull != -1);

                if (issemi) {
                    $select_elem.attr('disabled', 'disabled');
                    $select_elem.val("");
                } else {
                    $select_elem.removeAttr('disabled');
                }
                return issemi;
            }
            $select_elem.attr('disabled', 'disabled');
            $select_elem.val("");
        }
    } else {
        $select_elem.removeAttr('disabled');
    }
}

function SetAdditionals(randomID, isFlotillaMode) {

    var selectedCoverage = isFlotillaMode == true ? getHtmlElementByClass('ddlCoverages', randomID).val() : getOptionSelectedDropWithClass(".Coverages", randomID);

    var vehcicle = getAllDataVehicleByRandomID(randomID);

    if (selectedCoverage > 0) {

        var results = new Array();

        var ServicesCoverages = vehcicle.GlobalDataProductLimits.ServicesCoverages;
        var hasSelectedFromDb = false;

        for (var i = 0; i < ServicesCoverages.length; i++) {

            var sc = ServicesCoverages[i];
            var obj = { id: sc.Name, value: -2 };

            if (GlobalServicesSelected != null/* && GlobalServicesSelected.length > 0*/) {
                //Lo comparo con los servicios que yo tengo como marcados en la db                
                for (var c = 0; c < sc.Coverages.length; c++) {
                    var w = sc.Coverages[c];

                    var dd = altFind(GlobalServicesSelected, function (item) {
                        return (item.CoverageDetailCoreId == w.CoverageDetailCoreId && item.IsSelected);
                    });

                    //var dd = GlobalServicesSelected.find(function (item) {
                    //    return (item.CoverageDetailCoreId == w.CoverageDetailCoreId && item.IsSelected);
                    //});

                    if (dd !== undefined) {
                        obj.value = w.Id;
                    }
                }
                hasSelectedFromDb = true;
            } else {
                var selected = altFind(sc.Coverages, function (cov) { return cov.IsSelected; });

                //var selected = sc.Coverages.find(function (cov) { return cov.IsSelected; });
                if (selected) {
                    obj.value = selected.Id;
                }
            }

            results.push(obj);
        }

        GlobalDataPopupSelectedServices = results;

        vehcicle.GlobalDataPopupSelectedServices = GlobalDataPopupSelectedServices;

        popupCalculateAnnualTotal(vehcicle);

        var realv = setVehicle(vehcicle, randomID);

        setAllVehicleDataToSave(realv, "UPDATE");

        PaintTableServices(vehcicle.GlobalDataPopupSelectedServices, randomID);

        if (hasSelectedFromDb) {
            UpdateAdditionalSelected(randomID);
        }
    }
}

function popupCalculateAnnualTotal(vehcicle) {
    var total = 0;

    $.each(vehcicle.GlobalDataPopupSelectedServices, function (idx, item) {

        var service = altFind(vehcicle.GlobalDataProductLimits.ServicesCoverages, function (sc) { return sc.Name == item.id });

        if (service) {
            var selectedCoverage = altFind(service.Coverages, function (cov) { return cov.Id == item.value; });

            if (selectedCoverage) {
                total += selectedCoverage.Amount;
            }
        }
    });

    GLobalPopupAnnualTotal = total;
    vehcicle.GLobalPopupAnnualTotal = total;

    $("#txtTotalServicesSelected").val("$" + number_format(vehcicle.GLobalPopupAnnualTotal, 2));
}

function PaintTableServices(selectedservices, randomID) {
    var tblBody = $("#servicesBody");
    tblBody.empty();

    var vehcicle = getAllDataVehicleByRandomID(randomID);

    var ServicesCoverages = vehcicle.GlobalDataProductLimits.ServicesCoverages;

    $.each(ServicesCoverages, function (idx, i) {

        //Cabecera
        var cabecera = i.Name;
        var coverages = i.Coverages;

        var rowHeader = '<tr class="table-primary">' +
            '<td align="left"><button class="cleanService" data-sname="{2}">Limpiar</button></td>' +
            '<td align="center" class="font-weight-bold text-uppercase">{1}</td>' +
            '<td align="center"></td>' +
            '</tr>';

        rowHeader = rowHeader.replace('{1}', cabecera).replace('{2}', cabecera);

        tblBody.append(rowHeader);

        //detalle
        $.each(coverages, function (idx, c) {
            var isChecked = "";

            var selServ = selectedservices.filter(function (item) {
                return item.value == c.Id;
            });


            if (selServ.length > 0) {
                isChecked = "checked='checked'";
            }

            var radio = '<label class="mb-1 mdl-radio mdl-js-radio mdl-js-ripple-effect mdl-cell--6-col" for="IdentificationTypeCedPasaport">' +
                '<input type="radio" id="chk_{5}" class="mdl-radio__button servicesChecked" name="{7}" {4} value="{6}" data-sname="{8}" data-vehiclerandomid="{9}">' +
                '<span class="mdl-radio__label"></span>' +
                '</label>';

            var rowDetail = '<tr>' +
                '<td align="left">' +
                '<span class="mdl-list__item-secondary-action float-left mr-3">' +
                radio +
                '</label>' +
                '</span >' +
                '<span class="mdl-list__item-primary-content">{1}</span>' +
                '</td>' +
                '<td align="center">{2}</td>' +
                '<td align="center">{3}</td>' +
                '</tr>';

            rowDetail = rowDetail.replace('{1}', c.Name).replace('{2}', c.Name).replace('{3}', number_format(c.Amount, 2))
                .replace('{4}', isChecked).replace('{5}', c.Name.replace(' ', '')).replace('{6}', c.Id).replace('{7}', cabecera.replace(' ', ''))
                .replace('{8}', cabecera).replace('{9}', randomID);

            tblBody.append(rowDetail);
        });
    });

    $(".servicesChecked").off("change");
    $(".servicesChecked").change(function () {
        var chk = $(this);
        var vehicle = null;

        if (chk.is(":checked")) {
            var v = chk.val();
            var sname = chk.attr('data-sname');
            var randomid = chk.attr("data-vehiclerandomid");
            vehicle = getAllDataVehicleByRandomID(randomid);


            for (var i in vehicle.GlobalDataPopupSelectedServices) {
                if (vehicle.GlobalDataPopupSelectedServices[i].id == sname) {
                    vehicle.GlobalDataPopupSelectedServices[i].value = v;
                    break;
                }
            }
        } else {
            var v = chk.val();
            var sname = chk.attr('data-sname');
            var randomid = chk.attr("data-vehiclerandomid");
            vehicle = getAllDataVehicleByRandomID(randomid);


            for (var i in vehicle.GlobalDataPopupSelectedServices) {
                if (vehicle.GlobalDataPopupSelectedServices[i].id == sname) {
                    vehicle.GlobalDataPopupSelectedServices[i].value = -2;
                    break;
                }
            }
        }
        popupCalculateAnnualTotal(vehicle);

        var realv = setVehicle(vehicle, randomID);

        setAllVehicleDataToSave(realv, "UPDATE");
    });

    $(".cleanService").click(function () {
        var b = $(this);
        var name = b.attr("data-sname");

        if (name != undefined && name != '') {
            $.each($(".servicesChecked"), function (el) {
                var radio = $(this);
                var radioName = radio.attr("data-sname");

                if (name == radioName) {
                    radio.prop('checked', false);
                    radio.trigger('change');
                }
            });
        }
    });


    $(".saveServices").off("click");
    $(".saveServices").click(function () {
        var $this = $(this);

        var randomID = $this.attr("data-vehiclerandomid");
        UpdateAdditionalSelected(randomID);

        GLobalPopupAnnualTotal = 0;

        $("#addServicios").modal('hide');

        var tblBody = $("#servicesBody");
        tblBody.empty();

        GlobalDataPopupSelectedServices = [];

        var isFlotillaMode = $this.data("isflotillamode");
        if (isFlotillaMode == "S") {
            getRatesFlotMode(randomID);
        } else {
            getRates(randomID);
        }
    });
}

function getOldAgentSelected() {
    if ($("#oldAgentSelected").val() !== '') {
        var obj = JSON.parse($("#oldAgentSelected").val());
        return obj;
    }
    return null;
}

function getNewAgentSelected() {

    if ($("#AgentList").length > 0) {

        if ($("#AgentList").val() !== "") {
            var actualAgent = JSON.parse($("#AgentList").val());
            var oldAgent = getOldAgentSelected();
            if (oldAgent != null && oldAgent.AgentCode != actualAgent.AgentCode) {
                return actualAgent;
            } else {
                return actualAgent;
            }
        }
    }
}

function getCurrentDateTimeMomentFormat() {
    return "DD-MMM-YYYY hh:mm:ss a";
}

function getCurrentDateMomentFormat() {
    return "DD-MMM-YYYY";
}

function insuredAmount(randomID) {
    var vehicle = getAllDataVehicleByRandomID(randomID);

    var vehiclePrice = vehicle.VehiclePrice;
    if (vehiclePrice && GlobalpercentageToInsure)
        return vehiclePrice * (GlobalpercentageToInsure / 100);
    else
        return 0;
}

function isLawProduct(randomID, isRequoting, coverageCoreId, isFlotillaMode) {

    if (randomID == undefined || randomID == null) {
        return true;
    }

    var selectedCoverage = (isRequoting == false || isRequoting == undefined) ? getOptionSelectedDropWithClass(".Coverages", randomID) : coverageCoreId;


    if (isFlotillaMode == true) {
        selectedCoverage = (isRequoting == false || isRequoting == undefined) ? getHtmlElementByClass("ddlCoverages", randomID).val() : coverageCoreId;
    }


    var vehicle = getAllDataVehicleByRandomID(randomID);

    if (selectedCoverage) {

        var prod = altFind(vehicle.GlobalDataCoverages, function (item) {
            return item.Id == selectedCoverage;
        });

        if (prod) {
            return prod.IsLaw;
        }
    } else {
        return true;
    }
}

function getIdCapacidad(randomID, isRequoting, SelectedProductCore, isFlotillaMode) {

    var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlPlan', randomID) : $(".ddlAllProducts");
    var selectedProduct = "";
    var vehicle = null;

    if ((isRequoting == false || isRequoting == undefined)) {

        if ($select_elem.length > 1) {
            $.each($select_elem, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == randomID) {
                    $select_elem = $(obj);
                    selectedProduct = $select_elem;
                    vehicle = getAllDataVehicleByRandomID(randomid);
                }
            });
        }
        else {
            selectedProduct = $select_elem;
            var randomid = selectedProduct.attr("data-vehiclerandomid");
            vehicle = getAllDataVehicleByRandomID(randomid);
        }

        if (vehicle.GlobalAllProducts && selectedProduct.val()) {

            var pr = altFind(vehicle.GlobalAllProducts, function (p) {
                return p.Id == selectedProduct.val();
            });

            if (pr != null) {
                return pr.IdCapacidad;
            } else {
                return 0;
            }
        }
        else {
            return 0;
        }
    } else {

        vehicle = getAllDataVehicleByRandomID(randomID);
        selectedProduct = SelectedProductCore;

        if (vehicle.GlobalAllProducts && selectedProduct) {

            var pr = altFind(vehicle.GlobalAllProducts, function (p) {
                return p.Id == selectedProduct;
            });

            if (pr != null) {
                return pr.IdCapacidad;
            } else {
                return 0;
            }
        }
        else {
            return 0;
        }
    }
}

function getDescCapacidad(randomID, isRequoting, SelectedProductCore, isFlotillaMode) {

    var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlPlan', randomID) : $(".ddlAllProducts");
    var selectedProduct = "";
    var vehicle = null;


    if ((isRequoting == false || isRequoting == undefined)) {

        if ($select_elem.length > 1) {
            $.each($select_elem, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == randomID) {
                    $select_elem = $(obj);
                    selectedProduct = $select_elem;
                    vehicle = getAllDataVehicleByRandomID(randomid);
                }
            });
        }
        else {
            selectedProduct = $select_elem;
            var randomid = selectedProduct.attr("data-vehiclerandomid");
            vehicle = getAllDataVehicleByRandomID(randomid);
        }

        if (vehicle.GlobalAllProducts && selectedProduct.val()) {

            var pr = altFind(vehicle.GlobalAllProducts, function (p) {
                return p.Id == selectedProduct.val()
            });

            if (pr != null) {
                return pr.DescCapacidad;
            } else {
                return "";
            }
        }
        else {
            return "";
        }
    } else {
        vehicle = getAllDataVehicleByRandomID(randomID);
        selectedProduct = SelectedProductCore;

        if (vehicle.GlobalAllProducts && selectedProduct) {

            var pr = altFind(vehicle.GlobalAllProducts, function (p) {
                return p.Id == selectedProduct;
            });

            if (pr != null) {
                return pr.DescCapacidad;
            } else {
                return "";
            }
        }
        else {
            return "";
        }
    }
}

function validateGetRates(randomID, isFlotillaMode) {
    var msgs = [];

    var vehicle = getAllDataVehicleByRandomID(randomID);

    var vehiclePrice = vehicle.VehiclePrice;
    var driver = vehicle.Driver_Id;

    var selectedCoverage = isFlotillaMode == true ? getHtmlElementByClass("ddlCoverages", randomID).val() : getOptionSelectedDropWithClass(".Coverages", randomID);

    var selectedDeductible = isFlotillaMode == true ? getHtmlElementByClass("ddlDeducible", randomID).val() : getOptionSelectedDropWithClass(".Deducible", randomID);

    if (vehiclePrice == 0)
        msgs.push('Debe ingresar el Precio del Vehículo para poder obtener su cotización.');

    if (!vehicle.GlobalDataProductLimits)
        msgs.push('Debe seleccionar un Tipo de Producto para poder obtener su cotización.');


    if (driver == 0 && GlobalAppMode != "LEYMODE" && GlobalAppMode != "FULLMODE")
        msgs.push('Debe seleccionar un Conductor para poder obtener su cotización.');

    if (selectedCoverage == 0)
        msgs.push('Debe seleccionar una Cobertura para poder obtener su cotización.');


    if (!vehicle.GlobalDataDeductibleList || (vehicle.GlobalDataDeductibleList.length > 0 && selectedDeductible == 0))
        msgs.push('Debe seleccionar un Deducible para poder obtener su cotización.');


    if (msgs.length > 0) {
        //showError(msgs, "Obtener Cotización");
        return false;
    }
    else
        return true;
}

function GetCurrentIsc() {

    $.ajax({
        url: "/Home/GetCurrentIsc",
        dataType: 'json',
        async: false,
        cache: false,
        data: {},
        success: function (data) {
            GlobalCurrentIsc = data.isc;
        }
    });
}

function getTotalPrime(randomID) {
    var total = 0;
    var vehicle = getAllDataVehicleByRandomID(randomID);

    if (vehicle.GlobalDataProductLimits) {
        total = vehicle.GlobalDataProductLimits.SdPrime + vehicle.GlobalDataProductLimits.TpPrime + vehicle.GlobalDataProductLimits.ServicesPrime;
    }
    return total;
}

function getTotalPrimeOfAllVehicles() {
    var total = 0;

    $.each(AllVehicleDataToSave, function (idx, vehicle) {

        var actualVehicleQty = vehicle.VehicleQuantity;
        var actualVehiculePrime = (vehicle.TotalPrime == undefined || vehicle.TotalPrime == null || vehicle.TotalPrime == "") ? 0 : vehicle.TotalPrime;

        var ToPrimeAddQtyVehi = (actualVehiculePrime * actualVehicleQty);

        total += ToPrimeAddQtyVehi;
    });

    return total;
}

function setAllVehicleDataToSave(objVehicle, action) {

    if (AllVehicleDataToSave != null) {

        if (action == "UPDATE") {

            var current = altFind(AllVehicleDataToSave, function (item) {
                return item.randomId == objVehicle.randomId
            });

            if (current != undefined) {

                AllVehicleDataToSave = AllVehicleDataToSave.filter(function (item) {
                    return item.randomId != objVehicle.randomId
                });

                AllVehicleDataToSave.push(current);
            }
        } else {
            AllVehicleDataToSave.push(objVehicle);
        }

    } else {
        AllVehicleDataToSave = new Array();
        AllVehicleDataToSave.push(objVehicle);
    }
}

function resetVehicleInfo() {

    $("#VehicleDriver_Id").val("");
    //$("#VehicleDriver_Id").trigger("chosen:updated");
    $("#VehicleDriver_Id").trigger("change.select2");

    $("#VehicleModel_Make_Id").val("");
    //$("#VehicleModel_Make_Id").trigger("chosen:updated");
    $("#VehicleModel_Make_Id").trigger("change.select2");

    $("#VehicleModel_Model_Id").val("");
    $("#VehicleModel_Model_Id").empty();
    //$("#VehicleModel_Model_Id").trigger("chosen:updated");
    $("#VehicleModel_Model_Id").trigger("change.select2");

    $("#yearsAvaibles").val("");
    //$("#yearsAvaibles").trigger("chosen:updated");
    $("#yearsAvaibles").trigger("change.select2");

    $("#VehicleType").val("");
    $("#VehicleType").empty();
    //$("#VehicleType").trigger("chosen:updated");
    $("#VehicleType").trigger("change.select2");

    $("#Usages").val("");
    $("#Usages").empty();
    //$("#Usages").trigger("chosen:updated");
    $("#Usages").trigger("change.select2");

    $("#StoreCar").val("");
    //$("#StoreCar").trigger("chosen:updated");
    $("#StoreCar").trigger("change.select2");

    $("#vehiclePrice").val("0");
    //$("#vehiclePrice").parent().addClass('is-dirty');

    $("#VehicleYearsOld").val("");
    $("#VehicleYearsOld").parent().removeClass('is-dirty');

    $("#fuelType").val("");
    $("#fuelType").empty();
    $("#fuelType").trigger("change.select2");
}

function setVehicle(obj, randomID) {

    var newVehicle = {};

    var v = getAllDataVehicleByRandomID(randomID);

    if (v != undefined) {
        newVehicle = v;
    }

    newVehicle.randomId = obj.randomId != undefined ? obj.randomId : newVehicle.randomId;
    newVehicle.Id = obj.Id != undefined ? obj.Id : newVehicle.Id;
    newVehicle.isLawProduct = obj.isLawProduct != undefined ? obj.isLawProduct : newVehicle.isLawProduct;

    newVehicle.GlobalDataProductLimits = obj.GlobalDataProductLimits != undefined ? obj.GlobalDataProductLimits : newVehicle.GlobalDataProductLimits;
    newVehicle.GlobalDataUsages = obj.GlobalDataUsages != undefined ? obj.GlobalDataUsages : newVehicle.GlobalDataUsages;
    newVehicle.GlobalData = obj.GlobalData != undefined ? obj.GlobalData : newVehicle.GlobalData;
    newVehicle.GlobalDataDeductibleList = obj.GlobalDataDeductibleList != undefined ? obj.GlobalDataDeductibleList : newVehicle.GlobalDataDeductibleList;
    newVehicle.GlobalDataCoverages = obj.GlobalDataCoverages != undefined ? obj.GlobalDataCoverages : newVehicle.GlobalDataCoverages;
    newVehicle.GlobalDataProductsByUsage = obj.GlobalDataProductsByUsage != undefined ? obj.GlobalDataProductsByUsage : newVehicle.GlobalDataProductsByUsage;
    newVehicle.GlobalAllProducts = obj.GlobalAllProducts != undefined ? obj.GlobalAllProducts : newVehicle.GlobalAllProducts;
    newVehicle.GlobalDataPopupSelectedServices = obj.GlobalDataPopupSelectedServices != undefined ? obj.GlobalDataPopupSelectedServices : newVehicle.GlobalDataPopupSelectedServices;
    newVehicle.GLobalPopupAnnualTotal = obj.GLobalPopupAnnualTotal != undefined ? obj.GLobalPopupAnnualTotal : newVehicle.GLobalPopupAnnualTotal;;

    newVehicle.servicescoverages = obj.servicescoverages != undefined ? obj.servicescoverages : newVehicle.servicescoverages;
    newVehicle.limitSelfThirdsDamages = obj.limitSelfThirdsDamages != undefined ? obj.limitSelfThirdsDamages : newVehicle.limitSelfThirdsDamages;

    newVehicle.VehicleDescription = obj.VehicleDescription != undefined ? obj.VehicleDescription : newVehicle.VehicleDescription;
    newVehicle.Year = obj.Year != undefined ? obj.Year : newVehicle.Year;
    newVehicle.VehiclePrice = obj.VehiclePrice != undefined ? (obj.VehiclePrice == 0 ? 1 : obj.VehiclePrice) : (newVehicle.VehiclePrice == 0 ? 1 : newVehicle.VehiclePrice);
    newVehicle.InsuredAmount = obj.InsuredAmount != undefined ? obj.InsuredAmount : newVehicle.InsuredAmount;
    newVehicle.PercentageToInsure = obj.PercentageToInsure != undefined ? obj.PercentageToInsure : newVehicle.PercentageToInsure;
    newVehicle.TotalPrime = obj.TotalPrime != undefined ? obj.TotalPrime : newVehicle.TotalPrime;
    newVehicle.TotalIsc = obj.TotalIsc != undefined ? obj.TotalIsc : newVehicle.TotalIsc;
    newVehicle.SelectedProductCoreId = obj.SelectedProductCoreId != undefined ? obj.SelectedProductCoreId : newVehicle.SelectedProductCoreId;
    newVehicle.SelectedProductName = obj.SelectedProductName != undefined ? obj.SelectedProductName : newVehicle.SelectedProductName;
    newVehicle.VehicleMakeName = obj.VehicleMakeName != undefined ? obj.VehicleMakeName : newVehicle.VehicleMakeName;
    newVehicle.UsageId = obj.UsageId != undefined ? obj.UsageId : newVehicle.UsageId;
    newVehicle.UsageName = obj.UsageName != undefined ? obj.UsageName : newVehicle.UsageName;

    if (GlobalAppMode == "LEYMODE") {

        newVehicle.StoreId = 6;
        newVehicle.StoreName = "Casa";
        newVehicle.Driver_Id = $("#hdnDefaultClientId").val();

        newVehicle.VehicleYearOld = "No";

        newVehicle.SelectedVehicleFuelTypeId = 1747;
        newVehicle.SelectedVehicleFuelTypeDesc = "Gasolina";

        newVehicle.contactFormId = $("#contactForm").val();

        newVehicle.flotillaMode = false;
    }
    else {
        newVehicle.StoreId = obj.StoreId != undefined ? obj.StoreId : newVehicle.StoreId;
        newVehicle.StoreName = obj.StoreName != undefined ? obj.StoreName : newVehicle.StoreName;

        if (GlobalAppMode == "FULLMODE") {
            newVehicle.Driver_Id = $("#driver").val();
            newVehicle.flotillaMode = false;

        } else {
            newVehicle.Driver_Id = obj.Driver_Id != undefined ? obj.Driver_Id : newVehicle.Driver_Id;
        }

        newVehicle.SelectedVehicleFuelTypeId = obj.SelectedVehicleFuelTypeId != undefined ? obj.SelectedVehicleFuelTypeId : newVehicle.SelectedVehicleFuelTypeId;
        newVehicle.SelectedVehicleFuelTypeDesc = obj.SelectedVehicleFuelTypeDesc != undefined ? obj.SelectedVehicleFuelTypeDesc : newVehicle.SelectedVehicleFuelTypeDesc;
        newVehicle.contactFormId = null;


        newVehicle.flotillaMode = $("#QuotTypeFlotilla").is(':checked');
    }

    newVehicle.VehicleModel_Make_Id = obj.VehicleModel_Make_Id != undefined ? obj.VehicleModel_Make_Id : newVehicle.VehicleModel_Make_Id;
    newVehicle.VehicleModel_Model_Id = obj.VehicleModel_Model_Id != undefined ? obj.VehicleModel_Model_Id : newVehicle.VehicleModel_Model_Id;
    newVehicle.Quotation_Id = obj.Quotation_Id != undefined ? obj.Quotation_Id : newVehicle.Quotation_Id;
    newVehicle.SelectedVehicleTypeId = obj.SelectedVehicleTypeId != undefined ? obj.SelectedVehicleTypeId : newVehicle.SelectedVehicleTypeId;
    newVehicle.SelectedVehicleTypeName = obj.SelectedVehicleTypeName != undefined ? obj.SelectedVehicleTypeName : newVehicle.SelectedVehicleTypeName;
    newVehicle.SelectedCoverageCoreId = obj.SelectedCoverageCoreId != undefined ? obj.SelectedCoverageCoreId : newVehicle.SelectedCoverageCoreId;
    newVehicle.SelectedCoverageName = obj.SelectedCoverageName != undefined ? obj.SelectedCoverageName : newVehicle.SelectedCoverageName;
    newVehicle.VehicleYearOld = obj.VehicleYearOld != undefined ? obj.VehicleYearOld : newVehicle.VehicleYearOld;
    newVehicle.SurChargePercentage = obj.SurChargePercentage != undefined ? obj.SurChargePercentage : newVehicle.SurChargePercentage;
    newVehicle.RateJson = obj.RateJson != undefined ? obj.RateJson : newVehicle.RateJson;
    newVehicle.SecuenciaVehicleSysflex = obj.SecuenciaVehicleSysflex != undefined ? obj.SecuenciaVehicleSysflex : newVehicle.SecuenciaVehicleSysflex;
    newVehicle.IsFacultative = obj.IsFacultative != undefined ? obj.IsFacultative : newVehicle.IsFacultative;
    newVehicle.AmountFacultative = obj.AmountFacultative != undefined ? obj.AmountFacultative : newVehicle.AmountFacultative;
    newVehicle.VehicleQuantity = obj.VehicleQuantity != undefined ? obj.VehicleQuantity : newVehicle.VehicleQuantity;
    newVehicle.selectedDeductible = obj.selectedDeductible != undefined ? obj.selectedDeductible : newVehicle.selectedDeductible;
    newVehicle.idCapacidad = obj.idCapacidad != undefined ? obj.idCapacidad : newVehicle.idCapacidad;
    newVehicle.descCapacidad = obj.descCapacidad != undefined ? obj.descCapacidad : newVehicle.descCapacidad;
    newVehicle.isSemifull = obj.isSemifull != undefined ? obj.isSemifull : newVehicle.isSemifull;
    newVehicle.actualAgentSelected = obj.actualAgentSelected != undefined ? obj.actualAgentSelected : newVehicle.actualAgentSelected;
    newVehicle.principalDateOfBirth = obj.principalDateOfBirth != undefined ? obj.principalDateOfBirth : newVehicle.principalDateOfBirth;
    newVehicle.principalSex = obj.principalSex != undefined ? obj.principalSex : newVehicle.principalSex;
    newVehicle.principalrforeignlicense = obj.principalrforeignlicense != undefined ? obj.principalrforeignlicense : newVehicle.principalrforeignlicense;

    newVehicle.VehicleModelName = obj.VehicleModelName != undefined ? obj.VehicleModelName : newVehicle.VehicleModelName;

    newVehicle.startDate = obj.startDate != undefined ? obj.startDate : newVehicle.startDate;
    newVehicle.endDate = obj.endDate != undefined ? obj.endDate : newVehicle.endDate;

    newVehicle.PercentByQtyVehicle = obj.PercentByQtyVehicle != undefined ? obj.PercentByQtyVehicle : newVehicle.PercentByQtyVehicle;
    newVehicle.TotalByQtyVehicle = obj.TotalByQtyVehicle != undefined ? obj.TotalByQtyVehicle : newVehicle.TotalByQtyVehicle;

    newVehicle.iscompletedVehicle = obj.iscompletedVehicle != undefined ? obj.iscompletedVehicle : false;

    newVehicle.totalAllDiscountsByVH = obj.totalAllDiscountsByVH != undefined ? obj.totalAllDiscountsByVH : 0;


    //Datos del driver
    newVehicle.drivername = $("#FirstName").val();
    newVehicle.driversurname = $("#FirstSurname").val();
    newVehicle.driverdob = $("#DateOfBirth").val();
    newVehicle.driversex = $("#Sex").val();
    newVehicle.driverforeignlicense = $("#ForeignLicense").val();

    var check = $("input[name='IdentificationType']");

    $.each(check, function (i, iden) {
        var objCheck = $(iden);
        if (objCheck.is(":checked")) {
            newVehicle.driveridentificationtype = objCheck.val();
        }
    });

    newVehicle.driveridentificationNumber = $("#IdentificationNumber").val();
    newVehicle.driverphonenumber = $("#PhoneNumber").val();
    newVehicle.driveremail = $("#Email").val();
    //

    newVehicle.agentSelected = $("#AgentList").val();
    newVehicle.quotationCoreNumber = $("#QuotationCoreNumber").val();


    newVehicle.couponCode = $('#hdnCouponCode').val();
    newVehicle.couponPercentageDiscount = $('#hdnCuponDiscount').val();
    newVehicle.couponProspectId = $('#hdnProspectoID').val();

    newVehicle.appMode = GlobalAppMode;

    return newVehicle;
}

function paintVehicles(AllVehicleDataToSave) {

    var divVehicles = $("#sectVehicles");
    var dv = $(".parentSectionVehicle");
    var generateNewSection = dv.length == 0;

    if (AllVehicleDataToSave.length > 1 || generateNewSection) {

        var toignoreFirstVehicle = AllVehicleDataToSave[0];//ignoro el primer registro porque es el que viene por default

        var vehicles = AllVehicleDataToSave;

        if (generateNewSection == false) {

            vehicles = AllVehicleDataToSave.filter(function (item) {
                return item.randomId != toignoreFirstVehicle.randomId;
            });
        }

        $.each(vehicles, function (idx, v) {

            if (vehicleRandomID == v.randomId) {

                var vehicledesc = (v.VehicleDescription + " " + v.Year);
                var vRandomID = v.randomId;
                var usageSelected = v.UsageId;
                var selectedVehicleType = v.SelectedVehicleTypeId;
                var vehiclePrice = v.VehiclePrice;
                var qtyVehicle = v.VehicleQuantity;

                //Si el drop original existe entonces el usuario tiene permisos para ver dicho drop
                var surchargeDrop = "";
                var SurchargeId = 'SurchargePercent_' + vRandomID;
                if ($("#SurchargePercent").length > 0) {

                    surchargeDrop = '<li class="mdl-list__item mdl-card__actions mdl-card--border HideOnLeyMode HideOnFullMode">' +
                        '<strong class="mdl-list__item-primary-content mr-5">' +
                        'Recargo:' +
                        '</strong>' +
                        '<select id="{1}" class="form-control SurchargePercent" data-vehiclerandomid="{2}"></select>' +
                        '</li>';

                    surchargeDrop = surchargeDrop.replace('{1}', SurchargeId).replace('{2}', vRandomID);
                }
                //

                //Cabecera
                var header =
                    '<div class="mdl-card__media">' +
                    '<div class="mdl-card__title">' +
                    '<h2 data-vehiclerandomid="{4}" class="mdl-card__title-text m-auto mdl-color-text--white vehicleTitle">{1}</h2>' +
                    '<button id="btnEditVehicle_{2}" data-vehiclerandomid="{4}" type="button" class="btn float-right ec_btn btn-success btn-sm mr-2 editVehicle"><i class="material-icons">&#xE254;</i></button>' +
                    '<button id="btnDeleteVehicle_{3}" data-vehiclerandomid="{5}" type="button" class="btn float-right ec_btn btn-danger btn-sm deleteVehicle"><i class="material-icons">&#xE5CD;</i></button>' +
                    '</div>' +
                    '</div>';
                header = header.replace("{1}", vehicledesc).replace("{2}", vRandomID).replace("{3}", vRandomID)
                    .replace("{4}", vRandomID).replace("{4}", vRandomID).replace("{5}", vRandomID);



                //detalle
                var detail = '<div class="mdl-card__supporting-text w-100">' +
                    '<!--Icon List -->' +
                    '<ul class="demo-list-icon mdl-list">' +
                    /*'<li class="mdl-list__item text-center"><strong id="vehicleDescription_{2}" data-vehiclerandomid="{11}" class="mdl-list__item-primary-content m-auto text-success vehicleDescription">' +
                    '{1}</strong></li>' +*/
                    '<li class="mdl-list__item mdl-card__actions mdl-card--border HideOnLeyMode">' +
                    '<strong class="mdl-list__item-primary-content">Valor Asegurado:</strong>' +
                    '<span id="VehicleValue_{3}" data-vehiclerandomid="{12}" class="VehicleValue">{17}</span></li>' +
                    '<li class="mdl-list__item mdl-card__actions mdl-card--border putErrorBorder requerido HideOnLeyMode">' +
                    '<strong class="mdl-list__item-primary-content mr-5">Plan:</strong>' +
                    '<select id="ddlAllProducts_{4}" data-vehiclerandomid="{10}" class="form-control ddlAllProducts"><option value="">Seleccione</option></select>' +
                    '</li>' +
                    '<li class="mdl-list__item mdl-card__actions mdl-card--border putErrorBorder requerido">' +
                    '<strong class="mdl-list__item-primary-content mr-5">Cobertura:</strong>' +
                    '<select id="Coverages_{5}" data-vehiclerandomid="{13}" class="form-control Coverages"><option value="">Seleccione</option></select>' +
                    //'<button id="infoCB" data-toggle="modal" data-target="#ppCobertura" class="mdl-button mdl-js-button mdl-button--icon mdl-button--colored float-right"><i class="material-icons">&#xE88E;</i></button>' +
                    '</li>' +
                    '<li class="mdl-list__item mdl-card__actions mdl-card--border putErrorBorder requerido HideOnLeyMode">' +
                    '<strong class="mdl-list__item-primary-content mr-5">Deducible:</strong><select id="Deducible_{6}" data-vehiclerandomid="{14}" class="form-control Deducible"></select>' +
                    '</li>' +
                    '<li class="mdl-list__item mdl-card__actions mdl-card--border">' +
                    '<strong class="mdl-list__item-primary-content">Servicios:</strong>' +
                    '<span>' +
                    '<button id="servicesPopUp_{20}" disabled="disabled" data-vehiclerandomid="{21}" data-toggle="modal" data-target="#addServicios" class="mdl-button mdl-js-button mdl-button--colored bg-success mdl-color-text--white servicesPopUp" ><i class="material-icons">add</i></button>' +
                    '</span>' +
                    '</li>' +
                    surchargeDrop +
                    '<li class="mdl-list__item mdl-card__actions mdl-card--border putErrorBorder">' +
                    '<div class="mdl-textfield mdl-js-textfield mdl-textfield--floating-label is-upgraded is-dirty" data-upgraded=",MaterialTextfield">' +
                    '<input class="mdl-textfield__input qtyVehicles" type="text" id="qtyVehicles_{9}" data-vehiclerandomid="{16}" value="{19}">' +
                    '<label class="mdl-textfield__label" for="qtyVehicles_{18}">Cantidad de Vehículos</label>' +
                    '</div>' +
                    '</li>' +

                    '<li class="mdl-list__item mdl-card__actions mdl-card--border">' +
                    '<strong class="mdl-list__item-primary-content">' +
                    'Prima Neta:' +
                    '</strong>' +
                    '<span id="totalPrime_{7}" data-vehiclerandomid="{15}" class="totalPrime">$0</span>' +
                    '</li>' +

                    '<li id="liTotalAllDiscount" class="mdl-list__item mdl-card__actions mdl-card--border">' +
                    '<strong class="mdl-list__item-primary-content">' +
                    'Descuentos:' +
                    '</strong>' +
                    '<span id="totalAllDiscount" data-vehiclerandomid="{15}" class="totalAllDiscount">$0</span>' +
                    '</li>' +

                    '<li class="mdl-list__item mdl-card__actions mdl-card--border liflotillaDiscount" data-vehiclerandomid="{15}" style="display:none">' +
                    '<strong class="mdl-list__item-primary-content">' +
                    '   Descuento Flotilla:' +
                    '</strong>' +
                    '<span data-vehiclerandomid="{15}" class="flotillaDiscountAmount">$0</span>' +
                    '</li>' +

                    '<li class="mdl-list__item mdl-card__actions mdl-card--border liPrimeFlotillaDiscount" data-vehiclerandomid="{15}" style="display:none">' +
                    '<strong class="mdl-list__item-primary-content">' +
                    'Prima Anual con Descuento:' +
                    '</strong>' +
                    '<span data-vehiclerandomid="{15}" class="PrimeFlotillaDiscountAmount">$0</span>' +
                    '</li>' +

                    '<li class="mdl-list__item mdl-card__actions mdl-card--border">' +
                    '<strong class="mdl-list__item-primary-content">' +
                    'Impuesto:' +
                    '</strong>' +
                    '<span data-vehiclerandomid="{15}" class="ISC">$0</span>' +
                    '</li>' +

                    '<li class="mdl-list__item mdl-card__actions mdl-card--border">' +
                    '<strong class="mdl-list__item-primary-content">' +
                    'Prima Total Anual:' +
                    '</strong>' +
                    '<span data-vehiclerandomid="{15}" class="totalToPay">$0</span>' +
                    '</li>' +

                    '<li id="liCoverageExplication_{15}" class="mdl-list__item mdl-card__actions mdl-card--border liCoverageExplication" data-vehiclerandomid="{15}" style="display:none;">' +
                    '<div class="mdl-textfield">' +
                    '<strong class="mdl-list__item-primary-content">Descripción Cobertura:</strong>' +
                    '<button type="button" class="mdl-button mdl-js-button mdl-button--fab mdl-color--blue ShowOrNotExplication"><i class="large material-icons mdl-color-text--white">visibility_off</i ></button >' +
                    '<div class="notice info alert alert-primary float-left mt-1 w-100">' +
                    '<p class="coveragetexthere"></p>' +
                    '</div>' +
                    '</div>' +
                    '<a id="coverageExplication" href="javascript:void(0)" role="button" class="btn btn-info popover-test text-white rounded-circle font-weight-bold float-left ml-1 coverageExplicationBig" title="Qué es esto?">?</a>' +
                    '</li>' +

                    '</ul>' +
                    '</div>';


                //Si aplica a 0 como valor, entonces mostrar 0 en el campo VehicleValue
                var apply = ApplyToZero(vRandomID);
                if (apply && vehiclePrice <= 1) {
                    vehiclePrice = "0";
                } else if (!apply && vehiclePrice <= 1) {
                    vehiclePrice = "0";
                }

                detail = detail.replace("{1}", vehicledesc).replace("{2}", vRandomID)
                    .replace("{3}", vRandomID).replace("{4}", vRandomID)
                    .replace("{5}", vRandomID).replace("{6}", vRandomID)
                    .replace("{7}", vRandomID).replace("{8}", vRandomID)
                    .replace("{9}", vRandomID).replace("{10}", vRandomID)
                    .replace("{11}", vRandomID).replace("{12}", vRandomID)
                    .replace("{13}", vRandomID).replace("{14}", vRandomID)
                    .replace("{15}", vRandomID).replace("{16}", vRandomID)
                    .replace("{17}", "$" + number_format(vehiclePrice, 2)).replace("{18}", vRandomID)
                    .replace("{19}", LoadVehicleQuantity).replace("{20}", vRandomID).replace("{21}", vRandomID)
                    .replace("{15}", vRandomID).replace("{15}", vRandomID).replace("{15}", vRandomID)
                    .replace("{15}", vRandomID).replace("{15}", vRandomID).replace("{15}", vRandomID).replace("{15}", vRandomID).replace("{15}", vRandomID).replace("{15}", vRandomID)
                    ;

                var divFather =
                    '<div class="mdl-cell mdl-card mdl-shadow--4dp portfolio-card mdl-cell--6-col-tablet mdl-cell--12-col-phone parentSectionVehicle" data-vehiclerandomid="{1}">' +
                    header +
                    detail +
                    '</div>';
                divVehicles.append(divFather.replace("{1}", vRandomID));

                getRecargosForNewVehicles(SurchargeId);

                vehicleRandomID = v.randomId;

                ProductListByUsagesForNewVehicles(usageSelected, selectedVehicleType, v.randomId);

                //Asignandole Un change a todos los drops de plan dinamicos
                $(".ddlAllProducts").off("change");
                $(".ddlAllProducts").change(function () {
                    var $this = $(this);
                    var i = $this.attr('id');
                    var selectedProductName = $("#" + i + " option:selected").text();
                    var randomID = $this.attr("data-vehiclerandomid");

                    if (ApplyToZero(randomID) == false && ($this.val() !== "")) {
                        SetFieldsApplyToZero(false, randomID);
                        return false;
                    } else {
                        SetFieldsApplyToZero(true, randomID);
                    }

                    if (GlobalChangeForModeLey == false) {
                        currentCoveragesByUsage(selectedProductName, randomID);
                    }

                    var parentDiv = $this.parent();
                    if ($this.val() == "") {
                        if (parentDiv.hasClass('erarequerido')) {
                            parentDiv.addClass('requerido');
                            parentDiv.removeClass('erarequerido');
                        }
                    } else {
                        parentDiv.removeClass('requerido');
                        parentDiv.addClass('erarequerido');
                    }
                });

                $(".qtyVehicles").off("focusout");
                $(".qtyVehicles").focusout(function () {
                    var v = $(this);
                    var randomID = v.attr("data-vehiclerandomid");

                    getRates(randomID);

                    var parentDiv = v.parent().parent();
                    if (v.val() == "") {
                        if (parentDiv.hasClass('erarequerido')) {
                            parentDiv.addClass('requerido');
                            parentDiv.removeClass('erarequerido');
                        } else {
                            parentDiv.addClass('requerido');
                            parentDiv.removeClass('erarequerido');
                        }
                        v.parent().removeClass('is-dirty');

                    } else {
                        parentDiv.removeClass('requerido');
                        parentDiv.addClass('erarequerido');
                        v.parent().addClass('is-dirty');
                    }
                });

                $(".deleteVehicle").off("click");
                $(".deleteVehicle").click(function () {
                    var randomid = $(this).attr("data-vehiclerandomid");
                    removeVehicle(randomid);
                });

                $(".SurchargePercent").off("change");
                $(".SurchargePercent").change(function () {
                    var $this = $(this);
                    var randomID = $this.attr("data-vehiclerandomid");

                    getRates(randomID);

                });

                $(".servicesPopUp").off("click");
                $(".servicesPopUp").click(function () {
                    var $this = $(this);
                    var randomID = $this.attr("data-vehiclerandomid");

                    SetAdditionals(randomID);
                    //$("#addServicios").modal('show');
                    $("#addServicios").modal({ backdrop: 'static', keyboard: false, show: true });
                    $(".saveServices").attr("data-vehiclerandomid", randomID);

                    return false;
                });

                $(".editVehicle").off("click");
                $(".editVehicle").click(function () {
                    var randomid = $(this).attr("data-vehiclerandomid");
                    SetEditVehicle(randomid);
                });
            }
        });

        if (GlobalAppMode == "LEYMODE") {
            configAppMode();
        }
    }
}

function ProductListByUsagesForNewVehicles(usageSelected, selectedVehicleType, randomID) {

    if (usageSelected) {

        var vehicle = getAllDataVehicleByRandomID(randomID);

        var UsageFound = vehicle.GlobalDataUsages.filter(function (item) {
            return item.idUso == usageSelected;
        });

        if (UsageFound.length) {
            var currentUsageFound = UsageFound[0];

            var t = vehicle.GlobalData.filter(function (item) {
                return item.Name == selectedVehicleType;
            });

            var AllProductListByUsages;

            if (t.length > 0) {
                AllProductListByUsages = t[0].ProductByUsages;
                GlobalAllProducts = t[0].Products;
                var arrProds = listProductsNotShow(GlobalAppMode);

                if (GlobalAppMode == "LEYMODE") {

                    //GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                    //    return item.Name.indexOf("DE LEY") != -1 || item.Name.indexOf("ULTRA") != -1;
                    //});

                    //AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                    //    return item.ProductoDescripcion.indexOf("DE LEY") != -1 || item.ProductoDescripcion.indexOf("ULTRA") != -1;
                    //});

                    GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                        return arrProds.indexOf(item.Name) != -1;
                    });

                    AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                        return arrProds.indexOf(item.ProductoDescripcion) != -1;
                    });
                }

                if (GlobalAppMode == "FULLMODE") {
                    //var arrProds = listProductsNotShow();

                    //GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                    //    return arrProds.indexOf(item.Name) == -1;
                    //});

                    //AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                    //    return arrProds.indexOf(item.ProductoDescripcion) == -1;
                    //});
                    
                    GlobalAllProducts = GlobalAllProducts.filter(function (item) {
                        return arrProds.indexOf(item.Name) != -1;
                    });

                    AllProductListByUsages = AllProductListByUsages.filter(function (item) {
                        return arrProds.indexOf(item.ProductoDescripcion) != -1;
                    });
                }
            }

            var FilteredProductList = AllProductListByUsages.filter(function (item) {
                return item.UsoDescripcion.indexOf(currentUsageFound.descUso) != -1;
            });

            var $select_elem = $(".ddlAllProducts");

            if ($select_elem.length > 1) {
                $.each($select_elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == vehicleRandomID) {
                        $select_elem = $(obj);
                    }
                });
            }

            if (FilteredProductList.length > 0) {

                var NewCurrentProductList = [];

                $.each(FilteredProductList, function (idx, plist) {

                    var prodsNews = altFind(GlobalAllProducts, function (currProd) {
                        return currProd.Name.indexOf(plist.ProductoDescripcion) != -1
                    });

                    if (prodsNews != undefined) {
                        NewCurrentProductList.push(prodsNews);
                    }
                });

                GlobalDataProductsByUsage = NewCurrentProductList;

                vehicle.GlobalDataProductsByUsage = GlobalDataProductsByUsage;
                vehicle.GlobalAllProducts = GlobalAllProducts;

                var realVehicle = setVehicle(vehicle);

                setAllVehicleDataToSave(realVehicle, "UPDATE");

                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
                var firstValue = true;

                $.each(NewCurrentProductList, function (idx, obj) {

                    if (firstValue && GlobalAppMode == "LEYMODE") {
                        $select_elem.append("<option value='" + obj.Id + "' selected='selected'>" + obj.Name + "</option>");
                        firstValue = false;
                    }
                    else {
                        $select_elem.append("<option value='" + obj.Id + "'>" + obj.Name + "</option>");
                    }
                });

                if (!firstValue && GlobalAppMode == "LEYMODE") {
                    $select_elem.trigger('change');
                }
            }
            else {
                $select_elem.empty();
                $select_elem.append('<option value="">Seleccionar</option>');
            }
        }
    }
    return [];
}

function getAllDataVehicleByRandomID(randomID) {

    if (AllVehicleDataToSave != null) {

        var vehicle = altFind(AllVehicleDataToSave, function (item) {
            return item.randomId == randomID;
        });
    }

    return vehicle;
}

function getOptionSelectedDropWithClass(dropClass, randomID) {

    var $select_elem = $(dropClass);

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    return $select_elem.val();
}

function getOptionSelectedTextDropWithClass(dropClass, randomID) {

    var $select_elem = $(dropClass);

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    return $("#" + $select_elem.attr('id') + " option:selected").text();
}

function getOptionsByTextOfDropWithClassAndSelect(dropClass, randomID, theText) {

    var $select_elem = $(dropClass);

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    $("#" + $select_elem.attr('id') + " option").each(function () {
        if ($(this).text() == theText) {
            $(this).attr('selected', 'selected');
        }
    });
    $($select_elem).trigger('change');

}

function getOptionsByValueOfDropWithClassAndSelect(dropClass, randomID, value, noTriggerChange) {

    var $select_elem = $(dropClass);

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    $("#" + $select_elem.attr('id') + " option").each(function () {
        if ($(this).val() == value) {
            $(this).attr('selected', 'selected');
        }
    });

    if (noTriggerChange !== true) {
        $($select_elem).trigger('change');
    }


}

function qtyVehiclesByVehicle(isFlotillaMode) {

    var totalQtyByVehicle = 0;
    //contando la cantidad de cada Vehículo
    $.each(AllVehicleDataToSave, function (idx, vehicle) {

        var q = $(".qtyVehicles");
        if (q.length > 1) {
            $.each(q, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == vehicle.randomId) {

                    q = $(obj);
                }
            });
        }

        //if (isFlotillaMode == true) {
        //    q = getHtmlElementByClass('qtyVehicles_Flot', vehicle.randomId);
        //}

        var actualVehicleQty = parseInt(q.val()); //vehicle.VehicleQuantity;

        totalQtyByVehicle += actualVehicleQty;
    });

    return totalQtyByVehicle;
}

function qtyVehiclesByVehicleForRequoting() {

    var totalQtyByVehicle = 0;
    //contando la cantidad de cada Vehículo
    $.each(AllVehicleDataToSave, function (idx, vehicle) {

        var actualVehicleQty = parseInt(vehicle.VehicleQuantity);

        totalQtyByVehicle += actualVehicleQty;
    });

    return totalQtyByVehicle;
}

function removeVehicle(randomID) {

    var current = altFind(AllVehicleDataToSave, function (item) {
        return item.randomId == randomID
    });

    if (current != undefined) {
        AllVehicleDataToSave = AllVehicleDataToSave.filter(function (item) {
            return item.randomId != randomID
        });
    }

    //var qtyVehicles = qtyVehiclesByVehicle();

    //$.ajax({
    //    url: '/Home/GetPercentByQtyVehicle',
    //    type: 'POST',
    //    dataType: 'json',
    //    data: { qtyVehicles: qtyVehicles },
    //    async: true,
    //    success: function (data) {
    //        PercentByQtyVehicle = data;

    //        if (data > 0) {
    //            isFlotilla = true;
    //        } else {
    //            isFlotilla = false;
    //        }
    //    }
    //});

    var quotationCoreNumber = getQuotationCoreNumber();
    var vehicleID = current.Id;

    if (current.SecuenciaVehicleSysflex > 0) {
        $.ajax({
            url: '/Home/DeleteVehicleOnSysflex',
            type: 'POST',
            dataType: 'json',
            data: { SecuenciaVehicleSysflex: current.SecuenciaVehicleSysflex, quotationCoreNumber: quotationCoreNumber, vehicleID: vehicleID },
            async: false,
            success: function (data) {
                if (data == "ERROR") {
                    showError(['A ocurrido un error Eliminando el Vehículo'], 'Eliminando Vehículo');
                }
            }
        });
    }

    //remuevo el Vehículo de la seccion de Vehículos
    var dv = $(".parentSectionVehicle");
    if (dv.length > 1) {
        $.each(dv, function (idx, obj) {
            var randomid = $(obj).attr("data-vehiclerandomid");
            if (randomid == randomID) {
                dv = $(obj);
            }
        });
    }
    dv.remove();

    refreshDiscountSectionAllVehicle();

    GlobalVehicleDelete = true;
}

function getQuotationCoreNumber() {
    return $("#QuotationCoreNumber").val();
}

function SetEditVehicle(randomID) {
    var vehicle = getAllDataVehicleByRandomID(randomID);
    if (vehicle != undefined) {

        isEditingVehicle = true;
        vehicleRandomID = vehicle.randomId;
        wasLoadVehicle = false;

        //trayendo driver
        $("#VehicleDriver_Id").val(vehicle.Driver_Id);
        //$("#VehicleDriver_Id").trigger("chosen:updated");
        $("#VehicleDriver_Id").trigger("change");
        removeErrorBorderClass($("#VehicleDriver_Id"), true);

        $("#lastModelSelected").val(vehicle.VehicleModel_Model_Id);

        //Cascade
        $("#VehicleModel_Make_Id").val(vehicle.VehicleModel_Make_Id);
        //$("#VehicleModel_Make_Id").trigger("chosen:updated");
        $("#VehicleModel_Make_Id").trigger("change");
        $("#lastMakeSelected").val(vehicle.VehicleModel_Make_Id);

        $("#VehicleModel_Model_Id").val(vehicle.VehicleModel_Model_Id);
        //$("#VehicleModel_Model_Id").trigger("chosen:updated");
        $("#VehicleModel_Model_Id").trigger("change");
        removeErrorBorderClass($("#VehicleModel_Model_Id"), true);

        $("#yearsAvaibles").val(vehicle.Year);
        //$("#yearsAvaibles").trigger("chosen:updated");
        $("#yearsAvaibles").trigger("change");

        $("#VehicleType").val(vehicle.SelectedVehicleTypeName);
        //$("#VehicleType").trigger("chosen:updated");
        $("#VehicleType").trigger("change");

        $("#Usages").val(vehicle.UsageId);
        //$("#Usages").trigger("chosen:updated");
        $("#Usages").trigger("change");
        //

        $("#VehicleYearsOld").val(vehicle.VehicleYearOld);
        $("#VehicleYearsOld").parent().addClass('is-dirty');
        removeErrorBorderClass($("#VehicleYearsOld"), true);
        $("#VehicleYearsOld").trigger("change");

        if (GlobalIsMobile) {
            var vpr = vehicle.VehiclePrice;
            vpr = number_format(vpr);
            $("#vehiclePrice").val(vpr);
        }
        else {
            $("#vehiclePrice").val(vehicle.VehiclePrice);
        }
        removeErrorBorderClass($("#vehiclePrice"));

        $("#StoreCar").val(vehicle.StoreId);
        //$("#StoreCar").trigger("chosen:updated");
        $("#StoreCar").trigger("change");
        removeErrorBorderClass($("#StoreCar"), true);

        //$('#headVH').find('a').trigger('click');
        $('#headCober').find('a').trigger('click');
        $('#headCober').addClass('disabledAccordionTab');
        $('#headInfoCC').addClass('disabledAccordionTab');
        $("#AddVehicle").html('<i class="material-icons">add</i> Editar Vehículo');

        vehicle.iscompletedVehicle = false;

        $("#fuelType").val(vehicle.SelectedVehicleFuelTypeId);
        $("#fuelType").trigger("change");

        var realVehicle = setVehicle(vehicle, randomID);
        setAllVehicleDataToSave(realVehicle, "UPDATE");

        var apply = ApplyToZero(realVehicle.randomId);
        if (!apply) {
            $("#vehiclePrice").val("0");
        }

    } else {
        $('#headCober').removeClass('disabledAccordionTab');
        $('#headInfoCC').removeClass('disabledAccordionTab');
        $("#AddVehicle").html('<i class="material-icons">add</i> Agregar Vehículo');
    }

}

function updateHtmlValueFromVehicle(randomID) {

    var vh = getAllDataVehicleByRandomID(randomID);

    var desc = vh.VehicleDescription + " " + vh.Year;
    var p = vh.VehiclePrice;

    var $elem = $(".vehicleTitle");
    if ($elem.length > 1) {
        $.each($elem, function (idx, obj) {
            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $elem = $(obj);
            }
        });
    }
    $elem.html(desc);

    var $elem = $(".VehicleValue");
    if ($elem.length > 1) {
        $.each($elem, function (idx, obj) {
            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $elem = $(obj);
            }
        });
    }
    $elem.html("$" + number_format(p, 2));

    if (isEditingVehicle) {

        var $elem = $(".totalPrime");

        if ($elem.length > 1) {
            $.each($elem, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == randomID) {
                    $elem = $(obj);
                    $elem.html("$" + number_format("0", 2));
                }
            });
        } else {
            $elem.html("$" + number_format("0", 2));
        }

        $elem = $(".ISC");

        if ($elem.length > 1) {
            $.each($elem, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == randomID) {
                    $elem = $(obj);
                    $elem.html("$" + number_format("0", 2));
                }
            });
        } else {
            $elem.html("$" + number_format("0", 2));
        }

        $elem = $(".totalToPay");

        if ($elem.length > 1) {
            $.each($elem, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == randomID) {
                    $elem = $(obj);
                    $elem.html("$" + number_format("0", 2));
                }
            });
        } else {
            $elem.html("$" + number_format("0", 2));
        }
    }
}

function UpdateVehiclesByAgentChange() {

    if (getOldAgentSelected()) {

        var agentActual = getNewAgentSelected();
        var oldAgent = getOldAgentSelected();

        if (agentActual != null && oldAgent.AgentCode != agentActual.AgentCode) {

            CallRequoting();

            var NewAgent = $("#AgentList").val();
            $("#oldAgentSelected").val(NewAgent);
        }
    }
}

function someDataClientWasChanged() {

    if (changedDateBirth) {
        return true;
    } else if (changedSex) {
        return true;
    } else if (changedForeingLicenceDriver) {
        return true;
    } else { return false; }
}

function CallRequoting() {

    var allGood = false;
    if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {

        $.each(AllVehicleDataToSave, function (idx, vehicle) {

            var realVehicle = setVehicle(vehicle, vehicle.randomId);

            setAllVehicleDataToSave(realVehicle, "UPDATE");

            allGood = getRates(realVehicle.randomId, true);
        });

        if (allGood) {

            changedDateBirth = false;
            changedSex = false;
            changedForeingLicenceDriver = false;

            SaveDataVehicle(true);

            $('#headCober').find('a').trigger('click');
        }
    }
}

function SaveDataVehicle(requoting) {

    var pass = false;

    //Si se borro algun vehiculo, entonces cuando le de click al boton de "siguiente" recotizar
    if (GlobalVehicleDelete) {

        requoting = true;
    }
    //

    /*
      Solo se debera llamar este metodo cuando sea una recotizacion, ya que este recoje toda la data y la inserta
      si no es una recotizacion entonces se usara el metodo que guarda un vehiculo a la vez
    */
    if (requoting) {

        if ($("#hdnInvalidUsage").val() == "S") {
            showError(['No puede continuar porque tiene seleccionado un Uso Principal invalido.'], 'Uso Principal');
            return false;
        }

        if ($("#hdnChangedSomethingClient").val() == "S") {
            showError(['No puede continuar porque tiene que guardar los cambios realizados al Conductor Principal.'], 'Conductor Principal');
            return false;
        }

        if (isEditingVehicle) {
            showError(['No puede continuar porque tiene que guardar los cambios realizados al Vehículo.'], 'Cambios Vehículo');
            return false;
        }

        var entro = false;

        if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
            var incompleted = false;

            $.each(AllVehicleDataToSave, function (i, v) {

                if (!entro && GlobalAppMode == "LEYMODE") {
                    var current = getAllDataVehicleByRandomID(v.randomId);
                    var realVehicle = setVehicle(current, v.randomId);
                    setAllVehicleDataToSave(realVehicle, "UPDATE");
                    entro = true;
                }


                if (v.iscompletedVehicle == false) {
                    incompleted = true;
                }
            });

            if (incompleted) {
                showError(['Para poder continuar debe completar todos los datos del/los Vehículo(s).'], 'Completar Vehículo(s)');
                return false;
            }

            var objVehicle = JSON.stringify(AllVehicleDataToSave);

            $.ajax({
                url: "/Home/SaveDataVehicle",
                type: "POST",
                data: { jsondata: objVehicle, requoting: requoting },
                async: false,
                success: function (data, textStatus, jqXHR) {
                    if (data.messageError) {
                        showError([data.messageError], 'Error guardando la Cotización');
                        pass = false;
                    } else {

                        var datav = JSON.parse(data.VehicleDataMatch);
                        $.each(datav, function (i, item) {

                            var newVehicle = getAllDataVehicleByRandomID(item.randomId);
                            if (newVehicle != null || newVehicle != undefined) {

                                newVehicle.Id = item.vehicleID;

                                var realVehicle = setVehicle(newVehicle);
                                setAllVehicleDataToSave(realVehicle, "UPDATE");

                                GlobalChangeForModeLey = false;
                                GlobalVehicleDelete = false;

                                refreshDiscountSectionAllVehicle();
                            }
                        });

                        isEventAdded = false;
                        pass = true;
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
        } else {
            showError(['Para poder continuar debe agregar al menos 1 Vehículo.'], 'Debe agregar un Vehículo');
            pass = false;
        }
    }
    else if (GlobalSaveVehicleOnly) {
        pass = true;
    }

    var completed = areCompletedAllVehicles();

    if (pass && !completed) {
        showError(['Para poder continuar debe completar todos los datos del/los Vehículo(s).'], 'Completar Vehículo(s)');
        pass = false;
    }

    return pass;
}

function SaveBasicClientData() {

    if ($("#frmClientInfoBasic").valid() == false) {
        return false;
    }

    var newJson = {};

    if (GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") {

        var DefaultClient = $("#hdnDefaultClient").val();

        DefaultClient = JSON.parse(DefaultClient);

        //Cliente por default
        if (GlobalAppMode == "LEYMODE") {
            newJson.FirstName = DefaultClient.Name;
            newJson.FirstSurname = "";
            newJson.DateOfBirth = DefaultClient.DateOfBirth;
        }
        else if (GlobalAppMode == "FULLMODE") {
            newJson.FirstName = $("#FirstName").val();
            newJson.FirstSurname = $("#FirstSurname").val();
            newJson.DateOfBirth = $("#DateOfBirth").val();
        }

        newJson.Sex = DefaultClient.Sex;
        newJson.ForeignLicense = "NO";
        newJson.IdentificationType = DefaultClient.IdentificationType;

        newJson.IdentificationNumber = $("#IdentificationNumber").val();

        newJson.PhoneNumber = $("#PhoneNumber").val();
        newJson.Email = $("#Email").val();

        newJson.agentSelected = $("#AgentList").val();
        newJson.driver = $("#driver").val() == "" ? 0 : $("#driver").val();

        newJson.StartDate = GlobalcurrentStartDateSelected;
        newJson.EndDate = GlobalcurrentEndDateSelected;

        newJson.quotationID = $("#quotationID").val() == '' ? 0 : $("#quotationID").val();

        newJson.couponCode = $('#hdnCouponCode').val();
        newJson.couponPercentageDiscount = $('#hdnCuponDiscount').val();
        newJson.couponProspectId = $('#hdnProspectoID').val();

        newJson.contactFormId = $('#contactForm').val();

        if ($('#CouponCode').val() !== '') {

            $('#contactForm').val('');
            $('#contactForm').trigger('change');

            newJson.contactFormId = $('#contactForm').val();
        }

    }
    else {

        newJson.FirstName = $("#FirstName").val();
        newJson.FirstSurname = $("#FirstSurname").val();
        newJson.DateOfBirth = $("#DateOfBirth").val();
        newJson.Sex = $("#Sex").val();
        newJson.ForeignLicense = $("#ForeignLicense").val();

        var check = $("input[name='IdentificationType']");

        $.each(check, function (i, iden) {
            var objCheck = $(iden);
            if (objCheck.is(":checked")) {
                newJson.IdentificationType = objCheck.val();
            }
        });

        newJson.IdentificationNumber = $("#IdentificationNumber").val();
        newJson.PhoneNumber = $("#PhoneNumber").val();
        newJson.Email = $("#Email").val();

        newJson.agentSelected = $("#AgentList").val();
        newJson.driver = $("#driver").val() == "" ? 0 : $("#driver").val();

        newJson.StartDate = GlobalcurrentStartDateSelected;
        newJson.EndDate = GlobalcurrentEndDateSelected;

        newJson.quotationID = $("#quotationID").val() == '' ? 0 : $("#quotationID").val();

        newJson.couponCode = $('#hdnCouponCode').val();
        newJson.couponPercentageDiscount = $('#hdnCuponDiscount').val();
        newJson.couponProspectId = $('#hdnProspectoID').val();

        newJson.contactFormId = $('#contactForm').val();

        newJson.flotillaMode = $("#QuotTypeFlotilla").is(':checked');

    }


    //Datos Referidos
    newJson.ReferredId = $("#hdnReferredById").val() == '' ? 0 : $("#hdnReferredById").val();
    newJson.ReferredName = $("#referredByName").val();
    newJson.ReferredIdentificationNumber = $("#referredByIdentificationNumber").val();
    newJson.ReferredPhone = $("#referredByPhoneNumber").val();
    newJson.ReferredEmail = $("#referredByEmail").val();
    newJson.ReferredPolicy = $("#referredByPolicy").val();

    var objDriver = JSON.stringify(newJson);

    $.ajax({
        url: "/Home/SaveClientInfoBasic",
        type: "POST",
        data: { jsondata: objDriver },
        cache: false,
        success: function (data, textStatus, jqXHR) {

            //para redireccionar a una pagina y/o error
            if (data.isRedirect) {
                window.location.href = data.redirectUrl;
            }

            if (data.MessageSucess) {
                showSucess([data.MessageSucess]);
            }

            if (data.messageError) {
                showError([data.messageError]);
                return false;
            }

            if (data.showNextSection) {

                if ($('#QuotTypeFlotilla').is(':checked')) {
                    $('#headVH2').find('a').trigger('click');
                }

                if ($('#headVH').hasClass('disabledAccordionTab')) {
                    disableAccordionsSections(false);
                }

                $('#headVH').find('a').trigger('click');
            }

            if (someDataClientWasChanged()) {

                disableAccordionsSections(false, true);

                CallRequoting();

                $('#selecCober').addClass("collapse show");

                if ($('#QuotTypeFlotilla').is(':checked')) {
                    $('#headVH2').find('a').trigger('click');
                }
                return false;
            }

            var QuotationId = data.quotationIdEncript;
            $('#hdnEncrypQuotId').val(QuotationId);

            window.history.pushState({ order: 1 }, document.title, '/Home/Index/' + data.quotationIdEncript);
            loadQuotation(QuotationId);

            $('#hdnDefaultClientWasSaved').val("S");
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

    return false;
}

function TotalByQtyVehicle() {

    if (PercentByQtyVehicle > 0) {

        var totalPrime = getTotalPrimeOfAllVehicles();
        var _percentByQtyVehicle = PercentByQtyVehicle;

        var result = totalPrime * (_percentByQtyVehicle / 100);

        return result;
    }
    return 0;
}

function getCountriesByBl() {

    $.ajax({
        url: "/Home/getCountyBL",
        type: "POST",
        data: { countryID: 0 },
        cache: false,
        success: function (data, textStatus, jqXHR) {
            var countries = data;
            var bl = "";
            var defaultCountry = 129;//RD           

            var $select_elem = $("#ddlCountry");
            $select_elem.empty();
            //$select_elem.append('<option value=""></option>');
            $.each(countries, function (idx, obj) {

                if (defaultCountry == obj.countryID) {
                    $select_elem.append("<option value='" + obj.countryID + "' selected='selected'>" + obj.countryName + "</option>");
                } else {
                    $select_elem.append("<option value='" + obj.countryID + "'>" + obj.countryName + "</option>");
                }
            });
            $select_elem.trigger("chosen:updated");
            $select_elem.trigger("change");

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

function getBlByCountry() {

    $("#ddlCountry").change(function () {
        var cid = $(this);

        if (cid.val() !== '') {

            $.ajax({
                url: "/Home/getCountyBL",
                type: "POST",
                data: { countryID: cid.val() },
                cache: false,
                success: function (data, textStatus, jqXHR) {
                    var countries = data;

                    var $select_elem = $("#ddlbussinessline");
                    $select_elem.empty();

                    $.each(countries, function (idx, obj) {
                        var bl = obj._BussinesLines;
                        $.each(bl, function (idx, objBL) {
                            $select_elem.append("<option value='" + objBL.blID + "'>" + objBL.blName + "</option>");
                        });
                    });
                    $select_elem.trigger("chosen:updated");
                    $select_elem.trigger("change");

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

        } else {
            var $select_elem = $("#ddlbussinessline");
            $select_elem.empty();
        }
    });
}

function redirectByBl() {

    $("#ddlbussinessline").change(function () {
        var cid = $(this);

        if (cid.val() !== '') {

            $.ajax({
                url: "/Home/redirectToApp",
                type: "POST",
                data: { blName: $("#ddlbussinessline option:selected").text() },
                cache: false,
                success: function (data, textStatus, jqXHR) {

                    if (data.pathredirect != "") {
                        //Le notifico al cliente que perdera la informacion
                        showQuestion("En caso de que no haya guardado los datos que se han editado o capturado van a perderse.    Esta seguro que desea abandonar la pagina?", "Cambiar de Aplicacion",
                            function () {

                                location.href = data.pathredirect;
                                return false;
                            },
                            function () {
                                return false;
                            });
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
    });
}

function getMarbete() {

    var qid = parseInt($('#quotationID').val());

    $.ajax({
        url: "/Home/GetMarbete",
        type: "POST",
        data: { quotationId: qid },
        cache: false,
        success: function (data, textStatus, jqXHR) {

            if (data.error == undefined || data.error == '') {
                window.open(data.reportName, '_blank');
            } else {
                showError([data.error], 'Error obteniendo su marbete');
                return false;
            }
        },
        error: function (data, textStatus, jqXHR) {
            if (data.error) {
                showError([data.error], 'Error obteniendo su marbete');
            } else {
                var textError = data.error + " " + textStatus + " " + jqXHR;
                showError([textError], 'Error obteniendo su marbete');
            }

            return false;
        }
    });

}

function remove0kmIfIsNotNew(randomID) {

    var vehicle = getAllDataVehicleByRandomID(randomID);

    if (vehicle != null && vehicle.VehicleYearOld) {

        if (vehicle.GlobalDataProductsByUsage.length > 0) {

            var prods = altFind(vehicle.GlobalDataProductsByUsage, function (item) { return item.Name.indexOf('0 KM') != -1 });

            //var prods = vehicle.GlobalDataProductsByUsage.find(function (item) { return item.Name.indexOf('0 KM') != -1 });

            if (prods) {

                var exit = false;

                if (vehicle.VehicleYearOld == "Usado") {

                    var $select_elem = $('.ddlAllProducts');

                    if ($select_elem.length > 1) {

                        $.each($select_elem, function (idx, obj) {

                            var randomid = $(obj).attr("data-vehiclerandomid");

                            if (randomid == randomID) {
                                $select_elem = $(obj);
                            }
                        });
                    }

                    var ddlProdID = $select_elem.attr('id');

                    $("#" + ddlProdID + " option").each(function () {
                        var t = this;

                        if (t.text.indexOf('0 KM') != -1) {
                            var index = t.index;
                            document.getElementById(ddlProdID).options[index].disabled = true;
                            exit = true;
                        }

                        if (exit) {
                            return false;
                        }
                    });
                }
                else {

                    var $select_elem = $('.ddlAllProducts');

                    if ($select_elem.length > 1) {

                        $.each($select_elem, function (idx, obj) {

                            var randomid = $(obj).attr("data-vehiclerandomid");

                            if (randomid == randomID) {
                                $select_elem = $(obj);
                            }
                        });
                    }

                    var ddlProdID = $select_elem.attr('id');

                    $("#" + ddlProdID + " option").each(function () {
                        var t = this;

                        if (t.text.indexOf('0 KM') != -1) {
                            var index = t.index;
                            document.getElementById(ddlProdID).options[index].disabled = false;
                            exit = true;
                        }

                        if (exit) {
                            return false;
                        }
                    });
                }

            } else {

                var $select_elem = $('.ddlAllProducts');

                if ($select_elem.length > 1) {

                    $.each($select_elem, function (idx, obj) {

                        var randomid = $(obj).attr("data-vehiclerandomid");

                        if (randomid == randomID) {
                            $select_elem = $(obj);
                        }
                    });
                }

                var ddlProdID = $select_elem.attr('id');

                $("#" + ddlProdID + " option").each(function () {
                    var t = this;

                    if (t.text.indexOf('0 KM') != -1) {
                        var index = t.index;
                        document.getElementById(ddlProdID).options[index].disabled = false;
                        exit = true;
                    }

                    if (exit) {
                        return false;
                    }
                });
            }
        }
    }
}

function fillDdlActions() {

    var d = [];
    d.push({ id: '1', name: 'Nueva Cotización' });
    d.push({ id: '2', name: 'Historico Cotizaciónes' });
    d.push({ id: '3', name: 'Inclusión' });

    var $select_elem = $("#filtroHistorico");
    $select_elem.empty();
    var first = true;

    $.each(d, function (idx, item) {

        var sel = "";
        if (first) {

            sel = "selected='selected'";

            first = false;
        }

        $select_elem.append("<option value='" + item.id + "' " + sel + ">" + item.name + "</option>");
    });
    $select_elem.trigger("chosen:updated");
}

function disableAccordionsSections(disabled, disabledall) {

    if (disabledall) {
        if (disabled) {
            $("#headVH").addClass('disabledAccordionTab');
            $("#headCober").addClass('disabledAccordionTab');
            $("#hdnChangedSomethingClient").val("S");
        } else {
            $("#headVH").removeClass('disabledAccordionTab');
            $("#headCober").removeClass('disabledAccordionTab');
            $("#hdnChangedSomethingClient").val("N");
        }
    } else {
        if (disabled) {
            $("#headVH").addClass('disabledAccordionTab');
            $("#hdnChangedSomethingClient").val("S");
        } else {
            $("#headVH").removeClass('disabledAccordionTab');
            $("#hdnChangedSomethingClient").val("N");
        }
    }
}

function resetDateOfBirth(reset) {
    if (reset) {

        $(".dateOfBirth.datepicker").val("N/A");
        $(".dateOfBirth.datepicker").parent().addClass("is-dirty");
        $(".dateOfBirth.datepicker").datepicker("destroy");
        $(".dateOfBirth.datepicker").parent().removeClass("requerido");
        $(".dateOfBirth.datepicker").parent().addClass("erarequerido");

    } else {
        //Edad maxima para poder asegurar un Vehículo (86 anos)
        var currDate = moment(new Date());
        var minDate = currDate.add(18 * -1, 'years');
        //
        if ($(".dateOfBirth.datepicker").val() === "N/A") {
            $(".dateOfBirth.datepicker").datepicker("destroy");
            $(".dateOfBirth.datepicker").val("");
            $(".dateOfBirth.datepicker").parent().removeClass("is-dirty");
        }

        $('.dateOfBirth.datepicker').datepicker({
            changeMonth: true,
            changeYear: true,
            yearRange: "-80:+0",
            maxDate: new Date(minDate),
            onSelect: function (dateText) {
                var $parent = $(this).parent();
                $parent.addClass("is-dirty");
                $parent.removeClass('requerido');
                $parent.removeClass('erarequerido');
                $parent.addClass('erarequerido');

                if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                    changedDateBirth = true;
                    disableAccordionsSections(true, true);
                }
                /*else {
                    $("#headVH").removeClass('disabledAccordionTab');
                    $("#headCober").removeClass('disabledAccordionTab');
                    $("#hdnChangedSomethingClient").val("N");
                }*/
            }
        });

        $(".dateOfBirth.datepicker").focusout();
    }
}

function UpdateAdditionalSelected(randomID) {

    var veh = getAllDataVehicleByRandomID(randomID);

    var ServicesCoverages = veh.GlobalDataProductLimits.ServicesCoverages;

    $.each(veh.GlobalDataPopupSelectedServices, function (idx, item) {

        var service = altFind(ServicesCoverages, function (sc) { return sc.Name == item.id });

        //var service = ServicesCoverages.find(function (sc) { return sc.Name == item.id });

        $.each(service.Coverages, function (idx, cov) { cov.IsSelected = (cov.Id == item.value); });

    });
}

function ApplyToZero(randomID, isFlotillaMode) {

    if (randomID == null || randomID == 0) {
        return true;
    }

    var vehiclePrice = 0;

    var vh = getAllDataVehicleByRandomID(randomID);
    if (vh !== null) {
        vehiclePrice = vh.VehiclePrice;
    }

    var $select_elem = $('.ddlAllProducts');

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    if (isFlotillaMode == true) {
        $select_elem = getHtmlElementByClass('ddlPlan', randomID);
    }

    var plan = $("#" + $select_elem.attr("id") + " option:selected").text();

    if (plan !== "" && (plan.toLowerCase().indexOf('ley') != -1 || plan.toLowerCase().indexOf('ultra') != -1)) {
        return true;
    }
    else if (parseFloat(vehiclePrice) > 1) {
        return true;
    }
    return false;
}

function SetFieldsApplyToZero(obj, randomID) {

    var vehiclePrice = $("#vehiclePrice");

    if (obj == true) {
        $("#lnkContinue").show();

    } else {
        showWarning(['El Valor del vehículo debe ser mayor a 0 para este plan.', 'Favor Editar el vehículo y ponerle el Valor correcto.'], "Valor vehículo incorrecto");

        $("#lnkContinue").hide();

        vehiclePrice.removeClass("erarequerido");
        vehiclePrice.addClass("requerido");
        //putErrorBorder addAgain erarequerido;

        var $select_elem = $('.Coverages');
        if ($select_elem.length > 1) {

            $.each($select_elem, function (idx, obj) {
                var randomid = $(obj).attr("data-vehiclerandomid");
                if (randomid == randomID) {
                    $select_elem = $(obj);
                }
            });
        }
        $select_elem.empty();
        $select_elem.append('<option value="">Seleccionar</option>');
        $select_elem.trigger("chosen:updated");
    }
}

function getMainOptions() {

    $.ajax({
        url: "/Home/getMainOptions",
        dataType: "json",
        data: {},
        async: false,
        success: function (result) {
            var $select_elem = $("#filtroHistorico");
            $select_elem.empty();

            $.each(result.data, function (idx, obj) {
                $select_elem.append('<option value="' + obj.Value + '">' + obj.name + '</option>');
            });

            $("#hdnPolicySendByVO").val(result.PolicySendByVO);

            switch (result.optionSelected) {
                case "INCLUSION":
                    $select_elem.val('3');
                    $select_elem.trigger("change.select2");
                    $select_elem.trigger("change");
                    break;
                case "EXCLUSION":
                    $select_elem.val('4');
                    $select_elem.trigger("change.select2");
                    $select_elem.trigger("change");
                    break;
                case "CAMBIOS":
                    $select_elem.val('5');
                    $select_elem.trigger("change.select2");
                    $select_elem.trigger("change");
                    break;
                case "COTIRENOV":
                    $select_elem.val('7');
                    $select_elem.trigger("change.select2");
                    $select_elem.trigger("change");
                    break;
                default:
                    $("#hdnPolicySendByVO").val(null);
                    $select_elem.val('1');
                    $("#hdnAppMode").val(result.AppMode);
                    $("#hdnDefaultClient").val(result.defaultClient);
                    configAppMode();
                    break;
            }

            $select_elem.trigger("change.select2");
        },
        error: function (response) {
            showError([response.responseText], "Error buscando opciones de navegación");
        }
    });
}

//esto es por si hay que traducir el mensaje de notificacion de la seguridad(lo ideal seria hacer el arreglo en la dll)
function setTextNotification() {
    /*if ($("div.loading2").length > 0) {
            $("div.loading2").remove();
        }*/

    if ($("#container").hasClass("ui-notify")) {

        //Espanol
        $("#STFBtnSessionYes").val("Si");
        $("#STFBtnSessionNo").val("No");
        var $ptag = $("#container").find('p');
        var $htag = $("#container").find('h1');

        var ptext = "";

        if ($htag.length > 0) {
            $htag.html("Advertencia");
        }

        if ($ptag.length > 0) {
            var k = $ptag.html();
            k = k.replace("Your session will expire in ", "Su sesión expirará en <span id='STFSessionCountDown' class='style colorDefinition size_sm'>00:43</span>");
            k = k.replace(" min, do you want to extend the session?", "minuto(s), ¿desea extender la sesión?");
            $ptag.html(k);
        }
    }
    //
}

//Sobrescribiendo la funcion de la dll de seguridad que permite que la sesion siga abierta si el cliente le da a si
btnSTFNotificationYes = function () {

    STFValidateSessionOver();

    clearTimeout(redirectToWelcomePageTimer);
    clearTimeout(sessionWarningTimer);

    SetSessionTimeOuts();

    /*Quitando el BackGround*/
    $(".loading2").remove();
    /**/
};

function SendCallback() {
    var hasError = false;

    if ($("#FirstNamesCallBack").val() == '') {
        hasError = true;
        showError(["Debe indicar su Nombre y Apellido para que nuestros representantes le llamen"], "Información incompleta");
        return false;
    }
    if ($("#LastNamesCallBack").val() == '') {
        hasError = true;
        showError(["Debe indicar su Nombre y Apellido para que nuestros representantes le llamen"], "Información incompleta");
        return false;
    }
    if ($("#PhoneType").val() == '' || $("#PhoneType").val() == null) {
        hasError = true;
        showError(["Debe indicar el Tipo de teléfono"], "Información incompleta");
        return false;
    }
    if ($("#PhoneType").val() == '1') {
        if ($("#CountryCodeCallback").val() == '') {
            $("#CountryCodeCallback").addClass('requerido');
            hasError = true;
            showError(["Debe indicar el código del País"], "Información incompleta");
            return false;
        } else
            $("#CountryCodeCallback").removeClass('requerido');
    } else
        $("#CountryCodeCallback").removeClass('requerido');

    if ($("#CityCodeCallback").val() == '') {
        hasError = true;
        showError(["Debe indicar el código de área de la ciudad"], "Información incompleta");
        return false;
    }

    if ($("#NumToCall").val() == '') {
        hasError = true;
        showError(["Debe indicar el número al que desea que le llamen"], "Información incompleta");
        return false;
    }

    if (!hasError) {
        var datos = {
            CntCode: $("#CountryCodeCallback").val() != 1 ? "" : $("#CountryCodeCallback").val(),
            CityCode: $("#CityCodeCallback").val(),
            NumToCall: $("#NumToCall").val(),
            Lang: "ES",
            FirstNames: $("#FirstNamesCallBack").val(),
            LastNames: $("#LastNamesCallBack").val(),
            City: "Santo Domingo",
            Country: "Republica Dominicana",
            Client: "Y",
            Email: "",
            PolicyNum: $("#PolicyNumCallBack").val(),
            Products: "SERVICIO MOBILE",
            Reason: "Other",
            Company: "STB",
            Message: "",
            Svc: "a"
        };

        $.ajax({
            url: "/Home/SendCallback",
            method: "POST",
            dataType: "json",
            data: datos,
            success: function (result) {
                ClearCallBack(true);
                showInfo(["En breves instantes uno de nuestros representantes le estará contactando"], "Llamada en proceso");
            },
            error: function (response) {
                if (response.responseText == "True") {
                    ClearCallBack(true);
                    showInfo(["En breves instantes uno de nuestros representantes le estará contactando"], "Llamada en proceso");

                } else {
                    ClearCallBack(true);
                    showError(["Ha ocurrido un error mientras se realizaba su llamada. Intente nuevamente por favor"], "Error realizando la llamada");
                }
            }
        });
    }

}

function getPhoneTypes() {
    $.ajax({
        url: "/Home/GetPhoneTypes",
        dataType: "json",
        async: false,
        data: {},
        success: function (result) {
            var $select_elem = $("#PhoneType");
            $select_elem.empty();
            $select_elem.append('<option value="">Tipo de teléfono</option>');

            $.each(result, function (idx, obj) {
                $select_elem.append('<option value="' + obj.Value + '">' + obj.name + '</option>');
            });
            //$select_elem.val('1');
            $select_elem.trigger("chosen:updated");
            $("#CountryCodeCallback").prop('disabled', true);
            $("#CityCodeCallback").prop('disabled', true);
            $("#NumToCall").prop('disabled', true);
        },
        error: function (response) {
            showError([response.responseText], "Error buscando los tipos de telefonos");
        }
    });
}

function getChatParameters() {
    var $hdnChatConnectionServices = $('#hdnChatConnectionServices');
    var $hdnPathOpenfireHostname = $('#hdnPathOpenfireHostname');
    var $hdnBoshService = $('#hdnBoshService');
    var $hdnDefaultUserChat = $('#hdnDefaultUserChat');

    $.ajax({
        url: "/Home/GetChatConnections",
        dataType: "json",
        async: false,
        data: {},
        success: function (result) {
            if (result != null) {
                $hdnChatConnectionServices.val(result.ConnectionChatService);
                $hdnBoshService.val(result.ConnectionChatService);

                //pathOpenfireHostname = "@apps.statetrust.com";
                $hdnPathOpenfireHostname.val(result.OpenFireHostName);
                $hdnDefaultUserChat.val(result.DefaultUserChat);
                clientUsername = result.MobileAgents;
                var BOSH_SERVICE = $('#hdnBoshService').val();
                connection = new Strophe.Connection(BOSH_SERVICE);
            }
        },
        error: function (response) {
            showError(["ha ocurrido un error obteniendo los parámetros de conexión del chat. Intente nuevamente por favor!"], "Error buscando los parámetros del chat");
        }
    });
}

function chatConnect() {
    var BOSH_SERVICE = $('#hdnBoshService').val();
    connection = new Strophe.Connection(BOSH_SERVICE);
    //connection = new Strophe.Connection("http://172.16.191.140:7070/http-bind/");
    //connection = new Strophe.Connection("https://atl-srv40.atlantica.do:7443/http-bind");
    pathOpenfireHostname = $('#hdnPathOpenfireHostname').val();
    //pathOpenfireHostname = "@conference.apps.statetrust.com";
    //pathOpenfireHostname = "@atl-srv40.atlantica.do";
    chUsername = $('#hdnDefaultUserChat').val();

    var jid = chUsername + pathOpenfireHostname + "/StateTrustChat-Web";

    /*var jid = "abeltre@conference.apps.statetrust.com/StateTrustChat-Web";
    var chUsername = "abeltre";*/

    //var jid = "anon_014836005394839@apps.statetrust.com/StateTrustChat-Web";
    //var jid = "anon_014836005394839@atl-srv40.atlantica.do/StateTrustChat-Web";
    //chUsername = "anon_014836005394839";

    //var jid = "anon_f9c951ff8b294eda9a916e56ead94090@atl-srv40.atlantica.do/StateTrustChat-Web";
    //chUsername = "anon_f9c951ff8b294eda9a916e56ead94090";

    // Validiar en JavaScript si no existe la Session
    connection.connect(jid, chUsername, onConnect);
    connection.send($pres());
    connection.addHandler(onReceiveMessage, null, 'message', 'chat');
    connection.send($pres());
}

function CreateNewQueue() {
    $.ajax({
        url: "/Home/SetNewQueue",
        dataType: "json",
        async: false,
        data: {},
        success: function (result) {

            if (result.result != null) {
                $('#hdnChatJson').val(result.JsonChat);
                if (result.result.Case_Number == "Error") {
                    showError(["Ha ocurrido un error intentando iniciar el chat. Cierre el chat e intente nuevamente por favor"], "Error conectando con el chat")
                    return false;
                } else {
                    $('#hdnChatJson').val(result.JsonChat);
                    $("#hdnChatId").val(result.result.IncidenceId);
                }
            }
        },
        error: function (response) {
            showError([response.responseText + ". Cierre el chat e intente nuevamente por favor"], "Error generando el chat");
        }
    });
}

function AddMessage(sourceMessage, message) //Source: user y agent
{
    messageId += 1;
    var date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;

    message = message + "<br />" + strTime;

    var $ConversationHistory = $('#ConversationHistory');
    var $sendMessage = $('#userMessage');
    var msg = "";
    if (sourceMessage == 'user') {
        msg += '<li class="conversation-part user-cv focus">';
        msg += '<div class="dtl-comment-container">';
        msg += '<div class="comment-avatar">';
        msg += '<div class="agent-avatar-container">';
        msg += '<img class="avatar-small" src="/Content/images/user.png">';
        msg += '</div>';
        msg += '</div>';
        msg += '<div class="dtl-comment bg-primary text-white">';
        msg += '<div class="dtl-block-paragraph">' + message + '</div>';
        msg += '</div>';
        msg += '</div>';
        msg += '</li>';
        $ConversationHistory.append(msg);

        $sendMessage.val("");
        $sendMessage.focus().select();
        $(".dtl-conversation-body").scrollTop(999999);

    } else {
        msg += '<li class="conversation-part admin-cv focus">';
        msg += '<div class="dtl-comment-container">';
        msg += '<div class="comment-avatar">';
        msg += '<div class="agent-avatar-container">';
        msg += '<img class="avatar-small" src="/Content/images/support.png">';
        msg += '</div>';
        msg += '</div>';
        msg += '<div class="dtl-comment">';
        msg += '<div class="dtl-block-paragraph">' + message + '</div>';
        msg += '</div>';
        msg += '</div>';
        msg += '</li> ';
        $ConversationHistory.append(msg);
        $sendMessage.focus().select();
        $(".dtl-conversation-body").scrollTop(999999);
    }
}

function ClearChat() {
    var $ConversationHistory = $("#ConversationHistory");
    $ConversationHistory.html("");
    $('#userMessage').val('');
    $('#hdnChatId').val('');
}

function sendCustomMessage2(type, to, from, body, field1, field2) {
    var m = $msg({
        to: to,
        from: from,
        type: type
    }).c("body").t(body);
    connection.send(m);
}

function sendMsg(from, to, body, value) {
    var m = $msg({
        to: to,
        from: from,
        type: "chat"
    }).c("body").t(body);
    m.up().c("StChat", { xmlns: "urn:xmpp:chatid" }).c("chatid").t(value);
    //connection.send(m);    
    connection.send(m.tree());
}

function onReceiveMessage(msg) {
    var to = $(msg).attr('to');
    var from = $(msg).attr('from');
    var type = $(msg).attr('type');
    messageTO = from;
    $('#ChatProfile').html('En línea...');
    $('#ChatProfilePleaseWait').html('ATLANTICA SEGUROS');
    AddMessage('Admin', msg.textContent);

    return true;
}

function IsAMobile() {
    var isMobile = false; //initiate as false
    // device detection
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
        isMobile = true;
    }
    return isMobile
}

function CloseChatConnection() {
    var JsonChat = $('#hdnChatJson').val();
    var result = false;

    $.ajax({
        url: "/Home/CloseChat",
        dataType: "json",
        async: false,
        data: { pJsonChat: JsonChat },
        success: function (oData) {
            if (oData.name == "success")
                result = true;
            else {
                result = false;
                showError([oData.Value], "Error cerrando el chat");
            }
        },
        error: function (response) {
            showError([response.Value], "Error cerrando el chat");
            result = false;
        }
    });

    return result;
}

function changeOptionFilter() {
    var $select_elem = $("#filtroHistorico");
    $select_elem.append('<option value="99999">Proceso Completado</option>');
    $select_elem.val('99999');
    $select_elem.trigger("change.select2");
}

function ClearCallBack(CloseModal) {
    var $FirstNamesCallBack = $('#FirstNamesCallBack');
    var $LastNamesCallBack = $('#LastNamesCallBack');
    var $PhoneType = $('#PhoneType');
    var $CountryCodeCallback = $('#CountryCodeCallback');
    var $CityCodeCallback = $('#CityCodeCallback');
    var $NumToCall = $('#NumToCall');
    var $PolicyNumCallBack = $('#PolicyNumCallBack');
    $FirstNamesCallBack.val('');
    $LastNamesCallBack.val('');
    $PhoneType.val('');
    $PhoneType.trigger("chosen:updated");

    $CountryCodeCallback.val('');
    $CityCodeCallback.val('');
    $NumToCall.val('');
    $PolicyNumCallBack.val('');

    if (CloseModal)
        $('#ppCustomerCallback').modal('hide');
}

function simulateRequestFranklin(changeUrl) {
    $.ajax({
        url: "/Login/simulateRequestFranklin",
        data: { changeUrl: changeUrl },
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.Status) {
                location.href = data.UrlPath;
            } else {
                showError([data.UrlPath]);
                return false;
            }
        }
    });
}

function SetLeyMode(changeUrl) {
    $.ajax({
        url: "/Login/SetLeyMode",
        data: { changeUrl: changeUrl },
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data.Status) {
                location.href = data.UrlPath;
            } else {
                showError([data.UrlPath]);
                return false;
            }
        }
    });
}

function GetFuelType(fueltypeid, makeid, modelid, isFlotillaMode, randomId) {

    $.ajax({
        url: "/Home/GetFuelType",
        dataType: "json",
        async: false,
        data: { FuelTypeId: fueltypeid, MakeId: makeid, ModelId: modelid },
        success: function (result) {
            var $select_elem = isFlotillaMode == true ? getHtmlElementByClass('ddlfuelType', randomId) : $("#fuelType");
            $select_elem.empty();
            $select_elem.append('<option value=""></option>');

            if (result != null) {

                $.each(result, function (idx, obj) {
                    $select_elem.append('<option value="' + obj.Value + '">' + obj.Name + '</option>');
                });
            }
            $select_elem.trigger("change.select2");
            $select_elem.trigger("change");
        },
        error: function (response) {
            showError(["ha ocurrido un error obteniendo los Tipos de Combustibles. Intente nuevamente por favor!"], "Error obteniendo Tipos Combustibles");
        }
    });
}

function ProcessResponsePaymentCardnet() {

    PaymentStatus = $("#hdnPaymentStatus").val();
    PaymentFromCardnet = $("#hdnPaymentFromCardnet").val();
    PaymentSuccess = $("#hdnPaymentSuccess").val();
    failInsentingQuotationOnSysFlexOrVO = $("#hdnfailInsentingQuotationOnSysFlexOrVO").val();
    PaymentMessage = $("#hdnPaymentMessage").val();
    PolicyNumberPayment = $("#hdnPolicyNumber").val();
    AuthorizationCode = $("#hdnAuthorizationCode").val();
    errorGPToSysflexMessage = $("#hdnerrorGPToSysflexMessage").val();
    errorGPToSysflexMessage2 = $("#hdnerrorGPToSysflexMessage2").val();
    allowOnlyLoggedUsers = $("#hdnallowOnlyLoggedUsers").val();
    AmountPayByClient = $("#hdnAmountPayByClient").val();

    var quotationNumber = $("#hdnQuotationNumber").val();

    //Cuando se hace un pago por cardnet
    if (typeof PaymentStatus != 'undefined' && PaymentFromCardnet == "True") {

        if (PaymentStatus == "True")//El pago fue aprobado por cardnet
        {
            $('#noAuthorizationCode').show();

            if (failInsentingQuotationOnSysFlexOrVO == "W") {
                $("#noPolicyMarbete").html("Ha ocurrido un error al tratar envíar la cotización a la Bandeja, pero, se realizo el pago correctamente y se genero el número de póliza en SysFlex.. No. Póliza: " + PolicyNumberPayment);
                $("#noAuthorizationCode").html("No. Autorización Pago: " + AuthorizationCode);
                $("#ppMarbete").modal({ backdrop: 'static', keyboard: false, show: true });
            }
            else if (failInsentingQuotationOnSysFlexOrVO === "GP") {
                $("#noPolicyMarbete").html(errorGPToSysflexMessage.replace("{0}", PolicyNumberPayment));
                $("#noAuthorizationCode").html("No. Autorización Pago: " + AuthorizationCode);
                $("#ppMarbete").modal({ backdrop: 'static', keyboard: false, show: true });
            }
            else if (failInsentingQuotationOnSysFlexOrVO === "GP2") {
                $("#noPolicyMarbete").html(errorGPToSysflexMessage2.replace("{0}", PolicyNumberPayment));
                $("#noAuthorizationCode").html("No. Autorización Pago: " + AuthorizationCode);
                $("#ppMarbete").modal({ backdrop: 'static', keyboard: false, show: true });
            }
            else {
                $("#noPolicyMarbete").html("No. Poliza: " + PolicyNumberPayment);
                $("#noAuthorizationCode").html("No. Autorización Pago: " + AuthorizationCode);
                $("#ppMarbete").modal({ backdrop: 'static', keyboard: false, show: true });
            }


            $("#amountPay").html("Monto Pagado: $" + number_format(AmountPayByClient, 2));

            history.replaceState({}, '', '/Home/Index');

            changeOptionFilter();

        } else {
            if (failInsentingQuotationOnSysFlexOrVO == "S") {
                showError(['El pago fue procesado con éxito, pero, ocurrió un error enviando la cotización al sistema core.'], 'Enviando a Sysflex');
            }
            else if (failInsentingQuotationOnSysFlexOrVO == "E") {
                showError(['Esta cotización ya ha sido enviada a nuestros sistemas.'], 'Pagos');
            }
            else if (failInsentingQuotationOnSysFlexOrVO == "W") {
                showError(['Ocurrió un error enviando la cotización a la bandeja, pero, se genero el número de póliza........ No. Póliza: ' + PolicyNumberPayment], 'Enviando a Sysflex');
            }
            else if (failInsentingQuotationOnSysFlexOrVO === "GP") {
                showError([errorGPToSysflexMessage.replace("{0}", PolicyNumberPayment)], 'Pagos');

            }
            else if (failInsentingQuotationOnSysFlexOrVO === "GP2") {
                showError([errorGPToSysflexMessage2.replace("{0}", PolicyNumberPayment)], 'Pagos');
            }
            else if (failInsentingQuotationOnSysFlexOrVO === "CH") {
                showError([errorGPToSysflexMessage2], 'Pagos');
            }
            else {
                showError(['No se ha podido realizar el pago de la cotización Nro <strong>' + quotationNumber + '</strong>. El proveedor de pagos ha respondido con el siguiente mensaje: <strong>' + PaymentMessage + '</strong>'], 'Pagos');
            }
        }
    }
    //    
}

function ProcessCoupon(couponCode, quotID) {
    var agentNameCode = "";

    if ($("#AgentList").val()) {
        var SelectedAgent = JSON.parse($("#AgentList").val());
        agentNameCode = SelectedAgent.NameId;
    }

    if (couponCode !== "" && quotID !== "") {

        var name = $("#FirstName").val();
        var lastName = $("#FirstSurname").val();
        var email = $("#Email").val();
        var phonenumber = $("#PhoneNumber").val();

        var DefaultCupon = $("#hdnDefaultCupon").val();

        $.ajax({
            url: "/Home/GetCoupon",
            dataType: "json",
            data: { CouponCode: couponCode, name: name, lastName: lastName, email: email, phonenumber: phonenumber, agentNameCode: agentNameCode, quotID: quotID, couponDefault: DefaultCupon },
            async: true,
            success: function (result) {

                if (result.success) {
                    $('#hdnCouponCode').val(result.CouponCode);
                    $('#hdnCuponDiscount').val(result.CouponDiscount);
                    $('#hdnProspectoID').val(result.ProspectoID);

                    if (GlobalAppMode != "LEYMODE" && GlobalAppMode != "FULLMODE") {
                        showSucess([result.message], "Código Promocional");
                    }

                    //Hago esto para que me obligue a guardar el cupon cuando haya un vehiculo
                    if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {
                        changedSex = true;
                        disableAccordionsSections(true, true);
                    }

                    //Esto es para que si escriben un cupon y hay uno seleccionado se use el del cupon introducido en vez del seleccionado
                    if (GlobalAppMode == "LEYMODE" || GlobalAppMode == "FULLMODE") {
                        if ($('#CouponCode').val() !== '') {

                            $("#contactForm").val('');
                            $("#contactForm").trigger('change');

                            var parentDiv = $("#contactForm").parent().parent();
                            parentDiv.removeClass('requerido');
                        }
                    }
                    //

                } else {
                    showError([result.message], "Código Promocional");
                    $('#hdnCouponCode').val("");
                    $('#hdnCuponDiscount').val("");
                    $('#hdnProspectoID').val("");
                    $("#CouponCode").val("");
                    $("#CouponCode").parent().removeClass("is-dirty");
                }
            },
            error: function (response) {
                showError([response.responseText], "Error");
            }
        });
    }
}

function getAppMode() {
    return $('#hdnAppMode').val();
}

function configAppMode() {
    //
    GlobalAppMode = getAppMode();
    var DefaultCupon = $("#hdnDefaultCupon").val();

    //if (DefaultCupon) {
    //    $("#CouponCode").val(DefaultCupon);
    //    $("#CouponCode").parent().addClass("is-dirty");
    //    $("#CouponCode").attr('disabled', true);
    //}

    if (GlobalAppMode == "LEYMODE") {
        $(".HideOnLeyMode").hide();
        $(".HideOnNotLeyMode").show();

        $("#spanSectionDesc").text("Información de Contacto");

        $("#PhoneNumber").parent().parent().removeClass('mdl-cell--4-col');
        $("#PhoneNumber").parent().parent().addClass('mdl-cell--3-col');

        $("#contactForm").parent().parent().parent().removeClass('mdl-cell--12-col');
        //$("#contactForm").parent().parent().parent().addClass('mdl-cell--4-col');
        $("#contactForm").parent().parent().parent().addClass('mdl-cell--3-col');

        $("#Email").parent().parent().removeClass('mdl-cell--4-col');
        $("#Email").parent().parent().addClass('mdl-cell--3-col');

        $("#CouponCode").parent().parent().removeClass('mdl-cell--4-col');
        $("#CouponCode").parent().parent().addClass('mdl-cell--3-col');

        $(".showOnFullandLeyMode").show();
    }
    else if (GlobalAppMode == "FULLMODE") {

        $(".HideOnLeyMode").show();
        $(".HideOnNotLeyMode").remove();

        $(".HideOnFullMode").hide();
        $(".HideOnNotFullMode").show();

        $("#PhoneNumber").parent().addClass('putErrorBorder');

        $(".showOnFullandLeyMode").show();

        $("#contactForm").parent().parent().parent().removeClass('mdl-cell--12-col');
        $("#contactForm").parent().parent().parent().addClass('mdl-cell--4-col');
    }
    else {

        $(".showOnFullandLeyMode").remove();

        $(".HideOnLeyMode").show();
        $(".HideOnNotLeyMode").remove();

        $(".HideOnFullMode").show();
        $(".HideOnNotFullMode").hide();

        $("#spanNumberStep2").text("2");
        $("#spanNumberStep3").text("3");

        $("#PhoneNumber").parent().removeClass('putErrorBorder');
        $(".btnSaveClientBasic").removeClass('ui-state-disabled');
        $(".btnSaveClientBasic").removeAttr('disabled');

    }
}

function getAllContactForm() {

    $.ajax({
        url: "/Home/getContactForm",
        dataType: "json",
        data: {},
        async: false,
        success: function (result) {

            var $select_elem = $("#contactForm");
            $select_elem.empty();
            $select_elem.append('<option value=""></option>');

            $.each(result.data, function (idx, obj) {
                var sp = obj.name.split('|');
                var realName = obj.name;
                var coupon = "";

                if (sp.length > 1) {

                    realName = sp[0];
                    coupon = sp[1];
                }

                $select_elem.append('<option value="' + obj.Value + '" data-coupon="' + coupon + '">' + realName + '</option>');
            });
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function getChannelAgent(agentNameCode) {

    $.ajax({
        url: "/Home/getAgentChannel",
        dataType: "json",
        data: { agentNameCode: agentNameCode },
        async: false,
        success: function (result) {

            if (result.AgentChannel == "VENTAS DIRECTAS") {

                $(".HideOnNotLeyMode").remove();

                $("#contactForm").parent().parent().removeClass("putErrorBorder").removeClass("requerido");

                getAllContactForm();

                $(".HideOnNotFullMode").removeClass('mdl-cell--12-col');
                $(".HideOnNotFullMode").addClass('mdl-cell--4-col');

                $(".HideOnNotFullMode").show();

                $(".hideCouponTxt").hide();

                GlobalAgentDirectSales = true;
            }
            else {

                $(".HideOnNotFullMode").hide();
                $(".hideCouponTxt").show();

                $("#CouponCode").val("");
                $('#hdnCouponCode').val("");
                $("#hdnDefaultCupon").val("");
                $('#hdnCuponDiscount').val("");
                $('#hdnProspectoID').val("");
                $("#CouponCode").val("");
                $("#CouponCode").parent().removeClass("is-dirty");

                GlobalAgentDirectSales = false;
            }
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function SendVerificationCode() {

    var PhoneNumber = $("#PhoneNumber").val();

    if (PhoneNumber === '') {
        showError(['El Número de Celular no debe estar vacío.']);
        return false;
    }

    $.ajax({
        url: "/Home/SendVerificationCode",
        dataType: "json",
        data: { PhoneNumber: PhoneNumber },
        async: false,
        success: function (result) {

            if (result.success) {
                showInfo([result.message]);
                $("#hdnCurrentPhoneNumber").val(PhoneNumber);

                $("#btnSendVerCode").addClass('ui-state-disabled');
                $("#btnSendVerCode").attr('disabled', 'disabled');

                return true;
            }
            else {
                showError([result.message]);

                $("#btnSendVerCode").removeClass('ui-state-disabled');
                $("#btnSendVerCode").removeAttr('disabled');
                return false;
            }
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function ValidateCode() {

    var PhoneNumber = $("#PhoneNumber").val();
    var Code = $("#VerificationCode").val();

    if (PhoneNumber === '') {
        return false;
    }

    if (Code === '') {
        $(".btnSaveClientBasic").addClass('ui-state-disabled');
        $(".btnSaveClientBasic").attr('disabled', 'disabled');

        $("#btnSendVerCode").removeClass('ui-state-disabled');
        $("#btnSendVerCode").removeAttr('disabled');

        GlobalEventFired = false;
        return false;
    }

    Code = Code.trim();

    $.ajax({
        url: "/Home/ValidateCode",
        dataType: "json",
        data: { PhoneNumber: PhoneNumber, VerificationCode: Code },
        async: false,
        success: function (result) {

            if (result.success) {
                //le permito continuar
                showSucess([result.message], null, function () { $(".btnSaveClientBasic").trigger('click'); });

                $(".btnSaveClientBasic").removeClass('ui-state-disabled');
                $(".btnSaveClientBasic").removeAttr('disabled');

                $("#btnSendVerCode").addClass('ui-state-disabled');
                $("#btnSendVerCode").attr('disabled', 'disabled');

                GlobalEventFired = true;
            }
            else {

                $(".btnSaveClientBasic").addClass('ui-state-disabled');
                $(".btnSaveClientBasic").attr('disabled', 'disabled');

                $("#btnSendVerCode").removeClass('ui-state-disabled');
                $("#btnSendVerCode").removeAttr('disabled');

                disableAccordionsSections(true, true);

                showError([result.message]);

                GlobalEventFired = false;

                return false;
            }
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function GetCode() {

    var PhoneNumber = $("#PhoneNumber").val();

    $.ajax({
        url: "/Home/GetCode",
        dataType: "json",
        data: { PhoneNumber: PhoneNumber/*, VerificationCode: Code */ },
        async: false,
        success: function (result) {

            if (result.success) {
                $("#btnSendVerCode").addClass('ui-state-disabled');
                $("#btnSendVerCode").attr('disabled', 'disabled');

                $(".btnSaveClientBasic").removeClass('ui-state-disabled');
                $(".btnSaveClientBasic").removeAttr('disabled');

                $("#VerificationCode").val(result.code);
                $("#VerificationCode").parent().addClass('is-dirty');

            }
            else {
                if (PhoneNumber == '') {

                    $(".btnSaveClientBasic").addClass('ui-state-disabled');
                    $(".btnSaveClientBasic").attr('disabled', 'disabled');
                }

                $("#btnSendVerCode").removeClass('ui-state-disabled');
                $("#btnSendVerCode").removeAttr('disabled');

                $("#VerificationCode").val('');
            }
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function getTotalDiscountByVHCoupon(TotalPrime) {

    var discount = 0;

    if ($("#hdnCuponDiscount").val() !== '') {
        var percentajeDiscountCoupon = (parseInt($("#hdnCuponDiscount").val()) / 100);

        discount = (TotalPrime * percentajeDiscountCoupon);
    }

    return discount;
}

function getTotalDiscountByVHFlotilla(TotalPrime) {

    var discount = 0;
    var totalVehicles = countTotalVehicles();

    $.ajax({
        url: '/Home/GetPercentByQtyVehicle',
        type: 'POST',
        dataType: 'json',
        data: { qtyVehicles: totalVehicles },
        async: false,
        success: function (data) {

            if (data > 0) {

                var percentajeDiscountFlotilla = (parseInt(data) / 100);

                discount = (TotalPrime * percentajeDiscountFlotilla);
            }
        }
    });

    return discount;
}

function countTotalVehicles() {

    var t = 0;

    var totalQtyByVehicle = 0;

    //contando la cantidad de cada Vehículo
    var q = $(".qtyVehicles");

    $.each(q, function (idx, obj) {

        var valu = $(obj).val();

        if (valu !== '') {
            t += parseInt(valu);
        }
    });

    return t;
}

function SaveDataVehicleOnlyOne(vehicleRamdonId) {

    if ($("#hdnInvalidUsage").val() == "S") {
        showError(['No puede continuar porque tiene seleccionado un Uso Principal invalido.'], 'Uso Principal');
        return false;
    }

    if ($("#hdnChangedSomethingClient").val() == "S") {
        showError(['No puede continuar porque tiene que guardar los cambios realizados al Conductor Principal.'], 'Conductor Principal');
        return false;
    }

    if (isEditingVehicle) {
        showError(['No puede continuar porque tiene que guardar los cambios realizados al Vehículo.'], 'Cambios Vehículo');
        return false;
    }

    var entro = false;
    var incompleted = false;
    var newAllVehicleDataToSave = null;

    if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {

        $.each(AllVehicleDataToSave, function (i, v) {

            if (v.randomId == vehicleRamdonId) {


                if (!entro && GlobalAppMode == "LEYMODE") {
                    var current = getAllDataVehicleByRandomID(v.randomId);
                    var realVehicle = setVehicle(current, v.randomId);
                    setAllVehicleDataToSave(realVehicle, "UPDATE");
                    entro = true;
                }

                if (v.iscompletedVehicle == false) {
                    incompleted = true;
                }
            }
        });

        if (incompleted) {
            showError(['Debe completar todos los datos del/los Vehículo(s).'], 'Completar Vehículo(s)');
            return false;
        }


        newAllVehicleDataToSave = AllVehicleDataToSave.filter(function (v) {
            return v.randomId == vehicleRamdonId;
        });

        if (newAllVehicleDataToSave != null && newAllVehicleDataToSave.length > 0) {

            var objVehicle = JSON.stringify(newAllVehicleDataToSave);



            $.ajax({
                url: "/Home/SaveDataVehicle",
                type: "POST",
                data: { jsondata: objVehicle, requoting: false, saveOnlyVh: "S" },
                async: false,
                success: function (data, textStatus, jqXHR) {

                    if (data.messageError) {
                        showError([data.messageError], 'Error guardando la Cotización');
                        pass = false;
                    }
                    else {

                        var datav = JSON.parse(data.VehicleDataMatch);

                        $.each(datav, function (i, item) {

                            var newVehicle = getAllDataVehicleByRandomID(item.randomId);

                            if (newVehicle != null || newVehicle != undefined) {

                                newVehicle.Id = item.vehicleID;

                                var realVehicle = setVehicle(newVehicle);

                                setAllVehicleDataToSave(realVehicle, "UPDATE");

                                GlobalChangeForModeLey = false;
                            }
                        });

                        isEventAdded = false;
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
    } else {
        showError(['Para poder continuar debe agregar al menos 1 Vehículo.'], 'Debe agregar un Vehículo');
    }
}

function AllowSaveOnlyVehicle() {

    $.ajax({
        url: "/Home/AllowSaveOnlyVehicle",
        dataType: "json",
        data: {},
        async: false,
        success: function (result) {
            GlobalSaveVehicleOnly = result.allow;
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function areCompletedAllVehicles() {

    var completed = true;

    if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {

        $.each(AllVehicleDataToSave, function (i, v) {
            if (v.iscompletedVehicle == false) {
                completed = false;
            }
        });

    }

    return completed;
}

function getPercentDiscountFlotilla(totalQtyVehicles) {

    var discountPercentage = 0;

    $.ajax({
        url: '/Home/GetPercentByQtyVehicle',
        type: 'POST',
        dataType: 'json',
        data: { qtyVehicles: totalQtyVehicles },
        async: false,
        success: function (data) {
            discountPercentage = data;
        }
    });

    return discountPercentage;
}

function refreshDiscountSectionAllVehicle() {


    var totalVehicles = countTotalVehicles();
    var pecentageFlotila = getPercentDiscountFlotilla(totalVehicles);

    if (AllVehicleDataToSave != null && AllVehicleDataToSave.length > 0) {

        $.each(AllVehicleDataToSave, function (i, v) {
            var rID = v.randomId;

            var TotalPrime = 0;
            var TotalPrimeStr = "";
            var iscPercentage = parseFloat(GlobalCurrentIsc);
            var TotalIsc = 0;
            var totalMinusDiscount = 0;
            var totalPrimePlusIsc = 0;

            var $elem = $(".totalPrime");

            if ($elem.length > 1) {

                $.each($elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == rID) {
                        $elem = $(obj);
                        TotalPrimeStr = number_format($elem.html(), 2);
                    }
                });
            } else {
                TotalPrimeStr = number_format($elem.html(), 2);
            }



            TotalPrimeStr = TotalPrimeStr.split(",").join("");

            TotalPrime = parseFloat(TotalPrimeStr).toFixed(2);

            TotalPrime = parseFloat(TotalPrime);//El toFixed(2) lo convierte en un string, entonces lo vuelvo a convertir para que sea un numero.

            TotalIsc = (TotalPrime * (iscPercentage / 100));;

            totalPrimePlusIsc = (TotalPrime + TotalIsc);

            var percentajeDiscountFlotilla = (parseInt(pecentageFlotila) / 100);

            var totalAllDiscountsByVH = (TotalPrime * percentajeDiscountFlotilla);//Descuento Flotilla

            var totalMinusFlotillaDiscount = (TotalPrime - totalAllDiscountsByVH);

            totalAllDiscountsByVH += getTotalDiscountByVHCoupon(totalMinusFlotillaDiscount);//Descuento Cupon

            var $elem = $(".totalAllDiscount");

            if ($elem.length > 1) {
                $.each($elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == rID) {
                        $elem = $(obj);
                        $elem.html("$" + number_format(totalAllDiscountsByVH, 2));
                    }
                });
            } else {
                $elem.html("$" + number_format(totalAllDiscountsByVH, 2));
            }

            if (totalAllDiscountsByVH > 0) {

                totalMinusDiscount = (TotalPrime - totalAllDiscountsByVH);

                TotalIsc = (totalMinusDiscount * (iscPercentage / 100));

                totalPrimePlusIsc = totalMinusDiscount + TotalIsc;
            }

            var $elem = $(".ISC");

            if ($elem.length > 1) {
                $.each($elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == rID) {
                        $elem = $(obj);
                        $elem.html("$" + number_format(TotalIsc, 2));
                    }
                });
            } else {
                $elem.html("$" + number_format(TotalIsc, 2));
            }

            var $elem = $(".totalToPay");

            if ($elem.length > 1) {
                $.each($elem, function (idx, obj) {
                    var randomid = $(obj).attr("data-vehiclerandomid");
                    if (randomid == rID) {
                        $elem = $(obj);
                        $elem.html("$" + number_format(totalPrimePlusIsc, 2));
                    }
                });
            } else {
                $elem.html("$" + number_format(totalPrimePlusIsc, 2));
            }
        });
    }

}

function getClientIp() {

    var Ip = "";

    $.ajax({
        url: "https://api.ipify.org/?format=json",
        dataType: "json",
        async: false,
        data: {},
        success: function (data) {
            if (data) {
                Ip = data.ip;
                return Ip;
            }
        },
        error: function (response) {
            console.log("Error Call");
            console.log(response.responseText);
            console.log(response);
        }
    });

    return Ip;
}

function getClientIpInfo(mParam) {

    var clientip = getClientIp();
    var m = mParam;

    $.ajax({
        url: "/Home/getClientIpInfo",
        dataType: "json",
        async: false,
        data: { ip: clientip, m: m },
        success: function (data) {

            if (!data.success) {
                console.log(data.message);
            }
            else if (data.success) {
                if (data.medio > 0) {
                    $("#contactForm").val(data.medio);
                    $("#contactForm").trigger('change');
                }
            }
        },
        error: function (response) {
            console.log("Error Call");
            console.log(response.responseText);
            console.log(response);
        }
    });
}

function getQuotationReferredInfo(quotationId) {

    $.ajax({
        url: "/Home/GetQuotationReferredInfo",
        dataType: "json",
        data: { quotationId: quotationId },
        async: false,
        success: function (result) {

            if (result.data) {
                $('#hdnReferredById').val(result.data.ReferredId);
                $('#referredByName').val(result.data.ReferredName);
                $('#referredByIdentificationNumber').val(result.data.ReferredIdentificationNumber);
                $('#referredByPhoneNumber').val(result.data.ReferredPhone);
                $('#referredByEmail').val(result.data.ReferredEmail);
                $('#referredByPolicy').val(result.data.ReferredPolicy);

                $('#referredByName').parent().addClass("is-dirty");
                $('#referredByIdentificationNumber').parent().addClass("is-dirty");
                $('#referredByPhoneNumber').parent().addClass("is-dirty");
                $('#referredByEmail').parent().addClass("is-dirty");
                $('#referredByPolicy').parent().addClass("is-dirty");

            } else {
                cleanReferredInfo();
            }
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function getReferredInfoByPolicy(policy) {

    $.ajax({
        url: "/Home/GetReferredInfoByPolicy",
        dataType: "json",
        data: { policy: policy },
        async: false,
        success: function (result) {

            if (result.data) {

                $('#referredByName').val(result.data.FirstName);
                $('#referredByIdentificationNumber').val(result.data.IdentificationNumber);
                $('#referredByPhoneNumber').val(result.data.PhoneNumber);
                $('#referredByEmail').val(result.data.Email);

                $('#referredByName').parent().addClass("is-dirty");
                $('#referredByIdentificationNumber').parent().addClass("is-dirty");
                $('#referredByPhoneNumber').parent().addClass("is-dirty");
                $('#referredByEmail').parent().addClass("is-dirty");

            } else {

                showError(['Póliza no encontrada']);

                cleanReferredInfo(false);
            }
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}

function cleanReferredInfo(nopolicy = true) {

    $('#hdnReferredById').val('');
    $('#referredByName').val('');
    $('#referredByIdentificationNumber').val('');
    $('#referredByPhoneNumber').val('');
    $('#referredByEmail').val('');

    if (nopolicy) {
        $('#referredByPolicy').val('');
        $('#referredByPolicy').parent().removeClass("is-dirty");
    }

    $('#referredByName').parent().removeClass("is-dirty");
    $('#referredByIdentificationNumber').parent().removeClass("is-dirty");
    $('#referredByPhoneNumber').parent().removeClass("is-dirty");
    $('#referredByEmail').parent().removeClass("is-dirty");

}

function setFlotillaMode(flotillaMode) {

    $.ajax({
        url: "/Home/SetFlotillaMode",
        async: false,
        data: { _flotillaMode: flotillaMode },
        success: function (result) {
        },
        error: function (response) {
            showError([response.responseText], "Error");
        }
    });
}


function getExistAllDeclarativa() {

    if (AllVehicleDataToSave !== null && AllVehicleDataToSave.length > 1) {

        var existDecla = AllVehicleDataToSave.filter(function (item) {
            return item.SelectedProductName == "DECLARATIVA";
        });

        var isDecla = (existDecla.length > 0);

        var noExistDecla = AllVehicleDataToSave.filter(function (item) {
            return item.SelectedProductName !== "DECLARATIVA";
        });

        var noDecla = (noExistDecla.length > 0);


        if (isDecla && noDecla) {
            //existe algun vehiculo que no es declarativa
            return false;
        }
        else if (isDecla && noDecla == false) {
            //todos son declarativa
            return true;
        }
    }
    return true;
}

//-------------------------------------NEW FLOTILLA PROCESS-------------------------------------

function changesEvents() {

    //$('.ddlMakes').off('change');
    $('.ddlMakes').change(function () { makesChange(this) });

    $('.ddlModels').change(function () { modelChange(this) });

    $('.ddlYears').change(function () { yearChange(this) });

    $(".ddlVehicleType").change(function () { vehicleTypesChange(this) });

    $(".ddlUsages").change(function () { usageChange(this) });

    $(".ddlStoreCar").change(function () { storeCarChange(this) });

    $(".ddlPlan").change(function () { planChange(this) });

    $(".vehiclePrice_Flot").focusout(function () { vehiclePriceEvents(this) });

    $(".ddlCoverages").change(function () { coveragesChange(this) });

    $(".ddlDeducible").change(function () { deducibleChange(this) });

    $(".ddlSurchargePercent").change(function () { surchargeChange(this) });

    $(".servicesPopUp_Flot").click(function () { servicePopUpFlotClick(this) });

    $(".qtyVehicles_Flot").focusout(function () { vehicleQtyEvents(this) });

    isEventAdded = true;
}

function getVehicleModels_Flot(brandID, randomID) {

    $.ajax({
        url: "/Home/getVehiclesModelsByBrands",
        dataType: 'json',
        async: false,
        cache: false,
        data: {
            BrandID: brandID
        },
        success: function (json) {

            var $select_elem = getHtmlElementByClass("ddlModels", randomID);
            $select_elem.empty();
            $select_elem.append('<option value=""></option>');

            $.each(json, function (idx, obj) {
                $select_elem.append('<option value="' + obj.Id + '">' + obj.Name + '</option>');
            });
            $select_elem.trigger("change.select2");
        }
    });
}

function getHtmlElementByClass(dropClass, randomID) {

    var $select_elem = $("." + dropClass);

    if ($select_elem.length > 1) {

        $.each($select_elem, function (idx, obj) {

            var randomid = $(obj).attr("data-vehiclerandomid");

            if (randomid == randomID) {
                $select_elem = $(obj);
            }
        });
    }

    return $select_elem;
}

function CollectData_Flot(randomId) {

    var currentVehicle = getAllDataVehicleByRandomID(randomId);
    var SecuenciaVehicleSysflex = 1;

    if (currentVehicle) {
        var qtyVehicles = 1;

        var $qtyVehicles_elem = getHtmlElementByClass('qtyVehicles_Flot', randomId);
        qtyVehicles = $qtyVehicles_elem.val();

        var usage = getHtmlElementByClass('ddlUsages', randomId); //$("#ddlUsages").val();
        var selectedVehicleType = getHtmlElementByClass('ddlVehicleType', randomId);//$("#VehicleType").val();

        var brand = getHtmlElementByClass('ddlMakes', randomId);//$("#VehicleModel_Make_Id").val();
        var model = getHtmlElementByClass('ddlModels', randomId);//$("#VehicleModel_Model_Id").val();
        var year = getHtmlElementByClass('ddlYears', randomId); //$("#yearsAvaibles").val();
        var vehiclePrice = getHtmlElementByClass('vehiclePrice_Flot', randomId); //parseFloat($("#vehiclePrice").val().replace(/,/g, ''));
        var selectedCoverage = getHtmlElementByClass('ddlCoverages', randomId); //$("#Coverages").val();
        var VehicleDriver = $("#driver").val(); //$("#VehicleDriver_Id").val();
        var VehicleYearsOld = getHtmlElementByClass('ddlVehicleYearsOld', randomId); //$("#VehicleYearsOld").val();

        var VehicleFuelTypeId = getHtmlElementByClass('ddlfuelType', randomId);//$("#fuelType").val();    
        var VehicleFuelTypeDesc = VehicleFuelTypeId.find("option:selected").text(); //$("#fuelType option:selected").text();

        var makeName = brand.find("option:selected").text(); //$("#VehicleModel_Make_Id option:selected").text();
        var modelName = model.find("option:selected").text();//$("#VehicleModel_Model_Id option:selected").text();
        var store = getHtmlElementByClass('ddlStoreCar', randomId);//$("#StoreCar").val();
        var StoreName = store.find("option:selected").text();// $("#StoreCar option:selected").text();
        var VehicleQuantity = LoadVehicle ? LoadVehicleQuantity : qtyVehicles;

        hasServices = false;

        //currentVehicle.randomId = vehicleRandomID;
        currentVehicle.Id = LoadVehicle ? LoadVehicleCurrentID : currentVehicle.Id;
        currentVehicle.SecuenciaVehicleSysflex = LoadVehicle ? LoadVehicleCurrentSecuenciaVehicleSysflex : currentVehicle.SecuenciaVehicleSysflex;

        currentVehicle.Year = year.val();
        currentVehicle.VehiclePrice = parseFloat(vehiclePrice.val().replace(/,/g, ''));
        currentVehicle.SelectedVehicleTypeId = selectedVehicleType.val();
        currentVehicle.SelectedVehicleTypeName = selectedVehicleType.val();
        currentVehicle.UsageId = usage.val();
        currentVehicle.VehicleModel_Make_Id = brand.val();
        currentVehicle.VehicleMakeName = makeName;
        currentVehicle.VehicleModel_Model_Id = model.val();
        currentVehicle.VehicleModelName = modelName;
        currentVehicle.VehicleDescription = makeName + " " + modelName;
        currentVehicle.StoreId = store.val();
        currentVehicle.StoreName = StoreName;
        currentVehicle.Driver_Id = VehicleDriver;
        currentVehicle.VehicleYearOld = VehicleYearsOld.val();
        currentVehicle.VehicleQuantity = VehicleQuantity;

        currentVehicle.SelectedVehicleFuelTypeId = VehicleFuelTypeId.val();
        currentVehicle.SelectedVehicleFuelTypeDesc = VehicleFuelTypeDesc;

        currentVehicle.GlobalDataUsages = GlobalDataUsages;
        currentVehicle.GlobalData = GlobalData;
        currentVehicle.GlobalDataProductsByUsage = GlobalDataProductsByUsage;
        currentVehicle.GlobalAllProducts = GlobalAllProducts;

        var realVehicle = setVehicle(currentVehicle, randomId);

        setAllVehicleDataToSave(realVehicle, "UPDATE");
    }
    else {

        var newVehicle = null;

        if (AllVehicleDataToSave != null && LoadVehicle == false) {

            newVehicle = altFind(AllVehicleDataToSave, function (item) {
                return item.randomId == vehicleRandomID;
            });

            var arr = AllVehicleDataToSave.sort(function (a, b) { return (a.SecuenciaVehicleSysflex > b.SecuenciaVehicleSysflex) ? 1 : ((b.SecuenciaVehicleSysflex > a.SecuenciaVehicleSysflex) ? -1 : 0); })
            var lastVeh = arr[arr.length - 1];

            if (lastVeh) {
                SecuenciaVehicleSysflex = (lastVeh.SecuenciaVehicleSysflex + 1);
            }
        }

        if (newVehicle == null || newVehicle == undefined) {

            hasServices = false;

            var newVehicle = {};
            newVehicle.randomId = vehicleRandomID;
            newVehicle.Id = LoadVehicle ? LoadVehicleCurrentID : null;
            newVehicle.SecuenciaVehicleSysflex = LoadVehicle ? LoadVehicleCurrentSecuenciaVehicleSysflex : SecuenciaVehicleSysflex;

            newVehicle.GlobalDataUsages = GlobalDataUsages;
            newVehicle.GlobalData = GlobalData;
            newVehicle.GlobalDataProductsByUsage = GlobalDataProductsByUsage;
            newVehicle.GlobalAllProducts = GlobalAllProducts;

            var realVehicle = setVehicle(newVehicle);

            setAllVehicleDataToSave(realVehicle);
        }
    }
}

function assingRandomId() {

    var tbl = $("#tblAllVehicle");
    var $Tbody = tbl.find("tbody");
    var $trFirst = $Tbody.find("tr:first");
    $trFirst.attr('data-vehiclerandomid', vehicleRandomID);

    $trFirst.find('select').each(function () {
        var $s = $(this);
        $s.attr('data-vehiclerandomid', vehicleRandomID);
    });

    $trFirst.find('input').each(function () {
        var $i = $(this);
        $i.attr('data-vehiclerandomid', vehicleRandomID);
    });

    $trFirst.find('button').each(function () {
        var $i = $(this);
        $i.attr('data-vehiclerandomid', vehicleRandomID);
    });

    $trFirst.find('span').each(function () {
        var $i = $(this);
        $i.attr('data-vehiclerandomid', vehicleRandomID);
    });
}

function generateNewRow(newTr) {
    var newInput = "";
    var newButton = "";
    var newSpan = "";

    //Make
    var newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    var newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    var newSelect = $('<select>').attr('id', "ddlMakes_" + vehicleRandomID).addClass("isSelect2 form-control ddlMakes").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Marca");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Model
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlModels_" + vehicleRandomID).addClass("isSelect2 form-control ddlModels").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Modelo");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Year
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlYears_" + vehicleRandomID).addClass("isSelect2 form-control ddlYears").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Año");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //VehicleType
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlVehicleType_" + vehicleRandomID).addClass("isSelect2 form-control ddlVehicleType").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Tipo Vehículo");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //FuelType
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlfuelType_" + vehicleRandomID).addClass("isSelect2 form-control ddlfuelType").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Tipo Combustible");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //New
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlVehicleYearsOld_" + vehicleRandomID).addClass("isSelect2 form-control ddlVehicleYearsOld").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Nuevo");
    newSelect.append($('<option>', { value: '', text: '' }));
    newSelect.append($('<option>', { value: '0 Km', text: 'Sí' }));
    newSelect.append($('<option>', { value: 'Usado', text: 'No' }));
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Usage
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlUsages_" + vehicleRandomID).addClass("isSelect2 form-control ddlUsages").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Uso");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //StoreCar
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlStoreCar_" + vehicleRandomID).addClass("isSelect2 form-control ddlStoreCar").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Estacionamiento");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //vehiclePrice
    newTd = $('<td>');
    newDiv = $('<div>');
    newInput = $('<input>').attr('id', "vehiclePrice_Flot_" + vehicleRandomID).addClass("mdl-textfield__input vehiclePrice_Flot").attr('data-vehiclerandomid', vehicleRandomID).attr('value', 0);

    if (GlobalIsMobile) {
        newInput.inputmask('remove');
    } else {
        newInput.inputmask({ 'alias': 'numeric', 'groupSeparator': ',', 'autoGroup': true, 'digits': 2, 'digitsOptional': false, 'prefix': '', 'positionCaretOnTab': false });
    }
    newDiv.append(newInput);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Plan
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlPlan_" + vehicleRandomID).addClass("isSelect2 form-control ddlPlan").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Plan");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Coverages
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlCoverages_" + vehicleRandomID).addClass("isSelect2 form-control ddlCoverages").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Cobertura");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Deducible
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>').addClass("mdl-textfield mdl-js-textfield mdl-textfield--floating-label putErrorBorderFlot requerido");
    newSelect = $('<select>').attr('id', "ddlDeducible_" + vehicleRandomID).addClass("isSelect2 form-control ddlDeducible").attr('data-vehiclerandomid', vehicleRandomID).attr('data-placeholder', "Deducible");
    newDiv.append(newSelect);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Servicios
    newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
    newDiv = $('<div>');
    newButton = $('<button>').attr('id', "servicesPopUp_" + vehicleRandomID).addClass("mdl-button mdl-js-button mdl-button--colored bg-success mdl-color-text--white servicesPopUp_Flot").attr('data-vehiclerandomid', vehicleRandomID);
    newButton.append('<i class="material-icons">add</i>');
    newButton.attr('title', 'Servicios Adicionales')
    newButton.attr('type', 'button');
    newDiv.append(newButton);
    newTd.append(newDiv);
    newTr.append(newTd);

    //SurchargePercent
    if ($(".ddlSurchargePercent").length > 0) {

        newTd = $('<td>').addClass('mdl-cell mdl-cell--3-col mdl-cell--4-col-tablet mdl-cell--12-col-phone');
        newDiv = $('<div>');//.css('width', '80px !important');
        newSelect = $('<select>').attr('id', "ddlSurchargePercent_" + vehicleRandomID).addClass("isSelect2 form-control ddlSurchargePercent").attr('data-vehiclerandomid', vehicleRandomID)
            .attr('data-placeholder', "Recargos");
        newDiv.append(newSelect);
        newTd.append(newDiv);
        newTr.append(newTd);
    }

    //qty Vehicles
    newTd = $('<td>');
    newDiv = $('<div>');
    newInput = $('<input>').attr('id', "qtyVehicles_Flot_" + vehicleRandomID).addClass("mdl-textfield__input qtyVehicles_Flot").attr('data-vehiclerandomid', vehicleRandomID).attr('value', '1');
    newDiv.append(newInput);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Prime
    newTd = $('<td>');
    newDiv = $('<div>');
    newSpan = $('<span>').attr('id', "spTotalPrime_" + vehicleRandomID).addClass("spTotalPrime").attr('data-vehiclerandomid', vehicleRandomID);
    newSpan.text('$0.00');
    newDiv.append(newSpan);
    newTd.append(newDiv);
    newTr.append(newTd);

    //Action
    newTd = $('<td>');
    newDiv = $('<div>');
    newButton = $('<button>').attr('id', "btnNextVehicle_" + vehicleRandomID)
        .addClass("btnNextVehicle btn ec_btn btn-success btn-sm mr-2")
        .attr('data-vehiclerandomid', vehicleRandomID)
        .attr('title', 'Siguiente Vehículo')
        .attr('type', 'button');
    newButton.append('<i class="material-icons">add</i>');
    newTd.append(newButton);

    newButton = $('<button>').attr('id', "btnDeleteVehicle_" + vehicleRandomID)
        .addClass("btn ec_btn btn-danger btn-sm btnDeleteVehicle")
        .attr('data-vehiclerandomid', vehicleRandomID)
        .attr('title', 'Borrar Vehículo')
        .attr('type', 'button');
    newButton.append('<i class="material-icons">&#xE5CD;</i>');
    newTd.append(newButton);

    newTr.append(newTd);

    return newTr;
}

function generateDinamycEvents() {

    $(".isSelect2").select2({
        theme: "bootstrap",
        allowClear: true,
        minimumResultsForSearch: 1,
        width: "95%",
        language: {
            noResults: function (params) {
                return "No se han encontrado resultados.";
            }
        }
    });

    //duplicando marcas
    var $select_elem = $("#ddlMakes_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');

    var allMakes = $("#ddlMakesHidden option");
    $.each(allMakes, function (idx, obj) {
        $select_elem.append("<option value='" + obj.value + "'>" + obj.text + "</option>");
    });
    $select_elem.trigger("change.select2");
    $select_elem.change(function () { makesChange(this) });
    //

    //Agregando evento a las modelos
    $select_elem = $("#ddlModels_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');
    $select_elem.change(function () { modelChange(this) });
    //

    //Agregando evento a las anos
    $select_elem = $("#ddlYears_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');

    var allYears = $("#ddlYearsHidden option");
    $.each(allYears, function (idx, obj) {
        $select_elem.append("<option value='" + obj.value + "'>" + obj.text + "</option>");
    });
    $select_elem.trigger("change.select2");
    $select_elem.change(function () { yearChange(this) });
    //

    //Agregando evento a los tipos de vehiculos
    $select_elem = $("#ddlVehicleType_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');
    $select_elem.change(function () { vehicleTypesChange(this) });
    //

    //Agregando evento a los usos
    $select_elem = $("#ddlUsages_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');
    $select_elem.change(function () { usageChange(this) });
    //

    //Agregando evento al estacionamiento
    $select_elem = $("#ddlStoreCar_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');

    var allStore = $("#ddlStoreCarHidden option");
    $.each(allStore, function (idx, obj) {
        $select_elem.append("<option value='" + obj.value + "'>" + obj.text + "</option>");
    });
    $select_elem.trigger("change.select2");
    $select_elem.change(function () { storeCarChange(this) });
    //

    //Agregando evento a los planes
    $select_elem = $("#ddlPlan_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');
    $select_elem.change(function () { planChange(this) });
    //

    //Agregando evento a las coberturas
    $select_elem = $("#ddlCoverages_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');
    $select_elem.change(function () { coveragesChange(this) });
    //

    //Agregando evento a las coberturas
    $select_elem = $("#ddlDeducible_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value=""></option>');
    $select_elem.change(function () { deducibleChange(this) });
    //

    //Agregando evento a los recargos    
    $select_elem = $("#ddlSurchargePercent_" + vehicleRandomID);
    $select_elem.empty();
    $select_elem.append('<option value="">Seleccione</option>');

    var allSurcharge = $("#ddlSurchargePercentHidden option");
    $.each(allSurcharge, function (idx, obj) {
        $select_elem.append("<option value='" + obj.value + "'>" + obj.text + "</option>");
    });
    $select_elem.trigger("change.select2");
    $select_elem.change(function () { surchargeChange(this) });
    //

    //Agregando evento a el precio del vehiculo
    $("#vehiclePrice_Flot_" + vehicleRandomID).focusout(function () { vehiclePriceEvents(this) });
    //

    //Agregando evento a el boton de los servicios
    $("#servicesPopUp_" + vehicleRandomID).click(function () { servicePopUpFlotClick(this) });
    //

    //Agregando evento a el txt de la cantidad vehiculos
    $("#qtyVehicles_Flot_" + vehicleRandomID).focusout(function () { vehicleQtyEvents(this) });
    //
}

function makesChange(obj) {

    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');

    if ($this.val() !== '') {
        getVehicleModels_Flot($this.val(), randomID);
    }
}

function modelChange(obj) {
    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');

    var modelIdActual = $this.val();
    var brand = getHtmlElementByClass('ddlMakes', randomID).val();
    var year = getHtmlElementByClass('ddlYears', randomID).val();

    GetProductsFromSysflex(brand, modelIdActual, year, true, randomID);

    if (modelIdActual) {
        GetFuelType(null, brand, modelIdActual, true, randomID);
    }
}

function yearChange(obj) {

    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');

    var year = $this.val();
    var brand = getHtmlElementByClass('ddlMakes', randomID).val();
    var model = getHtmlElementByClass('ddlModels', randomID).val();

    GlobalDataProductLimits = null;

    //llamar metodo que me trae los productos de sysflex
    GetProductsFromSysflex(brand, model, year, true, randomID);
    enableVehicleOldCombo(year, true, randomID);
}

function vehicleTypesChange(obj) {
    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');

    var selectedVehicleType = $this.val();

    UsagesListByVehicleType(selectedVehicleType, true, randomID);
}

function usageChange(obj) {

    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');

    var usageSelected = $this.val();

    //$("#AddVehicle").removeAttr('disabled');
    $("#hdnInvalidUsage").val("N");

    var usage = GlobalDataUsages.filter(function (item) {
        return item.idUso == usageSelected;
    });

    if (usage.length > 0) {
        var UsageFound = usage[0];

        var allowed = UsageFound.allowed;
        var message = UsageFound.message;

        if (allowed == 2) {
            //mensaje error
            showError([message], 'Uso Principal');

            //$("#AddVehicle").attr('disabled', 'disabled');

            $("#hdnInvalidUsage").val("S");
            return;
        }
        else if (allowed == 3) {
            //mensaje advertencia
            showWarning([message], 'Uso Principal');
            return;
        }

        ProductListByUsages(usageSelected, true, randomID);

        if (GlobalDataProductsByUsage.length > 0) {
            CollectData_Flot(randomID);
        }

        updateGlobalDataProductLimits(randomID, null, true);
    }

    if ($this.val() === "") {
        var vehicle = getAllDataVehicleByRandomID(randomID);

        if (vehicle) {
            vehicle.iscompletedVehicle = false;
            var realVehicle = setVehicle(vehicle, randomID);
            setAllVehicleDataToSave(realVehicle, "UPDATE");
        }
    }
}

function storeCarChange(obj) {
    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');

    //Recotizar
    if ($this.val() !== '') {
        getRatesFlotMode(randomID);
    }
}

function planChange(obj) {

    var $this = $(obj);
    var i = $this.attr('id');
    var selectedProductName = $("#" + i + " option:selected").text();
    var randomID = $this.attr("data-vehiclerandomid");

    if (ApplyToZero(randomID, true) == false && ($this.val() !== "")) {
        SetFieldsApplyToZero_Flot(false, randomID);
        return false;
    } else {
        SetFieldsApplyToZero_Flot(true, randomID);
    }

    if ($this.val() === "") {
        var vehicle = getAllDataVehicleByRandomID(randomID);
        vehicle.iscompletedVehicle = false;
        var realVehicle = setVehicle(vehicle, randomID);
        setAllVehicleDataToSave(realVehicle, "UPDATE");
    }

    currentCoveragesByUsage(selectedProductName, randomID, true);
}

function coveragesChange(obj) {

    var $this = $(obj);
    var randomID = $this.attr("data-vehiclerandomid");

    updateGlobalDataProductLimits(randomID, $this.val(), true);

    showRechargeAllLawProducts($this.val(), randomID, true);

    var $elem = getHtmlElementByClass('servicesPopUp_Flot', randomID);

    if ($this.val() !== '' && hasServices) {
        $elem.removeAttr('disabled');
    } else {
        $elem.attr('disabled', 'disabled');
    }

    if ($this.val() === "") {
        var vehicle = getAllDataVehicleByRandomID(randomID);
        vehicle.iscompletedVehicle = false;
        var realVehicle = setVehicle(vehicle, randomID);
        setAllVehicleDataToSave(realVehicle, "UPDATE");
    }

    var parentDiv = $this.parent();
    if ($this.val() == "") {
        if (parentDiv.hasClass('erarequerido')) {
            parentDiv.addClass('requerido');
            parentDiv.removeClass('erarequerido');
        }
    } else {
        parentDiv.removeClass('requerido');
        parentDiv.addClass('erarequerido');
    }
}

function deducibleChange(obj) {

    var $this = $(obj);
    var randomID = $this.attr("data-vehiclerandomid");
    var parentDiv = $this.parent();

    if ($this.val() !== "") {
        getRatesFlotMode(randomID);

        parentDiv.removeClass('requerido');
        parentDiv.addClass('erarequerido');
    }

    if ($this.val() === "") {

        var vehicle = getAllDataVehicleByRandomID(randomID);
        vehicle.iscompletedVehicle = false;
        var realVehicle = setVehicle(vehicle, randomID);
        setAllVehicleDataToSave(realVehicle, "UPDATE");

        //$this.parent().addClass("putErrorBorderFlot");

        if (parentDiv.hasClass('erarequerido')) {
            parentDiv.addClass('requerido');
            parentDiv.removeClass('erarequerido');
        } else {
            parentDiv.addClass('requerido');
            parentDiv.removeClass('erarequerido');
        }
    }
}

function surchargeChange(obj) {
    var $this = $(obj);
    var randomID = $this.attr("data-vehiclerandomid");

    var currval = $this.val();
    if (currval !== '') {
        getRatesFlotMode(randomID);
    }
}

function vehiclePriceEvents(obj) {

    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');
    var currVh = getAllDataVehicleByRandomID(randomID);
    var currAmount = parseFloat($this.val().replace(/,/g, ''));
    var isSame = false;

    if (currVh !== undefined) {
        isSame = (currVh.VehiclePrice === currAmount);
    }

    CollectData_Flot(randomID);

    if (isSame == false) {
        getRatesFlotMode(randomID);
    }
}

function vehicleQtyEvents(obj) {
    var $this = $(obj);
    var randomID = $this.data('vehiclerandomid');
    var vehicle = getAllDataVehicleByRandomID(randomID);
    var isSame = false;

    var currVal = isNaN(parseInt($this.val())) ? 0 : parseInt($this.val());

    if (vehicle !== undefined) {
        isSame = (parseInt(vehicle.VehicleQuantity) === currVal);
    }

    if (isSame == false && currVal > 0) {
        getRatesFlotMode(randomID);
    }
    else {
        vehicle.iscompletedVehicle = false;
        var realVehicle = setVehicle(vehicle, randomID);
        setAllVehicleDataToSave(realVehicle, "UPDATE");
    }

    var parentDiv = $this.parent();

    if (currVal <= 0) {

        $this.parent().addClass("putErrorBorderFlot");

        if (parentDiv.hasClass('erarequerido')) {
            parentDiv.addClass('requerido');
            parentDiv.removeClass('erarequerido');
        } else {
            parentDiv.addClass('requerido');
            parentDiv.removeClass('erarequerido');
        }
    }
    else {
        parentDiv.removeClass('requerido');
        parentDiv.addClass('erarequerido');
    }
}

function vehiclePriceClick(obj) {
    obj.select();
}

function servicePopUpFlotClick(obj) {
    var $this = $(obj);
    var randomID = $this.attr("data-vehiclerandomid");

    SetAdditionals(randomID, true);

    //mostrarpopup
    $("#addServicios").modal({ backdrop: 'static', keyboard: false, show: true });
    $(".saveServices").attr("data-vehiclerandomid", randomID).attr("data-isflotillamode", "S");

    return false;
}

function SetFieldsApplyToZero_Flot(obj, randomID) {

    var vehiclePrice = getHtmlElementByClass("vehiclePrice_Flot", randomID);

    if (obj == true) {
        getHtmlElementByClass("btnNextVehicle", randomID).show();

    } else {
        showWarning(['El Valor del vehículo debe ser mayor a 0 para este plan.', 'Favor Editar el vehículo y ponerle el Valor correcto.'], "Valor vehículo incorrecto");

        getHtmlElementByClass("btnNextVehicle", randomID).hide();

        vehiclePrice.removeClass("erarequerido");
        vehiclePrice.addClass("requerido");
        //putErrorBorder addAgain erarequerido;

        var $select_elem = getHtmlElementByClass("ddlCoverages", randomID) //$('.Coverages');       
        $select_elem.empty();
        $select_elem.append('<option value="">Seleccionar</option>');
        $select_elem.trigger("change.select2");
    }
}

function getRatesFlotMode(randomID, isRequoting) {

    var vehicle = getAllDataVehicleByRandomID(randomID);
    var allGood = false;

    if (vehicle != null) {

        if (vehicle.GlobalDataProductLimits) {

            vehicle.iscompletedVehicle = false;

            if (validateGetRates(randomID, true)) {

                if (!GlobalAllowRequoting) {
                    return;
                }

                var servicesIdList = [];

                var ServicesCoverages = vehicle.GlobalDataProductLimits.ServicesCoverages;

                if (ServicesCoverages) {
                    var allCoverages = new Array();
                    $.each(ServicesCoverages, function (idx, item) { $.each(item.Coverages, function (idx, sItem) { allCoverages.push(sItem) }); });
                    $.each(allCoverages.filter(function (idx, item) { return item.IsSelected; }), function (idx, item) { servicesIdList.push(item.CoverageDetailCoreId); });
                }

                var getQuotationNumberForRates = $("#quotationID").val();
                var getQuotationNumber = $("#QuotationNumber").val();


                isRequoting = (isRequoting == false || isRequoting == undefined) ? false : true;

                //var _qtyVehiclesByVehicle = isRequoting == false ? qtyVehiclesByVehicle() : qtyVehiclesByVehicleForRequoting();

                //var realTotalVehiclesForDiscountFlotilla = _qtyVehiclesByVehicle;

                var q = getHtmlElementByClass('qtyVehicles_Flot', randomID);

                var qtyVehicles = isRequoting == false ? q.val() : vehicle.VehicleQuantity;

                var quotationCoreNumber = $("#QuotationCoreNumber").val();

                //Original
                var NewAgentID = "";
                var NewAgent = getNewAgentSelected();

                if (NewAgent != null) {
                    NewAgentID = NewAgent.NameId;
                }

                var getActualAgentSelected = getOldAgentSelected();
                var ActualAgentSelected = "";

                if (getActualAgentSelected != null) {
                    ActualAgentSelected = getActualAgentSelected.NameId;
                }

                var principalDateOfBirth = isRequoting == false
                    ? $("#DateOfBirth").val() :
                    (
                        (vehicle.driverdob == undefined || vehicle.driverdob == null)
                            ? vehicle.principalDateOfBirth
                            : vehicle.driverdob);

                var wasChangeDateBirth = false;
                if (changedDateBirth == true) {
                    wasChangeDateBirth = true;
                }

                var clientSex = isRequoting == false ? $("#Sex").val() : ((vehicle.driversex == undefined || vehicle.driversex == null) ? vehicle.principalSex : vehicle.driversex);
                var wasChangeClientSex = false;
                if (changedSex == true) {
                    wasChangeClientSex = true;
                }

                var getForeingLicenceDriver = isRequoting == false ? $("#ForeignLicense").val() : ((vehicle.driverforeignlicense == undefined || vehicle.driverforeignlicense == null) ? vehicle.principalrforeignlicense : vehicle.driverforeignlicense);

                var arraySelfAndThirdsDamage = [];
                var arrayServiceCoverages = [];

                if (vehicle.GlobalDataProductLimits.SelfDamagesCoverages) {
                    $.each(vehicle.GlobalDataProductLimits.SelfDamagesCoverages, function (idx, item) {

                        var AsociativeArraySelfAndThirdsDamage = {}
                        AsociativeArraySelfAndThirdsDamage["CoverageDetailCoreId"] = item.CoverageDetailCoreId;
                        AsociativeArraySelfAndThirdsDamage["Limit"] = item.Limit;
                        AsociativeArraySelfAndThirdsDamage["Name"] = item.Name;

                        arraySelfAndThirdsDamage.push(AsociativeArraySelfAndThirdsDamage);
                    });
                }

                if (vehicle.GlobalDataProductLimits.ThirdPartyCoverages) {
                    $.each(vehicle.GlobalDataProductLimits.ThirdPartyCoverages, function (idx, item) {

                        AsociativeArraySelfAndThirdsDamage = {}
                        AsociativeArraySelfAndThirdsDamage["CoverageDetailCoreId"] = item.CoverageDetailCoreId;
                        AsociativeArraySelfAndThirdsDamage["Limit"] = item.Limit;
                        AsociativeArraySelfAndThirdsDamage["Name"] = item.Name;

                        arraySelfAndThirdsDamage.push(AsociativeArraySelfAndThirdsDamage);
                    });
                }


                if (vehicle.GlobalDataProductLimits.ServicesCoverages) {
                    var allCoverages = new Array();
                    $.each(vehicle.GlobalDataProductLimits.ServicesCoverages, function (idx, item) {
                        $.each(item.Coverages, function (idx, sItem) {

                            var AsociativearrayServiceCoverages = {}
                            AsociativearrayServiceCoverages["CoverageDetailCoreId"] = sItem.CoverageDetailCoreId;
                            AsociativearrayServiceCoverages["Limit"] = sItem.Limit;
                            AsociativearrayServiceCoverages["Name"] = sItem.Name;
                            AsociativearrayServiceCoverages["isSelected"] = sItem.IsSelected;

                            arrayServiceCoverages.push(AsociativearrayServiceCoverages);
                        });
                    });
                }

                var limitSelfThirdJson = JSON.stringify(arraySelfAndThirdsDamage);
                var serviceCoberageJson = JSON.stringify(arrayServiceCoverages);

                var usage = vehicle.UsageId;
                var usageName = "";

                var UsageFound = altFind(vehicle.GlobalDataUsages, function (item) { return item.idUso == usage });

                if (UsageFound) {
                    usageName = UsageFound.descUso;

                    var allowed = UsageFound.allowed;
                    var message = UsageFound.message;

                    //No debe generar prima
                    if (allowed == 2) {
                        return;
                    }
                }

                var asyncOrNo = false; //self.parent.changeDate() ? false : true;

                var SecuenciaVehicleSysflex = vehicle.SecuenciaVehicleSysflex;

                var covinfo = getHtmlElementByClass('ddlCoverages', randomID);
                covinfo.find("option:selected").text();

                var coverageCoreId = isRequoting == false ? covinfo.val() : vehicle.SelectedCoverageCoreId;
                var SelectedCoverageName = isRequoting == false ? covinfo.find("option:selected").text() : vehicle.SelectedCoverageName;
                var isSemifull = SelectedCoverageName;
                var selectedVehicleType = vehicle.SelectedVehicleTypeId;


                var brand = vehicle.VehicleModel_Make_Id;
                var model = vehicle.VehicleModel_Model_Id;
                var makeName = vehicle.VehicleMakeName;
                var modelName = vehicle.VehicleModelName;

                var year = vehicle.Year;

                var selectedDeductible = isRequoting == false ? getHtmlElementByClass('ddlDeducible', randomID).val() : vehicle.selectedDeductible;
                var store = vehicle.StoreId;
                var storeName = vehicle.StoreName;
                var selectedSurchargePercent = isRequoting == false ? getHtmlElementByClass('ddlSurchargePercent', randomID).val() : vehicle.SurChargePercentage;

                var _insuredAmount = insuredAmount(randomID);

                var IsFacultative = false;
                var AmountFacultative = 0;

                var selectedVehicleTypeId = -1;
                var rateJson = "";
                var porcImpuesto = 0;

                var VehicleDriver = vehicle.Driver_Id;
                var vehiclePrice = vehicle.VehiclePrice;

                var prodinfo = getHtmlElementByClass('ddlPlan', randomID);
                prodinfo.find("option:selected").text();

                var SelectedProductCore = isRequoting == false ? prodinfo.val() : vehicle.SelectedProductCoreId;
                var SelectedProductName = isRequoting == false ? prodinfo.find("option:selected").text() : vehicle.SelectedProductName;

                var _isLawProduct = isLawProduct(randomID, isRequoting, coverageCoreId, true);
                var idCapacidad = getIdCapacidad(randomID, isRequoting, SelectedProductCore, true);
                var descCapacidad = getDescCapacidad(randomID, isRequoting, SelectedProductCore, true);

                var z = GlobalcurrentStartDateSelected;
                var qq = GlobalcurrentEndDateSelected;

                var fuelTypeId = vehicle.SelectedVehicleFuelTypeId;
                var fuelTypeDesc = vehicle.SelectedVehicleFuelTypeDesc;



                $.ajax({
                    url: '/Home/GetRates',
                    type: 'POST',
                    dataType: 'json',
                    data: {
                        coverageCoreId: coverageCoreId,
                        productId: selectedVehicleType,
                        brandId: brand,
                        modelId: model,
                        vehicleYear: year,
                        coveragePercent: GlobalpercentageToInsure,
                        startDate: GlobalcurrentStartDateSelected,
                        endDate: GlobalcurrentEndDateSelected,
                        insuredAmount: _insuredAmount,
                        servicesIdLstoreist: servicesIdList.join(),
                        deductibleId: selectedDeductible,
                        gender: clientSex,
                        principalDateOfBirth: principalDateOfBirth,
                        storageId: store,
                        percentSurCharge: selectedSurchargePercent,
                        QuotationNumberForRates: getQuotationNumberForRates,
                        LicenciaExtranjera: getForeingLicenceDriver,
                        qtyVehicles: qtyVehicles,
                        usage: usage,
                        secuencia: SecuenciaVehicleSysflex,
                        agentChangeSelected: NewAgentID,
                        quotationCore: quotationCoreNumber,
                        Esdeley: _isLawProduct,
                        idCapacidad: idCapacidad,
                        descCapacidad: descCapacidad,
                        coverages: serviceCoberageJson,
                        limitself: limitSelfThirdJson,
                        usagename: usageName,
                        isSemifull: isSemifull,
                        QuotationNumber: getQuotationNumber,
                        wasChangeDateBirth: wasChangeDateBirth,
                        wasChangeClientSex: wasChangeClientSex,
                        actualAgentSelected: ActualAgentSelected,
                        fuelTypeId: fuelTypeId,
                        fuelTypeDesc: fuelTypeDesc
                    },
                    async: isRequoting == false ? (LoadVehicle ? false : true) : false,//asyncOrNo,
                    success: function (data) {

                        var totalAllDiscountsByVH = 0;

                        if (data.messageError) {
                            showError([data.messageError], 'Error obteniendo la prima');
                            return false;
                        }

                        if (vehicle.GlobalDataProductLimits) {

                            if (data.TpPrime != undefined) {

                                vehicle.GlobalDataProductLimits.TpPrime = data.TpPrime;
                                vehicle.GlobalDataProductLimits.SdPrime = data.SdPrime;
                                vehicle.GlobalDataProductLimits.ServicesPrime = data.ServicesPrime;

                                var total = (data.SdPrime + data.TpPrime + data.ServicesPrime) * qtyVehicles;

                                var iscPercentage = parseFloat(GlobalCurrentIsc);
                                vehicle.GlobalDataProductLimits.TotalIsc = (total * (iscPercentage / 100));

                                selectedVehicleTypeId = data.VehicleTypeId;

                                rateJson = data.jsonRates;
                                porcImpuesto = iscPercentage;

                                var totalPrimePlusIsc = (total + vehicle.GlobalDataProductLimits.TotalIsc);
                                var ISC = vehicle.GlobalDataProductLimits.TotalIsc;

                                var $elem = getHtmlElementByClass('spTotalPrime', randomID);
                                $elem.html("$" + number_format(total, 2));

                                var subRamo = coverageCoreId;
                                //Solos los que no son de Ley
                                if (!_isLawProduct) {

                                    /*Reaseguro*/
                                    $.ajax({
                                        url: '/Home/getMaximoReaseguroSubRamo_New',
                                        dataType: 'json',
                                        data: { SecuenciaVehicleSysflex: SecuenciaVehicleSysflex, quotationCoreNumber: quotationCoreNumber, make: makeName, model: modelName, year: year },
                                        async: false, //LoadVehicle ? false : true,
                                        success: function (data) {

                                            if (data.IsFacultative) {
                                                IsFacultative = data.IsFacultative;
                                                AmountFacultative = data.AmountFacultative;
                                                showWarning([data.message], 'Advertencia Reaseguro');
                                            } else {
                                                IsFacultative = false;
                                                AmountFacultative = 0;
                                            }
                                        }
                                    });
                                }
                            }

                            var totaPrimelbyVH = getTotalPrime(randomID);

                            vehicle.isLawProduct = _isLawProduct;
                            vehicle.GlobalDataProductLimits = vehicle.GlobalDataProductLimits;
                            vehicle.servicescoverages = arrayServiceCoverages;
                            vehicle.limitSelfThirdsDamages = arraySelfAndThirdsDamage;

                            vehicle.VehicleDescription = makeName + " " + modelName;

                            vehicle.InsuredAmount = _insuredAmount;
                            vehicle.PercentageToInsure = GlobalpercentageToInsure;
                            vehicle.TotalPrime = totaPrimelbyVH;
                            vehicle.TotalIsc = (totaPrimelbyVH * (iscPercentage / 100));
                            vehicle.SelectedProductCoreId = SelectedProductCore;
                            vehicle.SelectedProductName = SelectedProductName;
                            vehicle.UsageId = usage;
                            vehicle.UsageName = usageName;
                            vehicle.Quotation_Id = getQuotationNumberForRates;
                            vehicle.SelectedVehicleTypeId = selectedVehicleTypeId;
                            vehicle.SelectedCoverageCoreId = coverageCoreId;
                            vehicle.SelectedCoverageName = SelectedCoverageName;
                            vehicle.SurChargePercentage = selectedSurchargePercent == "" ? 0 : selectedSurchargePercent;
                            vehicle.RateJson = rateJson;
                            vehicle.SecuenciaVehicleSysflex = SecuenciaVehicleSysflex;
                            vehicle.IsFacultative = IsFacultative
                            vehicle.AmountFacultative = AmountFacultative
                            vehicle.VehicleQuantity = qtyVehicles;
                            vehicle.selectedDeductible = selectedDeductible;

                            vehicle.idCapacidad = idCapacidad;
                            vehicle.descCapacidad = descCapacidad;
                            vehicle.isSemifull = isSemifull;
                            vehicle.actualAgentSelected = ActualAgentSelected;
                            vehicle.porcImpuesto = porcImpuesto;
                            vehicle.principalDateOfBirth = principalDateOfBirth;
                            vehicle.principalSex = clientSex;
                            vehicle.principalrforeignlicense = getForeingLicenceDriver;

                            vehicle.startDate = GlobalcurrentStartDateSelected;
                            vehicle.endDate = GlobalcurrentEndDateSelected;

                            vehicle.PercentByQtyVehicle = PercentByQtyVehicle > 0 ? PercentByQtyVehicle : 0;
                            vehicle.TotalByQtyVehicle = TotalByQtyVehicle();

                            vehicle.iscompletedVehicle = true;

                            vehicle.totalAllDiscountsByVH = totalAllDiscountsByVH;

                            var realVehicle = setVehicle(vehicle, randomID);

                            setAllVehicleDataToSave(realVehicle, "UPDATE");

                            allGood = true;

                            if (GlobalSaveVehicleOnly && !isRequoting) {

                                SaveDataVehicleOnlyOne(randomID);

                                //refreshDiscountSectionAllVehicle();
                            }
                        }
                    }
                });
            }
        }
    }

    return allGood;
}

function loadDataVehicleFlotMode(vehicle, data, qtyVehicleTotal) {

    var randomID = vehicleRandomID;

    LoadVehicleCurrentID = vehicle.Id;
    LoadVehicleCurrentSecuenciaVehicleSysflex = vehicle.SecuenciaVehicleSysflex;
    LoadVehicleQuantity = vehicle.VehicleQuantity;
    hasVehicle = true;
    GlobalSaveVehicleOnly = false;
    //wasLoadVehicle = true;

    $("#lastModelSelected").val(vehicle.VehicleModel_Model_Id);

    $element = getHtmlElementByClass('vehiclePrice_Flot', randomID);

    $element.val(vehicle.VehiclePrice);
    //Cascade
    var $element = getHtmlElementByClass('ddlMakes', randomID);
    $element.val(vehicle.VehicleModel_Make_Id);
    $element.trigger("change");

    $element = getHtmlElementByClass('ddlModels', randomID);
    $element.val(vehicle.VehicleModel_Model_Id);
    $element.trigger("change");

    $element = getHtmlElementByClass('ddlYears', randomID);
    $element.val(vehicle.Year);
    $element.trigger("change");

    $element = getHtmlElementByClass('ddlVehicleType', randomID);
    $("#" + $element.attr('id') + " option").each(function () {
        if ($(this).text() == vehicle.SelectedVehicleTypeName) {
            $(this).attr('selected', 'selected');
        }
    });
    $element.trigger("change");

    $element = getHtmlElementByClass('ddlUsages', randomID);
    $element.val(vehicle.UsageId);
    $element.trigger("change");
    //

    $element = getHtmlElementByClass('ddlVehicleYearsOld', randomID);
    $element.val(vehicle.VehicleYearOld);
    $element.trigger("change");

    $element = getHtmlElementByClass('ddlStoreCar', randomID);
    $element.val(vehicle.StoreId);
    $element.trigger("change");

    $element = getHtmlElementByClass('ddlfuelType', randomID);
    $element.val(vehicle.SelectedVehicleFuelTypeId);
    $element.trigger("change");

    $element = getHtmlElementByClass('qtyVehicles_Flot', randomID);
    $element.val(vehicle.VehicleQuantity);


    LoadVehicle = true;
    CollectData_Flot(randomID);

    GlobalServicesSelected = vehicle._services;

    if (vehicle.SurChargePercentage > 0) {
        $element = getHtmlElementByClass('ddlSurchargePercent', randomID);
        $element.val(vehicle.SurChargePercentage);
        //$element.trigger('change');
    }

    var theText = vehicle.SelectedProductName;
    $element = getHtmlElementByClass('ddlPlan', randomID);

    $("#" + $element.attr('id') + " option").each(function () {
        if ($(this).text() == theText) {
            $(this).attr('selected', 'selected');
        }
    });
    $element.trigger('change');

    theText = vehicle.SelectedCoverageName;
    $element = getHtmlElementByClass('ddlCoverages', randomID);

    $("#" + $element.attr('id') + " option").each(function () {
        if ($(this).text() == theText) {
            $(this).attr('selected', 'selected');
        }
    });
    $element.trigger('change');

    var pl = vehicle.vehicleProductLimits;

    if (pl != undefined && pl != null) {
        var dedu = pl.SelectedDeductibleName;

        if (dedu != null && dedu != "") {
            $element = getHtmlElementByClass('ddlDeducible', randomID);

            $("#" + $element.attr('id') + " option").each(function () {
                if ($(this).text() == dedu) {
                    $(this).attr('selected', 'selected');
                }
            });
            $element.trigger('change');
        }
        else {
            if (pl.SelectedDeductibleCoreId) {
                $element = getHtmlElementByClass('ddlDeducible', randomID);
                $element.val(pl.SelectedDeductibleCoreId);
                $element.trigger('change');
            }
        }
    }

    //Si no permite recotize, setiar mi objeto con las propiedades que vienen desde la db
    if (!GlobalAllowRequoting) {
        var vehicleActual = getAllDataVehicleByRandomID(randomID);

        var qtyVehicles = vehicle.VehicleQuantity;

        var realTotalVehiclesForDiscountFlotilla = data.qtyMaxVehicles > 0 ? data.qtyMaxVehicles : qtyVehiclesByVehicleForRequoting();

        var makeName = vehicle.VehicleMakeName;
        var modelName = vehicle.ModelDesc;
        var year = vehicle.Year;
        var IsFacultative = false;
        var AmountFacultative = 0;

        var getActualAgentSelected = getOldAgentSelected();
        var ActualAgentSelected = "";

        if (getActualAgentSelected != null) {
            ActualAgentSelected = getActualAgentSelected.NameId;
        }

        var _insuredAmount = insuredAmount(randomID);

        if (vehicleActual.GlobalDataProductLimits) {

            if (vehicle.vehicleProductLimits.TpPrime != undefined) {

                var TpPrime = vehicle.vehicleProductLimits.TpPrime
                var SdPrime = vehicle.vehicleProductLimits.SdPrime
                var ServicesPrime = vehicle.vehicleProductLimits.ServicesPrime

                vehicleActual.GlobalDataProductLimits.TpPrime = TpPrime;
                vehicleActual.GlobalDataProductLimits.SdPrime = SdPrime;
                vehicleActual.GlobalDataProductLimits.ServicesPrime = ServicesPrime;

                var total = (SdPrime + TpPrime + ServicesPrime) * qtyVehicles;
                var iscPercentage = parseFloat(GlobalCurrentIsc);
                vehicleActual.GlobalDataProductLimits.TotalIsc = (total * (iscPercentage / 100));

                var selectedVehicleTypeId = vehicle.SelectedVehicleTypeId;

                var rateJson = vehicle.RateJson;
                var porcImpuesto = iscPercentage;

                var totalPrimePlusIsc = (total + vehicleActual.GlobalDataProductLimits.TotalIsc);
                var ISC = vehicleActual.GlobalDataProductLimits.TotalIsc;

                var _isLawProduct = isLawProduct(randomID, false, vehicle.SelectedCoverageCoreId, true);
                var idCapacidad = getIdCapacidad(randomID, false, vehicle.SelectedProductCoreId, true);
                var descCapacidad = getDescCapacidad(randomID, false, vehicle.SelectedProductCoreId, true);

                var $elem = getHtmlElementByClass('spTotalPrime', randomID);
                $elem.html("$" + number_format(total, 2));

                if (PercentByQtyVehicle == 0) {

                    $.ajax({
                        url: '/Home/GetPercentByQtyVehicle',
                        type: 'POST',
                        dataType: 'json',
                        data: { qtyVehicles: realTotalVehiclesForDiscountFlotilla },
                        async: false,
                        success: function (data) {
                            PercentByQtyVehicle = data;

                            if (data > 0) {
                                isFlotilla = true;
                            } else {
                                isFlotilla = false;
                            }
                        }
                    });
                }

                var subRamo = vehicle.SelectedCoverageCoreId;

                //Solos los que no son de Ley
                if (!_isLawProduct) {

                    IsFacultative = vehicle.IsFacultative;
                    AmountFacultative = vehicle.AmountFacultative;
                    var msjFacultative = 'El vehículo Marca: {0} Modelo: {1} Año: {2} Sobrepasa el valor máximo del reaseguro. El monto de la prima puede variar.';
                    msjFacultative = msjFacultative.replace("{0}", makeName).replace("{1}", modelName).replace("{2}", year);

                    if (IsFacultative) {
                        showWarning([msjFacultative], 'Advertencia Reaseguro');
                    }
                }
            }

            var totaPrimelbyVH = getTotalPrime(randomID);

            vehicleActual.isLawProduct = _isLawProduct;
            vehicleActual.GlobalDataProductLimits = vehicleActual.GlobalDataProductLimits;
            //vehicleActual.servicescoverages = arrayServiceCoverages;
            //vehicleActual.limitSelfThirdsDamages = arraySelfAndThirdsDamage;

            vehicleActual.VehicleDescription = makeName + " " + modelName;

            vehicleActual.InsuredAmount = _insuredAmount;
            vehicleActual.PercentageToInsure = GlobalpercentageToInsure;
            vehicleActual.TotalPrime = totaPrimelbyVH;
            vehicleActual.TotalIsc = (totaPrimelbyVH * (iscPercentage / 100));
            vehicleActual.SelectedProductCoreId = vehicle.SelectedProductCoreId;
            vehicleActual.SelectedProductName = vehicle.SelectedProductName;
            vehicleActual.UsageId = vehicle.UsageId;
            vehicleActual.UsageName = vehicle.UsageName;
            vehicleActual.Quotation_Id = $("#quotationID").val();
            vehicleActual.SelectedVehicleTypeId = selectedVehicleTypeId;
            vehicleActual.SelectedCoverageCoreId = vehicle.SelectedCoverageCoreId;
            vehicleActual.SelectedCoverageName = vehicle.SelectedCoverageName;
            vehicleActual.SurChargePercentage = vehicle.SurChargePercentage;
            vehicleActual.RateJson = rateJson;
            vehicleActual.SecuenciaVehicleSysflex = vehicle.SecuenciaVehicleSysflex;
            vehicleActual.IsFacultative = IsFacultative
            vehicleActual.AmountFacultative = AmountFacultative
            vehicleActual.VehicleQuantity = qtyVehicles;
            vehicleActual.selectedDeductible = vehicle.vehicleProductLimits.SelectedDeductibleCoreId;
            vehicleActual.idCapacidad = idCapacidad;
            vehicleActual.descCapacidad = descCapacidad;
            vehicleActual.isSemifull = vehicle.SelectedCoverageName;
            vehicleActual.actualAgentSelected = ActualAgentSelected;
            vehicleActual.porcImpuesto = porcImpuesto;

            vehicleActual.startDate = GlobalcurrentStartDateSelected;
            vehicleActual.endDate = GlobalcurrentEndDateSelected;

            vehicleActual.PercentByQtyVehicle = PercentByQtyVehicle > 0 ? PercentByQtyVehicle : 0;
            vehicleActual.TotalByQtyVehicle = TotalByQtyVehicle();

            vehicleActual.iscompletedVehicle = true;

            var realVehicle = setVehicle(vehicleActual, randomID);

            setAllVehicleDataToSave(realVehicle, "UPDATE");
        }
    }
    //

    LoadVehicle = false;
    LoadVehicleCurrentID = 0;
    LoadVehicleCurrentSecuenciaVehicleSysflex = 0;
    LoadVehicleQuantity = 1;
    vehicleRandomID = 0;
    wasLoadVehicle = false;
    GlobalServicesSelected = null;
    GlobalSaveVehicleOnly = true;

    if (!GlobalAgentDirectSales) {
        configAppMode();
    }

    if (qtyVehicleTotal >= 1) {
        $('#btnNextVehicle').trigger('click');
    }


}

//function
function moveCursorToEnd(input) {
    var originalValue = input.val();
    input.val('');
    input.blur().focus().val(originalValue);
}

//-------------------------------------NEW FLOTILLA PROCESS-------------------------------------


function listProductsNotShow(appMode) {

    var arrProds = [];
    if (appMode !== "") {
        $.ajax({
            url: '/Home/getProductByAppMode',
            type: 'POST',
            dataType: 'json',
            data: { appMode: appMode },
            async: false,
            success: function (result) {
                if (result.data) {                    
                    $.each(result.data, function (idx, obj) {
                        arrProds.push(obj.ProductName);
                    });

                }
            }
        });
    }
    return arrProds;
}