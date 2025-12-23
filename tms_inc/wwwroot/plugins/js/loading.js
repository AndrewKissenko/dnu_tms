window.UploadLoader = {
    show: function (title) {
        $("#loadingTitle").text(title || "Uploading…");
        $("#uploadBar").css("width", "0%");
        $("#uploadPct").text("0%");
        $("#loadingOverlay").show();
    },
    setProgress: function (percent) {
        const p = Math.max(0, Math.min(100, Math.round(percent || 0)));
        $("#uploadBar").css("width", p + "%");
        $("#uploadPct").text(percent + "%");
    },
    hide: function () {
        $("#loadingOverlay").hide();
    }
};
