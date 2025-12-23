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
