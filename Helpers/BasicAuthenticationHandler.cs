using BackendApi.Entities;
using BackendApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BackendApi.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        readonly IUserService _userService;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService service) : base(options, logger, encoder, clock)
        { _userService = service; }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            User user = null;
            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                if (authHeader.Scheme.ToLower().Equals("basic"))
                {
                    byte[] creditentialBytes = Convert.FromBase64String(authHeader.Parameter);
                    string[] creditentials = Encoding.UTF8.GetString(creditentialBytes).Split(':');
                    user = await _userService.Authenticate(creditentials[0], creditentials[1]);
                }
                else
                    return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

            // create claims used in the application for roles
            var claims = new[]   // claim is a statement/property of a user (Role)
            {
                new Claim(ClaimTypes.Name, user.Email),
            };

            // https://docs.microsoft.com/en-us/dotnet/standard/security/principal-and-identity-objects
            // create authentication ticket
            var identity = new ClaimsIdentity(claims, Scheme.Name); // capsulates validated data about the user
            var principal = new ClaimsPrincipal(identity);  // represents the security context (role-based, passport)
            var ticket = new AuthenticationTicket(principal, Scheme.Name); // contains identity and authentication state

            return AuthenticateResult.Success(ticket);
        }
    }
}
