function onSaveSuccess(xhr) {
    $("#success").removeClass("d-none");
    setTimeout(function () {
        $("#success").addClass("d-none");
    }, 3000);
}

function oSaveFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("d-none");
}