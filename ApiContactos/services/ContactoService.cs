using ApiContactos.path;
public class ContactoService
{   
    private readonly DbA358b2Pam3Context _dbContext;
    public ContactoService(DbA358b2Pam3Context context)
    {
        _dbContext = context;
    }

    public List<Contacto> ObtenerTodos()
    {
        var contactos = _dbContext.Contactos.ToList();
        return [..contactos];
    }

    public Contacto? ObtenerPorId(int id)
    {
        var contacto = _dbContext.Contactos.Find(id) ?? null;
        return contacto;
    }

    public Contacto Crear(Contacto contacto)
    {
        var contactoNuevo = _dbContext.Contactos.Add(contacto);
        _dbContext.SaveChanges();
        return contactoNuevo.Entity;
    }
    public bool Eliminar(int Id)
    {
        var contacto = _dbContext.Contactos.Find(Id) ?? null;
        if (contacto == null)
        {
            return false;
        }
        _dbContext.Contactos.Remove(contacto);
        _dbContext.SaveChanges();
        return true;
    }
    public bool Editar(int Id, Contacto datosActualizados)
    {
        var contacto = _dbContext.Contactos.Find(Id) ?? null;
        if (contacto == null)
        {
            return false;
        }
        _dbContext.Entry(Id).CurrentValues.SetValues(datosActualizados);
        _dbContext.SaveChanges();
        return true;
    }

}