
let onConfirmationCheckBoxHandler = function (inpuParentId, nextBtnId) {
    $("#" + inpuParentId +" input").click(function () {
        if ($(this).is(':checked')) $("#" + nextBtnId).prop("disabled", false);
        else $("#" + nextBtnId).prop("disabled", true);
    })
}

let nextHandler = function (stepper, inputParentId) {
    if ($("#" + inputParentId + " input").is(':checked')) stepper.next();
}

$(document).ready(function () {

    $("#scroll-down").click(function () {
        $('html, body').animate({ scrollTop: $(document).height() }, 'slow');
        return false;
    })

    $(".print").click(function () {
        window.print();
    });

    var stepper = new Stepper($('#stepper2')[0]);

    $("#step1").click(function () {
        nextHandler(stepper, "step1Checkbox");
    })

    onConfirmationCheckBoxHandler("step1Checkbox", "step1");


    $("#step2").click(function() {
        nextHandler(stepper, "step2Checkbox");
    })

    onConfirmationCheckBoxHandler("step2Checkbox", "step2");

    $("#step3").click(function () {
        nextHandler(stepper, "step3Checkbox");
    })

    onConfirmationCheckBoxHandler("step3Checkbox", "step3");

    $("#step4").click(function () {
        nextHandler(stepper, "step4Checkbox");
        sessionStorage.setItem("allRulesChecked", true);
        document.location.href = "DriverApplication"
    })

    onConfirmationCheckBoxHandler("step4Checkbox", "step4");
  


    
    $(".btn-primary").click(function() {
        if ($(this).html().includes("Previous"))
            stepper.previous();
    })


 
});
