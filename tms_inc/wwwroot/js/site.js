// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    //$(".nav-buttons").css("width" ,(screen.width - 200) +"px");
    $(".tracking").attr("width" ,(screen.width) +"px");
   // $(".tracking").attr("width", (1820) + "px");
    $("#subject").html($('#subjectInput').val());
    $("#body").html($('#bodyInput').val());
     

    $("#footer").html($('#footerInput').val());
    $('#subjectInput').change(function () {
        let input = $(this).val();
        $("#subject").html(input);
    });

    $('#bodyInput').change(function () {
        let input = $(this).val();
        $("#body").html(input);
    });

    $('#footerInput').change(function () {
        let input = $(this).val();
        $("#footer").html(input);
    });


    /////
    $('.nav-item').mouseenter(function () {
        $(this).find(".text-dark").css("font-weight", "800");
    });

    $('.nav-item').mouseout(function () {
        $(this).find(".text-dark").css("font-weight", "500");
    });
    var host = window.location.protocol + "//" + window.location.host + "/";
    var selectedNav = " background-color: black;color:white !important;font-weight: 800 !important";
    if (window.location.pathname.includes("Drivers")) {
        $('#drivers').attr("style", selectedNav);
    }
    if (window.location.href === (host + "Trucks")) {

        $('#trucks').attr("style", selectedNav);
        $("#depot").attr("style", selectedNav);
    }
    if (window.location.href === (host +"Trailers")) {
        $('#trailers').attr("style", selectedNav);
        $("#depot").attr("style", selectedNav);
    }

    if (window.location.href==(host + "Trailers/GetOpenTrailers")) {
        $('#open-trailers').attr("style", selectedNav);
      
    }
    if (window.location.href == (host + "Trucks/GetOpenTrucks")) {
        $('#open-trucks').attr("style", selectedNav);

    }
    if (window.location.href === (host +"Cities")) {
        $('#cities').attr("style", selectedNav);
    }
    if (window.location.href === (host +"Clients")) {
        $('#clients').attr("style", selectedNav);
    }

    if (window.location.href === (host + "SystemHome/DispatchBoard")) {
        $('#dispatch-board').attr("style", selectedNav);
        $.get("GetCities", function (res) {
            sessionStorage.setItem('cities', res);
        });
    }

    if (window.location.href === (host + "SystemHome")) {
        $('#home').attr("style", selectedNav);
    }

    if (window.location.href === (host + "Users")) {
        $('#users').attr("style", selectedNav);
    }

    if (window.location.href.includes(host + "Updates")) {
        $('#updates').attr("style", selectedNav);
    }

    $(".city").autocomplete({
        source: sessionStorage.getItem('cities')?.split(',')
    });


    $(".city").change(function () {
        let self = $(this);
        let dataToSend = {
            city: $(this).val(),
            driverId: $(this).closest("#driver").find(".driver-id").html(),
            date: $(this).attr("asp-date")
        }
        $.post("systemHome/index", { saveDay: dataToSend }, function (res) { 
            showError(res, self);
        });
    });
    var showError = function (status, el) {
        if (status == "404") {
            el.val("");
            alert("Wrong city input. Make sure the city added to 'Cities'");
        }

        if (status == "500") {
            alert("Oops, something went wrong. Contact your developer.");
        }

        if (status == "424") {
            el.val("");
            alert("Wrong city input. Make sure the following city matches the format: 'CITY NAME, STATE NAME' ");
        }

    }
    $(".comment-area").change(function () {
      
        let commentToSend = {
            comment: $(this).val(),
            driverId: $(this).closest(".value").find(".driver-id").html()
        }
        $.post("SaveComment", { saveComment: commentToSend }, function (res) {
            showError(res); 
        });

    });

 

    $.get("/Updates/GetUnprocessedEntetiesNumber", function (res) {
        if (!res) return;
        if (res.total > 0) {
            $("#updates").find("span").html(res.total);
            if (res.applicantsNum > 0 )
                $("#hireMe").find("span").html(res.applicantsNum);
            if (res.quoteRequestsNum > 0)
                $("#quoteMe").find("span").html(res.quoteRequestsNum);
            if (res.contactMeRequestsNum > 0)
                $("#contactMe").find("span").html(res.contactMeRequestsNum);
        }
    });

    $(".dropdown-menu a").click(function () {
        $(".dropdown-menu a").removeClass("active");
        $(this).addClass("active");
    })
   

    window.AppModal = (function () {
        const $modal = $("#appModal");
        const $title = $("#appModalTitle");
        const $body = $("#appModalBody");

        $("#appModalOk").on("click", hide);
        $modal.on("click", function (e) { if (e.target === this) hide(); });

        function escapeHtml(s) {
            return String(s).replace(/[&<>"']/g, c => (
                { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]
            ));
        }

        function show({ title = "Message", html = "", text = "", errors = null } = {}) {
            $title.text(title);

            if (errors && errors.length) {
                const items = errors.map(e => `<li>${escapeHtml(e)}</li>`).join("");
                $body.html(`<ul>${items}</ul>`);
            } else if (html) {
                $body.html(html);              // only use with trusted HTML
            } else {
                $body.html(`<p>${escapeHtml(text)}</p>`);
            }

            $modal.fadeIn();
        }

        function hide() { $modal.fadeOut(); }

        function fromXhr(xhr, title = "Error") {
            let errors = null;

            if (xhr.responseJSON) {
                errors = Array.isArray(xhr.responseJSON)
                    ? xhr.responseJSON
                    : Object.values(xhr.responseJSON).flat(); // handles {field:[...]} too
            }

            show({
                title,
                errors,
                text: errors ? "" : (xhr.responseText || `Request failed (${xhr.status})`)
            });
        }

        return { show, hide, fromXhr };
    })();

});
