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
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICompanyRequirementRepository _companyRequirementRepository;
        public APIController(IUserRepository userRepository, IVacancyRepository vacancyRepository, ICandidateRepository candidateRepository, ICompanyRequirementRepository companyRequirementRepository)
        {
            _userRepository = userRepository;
            _vacancyRepository = vacancyRepository;
            _candidateRepository = candidateRepository;
            _companyRequirementRepository = companyRequirementRepository;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestModel requestModel)
        {
            try
            {
                var data = _userRepository.login(requestModel.UserName, requestModel.Password);
                return Ok(new { Login = data.FullName, Roles = data.Role });
            }
            catch (Exception)
            {
                throw;
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

        [HttpGet("getUserList")]
        public IActionResult GetUserList()
        {
            var data = _userRepository.getAll();
            return Ok(data);
        }

        #region Vacancy Master
        [HttpGet("getVacancyList")]
        public IActionResult GetVacancyList()
        {
            var data = _vacancyRepository.getList();
            return Ok(data);
        }
        [HttpGet("getVacancyInfoById/{id}")]
        public IActionResult GetVacancyInfoById(int id)
        {
            var data = _vacancyRepository.getInfoById(id);
            return Ok(data);
        }
        [HttpPost("saveVacancy")]
        public IActionResult SaveVacancy([FromBody] VacancyMasterModel userMasterModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Please check validation");
                }
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return Ok(StatusCode(500, ex.Message));
            }
        }
        [HttpDelete("deleteCandidate/{id}")]
        public IActionResult DeleteCandidate(int id)
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
