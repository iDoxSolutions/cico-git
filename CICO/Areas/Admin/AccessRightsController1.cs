using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Helpers;
using Cico.Models;
using Cico.Models.Authentication;
using Cico.Models.Helpers;
using PagedList;
using log4net;
using System.Dynamic;
using System.Text;

namespace Cico.Areas.Admin
{
    public class OfficeRight
    {
        public int Id { get; set; }
        public string OfficeName { get; set; }
        public string AccessLevel { get; set; }
    }

    public class AccessRight
    {
        public string FieldName { get; set; }
        public string FieldLabel { get; set; }
        public ICollection<OfficeRight> OfficeRights { get; set; }
    }

    public class TableAccess
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string OfficeName { get; set; }
        public string FieldLabel { get; set; }
        public string AccessLevel { get; set; }
    }

    public class AccessLevelModel

    {
        public List<DependentAccess> DependentAccess { get; set; }
        public List<WebGridColumn> WebGridColumns { get; set; }
        public string SelectedADM = "View";
        public List<EmployeeAccess> EmployeeAccessModel { get; set; }
        public ICollection<AccessRight> AccessRights { get; set; }
        public string SelectedAccess { get; set; }
        public Office StaffOffice { get; set; }
        public IList<Office> Offices { get; set; }
        public IEnumerable<SelectListItem> OfficeNames { get; set; }
        public string[] OfficeArray { get; set; }
        public IEnumerable<SelectListItem> DependentFields { get; set; }
        public IEnumerable<SelectListItem> EmployeeFields { get; set; }
        public IList<SelectListItem> EmployeeFieldNames { get; set; }
        public string DateCreated { get { return DateValue.HasValue ? DateValue.Value.ToShortDateString() : ""; } }
        public DateTime? DateValue { get; set; }
        public int TotalRows { get; set; }
        public int PageSize { get; set; }
        public bool EditEnabled { get; set; }
        public List<dynamic> GeneratedDataSource { get; set; }
        public IEnumerable<SelectListItem> AccessLevelList { get; set; }
        public List<TableAccess> TabAccess { get; set; }
        public void Load(ICicoContext db, UserSession session)
        {
            AccessLevelList = new List<SelectListItem>() {new SelectListItem()
                                                                  {Text = "Hide",
                                                                   Value = "Edit",
                                                                  },
                                                                {new SelectListItem()
                                                                  {Text = "View",
                                                                   Value = "View"}
                                                                 },
                                                                 {new SelectListItem()
                                                                  {Text = "Edit",
                                                                   Value = "Edit"}
                                                                  }};

            PageSize = 20;
            var accessRights = new List<AccessRight>();
            StaffOffice = session.GetCurrentStaffOffice();
            Offices = db.Offices.OrderBy(o => o.Name).ToList<Office>();
            var ol = new List<SelectListItem>();
            var sl = new List<string>();
            for  (var i = 0; i < Offices.Count; i++)
            { 
                Office office = Offices.ElementAt(i);
                ol.Add(new SelectListItem(){ Value = office.Name,
                                             Text =  office.Name});
                sl.Add(office.Name);
            }
            OfficeNames = ol.AsEnumerable();
            OfficeArray = sl.ToArray();

            EmployeeFieldNames = new List<SelectListItem>();
            
           
             

            //EmployeeAccess = db.EmployeeAccess.Where(e => e.Office.Name == StaffOffice.Name).ToList();
            //foreach (var empAcc in EmployeeAccess)
            //{
            //    var startIdx = empAcc.Name.ToString().LastIndexOf(" ");
            //    var len = empAcc.Name.ToString().Length;
            //    empAcc.Name = empAcc.Name.Substring(startIdx + 1, len - (startIdx + 1));
            //}
            DependentFields = typeof(Dependent).GetProperties().Select(a => new SelectListItem() { Text = a.Name.ToString(), Value = a.Name, }).ToList().OrderBy(a => a.Text);
            DependentAccess = db.DependentAccess.Where(e => e.Office.Name == StaffOffice.Name).ToList();
            var props = typeof(Employee).GetProperties();
            var ef = new List<SelectListItem>();
            foreach (var prop in props) 
            {
                DisplayNameAttribute[] ab = (DisplayNameAttribute[])prop.GetCustomAttributes(typeof(DisplayNameAttribute),true);
                if (ab.Count() > 0)
                {
                    ef.Add(new SelectListItem() { Text = ab[0].DisplayName, Value = ab[0].DisplayName });
                }
            }
            EmployeeFields = ef;
            EmployeeAccessModel = AddEmployeeAccess(db);
            
            foreach (var employeeField in EmployeeFieldNames)
            {
               
                var or = new List<OfficeRight>();
                var accRights = new AccessRight()
                {
                    FieldName = employeeField.Text,
                    OfficeRights = new List<OfficeRight>()
                };
                var empAccess = EmployeeAccessModel.SingleOrDefault(e => e.Name == employeeField.Text);
                if (empAccess != null)
                {
                    accRights.FieldLabel = empAccess.Name;
                }
                
                foreach (var office in Offices)
                {
                   
                    var emplAccess = EmployeeAccessModel.SingleOrDefault(e => e.Name == employeeField.Text);
                    if (emplAccess != null)
                    {
                        accRights.OfficeRights.Add(new OfficeRight()
                        { 
                            Id = emplAccess.Id,
                            OfficeName = office.Name
                        });
                    }
                    else
                    {
                        accRights.OfficeRights.Add(new OfficeRight()
                        {
                            OfficeName = office.Name,
                            AccessLevel = "Hide"   //default
                        });
                    }
                    
                }
                accessRights.Add(accRights);
            }
            AccessRights = accessRights;
        }
        private List<EmployeeAccess> AddEmployeeAccess(ICicoContext db)
        {
            var fieldNames = typeof(Employee).GetProperties().Select(a => new SelectListItem() { Text = a.ToString(), Value = a.Name, }).ToList().OrderBy(a => a.Text);
            var EmployeeAccessModel = new List<EmployeeAccess>();

            for (var i = 0; i < fieldNames.Count(); i++)
            {
                var fieldName = fieldNames.ToArray()[i];
                var employeeRow = db.EmployeeAccess.SingleOrDefault(e => e.Name == fieldName.Value);
                if (employeeRow == null)
                {
                    EmployeeAccessModel.Add(new EmployeeAccess()
                    {
                        Id = i,
                        Name = fieldName.Value,
                        Office0 = "Hide",
                        Office1 = "Hide",
                        Office2 = "Hide",
                        Office3 = "Hide",
                        Office4 = "Hide",
                        Office5 = "Hide",
                        Office6 = "Hide",
                        Office7 = "Hide",
                        Office8 = "Hide",
                        Office9 = "Hide",
                        Office10 = "Hide",
                        Office11 = "Hide",
                        Office12 = "Hide"
                    });
                }
                else
                {
                    EmployeeAccessModel.Add(employeeRow);
                }


            }
            return EmployeeAccessModel;
        }
       
    }

   
    //public IList<AccessRightsModel> AccessRightsModel { get; set; }
    //public int? Page { get; set; }
    //public bool EditEnabled { get; set; }
    //public IEnumerable<SelectListItem> Office { get; set; }

    //public void Load(ICicoContext db)
    //{
    //    List<AccessRightsModel> indexList = new List<AccessRightsModel>();

    //    foreach (var empFieldName in empFieldNames)
    //    {
    //        var accRightModel = new AccessRightsModel()
    //        {
    //            AccessLevel = ,
    //            AccessLevel = accRight.AccessType,
    //            SelectedOffice = accRight.Office.OfficeId,
    //            SelectedField = accRight.FieldName,
    //            FieldType = accRight.FieldType
    //        };
    //        accRightModel.Load(db);
    //        indexList.Add(accRightModel);
    //    }
    //    AccessRightsModel = indexList.ToPagedList(Page.Value, 50);

    //    Office = db.Offices.ToList().Select(c => new SelectListItem() { Text = c.Name, Value = c.OfficeId.ToString() }).ToList();
    //    Office = Office.OrderBy(c => c.Text);

    //}




    public class AccessRightsModel
    {
        public List<DependentAccess> DependentAccess { get; set; }
        public List<EmployeeAccess> EmployeeAccess { get; set; }
        public IEnumerable<SelectListItem> Office { get; set; }
        public IEnumerable<SelectListItem> DependentFields { get; set; }
        public IEnumerable<SelectListItem> EmployeeFields { get; set; }
        public string DateCreated { get { return DateValue.HasValue ? DateValue.Value.ToShortDateString() : ""; } }
        public DateTime? DateValue { get; set; }
        public void Load(ICicoContext context)
        {
            EmployeeFields = typeof(Employee).GetProperties().Select(a => new SelectListItem() { Text = a.Name.ToString(), Value = a.Name, }).ToList().OrderBy(a => a.Text);
            DependentFields = typeof(Dependent).GetProperties().Select(a => new SelectListItem() { Text = a.Name.ToString(), Value = a.Name, }).ToList().OrderBy(a => a.Text);
        }
    }
        public class AccessRightsController1 : Cico.Controllers.ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CheckListsController).Name);
        //
        // GET: /Access Rights/
        

        public ActionResult Index(AccessLevelModel model)
        {
             
            model.Load(Db, UserSession);
            var tabAccess = new List<TableAccess>();
            var data = GenerateGridDataSource(Db, model.EmployeeFields,model.AccessRights, tabAccess);

            List<WebGridColumn> webGridColumns = new List<WebGridColumn>();
            webGridColumns.Add(new WebGridColumn()
            {
                ColumnName = "Id",
                CanSort = false,
                Header = "Id"
               
            });
            webGridColumns.Add(new WebGridColumn()
            {
                ColumnName = "Name",
                CanSort = true,
                Header = "Fields",
                Format = (x) => { return new MvcHtmlString(GetFieldName(x)); }
            });
            var offices = Db.Offices.OrderBy(o => o.Name).ToArray();
            for (var i = 0; i <  Db.Offices.Count(); i++)
            {
                var columnName = string.Format("Office" + i.ToString());

                
                var wgc = new WebGridColumn()
                                    { CanSort = false,
                                      ColumnName = string.Format("Office" + i.ToString()),
                                      Header = offices[i].Name                                      
                                    };
                                 
                wgc.Format = (x) => { return new MvcHtmlString(GetPermission(x,wgc)); };
                webGridColumns.Add(wgc);
            };

            webGridColumns.Add(new WebGridColumn(){ ColumnName = "Action",
              Header = "Action",
              Format = (x) => { return new HtmlString(GetAction(x));}        
            });

             

            model.WebGridColumns = webGridColumns;
           
            //var al = data.SingleOrDefault(d => d.AccessLevel == "Hide");
            bool edit = false;
            //model.EditEnabled = edit || User.IsInRole(SystemRole.GlobalAdmin);
            model.EditEnabled = false;

            model.GeneratedDataSource = data;
            return View(model);
        }


        private string GetFieldName(dynamic x)
        {
          
            
            var builder = new TagBuilder("span");
            WebGridRow ea = (WebGridRow)x;
            var idx = ea.Value.Id;

           // builder.InnerHtml = "<span id=\"spanfield_" + idx + "\" class=\"editor-field\">" + System.Web.Mvc.Html.SelectExtensions.DropDownListFor(z => z.AccessCode, x.EmployeeFields, "") + ">" + x.Name + "</span> <input id=Name_" + idx + " value=\"" + x.Name + "\" name=\"" + x.Name + "\" type=\"text\" style=\"color:blue; display:none\" class=\"homescreen-display\">";
            return builder.ToString();

           
        }
        private string GetAction(dynamic x)
        {
            var sb = new StringBuilder();
            WebGridRow ea = (WebGridRow)x;
            var idx = ea.Value.Id;
            sb.Append("<a href=\"#\" id=\"Edit_" + idx + "\" class=\"edit\">Edit</a><a href=\"#\" id=\"Update_" + idx + "\" style=\"display:none\" class=\"update\">Update</a><a href=\"#\" id=\"Cancel_" + idx + "\" style=\"display:none\"  class=\"cancel\">Cancel</a><a href=\"#\" id=\"Delete_" + idx + "\"  class=\"delete\">Delete</a>");
            return sb.ToString();
        }

        private string GetPermission(dynamic x,WebGridColumn wgc)
        {
            var builder = new TagBuilder("span");
            WebGridRow ea = (WebGridRow)x;
            var idx = ea.Value.Id;
            var eav = (EmployeeAccess)ea.Value;
            string al;
            string ids; 
            switch (wgc.ColumnName)
            {
                case "Office0": al = eav.Office0;
                    ids = "Office0_" + idx;
                    break;
                case "Office1": al = eav.Office1;
                    ids = "Office1_" + idx;
                    break;
                case "Office2": al = eav.Office2;
                    ids = "Office2_" + idx;
                    break;
                case "Office3": al = eav.Office3;
                    ids = "Office3_" + idx;
                    break;
                case "Office4": al = eav.Office4;
                    ids = "Office4_" + idx;
                    break;
                case "Office5": al = eav.Office5;
                    ids = "Office5_" + idx;
                    break;
                case "Office6": al = eav.Office6;
                    ids = "Office6_" + idx;
                    break;
                case "Office7": al = eav.Office7;
                    ids = "Office7_" + idx;
                    break;
                case "Office8": al = eav.Office8;
                    ids = "Office8_" + idx;
                    break;
                case "Office9": al = eav.Office9;
                    ids = "Office9_" + idx;
                    break;
                case "Office10": al = eav.Office10;
                    ids = "Office10_" + idx;
                    break;
                case "Office11": al = eav.Office11;
                    ids = "Office11_" + idx;
                    break;
                case "Office12": al = eav.Office12;
                    ids = "Office12_" + idx;
                    break;
                default: al = ""; ids = "";
                    break;
                     
            }
            builder.InnerHtml = "<span id=\"span" + ids + "\">" + al + "</span> <input id=" + ids + " value=\"" + al + "\" name=\"" + al + "\" style=\"display:none\" class=\"homescreen-display\">";
            return builder.ToString();
            
        }

          public static List<dynamic> GenerateGridDataSource( CicoContext db, IEnumerable<SelectListItem> employeeFields, ICollection<AccessRight> employeeAccess, List<TableAccess> TabAccess)
            {
                //initialize list of dynamic objects
                var dataList = new List<dynamic>();
                var tableAccess = new List<TableAccess>();
                var emplAccess = db.EmployeeAccess;

                //loop through each field
                foreach (var ef in employeeFields)
                {
                    dynamic expandoObject = new ExpandoObject();
                    Dictionary<int, string> accessRights = new Dictionary<int, string>();
                    var dictionaryExpandoObject = (IDictionary<string, object>)expandoObject;
                    var accRights = emplAccess.SingleOrDefault( e => e.Name  == ef.Text);
                    if (accRights != null)
                    {
                        dictionaryExpandoObject.Add("Name", accRights.Name);

                        //loop through each elements of access rights for each office
                        //foreach (var or in accRights.Office)
                        //{
                        //    //adding the property name and value to the dictionary
                        //    dictionaryExpandoObject.Add(or.OfficeName, or.AccessLevel);
                        //    var ti = new TableAccess()
                        //    {
                        //        Id = or.Id,
                        //        FieldName = ef.Text,
                        //        OfficeName = or.OfficeName,
                        //        AccessLevel = or.AccessLevel
                        //    };
                        //    tableAccess.Add(ti);
                        //}

                        //finally add each ExpandoObject to list
                        dataList.Add(dictionaryExpandoObject);
                        //dataList.Add(tableAccess);
                        TabAccess = tableAccess;
                    }
                    else
                    {
                        
                    }

                }

                return dataList;
            }
        



        public ActionResult Edit(int id)
        {
            AccessRightsModel accRightsModel = new AccessRightsModel();
            accRightsModel.Load(Db);
            //accRightsModel.EmployeeAccess = Db.EmployeeAccess.SingleOrDefault();
            //accRightsModel.SelectedField = accRightsModel.AccessRights.Name;
            //accRightsModel.SelectedOffice = accRightsModel.AccessRights.Office.OfficeId;
            return View(accRightsModel);
        }

        [HttpPost]
        public ActionResult Edit(AccessRightsModel accRights)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    //if (accRights.SelectedOffice.HasValue)
                    //{
                    //    accRights.AccessRights.Office = Db.Offices.Single(c => c.OfficeId == accRights.SelectedOffice);
                    //}
                    //if (!String.IsNullOrEmpty(accRights.SelectedField))
                    //{ 
                    //    accRights.AccessRights.FieldName = accRights.SelectedField;
                    //}
                    //accRights.DateValue = DateTime.Now;
                }
                Db.Entry(accRights.EmployeeAccess).State = System.Data.EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accRights);
        }

        public ActionResult Delete(int id)
        {
            var accessRights = Db.EmployeeAccess.Find(id);
            return View(accessRights);
        }

        //
        // POST: /Admin/CheckListItemTemplate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
           // AccessLevelModel accessRights = Db.a.Find(id);
            //Db.AccessRights.Remove(accessRights);
           // Db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Admin/AccessRights/Save

        public ActionResult Create()
        {
            var model = new AccessLevelModel() { };
            model.Load(Db, UserSession);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AccessRightsModel model)
        {
            //Validate(model);

            //if (Db.AccessRights.Where(c => c.Name == model.SelectedField && c.Office.OfficeId == model.SelectedOffice).Any())
            //{
            //    ModelState.AddModelError("", "Access Rights definition already exists");
            //}
            //if (ModelState.IsValid)
            //{
            //    if (model.SelectedOffice.HasValue)
            //    {
            //        model.AccessRights.Office = Db.Offices.Single(c => c.OfficeId == model.SelectedOffice);
            //    }
            //    model.AccessRights.FieldName = model.SelectedField;
            //    model.DateValue = DateTime.Now;
            //    Db.AccessRights.Add(model.AccessRights);
            //    return RedirectToAction("index");
            //}
            //else
            //{
            //    model.Load(Db);
            return View(model);
            //}
        }

        public ActionResult Show(int id)
        {
            var model = new CheckListModel();
            model.Session = Db.CheckListSessions.Include("CheckListItemSubmitionTracks").Include("Employee").Single(c => c.Id == id);
            model.Employee = model.Session.Employee;
            if (UserSession.IsOfficeAdmin)
            {
                var staff = UserSession.GetCurrentStaff();
                model.SessionTracks =
                    model.Session.CheckListItemSubmitionTracks.Where(
                        c => c.CheckListItemTemplate.Office.OfficeId == staff.Office.OfficeId & c.CheckListItemTemplate.Active).ToList();
            }
            else
            {
                model.SessionTracks =
                    model.Session.CheckListItemSubmitionTracks.Where(c => c.CheckListItemTemplate.Active).ToList();
            }
            model.SessionTracks = model.SessionTracks.Where(c => SecurityGuard.CanCompleteCheckListItem(c)).ToList();
            return View(model);
        }

        public void Validate(AccessLevelModel model)
        {
            //if (String.IsNullOrEmpty(model.SelectedField))
            //{
            //    ModelState.AddModelError("", "A Field Name must be selected");
            //    return;
            //}
            //if (!model.SelectedOffice.HasValue)
            //{
            //    ModelState.AddModelError("", "You must pick the office ");
            //}
        }

        public ActionResult ApproveProvisional(int ItemId)
        {
            var item = Db.CheckListItemSubmitionTracks.Single(c => c.Id == ItemId);
            item.Provisioned = true;
            item.CheckListItemTemplate.CompletingChecklist = true;
            return RedirectToAction("show", new { id = item.CheckListSession.Id });
        }

        public ActionResult RejectProvisional(int ItemId)
        {
            var item = Db.CheckListItemSubmitionTracks.Single(c => c.Id == ItemId);
            item.Checked = false;
            //remove the links to  any uploaded documents
            if (item.SubmittedFile != null)
            {
                var systemFile = Db.SystemFiles.SingleOrDefault(s => s.Id == item.SubmittedFile.Id);
                if (systemFile != null)
                {
                    Db.SystemFiles.Remove(systemFile);
                }
            }
            log.DebugFormat("Dependent File count: {0}", item.DependentFiles.Count);
            item.SubmittedFile = null;
            item.DependentFiles = null;

            ///if checklist item completes the checklist we need to uncomplete
            if (item.CheckListItemTemplate.CompleteCheckList)
            {
                item.CheckListSession.Completed = false;
                item.CheckListItemTemplate.CompletingChecklist = false;
            }
            return RedirectToAction("show", new { id = item.CheckListSession.Id });
        }

        public ActionResult Cancel(int id)
        {
            var session = Db.CheckListSessions.Find(id);
            session.Active = false;
            return RedirectToAction("index");
        }

            
        [HttpGet]
        public JsonResult UpdateRecord(int id, string name, string office0
                                                          , string office1
            , string office2
            , string office3
            , string office4
            , string office5
            , string office6
            , string office7
            , string office8
            , string office9
            , string office10
            , string office11
            , string office12
                                      )
        {
            bool result = false;
            try
            {
               var newAccessLevel = new EmployeeAccess();
               var accessLevel = Db.EmployeeAccess.SingleOrDefault(e => e.Name == name);
               if (accessLevel == null)
               {
                   

                   newAccessLevel.Name = name;
                   newAccessLevel.Office0 = office0;
                   newAccessLevel.Office1 = office1;
                   newAccessLevel.Office2 = office2;
                   newAccessLevel.Office3 = office3;
                   newAccessLevel.Office4 = office4;
                   newAccessLevel.Office5 = office5;
                   newAccessLevel.Office6 = office6;
                   newAccessLevel.Office7 = office7;
                   newAccessLevel.Office8 = office8;
                   newAccessLevel.Office9 = office9;
                   newAccessLevel.Office10 = office10;
                   newAccessLevel.Office11 = office11;
                   newAccessLevel.Office12 = office12;
                  

                   Db.EmployeeAccess.Add(newAccessLevel);
                  
                   Db.SaveChanges();
                    
               }
               else
               {
                   accessLevel.Name = name;
                   accessLevel.Office0 = office0;
                   accessLevel.Office1 = office1;
                   accessLevel.Office2 = office2;
                   accessLevel.Office3 = office3;
                   accessLevel.Office4 = office4;
                   accessLevel.Office5 = office5;
                   accessLevel.Office6 = office6;
                   accessLevel.Office7 = office7;
                   accessLevel.Office8 = office8;
                   accessLevel.Office9 = office9;
                   accessLevel.Office10 = office10;
                   accessLevel.Office11 = office11;
                   accessLevel.Office12 = office12;

                  
                   Db.SaveChanges();
               }
                result = true;
            }
            catch (Exception ex)
            {
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SaveRecord(int id, string name, string office0
                                                          , string office1
            , string office2
            , string office3
            , string office4
            , string office5
            , string office6
            , string office7
            , string office8
            , string office9
            , string office10
            , string office11
            , string office12
                                      )
        {
            bool result = false;
            try
            {
                var newAccessLevel = new EmployeeAccess();
              
                   

                   newAccessLevel.Name = name;
                   newAccessLevel.Office0 = office0;
                   newAccessLevel.Office1 = office1;
                   newAccessLevel.Office2 = office2;
                   newAccessLevel.Office3 = office3;
                   newAccessLevel.Office4 = office4;
                   newAccessLevel.Office5 = office5;
                   newAccessLevel.Office6 = office6;
                   newAccessLevel.Office7 = office7;
                   newAccessLevel.Office8 = office8;
                   newAccessLevel.Office9 = office9;
                   newAccessLevel.Office10 = office10;
                   newAccessLevel.Office11 = office11;
                   newAccessLevel.Office12 = office12;
                  

                   Db.EmployeeAccess.Add(newAccessLevel);
                  
                   Db.SaveChanges();
                    

            }
            catch (Exception ex)
            {
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteRecord(int id)
        {
            bool result = false;
            try
            {
                result = false;

            }
            catch (Exception ex)
            {
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

    }
    
}

