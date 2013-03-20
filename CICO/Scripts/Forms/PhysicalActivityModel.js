function PhysicalActivityModel(item) {
    var self = this;
    self.item = item;
    self.FormBase = new CicoFormBase(item);
    self.templateName = "PhysicalActivity";
    self.ApprovalText = ko.observable(item.item.ApprovalText);
    self.complete = function () {
        $.post('/checklist/check/', { id: item.item.Id }, function (data) {
            item.ItemChecked(true);
            item.CssClass(data.CssClass);
        });
    };
    

    this.buttonText = ko.computed(function () {
        return (item.item.ApprovalText==""|| item.item.ApprovalText==null) ?"Complete":"Approve";
    }, this);

    this.buttonEnabled = ko.computed(function () {
        return !item.ItemChecked();
    }, this);
}