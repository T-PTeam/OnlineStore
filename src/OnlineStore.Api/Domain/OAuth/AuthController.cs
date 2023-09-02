using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Api.Common;
using OnlineStore.Api.Common.Responses;
using OnlineStore.Core.Domain.Users.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineStore.Api.Domain.OAuth
{
    [ApiController]
    [Route(Routs.Auth)]
    public class AuthController : ControllerBase
    {
        public readonly IMediator _mediator;

        public AuthController (IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IResult> LogIn([FromBody] string email, [FromBody]string password, CancellationToken cancellationToken)
        {
            var query = new GetUserByEmailWithPasswordQuery(email, password);
            User? user = await _mediator.Send(query, cancellationToken);

            if (user is not null) return Results.Json(MakeTokenByEmail(user.Email));
            return Results.NotFound("User was not found..."); 
        }
        [HttpPost]
        public async Task<IResult> Registrate([FromBody] User user, CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(user);
            var response = await _mediator.Send(command, cancellationToken);
        }

        private string MakeTokenByEmail(string email)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Email, email)
            };
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
