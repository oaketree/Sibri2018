//if (/Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent)) {
//    window.location.href = '/Mobile';
//}
$(function () {
    $.changeImg();
    $.meun();
    //top上的js
    $(".nav").slide({ type: "menu", titCell: ".m", targetCell: ".sub", effect: "slideDown", delayTime: 300, triggerTime: 100, returnDefault: true });
    //$.resizeWindow();
    //$(window).bind("resize", $.resizeWindow);

    //bottom上的js
    $(".bottombutton01").hover(
        function () {
            $(this).addClass("bottombutton01hover");
        },
        function () {
            $(this).removeClass("bottombutton01hover");
        }
    );
    //left上的js
    $('#nei10_011_left>div').hover(function () {
        $(this).addClass('select02').siblings().removeClass('select02');
    });
    $('#nei10_011_left>div').click(function () {
        $(this).addClass('select02').siblings().removeClass('select02');
    });
    $.product();
    //$.piccro();
    //首页中部切换
    $('.tit04jvzhong>div').hover(function () {
        var i = $(this).index();
        $('#con>li').eq(i).show().siblings().hide();
        $('.indexlxfsphone').hide();
    });

    $('.indexlxfsphone').hide();

    $('.indexlx').click(function () {
        if ($(this).parent().children().eq(1).is(':hidden')) {
            //console.log($(this).parent().children().eq(1));
            $(this).parent().children().eq(1).show();
        } else {
            $(this).parent().children().eq(1).show().hide();
        }
    })

    $(".bottombuttonindex01").hover(
        function () {
            $(this).addClass("bottombuttonindex01hover");
        },
        function () {
            $(this).removeClass("bottombuttonindex01hover");
        }
    );

    $(".button_showcenter").hover(
        function () {
            $(this).addClass("button_showcenterhover");
        },
        function () {
            $(this).removeClass("button_showcenterhover");
        }
    ); 
    


});
$.extend({
    changeImg: function () {
        let imgArr = ["topimg01.jpg", "topimg02.jpg", "topimg03.jpg"];
        let imgIndex = parseInt(Math.random() * (imgArr.length));
        let change = function () {
            if (imgIndex >= imgArr.length - 1) {
                imgIndex = 0;
            }
            $(".topbackground").animate({ opacity: "0" }, 200, "", function () {
                imgIndex++;
                let currentImage = imgArr[imgIndex];
                $(".topbackground").css("background-image", "url(/images/" + currentImage + ")");

            });
            $(".topbackground").animate({ opacity: "1" }, 200);
        }
        $(".topbackground").css("background-image", "url(/images/" + imgArr[imgIndex]+ ")");
        setInterval(change, 3000);
        //$(".topbackground").animate({opacity: "1"}, 200);
    }, meun: function () {
        let li = $('.nav ul .m');
        li.eq(0).find('a').eq(0).hover(function () {
            $(this).text('HOME');
        }, function () {
            $(this).text('\u9996\u9875');
        });
        li.eq(1).find('a').eq(0).hover(function () {
            $(this).text('News');
        }, function () {
            $(this).text('\u65b0\u95fb');
        });
        li.eq(2).find('a').eq(0).hover(function () {
            $(this).html('INTRODUCTION');
        }, function () {
            $(this).html('\u516c\u53f8\u7b80\u4ecb');
        });
        li.eq(3).find('a').eq(0).hover(function () {
            $(this).html('PLATFORM');
        }, function () {
            $(this).html('\u5e73\u53f0\u5efa\u8bbe');
        });
        li.eq(4).find('a').eq(0).hover(function () {
            $(this).html('SERVICE');
        }, function () {
            $(this).html('\u884c\u4e1a\u670d\u52a1');
        });

        li.eq(5).find('a').eq(0).hover(function () {
            $(this).html('INSPECTION');
        }, function () {
            $(this).html('\u68c0\u6d4b\u4e0e\u80fd\u5ba1');
        });

        li.eq(6).find('a').eq(0).hover(function () {
            $(this).html('PRODUCT');
        }, function () {
            $(this).html('\u4ea7\u54c1\u4e0e\u6280\u672f');
        });

        li.eq(7).find('a').eq(0).hover(function () {
            $(this).html('JOB');
        }, function () {
            $(this).html('\u4eba\u624d\u62db\u8058');
        });

        li.eq(8).find('a').eq(0).hover(function () {
            $(this).html('CONTACT');
        }, function () {
            $(this).html('\u8054\u7cfb\u6211\u4eec');
        });
    }, resizeWindow: function () {
        let newWindowWidth = $(window).width();
        if (newWindowWidth < 768) {
            $("#indexchoose").attr({
                href: "/css/phonestyle.css"
            });
        } else if (newWindowWidth > 768) {
            $("#indexchoose").attr({
                href: "/css/comstyle.css"
            });
        }
    }, loadImage: function () {
        $("img").lazyload({
            load: function () {
                $('#container').BlocksIt({
                    numOfCol: 3,
                    offsetX: 5,
                    offsetY: 5
                });
            }
        });
        //let count = 7;
        //$('.load_more').click(function () {
        //    var html = "";
        //    for (var i = count; i < count + 6; i++) {
        //        html = html + ` <div class="grid">
        //                <div class="imgholder">
        //                    <img class="lazy" title="${i}" data-original="/images/intro/${i}.jpg" width="225" />
        //                </div>
        //            </div>`;
        //    }
        //    //count = count + 6;
        //    $('#container').append(html);
        //    $('#container').BlocksIt({
        //        numOfCol: 3,  //每行显示数
        //        offsetX: 5,  //图片的间隔
        //        offsetY: 5   //图片的间隔
        //    });
        //    $("img.lazy").lazyload();
        //});
    }, product: function () {
        $('#tit>div').hover(function () {
            var i = $(this).index();//下标第一种写法			
            //var i = $('tit').index(this);//下标第二种写法
            //var bkong = i;
            $(this).addClass('select').siblings().removeClass('select');
            $('#con div').eq(i).show().siblings().hide();
            if (i == 0) {
                $('#neidaohangline').hide();
            }
            else {
                $('#neidaohangline').show();
            }
        });

    }, validate: function () {
        var searchTxt = $("#search").val().trim();
        if (searchTxt == "") {
            return false;
        } else
            return true;
    }, piccro: function () {
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
        $('#content').css('min-height', initHeight + 'px')
        //视窗变化时
        $(window).resize(function () {
            initHeight = $(window).height() - 60;
            $('#content').css('min-height', initHeight + 'px');
            if ($(window).height() > 866) {
                $('#ft_area').addClass('pst_ft');
            } else {
                $('#ft_area').removeClass('pst_ft');
            }
        })
        loadFinish();
        $('#switch_box').on('click', 'span', function () {
            var stepIndex = $switchList.index($(this));
            actFn(stepIndex);
        }).hover(function () {
            clearInterval(timer);
        }, function () {
            timer = setInterval(function () {
                actFn(key);
            }, 3000)
        })

        $('.s_arr').on('click', 'a', function () {
            var step = $('.s_arr').find('a').index($(this));
            $('#switch_box').find('span').each(function (i) {
                var css = $(this).hasClass("on");
                if (css) {
                    if (step == 0) {
                        if (i == 0) {
                            step = 2
                        } else {
                            step = i - 1;
                        }
                    } else {
                        if (i == 2) {
                            step = 0;
                        } else {
                            step = i + 1;
                        }
                    }
                }
            });
            actFn(step);
        }).hover(function () {
            clearInterval(timer);
        }, function () {
            timer = setInterval(function () {
                actFn(key);
            }, 3000)
        })
        //底部位置调整
        if ($(window).height() > 866) {
            $('#ft_area').addClass('pst_ft');
        } else {
            $('#ft_area').removeClass('pst_ft');
        }
        //轮播
        function actFn(stepIndex) {
            var stepIndex = stepIndex;
            $switchList.eq(stepIndex).addClass('on').siblings().removeClass('on');
            $colorList.stop().eq(stepIndex).animate({ opacity: 1 }, 800).css({ flter: "Alpha(Opacity=100)" }).siblings().animate({ opacity: 0 }, 800);
            $txtList.eq(stepIndex).addClass('on').siblings().removeClass('on');
            key = stepIndex;
            $('.bgs_box').eq(key).find(".img_area").addClass('item_img_css3');
            $('.bgs_box').eq(key).siblings().find(".img_area").removeClass('item_img_css3');
            $('.bgs_box').eq(key).find(".footer").fadeIn(800);
            $('.bgs_box').eq(key).siblings().find(".footer").fadeOut(800);
            $(".item_txt").eq(key).addClass('item_txt_css3').siblings().removeClass('item_txt_css3');
            key++;
            if (key == $txtList.length) {
                key = 0;
            }
        }
        //预加载banner动画背景图
        function preloadImages() {
            var arrImage = [];
            var parLen = arguments.length;
            var cur = 0;
            for (var i = 0; i < parLen; i++) {
                arrImage[i] = new Image();
                arrImage[i].onload = function () {
                    if (cur == parLen - 1) {
                        loadFinish();
                    }
                    cur++;
                };
                arrImage[i].src = arguments[i];
            }
        }
        function loadFinish() {
            $txtList.eq(0).addClass('item_txt_css3');
            $colorList.eq(0).find(".img_area").addClass('item_img_css3');
            //自动轮播
            timer = setInterval(function () {
                actFn(key);
            }, 3000);
        }
    }
});


