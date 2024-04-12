using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidateOTPController : ControllerBase
    {
        private readonly ValidateOTPManager _validateOTPManager;

        public ValidateOTPController(ValidateOTPManager validateOTPManager)
        {
            _validateOTPManager = validateOTPManager;
        }


        [HttpPost]
        [Route("CreateData")]
        public ActionResult CreateOTP(ValidateOTP validateOTP)
        {
            try
            {
                var vm = new ValidateOTPManager();
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
                var vm = new ValidateOTPManager();
                var otpList = vm.RetrieveAllOTP();
                return Ok(otpList);
            } catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}