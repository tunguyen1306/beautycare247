function clickTabContact(tabid,tabpanel){
		  	//var tabpanel=tabid+"_panel";
			$(".tab-contact .tab-contact-header li a").attr("class","");
			$(tabid).attr("class","active");
			$(".tab_contact_panel").css("display","none");
		 	$(tabpanel).css("display","block");
	  }

$(document).ready(function () {
	  
	 
	  $( ".f-video-detail-readmorebt" ).click(function() {
			
		  $( ".f-video-view-detail").slideToggle( 1000, function() {
			  if($(this).is(":hidden")){
					$( ".f-video-detail-readmorebt").text('Xem thêm...');
				}else{$( ".f-video-detail-readmorebt").text('Thu gọn...');}
			
		  });
		});
	  	$( ".f-album-detail-readmorebt" ).click(function() {
		  $( ".f-album-detail").slideToggle( 1000, function() {
			  if($(this).is(":hidden")){
					$( ".f-album-detail-readmorebt").text('Xem thêm...');
				}else{$( ".f-album-detail-readmorebt").text('Thu gọn...');}
			
		  });
		});
		
		
		
	  	$('#slidezoompage').carousel({
		  interval: 1000000
		});
		
		
	  
      $('[data-toggle=offcanvas]').click(function () {
          $('.row-offcanvas').toggleClass('active');
      });

      $("#grid").click(function () {
          
          $(".f-product-viewid").attr('class', 'f-product-viewid f-product row');
		  // $(".f-product-viewid > li").attr('class', 'col-lg-3 col-md-4');
		  // $(".f-product-viewid li .f-pr-item").attr('class', 'f-pr-item');
		  // $(".f-product-viewid .f-pr-item-img").attr('class', 'f-pr-item-img');
		  // $(".f-product-viewid .f-pr-item-img a").attr('class', '');
		  // $(".f-product-viewid .f-pr-item-boxdetail").attr('class', 'f-pr-item-boxdetail');
      });
      $("#list").click(function () {
      $(".f-product-viewid").attr('class', 'f-product-viewid viewlist row');
		  // $(".f-product-viewid > li").attr('class', '');
		  // $(".f-product-viewid li .f-pr-item").attr('class', 'f-pr-item row');
		  // $(".f-product-viewid .f-pr-item-img").attr('class', 'f-pr-item-img col-md-4 col-xs-4');
		  // $(".f-product-viewid .f-pr-item-img a").attr('class', 'thumbnail');
		  // $(".f-product-viewid .f-pr-item-boxdetail").attr('class', 'f-pr-item-boxdetail col-md-8 col-xs-8');
      });

      $('#f-compare .f-compare-title i').click(function () {
          if ($('#f-compare').attr('status') == 'closed') {
              $('#f-compare').animate({
                  right: '0px'
              }, 1000);
              $('#f-compare').attr('status', 'open');
          } else {
              $('#f-compare').animate({
                  right: '-157px'
              }, 1000);
              $('#f-compare').attr('status', 'closed');
          }
      });

      $('.f-compare-ul li').each(function () {

          var element = $(this);
          $(this).children('.f-compare-del').click(function () {
              //element.animate({opacity:'0.0'},"slow",function(){ element.remove();});
              element.hide('slow', function () {
                  element.remove();
              });
          });
      });

  });
   //Hàm add sản phẩm vào khung so sánh
  $('.f-pr-item-compare').click(function () {

      var productname = $(this).parents().children('.f-pr-item-boxdetail').children('h2').children('.f-pr-item-name').text();
      var newProToCompare = $('<li><a class="f-compare-del" href="javascript:void()" title="Xóa khỏi so sánh"></a><span> ' + productname + '</span></li>').hide();

      if ($('#f-compare').attr('status') == 'closed') {
          $('#f-compare').animate({
              right: '0px'
          }, 1000);
          $('#f-compare').attr('status', 'open');
      }

      $('.f-compare-ul').append(newProToCompare);
      newProToCompare.show(1000);
      // Hàm cài đặt script vào nút xóa phần tử trong so sánh
      $('.f-compare-ul li').each(function () {
          var elementRemove = $(this);
          $(this).children('.f-compare-del').click(function () {
              elementRemove.hide('slow', function () {
                  elementRemove.remove();
              });
          });
      });
      //Hiện thị nút so sánh khi đã chọn
      $(this).css('display', 'block');
      $(this).children('span').text('Đã chọn');
	  
	  
      $('#cartitem').tooltip();

  });

   // Jcallros
   if($('#galleria').length > 0){
   	  		 Galleria.loadTheme('resources/plugins/galleria/themes/classic/galleria.classic.min.js');
			 Galleria.run('#galleria', {
								autoplay: 3000, // will move forward every 7 seconds
								fullscreenDoubleTap:true
			});	
		}
      