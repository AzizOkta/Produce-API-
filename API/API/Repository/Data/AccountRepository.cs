using API.Context;
using API.Models;
using API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext Context;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.Context = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            var checkEmailPhone = Context.Employees.Where(e => e.Email == loginVM.Email || e.Phone == loginVM.Email)
                .FirstOrDefault();
            if (checkEmailPhone != null)
            {
                var getPassword = Context.Accounts.Where(e => e.NIK == checkEmailPhone.NIK).FirstOrDefault();
                if(BCrypt.Net.BCrypt.Verify(loginVM.Password, getPassword.Password))
                {
                    return 1;// login berhasil
                }
                else
                {
                    return 2;// checkEmail ditemukan password salah
                }
            }
            else
            {
                return 3;// email tidak ditemuakn
            }
          
        }

        public int GenerateOTP (string s, string otp)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(s);
            mail.From = new MailAddress("tesotpauthentication@gmail.com", "OTP", System.Text.Encoding.UTF8);
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "OTP Code =" + otp;
            mail.Body = DateTime.Now.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("tesotpauthentication@gmail.com", "Otp_12345");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
            }
            catch (Exception exc)
            {
                Exception newexc = exc;
                string errorMessage = string.Empty;
                while (newexc != null)
                {
                    errorMessage += newexc.ToString();
                    newexc = newexc.InnerException;
                }
            }
            return 0;
        }


    public int ForgotPass(string email)
        {
            var emp = Context.Employees.Where(e => e.Email == email).FirstOrDefault();
         
            Random Otp = new Random();
            var kirim = Otp.Next(11111, 99999);

            if (emp != null)
            {
                var acc = Context.Accounts.Where(e => e.NIK == emp.NIK).FirstOrDefault();
                acc.otp = kirim;
                acc.ExpiredTime = DateTime.Now.AddMinutes(5);
                acc.IsTrue = false;
                Context.Entry(acc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                Context.SaveChanges();
                GenerateOTP(email, kirim.ToString());
                return 1;
            }
            
                return 0;                   
        }



    }

}
