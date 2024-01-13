﻿function showRootsList() {
    $("#rootsHolder").removeClass('hidden');
    $("#rootsHolder").focus();
}

$(document).mouseup(function (e) {
    var container = $("#rootsHolder");
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.addClass('hidden');  
    }
});

$(document).not("#rootsHolder").click(function () {
    $("#rootsHolder").addClass('hidden');
});

$("#rootsHolder").focusout(function () {
    $("#rootsHolder").hide();
});

function hideRootsList() {
    $("#rootsHolder").addClass('hidden');
}

function addRootItem(id) {
    $("#innerRootItem_" + id).removeClass('hidden');
    $("#outerRootItem_" + id).addClass('hidden');
    $("#rootid_" + id).prop("disabled", false);
}

function removeRootItem(id) {
    $("#innerRootItem_" + id).addClass('hidden');
    $("#outerRootItem_" + id).removeClass('hidden');
    $("#rootid_" + id).prop("disabled", true);
}