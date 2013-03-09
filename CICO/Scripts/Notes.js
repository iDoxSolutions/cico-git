
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
                self.notes.unshift(new NoteModel(data));
                self.Content("");
            });
        
    };

    self.deleteNote = function(note) {
        $.post("/notes/delete", { id: note.Id }, function() {
            self.notes.remove(function(i) { return i.Id == note.Id; });
        });
    };


}