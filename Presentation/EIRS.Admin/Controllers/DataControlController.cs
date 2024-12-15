using EIRS.BLL;
using EIRS.BOL;
using System.Collections.Generic;
using System.Web.Mvc;
using Elmah;
using EIRS.Repository;
using System;
using EIRS.Common;
using System.Linq;
using System.Text;
using Microsoft.Ajax.Utilities;
using EIRS.Admin.Models;
using System.Runtime.Remoting.Contexts;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PagedList;
using System.Security.Policy;

namespace EIRS.Admin.Controllers
{
    public class DataControlController : BaseController
    {
        EirsDbContext _db;
        EIRSEntities _db2;
        IAssessmentRuleRepository repo = new AssessmentRuleRepository();
        // GET: DataControl
        public ActionResult TaxPayerWithoutAsset()
        {
            UI_FillTaxPayerTypeDropDown();
            UI_FillAssetTypeDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult SearchTaxPayerWithoutAsset(FormCollection pObjFormCollection)
        {
            string strTaxPayerTypeIds = pObjFormCollection.Get("cboTaxPayerType");
            string strAssetTypeIds = pObjFormCollection.Get("cboAssetType");

            IList<usp_DC_GetTaxPayerWithoutAsset_Result> lstTaxPayerWithoutAsset = new BLDataControl().BL_GetTaxPayerWithoutAsset(strTaxPayerTypeIds, strAssetTypeIds);
            return PartialView(lstTaxPayerWithoutAsset);
        }

        public ActionResult SyncToNewYear()
        {
            int year = Convert.ToInt32(DateTime.Now.Year.ToString());
            ViewBag.Year = year + 1;
            if (DateTime.Today.ToString() == $"01/01/{year}")
            {
                ViewBag.Year = year;
            }

            List<TempAssHolder> lsttempAssHolder = new List<TempAssHolder>();

            IList<DataControlRepository.AssessmentRuleRollover> lstAssessmentAndItem = new BLDataControl().BL_GetAssessmentAndRule();
            List<DataControlRepository.AssessmentRuleRollover> ARL = new List<DataControlRepository.AssessmentRuleRollover>();
            if (lstAssessmentAndItem.Count > 0)
            {
                ARL = lstAssessmentAndItem.DistinctBy(p => new { p.AssessmentRuleCode, p.AssessmentRuleName, p.AssessmentAmount, p.Profileid, p.Taxyear, p.RuleRunId, p.Paymentfrequencyid }).ToList();
                ARL.Count();
                var res = repo.REP_InsertUpdateAssessmentRule(ARL);
                if (res.Success)
                {
                    using (_db = new EirsDbContext())
                    {
                        foreach (var ret in res.AdditionalData.ToList())
                        {
                            TempAssHolder tempAssHolder = new TempAssHolder();
                            tempAssHolder.AssessmentRuleCode = ret.AssessmentRuleCode;
                            tempAssHolder.AssessmentRuleId = ret.AssessmentRuleID;

                            lsttempAssHolder.Add(tempAssHolder);
                            _db.TempAssHolder.Add(tempAssHolder);
                        }
                        try
                        {
                            SessionManager.lstTempAssHolder = lsttempAssHolder;
                            _db.SaveChanges();
                            foreach (var tempAssHolder in lsttempAssHolder)
                                _db.Database.ExecuteSqlCommand($"Update Assessment_Rules set AssessmentRuleCode = {tempAssHolder.AssessmentRuleCode} where AssessmentRuleID = {tempAssHolder.AssessmentRuleId}");

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            return View();
        }
        public ActionResult ProfileWithoutRule()
        {
            IList<usp_DC_GetProfileWithoutRule_Result> lstProfileWithoutRule = new BLDataControl().BL_GetProfileWithoutRule();
            return View(lstProfileWithoutRule);
        }
        public ViewResult AssessmentRollOverStep1(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";
            List<TempAssHolder> lsttempAssHolder = new List<TempAssHolder>();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            int year = Convert.ToInt32(DateTime.Now.Year.ToString());
            ViewBag.Year = year + 1;
            if (DateTime.Today.ToString() == $"01/01/{year}")
            {
                ViewBag.Year = year;
            }

            List<Assessment_Rules> assessment_Rules = new List<Assessment_Rules>();
            IList<AssessmentRuleRollover> assessments = new List<AssessmentRuleRollover>();
            IList<AssessmentAndItemRollOver> lstAssessmentItems = GetAssessmentAndItem();
            if (page.HasValue)
            {
                assessment_Rules = SessionManager.lstAssesRule.ToList();
                int pageSize = 20;
                int pageNumber = page.Value;
                var retVal = assessment_Rules.ToPagedList(pageNumber, pageSize);
                return View(retVal);
            }
            else
            {
                using (_db = new EirsDbContext())
                {
                    assessments = GetAssessmentAndRule();
                    _db.AssessmentRuleRollover.AddRange(assessments);
                    var ret = _db.SaveChanges();

                    if (ret != 0)
                    {
                        assessments = assessments.DistinctBy(p => new { p.AssessmentRuleCode, p.AssessmentRuleName, p.AssessmentAmount, p.Profileid, p.Taxyear, p.RuleRunId, p.Paymentfrequencyid }).ToList();
                        assessments.Count();
                        var res = AddAssessmentRule(assessments.ToList());
                        if (res.Success)
                        {
                            using (_db = new EirsDbContext())
                            {
                                foreach (var ret2 in res.AdditionalData.ToList())
                                {
                                    TempAssHolder tempAssHolder = new TempAssHolder();
                                    tempAssHolder.AssessmentRuleCode = ret2.AssessmentRuleCode;
                                    tempAssHolder.AssessmentRuleId = ret2.AssessmentRuleID;

                                    lsttempAssHolder.Add(tempAssHolder);
                                    _db.TempAssHolder.Add(tempAssHolder);
                                }
                                try
                                {
                                    SessionManager.lstTempAssHolder = lsttempAssHolder;
                                    _db.SaveChanges();
                                    foreach (var tempAssHolder in lsttempAssHolder)
                                    {
                                        _db.Database.ExecuteSqlCommand($"Update Assessment_Rules set AssessmentRuleCode = {tempAssHolder.AssessmentRuleCode} where AssessmentRuleID = {tempAssHolder.AssessmentRuleId}");

                                    }

                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            int pageSize = 20;
                            int pageNumber = (page ?? 1);
                            SessionManager.lstAssesRule = res.AdditionalData;
                            var retVal = res.AdditionalData.ToPagedList(pageNumber, pageSize);
                            return View(retVal);
                        }
                    }
                }
            }
            return View();
        }
        public ViewResult AssessmentRollOverStep2(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";
            List<AssessmentAndItemRollOver> lsttempAssHolder = new List<AssessmentAndItemRollOver>();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            List<Assessment_Rules> assessment_Rules = new List<Assessment_Rules>();
            if (page.HasValue)
            {
                assessment_Rules = SessionManager.lstAssesRule.ToList();
                int pageSize = 20;
                int pageNumber = page.Value;
                var retVal = assessment_Rules.ToPagedList(pageNumber, pageSize);
                return View(retVal);
            }
            else
            {
                int year = DateTime.Now.Year;
                year = year + 1;
                using (_db2 = new EIRSEntities())
                {
                    assessment_Rules = _db2.Assessment_Rules.Where(o => o.TaxYear == year).ToList(); ;
                }
                if (assessment_Rules.Count != 0)
                {
                    using (_db = new EirsDbContext())
                    {
                        try
                        {
                            foreach (var tempAssHolder in assessment_Rules)
                            {
                                string ruleNmae = tempAssHolder.AssessmentRuleName;
                                _db.Database.ExecuteSqlCommand($"Update AssessmentRuleRollover set NewAssessmentRuleId = {tempAssHolder.AssessmentRuleID} where AssessmentRuleName  =" + "'" + ruleNmae + "'");
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    int pageSize = 20;
                    int pageNumber = (page ?? 1);
                    //  assessment_Rules = SessionManager.lstAssesRule.ToList();
                    var retVal = assessment_Rules.ToPagedList(pageNumber, pageSize);
                    return View(retVal);
                }

            }
            return View();
        }
        public ViewResult AssessmentRollOverStep3(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<AssessmentRuleRollover> lsttempAssHolder = new List<AssessmentRuleRollover>();
            List<MAP_AssessmentRule_AssessmentItem> lstMap = new List<MAP_AssessmentRule_AssessmentItem>();
            using (_db = new EirsDbContext())
            {
                lsttempAssHolder = _db.AssessmentRuleRollover.ToList();
            }
            if (lsttempAssHolder.Count > 0)
            {
                using (_db2 = new EIRSEntities())
                {
                    foreach (var item in lsttempAssHolder)
                    {
                        MAP_AssessmentRule_AssessmentItem mAP_AssessmentRule_AssessmentItem = new MAP_AssessmentRule_AssessmentItem();
                        mAP_AssessmentRule_AssessmentItem.AssessmentItemID = item.AssessmentItemID;
                        mAP_AssessmentRule_AssessmentItem.AssessmentRuleID = item.NewAssessmentRuleID;
                        mAP_AssessmentRule_AssessmentItem.CreatedBy = 1;
                        mAP_AssessmentRule_AssessmentItem.CreatedDate = DateTime.Now;

                        lstMap.Add(mAP_AssessmentRule_AssessmentItem);
                    }

                    _db2.MAP_AssessmentRule_AssessmentItem.AddRange(lstMap);
                    var ret = _db2.SaveChanges();
                    if (ret != 0)
                    {
                        ViewBag.Res = "Roll-Over Done Successfully";
                        return View();
                    }
                    ViewBag.Res = "Mapping Not Successfully Done";
                    return View();
                }
            }
            ViewBag.Res = "No Record Found";
            return View();
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            IList<AssessmentAndItemRollOver> assessmentAndItemRollOvers = GetAssessmentAndItem();

            var lstAss = assessmentAndItemRollOvers;

            if (!String.IsNullOrEmpty(searchString))
            {
                lstAss = lstAss.Where(s =>
                        s.AssessmentRuleCode.Contains(searchString) ||
                        s.AssessmentRuleName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    lstAss = lstAss.OrderByDescending(s => s.AssessmentRuleName).ToList();
                    break;
                case "date":
                    lstAss = lstAss.OrderBy(s => s.AssessmentRuleCode).ToList();
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var ret = lstAss.ToPagedList(pageNumber, pageSize);
            return View(ret);
        }
        public ViewResult ServiceIndex(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            IList<ServiceBillAndItemRollOver> ServiceBillAndItemRollOvers = GetServiceBillAndItem();

            var lstSer = ServiceBillAndItemRollOvers;

            if (!String.IsNullOrEmpty(searchString))
            {
                lstSer = lstSer.Where(s =>
                        s.MDAServiceName.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "nameDesc":
                    lstSer = lstSer.OrderByDescending(s => s.MDAServiceName).ToList();
                    break;
            }
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            var ret = lstSer.ToPagedList(pageNumber, pageSize);
            return View(ret);
        }
        public ViewResult ServiceBillRollOverStep1(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";
            List<TempMdaHolder> lsttempMdaHolder = new List<TempMdaHolder>();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            int year = Convert.ToInt32(DateTime.Now.Year.ToString());
            ViewBag.Year = year + 1;
            if (DateTime.Today.ToString() == $"01/01/{year}")
            {
                ViewBag.Year = year;
            }

            List<MDA_Services> MDAServices = new List<MDA_Services>();
            IList<MdaserviceRollover> Mdaservice = new List<MdaserviceRollover>();
            IList<MdaserviceRollover> lstMDAServiceandMDAServiceItem = GetMDAServiceandMDAServiceItem();
            if (page.HasValue)
            {
                MDAServices = SessionManager.lstMDAServices.ToList();
                int pageSize = 20;
                int pageNumber = page.Value;
                var retVal = MDAServices.ToPagedList(pageNumber, pageSize);
                return View(retVal);
            }
            else
            {
                using (_db = new EirsDbContext())
                {
                    Mdaservice = GetMDAServiceandMDAServiceItem();
                    _db.MdaserviceRollover.AddRange(Mdaservice);
                    var ret = _db.SaveChanges();

                    if (ret != 0)
                    {
                        Mdaservice = Mdaservice.DistinctBy(p => new { p.MdaserviceCode, p.MdaserviceId, p.MdaserviceItemId, p.MdaserviceName, p.Msmiid, p.TaxYear, p.RuleRunId, p.PaymentFrequencyId }).ToList();
                        Mdaservice.Count();
                        var res = AddMDAServices(Mdaservice.ToList());
                        if (res.Success)
                        {
                            using (_db = new EirsDbContext())
                            {
                                foreach (var ret2 in res.AdditionalData.ToList())
                                {
                                    TempMdaHolder tempMdaHolder = new TempMdaHolder();
                                    tempMdaHolder.MDAServiceCode = ret2.MDAServiceCode;
                                    tempMdaHolder.MdaserviceId = ret2.MDAServiceID;

                                    lsttempMdaHolder.Add(tempMdaHolder);
                                    _db.TempMdaHolder.Add(tempMdaHolder);
                                }
                                try
                                {
                                    SessionManager.lsttempMdaHolder = lsttempMdaHolder;
                                    _db.SaveChanges();
                                    foreach (var tempMdaHolder in lsttempMdaHolder)
                                    {
                                        _db.Database.ExecuteSqlCommand($"Update MDA_Services set MdaserviceCode = {tempMdaHolder.MDAServiceCode} where MdaserviceId = {tempMdaHolder.MdaserviceId}");

                                    }

                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                            int pageSize = 20;
                            int pageNumber = (page ?? 1);
                            SessionManager.lstMDAServices = res.AdditionalData;
                            var retVal = res.AdditionalData.ToPagedList(pageNumber, pageSize);
                            return View(retVal);
                        }
                    }
                }
            }
            return View();
        }
        public ViewResult ServiceBillRollOverStep2(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "dateDesc" : "date";
        
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            List<MDA_Services> MDAServices = new List<MDA_Services>();
       
            if (page.HasValue)
            {
                MDAServices = SessionManager.lstMDAServices.ToList();
                int pageSize = 20;
                int pageNumber = page.Value;
                var retVal = MDAServices.ToPagedList(pageNumber, pageSize);
                return View(retVal);
            }
            else
            {
                int year = DateTime.Now.Year;
                year = year + 1;
                using (_db2 = new EIRSEntities())
                {
                    MDAServices = _db2.MDA_Services.Where(o => o.TaxYear == year).ToList(); ;
                }
                if (MDAServices.Count != 0)
                {
                    using (_db = new EirsDbContext())
                    {
                        try
                        {
                            foreach (var tempMDAHolder in MDAServices)
                            {
                                string ruleNmae = tempMDAHolder.MDAServiceName;
                                _db.Database.ExecuteSqlCommand($"Update MdaserviceRollover set NewMdaserviceId = {tempMDAHolder.MDAServiceID} where MdaserviceName  =" + "'" + ruleNmae + "'");
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                    int pageSize = 20;
                    int pageNumber = (page ?? 1);
                    var retVal = MDAServices.ToPagedList(pageNumber, pageSize);
                    return View(retVal);
                }

            }
            return View();
        }
        public ViewResult ServiceBillRollOverStep3(string sortOrder, string currentFilter, string searchString, int? page)
        {
            List<MdaserviceRollover> lsttempMdaHolder = new List<MdaserviceRollover>();
            List<MAP_MDAService_MDAServiceItem> lstMap = new List<MAP_MDAService_MDAServiceItem>();
            using (_db = new EirsDbContext())
            {
                lsttempMdaHolder = _db.MdaserviceRollover.ToList();
            }
            if (lsttempMdaHolder.Count > 0)
            {
                using (_db2 = new EIRSEntities())
                {
                    foreach (var item in lsttempMdaHolder)
                    {
                        MAP_MDAService_MDAServiceItem MapMDAServiceMdaServiceItem = new MAP_MDAService_MDAServiceItem();
                        MapMDAServiceMdaServiceItem.MDAServiceItemID = item.MdaserviceItemId;
                        MapMDAServiceMdaServiceItem.MDAServiceID = item.NewMdaserviceId;
                        MapMDAServiceMdaServiceItem.CreatedBy = 1;
                        MapMDAServiceMdaServiceItem.CreatedDate = DateTime.Now;

                        lstMap.Add(MapMDAServiceMdaServiceItem);
                    }

                    _db2.MAP_MDAService_MDAServiceItem.AddRange(lstMap);
                    var ret = _db2.SaveChanges();
                    if (ret != 0)
                    {
                        ViewBag.Res = "Roll-Over Done Successfully";
                        return View();
                    }
                    ViewBag.Res = "Mapping Not Successfully Done";
                    return View();
                }
            }
            ViewBag.Res = "No Record Found";
            return View();
        }

        public ActionResult AssessmentRulesWithAssessmentItems()
        {
            IList<DataControlRepository.AssessmentAndItemRollOver> lstAssessmentAndItem = new BLDataControl().BL_GetAssessmentAndItem();
            ViewBag.PresentYear = DateTime.Now.Year.ToString();
            return View(lstAssessmentAndItem);
        }

        public ActionResult AssessmentItemWithoutRule()
        {
            IList<usp_DC_GetAssessmentItemWithoutRule_Result> lstAIWithoutRule = new BLDataControl().BL_GetAssessmentItemWithoutRule();
            return View(lstAIWithoutRule);
        }

        public ActionResult IndividualWithoutAssessment()
        {
            IList<usp_DC_GetIndividualWithoutAssessment_Result> lstIndividualWithoutAssessment = new BLDataControl().BL_GetIndividualWithoutAssessment();
            return View(lstIndividualWithoutAssessment);
        }

        public ActionResult CompanyWithoutAssessment()
        {
            IList<usp_DC_GetCompanyWithoutAssessment_Result> lstCompanyWithoutAssessment = new BLDataControl().BL_GetCompanyWithoutAssessment();
            return View(lstCompanyWithoutAssessment);
        }

        [NonAction]
        public FuncResponse<List<Assessment_Rules>> AddAssessmentRule(List<AssessmentRuleRollover> roll)
        {
            var currentYear = "2025";
            List<Assessment_Rules> rlollover = new List<Assessment_Rules>();
            string substring = currentYear.Substring(2, 2);
            FuncResponse<List<Assessment_Rules>> mObjFuncResponse = new FuncResponse<List<Assessment_Rules>>(); //Return Object
            using (_db2 = new EIRSEntities())
            {
                foreach (var rl in roll)
                {
                    bool stat = false;
                    if (rl.Active == "True")
                        stat = true;
                    var kk = rl.AssessmentRuleCode.ToString();
                    var k2 = kk.Substring(0, kk.Length - 2);
                    var k1 = k2 + substring;
                    Assessment_Rules rollover = new Assessment_Rules();
                    rollover.AssessmentRuleName = rl.AssessmentRuleName;
                    rollover.AssessmentRuleID = rl.AssessmentRuleID;
                    rollover.AssessmentRuleCode = k1;
                    rollover.CreatedDate = DateTime.Now;
                    rollover.PaymentFrequencyID = rl.Paymentfrequencyid;
                    rollover.ProfileID = rl.Profileid;
                    rollover.AssessmentAmount = rl.AssessmentAmount;
                    rollover.TaxYear = Convert.ToInt32(currentYear);
                    rollover.PaymentOptionID = 1;
                    rollover.RuleRunID = rl.RuleRunId;
                    rollover.CreatedBy = -1;
                    rollover.Active = stat;

                    _db2.Assessment_Rules.Add(rollover);
                    rlollover.Add(rollover);
                }
                try
                {
                    _db2.SaveChanges();
                    mObjFuncResponse.AdditionalData = rlollover;
                    mObjFuncResponse.Success = true;
                }
                catch
                {
                    mObjFuncResponse.Success = false;
                }
                return mObjFuncResponse;
            }
        }

        [NonAction]
        public FuncResponse<List<MDA_Services>> AddMDAServices(List<MdaserviceRollover> roll)
        {
            var currentYear = "2025";
            List<MDA_Services> rlollover = new List<MDA_Services>();
            string substring = currentYear.Substring(2, 2);
            FuncResponse<List<MDA_Services>> mObjFuncResponse = new FuncResponse<List<MDA_Services>>();

            if (roll == null)
            {
                mObjFuncResponse.Success = false;
                mObjFuncResponse.Message = "The input list is null.";
                return mObjFuncResponse;
            }

            using (_db2 = new EIRSEntities())
            {
                if (_db2 == null)
                {
                    mObjFuncResponse.Success = false;
                    mObjFuncResponse.Message = "Database context is not initialized.";
                    return mObjFuncResponse;
                }

                foreach (var rl in roll)
                {
                    if (rl == null) continue;
                    bool stat = rl.Active;
                    if (string.IsNullOrEmpty(rl.MdaserviceCode)) continue;

                    var kk = rl.MdaserviceCode.ToString();
                    var k2 = kk.Substring(0, kk.Length - 2);
                    //var k1 = k2 + substring;
                    var k1 = k2 + "-" + substring;

                    MDA_Services rollover = new MDA_Services
                    {
                        MDAServiceName = rl.MdaserviceName ?? string.Empty,
                        MDAServiceCode = k1,
                        CreatedDate = DateTime.Now,
                        PaymentFrequencyID = rl.PaymentFrequencyId,
                        RuleRunID = rl.RuleRunId,
                        ServiceAmount = rl.ServiceAmount.HasValue ? rl.ServiceAmount.Value : 0m,
                        TaxYear = Convert.ToInt32(currentYear),
                        PaymentOptionID = 1,
                        CreatedBy = -1,
                        Active = stat
                    };

                    _db2.MDA_Services.Add(rollover);
                    rlollover.Add(rollover);
                }
                try
                {
                    _db2.SaveChanges();
                    mObjFuncResponse.AdditionalData = rlollover;
                    mObjFuncResponse.Success = true;
                }
                catch (Exception ex)
                {
                    mObjFuncResponse.Success = false;
                    mObjFuncResponse.Message = $"An error occurred while saving changes: {ex.Message}";
                }
                return mObjFuncResponse;
            }
        }

        [NonAction]
        public IList<AssessmentAndItemRollOver> GetAssessmentAndItem()
        {
            using (_db2 = new EIRSEntities())
            {
                List<AssessmentAndItemRollOver> assessmentAndItems = new List<AssessmentAndItemRollOver>();
                var presentYear = DateTime.Now.Year;
                var newYear = presentYear;
                var retVal = (from r in _db2.Assessment_Rules
                              join a in _db2.MAP_AssessmentRule_AssessmentItem
                              on r.AssessmentRuleID equals a.AssessmentRuleID
                              join b in _db2.Assessment_Items
                              on a.AssessmentItemID equals b.AssessmentItemID
                              where r.TaxYear == presentYear
                              select new
                              {
                                  active = r.Active,
                                  taxYear = newYear,
                                  taxMonth = r.TaxMonth,
                                  percentage = b.Percentage,
                                  taxBaseAmount = b.TaxBaseAmount,
                                  taxAmount = b.TaxAmount,
                                  assessmentItemName = b.AssessmentItemName,
                                  assessmentRuleCode = r.AssessmentRuleCode,
                                  assessmentRuleName = r.AssessmentRuleName,
                                  assessmentAmount = r.AssessmentAmount,
                                  assessmentRuleId = r.AssessmentRuleID
                              }).ToList();

                foreach (var item in retVal)
                {
                    AssessmentAndItemRollOver andItem = new AssessmentAndItemRollOver();
                    andItem.AssessmentAmount = item.assessmentAmount.Value;
                    andItem.TaxYear = item.taxYear.ToString();
                    andItem.TaxMonth = item.taxMonth.ToString();
                    andItem.Active = item.active.Value;
                    andItem.TaxAmount = item.taxAmount.Value;
                    andItem.Percentage = item.percentage.HasValue ? item.percentage.Value : 0;
                    andItem.TaxBaseAmount = item.taxBaseAmount.Value;
                    andItem.AssessmentItemName = item.assessmentItemName;
                    andItem.AssessmentRuleCode = item.assessmentRuleCode;
                    andItem.AssessmentRuleName = item.assessmentRuleName;
                    andItem.AssessmentRuleId = item.assessmentRuleId;
                    andItem.NewAssessmentRuleId = 0;
                    assessmentAndItems.Add(andItem);
                }
                SessionManager.lstAssessmentAndItemRollOver = assessmentAndItems;
                return assessmentAndItems;
            }
        }

        [NonAction]
        public IList<ServiceBillAndItemRollOver> GetServiceBillAndItem()
        {
            using (_db2 = new EIRSEntities())
            {
                List<ServiceBillAndItemRollOver> assessmentAndItems = new List<ServiceBillAndItemRollOver>();
                var presentYear = DateTime.Now.Year;
                var newYear = presentYear;

                var retVal = (from r in _db2.MDA_Services
                              join a in _db2.MAP_MDAService_MDAServiceItem
                              on r.MDAServiceID equals a.MDAServiceID into serviceItemsGroup
                              from a in serviceItemsGroup.DefaultIfEmpty() // Left join with MAP_MDAService_MDAServiceItem
                              join b in _db2.MDA_Service_Items
                              on a.MDAServiceItemID equals b.MDAServiceItemID into serviceItemsDetailsGroup
                              from b in serviceItemsDetailsGroup.DefaultIfEmpty() // Left join with MDA_Service_Items
                              where r.TaxYear == presentYear
                              select new
                              {
                                  active = r.Active,
                                  taxYear = newYear,
                                  percentage = b != null ? b.Percentage : (decimal?)null,
                                  serviceBaseAmount = b != null ? b.ServiceBaseAmount : (decimal?)null,
                                  mDAServiceCode = r.MDAServiceCode,
                                  mDAServiceName = r.MDAServiceName,
                                  serviceAmount = r.ServiceAmount,
                              }).ToList();


                foreach (var item in retVal)
                {
                    ServiceBillAndItemRollOver andItem = new ServiceBillAndItemRollOver();
                    andItem.ServiceAmount = item.serviceAmount.Value;
                    andItem.TaxYear = item.taxYear.ToString();
                    andItem.Active = item.active.Value;
                    andItem.Percentage = item.percentage.HasValue ? item.percentage.Value : 0;
                    andItem.ServiceBaseAmount = item.serviceBaseAmount.HasValue ? item.serviceBaseAmount.Value : 0;
                    andItem.MDAServiceName = item.mDAServiceName;
                    andItem.MDAServiceCode = item.mDAServiceCode;
                    //andItem.NewAssessmentRuleId = 0;
                    assessmentAndItems.Add(andItem);
                }
                return assessmentAndItems;
            }
        }

        [NonAction]
        public List<AssessmentRuleRollover> GetAssessmentAndRule()
        {
            using (_db2 = new EIRSEntities())
            {
                List<AssessmentRuleRollover> assessmentAndItems = new List<AssessmentRuleRollover>();
                var presentYear = DateTime.Now.Year;
                presentYear = presentYear + 1;
                var retVal = (from r in _db2.Assessment_Rules
                              join a in _db2.MAP_AssessmentRule_AssessmentItem
                              on r.AssessmentRuleID equals a.AssessmentRuleID
                              where r.TaxYear == 2024
                              select new
                              {
                                  assessmentRuleID = r.AssessmentRuleID,
                                  assessmentRuleCode = r.AssessmentRuleCode,
                                  profileId = r.ProfileID,
                                  assessmentRuleName = r.AssessmentRuleName,
                                  assessmentAmount = r.AssessmentAmount,
                                  taxYear = r.TaxYear,
                                  ruleRunID = r.RuleRunID,
                                  paymentFrequencyId = r.PaymentFrequencyID,
                                  active = r.Active,
                                  araiid = a.ARAIID,
                                  assessmentitemID = a.AssessmentItemID
                              }).ToList();

                foreach (var item in retVal)
                {
                    AssessmentRuleRollover andItem = new AssessmentRuleRollover();
                    andItem.AssessmentRuleID = item.assessmentRuleID;
                    andItem.AssessmentRuleCode = item.assessmentRuleCode;
                    andItem.AssessmentRuleName = item.assessmentRuleName;
                    andItem.AssessmentAmount = item.assessmentAmount.Value;
                    andItem.NewAssessmentRuleID = 0;
                    andItem.ARAIID = item.araiid;
                    andItem.AssessmentItemID = item.assessmentitemID.Value;
                    andItem.Paymentfrequencyid = item.paymentFrequencyId.Value;
                    andItem.Taxyear = presentYear;
                    andItem.Profileid = item.profileId.Value;
                    andItem.RuleRunId = item.ruleRunID.Value;
                    andItem.Active = item.active.ToString();
                    andItem.Createddate = DateTime.Now;
                    andItem.Createdby = -1;
                    assessmentAndItems.Add(andItem);
                }

                return assessmentAndItems;
            }
        }

        [NonAction]
        public IList<MdaserviceRollover> GetMDAServiceandMDAServiceItem()
        {
            using (_db2 = new EIRSEntities())
            {
                List<MdaserviceRollover> MADServiceAndItems = new List<MdaserviceRollover>();
                var presentYear = DateTime.Now.Year;
                var newYear = presentYear + 1;
                //var retVal = (from r in _db2.MDA_Services
                //              join a in _db2.MAP_MDAService_MDAServiceItem
                //              on r.MDAServiceID equals a.MDAServiceID
                //              where r.TaxYear == presentYear
                //              select new
                //              {
                //                  active = r.Active,
                //                  taxYear = newYear,
                //                  mdaserviceid = r.TaxMonth,
                //                  percentage = b.Percentage,
                //                  taxBaseAmount = b.TaxBaseAmount,
                //                  taxAmount = b.TaxAmount,
                //                  assessmentItemName = b.AssessmentItemName,
                //                  assessmentRuleCode = r.AssessmentRuleCode,
                //                  assessmentRuleName = r.AssessmentRuleName,
                //                  assessmentAmount = r.AssessmentAmount,
                //                  assessmentRuleId = r.AssessmentRuleID
                //              }).ToList();

                var retVal = from m in _db2.MDA_Services
                             join mi in _db2.MAP_MDAService_MDAServiceItem
                                on m.MDAServiceID equals mi.MDAServiceID into miGroup
                            from mi in miGroup.DefaultIfEmpty() 
                            where m.TaxYear == 2024
                            select new
                            {
                                m.MDAServiceID,
                                m.MDAServiceCode,
                                m.MDAServiceName,
                                m.RuleRunID,
                                m.PaymentFrequencyID,
                                m.ServiceAmount,
                                m.TaxYear,
                                m.PaymentOptionID,
                                m.Active,
                                MdaServiceItemId = mi.MDAServiceItemID
                            };


                foreach (var item in retVal)
                {
                    MdaserviceRollover andItem = new MdaserviceRollover();
                    andItem.MdaserviceId = item.MDAServiceID;
                    andItem.TaxYear = newYear.ToString();
                    andItem.PaymentFrequencyId = item.PaymentFrequencyID;
                    andItem.Active = item.Active.Value;
                    andItem.RuleRunId = item.RuleRunID.Value;
                    andItem.ServiceAmount = item.ServiceAmount.HasValue ? item.ServiceAmount.Value : 0;
                    andItem.MdaserviceName = item.MDAServiceName;
                    andItem.MdaserviceCode = item.MDAServiceCode;
                    andItem.MdaserviceItemId = item.MdaServiceItemId;
                    andItem.Msmiid = item.MdaServiceItemId;
                    andItem.NewMdaserviceId = 0;
                    MADServiceAndItems.Add(andItem);
                }
                SessionManager.lstMdaserviceRollover = MADServiceAndItems;
                return MADServiceAndItems;
            }
        }
    }
}