
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

            additionalData: {},
            success: function (response) { },
            error: function (xhr, status, error) { }

        }, options);

        this.each(function () {

            var form = $(this);
            form.on('submit', function (e) {

                if (settings.validation && this.checkValidity() === false) {
                    e.preventDefault();
                    e.stopPropagation();
                    this.classList.add('was-validated');
                }
                else {

                    var _data = form.serialize()

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
          
                            var auth = GetCookie('auth');
                            if (auth !=null)  
                                xhr.setRequestHeader('Authorization',  auth);
                        },

                        success: (response) => {

                            debugger
                            if (settings.mixin)
                                if (response.message != undefined && response.message != null && response.message != "")
                                    if (response.result)
                                        toast({
                                            type: 'success',
                                            title: response.message,
                                        })
                                    else
                                        toast({
                                            type: 'error',
                                            title: response.message,
                                        })


                            settings.success(response);
                        },

                        error: (xhr, status, error) => {

                            if (settings.mixin)
                                toast({
                                    type: 'error',
                                    title: 'بروز خطا لطفا دقایقی بعد تلاش کنید.',
                                    padding: '1em',
                                })

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