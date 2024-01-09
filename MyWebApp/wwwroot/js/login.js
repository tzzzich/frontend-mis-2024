function onLoginSuccess(xhr) {
    alert("Вы вошли успешно!");
    window.location.replace("/");
}

function onLoginFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("d-none");
}