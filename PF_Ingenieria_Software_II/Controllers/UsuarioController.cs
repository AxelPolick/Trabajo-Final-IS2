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

        public async Task<IActionResult> Authenticate(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var Obj = (from UserTable in Context.Usuarios
                           where UserTable.Correo == usuario.Correo
                           && UserTable.Contrasena == usuario.Contrasena
                           select UserTable).FirstOrDefault();
                if (Obj != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                        new Claim("Email", usuario.Correo)
                    };

                    var rolId = usuario.RolId;
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
        public async Task<IActionResult> Create([Bind("Id,Nombres,ApellidoPaterno,ApellidoMaterno,Edad,Direccion,Telefono,Correo,Contrasena,NroDocumento,DocumentoId")] Usuario usuario)
        {
            usuario.NombreUsuario = "";
            usuario.RolId = 3; //Siempre se crean como Alumnos
            usuario.EstadoId = 1; //Activo
            if (ModelState.IsValid)
            {
                Context.Add(usuario);
                await Context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id", usuario.DocumentoId);
            ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id", usuario.EstadoId);
            ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id", usuario.RolId);
            return View(usuario);
        }

        //// GET: Usuario
        //public async Task<IActionResult> Index()
        //{
        //    var polideportivobdContext = Context.Usuarios.Include(u => u.Documento).Include(u => u.Estado).Include(u => u.Rol);
        //    return View(await polideportivobdContext.ToListAsync());
        //}

        //// GET: Usuario/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || Context.Usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuario = await Context.Usuarios
        //        .Include(u => u.Documento)
        //        .Include(u => u.Estado)
        //        .Include(u => u.Rol)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuario);
        //}

        //// GET: Usuario/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || Context.Usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuario = await Context.Usuarios.FindAsync(id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id", usuario.DocumentoId);
        //    ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id", usuario.EstadoId);
        //    ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id", usuario.RolId);
        //    return View(usuario);
        //}

        //// POST: Usuario/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,ApellidoPaterno,ApellidoMaterno,Edad,Direccion,Telefono,Correo,NombreUsuario,Contrasena,RolId,NroDocumento,DocumentoId,EstadoId")] Usuario usuario)
        //{
        //    if (id != usuario.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            Context.Update(usuario);
        //            await Context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UsuarioExists(usuario.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id", usuario.DocumentoId);
        //    ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id", usuario.EstadoId);
        //    ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id", usuario.RolId);
        //    return View(usuario);
        //}

        //// GET: Usuario/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || Context.Usuarios == null)
        //    {
        //        return NotFound();
        //    }

        //    var usuario = await Context.Usuarios
        //        .Include(u => u.Documento)
        //        .Include(u => u.Estado)
        //        .Include(u => u.Rol)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuario);
        //}

        //// POST: Usuario/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (Context.Usuarios == null)
        //    {
        //        return Problem("Entity set 'PolideportivobdContext.Usuarios'  is null.");
        //    }
        //    var usuario = await Context.Usuarios.FindAsync(id);
        //    if (usuario != null)
        //    {
        //        Context.Usuarios.Remove(usuario);
        //    }
            
        //    await Context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UsuarioExists(int id)
        //{
        //  return (Context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
