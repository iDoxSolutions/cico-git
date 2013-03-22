function CicoFormBase(item) {
    var self = this;
    self.Enabled = ko.computed(function () {
        return item.Enabled();
    }, this);
    
    self.Completed = ko.computed(function () {
        return item.Completed();
    }, this);
    //self.notes = new NoteListModel(item);
}