//main settings
var pageIndex = 1;
var pageSizeSettings = 30;
var pageSizeCategories = 30;
var pageSizeArticles = 30;
var pageSizeTutorials = 30;
var pageSizeStatistics = 30;

$(function () {
    $(".required").bind("blur", function () {
        if ($(this).attr("class").indexOf("red-border") >= 0 && $("#messages").html().length > 0) {
            $(this).removeClass("red-border");
        }
    });

    $(".form input, .form select, .form textarea").bind("keydown, keyup", function (event) {
        var messageDiv = $(this).parents().find(".form").attr("data-message");
        if (messageDiv != null) {
            if ($("#" + messageDiv).html().length > 0) {
                var messages = new Array();
                $("#" + messageDiv).html("");
                validateFields(messages, "#" + $(this).parents().find(".form").attr("id"));
                if (messages.length > 0) {
                    $("#" + messageDiv).html("<div class='error-message'>" + messages.join(' ') + "</div>");
                }
            }
        }

        var keycode = (event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode));
        if (keycode == 13) {
            var triggerButton = $(this).parents().find(".form").attr("data-button");
            if ($("#" + triggerButton).length > 0) {
                $("#" + triggerButton).click();
            }
            return false;
        } else {
            return true;
        }
    });

    $(".date-picker").each(function () {
        $(this).datepicker({
            dateFormat: 'd M yy',
            showButtonPanel: true,
            changeMonth: true,
            changeYear: true,
            showOtherMonths: true,
            selectOtherMonths: true
        });
    });

    if ($(".scrollsection").length > 0) {
        $(".scrollsection").click(function () {
            var tagID = $(this).attr("data-id");
            scrollingPage(tagID, 100);
        });
    }

    if ($('pre').length > 0) {
        $('pre').each(function (i, e) { hljs.highlightBlock(e) });
    }

    if($(".sticky").length > 0){
        $(".sticky").sticky({ topSpacing: 0 });
    }

});

function scrollingPage(tagName, point) {
    $("html, body").stop().animate({ scrollTop: $(tagName).position().top }, 2000);
}

function validateFields(messages, prefix) {
    $(prefix + " .required").each(function () {
        if ($(this).val() == "" || ($(this).attr("placeholder") != "" && $(this).attr("placeholder") == $(this).val())) {
            messages.push("* " + $(this).attr("data-name") + " required.<br/>");
            $(this).addClass("red-border");
        } else {
            $(this).removeClass("red-border");
        }
    });

    $(prefix + " .email-field").each(function () {
        if (($(this).attr("placeholder") != "" && $(this).attr("placeholder") != $(this).val())) {
            if (validateEmail($(this).val())) {
                $(this).removeClass("red-border");
            } else {
                messages.push("* Invalid email for field " + $(this).attr("data-name") + ".<br/>");
                $(this).addClass("red-border");
            }
        } else {
            $(this).removeClass("red-border");
        }
    });
}

function validateEmail(email) {
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
}

function getSearchParameters() {
    var prmstr = window.location.search.substr(1);
    return prmstr != null && prmstr != "" ? transformToAssocArray(prmstr) : {};
}

function transformToAssocArray(prmstr) {
    var params = {};
    var prmarr = prmstr.split("&");
    for (var i = 0; i < prmarr.length; i++) {
        var tmparr = prmarr[i].split("=");
        params[tmparr[0]] = tmparr[1];
    }
    return params;
}

function getDate(dt) {
    var d = new Date(dt);
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return d.getDate() + ' ' + months[d.getMonth()] + ' ' + d.getFullYear();
}

/*************** POPUP ****************/
function popUpTransparency() {
    //we going to check first if a popup transparent is exist or not, if not created one.
    if ($("#popup-box-transparent").length == 0) {
        $("body").prepend("<div id='popup-box-transparent'></div>");
        $("body").prepend("<div id='popup-box'></div>");
    }

    //we set the max height
    $("#popup-box-transparent").css("height", $(document).height() + "px");
    $("#popup-box-transparent").show();
}

function hidePopUpTransparency() {
    $("#popup-box-transparent").hide();
    $("#popup-box").hide();
}

function loadPopUpMessage(message) {
    popUpTransparency();
    $("#popup-box").html("<div class='loading-image'><img src='/content/images/ajax-loader.gif'/></div>" + message);
    $("#popup-box").show();
}

function loadPopUpCloseMessage(message) {
    popUpTransparency();
    $("#popup-box-transparent").bind("click", function () {
        closePopup();
    });
    $("#popup-box").html("<div class='loading-image'><img class='pointer' align='right' src='/content/images/close.png' onclick='closePopup()'/></div>" + message);
    $("#popup-box").show();
}

function closePopup() {
    $("#popup-box-transparent").unbind("click");
    $("#popup-box-transparent").hide();
    $("#popup-box").hide();
}

