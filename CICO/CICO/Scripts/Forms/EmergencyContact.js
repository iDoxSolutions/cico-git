
EmergencyContact.prototype = new OnlineFormBase("EmergencyContact");
function EmergencyContact() {
    var self = this;
    //ko.utils.extend(self, new OnlineFormBase("EmergencyContact"));
    self.data = ko.observable(null);
    self.message = ko.observable("");
    self.messageCss = ko.observable("green");
    self.SaveEnabled = ko.observable(true);
    
    self.save = function() {
       
        
        $.ajax({
            type: "POST",
            url: "/emergencyData/save",
            data: self.data(),
            success: function(data) {
                self.data(data);
                self.check(self.item,
                    function (result) {
                        self.messageCss("green-text");
                        self.message("Thank you!");
                        self.SaveEnabled(false);
                    }
                );
                
            },
            error: function (data) {
                self.messageCss("red-text");
                self.message(data.responseText);
            },
            dataType: 'json'
        });
    };

    self.initForm = function (item) {
        self.SaveEnabled(!item.item.Checked);
        EmergencyContact.prototype.initForm(item);
        $.post("/emergencyData/GetByCheckListId", { id: item.CheckListId }, function (data) {
            self.data(data);
        });
    };
}
RegisterForm(EmergencyContact);