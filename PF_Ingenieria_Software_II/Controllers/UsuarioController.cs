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
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace PF_Ingenieria_Software_II.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly PolideportivobdContext Context;
        private readonly IRolService _rolService;
        private readonly IDocumentTypeService _documentTypeService;

        public UsuarioController(PolideportivobdContext context, IRolService rolService, IDocumentTypeService documentTypeService)
        {
            Context = context;
            _rolService = rolService;
            _documentTypeService = documentTypeService; 
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
                           && UserTable.Contrasena == UtilityService.Encrypt(contrasena)
                           select UserTable).FirstOrDefault();
                if (Obj != null)
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, Obj.NombreUsuario),
                        new Claim(ClaimTypes.Email, Obj.Correo),
                        new Claim(ClaimTypes.NameIdentifier, Obj.Id.ToString())
                    };

                    var rolId = Obj.RolId;
                    Rol userRol = _rolService.FindRol(rolId);
                    
                    claims.Add(new Claim(ClaimTypes.Role, userRol.Nombre));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    // Configurar variables de sesión
                    HttpContext.Session.SetString("UserName", Obj.NombreUsuario);
                    HttpContext.Session.SetString("UserId", Obj.Id.ToString());

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Usuario o contraseña incorrectos";
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

        public Usuario FindUsuario(int id)
        {
            var user = Context.Usuarios.FirstOrDefault(u => u.Id == id);
            return user;
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id");
            ViewData["NombreDocumento"] = new SelectList(Context.TipoDocumentos, "Nombre", "Nombre");
            ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id");
            ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id");
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUsuario(string nombres, string apellidoPaterno, string apellidoMaterno,
                                int edad, string direccion, string telefono,
                                string correo, string contrasena, string confirmarContrasena,
                                string nombreDocumento, string nroDocumento)
        {
            var documentoId = _documentTypeService.GetDocumentId(nombreDocumento);
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
                Contrasena = UtilityService.Encrypt(contrasena),
                RolId = 3, // Se crea como Alumno por defecto
                EstadoId = 1, // Activo por defecto
                DocumentoId = documentoId,
                NroDocumento = nroDocumento,
                NombreUsuario = $"{nombres.Substring(0, 1).ToLower()}.{apellidoPaterno.ToLower()}"
            };

            if (string.IsNullOrEmpty(nombres))
            {
                ModelState.AddModelError("Nombres", "El campo Nombres es obligatorio");
            }
            else if (!Regex.IsMatch(nombres, @"^[a-zA-Z ]+$"))
            {
                ModelState.AddModelError("Nombres", "El campo Nombres solo debe contener letras");
            }

            if (string.IsNullOrEmpty(apellidoPaterno))
            {
                ModelState.AddModelError("ApellidoPaterno", "El campo Apellido Paterno es obligatorio");
            }
            else if (!Regex.IsMatch(apellidoPaterno, @"^[a-zA-Z]+$"))
            {
                ModelState.AddModelError("ApellidoPaterno", "El campo Apellido Paterno solo debe contener letras");
            }

            if (string.IsNullOrEmpty(apellidoMaterno))
            {
                ModelState.AddModelError("ApellidoMaterno", "El campo Apellido Materno es obligatorio");
            }
            else if (!Regex.IsMatch(apellidoMaterno, @"^[a-zA-Z]+$"))
            {
                ModelState.AddModelError("ApellidoMaterno", "El campo Apellido Materno solo debe contener letras");
            }

            if (edad < 0 || edad > 150)
            {
                ModelState.AddModelError("Edad", "La Edad debe estar entre 0 y 150");
            }

            if (string.IsNullOrEmpty(direccion))
            {
                ModelState.AddModelError("Direccion", "El campo Dirección es obligatorio");
            }

            if (string.IsNullOrEmpty(telefono))
            {
                ModelState.AddModelError("Telefono", "El campo Teléfono es obligatorio");
            }

            if (string.IsNullOrEmpty(correo))
            {
                ModelState.AddModelError("Correo", "El campo EMAIL es obligatorio");
            }
            else if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                ModelState.AddModelError("Correo", "El campo EMAIL no es una dirección de correo electrónico válida");
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                ModelState.AddModelError("Contrasena", "El campo CONTRASEÑA es obligatorio");
            }
            else if (contrasena.Length != 8)
            {
                ModelState.AddModelError("Contrasena", "La clave debe contener 8 caracteres");
            }

            if (string.IsNullOrEmpty(confirmarContrasena))
            {
                ModelState.AddModelError("ConfirmarContrasena", "Debe confirmar la contraseña");
            }
            else if (contrasena != confirmarContrasena)
            {
                ModelState.AddModelError("ConfirmarContrasena", "Las contraseñas no coinciden.");
            }

            if (string.IsNullOrEmpty(nroDocumento))
            {
                ModelState.AddModelError("NroDocumento", "El campo NroDocumento es obligatorio");
            }
            else if (!Regex.IsMatch(nroDocumento, @"^[0-9]+$"))
            {
                ModelState.AddModelError("NroDocumento", "El campo NroDocumento solo debe contener números");
            }

            if (ModelState.IsValid)
            {
                Context.Add(usuario);
                await Context.SaveChangesAsync(); 
                return RedirectToAction("Login");
            }

            // Cargar los datos en el ViewBag para las listas desplegables en la vista
            ViewData["DocumentoId"] = new SelectList(Context.TipoDocumentos, "Id", "Id", documentoId);
            ViewData["NombreDocumento"] = new SelectList(Context.TipoDocumentos, "Nombre", "Nombre", nombreDocumento);
            ViewData["EstadoId"] = new SelectList(Context.Estados, "Id", "Id", usuario.EstadoId);
            ViewData["RolId"] = new SelectList(Context.Rols, "Id", "Id", usuario.RolId);
            return View("Create", usuario);
        }

        [Authorize(Roles = "Alumno,Tutor,Administrador")]
        public IActionResult Perfil()
        {
            int idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var usuario = FindUsuario(idUsuario);

            ViewBag.RolService = _rolService;
            ViewBag.DocumentTypeService = _documentTypeService;
            return View(usuario);
        }

        public async Task<IActionResult> EditUsuario(UsuarioViewModel objNew)
        {
            var ObjOld = (from UserTable in Context.Usuarios
                          where UserTable.Id == objNew.Id
                          select UserTable).FirstOrDefault();

            if (ModelState.IsValid)
            {
                // Actualizamos la nueva información
                ObjOld.Nombres = objNew.Nombres;
                ObjOld.ApellidoPaterno = objNew.ApellidoPaterno;
                ObjOld.ApellidoMaterno = objNew.ApellidoMaterno;
                ObjOld.Edad = objNew.Edad;
                ObjOld.Direccion = objNew.Direccion;
                ObjOld.Telefono = objNew.Telefono;
                ObjOld.Correo = objNew.Correo;
                ObjOld.NroDocumento = objNew.NroDocumento;

                await Context.SaveChangesAsync(); // Guardamos los cambios

                return RedirectToAction("Index", "Home", ObjOld);
            }
            else
            {
                var ObjParcial = ObjOld;
                ObjParcial.Nombres = objNew.Nombres;
                ObjParcial.ApellidoPaterno = objNew.ApellidoPaterno;
                ObjParcial.ApellidoMaterno = objNew.ApellidoMaterno;
                ObjParcial.Edad = objNew.Edad;
                ObjParcial.Direccion = objNew.Direccion;
                ObjParcial.Telefono = objNew.Telefono;
                ObjParcial.Correo = objNew.Correo;
                ObjParcial.NroDocumento = objNew.NroDocumento;
                return View("Perfil", ObjParcial);
            }
        }

    }
}
