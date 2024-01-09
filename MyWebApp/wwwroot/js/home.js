$(document).on("submit", "#filterForm", function (event) {
    event.preventDefault();
    var params = "";
    var inputs = $("#filterForm input");
    var selects = $("#filterForm select");
    inputs.each(function () {
        var curInput = $(this);
        if (curInput.attr('type') == "checkbox") {
            var checked = curInput.is(":checked");
            if (checked) params += curInput.attr("name") + "=" + checked + "&";
        }
        else {
            if (curInput.val() != "") params += $(this).attr('name') + "=" + curInput.val() + "&";
        }
    });
    selects.each(function () {
        var curSelect = $(this);
        if (curSelect.val() != "" && curSelect.val() != "placeholder") params += $(this).attr('name') + "=" + curSelect.val() + "&";
    });
    params = params.slice(0, -1);
    var href = window.location.href;
    href = href.split("?")[0] != "" ? href.split("?")[0] : href;
    href += (params!="")?"?" + params:"";
    window.location.replace(href);
});

$(document).on("click", "#btnRegister", async function () {
    var modalcontent = await fetch("/home/getmodal");
    modalcontent = await modalcontent.text();
    $("#modalFormHolder").html(modalcontent);
    $("#patientCreateModal").css("display", "block");
    
});

$(document).on("click", ".close", function () {
    $("#patientCreateModal").css("display", "none");
});

function onPatientCreateSuccess(xhr) {
    $("#success").removeClass("d-none");
    setTimeout(function () {
        $("#success").addClass("d-none");
    }, 5000);
}

function onPatientCreateFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("d-none");
}