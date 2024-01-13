$(document).on("click", ".switch", function () {
    const checked = $("#IsSecondary").is(":checked");
    const primary = $("#primaryLabel");
    const secondary = $("#secondaryLabel");
    const prevHolder = $("#prevSelectHolder");
    const prevSelect = $("#prevSelectId");
    if (checked) {
        primary.removeClass("deepblue");
        secondary.addClass("deepblue");
        prevSelect.prop("disabled", false);
        prevHolder.removeClass("hidden");
    }
    else {
        primary.addClass("deepblue");
        secondary.removeClass("deepblue");
        prevSelect.prop("disabled", true);
        prevHolder.addClass("hidden");
    }
});

function onCreateSuccess(xhr) {
    alert("Данные сохранены успешно");
    window.location.replace("/patient/" + patientId);
}

function onCreateFailed(xhr) {
    var message = xhr.responseJSON.message;
    $("#errors").html(message);
    $("#errors").removeClass("hidden");
}

$(document).on("change", "#conclusion-select", function () {
    const concl = $(this).val();
    if (concl == "Disease") {
        $("#nextVisitDate").removeClass("hidden");
        $("#deathDate").addClass("hidden");
        $("#deathDate").val("");
    }
    else if (concl == "Death") {
        $("#nextVisitDate").addClass("hidden");
        $("#deathDate").removeClass("hidden");
        $("#nextVisitDate").val("");
    }
    else {
        $("#nextVisitDate").addClass("hidden");
        $("#deathDate").addClass("hidden");
        $("#nextVisitDate").val("");
        $("#deathDate").val("");
    }
})

var consCount = 0;
var diagCount = 0;

function toggleConsult(selector) {
    $("#" + selector).prop('disabled', (i, v) => !v);
}

$(document).on("click", "#addConsultation", function (e) {
    e.preventDefault();
    $.ajax({
        url: "/inspection/addconsultation/" + consCount,
        method: "get",
        dataType: "html",
        success: function (data) {
            $("#ConsultationsHolder").append(data);
            consCount += 1;
            $("#errors").addClass("hidden");
        },
        error: function (xhr) {
            var message = xhr.responseJSON.message;
            $("#errors").html(message);
            $("#errors").removeClass("hidden");
        }
    })
})

$(document).on("click", "#addDiagnosis", function (e) {
    e.preventDefault();
    $.ajax({
        url: "/inspection/adddiagnosis/" + diagCount,
        method: "get",
        dataType: "html",
        success: function (data) {
            $("#diagnosesHolder").append(data);
            diagCount += 1;
            $("#errors").addClass("hidden");
        },
        error: function (xhr) {
            var message = xhr.responseJSON.message;
            $("#errors").html(message);
            $("#errors").removeClass("hidden");
        }
    })
})

function selectChanged(level, position, select) {
    $("#diag_" + position).val($(select).val());
    const selectedItem = select.options[select.selectedIndex]
    const selectedItemText = selectedItem.text;
    var code = selectedItemText.split(" ")[0].replace("(", "").replace(")");
    $.ajax({
        url: "/inspection/getdiaglevellist?level=" + level + "&position=" + position + "&parentDiag=" + code,
        method: "get",
        dataType: "html",
        success: function (data) {
            $("#innder_"+level+"_"+position).html(data);
            $("#errors").addClass("hidden");
        },
        error: function (xhr) {
            var message = xhr.responseJSON.message;
            $("#errors").html(message);
            $("#errors").removeClass("hidden");
        }
    })
}