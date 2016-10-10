$(document).ready(function () {

    var url = window.location.href;
    if (url.indexOf('/WebSite/Contact')) {

        $('body').removeClass('common-home');

    }
    else if (url.indexOf('/WebSite/Index')) {

        $('body').addClass('common-home');

    }
    else if (url.indexOf('/WebSite/Login')) {

        $('body').addClass('account-login');

    }
});