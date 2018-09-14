﻿var _fixheightInspeccion;

function pageLoad() {
    setUserAgent();

    $('.pop_up_wrapper').hide();

    setAccordeaons();

    //$('.trigger.shown').click(function () {
    //    _fixheightInspeccion = setInterval('fixheight();', 600);
    //});

    $('#acc1 li').fadeIn(2);

    $("#bodyContent_UCInspectionForm1_UCProperty1_gvPictures_DXMainTable").find("img").each(function () {
        var $this = $(this);
        var $parent = $this.parent();
        $parent.append("<a href='' rel='prettyPhoto[pp_gal]' />");
        var $a = $parent.find("a:first");
        $a.attr("href", $this.attr('src'));
        $a.append($this);
    });

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

    var $div = document.getElementById("map");

    var $txtLongitud = $("#txtLongitud");
    var $txtLatitud = $("#txtLatitud");

    var sLongitud = $txtLongitud.val();
    var sLatitud = $txtLatitud.val();
    if (sLatitud != "0" && sLongitud != "0") {
        var LatLng = new google.maps.LatLng(sLatitud, sLongitud);
        initializeSubmitMap($div, LatLng);
    } else {
        //Crear el marcador usando pais provincia y municipio
        var sProvincia = $("#txtProvincia").val();
        var sMunicipio = $("#txtMunicipio").val();
        var sSectorParajeSeccion = $("#txtSectorParajeSeccion").val();

        searchAddress("Republica Dominicana, " + sProvincia + ", " + sMunicipio + ", " + sSectorParajeSeccion, function (latlon) {
            var lat = latlon.lat;
            var lon = latlon.lng;
            $("#txtLatitud").val(lat);
            $("#txtLongitud").val(lon);
            initializeSubmitMap($div, latlon);
        });
    }

    var $btnGetCurrentLocation = $("#btnGetCurrentLocation");
    ////Configutations();
    ////if (IsMobileDev) {
    $btnGetCurrentLocation.show();
    $("#btnGetCurrentLocation").click(function () {
        GetCurrentLocation(function (lat, lon) {
            $txtLongitud.val(lon);
            $txtLatitud.val(lat);
            var LatLng = new google.maps.LatLng(lat, lon);
            initializeSubmitMap($div, LatLng);
        });
    });
    //}

    var HasSign = $("#hdnHasSign").val() == "true";
    var CustomerSign = $("#hdnCustomerSign").val();
    var $imgSignatureImage = $("#imgSignatureImage");
    var $pnSignature = $("#pnSignature");

    if (!HasSign) {
        //Call method to create Signature Panel
        $("#signatureparent").jSignature({
            //color: "#194E92",
            lineWidth: 3,
            width: '100%',
            height: 200
            //background-color:"#0f0"
        });

        $pnSignature.show();
        $imgSignatureImage.hide();
    } else {
        $imgSignatureImage.attr('src', CustomerSign);
        $imgSignatureImage.show();
        $pnSignature.hide();

    }

    //$("#signatureparent").jSignature('disable');
}


function SetearFirma() {
    //Verificar si se escribio una firma
    var HasSign = $("#signatureparent > div").eq(1).find("img:first").css("display") == "none";
    $("#hdnHasSign").val(HasSign);
    if (HasSign) {
        var data = $("#signatureparent").jSignature('getData');
        $("#hdnCustomerSign").val(data);
    }
}

function setSumasAseguradasText(text) {
    $('#lblSumasAseguradas').text(text);
}

function setFechaInspeccion() {
    var DateCompleted = $('.fecha').val().toString().substr(0, 11);
    $('.fecha').val(DateCompleted);

    var date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    hours = hours < 10 ? '0' + hours : hours;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    seconds = seconds < 10 ? '0' + seconds : seconds;
    var strDateTime = $('.fecha').val() + ' ' + hours + ':' + minutes + ':' + seconds + ' ' + ampm;
    $('.fecha').val(strDateTime);
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

function SetImageDigitalSign() {
    var dataBase64Png = $("#signatureparent").jSignature('getData');
    var $imgSignatureImage = $("#imgSignatureImage");
    $imgSignatureImage.attr('src', dataBase64Png);

    return dataBase64Png;
}

function fileUpload() {
    $('#bodyContent_UCInspectionForm1_UCProperty1_fuFotografia1').click();
}

function uploadImage() {
    BeginRequestHandler();

    $('#bodyContent_UCInspectionForm1_UCProperty1_btnCargarFoto').click()

    EndRequestHandler();
}

function clearControls(enabled) {
    $('input:radio:checked').removeAttr('checked');

    if (enabled) {
        $('input:radio').removeAttr('disabled');
    }
    else {
        $('.categoria_foto').val('0').attr('disabled', 'disabled');
        $('input:radio').attr('disabled', 'disabled');
    }
    //$('#txtUsuarioInspeccion').removeAttr('disabled');
    $("#txtUsuarioInspeccion").val('');
    $("#txtUsuarioInspeccion").removeAttr('disabled');
}

function perdidasSiNo() {
    if ($('input:radio[id*="rblHaTenidoPerdidas"]:checked').parent().find('label').html().toLowerCase() == 'no') {
        $($('.nivelesPerdidas input:radio')[3]).prop('checked', true).attr('disabled', 'disabled');
        $('.nivelesPerdidas input:radio').attr('disabled', 'disabled');
        $('.causaPerdidas input:radio').attr('disabled', 'disabled').prop('checked', false);
        $('#bodyContent_UCInspectionForm1_UCProperty1_txtHistorialPerdidaOtros').val('');
        $('#bodyContent_UCInspectionForm1_UCProperty1_txtHistorialPerdidaOtros').attr('disabled', 'disabled');
        //$('input.perdidasOtros').attr('disabled', 'disabled').val('');
    }
    else {
        $('.nivelesPerdidas input:radio').prop('checked', false).removeAttr('disabled');
        $('.causaPerdidas input:radio').removeAttr('disabled');
        $('#bodyContent_UCInspectionForm1_UCProperty1_txtHistorialPerdidaOtros').removeAttr('disabled');
        //$('input.perdidasOtros').removeAttr('disabled');
    }
}

function enabledPhotoButton(enable) {
    if (enable)
        $('.selectPhoto').removeAttr('disabled');
    else
        $('.selectPhoto').attr('disabled', 'disabled');
}

function checkRBL(selected) {
    $(selected).each(function (index, value) {
        $("input:radio[value='" + value + "']").prop("checked", true);
    });
}

function disableRBLDatosGenerales() {
    $('.datos_generales input:radio').attr('disabled', 'disabled');
    return false;
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

function setUbicacionInspeccionada(text) {
    $('input[id*="txtUbicacionInspeccionada"]').val(text);
}

function setUserInspection(usuario) {
    $("#txtUsuarioInspeccion").val(usuario);
}

DlgConfirmWithCallBackALIF = function (obj, Message, pwidth, pheight, Func, FuncNo, key) {
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

DlgConfirmALIF = function (obj, Message, pwidth, pheight) {

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

CustomDialogMessageExALIF = function (Message, pwidth, pheight, isModal, titlex, key) {

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
