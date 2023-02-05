using AutoMapper;
using CardPileAPI.Infrastructure.Authentication.OAuth;
using CardPileAPI.Infrastructure.Errors;
using CardPileAPI.Infrastructure.Exceptions;
using CardPileAPI.Presentation.Commands.OAuth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection;

namespace CardPileAPI.Controllers
{

    public class OAuthController : BaseController
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public OAuthController(IMapper mapper)
        {
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

            return default;
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
