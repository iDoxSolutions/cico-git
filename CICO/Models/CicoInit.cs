using Cico.Models.Helpers;

namespace Cico.Models
{
    public class CicoInit :System.Data.Entity.DropCreateDatabaseIfModelChanges<Cico.Models.CicoContext>
    {
        
        protected override void Seed(CicoContext context)
        {

            var template1 = context.SystemFiles.Add(new SystemFile() {FileType = "DocTemplate", Description = "Doc Template1", Patch = "DocTemplates/Template1.docx" });
            var template2 = context.SystemFiles.Add(new SystemFile() { FileType = "DocTemplate", Description = "Doc Template2", Patch = "DocTemplates/Template2.docx" });
           
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "SelfContainedForm", Description = "Self-Contained Form" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentSubmitted", Description = "Document Submitted" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentWriting", Description = "Document w/Writing" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "PhysicalActivity", Description = "Physical Activity" });
            

            context.Settings.Add(new Setting(){Name = "checklisttemplate",Value = "1"});
            var template = context.CheckListTemplates.Create();//(new CheckListTemplate(){Name = "Name of the template",Type="Test"});
            template.Name = "Base Checklist Template";
            template.Type = "Test";
            template.CheckListItemTemplates.Add(new CheckListItemTemplate() {File = template1, Description = "Document Submitted Class", Item = "DocumentSubmitted", Type = "DocumentSubmitted" });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate() { Description = "Self-Contained Form", Item = "SelfContainedForm", Type = "SelfContainedForm" });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate() { Description = "Document w/Writing", Item = "DocumentWriting", Type = "DocumentWriting" });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate() { Description = "Physical Activity", Item = "PhysicalActivity", Type = "PhysicalActivity" });
            context.CheckListTemplates.Add(template);

            AddStates(context);
            AddSystemForms(context);
            context.SaveChanges();
           
        }

        private void AddSystemForms(CicoContext context)
        {
            context.DropdownItems.Add(new DropdownItem() { Key = "Form1", Description = "Form 1", ValueType = "SystemForms" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Form2", Description = "Form 2", ValueType = "SystemForms" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Form3", Description = "Form 3", ValueType = "SystemForms" });
        }

        private void AddStates(CicoContext context)
        {
            context.DropdownItems.Add(new DropdownItem(){Key = "MD",Description = "Maryland",ValueType = "States"});
        } 
    }
}