using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Registration_and_Autorization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegistrationForm registration;
        AurizationForm aurization;
        public MainWindow()
        {
            InitializeComponent();
            registration = new RegistrationForm(
              TextLogin, TextPassword
              );

            aurization = new AurizationForm(
                TextLogin, TextPassword
                );
        }
        private void AvtorizButton_Click(object sender, RoutedEventArgs e)
        {
            if (AvtorizButton.Content.ToString() == "Уже есть аккаунт")
            {
                LabelRegisrt.Content = "Авторизация";
                RegButton.Content = "Войти";
                AvtorizButton.Content = "Назад";
            }
            else
            {
                LabelRegisrt.Content = "Регистрация";
                RegButton.Content = "Зарегестрироваться";
                AvtorizButton.Content = "Уже есть аккаунт";
            }
        }
        private bool Str(string str )
        {
            if (str.Length>0 && str.IndexOfAny(new char[] { '/', '*', '+', '-', '&', '?', '.', ',', ' ', '!', '#', '$', '%', '^', '(', ')', '=', '<', '>' })==-1)
            {
                return true;
            }
            return false;
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if(Str(TextLogin.Text) && Str(TextPassword.Password))
            {
                if (RegButton.Content.ToString() == "Зарегестрироваться")
                {
                    if (registration.execute("users.txt"))
                    {
                        MessageBox.Show("Удачная регистрация");
                    }
                    else
                    {
                        MessageBox.Show("Регистрация не удалась");
                    }
                }
                else
                {
                    if (aurization.execute("users.txt"))
                    {
                        MessageBox.Show($"Добрый день, {TextLogin.Text}");
                        //Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверно введенные данные");
                    }
                }

            }
            else
            {
                MessageBox.Show("Запрещенный символ!");
            }
        }
    }
}
