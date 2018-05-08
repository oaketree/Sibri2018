


$(function () {  //
    var imgArr=["topimg01.jpg",   
                "topimg02.jpg",   
                "topimg03.jpg",
		
    ];  
    var img=parseInt(Math.random()*(imgArr.length));
	
	
	var currentImage=imgArr[img]; 
	$(".topbackground").css("background-image","url(/Content/images/"+currentImage+")"); 
	
function changeImg(){
		$(".topbackground").animate({opacity:"0"},200,function(){$(this).css("background-image","url(/Content/images/"+currentImage+")")});  
	
	
		if(img>=imgArr.length-1){
			img=0;
			}
		
		else {
			img++;
			}
			


		 var currentImage=imgArr[img]; 
		 
		 
		
		 
   	/*	$(".topbackground").css("background-image","url(/Content/images/"+currentImage+")");  */
		$(".topbackground").animate({opacity:"1"},200);  
	
    
		};
	
	

	
	 setInterval(changeImg,3000); 
	

});  












