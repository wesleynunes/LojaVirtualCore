using LojaVirtualCore.Models;
using System.Net;
using System.Net.Mail;

namespace LojaVirtualCore.Libraries.Email
{
    public class ContatoEmail
    {
        public static void EnviarContatoPorEmail(Contato contato)
        {
            //smtp -> Servidor que vai enviar a mensagem
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("wesleysilva.ti@gmail.com", "Blue130186!@#"),
                EnableSsl = true
            };


            string corpoMsg = string.Format("<h2>Contato - LojaVirtual</h2>" + 
                "<b>Nome: </b> {0} <br/>" +
                "<b>E-mail: </b> {1} <br/>" +
                "<b>Texto: </b> {2} <br/>" +
                "<br/> E-mail enviado automaticamente do site LojaVirtual.",
                contato.Nome,
                contato.Email,
                contato.Texto
            );


            //MailMessage -> construir a mensagem
            MailMessage message = new MailMessage
            {
                From = new MailAddress("wesleysilva.ti@gmail.com")
            };
            message.To.Add("wesleysilva.ti@gmail.com");
            message.Subject = "Contato - LojaVirtual - E-Mail: " + contato.Email;
            message.Body = corpoMsg;
            message.IsBodyHtml = true;

            //enviar mensagem via smtp
            smtp.Send(message);
        }
    }
}
