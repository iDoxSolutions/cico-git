using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cico.Models.Helpers;

namespace Cico.Models
{
    public class CicoInit : System.Data.Entity.DropCreateDatabaseIfModelChanges <Cico.Models.CicoContext>
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


        private void InitStaff(CicoContext context)
        {
            var globalAdmin = context.SystemRoles.Add(new SystemRole(){Name = "GlobalAdmin",Staffs = new List<Staff>()});
            var officeAdmin = context.SystemRoles.Add(new SystemRole() { Name = "OfficeAdmin",Staffs = new List<Staff>()});
            var proxy = context.SystemRoles.Add(new SystemRole() { Name = SystemRole.UserProxy, Staffs = new List<Staff>() });

            var user = context.Staffs.Add(new Staff(){UserId = "ABAPER-W8\\Pawel",Office=_hrOffice,Email = "wasilewski.pawel@gmail.com"});
            user.SystemRoles = new Collection<SystemRole>();
            user.SystemRoles.Add(globalAdmin);

            user = context.Staffs.Add(new Staff() { UserId = "Lightkeeperdev\\Ken", Office = _hrOffice, Email = "kenhambright@gmail.com" });
            user.SystemRoles = new Collection<SystemRole>();
            user.SystemRoles.Add(globalAdmin);

            var hradmin = context.Staffs.Add(new Staff() { UserId = "LTKSERVER\\HrAdmin", Office = _hrOffice, Email = "wasilewski.pawel@gmail.com" });
            hradmin.SystemRoles = new List<SystemRole>();
            hradmin.SystemRoles.Add(officeAdmin);
            officeAdmin.Staffs.Add(hradmin);

            var gsodmin = context.Staffs.Add(new Staff() { UserId = "LTKSERVER\\GsoAdmin", Office = _hrOffice, Email = "wasilewski.pawel@gmail.com" });
            gsodmin.SystemRoles = new List<SystemRole>();
            gsodmin.SystemRoles.Add(officeAdmin);
            officeAdmin.Staffs.Add(gsodmin);

            var global = context.Staffs.Add(new Staff() { UserId = "LTKSERVER\\GlobalAdmin", Office = _hrOffice, Email = "wasilewski.pawel@gmail.com" });
            global.SystemRoles = new Collection<SystemRole>();
            global.SystemRoles.Add(globalAdmin);
            globalAdmin.Staffs.Add(global);
        }

        protected override void Seed(CicoContext context)
        {
            
            var template1 =
                context.SystemFiles.Add(new SystemFile()
                    {
                        FileType = "DocTemplate",
                        Description = "Doc Template1",
                        Path = "DocTemplates/Template1.docx"
                    });
            var template2 =
                context.SystemFiles.Add(new SystemFile()
                    {
                        FileType = "DocTemplate",
                        Description = "Doc Template2",
                        Path = "DocTemplates/Template2.docx"
                    });

            context.CheckListItemTypes.Add(new CheckListItemType()
                {
                    Name = "SelfContainedForm",
                    Description = "Self-Contained Form"
                });
            context.CheckListItemTypes.Add(new CheckListItemType()
                {
                    Name = "DocumentSubmitted",
                    Description = "Document Submitted"
                });
            context.CheckListItemTypes.Add(new CheckListItemType()
                {
                    Name = "DocumentWriting",
                    Description = "Document w/Writing"
                });
            context.CheckListItemTypes.Add(new CheckListItemType()
                {
                    Name = "PhysicalActivity",
                    Description = "Physical Activity"
                });
            context.CheckListItemTypes.Add(new CheckListItemType()
            {
                Name = "DocumentApproval",
                Description = "Document w/Online Approval"
            });


            //Check In Template
            context.Settings.Add(new Setting() {Name = "checklisttemplate", Value = "1"});
            var x = context.CheckListSessions.Create();
            var template = context.CheckListTemplates.Create();
            //(new CheckListTemplate(){Name = "Name of the template",Type="Test"});
            template.Name = "Check In";
            template.Type = "CheckIn";
            template.Published = true;


            //Check Out Template
            context.Settings.Add(new Setting() { Name = "checkouttemplate", Value = "2" });
            var cko_x = context.CheckListSessions.Create();
            var cko_template = context.CheckListTemplates.Create();
            //(new CheckListTemplate(){Name = "Name of the template",Type="Test"});
            cko_template.Name = "Check Out";
            cko_template.Type = "CheckOut";
            cko_template.Published = true;

            AddStates(context);
            AddOffices(context);
            InitStaff(context);
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
                    InstructionText =
                        "Upload a copy of your Travel Orders.  May be electronic copy, scanned or photographed."
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
                    InstructionText =
                        "Upload a copy of your Passport.  May be electronic copy, scanned or photographed."
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
                    InstructionText =
                        "Upload a copy of your Mexico Visa.  May be electronic copy, scanned or photographed."
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
                    InstructionText =
                        "Schedule and attend the Newcomer Orientation session.  Contact GSO at this address to schedule your attendence."
                    ,
                    Provisional = true
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
                    InstructionText =
                        "Schedule and attend the RSO Security Briefing.  Contact RSO at this address to schedule your attendence."
                    ,
                    Provisional = true
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
                    InstructionText =
                        "Download the Computer Security Requirements document, then indicate your agreement by checking the box."
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
                    InstructionText =
                        "Hand deliver a copy of your and your dependents' medical records to the Health Unit ASAP after arrival."
                    ,
                    AlertDays = "3"
                    ,
                    Provisional = true
                    ,
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
                    InstructionText =
                        "Schedule a meeting with the Ambassador by contacting Anna in ADM at anne@state.gov"
                    ,
                    AlertDays = "3",
                    Provisional = true,
                    SystemFile = template1
                });
            template.CheckListItemTemplates.Add(new CheckListItemTemplate()
                {
                    Office = _admOffice,
                    Description = "Schedule mtg with IT"
                    ,
                    Item = "PhysicalActivity"
                    ,
                    Type = "PhysicalActivity"
                    ,
                    InstructionText =
                        "Schedule a meeting with the the IT group by contacting James in IT at James@state.gov"
                    ,
                    AlertDays = "3",
                    Provisional = true,
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
            context.CheckListTemplates.Add(cko_template);
            AddRelationships(context);
            AddSystemForms(context);
            AddChecklistTypes(context);

            context.SaveChanges();

        }

        private void AddOffices(CicoContext context)
        {
             _hrOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "HR"});
            _admOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "ADM"});
            _gsoOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "GSO"});
            _fmcOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "FMC"});
            _rsoOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "RSO"});
            _imOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "IM"});
            _huOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "HU"});
            _cloOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "CLO"});
            _ambOffice = context.Offices.Add(new Office() {ContactUser = "Ken Hambright", Name = "AMB"});

        }

    private void AddSystemForms(CicoContext context)
        {
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Form1",
                    Description = "Form 1",
                    ValueType = "SystemForms"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Form2",
                    Description = "Form 2",
                    ValueType = "SystemForms"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Form3",
                    Description = "Form 3",
                    ValueType = "SystemForms"
                });
        }

    private void AddChecklistTypes(CicoContext context) {
        context.DropdownItems.Add(new DropdownItem() {
            Key = "Checkin",
            Description = "Checkin",
            ValueType = "CheckListType"
        });
        context.DropdownItems.Add(new DropdownItem() {
            Key = "Checkout",
            Description = "Checkout",
            ValueType = "CheckListType"
        });
       
    }


           

        private void AddStates(CicoContext context)
        {
            context.DropdownItems.Add(new DropdownItem() {Key = "AL", Description = "Alabama", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "AK", Description = "Alaska", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "AZ", Description = "Arizona", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "AR", Description = "Arkansas", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "CA", Description = "California", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "CO", Description = "Colorado", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "CT",
                    Description = "Conneciticut",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem() {Key = "DE", Description = "Delaware", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "FL", Description = "Florida", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "GA", Description = "Georgia", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "HI", Description = "Hawaii", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "ID", Description = "Idaho", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "IL", Description = "Illinois", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "IN", Description = "Indiana", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "IA", Description = "Iowa", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "KS", Description = "Kansas", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "KY", Description = "Kentucky", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "LA", Description = "Louisana", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "ME", Description = "Maine", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "MD", Description = "Maryland", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "MA",
                    Description = "Massachusetts",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem() {Key = "MI", Description = "Michigan", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "MN", Description = "Minnesota", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "MS", Description = "Missippi", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "MO", Description = "Missouri", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "MT", Description = "Montana", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "NE", Description = "Nebraska", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "NV", Description = "Nevada", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "NH",
                    Description = "New Hampshire",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem() {Key = "NJ", Description = "New Jersey", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "NM", Description = "New Mexico", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "NY", Description = "New York", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "NC",
                    Description = "North Carolina",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "ND",
                    Description = "North Dakota",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem() {Key = "OH", Description = "Ohio", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "OK", Description = "Oklohoma", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "OR", Description = "Oregon", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PA",
                    Description = "Pennsylvania",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "RI",
                    Description = "Rhode Island",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "SC",
                    Description = "South Carolina",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "SD",
                    Description = "South Dakota",
                    ValueType = "States"
                });
            context.DropdownItems.Add(new DropdownItem() {Key = "TN", Description = "Tennessee", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "TX", Description = "Texas", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "UT", Description = "Utah", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "VT", Description = "Vermont", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "VA", Description = "Virginia", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "WA", Description = "Washington", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "MDWV",
                    Description = "West Virginia",
                    ValueType = "States"
                });




            context.DropdownItems.Add(new DropdownItem() { Description = "AMERICAN SAMOA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ANDORRA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ANGOLA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ANGUILLA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ANTARCTICA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ANTIGUA AND BARBUDA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ARGENTINA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ARMENIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ARUBA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "AUSTRALIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "AUSTRIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "AZERBAIJAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BAHAMAS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BAHRAIN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BANGLADESH", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BARBADOS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BELARUS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BELGIUM", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BELIZE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BENIN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BERMUDA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BHUTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BOLIVIA, PLURINATIONAL STATE OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BONAIRE, SINT EUSTATIUS AND SABA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BOSNIA AND HERZEGOVINA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BOTSWANA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BOUVET ISLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BRAZIL", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BRITISH INDIAN OCEAN TERRITORY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BRUNEI DARUSSALAM", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BULGARIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BURKINA FASO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "BURUNDI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CAMBODIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CAMEROON", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CANADA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CAPE VERDE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CAYMAN ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CENTRAL AFRICAN REPUBLIC", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CHAD", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CHILE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CHINA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CHRISTMAS ISLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "COCOS (KEELING) ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "COLOMBIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "COMOROS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CONGO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CONGO, THE DEMOCRATIC REPUBLIC OF THE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "COOK ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "COSTA RICA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CÔTE D'IVOIRE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CROATIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CUBA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CURAÇAO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CYPRUS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "CZECH REPUBLIC", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "DENMARK", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "DJIBOUTI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "DOMINICA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "DOMINICAN REPUBLIC", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ECUADOR", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "EGYPT", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "EL SALVADOR", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "EQUATORIAL GUINEA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ERITREA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ESTONIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ETHIOPIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FALKLAND ISLANDS (MALVINAS)", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FAROE ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FIJI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FINLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FRANCE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FRENCH GUIANA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FRENCH POLYNESIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "FRENCH SOUTHERN TERRITORIES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GABON", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GAMBIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GEORGIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GERMANY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GHANA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GIBRALTAR", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GREECE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GREENLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GRENADA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUADELOUPE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUAM", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUATEMALA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUERNSEY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUINEA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUINEA-BISSAU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "GUYANA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "HAITI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "HEARD ISLAND AND MCDONALD ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "HOLY SEE (VATICAN CITY STATE)", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "HONDURAS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "HONG KONG", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "HUNGARY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ICELAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "INDIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "INDONESIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "IRAN, ISLAMIC REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "IRAQ", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "IRELAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ISLE OF MAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ISRAEL", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ITALY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "JAMAICA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "JAPAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "JERSEY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "JORDAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KAZAKHSTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KENYA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KIRIBATI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KOREA, DEMOCRATIC PEOPLE'S REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KOREA, REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KUWAIT", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "KYRGYZSTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LAO PEOPLE'S DEMOCRATIC REPUBLIC", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LATVIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LEBANON", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LESOTHO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LIBERIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LIBYA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LIECHTENSTEIN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LITHUANIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "LUXEMBOURG", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MACAO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MADAGASCAR", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MALAWI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MALAYSIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MALDIVES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MALI", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MALTA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MARSHALL ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MARTINIQUE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MAURITANIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MAURITIUS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MAYOTTE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MEXICO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MICRONESIA, FEDERATED STATES OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MOLDOVA, REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MONACO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MONGOLIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MONTENEGRO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MONTSERRAT", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MOROCCO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MOZAMBIQUE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "MYANMAR", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NAMIBIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NAURU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NEPAL", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NETHERLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NEW CALEDONIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NEW ZEALAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NICARAGUA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NIGER", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NIGERIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NIUE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NORFOLK ISLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NORTHERN MARIANA ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "NORWAY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "OMAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PAKISTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PALAU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PALESTINE, STATE OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PANAMA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PAPUA NEW GUINEA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PARAGUAY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PERU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PHILIPPINES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PITCAIRN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "POLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PORTUGAL", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "PUERTO RICO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "QATAR", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "RÉUNION", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ROMANIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "RUSSIAN FEDERATION", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "RWANDA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT BARTHÉLEMY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT KITTS AND NEVIS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT LUCIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT MARTIN (FRENCH PART)", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT PIERRE AND MIQUELON", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAINT VINCENT AND THE GRENADINES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAMOA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAN MARINO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAO TOME AND PRINCIPE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SAUDI ARABIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SENEGAL", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SERBIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SEYCHELLES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SIERRA LEONE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SINGAPORE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SINT MAARTEN (DUTCH PART)", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SLOVAKIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SLOVENIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SOLOMON ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SOMALIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SOUTH AFRICA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SOUTH SUDAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SPAIN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SRI LANKA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SUDAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SURINAME", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SVALBARD AND JAN MAYEN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SWAZILAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SWEDEN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SWITZERLAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "SYRIAN ARAB REPUBLIC", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TAIWAN, PROVINCE OF CHINA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TAJIKISTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TANZANIA, UNITED REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "THAILAND", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TIMOR-LESTE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TOGO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TOKELAU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TONGA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TRINIDAD AND TOBAGO", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TUNISIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TURKEY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TURKMENISTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TURKS AND CAICOS ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "TUVALU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UGANDA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UKRAINE", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UNITED ARAB EMIRATES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UNITED KINGDOM", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UNITED STATES", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UNITED STATES MINOR OUTLYING ISLANDS", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "URUGUAY", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "UZBEKISTAN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "VANUATU", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "VENEZUELA, BOLIVARIAN REPUBLIC OF", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "VIET NAM", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "VIRGIN ISLANDS, BRITISH", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "VIRGIN ISLANDS, U.S.", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "WALLIS AND FUTUNA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "WESTERN SAHARA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "YEMEN", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ZAMBIA", ValueType = "Nations"});
            context.DropdownItems.Add(new DropdownItem() { Description = "ZIMBABWE", ValueType = "Nations"});





            context.DropdownItems.Add(new DropdownItem() {Key = "WI", Description = "Wisconsin", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "WY", Description = "Wyoming", ValueType = "States"});
            context.DropdownItems.Add(new DropdownItem() {Key = "IT", Description = "IT", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "HR", Description = "HR", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "FMC", Description = "FMC", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "ADM", Description = "ADM", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "GSO", Description = "GSO", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "RSO", Description = "RSO", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "IM", Description = "IM", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "HU", Description = "HU", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "CLO", Description = "CLO", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "AMB", Description = "AMB", ValueType = "Office"});
            context.DropdownItems.Add(new DropdownItem() {Key = "Other", Description = "Other", ValueType = "Office"});






            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Location1",
                    Description = "Location1",
                    ValueType = "Location"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Location2",
                    Description = "Location2",
                    ValueType = "Location"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Location3",
                    Description = "Location3",
                    ValueType = "Location"
                });

            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Nation1",
                    Description = "Nation1",
                    ValueType = "Nationality"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Nation2",
                    Description = "Nation2",
                    ValueType = "Nationality"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Nation3",
                    Description = "Nation3",
                    ValueType = "Nationality"
                });

            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PassportType1",
                    Description = "PassportType1",
                    ValueType = "PassportType"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PassportType2",
                    Description = "PassportType2",
                    ValueType = "PassportType"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PassportType3",
                    Description = "PassportType3",
                    ValueType = "PassportType"
                });

            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PassportType1",
                    Description = "PassportType1",
                    ValueType = "PassportType"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PassportType2",
                    Description = "PassportType2",
                    ValueType = "PassportType"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "PassportType3",
                    Description = "PassportType3",
                    ValueType = "PassportType"
                });

            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Agency1",
                    Description = "Agency1",
                    ValueType = "Agency"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Agency2",
                    Description = "Agency2",
                    ValueType = "Agency"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Agency3",
                    Description = "Agency3",
                    ValueType = "Agency"
                });
        }

        private void AddRelationships(CicoContext context)
        {
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Spouse",
                    Description = "Spouse",
                    ValueType = "Relationship"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Sibling",
                    Description = "Sibling",
                    ValueType = "Relationship"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Parent",
                    Description = "Parent",
                    ValueType = "Relationship"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Child",
                    Description = "Child",
                    ValueType = "Relationship"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Grandparent",
                    Description = "Grandparent",
                    ValueType = "Relationship"
                });
            context.DropdownItems.Add(new DropdownItem()
                {
                    Key = "Other",
                    Description = "Other",
                    ValueType = "Relationship"
                });

        }

        
    
        
    }
}   