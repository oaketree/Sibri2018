(function($){
    var initHeight = $(window).height() - 60,
        $colorList = $('#color_list').find('.bgs_box'),
        $txtList = $('#txt_list').find('.item_txt'),
        $switchList = $('#switch_box').find('span'),
        timer = null,
        key = 1;
	
    //IE6 bug
    $switchList.eq(0).addClass('on');
    $txtList.eq(0).addClass('on');
    $colorList.eq(0).addClass('on');

    //高度调整
    $('#content').css('min-height',initHeight + 'px')
    //视窗变化时
    $(window).resize(function(){
        initHeight = $(window).height() - 60;
        $('#content').css('min-height',initHeight + 'px');
        if($(window).height() > 866){
            $('#ft_area').addClass('pst_ft');
        }else{
           $('#ft_area').removeClass('pst_ft'); 
        }
    })
	loadFinish();
    $('#switch_box').on('click','span',function(){
        var stepIndex = $switchList.index($(this));
        actFn(stepIndex);
    }).hover(function(){
        clearInterval(timer);
    },function(){
        timer = setInterval(function(){
            actFn(key);
        },3000)
    })

    $('.s_arr').on('click','a',function(){
        var step = $('.s_arr').find('a').index($(this));
		$('#switch_box').find('span').each(function(i){
   			var css=$(this).hasClass("on");
			if(css){
				if(step==0){
					if(i==0){
						step=2	
					}else{
						step=i-1;
					}
				}else{
					if(i==2){
						step=0;	
					}else{
						step=i+1;
					}
				}	
			}
 		});
        actFn(step);
    }).hover(function(){
        clearInterval(timer);
    },function(){
        timer = setInterval(function(){
            actFn(key);
        },3000)
    })
    //底部位置调整
    if($(window).height() > 866){
        $('#ft_area').addClass('pst_ft');
    }else{
       $('#ft_area').removeClass('pst_ft'); 
    }
    //轮播
    function actFn(stepIndex){
        var stepIndex = stepIndex;
        $switchList.eq(stepIndex).addClass('on').siblings().removeClass('on');
        $colorList.stop().eq(stepIndex).animate({opacity:1},800).css({flter:"Alpha(Opacity=100)"}).siblings().animate({opacity:0},800);
        $txtList.eq(stepIndex).addClass('on').siblings().removeClass('on');
        key = stepIndex;
        $('.bgs_box').eq(key).find(".img_area").addClass('item_img_css3');
        $('.bgs_box').eq(key).siblings().find(".img_area").removeClass('item_img_css3');
        $('.bgs_box').eq(key).find(".footer").fadeIn(800);
        $('.bgs_box').eq(key).siblings().find(".footer").fadeOut(800);
        $(".item_txt").eq(key).addClass('item_txt_css3').siblings().removeClass('item_txt_css3');
        key++;
        if(key == $txtList.length){
            key = 0;
        }
    }
	//预加载banner动画背景图
	function preloadImages(){
		var arrImage = [];
		var parLen = arguments.length;
		var cur = 0;
		for (var i = 0; i < parLen; i++) {
			arrImage[i] = new Image();
			arrImage[i].onload = function(){
				if(cur == parLen -1){
					loadFinish();	
				}
				cur++;
			};
			arrImage[i].src = arguments[i];
		}
	}
	function loadFinish(){
        $txtList.eq(0).addClass('item_txt_css3');
        $colorList.eq(0).find(".img_area").addClass('item_img_css3');
        //自动轮播
        timer = setInterval(function(){
            actFn(key);
        },3000);
	}
})(jQuery)