using System;
using System.Collections.Generic;

namespace ApiContactos.path;

public partial class Contacto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime FechaCreacion { get; set; }
}
