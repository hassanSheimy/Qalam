using Qalam.BLL.ViewModels;
using Qalam.Common.Helper;
using Qalam.MYSQL.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;
using Qalam.Common.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Qalam.Common.Extensions;

namespace Qalam.BLL.Managers
{
    public class TokenManager
    {
        private readonly IMapper _mapper;
        private readonly string QALAM_SECRET_TOKEN_KEY;
        public TokenManager(IConfiguration configuration, IMapper mapper)
        {
            QALAM_SECRET_TOKEN_KEY = configuration["Jwt:JWTSecret"];
            _mapper = mapper;
        }

        private RequestAttributes ExtractAttributes(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var userId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                var teacherId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "TeacherId")?.Value;
                var studentId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "StudentId")?.Value;
                var userEmail = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserEmail")?.Value;
                var countryId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "CountryId")?.Value;
                var userRole = (EUserTypes)int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value);

                return new RequestAttributes
                {
                    UserId = HelperFunctions.IntOrDefault(userId, -1),
                    TeacherId = HelperFunctions.IntOrDefault(teacherId, -1),
                    StudentId = HelperFunctions.IntOrDefault(studentId, -1),
                    UserEmail = userEmail,
                    UserRole = userRole,
                    CountryId = HelperFunctions.IntOrDefault(countryId, -1)
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public RequestAttributes ValidateToken(string token, params EUserTypes[] userTypes)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(QALAM_SECRET_TOKEN_KEY));
                var TokenValidationParameters = new TokenValidationParameters
                {
                    //what to validate
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    //setup validate data
                    ValidIssuer = "Qalam",
                    ValidAudience = "Qalam-Users",
                    IssuerSigningKey = symmetricSecurityKey
                };

                var data = tokenHandler.ValidateToken(token, TokenValidationParameters, out SecurityToken validatedToken);

                var attribute = ExtractAttributes(data);

                if (!userTypes.Contains(attribute.UserRole) && !userTypes.Contains(EUserTypes.Any))
                {
                    throw new Exception();
                }

                return attribute;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UserViewModel GenerateUserToken(User user, DateTime expires)
        {
            try
            {
                //symmetric security key
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(QALAM_SECRET_TOKEN_KEY));

                // signing credentials
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

                // add claims
                var claims = new List<Claim>
                {
                    // add user role on the claims
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),

                    // add email
                    new Claim("UserEmail", user.Email),

                    // add user id
                    new Claim("UserId", user.Id.ToString()),

                    // add country id
                    new Claim("CountryId", user.CountryId.ToString())
                };
                if (user.Teacher != null)
                {
                    claims.Add(new Claim("TeacherId", user.Teacher.Id.ToString()));
                }
                if (user.Student != null)
                {
                    claims.Add(new Claim("StudentId", user.Student.Id.ToString()));
                }

                // Token security information
                var token = new JwtSecurityToken(
                    issuer: "Qalam",
                    audience: "Qalam-Users",
                    expires: expires,
                    signingCredentials: signingCredentials,
                    claims: claims
                );

                // result
                var result = _mapper.Map<UserViewModel>(user);
                result.Token = new JwtSecurityTokenHandler().WriteToken(token);
                return result;
            }
            // any unexcpected error happened
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
