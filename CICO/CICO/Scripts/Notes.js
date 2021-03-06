﻿
function NoteModel(note, viewOnly) {
    var self = this;
    self.Content = note.Content;
    self.DateCreated = note.DateCreated;
    self.Id = note.Id;
    self.UserCreated = note.UserCreated;
    self.Deletable = note.Deletable && !viewOnly;
}

function NoteListModel(item) {
    var self = this;
    ko.utils.extend(self, new CicoFormBase(item));
    //self.notes = ko.observableArray(item ? item.item.Notes : []);
    self.notes = ko.observableArray([]);
    if (item) {

        ko.utils.arrayForEach(item.item.Notes, function (data) {
            self.notes.push(new NoteModel(data, item.item.ViewOnlyNotes));
        }); 
    }

self.AddEnabled = ko.computed(function () {
        return !item.item.ViewOnlyNotes;
    }, self);
    self.Content = ko.observable();
    self.addNote = function () {
        
        //alert(tinyMCE.get('note-editor').getContent());
        var noteContent = tinyMCE.get('note-editor').getContent({ format: 'raw' });
        $.post("/notes/create", { TemplateItemId: item.item.Id, Content: noteContent, checklistId: item.CheckListId },
            function (data) {
                self.notes.unshift(new NoteModel(data, item.item.ViewOnlyNotes));
                tinyMCE.get('note-editor').setContent('');
            });
        
    };

    self.deleteNote = function(note) {
        $.post("/notes/delete", { id: note.Id }, function() {
            self.notes.remove(function(i) { return i.Id == note.Id; });
        });
    };


}