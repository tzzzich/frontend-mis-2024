$(document).on("click", ".switch", function () {
    const checked = $("#IsSecondary").is(":checked");
    const primary = $("#primaryLabel");
    const secondary = $("#secondaryLabel");
    if (checked) {
        primary.removeClass("deepblue");
        secondary.addClass("deepblue");
    }
    else {
        primary.addClass("deepblue");
        secondary.removeClass("deepblue");
    }
});

function onCreateSuccess(xhr) {
    $("#success").removeClass("d-none");
    setTimeout(function () {
        $("#success").addClass("d-none");
    }, 3000);
}

function onCreateFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("d-none");
}