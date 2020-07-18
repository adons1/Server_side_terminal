
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ClientTerminal
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private  void Form2_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            if (Global.person.Status == 1)
            {
                историяДействийСБДToolStripMenuItem.Visible = true;
                списокРаботниковToolStripMenuItem.Visible = true;
            }
        }
        List<Product> products_list = new List<Product>();
        List<OrderExtended> orders_list = new List<OrderExtended>();
        List<Person> workers_list = new List<Person>();
        List<Client> clients_list = new List<Client>();
        List<Action> actions_list = new List<Action>();
        List<string> goods = new List<string>();
        List<string> adres = new List<string>();
        private async void списокТоваровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            listBox1.Items.Clear();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync("http://localhost:7907/Home/GetProducts"))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();
                            byte[] data = Convert.FromBase64String(mycontent);
                            byte[] results = Serialization_Encryption_Certification.Decryption(data);
                            XmlDocument doc = new XmlDocument();
                            string res = UTF8Encoding.UTF8.GetString(results);
                            doc.LoadXml(res);
                            X509Certificate2 pubCert = new X509Certificate2(@"C:\Users\Adons_\Desktop\ceritif-cert-public.pem");
                            if (Serialization_Encryption_Certification.ValidateXmlDocumentWithCertificate(doc, pubCert))
                            {
                                label1.Text = "fine";
                                label1.Visible = true;
                            }
                            else
                            {
                                label1.Text = "В процессе передачи данные были повреждены";
                                label1.Visible = true;
                            }
                           products_list = Serialization_Encryption_Certification.Deserialize0(results);
                            int c = 0;
                            comboBox1.Items.Add("Любой");
                            comboBox2.Items.Add("Любой");
                            foreach (Product p in products_list)
                            {
                                listBox1.Items.Add(Convert.ToString(p.ProductId) + "\t" + Convert.ToString(p.ProductName) + "\t" + Convert.ToString(p.Addres) + "\t" + Convert.ToString(p.Weigth) + "\t" + Convert.ToString(p.Price));

                                button1.Visible = true;
                                comboBox1.Visible = true;
                                comboBox2.Visible = true;

                                if (goods.IndexOf(p.ProductName) == -1)
                                {
                                    comboBox1.Items.Add(p.ProductName);
                                }
                                goods.Add(p.ProductName);
                                if (adres.IndexOf(p.Addres) == -1)
                                {
                                    comboBox2.Items.Add(p.Addres);
                                }
                                adres.Add(p.Addres);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            finally
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string ProdName = comboBox1.Text;
            string Adres = comboBox2.Text;
            listBox1.Items.Clear();

            foreach(Product p in products_list)
            {
                if ((ProdName == p.ProductName) && (Adres == p.Addres))
                {
                    listBox1.Items.Add(Convert.ToString(p.ProductId) + "\t" + Convert.ToString(p.ProductName) + "\t" + Convert.ToString(p.Addres) + "\t" + Convert.ToString(p.Weigth) + "\t" + Convert.ToString(p.Price));
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void списокЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label1.Visible = false;

            listBox1.Items.Clear();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync("http://localhost:7907/Home/GetOrders"))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();
                            byte[] data = Convert.FromBase64String(mycontent);
                            byte[] results = Serialization_Encryption_Certification.Decryption(data);
                            XmlDocument doc = new XmlDocument();
                            string res = UTF8Encoding.UTF8.GetString(results);
                            doc.LoadXml(res);
                            X509Certificate2 pubCert = new X509Certificate2(@"C:\Users\Adons_\Desktop\ceritif-cert-public.pem");
                            if (Serialization_Encryption_Certification.ValidateXmlDocumentWithCertificate(doc, pubCert))
                            {
                                label1.Text = "fine";
                                label1.Visible = true;
                            }
                            else
                            {
                                label1.Text = "В процессе передачи данные были повреждены";
                                label1.Visible = true;
                            }
                            orders_list = Serialization_Encryption_Certification.DeserializeOrdersExtended(results);
                            foreach (OrderExtended p in orders_list)
                            {
                                listBox1.Items.Add(Convert.ToString(p.ProductId) + "\t" + Convert.ToString(p.ClientId) + "\t" + Convert.ToString(p.OrderId) + "\t" + Convert.ToString(p.ClientName) + "\t" + Convert.ToString(p.OrderDate) + "\t" + Convert.ToString(p.ProductName) + "\t" + Convert.ToString(p.Price));

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            finally
            {

            }
        }

        private async void списокРаботниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label1.Visible = false;
            listBox1.Items.Clear();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync("http://localhost:7907/Home/GetWorkers"))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();
                            byte[] data = Convert.FromBase64String(mycontent);
                            byte[] results = Serialization_Encryption_Certification.Decryption(data);
                            XmlDocument doc = new XmlDocument();
                            string res = UTF8Encoding.UTF8.GetString(results);
                            doc.LoadXml(res);
                            X509Certificate2 pubCert = new X509Certificate2(@"C:\Users\Adons_\Desktop\ceritif-cert-public.pem");
                            if (Serialization_Encryption_Certification.ValidateXmlDocumentWithCertificate(doc, pubCert))
                            {
                                label1.Text = "fine";
                                label1.Visible = true;
                            }
                            else
                            {
                                label1.Text = "В процессе передачи данные были повреждены";
                                label1.Visible = true;
                            }
                            workers_list = Serialization_Encryption_Certification.DeserializePerson(results);

                            foreach (Person p in workers_list)
                            {
                                listBox1.Items.Add(Convert.ToString(p.PersonId) + "\t" + Convert.ToString(p.Login) + "\t" + Convert.ToString(p.Password) + "\t" + Convert.ToString(p.Name) + "\t" + Convert.ToString(p.Status));

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            finally
            {

            }
        }

        private async void списокКлиентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label1.Visible = false;
            listBox1.Items.Clear();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync("http://localhost:7907/Home/GetClients"))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();
                            byte[] data = Convert.FromBase64String(mycontent);
                            byte[] results = Serialization_Encryption_Certification.Decryption(data);
                            XmlDocument doc = new XmlDocument();
                            string res = UTF8Encoding.UTF8.GetString(results);
                            doc.LoadXml(res);
                            X509Certificate2 pubCert = new X509Certificate2(@"C:\Users\Adons_\Desktop\ceritif-cert-public.pem");
                            if (Serialization_Encryption_Certification.ValidateXmlDocumentWithCertificate(doc, pubCert))
                            {
                                label1.Text = "fine";
                                label1.Visible = true;
                            }
                            else
                            {
                                label1.Text = "В процессе передачи данные были повреждены";
                                label1.Visible = true;
                            }
                            clients_list = Serialization_Encryption_Certification.DeserializeClient(results);

                            foreach (Client p in clients_list)
                            {
                                listBox1.Items.Add(Convert.ToString(p.ClientId) + "\t" + Convert.ToString(p.ClientName) + "\t" + Convert.ToString(p.PhoneNumber));

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            finally
            {

            }
        }

        private async void историяДействийСБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            label1.Visible = false;
            listBox1.Items.Clear();
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage response = await client.GetAsync("http://localhost:7907/Home/GetActions"))
                    {
                        using (HttpContent content = response.Content)
                        {
                            string mycontent = await content.ReadAsStringAsync();
                            byte[] data = Convert.FromBase64String(mycontent);
                            byte[] results = Serialization_Encryption_Certification.Decryption(data);
                            XmlDocument doc = new XmlDocument();
                            string res = UTF8Encoding.UTF8.GetString(results);
                            doc.LoadXml(res);
                            X509Certificate2 pubCert = new X509Certificate2(@"C:\Users\Adons_\Desktop\ceritif-cert-public.pem");
                            if (Serialization_Encryption_Certification.ValidateXmlDocumentWithCertificate(doc, pubCert))
                            {
                                label1.Text = "fine";
                                label1.Visible = true;
                            }
                            else
                            {
                                label1.Text = "В процессе передачи данные были повреждены";
                                label1.Visible = true;
                            }
                            actions_list = Serialization_Encryption_Certification.DeserializeAction(results);

                            foreach (Action p in actions_list)
                            {
                                listBox1.Items.Add(Convert.ToString(p.idAction) + "\t" + Convert.ToString(p.ActionId) + "\t" + Convert.ToString(p.ActionDescription) + "\t" + Convert.ToString(p.PersonId) + "\t" + Convert.ToString(p.Date));

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
            finally
            {

            }
        }
    }
}
