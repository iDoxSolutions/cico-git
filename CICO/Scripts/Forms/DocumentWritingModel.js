﻿function DocumentWritingModel(item) {
    var self = this;
    self.FileUrl = item.item.FileUrl;
    self.FileDesc = item.item.FileDesc;
    self.FormBase = new CicoFormBase(item);
    self.templateName = "DocumentWriting";
    self.submittedFile = ko.observable(item.item.SubmittedFile);
    self.submitDoc = function (e) {
        $(".loader").show();
        var options = {

            //beforeSubmit: showRequest,  // pre-submit callback 
            success: function (data) {
                data = JSON.parse(data);
                self.submittedFile(data.SubmittedFile);
                item.ItemChecked(true);
                item.CssClass(data.CssClass);
                $(".loader").hide();
                return false;
            },
            error: function (e) {
                $(".loader").hide();
                alert("File is required");
            },
            type: 'post',
            enctype: 'multipart/form-data',
            // other available options: 
            url: "/checklist/UploadFile",   //  ,    // override for form's 'action' attribute 
            data: { itemTemplateId: item.item.Id },
            dataType: 'text'
        };
        // $(e).ajaxForm(options);
        $("#docSubmitted").ajaxSubmit(options);
        ///e.preventDefault();
        return false;
    };
}