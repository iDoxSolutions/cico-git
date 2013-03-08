
function NoteModel(note) {
    self.Content = note.Content;
    self.DateCreated = note.DateCreated;
}

function NoteListModel(item) {
    var self = this;
    self.notes = ko.observableArray([]);
    $.post('/notes', { checklistItemTemplateId:item.Id},
        function(data) {
            ko.utils.arrayForEach(data, function (note) {
                self.notes.push(new NoteModel(note));
            });
        }
    );
}