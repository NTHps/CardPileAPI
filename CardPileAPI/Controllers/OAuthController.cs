using AutoMapper;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Application.Services.Security.Authentication.OAuth;
using CardPileAPI.Infrastructure.Security.Authentication;
using CardPileAPI.Presentation.Commands.OAuth;
using CardPileAPI.Services.Security.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Text;

namespace CardPileAPI.Controllers
{

    [ApiController]
    public class OAuthController : BaseController
    {

        #region - - - - - - Fields - - - - - -

        private readonly ICustomAuthenticationManager m_AuthenticationManager;
        private readonly IConfiguration m_Configuration;
        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public OAuthController(ICustomAuthenticationManager authenticationManager, IConfiguration configuration, IMapper mapper)
        {
            this.m_AuthenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(authenticationManager));
            this.m_Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region - - - - - - OAuth - - - - - -

        /// <summary>
        /// Get the access token.
        /// </summary>
        /// <param name="jsonData">An OAuth2.0 request</param>
        /// <returns>An OAuth2.0 response</returns>
        /// <remarks>Implemented Grants: Password, Client Credentials, Refresh.</remarks>
        /// <exception cref="OAuthException"></exception>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(OAuthError), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetOAuthToken([FromBody] dynamic jsonData)
        {
            JObject _JsonRequestObject = JsonConvert.DeserializeObject(jsonData.ToString());

            OAuthCommand _OAuthRequest = JsonConvert.DeserializeObject<OAuthCommand>(jsonData.ToString())
                ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_request);

            if (string.IsNullOrEmpty(_OAuthRequest.GrantType))
                throw new OAuthException(OAuthErrorValuesEnum.invalid_request, "Invalid request.");

            switch (_OAuthRequest.GrantType)
            {
                case GrantTypes.Password: // Login using username and password
                    return await this.CreatePasswordGrantOAuthToken(_JsonRequestObject, _OAuthRequest);
                case GrantTypes.RefreshToken: // Login using refresh token
                    return await this.CreateRefreshTokenGrantOAuthToken(_JsonRequestObject, _OAuthRequest);
                case GrantTypes.ClientCredentials: // Client credentials dw
                    return await this.CreateClientCredentialsOAuthToken(_JsonRequestObject, _OAuthRequest);
                default:
                    throw new OAuthException(OAuthErrorValuesEnum.unsupported_grant_type);
            }
        }

        #endregion OAuth

        #region - - - - - - Private Methods - - - - - -

        private void ValidateRequest(JObject jsonRequest, Type requestType)
        {
            var _RequestTypeProperties = requestType.GetProperties()
                .SelectMany(p => p.GetCustomAttributes<JsonPropertyAttribute>()
                    .Select(ca => ca.PropertyName))
                .ToList();

            var _JsonRequestProperties = jsonRequest.Properties().ToList().Select(j => j.Name.ToString()).ToList();
            var _PropertyComparison = _RequestTypeProperties.Except(_JsonRequestProperties, StringComparer.CurrentCultureIgnoreCase).ToList();

            if (_PropertyComparison.Any() || _JsonRequestProperties.Count != _RequestTypeProperties.Count())
                throw new OAuthException(OAuthErrorValuesEnum.invalid_request, "Invalid request.");
        }

        private string GenerateJSONWebToken(OAuthCommand command)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.m_Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                this.m_Configuration["Jwt:Issuer"],
                this.m_Configuration["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IActionResult> CreateClientCredentialsOAuthToken(JObject jsonRequest, OAuthCommand request)
        {
            //this.ValidateRequest(jsonRequest, typeof(CreateClientCredentialsOAuthTokenRequest));

            //var _CreateClientCredentialsRequest = this.Mapper.Map<CreateClientCredentialsOAuthTokenRequest>(request);
            //var _CreateClientCredentialsResponse = await this.Mediator.Send(_CreateClientCredentialsRequest);
            //return this.m_Mapper.Map<OAuthViewModel>(_CreateClientCredentialsResponse);

            return default;
        }

        private async Task<IActionResult> CreatePasswordGrantOAuthToken(JObject jsonRequest, OAuthCommand request)
        {
            //this.ValidateRequest(jsonRequest, typeof(CreatePasswordGrantOAuthTokenRequest));
            //var _CreatePasswordGrantRequest = this.Mapper.Map<CreatePasswordGrantOAuthTokenRequest>(request);
            //var _CreatePasswordGrantResponse = await this.Mediator.Send(_CreatePasswordGrantRequest);
            //return this.m_Mapper.Map<OAuthViewModel>(_CreatePasswordGrantResponse);

            //var _T = this.GenerateJSONWebToken(request);

            var token = await this.m_AuthenticationManager.AuthenticateAsync(request.Username, request.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        private async Task<IActionResult> CreateRefreshTokenGrantOAuthToken(JObject jsonRequest, OAuthCommand request)
        {
            //this.ValidateRequest(jsonRequest, typeof(CreateRefreshTokenGrantOAuthTokenRequest));
            //var _CreateRefreshTokenGrantRequest = this.Mapper.Map<CreateRefreshTokenGrantOAuthTokenRequest>(request);
            //var _CreateRefreshTokenGrantResponse = await this.Mediator.Send(_CreateRefreshTokenGrantRequest);
            //return this.m_Mapper.Map<OAuthViewModel>(_CreateRefreshTokenGrantResponse);

            return default;
        }

        #endregion Private Methods

    }

}
