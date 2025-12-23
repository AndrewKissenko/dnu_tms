


var form = {
    firstName: undefined,
    middleName: undefined,
    lastName: undefined,
    suffix: undefined,
    ssn: undefined,
    birthday: undefined,
    address: {
        streetLine1: undefined,
        streetLine2: undefined,
        country: undefined,
        city: undefined,
        state: undefined,
        zip: undefined,
        residence: undefined,
    },
    contact: {
        primaryPhone: undefined,
        cellPhone: undefined,
        email: undefined,
        emailConfirmation: undefined,
        contactMethod: undefined,
        contactTime: undefined,
    },
    generalInformation: {
        isEligible: undefined ,
        isEmployed: undefined,
        lastEmploymentDay: undefined,
        isEnglish: undefined,
        isWorkedBefore: undefined,
        isTWICOwner: undefined,
        twicExpirationDate: undefined,
        isOtherName: undefined,
        otherName: undefined,
        isFMCSACompleted: undefined,
        emergencyContact: undefined,
        medicalCardExpirationDate: undefined,
        howFoundInfo: undefined,
    },

    drivingExperience: {
        straightTruck: undefined,
        tractorSemiTreailer: undefined,
        tractorTwoTrailers: undefined,
        other: undefined
    },
    equipmentDescription: {
        type: undefined,
        year: undefined,
        make: undefined,
        model: undefined,
        color: undefined,
        vin: undefined,
        weight: undefined,
        mileage: undefined
    },
    education: {
        highestGradeCompleted: undefined
    },
    licenseDetails: {
        licenseNumber: undefined,
        licenseState: undefined,
        licenseExpiration: undefined,
        physicalExpiration: undefined,
        isCurrentDrivingLicense: undefined,
        isCommericalDrivingLicense: undefined,
        licenseClass: undefined,
        endorsements: [""], 
        hazmatExpiration: undefined,
        //violations
        hadIssuesWithLicense: undefined,
        hadIssuesWithLicenseDate: undefined,
        hadIssuesWithLicenseDetail: undefined,

        hadLicenseSuspensionDriving: undefined,
        hadLicenseSuspensionDrivingDate: undefined, 
        hadLicenseSuspensionDrivingDetail: undefined,
        hadDrunkDriving: undefined,
        hadDrunkDrivingDate: undefined,
        hadDrunkDrivingDetail: undefined, 
        hadDrugTransfer: undefined,
        hadDrugTransferDate: undefined,
        hadDrugTransferDetail: undefined,
        hadRecklessDriving: undefined,
        hadRecklessDrivingDate: undefined,
        hadRecklessDrivingDetail: undefined,
        hadDrugTestPositive: undefined, 
        hadDrugTestPositiveDate: undefined,
        hadDrugTestPositiveDetail: undefined
    },

    isMilitary: undefined,
    hasDriverTraining: undefined,
    driverTraining: {
        startDate: undefined, 
        endDate: undefined,
        trainingState: undefined,
        trainingSchoolName: undefined, 
        trainingCity: undefined, 
        telephone: undefined,
        isGraduated: undefined,
        hasFederalMotorCarrierSubject: undefined, 
        hasAlcoDrugTestingSubject: undefined, 
        gpa: undefined,
        trainingTime: undefined,
        trainedSkills: new Array()

    },
   // make unique ids
    isSchoolLast3Years: undefined,
    schoolInformation: {
        schoolStartDate: undefined,
        schoolEndDate: undefined,
        schoolState: undefined,
        schoolName: undefined,
        schoolCity: undefined,
        schoolTelephone: undefined,
        subjects: undefined,
        graduationDate: undefined
    },
    isEnemployedLast3Years: undefined,

    unemployment: {
        enemployementStartDate: undefined,
        enemployementEndDate: undefined,
        unemploymentComment: undefined
    },

    FMCSR: {
        isDrivingDisqualified: undefined,
        isDrivingDisqualifiedDetail: undefined,
        hadLicenseSuspense: undefined, 
        hadLicenseSuspenseDetail: undefined,
        hadLicenseDenial: undefined,
        hadLicenseDenialDetail: undefined,
        didRefuseDrugTest: undefined,
        didRefuseDrugTestDetail: undefined,
        lastRefuseDrugTestDate: undefined,
        beenConvictedOnDuty: undefined,
        beenConvictedOnDutyDetail: undefined

    },

    movingViolation: {
        isMovingViolationPast3Y: undefined,
        violationDate: undefined,
        violationDescription: undefined,
        movingViolationState: undefined,
        penalties: [],
        fineAmount: undefined,
        movingViolationStateComment: undefined
    },

    movingAccident: {
        isAccidentInvolved: undefined,
        accidentDate: undefined,
        accidentType: undefined, 
        isHazmat: undefined, 
        isVehicleTowedAway: undefined,
        accidentState: undefined,
        accidentCity: undefined, 
        inCommercialVehicle: undefined,
        wasDepartmentOfTransportationRecordableAccident: undefined,
        wereAtFault: undefined,
        isTicketed: undefined,



    },
    criminalRecord: {
        beenCrimeConvicted: undefined,
        beenCrimeConvictedDetail: undefined,
        beenDeferredProsecuted: undefined,
        beenDeferredProsecutedDetail: undefined,
        hasCriminalChargesPending: undefined,
        hasCriminalChargesPendingDetail: undefined,
        everPledGuilty: undefined,
        everPledGuiltyDetail: undefined,
        beenMisdemeanorGuilty: undefined,
        beenMisdemeanorGuiltyDetail: undefined

    }

}
var id = 0;

let getFormData = function () {
    for (const [key, value] of Object.entries(form)) {
        if (form[key] && Object.entries(form[key]).length > 0) {
            for (const [innerKey, value] of Object.entries(form[key])) {
              form[key][innerKey] =  $("#" + innerKey).val();
            }
        }
        else form[key] = $("#" + key).val();
    }
    getRadioButtonData();
}

let getRadioButtonData = function () {
    form.address.residence = $("input[name='residence']:checked").val();
    form.generalInformation.isEligible = $("input[name='isEligible']:checked").val();
    form.generalInformation.isEmployed = $("input[name='isEmployed']:checked").val();
    form.generalInformation.isEnglish = $("input[name='isEnglish']:checked").val();
    form.generalInformation.isWorkedBefore = $("input[name='isWorkedBefore']:checked").val();
    form.generalInformation.isTWICOwner = $("input[name='isTWICOwner']:checked").val();
    form.generalInformation.isOtherName = $("input[name='isOtherName']:checked").val();
    form.generalInformation.isFMCSACompleted = $("input[name='isFMCSACompleted']:checked").val();
    form.licenseDetails.isCurrentDrivingLicense = $("input[name='isCurrentDrivingLicense']:checked").val();
    form.licenseDetails.isCommericalDrivingLicense = $("input[name='isCommericalDrivingLicense']:checked").val();
    form.licenseDetails.hadIssuesWithLicense = $("input[name='hadIssuesWithLicense']:checked").val();
    form.licenseDetails.hadLicenseSuspensionDriving = $("input[name='hadLicenseSuspensionDriving']:checked").val();
    form.licenseDetails.hadDrunkDriving = $("input[name='hadDrunkDriving']:checked").val();
    form.licenseDetails.hadDrugTransfer = $("input[name='hadDrugTransfer']:checked").val();
    form.licenseDetails.hadRecklessDriving = $("input[name='hadRecklessDriving']:checked").val();
    form.licenseDetails.hadDrugTestPositive = $("input[name='hadDrugTestPositive']:checked").val();

    let arr = [];
    $("#endorsements").find("input:checked").each((idx, el) => {
            arr.push(el.name)
    });
    form.licenseDetails.endorsements = arr;

    form.isMilitary = $("input[name='isMilitary']:checked").val();
    form.hasDriverTraining = $("input[name='hasDriverTraining']:checked").val();
    form.driverTraining.isGraduated = $("input[name='isGraduated']:checked").val();
    form.driverTraining.isGraduated = $("input[name='hasFederalMotorCarrierSubject']:checked").val();
    arr = [];
    $("#trainedSkills").find("input:checked").each((idx, el) => {
        arr.push(el.name);
    });
    form.driverTraining.trainedSkills = arr;
    form.driverTraining.isSchoolLast3Years = $("input[name='hasDriverTraining']:checked").val();
    form.isEnemployedLast3Years = $("input[name='isEnemployedLast3Years']:checked").val();
   // check
    form.FMCSR.isDrivingDisqualified = $("input[name='isDrivingDisqualified']:checked").val();
    form.FMCSR.hadLicenseSuspense = $("input[name='hadLicenseSuspense']:checked").val();
    form.FMCSR.didRefuseDrugTest = $("input[name='didRefuseDrugTest']:checked").val();
    form.FMCSR.beenConvictedOnDuty = $("input[name='beenConvictedOnDuty']:checked").val();
    form.movingViolation.isMovingViolationPast3Y = $("input[name='isMovingViolationPast3Y']:checked").val();
    form.movingViolation.wasInCommercialVehicle = $("input[name='wasInCommercialVehicle']:checked").val();
    arr = [];
    $("#penalties").find("input:checked").each((idx, el) => {
        arr.push(el.name);
        
    });
    form.movingViolation.penalties = arr;
    form.movingAccident.isAccidentInvolved = $("input[name='isAccidentInvolved']:checked").val();
    form.movingAccident.isHazmat = $("input[name='isHazmat']:checked").val();
    form.movingAccident.isVehicleTowedAway = $("input[name='isVehicleTowedAway']:checked").val();
    form.movingAccident.inCommercialVehicle = $("input[name='inCommercialVehicle']:checked").val();
    form.movingAccident.wasDepartmentOfTransportationRecordableAccident = $("input[name='wasDepartmentOfTransportationRecordableAccident']:checked").val();
    form.movingAccident.wereAtFault = $("input[name='wereAtFault']:checked").val();
    form.movingAccident.isTicketed = $("input[name='isTicketed']:checked").val();

    form.criminalRecord.beenCrimeConvicted = $("input[name='beenCrimeConvicted']:checked").val();
    form.criminalRecord.beenDeferredProsecuted = $("input[name='beenDeferredProsecuted']:checked").val();
    form.criminalRecord.hasCriminalChargesPending = $("input[name='hasCriminalChargesPending']:checked").val();
    form.criminalRecord.everPledGuilty = $("input[name='everPledGuilty']:checked").val();
    form.criminalRecord.beenMisdemeanorGuilty = $("input[name='beenMisdemeanorGuilty']:checked").val();
   
}

const employerInfo = {
    id: "",
    key: "",
    value: ""
}
let employerInfoArray = [];

let nextHandler = function (stepper) {
    stepper.next();
}

let previousHandler = function (stepper) {
    stepper.previous();
}

let generateFormBase = function (inputName, elementToShowId, ...makeRequiredInputId) {
    $("input[name='" + inputName + "']").click(function () {
        if ($("input[name='" + inputName + "']:checked").val() === "yes") {
            if (makeRequiredInputId) {
                makeRequiredInputId.forEach(id => {
                    $("#" + id).prop('required', true);
                })
              
            }
            $("#" + elementToShowId).switchClass("hide", "show");
        }
        else {
            if (makeRequiredInputId) {
                makeRequiredInputId.forEach(id => {
                    $("#" + id).prop('required', false);
                })

            }
            $("#" + elementToShowId).switchClass("show", "hide");
        }
    });
}



let generateHazmatExpirationDate = function () {
    $("#endorsements").click(function () {
        if ($(this).find("input[name='XEndorsement']").is(":checked") || $(this).find("input[name='HazMat']").is(":checked")) {
            $("#hazmatExpiration").prop("required", true);
            $("#hazmat").switchClass("hide", "show");
        }
        else {
            $("#hazmatExpiration").prop("required", false);
            $("#hazmat").switchClass("show", "hide");
        }
    })
}

let generateLicenseClass = function () {
    generateFormBase("isCommericalDrivingLicense", "licenceClassList", "licenseClass");
}

let generateTWICOwnerDatePicker = function () {
    generateFormBase("isTWICOwner", "twicYes", "twicExpirationDate");
}

let generateLastEmploymentDateField = function () {
    $("input[name='isEmployed']").click(function () {
        if ($("input[name='isEmployed']:checked").val() === "no") {
            $("#lastEmploymentDayField").switchClass("hide", "show");
            $("#lastEmploymentDay").prop("required", true);
        }
        else {
            $("#lastEmploymentDayField").switchClass("show", "hide");
            $("#lastEmploymentDay").prop("required", false);
        }
    });
}

let generateOtherNameField = function () {
    generateFormBase("isOtherName", "otherNameField", "otherName");
}

let generateDriverTrainingForm = function () {
    generateFormBase("hasDriverTraining", "hadDriverTraining", "startDate", "endDate",
        "trainingState", "trainingSchoolName", "trainingCity");
}

let generateSchoolInfoForm = function () {
    generateFormBase("isSchoolLast3Years", "hadSchoolLast3Years", "schoolStartDate", "schoolName",
        "schoolCity", "schoolState");
}

let generateUnemploymentForm = function () {
    generateFormBase("isEnemployedLast3Years", "getUnemploymentInformation", "enemployementStartDate", "enemployementEndDate");
}
//FMCSR

let generateDetailsDrivingDisqualification = function () {
    generateFormBase("isDrivingDisqualified", "isDrivingDisqualifiedDetailShow", "isDrivingDisqualifiedDetail");
}

let generateDetailsLicenceSuspense = function () {
    generateFormBase("hadLicenseSuspense", "hadLicenseSuspenseDetailShow", "hadLicenseSuspenseDetail");
}

let generateLincenseDenialShow = function () {
    generateFormBase("hadLicenseDenial", "hadLicenseDenialDetailShow", "hadLicenseDenialDetail");
}

let generateDrugTestDetailShow = function () {
    generateFormBase("didRefuseDrugTest", "didRefuseDrugTestDetailShow", "didRefuseDrugTestDetail");
}

let generateConvictedOnDutyDetailShow = function () {
    generateFormBase("beenConvictedOnDuty", "beenConvictedOnDutyDetailShow");
}

let generateIncidentDetailsShow = function () {
    generateFormBase("isMovingViolationPast3Y", "getIncidentDetails", "violationDate", "violationDescription", "state");
}

let generateAccidentDetailsShow = function () {
    generateFormBase("isAccidentInvolved", "getAccidentDetails", "accidentDate", "accidentType", "state",
        "inCommercialVehicle", "wereAtFault", "isTicketed");
}

let generateDepartmentOfTransportationRecordableAccident = function () {
    generateFormBase("inCommercialVehicle", "departmentOfTransportationRecordableAccident" ,"wasDepartmentOfTransportationRecordableAccident");
}

let hadIssuesWithLicenseDetail = function () {
    generateFormBase("hadIssuesWithLicense", "hadIssuesWithLicenseDetails");
}

let hadLicenseSuspensionDrivingDetails = function () {
    generateFormBase("hadLicenseSuspensionDriving", "hadLicenseSuspensionDrivingDetails");
}

let hadDrunkDrivingDetails = function () {
    generateFormBase("hadDrunkDriving", "hadDrunkDrivingDetails");
}

let hadDrugTransferDetails = function () {
    generateFormBase("hadDrugTransfer", "hadDrugTransferDetails");
}

let hadRecklessDrivingDetails = function () {
    generateFormBase("hadRecklessDriving", "hadRecklessDrivingDetails");
}


let hadDrugTestPositiveDetails = function () {
    generateFormBase("hadDrugTestPositive", "hadDrugTestPositiveDetails");
}

let beenCrimeConvictedDetails = function () {
    generateFormBase("beenCrimeConvicted", "beenCrimeConvictedDetails");
}

let hasCriminalChargesPendingDetails = function () {
    generateFormBase("hasCriminalChargesPending", "hasCriminalChargesPendingDetails");
}

let beenDeferredProsecutedDetails = function () {
    generateFormBase("beenDeferredProsecuted", "beenDeferredProsecutedDetails");
}

let everPledGuiltyDetails = function () {
    generateFormBase("everPledGuilty", "everPledGuiltyDetails");
}

let beenMisdemeanorGuiltyDetails = function () {
    generateFormBase("beenMisdemeanorGuilty", "beenMisdemeanorGuiltyDetails");
}

let displayEmploymentTable = function () {
    generateFormBase("isEnemployedLast3Years", "employmentArray");
}


const addSavedEmpoymentFile = function (id, name, location, startDate, endDate, form) {
    
    const tr = document.createElement("tr");
    tr.setAttribute("tr-id", arguments[0]);
    for (var i = 1; i <= 5; i++) {
        const td = document.createElement("td");
        if (i !== 5) {
            td.textContent = arguments[i];
            hoverElement(td);

            td.addEventListener("click", function (e) {
                const otherForm = document.getElementById("employmentArray").querySelector("form");
                if (otherForm) {
                    if (confirm('Information you`re changing won`t be saved without actually clicking SAVE button. Continue without Saving?')) {
                        document.getElementById("employmentArray").removeChild(otherForm);
                    }
                }
                if (!document.querySelector("#employmentArray form[data-id='" + id + "']")) {
                    
                    addEditEmpoymentFileEvent("update", id);
                }
                
            });
        }
        if (i > 2) {
            td.classList.add("c");
        }
        if (i == 5) {
            const trashCan = document.createElement("i");
            trashCan.setAttribute("title", "remove this item");
            trashCan.classList = ("far fa-trash-alt");
            td.addEventListener("click", function (e) {
                if (confirm('Are you sure you want to delete the record?')) {
                    deleteEmpoymentFile(tr, id)
                } 
            });
       
            td.appendChild(trashCan);
        }
       
        tr.appendChild(td);
    }
    document.querySelector(".tablegrid tbody").appendChild(tr);
}

const deleteEmpoymentFile = function (el, id) {
    document.querySelector(".tablegrid tbody").removeChild(el);
    employerInfoArray = employerInfoArray.filter(x => x.key !== id);
}

const hoverElement = function (el) {
    el.addEventListener("mouseenter", function (e) {
        el.classList.add('background-red');
    });
    el.addEventListener("mouseleave", function (e) {
        el.classList.remove('background-red');
    });
}


const generateEmpoymentHistory = function () {
    const form = document.forms.activity;
    if (form['isEnemployedLast3Years'].value === "yes") {
        const table = document.querySelector("#tableBody");
        const employmentYes = table.querySelector("#empoymentQuestion");
        const headerTr = $.parseHTML("<tr> </tr>")[0];
        let tdHeader = $.parseHTML('<td colspan="2" class="section_header">Employment Record</td>')[0];
        headerTr.appendChild(tdHeader);
        employmentYes.after(headerTr);
        try {
            for (let mainArr of employerInfoArray) {
                let recordHeaderTr = $.parseHTML("<tr> </tr>")[0];
                let headerTd = $.parseHTML(`<td style='font-weight: bold; text-align: end;'>Empoyment record ${mainArr.key} <i class="fas fa-file-alt"></i></td>`)[0];
                recordHeaderTr.appendChild(headerTd);
                headerTr.after(recordHeaderTr);
                const record = mainArr.value.employmentFileArr;
                for (let i = record.length -1; i >= 0; i--) {
                    if (record[i].value.length > 1) {
                        let tableInput = $.parseHTML(`<tr><td>${record[i].question}</td><td>${record[i].value}</td> </tr>`)[0];
                        recordHeaderTr.after(tableInput);
                    }
                }
            }
        } catch (e) {
            console.log("EXCEPTION THROWN ", e);
        }
        


    }
        
}


var prevStepId;
//HERE MAGIC COMES
let generateTable = function () {
    getFormData();

    clearTable();
    for (const [key, value] of Object.entries(form)) {
        if (form[key] && typeof value === "object") {
            for (const [innerKey, value] of Object.entries(form[key])) {
                if (value && value.length > 1) {
                    initTd(innerKey, value);
                }
            }
        }
        else {
            if (value) {
                initTd(key, value);
            }
        
        }
    }
    generateEmpoymentHistory();

}

let clearTable = function () {
    $("#tableBody").html("");
}

let initTd = function (key, value) {
    let tableBody = $("#tableBody")
    let tr = $.parseHTML("<tr> </tr>")[0];
    let tr2 =$.parseHTML("<tr></tr>")[0];
    let tdValue = $.parseHTML('<td></td>')[0];
    let tdSeparator = $.parseHTML('<td class="separator"></td>')[0];
    let table = $.parseHTML("<table cellpadding='2' cellspacing='0' align='center' width='100%' class='section'> </table>")[0];
    let tbody = $.parseHTML('<tbody></tbody>')[0];
    let tdHeader = $.parseHTML('<td colspan="2" class="section_header"></td>')[0];
    let tdQuestion = $.parseHTML('<td></td>')[0];
    let row = tr.appendChild(tdSeparator).appendChild(table).appendChild(tbody).appendChild(tr2);

    let element = $("#" + key); 
    let currentStepId = element.closest("form").attr("id");
    if (currentStepId) {
        if (prevStepId !== currentStepId) {
            tdHeader.innerHTML = element.closest("fieldset").find("legend").html();
            row.appendChild(tdHeader);
            tableBody.append(row);
            prevStepId = currentStepId;
            initTd(key, value);
            return;
        }
    }
    else { // if check from radio btn with no id 
        let currentStepId = $("input[name='" + key + "'").closest("form").attr("id");
        if (prevStepId !== currentStepId) {
            tdHeader.innerHTML = $("input[name='" + key + "'").closest("fieldset").find("legend").html();
            row.appendChild(tdHeader);
            tableBody.append(row);
            prevStepId = currentStepId;
            initTd(key, value);
            return;
        }
    }

   //radio yes/no case
    if (!element[0])
    {
        tdQuestion.innerHTML = $("input[name='" + key + "'").closest("div").find("div").html();
        tdValue.innerHTML = value.charAt(0).toUpperCase() + value.slice(1);
        if (key == "isEnemployedLast3Years") {
            row.setAttribute("id", "empoymentQuestion");
        }
        row.appendChild(tdQuestion);
        row.appendChild(tdValue);
        tableBody.append(row);
        return;
    }
    let tagName = element[0].tagName.toUpperCase();

    if (tagName === "SELECT") {
        tdQuestion.innerHTML = element.siblings("label").html();
    }
    else if (tagName === "INPUT") {
        tdQuestion.innerHTML = element.closest("div").find("label").html();
    }
    else if (tagName === "DIV" && element.hasClass("form-checkbox")) {
        tdQuestion.innerHTML = element.closest(".form-checkbox").find(".form-checkbox-legend").html();
        value.forEach(item => {
            tdValue.innerHTML += "<i class='fas fa-check-square' style='color: green'></i>" + " " + item + " ";
        })

    }
    else if (tagName === "TEXTAREA") {
        tdQuestion.innerHTML = element.closest(".form-textarea").find("label").html();
    }

    row.appendChild(tdQuestion);
    if (!element.hasClass("form-checkbox")) tdValue.innerHTML = value.charAt(0).toUpperCase() + value.slice(1);
    row.appendChild(tdValue);

    tableBody.append(row);
    
}

var entryId;

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function setLoading(isLoading) {
    if (isLoading) {
        $(".container button").attr("disabled", true);
        $(".container input").attr("disabled", true);
        $("#loader").removeClass("hide");
        $("#loader").addClass("show");
    } else {
        $(".container button").attr("disabled", false);
        $(".container input").attr("disabled", false);
        $("#loader").addClass("hide");
        $("#loader").removeClass("show");
    }

}

let uploadLicensePicture = function (inputId, showMessagecontainerId, url) {
    setLoading(true);
    var pic = new FormData();
    var id = entryId ? entryId : "";
        $.each($("#" + inputId)[0].files, function (i, file) {
            if (file.type.split('/')[0] !== "image" && file.type !== "application/pdf") {
                alert("File is not image");
            }
            else {

                let arrayLength = file.name.split('.').length;

                let fileName = uuidv4() + "." + file.name.split('.')[arrayLength - 1];
                pic.set(id, file, fileName);

            }

        });
   

    if (pic) {
            $.ajax({
                url: url,
                data: pic,
                cache: false,
                contentType: false,
                processData: false,
                processData: false,
         
                method: 'POST',
                type: 'POST', // For jQuery < 1.9
                success: function (data) {
                   
                    if (data.isError === true) {
                        alert(data.message);
                    }
                    else {
                        entryId = data.entityId;
                        $("#" + showMessagecontainerId).toggleClass("show");
                        $("#accept").removeAttr("disabled");
                        $("#createdAt").html('Applying on '+ data.createdAt);
                    }

                    setLoading(false);
                }
            })
        }

}

let printForm = function () {
    $("#print").click(function () {
        window.print();
    });
}

var frontReceived = false;
var backReceived = false;

$(document).ready(function () {
    $(document).tooltip();
    let driverForm2IsDone = sessionStorage.getItem("allRulesChecked");
    if (!driverForm2IsDone) {
        alert("You are not allowed. Read Your Rights");
        document.location.href = "/Career/DriverApplication2";
    }

    var id = 0;
  
    var stepper = new Stepper($('#stepper1')[0]);
      //START
  //  stepper.to(10);
    $("#step1").click(function() {
        nextHandler(stepper);
    })

    $("#test-l-3").submit(function () {
        nextHandler(stepper);
        return false;
    })

    registerCustomDateTimeInputs();

     $("#test-l-4").submit(function() {
         nextHandler(stepper);
        return false;
     })

    generateLastEmploymentDateField();

    generateTWICOwnerDatePicker();
    generateOtherNameField();

    $("#test-l-5").submit(function () {
        nextHandler(stepper);
       // stepper.to(11);
        return false;
    })

    generateHazmatExpirationDate();
    generateLicenseClass();


    $("#test-l-6").submit(function () {
        nextHandler(stepper);
        return false;
    })

    hadIssuesWithLicenseDetail();
    hadLicenseSuspensionDrivingDetails();
    hadDrunkDrivingDetails();
    hadDrugTransferDetails();
    hadRecklessDrivingDetails();
    hadDrugTestPositiveDetails();


    generateDriverTrainingForm();
    generateSchoolInfoForm();

    displayEmploymentTable();

    addNewBlankFile();

    $("#isEnemployedLast3YearsYES").click(function (e) {
        if (!document.querySelector("#employmentArray form")) {
            addEditEmpoymentFileEvent("create", null);
        }
       
    });
  


    $("#test-l-7").submit(function () {
        nextHandler(stepper);
        return false;
    })
    generateDetailsDrivingDisqualification();
    generateDetailsLicenceSuspense();
    generateLincenseDenialShow();
    generateDrugTestDetailShow();
    generateConvictedOnDutyDetailShow();

   // incidentDetails

    $("#test-l-8").submit(function () {
       nextHandler(stepper);
        return false;
    })

    generateIncidentDetailsShow();

  //  step 9 

    generateAccidentDetailsShow();
    generateDepartmentOfTransportationRecordableAccident();


    $("#test-l-9").submit(function () {
        nextHandler(stepper);
        return false;
    })

    //step 11 goes before 10

    beenCrimeConvictedDetails();

    hasCriminalChargesPendingDetails();

    beenDeferredProsecutedDetails();

    everPledGuiltyDetails();

    beenMisdemeanorGuiltyDetails();


    $("#test-l-11").submit(function () {
        generateTable();
     //   nextHandler(stepper);
        stepper.to(10)
        return false;
    })



   // step 10 
    printForm();

    $("#licencePictureFrontSubmit").click(function () {
        uploadLicensePicture("licencePictureFront", "frontReceived", "AddLicenseFront");
    })

    $("#licencePictureBackSubmit").click(function () {
        uploadLicensePicture("licencePictureBack", "backReceived", "AddLicenseBack");
    })

    $("#signatureSubmit").click(function () {
        uploadLicensePicture("signature", "signatureReceived", "AddSignature");
    })


    $("#accept").click(function () {
        setLoading(true);
        var code = $("#generated").html().trim();
        var instance = $('.sigPad').signaturePad();
        let sig = instance.getSignatureString();
        let phoneVal="";
        if (form.contact.cellPhone)
            phoneVal = form.contact.cellPhone;
        else (form.contact.primaryPhone)
            phoneVal = form.contact.primaryPhone;
        let candidate = {
            tableHtml: code,
            signature: sig,
            id: entryId,
            applicantFirstName: form.firstName,
            applicantSecondName: form.lastName,
            phone: phoneVal
        }

   
       let request = $.ajax({
            url: 'UploadStep1Data',
            data: { candidate: candidate },
            method: 'post',
           success: function () {
               document.location.href = "/Career/Success";
           },
           error: function (msg) {
               setLoading(false);
               alert(msg);
           }
       })

    });


    $("#logo").click(function () {
        ConfirmDialog("Your progress will be lost. Are you sure")
    })

    
    $(".btn-primary").click(function() {
         if ($(this).html().includes("Previous"))
            previousHandler(stepper);
    })


 
});

function ConfirmDialog(message) {
    $('<div></div>').appendTo('body')
        .html('<div><h6>' + message + '?</h6></div>')
        .dialog({
            modal: true,
            title: 'Confirmation message',
            zIndex: 10000,
            autoOpen: true,
            width: 'auto',
            resizable: false,
            buttons: {
                Yes: function () {
                    document.location.href = "/";
                },
                No: function () {

                    $(this).dialog("close");
                }
            },
            close: function (event, ui) {
                $(this).remove();
            }
        });
};

const registerCustomDateTime = function (elementId, error, lowerBound) {
    const el = document.getElementById(elementId);
    const myError = error
        ? error : "Invalid date";
    if (el) {
        el.addEventListener("keydown", onDateKeyDownHandler);
        el.addEventListener("focusout", (e) => { onDateChangeHandler(e, el, myError, lowerBound) });
    }
}

const registerCustomDateTimeInputs = function () {
    registerCustomDateTime("birthday", "Invalid date", 65);
    registerCustomDateTime("lastEmploymentDay", "Invalid date", 65);
    registerCustomDateTime("medicalCardExpirationDate", "Invalid date", 65);
    registerCustomDateTime("twicExpirationDate", "Invalid date", 65);
    registerCustomDateTime("licenseExpiration", "Invalid date", 65);
    registerCustomDateTime("physicalExpiration", "Invalid date", 65);
    registerCustomDateTime("hazmatExpiration", "Invalid date", 65);
    registerCustomDateTime("lastRefuseDrugTestDate", "Invalid date", 65);
    registerCustomDateTime("accidentDate", "Invalid date", 65);
    registerCustomDateTime("startDate", "Invalid date", 65);
    registerCustomDateTime("endDate", "Invalid date", 65);
    registerCustomDateTime("violationDate", "Invalid date", 65);
    registerCustomDateTime("hadLicenseSuspensionDrivingDate", "Invalid date", 65);
    registerCustomDateTime("hadDrunkDrivingDate", "Invalid date", 65);
    registerCustomDateTime("hadDrugTransferDate", "Invalid date", 65);
    registerCustomDateTime("hadRecklessDrivingDate", "Invalid date", 65);
    registerCustomDateTime("hadDrugTestPositiveDate", "Invalid date", 65);
    registerCustomDateTime("hadIssuesWithLicenseDate", "Invalid date", 65);
    registerCustomDateTime("schoolStartDate", "Invalid date", 65);
    registerCustomDateTime("graduationDate", "Invalid date", 65);
}

const addEditEmpoymentFileEvent = function (mode, idToEdit) {
   
    const form = document.createElement("form");
    form.setAttribute("name", 'testForm');
    const parent = document.querySelector("#employmentArray");
    let innerId;
    $.get("/Career/EmploymentInformation", function (res) {
        form.innerHTML = res;
        if (mode == 'create') {
            ++id;
            innerId = id;
        } else {
            innerId = idToEdit;
        }

        let legend = form.querySelector(".form-legend");
        legend.textContent = " Employer/Contract Information No. " + innerId;
        legend.style.fontWeight = 700;
        form.setAttribute("data-id", id);
        form.querySelectorAll(".date").forEach(el => {
            el.addEventListener("keydown", onDateKeyDownHandler);
            el.addEventListener("focusout", (e) => { onDateChangeHandler(e, el, "Make sure the date is within the last 3 years") });
        });

        form.querySelectorAll("input[name='employmentTermination'").forEach(el => {
            el.addEventListener("click", function (e) {
                showHidden(e, "#employmentTerminationDetailShow", ["#employmentTerminationDetail"]);
            });
        });

        form.querySelectorAll("input[name='didOperateCommercialVehicle'").forEach(el => {
            el.addEventListener("click", function (e) {
                showHidden(e, "#didOperateCommercialVehicleShow");
            });
        });

        //TODO: add feature to change view and edit employment info 
        form.querySelector("#saveEmployerInfo").addEventListener("click", function (event) {
           // event.preventDefault();
            
            const myForm = document.forms.testForm;
            formData = new FormData(myForm);
            let tmpDict = new EmploymentRecord();
            const location = formData.get('employerCity') + ", " + formData.get('employerState');
            const employerCompany = formData.get('employerCompany');
            const enemployementStartDate = formData.get('enemployementStartDate');
            const enemployementEndDate = formData.get('enemployementEndDate');

            if (mode == "create") {
                addSavedEmpoymentFile(id, employerCompany , location,
                    enemployementStartDate, enemployementEndDate, form);
                for (let [key, value] of formData.entries()) {
                    tmpDict.employmentFileArr.find(x => x.key == key).value = value;
                }
                employerInfoArray.push({ key: id, value: tmpDict });
            } else {
                let array = employerInfoArray.find(x => x.key == idToEdit);
                for (let [key, value] of formData.entries()) {
                    try {
                        let element = array.value.employmentFileArr.find(x => x.key == key);
                        element.value = value;
                    } catch (e) {
                        console.log("EXCEPTION THROWN AT " + "KEY " + key + " VALUE " + value);
                    }
                  
                }

                let row = document.getElementById("employmentArray").querySelector("tr[tr-id='" + idToEdit + "']");
                let cells = row.querySelectorAll("td");
                cells[0].textContent = employerCompany;
                cells[1].textContent = location;
                cells[2].textContent = enemployementStartDate;
                cells[3].textContent = enemployementEndDate;
            }
         
         
            hoverCells();
            document.getElementById("employmentArray").removeChild(form);
            return false;
        });

        form.querySelector("#cancelEmployerInfo").addEventListener("click", function (e) {

            const form = document.querySelector("#cancelEmployerInfo").closest("form");
            form.remove();
            e.preventDefault();
        });

        if (mode == "update") {
            fillUpFile(form, idToEdit);
        }

    });
    parent.appendChild(form);
    return form;
}

const hoverCells = function (className) {
    const tableCellSelector = ".tablegrid tbody tr:not(:first-child) td:not(:last-child)";
    modifyStylesIn(tableCellSelector, "mouseenter", "add");
    modifyStylesIn(tableCellSelector, "mouseleave", "remove");
}

const modifyStylesIn = function (selector, eventName, action, className) {
    className == undefined ? className = 'background-red' : className;
    const tableCell = document.querySelectorAll(selector);
    tableCell.forEach(el => {
        el.addEventListener(eventName, function (e) {
            e.target.closest("tr").querySelectorAll("td").forEach(inEl => {
                inEl.classList[action](className);
            });

        })
    });
}
//TODO : FINISH EDITION BUG VUG
const fillUpFile = function (form, id) {
    const array = employerInfoArray.find(x => x.key == id);
    if (!array) return;
    const didOperateCommercialVehicle = document.querySelector("[name='didOperateCommercialVehicle'");
    for (let innerArr of array.value.employmentFileArr) {
        form.querySelectorAll("[name=" + innerArr.key + "]").forEach(el => {
            if (el) {
                if (el.getAttribute("type") == "radio") {
                    if (el.value == innerArr.value) {
                        el.checked = true;
                    }

                    if (el === didOperateCommercialVehicle && el.value === 'yes') {
                        const didOperateCommercialVehicleShow = document.querySelector("#didOperateCommercialVehicleShow");
                        didOperateCommercialVehicleShow.classList.remove("hide");
                        didOperateCommercialVehicle.classList.add("show");
                    }
                } else if (el.nodeName === "SELECT") {
                    el.value = innerArr.value;
                    el.classList[el.value ? "add" : "remove"]("-hasvalue");
                }
                else {
                   
                    el.value = innerArr.value;
                }
            }
        });
 
    }
}

const addNewBlankFile = function () {
    document.getElementById("add-employment-record").addEventListener("click", function () {
        const otherForm = document.getElementById("employmentArray").querySelector("form");
        if (!otherForm) {
            addEditEmpoymentFileEvent("create");
        } else {
            const formId = otherForm.getAttribute("data-id");
            alert("Finish Employer/Contract Information No. " + formId + "!");
        }
    })
}

class EmploymentRecord {
    employmentFileArr = [
        {
            "key": "employerCompany",
            "value": "",
            "question": "Company Name"
        },
        {
            "key": "enemployementStartDate",
            "value": "",
            "question": "Start date"
        },
        {
            "key": "enemployementEndDate",
            "value": "",
            "question": "End Date"
        },
        {
            "key": "employerStreetAddress",
            "value": "",
            "question": "Street Address"
        },
        {
            "key": "employerCountry",
            "value": "",
            "question": "Country"
        },
        {
            "key": "employerCity",
            "value": "",
            "question": "City"
        },
        {
            "key": "employerState",
            "value": "",
            "question": "State"
        },
        {
            "key": "employerZip",
            "value": "",
            "question": "Zip/Posta"
        },
        {
            "key": "employerTelephone",
            "value": "",
            "question": "Telephone"
        },
        {
            "key": "employmentPosition",
            "value": "",
            "question": "Position Held"
        },
        {
            "key": "employmentLeavingReason",
            "value": "",
            "question": "Reason for leaving?"
        },
        {
            "key": "employmentTermination",
            "value": "",
            "question": "Were you terminated/discharged/laid off?"
        },
        {
            "key": "employmentTerminationDetail",
            "value": "",
            "question": "Please Explain"
        },
        {
            "key": "isCurrentEmpoyer",
            "value": "",
            "question": "Is this your current employer?"
        },
        {
            "key": "canContactEmpoyer",
            "value": "",
            "question": "May we contact this employer at this time?"
        },
        {
            "key": "didOperateCommercialVehicle",
            "value": "",
            "question": "Did you operate a commercial motor vehicle?"
        },
        {
            "key": "fmcSubject",
            "value": "",
            "question": "Were you subject to the Federal Motor Carrier or Transport Canada Safety Regulations while employed/contracted by this employer/contractor?"
        },
        {
            "key": "didPerformSafetyFunctions",
            "value": "",
            "question": "Did you perform any safety sensitive functions in this job, regulated by DOT, and subject to drug and alcohol testing?"
        },
        {
            "key": "areasDriven",
            "value": "",
            "question": "Areas Driven"
        },
        {
            "key": "milesDrivenWeekly",
            "value": "",
            "question": "Miles driven weekly"
        },
        {
            "key": "payRange",
            "value": "",
            "question": "Pay Range(cents/mile)"
        },
        {
            "key": "mostCommonTruckDriven",
            "value": "",
            "question": "Most common truck driven"
        },
        {
            "key": "mostCommonTrailer",
            "value": "",
            "question": "Most common trailer"
        },
        {
            "key": "trailerLength",
            "value": "",
            "question": "Trailer length"
        }
    ]

}

