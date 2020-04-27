using LojaVirtualCore.Data;
using LojaVirtualCore.Libraries.Email;
using LojaVirtualCore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtualCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly LojaVirtualCoreContext _banco;

        public HomeController(LojaVirtualCoreContext banco)
        {
            _banco = banco;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
           

        [HttpPost]
        public IActionResult Index([FromForm] NewsletterEmail newsletterEmail)
        {
           
            if (ModelState.IsValid)
            {
                _banco.NewsletterEmails.Add(newsletterEmail);
                _banco.SaveChanges();
                TempData["MSG_S"] = "E-mail cadastrado! Agora você vai receber promoções especiais no seu e-mail! Fique atento as novidades!";
                return RedirectToAction(nameof(Index));                
            }
            else
            {
                return View();
            }

            //return View();
        }
        
        public IActionResult Contato()
        {
            return View();
        }


        public IActionResult ContatoAcao()
        {

            try
            {
                Contato contato = new Contato
                {
                    Nome = HttpContext.Request.Form["nome"],
                    Email = HttpContext.Request.Form["email"],
                    Texto = HttpContext.Request.Form["texto"]
                };

                var listaMensagens = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                bool isValid =  Validator.TryValidateObject(contato, contexto, listaMensagens, true);

                if (isValid)
                {
                    ContatoEmail.EnviarContatoPorEmail(contato);
                    ViewData["MSG_S"] = "Mensagem de contato enviado com sucesso!";
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach(var texto in listaMensagens)
                    {
                        sb.Append(texto.ErrorMessage + "<br/>");
                    }

                    ViewData["MSG_E"] = sb.ToString();
                    ViewData["CONTATO"] = contato;
                }           

            }
            catch (Exception)
            {
                ViewData["MSG_E"] = "Opps! Tivemos um erro. tente novamente mais tarde!";

                //TODO - implementar log
            }

            return View("contato");
        }


        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
