(function ($) {
    $.fn.ajaxForm = function (options) {
        var settings = $.extend({
            url: '',
            method: 'POST',
            validation: true,
            jwt: true,
            miniloader: false,
            blockui: false,
            mixin: false,
            target: "",
            additionalData: {},
            success: function (response) { },
            error: function (xhr, status, error) { }

        }, options);

        this.each(function () {
            var form = $(this);
            form.on('submit', function (e) {
                if (settings.validation && ValidateForm(this) === false) {
                    e.preventDefault();
                    e.stopPropagation();
                    this.classList.add('was-validated');
                }
                else {
                    var _data = form.serialize()

                    debugger;

                    if (!settings.miniloader && !settings.blockui)
                        ShowLoader();

                    if (settings.miniloader)
                        ShowMiniLoader(this);

                    if (settings.blockui)
                        BlockUi(this);

                    $.ajax({
                        url: settings.url || form.attr('action'),
                        method: settings.method || form.attr('method'),
                        data: _data,
                        beforeSend: function (xhr) {
                            var auth = GetCookie('gldauth');
                            if (auth != null)
                                xhr.setRequestHeader('Authorization', auth);
                        },
                        success: (response) => {
                            debugger;
                            if (response) {
                                if (settings.mixin) {
                                    if (response.message) {
                                        if (response.result) {
                                            toast({
                                                type: 'success',
                                                title: response.message,
                                            })
                                        }
                                        else {
                                            toast({
                                                type: 'error',
                                                title: response.message,
                                            })
                                        }
                                    }
                                }
                                if (settings.target && response) {
                                    $(settings.target).empty();
                                    $(settings.target).append(response);
                                }
                            } else {
                                GetRedirectToUrl("/Account/Login");
                            }
                            settings.success(response);
                        },
                        error: (xhr, status, error) => {
                            if (settings.mixin) {
                                toast({
                                    type: 'error',
                                    title: 'بروز خطا لطفا دقایقی بعد تلاش کنید.',
                                    padding: '1em',
                                });
                            }
                            console.error('Form submission failed:', error);
                            settings.error(xhr, status, error);
                        },
                        complete: () => {
                            HideLoader();
                            if (settings.miniloader)
                                HideMiniLoader(this);
                            if (settings.blockui)
                                UnBlock(this);
                        }
                    });
                }
            });
        });
        return this;
    };
})(jQuery);

const toast = swal.mixin({
    toast: true,
    position: 'bottom-start',
    showConfirmButton: false,
    timer: 5000,
    padding: '1em',
    background: 'rgb(95, 193, 94)',
    color: '#000'
});

let loader = $('<div id="loader">'
    + '<div class="loader mx-auto" ></div>'
    + '</div>');

let ShowLoader = () => {
    loader.appendTo('body').show();
}

let HideLoader = () => {
    loader.remove();
}

let ShowMiniLoader = (form) => {
    let submit = $(form).find('[type = "submit"]');
    let text = $(submit).html();
    $(submit).data('text', text);
    let miniloader = '<div class="spinner-border text-white mr-2 align-self-center loader-sm "></div>لطفا صبر کنید . . .'
    $(submit).attr('disabled', true).html(miniloader);
}

let HideMiniLoader = (form) => {
    let submit = $(form).find('[type = "submit"]');
    let text = $(submit).data('text');
    $(submit).attr('disabled', false).html(text);
}

let BlockUi = (form) => {
    var block = $(form).parent();
    $(block).block({

        ignoreIfBlocked: true,
        message: 'لطفا صبر کنید ...',
        overlayCSS: {
            backgroundColor: '#000',
            opacity: 0.4,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#fff',
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
}

let UnBlock = (form) => {

    var block = $(form).parent();
    $(block).unblock();
}

function GetCookie(name) {
    var value = "; " + document.cookie;
    var parts = value.split("; " + name + "=");
    if (parts.length == 2) return parts.pop().split(";").shift();
}

function GetRedirectToUrl(url) {
    window.location = url;
}

function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}


function ValidateForm(form) {
    debugger
    let isValid = true;
    $('.invalid-feedback', form).remove();

    Array.from(form.elements).forEach(element => {


        if (!element.checkValidity()) {

            isValid = false;
            let errorMessage = element.dataset.valRequired || "وارد کردن این فیلد الزامیست";

            if (element.validity.valueMissing) {
                errorMessage = element.dataset.valRequired || "وارد کردن این فیلد الزامیست";
            } else if (element.validity.typeMismatch && element.type === 'email') {
                errorMessage = element.dataset.valEmail || "لطفا ایمیل معتبر وارد کنید.";
            } else if (element.validity.rangeUnderflow || element.validity.rangeOverflow) {
                errorMessage = element.dataset.valRange || "مفدار وارد شده خارج از محدوده مورد نظر است";
            } else if (element.validity.patternMismatch) {
                errorMessage = element.dataset.valPattern || "مقدار وارد شده معتبر نیست";
            }

            $(element).parent('.form-group').append('<div class="invalid-feedback">' + errorMessage +'</div>')

        }

        // Check if the element is an email input and validate it
        if (element.type === 'email' && !isValidEmail(element.value)) {
            isValid = false;
            const errorMessage = element.dataset.valEmail || "Please enter a valid email address.";
            $(element).parent('.form-group').append('<div class="invalid-feedback">' + errorMessage + '</div>')
        }
    });

    return isValid;

}

