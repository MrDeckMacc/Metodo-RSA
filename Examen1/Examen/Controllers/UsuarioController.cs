using Microsoft.AspNetCore.Mvc;
using Examen.Models;
using Examen.Data;
using System.Numerics;

public class UsuarioController : Controller
{
    //Valores para la generacion de Llaves
    public BigInteger p = 83;
    public BigInteger q = 97;
    public BigInteger e = 11;
    private readonly ApplicationDbContext _context;
    public UsuarioController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Registro()
    {
        return View(new Usuario());
    }
    [HttpPost]
    public IActionResult Registrar(Usuario usuario)
    {
        if (usuario == null)
        {
            return BadRequest("El objeto usuario es null");
        }
        if (ModelState.IsValid) 
        {
            string contraseñaCifrada = CifradoRSA.CifrarContraseña(usuario.Contraseña, p, q);
            usuario.Contraseña = contraseñaCifrada;

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

            TempData["MensajeExito"] = "¡Registro exitoso!";
            return RedirectToAction("Index", "Home");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public IActionResult InicioSesion()
    {
        return View();
    }

    [HttpPost]
    public IActionResult InicioSesion(string nombreUsuario, string contraseña)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

        if(usuario != null)
        {
            string contraseñaAlmacenada = usuario.Contraseña;
            string contraseñaDescifrada = CifradoRSA.DescifrarContraseña(contraseñaAlmacenada, p, q);

            if(contraseña == contraseñaDescifrada) 
            {
                TempData["MensajeExito"] = "¡Inicio de sesión exitoso!";
                return RedirectToAction("Privacy","Home");
            }
        }
        ModelState.AddModelError(string.Empty, "La contraseña o el usuario son incorrectos.");
        return RedirectToAction("Index", "Home");
    }
}
