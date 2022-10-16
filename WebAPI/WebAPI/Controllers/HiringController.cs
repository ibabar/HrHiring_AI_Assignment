using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using WebAPI.Cache;
using WebAPI.DTO;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class HiringController : ControllerBase
    {
        private readonly HRDBContext _context;
        public HiringController(HRDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetVacancy/{id}")]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetAllAddress(int id)
        {

            List<Vacancy> lst = new List<Vacancy>();
            try
            {
                if (id == 0)
                    lst = _context.Vacancies.ToList();
                else
                    lst = _context.Vacancies.Where(t => t.Id == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }


        [HttpGet]
        [Route("GetApplicant/{id}")]
        public async Task<ActionResult<IEnumerable<ApplicantInfo>>> GetApplicant(int id)
        {
            List<ApplicantInfo> lst = new List<ApplicantInfo>();
            try
            {
                if (id == 0)
                {
                    var candidates = _context.Candidates.Include(x => x.Vacancies).ToList();


                    foreach (var item in candidates)
                    {
                        var vacancyInfo = _context.Vacancies.Where(t => t.Id == item.VacancyId).FirstOrDefault();
                        lst.Add(new ApplicantInfo()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Email = item.Email,
                            MobileNumber = item.MobileNumber,
                            VacancyId = item.VacancyId,
                            VacancyName = vacancyInfo.OpenPosition,
                            ApprovalOne = item.ApprovalOne,
                            ApprovalTwo = item.ApprovalTwo
                        });
                    }
                }

                else
                {
                    var candidates = _context.Candidates.FirstOrDefault(t => t.Id == id);

                    var vacancyInfo = _context.Vacancies.Where(t => t.Id == candidates.VacancyId).FirstOrDefault();

                    lst = _context.Candidates.Where(t => t.Id == id).Select(t => new ApplicantInfo()
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Email = t.Email,
                        MobileNumber = t.MobileNumber,
                        VacancyId = t.VacancyId,
                        VacancyName = vacancyInfo.OpenPosition,
                        ApprovalOne = t.ApprovalOne,
                        ApprovalTwo = t.ApprovalTwo

                    }).ToList();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

        [HttpPost]
        [Route("CreateVacancy")]
        public async Task<ActionResult<ResponseModel>> CreateVacancy(Position position)
        {

            ResponseModel model = new ResponseModel();
            try
            {
                Vacancy temp = null;
                if (position.Id > 0)
                    temp = _context.Vacancies.FirstOrDefault(t => t.Id == position.Id);

                if (temp != null)
                {
                    temp.OpenPosition = position.OpenPosition;
                    temp.ApprovedCandidate = position.ApprovedCandidate;

                    _context.Update<Vacancy>(temp);
                    model.Messsage = "Position Updated Successfully";
                }
                else
                {
                    temp = new Vacancy();
                    temp.OpenPosition = position.OpenPosition;
                    temp.CreatedOn = DateTime.Now;
                    _context.Add<Vacancy>(temp);
                    model.Messsage = "Vacancy Created Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }


        [HttpPost]
        [Route("AddCandidate")]
        public async Task<ActionResult<ResponseModel>> AddCandidate(Applicant position)
        {

            ResponseModel model = new ResponseModel();
            try
            {
                Candidate temp = null;
                if (position.Id > 0)
                    temp = _context.Candidates.FirstOrDefault(t => t.Id == position.Id);

                if (temp != null)
                {
                    temp.Name = position.Name;
                    temp.Email = position.Email;
                    temp.MobileNumber = position.MobileNumber;
                    temp.VacancyId = position.VacancyId;

                    _context.Update<Candidate>(temp);
                    model.Messsage = "candidate Updated Successfully";
                }
                else
                {
                    temp = new Candidate();
                    temp.Name = position.Name;
                    temp.Email = position.Email;
                    temp.MobileNumber = position.MobileNumber;
                    temp.VacancyId = position.VacancyId;
                    temp.Resume = position.ImgPath;
                    temp.ApplicationStatus = (int)ApplicationStatus.Pending;

                    _context.Add<Candidate>(temp);
                    model.Messsage = "candidate Created Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }


        [HttpPost]
        [Route("GiveApproval")]
        public async Task<ActionResult<ResponseModel>> GiveApproval(Approval aproval)
        {
            ResponseModel model = new ResponseModel();

            try
            {
                string role = Convert.ToString(GetRequestHeader()["Role"]);
                Int64 UserID = Convert.ToInt64(GetRequestHeader()["UserID"]);
                string msg = "Approval Granded.";
                var candidates = _context.Candidates.FirstOrDefault(t => t.Id == aproval.CandidateID);
                var vacancy = _context.Vacancies.FirstOrDefault(t => t.Id == aproval.VacancyId);

                if (role.ToLower().Trim() == "manager")
                    candidates.ApprovalOne = UserID;
                else if (role.ToLower().Trim() == "director")
                    candidates.ApprovalTwo = UserID;
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "You are not Authorize . ";
                    return model;
                }

                if (Convert.ToInt64(candidates.ApprovalOne) > 0 && Convert.ToInt64(candidates.ApprovalTwo) > 0)
                {
                    var allCandidates = _context.Candidates.Where(t => t.VacancyId == aproval.VacancyId);
                    foreach (var cand in allCandidates)
                    {
                        if (cand.Id == aproval.CandidateID)
                            cand.ApplicationStatus = (int)ApplicationStatus.Selected;
                        else
                            cand.ApplicationStatus = (int)ApplicationStatus.Rejected;
                    }
                    msg = "Congratulations !!! . All Approvals are done and Candicate has been selected.";
                }
                model.Messsage = msg;

                _context.SaveChanges();
                model.IsSuccess = true;

            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }


        private JObject GetRequestHeader()
        {

            var AccessToken = Convert.ToString(this.HttpContext.Request.Headers["Authorization"]);
            dynamic jsonObject = new JObject();
            if (!string.IsNullOrEmpty(AccessToken))
            {
                AccessToken = AccessToken != null ? AccessToken.Split(' ')[1] : "";
                var stream = AccessToken;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;

                jsonObject.AccessToken = AccessToken;
                jsonObject.UserID = tokenS.Claims.First(claim => claim.Type == "userID").Value;
                jsonObject.Role = tokenS.Claims.First(claim => claim.Type == "Role").Value;
                return jsonObject;
            }
            return jsonObject;
        }


        [HttpGet]
        [Route("GetOpenVacancy/{id}")]
        public async Task<ActionResult<IEnumerable<Vacancy>>> GetOpenVacancy(int id)
        {

            List<Vacancy> lst = new List<Vacancy>();
            try
            {
                if (id == 0)
                    lst = _context.Vacancies.Where(t => !t.ApprovedCandidate.HasValue).ToList();
                else
                    lst = _context.Vacancies.Where(t => t.Id == id).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return lst;
        }

    }

    public enum ApplicationStatus : int
    {
        Pending = 0,
        Rejected = 1,
        Selected = 9
    }


}
