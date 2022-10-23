using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using TicketManagement.DataAccess.Entities;
using TicketManagement.UserApi.BusinessLogic.Settings;

namespace TicketManagement.UserApi.BusinessLogic.Services
{
    public class JwtTokenManager
    {
        private readonly JwtTokenSettings _jwtTokenSettings;

        public JwtTokenManager(IOptions<JwtTokenSettings> options)
        {
            _jwtTokenSettings = options.Value;
        }

        public string GetToken(User user, IList<string> roles)
        {
            var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            };

            userClaims.AddRange(roleClaims);

            var jwt = new JwtSecurityToken(
                issuer: _jwtTokenSettings.JwtIssuer,
                audience: _jwtTokenSettings.JwtAudience,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtSecretKey)),
                    SecurityAlgorithms.HmacSha256),
                claims: userClaims);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public bool IsValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenSettings.JwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = _jwtTokenSettings.JwtAudience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtSecretKey)),
                        ValidateLifetime = false,
                    },
                    out var _);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public IEnumerable<string> GetUserRolesByJwt(string jwt)
        {
            IdentityModelEventSource.ShowPII = true;

            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = false;
            validationParameters.ValidAudience = _jwtTokenSettings.JwtAudience;
            validationParameters.ValidIssuer = _jwtTokenSettings.JwtIssuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtSecretKey));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);

            var roles = principal.Identities.First().Claims.Where(claim => claim.Type == ClaimTypes.Role).Select(claim => claim.Value);

            return roles;
        }

        public string GetUsernameByJwt(string jwt)
        {
            IdentityModelEventSource.ShowPII = true;

            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = false;
            validationParameters.ValidAudience = _jwtTokenSettings.JwtAudience;
            validationParameters.ValidIssuer = _jwtTokenSettings.JwtIssuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtSecretKey));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);

            var username = principal.Identities.First().Claims.Where(claim => claim.Type == ClaimTypes.NameIdentifier).Select(claim => claim.Value).First();

            return username;
        }

        public ClaimsIdentity GetClaimsIdentityByJwt(string jwt)
        {
            IdentityModelEventSource.ShowPII = true;

            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = false;
            validationParameters.ValidAudience = _jwtTokenSettings.JwtAudience;
            validationParameters.ValidIssuer = _jwtTokenSettings.JwtIssuer;
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.JwtSecretKey));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);

            var claimsIdentity = new ClaimsIdentity(principal.Identities.First().Claims);

            return claimsIdentity;
        }
    }
}