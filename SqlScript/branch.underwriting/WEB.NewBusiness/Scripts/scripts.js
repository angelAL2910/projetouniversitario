var IsMobileDev = false;

window.onerror = function (errorMsg, url, lineNumber, column, errorObj) {
    /* if (errorMsg == "Uncaught TypeError: Cannot read property 'style' of null" ||
         errorMsg == "TypeError: contentElement is null" ||
         errorMsg == "Unable to get property 'style' of undefined or null reference")
     return false;
 
     CustomDialogMessageEx('Error: ' + errorMsg, 500, 200, true, "Error");
     */
}

function setESCAPEkey(sender, args) {
    //  adding handler to the document's keydown event
    $addHandler(document, "keydown", onKeyDown);
}

function onKeyDown(e) {
    if (e && e.keyCode == Sys.UI.Key.esc) {
        var Popups = $(".PdfViewer,#popCoverage");

        Popups.each(function () {
            var $this = $(this);
            var isEmpty = $this.html() == "";
            if (!isEmpty) {
                var padre = $this.parent();
                $(padre).find("input[type='button']").click();
            }
        });
    }
}

function btnDummyClick() {
    $("#btnDummy").click();
}

divScroll = function () {
    $(".st-content-inner").scroll(function () {
        try {

            //Posicion de los DatePickers
            if ($("#CurrentDatePickerShow").val() != "") {
                var objP = $("#ui-datepicker-div");
                var objR = $($("#CurrentDatePickerShow").val().split(",")[0]);
                var offset = objR.offset();
                var ArribaOrAbajo = $("#CurrentDatePickerShow").val().split(",")[1];
                if (ArribaOrAbajo == "AR") {
                    var TopArriba = parseInt(objP.css("height").replace("px", ""));
                    $(objP).offset({ top: offset.top - TopArriba, left: offset.left });
                } if (ArribaOrAbajo == "AB") {
                    var TopAbajo = parseInt(objR.css("height").replace("px", ""));
                    $(objP).offset({ top: offset.top + TopAbajo, left: offset.left });
                }
            }
            //End Posicion de los DatePickers
            $("#hdnPosScroll").val($(this).scrollTop());
        } catch (e) {
            console.log(e.message);
        }
    });
};

/**
* Determine the mobile operating system.
* This function either returns 'iOS', 'Android' or 'unknown'
*
* @returns {String}
*/
function getMobileOperatingSystem() {
    var userAgent = navigator.userAgent || navigator.vendor || window.opera;
    if (userAgent.match(/iPad/i) || userAgent.match(/iPhone/i) || userAgent.match(/iPod/i))
        return 'iOS';
    else if (userAgent.match(/Android/i))
        return 'Android';
    else
        return 'unknown';
}

setPositionElementDatePicker = function (ObjPosition, ObjRelation) {
    setTimeout(function () {
        var objP = $(ObjPosition);
        var objR = $(ObjRelation);
        $('body').prepend(objP);
        var offset = objR.offset();
        var Top = parseInt($(objP).css("height").replace("px", ""));
        $(objP).offset({ top: offset.top - Top, left: offset.left });
    }, 10);
};

var ObjAcordeon = "#ulAvaliableDashboard,#STFCUserProfile1_acordeon_agent_profile,#accAddress,#accordeonEforms,#acordeon_agent_profile, #acc2,#acc3, #acc4, #acc5, #acc6,#accBeneficiaries";

Notify = function (sender) {
    var span = $(sender).find("span")[1];

    var msj = "";
    if (span != null)
        msj = $(span).html();
    else
        msj = $(sender).html();

    CustomDialogMessageEx(msj + " tab is disabled", 350, null, true, lang == "en" ? "Warning" : "Advertencia");
};

var openDialog = function (uri, name, options, closeCallback) {
    var win = window.open(uri, name, options);
    var interval = window.setInterval(function () {
        try {
            if (win == null || win.closed) {
                window.clearInterval(interval);
                closeCallback(win);
            }
        }
        catch (e) {
        }
    }, 500);
    return win;
};

function ShowPopCardNetPay(Url, pwidth, pheight) {
    pwidth = pwidth != null && pwidth != "-1" ? pwidth : 1000;
    pheight = pheight != null && pheight != "-1" ? pheight : 800;

    var x = screen.width / 2 - pwidth / 2;
    var y = screen.height / 2 - pheight / 2;

    openDialog(Url, "_blank", 'height = ' + pheight + ', width = ' + pwidth + ',top = ' + y + ', left = ' + x + ', toolbar = no, directories= no, menubar = no, scrollbars = yes, resizable = No, location = no , status = no ',
        function () {
            __doPostBack('ctl00$bodyContent$lnkPayment', '');
        });
}

Configutations = function () {

    IsMobileDev = IsAMobileDevice();

    $("#bodyContent_RequirementsContainer_pdfUploadPanel,#pnModalPopupPDFViewer").draggable({
        handle: ".titulos_azules"
    });

    $(".titulos_azules").css("cursor", "move");

    //Marcar la opcion seleccionada en el menu de la izquierda
    var CurrentMenu = $("#hdnCurrentMenuSelectedMenuLeft").val();

    if (CurrentMenu != null & CurrentMenu != "") {
        $("#Menu >li > a").removeClass("activo");
        $("#" + CurrentMenu).addClass("activo");
    }

    setAccordeaons();
    //Setear el lenguaje de la application
    setLanguage();

    $("body").find(".dxgvGroupPanel_DevEx").css("background", "url('')");
    $("body").find("table").find("td.dxeButton").css("width", "20%");
    $("body").find("table").find("td.dxeButton").find("img:first").addClass("datepicker_02");

    $('.dxWeb_pcCloseButton_DevEx').hide();

    $("[data-inputmask]").inputmask();

    $("[number = 'number4']").inputmask({ alias: 'integer', autoGroup: false, digits: 0, repeat: 4, allowMinus: false, allowPlus: false, rightAlign: false });

    $("[numberWithoutValidation = 'numberWithoutValidation']").inputmask({ alias: 'integer', groupSeparator: ',', autoGroup: true, repeat: 3, digits: 0, allowMinus: false, allowPlus: false });

    $("[number = 'number3']").inputmask({ alias: 'integer', groupSeparator: ',', autoGroup: true, repeat: 3, digits: 0, allowMinus: false, allowPlus: false });

    $("[number = 'number2']").inputmask({ alias: 'integer', groupSeparator: ',', autoGroup: true, repeat: 2, digits: 0, allowMinus: false, allowPlus: false });

    $("[number = 'number']").inputmask({ alias: 'integer', groupSeparator: ',', autoGroup: true, repeat: 9, digits: 0, allowMinus: false, allowPlus: false });

    $("[RncNumber = 'RncNumber']").attr('data-inputmask-regex', "[0-9\(\)\.\+/ ]*");

    $("[Cedula = 'Cedula']").inputmask("999-9999999-9");

    $("[decimal='decimal']").inputmask({ alias: 'decimal', groupSeparator: ',', autoGroup: true, repeat: 9, digits: 2, allowMinus: false, allowPlus: false, rightAlign: true });

    $("[decimalWithsign='decimalWithsign']").inputmask({ alias: 'decimal', groupSeparator: ',', autoGroup: true, repeat: 9, digits: 2, allowMinus: true, allowPlus: false, rightAlign: true });

    $("[decimal='decimal3']").inputmask({ alias: 'decimal', groupSeparator: ',', autoGroup: true, repeat: 3, digits: 2, allowMinus: false, allowPlus: false, rightAlign: true });

    $("[Policy='Policy']").attr('data-inputmask-regex', "[0-9A-Za-z-\\sñÑ]*");

    $("[alphabetical='alphabetical']").attr('data-inputmask-regex', "[A-Za-z.\\sñÑáéíóúÁÉÍÓÚ']*");

    $("[alphabetical='alphabeticalLastName']").attr('data-inputmask-regex', "[A-Za-z.'-\\sñÑáéíóúÁÉÍÓÚ']*");

    $("[alphabetical='alphabetical']").inputmask("Regex");

    $("[RncNumber = 'RncNumber']").inputmask("Regex");
    $("[Rnc = 'Rnc']").inputmask("999-99999-9");

    $("[Policy = 'Policy']").inputmask("Regex");

    $("[alphabetical='alphabeticalLastName']").inputmask("Regex");

    $("#tb_WUC_PI_Age").setMask("age");

    var CurrentMenu = $("#hdnCurrentMenuSelectedMenuLeft").val();
    try {
        //Cambiar de Tab
        var CurrentTab = $("#hdnCurrentTabAddNewClient").val().split("|")[0];
    } catch (e) {
        var CurrentTab = $("#hdnCurrentTabAddNewClient").val();
    }

    if (CurrentMenu == "MenulnkContactList")
        setInterval("fixheight();", 600);

    $("select").each(function () {
        if ($(this).attr("Policy") == null)
            $(this).attr('onkeydown', 'return (event.keyCode!=8 && event.keyCode!=13)');
        else
            $(this).attr('onkeydown', 'return (event.keyCode!=8)');
    });

    $("input:text").each(function () {
        var $this = $(this);
        var AllowEnter = ($this.attr('AllowEnter') != null && $this.attr('AllowEnter') == "true");
        if (!AllowEnter) {
            if ($this.attr("Policy") == null) {
                var decimal = $this.attr("decimal");
                var number = $this.attr("number");
                if (decimal == null && number == null && !$this.hasClass("dxeEditArea_DevEx"))
                    $this.attr('onkeydown', 'return (event.keyCode!=13)');
            }
        }
    });

    $("input:text[readonly='readonly']").attr('onkeydown', 'return (event.keyCode!=8 && event.keyCode!=13)');

    if ($('#hdnLangChange').val() == "true") {
        //Seleccionar todos los valores que estaban en los drops
        var objects = JSON.parse($("#hdnDropsValues").val());

        for (var x = 0; x < objects.length; x++) {
            var $this = objects[x];
            var DropId = '#' + replaceAll(' ', '_', $this.IdDrop);
            $(DropId).val($this.value);
        }

        $('#hdnLangChange').val("false");
    }

    try {
        document.getElementById("content").style.display = "none";
    } catch (e) {

    }      
   
};

validateFilter = function () {
    var result = true;
    var FromTxt = $("#FromTxt").val();
    var ToTxt = $("#ToTxt").val();

    if (FromTxt != "" || ToTxt != "") {
        if (FromTxt == "" || ToTxt == "") {
            result = false;
            CustomDialogMessageEx('The start and end fields should not be empty', 350, 150, true, lang == "en" ? "Warning" : "Advertencia");
        }
    }
    return result;
};

CorfimRedirect = function (sender) {
    if ($(sender).attr("alt") == "Disabled")
        return false;

    var CurrentMenu = $("#hdnCurrentMenuSelectedMenuLeft").val();

    if (CurrentMenu == "MenulnkClientInfo") {

        var Tab = $("#hdnCurrentTabAddNewClient").val().split('|')[0];

        if (Tab != "lnkPayment" && Tab != "lnkBeneficiaries" && Tab != "lnkRequirements") {
            var lang = $("#hdnLang").val();
            var msj = lang == "en" ? "If you have not saved the data that you edit or insert will be lose. Do you want go out of this page?"
                                      :
                                     "En caso de que no haya guardado los datos que se han editado o capturado van a perderse. Esta seguro que desea abandonar la pagina?";
            return DlgConfirm(sender, msj);
        }
    }
    else BeginRequestHandler();
};

showAlert = function (msg) {
    CustomDialogMessage(msg);
};

function showCommonLoading() {
    BeginRequestHandler();
}

function hideCommonLoading() {
    EndRequestHandler();
}

function commonAlert(val) {
    CustomDialogMessage(val);
}

RemenberSelectionDropDowns = function () {
    var ArrayObjects = [];
    $("body").find("select").each(function () {
        var $this = $(this);
        var ObjectDrops = {};
        if ($this.val() != "" || $this.val() == undefined) {
            ObjectDrops.IdDrop = $this.attr("id");
            ObjectDrops.value = $this.val();
            ArrayObjects.push(ObjectDrops);
        }
    });

    var jsonResult = JSON.stringify(ArrayObjects);
    $("#hdnDropsValues").val(jsonResult);
};

changeLanguage = function (lang) {
    $("#hdnLang").val(lang);
    BeginRequestHandler();
    RemenberSelectionDropDowns();
    setTimeout(PostBack(), 100);
};

function PostBack() {

    var CurrentTab = $("#hdnCurrentTabAddNewClient").val();

    if (CurrentTab != null)
        CurrentTab.split("|")[0];

    if (CurrentTab == "lnkBeneficiaries") {
        $("#hdnPendingRefreshBeneficiariesTab").val("false");
        $("#hdnRefreshBeneficiariesTab").val("true");
        $("#btnRefresh").click();
    }
    else {
        $("#hdnPendingRefreshBeneficiariesTab").val("true");
        __doPostBack();
    }
}

setLanguage = function () {
    var dvIdiomas = $("#divLanguage");
    var idioma = $("#hdnLang").val();
    dvIdiomas.removeAttr("class").addClass("idiomas").addClass(idioma);
    var text = dvIdiomas.find("ul > li[class~='" + (idioma) + "']").html();
    var label = dvIdiomas.find("label")[0];
    $(label).html(text).removeAttr("class");
    $(label).addClass(idioma);
};

// on window resize run function
$(window).resize(function () {
    fluidDialog();
});

// catch dialog if opened within a viewport smaller than the dialog width
$(document).on("dialogopen", ".ui-dialog", function (event, ui) {
    fluidDialog();
});

function fluidDialog() {
    var $visible = $(".ui-dialog:visible");
    // each open dialog
    $visible.each(function () {
        var $this = $(this);
        var dialog = $this.find(".ui-dialog-content").data("ui-dialog");
        // if fluid option == true
        if (dialog.options.fluid) {
            var wWidth = $(window).width();
            // check window width against dialog width
            if (wWidth < (parseInt(dialog.options.maxWidth) + 50)) {
                // keep dialog from filling entire screen
                $this.css("max-width", "90%");
            } else {
                // fix maxWidth bug
                $this.css("max-width", dialog.options.maxWidth + "px");
            }
            //reposition dialog
            dialog.option("position", dialog.options.position);
        }
    });

}

$(document).ready(function () {

    $(document).tooltip();

    $("body").find("a[alt='Disabled']").each(function () {
        $(this).click(function () {
            CustomDialogMessageEx($(this).html() + "tab is disabled", 350, null, true, lang == "en" ? "Warning" : "Advertencia");
            EndRequestHandler();
            return false;
        });
    });

    // Scripts
    //************************************//
    /*	
        Funciones usadas para mejorar 
        compatibilidad, estetica y apariencia
        en los distos browsers
    */

    /*****************************************************************/
    /********************** DETECTA NAVEGADOR ************************/
    /*****************************************************************/
    var browser = {
        chrome: false,
        mozilla: false,
        opera: false,
        msie: false,
        safari: false
    };

    var sUsrAg = navigator.userAgent;
    if (sUsrAg.indexOf("Chrome") > -1) {
        browser.chrome = true;
    } else if (sUsrAg.indexOf("Safari") > -1) {
        getWidth();
    } else if (sUsrAg.indexOf("Opera") > -1) {
        getWidth();
    } else if (sUsrAg.indexOf("Firefox") > -1) {
        getWidth();
    } else if (sUsrAg.indexOf("MSIE") > -1) {
        getWidth();
    }

    /*****************************************************************/
    /*********************** MENU SUPERIOR ***************************/
    /*****************************************************************/

    //  The function to change the class
    var changeClass = function (r, className1, className2) {
        var regex = new RegExp("(?:^|\\s+)" + className1 + "(?:\\s+|)");
        if (regex.test(r.className)) {
            r.className = r.className.replace(regex, ' ' + className2 + ' ');
        }
        else {
            r.className = r.className.replace(new RegExp("(?:^|\\s+)" + className2 + "(?:\\s+|)"), ' ' + className1 + ' ');
        }
        return r.className;
    };

    //  afecta menu para pantallas mas pequenas
    var menuElements = document.getElementById('menu');
    menuElements.insertAdjacentHTML('afterBegin', '<button type="button" id="menutoggle" class="navtoogle" aria-hidden="true"><i aria-hidden="true" class="icon-menu"> </i> Menu</button>');

    //  Toggle the class on click to show / hide the menu
    document.getElementById('menutoggle').onclick = function () {
        changeClass(this, 'navtoogle active', 'navtoogle');
    }
    document.onclick = function (e) {
        var mobileButton = document.getElementById('menutoggle'),
            buttonStyle = mobileButton.currentStyle ? mobileButton.currentStyle.display : getComputedStyle(mobileButton, null).display;

        if (buttonStyle === 'block' && e.target !== mobileButton && new RegExp(' ' + 'active' + ' ').test(' ' + mobileButton.className + ' ')) {
            changeClass(mobileButton, 'navtoogle active', 'navtoogle');
        }
    }

    $(window).load(function () {
        equalheight('.fix_height');//siempre que la cantidad de columnas sean las mismas en todos los pisos
        equalheight('.fix_height1');//si varian de un piso a otro, ej arriba tengo 2 abajo 3 abajo debo llamar fix_height1 etc
        equalheight('.fix_height2');
        equalheight('.fix_height3');
        equalheight('.fix_height4');

    });

    $(window).resize(function () {
        equalheight('.fix_height');
        equalheight('.fix_height1');
        equalheight('.fix_height2');
        equalheight('.fix_height3');
        equalheight('.fix_height4');
        $(".ui-dialog-content:visible").dialog({ position: { my: "center", at: "center", of: window } });
    });

    $('.refresh_height').on('change', function () {
        equalheight('.fix_height');
        equalheight('.fix_height1');
        equalheight('.fix_height2');
        equalheight('.fix_height3');
        equalheight('.fix_height4');
    });

    $('.checkbox_left').on('change', '.refresh_height', function () {
        equalheight('.fix_height');
        equalheight('.fix_height1');
        equalheight('.fix_height2');
        equalheight('.fix_height3');
        equalheight('.fix_height4');
    });

    /*****************************************************************/
    /********************* RESPONSIVE TABS ***************************/
    /*****************************************************************/


    if ($('div#mySliderTabs').length != 0) {
        var slider = $("div#mySliderTabs").sliderTabs({
            autoplay: false,
            mousewheel: false,
            position: "top"
        });
    }

    /*****************************************************************/
    /************************ NICE SCROLL ****************************/
    /****** SCROLL PERSONALIZADO QUE SALE EN EL PERFIL DEL AGENTE ****/
    /*****************************************************************/

    var nice = $("").niceScroll();  // The document page (body)

    $("#div1").html($("#div1").html() + ' ' + nice.version);

    $("#scroll_1").niceScroll({ cursorborder: "", cursorcolor: "#8D8D8E", boxzoom: false }); // First scrollable DIV	


    /*****************************************************************/
    /************************* EDIT PROFILE **************************/
    /*****************************************************************/
    // ~ .content_tabs input[type="text"], .tabs input.tab-selector-6:checked ~ .content_tabs select, .tabs input.tab-selector-6:checked ~ .content_tabs textarea 
    var isIllustrationList = window.location.href.indexOf('Illustrations.aspx') > 0;
    if (isIllustrationList) {
        var $hdnCounterFreQ = $("#hdnCounterFreQ");
        var Freq = parseInt($hdnCounterFreQ.val()) * 60 * 1000;
        var Intervalo = setInterval("setCounterQuotationInbox();", Freq);
    }
});

setEndorsementBeneficaryAutoComplete = function () {

    $("#txtSelectBenficiary").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetEndorsementBenficiary",
                data: JSON.stringify({
                    description: $.trim($("#txtSelectBenficiary").val()),
                    _LanguageId: $("#hdnLang").val()
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    var txtSelectBenficiary = $("#txtSelectBenficiary");
                    txtSelectBenficiary.css("background-repeat", "no-repeat");
                    txtSelectBenficiary.css("background-position", "right");
                    txtSelectBenficiary.css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.Text,
                            id: item.Value
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "500px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtSelectBenficiary").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            var DEndorsement = JSON.parse(ui.item.id);
            if (DEndorsement != null) {
                $("#txtSelectBenficiary").val('');
                $("#txtBeneficiario").val(ui.item.label);
                $("#txtRNC").val(DEndorsement.Rnc);
                $("#txtContactName").val(DEndorsement.ContactName);
                $("#txtPhoneNumber").val(DEndorsement.ContactTel);
                $("#txtEmailAddress").val(DEndorsement.ContactAdress);
            }
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 39 && event.which != 40 && event.which != 38) {

        }
    });
};

setAgentsAutoComplete = function () {
    $("#txtAgentsOrSubscriptor").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetAgents",
                data: JSON.stringify({
                    description: $.trim($("#txtAgentsOrSubscriptor").val()),
                    _LanguageId: $("#hdnLang").val(),
                    pAgentId: $("#LoginAgentId").val(),
                    BusinessLineId: Number($('#hdnBusinessLineId').val())
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    var txtAgentsOrSubscriptor = $("#txtAgentsOrSubscriptor");
                    txtAgentsOrSubscriptor.css("background-repeat", "no-repeat");
                    txtAgentsOrSubscriptor.css("background-position", "right");
                    txtAgentsOrSubscriptor.css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtAgentsOrSubscriptor").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $("#txtAgentsOrSubscriptor").val(ui.item.description);
            $("#drpAssignIllustrationsSubscribers").val(ui.item.id);
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 39 && event.which != 40 && event.which != 38) {
            $("#drpAssignIllustrationsSubscribers").val("-1");
        }
    });
};

setPorcentajeDescuento = function () {

    $("#txtPorcentajeDescuento").autocomplete({
        source: function (request, response) {

            var _corpId = Number($.trim($("#hdnCorpId").val())),
                _role = $.trim($("#hdnDiscountRole").val()),
                _percent = request.term;

            $.ajax({
                url: "../../SearchMethods.asmx/GetPercentages",
                data: JSON.stringify({ corpId: _corpId, role: _role, percent: _percent }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                beforeSend: function () {
                    var span = $(".waiting_percentage");
                    span.css("background-repeat", "no-repeat");
                    span.css("background-position", "right");
                    span.css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        var datos = item.split('|');
                        return {
                            label: datos[1],
                            val: datos[0],
                            json: datos[2]
                        }
                    }));
                    $(".waiting_percentage").css("background-image", "");
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            if ($("#ddlPorcentajeDescuento") != null)
                $("#ddlPorcentajeDescuento").val(i.item.json).change();
        },
        minLength: 1,
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        if (event.which != 13 && event.which != 37 && event.which != 39 && event.which != 40 && event.which != 38) {
            if ($("#ddlPorcentajeDescuento") != null)
                $("#ddlPorcentajeDescuento").val('0').change();
        }
    });
}

setOccupationAutoComplete = function () {
    $("#txtOccupation").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupation",
                data: JSON.stringify({
                    description: $.trim($("#txtOccupation").val()), _LanguageId: $("#hdnLang").val()
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtOccupation").css("background-repeat", "no-repeat");
                    $("#txtOccupation").css("background-position", "right");
                    $("#txtOccupation").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value,
                            descGroup: item.OccupationGroupDesc,
                            GroupId: item.OccupationGroupId
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtOccupation").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $("#txtProfession").val(ui.item.descGroup);
            $("#hdnOccupationId").val(ui.item.id);
            $("#hdnOccupationGroupId").val(ui.item.GroupId);
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 38 && event.which != 39 && event.which != 40) {
            $("#hdnOccupationId").val("");
            $("#hdnOccupationGroupId").val("");
            $("#txtProfession").val("");
            changeBorderColor($("#txtProfession"));
        }
    });
};

setOccupationAutoComplete2 = function () {
    $("#txtOccupation_Legal").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupation",
                data: JSON.stringify({
                    description: $.trim($("#txtOccupation_Legal").val()), _LanguageId: $("#hdnLang").val()
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtOccupation_Legal").css("background-repeat", "no-repeat");
                    $("#txtOccupation_Legal").css("background-position", "right");
                    $("#txtOccupation_Legal").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value,
                            descGroup: item.OccupationGroupDesc,
                            GroupId: item.OccupationGroupId
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtOccupation_Legal").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $("#txtProfession_Legal").val(ui.item.descGroup);
            $("#hdnOccupationId_Legal").val(ui.item.id);
            $("#hdnOccupationGroupId_Legal").val(ui.item.GroupId);
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 38 && event.which != 39 && event.which != 40) {
            $("#hdnOccupationId_Legal").val("");
            $("#hdnOccupationGroupId_Legal").val("");
            $("#txtProfession_Legal").val("");
            changeBorderColor($("#txtProfession_Legal"));
        }
    });
};

setPositionnAutoComplete = function () {
    $("#txtPosition").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupation",
                data: JSON.stringify({
                    description: $.trim($("#txtPosition").val()), _LanguageId: $("#hdnLang").val()
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtPosition").css("background-repeat", "no-repeat");
                    $("#txtPosition").css("background-position", "right");
                    $("#txtPosition").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value,
                            descGroup: item.OccupationGroupDesc,
                            GroupId: item.OccupationGroupId
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtPosition").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    });
};

setPositionnAutoCompleteDesignated = function () {
    $("#txtPosition_Designated").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupation",
                data: JSON.stringify({
                    description: $.trim($("#txtPosition_Designated").val()), _LanguageId: $("#hdnLang").val()
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtPosition_Designated").css("background-repeat", "no-repeat");
                    $("#txtPosition_Designated").css("background-position", "right");
                    $("#txtPosition_Designated").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value,
                            descGroup: item.OccupationGroupDesc,
                            GroupId: item.OccupationGroupId
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtPosition_Designated").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    });
};

setPositionnAutoCompletelegal = function () {
    $("#txtPosition_legal").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupation",
                data: JSON.stringify({
                    description: $.trim($("#txtPosition_legal").val()), _LanguageId: $("#hdnLang").val()
                }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtPosition_legal").css("background-repeat", "no-repeat");
                    $("#txtPosition_legal").css("background-position", "right");
                    $("#txtPosition_legal").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value,
                            descGroup: item.OccupationGroupDesc,
                            GroupId: item.OccupationGroupId
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtPosition_legal").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    });
};

setOccupationTypeAutoComplete = function () {
    $("#txtProfession").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupationType",
                data: JSON.stringify({ description: $.trim($("#txtProfession").val()) }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtProfession").css("background-repeat", "no-repeat");
                    $("#txtProfession").css("background-position", "right");
                    $("#txtProfession").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtProfession").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            if ($("#ddl_WUC_PI_Profession") != null)
                $("#ddl_WUC_PI_Profession").val(ui.item.id).change();

            if ($("#ddlProfession") != null)
                $("#ddlProfession").val(ui.item.id).change();

            if ($("#ddlOccupationTpye") != null)
                $("#ddlOccupationTpye").val(ui.item.id).change();
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 39 && event.which != 40 && event.which != 38) {
            if ($("#ddl_WUC_PI_Occupation") != null)
                $("#ddl_WUC_PI_Occupation option").remove();

            if ($("#ddlOccupation") != null)
                $("#ddlOccupation option").remove();

            changeBorderColor($("#txtProfession"));
        }
    });
};


setOccupationTypeAutoComplete2 = function () {
    $("#txtProfession_Legal").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetOccupationType",
                data: JSON.stringify({ description: $.trim($("#txtProfession_Legal").val()) }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtProfession_Legal").css("background-repeat", "no-repeat");
                    $("#txtProfession_Legal").css("background-position", "right");
                    $("#txtProfession_Legal").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.description,
                            id: item.value
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "200px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtProfession_Legal").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            if ($("#ddl_WUC_PI_Profession") != null)
                $("#ddl_WUC_PI_Profession").val(ui.item.id).change();

            if ($("#ddlProfession") != null)
                $("#ddlProfession").val(ui.item.id).change();

            if ($("#ddlOccupationTpye") != null)
                $("#ddlOccupationTpye").val(ui.item.id).change();
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 39 && event.which != 40 && event.which != 38) {
            if ($("#ddl_WUC_PI_Occupation") != null)
                $("#ddl_WUC_PI_Occupation option").remove();

            if ($("#ddlOccupation") != null)
                $("#ddlOccupation option").remove();

            changeBorderColor($("#txtProfession_Legal"));
        }
    });
};

//Autocomplete para el ABA number
setABAautoComplete = function () {
    $("#txtABANumber").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "../../SearchMethods.asmx/GetBankABANumber",
                data: JSON.stringify({ abaNumber: $.trim($("#txtABANumber").val()) }),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                beforeSend: function () {
                    $("#txtABANumber").css("background-repeat", "no-repeat");
                    $("#txtABANumber").css("background-position", "right");
                    $("#txtABANumber").css("background-image", "url('../../images/ui-anim_basic_16x16.gif')");
                },
                success: function (data) {
                    response($.map(data.d, function (item) {
                        return {
                            label: item.AbaNumber,
                            id: item.BankDesc
                        };
                    }));

                    $(".ui-autocomplete").css({ "height": "285px", "overflow-y": "scroll", "overflow-x": "hidden" });
                    $("#txtABANumber").css("background-image", "");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        },
        minLength: 3,
        select: function (event, ui) {
            $("#txtBankName").val(ui.item.id);
            changeBorderColor($("#txtBankName"));
        },
        response: function (event, ui) {
            var len = ui.content.length;
        },
        delay: 5
    }).on('keyup', function (event) {
        //Limpiar los campos
        if (event.which != 13 && event.which != 37 && event.which != 39 && event.which != 40 && event.which != 38) {
            $("#txtBankName").val("");
            changeBorderColor($("#txtBankName"));
        }
    });

};

/*****************************************************************/
/*************************** FUNCIONES ***************************/
/*****************************************************************/

function getWidth() {

    $('fieldset .details_grid').width(($('.content_fondo_blanco').width() - 20))

    $(window).resize(function () {
        //console.log($( window ).width());
        $('fieldset .details_grid').width(($('.content_fondo_blanco').width() - 20))
    });
}

/*****************************************************************/
/************************* FIX HEIGHT ****************************/
/********** Altos iguales para todos los elementos ***************/
/*********** que contengan la clase "fix_height" *****************/
/*****************************************************************/
equalheight = function (container) {

    var currentTallest = 0,
         currentRowStart = 0,
         rowDivs = new Array(),
         $el,
         topPosition = 0;
    $(container).each(function () {

        $el = $(this);
        $el.height('auto');
        topPostion = $el.position().top;

        if (currentRowStart != topPostion) {
            for (currentDiv = 0 ; currentDiv < rowDivs.length ; currentDiv++) {
                rowDivs[currentDiv].height(currentTallest);
            }
            rowDivs.length = 0; // empty the array
            currentRowStart = topPostion;
            currentTallest = $el.height();
            rowDivs.push($el);
        } else {
            rowDivs.push($el);
            currentTallest = (currentTallest < $el.height()) ? ($el.height()) : (currentTallest);
        }
        for (currentDiv = 0 ; currentDiv < rowDivs.length ; currentDiv++) {
            rowDivs[currentDiv].height(currentTallest);
        }
    });
}

function fixheight() {
    equalheight('.fix_height');//siempre que la cantidad de columnas sean las mismas en todos los pisos
    equalheight('.fix_height1');//si varian de un piso a otro, ej arriba tengo 2 abajo 3 abajo debo llamar fix_height1 etc
    equalheight('.fix_height2');
    equalheight('.fix_height3');
    equalheight('.fix_height4');
}

/*****************************************************************/
/************************ ACCORDION ****************************/
/*****************************************************************/

$("html").addClass("js");

function validateDateRange(FromTxt, Totxt, sender) {
    var lang = $("#hdnLang").val();
    if (FromTxt.length > 0 && Totxt.length > 0) {
        if (FromTxt.val() != "" && Totxt.val() != "") {

            var DateFrom = FromTxt.val().ToDate("mm/dd/yyyy");
            var DateTo = Totxt.val().ToDate("mm/dd/yyyy");
            if (DateTo < DateFrom) {
                $(sender).val("");
                $(sender).focus();
                var message = lang == "en" ? "The deadline can not be less than the initial date" : "La fecha final no puede ser menor que la fecha inicial";
                CustomDialogMessageEx(message, null, null, true, lang == "en" ? "Warning" : "Advertencia");
                return false;
            }
        }
    }
}

function setAccordeaons() {
    $.fn.accordion.defaults.container = false;
    $(ObjAcordeon).accordion({
        initShow: "#current",
        collapsible: true
    });
}
/*****************************************************************/
/************************ DATE PICKER ****************************/
/*****************************************************************/

DatePickerEachFunc = function () {
    //if (!$(this).prop("disabled"))   
    $(this).inputmask("mm/dd/yyyy", { "placeholder": "mm/dd/yyyy", "clearIncomplete": true });

    if (this.getAttribute("date") == "birth")
        $(this).datepicker("option", "maxDate", "0");

    if (this.getAttribute("maxDate") != "" && this.getAttribute("maxDate") != null) {
        var maxDate = this.getAttribute("maxDate");
        $(this).datepicker("option", "maxDate", maxDate);
        $(this).datepicker("option", "defaultDate", maxDate);
    }
}

SetDatePicker = function () {

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

    $(".datepicker").datepicker({
        defaultDate: $("#hdnGetDate").val(),
        changeMonth: true,
        changeYear: true,
        yearRange: "c-100:c+100",
        onSelect: function (selectedDate) {
            changeBorderColor(this);
            CallExecuteOnCloseEvent(this, selectedDate);
        },
        onClose: function (selectedDate) {
            changeBorderColor(this);
            CallExecuteOnCloseEvent(this, selectedDate);
        },
        beforeShow: function () {
            SavePosDatePicker(this);
        }
    }).each(DatePickerEachFunc);
};

SavePosDatePicker = function (Sender) {
    var Obj = $(Sender);
    setTimeout(function () {
        var datePickerTop = $("#ui-datepicker-div").offset().top;

        var inputTop = $(Obj).offset().top;

        var AbajoOrArriba = (datePickerTop > inputTop) ? "AB" : "AR";

        $("#CurrentDatePickerShow").val("#" + $(Obj).attr("id") + "," + AbajoOrArriba);
    }, 40);
}

setCurrentAccordeon = function (obj, hiddenfield) {

    var index = 0;

    var parent = $(obj).parents()[1];

    var isOpen = $(obj).parent().find("ul:first").css("display") == "block";

    if (!isOpen) {
        $(parent).find("li").find("a[lnk='lnk']").removeAttr("alt");

        //Marcar el Objeto como abierto
        $(obj).attr("alt", "Open");

        var hrefArray = $(parent).find("li").find("a[lnk='lnk']");

        var divParent = $(parent).attr("id");

        for (var x = 0; x <= hrefArray.length - 1; x++) {
            if ($(hrefArray[x]).attr("alt") == "Open") {
                $(hiddenfield).val(divParent + "|" + x);
                break;
            }
        }
    }

    var intervalo = setInterval("fixheight", 100);
    setTimeout(function () { clearInterval(intervalo) }, 400);

};

setCurrentAccordeonForIndex = function (hiddenfield) {
    if ($(hiddenfield).val() != "" & $(hiddenfield).val() != null) {
        var divActiveAccordeon = $(hiddenfield).val().split("|")[0];

        var ActiveAccordeonIndex = $(hiddenfield).val().split("|")[1];

        var lnk = $("#" + divActiveAccordeon + " > li").find("a[lnk='lnk']");

        $(lnk).attr("id", "");

        $(lnk[ActiveAccordeonIndex]).addClass("shown").addClass("open").attr("id", "current");
    }

    //$(ObjAcordeon).accordion({ initShow: "#current" });

};

setCurrentTabForIndex = function (hiddenfield) {
    if ($(hiddenfield).val() != "" & $(hiddenfield).val() != null) {
        var divActiveAccordeon = $(hiddenfield).val().split("|")[0];

        var ActiveAccordeonIndex = $(hiddenfield).val().split("|")[1];

        var lnk = $("#" + divActiveAccordeon + " > li").find("a[lnk='lnk']");

        $(lnk).attr("id", "");

        $($(lnk[ActiveAccordeonIndex]).addClass("selected").attr("href")).addClass("selected");
    }

    $(ObjAcordeon).accordion({ initShow: "#current" });
};

counterDisplay = function (dta) {
    for (var i = 0; i < dta.length; i++) {
        var $parentObj = $(dta[i].Tab).parent();
        var $i = $parentObj.find("i:first");
        var HdnId = replaceAll('#lnk', 'hdn', dta[i].Tab);
        var Selector = "#" + HdnId + "Count";
        var $hidden = $(Selector);
        $i.removeAttr("style");
        var counter = dta[i].Count;
        $i.text(number_format(counter, 0));
        $hidden.val(counter);
    }
};

setCounterQuotationInbox = function () {
    var vCss = { "background-repeat": "no-repeat", "background-position": "center", "background-image": "url('../../images/ui-anim_basic_16x16.gif')" };
    var Param = $("#hdnParameters").val();

    $.ajax({
        url: "../../SearchMethods.asmx/GetCounter",
        data: JSON.stringify({ Parameters: Param }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        beforeSend: function () {
            var $lnkIllustrationsToWork = $("#lnkIllustrationsToWork");
            var $i = $lnkIllustrationsToWork.parent().find("i:first");
            $i.css(vCss);

            var $lnkCompleteIllustrations = $("#lnkCompleteIllustrations");
            var $i = $lnkCompleteIllustrations.parent().find("i:first");
            $i.css(vCss);

            var $lnkDeclinedByClient = $("#lnkDeclinedByClient");
            var $i = $lnkDeclinedByClient.parent().find("i:first");
            $i.css(vCss);

            var $lnkExpired = $("#lnkExpired");
            var $i = $lnkExpired.parent().find("i:first");
            $i.css(vCss);

            var $lnkExpiring = $("#lnkExpiring");
            var $i = $lnkExpiring.parent().find("i:first");
            $i.css(vCss);

            var $lnkSubscriptions = $("#lnkSubscriptions");
            var $i = $lnkSubscriptions.parent().find("i:first");
            $i.css(vCss);

            var $lnkDiscounts = $("#lnkDiscounts");
            var $i = $lnkDiscounts.parent().find("i:first");
            $i.css(vCss);

            var $lnkConfirmationCall = $("#lnkConfirmationCall");
            var $i = $lnkConfirmationCall.parent().find("i:first");
            $i.css(vCss);

            var $lnkMissingDocuments = $("#lnkMissingDocuments");
            var $i = $lnkMissingDocuments.parent().find("i:first");
            $i.css(vCss);

            var $lnkMissingInspections = $("#lnkMissingInspections");
            var $i = $lnkMissingInspections.parent().find("i:first");
            $i.css(vCss);

            var $lnkFacultativesCases = $("#lnkFacultative");
            var $i = $lnkFacultativesCases.parent().find("i:first");
            $i.css(vCss);

            var $lnkDeclinedBySubscription = $("#lnkDeclinedBySubscription");
            var $i = $lnkDeclinedBySubscription.parent().find("i:first");
            $i.css(vCss);

            var $lnkApprovedBySubscription = $("#lnkApprovedBySubscription");
            var $i = $lnkApprovedBySubscription.parent().find("i:first");
            $i.css(vCss);

            var $lnkHistoricalIllustrations = $("#lnkHistoricalIllustrations");
            var $i = $lnkHistoricalIllustrations.parent().find("i:first");
            $i.css(vCss);

            var $lnkPuntoVentaTab = $("#lnkPuntoVentaTab");
            var $i = $lnkPuntoVentaTab.parent().find("i:first");
            $i.css(vCss);

        },
        success: function (data) {
            var dta = data.d;
            counterDisplay(dta);
            SetCounters();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            //CustomDialogMessage(xhr.responseText);
        }
    });
};

function DoPostBack(obj) {
    __doPostBack($(obj).attr('name'), '');
}

function ClearFilter(obj) {
    gridList.ClearFilter();
    setTimeout(function () { DoPostBack(obj) }, 1500);
    return false;
}

function SetCounters() {
    //Sumatoria de los grupos
    var IllustrationsToWorkCount = parseInt($("#hdnIllustrationsToWorkCount").val());
    var CompleteIllustrationsCount = parseInt($("#hdnCompleteIllustrationsCount").val());
    var DeclinedByClientCount = parseInt($("#hdnDeclinedByClientCount").val());
    var ExpiredCount = parseInt($("#hdnExpiredCount").val());
    var ExpiringCount = parseInt($("#hdnExpiringCount").val());

    var vPreSuscripcion = IllustrationsToWorkCount + CompleteIllustrationsCount + DeclinedByClientCount + ExpiredCount + ExpiringCount;

    $("#hdnPreSuscripcionCount").val(vPreSuscripcion);
    $("#PreSuscripcion").text(number_format(vPreSuscripcion, 0));

    var SubscriptionsCount = parseInt($("#hdnSubscriptionsCount").val());
    var DiscountsCount = parseInt($("#hdnDiscountsCount").val());
    var ConfirmationCallCount = parseInt($("#hdnConfirmationCallCount").val());
    var MissingDocumentsCount = parseInt($("#hdnMissingDocumentsCount").val());
    var MissingInspectionsCount = parseInt($("#hdnMissingInspectionsCount").val());
    var FacultativesCasesCount = parseInt($("#hdnFacultativeCount").val());

    var vSuscripcion = SubscriptionsCount + DiscountsCount + ConfirmationCallCount + MissingDocumentsCount + MissingInspectionsCount + FacultativesCasesCount;

    $("#hdnSuscripcionCount").val(vSuscripcion);
    $("#Suscripcion").text(number_format(vSuscripcion, 0));

    var DeclinedBySubscriptionCount = parseInt($("#hdnDeclinedBySubscriptionCount").val());
    var ApprovedBySubscriptionCount = parseInt($("#hdnApprovedBySubscriptionCount").val());
    var hdnHistoricalIllustrationsCount = parseInt($("#hdnHistoricalIllustrationsCount").val());
    var PuntoVentaTabCount = parseInt($("#hdnPuntoVentaTabCount").val());

    var vHistorical = DeclinedBySubscriptionCount + ApprovedBySubscriptionCount + hdnHistoricalIllustrationsCount + PuntoVentaTabCount;

    $("#hdnHistoricoCount").val(vHistorical);
    $("#Historico").text(number_format(vHistorical, 0));
}

getToolTipMessage = function (key, success) {
    $.ajax({
        url: "../../SearchMethods.asmx/GetToolTipMessage",
        async: false,
        data: JSON.stringify({ key: $.trim(key), lang: $.trim($("#hdnLang").val()) }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        beforeSend: function () {

        },
        success: success,
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            //CustomDialogMessage(xhr.responseText);
        }
    });
}

GenerateDocTransunion = function (html, dReq, success) {
    $.ajax({
        url: "../../SearchMethods.asmx/GenerateDocTransunion",
        data: JSON.stringify({ HtmlString: html, dataReq: dReq }),
        dataType: "json",
        type: "POST",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        beforeSend: function () {
            BeginRequestHandler();
        },
        success: success,
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            //CustomDialogMessage(xhr.responseText);
        }
    });
};

getTranslate = function (key, success) {
    $.ajax({
        url: "../../SearchMethods.asmx/GetTranslate",
        data: JSON.stringify({ key: $.trim(key), lang: $.trim($("#hdnLang").val()) }),
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataFilter: function (data) { return data; },
        beforeSend: function () {

        },
        success: success,
        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr);
            //CustomDialogMessage(xhr.responseText);
        }
    });
};

function CallPrint(obj) {
    var id = "iframePrinter";
    var iframePrinter = $("#" + id);
    iframePrinter.remove();
    iframePrinter = $("<iframe/>").attr({ src: obj.src, id: id });
    $("body").append(iframePrinter)
    iframePrinter[0].contentWindow.print();
    //iframe.remove();
}

function configurePivotColumn() {
    $("td[id*='_C']:contains('-')").each(replaceDataPivotGrid);
}

function replaceDataPivotGrid(index, obj) {
    if (obj.innerText != undefined)
        obj.innerText = obj.innerText
        .replace("00-", "")
        .replace("01-", "")
        .replace("02-", "")
        .replace("03-", "")
        .replace("04-", "")
        .replace("05-", "")
        .replace("06-", "")
        .replace("07-", "")
        .replace("08-", "")
        .replace("09-", "")
        .replace("10-", "")
        .replace("11-", "")
        .replace("12-", "")
    else
        obj.textContent = obj.textContent
    .replace("00-", "")
    .replace("01-", "")
    .replace("02-", "")
    .replace("03-", "")
    .replace("04-", "")
    .replace("05-", "")
    .replace("06-", "")
    .replace("07-", "")
    .replace("08-", "")
    .replace("09-", "")
    .replace("10-", "")
    .replace("11-", "")
    .replace("12-", "")
}

function addtxtFilterToFilterGrid() {
    var firstTd = $("div.dxpcLite_DevEx.dxpclW:visible td.dxpgFilterItem_DevEx:first");
    if (!firstTd.length) return;
    var txtFilter = $("<input type='text' placeholder='Search...'/>");
    txtFilter.css("width", "90%").css("margin-left", "5px").css("margin-top", "5px");
    txtFilter.keyup(function () {
        if (this.value == "")
            $("div.dxpcLite_DevEx.dxpclW:visible td.dxpgFilterItem_DevEx label").parent().parent().show();
        else {
            $("div.dxpcLite_DevEx.dxpclW:visible td.dxpgFilterItem_DevEx label:contains('" + this.value + "')").parent().parent().show();
            $("div.dxpcLite_DevEx.dxpclW:visible td.dxpgFilterItem_DevEx label:not(:contains('" + this.value + "'))").parent().parent().hide();
        }
    });
    firstTd.parent().before(txtFilter);
}

function setActiveClassTabsNewbusiness(activeTab) {
    var tabs = ["#liPuntoVenta", "#VidaFunerarios", "#SaludInternacional", "#Comerciales"];
    $.each(tabs, function (index, item) {
        if (item != activeTab) {
            $(item).removeClass('active');
        }
    });

    $(activeTab).addClass('active');
}

function addClassDoble() {
    var l = $(".addDobleDynamic .label_plus_input.par span");

    l.each(function () {

        var $this = $(this);
        var lenthSpan = $this.html().length;

        if (lenthSpan > 33) {
            $this.parent().addClass('doble40');
        } else {
            $this.parent().removeClass('doble40');
        }
    });

}

/*MAPA DE GOOGLE*/
function searchAddressLatLon(latlon) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'latLng': latlon }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            result = results[0].formatted_address; // reference LatLng value
            var $elementToPutAddress = $("#ltSelectedAddressOnMap");
            if ($elementToPutAddress != undefined)
                $elementToPutAddress.text("Dirección seleccionada en el mapa : " + result);
            //results[0].geometry.location
            //$('#HFlista').val(results[0].formatted_address);          
            //createMarker(myResult); // call the function that adds the marker
            //map.setCenter(myResult);
            //map.setZoom(17);
        }
    });
}

function searchAddress(address, callBack) {
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            result = results[0].formatted_address; // reference LatLng value          
            //var lat = results[0].geometry.location.lat;
            //var lon = results[0].geometry.location.lng;
            if (callBack != undefined)
                callBack(results[0].geometry.location);
            //$('#HFlista').val(results[0].formatted_address);
            //(myResult); // call the function that adds the marker
            //map.setCenter(myResult);
            //map.setZoom(17);
        }
    });
}


function createMarker(myLatLng, Map) {
    var marker = new google.maps.Marker({
        position: myLatLng,
        map: Map,
        draggable: true
        // icon: image
    });

    //google.maps.event.addListener(marker, 'dragend', function (event) {
    //    var Lat = (event.latLng.lat());
    //    var Lon = (event.latLng.lng());
    //    $('#hdnlatitud').val(Lat);
    //    $('#hdnlongitud').val(Lon);
    //    var LatLng = new google.maps.LatLng(Lat, Lon);
    //    //var $txt = $("#txtInspectionAddress");
    //    //searchAddressLatLon(LatLng, $txt);
    //});
}

function showInputTextSearch() {
    var $dv = $('#dvInputSearch');
    var IsOpen = $dv.data("status") == 'open';
    var $txtAddressSearchMap = $("#txtAddressSearchMap");

    if (!IsOpen) {
        $dv.fadeIn(200);
        $dv.data("status", "open");
        $txtAddressSearchMap.attr("placeHolder", "Escriba la dirección Pais, Ciudad , Sector, Calle");
        $txtAddressSearchMap.focus();
    }
    else {
        $dv.hide();
        $dv.data("status", "closed");
    }
}

function initializeSubmitMap($div, pLatpLon, AllowSearch, AllowDraggableMarker, SearchTxtVisible, plat, plon) {
    var element = $div;
    var $txtpAdress = null;
    var myLatLng = null;
    var $element = $(element);
    $element.html('');
    var $dvInputSearch = $element.parent().find("div[id='dvInputSearch']");

    if (SearchTxtVisible != undefined && SearchTxtVisible)
        $dvInputSearch.show();
    else
        $dvInputSearch.hide();

    if ($("#txtAddressSearchMap").length <= 0)
        $dvInputSearch.append("<input type='text' id='txtAddressSearchMap'/>");

    if ($("#lnkSearch").length <= 0) {
        var button = "<a  onclick='showInputTextSearch()' id='lnkSearch' style='text-decoration: underline;cursor: pointer;color:blue;'> Buscar en el mapa </a>";
        var $dvParent = $dvInputSearch.parent();
        $dvParent.prepend(button);
        $dvParent.prepend("<br/>");
        $txtpAdress = document.getElementById('txtAddressSearchMap');
        $($txtpAdress).attr("placeHolder", "Escriba la dirección Pais, Ciudad , Sector, Calle")
        $aLink = $dvInputSearch.prev();
    }

    if (AllowSearch != undefined && AllowSearch == true) {
        $aLink.attr("onclick", "showInputTextSearch()");
        $aLink.css('color', 'blue');
    }
    else {
        $aLink.removeAttr("onclick");
        $aLink.css({ 'color': 'transparent', 'cursor': 'pointer' });
    }

    if (element != null) {
        myLatLng = new google.maps.LatLng(18.47321276333223, -69.85608376289065);
        var mapOptions = {
            zoom: 15,
            center: myLatLng,
            // This is where you would paste any style found on Snazzy Maps.
            styles: [{ "featureType": "water", "elementType": "geometry", "stylers": [{ "color": "#a0d6d1" }, { "lightness": 17 }] }, { "featureType": "landscape", "elementType": "geometry", "stylers": [{ "color": "#f2f2f2" }, { "lightness": 20 }] }, { "featureType": "road.highway", "elementType": "geometry.fill", "stylers": [{ "color": "#dedede" }, { "lightness": 17 }] }, { "featureType": "road.highway", "elementType": "geometry.stroke", "stylers": [{ "color": "#dedede" }, { "lightness": 29 }, { "weight": 0.2 }] }, { "featureType": "road.arterial", "elementType": "geometry", "stylers": [{ "color": "#dedede" }, { "lightness": 18 }] }, { "featureType": "road.local", "elementType": "geometry", "stylers": [{ "color": "#ffffff" }, { "lightness": 16 }] }, { "featureType": "poi", "elementType": "geometry", "stylers": [{ "color": "#f1f1f1" }, { "lightness": 21 }] }, { "elementType": "labels.text.stroke", "stylers": [{ "visibility": "on" }, { "color": "#ffffff" }, { "lightness": 16 }] }, { "elementType": "labels.text.fill", "stylers": [{ "saturation": 36 }, { "color": "#333333" }, { "lightness": 40 }] }, { "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "geometry", "stylers": [{ "color": "#f2f2f2" }, { "lightness": 19 }] }, { "featureType": "administrative", "elementType": "geometry.fill", "stylers": [{ "color": "#fefefe" }, { "lightness": 20 }] }, { "featureType": "administrative", "elementType": "geometry.stroke", "stylers": [{ "color": "#fefefe" }, { "lightness": 17 }, { "weight": 1.2 }] }],

            //Extra options
            //mapTypeControl: false,
            panControl: false,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.SMALL,
                position: google.maps.ControlPosition.LEFT_BOTTOM
            }
        };
        map = new google.maps.Map(element, mapOptions);

        if (pLatpLon == undefined)
            pLatpLon = myLatLng;

        var marker = new google.maps.Marker({
            position: pLatpLon,
            map: map,
            draggable: AllowDraggableMarker == undefined ? false : AllowDraggableMarker
            // icon: image
        });

        map.setZoom(10);
        map.panTo(marker.position);

        google.maps.event.addListener(marker, 'dragend', function (event) {
            $('#hdnlatitud').val(event.latLng.lat());
            $('#hdnlongitud').val(event.latLng.lng());
            var LatLng = new google.maps.LatLng($('#hdnlatitud').val(), $('#hdnlongitud').val());
            searchAddressLatLon(LatLng);
        });

        var input = $txtpAdress;
        var autocomplete = new google.maps.places.Autocomplete(input);
        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            var place = autocomplete.getPlace();
            $('#hdnlatitud').val(place.geometry.location.lat());
            $('#hdnlongitud').val(place.geometry.location.lng());
            marker.setPosition(place.geometry.location);
            map.setCenter(place.geometry.location);
            map.setZoom(15);
            var LatLng = new google.maps.LatLng($('#hdnlatitud').val(), $('#hdnlongitud').val());
            searchAddressLatLon(LatLng);
        });
    }
}

function GetCurrentLocation(CallBack) {
    try {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (objPosition) {
                var lon = objPosition.coords.longitude;
                var lat = objPosition.coords.latitude;

                if (CallBack != undefined)
                    CallBack(lat, lon);

            }, function (objPositionError) {
                switch (objPositionError.code) {
                    case objPositionError.PERMISSION_DENIED:
                        CustomDialogMessage("No se ha permitido el acceso a la posición del usuario.", null);
                        break;
                    case objPositionError.POSITION_UNAVAILABLE:
                        CustomDialogMessage("No se ha podido acceder a la información de su posición.", null);
                        break;
                    case objPositionError.TIMEOUT:
                        CustomDialogMessage("El servicio ha tardado demasiado tiempo en responder.", null);
                        break;
                    default:
                        CustomDialogMessage("Error desconocido.", null);
                }
            }, {
                maximumAge: 75000,
                timeout: 15000
            });
        }
        else {
            CustomDialogMessage("Su navegador no soporta la API de geolocalización.", null);
        }
    } catch (e) {
        CustomDialogMessage(e.message, null);
    }   
}
/*FIN MAPA DE GOOGLE*/

function IsAMobileDevice() {
    var isMobile = false; //initiate as false
    // device detection
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
        isMobile = true;
    }
    return isMobile
}