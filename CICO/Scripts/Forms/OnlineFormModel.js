
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
    self.Form = item.item.Form;
    self.formModel = ko.observable();
    ko.utils.arrayForEach(OnlineForms, function (onlineForm) {
        if (onlineForm.name == self.Form) {
            self.formModel = onlineForm;
            self.formModel.initForm(item);
        }
    });

    
    
    self.templateName = "OnlineForm";
}