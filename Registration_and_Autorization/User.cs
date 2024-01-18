using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Registration_and_Autorization
{
    abstract class UserForm
    {
        protected TextBox login;
        protected PasswordBox password;

        public UserForm(TextBox login, PasswordBox password)
        {
            this.login = login;
            this.password = password;
        }

        abstract public bool execute(string path);
    }
    class RegistrationForm : UserForm
    {
        public RegistrationForm(TextBox login, PasswordBox password) :
            base(login, password)
        { }

        public override bool execute(string path)
        {
            string textLogin = login.Text;

            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string log = s.Split(' ')[0];
                        if (log == textLogin)
                            return false;

                    }
                }
            }

            int hashPassword = password.Password.GetHashCode();
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine($"{textLogin} {hashPassword}");
            }
            return true;
        }
    }

    class AurizationForm : UserForm
    {
        public AurizationForm(TextBox login, PasswordBox password) :
           base(login, password)
        { }

        public override bool execute(string path)
        {
            string textLogin = login.Text;
            string textHashPassword = password.Password.GetHashCode().ToString();
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] data = s.Split(' ');
                        if (data[0] == textLogin && data[1] == textHashPassword)
                            return true;
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
