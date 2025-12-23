
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
        isEligible: undefined,
        isEmployed: undefined,
        lastEmploymentDay: undefined,
        isEnglish: undefined,
        isWorkedBefore: undefined,
        isTWICOwner: undefined,
        twicExpirationDate: undefined,
        isOtherName: undefined,
        otherName: undefined,
        isFMCSACompleted: undefined,
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



    }

}