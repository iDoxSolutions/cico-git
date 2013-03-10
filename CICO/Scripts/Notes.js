﻿
function NoteModel(note) {
    var self = this;
    self.Content = note.Content;
    self.DateCreated = note.DateCreated;
    self.Id = note.Id;
}

function NoteListModel(item) {
    var self = this;
    self.notes = ko.observableArray(item?item.Notes:[]);
    self.Content = ko.observable();
    self.addNote = function () {

        //alert(tinyMCE.get('note-editor').getContent());
        $.post("/notes/create", { TemplateItemId: item.Id, Content: tinyMCE.get('note-editor').getContent({ format: 'raw' }) },
            
            function (data) {
                self.notes.unshift(new NoteModel(data));
                tinyMCE.get('note-editor').setContent('');
            });
        
    };

    self.deleteNote = function(note) {
        $.post("/notes/delete", { id: note.Id }, function() {
            self.notes.remove(function(i) { return i.Id == note.Id; });
        });
    };


}