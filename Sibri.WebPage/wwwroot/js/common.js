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
});
$.extend({
    changeImg: function () {
        let imgArr = ["topimg01.jpg", "topimg02.jpg", "topimg03.jpg"];
        let imgIndex = 0;
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
        $("img.lazy").lazyload({
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
    }
});


