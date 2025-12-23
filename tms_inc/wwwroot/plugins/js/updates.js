
$(document).ready(function () {

    $(".nav-pills a").click(function () {
        $(".nav-pills a").removeClass("active");
        $(this).addClass("active");
    })

    $("#pendingApplicants").click(function () {
        $.get("/Updates/GetActiveApplicationsNoLayout", function (res) {
            $("#result").html(res);
        })

    })

    $("#processedApplicants").click(function () {
        $.get("/Updates/GetProcessedApplications", function (res) {
            $("#result").html(res);
        })

    })

    $("#deletedApplicants").click(function () {
        $.get("/Updates/GetDeletedApplications", function (res) {
            $("#result").html(res);
        })

    })

    $("#pendingQuoteRequests").click(function () {
        $.get("/Updates/GetActiveQuoteRequestsNoLayout", function (res) {
            $("#result").html(res);
        })
    })

    $("#processedQuoteRequests").click(function () {
        $.get("/Updates/GetProcessedQuoteRequests", function (res) {
            $("#result").html(res);
        })
    })

    $("#deletedQuoteRequests").click(function () {
        $.get("/Updates/GetDeletedQuoteRequests", function (res) {
            $("#result").html(res);
        })
    })

    $("#pendingContactRequests").click(function () {
        $.get("/Updates/GetActiveContactRequestsNoLayout", function (res) {
            $("#result").html(res);
        })
    })

    $("#processedContactRequests").click(function () {
        $.get("/Updates/GetProcessedContactRequests", function (res) {
            $("#result").html(res);
        })
    })

    $("#deletedContactRequests").click(function () {
        $.get("/Updates/GetDeletedContactRequests", function (res) {
            $("#result").html(res);
        })
    })

 

 

});