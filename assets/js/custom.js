$(window).load(function () {

       

	function adjustHeight(){
		var getBodyH= $('#left-panel').outerHeight();
		$('body').css('min-height',getBodyH);
	}
  
     
    
	adjustHeight();
	$('#left-panel li').click(function(){
		//alert('ok');
		setTimeout(function(){
			adjustHeight();
		}, 500); 
		
	});

	

//    function myLoadFunction() {
//    setTimeout(function () {
//        $('.loading-container')
//            .addClass('loading-inactive');
//    }, 2000);
//}

});