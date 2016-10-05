var gl_path = jQuery('#gl_path').html();
function include(scriptUrl) {
    document.write('<script src="/Template/' + scriptUrl + '"><\/script>');
   console.log(scriptUrl)
}


/**
* @function      isIE
* @description   checks if browser is an IE
* @returns       {number} IE Version
*/
function isIE() {
	var myNav = navigator.userAgent.toLowerCase();
	return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
};

;(function ($) {
	if (isIE() && isIE() < 11) {
		$('html').addClass('lt-ie11');
	}
})(jQuery);


/* jquery easing lib
========================================================*/
;
(function ($) {
	include('js/jquery.easing.1.3.js');
})(jQuery);


/* Unveil
========================================================*/
;
(function ($) {
	var o = $('.lazy img');
	if (o.length > 0) {
		include('js/jquery.unveil.min.js');
		$(document).ready(function () {
			$(o).unveil(0, function () {
				$(this).load(function () {
					$(this).parent().addClass("lazy-loaded");
				});
			});
		});
		$(window).load(function () {
			$(window).trigger('lookup.unveil');
			if ($('.nav-tabs').length) {
				$('.nav-tabs').find('a[data-toggle="tab"]').click(function () {
					setTimeout(function () {
						$(window).trigger('lookup.unveil');
					}, 400);
				});
			}
		});
	}
})(jQuery);

/*----------------------------------*/


/**
* @module       ToTop
* @description  Enables ToTop Plugin
*/
;
(function ($) {
	var o = $('html');
	if (o.hasClass('desktop')) {
		include('js/jquery.ui.totop.min.js');
		$(document).ready(function () {
			$().UItoTop({
				easingType: 'easeOutQuart',
				containerClass: 'ui-to-top fa fa-angle-up'
			});
		});
	}
})(jQuery);


/* banner
========================================================*/

$('.common-home #content .blog_articles .col-sm-4:nth-child(3n+1), .common-home #content .blog_articles .col-sm-4:nth-child(3n+2)').wrapAll('<div class="col-sm-6">');

/* megamenu line
===================================*/

function setEqualHeight(columns) {
	var tallestcolumn = 0;
	columns.each(
		function() {
			currentHeight = $(this).parent().parent().height()-8;
			console.log(currentHeight);
			if(currentHeight > tallestcolumn)
				{ tallestcolumn = currentHeight; }
		}
	);
	columns.height(tallestcolumn);
}
$(document).ready(function() {
	setEqualHeight($(".sf-mega > li > .sf-mega_section"));
});

/* Search 
===================================*/
	
jQuery(document).ready(function(){
	
   $('.top-search').on("click", function(){		
		$('.search').toggleClass('active');		
   });   
	
	$(document).on("click touchstart", function(e){
		var container = $(".search");
		if (!container.is(e.target) && container.has(e.target).length === 0 && container.hasClass('active')) { 
			container.toggleClass('active');
		}
	});
	
 
});

/* Stick up menus
========================================================*/
;(function ($) {
	var o = $('html');
	var menu = $('#stuck');
	if (o.hasClass('desktop') && menu.length) {
		include('js/scrollfix.min.js');
		$(window).load(function () {
			menu.scrollFix({
				style: false
			});
		});
	}
})(jQuery);


/* FancyBox
========================================================*/
;(function ($) {
	var o = $('.quickview');
	var o2 = $('#default_gallery');
	if (o.length > 0 || o2.length > 0) {
		include('js/jquery.fancybox.js');
	}
	if (o.length) {
		$(document).ready(function () {
			o.fancybox({
				maxWidth: 800,
				maxHeight: 600,
				fitToView: false,
				width: '70%',
				height: '70%',
				autoSize: false,
				closeClick: false,
				openEffect: 'elastic',
				closeEffect: 'elastic',
				speedIn: 600,
				speedOut: 600
			});
		});
	}
})(jQuery);


/* Toggle
========================================================*/
;
(function ($) {
	var o = $('.toggle');
	$(document).ready(function () {
		$('.toggle').click(function (e) {
			e.preventDefault();
			var tmp = $(this);
			o.each(function () {
				if ($(this).hasClass('active') && !$(this).is(tmp)) {
					$(this).parent().find('.toggle_cont').slideToggle();
					$(this).removeClass('active');
				}
			});
			$(this).toggleClass('active');
			$(this).parent().find('.toggle_cont').slideToggle();
		});
		$(document).on('click touchstart', function (e) {
			var container = $(".toggle-wrap");
			if (!container.is(e.target) && container.has(e.target).length === 0 && container.find('.toggle').hasClass('active')) { 
				container.find('.active').toggleClass('active').parent().find('.toggle_cont').slideToggle();
			}
		});
	});
})(jQuery);

;
(function ($) {
	$(document).ready(function () {
		$(document).on('click', '.btn-close-cart', function(){
			$(this).parent().parent().parent().find('.toggle_cont').slideToggle();
			$(this).parent().parent().parent().find('button.toggle').toggleClass('active');
		});
	});
})(jQuery);


/* Owl Carousel
========================================================*/
;
(function ($) {
	var o1 = $('.testimonials'),
	o2 = $('.box-carousel');
	if (o1.length > 0 || o2.length > 0) {
		include('js/owl.carousel.min.js');
		var settings = {
			nav: true,
			navClass: ['owl-prev fa fa-angle-left', 'owl-next fa fa-angle-right'],
			navText: ['', ''],
			rtl: $('html').attr('dir') == 'rtl' ? true : undefined
		};
	}
	if (o1.length > 0) {
		o1.addClass('owl-carousel');
		$(document).ready(function () {
			o1.owlCarousel($.extend({}, settings, {
				items: 1
			}));
		});
	}
	if (o2.length > 0) {
		o2.addClass('owl-carousel');
		$(document).ready(function () {
			$.each(o2, function () {
				if ($(this).parents('aside').length) {
					$(this).owlCarousel($.extend({}, settings, {
						items: 1,
						responsive: {
							0: {
								items: 1
							},
							480: {
								items: 2
							},
							768: {
								items: 1
							}
						}
					}));
				} else if ($(this).parents().hasClass('col-sm-9')){
					$(this).owlCarousel($.extend({}, settings, {
						responsive: {
							0: {
								items: 1
							},
							480: {
								items: 2
							},
							768: {
								items: 3
							}
						}
					}));
				} else if ($(this).parents().hasClass('col-sm-6')){
					$(this).owlCarousel($.extend({}, settings, {
						responsive: {
							0: {
								items: 1
							},
							480: {
								items: 2
							}
						}
					}));
				} else {
					$(this).owlCarousel($.extend({}, settings, {
						responsive: {
							0: {
								items: 1
							},
							480: {
								items: 2
							},
							768: {
								items: 3
							},
							1200: {
								items: 4
							},
							1635: {
								items: 5
							}
						}
					}));
				}
			});
		});
	}
})(jQuery);


/* selectbox Replacement
========================================================*/
;(function ($) {
	var o = $('#accordion'),
	o1 = $('select'),
	o2 = $('.radio'),
	o3 = $('label.radio-inline'),
	o4 = $('.checkbox'),
	o5 = $('input[name=\'agree\'][type=\'checkbox\']'),
	o6 = $('.checkbox-inline'),
	o7 = $('textarea');
	if (o.length || o1.length) {
	    include('js/jquery.selectbox-0.2.min.js');
	}
	$(window).load(function () {
		// Accordion fix
		if (o.length) {
			$('body').delegate('.accordion-toggle', 'click', replaceForm);
			$('.accordion-toggle').trigger('click');
		}
		// Radio Replacement
		if (o2.length) {
			var o2Input;
			var o2ArrVal = [];
			o2.each(function (i) {
				o2Input = $(this).find('input[type="radio"]');
				if ($.inArray(o2Input.attr('name') + o2Input.attr('value'), o2ArrVal) == -1) {
					o2Input.attr('id', o2Input.attr('name') + o2Input.attr('value'))
					o2Input.insertBefore($(this).find('label').attr('for', o2Input.attr('name') + o2Input.attr('value')));
					o2ArrVal.push(o2Input.attr('name') + o2Input.attr('value'))
				} else {
					o2Input.attr('id', o2Input.attr('name') + o2Input.attr('value') + i.toString());
					o2Input.insertBefore($(this).find('label').attr('for', o2Input.attr('name') + o2Input.attr('value') + i.toString()));
					o2ArrVal.push(o2Input.attr('name') + o2Input.attr('value') + i.toString());
				}
			});
		}
	});
	$(document).ready(function () {
		// Select Replacement
		if (o1.length) {
			o1.removeClass('form-control');
			o1.selectbox({
				effect: "slide",
				speed: 400
			});
		}
		// Radio Replacement
		if (o3.length) {
			var o3Input;
			o3.each(function () {
				o3Input = $(this).find('input[type="radio"]');
				o3Input.attr('id', o3Input.attr('name') + o3Input.attr('value'))
				o3Input.insertBefore($(this).attr('for', o3Input.attr('name') + o3Input.attr('value')));
			});
		}
		// Checkbox Replacement
		if (o4.length) {
			var o4Input;
			var o4ArrVal = [];
			o4.each(function (i) {
				o4Input = $(this).find('input[type="checkbox"]');
				if ($.inArray(o4Input.attr('name') + o4Input.attr('value'), o4ArrVal) == -1) {
					o4Input.attr('id', o4Input.attr('name') + o4Input.attr('value'))
					o4Input.insertBefore($(this).find('label').attr('for', o4Input.attr('name') + o4Input.attr('value')));
					o4ArrVal.push(o4Input.attr('name') + o4Input.attr('value'))
				} else {
					o4Input.attr('id', o4Input.attr('name') + o4Input.attr('value') + o4Input.attr('value') + i.toString())
					o4Input.insertBefore($(this).find('label').attr('for', o4Input.attr('name') + o4Input.attr('value') + o4Input.attr('value') + i.toString()));
					o4ArrVal.push(o4Input.attr('name') + o4Input.attr('value') + i.toString());
				}
			});
		}
		// Checkbox Replacement
		if (o5.length) {
			o5.attr('id', o5.attr('name') + o5.attr('value'));
			o5.parent().append('<label for="' + o5.attr('name') + o5.attr('value') + '"></label>');
			$('label[for="' + o5.attr('name') + o5.attr('value') + '"]').insertAfter(o5);
		}
		// Checkbox Replacement
		if (o6.length) {
			var o6Input;
			o6.each(function (i) {
				o6Input = $(this).find('input[type="checkbox"]');
				o6Input.attr('id', o6Input.attr('name') + o6Input.attr('value'))
				o6Input.insertBefore($(this).attr('for', o6Input.attr('name') + o6Input.attr('value')));
			});
		}
		if (o7.length) {
			o7.removeClass('form-control');
		}
	});
})(jQuery);

function replaceForm() {
	var o = $('.radio');
	var input;
	o.each(function () {
		input = $(this).find('input[type="radio"]');
		input.attr('id', input.attr('name') + input.attr('value'));
		input.insertBefore($(this).find('label').attr('for', input.attr('name') + input.attr('value')));
	});
	o = $('label.radio-inline');
	o.each(function () {
		input = $(this).find('input[type="radio"]');
		input.attr('id', input.attr('name') + input.attr('value'));
		input.insertBefore($(this).attr('for', input.attr('name') + input.attr('value')));
	});
	o = $('.checkbox');
	o.each(function () {
		input = $(this).find('input[type="checkbox"]');
		input.attr('id', input.attr('name') + input.attr('value'));
		input.insertBefore($(this).find('label').attr('for', input.attr('name') + input.attr('value')));
	});
	o = $('input[name=\'agree\'][type=\'checkbox\']');
	if (o.length) {
		o.attr('id', o.attr('name') + o.attr('value'));
		o.parent().append('<label for="' + o.attr('name') + o.attr('value') + '"></label>');
		$('label[for="' + o.attr('name') + o.attr('value') + '"]').insertAfter(o);
	}
	o = $('select');
	o.selectbox({
		effect: "slide",
		speed: 400
	});
	var o = $('textarea');
	o.removeClass('form-control'); 
}


/* Breadcrumb Last element replacement
========================================================*/
$(document).ready(function () {
	if ($('.breadcrumb').length) {
		var o = $('.breadcrumb li:last-child > a');
		o.replaceWith('<span>' + o.html() + '</span>');
	}
});


/* Quantity Counter
========================================================*/
;(function ($) {
	var o = $('input[name*="quantity"]');
	$(document).ready(function () {
		$('.counter-minus').click(function (e) {
			e.preventDefault();
			input = $(this).parent().find('input[name*="quantity"]');
			if (input.val() > 1){
				value = parseInt(input.val()) - 1;
				input.val(value);
			}
		})
		$('.counter-plus').click(function (e) {
			e.preventDefault();
			input = $(this).parent().find('input[name*="quantity"]');
			value = parseInt(input.val()) + 1;
			input.val(value);
		});
	});
})(jQuery);


/* Top-Links Active
========================================================*/
;(function ($) {
	$(document).ready(function(){
		var pgurl = window.location.href;
		$("#top-links a").each(function(){
			if($(this).attr("href") == pgurl || $(this).attr("href") == '' ) {
				$(this).addClass("current");
			}
		});
	});
})(jQuery);


/* Bx Slider
========================================================*/
;(function ($) {
	var o = $('#productGallery');
	var o1 = $('#productZoom');
	var o2 = $('#productFullGallery');
	if (o.length) {
	    include('js/jquery.bxslider.js');
		$(document).ready(function () {
			o
			.bxSlider({
				mode: 'vertical',
				pager: false,
				controls: true,
				slideMargin: 27,
				minSlides: 4,
				maxSlides: 4,
				slideWidth: o.attr('data-slide-width') ? o.attr('data-slide-width') : undefined,
				nextText: '<i class="fa fa-angle-down"></i>',
				prevText: '<i class="fa fa-angle-up"></i>',
				infiniteLoop: false,
				adaptiveHeight: true,
				moveSlides: 1
			})
			.find('li:first-child > a').addClass('zoomGalleryActive');
		});
	}
	if (o1.length) {
		o1
		.elevateZoom({
			gallery:'productGallery',
			responsive: true
		})
		.bind("click", function(e) {
			$.fancybox(o1.data('elevateZoom').getGalleryList());
			return false;
		});
	}
	if (o2.length) {
	    include('js/klass.min.js');
	    include('js/code.photoswipe-3.0.5.min.js');
		$(document).ready(function () {
			o2
			.bxSlider({
				pager: false,
				controls: true,
				minSlides: 1,
				maxSlides: 1,
				infiniteLoop: false,
				moveSlides: 1
			})
			.find('a').photoSwipe({
				enableMouseWheel: false,
				enableKeyboard: false,
				captionAndToolbarAutoHideDelay: 0
			});
		});
	}
})(jQuery);


/* Resize
========================================================*/
var flag = true;
function respResize() {
	var width = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
	if ($('#column-left').length) {
		var columnLeft = $('#column-left');
	} else {
		return false;
	}
	if (width > 767) {
		if (!flag) {
			flag = true;
			columnLeft.insertBefore('#content');
			if (window.twttr != undefined){
				twtrefresher.doRefresh();
			}
			$('.col-sm-3 .box-heading').unbind("click");
			$('.col-sm-3 .box-content').each(function () {
				if ($(this).is(":hidden")) {
					$(this).slideToggle();
				}
			});
		}
	} else if (flag) {
		flag = false;
		columnLeft.insertAfter('#content');
		if (window.twttr != undefined){
			twtrefresher.doRefresh();
		}
		$('.col-sm-3 .box-content').each(function () {
			if (!$(this).is(":hidden")) {
				$(this).parent().find('.box-heading').addClass('active');
			}
		});
		$('.col-sm-3 .box-heading').bind("click", function () {
			if ($(this).hasClass('active')) {
				$(this).removeClass('active').parent().find('.box-content').slideToggle();
			}else {
				$(this).addClass('active');
				$(this).parent().find('.box-content').slideToggle();
			}
		});
	}
}
$(window).load(respResize);
$(window).resize(function () {
	clearTimeout(this.id);
	this.id = setTimeout(respResize, 500);
});


/* DatetimePicker
========================================================*/
;(function ($) {
	var o1 = $('.date'),
	o2 = $('.datetime'),
	o3 = $('.time');
	if (o1.length || o2.length || o3.length) {
	    document.write('<script src="~/Template/js/moment.js"><\/script>');
	    document.write('<script src="~/Template/js/bootstrap-datetimepicker.min.js"><\/script>');
	}

	$(document).ready(function () {

		if (o1.length) {
			o1.datetimepicker({
				pickTime: false
			});
		}
		if (o2.length) {
			$('.datetime').datetimepicker({
				pickDate: true,
				pickTime: true
			});
		}
		if (o3.length) {
			$('.time').datetimepicker({
				pickDate: false
			});
		}

		
		$('.date,.datetime,.time').each(function () {
			var $this = $(this);
			$(this).find('.btn').click(function () {
				var body = $('body');
				if (body.hasClass('ajax-overlay-open')) {
					var open;
					setTimeout(function () {
						open = $('body').find('.bootstrap-datetimepicker-widget.picker-open');
						if (open.hasClass('top')) {
							open.css('bottom', $(window).height() - ($this.offset().top - $('.ajax-overlay').offset().top));
						}
					}, 10);
				}
			});
		});
	});
	$(document).ready(function () {
		$('.date, .datetime, .time').on('dp.show', function () {
			$('.date, .datetime, .time').not($(this)).each(function () {
				$(this).data("DateTimePicker").hide();
			});
		});
		$(this).on('click touchstart', function () {
			$('.date, .datetime, .time').each(function () {
				$(this).data("DateTimePicker").hide();
			});
		});
	});

})(jQuery);


/* Ajax-Add-to-Cart Upload File
========================================================*/
;(function ($) {
	var o = $('button[id^=\'button-upload\']');
	if (o.length) {
		$('button[id^=\'button-upload\']').on('click', function() {
			var node = this;
			$('#form-upload').remove();
			$('body').prepend('<form enctype="multipart/form-data" id="form-upload" style="display: none;"><input type="file" name="file" /></form>');
			$('#form-upload input[name=\'file\']').trigger('click');
			if (typeof timer != 'undefined') {
				clearInterval(timer);
			}
			timer = setInterval(function() {
				if ($('#form-upload input[name=\'file\']').val() != '') {
					clearInterval(timer);
					$.ajax({
						url: 'index.php?route=tool/upload',
						type: 'post',
						dataType: 'json',
						data: new FormData($('#form-upload')[0]),
						cache: false,
						contentType: false,
						processData: false,
						beforeSend: function() {
							$(node).button('loading');
						},
						complete: function() {
							$(node).button('reset');
						},
						success: function(json) {
							$('.text-danger').remove();

							if (json['error']) {
								$(node).parent().find('input').after('<div class="text-danger">' + json['error'] + '</div>');
							}
							if (json['success']) {
								alert(json['success']);

								$(node).parent().find('input').attr('value', json['code']);
							}
						},
						error: function(xhr, ajaxOptions, thrownError) {
							alert(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
						}
					});
				}
			}, 500);
		});
	}
})(jQuery);

/* Disable Mobile layout
========================================================*/
;(function ($) {
	var o = $('html');
	if (o.hasClass('mobile-responsive-off') && !o.hasClass('desktop')) {
		$('meta[name="viewport"]').prop('content', 'width=1200, initial-scale=1');
	}
})(jQuery);