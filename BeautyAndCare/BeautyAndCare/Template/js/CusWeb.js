$(function () {
    showToCart();
        GetCity();
   
    var url = window.location.href;
   
    if (url.indexOf('/WebSite/Index')>=0) {

       
        $('body').removeAttr("class");
        $('body').attr("class", 'common-home');
    }
    if (url.indexOf('/WebSite/Login') >= 0) {

    
        $('body').removeAttr("class");
        $('body').attr("class", 'account-login');
    }
    if (url.indexOf('/WebSite/Register') >= 0) {

  
        $('body').removeAttr("class");
        $('body').attr("class", 'account-register');
    }
    if (url.indexOf('/WebSite/Contact') >= 0) {
        $('body').removeAttr("class");
        $('body').attr("class", 'information-contact');
       

    }
    if (url.indexOf('/WebSite/About') >= 0) {
        $('body').removeAttr("class");
        $('body').attr("class",'information-information-4');
     
    }
    if (url.indexOf('/WebSite/ListBlog') >= 0) {
        $('body').removeAttr("class");
        $('body').attr("class", 'simple_blog-article');
     
    } if (url.indexOf('/WebSite/DetailBlog') >= 0) {
        $('body').removeAttr("class");
        $('body').attr("class", 'simple_blog-article-view');
     
    }
    var vi = getCookie("vi");
    if (vi != null) {
        if (vi == 1) {
            $('.clvi').removeClass('hidden');
            $('.clen').addClass('hidden');
        }
        if (vi == 2) {
            $('.clvi').addClass('hidden');
            $('.clen').removeClass('hidden');
        }

    }
    else {
        setCookie("vi", 1, 365);
    }
    $('#btnVn').click(function () {
        var t = $('#btnVn').attr('rel')
        setCookie("vi", t, 365);
        $('#btnVn').addClass('selected');
        $('#btnEn').removeClass('selected');
    });
    $('#btnEn').click(function () {
        var t = $('#btnEn').attr('rel')
        setCookie("vi", t, 365);
        $('#btnEn').addClass('selected');
        $('#btnVn').removeClass('selected');
    });
    $('#btnJp').click(function () {
        var t = $('#btnJp').attr('rel')
        setCookie("vi", t, 365);
    });
    //load lan
    var url = "WebSite/GetRowLan";
    $.ajax
   ({
       type: "POST",
       url: url, //LyricsloadMore
       data: JSON.stringify({ lan: vi }),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
          
           $.each(data, function (i, o) {
               if (o.KeyVocabulary.lastIndexOf("ip_") != -1)
                   $('.' + o.KeyVocabulary).val(o.NameVocabulary);
               else if (o.KeyVocabulary.lastIndexOf("pl_") != -1)
                   $('.' + o.KeyVocabulary).attr("placeholder", o.NameVocabulary);
               else
                   $('.' + o.KeyVocabulary).html(o.NameVocabulary);

           });


       }
   });
    
   
});
$('.btnLogOut').click(function () {
    alert();
    Logout();
});
function Logout() {
    var url = "/WebSite/LogOut";
    $.ajax
   ({
       type: "POST",
       url: url,
       data: '',
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
       }
   });
}

function GetCity() {
    //Get City
    var url = "/WebSite/GetCity";
    var stringCity = "<option value=\"0\">Chọn thành phố</option>";
    
    $.ajax
   ({
       type: "POST",
       url: url,
       data: '',
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
           
           $.each(data, function (i, o) {
               stringCity += "<option value=" + o.CityId + ">" + o.CityName + "</option>";

           });
          
           $('#dropCity').removeAttr('style');
           $('#dropCity').removeAttr('sb');
           $('#dropCity').attr('class', 'form-control');
           $('#dropCity').html(stringCity);
           $('.sbHolder').remove();
           $('#dropCity').change(function () {
               var idCity = $("#dropCity option:selected").val();
               $('.cldistrictcheckout').removeClass('hidden');
               GetDistrict(idCity);
               GetPriceShip(idCity);
           });
       }
   });
}
function GetPriceShip(_id) {
   
    //Get City
    var url = "/WebSite/GetPriceShip";
    
    var obj = {};
    obj.id = _id;
    $.ajax
   ({
       type: "POST",
       url: url,
       data: JSON.stringify(obj),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
           var TotalPriceShip = 0;
           var totalPriceNo = $('#hdPriceNo').val();
           $('.showPrice').html(tien(totalPriceNo));
           if (data.length ==0 ) {
               TotalPriceShip = parseInt(totalPriceNo);
               $('.showShip').html('0đ');
           } else {
               TotalPriceShip = parseInt(totalPriceNo) + parseInt(data[0].PriceShip);
               $('.showShip').html(tien(data[0].PriceShip));
           }
           $('.showPricePay').html(tien(TotalPriceShip));
           $('.clTotalPrcie').html(tien(TotalPriceShip));
           $('#hdPriceNoPayout').val(TotalPriceShip);

       }
   });
}
function GetDistrict(_id) {
    //Get City
    var url = "/WebSite/GetDistrict";
    var stringCity = "<option value=\"0\">Chọn quận huyện</option>";
    $('#dropDistrict').removeAttr('style');
    $('#dropDistrict').removeAttr('sb');
    $('#dropDistrict').attr('class', 'form-control');
   
    $('.sbHolder').remove();
    var obj={};
    obj.id = _id;
    $('#dropDistrict').html('');
    $.ajax
   ({
       type: "POST",
       url: url,
       data: JSON.stringify(obj),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
         
           $.each(data, function (i, o) {
             
               stringCity += "<option value=" + o.DistId + ">" + o.DistName + "</option>";

           });

           $('#dropDistrict').html(stringCity);
           $('#dropDistrict').change(function () {
               $('.clwardcheckout').removeClass('hidden');
               var idDistric = $("#dropDistrict option:selected").val();
               GetWard(idDistric);
           });
       }
   });
}
function GetWard(_id) {
    //Get City
    var url = "/WebSite/GetWard";
    var stringCity = "<option value=\"0\">Chọn phường xã</option>";
    $('#dropWard').removeAttr('style');
    $('#dropWard').removeAttr('sb');
    $('#dropWard').attr('class', 'form-control');
    $('.sbHolder').remove();
    var obj={};
    obj.id = _id;
    $('#dropWard').html('');
    $.ajax
   ({
       type: "POST",
       url: url,
       data: JSON.stringify(obj),
       dataType: "json",
       contentType: "application/json;charset=utf-8",
       success: function (data) {
         
           $.each(data, function (i, o) {
               stringCity += "<option value=" + o.WardId + ">" + o.WardName + "</option>";
           });

           $('#dropWard').html(stringCity);
       }
   });
}
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
(function ($) {


    $(window).load(function () {
        var o = $('.sf-menu');
        o.superfish();
        o.find('li a').each(function () {
            if ($(location).attr('href').indexOf($(this).attr('href')) >= 0) {
                $(this).addClass('active');
                return;
            }
        })
        if (o.parents('aside').length) {
            var width = $('.container').outerWidth() - $('aside').outerWidth();
            o.find('.sf-mega').each(function () {
                $(this).width(width);
            })
        }
    });
})(jQuery);
$(document).ready(function () {

    var total = 0;
    $.each($('.price-total'), function (i, o) {
        var t = $(o).find('input[type="hidden"][name="hdtotal"]').val();
        total += parseInt(t)

    });
    $('.tx-price').html(tien(total));
    $('.priceHd').html(total);
    $('#hdPriceNo').val(total);
});
///////////////SubMit
$('#button-coupon').click(function () {
    AddCoupon();
});
//////////////
function addToCart(id) {
    $('.alert-success').addClass('hidden');
    var url = "WebSite/AddToCart"
    $.post(url,
   {
       ProductId: id
   },
   function (o, status) {
       //$('#cart-total2').html(o.count);
       //var stringHtml;
       //stringHtml += "<tr class=\"cusTr\">"
       //+ "<td class=\"text-center\">"
       //+ " <div class=\"image\">"
       //  + " <input type=\"hidden\" name=\"idPro\" id=\"hdProductsId_" + o.dataPro.IdProducts + "\" value=\"" + o.dataPro.IdProducts + "\" />"
       //+ " <a href=\"#\">"
       //+ "   <img src=\"" + o.nameImg + "\" alt=\"" + o.dataPro.NameProducts + "\" title=\"" + o.dataPro.NameProducts + "\" class=\"img-thumbnail\">"
       //+ "  </a>"
       //+ " </div>"
       //+ "  </td>"
       //+ " <td class=\"text-left\">"
       //+ " <div class=\"name\">"
       //+ " <a href=\"#\">" + o.dataPro.NameProducts + "</a>"
       //+ " </div>"
       //+ " </td>"
       //+ "<td class=\"text-right\">"
       //+ " <span class=\"price-cart PriceCart_" + o.dataPro.IdProducts + "\">" + o.dataPro.PriceNewProducts + "</span>"
       //+ " </td>"
       //+ " </tr>";
       //$('.LoadProducts').append(stringHtml);

       showToCart();
       $('#content').parent().before('<div class="alert alert-success"><i class="material-design-verification24"></i> Bạn đã thêm sản phẩm ' + o.dataPro.NameProducts + ' vào giỏ hàng thành công <button type="button" class="close material-design-close47"></button></div>');
       $('.PriceCart_' + o.dataPro.IdProducts).text(tien(o.dataPro.PriceProducts));
   });

}
function showToCart() {
    var url = "WebSite/ShowToCart"
    $('.LoadProducts').html('');
    $.post(url,
   function (o, status) {
       $('#cart-total2').html(o.count);
       console.log(o);
       var stringHtml;
     
       $.each(o.dataPro, function (i, t) {
           console.log(t);
           stringHtml += "<tr class=\"cusTr\">"
             + "<td class=\"text-center\">"
       + " <div class=\"image\">"
         + " <input type=\"hidden\" name=\"idPro\" id=\"hdProductsId_" + t.IdProducts + "\" value=\"" + t.IdProducts + "\" />"
       + " <a href=\"#\">"
       + "   <img src=\"" + t.FullUrlImg2 + "\" alt=\"" + t.ProductName + "\" title=\"" + t.ProductName + "\" class=\"img-thumbnail\">"
       + "  </a>"
       + " </div>"
       + "  </td>"
       + " <td class=\"text-left\">"
       + " <div class=\"name\">"
       + " <a href=\"#\">" + t.ProductName + "</a>"
       + " </div>"
       + " </td>"
       + "<td class=\"text-right\">"
       + " <span class=\"price-cart PriceCart_" + t.IdProducts + "\">" + tien(t.Price) + "</span>"
       + " </td>"
            + " </tr>";
       });
     


      
       $('.LoadProducts').append(stringHtml);
   });
}

function ViewCart() {
    var index = '';
    $.each($('.cusTr'), function (i, o) {
        var t = $(o).find('input[type="hidden"][name="idPro"]').val();
        index += t + ','
    });
    $('#hdIdPro').val(index);
    var url = 'CheckOut?ProductId=' + index;
    window.location.href = url;
}
function UpdateToCart(id) {
    var url = "WebSite/UpdateToCart"
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
    if (hdUser != null) {
        var url = "WebSite/CheckCoupon"
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
           if (o[0] == null) {
               $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Sai mã giảm giá .Vui lòng nhập lại ! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

               $('html, body').animate({ scrollTop: 0 }, 'slow');
           }
           if (o[0] != null) {
               var dateEnd = new Date(parseInt(o[0].DateEndCode.substr(6)));
               var dateNow = new Date($.now());
               if (convert(dateEnd) < convert(dateNow)) {

                   $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Mã giảm giá hết hạn.Vui lòng nhập mã mới! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

                   $('html, body').animate({ scrollTop: 0 }, 'slow');
               }
               if (convert(dateEnd) > convert(dateNow)) {
                   $('#hdValueCoupon').val(o[0].PriceCode);
                   var showSubTotal = $('.priceHd').html();

                   if (showSubTotal >= 300000) {
                       var cal = showSubTotal - o[0].PriceCode;
                      
                       $('.showTotalAll').html(tien(cal));
                       $('#hdPriceNo').val(cal);
                      
                       $('#button-coupon').addClass('disabled')
                      
                       $('.breadcrumb').after('<div class="alert alert-success"><i class="fa fa-exclamation-circle"></i> Bạn đã nhập mã giảm giá thành công! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

                   $('html, body').animate({ scrollTop: 0 }, 'slow');
               } else {
                       $('.breadcrumb').after('<div class="alert alert-danger"><i class="fa fa-exclamation-circle"></i>Thông báo: Bạn không đủ điều xử dụng mã giảm giá! <button type="button" class="close" data-dismiss="alert">&times;</button></div>');

                       $('html, body').animate({ scrollTop: 0 }, 'slow');
                   }


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

