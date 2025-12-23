

$(document).ready(function () {

    const form = document.getElementById("cvForm");
    const appyBtn = document.getElementById("accept");
    appyBtn.addEventListener("click", e => {
        if (!form.checkValidity()) {
            e.preventDefault();        // stop submit
            form.reportValidity();     // show errors
            return;
        }

        let fName = $("#name").val();
        let lNmae = $("#lastName").val();
        let phone = $("#phone").val();
        if (fName.length < 2 || lNmae < 2 || phone < 2) {
            alert("Fill up the form plese");
            return;
        }

        const files = document.getElementById("cv")?.files;

        if (!files?.length) {
            alert("Attach the CV plese");
            return;
        }

        let candidate = {
            firstName: fName,
            lastName: lNmae,
            phone: phone,
            fileName: files[0].name
        }

        $.ajax({
            url: 'UploadCV',
            data: candidate,
            method: 'post',
            beforeSend: function () {
                UploadLoader.show("Loading");
            },
            success: function (response) {

                console.log(response);
                if (response.fileID && !!response.preSignedUrl) {
                    FileService.uploadFile(response.preSignedUrl, files[0], function (percent) {
                        console.log(percent)
                        UploadLoader.setProgress(percent);
                    })
                    .then(res => {
                        UploadLoader.setProgress(100);
                        markFileUploaded(response.fileID);
                        // success
                    })
                    .catch(xhr => {
                        UploadLoader.hide();
                        AppModal.fromXhr(xhr, "Upload failed");
                    });
                }
            },
            error: function (msg) {
               console.error(msg?.responseText);
                UploadLoader.hide(); 
                AppModal.fromXhr(msg, "Errors");

            }
         
        })
    });

});

let markFileUploaded = function (fileId) {
    $.ajax({
        url: 'MarkFileAsUploaded',
        data: {fileId },
        method: 'post',

        success: function (response) {
            UploadLoader.hide();
            AppModal.show({ title: "Success", text: "Submitted!" });
            setTimeout(function () {
                document.location.href = "/Career/Success";
            }, 5000);
        },
        error: function (msg) {
            console.error(msg?.responseText);
            UploadLoader.hide();
            AppModal.fromXhr(msg, "Errors");
        }
    });
}

