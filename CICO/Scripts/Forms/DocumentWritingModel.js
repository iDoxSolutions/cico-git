function DocumentWritingModel(item) {
    var self = this;
    ko.utils.extend(self, new CicoFormBase(item));
    self.FileUrl = item.item.FileUrl;
    self.FileDesc = item.item.FileDesc;
    self.FormBase = new CicoFormBase(item);
    self.DependentsFiles = ko.observable(item.item.DependentsFiles);
    self.templateName = "DocumentWriting";
    self.submittedFile = ko.observable(item.item.SubmittedFile);
    self.submitDoc = function (e) {
        if (!self.Enabled()) {
            alert("Checklist is completed");
            return false;
        }
        $(".loader").show();
        var options = {
           
            success: function (data) {
                
                try {
                    data = JSON.parse(data);
                    self.submittedFile(data.SubmittedFile);
                    item.ItemChecked(true);
                    item.CssClass(data.CssClass);
                    self.DependentsFiles(data.DependentsFiles);
                    $(".loader").hide();
                }
                catch(x) {
                    alert('Error');
                    $(".loader").hide();
                }

                return false;
            },
            error: function (e) {
                $(".loader").hide();
                alert(e.responseText);
            },
            type: 'post',
            enctype: 'multipart/form-data',
            // other available options: 
            url: "/checklist/UploadFile",   //  ,    // override for form's 'action' attribute 
            data: { itemTemplateId: item.item.Id, checklistId: item.CheckListId },
            dataType: 'text'
        };
        // $(e).ajaxForm(options);
        $("#docSubmitted").ajaxSubmit(options);
        ///e.preventDefault();
        return false;
    };
}