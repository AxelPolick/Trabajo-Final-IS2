using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PF_Ingenieria_Software_II.Models;
using PF_Ingenieria_Software_II.Controllers;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using PF_Ingenieria_Software_II.Interfaces;
using PF_Ingenieria_Software_II.Services;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly PolideportivobdContext Context;
        private readonly IRolService RolService;

        public UsuarioController(PolideportivobdContext context, IRolService rolService)
        {
            Context = context;
            RolService = rolService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Authenticate(string correo, string contrasena)
        {
            if (ModelState.IsValid)
            {
                var Obj = (from UserTable in Context.Usuarios
                           where UserTable.Correo == correo
                           && UserTable.Contrasena == contrasena
                           select UserTable).FirstOrDefault();
                if (Obj != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, Obj.NombreUsuario),
                        new Claim("Email", Obj.Correo)
                    };

                    var rolId = Obj.RolId;
                    Rol userRol = RolService.FindRol(rolId);
                    
                    claims.Add(new Claim(ClaimTypes.Role, userRol.Nombre));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View("Login");
                }

            }
            else
            {
                return View("Login");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Landing");
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id");
            ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id");
            ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id");
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUsuario(string nombres, string apellidoPaterno, string apellidoMaterno,
                                    int edad, string direccion, string telefono,
                                    string correo, string contrasena,
                                    int documentoId, string nroDocumento)
        {
            // Crear un nuevo objeto Usuario
            var usuario = new Usuario
            {
                Id = Context.Usuarios.Count() + 1,
                Nombres = nombres,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Edad = edad,
                Direccion = direccion,
                Telefono = telefono,
                Correo = correo,
                Contrasena = contrasena,
                RolId = 3, // Siempre se crean como Alumnos
                EstadoId = 1, // Activo
                DocumentoId = documentoId,
                NroDocumento = nroDocumento,
                NombreUsuario = $"{nombres.Substring(0, 1).ToLower()}.{apellidoPaterno.ToLower()}"
            };
            if (ModelState.IsValid)
            {
                Context.Add(usuario);
                await Context.SaveChangesAsync(); // Asegúrate de esperar la operación
                return RedirectToAction("Index", "Home");
            }
            ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id", usuario.DocumentoId);
            ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id", usuario.EstadoId);
            ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id", usuario.RolId);
            return View("Create");
        }
    }
}
