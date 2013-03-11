function Form1() {
    
}


function Form2() {
    var self = this;
    self.submitForm = function () {
        alert('');
    };
}


function Form3() {

}

function SelfContainedFormModel(item) {
    var self = this;
    self.Form = item.Form;
    
    switch (self.Form) {
        case "Form1":
            self.formModel = new Form1();
            break;
        case "Form2":
            self.formModel = new Form2();
            break;
        case "Form3":
            self.formModel = new Form3();
            break;
    }

    self.FormBase = new CicoFormBase(item);
    self.templateName = "SelfContainedForm";
}