// BasicAuthenticationHandler.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyFace.Models.Database;
using MyFace.Repositories;
using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;


namespace MyFace.Models.Response
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		 //private readonly IUserService _userService;
		  private readonly IUsersRepo _users;


		  public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUsersRepo users) : base(options, logger, encoder, clock)
        	{

            	_users = users;
        	}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync()

        {

            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            User user = null;
			
			try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);  //{username}:{password}
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _users.Authenticate(username, password);
            }
			catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

			if (user == null)
                return AuthenticateResult.Fail("Invalid Username or Password");

			 var claims = new[] {

                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);

		}



	}
}