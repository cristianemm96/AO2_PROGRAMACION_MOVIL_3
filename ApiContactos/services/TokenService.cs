using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService
{

    private readonly IConfiguration _config;

    public TokenService (IConfiguration config)
    {
        _config = config;
    }

    

    public string GenerarToken(string usuario)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim(ClaimTypes.Name, usuario));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _config["jwt:Issuer"],
            Audience = _config["jwt:Audience"],
            SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!)),
            SecurityAlgorithms.HmacSha256Signature
        )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(tokenConfig);
    }
}