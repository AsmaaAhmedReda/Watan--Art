

window.onload = function () {
    'use strict';
    var Cropper = window.Cropper;
    var URL = window.URL || window.webkitURL;
    var container = document.querySelector('.img-container');
    var image = container.getElementsByTagName('img').item(0);
    var download = document.getElementById('download');
    var actions = document.getElementById('actions');
    var dataX = document.getElementById('dataX');
    var dataY = document.getElementById('dataY');
    var dataHeight = document.getElementById('dataHeight');
    var dataWidth = document.getElementById('dataWidth');
    var dataRotate = document.getElementById('dataRotate');
    var dataScaleX = document.getElementById('dataScaleX');
    var dataScaleY = document.getElementById('dataScaleY');
  

    var options = {
        aspectRatio: 8 / 8,
        preview: '.img-preview',
        ready: function (e) {
            console.log(e.type);
            //.log(c1);
        },
        cropstart: function (e) {
            console.log(e.type, e.detail.action);
            //console.log(c2);
        },
        cropmove: function (e) {
            console.log(e.type, e.detail.action);
            //console.log(c3);
        },
        cropend: function (e) {
            console.log(e.type);
            console.log("cropend");
            //console.log(c4);
        },
        crop: function (e) {
            var data = e.detail;

            console.log(e.type);
            dataX.value = Math.round(data.x);
            dataY.value = Math.round(data.y);
            dataHeight.value = Math.round(data.height);
            dataWidth.value = Math.round(data.width);
            dataRotate.value = typeof data.rotate !== 'undefined' ? data.rotate : '';
            dataScaleX.value = typeof data.scaleX !== 'undefined' ? data.scaleX : '';
            dataScaleY.value = typeof data.scaleY !== 'undefined' ? data.scaleY : '';
            debugger;
            //console.log("c5" + Math.round(data.x) + "_" + Math.round(data.y));
            var indeximage = $(".thumbnails .selected").closest(".col-md-2").index();
            if (IOBreadcrumb.breadcrumbs.length >= indeximage) {
                var resultout = cropper['getCroppedCanvas'](data.option, data.secondOption);
                var indeximage = $(".thumbnails .selected").closest(".col-md-2").index();
                IOBreadcrumb.breadcrumbs[indeximage - 1].croppedimage = resultout.toDataURL(uploadedImageType);
            }
        },
        zoom: function (e) {
            console.log(e.type, e.detail.ratio);
            var data = e.detail;
            //var indeximage = $(".thumbnails .selected").closest(".col-md-2").index();
            //if (IOBreadcrumb.breadcrumbs.length >= indeximage) {
            //    var resultout = cropper['getCroppedCanvas'](data.option, data.secondOption);
            //    var indeximage = $(".thumbnails .selected").closest(".col-md-2").index();
            //    IOBreadcrumb.breadcrumbs[indeximage - 1].croppedimage = resultout.toDataURL(uploadedImageType);
            //}

        }

    };
    var cropper = new Cropper(image, options);
    var originalImageURL = image.src;
    var uploadedImageType = 'image/jpeg';
    var uploadedImageName = 'cropped.jpg';
    var uploadedImageURL;

    // Tooltip
    $('[data-toggle="tooltip"]').tooltip();

    // Buttons
    if (!document.createElement('canvas').getContext) {
        $('button[data-method="getCroppedCanvas"]').prop('disabled', true);
    }

    if (typeof document.createElement('cropper').style.transition === 'undefined') {
        $('button[data-method="rotate"]').prop('disabled', true);
        $('button[data-method="scale"]').prop('disabled', true);
    }

    // Download
    if (typeof download.download === 'undefined') {
        download.className += ' disabled';
    }

    // Options
    actions.querySelector('.docs-toggles').onchange = function (event) {
        //debugger;
        var e = event || window.event;
        var target = e.target || e.srcElement;
        var cropBoxData;
        var canvasData;
        var isCheckbox;
        var isRadio;

        if (!cropper) {
            return;
        }

        if (target.tagName.toLowerCase() === 'label') {
            target = target.querySelector('input');
        }
        //debugger;
        isCheckbox = target.type === 'checkbox';
        isRadio = target.type === 'radio';

        if (isCheckbox || isRadio) {
            if (isCheckbox) {
                options[target.name] = target.checked;
                cropBoxData = cropper.getCropBoxData();
                canvasData = cropper.getCanvasData();

                options.ready = function () {
                    console.log('ready');
                    cropper.setCropBoxData(cropBoxData).setCanvasData(canvasData);
                };
            } else {
                options[target.name] = target.value;
                options.ready = function () {
                    console.log('');
                };
             
                //if (canvasData != null && canvasData.left != null) {
                //    debugger;
                //    cropper.move(0, 0);
                //}
               
            }

            
            // Restart
            cropper.destroy();
            cropper = new Cropper(image, options, {
                ready() {
                    // this.cropper[method](argument1, , argument2, ..., argumentN);
                    this.cropper.move(-1, 0);

                    
                }

            });
            //console.log('startnew');
            //
            
            //var checkExist = setInterval(function () {
            //    if ($('#the-canvas').length) {
            //        console.log("Exists!");
            //        clearInterval(checkExist);
            //    }
            //}, 100); // check every 100ms
          

            console.log('startnew');

           

            //var resultout = cropper['getCroppedCanvas'](options);
            //var indeximage = $(".thumbnails .selected").closest(".col-md-2").index();
            //IOBreadcrumb.breadcrumbs[indeximage - 1].croppedimage = resultout.toDataURL(uploadedImageType);


        }
    };

    // Methods
    actions.querySelector('.docs-buttons').onclick = function (event) {

        //debugger;
        var e = event || window.event;
        var target = e.target || e.srcElement;
        var cropped;
        var result;
        var input;
        var data;

        if (!cropper) {
            return;
        }

        while (target !== this) {
            if (target.getAttribute('data-method')) {
                break;
            }

            target = target.parentNode;
        }

        if (target === this || target.disabled || target.className.indexOf('disabled') > -1) {
            return;
        }

        data = {
            method: target.getAttribute('data-method'),
            target: target.getAttribute('data-target'),
            option: target.getAttribute('data-option') || undefined,
            secondOption: target.getAttribute('data-second-option') || undefined
        };

        cropped = cropper.cropped;

        if (data.method) {
            if (typeof data.target !== 'undefined') {
                input = document.querySelector(data.target);

                if (!target.hasAttribute('data-option') && data.target && input) {
                    try {
                        data.option = JSON.parse(input.value);
                        //console.log(JSON.parse(input.value));
                    } catch (e) {
                        console.log(e.message);
                    }
                }
            }

            switch (data.method) {
                case 'rotate':
                    if (cropped && options.viewMode > 0) {
                        cropper.clear();
                    }

                    break;

                case 'getCroppedCanvas':
                    try {
                        data.option = JSON.parse(data.option);
                    } catch (e) {
                        console.log(e.message);
                    }

                    if (uploadedImageType === 'image/jpeg') {
                        if (!data.option) {
                            data.option = {};
                        }

                        data.option.fillColor = '#fff';
                    }
                    break;


                //case 'setolddata':
                //    //debugger;
                //    cropper.setimageolddata();

                //    break;
            }

            result = cropper[data.method](data.option, data.secondOption);

            switch (data.method) {
                case 'rotate':
                    if (cropped && options.viewMode > 0) {
                        cropper.crop();
                    }

                    break;

                case 'scaleX':
                case 'scaleY':
                    target.setAttribute('data-option', -data.option);
                    break;

                case 'getCroppedCanvas':
                    if (result) {
                        // Bootstrap's Modal
                        //$('#getCroppedCanvasModal').modal().find('.modal-body').html(result);
                        //var indeximage = $(".thumbnails .selected").closest(".col-md-2").index();
                        if (!download.disabled) {
                            download.download = uploadedImageName;
                            download.href = result.toDataURL(uploadedImageType);
                            //IOBreadcrumb.breadcrumbs[indeximage - 1].croppedimage = result.toDataURL(uploadedImageType);
                        }
                    }

                    break;


             




                case 'destroy':
                    cropper = null;

                    if (uploadedImageURL) {
                        URL.revokeObjectURL(uploadedImageURL);
                        uploadedImageURL = '';
                        image.src = originalImageURL;
                    }
                    break;
            }
            //var base64Content = $('#download').attr('href').split('base64,')[1];
            ////debugger;
            //var src = $('.selected').attr('src').split('/')[3];
            //var yy = $("#" + src).text();
            //$("#" + src).text(base64Content);
            //var mm = $("#" + src).text();
            //$('#getCroppedCanvasModal').modal('hide');
            //if (typeof result === 'object' && result !== cropper && input) {
            //    try {
            //        input.value = JSON.stringify(result);
            //    } catch (e) {
            //        console.log(e.message);
            //    }
            //}
        }
    };

    document.body.onkeydown = function (event) {
        var e = event || window.event;
        if (!cropper || this.scrollTop > 300) {
            return;
        }
        //debugger;
        switch (e.keyCode) {
            case 37:
                e.preventDefault();
                cropper.move(-1, 0);
                break;

            case 38:
                e.preventDefault();
                cropper.move(0, -1);
                break;

            case 39:
                e.preventDefault();
                cropper.move(1, 0);
                break;

            case 40:
                e.preventDefault();
                cropper.move(0, 1);
                break;
        }
    };
    //////////////////////////
   var promises = [];

function loadImage(img) {

	return new Promise(function(resolve, reject) {
    
    var reader = new FileReader();
    var validteimage = $('#imagevalid').val();
    reader.onload = function (e) {
        debugger;
        var image3 = new Image();
        image3.src = e.target.result;

        image3.onload = function () {

            //console.log("imageobj"+image3);
           // window.alert(image3.width);

            var width = image3.naturalWidth, height = image3.naturalHeight;

           // console.log("newwidth" + width);
            if (width < 600 || height < 600) {
                //return false;
                Show_Alert('Confirmation', 'info', validteimage, true, 'Ok', '', false, '', '', false, '', '', null, null, null);
            }

            else {
           
              
            

                image.src = uploadedImageURL = e.target.result;//uploadedImageURL = URL.createObjectURL(file);
                $('.selectedImg').append('<div class="col-md-2 selectedImgDiv clearfix"><img  id=Image_' + newpath + '  class="img-fluid" ondblclick="test(this);" alt="Picture"><p><input class="patternNumber" id="number_' + newpath + '" type="number" min="1" value="1"></p> <textarea class="makeorder_imageUploaded" id="' + newpath + '" style="display:none;"></textarea><span id="dimemnsion_' + newpath + '"></span> <span class="patternid" id="patternId_' + newpath + '"></span> </div>');
                $(".selectedImg .selectedImgDiv:last-child img").attr('src', uploadedImageURL);
                $(".thumbnails .col-md-2 img").removeClass("selected");
                $(".thumbnails .col-md-2:last img").addClass("selected");

                               

                $(".cropper-container").css("display", "block");
                $("#image-preview_dragdrop-container").css("display", "none");
                $(".cropper-canvas img").attr("src", uploadedImageURL);
                $(".cropper-view-box img").attr("src", uploadedImageURL);
                var items2 = $(".thumbnails .col-md-2").length;
                if (items2 > 0) {
                    var index2 = $(".selected").closest(".col-md-2").index() - 1;

                } else {
                    var index2 = -1;
                }
                $(".thumbnails .col-md-2:eq(" + index2 + ") img").addClass("selected");
                $('.thumbnails .col-md-2:eq(' + index2 + ') img').dblclick();
                              

                                

                var   patrenID = "";
                if ((width < 1000 || height< 1000) && (width > 600 || height > 600)) {
                    patrenID="aspectRatio1";
                }
                else if (width / height == 1) {
                                                
                    patrenID="aspectRatio1";
                }
                else if (height / width > 1) {
                                                  
                    patrenID="aspectRatio2";
                }
                else if (height / width < 1) {
                                    
                    patrenID="aspectRatio3";
                }
                var can = "";
                var patrenIDOBJ = patrenID;
                var zoomvalue = 1;
                var rotate = 0
                //var object = { image: imagesrc, crop: cropimage, patern: patrenIDOBJ, can: can, zomm: cropBox.getElementsByClassName('zoom-slider')[0].value }
                //console.log("object" + JSON.stringify(object));

                IOBreadcrumb.add(uploadedImageURL, uploadedImageURL, patrenIDOBJ, can, zoomvalue, rotate,1);
                              
                //cropper.move(-1, 0);
                //cropper.move(1, 0);
                var newpath = "";
                $("#btnUploadImage").show();

                if ((width < 1000 || height < 1000) && (width > 600 || height > 600)) {
                    $("#aspectRatio1").click();
                }
                else if (width / height == 1) {
                    $("#aspectRatio1").click();
                }
                else if (height / width > 1) {
                    $("#aspectRatio2").click();
                }
                else if (height / width < 1) {
                    $("#aspectRatio3").click();
                }
                inputImage.value = null;
           
                               
                cropper.destroy();
                resolve(cropper = new Cropper(image, options));
                                   
                //resolve(e.target.result);
            }
        }
    }
   	
  	reader.readAsDataURL(img);
 
  });
}
    // Import image
    //document.getElementById('inputImage').addEventListener('onchange', updatecrop);
    var inputImage = document.getElementById('inputImage');
  
    if (URL) {
        inputImage.onchange = function () {
            var files = this.files;
            //var file;
            if (cropper && files && files.length > 0) {

                for (var i = 0; i < files.length; i++) {

                    (function () {
                        var ii = i;
                        //alert(ii);
                        setTimeout(function () {
                            var file = files[ii];
                            if (/^image\/\w+/.test(file.type)) {
                                uploadedImageType = file.type;
                                uploadedImageName = file.name;

                                if (uploadedImageURL) {
                                    URL.revokeObjectURL(uploadedImageURL);
                                }

                                promises.push(loadImage(file));
                                //   var img2 = new Image();
                                //   img2.src = window.URL.createObjectURL(file);
                                //   img2.onload = function () {
                                //       var width = img2.naturalWidth, height = img2.naturalHeight;
                                //       if (width < 600 || height < 600) {
                                //           return false;
                                //       }
                                //       else {
                                //           //if (ii == 0) {
                                //           //    cropper.destroy();
                                //           //    cropper = new Cropper(image, options);
                                //           //}

                                          


                                
                             


                                //   };
                            } else {
                                window.alert('Please choose an image file.');
                            }
                        }, 1000);
                    })();
                }
            }



                for (var j = 0; j < promises.length; j++) {
                    promises[j]
                      .then(function (src, index) {


                         

                          
                          //$('#div-result').append($('<img>').attr('src', src).css('width', '50px'));
                          // you can not do this j will always be 4 here
                          // $('#div-info').append('image #'+j+' loaded<br>'); 
                          // do any business related to this image after load

                          //image.src = uploadedImageURL = src
                          //$('.selectedImg').append('<div class="col-md-2 selectedImgDiv clearfix"><img  id=Image_' + newpath + '  class="img-fluid" ondblclick="test(this);" alt="Picture"><p><input class="patternNumber" id="number_' + newpath + '" type="number" min="1" value="1"></p> <textarea class="makeorder_imageUploaded" id="' + newpath + '" style="display:none;"></textarea><span id="dimemnsion_' + newpath + '"></span> <span class="patternid" id="patternId_' + newpath + '"></span> </div>');
                          //$(".selectedImg .selectedImgDiv:last-child img").attr('src', uploadedImageURL);
                          //$(".thumbnails .col-md-2 img").removeClass("selected");
                          //$(".thumbnails .col-md-2:last img").addClass("selected");



                          //$(".cropper-container").css("display", "block");
                          //$("#image-preview_dragdrop-container").css("display", "none");
                          //$(".cropper-canvas img").attr("src", uploadedImageURL);
                          //$(".cropper-view-box img").attr("src", uploadedImageURL);
                          //var items2 = $(".thumbnails .col-md-2").length;
                          //if (items2 > 0) {
                          //    var index2 = $(".selected").closest(".col-md-2").index() - 1;

                          //} else {
                          //    var index2 = -1;
                          //}
                          //$(".thumbnails .col-md-2:eq(" + index2 + ") img").addClass("selected");
                          //$('.thumbnails .col-md-2:eq(' + index2 + ') img').dblclick();




                          //var patrenID = "";
                          //if ((width < 1000 || height < 1000) && (width > 600 || height > 600)) {
                          //    patrenID = "aspectRatio1";
                          //}
                          //else if (width / height == 1) {

                          //    patrenID = "aspectRatio1";
                          //}
                          //else if (height / width > 1) {

                          //    patrenID = "aspectRatio2";
                          //}
                          //else if (height / width < 1) {

                          //    patrenID = "aspectRatio3";
                          //}
                          //var can = "";
                          //var patrenIDOBJ = patrenID;
                          //var zoomvalue = 1;
                          //var rotate = 0
                          ////var object = { image: imagesrc, crop: cropimage, patern: patrenIDOBJ, can: can, zomm: cropBox.getElementsByClassName('zoom-slider')[0].value }
                          ////console.log("object" + JSON.stringify(object));

                          //IOBreadcrumb.add(uploadedImageURL, uploadedImageURL, patrenIDOBJ, can, zoomvalue, rotate, 1);

                          ////cropper.move(-1, 0);
                          ////cropper.move(1, 0);
                          //var newpath = "";
                          //$("#btnUploadImage").show();

                          //if ((width < 1000 || height < 1000) && (width > 600 || height > 600)) {
                          //    $("#aspectRatio1").click();
                          //}
                          //else if (width / height == 1) {
                          //    $("#aspectRatio1").click();
                          //}
                          //else if (height / width > 1) {
                          //    $("#aspectRatio2").click();
                          //}
                          //else if (height / width < 1) {
                          //    $("#aspectRatio3").click();
                          //}
                          //inputImage.value = null;
                          ////$(".btunratio label").mousedown();


                          //if (ii == 0) {

                          //} else {
                          //    //$(".cropedbutton2").click();
                          //}
                      
                          myFunctionofupdate34();

                      })
                     
                      .catch(function(err){
                          // log error
                      });


            }

            
        }
            
       
    } else {
        inputImage.disabled = true;
        inputImage.parentNode.className += ' disabled';
    }
    var inputImage2 = document.getElementById('inputFileToLoad_makeOrder');

    if (URL) {
        inputImage2.onchange = function () {
            var files = this.files;
            //var file;
            if (cropper && files && files.length > 0) {

                for (var i = 0; i < files.length; i++) {

                    (function () {
                        var ii = i;
                        //alert(ii);
                        setTimeout(function () {
                            var file = files[ii];
                            if (/^image\/\w+/.test(file.type)) {
                                uploadedImageType = file.type;
                                uploadedImageName = file.name;

                                if (uploadedImageURL) {
                                    URL.revokeObjectURL(uploadedImageURL);
                                }

                                promises.push(loadImage(file));
                                //   var img2 = new Image();
                                //   img2.src = window.URL.createObjectURL(file);
                                //   img2.onload = function () {
                                //       var width = img2.naturalWidth, height = img2.naturalHeight;
                                //       if (width < 600 || height < 600) {
                                //           return false;
                                //       }
                                //       else {
                                //           //if (ii == 0) {
                                //           //    cropper.destroy();
                                //           //    cropper = new Cropper(image, options);
                                //           //}








                                //   };
                            } else {
                                window.alert('Please choose an image file.');
                            }
                        }, 1000);
                    })();
                }
            }



            for (var j = 0; j < promises.length; j++) {
                promises[j]
                  .then(function (src, index) {
                      //$('#div-result').append($('<img>').attr('src', src).css('width', '50px'));
                      // you can not do this j will always be 4 here
                      // $('#div-info').append('image #'+j+' loaded<br>'); 
                      // do any business related to this image after load

                      //image.src = uploadedImageURL = src
                      //$('.selectedImg').append('<div class="col-md-2 selectedImgDiv clearfix"><img  id=Image_' + newpath + '  class="img-fluid" ondblclick="test(this);" alt="Picture"><p><input class="patternNumber" id="number_' + newpath + '" type="number" min="1" value="1"></p> <textarea class="makeorder_imageUploaded" id="' + newpath + '" style="display:none;"></textarea><span id="dimemnsion_' + newpath + '"></span> <span class="patternid" id="patternId_' + newpath + '"></span> </div>');
                      //$(".selectedImg .selectedImgDiv:last-child img").attr('src', uploadedImageURL);
                      //$(".thumbnails .col-md-2 img").removeClass("selected");
                      //$(".thumbnails .col-md-2:last img").addClass("selected");



                      //$(".cropper-container").css("display", "block");
                      //$("#image-preview_dragdrop-container").css("display", "none");
                      //$(".cropper-canvas img").attr("src", uploadedImageURL);
                      //$(".cropper-view-box img").attr("src", uploadedImageURL);
                      //var items2 = $(".thumbnails .col-md-2").length;
                      //if (items2 > 0) {
                      //    var index2 = $(".selected").closest(".col-md-2").index() - 1;

                      //} else {
                      //    var index2 = -1;
                      //}
                      //$(".thumbnails .col-md-2:eq(" + index2 + ") img").addClass("selected");
                      //$('.thumbnails .col-md-2:eq(' + index2 + ') img').dblclick();




                      //var patrenID = "";
                      //if ((width < 1000 || height < 1000) && (width > 600 || height > 600)) {
                      //    patrenID = "aspectRatio1";
                      //}
                      //else if (width / height == 1) {

                      //    patrenID = "aspectRatio1";
                      //}
                      //else if (height / width > 1) {

                      //    patrenID = "aspectRatio2";
                      //}
                      //else if (height / width < 1) {

                      //    patrenID = "aspectRatio3";
                      //}
                      //var can = "";
                      //var patrenIDOBJ = patrenID;
                      //var zoomvalue = 1;
                      //var rotate = 0
                      ////var object = { image: imagesrc, crop: cropimage, patern: patrenIDOBJ, can: can, zomm: cropBox.getElementsByClassName('zoom-slider')[0].value }
                      ////console.log("object" + JSON.stringify(object));

                      //IOBreadcrumb.add(uploadedImageURL, uploadedImageURL, patrenIDOBJ, can, zoomvalue, rotate, 1);

                      ////cropper.move(-1, 0);
                      ////cropper.move(1, 0);
                      //var newpath = "";
                      //$("#btnUploadImage").show();

                      //if ((width < 1000 || height < 1000) && (width > 600 || height > 600)) {
                      //    $("#aspectRatio1").click();
                      //}
                      //else if (width / height == 1) {
                      //    $("#aspectRatio1").click();
                      //}
                      //else if (height / width > 1) {
                      //    $("#aspectRatio2").click();
                      //}
                      //else if (height / width < 1) {
                      //    $("#aspectRatio3").click();
                      //}
                      //inputImage.value = null;
                      ////$(".btunratio label").mousedown();


                      //if (ii == 0) {

                      //} else {
                      //    //$(".cropedbutton2").click();
                      //}
                      myFunctionofupdate34();

                  })

                  .catch(function (err) {
                      // log error
                  });


            }


        }


    } else {
        inputImage2.disabled = true;
        inputImage2.parentNode.className += ' disabled';
    }

};



//inputImage

//function updatecrop() {
//    var count = $(".thumbnails .col-md-2").length;
//    for (var i = 0; i < count; i++) {
//         (function () {
//            var ii = i;
//            setTimeout(function () {
//           $(".thumbnails .col-md-2 img").removeClass("selected");
//           $(".thumbnails .col-md-2:eq(" + ii + ") img").addClass("selected");
//           $('.thumbnails .col-md-2:eq(' + ii + ') img').dblclick();
//             setTimeout(myFunctionofupdate34, 5000)
//            }, 500);
//         })();
//    }
   
//}
//function myFunctionofupdate33() {

   
//}

function myFunctionofupdate34() {
    debugger;

   

    $(".cropedbutton2").click();
}

$(document).ready(function () {
 $('#getCroppedCanvasModal').modal('hide');
});
//----------------------------------------------------------------------------------

//$('.docs-buttons3').on('click', '[data-method]', function () {
//    var $this = $(this);
//    var data = $this.data();
//    var $target;
//    var result;

//    var $image = $('#image');
//    if ($this.prop('disabled') || $this.hasClass('disabled')) {
//        return;
//    }

//    if ($image.data('cropper') && data.method) {
//        data = $.extend({}, data); // Clone a new one

//        if (typeof data.target !== 'undefined') {
//            $target = $(data.target);

//            if (typeof data.option === 'undefined') {
//                try {
//                    data.option = JSON.parse($target.val());
//                } catch (e) {
//                    console.log(e.message);
//                }
//            }
//        }

//        if (data.method === 'rotate') {
//            $image.cropper('clear');
//        }

//        result = $image.cropper(data.method, data.option, data.secondOption);

//        if (data.method === 'rotate') {
//            $image.cropper('crop');
//        }

//        switch (data.method) {
//            case 'scaleX':
//            case 'scaleY':
//                $(this).data('option', -data.option);
//                break;

//            case 'getCroppedCanvas':
//                if (result) {
//                    $('#getCroppedCanvasModal').modal('hide');
//                    // Bootstrap's Modal
//                    //$('#getCroppedCanvasModal').modal().find('.modal-body').html(result);
//                    if (!$download.hasClass('disabled')) {
//                        $download.attr('href', result.toDataURL('image/jpeg'));
//                    }
//                }

//                break;
//        }
//        if ($.isPlainObject(result) && $target) {
//            try {
//                $target.val(JSON.stringify(result));
//            } catch (e) {
//                console.log(e.message);
//            }
//        }
//        alert($('#download').attr('href'));
//    }
//});
