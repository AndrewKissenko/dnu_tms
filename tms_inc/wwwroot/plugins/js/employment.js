

const onDateKeyDownHandler = function (e) {
    if (e.which !== 8 && e.which !== 46 && e.which !== 37 && e.which !== 39 && e.which !== 38 && e.which !== 40) {
        e.preventDefault();
        }
   
    let prevVal = e.target.value;
        const res = parseInt(e.key);
        const selectionLength = e.target.selectionStart;
        if (!isNaN(res) && prevVal.length <= 9) {
            prevVal = !prevVal ? "" : prevVal;

            if (selectionLength == prevVal.length) {
                e.target.value = "";
                let str = prevVal + e.key;
                if (str.length === 2 || str.length === 5) {
                    str += "/";
                }
                e.target.value = str;
            }
            else {
                prevVal = prevVal.split('');
                prevVal.splice(selectionLength, 0, e.key);
                e.target.value = prevVal.join("");
                e.target.setSelectionRange(selectionLength + 1, selectionLength + 1);
            }
        }
};

const onDateChangeHandler = function (event, el, error, lowerBound) {
        try {
            const dateStr = event.target.value;
            const date = new Date(dateStr);
            if (date == "Invalid Date") {
                event.target.focus();
                event.target.style.color = 'red';
                el.closest("div").classList.add("form-has-error");
                if (!el.closest("div").querySelector("small")) {
                    const small = document.createElement("small");
                    small.classList.add("form-element-hint");
                    small.textContent = error;
                    el.closest("div").appendChild(small);
                }
            } else {
                el.style.color = 'black';
                el.closest("div").classList.remove("form-has-error");
                if (el.closest("div").querySelector("small")) {
                    el.closest("div").querySelector("small").remove();
                }
            }

        } catch (e) {
            console.log(e);
        }
};



const showHidden = function (event, elementToShowId, ...makeRequiredInputIds) {
    let element = document.querySelector(elementToShowId);
        if (event.target.value === "yes") {
            if (makeRequiredInputIds) {
                makeRequiredInputIds.forEach(id => {
                    document.querySelector(id).setAttribute("required", true);
                })

            }
            element.classList.add("show");
            element.classList.remove("hide");
        }
        else {
            if (makeRequiredInputIds) {
                makeRequiredInputIds.forEach(id => {
                    document.querySelector(id).setAttribute("required", false);
                })

            }
            element.classList.add("hide");
            element.classList.remove("show");
        }
}




const dateIsValid = function (date, notLaterThan) {
    const myNotLaterThan = notLaterThan ?
        notLaterThan : 4;
    const year = new Date().getFullYear() - myNotLaterThan;
    const lowerBoundDate = new Date(year, 6);
    if (date > new Date()
        || date < lowerBoundDate) {
        return false;
    }
    return true;
}


const employmentInformation = {
    empoymentId: undefined,
    employerCompany: "",
    enemployementStartDate: "",
    enemployementEndDate: "",
    employerStreetAddress: "",
    employerCountry: "",
    employerCity: "",
    employerState: "",
    employerZip: "",
    employerTelephone: "",
    employmentPosition: "",
    employmentLeavingReason: "",
    employmentTermination: "",
    employmentTerminationDetail: "",
    isCurrentEmpoyer: "",
    canContactEmpoyer: "",
    didOperateCommercialVehicle: "",
    fmcSubject: "",
    didPerformSafetyFunctions: "",
    milesDrivenWeekly: "",
    payRange: "",
    mostCommonTruckDriven: "",
    mostCommonTrailer: "",
    trailerLength: ""

}
