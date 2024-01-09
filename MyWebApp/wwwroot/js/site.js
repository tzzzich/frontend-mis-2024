$(document).on("click", "#logoutBtn", function () {
    $.ajax({
        url: "/login/logout",
        method: "post",
        complete: function () {
            window.location.reload();
        },
    });
});

$(document).ready(function () {
    $(".phone-mask").each(function () {
        var val = $(this).val().replace("+", "").replace("(", "").replace(")", "").replace("-", "").replace("+");
        if (val.substring(0,1) == "8" || val.substring(0,1) == "7") {
            $(this).val(val.substring(1))
        }
    });
    $(".phone-mask").mask("+7(999)999-9999");
});
