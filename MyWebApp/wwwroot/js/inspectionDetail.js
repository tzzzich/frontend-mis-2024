function expandComents(id, count, specName, expander) {
    const commentsHolder = $("#commentsHolder_" + id);
    if (expander.innerHTML.includes("Показать")) {
        if (commentsHolder.html() == "") {
            $.ajax({
                url: '/inspection/getcomments/' + id + '?SpecName='+ specName,
                method: 'get',
                type: 'text/html',
                success: function (data) {
                    if (data != "") {
                        commentsHolder.html(data);
                        commentsHolder.removeClass("hidden");
                        expander.innerHTML = " Скрыть ответы";
                    }
                },
                error: function () {
                    alert("Ошибка загрузки комментариев");
                }
            });
        }
        else {
            commentsHolder.removeClass("hidden");
            expander.innerHTML = " Скрыть ответы";
        }
    }
    else {
        commentsHolder.addClass("hidden");
        expander.innerHTML = " Показать ответы ("+count+")";
    }
}

function addComment(commentId) {
    $("#commentReplyHolder_" + commentId).removeClass("hidden");
}

function addCommentSave(commentId, consultId) {
    const text = $("#newCommentText_" + commentId).val();
    data = {
        content: text,
        parentId: commentId,
        consultId: consultId,
    };
    $.ajax({
        url: "/Inspection/AddComment",
        method: 'post',
        data: data,
        success: function () {
            alert('Комментарий успешно добавлен');
            window.location.reload();
        },
        error: function (xhr) {
            try {
                var message = xhr.responseJSON.message;
                if (message.includes("User doesn't have a specialty to participate")) {
                    message = "Вы не можете добавлять коментарии к этой консультации";
                    $("#commentReplyHolder_" + commentId).addClass("hidden");
                }
                if (message.includes("The Content field is required")) {
                    message = "Комментарий не может быть пустым";
                }
                $("#errorComment_" + commentId).html(message);
                $("#errorComment_" + commentId).removeClass("hidden");
            }
            catch {
                alert("Ошибка добавления комментариев");
            }
        }
    });
}

function editComment(commentId, consultId, button) {
    if (!$(button).data('lockedAt') || +new Date() - $(button).data('lockedAt') > 300)
    {
        editLocked = true;
        const span = $("#comment_span_" + commentId);
        const input = $('#comment_input_' + commentId);
        const errorHolder = $("#errorComment_" + commentId);
        if (button.innerHTML.includes("Редактировать")) {
            button.innerHTML = "Сохранить";
            span.addClass("hidden");
            input.removeClass("hidden");
            input.focus();
        }
        else {
            const data = {
                CommentId: commentId,
                Content: input.val(),
            }
            $.ajax({
                url: "/Inspection/EditComment",
                method: 'put',
                data: data,
                success: function () {
                    errorHolder.html("Комментарий сохранен успешно");
                    errorHolder.removeClass('hidden');
                    errorHolder.removeClass('text-danger');
                    errorHolder.addClass('text-success');
                    setTimeout(function () {
                        errorHolder.addClass('hidden');
                        errorHolder.addClass('text-danger');
                        errorHolder.removeClass('text-success');
                    }, 5000);
                    span.html(input.val());
                    span.removeClass('hidden');
                    input.addClass('hidden');
                    button.innerHTML = "Редактировать комментарий";
                },
                error: function (xhr) {
                    var message = xhr.responseJSON.message;
                    alert("Ошибка сохранения комментария: " + message);
                }
            });

        }
    }
    $(button).data('lockedAt', +new Date());
}

var editLocked = false;

function closeEdit(commentId) {
    setTimeout(function () {
        $("#comment_span_" + commentId).removeClass('hidden');
        $('#comment_input_' + commentId).addClass('hidden');
        $('#comment_button_' + commentId).html("Редактировать комментарий");
    }, 100);
}

$(document).on("click", "#btnInspectionEdit", async function () {
    var modalcontent = await fetch("/Inspection/GetInspectionEditModal/"+ inspectionId);
    modalcontent = await modalcontent.text();
    $("#modalFormHolder").html(modalcontent);
    $("#inspectionModal").removeClass('hidden');

});

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
});

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
});

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
            $("#innder_" + level + "_" + position).html(data);
            $("#errors").addClass("hidden");
        },
        error: function (xhr) {
            var message = xhr.responseJSON.message;
            $("#errors").html(message);
            $("#errors").removeClass("hidden");
        }
    })
}

function onEditSuccess(xhr) {
    alert("Данные сохранены успешно");
    window.location.reload();
}

function onEditFailed(xhr) {
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

function closeModal() {
    $('#inspectionModal').addClass('hidden');
}

$(document).on("submit", "#editInspectionForm", function () {
    $(".text-danger").each(function () {
        $(this).addClass('hidden');
        $(this).html("");
    });
});