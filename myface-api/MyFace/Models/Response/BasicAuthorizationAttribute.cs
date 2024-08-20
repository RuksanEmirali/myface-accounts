// BasicAuthorizationAttribute.cs
using Microsoft.AspNetCore.Authorization;
 
namespace MyFace.Models.Response
{
	public class BasicAuthorizationAttribute : AuthorizeAttribute
	{
		public BasicAuthorizationAttribute()
		{
			AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
		}
	}
}