namespace UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst("sub")?.Value;
            var user = _userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.UserName,
                user.Email
            });
        }
    }
}
