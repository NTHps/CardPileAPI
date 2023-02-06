//using CardPile.Application.Services.Security.Authentication;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Text;

//namespace CardPile.Application.Infrastructure.Security.Authentication
//{

//    public class JWTFactory : IJWTFactory
//    {

//        #region - - - - - - Fields - - - - - -

//        private readonly IConfiguration m_Configuration;

//        #endregion Fields

//        #region - - - - - - JWTFactory Implementation - - - - - -

//        public string CreateToken()
//        {
//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.m_Configuration["Jwt:Key"]));
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                this.m_Configuration["Jwt:Issuer"],
//                this.m_Configuration["Jwt:Issuer"],
//                null,
//                expires: DateTime.Now.AddMinutes(120),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }

//        #endregion JWTFactory Implementation

//    }

//}
