using CoreApp;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ValidateOTPController : ControllerBase
    {
        [HttpGet]
        [Route("ValidateOTP")]
        public IActionResult ValidateOTP(string email, string otp)
        {
            try
            {
                var vc = new ValidateOTPManager();
                var validationResult = vc.ValidateOTP(email, otp);

                if (validationResult != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
