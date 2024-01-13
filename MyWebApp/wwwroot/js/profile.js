function onSaveSuccess(xhr) {
    $("#success").removeClass("hidden");
    setTimeout(function () {
        $("#success").addClass("hidden");
    }, 3000);
}

function oSaveFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("hidden");
}