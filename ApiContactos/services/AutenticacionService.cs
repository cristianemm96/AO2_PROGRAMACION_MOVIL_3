
using ApiContactos.path;
using BCrypt.Net;

public class AutenticacionService
{
    private readonly DbA358b2Pam3Context _dbContext;
   private readonly TokenService tokenService;

    public AutenticacionService(DbA358b2Pam3Context context, TokenService tokenService)
    {
        _dbContext = context;
       this.tokenService = tokenService;
    }
    public bool RegistrarUsuario(RegistrarUsuarioDTO usuario)
    {
        if (_dbContext.Usuarios.Any(u => u.UserName == usuario.UserName))
        {
            return false;
        }
    
       var password = usuario.password;
        var nuevoUsuario = new Usuario { UserName = usuario.UserName, Password = password };
        _dbContext.Usuarios.Add(nuevoUsuario);
        _dbContext.SaveChanges();
        return true;
    }

    public string Autenticar(LoginRequest loginRequest)
    {
      var usuario = _dbContext.Usuarios.FirstOrDefault(u=>u.UserName==loginRequest.UserName);
      if(usuario!= null && usuario.Password == loginRequest.password)
        {
            return tokenService.GenerarToken(usuario.UserName);
        }
         throw new UnauthorizedAccessException("Credenciales incorrectas");
    }
    


}