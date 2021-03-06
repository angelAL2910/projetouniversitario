﻿/*
  Author : Lic. Carlos Lebron
*/
var qtblVehicles = null;

Inclusioninitialize = function (Title) {

    $('#HideInHistory, #HideInHistory2').show();

    InitializeChosen();
    //InitializeCustom();

    if ($.fn.DataTable.isDataTable('#tblVehicles')) {
        $('#tblVehicles').DataTable().destroy();
    }


    var $filtroHistorico = $("#filtroHistorico");
    var $frmVehicleInclusion = $("#frmVehicleInclusion");
    var $selecCober = $("#selecCober");


    if (Title == "INCLUSIONES") {
        $filtroHistorico.val("3");
        $frmVehicleInclusion.show();
        $selecCober.show();
        $("#main").removeClass("onlyexclusion");
    } else if (Title == "EXCLUSIONES") {
        $filtroHistorico.val("4");
        $frmVehicleInclusion.hide();
        $selecCober.hide();
        $("#main").addClass("onlyexclusion");
    } else if (Title == "CAMBIOS") {
        $filtroHistorico.val("5");
        $frmVehicleInclusion.hide();
        $selecCober.hide();
        $("#main").addClass("onlyexclusion");
    }


    $filtroHistorico.trigger("change.select2");

    $("#btnAddEditVehicle").data("ActionMode", "Add");
    $("#btnSearch").click(function () { SearchPolicy() });
    $("#btnCancel").click(function () {
        var Title = $("#hdnTitle").val();
        LoadInclusion(false, Title);
    });

    $("#ddlMarca").change(function () {
        var $this = $(this);
        getVehiclesModelsByBrands($this.val());
    });

    $("#ddlModelo").change(function () {
        var $this = $(this);

        var brand = $("#ddlMarca").val();

        GetFuelType_Inclusion(null, brand, $this.val());
    });


    $("#ddlAnio").change(function () {
        var $this = $(this);
        var vehicleYear = $this.val();
        var brandId = $("#ddlMarca").val();
        var modelId = $("#ddlModelo").val();
        var SelectedAgent = JSON.parse($("#AgentList").val());
        var AgentCode = SelectedAgent.AgentCode;
        GetVehicleTypes_New(brandId, modelId, vehicleYear, AgentCode);
        enableVehicleOldCombo_Inclusion(vehicleYear);
    });

    $("#ddlType").change(function () {
        LoadUsages($(this));
    });

    $("#ddlPrincipalUsage").change(function () {
        var $thisTypeSeleted = $("#ddlType option:selected");
        var $this = $(this);
        var UsageValue = $("#ddlPrincipalUsage option:selected").text();
        var ProductByUsages = $thisTypeSeleted.data("ProductByUsages");
        var CoveragesByUsages = $thisTypeSeleted.data("CoveragesByUsages");
        var Products = $thisTypeSeleted.data("Products");

        var UsageFilterPlan = ProductByUsages.filter(function (valor) {
            return valor.UsoDescripcion == UsageValue;
        });
        var CoverageByUsageFilter = CoveragesByUsages.filter(function (valor) {
            return valor.UsoDescripcion == UsageValue;
        });

        $this.data("UsageFilterPlan", UsageFilterPlan);
        $this.data("CoverageByUsageFilter", CoverageByUsageFilter);
        $this.data("Products", Products);
    });

    $("#valor").on({
        focusin: function () {
            var $this = $(this);
            $this.parent().addClass("is-dirty");
        },
        focusout: function () {
            var $this = $(this);

            if ($this.val() == "")
                $this.parent().removeClass("is-dirty");
            else {
                $this.parent().parent().removeClass('requerido');
                $this.parent().parent().addClass('erarequerido');
            }
        }
    });

    $("#btnDissmisPayment").hide();
    var $AgentList = $("#AgentList");
    $AgentList.prop("disabled", true);
    //$AgentList.trigger("chosen:updated");
    $AgentList.trigger("change.select2");

    $('#ExcludeStartDate.datepicker').datepicker({
        changeMonth: true,
        changeYear: true,
        minDate: new Date(),
        //maxDate: new Date(),        
        onSelect: function (dateText) {
            var $parent = $(this).parent();
            $parent.addClass("is-dirty");
            $parent.removeClass('requerido');
            $parent.removeClass('erarequerido');
            $parent.addClass('erarequerido');
        }
    });


    $("#lnkContinue").off("click");

    $("#spQuotationNumber").html('');

    GlobalOnlyForEmission = false;

    componentHandler.upgradeAllRegistered();
}

SearchPolicy = function () {

    var IsSerch = $("#hdnCustomerDataInclusion").val() != "";

    if (IsSerch)
        return false;

    //Validar que se introduzca un numero de poliza
    var PolicyNumber = $("#nbPolicyNo").val();
    //Validar si es necesario el numero de identificación
    var $nbID = $("#nbID");
    var id = $nbID.val();

    var $OptionsRadios = $("#OptionsRadios");
    var $OptionsRadiosChecked = $OptionsRadios.find("input[type='radio']:checked");

    if ($OptionsRadios.hasClass("requerido") && $OptionsRadiosChecked.length == 0) {
        showWarning(['Por elija el tipo de Identificación'], 'Advertencia');
        return false;
    }

    if ($nbID.parent().hasClass('requerido') && id == "") {
        showWarning(['Por favor digite el número de Identificación'], 'Advertencia');
        return false;
    }

    if (PolicyNumber == "") {
        showWarning(['Por favor digite un número de Póliza'], 'Advertencia');
        return false;
    }
    var title = $("#hdnTitle").val();
    $.ajax({
        type: "POST",
        url: "/Inclusion/GetDataCustomerFromPolicy",
        data: JSON.stringify({ Id: id, PolicyNumber: PolicyNumber, Title: title }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var hasCode = !isNaN(data.code);
            if (hasCode) {
                var PolicyIsNotFound = data.code == "001";
                var ClientISnotCorrect = data.code == "002";
                var PolicyDontActive = data.code == "003";
                var UserDontAgentChain = data.code == "004";

                if (UserDontAgentChain) {
                    showWarning([data.msg], 'Adventencia');
                    return false;
                }

                if (!PolicyDontActive) {
                    var IsValidAgent = data.IsvalidAgent;

                    if (PolicyIsNotFound) {
                        showWarning([data.msg], 'Póliza no encontrada');
                        return false;
                    }

                    if (ClientISnotCorrect) {
                        showWarning([data.msg], 'Adventencia');
                        return false;
                    }

                    if (!IsValidAgent) {
                        showWarning(['El agente de esta póliza no esta en su cadena, comuniquese con el departamento tecnico'], 'Advertencia');
                        return false;
                    }
                } else {
                    showWarning([data.msg], 'Adventencia');
                    return false;
                }
            }

            $("#hdnIntermediarioId").val(data.DataCoreCustomer.Intermediario);
            var DataCustomer = JSON.stringify(data.DataCoreCustomer);
            $("#hdnCustomerDataInclusion").val(DataCustomer);
            $('#policyNoMain').val(PolicyNumber);
            var IsExclusion = data.IsExclusion;
            var IsVehicleChange = data.IsVehicleChange;

            LoadCustomerData(data.DataCoreCustomer, data.userAgenCode, PolicyNumber, "", IsExclusion, "", IsVehicleChange);
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

LoadCustomerData = function (DataCoreCustomer, userAgenCode, PolicyNumber, ExclusionStartDate, IsExclusion, sysflexVehiclesIdToExclude, IsVehicleChange) {
    //Cargar Data del cliente
    var isRNC = false;

    var $FullName = $("#FullName");
    $FullName.val(DataCoreCustomer.NombreCliente);
    $FullName.parent().addClass("is-dirty");

    var $Identification = $("#Identification");
    $Identification.val(DataCoreCustomer.RNC);
    $Identification.parent().addClass("is-dirty");

    var $labelId = $Identification.parent().find("label:first");
    var Tipo = DataCoreCustomer.IdentificationType;
    $labelId.text("Numero de Identificación ({0})".replace("{0}", Tipo));

    if (Tipo == "RNC") {
        isRNC = true;
    }

    var $DateOfBirth = $("#DateOfBirth");
    $DateOfBirth.val(moment(DataCoreCustomer.FechaNacimiento).format('DD-MMM-YYYY'));
    $DateOfBirth.parent().addClass("is-dirty");

    var $Sex = $("#Sex");
    $Sex.val(DataCoreCustomer.Sexo);
    $Sex.parent().addClass("is-dirty");

    var $LicenciaExt = $("#LicenciaExt");
    $LicenciaExt.val(DataCoreCustomer.Licencia_Extranjera);
    $LicenciaExt.parent().addClass("is-dirty");

    getVehiclesFromPolicy(PolicyNumber, IsExclusion, sysflexVehiclesIdToExclude, IsVehicleChange);

    if (isRNC) {
        $DateOfBirth.val("N/A");
        $Sex.val("N/A");
    }

    if (IsExclusion) {

        if (ExclusionStartDate) {
            var $ExcludeStartDate = $("#ExcludeStartDate");
            ExclusionStartDate = ExclusionStartDate.replace('.', '');

            $ExcludeStartDate.val(moment(ExclusionStartDate).format('DD-MMM-YYYY'));
            $ExcludeStartDate.parent().addClass("is-dirty");
        }
    } else if (IsVehicleChange) {

        $("#tblVehicles").on('click', '.vehicleCheck', function () {

            var $this = $(this);

            var $frmVehicleChanges = $("#frmVehicleChanges");

            var allchecks = $('.vehicleCheck');
            var checksCheckeds = 0;

            $.each(allchecks, function (index, obj) {
                var $this2 = $(this);
                if ($this2.is(':checked')) {
                    checksCheckeds += 1;
                }
            });

            if (checksCheckeds > 1) {
                showError(['No puede seleccionar mas de un vehiculo para modificar.'], 'Seleccionar Vehiculos Modificación');
                $this.prop('checked', false);
                $frmVehicleChanges.hide();
                return false;
            } else if (checksCheckeds == 1) {
                $frmVehicleChanges.show();

                var $Tbody = $("#tblVehicles").find("tbody");
                var dveh = null;

                $Tbody.find("tr").each(function () {
                    var $thisTR = $(this);
                    var $checkbox = $thisTR.find("input[type='checkbox']");
                    if ($checkbox.prop('checked')) {
                        dveh = $thisTR.data("dataVehicle");
                    }
                });

                setDataChangeVehicle(dveh);

            } else {
                $frmVehicleChanges.hide();
            }
        });
    }

    var $AgentList = $("#AgentList");
    $AgentList.find("option").each(function () {
        var $this = $(this);
        if ($this.text() == userAgenCode.FullNameAll) {
            $AgentList.val($this.val());
            $AgentList.trigger("change.select2");
            $AgentList.trigger("change");
            return false;
        }
    });
}

function LoadUsages($this) {

    var $this = $("#ddlType option:selected");
    var NewUsages = $this.data("NewUsages");
    var $ddlPrincipalUsage = $("#ddlPrincipalUsage");

    clearDrop($ddlPrincipalUsage);

    $.each(NewUsages, function (index, value) {
        var NewOption = '<option value="{0}">{1}</option>';
        NewOption = NewOption.replace("{0}", value.idUso);
        NewOption = NewOption.replace("{1}", value.descUso);
        $ddlPrincipalUsage.append(NewOption);
    });

    //$ddlPrincipalUsage.trigger("chosen:updated");
    $ddlPrincipalUsage.trigger("change.select2");
}

function clearDrop($ddl) {
    try {
        $ddl.find("option").remove();
        var NewOption = '<option value="">Seleccione</option>';
        var NewOptionChosen = '<option value=""></option>';
        var isChosen = $ddl.hasClass("chosen-select-deselect");
        $ddl.append(isChosen ? NewOptionChosen : NewOption);
        //$ddl.trigger("chosen:updated");
        $ddl.trigger("change.select2");
    } catch (e) {
        console.log(e.message);
    }
}

function InitializePartialControls() {
    $(".AllProducts").change(function () {
        var $this = $(this);
        var ProductName = $this.find("option:selected").text();
        var $ProductSelected = $("#ddlPrincipalUsage");
        var CoveragesByUsages = $ProductSelected.data("CoverageByUsageFilter");

        var $dvParent = $("#" + $this.attr("data"));
        var $ddlCoverage = $dvParent.find(".coverage");
        var $ddlDeducible = $dvParent.find(".Deducible");
        var CoveragesByUsagesFilter = CoveragesByUsages.filter(function (valor) {
            return valor.ProductName == ProductName;
        });

        clearDrop($ddlCoverage);
        clearDrop($ddlDeducible);

        $.each(CoveragesByUsagesFilter, function (index, value) {
            var NewOption = '<option value="{0}">{1}</option>';
            NewOption = NewOption.replace("{0}", value.Id);
            NewOption = NewOption.replace("{1}", value.Name);
            $ddlCoverage.append(NewOption);
        });

        $ddlCoverage.data("CoveragesByUsagesFilter", CoveragesByUsagesFilter);

        $ddlCoverage.change(function () {
            var $this = $(this);
            var coverageCoreId = $this.val();
            if (coverageCoreId != "") {
                var makeId = $("#hdnMakeId").val();
                var modelId = $("#hdnmodelId").val();
                var vehiclePrice = $("#hdnVehicleValue").val();
                GetCoverageDetailsOfVehicle(coverageCoreId, makeId, modelId, vehiclePrice, function (data) {
                    var Deductibles = data.deductibles;
                    var coverageLimits = data.coverageLimits;
                    $ddlCoverage.data("coverageLimits", coverageLimits);
                    $ddlCoverage.data("Deductibles", Deductibles);
                    clearDrop($ddlDeducible);
                    var isdisabled = !(Deductibles.length > 0);
                    $ddlDeducible.prop("disabled", isdisabled);

                    $.each(Deductibles, function (index, value) {
                        var NewOption = '<option value="{0}">{1}</option>';
                        NewOption = NewOption.replace("{0}", value.CoreId);
                        NewOption = NewOption.replace("{1}", value.Name);
                        $ddlDeducible.append(NewOption);
                    });

                    var dvVehicleId = "#" + $ddlDeducible.attr("data");
                    var $dvVehicle = $(dvVehicleId);
                    ClearPrime($dvVehicle);

                    var $hdnServiceSelected = $dvVehicle.find("input[id='hdnServiceSelected']");
                    var $AdditionalServices = $dvVehicle.find("input[id='AdditionalServices']");
                    var $hdnVehicleId = $dvVehicle.find("input[id='VehicleId']");
                    var hdnHasPrime = $dvVehicle.find("input[id='hdnHasPrime']").val() == "true";
                    var IsEditing = $dvVehicle.find("input[id='IsEditing']").val() == "true";


                    var DefaultCoverage = coverageLimits.ServicesCoverages;
                    var isFromHistory = $("#isFromHistory").val() == "true";

                    var IsLawProduct = isLawProductInclusion($dvVehicle);
                    $dvVehicle.find("input[id='IsLawProduct']").val(IsLawProduct);

                    if (isFromHistory) {
                        if ($hdnServiceSelected.val() != "") {
                            var ServicesSelected = JSON.parse($hdnServiceSelected.val());
                            var DataServiceSeletected = getSelectedServices(ServicesSelected, DefaultCoverage, false);
                            $AdditionalServices.val(JSON.stringify(DataServiceSeletected));
                        }
                        else {

                            if (parseInt($hdnVehicleId.val()) > 0 && !IsEditing) {
                                //Viene de base de datos sin servicios seleccionados
                                var DataServicesNonSelected = getSelectedServices("", DefaultCoverage, true);
                                $AdditionalServices.val(JSON.stringify(DataServicesNonSelected));
                            }
                            else {
                                var DataServicesNonSelected = DefaultCoverage;
                                $AdditionalServices.val(JSON.stringify(DataServicesNonSelected));
                            }
                        }
                    } else {
                        var DataServicesNonSelected = DefaultCoverage;
                        $AdditionalServices.val(JSON.stringify(DataServicesNonSelected));
                    }

                    if (isdisabled) {
                        GetRateInclusionFromDrop($ddlCoverage);
                    }
                    else {
                        $ddlDeducible.change(function () {
                            GetRateInclusionFromDrop(this);
                        });
                    }
                });
            }
        });

    });
    setEventVehicleQty();
    getChangeOfSurcharge();
}

function setEventVehicleQty() {
    var $dvVehicle = $("#selecCober").find("div[id*='dvVehicle']");
    $dvVehicle.each(function () {
        var $this = $(this);
        var $cVehi = $this.find("input[id='cvehi']");

        $cVehi.off("focusin");
        $cVehi.off("focusout");

        $cVehi.on({
            focusin: function () {
                var $this = $(this);
                $this.parent().addClass("is-dirty");
            },
            focusout: function () {
                var $this = $(this);

                if ($this.val() == "")
                    $this.parent().removeClass("is-dirty");

                var $dvVehicle = $("#" + $this.attr("data"));
                var $drop = $dvVehicle.find("select.Deducible");
                GetRateInclusionFromDrop($drop);
            }
        });
    });
}

function getChangeOfSurcharge() {
    $(".SurchargePercent").change(function () {
        GetRateInclusionFromDrop(this);
    });
}

function ConvertServices(ServicesCoverages) {
    var ServicesSelected = [];
    //Servicios Seleccionados
    $.each(ServicesCoverages, function (index, value) {
        $.each(value.Coverages, function (index2, value2) {
            var oServicesSelected = {};
            oServicesSelected.CoverageDetailCoreId = value2.CoverageDetailCoreId;
            oServicesSelected.Limit = value2.Limit;
            oServicesSelected.Name = value2.Name;
            oServicesSelected.IsSelected = value2.IsSelected;
            ServicesSelected.push(oServicesSelected);
        });
    });

    return ServicesSelected;
}

function ClearPrime($dvVehicle) {
    $dvVehicle.find("span[id='AnnualPrime']").text("0.00");
    $dvVehicle.find("span[id='ISC']").text("0.00");
    $dvVehicle.find("span[id='TAnnualPrime']").text("0.00");
}

function GetVehicleTypes(brandId, modelId, vehicleYear, isAsync) {
    $.ajax({
        type: "POST",
        url: "/Inclusion/GetVehicleTypes_New",
        data: JSON.stringify({ brandId: brandId, modelId: modelId, vehicleYear: vehicleYear }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: isAsync,
        success: function (data) {
            $("#hdndataVehicleTypes").val(JSON.stringify(data));
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function GetVehicleTypes_New(brandId, modelId, vehicleYear, AgentCode, isAsync) {
    $.ajax({
        type: "POST",
        url: "/Inclusion/GetVehicleTypes_New",
        data: JSON.stringify({ brandId: brandId, modelId: modelId, vehicleYear: vehicleYear, AgentCode: AgentCode }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: isAsync,
        success: function (data) {
            var $ddlType = $("#ddlType");
            clearDrop($ddlType);
            $.each(data, function (index, value) {
                var NewOption = '<option value="{0}">{1}</option>';
                NewOption = NewOption.replace("{0}", value.Name);
                NewOption = NewOption.replace("{1}", value.Name);
                $ddlType.append(NewOption);
                var $thisOption = $ddlType.find("option:last");
                //Agregar informacion en el data de cada option del dropdown                
                $thisOption.data("CoveragesByUsages", value.CoveragesByUsages);
                $thisOption.data("NewUsages", value.NewUsages);
                $thisOption.data("ProductByUsages", value.ProductByUsages);
                $thisOption.data("Products", value.Products);
            });

            //$ddlType.trigger("chosen:updated");
            $ddlType.trigger("change.select2");
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function getVehiclesModelsByBrands(BrandId, isAsync) {
    $.ajax({
        type: "POST",
        url: "/Inclusion/getVehiclesModelsByBrands",
        data: JSON.stringify({ BrandId: BrandId }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: isAsync,
        success: function (data) {
            var $ddlModelo = $("#ddlModelo");
            clearDrop($ddlModelo);
            $.each(data, function (index, value) {
                var NewOption = '<option value="{0}">{1}</option>';
                NewOption = NewOption.replace("{0}", value.Id);
                NewOption = NewOption.replace("{1}", value.Name);
                $ddlModelo.append(NewOption);
            });

            //$ddlModelo.trigger("chosen:updated");
            $ddlModelo.trigger("change.select2");

        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function getVehiclesFromPolicy(PolicyNumber, IsExclusion, sysflexVehiclesIdToExclude, IsVehicleChange) {
    var Title = $("#hdnTitle").val();

    $.ajax({
        type: "POST",
        url: "/Inclusion/GetDataVehicleFromPolicy",
        data: JSON.stringify({ PolicyNumber: PolicyNumber }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var secuencias = [];
            var TrReplace = "";
            var $Tbody = $("#tblVehicles").find("tbody");
            $Tbody.find("tr").remove();

            var trText = (Title == "INCLUSIONES") ? "<tr><th scope='row'>{NumOrd}</th><td>{PolicyNumber}</td><td>{Make}</td><td>{Model}</td><td>{Year}</td><td>{Plate}</td><td>{chassis}</td><td>{Color}</td></tr>"
                : "<tr><th scope='row'>{NumOrd}</th><td>{PolicyNumber}</td><td>{Make}</td><td>{Model}</td><td>{Year}</td><td>{Plate}</td><td>{chassis}</td><td>{Color}</td><td class='notchecked'><input id='{checkid}' class='vehicleCheck' type='checkbox'></td></tr>";

            var dataVehicle = data.DataCoreVehicle;
            var itemPolicyNumber = data.PolicyNumber;
            var qtyVehicles = dataVehicle.length;
            $("#hdnExcludeVehiclesQuantity").val(qtyVehicles);
            var isFinanced = false;
            var isFinanceSet = false;

            if (dataVehicle.length >= 5) {
                $("#dvtblFilters").show();
            } else {
                $("#dvtblFilters").hide();
            }

            $.each(dataVehicle, function (index, value) {
                //$("#hdnEndDate").val(moment(value.FechaFin).format(getCurrentDateMomentFormat()));
                $("#hdnEndDate").val(moment(value.FechaFinString.replace(".", "")).format(getCurrentDateMomentFormat()));
                TrReplace = trText.replace("{NumOrd}", (index + 1));
                TrReplace = TrReplace.replace("{PolicyNumber}", itemPolicyNumber);
                TrReplace = TrReplace.replace("{Make}", value.Marca);
                TrReplace = TrReplace.replace("{Model}", value.Modelo);
                TrReplace = TrReplace.replace("{Year}", value.Ano);
                TrReplace = TrReplace.replace("{Plate}", value.placa);
                TrReplace = TrReplace.replace("{chassis}", value.chasis);
                TrReplace = TrReplace.replace("{Color}", value.color);
                TrReplace = TrReplace.replace("{checkid}", "vehicle_secuence_" + value.Item);
                $Tbody.append(TrReplace);
                var $trLast = $Tbody.find("tr:last");
                $trLast.data("dataVehicle", JSON.stringify(value));
                secuencias.push(value.Item);

                if (!isFinanceSet) {

                    isFinanced = value.Financed;
                    if (!isFinanced && value.TieneEndoso) {
                        isFinanced = value.TieneEndoso;
                    }
                    isFinanceSet = true;
                }
            });

            var SecuenciaSysFlexMax = Math.max.apply(null, secuencias);
            $("#SecuenciaSysFlexMax").val(SecuenciaSysFlexMax);

            if (IsExclusion) {

                var endExcludeDate = $("#hdnEndDate").val();
                var d = $.datepicker.formatDate(getCurrentDateFormat(), moment(endExcludeDate).toDate());
                $('#ExcludeStartDate').datepicker('option', 'maxDate', d);

                var $selecCober = $("#selecCober");
                $selecCober.hide();

                if (isFinanced) {
                    showWarning(['Esta póliza está financiada o endosada por tal razon debe de tener una autorización por parte de la entidad financiera para poder completar esta exclusión.'], 'Póliza Financiada/Endosada');
                }
            }
            else if (IsVehicleChange) {

                var $selecCober = $("#selecCober");
                $selecCober.hide();

                //marcando los vehiculos seleccionados         

                var allchecks = $('.vehicleCheck');
                var checksCheckeds = 0;

                if (sysflexVehiclesIdToExclude) {

                    var sp = sysflexVehiclesIdToExclude.split(',');

                    $.each(allchecks, function (index, obj) {
                        var $this2 = $(this);

                        var CheckId = $this2.attr("id").split('_');
                        var realID = CheckId[2];

                        $.each(sp, function (index2, obj2) {
                            if (realID == obj2) {

                                $this2.prop('checked', true);

                                var $frmVehicleChanges = $("#frmVehicleChanges");
                                $frmVehicleChanges.show();

                                var $Tbody = $("#tblVehicles").find("tbody");
                                var dveh = null;

                                $Tbody.find("tr").each(function () {
                                    var $thisTR = $(this);
                                    var $checkbox = $thisTR.find("input[type='checkbox']");
                                    if ($checkbox.prop('checked')) {
                                        dveh = $thisTR.data("dataVehicle");
                                    }
                                });
                                setDataChangeVehicle(dveh);

                                getVehicleRequestChange(itemPolicyNumber, realID);
                            }
                        });
                    });
                }
                //
            }
        },
        complete: function (jqXHR, textStatus) {

            qtblVehicles = $('#tblVehicles').DataTable({
                paging: true,
                responsive: true,
                sort: false,
                pageLength: 10,
                fixedColumns: false,
                lengthChange: false,
                Info: true,
                language: {
                    zeroRecords: "No registros disponibles"
                }
            });

            if (Title == "EXCLUSIONES") {

                $('#txtMake').on('keyup', function () {
                    var text = this.value;
                    qtblVehicles.columns(2).search(text).draw();
                });

                $('#txtmodel').on('keyup', function () {
                    var text = this.value;
                    qtblVehicles.columns(3).search(text).draw();
                });

                $('#txtchasis').on('keyup', function () {
                    var text = this.value;
                    qtblVehicles.columns(6).search(text).draw();
                });


                $("#tblVehicles").on('click', '.vehicleCheck', function () {

                    var $this = $(this);                    
                    var checksCheckeds = 0;

                    var $tdParent = $this.parent();

                    if ($this.is(':checked')) {
                        $tdParent.addClass('ischecked');
                        $tdParent.removeClass('notchecked');
                    } else {
                        $tdParent.removeClass('ischecked');
                        $tdParent.addClass('notchecked');
                    }

                    var allchecks = qtblVehicles.cells('td.ischecked').nodes();
                    var alltd = qtblVehicles.cells('td.notchecked').nodes();

                    var tblfilters = $('.tblfilter');
                    var filterWithValue = 0;
                    $.each(tblfilters, function (index, obj) {
                        var $tblfilter = $(this);
                        if ($tblfilter.val() !== "") {
                            filterWithValue += 1;
                        }
                    });

                    //si alltd es 0 quiere decir que se marco el unico check que existe en la tabla, lo cual no se debe
                    if (allchecks.length == 1 && alltd.length == 0 && filterWithValue == 0) {
                        showError(['No puede realizar una exclusion en una poliza de un solo vehiculo.'], 'Seleccionar Vehiculos Exclusión');
                        $this.prop('checked', false);
                        $tdParent.removeClass('ischecked');
                        $tdParent.addClass('notchecked');
                        return false;
                    }
                    
                    if (alltd.length == 0 && allchecks.length > 1 && filterWithValue == 0) {
                        showError(['No puede seleccionar todos los vehiculos para excluir.'], 'Seleccionar Vehiculos Exclusión');
                        $this.prop('checked', false);
                        $tdParent.removeClass('ischecked');
                        $tdParent.addClass('notchecked');
                        return false;
                    }
                });

                //marcando los vehiculos seleccionados         
                var allchecks = $('.vehicleCheck');
                var checksCheckeds = 0;

                var vehiclesSelected = qtblVehicles.cells('td').nodes();

                if (sysflexVehiclesIdToExclude) {

                    var sp = sysflexVehiclesIdToExclude.split(',');

                    $.each(vehiclesSelected, function (index, vhs) {
                        var $thisTd = $(this);

                        var $checkbox = $thisTd.find(".vehicleCheck");

                        if ($checkbox.length > 0) {

                            var CheckId = $checkbox.attr("id").split('_');
                            var realID = CheckId[2];

                            $.each(sp, function (index2, obj2) {
                                if (realID == obj2) {

                                    $checkbox.prop('checked', true);
                                    $thisTd.addClass('ischecked');
                                    $thisTd.removeClass('notchecked');
                                }
                            });
                        }
                    });
                }
            }
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function GetCoverageDetailsOfVehicle(pcoverageCoreId, pmakeId, pmodelId, pvehiclePrice, callBack) {
    $.ajax({
        type: "POST",
        url: "/Inclusion/GetCoverageDetailsOfVehicle",
        data: JSON.stringify({ coverageCoreId: pcoverageCoreId, makeId: pmakeId, modelId: pmodelId, vehiclePrice: pvehiclePrice }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: callBack,
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function GetServicesVehicle(VehicleId) {
    $.ajax({
        type: "POST",
        url: "/Inclusion/GetServicesVehicle",
        data: { VehicleId: VehicleId },
        dataType: "json",
        async: false,
        success: function (data) {

        },
        failure: function (data) {
            alert(response.responseText);
        },
        error: function (data) {
            alert(response.responseText);
        }
    });
}

function LoadInclusionFormData(isFromHistory) {
    var PolicyNumber = $("#policyNoMain").val();
    //Cargar las vehiculos a incluir
    $.ajax({
        type: "POST",
        url: "/Inclusion/GetVehiclesFromInclusion",
        data: { PolicyNo: PolicyNumber },
        dataType: "json",
        success: function (data) {
            $("#isFromHistory").val(isFromHistory);
            $("#spQuotationNumber").text(data.QuotationNumber + " / " + PolicyNumber);
            var dataVehicle = data.dataVehicles;
            var DataCustomer = data.dataCustomer;
            var DataAgent = data.userAgenCode;
            var ExclusionStartDate = data.ExclusionStartDate;
            var IsExclusion = data.IsExclusion;
            var IsVehicleChange = data.IsVehicleChange;
            var sysflexVehiclesIdToExclude = data.sysflexVehiclesIdToExclude
            LoadCustomerData(DataCustomer, DataAgent, PolicyNumber, ExclusionStartDate, IsExclusion, sysflexVehiclesIdToExclude, IsVehicleChange);

            if (!IsExclusion && !IsVehicleChange) {
                var VehicleSec = 0;
                $.each(dataVehicle, function (index, value) {
                    VehicleSec = value.SecuenciaVehicleSysflex;
                    oVehicleData = {}
                    oVehicleData.makeId = value.VehicleModel_Make_Id;
                    oVehicleData.modelId = value.VehicleModel_Model_Id;
                    oVehicleData.Year = value.Year;
                    oVehicleData.TypeId = value.SelectedVehicleTypeId;
                    oVehicleData.TypeIdDesc = value.SelectedVehicleTypeName;
                    oVehicleData.PrincipalUsageId = value.UsageId;
                    oVehicleData.PrincipalUsageDesc = value.UsageName;
                    oVehicleData.StorageId = value.StoreId;
                    oVehicleData.storeName = value.StoreName;
                    oVehicleData.VehicleValue = value.InsuredAmount;
                    oVehicleData.VehicleYearsOld = value.VehicleYearOld;
                    oVehicleData.vehicleDescription = value.VehicleDescription;
                    oVehicleData.vehicleMakeName = value.VehicleMakeName;
                    oVehicleData.SecuenciaSysFlex = VehicleSec;
                    oVehicleData.VehicleId = value.Id;
                    oVehicleData.VehicleQuantity = value.VehicleQuantity;
                    oVehicleData.serviceSelected = value._services;
                    var DataCoverage = {}
                    DataCoverage.VehicleValue = value.InsuredAmount;
                    DataCoverage.VechicleDesc = value.VehicleMakeName + " " + value.ModelDesc + " " + value.Year;
                    DataCoverage.makeId = value.VehicleModel_Make_Id;
                    DataCoverage.modelId = value.VehicleModel_Model_Id;
                    DataCoverage.SelectedProductName = value.SelectedProductName;
                    DataCoverage.SelectedCoverageName = value.SelectedCoverageName;
                    DataCoverage.SelectedDeductibleName = value.SelectedDeductibleName;

                    //if (value.SelectedVehicleFuelTypeId) {
                    oVehicleData.SelectedVehicleFuelTypeId = value.SelectedVehicleFuelTypeId;
                    oVehicleData.SelectedVehicleFuelTypeDesc = value.SelectedVehicleFuelTypeDesc;
                    //} else {
                    //    oVehicleData.SelectedVehicleFuelTypeId = "0";
                    //    oVehicleData.SelectedVehicleFuelTypeDesc = 'No Definido';
                    //}

                    GetVehicleTypes(value.VehicleModel_Make_Id, value.VehicleModel_Model_Id, value.Year, false);
                    var dataVehicleTypes = JSON.parse($("#hdndataVehicleTypes").val());

                    var dataVehicleFilter = altFind(dataVehicleTypes, function (valor) {
                        return valor.Name == value.SelectedVehicleTypeName;
                    });

                    var UsageFilterPlan = dataVehicleFilter.ProductByUsages.filter(function (valor) {
                        return valor.UsoDescripcion == value.UsageName;
                    });
                    var Products = dataVehicleFilter.Products;

                    DataCoverage.planData = UsageFilterPlan;
                    DataCoverage.Products = Products;
                    DataCoverage.VehicleData = oVehicleData;

                    var CoveragesByUsages = dataVehicleFilter.CoveragesByUsages;
                    var CoverageByUsageFilter = CoveragesByUsages.filter(function (valor) {
                        return valor.UsoDescripcion == value.UsageName;
                    });

                    $("#ddlPrincipalUsage").data("CoverageByUsageFilter", CoverageByUsageFilter);
                    AddVehicle(DataCoverage);

                    var $dvVehicle = $("#dvVehicle" + VehicleSec);
                    var $ddlAllProducts = $dvVehicle.find("select[id='ddlAllProducts']");
                    var $ddlCoverage = $dvVehicle.find("select.coverage");
                    var $ddlDeducible = $dvVehicle.find("select.Deducible");
                    var $SecuenciaSysFlex = $dvVehicle.find("input[id='SecuenciaSysFlex']");
                    var $UsageFilterPlan = $dvVehicle.find("input[id='UsageFilterPlan']");
                    $UsageFilterPlan.val(JSON.stringify(UsageFilterPlan));
                    $SecuenciaSysFlex.val(VehicleSec);

                    var $ddlSurcharge = $dvVehicle.find("select[id='SurchargePercent']");
                    if ($ddlSurcharge.length > 0) {
                        value.SurChargePercentage = (value.SurChargePercentage == 0 ? "" : value.SurChargePercentage.toFixed(2));

                        $ddlSurcharge.val(value.SurChargePercentage);
                        $ddlSurcharge.trigger("change.select2");
                    }

                    $ddlAllProducts.find("option").each(function () {
                        var $this = $(this);
                        if ($this.text() == DataCoverage.SelectedProductName) {
                            $ddlAllProducts.val($this.val());
                            //$ddlAllProducts.trigger("chosen:updated");
                            $ddlAllProducts.trigger("change.select2");
                            $ddlAllProducts.trigger("change");
                            $ddlCoverage.find("option").each(function () {
                                var $this = $(this);
                                if ($this.text() == DataCoverage.SelectedCoverageName) {
                                    $ddlCoverage.val($this.val());
                                    //$ddlCoverage.trigger("chosen:updated");
                                    $ddlCoverage.trigger("change.select2");
                                    $ddlCoverage.trigger("change");

                                    $ddlDeducible.find("option").each(function () {
                                        var $this = $(this);
                                        if ($this.text() == DataCoverage.SelectedDeductibleName) {
                                            $ddlDeducible.val($this.val());
                                            //$ddlDeducible.trigger("chosen:updated");
                                            $ddlDeducible.trigger("change.select2");
                                            $ddlDeducible.trigger("change");
                                        }
                                    });
                                }
                            });
                        }
                    });



                });
            }
        },
        failure: function (data) {
            console.log(data);
            alert(response.responseText);
        },
        error: function (data) {
            console.log(data);
            alert(response.responseText);
        }
    });
}

function LoadInclusion(isFromHistory, Title) {

    //    $("#tblVehicles").off('.vehicleCheck', 'click');

    $.ajax({
        type: "POST",
        url: "/Inclusion/Inclusion",
        data: { Title: Title, isFromHistory: isFromHistory },
        dataType: "html",
        success: function (response) {
            $('#TitleView').html(Title);
            $('#main').removeClass('hist');
            $('#dvContainer').html(response);
            Inclusioninitialize(Title);
            isFromHistory = isNaN(isFromHistory) ? false : isFromHistory;

            if (isFromHistory) {
                LoadInclusionFormData(isFromHistory);
            }

            var _hdnPolicySendByVO = $("#hdnPolicySendByVO").val();
            if (_hdnPolicySendByVO) {

                $("#nbPolicyNo").val(_hdnPolicySendByVO);
                $("#nbPolicyNo").parent().addClass("is-dirty");

                $("#btnSearch").trigger("click");

                $("#hdnPolicySendByVO").val(null);
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function EditVehicle(dvId) {
    var btnAddEditVehicle = $("#btnAddEditVehicle");
    btnAddEditVehicle.find("span:first").text("Editar Vehículo");
    btnAddEditVehicle.data("ActionMode", "Edit");
    btnAddEditVehicle.data("dvVehicleId", dvId);
    isEditVehicle = true;

    var $dvVehicle = $(dvId);
    var VehicleId = $dvVehicle.find("input[id='VehicleId']").val();
    var $hdnVehicleDataFromSaveAndEdit = $dvVehicle.find("input[id='hdnVehicleDataFromSaveAndEdit']");
    var jsonData = $hdnVehicleDataFromSaveAndEdit.val();
    var dataVehicle = JSON.parse(jsonData);

    var $ddlMarca = $("#ddlMarca");
    $ddlMarca.val(dataVehicle.makeId);
    getVehiclesModelsByBrands(dataVehicle.makeId, false);
    $ddlMarca.parent().parent().removeClass('requerido');
    $ddlMarca.parent().parent().addClass('erarequerido');
    //$ddlMarca.trigger("chosen:updated");
    $ddlMarca.trigger("change.select2");

    var $ddlModelo = $("#ddlModelo");
    $ddlModelo.val(dataVehicle.modelId);
    $ddlModelo.parent().parent().removeClass('requerido');
    $ddlModelo.parent().parent().addClass('erarequerido');
    //$ddlModelo.trigger("chosen:updated");
    $ddlModelo.trigger("change.select2");

    var $ddlAnio = $("#ddlAnio");
    $ddlAnio.val(dataVehicle.Year);
    $ddlAnio.parent().removeClass('requerido');
    $ddlAnio.parent().addClass('erarequerido');
    //$ddlAnio.trigger("chosen:updated");
    $ddlAnio.trigger("change.select2");
    GetVehicleTypes_New(dataVehicle.makeId, dataVehicle.modelId, dataVehicle.Year, false);

    var $ddlType = $("#ddlType");
    var IsFromhistory = $("#isFromHistory").val() == "true";
    var TypeId = IsFromhistory ? dataVehicle.TypeIdDesc : dataVehicle.TypeId;
    $ddlType.val(TypeId);
    $ddlType.parent().parent().removeClass('requerido');
    $ddlType.parent().parent().addClass('erarequerido');
    //$ddlType.trigger("chosen:updated");
    $ddlType.trigger("change.select2");

    LoadUsages($ddlType);

    var $ddlPrincipalUsage = $("#ddlPrincipalUsage");
    $ddlPrincipalUsage.val(dataVehicle.PrincipalUsageId);
    $ddlPrincipalUsage.parent().parent().removeClass('requerido');
    $ddlPrincipalUsage.parent().parent().addClass('erarequerido');
    //$ddlPrincipalUsage.trigger("chosen:updated");
    $ddlPrincipalUsage.trigger("change.select2");

    var $UsageFilterPlan = $dvVehicle.find("input[id='UsageFilterPlan']");
    if ($UsageFilterPlan != null) {
        var UsageFilterPlan = JSON.parse($UsageFilterPlan.val());
        $("#ddlPrincipalUsage").data("UsageFilterPlan", UsageFilterPlan);
    }

    var $hdnProducts = $dvVehicle.find("input[id='hdnProducts']");
    if ($hdnProducts != null) {
        var Products = JSON.parse($hdnProducts.val());
        $("#ddlPrincipalUsage").data("Products", Products);
    }

    var $ddlEstacionamiento = $("#ddlEstacionamiento");
    $ddlEstacionamiento.val(dataVehicle.StorageId);
    $ddlEstacionamiento.parent().parent().removeClass('requerido');
    $ddlEstacionamiento.parent().parent().addClass('erarequerido');
    //$ddlEstacionamiento.trigger("chosen:updated");
    $ddlEstacionamiento.trigger("change.select2");

    var $VehicleYearsOld = $("#VehicleYearsOld");
    $VehicleYearsOld.val(dataVehicle.VehicleYearsOld);
    $VehicleYearsOld.parent().parent().removeClass('requerido');
    $VehicleYearsOld.parent().parent().addClass('erarequerido');
    $VehicleYearsOld.parent().addClass("is-dirty");

    var $valor = $("#valor");
    $valor.val(dataVehicle.VehicleValue);
    $valor.parent().parent().removeClass('requerido');
    $valor.parent().parent().addClass('erarequerido');
    $valor.parent().addClass("is-dirty");
    $valor.data("VehicleId", VehicleId);


    if (dataVehicle.modelId > 0) {

        GetFuelType_Inclusion(null, dataVehicle.makeId, dataVehicle.modelId);

        var $fuelType = $("#fuelType");
        $fuelType.val(dataVehicle.SelectedVehicleFuelTypeId);
        $fuelType.trigger("change.select2");

        $fuelType.parent().parent().removeClass('requerido');
        $fuelType.parent().parent().addClass('erarequerido');
    }


}

function CreateNewQuotationExclusion(EndDate, AgentCode, DataCustomer) {
    var policyNoMain = $('#policyNoMain').val();
    $.ajax({
        type: "POST",
        url: "/Inclusion/CreateQuotationByExclusion",
        data: { EndDate: EndDate, AgentCode: AgentCode, DataCustomer: JSON.stringify(DataCustomer), policyNoMain: policyNoMain },
        dataType: "json",
        success: function (data) {
            $("#spQuotationNumber").text(data.Quotation.QuotationNumber);
            $("#hdnQuotationId").val(data.Quotation.Id);
            $('#quotationID').val(data.Quotation.Id);
            $("#hdnDriverData").val(JSON.stringify(data.driverData));
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function CreateNewQuotation(EndDate, AgentCode, DataCustomer) {
    var policyNoMain = $('#policyNoMain').val();
    $.ajax({
        type: "POST",
        url: "/Inclusion/CreateQuotationByInclusion",
        data: { EndDate: EndDate, AgentCode: AgentCode, DataCustomer: JSON.stringify(DataCustomer), policyNoMain: policyNoMain },
        dataType: "json",
        success: function (data) {
            $("#spQuotationNumber").text(data.Quotation.QuotationNumber);
            $("#hdnQuotationId").val(data.Quotation.Id);
            $('#quotationID').val(data.Quotation.Id);
            $("#hdnDriverData").val(JSON.stringify(data.driverData));
            //window.history.pushState({ order: 1 }, document.title, '/Home/Index/' + data.quotationIdEncript);
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function AddVehicle(DataCoverageFromHist) {
    var SecuenciaSysFlex;
    var VehicleId;

    if (DataCoverageFromHist == null) {
        //Validar que se este trabajando con una poliza existente en sysflex
        var PolicyExist = $('#policyNoMain').val() != "";

        if (!PolicyExist) {
            showWarning(["Para continuar debe buscar la póliza a trabajar"], 'Advertencia');
            return false;
        }

        var MarcaSelected = $("#ddlMarca option:selected");
        var ModeloSelected = $("#ddlModelo option:selected");
        var AnioSelected = $("#ddlAnio option:selected");
        var TipoSelected = $("#ddlType option:selected");
        var PrincipalUsageSelected = $("#ddlPrincipalUsage option:selected");
        var EstacionamientoSelected = $("#ddlEstacionamiento option:selected");
        var VehicleYearsOldSelected = $("#VehicleYearsOld option:selected");
        var $btnAddEditVehicle = $("#btnAddEditVehicle");
        var ActionMode = $btnAddEditVehicle.data("ActionMode");
        var vehicleValue = $("#valor");
        var IsEditing = false;
        var secuencias = [];

        SecuenciaSysFlex = parseInt($("#SecuenciaSysFlexMax").val()) + 1;
        var FuelTypeSelected = $("#fuelType option:selected");

        //Verificar cuantos vehiculos han sido agregados
        var $vehiclesDv = $("#selecCober").find("div[id*='dvVehicle']");
        if ($vehiclesDv.length > 0) {
            $vehiclesDv.each(function () {
                var $this = $(this);
                var sec = $this.attr("id").replace("dvVehicle", "");
                secuencias.push(sec);
            });

            SecuenciaSysFlex = secuencias.length > 0 ? Math.max.apply(null, secuencias) + 1 : 1;
        }

        if (ActionMode == "Edit") {
            IsEditing = true;
            var dataVehicleId = $btnAddEditVehicle.data("dvVehicleId");
            var $dvVehicleId = $(dataVehicleId);
            SecuenciaSysFlex = $dvVehicleId.find("input[id='SecuenciaSysFlex']").val();
            VehicleId = vehicleValue.data("VehicleId");
        }

        var errorList = [];

        //Validar que todos los campos esten llenos
        if (MarcaSelected.val() == "-1" || MarcaSelected.val() == "")
            errorList.push('El campo marca es requerido');

        if (ModeloSelected.val() == "-1" || ModeloSelected.val() == "")
            errorList.push('El campo modelo es requerido');

        if (AnioSelected.val() == "-1" || AnioSelected.val() == "")
            errorList.push('El campo año es requerido');

        if (TipoSelected.val() == "-1" || TipoSelected.val() == "")
            errorList.push('El campo tipo de vehiculo es requerido');

        if (FuelTypeSelected.val() == "-1" || FuelTypeSelected.val() == "")
            errorList.push('El campo tipo combustible es requerido');

        if (PrincipalUsageSelected.val() == "-1" || PrincipalUsageSelected.val() == "")
            errorList.push('El campo uso del vehiculo es requerido');

        if (EstacionamientoSelected.val() == "-1" || EstacionamientoSelected.val() == "")
            errorList.push('El campo estacionamiento es requerido');

        if (VehicleYearsOldSelected.val() == "-1" || VehicleYearsOldSelected.val() == "")
            errorList.push('El campo Nuevo / 0km es requerido');

        if (errorList.length > 0) {
            showWarning(errorList, 'Advertencia');
            return false;
        }

        //Crear la cotizacion si no existe aun
        if ($("#hdnQuotationId").val() == "") {
            var IntermediarioId = $("#hdnIntermediarioId").val();
            var DataCustomer = JSON.parse($("#hdnCustomerDataInclusion").val());
            var EndDate = $("#hdnEndDate").val();
            CreateNewQuotation(EndDate, parseInt(IntermediarioId), DataCustomer);
        }

        var DataCoverage = {}
        var vehiclevalue = $("#valor").inputmask('unmaskedvalue');
        DataCoverage.VehicleValue = vehiclevalue;
        DataCoverage.VechicleDesc = MarcaSelected.text() + " " + ModeloSelected.text() + " " + AnioSelected.text();
        DataCoverage.makeId = MarcaSelected.val();
        DataCoverage.modelId = ModeloSelected.val();
        DataCoverage.planData = $("#ddlPrincipalUsage").data("UsageFilterPlan");
        DataCoverage.Products = $("#ddlPrincipalUsage").data("Products");
        DataCoverage.IsEditing = IsEditing;

        //Data del vehiculo en cuestion
        oVehicleData = {}
        oVehicleData.makeId = MarcaSelected.val();
        oVehicleData.vehicleMakeName = MarcaSelected.text();
        oVehicleData.modelId = ModeloSelected.val();
        oVehicleData.vehicleModelName = ModeloSelected.text();
        oVehicleData.Year = AnioSelected.val();
        oVehicleData.TypeId = TipoSelected.val();
        oVehicleData.TypeIdDesc = TipoSelected.text();
        oVehicleData.PrincipalUsageId = PrincipalUsageSelected.val();
        oVehicleData.PrincipalUsageDesc = PrincipalUsageSelected.text();
        oVehicleData.StorageId = EstacionamientoSelected.val();
        oVehicleData.storeName = EstacionamientoSelected.text();
        oVehicleData.VehicleValue = vehiclevalue;
        oVehicleData.VehicleYearsOld = VehicleYearsOldSelected.val();
        oVehicleData.vehicleDescription = DataCoverage.VechicleDesc;
        oVehicleData.SecuenciaSysFlex = SecuenciaSysFlex;
        oVehicleData.VehicleId = VehicleId;

        oVehicleData.SelectedVehicleFuelTypeId = FuelTypeSelected.val();
        oVehicleData.SelectedVehicleFuelTypeDesc = FuelTypeSelected.text();

        DataCoverage.VehicleData = oVehicleData;


        if (ActionMode == "Edit")
            DeleteVehicle("#" + $dvVehicleId.attr("id"), true);

        $.ajax({
            type: "POST",
            url: "/Inclusion/getCoverage",
            data: { data: DataCoverage },
            dataType: "html",
            success: function (response) {
                var $selecCober = $('#selecCober');
                $selecCober.find("div:first").append(response);
                var IsShowdiv = $selecCober.css("display") == "flex";
                if (!IsShowdiv)
                    $selecCober.fadeIn(200);

                InitializePartialControls();

                ClearForm($("#dvVehicleForm"));
                var btnAddEditVehicle = $("#btnAddEditVehicle");
                btnAddEditVehicle.find("span:first").text("Agregar Vehículo");
                btnAddEditVehicle.data("ActionMode", "Add");
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    } else {
        //Cargar los vehiculos
        $.ajax({
            type: "POST",
            url: "/Inclusion/getCoverage",
            data: { data: DataCoverageFromHist },
            dataType: "html",
            async: false,
            success: function (response) {
                var $selecCober = $('#selecCober');
                $selecCober.find("div:first").append(response);
                var IsShowdiv = $selecCober.css("display") == "flex";
                if (!IsShowdiv)
                    $selecCober.fadeIn(200);
                InitializePartialControls();
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }
}

function DeleteVehicle(id, isEdit) {
    var $divVehicle = $(id);
    if (!isEdit) {
        showQuestion("Esta seguro de realizar esta operación?", "Confirmación", function () {
            //Eliminar el vehiculo de la base de datos
            var SecuenciaVehicleSysflex = $divVehicle.find("input[id='SecuenciaSysFlex']").val();
            var quotationCoreNumber = $("#hdnCoreQuotationNumber").val();
            var vehicleId = $divVehicle.find("input[id='VehicleId']").val();
            DeleteVehicleInclusion(SecuenciaVehicleSysflex, quotationCoreNumber, vehicleId);
            $divVehicle.remove();
            HideSelectCobertura();
        }, function () { return false; });
    } else {
        $divVehicle.remove();
        HideSelectCobertura();
    }
}

function HideSelectCobertura() {
    var $selecCober = $("#selecCober");
    var $VehiclesSection = $selecCober.find("div[id*='dvVehicle']");
    if ($VehiclesSection.length == 0)
        $selecCober.hide();
}

function GetRateInclusionFromDrop(obj) {
    var $obj = $(obj);
    var dvId = "#" + $obj.attr("data");
    var $dvVehicle = $(dvId);
    var $ddlCobertura = $dvVehicle.find(".coverage");
    var coverageLimits = $ddlCobertura.data("coverageLimits");
    var $hdnVehicleDataFromSaveAndEdit = $dvVehicle.find("input[id='hdnVehicleDataFromSaveAndEdit']");
    var $hdnProducts = $dvVehicle.find("input[id='hdnProducts']");
    var $ddlAllProducts = $dvVehicle.find("[id='ddlAllProducts']");
    var ProductNameSelected = $ddlAllProducts.find("option:selected").text();
    var jsonData = $hdnVehicleDataFromSaveAndEdit.val();
    var dataVehicle = JSON.parse(jsonData);
    var jsonDataProduct = $hdnProducts.val();
    var DataProducts = JSON.parse(jsonDataProduct);

    var UsageFilterPlan = altFind(DataProducts, function (valor) {
        return valor.Name == ProductNameSelected;
    });

    var idCapacidad = UsageFilterPlan.IdCapacidad;
    var descCapacidad = UsageFilterPlan.DescCapacidad;

    var deductibleId = null;

    if ($(obj).hasClass("Deducible"))
        deductibleId = $obj.val();

    var coverageCoreId = $ddlCobertura.val();
    var secuenciaVeh = $dvVehicle.find("input[id='Secuencia']").val();
    var IsLawProduct = $dvVehicle.find("input[id='IsLawProduct']").val() == "true";

    if (!IsLawProduct) {
        var valorVehiculo = parseFloat($dvVehicle.find("input[id='hdnVehicleValue']").val());
        if (valorVehiculo <= 0) {
            showWarning(["El valor del vehiculo no debe ser cero, edite el vehiculo"], "Advertencia");
            return false;
        }
    }

    var $ddlSurcharge = $dvVehicle.find(".SurchargePercent");
    var surchargePercent = 0;
    if ($ddlSurcharge.length > 0) {
        surchargePercent = $ddlSurcharge.val();
    }

    getRatesInclusion(dataVehicle, deductibleId, coverageCoreId, idCapacidad, descCapacidad, coverageLimits, $dvVehicle, secuenciaVeh, IsLawProduct, surchargePercent);
}

function getRatesInclusion(dataVehicle, deductibleId, coverageCoreId, idCapacidad, descCapacidad, coverageLimits, $dvVehicle, secuenciaVeh, IsLawProduct, surchargePercent) {
    var pcoverageCoreId = coverageCoreId; //Subramo
    var pbrandId = dataVehicle.makeId; //Id de la marca
    var pmodelId = dataVehicle.modelId; //Id del modelo
    var pvehicleYear = parseInt(dataVehicle.Year); //Año del vehicuo         
    var pinsuredAmount = IsLawProduct ? 1 : dataVehicle.VehicleValue; //Valor del vehiculo         
    var pstorageId = dataVehicle.StorageId;//id del estacionamiento
    var pdeductibleId = deductibleId; //Id del deducible        
    var pqtyVehicles = parseInt($dvVehicle.find("[id='cvehi']").val());//Cantidad de Vehiculos
    var pusage = dataVehicle.PrincipalUsageId; //Id del uso
    var pusageName = dataVehicle.PrincipalUsageDesc;//Uso Principal

    var pfuelTypeId = dataVehicle.SelectedVehicleFuelTypeId;
    var pfuelTypeDesc = dataVehicle.SelectedVehicleFuelTypeDesc;

    var limitself = []

    surchargePercent = (surchargePercent == "" ? 0 : surchargePercent);

    //Coberturas de daños a terceros
    $.each(coverageLimits.ThirdPartyCoverages, function (index, value) {
        var CoveragesLimitSelf = {};
        CoveragesLimitSelf.Name = value.Name;
        CoveragesLimitSelf.CoverageDetailCoreId = value.CoverageDetailCoreId;
        CoveragesLimitSelf.Limit = value.Limit;
        CoveragesLimitSelf.isSelected = value.isSelected;
        CoveragesLimitSelf.delete = value.delete;
        limitself.push(CoveragesLimitSelf);
    });

    //Coberturas de daños propios
    $.each(coverageLimits.SelfDamagesCoverages, function (index, value) {
        var CoveragesLimitSelf = {};
        CoveragesLimitSelf.Name = value.Name;
        CoveragesLimitSelf.CoverageDetailCoreId = value.CoverageDetailCoreId;
        CoveragesLimitSelf.Limit = value.Limit;
        CoveragesLimitSelf.isSelected = value.isSelected;
        CoveragesLimitSelf.delete = value.delete;
        limitself.push(CoveragesLimitSelf);
    });

    var AdditionalServicesData = JSON.parse($dvVehicle.find("input[id='AdditionalServices']").val());
    var ServicesSelected = ConvertServices(AdditionalServicesData);

    var IsLaw = isLawProductInclusion($dvVehicle);

    $.ajax({
        url: '/Inclusion/GetRates',
        type: 'POST',
        dataType: 'json',
        data: {
            coverageCoreId: pcoverageCoreId,
            brandId: pbrandId,
            modelId: pmodelId,
            vehicleYear: pvehicleYear,
            insuredAmount: pinsuredAmount,
            storageId: pstorageId,
            deductibleId: isNaN(pdeductibleId) ? null : pdeductibleId,
            qtyVehicles: pqtyVehicles,
            usage: pusage,
            secuencia: secuenciaVeh,
            Esdeley: IsLaw,
            idCapacidad: idCapacidad,
            descCapacidad: descCapacidad,
            Services: JSON.stringify(ServicesSelected),
            limitself: JSON.stringify(limitself),
            usagename: pusageName,
            isSemifull: "",
            percentSurCharge: surchargePercent,
            coveragePercent: 100,
            fuelTypeId: pfuelTypeId,
            fuelTypeDesc: pfuelTypeDesc
        },
        async: true,
        success: function (data) {
            var TotalPrimeWithoutTax = (data.output.TpPrime + data.output.SdPrime + data.output.ServicesPrime);
            var TotalTax = ((GlobalCurrentIsc / 100) * TotalPrimeWithoutTax)
            var TotalPrimeWithTax = (TotalPrimeWithoutTax + TotalTax);

            $dvVehicle.find("span[id='AnnualPrime']").text(number_format(TotalPrimeWithoutTax * pqtyVehicles, 2));
            $dvVehicle.find("span[id='ISC']").text(number_format(TotalTax * pqtyVehicles, 2));
            $dvVehicle.find("span[id='TAnnualPrime']").text(number_format(TotalPrimeWithTax * pqtyVehicles, 2));
            $dvVehicle.find("input[id='hdnHasPrime']").val("true");

            $dvVehicle.find("input[id='totalPrimeWithoutTax']").val(TotalPrimeWithoutTax);
            $dvVehicle.find("input[id='totalIsc']").val(TotalTax);
            $dvVehicle.find("input[id='totalPrime']").val(TotalPrimeWithTax);
            $dvVehicle.find("input[id='rateJson']").val(data.output.jsonRates);

            $dvVehicle.find("input[id='servicesPrime']").val(data.output.ServicesPrime);
            $dvVehicle.find("input[id='tpPrime']").val(data.output.TpPrime);
            $dvVehicle.find("input[id='sdPrime']").val(data.output.SdPrime);
            $dvVehicle.find("input[id='SelectedVehicleTypeId']").val(data.output.VehicleTypeId);

            $dvVehicle.find("input[id='primaproratadiasinimpuestos']").val(data.output.primaproratadiasinimpuestos);
            $dvVehicle.find("input[id='DiasCotizados']").val(data.output.DiasCotizados);
            $dvVehicle.find("input[id='IsLawProduct']").val(data.IsLawProduct);

            var _isLawProduct = $dvVehicle.find("input[id='IsLawProduct']").val() == "true";
            var VehicleData = JSON.parse($dvVehicle.find("input[id='hdnVehicleDataFromSaveAndEdit']").val());
            var SecuenciaVehicleSysflex = VehicleData.SecuenciaSysFlex;
            var makeName = VehicleData.vehicleMakeName;
            var modelName = VehicleData.vehicleModelName;
            var year = VehicleData.Year;
            var quotationCoreNumber = data.quotationCore;

            //Solos los que no son de Ley
            if (!_isLawProduct) {
                /*Reaseguro*/
                $.ajax({
                    url: '/Inclusion/getMaximoReaseguroSubRamo_NewInlcusion',
                    dataType: 'json',
                    data: { SecuenciaVehicleSysflex: SecuenciaVehicleSysflex, quotationCoreNumber: quotationCoreNumber, make: makeName, model: modelName, year: year },
                    async: false,//LoadVehicle ? false : true,
                    success: function (data) {

                        if (data.IsFacultative) {
                            IsFacultative = data.IsFacultative;
                            AmountFacultative = data.AmountFacultative;
                            $dvVehicle.find("input[id='hdnIsFacultative']").val(IsFacultative);
                            $dvVehicle.find("input[id='hdnAmountFacultative']").val(AmountFacultative);
                            showWarning([data.message], 'Advertencia Reaseguro');
                        } else {
                            IsFacultative = false;
                            AmountFacultative = 0;
                        }
                    }
                });
            }
            //descuento lfotilla
            var realTotalVehiclesForDiscountFlotilla = qtyVehiclesByVehicle_Inclusion();
            var PercentByQtyVehicle = 0;

            $.ajax({
                url: '/Inclusion/GetPercentByQtyVehicle',
                type: 'POST',
                dataType: 'json',
                data: { qtyVehicles: realTotalVehiclesForDiscountFlotilla },
                async: false,
                success: function (data) {
                    PercentByQtyVehicle = data;
                }
            });

            $("#hdnPercentByQtyVehicle").val(PercentByQtyVehicle);
            var _totalByQtyVehicle = TotalByQtyVehicle_Inclusion(PercentByQtyVehicle);
            $("#hdnTotalByQtyVehicle").val(_totalByQtyVehicle);
        }
    });
}

function SaveVehicleChange() {

    var allchecks = $('.vehicleCheck');
    var checksCheckeds = false;
    var qtycheckeds = 0;

    $.each(allchecks, function (index, obj) {
        var $this2 = $(this);
        if ($this2.is(':checked')) {
            checksCheckeds = true;
            qtycheckeds += 1;
            return;
        }
    });

    if (qtycheckeds > 1) {
        showError(['No puede realizar cambios a mas de un vehiculo.'], 'Seleccionar Vehiculos Exclusión');
        return false;
    }

    if (!checksCheckeds) {
        showError(['Debe seleccionar un vehiculo a para poder realizar algun cambio.'], 'Seleccionar Vehiculo a Cambiar');
        return false;
    }


    var chassis = $("#chassis").val();
    if (chassis == "") {
        showError(['El campo Chasis Nuevo es requerido.'], 'Campo Requerido');
        return false;
    }

    var plate = $("#plate").val();
    if (plate == "") {
        showError(['El campo Placa Nueva es requerido.'], 'Campo Requerido');
        return false;
    }

    var ddlColor = $("#ddlColor").val();
    if (ddlColor == "") {
        showError(['El campo Color Nuevo es requerido.'], 'Campo Requerido');
        return false;
    }


    //valido el chassis
    var r = reviseChassisDuplicate();
    if (!r) {
        return false;
    }
    //




    var $Tbody = $("#tblVehicles").find("tbody");
    var dVehiclesChanges = [];

    $Tbody.find("tr").each(function () {
        var $this = $(this);
        var $checkbox = $this.find("input[type='checkbox']");
        if ($checkbox.prop('checked'))
            dVehiclesChanges.push(JSON.parse($this.data("dataVehicle")));
    });

    var vehicleList = [];
    var vehicleId = null;
    var ProratedPremium = 0;

    $.each(dVehiclesChanges, function (index, vehicleData) {
        var vparam = {};
        vparam.id = isNaN(vehicleId) || vehicleId == 0 ? null : vehicleId;
        vparam.IsFacultative = false;
        vparam.AmountFacultative = 0;
        vparam.vehicleDescription = vehicleData.Marca + " " + vehicleData.Modelo;
        vparam.ModeloDesc = vehicleData.Modelo;
        vparam.year = vehicleData.Ano;
        vparam.vehiclePrice = (vehicleData.ValorVehiculo == undefined || vehicleData.ValorVehiculo == null) ? 0 : vehicleData.ValorVehiculo;
        vparam.insuredAmount = (vehicleData.ValorVehiculo == undefined || vehicleData.ValorVehiculo == null) ? 0 : vehicleData.ValorVehiculo;
        vparam.percentageToInsure = 0;
        vparam.totalPrime = (vehicleData.Prima == undefined || vehicleData.Prima == null) ? 0 : vehicleData.Prima;
        vparam.totalIsc = 0;
        vparam.selectedProductCoreId = 0;
        vparam.selectedProductName = vehicleData.TipoPolizaDescripcion;
        vparam.vehicleMakeName = vehicleData.Marca;
        vparam.usageId = vehicleData.Iduso;
        vparam.usageName = vehicleData.Uso;
        vparam.storeId = vehicleData.IdEstacionamiento;
        vparam.storeName = vehicleData.Estacionamiento;

        var $hdnDriverData = $("#hdnDriverData").val();

        if ($hdnDriverData != "") {
            var driverData = JSON.parse($hdnDriverData);
            var driver_Id = driverData[0].Id;
            vparam.driver_Id = driver_Id;
            var IdentificationNumber = driverData[0].IdentificationNumber;
            var FullName = driverData[0].FirstName + " " + driverData[0].FirstSurname;
            vparam.principalFullName = FullName;
            vparam.principalIdentificationNumber = IdentificationNumber;
        }

        vparam.vehicleModel_Make_Id = vehicleData.IdMarca;
        vparam.vehicleModel_Model_Id = vehicleData.Idmodelo;
        vparam.selectedVehicleTypeId = vehicleData.Idtipovehiculo;
        vparam.selectedVehicleTypeName = vehicleData.DescripcionTarifa;
        vparam.selectedCoverageCoreId = vehicleData.SubRamo;
        vparam.selectedCoverageName = vehicleData.DescripcionSubramo;
        vparam.vehicleYearOld = '0 Km';
        vparam.SelectedDeductibleCoreId = 0;
        vparam.SelectedDeductibleName = '';
        vparam.selectedDeductible = '0';
        vparam.surChargePercentage = 0;
        vparam.rateJson = '';
        vparam.secuenciaVehicleSysflex = vehicleData.Item;
        vparam.isNotLaw = true
        vparam.ProratedPremium = vehicleData.primaproratadiasinimpuestos;
        vparam.vehicleQuantity = 1;

        vparam.Chassis = $("#chassis").val();
        vparam.Plate = $("#plate").val();
        vparam.Color = $("#ddlColor option:selected").val();

        vparam.OldChassis = vehicleData.chasis;
        vparam.OldPlate = vehicleData.placa;
        vparam.OldColor = vehicleData.color;


        vparam.conditionIdColor = vehicleData.ColorId;
        vparam.conditionIdChasis = vehicleData.ChasisId;
        vparam.conditionIdPlaca = vehicleData.PlacaId;

        vparam.ChangeID = $("#hdnChangeID").val() == "" ? "" : $("#hdnChangeID").val();

        vparam.isFinanced = vehicleData.Financed;

        vparam.GlobalDataDeductibleList = null;
        var productLimit = {};
        productLimit.TotalIsc = 0;
        productLimit.TotalDiscount = 0;
        productLimit.TotalPrime = 0;
        var servicesPrime = 0;
        var tpPrime = 0;
        var sdPrime = 0;
        productLimit.ServicesPrime = servicesPrime;
        productLimit.TpPrime = tpPrime;
        productLimit.SdPrime = sdPrime;
        vparam.ServicesSelected = null;
        vparam.limitself = null;
        vparam.productLimit = productLimit;
        vparam.PercentByQtyVehicle = 0;
        vparam.TotalByQtyVehicle = 0;

        vehicleList.push(vparam);
    });

    var policyNoMain = $('#policyNoMain').val();
    var IntermediarioId = $("#hdnIntermediarioId").val();
    var _dataCustomer = $("#hdnCustomerDataInclusion").val();
    var EndDate = $("#hdnEndDate").val();
    var StartDate = '';

    var hdnQuotationId = $("#hdnQuotationId").val();
    var re = false;
    $.ajax({
        url: '/Inclusion/SaveVehicleChanges',
        type: 'POST',
        dataType: 'json',
        data: {
            jsondataVehicle: JSON.stringify(vehicleList),
            StartDate: StartDate,
            EndDate: EndDate,
            AgentCode: parseInt(IntermediarioId),
            DataCustomer: _dataCustomer,
            policyNoMain: policyNoMain,
            QuotationId: hdnQuotationId
        },
        async: false,
        success: function (data) {
            if (data.messageError) {
                showError([data.messageError], "Ha ocurrido el siguiente error");
                return false;
            }

            re = true;
            $("#spQuotationNumber").text(data.QuotationNumber + " / " + policyNoMain);
            $("#quotationID").val(data.QuotationId);

            $("#lnkContinue").off("click");
        }
    });

    return re;
}

function SaveVehicleExlcusion() {

    var fcha = $("#ExcludeStartDate").val();
    if (fcha == "") {
        showError(['Debe seleccionar una Fecha de Exclusión.'], 'Fecha de Exclusión');
        return false;
    }

    var tblfilters = $('.tblfilter');
    var filterWithValue = 0;
    $.each(tblfilters, function (index, obj) {
        var $tblfilter = $(this);
        if ($tblfilter.val() !== "") {
            filterWithValue += 1;
        }
    });

    if (filterWithValue > 0) {
        showError(['Favor limpiar los campos de filtro (Marca, Modelo, Chasis) para poder continuar.'], 'Limpiar filtros');
        return false;
    }

    var vehiclesSelected = qtblVehicles.cells('td.ischecked').nodes();
    debugger
    if (vehiclesSelected.length == 0) {

        var allchecks = $('.vehicleCheck');
        var checksCheckeds = false;

        if (allchecks.length == 1) {
            showError(['No puede realizar una exclusion en una poliza de un solo vehiculo.'], 'Seleccionar Vehiculos Exclusión');
            return false;
        }
    }

    $.each(vehiclesSelected, function (index, obj) {
        var $thisTd = $(this);
        var $checkbox = $thisTd.find("input[type='checkbox']");

        if ($checkbox.is(':checked')) {
            checksCheckeds = true;
            return;
        }
    });

    if (!checksCheckeds) {
        showError(['Debe seleccionar un vehiculo a Excluir.'], 'Seleccionar Vehiculo a Excluir');
        return false;
    }

    var dExcludeVehicles = [];

    $.each(vehiclesSelected, function (index, vhs) {
        var $thisTd = $(this);
        var $thisTr = $thisTd.parent();
        dExcludeVehicles.push(JSON.parse($thisTr.data("dataVehicle")));
    });



    var vehicleList = [];
    var vehicleId = null;
    var ProratedPremium = 0;

    $.each(dExcludeVehicles, function (index, vehicleData) {
        var vparam = {};
        vparam.id = isNaN(vehicleId) || vehicleId == 0 ? null : vehicleId;
        vparam.IsFacultative = false;
        vparam.AmountFacultative = 0;
        vparam.vehicleDescription = vehicleData.Marca + " " + vehicleData.Modelo;
        vparam.ModeloDesc = vehicleData.Modelo;
        vparam.year = vehicleData.Ano;
        vparam.vehiclePrice = (vehicleData.ValorVehiculo == undefined || vehicleData.ValorVehiculo == null) ? 0 : vehicleData.ValorVehiculo;
        vparam.insuredAmount = (vehicleData.ValorVehiculo == undefined || vehicleData.ValorVehiculo == null) ? 0 : vehicleData.ValorVehiculo;
        vparam.percentageToInsure = 0;
        vparam.totalPrime = (vehicleData.Prima == undefined || vehicleData.Prima == null) ? 0 : vehicleData.Prima;
        vparam.totalIsc = 0;
        vparam.selectedProductCoreId = 0;
        vparam.selectedProductName = vehicleData.TipoPolizaDescripcion;
        vparam.vehicleMakeName = vehicleData.Marca;
        vparam.usageId = vehicleData.Iduso;
        vparam.usageName = vehicleData.Uso;
        vparam.storeId = vehicleData.IdEstacionamiento;
        vparam.storeName = vehicleData.Estacionamiento;

        var $hdnDriverData = $("#hdnDriverData").val();

        if ($hdnDriverData != "") {
            var driverData = JSON.parse($hdnDriverData);
            var driver_Id = driverData[0].Id;
            vparam.driver_Id = driver_Id;
            var IdentificationNumber = driverData[0].IdentificationNumber;
            var FullName = driverData[0].FirstName + " " + driverData[0].FirstSurname;
            vparam.principalFullName = FullName;
            vparam.principalIdentificationNumber = IdentificationNumber;
        }

        vparam.vehicleModel_Make_Id = vehicleData.IdMarca;
        vparam.vehicleModel_Model_Id = vehicleData.Idmodelo;
        vparam.selectedVehicleTypeId = vehicleData.Idtipovehiculo;
        vparam.selectedVehicleTypeName = vehicleData.DescripcionTarifa;
        vparam.selectedCoverageCoreId = vehicleData.SubRamo;
        vparam.selectedCoverageName = vehicleData.DescripcionSubramo;
        vparam.vehicleYearOld = '0 Km';
        vparam.SelectedDeductibleCoreId = 0;
        vparam.SelectedDeductibleName = '';
        vparam.selectedDeductible = '0';
        vparam.surChargePercentage = 0;
        vparam.rateJson = '';
        vparam.secuenciaVehicleSysflex = vehicleData.Item;
        vparam.isNotLaw = true
        vparam.ProratedPremium = vehicleData.primaproratadiasinimpuestos;
        vparam.vehicleQuantity = 1;
        vparam.Chassis = vehicleData.chasis;
        vparam.Plate = vehicleData.placa;
        vparam.Color = vehicleData.color;
        vparam.isFinanced = vehicleData.Financed;

        vparam.GlobalDataDeductibleList = null;
        var productLimit = {};
        productLimit.TotalIsc = 0;
        productLimit.TotalDiscount = 0;
        productLimit.TotalPrime = 0;
        var servicesPrime = 0;
        var tpPrime = 0;
        var sdPrime = 0;
        productLimit.ServicesPrime = servicesPrime;
        productLimit.TpPrime = tpPrime;
        productLimit.SdPrime = sdPrime;
        vparam.ServicesSelected = null;
        vparam.limitself = null;
        vparam.productLimit = productLimit;
        vparam.PercentByQtyVehicle = 0;
        vparam.TotalByQtyVehicle = 0;

        vehicleList.push(vparam);
    });

    var policyNoMain = $('#policyNoMain').val();
    var IntermediarioId = $("#hdnIntermediarioId").val();
    var _dataCustomer = $("#hdnCustomerDataInclusion").val();
    var EndDate = $("#hdnEndDate").val();
    var StartDate = $("#ExcludeStartDate").val();

    var hdnQuotationId = $("#hdnQuotationId").val();
    var re = false;
    $.ajax({
        url: '/Inclusion/SaveVehicleExclusion',
        type: 'POST',
        dataType: 'json',
        data: {
            jsondataVehicle: JSON.stringify(vehicleList),
            StartDate: StartDate,
            EndDate: EndDate,
            AgentCode: parseInt(IntermediarioId),
            DataCustomer: _dataCustomer,
            policyNoMain: policyNoMain,
            QuotationId: hdnQuotationId
        },
        async: false,
        success: function (data) {
            if (data.messageError) {
                showError([data.messageError], "Ha ocurrido el siguiente error");
                return false;
            }

            re = true;
            $("#spQuotationNumber").text(data.QuotationNumber + " / " + policyNoMain);
            $("#quotationID").val(data.QuotationId);

            $("#lnkContinue").off("click");
        }
    });

    return re;
}

function SaveVehicleInclusion() {
    //Validar que al menos se haya 1 vehiculo
    var $selecCober = $("#selecCober");
    var $hdnHasPrime = $selecCober.find("input[id='hdnHasPrime']");
    var $VehiclesSection = $selecCober.find("div[id*='dvVehicle']");

    if ($hdnHasPrime.length == 0) {
        showWarning(['Para poder continuar debe agregar al menos un vehiculo en la lista'], 'Advertencia');
        return false;
    } else {

        var IsFalseHasPrime = [];
        $hdnHasPrime.each(function () {
            var $this = $(this);
            var hasPrime = ($this.val().toLowerCase() == "true");
            if (!hasPrime)
                IsFalseHasPrime.push(hasPrime);
        });

        if (IsFalseHasPrime.length > 0) {
            showWarning(['Para poder continuar debe completar la información de todos los vehiculos'], 'Advertencia');
            return false;
        }
    }

    $("#lnkContinue").off("click");

    var vehicleList = [];

    //descuento flotilla
    var PercentByQtyVehicle = parseFloat($("#hdnPercentByQtyVehicle").val());
    var TotalByQtyVehicle = parseFloat($("#hdnTotalByQtyVehicle").val());
    // 

    var vehicleCounter = 0;
    $VehiclesSection.each(function () {
        var $this = $(this);
        vehicleCounter++;
        var jsonVehicleData = $this.find("input[id='hdnVehicleDataFromSaveAndEdit']").val();
        var vehicleData = JSON.parse(jsonVehicleData);
        var vehicleQty = parseInt($this.find("[id='cvehi']").val());
        var TotalPrimeWithoutTax = parseFloat($this.find("input[id='totalPrimeWithoutTax']").val());
        var TotalTax = parseFloat($this.find("input[id='totalIsc']").val());
        var TotalPrimeWithTax = parseFloat($this.find("input[id='totalPrime']").val());
        var ProratedPremium = parseFloat($this.find("input[id='primaproratadiasinimpuestos']").val());
        var IsFacultative = $this.find("input[id='hdnIsFacultative']").val() == "true";
        var AmountFacultative = parseFloat($this.find("input[id='hdnAmountFacultative']").val());

        var $ddlCoverage = $this.find(".coverage");
        var coverageLimits = $ddlCoverage.data("coverageLimits");
        var Deductibles = $ddlCoverage.data("Deductibles");

        var $ddlDeducible = $this.find(".Deducible").find("option:selected");
        var vehicleId = $this.find("input[id='VehicleId']").val();


        var vparam = {};
        vparam.id = isNaN(vehicleId) || vehicleId == 0 ? null : vehicleId;
        vparam.IsFacultative = IsFacultative;
        vparam.AmountFacultative = AmountFacultative;
        vparam.vehicleDescription = vehicleData.vehicleDescription;
        vparam.year = vehicleData.Year;
        vparam.vehiclePrice = vehicleData.VehicleValue;
        vparam.insuredAmount = vehicleData.VehicleValue;
        vparam.percentageToInsure = 0;
        vparam.totalPrime = TotalPrimeWithoutTax;
        vparam.totalIsc = TotalTax;
        var $CoverageSelected = $this.find(".coverage").find("option:selected");
        var $productSelected = $this.find("select[id='ddlAllProducts']").find("option:selected");
        vparam.selectedProductCoreId = $productSelected.val();
        vparam.selectedProductName = $productSelected.text();
        vparam.vehicleMakeName = vehicleData.vehicleMakeName;
        vparam.usageId = vehicleData.PrincipalUsageId;
        vparam.usageName = vehicleData.PrincipalUsageDesc;
        vparam.storeId = vehicleData.StorageId;
        vparam.storeName = vehicleData.storeName;

        var $hdnDriverData = $("#hdnDriverData").val();

        if ($hdnDriverData != "") {
            var driverData = JSON.parse($hdnDriverData);
            var driver_Id = driverData[0].Id;
            vparam.driver_Id = driver_Id;
            var IdentificationNumber = driverData[0].IdentificationNumber;
            var FullName = driverData[0].FirstName + " " + driverData[0].FirstSurname;
            vparam.principalFullName = FullName;
            vparam.principalIdentificationNumber = IdentificationNumber;
        }

        vparam.vehicleModel_Make_Id = vehicleData.makeId;
        vparam.vehicleModel_Model_Id = vehicleData.modelId;
        var SelectedVehicleTypeId = $this.find("input[id='SelectedVehicleTypeId']").val();
        vparam.selectedVehicleTypeId = SelectedVehicleTypeId;
        vparam.selectedVehicleTypeName = vehicleData.TypeIdDesc;
        vparam.selectedCoverageCoreId = $CoverageSelected.val();
        vparam.selectedCoverageName = $CoverageSelected.text();
        vparam.vehicleYearOld = vehicleData.VehicleYearsOld;
        vparam.SelectedDeductibleCoreId = $ddlDeducible.val();
        vparam.SelectedDeductibleName = $ddlDeducible.text();
        vparam.selectedDeductible = $ddlDeducible.val();

        vparam.surChargePercentage = 0;
        if ($this.find("select[id='SurchargePercent']").length > 0) {

            if ($this.find("select[id='SurchargePercent']").find("option:selected").val() != "") {

                vparam.surChargePercentage = $this.find("select[id='SurchargePercent']").find("option:selected").val();
            }
        }

        var rateJson = $this.find("input[id='rateJson']").val();
        vparam.rateJson = rateJson;
        var Secuencia = $this.find("input[id='Secuencia']").val();

        vparam.secuenciaVehicleSysflex = Secuencia;
        var EsDeLey = $this.find("input[id='IsLawProduct']").val() == "true";
        vparam.isNotLaw = !EsDeLey;
        vparam.ProratedPremium = ProratedPremium;
        vparam.vehicleQuantity = vehicleQty;
        vparam.GlobalDataDeductibleList = Deductibles;
        var ServicesSelected = JSON.parse($this.find("input[id='AdditionalServices']").val());
        var productLimit = {};
        productLimit.TotalIsc = TotalTax;
        productLimit.TotalDiscount = 0;
        productLimit.TotalPrime = TotalPrimeWithTax;
        var servicesPrime = parseFloat($this.find("input[id='servicesPrime']").val());
        var tpPrime = parseFloat($this.find("input[id='tpPrime']").val());
        var sdPrime = parseFloat($this.find("input[id='sdPrime']").val());
        productLimit.ServicesPrime = servicesPrime;
        productLimit.TpPrime = tpPrime;
        productLimit.SdPrime = sdPrime;
        vparam.ServicesSelected = ServicesSelected;
        vparam.limitself = coverageLimits;
        vparam.productLimit = productLimit;
        vparam.PercentByQtyVehicle = PercentByQtyVehicle;
        vparam.TotalByQtyVehicle = TotalByQtyVehicle;

        vparam.SelectedVehicleFuelTypeId = vehicleData.SelectedVehicleFuelTypeId;
        vparam.SelectedVehicleFuelTypeDesc = vehicleData.SelectedVehicleFuelTypeDesc;

        vehicleList.push(vparam);
    });

    $.ajax({
        url: '/Inclusion/SaveVehicleInclusion',
        type: 'POST',
        dataType: 'json',
        data: { jsondataVehicle: JSON.stringify(vehicleList) },
        async: false,
        success: function (data) {
            $("#hdnIsInclusion").val("S");
        }
    });

    return true;
}

function DeleteVehicleInclusion(SecuenciaVehicleSysflex, quotationCoreNumber, vehicleId) {

    $.ajax({
        url: '/Inclusion/DeleteVehicleOnSysflexInclusion',
        type: 'POST',
        dataType: 'json',
        data: { SecuenciaVehicleSysflex: SecuenciaVehicleSysflex, quotationCoreNumber: quotationCoreNumber, vehicleId: vehicleId },
        async: false,
        success: function (data) {
            if (data == "ERROR") {
                showError(['A ocurrido un error Eliminando el Vehículo'], 'Eliminando Vehículo');
            }
        }
    });
}

function ShowServicesInclusion(obj) {

    var IddvVehicle = $(obj).attr("data");
    var $dvVehicle = $("#" + IddvVehicle);
    var $ddlCoverage = $dvVehicle.find(".coverage");
    var $ddlAllProducts = $dvVehicle.find("select[id='ddlAllProducts']");
    var coverageLimits = $ddlCoverage.data("coverageLimits");
    var $AdditionalServices = $dvVehicle.find("input[id='AdditionalServices']");
    var $hdnServiceSelected = $dvVehicle.find("input[id='hdnServiceSelected']");
    var EnabledShowServices = true;

    if ($ddlCoverage.val() == "" || $ddlAllProducts.val() == "")
        EnabledShowServices = false;

    if (!EnabledShowServices) {
        showWarning(['Debe completar los datos del plan y la cobertura'], 'Advertencia');
        return false;
    }

    //Servicios por defecto
    var CoverageServices = JSON.parse($AdditionalServices.val());
    var StrDataServices = JSON.stringify(CoverageServices);

    $.ajax({
        type: "POST",
        url: "/Inclusion/_AdditionalServices",
        data: JSON.stringify({ jsonDataServices: StrDataServices }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $("#servicesBody").html(data)
            SetTotalServices();
            $("#addServicios").modal({ backdrop: 'static', keyboard: false, show: true });
            $("#dvVehicleIdFromAdditionalServices").val(IddvVehicle);
            var $btnSave = $(".saveServices")
            $btnSave.off("click");
            $btnSave.click(function () {
                SaveServicesSelected($dvVehicle);
            });
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function getSelectedServices(SelectedServices, CoverageServices, AllServiceFalse) {
    var ListServices = [];
    $.each(CoverageServices, function (index, value) {
        var Services = {};
        var ListCoverage = [];
        Services.Id = value.Id;
        Services.Name = value.Name;
        Services.UserId = value.UserId;
        $.each(value.Coverages, function (index2, valueCoverage) {
            var Coverage = {};
            //Determinar si este servicio fue seleccionado
            if (!AllServiceFalse) {
                var itemResult = altFind(SelectedServices, function (item) {
                    return item.CoverageDetailCoreId == valueCoverage.CoverageDetailCoreId;
                });
                Coverage.IsSelected = (itemResult != null) ? itemResult.IsSelected : false;
            } else
                Coverage.IsSelected = false;

            Coverage.Id = valueCoverage.Id;
            Coverage.CoverageDetailCoreId = valueCoverage.CoverageDetailCoreId;
            Coverage.Name = valueCoverage.Name;
            Coverage.Amount = valueCoverage.Amount;
            Coverage.MinDeductible = valueCoverage.MinDeductible;
            Coverage.Limit = valueCoverage.Limit;
            Coverage.Deductible = valueCoverage.Deductible;
            Coverage.UserId = valueCoverage.UserId;
            Coverage.Modi_Date = valueCoverage.Modi_Date;
            ListCoverage.push(Coverage);
        });

        Services.Coverages = ListCoverage;
        ListServices.push(Services);
    });

    return ListServices;
}

function SetAdditionalSelected(SelectedServices, jsonDataServices, $hdnAdditionalServices) {
    $.ajax({
        type: "POST",
        url: "/Inclusion/SetAdditionalServicesCheckMark",
        data: JSON.stringify({ jsonDataServices: jsonDataServices, SelectedServices: SelectedServices }),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            $hdnAdditionalServices.val(data);
            var IddvVehicle = $("#dvVehicleIdFromAdditionalServices").val();
            var $dvVehicle = $("#" + IddvVehicle);
            var $obj = $dvVehicle.find("select.Deducible");
            GetRateInclusionFromDrop($obj);
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function SaveServicesSelected($divParent) {
    var $radios = $("#servicesBody").find("input[type='radio']");
    var $hdnAdditionalServices = $divParent.find("input[id='AdditionalServices']");
    var AdditionalServicesJSON = $hdnAdditionalServices.val();
    var SelectedServices = [];

    $radios.each(function () {
        var $this = $(this);
        //Enviar la data de los servicios seleccionados
        var CoverageGroupName = $this.attr("data");
        var CoverageDetailCoreId = $this.attr("dataItem");
        var item = {}
        item.CoverageGroupName = CoverageGroupName;
        item.CoverageDetailCoreId = CoverageDetailCoreId;
        item.isSelected = $this.prop("checked");
        SelectedServices.push(item);
    });

    SetAdditionalSelected(JSON.stringify(SelectedServices), AdditionalServicesJSON, $hdnAdditionalServices);

}

function SetTotalServices() {
    var $radios = $("#servicesBody").find("input[type='radio']:checked");
    var Amount = 0;

    $radios.each(function () {
        var $this = $(this);
        var AmounText = replaceAll(",", "", $this.parents().eq(3).find("td").eq(2).text().trim());
        Amount += parseFloat(AmounText);
    });

    $("#txtTotalServicesSelected").val(number_format(Amount, 2));
}

function ClearSelection(obj) {
    var RadioGroupName = $(obj).attr("data");
    $("#servicesBody").find("input[name='" + RadioGroupName + "']").prop("checked", false);
    SetTotalServices();
}

function ClearForm($dvParent) {
    var DropChosen = $dvParent.find(".chosen-select-deselect");
    DropChosen.val("");
    //DropChosen.trigger("chosen:updated");
    DropChosen.trigger("change.select2");

    clearDrop($("#ddlModelo"));
    clearDrop($("#ddlType"));
    clearDrop($("#ddlPrincipalUsage"));

    var NormalDrop = $dvParent.find("select[class='mdl-textfield__input']");
    NormalDrop.val("");
    NormalDrop.parent().removeClass("is-dirty");

    var TextBox = $dvParent.find("input[type='text']");
    TextBox.val("");
    TextBox.parent().removeClass("is-dirty");
}

function isLawProductInclusion($dvVehicle) {
    var $ddlCoverage = $dvVehicle.find(".coverage");
    var selectedCoverage = $ddlCoverage.val();
    var CoveragesByUsagesFilter = $ddlCoverage.data("CoveragesByUsagesFilter");

    if (selectedCoverage) {

        var prod = altFind(CoveragesByUsagesFilter, function (item) {
            return item.Id == selectedCoverage;
        });

        if (prod) {
            return prod.IsLaw;
        }
    } else {
        return true;
    }
}

function enableVehicleOldCombo_Inclusion(vehicleYear, isEditingVehicle) {

    //pendiente logica: ME FALTA LA LOGICA DE CUANDO SEA NO SEA 0 KM BLOQUEAR ESE PRODUCTO self.remove0kmIfIsNotNew
    var VehicleYearsOld = $("#VehicleYearsOld ");

    if (vehicleYear > 0) {

        var QtyYearsBack0KmVip = "";

        $.ajax({
            url: "/Inclusion/GetQtyYearsBack0KmVipInclusion",
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
        }
        else {

            VehicleYearsOld.val("Usado");
            //VehicleYearsOld.trigger("chosen:updated");
            VehicleYearsOld.trigger("change.select2");
            VehicleYearsOld.parent().addClass("is-dirty");
            VehicleYearsOld.trigger("change");

            $("#LastSelectedVehicleYearsOld").val(VehicleYearsOld.val());
            //self.isFirstLoading(false);

            VehicleYearsOld.attr("disabled", "disabled");
        }
    }
    else
        VehicleYearsOld.removeAttr("disabled");
}

function qtyVehiclesByVehicle_Inclusion() {

    //contando la cantidad de cada Vehículo
    var totalQtyByVehicle = 0;
    var $selecCober = $("#selecCober");
    var $hdnHasPrime = $selecCober.find("input[id='hdnHasPrime']");
    var $VehiclesSection = $selecCober.find("div[id*='dvVehicle']");

    $VehiclesSection.each(function () {

        var $this = $(this);

        var actualVehicleQty = parseInt($this.find("[id='cvehi']").val());

        totalQtyByVehicle += actualVehicleQty;
    });

    return totalQtyByVehicle;
}

//monto de descuento flotilla
function TotalByQtyVehicle_Inclusion(_percentByQtyVehicle) {

    if (_percentByQtyVehicle > 0) {

        var totalPrime = getTotalPrimeOfAllVehicles_Inclusion();

        var result = totalPrime * (_percentByQtyVehicle / 100);

        return result;
    }
    return 0;
}

function getTotalPrimeOfAllVehicles_Inclusion() {
    var total = 0;
    var $selecCober = $("#selecCober");
    var $VehiclesSection = $selecCober.find("div[id*='dvVehicle']");


    $VehiclesSection.each(function () {

        var $this = $(this);
        var actualVehicleQty = parseInt($this.find("[id='cvehi']").val());
        var actualVehiculePrime = parseFloat($this.find("input[id='totalPrimeWithoutTax']").val());
        //var actualVehiculePrimeProrrated = parseFloat($this.find("input[id='primaproratadiasinimpuestos']").val());
        //var DiasCotizados = GetQuotedDays();// parseFloat($this.find("input[id='DiasCotizados']").val());

        var ToPrimeAddQtyVehi = (actualVehiculePrime * actualVehicleQty); //* DiasCotizados;

        total += ToPrimeAddQtyVehi;
    });

    return total;
}

function GetQuotedDays() {
    var value = 0;

    $.ajax({
        type: "POST",
        url: "/Inclusion/getQuotedDays",
        data: {},
        async: false,
        success: function (data) {

            value = data;
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });

    return value;
}

function setDataChangeVehicle(obj) {

    if (obj) {
        var realObj = JSON.parse(obj);

        $("#Actualchassis").val(realObj.chasis);
        $("#Actualchassis").parent().addClass("is-dirty");

        $("#Actualplate").val(realObj.placa);
        $("#Actualplate").parent().addClass("is-dirty");

        $("#ActualColor").val(realObj.color);
        $("#ActualColor").parent().addClass("is-dirty");

        $("#chassis").val();
        $("#plate").val();
        //$("#ddlColor").val();
        //$("#ddlColor").trigger("change");
        //$("#ddlColor").trigger("change.select2");   
    }
}

function getVehicleRequestChange(pl, ite) {

    $.ajax({
        type: "POST",
        url: "/Inclusion/GetVehicleRequestChange",
        data: { plno: pl, item: ite },
        async: false,
        success: function (data) {

            $("#chassis").val(data.newChassis);
            $("#chassis").parent().addClass("is-dirty");

            $("#plate").val(data.newPlate);
            $("#plate").parent().addClass("is-dirty");

            $("#ddlColor").val(data.newColor);
            $("#ddlColor").trigger("change.select2");

            $("#hdnChangeID").val(data.chID);
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function reviseChassisDuplicate() {

    var chassis = $("#chassis").val();
    var plate = $("#plate").val();
    var pl = $("#policyNoMain").val();

    var result = true;

    $.ajax({
        type: "POST",
        url: "/Inclusion/CheckChassisPlateLawProducts",
        data: { Chassis: chassis, Plate: plate, Poliza: pl },
        async: false,
        success: function (data) {
            if (!data.success) {
                result = data.success;

                showError([data.message], 'Chasis o Placa duplicado');

                return result;
            }
        },
        failure: function (data) {
            alert(data.responseText);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });

    return result;
}

function GetFuelType_Inclusion(fueltypeid, makeid, modelid) {

    $.ajax({
        url: "/Home/GetFuelType",
        dataType: "json",
        async: false,
        data: { FuelTypeId: fueltypeid, MakeId: makeid, ModelId: modelid },
        success: function (result) {
            var $select_elem = $("#fuelType");
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
