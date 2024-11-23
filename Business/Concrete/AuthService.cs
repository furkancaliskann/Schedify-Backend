using DataAccess.Abstract;
using Entities.Concrete;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Concrete
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateGoogleUser(string idToken)
        {
            var payload = await VerifyGoogleToken(idToken);
            if (payload == null)
                throw new UnauthorizedAccessException("Invalid Google token");

            var user = await _userRepository.GetByEmailAsync(payload.Email) ?? new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = payload.Email,
                Name = payload.Name,
                ProfilePicture = payload.Picture
            };

            await _userRepository.AddUserAsync(user);

            return GenerateJwtToken(user);
        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = [_configuration["OAuth:ClientId"]]
            };

            return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["OAuth:ClientSecret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:
                [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
                ],
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
