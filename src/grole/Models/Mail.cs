using grole.src.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace grole.Models
{
    public class Mail
    {
        public void EnviarCorreoCambioProducto(CorreoCambioTara ACorreo)
        {
            MailMessage objeto_mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "192.168.0.197";
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("soles\\soporteweb", "soles1.");
            objeto_mail.From = new MailAddress("soporteweb@soles.com");
            objeto_mail.To.Add(new MailAddress("adrian.avila.mtz@gmail.com"));
            objeto_mail.Subject = "Password Recover";
            objeto_mail.Body = "From: Soporte WEB <soporteweb@soles.com> " +
                                "To: Grole " +
                                "BC: Hernando Camacho " +
                                "Subject: Accion: #{a_params[:accion]}, Producto: " + ACorreo.Folio + " " + ACorreo.Descripcion + ", usuario " + ACorreo.Usuario +
                                "MIME - Version: 1.0 " +
                                "Content - type: text / html " +

                                "Se " + ACorreo.Accion + " Producto " + ACorreo.Folio + ACorreo.Descripcion + ", usuario: " + ACorreo.Usuario + ", fecha: " + ACorreo.Fecha.ToShortDateString() +
                                "</ br >" +
                                "AData";
            client.Send(objeto_mail);
        }
    }
}
