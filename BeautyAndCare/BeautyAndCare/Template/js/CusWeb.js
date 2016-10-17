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
});

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