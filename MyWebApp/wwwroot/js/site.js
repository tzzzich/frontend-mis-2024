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
    $(".phone-mask").mask("?+7(999)999-9999");
});

const hamburger = document.querySelector('.hamburger');
const mobileMenu = document.querySelector('.mobile-menu');

hamburger.addEventListener('click', function () {
    mobileMenu.classList.toggle('active-for-mobil');
});

const profileBtn = document.querySelector('.profile-btn');
const profileMenu = document.querySelector('.profile-menu');

profileBtn.addEventListener('click', () => {
    profileMenu.classList.toggle('active');
});

$(document).on("change", "with-placeholder", function () {
    if ($(this).val() == "placeholder") $(this).addClass("placeholder");
    else $(this).removerClass("placeholder");
})