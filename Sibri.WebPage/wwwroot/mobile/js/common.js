$(function () {  //内页导航图片切换
    $.changeImg();
    $.Popup();
    $.productdetail();
}); 


$.extend({
    changeImg: function () {//内页导航图片切换
        var imgArr = ["topimg01_2.jpg",
            "topimg02_2.jpg",
            "topimg03_2.jpg",
        ];
        var img = parseInt(Math.random() * (imgArr.length));
        var currentImage = imgArr[img];
        $(".topbackground00").css("background-image", "url(/mobile/images/" + currentImage + ")");
        function changeImg() {
            $(".topbackground00").animate({ opacity: "0" }, 200, function () { $(this).css("background-image", "url(/mobile/images/" + currentImage + ")") });
            if (img >= imgArr.length - 1) {
                img = 0;
            }
            else {
                img++;
            }
            var currentImage = imgArr[img];
            $(".topbackground00").animate({ opacity: "1" }, 200);
        };
        setInterval(changeImg, 3000);
    }, Popup: function () {
        $("#opentechdh").click(function () {
            if ($("#techdh").css("display") == "none") {
                $("#techdh").show();
                $("#techdhmain").show();
            }
        });
        /*关闭弹出框*/
        $("#closedh,#techdh").click(function () {
            if ($("#techdh").css("display") == "block") {
                $("#techdh").hide();
                $("#techdhmain").hide();
            }
        });
    }, productdetail: function () {
        $("#dh_prodetail li").addClass("btn btn-xs");
        $("#dh_prodetail>ul li:nth-child(3n)").addClass("btn-warning");
        $("#dh_prodetail>ul li:nth-child(3n+1)").addClass("btn-primary");
        $("#dh_prodetail>ul li:nth-child(3n+2)").addClass("btn-info");
    }, validate: function () {
        var searchTxt = $("#search").val().trim();
        if (searchTxt == "") {
            return false;
        } else
            return true;
    }
});




