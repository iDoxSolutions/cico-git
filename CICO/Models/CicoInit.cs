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
            context.DropdownItems.Add(new DropdownItem() { Key = "AL", Description = "Alabama", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "AK", Description = "Alaska", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem(){ Key = "AZ", Description = "Arizona", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "AR", Description = "Arkansas", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "CA", Description = "California", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "CO", Description = "Colorado", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "CT", Description = "Conneciticut", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "DE", Description = "Delaware", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "FL", Description = "Florida", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "GA", Description = "Georgia", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "HI", Description = "Hawaii", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "ID", Description = "Idaho", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "IL", Description = "Illinois", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "IN", Description = "Indiana", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "IA", Description = "Iowa", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "KS", Description = "Kansas", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "KY", Description = "Kentucky", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "LA", Description = "Louisana", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "ME", Description = "Maine", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MD", Description = "Maryland", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MA", Description = "Massachusetts", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MI", Description = "Michigan", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MN", Description = "Minnesota", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MS", Description = "Missippi", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MO", Description = "Missouri", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MT", Description = "Montana", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NE", Description = "Nebraska", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NV", Description = "Nevada", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NH", Description = "New Hampshire", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NJ", Description = "New Jersey", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NM", Description = "New Mexico", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NY", Description = "New York", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "NC", Description = "North Carolina", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "ND", Description = "North Dakota", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "OH", Description = "Ohio", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "OK", Description = "Oklohoma", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "OR", Description = "Oregon", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PA", Description = "Pennsylvania", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "RI", Description = "Rhode Island", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "SC", Description = "South Carolina", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "SD", Description = "South Dakota", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "TN", Description = "Tennessee", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "TX", Description = "Texas", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "UT", Description = "Utah", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "VT", Description = "Vermont", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "VA", Description = "Virginia", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "WA", Description = "Washington", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "MDWV", Description = "West Virginia", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "WI", Description = "Wisconsin", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "WY", Description = "Wyoming", ValueType = "States" });

            context.DropdownItems.Add(new DropdownItem() { Key = "Spouse", Description = "Spouse", ValueType = "Relationship" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Dependent", Description = "Dependent", ValueType = "Relationship" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Other", Description = "Other", ValueType = "Relationship" });

            context.DropdownItems.Add(new DropdownItem() { Key = "IT", Description = "IT", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "HR", Description = "HR", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "FMC", Description = "FMC", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "ADM", Description = "ADM", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "GSO", Description = "GSO", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "RSO", Description = "RSO", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "IM", Description = "IM", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "HU", Description = "HU", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "CLO", Description = "CLO", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "AMB", Description = "AMB", ValueType = "Office" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Other", Description = "Other", ValueType = "Office" });

            context.DropdownItems.Add(new DropdownItem() { Key = "Location1", Description = "Location1", ValueType = "Location" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Location2", Description = "Location2", ValueType = "Location" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Location3", Description = "Location3", ValueType = "Location" });

            context.DropdownItems.Add(new DropdownItem() { Key = "Nationality1", Description = "Nationality1", ValueType = "Nationality" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Nationality2", Description = "Nationality2", ValueType = "Nationality" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Nationality3", Description = "Nationality3", ValueType = "Nationality" });

            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType1", Description = "PassportType1", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType2", Description = "PassportType2", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType3", Description = "PassportType3", ValueType = "PassportType" });

            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType1", Description = "PassportType1", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType2", Description = "PassportType2", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType3", Description = "PassportType3", ValueType = "PassportType" });



            
            
            

        } 
    }
}