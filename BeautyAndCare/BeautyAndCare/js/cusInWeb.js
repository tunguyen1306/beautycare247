
$(document).ready(function () {
    $("#menu-icon").on("click", function () {
        $("#menu-gadget .menu").slideToggle();
        $(this).toggleClass("active");
    });

    $('#menu-gadget .menu').find('li>ul').before('<i class="fa fa-angle-down"></i>');
    $('#menu-gadget .menu li i').on("click", function () {
        if ($(this).hasClass('fa-angle-up')) {
            $(this).removeClass('fa-angle-up').parent('li').find('> ul').slideToggle();
        }
        else {
            $(this).addClass('fa-angle-up').parent('li').find('> ul').slideToggle();
        }
    });
});



var num = 80; //number of pixels before modifying styles

$(window).bind('scroll', function () {
    if ($(window).scrollTop() > num) {
        $('.isStuck').css('top', '-48px');
        $('.isStuck').css('visibility', 'hidden');
        $('.isStuck').css('width', '100%');
        $('.isStuck').css('margin-top', 'hidden');
        $('.isStuck').css('visibility', 'hidden');
    } else {
        $('#main-header1').removeClass('fixed');
    }
});


$(document).ready(function () {
    $('body').data('home_url', '');
    //$('body').data('page_url', '');
    $('body').data('extension', '.html');
    Product.init();
    WebCommon.init();
    // alert("-Golbal aler- "+$('body').data('home_url'));
});


$(".lentop").click(function (event) {
    $('html, body').animate({ scrollTop: 0 }, 1000);
});
function alertMsg() {
    var tt = $('.popup_qc').fadeOut("slow");
}
function alertMsg2() {
    var tt = $('.popup_qc').fadeIn();
}
$(document).ready(function () {
    if ($.cookie('clpopup') != 1) {
        setTimeout(alertMsg2, 3000);
        var date = new Date();
        date.setTime(date.getTime() + (5 * 60 * 1000));
        $.cookie("clpopup", "1", { expires: date });
    }
    $(".popup_qc_close").click(function () {
        $('.popup_qc').fadeOut("slow");
    });
});

$(document).ready(function () {
    Likeproduct.init();
    HomeProductCategory.init();
});