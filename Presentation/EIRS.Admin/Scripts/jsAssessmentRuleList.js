﻿var vAssessmentRuleDataTable;

jQuery(document).ready(function () {
    jsfn_BuildDataTable();
});

function jsfn_ChangeStatus(AssessmentRuleid) {
    var vData = {
        AssessmentRuleID: AssessmentRuleid
    }
    jsfn_ShowLoading();
    jsfn_ajaxPost('/AssessmentRule/UpdateStatus', vData, jsfn_ChangeStatusResponse);
}

function jsfn_ChangeStatusResponse(data) {
    jsfn_HideLoading();
    if (data.success) {
        jsfn_ShowAlert(data.Message, 'success');
        vAssessmentRuleDataTable.draw();
    }
    else {
        jsfn_ShowAlert(data.Message, 'danger');
    }
}

function jsfn_BuildDataTable() {
    debugger
    vAssessmentRuleDataTable = $("#tbAssessmentRule").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true,
        "orderMulti": false, // for disable multiple column at once
        "ajax": {
            "url": "/AssessmentRule/LoadData",
            "type": "POST",
            "datatype": "json",
            "data": function (data) {
                data.AssessmentRuleCode = $("#txtAssessmentRuleCode").val();
                data.ProfileReferenceNo = $("#txtProfile").val();
                data.AssessmentRuleName = $("#txtAssessmentRuleName").val();
                data.RuleRunName = $("#txtRuleRunName").val();
                data.PaymentFrequencyName = $("#txtPaymentFrequencyName").val();
                data.AssessmentItemName = $("#txtAssessmentItems").val();
                data.AssessmentAmount = $("#txtAssessmentAmount").val();
                data.TaxYear = $("#txtTaxYear").val();
                data.SettlementMethodName = $("#txtSettlementMethod").val();
                data.PaymentOptionName = $("#txtPaymentOption").val();
                data.ActiveText = $("#txtStatus").val();
            },
        },
        "dom": "<'dt-toolbar'<'col-xs-12 col-sm-6 hidden-xs'f><'col-sm-6 col-xs-12 hidden-xs'C>r>" +
            "t" +
            "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-xs-12 col-sm-6'p>>",

        "columns": [
            { "data": "AssessmentRuleCode", "orderable": true, "name": "AssessmentRuleCode" },
            { "data": "ProfileReferenceNo", "orderable": true, "name": "ProfileReferenceNo" },
            { "data": "AssessmentRuleName", "orderable": true, "name": "AssessmentRuleName" },
            { "data": "RuleRunName", "orderable": true, "name": "RuleRunName" },
            { "data": "PaymentFrequencyName", "orderable": true, "name": "PaymentFrequencyName" },
            { "data": "AssessmentItemNames", "orderable": true, "name": "AssessmentItemNames" },
            {
                "data": "AssessmentAmount", "orderable": true, "name": "AssessmentAmount", "render": function (data, type, asrule) {
                    return asrule.AssessmentAmount.formatMoney();
                }
            },
            {
                "data": "TaxYear", "orderable": true, "name": "TaxYear"
            },
            { "data": "SettlementMethodNames", "orderable": true, "name": "SettlementMethodNames" },
            { "data": "PaymentOptionName", "orderable": true, "name": "PaymentOptionName" },
            { "data": "ActiveText", "orderable": true, "name": "ActiveText" },
            {
                "data": "", "orderable": false, "name": "Action", "render": function (data, type, asrule) {
                    return '<div class="btn-group">' + '<button class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="false">' +
                        'Actions  <span class="caret"></span></button><ul class="dropdown-menu" role="menu">'
                        + '<li><a href="/AssessmentRule/Details/' + asrule.AssessmentRuleID + '/' + asrule.AssessmentRuleName.toSeoUrl() + '">View Details</a></li>'
                        + '<li><a href="/AssessmentRule/Edit/' + asrule.AssessmentRuleID + '/' + asrule.AssessmentRuleName.toSeoUrl() + '">Edit Details</a></li>'
                        + '<li><a href="/AssessmentRule/ProfileDetails/' + asrule.AssessmentRuleID + '/' + asrule.AssessmentRuleName.toSeoUrl() + '">View Profile Info</a></li>'
                        + '<li><a href="/AssessmentRule/AssessmentItemDetails/' + asrule.AssessmentRuleID + '/' + asrule.AssessmentRuleName.toSeoUrl() + '">View Assessment Item Info</a></li>'
                        + '<li><a onclick="javascript:jsfn_ChangeStatus(' + asrule.AssessmentRuleID + ')">' + (asrule.Active == 0 ? "Mark Active" : "Mark Inactive") + '</a></li>'
                        + '</ul></div>';
                }
            },
        ],

        "order": [[1, "asc"]]
    });

    $("#tbAssessmentRule thead th input[type=text]").on('change', function () {
        vAssessmentRuleDataTable
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();
    });
    //Hide Columns
    vAssessmentRuleDataTable.column(3).visible(false);
    vAssessmentRuleDataTable.column(4).visible(false);
    vAssessmentRuleDataTable.column(7).visible(false);
    vAssessmentRuleDataTable.column(8).visible(false);
    vAssessmentRuleDataTable.column(9).visible(false);
}

