
var fail_upload = -1;
var split_char = '#';
var upload_folder = "Upload/";
var wrong_extention = "Sorry, this extension not correct";
var Found_Vote = "Sorry an active Vote is found,Do you want to deactive all but this";
var Deactive_All_Vote = "Do you want to deactive all but this? ";
var is_success;
var _result_call;
var _result_call_no = 0;
var imageExtensionType = 0, docExtensionType = 1, pdfExtensionType = 2, vedioExtensionType = 3, vedioImageExtentionType = 4, allExtensionType = 5;


function uploadFile(e, fileUpload, fileType) {
    try {

        _result_call = 0;
        _result_call_no = 0;
        var file_list = e.target.files;
        for (var i = 0, file; file = file_list[i]; i++) {
            var sFileName = file.name;
            var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
            var iFileSize = file.size;
            if (validateExtentionType(fileType, sFileExtension) && iFileSize < 104857600 && iFileSize > 512) {

                var files = fileUpload.files;
                var data = new FormData();
                for (var i = 0; i < files.length; i++) {
                    data.append(files[i].name, files[i]);
                }
                return $.ajax(callGenericHandlerFile(data));
            }
            else {

                _result_call = wrong_extention;
                _result_call_no = fail_upload;
                return (_result_call);
            }
        }
    }
    catch (error) {
        _result_call = error.status;
        _result_call_no = fail_upload;
        return (_result_call);
    }
};
function callGenericHandlerFile(data) {
    var choice = {};
    choice.url = "/Helpers/FileHandler.ashx";
    choice.type = "POST";
    choice.data = data;
    choice.contentType = false;
    choice.processData = false;
    choice.success = function (result) {
        
        _result_call = result.split(split_char)[1];
        _result_call_no = result.split(split_char)[0];
    };
    choice.error = function (error) {
        
        _result_call = error;
        _result_call_no = fail_upload;
    };
    return $.ajax(choice);
    event.preventDefault();
}
function validateExtentionType(fileType, fileExtention) {
    debugger;
    is_success = false;
    switch (fileType) {
        case 0: {//image
            
            if (fileExtention.toLowerCase() === "png" ||fileExtention.toLowerCase() === "bmp"|| fileExtention.toLowerCase() === "jpg" || fileExtention.toLowerCase() === "jpeg" || fileExtention.toLowerCase() === "gif") {
                is_success = true;
            }

        } break;
        case 1: {//word
            if (fileExtention.toLowerCase() === "doc" || fileExtention.toLowerCase() === "docx") {
                is_success = true;
            }
        } break;
        case 2: {//pdf
            if (fileExtention.toLowerCase() === "pdf") {
                is_success = true;
            }
        } break;
        case 3://voice
            {
                if (fileExtention.toLowerCase() === "m4a" || fileExtention.toLowerCase() === "mp3" || fileExtention.toLowerCase() === "mp4" || fileExtention.toLowerCase() === "wav" || fileExtention.toLowerCase() === "ogv" || fileExtention.toLowerCase() === "webm" || fileExtention.toLowerCase() === "wmv" || fileExtention.toLowerCase() === "3gp" || fileExtention.toLowerCase() === "wmp" || fileExtention.toLowerCase() === "flv" || fileExtention.toLowerCase() === "mov" || fileExtention.toLowerCase() === "swf")
                {
                    is_success = true;
                }
            }
            break;
        case 4: {
            if (fileExtention.toLowerCase() === "png" || fileExtention.toLowerCase() === "bmp" || fileExtention.toLowerCase() === "jpg" || fileExtention.toLowerCase() === "jpeg" || fileExtention.toLowerCase() === "gif" || fileExtention.toLowerCase() === "mp3" || fileExtention.toLowerCase() === "mp4" || fileExtention.toLowerCase() === "ogv" || fileExtention.toLowerCase() === "3gp" || fileExtention.toLowerCase() === "mov" || fileExtention.toLowerCase() === "webm") {
                is_success = true;
            }
        } break;
        case 5:
            {
                is_success = true;
            }
            break;
    }
    return is_success;
}

function validateFileUpload(e, fileUpload, fileType) {
    alert(fileType);
    debugger;
    var file_list = e.target.files;
    for (var i = 0, file; file = file_list[i]; i++) {
        var sFileName = file.name;
        var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
        var iFileSize = file.size;
        //alert(iFileSize);
        if (validateExtentionType(fileType, sFileExtension) && iFileSize < 15485760 && iFileSize > 512) {
            return true;
        }
        else {
            return false;
        }
    }
}