using System.Collections.Generic;

namespace Cico.Controllers.ViewModels
{
    public class CheckListModel
    {
        public CheckListModel()
        {
            CheckListItems = new List<CheckListItemModel>();
        }
        public IList<CheckListItemModel> CheckListItems { get; set; }
        public int Id { get; set; }
        public bool Completed{get; set; }
        
    }
}