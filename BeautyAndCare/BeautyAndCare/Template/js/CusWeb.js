$(document).ready(function () {

    var url = window.location.href;
    if (url.indexOf('/WebSite/Contact')) {

        $('body').removeClass('common-home');

    }
    if (url.indexOf('/WebSite/Index')) {

        $('body').addClass('common-home');

    }
    if (url.indexOf('/WebSite/Login')) {

        $('body').addClass('account-login');

    } if (url.indexOf('/WebSite/Register')) {

        $('body').addClass('account-register');

    }
    var total=0;
    $.each($('.price-total'), function (i, o) {
        var t = $(o).find('input[type="hidden"][name="hdtotal"]').val();
        console.log(parseInt(t));
        total += parseInt(t)

    });
    $('.tx-price').html(total);
    console.log(total);
   
});
///////////////SubMit
$('#button-coupon').click(function () {
    AddCoupon();
});
//////////////
function addToCart(id) {
    var url = "WebSite/AddToCart"
    $.post(url,
   {
       ProductId: id
   },
   function (o, status) {
       var stringHtml;
           stringHtml += "<tr class=\"cusTr\">"
           + "<td class=\"text-center\">"
           + " <div class=\"image\">"
             + " <input type=\"hidden\" name=\"idPro\" id=\"hdProductsId_" + o.IdProducts + "\" value=\"" + o.IdProducts + "\" />"
           + " <a href=\"#\">"
           + "   <img src=\"http://livedemo00.template-help.com/opencart_59086/image/cache/catalog/products/product_55-150x150.png\" alt=\"" + o.NameProducts + "\" title=\"" + o.NameProducts + "\" class=\"img-thumbnail\">"
           + "  </a>"
           + " </div>"
           + "  </td>"
           + " <td class=\"text-left\">"
           + " <div class=\"name\">"
           + " <a href=\"#\">" + o.NameProducts + "</a>"
           + " </div>"
           + " </td>"
           + "<td class=\"text-right\">"
           + " <span class=\"price-cart\">" + o.PriceProducts + "</span>"
           + " </td>"
           + " </tr>";
           $('.LoadProducts').append(stringHtml);
       });
}
function ViewCart()
{
    var index='';
    $.each($('.cusTr'), function (i, o) {
        var t = $(o).find('input[type="hidden"][name="idPro"]').val();
       index += t + ','
    });
    $('#hdIdPro').val(index);
    var url = 'CheckOut?ProductId=' + index;
    window.location.href = url;
}
function UpdateToCart(id) {
    var url = "UpdateToCart"
    var valueQuantity = $('#txtQuantity_' + id).val();
    $.post(url,
   {
       ProductId: id,
       Quantity: valueQuantity
   },
   function (o, status) {
       console.log(o);
   });
}
function CheckCoupon() {
    $('.alert-danger').addClass('hidden');
    var hdUser = $('#hdUserName').val();
    console.log(hdUser);
    if (hdUser!=null) {
        var url = "CheckCoupon"
        var valueCoupon = $('#input-coupon').val();
        if (valueCoupon == "") {
            $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Vui lòng nhập mã giảm giá ! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

            $('html, body').animate({ scrollTop: 0 }, 'slow');
        }
        $.post(url,
       {
           coupon: valueCoupon
       },
       function (o, status) {
           console.log(o[0]);

           if (o[0] == null) {
               $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Sai mã giảm giá .Vui lòng nhập lại ! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

               $('html, body').animate({ scrollTop: 0 }, 'slow');
           }
           if (o[0] != null) {
               var dateEnd = new Date(parseInt(o[0].DateEndCode.substr(6)));
               var dateNow = new Date($.now());
               console.log(convert(dateEnd) + '/' + convert(dateNow));
               if (convert(dateEnd) < convert(dateNow)) {

                   $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Mã giảm giá hết hạn.Vui lòng nhập mã mới! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

                   $('html, body').animate({ scrollTop: 0 }, 'slow');
               }
               if (convert(dateEnd) > convert(dateNow)) {
                   alert('Mã Thành công');
               }
           }
       });
    }
    else {
        $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Vui lòng đăng nhập! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

        $('html, body').animate({ scrollTop: 0 }, 'slow');
    }

    
}
function convert(str) {
    var date = new Date(str),
        mnth = ("0" + (date.getMonth() + 1)).slice(-2),
        day = ("0" + date.getDate()).slice(-2);
    return [date.getFullYear(), mnth, day].join("-");
}