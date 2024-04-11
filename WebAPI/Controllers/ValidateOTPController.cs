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
        public IActionResult VerifyOtp(string email, string otpInput)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(otpInput))
            {
                return BadRequest("Email or OTP input is missing.");
            }

            try
            {
                bool isValid = _validateOTPManager.ValidateOtp(email, otpInput);
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}