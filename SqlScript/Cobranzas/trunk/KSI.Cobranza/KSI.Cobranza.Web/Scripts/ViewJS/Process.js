﻿$(document).ready(function () {
    HideHeaderGrid();
    $('.myrow').click(function () {
        var datos = $(this).attr('data-json');
        if (datos.length <= 0) {
            return;
        }
        var row = JSON.parse(datos);

        $('#PolicyType').val(row.policyTypeName);
        $('#Aseguradora').val(row.FullName);
        $('#PolicyNo').val(row.policyNo);
        $('#PolicyAmount').val(number_format(row.endorseAmount, 2));

        $('#PolicyDescripcion').val(row.policyCollateralName);
        $('#PolicyEstado').val(row.isActive == 1 ? 'Activo' : 'Inactivo');

        var dFormat = "dd/mm/yyyy";
        var PolicyDate = new Date(row.Date);
        $('#PolicyDate').val(PolicyDate.format(dFormat));

        var EffectiveDate = new Date(row.EffectiveDate);
        $('#EffectiveDate').val(EffectiveDate.format(dFormat));
    });

    $(document).on('click', '#ApplyFilters', function () {
        var filters = [];
        var $dvReceipOfPayment = $('#dvReceipOfPayment');        
        var controls = $dvReceipOfPayment.find('input[type="checkbox"]:checked');

            controls.each(function () {
                var $this = $(this);
                
                filters.push($this.val());
                
            })

            if (filters.length > 0) {
                GetQuotaInformationGrid(filters, $('#hdnAccountId').val());
                //GetDataFilterReceipOfPayment(filters, $('#hdnAccountId').val());
            }
    });


    $(document).on('change', '#ProjectedFilterBy', function () {
        var $this = $(this);
        if ($this.val() != null && ($this.val() == 4 || $this.val() == 5)) {
            $('#ProjectedAmountToPay').removeAttr('disabled');
        } else {
            $('#ProjectedAmountToPay').prop('disabled', 'disabled');
            $('#ProjectedAmountToPay').val('');
        }
    });

    $(document).on('click', '#ProjectedApplyFilters', function () {
        var ProjectedAmountToPay = parseFloat($('#ProjectedAmountToPay').val() == '' ? 0 : $('#ProjectedAmountToPay').val());
        var ProjectedFilterBy = $('#ProjectedFilterBy').val();
        var $hdnAccountId = $('#hdnAccountId');
        var FilterDateProjected = $('#FilterDateProjected').val();
        var totalAmount = parseFloat($('#OutStandingBalance').val().replace(',',''));//saldo total

        if (ProjectedFilterBy == '') {
            ShowMessageBS("Debe seleccionar el tipo de filtro", "Filtrar datos");
            return false;
        }
        else if (FilterDateProjected == '') {
            ShowMessageBS("Debe indicar la fecha a la que desea visualizar los datos", "Filtrar datos");
            return false;
        }
        else if ((ProjectedFilterBy == 4 || ProjectedFilterBy == 5) && (ProjectedAmountToPay == '' || ProjectedAmountToPay <= 0)) {
            ShowMessageBS("Debe indicar el monto a pagar antes de aplicar los filtros", "Filtrar datos");
            return false;
        }
        else if ((ProjectedFilterBy == 4 || ProjectedFilterBy == 5) && ProjectedAmountToPay > totalAmount) {
            ShowMessageBS("El monto a pagar no puede ser mayor que el saldo actual", "Filtrar datos");
            return false;
        }
        else {
            GetProjectedGridStatement($hdnAccountId.val(), FilterDateProjected, ProjectedFilterBy, ProjectedAmountToPay);
        }

    });

    SetDatePicker();
});

function HideHeaderGrid() {
    var $GridDetail = $("#gvGuaranteesVehiclesDetail");
    $GridDetail.find("thead").remove();
}

function InitVehicles() {
    var $Grid = $("#gvGuaranteesVehicles");
    var $rows = $Grid.find("tbody").find("tr");

    $rows.on({
        click: function () {
            var $row = $(this);
            var $hidden = $row.find("input:hidden");
            GetGuaranteeVehicleDetail($hidden.val());
        },
        mouseover: function () {
            var $row = $(this);
            $row.css("cursor", "pointer");
        }
    });

    HideHeaderGrid();

}

function GetPhones(CustId) {
    GetPartialView("/Process/_ContactPhone", JSON.stringify({ CustomerId: CustId }), function (data) {
        var $Container = $("#dvPhones");
        $Container.html(data);
        var lnkBase = $("#hdnURLBase").val();
        $("#gvPhones tfoot a").each(function () {
            var $this = $(this);
            $this.attr("href", lnkBase + $this.text())
        });  
    }, true);
}

function GetProjectedStatement(accountId) {
    GetPartialView("/Process/_ProjectedStatement", JSON.stringify({ accountId: accountId }), function (data) {
        var $Container = $("#dvProjectedStatement");
        $Container.html(data);
        SetDatePicker();
    }, true);
}
function GetProjectedGridStatement(accountId, dateStatement, idTipo, pMontoPago) {
    GetPartialView("/Process/_GridProjectedStatement", JSON.stringify({ accountId: accountId, dateStatement: dateStatement, idTipo: idTipo, montoPago: pMontoPago }), function (data) {
        var $Container = $("#dvGridProjectedStatement");
        $Container.html(data);
        $('#OutStandingBalance').val($('#hdnTotalCalculate').val());
    }, true);

}

function GetEmails(CustId) {
    GetPartialView("/Process/_ContactEmailAddress", JSON.stringify({ CustomerId: CustId }), function (data) {
        var $Container = $("#dvEmails")
        $Container.html(data);
        var lnkBase = $("#hdnURLBase").val();

        $("#gvEmails tfoot a").each(function () {
            var $this = $(this);
            $this.attr("href", lnkBase + $this.text())
        });
    }, true);
}

function CheckPrimary(CustId, Id) {
    CallAjax("/Process/CheckPrimaryPhone", { CustomerId: CustId, Id: Id }, "", function (data) {
        GetPhones(CustId);
    }, "GET", true);
}

function CheckPrimaryEmail(CustId, Id) {
    CallAjax("/Process/CheckPrimaryEmail", { CustomerId: CustId, Id: Id }, "", function (data) {
        GetEmails(CustId);
    }, "GET", true);
}

function GetGuarantee(AccountId, ViewName) {
    GetPartialView("/Process/" + ViewName, JSON.stringify({ AccountId: AccountId }), function (data) {
        var ContainerId = ViewName == "_GuaranteeList" ? "#dvGuarantee" : "#dvVehicles";
        var $Container = $(ContainerId);
        $Container.html(data);

        if (ViewName == "_VehicleInformation") {
            InitVehicles();
            var $RowClick = $("#gvGuaranteesVehicles tbody tr:first");
            try {
                $($RowClick).click();
            } catch (e) {

            }
        }
    }, true);
}

function GetGuaranteeVehicleDetail(CollateralId) {
    GetPartialView("/Process/_VehicleDetail", JSON.stringify({ CollateralId: CollateralId }), function (data) {
        var ContainerId = "#dvVehicleDetail";
        var $Container = $(ContainerId);
        $Container.html(data);
        HideHeaderGrid();
    }, true);
}

function GetLoanDetail(quotationId, loanNumber, accountId) {
    GetPartialView("/Process/_LoanInformation", JSON.stringify({ quotationId: quotationId, loanNumber: loanNumber, accountId: accountId }), function (data) {
        var $Container = $("#dvLoanInformation")
        $Container.html(data);
    }, true);
}

function GetCodeudor(AccountId) {
    GetPartialView("/Process/_CodeudorInformation", JSON.stringify({ AccountId: AccountId }), function (data) {
        var $Container = $("#dvCodeudorInformation")
        $Container.html(data);
    }, true);
}

function GetPaymentPlan(AccountId) {
    GetPartialView("/Process/_PaymentPlan", JSON.stringify({ AccountId: AccountId }), function (data) {
        var $Container = $("#dvPaymentPlan")
        $Container.html(data);
        InitGridPlanPago();
    }, true);
}

function GetPolicyInformation(AccountId) {
    GetPartialView("/Process/_PolicyInformation", JSON.stringify({ AccountId: AccountId }), function (data) {
        var $Container = $("#dvPolicyInformation")
        $Container.html(data);
    }, true);
}

function InitGridPlanPago() {
    var $grid = $("#gvPaymentPlan");
    var $foot = $grid.find("tfoot");
    var accountId = $("#hdnAccountId").val();
    $foot.find("a").each(function () {
        var $this = $(this);
        var Originalhref = $this.attr("href");
        var Targethref = Originalhref + "&AccountId=" + accountId;
        $this.attr("href", Targethref);
    });

    InitializeGrid($grid);
}

function GetCodeudorPersonalInformation(Id) {
    GetPartialView("/Process/GetCodeudorPersonalInformation", JSON.stringify({ Id: Id }), function (data) {
        ShowPopupBS(data, "Codeudor", function () {

        });
    }, true);
}

function GetPhoneView(Id) {
    GetPartialView("/Process/_frmPhone", JSON.stringify({ Id: Id }), function (data) {
        ShowPopupBS(data, "AGREGAR TELEFONO", function () {

        });
        SetMask();
        $.validator.unobtrusive.parse($("#dvContainerPhone"));
        $('#IsActive').prop('checked', true);
    }, true);
}

function GetEmailView(Id) {
    GetPartialView("/Process/_frmEmail", JSON.stringify({ Id: Id }), function (data) {
        ShowPopupBS(data, "AGREGAR CORREO", function () {

        });
        SetMask();
        $.validator.unobtrusive.parse($("#dvContainerEmail"));
        $('#IsActive').prop('checked', true);
    }, true);
}

function GetQuotaInformation(AccountId) {
    GetPartialView("/Process/_ReceipOfPayment", JSON.stringify({ AccountId: AccountId }), function (data) {
        var $Container = $("#dvReceipOfPayment")
        $Container.html(data);
        InitQuotaInformation();
        InitGridReceiptOfPayment();
    }, true);
}

function GetQuotaInformationGrid(pFilters, AccountId) {
    GetPartialView("/Process/_GridReceipOfPayment", JSON.stringify({ AccountId: AccountId, filters: pFilters }), function (data) {
        var $Container = $("#dvGridReceipOfPayment")
        $Container.html(data);
        InitQuotaInformation();
        InitGridReceiptOfPayment();

        if (pFilters == null)
            $('#filterAll').prop('checked', true);

    }, true);
}

function InitGridReceiptOfPayment() {
    var $grid = $("#gvReceipOfPayment");
    var $foot = $grid.find("tfoot");
    var accountId = $("#hdnAccountId").val();
    $foot.find("a").each(function () {
        var $this = $(this);
        var Originalhref = $this.attr("href");
        var Targethref = Originalhref + "&AccountId=" + accountId;
        $this.attr("href", Targethref);
    });  

    InitializeGrid($grid);
}

function InitQuotaInformation() {
    var $Grid = $("#gvReceipOfPayment");
    var $rows = $Grid.find("tbody").find("tr");

    $rows.on({
        click: function () {
            BeginRequestHandler();
            var $row = $(this);
            var $hidden = $row.find("input:hidden");
            var row = JSON.parse($hidden.val());

            ////Header
            //$('#NoCuenta').val(row.accountId);
            //$('#loanNumber').val(row.loanNumber);
            //$('#LoanStatusName').val(row.LoanStatusName);
            //$('#AccountName').val(row.fullName);

            //Columna Monto
            $('#CapitalMonto').html(number_format(row.capitalAmount, 2));
            $('#MontoInteres').html(number_format(row.interestAmoint, 2));
            $('#MontoComision').html(number_format(row.commissionAmount, 2));
            $('#MontoGastos').html(number_format(row.expenseAmount, 2));
            $('#MontoMora').html(number_format(row.rateLateAmount, 2));
            $('#MontoCargo').html(number_format(row.chargesPrepayment, 2));

            //Columna Saldo
            $('#CapitalSaldo').html(number_format(row.capitalBalance, 2));
            $('#SaldoInteres').html(number_format(row.interestBalance, 2));
            $('#SaldoComision').html(number_format(row.commissionBalance, 2));
            $('#GastosSaldo').html(number_format(row.expenseBalance, 2));
            $('#MoraSaldo').html(number_format(row.rateLateBalance, 2));
            $('#CargoSaldo').html(number_format(row.chargesPrepaymentBalance, 2));

            //Columna Señal
            $('#SenalCapital').html(row.transactionReasonName);
            $('#SenalInteres').html(row.transactionReasonName);
            $('#SenalComision').html(row.transactionReasonName);
            $('#SenalGastos').html(row.transactionReasonName);
            $('#SenalMora').html(row.transactionReasonName);
            $('#SenalCargo').html(row.transactionReasonName);


            //Columna Fecha Ult. Pago
            var dFormat = "dd/mm/yyyy";
            if (row.dCapital != null) {
                var dCapital = new Date(row.dCapital);

                $('#FechaUltPagoCapital').html(dCapital.format(dFormat));
            } else
                $('#FechaUltPagoCapital').html('');

            if (row.dInterestAmoint != null) {
                var dInterestAmoint = new Date(row.dInterestAmoint);
                $('#UltFechaPagoInteres').html(dInterestAmoint.format(dFormat));
            } else
                $('#UltFechaPagoInteres').html('');

            if (row.dCommissionAmoun != null) {
                var dCommissionAmoun = new Date(row.dCommissionAmoun);
                $('#FechaUltPagoComision').html(dCommissionAmoun.format(dFormat));
            } else
                $('#FechaUltPagoComision').html('');

            if (row.dExpenseAmount != null) {
                var dExpenseAmount = new Date(row.dExpenseAmount);
                $('#dExpenseAmount').html(dExpenseAmount.format(dFormat));
            } else
                $('#dExpenseAmount').html('');

            if (row.dRateLateAmount != null) {
                var dRateLateAmount = new Date(row.dRateLateAmount);
                $('#dRateLateAmount').html(dRateLateAmount.format(dFormat));
            } else
                $('#dRateLateAmount').html('');

            if (row.dChargeAmount != null) {
                var dChargeAmount = new Date(row.dChargeAmount);
                $('#dChargeAmount').html(dChargeAmount.format(dFormat));
            } else
                $('#dChargeAmount').html('');

            EndRequestHandler();

        },
        mouseover: function () {
            var $row = $(this);
            $row.css("cursor", "pointer");
        }
    });
}

function GetDataFilterReceipOfPayment(pFilters, AccountId) {
    GetPartialView("/Process/_ReceipOfPayment", JSON.stringify({ AccountId: AccountId, filters: pFilters }), function (data) {
        var $Container = $("#dvReceipOfPayment")
        $Container.html(data);
        InitQuotaInformation();
        InitGridReceiptOfPayment();
    }, true);
}