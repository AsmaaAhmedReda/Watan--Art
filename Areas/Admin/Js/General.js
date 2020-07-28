/// <reference path="../Views/login/login.cshtml" />
// For Ajax call General 
var table;
var upload_folder = "";
var UploadDisplay_Folder = "/UploadedImages/";
var SiteURL = "";

//$.ajax({
//    beforeSend: function () {

//       $('.loading-container')
//                .removeClass('loading-inactive');
//    },
//    complete: function () {

//    }
//    // ......
//});


/*Loading*/
$(window)
    .load(function () {
        setTimeout(function () {

            $('.loading-container')
                .addClass('loading-inactive');
        }, 300);
    });

//function myLoadFunction() {
//     setTimeout(function () {
//        $('.loading-container')
//            .addClass('loading-inactive');
//    }, 2000);
//}

//document.getElementById("fileContentVoice").onchange =
$(document).ajaxStart(function () {
    show_Loader();
});

$(document).ajaxComplete(function () {
    hide_Loader();

});
$(document).ajaxSuccess(function () {

    hide_Loader();




});
$(document).ajaxError(function () {
    hide_Loader();

});
//function UploadPhoto(e, file, hiddenImg, ImgScr) {

//    fileUpload = $("#" + file).get(0);
//    if (validateFileUpload(e, fileUpload, imageExtensionType, file)) {
//        uploadFileIn(e, fileUpload, imageExtensionType, 1, file, ImgScr, hiddenImg);
//    }
//    else {
//        Show_Alert('', 'info', "Choose Correct Extension", true, 'Ok', '', false, '', '', false, '', '', null, null, null);
//        //sweetAlert("Choose Correct Extetntion");

//    }

//}
//function UploadVideo(e, file, hiddenImg, ImgScr) {
//     fileUpload = $("#" + file).get(0);
//    if (validateFileUpload(e, fileUpload, vedioExtensionType, file)) {
//        uploadFileIn(e, fileUpload, vedioExtensionType, 2, file, ImgScr, hiddenImg);
//    }
//    else {
//        Show_Alert('', 'info', "Choose Correct Extension", true, 'Ok', '', false, '', '', false, '', '', null, null, null);

//        //sweetAlert("Choose Correct Extetntion");
//    }  
//}
//function uploadFileIn(e, fileUpload, fileType, caseNo, file, ImgScr, HiddenImg) {

//    try {
//        var file_list = e.target.files;
//        for (var i = 0, file; file = file_list[i]; i++) {
//            var sFileName = file.name;
//            var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
//            var iFileSize = file.size;
//            var files = fileUpload.files;
//            var data = new FormData();
//            for (var i = 0; i < files.length; i++) {
//                data.append(files[i].name, files[i]);
//            }
//            callGenericHandlerFileIn(data, file, caseNo, ImgScr, HiddenImg);
//        }
//    }
//    catch (error) {
//        Show_Alert('', 'info', "Sorry, there's Error Accured", true, 'Ok', '', false, '', '', false, '', '', null, null, null);

//        //sweetAlert("@Sama_Alkhaleej.Helper.CommonData.FAIL_MESSAGE");
//        $("#" + file).val("");


//    }


//};
//function callGenericHandlerFileIn(data, file, caseNo, ImgScr, HiddenImg) {
//    var choice = {};
//    choice.url = "/Helper/FileHandler.ashx";
//    choice.type = "POST";
//    choice.data = data;
//    choice.contentType = false;
//    choice.processData = false;
//    choice.success = function (result) {

//        result = _result_call = result.split(split_char)[1];

//        if (result != "-1" && result != "-2") {
//            var imagePath = SiteURL + UploadDisplay_Folder + result;
//            if (caseNo == 1)//img
//            {

//                if (ImgScr != '') {
//                    $("#" + ImgScr).attr("src", imagePath);
//                }

//                $("#" + HiddenImg).val(upload_folder + result);
//                $('#ImageDivValidator').removeClass("has-error");
//            }
//            else if (caseNo == 2)//video
//            {
//                $("#" + HiddenImg).val(upload_folder + result);

//            }
//        }
//        else {
//            Show_Alert('', 'info', "Sorry, there's Error Accured", true, 'Ok', '', false, '', '', false, '', '', null, null, null);

//            //sweetAlert("@Sama_Alkhaleej.Helper.CommonData.FAIL_MESSAGE");


//        }


//    };
//    choice.error = function (error) {
//        Show_Alert('', 'info', "Sorry, there's Error Accured", true, 'Ok', '', false, '', '', false, '', '', null, null, null);

//        //sweetAlert("@Sama_Alkhaleej.Helper.CommonData.FAIL_MESSAGE");


//    };
//    return $.ajax(choice);
//    event.preventDefault();
//}




function CallServer(url, data, successFunction, errorFunction, datatype) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: ((datatype != undefined && datatype != null) ? (datatype) : ("json")),
        async: false,
        success: successFunction,
        error: (errorFunction == null && errorFunction == undefined) ? (function (d) {
            //window.location.href = "/login/login";

        }) : (errorFunction)
    });
}
// For Ajax call return partial view 
function CallServerHTML(url, data, successFunction, errorFunction) {

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: true,
        success: successFunction,
        error: (errorFunction == null && errorFunction == undefined) ? (function (d) {
            //window.location.href = "/login/login";

        }) : (errorFunction)
    });
}

// To Use In Mvc Way
function CallServerMvc(url, data, successFunction, errorFunction) {
    $.ajax({
        url: url,
        data: data,
        async: false,
        success: successFunction,
        error: (errorFunction == null && errorFunction == undefined) ? (function (d) {
            //
            //window.location.href = "/login/login";
        }) : (errorFunction)
    });
}
// To Get Object id //exe GetObject('Txt') like document.getElementById
function GetObject(Id) {
    var obj;
    if (document.getElementById) {
        obj = document.getElementById(Id);
    }
    else if (document.all) {
        obj = document.all[Id];
    }
    else if (document.layers) {
        obj = document.layers[Id];
    }
    return obj;
}

// To Check If Object Null Or Not
function ObjExists(obj) {
    return (obj != null && obj != undefined);
}

// To Set Display none-block exe SetObjDisplay(GetObject('div_name'), 'block');
function SetObjDisplay(obj, displayValue) {
    if (ObjExists(obj)) {
        if (displayValue == 'block') { $(obj).show('slow'); } else { $(obj).hide('slow'); }
        //obj.style.display = displayValue;
    }
}

// To Accept Only Number used in onkeydown
//exe document.getElementById('txt').onkeydown = function(event) {return numbers_only(event);}
function numbers_only(ev, Func) {

    if (Func) {
        if (typeof (Func) == 'function') {
            Func(((document.all) ? (window.event.srcElement) : (ev.currentTarget)));
        }
    }
    var result = true;
    var charCode = (ev.which != undefined || ev.which != null) ? ev.which : event.keyCode

    result = (charCode == 8 || charCode == 46 || (charCode >= 37 && charCode <= 40) || (charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105));
    //Control
    var ctrl = typeof ev.modifiers == 'undefined' ? ev.ctrlKey : ev.modifiers & ev.CONTROL_MASK;
    //Char V
    var v = typeof ev.which == 'undefined' ? ev.keyCode == 86 : ev.which == 86;
    //Char Z
    var z = typeof ev.which == 'undefined' ? ev.keyCode == 90 : ev.which == 90;
    //Char X
    var x = typeof ev.which == 'undefined' ? ev.keyCode == 88 : ev.which == 88;
    //Char C
    var c = typeof ev.which == 'undefined' ? ev.keyCode == 86 : ev.which == 86;
    //Char A
    var a = typeof ev.which == 'undefined' ? ev.keyCode == 65 : ev.which == 65;
    // If the control and 'V' keys are pressed at the same time
    if ((ctrl && v) || (ctrl && z) || (ctrl && x) || (ctrl && c) || (ctrl && a)) {
        result = true;
    }
    if (charCode == 46 || charCode == 8 || charCode == 9) {
        result = true;
    }
    return result;
}


// numbers only with Arabic Escape Sequence, 'ظ', 'ظ'
// document.getElementById('txt').onkeypress = function(event) {return numbers_only_Arabic_Escape_Sequnse(event);}
function numbers_only_Arabic_Escape_Sequnse(e, func) {
    var evt = ((navigator.appName == "Microsoft Internet Explorer") ? (event) : (e));
    var charCode = (evt.which != undefined || evt.which != null) ? evt.which : evt.keyCode
    var x = String.fromCharCode(charCode);
    var charAry = ['ا', 'أ', 'آ', '!', '@', '#', '$', '%', '^', '&', '*', ')', '(', '_', 'ذ', '\\']
    if (charAry.indexOf(x) > 0 || evt.shiftKey == true) {
        return false;
    }
    if (func) {
        return func(evt);
    }
    else {
        return true;
    }

}

//To Accept Only Float Numbers
function float_numbers_only(evt, elementRef) {
    var result = true;
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (!(charCode <= 105 && charCode >= 96)) {
            result = false;
        }
    }
    //Event
    var ev = (document.all) ? (event) : (evt);
    //Control
    var ctrl = typeof ev.modifiers == 'undefined' ? ev.ctrlKey : ev.modifiers & ev.CONTROL_MASK;
    //Char V
    var v = typeof ev.which == 'undefined' ? ev.keyCode == 86 : ev.which == 86;
    //Char Z
    var z = typeof ev.which == 'undefined' ? ev.keyCode == 90 : ev.which == 90;
    //Char X
    var x = typeof ev.which == 'undefined' ? ev.keyCode == 88 : ev.which == 88;
    //Char C
    var c = typeof ev.which == 'undefined' ? ev.keyCode == 86 : ev.which == 86;
    //Char A
    var a = typeof ev.which == 'undefined' ? ev.keyCode == 65 : ev.which == 65;
    // If the control and 'V' keys are pressed at the same time
    if ((ctrl && v) || (ctrl && z) || (ctrl && x) || (ctrl && c) || (ctrl && a)) {
        result = true;
    }
    if (charCode == 46 || charCode == 8 || charCode == 110) {
        result = true;
    }
    // Allow only 1 decimal point ('.')...  
    //if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
    //    result = false;
    //else
    //    result = true;

    return result;
}

// numbers only with only one dot
//onkeypress = "return numericOnly(event,this);"
function numericOnly(event, elementRef) {
    var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;
    if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
        return true;
    }
        // '.' decimal point...  
    else if (keyCodeEntered == 46) {
        // Allow only 1 decimal point ('.')...  
        if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
            return false;
        else
            return true;
    }
    return false;
}


// To Accept Letter only used in Name of person
// document.getElementById('txt').onkeypress = function(event) {return onlyAlphabets(event,this);}
function onlyAlphabets(e, t) {
    var specCharAry = ["’", "‘", "،", "؟", "?", "=", "+", "!", "@", "#", "$", "%", "^", "&", "*", ")", "(", "_", ">", "<", "{", "}", "[", "]", "|", ":", ";", "÷", "؛", '"', "،", ",", ".", "~", "'", '×']
    var evt = ((navigator.appName == "Microsoft Internet Explorer") ? (event) : (e));
    var charCode = (evt.which != undefined || evt.which != null) ? evt.which : evt.keyCode
    var x = String.fromCharCode(charCode);
    try {
        if (window.event) { var charCode = window.event.keyCode; }
        else if (e) { var charCode = e.which; }
        else { return true; }

        if ((charCode > 47 && charCode < 58) || (charCode > 41 && charCode < 45) || charCode == 61 || charCode == 39 || charCode == 91 || charCode == 92 || charCode == 93 || charCode == 96 || charCode == 13 || specCharAry.indexOf(x) > -1)
            return false;
        else
            return true;
    }
    catch (err) {
        return false;
        //alert(err.Description);
    }

}

// document.getElementById('txt').onkeyup = function(event) {CheckEnglishOnly(this);}
// To Make Text Accept Enghlish Charchter
function CheckEnglishOnly(field) {
    var sNewVal = "";
    var sFieldVal = field.value;
    for (var i = 0; i < sFieldVal.length; i++) {
        var ch = sFieldVal.charAt(i);
        var c = ch.charCodeAt(0);
        if (c < 0 || c > 255) {
            // Discard
        }
        else {
            sNewVal += ch;
        }
    }
    field.value = sNewVal;
}

// To Make Text Accept Arabic Charcter
// document.getElementById('txt').onkeyup = function(event) {CheckArabicOnly(this);}
function CheckArabicOnly(field) {
    var sNewVal = "";
    var sFieldVal = field.value;
    for (var i = 0; i < sFieldVal.length; i++) {
        var ch = sFieldVal.charAt(i);;
        var c = ch.charCodeAt(0);
        if ((c < 48 || c > 57) && (c != 32)) {
            if (c < 1536 || c > 1791 && (c < 48 && c > 57)) {
                // Discard
            }
            else {
                sNewVal += ch;
            }
        } else {
            sNewVal += ch;
        }
    }
    field.value = sNewVal;
}

//specCharAry >> Array of Special character Prevent to write in textbox 
//var specCharAry = ["’", "‘", "،", "؟", "?", "=", "+", "!", "@", "#", "$", "%", "^", "&", "*", ")", "(", "_", ">", "<", "{", "}", "[", "]", "|", ":", ";", "÷", "؛", '"', "،", ",", ".", "~", "'", '×', 'ا', 'أ', 'آ']
//GetObject('txt').onpaste = function () {PrventcharonPaste(this, specCharAry);}
// To Prvent Paste Any Special character
function PrventcharonPaste(e/*Element*/, r/*r Restrections*/) {
    var vBeforePaste = e.value, lBeforePaste = vBeforePaste.length;
    var inerv = setInterval(
                function () {
                    if (e.value != '') {
                        o: for (var i = 0; i < r.length; i++) {
                            if (e.value.indexOf(((typeof (r) == "string") ? (r.charAt(i)) : (r[i]))) != -1) {
                                e.value = e.value.replaceAll(r[i], '')
                                continue o;
                            }
                        }
                        clearInterval(inerv);
                    }
                    else {
                        clearInterval(inerv);
                    }
                }
                , 0.1);

}

//document.getElementById('txt').onkeypress = function(event) {specialCharacter(event);}
function specialCharacter(e) {
    var ret;
    var evt = ((navigator.appName == "Microsoft Internet Explorer") ? (event) : (e));
    var charCode = (evt.which != undefined || evt.which != null) ? evt.which : evt.keyCode
    var x = String.fromCharCode(charCode);
    var charAry = ["’", "‘", "،", "؟", "?", '=', '+', '!', '@', '#', '$', '%', '^', '&', '*', ')', '(', '_', "'", ">", "<", "{", "}", "[", "]", "|", ":", ";", "÷", "؛", '"', "،", ",", ".", "~", "'", '×', '/', '-']

    if (charAry.indexOf(x) == -1) {
        ret = true

    }
    else {
        ret = false;
        e.preventDefault();
    }
    return ret;
}

// To Get Type Of Any Variable
function toType(obj) {
    return ({}).toString.call(obj).match(/\s([a-zA-Z]+)/)[1].toLowerCase()
}

// To Convert Number To digit
function pad(num) {
    num = num + '';
    return num.length < 2 ? '0' + num : num;
}

// To Check If Number Is Odd Or Even isEven(2)==true
function isEven(value) {
    if (value % 2 == 0)
        return true;
    else
        return false;
}

// To Write Arabic Number arNum(1)
function arNum(numb) {
    var str = numb.toString();
    var arabic = { "0": '٠', "1": '١', "2": '٢', "3": '٣', "4": '٤', "5": '٥', "6": '٦', "7": '٧', "8": '٨', "9": '٩' };
    var chars = str.split("");
    var newnum = new Array();
    for (var i = 0; i < chars.length; i++) {
        newnum[i] = arabic[chars[i]];
    }
    return newnum.join("");
}

// To Handle Absoulte Path Of Pages
function GetSiteRoot() {

    var path = window.location.pathname;
    if (path.indexOf("/") == 0)
        path = ((path.split('/').length - 1) == 1) ? ("../") : ("../../");
    else
        path = "../";
    return path;
}

// To check If This Permission exist or not 
//if (chkPermisson('cp_activity_add') == false) { GetObject('div_add').innerHTML = "";}
function chkPermisson(permname) {
    var param = false;
    var myvar = getURLParameter('pg');
    if (ObjExists(myvar) == true && permname != "") {
        CallServer((GetSiteRoot() + 'General.asmx/check_permission'), '{pageid:"' + myvar + '",pagename:"' + permname + '"}',
                    function (response) { param = response.d });
    }
    else {
        window.location.href = (GetSiteRoot() + '/Login.aspx');
    }
    return param;
}

function chkallPermisson() {

    var myvar = getURLParameter('pg');
    if (ObjExists(myvar) == true) {
        CallServer((GetSiteRoot() + 'General.asmx/check_all_permission'), '{pageid:"' + myvar + '"}',
                    function (response) {
                        var result = eval("(" + response.d + ")");
                        if (result.res > 0) {
                            param = result.resdata;
                        }
                        else {
                            window.location.href = (GetSiteRoot() + '/Login.aspx');
                        }
                    });
    }
    else {
        window.location.href = (GetSiteRoot() + '/Login.aspx');
    }
    return param;
}

// To Get Paramter From URL
function getURLParameter(name) {
    return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
}

// To Get Page name to set it in title of page
function getpagename() {
    var mypage = ((ObjExists(getURLParameter('pg')) == true) ? (getURLParameter('pg')) : (0));
    var page = "";
    //if ($('#ul_nav li.nav-item').closest('[id=' + mypage + ']').parents('ul.ul_sub').length != 0) {
    //    

    //}
    //else {
    page = $('#ul_nav li.nav-item').closest('[id=' + mypage + ']').find('span.title').html();
    //}
    return page;
}

// to set alert div  //set_error_message('warning', 'لا يوجد بيانات متاحة ', GetObject('div_alert_message'))
function set_error_message(type, message, divid) {
    if (ObjExists(divid) == true) {

        switch (type) {
            case 'success':
                divid.className = "custom-alerts alert alert-success animated fadeIn";
                divid.innerHTML = "<i class='ace-icon fa fa-check-circle green bigger-130'></i> &nbsp; <strong>" + message + "</strong>";
                break;
            case 'info':
                divid.className = "custom-alerts alert alert-info animated fadeIn";
                divid.innerHTML = "<i class='ace-icon fa fa-info-circle blue bigger-130'></i> &nbsp; <strong>" + message + "</strong>";
                break;
            case 'warning':
                divid.className = "custom-alerts alert alert-warning animated fadeIn";
                divid.innerHTML = "<i class='ace-icon fa fa-exclamation-triangle orange bigger-130'></i> &nbsp; <strong>" + message + "</strong>";
                break;
            case 'danger':
                divid.className = "custom-alerts alert alert-danger animated fadeIn";
                divid.innerHTML = "<i class='ace-icon fa fa-times-circle red bigger-130'></i> &nbsp; <strong>" + message + "</strong>";
                break;
            case '':
                divid.className = "";
                divid.innerHTML = "";
                break;
        }


        setTimeout(function () {
            divid.className = "";
            divid.innerHTML = "";

        }, 3500);
    }

}

// popuptitle >> Popup Title >> text
// popuptype >> type of popup >> warning - info - Error - success
// popupbody >> popup body text 
// yesbtndisplay >> To Show Button Yes or not default (true) >> true - false -''
// yesbtntxt >> To Set Text Of Yes Button >> txt - ''
// yesbtncolour >> Color of button yes >> #fffff - ''
// nobtndisplay >> To Show Button No or not default (true)>> true - false -''
// nobtntxt >> To Set Text Of No Button >> txt - ''
// nobtncolor >> Color of button No >> #fffff - ''
// cancelbtndisplay >> To Show Button Cancel or not default (false) >> true - false -''
// cancelbtntxt >> To Set Text Of Cancel Button >> txt - ''
// cancelbtncolor >>  Color of button Cancel >> #fffff - ''
// okfun >> function will fire when press ok delete_fun not delete_fun() must send
// nofun >> function will fire when press No if you want to fire function when press no default >> hide popup
// cancelfun >> function will fire when press Cancel delete_fun not delete_fun() must send if button show
// To Show Popop
function Show_Alert(popuptitle, popuptype, popupbody, yesbtndisplay, yesbtntxt, yesbtncolour, nobtndisplay, nobtntxt, nobtncolor, cancelbtndisplay, cancelbtntxt, cancelbtncolor, okfun, nofun, cancelfun) {
    // To Set Title
    GetObject('div_title').innerHTML = "";
    switch ($.trim(popuptype.toLowerCase())) {

        case "warning":
            GetObject('bs-example-modal-sm').className = 'modal modal-message modal-warning fade';
            GetObject('div_header').innerHTML = "<i class='pe-7s-attention'></i>";

            GetObject('div_title').innerHTML += ($.trim(popuptitle) != "") ? (popuptitle) : ("");
            GetObject('div_modal_body').innerHTML = (($.trim(popupbody) != "") ? (popupbody) : (""));
            GetObject('btn_model_yes').className = (yesbtncolour != "") ? (yesbtncolour) : ('btn btn-warning');

            break;
        case "info":
            GetObject('bs-example-modal-sm').className = 'modal modal-message modal-info fade';
            GetObject('div_header').innerHTML = "<i class='pe-7s-info'></i>";

            GetObject('div_title').innerHTML += ($.trim(popuptitle) != "") ? (popuptitle) : ("");
            GetObject('div_modal_body').innerHTML = (($.trim(popupbody) != "") ? (popupbody) : (""));
            GetObject('btn_model_yes').className = (yesbtncolour != "") ? (yesbtncolour) : ('btn btn-info');

            break;
        case "error":
            GetObject('bs-example-modal-sm').className = 'modal modal-message modal-danger fade';
            GetObject('div_header').innerHTML = "<i class='pe-7s-shield'></i>";

            GetObject('div_title').innerHTML += ($.trim(popuptitle) != "") ? (popuptitle) : ("");
            GetObject('div_modal_body').innerHTML = (($.trim(popupbody) != "") ? (popupbody) : (""));
            GetObject('btn_model_yes').className = (yesbtncolour != "") ? (yesbtncolour) : ('btn btn-danger');

            break;
        case "success":
            GetObject('bs-example-modal-sm').className = 'modal modal-message modal-success fade';
            GetObject('div_header').innerHTML = "<i class='pe-7s-check'></i>";

            GetObject('div_title').innerHTML += ($.trim(popuptitle) != "") ? (popuptitle) : ("");
            GetObject('div_modal_body').innerHTML = (($.trim(popupbody) != "") ? (popupbody) : (""));
            GetObject('btn_model_yes').className = (yesbtncolour != "") ? (yesbtncolour) : ('btn btn-success');

            break;
    }

    // Yes Button
    GetObject('btn_model_yes').style.display = ((yesbtndisplay == true) ? ('inline-block') : ('none'));
    GetObject('btn_model_yes').innerHTML = (yesbtntxt != "") ? ("<i class='fa fa-check'></i>&nbsp; " + yesbtntxt) : ("<i class='fa fa-check'></i>&nbsp; " + "موافق");
    // No Button
    GetObject('btn_model_no').style.display = ((nobtndisplay == true) ? ('inline-block') : ('none'));
    GetObject('btn_model_no').innerHTML = (nobtntxt != "") ? ("<i class='fa fa-times'></i>&nbsp; " + nobtntxt) : ("<i class='fa fa-times'></i>&nbsp; " + "غير موافق");
    GetObject('btn_model_no').className = (nobtncolor != "") ? (nobtncolor) : ('btn btn-grey');
    // Cancel
    GetObject('btn_model_cancel').style.display = ((cancelbtndisplay == true) ? ('inline-block') : ('none'));
    GetObject('btn_model_cancel').innerHTML = (cancelbtntxt != "") ? ("<i class='fa fa-times'></i>&nbsp; " + cancelbtntxt) : ("<i class='fa fa-times'></i>&nbsp; " + "إلغاء");
    GetObject('btn_model_cancel').className = (cancelbtncolor != "") ? (cancelbtncolor) : ('btn btn-primary');

    if (toType(okfun) == 'function') {
        GetObject('btn_model_yes').onclick = function (e) {
            okfun(e);
            $("#bs-example-modal-sm").modal('hide');
        }
    }
    if (toType(nofun) == 'function') {
        GetObject('btn_model_no').onclick = function (e) {
            nofun(e);
        }
    }
    if (toType(cancelfun) == 'function') {
        GetObject('btn_model_cancel').onclick = function (e) {
            cancelfun(e);
            $("#bs-example-modal-sm").modal('hide');
        }
    }

    $("#bs-example-modal-sm").modal('show');
}


// To Bind Dropdownlist with this paramter
// ddl >> dropdownlist which fill data in it 
// alloption >> to show all option or not (choose and all >> pass 0)(choose >> pass 1) (no choose no all >> pass 2)
// id >> if you want ddl bine based on pervious ddl
// id2 >> to use in sphical cases
// flag >> to use in general course - program course
// get data from database
function get_data_for_bind(keytxt, id, id2, flag) {
    var result = "";
    var id_txt = ((id != "" && id != undefined) ? (id) : (0));
    var id2_txt = ((id2 != "" && id2 != undefined) ? (id2) : (0));
    var flag_txt = ((flag != "" && flag != undefined) ? (flag) : (0));
    CallServer(('/Main/Bindddldynamic'), '{key:"' + keytxt + '",id:' + id_txt + ',id2:' + id2_txt + ',flag:' + flag_txt + '}',
                         function (response) {
                             result = response;
                         });
    return result;
}

// append option in ddl
function Bind_ddl(ddl, alloption, keytxt, id, id2, flag, bindFunction) {
    GetObject(ddl).options.length = 0;// Clear All Option In ddl befor binding
    var result = get_data_for_bind(keytxt, id, id2, flag);
    if (result.res == "-5") {
        //session out response to login
        //window.location.href = "/login/login";
    }
        // In Case Exception occur
    else if (result.res == "-1") {
        option = document.createElement("option");
        option.text = "حدث خطأ";
        option.value = '';
        GetObject(ddl).appendChild(option);
    }
    else if (result.res == "0") {
        option = document.createElement("option");
        option.text = "لا يوجد بيانات متاحة";
        option.value = '';
        GetObject(ddl).appendChild(option);
    }
    else {
        // In Case Fill Data In DDL
        if (ObjExists(result.resdata) == true) {
            var res = result.resdata;
            var option;
            if (res.length > 0) {
                // Result more than one && alloption >> 2
                if (res.length != 1 && alloption != 2) {
                    option = document.createElement("option");
                    option.text = "اختر";
                    option.value = '';
                    GetObject(ddl).appendChild(option);
                    if (alloption != undefined && alloption != null) {
                        if (alloption == 0) {
                            option = document.createElement("option");
                            option.text = "الكل";
                            option.value = '0';
                            GetObject(ddl).appendChild(option);
                        }
                    }
                }

                for (var i = 0; i < res.length; i++) {
                    option = document.createElement("option");
                    option.text = ((ObjExists(res[i].name_ar) == true) ? ((res[i].name_ar != "") ? (res[i].name_ar) : ("")) : (""));
                    option.value = ((ObjExists(res[i].id) == true) ? ((res[i].id != "") ? (res[i].id) : (0)) : (0));
                    option.Object = res[i];
                    GetObject(ddl).appendChild(option);
                }

                if (res.length == 1) {
                    if (bindFunction && GetObject(ddl).options[0].value != '')
                        bindFunction();
                }
            }
            else {
                // In Case No data
                option = document.createElement("option");
                option.text = "لا يوجد بيانات متاحة";
                option.value = '';
                GetObject(ddl).appendChild(option)
            }
        }
        else {
            // In Case No data
            option = document.createElement("option");
            option.text = "لا يوجد بيانات متاحة";
            option.value = '';
            GetObject(ddl).appendChild(option)
        }
    }
    GetObject(ddl).style.display = 'none';
}

// Get This Date From Server DB
function get_keysetting_formdb(keyname) {
    var keyvalue = "";
    CallServer((GetSiteRoot() + 'General.asmx/Getkeysetting'), '{key:"' + ((keyname != "") ? (keyname) : ("")) + '"}',
                 function (response) {
                     if (ObjExists(response.d) == true && response.d != "") {
                         keyvalue = response.d;
                     }
                 });
    return keyvalue;
}
// to set loan id in session to use it in pages
function set_applicat_in_session(App_id, dur, app_state_id, link) {
    CallServer((GetSiteRoot() + 'General.asmx/set_session'), '{app_id:' + App_id + ',durid:' + dur + ',app_state:' + app_state_id + '}', function (response) {
        var ret = response.d;
        if (ObjExists(ret) == true) {
            // session OFF - No Perm
            if (ret == -5) {
                window.location.href = (GetSiteRoot() + '/Login.aspx');
            }
            else {
                window.location.href = (GetSiteRoot() + link);
            }
        }
    });
}

// To Show lOADER
function show_Loader() {
    debugger;
    //if ($('.loading-container').hasClass('loading-inactive')) {

    //   // $('#wrapperL').show();
    //} else {
    //    //  $('.spinWrap').show();
    //    setTimeout(function () {
    $('.spinWrap').show();

    //    }, 600);
    //}



    //$('.loading-container').removeClass('loading-inactive');

}

// To hide lOADER
function hide_Loader() {
    //if ($('.loading-container').hasClass('loading-inactive')) {


    //    //setTimeout(function () {
    //    // $('#wrapperL').hide();
    //    $('.spinWrap').hide();
    //}

    $('.spinWrap').hide();

    //}, 500);

    //$('.loading-container').addClass('loading-inactive');

}

// arraytosearch >> array that search in it
// key >> key in array to search by it (name)
// valuetosearch >> value of enter in text
// retrun by array of object that result of search
function functiontofindIndexByKeyValue(arraytosearch, key, valuetosearch) {
    var Array_res = [];
    for (var i = 0; i < arraytosearch.length; i++) {
        if (arraytosearch[i][key].toLowerCase().indexOf(valuetosearch.toLowerCase()) >= 0) {
            Array_res.push(arraytosearch[i]);
        }
    }
    return Array_res;
}

// arr >> array that remove object in it
// attr >> key in array to remove by it (id)
// value >> value which deleted from array
// retrun by array of object after delete object from array
function removeByAttr(arr, attr, value) {
    var i = arr.length;
    while (i--) {
        if (arr[i]
            && arr[i].hasOwnProperty(attr)
            && (arguments.length > 2 && parseInt(arr[i][attr]) === parseInt(value))) {
            arr.splice(i, 1);
        }
    }
    return arr;
}


function RemoveAlphaChars(txt, e) {
    setTimeout(function () {
        var initVal = $.trim($(txt).val());
        outputVal = initVal.replace(/[^0-9]/g, "");
        if (initVal != outputVal)
            $(txt).val(outputVal);
    }, 1);
}

// Speed up calls to hasOwnProperty
var hasOwnProperty = Object.prototype.hasOwnProperty;
//isEmpty(""), // true isEmpty(33), // true (arguably could be a TypeError) isEmpty([]), // true isEmpty({}), // true isEmpty({ length: 0, custom_property: [] }), // true
//isEmpty("Hello"), // false isEmpty([1, 2, 3]), // false isEmpty({ test: 1 }), // false isEmpty({ length: 3, custom_property: [1, 2, 3] }) // false
function isEmpty(obj) {

    // null and undefined are "empty"
    if (obj == null) return true;

    // Assume if it has a length property with a non-zero value
    // that that property is correct.
    if (obj.length > 0) return false;
    if (obj.length === 0) return true;

    // If it isn't an object at this point
    // it is empty, but it can't be anything *but* empty
    // Is it empty?  Depends on your application.
    if (typeof obj !== "object") return true;

    // Otherwise, does it have any properties of its own?
    // Note that this doesn't handle
    // toString and valueOf enumeration bugs in IE < 9
    for (var key in obj) {
        if (hasOwnProperty.call(obj, key)) return false;
    }

    return true;
}
// Get Data Of App From DB
function get_app_data_formdb(app_id, applicantID, get_applicant, get_app, get_app_steps, get_attach, get_conditions, get_interview_questions, get_assessment, get_initial_memebr, get_interview_memebr, get_magles_memebr, get_final_memebr, perm_no) {
    var result = [];
    var page = getURLParameter('pg');
    var myvar = ((ObjExists(page) == true) ? (page) : (0));
    CallServer((GetSiteRoot() + 'General.asmx/Requests_Data_byid'), '{app_id:' + app_id + ',applicant:' + applicantID + ',get_applicant:"' + get_applicant + '",get_app:"' + get_app + '",get_app_steps:"' + get_app_steps + '",get_attach:"' + get_attach + '",get_conditions:' + get_conditions + ',get_interview_questions:' + get_interview_questions + ',get_assessment:' + get_assessment + ',get_initial_memebr:' + get_initial_memebr + ',get_interview_memebr:' + get_interview_memebr + ',get_magles_memebr:' + get_magles_memebr + ',get_final_memebr:' + get_final_memebr + ',perm_no:' + perm_no + ',PID:"' + myvar + '"}',
            function (response) {
                if (ObjExists(response.d) == true && response.d != "") {
                    result = eval("(" + response.d + ")");;
                }
            });
    return result;
}

function Get_Set_To_Data(app_id, acceptstate, user_type) {
    var userType = ((ObjExists(user_type) == true) ? (user_type) : (0));
    CallServer((GetSiteRoot() + 'General.asmx/get_send_to'), '{app_Id:' + app_id + ',accept_state:' + acceptstate + ',usertype:' + userType + '}',
           function (response) {
               if (ObjExists(response.d) == true && response.d != "") {
                   result = eval("(" + response.d + ")");
               }
           });
    return result;

}

//To get Json data from your form
//just send form id 
function ConvertFormToJSON(FormName) {
    try {
        var jsonData = {};
        var formData = $("#" + FormName + "").serializeArray();
        var result = "{";
        $.each(formData, function () {
            var arr = $(this);
            alert(arr[0].name);
            debugger;
            var name = arr[0].name;
            var value = arr[0].value.replace(/'/g, "\\'");
            if (value != "") { result = result + "'" + name + "'" + ":'" + value + "',"; }
        });
        result = result.replace(/(^[,\s]+)|([,\s]+$)/g, '');
        result = result + "}";
        return result;
    }
    catch (err) {
        $('#ErrorMessage').text(err.message);
        $('#Error').show(250);
    }
}

//To get Json data from your form
//just send form id 
function ConvertFormToJSONWithTable(FormName) {
    try {
        var jsonData = {};
        var formData = $("#" + FormName + "").find(".formdata");
        var result = "{";
        $.each(formData, function () {
            var arr = $(this);



            var name = arr[0].id;


            if (arr[0].tagName.toLowerCase() == "table") {
                result = result + "'" + name + "'" + ":['";

                $($("#" + name).find('tbody tr').each(function () {
                    var json = [];
                    var obj = {};
                    var arrayJson = '{';
                    $($(this).find('td input').each(function () {
                        debugger;
                        key = $(this).id;
                        val = $(this).text();
                        //obj[key] = val;
                        //json.push(obj)

                        arrayJson = arrayJson + "'" + key + "':'" + val + "',";
                    }));

                    //arrayJson = JSON.stringify(json).replace('{', '').replace('}', '');

                    result = result + arrayJson + "},"
                    alert(arrayJson);

                }));

                alert(result);
                result = result + "]" + "',";
            }
            else {
                var value = arr[0].value.replace(/'/g, "\\'");


                if (value != "") {
                    result = result + "'" + name + "'" + ":'" + value + "',";
                }
            }
        });
        result = result.replace(/(^[,\s]+)|([,\s]+$)/g, '');
        result = result + "}";
        return result;
    }
    catch (err) {
        $('#ErrorMessage').text(err.message);
        $('#Error').show(250);
    }
}


//To get Dictionary data from your form
//just send form id 
function ConvertFormToDictionary(FormName) {
    try {
        var jsonData = {};
        var formData = $("#" + FormName + "").serializeArray();
        var result = {};
        $.each(formData, function () {
            var arr = $(this);
            var name = '@' + arr[0].name;
            var value = arr[0].value;//.replace(/'/g, "\\'");
            if (value != "") {
                result[name] = value;
            }
            else { result[name] = null; }
        });
        //result = result.replace(/(^[,\s]+)|([,\s]+$)/g, '');
        //result = result .substring(0,result.length-1);
        return result;
    }
    catch (err) {
        $('#ErrorMessage').text(err.message);
        $('#Error').show(250);
    }
}


//Show file in New window 
//send file name and it will open in Show file view
// it calles open_preview_win and getWindowFeatures automatic
function View_att(att) {
    var ext = att.split('.')[1];
    ext = "." + ext;
    var uri = "/Default/ShowFile?";
    uri += "&file=" + att;
    open_preview_win(uri);
    return;
}
function open_preview_win(url) {
    var windowFeatures = getWindowFeatures();
    myWindow = window.open(url, '_blank', ("preview" + Math.floor(Math.random() * 10)), true);
}
function getWindowFeatures() {
    var width = 900;
    var height = 600;
    var left = parseInt((screen.availWidth / 2) - (width / 2));
    var top = parseInt((screen.availHeight / 2) - (height / 2));
    var windowFeatures = "width=" + width + ",height=" + height + ",left=" + left + ",scrollbars=yes,top=" + top + ",screenX=" + left + ",screenY=" + top;
    return windowFeatures;
}

// To Reorder Number Col After Delete Row
// tbody >> name of tbody 
// example >> reorder_num('tbody_selected_activity_view');
function reorder_num(tbody) {
    $("#" + tbody + " tr").each(function () {
        $(this).find('td:first').html((this.rowIndex));
    });
}

// To Call In BeginForm If Error Occur
function ShowPopUpError() {
    Show_Alert('', 'error', 'Error Ocurred ,please try again', true, '', '', false, '', '', false, '', '', function () { }, null, null);
}

//this.value = minmax($.trim(this.value), 1, max);
// value >> value of textbox
// min >> the min of Grid
// max >> The Max Of Grid
function minmax(value, min, max) {
    if ($.trim(value) == "" || parseInt(value) < min || isNaN(parseInt(value)))
        return 1;
    else if (parseInt(value) > max)
        return max;
    else return value;
}

// To Add Days
// new Date(param.mindate).addDays(1)
Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}

// clear div data and remove table selected colors
function clearData(divID) {

    var divs = document.getElementsByClassName(divID);
    for (i = 0; i < divs.length; i++)
    { divs[i].innerHTML = ''; }
    $("tr").removeClass('tbl_row_selected');
    $("td").removeClass('tbl_row_selected');


}

// If you want to draw your charts with Theme colors you must run initiating charts after that current skin is loaded
function ValidateObject(id) {

    var element = $('#' + id);
    if (element != null) {

        if (element.val() != '') {
            element.closest('.form-group').removeClass("has-error");
            return true;
        }


    }
    element.closest('.form-group').addClass("has-error");
    return false

}

function RemoveValidationError(e) {
    // $('form').bootstrapValidator();

    var element = $('#' + e.id);
    element.closest('.form-group').removeClass("has-error");


}

function PreventWritting(event) {

    if (event.keyCode != 8 && event.keyCode != 46) {
        return false;
    }
    else {
        return true;
    }
}






function General_ActiveDective(ID, control, Data, Url, Message, SuccMessage, FailMessage) {

    bootbox.confirm(Message, function (result) {
        debugger;
        if (result) {
            $.ajax({
                Type: "GET",
                url: Url,
                data: Data,
                success: function (data) {

                    if (data == 'Success') {
                        ViewMessagePopup(SuccMessage);
                    }
                    else if (data == 'Error') {
                        ViewMessagePopup(FailMessage);
                    }
                }
            });
        }
        else {
            var value = !$(control).is(':checked');
            $(control).prop('checked', value);


        }
    });
}




$(document).ready(function () {
    $("form").bootstrapValidator();
    LoadEditorHTML("EditorBox");
    pageSetUp();

    var responsiveHelper_datatable_fixed_column = undefined;
    var breakpointDefinition = {
        tablet: 1024,
        phone: 480
    };
    /* COLUMN FILTER  */

    /* TABLETOOLS */
    otable = $('#datatable_tabletools').DataTable({

        // Tabletools options:
        //   https://datatables.net/extensions/tabletools/button_options
        //"sDom": "TBflt<'row DTTTFooter'<'col-sm-6'i><'col-sm-6'p>>",
        "sDom": "TBflt<'row DTTTFooter'<'col-sm-6 col-xs-4 hidden-xs'T><'col-sm-6 col-xs-2 hidden-xs'C>r>" +
              "t" +
              "<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
        "dom": '<"top"f>',
        "oLanguage": { "sSearch": '<a class="searchBtn" id="searchBtn"><i class="fa fa-search"></i></a>' },
        "oTableTools": {
            "aButtons": [
            //"copy",
            //"csv",
            //"xls",
            //   {
            //       "sExtends": "pdf",
            //       "sTitle": "SmartAdmin_PDF",
            //       "sPdfMessage": "SmartAdmin PDF Export",
            //       "sPdfSize": "letter"
            //   },
            //   {
            //       "sExtends": "print",
            //       "sMessage": "Generated by SmartAdmin <i>(press Esc to close)</i>"
            //   }
            ],
            "sSwfPath": "../../assets/js/plugin/datatables/swf/copy_csv_xls_pdf.swf"
        },


        "autoWidth": true,
        "aaSorting": [],
        "preDrawCallback": function () {
            // Initialize the responsive datatables helper once.
            if (!responsiveHelper_datatable_fixed_column) {
                responsiveHelper_datatable_fixed_column = new ResponsiveDatatablesHelper($('#datatable_tabletools'), breakpointDefinition);
                var holder = $("#hid_SearchPlaceHolder").val();
                $("input[type=search]").attr("placeholder", '' + holder + '');
            }
        },
        "rowCallback": function (nRow) {
            responsiveHelper_datatable_fixed_column.createExpandIcon(nRow);
        },
        "drawCallback": function (oSettings) {
            responsiveHelper_datatable_fixed_column.respond();
        },

        buttons: [
                {
                    extend: 'pdfHtml5',
                    orientation: 'landscape',
                    pageSize: 'LEGAL',
                    exportOptions: {
                        columns: ':not(.last)'
                    },
                    customize: function (doc) {
                        doc.defaultStyle.alignment = 'right';
                        doc.styles.tableHeader.alignment = 'right';
                        doc.styles['td'] = {
                            width: '200px',
                            'min-width': '200px'
                        };
                    }
                }, {
                    extend: 'print',
                    exportOptions: {
                        columns: ':not(.last)'
                    }
                },
                {
                    extend: 'excel',
                    exportOptions: {
                        columns: ':not(.last)'
                    }
                }
        ]
    });
    /* END TABLETOOLS */

    // Apply the filter
    $("#datatable_tabletools thead th input[type=text]").on('keyup change', function () {

        otable
            .column($(this).parent().index() + ':visible')
            .search(this.value)
            .draw();

    });
    /* END COLUMN FILTER */


    //slider ata table 


    $("#sliderdatatable_tabletoolsNew").dataTable().fnDestroy();
    var New2 = $("#sliderdatatable_tabletoolsNew");
    New2.dataTable({
        bFilter: false,
        language: {
            aria: {
                sortAscending: ": activate to sort column ascending",
                sortDescending: ": activate to sort column descending"
            },
            emptyTable: "No data available in table",
            info: "Showing _START_ to _END_ of _TOTAL_ records",
            infoEmpty: "No records found",
            infoFiltered: "(filtered1 from _MAX_ total records)",
            lengthMenu: "Show _MENU_",
            search: " Search  :",

            //searching: false,
            zeroRecords: "No matching records found",
            paginate: {
                previous: "Prev",
                next: "Next",
                last: "Last",
                first: "First"
            }
        },
        responsive: {
            details: {
            }
        },

        //bStateSave: !0,
        lengthMenu: [
            [5, 10, 15, 20, 500000],
            [5, 10, 15, 20, "All"]
        ],
        pageLength: 5,
        pagingType: "full_numbers",
        columnDefs: [{
            orderable: !0,
            targets: [0]
        }, {
            searchable: !1,
            targets: [0]
        }, {
            className: "dt-right"
        }],
        order: [
            [0, "asc"]
        ]
    });




    ///


    ///////////////////////// new datatable
    $("#datatable_tabletoolsNew").dataTable().fnDestroy();
    var New = $("#datatable_tabletoolsNew");
    New.dataTable({

        language: {
            aria: {
                sortAscending: ": activate to sort column ascending",
                sortDescending: ": activate to sort column descending"
            },
            emptyTable: "No data available in table",
            info: "Showing _START_ to _END_ of _TOTAL_ records",
            infoEmpty: "No records found",
            infoFiltered: "(filtered1 from _MAX_ total records)",
            lengthMenu: "Show _MENU_",
            search: " Search :",
            zeroRecords: "No matching records found",
            paginate: {
                previous: "Prev",
                next: "Next",
                last: "Last",
                first: "First"
            }
        },
        responsive: {
            details: {
            }
        },
        buttons: [
         {
             extend: 'print', className: 'btn dark btn-outline',
             exportOptions: {
                 columns: ':not(.last)'
             }
             ,
             customize: function (doc) {
                 doc.defaultStyle.alignment = 'right';
                 doc.styles.tableHeader.alignment = 'right';
                 doc.styles['td'] = {
                     width: '200px',
                     'min-width': '200px'
                 };
             }
         },
         {
             extend: 'copy', className: 'btn red btn-outline'
         },
         {
             extend: 'pdf', className: 'btn green btn-outline', orientation: 'landscape',
             pageSize: 'LEGAL',
             exportOptions: {
                 columns: ':not(.last)'
             },
             customize: function (doc) {
                 doc.defaultStyle.alignment = 'right';
                 doc.styles.tableHeader.alignment = 'right';
                 doc.styles['td'] = {
                     width: '200px',
                     'min-width': '200px'
                 };
             }
         },
         {
             extend: 'excel', className: 'btn yellow btn-outline ',
             exportOptions: {
                 columns: ':not(.last)'
             }
         }


        ],
        //bStateSave: !0,
        lengthMenu: [
            [5, 10, 15, 20, 500000],
            [5, 10, 15, 20, "All"]
        ],
        pageLength: 5,
        pagingType: "full_numbers",
        columnDefs: [{
            orderable: !0,
            targets: [0]
        }, {
            searchable: !1,
            targets: [0]
        }, {
            className: "dt-right"
        }],
        order: [
            [0, "asc"]
        ]
    });
    jQuery("#sample_1_wrapper");
    $('#sample_3_tools > li > a.tool-action').on('click', function () {
        var action = $(this).attr('data-action');
        New.DataTable().button(action).trigger();
    });
    New.find(".group-checkable").change(function () {
        var e = jQuery(this).attr("data-set"),
            t = jQuery(this).is(":checked");
        jQuery(e).each(function () {
            t ? ($(this).prop("checked", !0), $(this).parents("tr").addClass("active")) : ($(this).prop("checked", !1), $(this).parents("tr").removeClass("active"))
        })
    }), New.on("change", "tbody tr .checkboxes", function () {
        $(this).parents("tr").toggleClass("active")
    });





    ///////////////////////////////////



});
$(document).on('submit', 'form', function (e) {

    $('form').bootstrapValidator();
    //e.defaultPrevented();
});
function General_DeleteItem(ID, control, Data, Url, Message, SuccMessage, FailMessage) {


    var item = $(control);


    bootbox.confirm(Message, function (result) {
        if (result) {

            $.ajax({
                Type: "GET",
                url: Url,
                data: Data,
                success: function (data) {
                    if (data == 'Success') {

                        var row = otable.row(item.closest('tr'));
                        row.remove().draw(false);



                        //item.closest('tr').remove();
                        var total = $("#TotalRows").text();
                        total = total - 1;
                        $("#TotalRows").text(total);
                        ViewMessagePopup(SuccMessage);


                    }
                    else if (data == 'Error') {
                        ViewMessagePopup(FailMessage);
                    }
                }
            });
        }
        else {
        }
    });
}



// function ConfirmationMessageWebSite(Message)
//{
//         bootbox.confirm(Message, function (result) {
//                if (result) {

//                    $.ajax({
//                        Type: "GET",
//                        url: Url,
//                        data: Data,
//                        success: function (data) {
//                            if (data == 'Success') {
//                                deleteImage();
//                            }
//                            else if (data == 'Error') {
//                                ViewMessagePopup("error");
//                            }
//                        }
//                    });
//                }
//                else {
//                }
//            });

//}

function General_DeleteItemnew(ID, control, Data, Url, Message, SuccMessage, FailMessage) {

  
    var item = $(control);
  

    bootbox.confirm(Message, function (result) {
      
        if (result) {

            $.ajax({
                Type: "GET",
                url: Url,
                data: Data,
                success: function (data) {
                   
                    if (data == 'Success') {
                        
                        //var row = otable.row(item.closest('tr'));
                        //row.remove().draw(false);
                        item.closest('tr').remove();



                        //item.closest('tr').remove();

                        ViewMessagePopup(SuccMessage);


                    }
                    else if (data == 'Error') {
                       
                        ViewMessagePopup(FailMessage);
                    }
                }
            });
        }
        else {
        }
    });
}

function AppendDTRows(dataGet) {
    debugger;

    //alert(dataGet);
    dataGet = dataGet.substring(dataGet.indexOf("<tr>"), dataGet.lastIndexOf("</tr>"));
    if (dataGet != '') {
        dataGet += "</tr>";
        var rows = [];
        rows = dataGet.split("</tr>");
        var obj = {};
        for (i = 0; i < (rows.length) - 1; i++) {
            var rowNoTr = rows[i].replace('<tr>', '');
            rows[i] = rowNoTr;
            var x = i.toString();
            var addedF = "";
            obj["arrayN" + x] = [];
            obj["arrayN" + x].push(rowNoTr);
            var getTd = obj["arrayN" + x].toString().split("</td>");
            obj["arrayN2" + x] = [];
            obj["arrayN3" + x] = [];
            obj["arrayN2" + x] = getTd;
            for (y = 0; y < (obj["arrayN2" + x].length) - 1; y++) {
                var rowNoTd = obj["arrayN2" + x][y].toString().replace('<td>', '');
                if (rowNoTd.includes('<div class="replacedText">')) {

                    var getTEXT = rowNoTd;
                    var getText2 = getTEXT.split(/[#]+/).join('<span>');
                    var getText3 = getText2.split(/[$]+/).join('</span>');
                    rowNoTd = getText3;
                }
                obj["arrayN2" + x][y] = rowNoTd;
                obj["arrayN3" + x].push(rowNoTd);
            }
            otable.row.add(
              obj["arrayN3" + x]
              ).draw(false);
            var info = otable.page.info();
        }

    }

    var CurrentPage = $('#CurrentPage').val();
    var PageNumber = $('#page_count').val();
    if (CurrentPage >= PageNumber) {
        $('#tbl_Next').hide();
    }
    else {
        $('#tbl_Next').show();
    }
}


function General_ReloadTable(e, Method_Link, params) {
    var currentpage = 1;
    if (e != null) {
        var elem = $(e).parent().find('#CurrentPage')
        currentpage = elem.attr('value');
    }
    var currentRows = otable.rows().count();
    params = params + "&currentRows=" + currentRows;
    $.get(Method_Link + currentpage + params, function (data) {
        AppendDTRows(data);
    });
}


function onChange(e) {
    //$("input[type='submit']").removeAttr('disabled');
    //$("input[type='hidden']").remove();

}
function onSelect(e) {
    //$("input[type='submit']").removeAttr('disabled');
    //$("input[type='hidden']").remove();

}
function onExecute(e) {
    //$("input[type='submit']").removeAttr('disabled');
    //$("input[type='hidden']").remove();
}
function onPaste(e) {
    //$("input[type='submit']").removeAttr('disabled');
    //$("input[type='hidden']").remove();
}


function LoadEditorHTML(className) {
    var EditBoxes = document.getElementsByClassName(className);
    for (var i = 0; i < EditBoxes.length; i++) {
        var ModuleDiscAr = $('#' + EditBoxes[i].id),
editor = ModuleDiscAr.data("kendoEditor");
        ModuleDiscAr.kendoEditor({
            encoded: false,
            deserialization: {
                custom: function (html) {
                    return html.replace(/(<\/?)b(\s?)/, "$1strong$2");
                }
            },
            tools: [
                "bold",
                "italic",
                "underline",
                "strikethrough",
                "justifyLeft",
                "justifyCenter",
                "justifyRight",
                "justifyFull",
                "insertUnorderedList",
                "insertOrderedList",
                "indent",
                "outdent",
                "unlink",
                "subscript",
                "superscript",
                "createTable",
                "addRowAbove",
                "addRowBelow",
                "addColumnLeft",
                "addColumnRight",
                "deleteRow",
                "deleteColumn",
                "viewHtml",
                "formatting",
                "cleanFormatting",
                "fontName",
                "fontSize",
                "foreColor",
                "backColor"
            ],
            change: onChange,
            select: onSelect,
            execute: onExecute,
            paste: onPaste
        });

    }

}

