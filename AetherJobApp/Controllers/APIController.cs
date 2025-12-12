using AetherJobApp.Models;
using BusinessLogicLayer.Repository;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AetherJobApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyJobRequirementRepository _jobRequirementRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IVacancyRepository _vacancyRepository;
        public APIController(IUserRepository userRepository, ICompanyJobRequirementRepository jobRequirementRepository, ICandidateRepository candidateRepository, IVacancyRepository vacancyRepository)
        {
            _userRepository = userRepository;
            _jobRequirementRepository = jobRequirementRepository;
            _candidateRepository = candidateRepository;
            _vacancyRepository = vacancyRepository;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            try
            {
                var data = _userRepository.login(requestModel.UserName, requestModel.Password);
                if (data == null)
                {
                    return BadRequest(new { Message = "Invalid username or password" });
                }
                if (!data.IsActive)
                {
                    return BadRequest(new { Message = "This user is deactivated" });
                }
                return Ok(new { Login = data.FullName, Roles = data.Role, UserId = data.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        [HttpPost("registration")]
        public IActionResult RegistrationUser([FromBody] UserMasterRequestModel userMasterModel)
        {
            try
            {
                UserMasterModel objModel = new UserMasterModel();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Please check validation");
                }
                objModel.FullName = userMasterModel.FullName;
                objModel.Username = userMasterModel.Username;
                objModel.Password = userMasterModel.Password;
                objModel.Email = userMasterModel.Email;
                objModel.MobileNo = userMasterModel.MobileNo;
                objModel.RefId_CountryMaster = userMasterModel.RefId_CountryMaster;
                objModel.RefId_StateMaster = userMasterModel.RefId_StateMaster;
                objModel.RefId_CityMaster = userMasterModel.RefId_CityMaster;

                _userRepository.saveRegistration(objModel);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        [HttpPost("updateActive")]
        public IActionResult UpdateActive([FromBody] UpdateActiveRequestModel model)
        {
            try
            {
                _userRepository.updateIsActiveStatus(model.Id, model.IsActive);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }

        [HttpGet("getUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var data = await _userRepository.getUserAll();
            return Ok(data);
        }
        [HttpGet("getCountryList")]
        public async Task<IActionResult> GetCountryList()
        {
            var data = await _userRepository.getCountryList();
            return Ok(data);
        }
        [HttpGet("getStateListByCountryId/{id}")]
        public async Task<IActionResult> GetStateListByCountryId(int id)
        {
            var data = await _userRepository.getStatelistByCountryId(id);
            return Ok(data);
        }
        [HttpGet("getCityListByStateId/{id}")]
        public async Task<IActionResult> GetCityListByStateId(int id)
        {
            var data = await _userRepository.getCityListByCityId(id);
            return Ok(data);
        }
        #region Vacancy Master
        [HttpGet("getVacancyList")]
        public async Task<IActionResult> GetVacancyList()
        {
            try
            {
                var data = await _vacancyRepository.getList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching vacancy list.");
            }
        }
        [HttpGet("getActiveVacancyList")]
        public async Task<IActionResult> getActiveList()
        {
            try
            {
                var data = await _vacancyRepository.getActiveList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching vacancy list.");
            }
        }
        [HttpGet("getVacancyInfoById/{id}")]
        public IActionResult GetVacancyInfoById(int id)
        {
            var data = _vacancyRepository.getInfoById(id);
            return Ok(data);
        }
        [HttpPost("saveVacancy")]
        public IActionResult SaveVacancy([FromBody] VacancyMasterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Please check validation");
                }
                _vacancyRepository.saveVacancy(model);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        [HttpPost("updateVacancyActive")]
        public IActionResult UpdateVacancyActive([FromBody] UpdateActiveRequestModel model)
        {
            try
            {
                _vacancyRepository.updateIsActiveStatus(model.Id, model.IsActive);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        [HttpDelete("deleteVacancy/{id}")]
        public IActionResult DeleteVacancy(int id)
        {
            _vacancyRepository.deleteById(id);
            return Ok(new { success = true });
        }
       
        #endregion

        #region Job Requirement
        [HttpGet("getJobRequirementList")]
        public async Task<IActionResult> GetJobRequirementList()
        {
            var data = await _jobRequirementRepository.getJobRequirementList();
            return Ok(data);
        }
        [HttpGet("getJobRequirementActiveList")]
        public async Task<IActionResult> getFindActiveList()
        {
            var data = await _jobRequirementRepository.getFindActiveList();
            return Ok(data);
        }
        [HttpGet("getJobRequirementInfoById/{id}")]
        public IActionResult GetJobRequirementInfoById(int id)
        {
            var data = _jobRequirementRepository.getInfoById(id);
            return Ok(data);
        }
        [HttpPost("saveJobRequirement")]
        public IActionResult SaveJobRequirement([FromBody] CompanyJobRequirementModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Please check validation");
                }
                _jobRequirementRepository.saveJobRequirement(model);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        [HttpPost("updateJobRequirementActive")]
        public IActionResult UpdateJobRequirementActive([FromBody] UpdateActiveRequestModel model)
        {
            try
            {
                _jobRequirementRepository.updateIsActiveStatus(model.Id, model.IsActive);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        [HttpDelete("deleteJobRequirement/{id}")]
        public IActionResult DeleteJobRequirement(int id)
        {
            _jobRequirementRepository.deleteById(id);
            return Ok(new { success = true });
        }
        #endregion

        #region Candidate Detail
        [HttpGet("getCandidateList")]
        public async Task<IActionResult> GetCandidateList()
        {
            try
            {
                var data = await _candidateRepository.GetAllCandidates();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
            
        }
        [HttpGet("getCandidateInfoById/{id}")]
        public IActionResult GetCandidateInfoById(int id)
        {
            var data = _candidateRepository.getInfoById(id);
            return Ok(data);
        }

        #endregion

        #region Career
        [HttpPost("saveApplyJob")]
        public IActionResult SaveApplyJob([FromForm] CareerRequestModel model, IFormFile resume)
        {
            try
            {
                var data = _candidateRepository.checkExistJobApply(model.RefId_UserMaster,model.RefId_CompanyRequirement);
                if (data != null)
                {
                    // Return existing application details
                    return Ok(new
                    {
                        success = false,
                        message = "Candidate has already applied for this job.",
                        data = data
                    });
                }

                CandidateDetailModel objCandidateDetailModel = new CandidateDetailModel();
                if (resume != null && resume.Length > 0)
                {
                    var ext = Path.GetExtension(resume.FileName).ToLower();

                    if (ext != ".pdf")
                        return BadRequest(new { success = false, message = "Only PDF files are allowed." });

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid() + ext;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        resume.CopyToAsync(stream);
                    }
                    objCandidateDetailModel.RefId_UserMaster = model.RefId_UserMaster;
                    objCandidateDetailModel.RefId_CompanyRequirement = model.RefId_CompanyRequirement;
                    objCandidateDetailModel.PrimarySkill = model.PrimarySkill;
                    objCandidateDetailModel.Status = model.Status;
                    objCandidateDetailModel.ExperienceYear = model.ExperienceYear;
                    objCandidateDetailModel.ExperienceMonth = model.ExperienceMonth;
                    objCandidateDetailModel.ResumePath = uniqueFileName;
                    objCandidateDetailModel.ReferBy = model.ReferBy;
                    //objCandidateDetailModel.ReviewDate = null;
                    _candidateRepository.saveCandidate(objCandidateDetailModel);
                }

                return Ok(new { success = true, message = "Job applied successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
        [HttpGet("download{fileName}")]
        public IActionResult DownloadResume(string fileName)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/pdf", fileName);
        }
        [HttpPost("updateStatusCandidate")]
        public IActionResult UpdateStatusCandidate([FromBody] CandidateStatusRequestModel model)
        {
            try
            {
                _candidateRepository.updateStatusCandidate(model.Id, model.Status,model.ReviewBy);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        #endregion

    }
}
