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
        public IActionResult GetUserList()
        {
            var data = _userRepository.getAll();
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
            var data = await _jobRequirementRepository.getList();
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
            _vacancyRepository.deleteById(id);
            return Ok(new { success = true });
        }
        #endregion

        #region Candidate Detail
        [HttpGet("getCandidateList")]
        public IActionResult GetCandidateList()
        {
            var data = _candidateRepository.getList();
            return Ok(data);
        }
        [HttpGet("getCandidateInfoById/{id}")]
        public IActionResult GetCandidateInfoById(int id)
        {
            var data = _candidateRepository.getInfoById(id);
            return Ok(data);
        }

        #endregion
    }
}
