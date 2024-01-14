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
    if (message.includes("#")) {
        const messages = message.split("#");
        for (mess in messages) {
            if (messages[mess].includes("DiagnosisError")) {
                messages[mess] = messages[mess].replace("DiagnosisError", "");
                if (messages[mess].includes("|")) {
                    const messageArray = messages[mess].split("|");
                    const position = messageArray[0];
                    messages[mess] = messageArray[1];
                    $("#diagValidation_" + position).removeClass('hidden');
                    if ($("#diagValidation_" + position).html != "") $("#diagValidation_" + position).append("<br/>")
                    $("#diagValidation_" + position).append(messages[mess]);
                } else {
                    $("#diagCommonValidation").removeClass("hidden");
                    $('#diagCommonValidation').append(messages[mess]);
                }
            }
            else if (messages[mess].includes("ConclusionError")) {
                messages[mess] = messages[mess].replace("ConclusionError", "");
                $("#conclusionValidation").removeClass('hidden');
                $('#conclusionValidation').append(messages[mess]);
            }
        }
    }
    else {
        if (message.includes("DiagnosisError")) {
            message = message.replace("DiagnosisError", "");
            if (message.includes("|")) {
                const messageArray = message.split("|");
                const position = messageArray[0];
                message = messageArray[1];
                $("#diagValidation_" + position).removeClass('hidden');
                $("#diagValidation_" + position).html(message);
            } else {
                $("#diagCommonValidation").removeClass("hidden");
                $('#diagCommonValidation').html(message);
            }
        }
        else if (message.includes("ConclusionError")) {
            message = message.replace("ConclusionError", "");
            $("#conclusionValidation").removeClass('hidden');
            $('#conclusionValidation').html(message);
        }
        else {
            $("#errors").html(message);
            $("#errors").removeClass("hidden");
        }
    }
}

$(document).on("submit", "#createInspectionForm", function () {
    $(".text-danger").each(function () {
        $(this).addClass('hidden');
        $(this).html("");
    });
});

$(document).on("change", "#conclusion-select", function () {
    const concl = $(this).val();
    if (concl == "Disease") {
        $("#nextVisitDate").removeClass("hidden");
        $("#deathDate").addClass("hidden");
        $("#deathDate").val(null);
    }
    else if (concl == "Death") {
        $("#nextVisitDate").addClass("hidden");
        $("#deathDate").removeClass("hidden");
        $("#nextVisitDate").val(null);
    }
    else {
        $("#nextVisitDate").addClass("hidden");
        $("#deathDate").addClass("hidden");
        $("#nextVisitDate").val(null);
        $("#deathDate").val(null);
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