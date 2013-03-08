
function NoteModel(note) {
    self.Content = note.Content;
    self.DateCreated = note.DateCreated;
}

function NoteListModel(item) {
    var self = this;
    self.notes = ko.observableArray(item?item.Notes:[]);
    self.Content = ko.observable();
    self.addNote = function() {

        $.post("/notes/create", { TemplateItemId: item.Id, Content: self.Content() },
            function (data) {
                self.notes.push(new NoteModel(data));
                self.Content("");
            });
        
    };


}