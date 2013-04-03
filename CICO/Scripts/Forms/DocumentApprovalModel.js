function DocumentApprovalModel(item) {
    var self = this;
    ko.utils.extend(self, new CicoFormBase(item));

    self.FormBase = new CicoFormBase(item);
    self.templateName = "DocumentApproval";
    self.FileUrl = item.item.FileUrl;
    self.FileDesc = item.item.FileDesc;
    
    self.ApprovalText = ko.observable(item.item.ApprovalText);
    /*self.docDownloaded = function () {
        $.post('/checklist/check/', { id: item.item.Id },function() {
            //item.ItemChecked(true);
            //window.open(self.FileUrl);
        });
        
    };*/

    self.approve = function() {
        $.post('/checklist/check/', { id: item.item.Id, checklistId: item.CheckListId }, function (data) {
            item.ItemChecked(true);
            item.CssClass(data.CssClass);
        });
    };
    
    this.buttonEnabled = ko.computed(function () {
        return !item.ItemChecked();
    }, this);

}