function onLoginSuccess(xhr) {
    alert("Вы вошли успешно!");
    window.location.replace("/");
}

function onLoginFailed(xhr) {
    var message = xhr.responseJSON.message;
    if (message == "Login failed") message = "Неверный логин или пароль";
    $("#errors").html(message);
    $("#errors").removeClass("hidden");
}