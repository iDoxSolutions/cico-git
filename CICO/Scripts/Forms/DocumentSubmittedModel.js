function DocumentSubmittedModel(item) {
    var self = this;
    self.FileUrl = item.FileUrl;
    self.FileDesc = item.FileDesc;
    self.FormBase = new CicoFormBase(item);
    self.templateName = "DocumentSubmitted";
    self.submitDoc = function() {
        alert('');
    };
}