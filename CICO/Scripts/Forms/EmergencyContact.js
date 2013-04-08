
EmergencyContact.prototype = new OnlineFormBase("EmergencyContact");
function EmergencyContact() {
    var self = this;
    //ko.utils.extend(self, new OnlineFormBase("EmergencyContact"));
    self.data = ko.observable(null);
    self.message = ko.observable("");
    self.messageCss = ko.observable("green");
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

    self.initForm = function(item) {
        this._super.initForm(item);
        $.post("/emergencyData/GetByCheckListId", { id: item.CheckListId }, function (data) {
            self.data(data);
        });
    };
}
RegisterForm(EmergencyContact);