function EmergencyContact() {
    var self = this;
    ko.utils.extend(self, new OnlineFormBase("EmergencyContact"));
    self.data =  ko.observable(null);
    self.save = function() {
       
        
        $.ajax({
            type: "POST",
            url: "/emergencyData/save",
            data: self.data(),
            success: function(data) {
                self.data(data);
                self.check(self.item,
                    function(result) {
                       
                    }
                );
            },
            error: function (data) {
                alert(data.responseText);
            },
            dataType: 'json'
        });
    };

    self.initForm = function(item) {
        self.item = item;
        $.post("/emergencyData/GetByCheckListId", { id: item.CheckListId }, function (data) {
            self.data(data);
        });
    };
}
RegisterForm(EmergencyContact);