/*
=========================================
|                                       |
|           Scroll To Top               |
|                                       |
=========================================
*/
$('.scrollTop').click(function () {
    $("html, body").animate({ scrollTop: 0 });
});


$('.navbar .dropdown.notification-dropdown > .dropdown-menu, .navbar .dropdown.message-dropdown > .dropdown-menu ').click(function (e) {
    e.stopPropagation();
});

/*
=========================================
|                                       |
|       Multi-Check checkbox            |
|                                       |
=========================================
*/

function checkall(clickchk, relChkbox) {

    var checker = $('#' + clickchk);
    var multichk = $('.' + relChkbox);


    checker.click(function () {
        multichk.prop('checked', $(this).prop('checked'));
    });
}


/*
=========================================
|                                       |
|           MultiCheck                  |
|                                       |
=========================================
*/

/*
    This MultiCheck Function is recommanded for datatable
*/

function multiCheck(tb_var) {
    tb_var.on("change", ".chk-parent", function () {
        var e = $(this).closest("table").find("td:first-child .child-chk"), a = $(this).is(":checked");
        $(e).each(function () {
            a ? ($(this).prop("checked", !0), $(this).closest("tr").addClass("active")) : ($(this).prop("checked", !1), $(this).closest("tr").removeClass("active"))
        })
    }),
        tb_var.on("change", "tbody tr .new-control", function () {
            $(this).parents("tr").toggleClass("active")
        })
}

/*
=========================================
|                                       |
|           MultiCheck                  |
|                                       |
=========================================
*/

function checkall(clickchk, relChkbox) {

    var checker = $('#' + clickchk);
    var multichk = $('.' + relChkbox);


    checker.click(function () {
        multichk.prop('checked', $(this).prop('checked'));
    });
}

/*
=========================================
|                                       |
|               Tooltips                |
|                                       |
=========================================
*/

$('.bs-tooltip').tooltip();

/*
=========================================
|                                       |
|               Popovers                |
|                                       |
=========================================
*/

$('.bs-popover').popover();


/*
================================================
|                                              |
|               Rounded Tooltip                |
|                                              |
================================================
*/

$('.t-dot').tooltip({
    template: '<div class="tooltip status rounded-tooltip" role="tooltip"><div class="arrow"></div><div class="tooltip-inner"></div></div>'
})


/*
================================================
|            IE VERSION Dector                 |
================================================
*/

function GetIEVersion() {
    var sAgent = window.navigator.userAgent;
    var Idx = sAgent.indexOf("MSIE");

    // If IE, return version number.
    if (Idx > 0)
        return parseInt(sAgent.substring(Idx + 5, sAgent.indexOf(".", Idx)));

    // If IE 11 then look for Updated user agent string.
    else if (!!navigator.userAgent.match(/Trident\/7\./))
        return 11;

    else
        return 0; //It is not IE
}

$(function () {
    $(".select2").prepend('<option selected=""></option>').select2({
        placeholder: "لطفا انتخاب کنید ...",
        theme: "bootstrap",
        allowClear: true,
        width: "100%",
        dir: "rtl",
        //dropdownAutoWidth: true,
    });
    $(".select2").on("change", function (e) {
        var value = $(this).val();
        var required = $(this).attr('required');
        if (!value && required) {
            $(this).siblings(".select2-container").children(".selection").children(".select2-selection").css({ "border-color": "red" });
            const errorMessage = "وارد کردن این فیلد الزامیست";
            var errorLiveMsg = $(this).parent('div').children(".invalid-feedback.live").length;
            var errorMsg = $(this).parent('div').children(".invalid-feedback").length;
            if (errorMsg < 1 && errorLiveMsg < 1) {
                $(this).parent('div').append('<div class="invalid-feedback live">' + errorMessage + '</div>')
            }
            $(this).parent('div').children(".invalid-feedback.live").css("display", "block");
        } else {
            $(this).siblings(".select2-container").children(".selection").children(".select2-selection").css({ "border-color": "" });
            $(this).parent('div').children(".invalid-feedback.live").css("display", "none");
        }
    });

});