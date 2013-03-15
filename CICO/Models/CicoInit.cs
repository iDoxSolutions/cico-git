using Cico.Models.Helpers;

namespace Cico.Models
{
    public class CicoInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<Cico.Models.CicoContext>
    {
        private Office _hrOffice;
        private Office _cloOffice;
        private Office _huOffice;
        private Office _imOffice;
        private Office _admOffice;
        private Office _gsoOffice;
        private Office _fmcOffice;
        private Office _rsoOffice;
        private Office _ambOffice;


        protected override void Seed(CicoContext context)
        {

            var template1 = context.SystemFiles.Add(new SystemFile() { FileType = "DocTemplate", Description = "Doc Template1", Patch = "DocTemplates/Template1.docx" });
            var template2 = context.SystemFiles.Add(new SystemFile() { FileType = "DocTemplate", Description = "Doc Template2", Patch = "DocTemplates/Template2.docx" });

            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "SelfContainedForm", Description = "Self-Contained Form" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentSubmitted", Description = "Document Submitted" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "DocumentWriting", Description = "Document w/Writing" });
            context.CheckListItemTypes.Add(new CheckListItemType() { Name = "PhysicalActivity", Description = "Physical Activity" });



            context.Settings.Add(new Setting() { Name = "checklisttemplate", Value = "1" });
            var x = context.CheckListSessions.Create();
            var template = context.CheckListTemplates.Create();//(new CheckListTemplate(){Name = "Name of the template",Type="Test"});
            template.Name = "Check In";
            template.Type = "Mexico";
            AddStates(context);

            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _hrOffice,
                Description = "Submit travel orders"

                   ,
                Item = "DocumentSubmitted"
                   ,
                Type = "DocumentSubmitted"

                   ,
                SystemFile = template1

                   ,
                InstructionText = "Upload a copy of your Travel Orders.  May be electronic copy, scanned or photographed."
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _hrOffice,
                Description = "Submit passport copy"
                                                                                   ,
                Item = "DocumentSubmitted"
                                                                                   ,
                Type = "DocumentSubmitted"
                                                                                   ,
                InstructionText = "Upload a copy of your Passport.  May be electronic copy, scanned or photographed."
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _hrOffice,
                Description = "Submit visa"
                                                                                   ,
                Item = "DocumentSubmitted"
                                                                                   ,
                Type = "DocumentSubmitted"
                                                                                   ,
                InstructionText = "Upload a copy of your Mexico Visa.  May be electronic copy, scanned or photographed."
                                                                                   ,
                AlertDays = "3"
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _hrOffice,
                Description = "Sign Standards of Conduct"
                                                                                   ,
                Item = "DocumentWriting"
                                                                                   ,
                Type = "DocumentWriting"
                                                                                   ,
                InstructionText = "Download the Standards of Conduct, read and sign.  Then upload copy."
                                                                                   ,
                AlertDays = "3"
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _hrOffice,
                Description = "Submit bio form"
                                                                                   ,
                Item = "DocumentWriting"
                                                                                   ,
                Type = "DocumentWriting",
                SystemFile = template1
                                                                                   ,
                InstructionText = "Download & complete the Biographical History form.  Then upload copy."
                                                                                   ,
                AlertDays = "3"
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _gsoOffice,
                Description = "Attend orientation"
                                                                                   ,
                Item = "PhysicalActivity"
                                                                                   ,
                Type = "PhysicalActivity",
                SystemFile = template1
                                                                                   ,
                InstructionText = "Schedule and attend the Newcomer Orientation session.  Contact GSO at this address to schedule your attendence."
                                                                                   ,
                AlertDays = "3"
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _rsoOffice,
                Description = "RSO security briefing"
                                                                                   ,
                Item = "PhysicalActivity"
                                                                                   ,
                Type = "PhysicalActivity",
                SystemFile = template1
                                                                                   ,
                InstructionText = "Schedule and attend the RSO Security Briefing.  Contact RSO at this address to schedule your attendence."
                                                                                   ,
                AlertDays = "3"
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _fmcOffice,
                Description = "Submit EFT form"
                                                                                   ,
                Item = "DocumentWriting"
                                                                                   ,
                Type = "DocumentWriting"
                                                                                   ,
                InstructionText = "Download & complete the Electronic Funds Transfer form.  Then upload copy."
                                                                                   ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _imOffice,
                Description = "Computer security requirements"
                                                                                   ,
                Item = "DocumentSubmitted"
                                                                                   ,
                Type = "DocumentSubmitted"
                                                                                   ,
                InstructionText = "Download the Computer Security Requirements document, then indicate your agreement by checking the box."
                                                                                   ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _imOffice,
                Description = "Unclass ISC user form"
                                                                                  ,
                Item = "DocumentWriting"
                                                                                  ,
                Type = "DocumentWriting"
                                                                                  ,
                InstructionText = "Download & complete the Unclassified System User form.  Then upload copy."
                                                                                  ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _huOffice,
                Description = "Deliver medical records"
                                                                                  ,
                Item = "DocumentWriting"
                                                                                  ,
                Type = "DocumentWriting"
                                                                                  ,
                InstructionText = "Hand deliver a copy of your and your dependents' medical records to the Health Unit ASAP after arrival."
                                                                                  ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _cloOffice,
                Description = "Family network data sheet"
                                                                                  ,
                Item = "DocumentWriting"
                                                                                  ,
                Type = "DocumentWriting"
                                                                                  ,
                InstructionText = "Download & complete the Family Network Data Sheet.  Then upload copy."
                                                                                  ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _admOffice,
                Description = "Schedule mtg with Ambassador"
                                                                                  ,
                Item = "PhysicalActivity"
                                                                                  ,
                Type = "PhysicalActivity"
                                                                                  ,
                InstructionText = "Schedule a meeting with the Ambassador by contacting Anna in ADM at anne@state.gov"
                                                                                  ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _gsoOffice,
                Description = "Submit housing questionaire"
                                                                                  ,
                Item = "DocumentWriting"
                                                                                  ,
                Type = "DocumentWriting"
                                                                                  ,
                InstructionText = "Download & complete the Housing Questionaire form.  Then upload copy."
                                                                                  ,
                AlertDays = "3",
                SystemFile = template1
            });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
            {
                Office = _gsoOffice,
                Description = "Submit HHE inventory"
                                                                                  ,
                Item = "DocumentWriting"
                                                                                  ,
                Type = "DocumentWriting"
                                                                                  ,
                InstructionText = "Download & complete the HHE/UAB Inventory form.  Then upload copy."
                                                                                  ,
                AlertDays = "3",
                SystemFile = template1
            });

            context.CheckListTemplates.Add(template);


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
            context.DropdownItems.Add(new DropdownItem() { Key = "AL", Description = "Alabama", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "AK", Description = "Alaska", ValueType = "States" });
            context.DropdownItems.Add(new DropdownItem() { Key = "AZ", Description = "Arizona", ValueType = "States" });
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

            context.DropdownItems.Add(new DropdownItem() { Key = "Nation1", Description = "Nation1", ValueType = "Nationality" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Nation2", Description = "Nation2", ValueType = "Nationality" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Nation3", Description = "Nation3", ValueType = "Nationality" });

            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType1", Description = "PassportType1", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType2", Description = "PassportType2", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType3", Description = "PassportType3", ValueType = "PassportType" });

            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType1", Description = "PassportType1", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType2", Description = "PassportType2", ValueType = "PassportType" });
            context.DropdownItems.Add(new DropdownItem() { Key = "PassportType3", Description = "PassportType3", ValueType = "PassportType" });

            context.DropdownItems.Add(new DropdownItem() { Key = "Agency1", Description = "Agency1", ValueType = "Agency" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Agency2", Description = "Agency2", ValueType = "Agency" });
            context.DropdownItems.Add(new DropdownItem() { Key = "Agency3", Description = "Agency3", ValueType = "Agency" });


            _hrOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "HR" });
            _admOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "ADM" });
            _gsoOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "GSO" });
            _fmcOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "FMC" });
            _rsoOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "RSO" });
            _imOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "IM" });
            _huOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "HU" });
            _cloOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "CLO" });
            _ambOffice = context.Offices.Add(new Office() { ContactUser = "Ken Hambright", Name = "AMB" });





        }
    }
}