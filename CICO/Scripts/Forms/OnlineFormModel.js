
function Form1() {
    var self = this;
    ko.utils.extend(self, new OnlineFormBase("Form1"));
}

RegisterForm(Form1);

function Form2() {
    var self = this;
    ko.utils.extend(self, new OnlineFormBase("Form2"));
    self.submitForm = function () {
        alert('');
    };
}

RegisterForm(Form2);

function Form3() {

    var self = this;
    ko.utils.extend(self, new OnlineFormBase("Form3"));
}
RegisterForm(Form3);

function OnlineFormModel(item) {
    var self = this;
    self.CustomFormUrl = item.item.CustomFormUrl + "?id=" + item.item.TrackId;
    self.Description = item.item.Description;
    self.physicalActivity = ko.observable(new PhysicalActivityModel(item));
    self.templateName = "OnlineForm";
}