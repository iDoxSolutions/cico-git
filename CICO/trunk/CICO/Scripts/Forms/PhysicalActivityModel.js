function PhysicalActivityModel(item) {
    var self = this;
    ko.utils.extend(self, new CicoFormBase(item));
    self.item = item;
    self.templateName = "PhysicalActivity";
    self.ApprovalText = ko.observable(item.item.ApprovalText);
    self.complete = function () {
        $.post('/checklist/check/', { id: item.item.Id, checklistId: item.CheckListId }, function (data) {
            item.ItemChecked(true);
            item.CssClass(data.CssClass);
            if (data.CompleteChecklist) {
                item.completeChecklist();
            }
        });
    };
    

    this.buttonText = ko.computed(function () {
        return (item.item.ApprovalText==""|| item.item.ApprovalText==null) ?"Complete":"Approve";
    }, this);

    this.buttonEnabled = ko.computed(function () {
        return !item.ItemChecked();
    }, this);
}