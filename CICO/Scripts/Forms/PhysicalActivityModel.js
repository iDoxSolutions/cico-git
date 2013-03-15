function PhysicalActivityModel(item) {
    var self = this;
    self.FormBase = new CicoFormBase(item);
    self.templateName = "PhysicalActivity";
    self.complete = function () {
        item.ItemChecked(true);
        alert('');
    };
}