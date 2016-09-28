        if ($('#BNC-slider-price').length) {
            $('#BNC-slider-price').noUiSlider({
                start: [100, 5000],
                connect: true,
                behaviour: 'drag',
                //snap: true,
                range: {
                    'min': [2000],
                    'max': [10000]
                },
                format: wNumb({
                    decimals: 3,
                    thousand: '.',
                    postfix: ' (VND)',
                })
            });
            $("#BNC-slider-price").Link('lower').to('-inline-<div class="tooltip"></div>', function(value) {
                $(this).html(
                    '<span>' + value + '</span>' +
                    '<input id="BNC_gia_tu" type="hidden" value="' + value + '">'
                );
            });
            $("#BNC-slider-price").Link('upper').to('-inline-<div class="tooltip"></div>', function(value) {
                $(this).html(
                    '<span>' + value + '</span>' +
                    '<input id="BNC_gia_den" type="hidden" value="' + value + '">'
                );
            });
        }
         // $('#BNC-slider-price').Link('lower').to($('#BNC-slider-price-value-lower'));
         // $('#BNC-slider-price').Link('upper').to($('#BNC-slider-price-value-upper'));
         //
         // Replaces all instances of the given substring.
        String.prototype.replaceNXT = function(
            strTarget, // The substring you want to replace
            strSubString // The string you want to replace in.
        ) {
            var strText = this;
            var intIndexOfMatch = strText.indexOf(strTarget);
            // Keep looping while an instance of the target string
            // still exists in the string.
            while (intIndexOfMatch != -1) {
                // Relace out the current instance.
                strText = strText.replace(strTarget, strSubString)
                // Get the index of any next matching substring.
                intIndexOfMatch = strText.indexOf(strTarget);
            }
            // Return the updated string with ALL the target strings
            // replaced out with the new substring.
            return (strText);
        }

        function replaceAll(find, replace, str) {
            return str.replaceNXT(find, replace);
        }

        function ajax_global(dataString, urlSend, method, type) {
            var res = '';
            $.ajax({
                url: $('base').attr('href') + urlSend,
                type: method,
                async: false,
                dataType: type,
                data: dataString,
            }).success(function(res) {
                result = res;
            }).error(function(error) {
                console.log(error);
            });
            return result;
        }

        function array2json(arr) {
            var parts = [];
            var is_list = (Object.prototype.toString.apply(arr) === '[object Array]');
            for (var key in arr) {
                var value = arr[key];
                if (typeof value == "object") { //Custom handling for arrays
                    if (is_list) parts.push(array2json(value)); /* :RECURSION: */
                    else parts.push('"' + key + '":' + array2json(value)); /* :RECURSION: */
                    //else parts[key] = array2json(value); /* :RECURSION: */
                } else {
                    var str = "";
                    if (!is_list) str = '"' + key + '":';
                    //Custom handling for multiple data types
                    if (typeof value == "number") str += value; //Numbers
                    else if (value === false) str += 'false'; //The booleans
                    else if (value === true) str += 'true';
                    else str += '"' + value + '"'; //All other things
                    // :TODO: Is there any more datatype we should be in the lookout for? (Functions?)
                    parts.push(str);
                }
            }
            var json = parts.join(",");
            if (is_list) return '[' + json + ']'; //Return numerical JSON
            return '{' + json + '}'; //Return associative JSON
        }
        var Product = function() {
            var ajaxSearchProduct = function() {
                $(document).on('click', '.BNC-search-product', function(event) {
                    event.preventDefault();
                    $("#product-search").submit();
                });
            }
            var ajaxTab = function() {
                $('.product-tab').click(function() {
                    var type = $(this).attr('data-type');
                    var url_ajax = $('body').data('home_url') + '/product-ajaxHome-ajaxTab' + $('body').data('extension');
                    var data = {
                        'type': type
                    };
                    $(this).parents('ul').find('li').removeClass('active');
                    $(this).parent().addClass('active');
                    $.post(url_ajax, data, function(response) {
                        $('#product-tab').html(response);
                    });
                });
            }
            var ajaxCategory = function() {
                    $('body').on('click', '.product-category', function(event) {
                        event.preventDefault();
                        if ($(this).attr('data-ajax') != 0) {
                            loadding('load');
                            var cate_id = $(this).attr('data-id');
                            var block_id = $(this).attr('data-block');
                            var url_ajax = $('body').data('home_url') + '/product-ajaxHome-ajaxCategory' + $('body').data('extension');
                            var data = {
                                'cate_id': cate_id,
                                'block_id': block_id,
                            };
                            $(this).parents('ul').find('li').removeClass('active');
                            $(this).parent().addClass('active');
                            $('#product-category-' + block_id).find('li').removeClass('active');
                            $('#product-category-' + block_id).find('li:eq(0)').addClass('active');
                            $('#product-category-' + block_id).find('li a').attr('data-id', cate_id);
                            $.post(url_ajax, data, function(response) {
                                $('#product-content-' + block_id).html(response);
                                loadding('remove');
                            });
                            var dataHref = $(this).attr('href');
                            var parent = $(this).parents('.v2-home-catepr');
                            parent.find('.view-more-l').attr('href', dataHref);
                            return false;
                        } else {
                            var dataHref = $(this).attr('href');
                            window.location.href = dataHref;
                        }

                    });

                }
                // var ajaxCategoryTab = function() {
                //     $('body').on('click','.product-category-tab',function() {
                //         var type = $(this).attr('data-type');
                //         var cate_id = $(this).attr('data-id');
                //         var block_id = $(this).attr('data-block');
                //         var url_ajax = $('body').data('home_url')+'/product-ajaxHome-ajaxCategoryTab'+$('body').data('extension');
                //         var data = {
                //             'type' : type,
                //             'cate_id' : cate_id
                //         };
                //         $(this).parents('ul').find('li').removeClass('active');
                //         $(this).parent().addClass('active');
                //         $.post(url_ajax, data, function(response) {
                //             $('#product-content-'+block_id).html(response);
                //         });
                //     });
                // }
            var filterProduct = function() {
                    $('#sort_filter').change(function() {
                        var sort = $(this).val();
                        var params = $('#params').val();
                        var url_ajax = $('body').data('home_url') + '/product-ajaxProduct-ajaxFilter' + $('body').data('extension');
                        var data = {
                            'sort': sort,
                            'params': params
                        };
                        $.post(url_ajax, data, function(response) {
                            location.href = response;
                        });
                    });
                    $('#limit_filter').change(function() {
                        var limit = $(this).val();
                        var params = $('#params').val();
                        var url_ajax = $('body').data('home_url') + '/product-ajaxProduct-ajaxFilter' + $('body').data('extension');
                        var data = {
                            'limit': limit,
                            'params': params
                        };
                        $.post(url_ajax, data, function(response) {
                            location.href = response;
                        });
                    });
                }
                // cart
            var selectShop = function() {
                // case with size
                $('body').on('change', '#select-shop-size', function() {
                    var idshop = $(this).val();
                    if (idshop != "") {
                        $('.f-pr-view-box-size').show();
                        $('.select-shop-size').hide();
                        $('#shop-size-' + idshop).show();
                    } else {
                        $('.f-pr-view-box-size').hide();
                        $('.select-shop-size').hide();
                    }
                });
                // case no size
                $('body').on('change', '#select-shop', function() {
                    var idshop = $(this).val();
                    if (idshop != "") {
                        $('.f-pr-view-box-size').show();
                        $('.select-shop').hide();
                        $('#shop-' + idshop).show();
                    } else {
                        $('.f-pr-view-box-size').hide();
                        $('.select-shop').hide();
                    }
                });
                $('body').on('change', '#size', function() {
                    var size_name = $(this).val();
                    $('#quantity').show();
                    $('.quantity').hide();
                    $('#quantity-' + size_name).show();
                    $('.BNC-add-cart').attr('data-size', size_name);
                });
            }
            var popoper = function() {
                $('[data-toggle="popover"]').popover()
            }
            var miniCart = function() {
                $('body').on('click', ".miniv2-toolbar-name", function() {
                    if ($(".f-miniCart-miniv2").attr("data-status") == 'hide') {
                        $(".f-miniCart-miniv2").animate({
                            right: "0"
                        }, 500, function() {
                            $(".miniv2-toolbar-close").css("visibility", "visible");
                            $(".f-miniCart-miniv2").attr("data-status", "show");
                        });
                    } else {
                        $(".f-miniCart-miniv2").animate({
                            right: "-250px"
                        }, 500, function() {
                            $(".miniv2-toolbar-close").css("visibility", "hidden");
                            $(".f-miniCart-miniv2").attr("data-status", "hide");
                        });
                    };
                });
                $('body').on('click', ".miniv2-toolbar-close", function() {
                    $(".f-miniCart-miniv2").animate({
                        right: "-250px"
                    }, 500, function() {
                        $(".miniv2-toolbar-close").css("visibility", "hidden");
                        $(".f-miniCart-miniv2").attr("data-status", "hide");
                    });
                });
                // case with size
                $('body').on('click', ".add-cart", function() {

                    var idProduct = $(this).attr("data-product");
                    var idShop = $(this).attr("data-shop");
                    var idSize = $(this).attr("data-size");
                    var quantity = $('select[name="ProductQuantity"] option:selected').val();
                    // if (quantity == undefined) {
                    //     var quantity = $('#select-quantity-' + idSize + '-' + idProduct).val();
                    //     if (quantity == undefined) {
                    //         var quantity = $('#select-quantity-' + idProduct).val();
                    //     }
                    // }
                    //console.log(quantity);
                    var status = $(".f-miniCart-miniv2").attr('data-status');
                    var url_ajax = $('body').data('home_url') + '/product-ajaxCart-ajaxAddCart' + $('body').data('extension');
                    var data = {
                        'idProduct': idProduct,
                        'idShop': idShop,
                        'idSize': idSize,
                        'quantity': quantity,
                        'sizeName': $('.paymentSize span.active').text(),
                        'colorName': $('.paymentColor span.active').text(),
                        'sizeId': $('.paymentSize span.active').attr('data-id'),
                        'colorId': $('.paymentColor span.active').attr('data-id'),
                    };
                    $.post(url_ajax, data, function(response) {
                        $(".f-miniCart-miniv2").replaceWith(response);
                        if (status == 'hide') {
                            $(".f-miniCart-miniv2").css('z-index', '10000');
                            $(".f-miniCart-miniv2").animate({
                                right: "0px"
                            }, 500, function() {
                                $(".miniv2-toolbar-close").css("visibility", "visible");
                                $(".f-miniCart-miniv2").attr("data-status", "show");
                                Product.flyToCart(idProduct);
                            });
                        } else {
                            $(".f-miniCart-miniv2").css('z-index', '10000');
                            $(".miniv2-toolbar-close").css("visibility", "visible");
                            $(".f-miniCart-miniv2").attr("data-status", "show");
                            $(".f-miniCart-miniv2").css("right", "0px");
                            Product.flyToCart(idProduct);
                        }
                    });
                });
                // case with size
                $('body').on('click', ".BNC-add-cart", function() {
                    var idProduct = $(this).attr("data-product");
                    var idShop = $(this).attr("data-shop");
                    var idSize = $(this).attr("data-size");
                    //3
                    var quantity = $('input.BNC-quantity-' + idProduct).val();
                    //0
                    if (quantity == undefined) {
                        var quantity = $('select.BNC-quantity-' + idProduct + ' option:selected').val();
                    }

                    //1
                    if (quantity == undefined) {
                        var quantity = $(this).attr('data-quantity');
                    }
                    //2
                    if (quantity == undefined) {
                        var quantity = $('.BNC-quantity-select-' + idSize + '-' + idProduct).val();
                    }



                    var price_pro = $('input[name="price"]:checked').val();

                    if (price_pro == undefined) {
                        price_pro = false;
                    } else {
                        price_pro = 'key' + price_pro;
                    }

                    // if(idSize!=undefined){
                    //     var quantity = $('.BNC-quantity-'+idProduct).val();
                    // }else{
                    //     var quantity = $('.BNC-quantity-select-'+idSize+'-'+idProduct).val();
                    // }
                    // //Lay du lieu cua quantity
                    // if(quantity==undefined){
                    //     var quantity=$(this).attr('data-quantity');
                    // }
                    var status = $(".f-miniCart-miniv2").attr('data-status');
                    var url_ajax = $('body').data('home_url') + '/product-ajaxCart-ajaxAddCart' + $('body').data('extension');
                    if (quantity != '') {
                        var data = {
                            'idProduct': idProduct,
                            'idShop': idShop,
                            'idSize': idSize,
                            'quantity': quantity,
                            'price_pro': price_pro
                        };
                        $.post(url_ajax, data, function(response) {
                            $(".f-miniCart-miniv2").replaceWith(response);
                            if (status == 'hide') {
                                $(".f-miniCart-miniv2").css('z-index', '10000');
                                $(".f-miniCart-miniv2").animate({
                                    right: "0px"
                                }, 500, function() {
                                    $(".miniv2-toolbar-close").css("visibility", "visible");
                                    $(".f-miniCart-miniv2").attr("data-status", "show");
                                    Product.flyToCart(idProduct);
                                });
                            } else {
                                $(".f-miniCart-miniv2").css('z-index', '10000');
                                $(".miniv2-toolbar-close").css("visibility", "visible");
                                $(".f-miniCart-miniv2").attr("data-status", "show");
                                $(".f-miniCart-miniv2").css("right", "0px");
                                Product.flyToCart(idProduct);
                            }
                        });
                    } else {
                        alert("Vui long chon so luong");
                        return false;
                    }
                });
                // case no size
                $('body').on('click', "#add-cart", function() {
                    var idProduct = $(this).attr("data-product");
                    var idShop = $('#select-shop').val();

                    var quantity = $('select[name="ProductQuantity"] option:selected').val();
                    if (quantity == false || quantity == undefined) {
                        quantity = 1;
                    }
                    var nameProduct = $(this).attr('data-name');
                    var priceFloatProduct = $(this).attr('data-price-float');
                    var status = $(".f-miniCart-miniv2").attr('data-status');
                    //Xử lý giá khi lựa chọn giá theo thuộc tính
                    var price_pro = $('input[name="price"]:checked').val();
                    if (price_pro == undefined) {
                        price_pro = false;
                    } else {
                        price_pro = 'key' + price_pro;
                    }
                    if (idShop != "") {
                        var url_ajax = $('body').data('home_url') + '/product-ajaxCart-ajaxAddCart' + $('body').data('extension');
                        var data = {
                            'idProduct': idProduct,
                            'idShop': idShop,
                            'quantity': quantity,
                            'sizeName': $('.paymentSize span.active').text(),
                            'colorName': $('.paymentColor span.active').text(),
                            'sizeId': $('.paymentSize span.active').attr('data-id'),
                            'colorId': $('.paymentColor span.active').attr('data-id'),
                            'price_pro': price_pro,
                        };
                        $.post(url_ajax, data, function(response) {
                            $(".f-miniCart-miniv2").replaceWith(response);
                            if (status == 'hide') {
                                $(".f-miniCart-miniv2").animate({
                                    right: "0px"
                                }, 500, function() {
                                    $(".miniv2-toolbar-close").css("visibility", "visible");
                                    $(".f-miniCart-miniv2").attr("data-status", "show");
                                    Product.flyToCart(idProduct);
                                });
                            } else {
                                $(".miniv2-toolbar-close").css("visibility", "visible");
                                $(".f-miniCart-miniv2").attr("data-status", "show");
                                $(".f-miniCart-miniv2").css("right", "0px");
                                Product.flyToCart(idProduct);
                            }
                        });

                        //Add Analytics
                        dataLayer.push({
                            'event': 'addToCart',
                            'ecommerce': {
                                'currencyCode': 'VND',
                                'add': {
                                    'products': [{
                                        'id': idProduct, // Mã sp
                                        'name': nameProduct,
                                        'price': priceFloatProduct, // String Float. vd ""10000.00""
                                        'category': 'Mẹ và bé' // Tên Category
                                    }]
                                }
                            }
                        });

                    } else {
                        alert("Bạn chưa chọn cửa hàng và số lượng sản phẩm!!!");
                        return false;
                    }
                });
                // remove product from cart
                $('body').on('click', '.removeCartItem', function() {
                    var idProduct = $(this).attr("data-product");
                    var idSize = $(this).attr("data-size");
                    var url_ajax = $('body').data('home_url') + '/product-ajaxCart-ajaxRemoveCart' + $('body').data('extension');
                    var data = {
                        'idProduct': idProduct,
                        'idSize': idSize
                    };
                    $.post(url_ajax, data, function(response) {
                        $(".f-miniCart-miniv2").replaceWith(response);
                        $(".miniv2-toolbar-close").css("visibility", "visible");
                        $(".f-miniCart-miniv2").attr("data-status", "show");
                        $(".f-miniCart-miniv2").css("right", "0px");
                        if ($('.miniCartItem').find('li').length == 0) {
                            $('.miniv2-toolbar-close').trigger('click');
                        }
                    });
                });
            }
            var sizeProduct = function() {
                $('body').on('click', '.BNC-size', function(event) {
                    event.preventDefault();
                    var size_name = $(this).attr('data-name-size');
                    var id_product = $(this).attr('data-id-product');
                    $(this).css('border', 'rgb(66, 87, 106) solid 1px');
                    $(this).css('border', 'rgb(255, 0, 0) solid 1px');
                    //An het
                    $('.BNC-quantity-' + id_product).hide();
                    //Show select
                    $('.BNC-quantity-select-' + size_name + '-' + id_product).show();
                    $('.BNC-add-cart-' + id_product).attr('data-size', size_name).prop('disabled', false);
                    return false;
                });
            }
            var paymentCart = function() {
                $('body').on('click', '#payment', function(event) {
                    event.preventDefault();
                    var href = $(this).attr('href');
                    href = href.replace('payment.html', 'payment-oncepage.html');
                    var url_ajax = $('body').data('home_url') + '/product-ajaxCart-ajaxSaveCart' + $('body').data('extension');
                    $.post(url_ajax, '', function(response) {
                        location.href = href;
                        // console.log(response);
                        // console.log(123);
                    });
                    // console.log(url_ajax);
                    // console.log(href);
                    return false;
                });
            }
            var more_shop = function() {
                $('body').on('click', '#more-shop', function() {
                    if ($('#shop-content').attr('data-is-more') == 0) {
                        $('#shop-content');
                        $('#shop-content').attr('data-is-more', 1).css('overflow', 'auto').css('height', 'auto');
                        $(this).text('Thu gọn')
                    } else {
                        $('#shop-content').attr('data-is-more', 0).css('overflow', 'hidden').css('height', '127px');
                        $(this).text('Xem thêm')
                    }
                });
            }
            var select_size = function() {
                $('body').on('click', '.size', function(e) {
                    e.preventDefault();
                    $('body').find('.size').css({
                        'border': 'rgb(194, 209, 223) solid 1px'
                    });
                    $(this).css({
                        'border': 'rgb(255, 0, 0) solid 1px'
                    });
                    var size_name = $(this).attr('data-name-size');
                    $('#quantity').show();
                    $('.quantity').hide();
                    $('#quantity-' + size_name).show();
                    $('.BNC-add-cart').attr('data-size', size_name);
                });
            }
            var handleAjaxShowMoreItemCategory = function() {
                $('body').on('click', '.NXT_click_ajax_more', function(event) {
                    event.preventDefault();
                    $('body').find('.NXT_ul_cat').find('.first_cat').removeClass('first_cat');
                    $(this).addClass('first_cat');
                    var url_ajax = $('body').data('home_url') + '/product-ajaxHome-ajaxCategoryTabMoreShowAll' + $('body').data('extension');
                    var data = {
                        'author': 'NXT',
                        'cat_id': $(this).attr('data-id')
                    };
                    $.post(url_ajax, data, function(response) {
                        $('.NXT_content_ajax_more_cat').removeClass('hide');
                        $('.NXT_content_ajax_more_cat').html(response);
                    });
                });
            }
            var handleAuction = function() {
                $('body').on('click', '.BNC_auction', function() {
                    var id_product = $(this).attr('data-product-id');
                    var dataString = {
                        'id_product': id_product
                    };
                    $.ajax({
                        url: $('body').data('home_url') + '/product-ajaxAuction-auction' + $('body').data('extension'),
                        type: 'POST',
                        dataType: 'json',
                        data: dataString,
                    })
                        .success(function(res) {
                            if (res.status == false) {
                                toastr.error(res.message);
                                return false;
                            } else if (res.status == true) {
                                $('.BNC_notify_auction').html(res.message);
                                $('.BNC_notify_auction').attr('data-type', res.type);
                                $('.BNC_notify_auction').attr('data-price', res.price);
                                $('#modealAuction').modal('show');
                            }
                            console.log(res);
                        })
                        .done(function() {
                            console.log("success");
                        })
                        .fail(function() {
                            console.log("error");
                        })
                        .always(function() {
                            console.log("complete");
                        });
                });
                $('body').on('keyup', 'input[name="price"]', function() {
                    var price = $(this).val();
                    $(this).val(numberFormat(price, 0, '.', '.'));
                });
                $('body').on('click', '.BNC_auction_send', function() {
                    var price = $('input[name="price"]').val();
                    price = replaceAll('.', '', price);
                    var priceD = $('.BNC_notify_auction').attr('data-price');
                    var type = $('.BNC_notify_auction').attr('data-type');
                    if (type == 1 && price < priceD) {
                        toastr.error('Bạn cần phải trả giá cao hơn giá ban đầu');
                        $('input[name="price"]').focus();
                        return false;
                    } else if (type == 2 && price > priceD) {
                        toastr.error('Bạn phải trả giá nhỏ hơn hoặc bằng với giá ban đầu');
                        $('input[name="price"]').focus();
                        return false;
                    }
                    var dataString = {
                        price: price,
                        id_product: $('.BNC_auction').attr('data-product-id')
                    };
                    var urlSend = '/product-ajaxAuction-SendAuction' + $('body').data('extension');
                    var data = ajax_global(dataString, urlSend, 'POST', 'json');
                    if (data.status == true) {
                        $('#modealAuction').modal('hide');
                        toastr.success(data.message);
                        $('input[name="price"]').val('');
                    } else {
                        $('#modealAuction').modal('hide');
                        toastr.error(data.message);
                        $('input[name="price"]').val('');
                    }

                });
            }
            var numberFormat = function(number, decimals, dec_point, thousands_sep) {
                number = (number + '').replace(/[^0-9]/g, '');
                var n = !isFinite(+number) ? 0 : +number,
                    prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
                    sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
                    dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
                    s = '',
                    toFixedFix = function(n, prec) {
                        var k = Math.pow(10, prec);
                        return '' + Math.round(n * k) / k;
                    };
                // Fix for IE parseFloat(0.55).toFixed(0) = 0;
                s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
                if (s[0].length > 3) {
                    s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
                }
                if ((s[1] || '').length < prec) {
                    s[1] = s[1] || '';
                    s[1] += new Array(prec - s[1].length + 1).join('0');
                }
                return s.join(dec);
            }
            return {
                init: function() {
                    ajaxTab();
                    ajaxCategory();
                    //ajaxCategoryTab();
                    filterProduct(); // page product
                    selectShop(); // cart
                    miniCart(); // cart mini
                    paymentCart();
                    more_shop();
                    select_size();
                    sizeProduct();
                    popoper();
                    ajaxSearchProduct();
                    handleAjaxShowMoreItemCategory();
                    handleAuction();
                    //handleCompare();
                    //handleOnloadCompare();
                },
                flyToCart: function(id_product) {
                    var image = $(".BNC-image-add-cart-" + id_product);
                    var currentCount = $(".miniv2-toolbar-count").text();
                    var cartToolBar = $(".miniv2-toolbar-barclick");
                    var cartPosition = cartToolBar.offset();
                    var cartPositionTop = cartPosition.top - $(window).scrollTop();
                    var cartPositionLeft = cartPosition.left - $(window).scrollLeft();
                    var eTop = image.offset().top;
                    var eLeft = image.offset().left;
                    var imagePositionTop = eTop - $(window).scrollTop();
                    var imagePositionLeft = eLeft - $(window).scrollLeft();
                    var newImage = "<img style='top:" + imagePositionTop + "px;left:" + imagePositionLeft + "px' class='moveimg' src=" + image.attr("src") + " />";
                    $("body").append(newImage);
                    $(".moveimg").animate({
                        top: cartPositionTop,
                        left: cartPositionLeft,
                        height: "118px",
                        width: "35px",
                        opacity: 0.5,
                    }, 1200, function() {
                        $(this).remove();
                    });
                }
            };
        }();

        function analyticsClick(myObject) {

            var id = $(myObject).attr('data-id');
            var name = $(myObject).attr('data-name');
            var price = $(myObject).attr('data-price-float');
            var list = $(myObject).attr('data-title');
            var position = $(myObject).attr('data-position');

            dataLayer.push({
                'event': 'product_click',
                'ecommerce': {
                    'click': {
                        'actionField': {
                            'list': list
                        }, // Vi tri CLick
                        'products': [{
                            'id': id, // ID sản phẩm
                            'name': name, // Tên sản phẩm
                            'price': price, // Giá
                            'list': list, // Trang danh mục
                            'position': position, // Int vị trí hiển thị
                        }]
                    }
                },
            });
        }



        $(document).ready(function() {
            $('body').on('click', '.view_maps', function() {
                return false;
            });

            //Change PC
            $('body').on('click', '.mobile_to_pc', function(event) {
                event.preventDefault();
                var cook = $.cookie('mobile_to_pc');
                if (cook != undefined) {
                    $.cookie('mobile_to_pc', '1', {
                        path: '/',
                        expires: -1
                    });
                    window.location.reload();
                    return;
                } else {
                    $.cookie('mobile_to_pc', '1', {
                        path: '/'
                    });
                    window.location.reload();
                    return;
                }
            });

        });