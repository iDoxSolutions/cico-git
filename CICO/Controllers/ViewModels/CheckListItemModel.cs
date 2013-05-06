using System.Collections.Generic;

namespace Cico.Controllers.ViewModels
{
    public class CheckListItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string ItemTemplate { get; set; }
        public bool Checked { get; set; }
        public string CssClass{get; set; }
        public string InstructionText { get; set; }
        public IList<NoteViewModel> Notes{get; set; }

        public string FileUrl{get; set; }

        public string FileDesc{get; set; }

        public string Form{get; set; }

        public string DueDate{get; set; }

        public FileModel SubmittedFile { get; set; }

        public string ApprovalText{get; set; }

        public int TrackId{get; set; }

        public string ItemUrl{get; set; }

        public bool CompleteChecklist{get; set; }

        public IList<DependentsFile> DependentsFiles{ get; set; }

        public string CustomFormUrl{get; set; }

        public bool NotesEnabled{get; set; }

        public bool ViewOnlyNotes
        {
            get; set; }
    }
}