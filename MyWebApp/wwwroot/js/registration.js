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
    $("#success").removeClass("d-none");
    $(".form-container").prop("disable", true);
    setTimeout(function () {
        $("#success").addClass("d-none");
        window.location.replace("/profile");
    }, 5000);
}

function onRegistrationFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("d-none");
}