using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System;
using WebApplication1.Models;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Drawing;

public class UsuariosController : ApiController
{
    private DbModels context = new DbModels();

    // GET: api/Usuarios
    public IQueryable<Usuarios> GetUsuarios()
    {
        return context.Usuarios;
    }

    // GET: api/Usuarios/5
    public IHttpActionResult GetUsuario(int id)
    {
        var usuario = context.Usuarios.Find(id);
        if (usuario == null)
        {
            return NotFound();
        }

        return Ok(usuario);
    }

    // GET: api/Usuarios/Page
    [HttpGet]
    [Route("api/Usuarios/Page")]
    public IHttpActionResult GetUsuariosPage(int page = 1, int pageSize = 1)
    {
        try
        {
            // Validar valores de página y tamaño de página para evitar valores negativos o incorrectos
            if (page < 1 || pageSize < 1)
            {
                return BadRequest("Los valores de página y tamaño de página deben ser mayores que cero.");
            }

            // Calcular el índice de inicio para la paginación
            int startIndex = (page - 1) * pageSize;

            // Obtener usuarios paginados de la base de datos
            var usuarios = context.Usuarios.OrderBy(u => u.Id)
                                           .Skip(startIndex)
                                           .Take(pageSize)
                                           .ToList();

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            // Manejar otras excepciones aquí si es necesario
            return InternalServerError(ex);
        }
    }

    // POST: api/Usuarios

    [HttpPost]
    public IHttpActionResult PostUsuario(Usuarios usuario)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verificar si el usuario ya existe en la base de datos
            var existingUser = context.Usuarios.FirstOrDefault(u => u.Rut == usuario.Rut);
            if (existingUser != null)
            {
                // Usuario ya existe, puedes manejar esto según tus necesidades
                return Conflict(); // Estado HTTP 409 Conflict
            }

            // Si el usuario no existe, agregarlo a la base de datos
            context.Usuarios.Add(usuario);

            // Comentar la siguiente línea para permitir que la base de datos genere automáticamente la clave primaria
            // context.SaveChanges();

            // Puedes devolver una respuesta JSON, por ejemplo, el usuario recién creado
            return Ok(usuario);
        }
        catch (DbUpdateException ex)
        {
            // Manejar la excepción según tus necesidades
            // Puedes registrar el error o devolver una respuesta de error específica
            return InternalServerError(ex);
        }
        catch (Exception)
        {
            // Manejar otras excepciones aquí si es necesario
            return InternalServerError();
        }
    }


    // PUT: api/Usuarios/5/Actualizar
    [HttpPut]
    public IHttpActionResult PutUsuario(int id, [FromBody] Usuarios usuario)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // Lógica para actualizar el usuario en la base de datos
            var usuarioExistente = context.Usuarios.Find(id);

            if (usuarioExistente != null)
            {
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Rut = usuario.Rut;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;

                context.SaveChanges();

                return Ok(usuarioExistente);
            }
            else
            {
                return NotFound();
            }
        }
        catch (DbUpdateException ex)
        {
            // Manejar la excepción según tus necesidades
            // Puedes registrar el error o devolver una respuesta de error específica
            return InternalServerError(ex);
        }
        catch (Exception)
        {
            // Manejar otras excepciones aquí si es necesario
            return InternalServerError();
        }
    }

    // DELETE: api/Usuarios/5
    public IHttpActionResult DeleteUsuario(int id)
    {
        var usuario = context.Usuarios.Find(id);
        if (usuario == null)
        {
            return NotFound();
        }

        context.Usuarios.Remove(usuario);
        context.SaveChanges();

        return Ok(usuario);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            context.Dispose();
        }
        base.Dispose(disposing);
    }

    [HttpGet]
    [Route("api/Usuarios/ExportToExcel")]
    public HttpResponseMessage ExportToExcel()
    {
        var usuarios = context.Usuarios.ToList();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Usuarios");

            // Encabezados
            worksheet.Cells[1, 1].Value = "Nombre";
            worksheet.Cells[1, 2].Value = "Rut";
            worksheet.Cells[1, 3].Value = "Correo";
            worksheet.Cells[1, 4].Value = "Fecha Nacimiento";

            // Estilo del encabezado
            using (var range = worksheet.Cells["A1:D1"])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            }

            // Datos
            for (int i = 0; i < usuarios.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = usuarios[i].Nombre;
                worksheet.Cells[i + 2, 2].Value = usuarios[i].Rut;
                worksheet.Cells[i + 2, 3].Value = usuarios[i].Correo;
                worksheet.Cells[i + 2, 4].Value = usuarios[i].FechaNacimiento.ToString("yyyy-MM-dd");
            }

            // Guardar el paquete en un MemoryStream
            var stream = new MemoryStream(package.GetAsByteArray());

            // Devolver el archivo como HttpResponseMessage
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };

            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Usuarios.xlsx"
            };

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return result;
        }
    }

    [HttpGet]
    [Route("api/Usuarios/Search")]
    public IHttpActionResult SearchUsuarios(string term)
    {
        try
        {
            // Filtrar usuarios según el término de búsqueda
            var usuariosFiltrados = context.Usuarios
                .Where(u => u.Nombre.Contains(term) || u.Rut.Contains(term) || u.Correo.Contains(term))
                .ToList();

            return Ok(usuariosFiltrados);
        }
        catch (Exception ex)
        {
            // Manejar otras excepciones aquí si es necesario
            return InternalServerError(ex);
        }
    }

}


