var OnlineForms = new Array();

function RegisterForm(model) {
    OnlineForms.push(new model);
}

function OnlineFormBase(name) {
    var self = this;
    self.name = name;
    self.initForm = function(item) {
        self.item = item;
    };

    self.check = function(item,success) {
            $.post("/checklist/check", { id: item.item.Id, checklistId: item.CheckListId },function(data) {
                item.ItemChecked(true);
                item.CssClass(data.CssClass);
                success(data);
            }
        );
    };

}