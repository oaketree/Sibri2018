

$(function(){	


$(window).bind("resize", resizeWindow);

var e = $(window).width();


	function resizeWindow(e){
		
		var newWindowWidth = $(window).width();
		
		if(newWindowWidth < 768){
			$("#indexchoose").attr({href : "/Content/css/phonestyle.css"});
			}
		else if(newWindowWidth > 768  ){
			
			$("#indexchoose").attr({href : "/Content/css/comstyle.css"});
			
			}
	
		
	 
 }

resizeWindow(e);





}); 














/*
$(function(){	


$(window).bind("resize", resizeWindow);

var e = $(window).width();


	function resizeWindow(e){
		
		var newWindowWidth = $(window).width();
		
		if(newWindowWidth < 768){
			$("#indexchoose").attr({href : "/Content/css/phonestyleindex.css"});
			}
		else if(newWindowWidth > 1150){
			
			$("#indexchoose").attr({href : "/Content/css/comstyleindex.css"});
			
			}
		else {
	 $("#indexchoose").attr({href : "/Content/css/ipadtyleindex.css"});	 
	 
	 }
	 
 }

resizeWindow(e);





}); 
*/

