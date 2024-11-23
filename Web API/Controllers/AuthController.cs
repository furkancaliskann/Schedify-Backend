using Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Web_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var clientId = _configuration["OAuth:ClientId"];
            var redirectUri = _configuration["Jwt:RedirectUri"];
            var scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
            var state = Guid.NewGuid().ToString();

            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/v2/auth?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&scope={scope}&state={state}";

            return Redirect(googleAuthUrl);
        }

        /*[HttpGet("google/callback")]
        public async Task<IActionResult> GoogleCallback(string code, string state)
        {
            var clientId = _configuration["OAuth:ClientId"];
            var clientSecret = _configuration["OAuth:ClientSecret"];
            var redirectUri = _configuration["Jwt:RedirectUri"];

            using var client = new HttpClient();
            var tokenRequest = new Dictionary<string, string>
    {
        { "code", code },
        { "client_id", clientId },
        { "client_secret", clientSecret },
        { "redirect_uri", redirectUri },
        { "grant_type", "authorization_code" }
    };

            var response = await client.PostAsync("https://oauth2.googleapis.com/token", new FormUrlEncodedContent(tokenRequest));
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorDetails = await response.Content.ReadAsStringAsync();
                return BadRequest($"Google authentication failed: {errorDetails}");
            }

            var tokenResponse = JsonSerializer.Deserialize<GoogleTokenResponse>(json);
            var idToken = tokenResponse.IdToken;

            var jwt = await _authService.AuthenticateGoogleUser(idToken);

            return Ok(new { token = jwt });
        }*/

        [HttpPost("google/callback")]
        public async Task<IActionResult> GoogleCallback([FromBody] GoogleTokenResponse request)
        {
            try
            {
                var jwt = await _authService.AuthenticateGoogleUser(request.IdToken); // Sadece idToken kullanılır
                return Ok(new { token = jwt });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        public class GoogleTokenResponse
        {
            [JsonPropertyName("id_token")]
            public string IdToken { get; set; }
        }
    }
}
