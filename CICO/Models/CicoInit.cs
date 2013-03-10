using Cico.Models.Helpers;

namespace Cico.Models
{
    public class CicoInit :System.Data.Entity.DropCreateDatabaseIfModelChanges<Cico.Models.CicoContext>
    {
        
        protected override void Seed(CicoContext context)
        {
           
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "SelfContainedForm", Description = "Self-Contained Form" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentSubmitted", Description = "Document Submitted" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentWriting", Description = "Document w/Writing" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentApproval", Description = "Document w/On-Line Approval" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "PhysicalActivity", Description = "Physical Activity" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "ProvisionalStatus", Description = "Provisional Status" });

            context.Settings.Add(new Setting(){Name = "checklisttemplate",Value = "1"});
            var x = context.CheckListSessions.Create();
            var template = context.CheckListTemplates.Create();//(new CheckListTemplate(){Name = "Name of the template",Type="Test"});
            template.Name = "Name of the template";
            template.Type = "Test";
            template.CheckListItemTemplates.Add(new CheckListItemTemplate() { Description = "Desc", Item = "DocumentSubmitted", Type = "DocumentSubmitted" });
            context.CheckListTemplates.Add(template);

            AddStates(context);

            context.SaveChanges();
           
        }

        private void AddStates(CicoContext context)
        {
            context.DropdownItems.Add(new DropdownItem(){Key = "MD",Description = "Maryland",ValueType = "States"});
        } 
    }
}