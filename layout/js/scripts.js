$(document).ready(function () {
    'use strict';
    /*
    ======= Menu Action ======
    */
    var winW = $(window).width(),
     navbarW = $('.side-menu').innerWidth();

    $("header .toggle-menu").click(function () {
        view_menu();
    });
    $(".side-menu .toggle-menu").click(function () {
        hide_menu();
    });
    if (winW < 480) {
        $(".nav-link").click(function () {
            hide_menu();
        });
    }
    function view_menu() {
        var headerH = $('header').innerHeight();
        $(".side-menu").addClass("nav-view");
        $("header").animate({ 'margin-top': '-' + headerH }, 500);
        if (winW > 480) {
            $(".auto-content").animate({ 'width': winW - navbarW, 'margin-left': navbarW }, 1000).css('float', 'right');
        } else {
            $(".auto-content").animate({ 'width': winW, 'margin-left': 0 }, 1000).css('float', 'left');
        }
        $('#myCarousel').css('padding-top', 0);
        $('#myCarousel .carousel-item').height(winH);
    }
    function hide_menu() {
        $(".side-menu").removeClass("nav-view");
        $(".auto-content").animate({ 'width': 100 + '%', 'margin-left': 0 }, 500).css('float', 'none');

        $("header").animate({ 'margin-top': 0 }, 500);
    }

    /*
    ======= Slideshow Dinamic Padding ======
    */

    var winH = $(window).height(),
     headerH = $('header').innerHeight();
    $('#myCarousel .carousel-item').height(winH - headerH);
    $('#myCarousel').css('padding-top', headerH);
    $('.nav-link').on('click', function (event) {
        headerH = $('header').innerHeight();
        var target = $(this.getAttribute('href'));

        if (target.length) {
            event.preventDefault();
            $('html, body').stop().animate({
                scrollTop: target.offset().top - headerH
            }, 1000);
        }

    });

    /*
    ======= Account dropdown ======
    */

    $(".account-dropdown").hide();
    $(".account-dropdown-click").click(function () {
        $(".account-dropdown").slideToggle();
    });

    /*
    ======= Accordion Plus And Minus ======
    */

    function toggleIcon(e) {
        $(e.target)
            .prev('.card-header')
            .find(".more-less")
            .toggleClass('fa-plus-circle fa-minus-circle');
    }
    $('.card').on('hidden.bs.collapse', toggleIcon);
    $('.card').on('shown.bs.collapse', toggleIcon);

    /*
    ======= Switch Between Login And Forget Password Form ======
    */

    $(".forget-pass-form").addClass("hide");
    $(".login-form").addClass("view");
    $(".forget-password").click(function () {
        $(".forget-pass-form").addClass("view").removeClass("hide");
        $(".login-form").addClass("hide").removeClass("view");
        $(".modal-header .modal-title").text("");
    });
    $(".cancle").click(function () {
        $(".login-form").addClass("view").removeClass("hide");
        $(".forget-pass-form").addClass("hide").removeClass("view");
        $(".modal-header .modal-title").text("Login");
    });

    /*
    ======= Toggle Change Password Box  ======
    */

    $(".changepass").hide();
    $(".changepass-btn").click(function () {
        $(".changepass").slideToggle(500);
    });



    /*
    ======= nav-scroll ======
    */
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        if (scroll > 180) {
            $("header").css("background-color", "#272a2f");
        }

        else {
            $("header").css("background-color", "rgba(39, 42, 47, 0.45)");
        }
    });


    /*
    ======= shipping-sidebar-counter ======
    */

    $(".shipping-sidebar .col-md-6").each(function () {
        var this_count = $(this).find(".card span").text();
        console.log(this_count);
        $(this).find(".fa-plus").click(function () {
            this_count++;
            $(this).closest(".col-md-6").find("span").text(this_count);
        });
        $(this).find(".fa-minus").click(function () {
            if (this_count > 0) {
                this_count--;
            }
            $(this).closest(".col-md-6").find("span").text(this_count);
        });

    });



    /*
    ======= Crop-Image-Plugin ======
    */

    // $(".default-img").css("display","none");
    $(".numbers").text(0);
   

    //$(".fa-close").click(function () {
    //    $(".img-container").find("img").remove();
    //});

    var items = $(".thumbnails .col-md-2:visible").length;
    $(document).on('click', '.number-slider .fa-chevron-circle-right', function (e) {

        var index = $(".selected").closest(".col-md-2:visible").index() - 2;
        $('.thumbnails .col-md-2:eq(' + (index) + ') img').dblclick();

        //var i = index + 1;
        //if (index > 0) {
        //    index--;
        //    i--;
        //    $(".numbers").text(i);
        //    $(".thumbnails .col-md-2:visible img").removeClass("selected");
        //    var src = $(".thumbnails .col-md-2:visible:eq(" + index + ") img").attr("src");
        //    $(".thumbnails .col-md-2:visible:eq(" + index + ") img").dblclick();

        //    $(".cropper-canvas img").attr("src", src);
        //    $(".cropper-view-box img").attr("src", src);
        //    $(".default-img").attr("src", src);

        //} else {
        //    if ($(".thumbnails .col-md-2:visible").length > 0) {
        //        i = 1;
        //    } else
        //    {
        //        i = 0;
        //    }
           
        //    $(".numbers").text(i);


        //}

        //$(".setolddata").click();

       //////////////////////
        //var index = $(".selected").closest(".col-md-2:visible").index() - 1;
        //var i = index + 1;
        //if (index < $(".thumbnails .col-md-2:visible").length - 1) {
        //    index++;
        //    if (i < $(".thumbnails .col-md-2:visible").length) {
        //        i++;
        //    }
        //    $(".numbers").text(i);
        //    $(".thumbnails .col-md-2:visible img").removeClass("selected");
        //    var src = $(".thumbnails .col-md-2:visible:eq(" + index + ") img").attr("src");
        //    $(".thumbnails .col-md-2:visible:eq(" + index + ") img").dblclick();
        //    $(".cropper-canvas img").attr("src", src);
        //    $(".cropper-view-box img").attr("src", src);
        //    $(".default-img").attr("src", src);

        //} else {
        //    $(".numbers").text(i);
        //}
    });
    $(document).on('input', '.patternNumber', function (e) {

        var m = $(this).val();
        if (m && m > 0) {

        } else {
            $(this).val(1);

        }

    });
     //$('.patternNumber').on('input', function () {
     //       debugger;
     //       var m = $(this).val();
     //       if (m && m>0) {
              
     //       } else {
     //           $(this).val(1);
             
     //       }
     //   });


    


    $(document).on('mouseup', '.btunratio label', function (e) {
        debugger;
        if ($('#image-preview_dragdrop-container:visible').length == 0) {
              $('.loader-gif').show();
    //$("input[type='radio']").mouseup(function () {
        debugger;
        console.log('fun1');
        setTimeout(myFunctionofupdate, 3000)
        }
      
    });

    function myFunctionofupdate() {
        debugger;
        console.log('fun2');
      
        $(".cropedbutton").click();
        $('.loader-gif').hide();
       
    }

    //$(document).on('mouseup', '#inputImage', function (e) {
     
           
    //        //$("input[type='radio']").mouseup(function () {
    //        debugger;
    //        console.log('fun1');
    //        setTimeout(myFunctionofupdate3, 3000)
        

    //});


    //$(document).on('change', '#inputImage', function () {
    //    console.log('after all callbacks');
    //    setTimeout(myFunctionofupdate3, 3000)
       
    //});

    //function myFunctionofupdate3() {
    //    debugger;
    //    $('.loader-gif').show();
    //    var items2 = $(".thumbnails .col-md-2").length;
    //    $(".cropedbutton2").click();
    //    for (var i = 0; i < items2; i++) {
         


    //        setTimeout(myFunctionofupdate33, 2000)
            
    //        setTimeout(myFunctionofupdate34, 2000)
            
           
         
              
          

    //    }
    //    $('.loader-gif').hide();
    //}


    

    
    //$(document).on('mouseup', '#inputImage', function (e) {
    //    debugger;
    //    if ($('#image-preview_dragdrop-container:visible').length == 0) {
    //          $('.loader-gif').show();
    ////$("input[type='radio']").mouseup(function () {
    //    debugger;
    //    console.log('fun1');
    //    setTimeout(myFunctionofupdate, 3000)
    //    }
      
    //});



    $(document).on('click', '.number-slider .fa-chevron-circle-left', function (e) {

        var index = $(".selected").closest(".col-md-2:visible").index();
        ////debugger;
        //if (true) {

        //}
        $('.thumbnails .col-md-2:eq('+(index)+') img').dblclick();
        //var i = index + 1;
        //if (index < $(".thumbnails .col-md-2:visible").length - 1) {
        //    index++;
        //    if (i < $(".thumbnails .col-md-2:visible").length) {
        //        i++;
        //    }
        //    $(".numbers").text(i);
        //    $(".thumbnails .col-md-2:visible img").removeClass("selected");
        //    var src = $(".thumbnails .col-md-2:visible:eq(" + index + ") img").attr("src");
        //    $(".thumbnails .col-md-2:visible:eq(" + index + ") img").dblclick();
        //    $(".cropper-canvas img").attr("src", src);
        //    $(".cropper-view-box img").attr("src", src);
        //    $(".default-img").attr("src", src);

        //} else {
        //    $(".numbers").text(i);
        //}

        //$(".setolddata").click();

        //////////////////////////
        //var index = $(".selected").closest(".col-md-2:visible").index() - 1;
        //var i = index + 1;
        //if (index > 0) {
        //    index--;
        //    i--;
        //    $(".numbers").text(i);
        //    $(".thumbnails .col-md-2:visible img").removeClass("selected");
        //    var src = $(".thumbnails .col-md-2:visible:eq(" + index + ") img").attr("src");
        //    $(".thumbnails .col-md-2:visible:eq(" + index + ") img").dblclick();

        //    $(".cropper-canvas img").attr("src", src);
        //    $(".cropper-view-box img").attr("src", src);
        //    $(".default-img").attr("src", src);

        //} else {
        //    $(".numbers").text(i);
        //}
    });

    var selectors1 = [];
    var selectors2 = [];
    var selectors3 = [];
    var selectors4 = [];
    var selectors5 = [];
    var selectors6 = [];

    $(".docs-toggles label:first").removeClass("active");
    $(document).on('dblclick', '.thumbnails .col-md-2 img', function (e) {
        // $(".thumbnails .col-md-2 img").dblclick(function(){
        var this_src = $(this).attr("src");
        $(".cropper-canvas img").attr("src", this_src);
        $(".cropper-view-box img").attr("src", this_src);
        $(".default-img").attr("src", this_src);
        $(".thumbnails .col-md-2 img").removeClass("selected");
        console.log($(this).index());
        $(this).addClass("selected");
        var data_label = $(this).closest(".col-md-2").index();
        //debugger;
        var data_label2 = $(this).closest(".col-md-2:visible").index();
        $(".docs-toggles label").removeClass("active");
        $(".numbers").text(data_label2);
        if ($.inArray(data_label, selectors1) > -1) {
            $(".docs-toggles label[data-selected='" + selectors1 + "']").click();
        } else if ($.inArray(data_label, selectors2) > -1) {
            $(".docs-toggles label[data-selected='" + selectors2 + "']").click();
        } else if ($.inArray(data_label, selectors3) > -1) {
            $(".docs-toggles label[data-selected='" + selectors3 + "']").click();
        } else if ($.inArray(data_label, selectors4) > -1) {
            $(".docs-toggles label[data-selected='" + selectors4 + "']").click();
        } else if ($.inArray(data_label, selectors5) > -1) {
            $(".docs-toggles label[data-selected='" + selectors5 + "']").click();
        } else if ($.inArray(data_label, selectors6) > -1) {
            $(".docs-toggles label[data-selected='" + selectors6 + "']").click();
        }
    });

    $(document).on('click', '.docs-toggles label', function (e) {
        var selected_index = $(".selected").closest(".col-md-2").index();
        $(".docs-toggles label[data-selected='" + selected_index + "']:not(this)").attr('data-selected', '');
        var selected_indexpattern = $(this).find("input").attr("id");

        // add to new

        if (selected_indexpattern == "aspectRatio1") {
            var index1 = selectors1.indexOf(selected_index);
            var index2 = selectors2.indexOf(selected_index);
            var index3 = selectors3.indexOf(selected_index);
            var index4 = selectors4.indexOf(selected_index);
            var index5 = selectors5.indexOf(selected_index);
            var index6 = selectors6.indexOf(selected_index);
            if (index1 != -1) {
                selectors1.splice(index1, 1);
            }
            if (index2 != -1) {
                selectors2.splice(index2, 1);
            }
            if (index3 != -1) {
                selectors3.splice(index3, 1);
            }
            if (index4 != -1) {
                selectors4.splice(index4, 1);
            }
            if (index5 != -1) {
                selectors5.splice(index5, 1);
            }
            if (index6 != -1) {
                selectors6.splice(index6, 1);
            }


            selectors1.push(selected_index);


            var myListofArrays = [];
            myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

            $(".docs-toggles label").each(function (index) {
                var x = 'selectors' + (index + 1);
                $(this).attr('data-selected', myListofArrays[index]);

            });



        } else if (selected_indexpattern == "aspectRatio2") {
            var index1 = selectors1.indexOf(selected_index);
            var index2 = selectors2.indexOf(selected_index);
            var index3 = selectors3.indexOf(selected_index);
            var index4 = selectors4.indexOf(selected_index);
            var index5 = selectors5.indexOf(selected_index);
            var index6 = selectors6.indexOf(selected_index);
            if (index1 != -1) {
                selectors1.splice(index1, 1);
            }
            if (index2 != -1) {
                selectors2.splice(index2, 1);
            }
            if (index3 != -1) {
                selectors3.splice(index3, 1);
            }
            if (index4 != -1) {
                selectors4.splice(index4, 1);
            }
            if (index5 != -1) {
                selectors5.splice(index5, 1);
            }

            if (index6 != -1) {
                selectors6.splice(index6, 1);
            }

            selectors2.push(selected_index);

            var myListofArrays = [];
            myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

            $(".docs-toggles label").each(function (index) {
                var x = 'selectors' + (index + 1);
                $(this).attr('data-selected', myListofArrays[index]);

            });
        } else if (selected_indexpattern == "aspectRatio3") {
            var index1 = selectors1.indexOf(selected_index);
            var index2 = selectors2.indexOf(selected_index);
            var index3 = selectors3.indexOf(selected_index);
            var index4 = selectors4.indexOf(selected_index);
            var index5 = selectors5.indexOf(selected_index);
            var index6 = selectors6.indexOf(selected_index);
            if (index1 != -1) {
                selectors1.splice(index1, 1);
            }
            if (index2 != -1) {
                selectors2.splice(index2, 1);
            }
            if (index3 != -1) {
                selectors3.splice(index3, 1);
            }
            if (index4 != -1) {
                selectors4.splice(index4, 1);
            }
            if (index5 != -1) {
                selectors5.splice(index5, 1);
            }

            if (index6 != -1) {
                selectors6.splice(index6, 1);
            }
            selectors3.push(selected_index);
            var myListofArrays = [];
            myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

            $(".docs-toggles label").each(function (index) {
                var x = 'selectors' + (index + 1);
                $(this).attr('data-selected', myListofArrays[index]);

            });
        } else if (selected_indexpattern == "aspectRatio4") {
            var index1 = selectors1.indexOf(selected_index);
            var index2 = selectors2.indexOf(selected_index);
            var index3 = selectors3.indexOf(selected_index);
            var index4 = selectors4.indexOf(selected_index);
            var index5 = selectors5.indexOf(selected_index);
            var index6 = selectors6.indexOf(selected_index);
            if (index1 != -1) {
                selectors1.splice(index1, 1);
            }
            if (index2 != -1) {
                selectors2.splice(index2, 1);
            }
            if (index3 != -1) {
                selectors3.splice(index3, 1);
            }
            if (index4 != -1) {
                selectors4.splice(index4, 1);
            }
            if (index5 != -1) {
                selectors5.splice(index5, 1);
            }
            if (index6 != -1) {
                selectors6.splice(index6, 1);
            }
            selectors4.push(selected_index);
            var myListofArrays = [];
            myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

            $(".docs-toggles label").each(function (index) {
                var x = 'selectors' + (index + 1);
                $(this).attr('data-selected', myListofArrays[index]);
            });
        }

        else if (selected_indexpattern == "aspectRatio5") {
            var index1 = selectors1.indexOf(selected_index);
            var index2 = selectors2.indexOf(selected_index);
            var index3 = selectors3.indexOf(selected_index);
            var index4 = selectors4.indexOf(selected_index);
            var index5 = selectors5.indexOf(selected_index);
            var index6 = selectors6.indexOf(selected_index);
            if (index1 != -1) {
                selectors1.splice(index1, 1);
            }
            if (index2 != -1) {
                selectors2.splice(index2, 1);
            }
            if (index3 != -1) {
                selectors3.splice(index3, 1);
            }
            if (index4 != -1) {
                selectors4.splice(index4, 1);
            }
            if (index5 != -1) {
                selectors5.splice(index5, 1);
            }
            if (index6 != -1) {
                selectors6.splice(index6, 1);
            }
            selectors5.push(selected_index);
            var myListofArrays = [];
            myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

            $(".docs-toggles label").each(function (index) {
                var x = 'selectors' + (index + 1);
                $(this).attr('data-selected', myListofArrays[index]);

            });
        }
        else {

            var index1 = selectors1.indexOf(selected_index);
            var index2 = selectors2.indexOf(selected_index);
            var index3 = selectors3.indexOf(selected_index);
            var index4 = selectors4.indexOf(selected_index);
            var index5 = selectors5.indexOf(selected_index);
            var index6 = selectors6.indexOf(selected_index);
            if (index1 != -1) {
                selectors1.splice(index1, 1);
            }
            if (index2 != -1) {
                selectors2.splice(index2, 1);
            }
            if (index3 != -1) {
                selectors3.splice(index3, 1);
            }
            if (index4 != -1) {
                selectors4.splice(index4, 1);
            }
            if (index5 != -1) {
                selectors5.splice(index5, 1);
            }
            if (index6 != -1) {
                selectors6.splice(index6, 1);
            }
            selectors6.push(selected_index);
            var myListofArrays = [];
            myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

            $(".docs-toggles label").each(function (index) {
                var x = 'selectors' + (index + 1);
                $(this).attr('data-selected', myListofArrays[index]);

            });
        }


       
    });
    var zoomvalue = 0;
    var mouseeventflag=0
    //$(document).on('mousedown', '.zoom-slider', function (e) {
    //    mouseeventflag = 1;
    //});
    //$(document).on('input', '.zoom-slider', function (e) {
    //    //debugger;
    //    //if (mouseeventflag==1) {
    //    if ($(this).val() > zoomvalue) {
    //        //cropper.zoom(0.05);
    //        $(".zoom-in").click();
            
    //    } else {
    //        $(".zoom-out").click();
            
    //        //zoom(-0.1)

    //    }
    //    console.log(IOBreadcrumb.breadcrumbs);
    //    zoomvalue = $(this).val();
    //   // mouseeventflag = 0;
    ////}
    //});

  
    //$(document).on({
       
    //    mousedown: function () {
    //        debugger;
    //        mouseeventflag = 1;
    //    },
    //    mouseup: function () {
    //        mouseeventflag = 0;
    //    },
    //    mousemove: function () {
    //        if (mouseeventflag == 1) {
    //                if ($(this).val() > zoomvalue) {
    //                    $(".zoom-in").attr("data-option", "" + $(this).val() + "");

    //                } else if ($(this).val() < zoomvalue) {
    //                    $(".zoom-in").attr("data-option", "" + -$(this).val() + "");

    //                }
    //            //    //console.log(IOBreadcrumb.breadcrumbs);
    //            //    zoomvalue = $(this).val();
    //            //}
    //           // $(".zoom-in").attr("data-option", "" + $(this).val() + "");
    //                $(".zoom-in").click();
    //                zoomvalue = $(this).val();
    //        }
    //    }
    //}, '.zoom-slider');
  

   
   
    
    //this.setimageolddata();


    $("#imagedelete").click(function () {
        //debugger;
        var indeximage = $(".selected").closest(".col-md-2").index();
        $(".selected").closest(".col-md-2").remove();
      
        var count = indeximage-1;
       
        if (count <= 0 && $('.thumbnails .col-md-2').length>0) {
            count = 1;
        } else {
            count = 0;
        }
        $(".numbers").text(count);
       
        var index1 = selectors1.indexOf(indeximage);
        var index2 = selectors2.indexOf(indeximage);
        var index3 = selectors3.indexOf(indeximage);
        var index4 = selectors4.indexOf(indeximage);
        var index5 = selectors5.indexOf(indeximage);
        var index6 = selectors6.indexOf(indeximage);
        if (index1 != -1) {
            selectors1.splice(index1, 1);
        }
        if (index2 != -1) {
            selectors2.splice(index2, 1);
           
        }
        if (index3 != -1) {
            selectors3.splice(index3, 1);
          
        }
        if (index4 != -1) {
            selectors4.splice(index4, 1);
           
        }
        if (index5 != -1) {
            selectors5.splice(index5, 1);

           
        }
        if (index6 != -1) {
            selectors6.splice(index6, 1);

        }

      debugger;
          
        IOBreadcrumb.breadcrumbs.splice((indeximage-1), 1)
        
            $.each(selectors1, function (index, value) {
                if (value > indeximage) {
                    selectors1[index] = value - 1;
                }
            });

            $.each(selectors2, function (index, value) {
                if (value > indeximage) {
                    selectors2[index] = value - 1;
                }
            });

            $.each(selectors3, function (index, value) {
                if (value > indeximage) {
                    selectors3[index] = value - 1;
                }
            });

            $.each(selectors4, function (index, value) {
                if (value > indeximage) {
                    selectors4[index] = value - 1;
                }
            });

            $.each(selectors5, function (index, value) {
                if (value > indeximage) {
                    selectors5[index] = value - 1;
                }
            });


            $.each(selectors6, function (index, value) {
                if (value > indeximage) {
                    selectors6[index] = value - 1;
                }
            });


        var myListofArrays = [];
        myListofArrays = [selectors1, selectors2, selectors3, selectors4, selectors5, selectors6];

        $(".docs-toggles label").each(function (index) {
            var x = 'selectors' + (index + 1);
            $(this).attr('data-selected', myListofArrays[index]);

        });


        $(".thumbnails .col-md-2:eq(0) img").dblclick();
        //$(".fa-chevron-circle-left").click();

        if ($(".thumbnails .col-md-2").length<=0) {

            $(".cropper-view-box > img").attr('src','');
            $('.cropper-canvas> img').attr('src','');
            $("#image-preview_dragdrop-container").css("display", "block");
        }
        //$('.cropit-image-preview').css('background-image', '');

        //$(".cropper-canvas img").attr("src", src).remove();
        //$(".cropper-view-box img").attr("src", src).remove();
        $("#deleteModal").modal('hide');

        
    });
});


// check it 


//$('.append-item').change(function (event) {
//    $(".cropper-container").css("display", "block");
//    $("#image-preview_dragdrop-container").css("display", "none");

//    var newpath = "";
//    var file, img;
//    if ((file = this.files[0])) {
//        img = new Image();
//        img.onload = function () {
//            alert(this.width + " " + this.height);
//        };
//        img.onerror = function () {
//            alert("not a valid file: " + file.type);
//        };
//    }
//    var tmppath = URL.createObjectURL(event.target.files[0]);
//    if (tmppath != "") {
//        newpath = tmppath.split('/')[3];
//    }
//    if ($('.selectedImgDiv:hidden').length == 0) {
//        $('.selectedImg').append('<div class="col-md-2 selectedImgDiv clearfix"><img id="inputImage2" class="img-fluid" ondblclick="test(this);" alt="Picture"><p><input id="number" type="number" min="1" value="1"></p></div>');
//        $(".selectedImg .selectedImgDiv:last-child img").attr('src', tmppath);

//        $(".thumbnails .col-md-2 img").removeClass("selected");
//        $(".thumbnails .col-md-2:last img").addClass("selected");
//        var this_src = $(".thumbnails .col-md-2:last img").attr("src");
//        $(".default-img").attr("src", this_src);
//        $(".cropper-canvas img").attr("src", this_src);
//        $(".cropper-view-box img").attr("src", this_src);

//        var items = $(".thumbnails .col-md-2").length;
//        if (items > 0) {
//            var index = $(".selected").closest(".col-md-2").index() - 1;

//        } else {
//            var index = -1;
//        }
//        var count = $(".numbers").text();
//        count++;
//        $(".numbers").text(count)
//        $(".thumbnails .col-md-2:eq(" + index + ") img").addClass("selected");
//    } else {

//        $('.selectedImgDiv:hidden img').attr("src", tmppath);
//        $(".default-img").attr("src", tmppath);
//        $(".cropper-canvas img").attr("src", tmppath);
//        $(".cropper-view-box img").attr("src", tmppath);
//        $('.selectedImgDiv:hidden').css("display", "block");
//    }
//});

