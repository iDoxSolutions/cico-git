function DocumentSubmittedModel(item) {
    var self = this;
    ko.utils.extend(self, new CicoFormBase(item));
    self.FileUrl = item.item.FileUrl;
    self.FileDesc = item.item.FileDesc;
    self.DependentsFiles = ko.observable(item.item.DependentsFiles);
    self.templateName = "DocumentSubmitted";
    self.submittedFile = ko.observable(item.item.SubmittedFile);
    self.submitDoc = function (e) {
        if (!self.Enabled()) {
            alert("Checklist is completed");
            return false;
        }
        $(".loader").show();
        var options = {
            
            //beforeSubmit: showRequest,  // pre-submit callback 
            success: function (data, textStatus, jqXHR) {
                data = JSON.parse(data);
                self.submittedFile(data.SubmittedFile);
                item.ItemChecked(true);
                item.CssClass(data.CssClass);
                self.DependentsFiles(data.DependentsFiles);
                $(".loader").hide();
                return false;
            },
            error:function(e) {
                alert(e.responseText);
                $(".loader").hide();
            },
            type: 'post',
            enctype: 'multipart/form-data',
            // other available options: 
            url: "/checklist/UploadFile",   //  ,    // override for form's 'action' attribute 
            data: { itemTemplateId: item.item.Id },
            dataType:'text'
        };
        // $(e).ajaxForm(options);
        $("#docSubmitted").ajaxSubmit(options);
        ///e.preventDefault();
        return false;
    };
}