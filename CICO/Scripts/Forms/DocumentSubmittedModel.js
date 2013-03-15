function DocumentSubmittedModel(item) {
    var self = this;
    self.FileUrl = item.item.FileUrl;
    self.FileDesc = item.item.FileDesc;
    self.FormBase = new CicoFormBase(item);
    self.templateName = "DocumentSubmitted";
    self.submittedFile = ko.observable(item.item.SubmittedFile);
    self.submitDoc = function (e) {
        var options = {
            
            //beforeSubmit: showRequest,  // pre-submit callback 
            success: function(data) {
                self.submittedFile(data);
                item.ItemChecked(true);
                return false;
            },
            error:function(e) {
                alert("File is required");
            },
            type: 'post',
            enctype: 'multipart/form-data',
            // other available options: 
            url: "/checklist/UploadFile",   //  ,    // override for form's 'action' attribute 
            data: { itemTemplateId: item.item.Id }
        };
        // $(e).ajaxForm(options);
        $("#docSubmitted").ajaxSubmit(options);
        ///e.preventDefault();
        return false;
    };
}