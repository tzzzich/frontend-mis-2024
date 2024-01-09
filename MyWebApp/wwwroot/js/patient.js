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
        else if (curInput.attr('type') == "radio") {
            if (curInput.is(':checked') && curInput.val() == "true")
                params += $(this).attr('name') + "=" + curInput.val() + "&";
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

function inspectionCreateRederict(patient, secondary, parent) {
    var data = {};
    data.PatientId = patient;
    data.IsSecondary = secondary;
    if (parent != null || parent !== undefined || parent != "undefined") {
        data.ParentId = parent;
    }
    $.ajax({
        url: "/patient/storedata",
        data: data,
        method: "post",
        success: function () {
            window.location.replace("/inspection/create/");
        },
    });
}

function expand(el, selector) {
    if ($(el).html() == "+") {
        $(el).html("-");
        $("." + selector).each(function () {
            $(this).removeClass("hidden-block");
        });
    }
    else {
        $(el).html("+");
        $("." + selector).each(function () {
            $(this).addClass("hidden-block");
            $("." + $(this).data("id")).each(function(){
                $(this).addClass("hidden-block");
            });
            var expTmp = $(this).find(".inspection-expander");
            expTmp.html('+');
        });
    }
}

$(document).ready(function () {
    var maxheight = 0;
    var elements = $(".inspection-box");
    elements.each(function() {
        var height = $(this).outerHeight();
        if (maxheight < height) maxheight = height;
    });
    elements.each(function ()
    {
        $(this).css("min-height", maxheight);
    })
});