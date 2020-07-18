using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Net.Http;
using System.Threading;

namespace ClientTerminal
{
    public class Auth
    {
        public string login;
        public string password;
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            Person user = new Person();
            user.Login = textBox1.Text;
            user.Password = textBox2.Text;
            if (user.Password.Length < 4)
            {
                label3.Text = "Пароль должен быть не менее 4 символов.";
                label3.Visible = true;
                
            }
            else{
                string log_pass_enc = Serialization_Encryption_Certification.Encryption(user);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        Dictionary<string, string> str = new Dictionary<string, string>
                    {
                        {"log_pass_enc",log_pass_enc }
                    };
                        FormUrlEncodedContent cont = new FormUrlEncodedContent(str);
                        using (HttpResponseMessage response = await client.PostAsync("http://localhost:7907/Home/Hello", cont))
                        {
                            using (HttpContent content = response.Content)
                            {
                                string mycontent = await content.ReadAsStringAsync();
                               
                                if (mycontent == "access_denied")
                                {
                                    label3.Text = "Неправильный логин или пароль";
                                    label3.Visible = true;
                                }
                                else
                                {
                                    Global.person = Serialization_Encryption_Certification.Deserialize(Serialization_Encryption_Certification.Decryption(Convert.FromBase64String(mycontent)));
                                    this.Close();
                                    Thread th = new Thread(opennewform);
                                    th.SetApartmentState(ApartmentState.STA);
                                    th.Start();
                                }
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    label3.Text = "Сервер выключен.";
                    label3.Visible = true;
                }
                finally
                {

                }
            }
            
        }
        private void opennewform()
        {
            Application.Run(new Form2());
        }
    }
}
