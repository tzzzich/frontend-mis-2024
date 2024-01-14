$(document).on("click", ".spec-results option", function () {
    var selectedText = $(this).html();
    var selectedValue = $(this).val();
    $("#searchBox").val(selectedText)
    $("#Speciality").val(selectedValue);
});

$(document).on("input", "#searchBox", function () {
    var text = $(this).val();
    $.ajax({
        method: "get",
        url: "/registration/getspecialites?spec=" + text,
        dataType: "html",
        success: function (data) {
            $("#Speciality").val(null);
            $(".spec-results").html(data);
            var options = $(".spec-results option");
            var optionsCount = options.length;
            if (optionsCount > 10) optionsCount = 10;
            $(".spec-results").prop("size", optionsCount)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR);
        }
    });
});

$(document).ready(function () {
    $.ajax({
        method: "get",
        url: "/registration/getspecialites?spec=",
        dataType: "html",
        success: function (data) {
            $(".spec-results").html(data);
            var options = $(".spec-results option");
            var optionsCount = options.length;
            if (optionsCount > 10) optionsCount = 10;
            $(".spec-results").prop("size", optionsCount)
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(jqXHR);
        }
    });
});

function onRegistrationSuccess(xhr) {
    $("#success").removeClass("hidden");
    $("#errors").addClass("hidden");
    $(".form-container").prop("disable", true);
    setTimeout(function () {
        $("#success").addClass("hidden");
        window.location.replace("/profile");
    }, 5000);
}

function onRegistrationFailed(xhr) {
    var message = xhr.responseJSON.message;
    if (message.includes("is already taken")) {
        message = message.replace('Username', "Пользователь с адресом электронной почты").replace('is already taken', 'уже существует');
    }
    $("#errors").html(message);
    $("#errors").removeClass("hidden");
}