$(function(){
	var li = $('.nav ul .m');
	li.eq(0).find('a').eq(0).hover(function(){
		$(this).html('HOME');
	},function(){
		$(this).html('首页');
	});

	li.eq(1).find('a').eq(0).hover(function(){
		$(this).html('INTRODUCTION');
	},function(){
		$(this).html('公司简介');
	});
	
	li.eq(2).find('a').eq(0).hover(function(){
		$(this).html('PLATFORM');
	},function(){
		$(this).html('平台建设');
	});
	
	li.eq(3).find('a').eq(0).hover(function(){
		$(this).html('SERVICE');
	},function(){
		$(this).html('行业服务');
	});
	
	li.eq(4).find('a').eq(0).hover(function(){
		$(this).html('INSPECTION');
	},function(){
		$(this).html('检测与能审');
	});
	
	li.eq(5).find('a').eq(0).hover(function(){
		$(this).html('PRODUCT');
	},function(){
		$(this).html('产品与技术');
	});
	
	li.eq(6).find('a').eq(0).hover(function(){
		$(this).html('JOB');
	},function(){
		$(this).html('人才招聘');
	});

	li.eq(7).find('a').eq(0).hover(function(){
		$(this).html('CONTACT');
	},function(){
		$(this).html('联系我们');
	});
	
	
	
	//根据网站目录结构对应导航高亮显示（可忽略）
	
	var url = window.location.href.toLowerCase();
	//alert(url);
	if (url.indexOf("/Views/Static/行业服务.asp") > -1) {
		$(".aa4").attr("id", "sel");
	} else if (url.indexOf("/Views/Product/") > -1||url.indexOf("/marketing/knowledge/") > -1||url.indexOf("/about/news/") > -1||url.indexOf("/website/newweb/") > -1||url.indexOf("/marketing/seo/") > -1) {
		$(".aa5").attr("id", "sel");
	} else if (url.indexOf("/Views/Static/联系我们.asp") > -1) {
		$(".aa7").attr("id", "sel");
	} else if (url.indexOf("/Views/Static/检测与能审.asp") > -1||url.indexOf("/service/") > -1) {
		$(".aa2").attr("id", "sel");
	} else if (url.indexOf("/Views/Static/平台建设.asp") > -1||url.indexOf("/wangzhanjianshe/") > -1) {
		$(".aa6").attr("id", "sel");
	} else if (url.indexOf("/Views/Static/公司简介.asp") > -1) {
		$(".aa3").attr("id", "sel");
	} else if (url.indexOf("/Views/Static/人才招聘.asp") > -1) {
		$(".aa8").attr("id", "sel");
	} else if (url.indexOf("/Views/Home/index.asp") > -1) {
		$(".aa1").attr("id", "sel");
	} else {
		$(".aa1").attr("id", "sel");
	}
	
});