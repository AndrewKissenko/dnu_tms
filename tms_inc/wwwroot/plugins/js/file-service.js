window.FileService = (function () {

    function uploadFile(fileUploadUrl, file, onProgress) {
        return new Promise((resolve, reject) => {

            const formData = new FormData();
            formData.append("file", file);

            const xhr = new XMLHttpRequest();
            xhr.open("PUT", fileUploadUrl, true);

            // Upload progress
            xhr.upload.onprogress = function (e) {
                if (e.lengthComputable && typeof onProgress === "function") {
                    const percent = Math.round((e.loaded / e.total) * 100);
                    onProgress(percent);
                }
            };  

            xhr.onload = function () {
                if (xhr.status >= 200 && xhr.status < 300) {
                    resolve(xhr.response);
                } else {
                    reject(xhr);
                }
            };

            xhr.onerror = function () {
                reject(xhr);
            };

            xhr.send(formData);
        });
    }

    return {
        uploadFile
    };

})();
