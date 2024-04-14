using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecoverPasswordController : ControllerBase
    {
        private readonly UserManager _userManager;

        public RecoverPasswordController(UserManager userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        [Route("CreateData")]
        public ActionResult CreateOTP(ValidateOTP validateOTP)
        {
            try
            {
                var vm = new UserManager();
                vm.CreateOTP(validateOTP);
                return Ok(validateOTP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet]
        [Route("VerifyOtp")]
        public IActionResult VerifyOtp()
        {
            try
            {
                var vm = new UserManager();
                var otpList = vm.RetrieveAllOTP();
                return Ok(otpList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
