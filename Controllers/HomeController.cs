using Server_side.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using System.Data.Entity;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;

namespace Server_side.Controllers
{
    public class OrderExtended
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public string ClientName { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
    }
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        
        static public string Serialize(Person user)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.xml", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(Person));
            ser.Serialize(stream, user);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.xml");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.xml");
            return xmldoc;
        }
        static public string Serialize(List<Product> products)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.txt", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(List<Product>));
            ser.Serialize(stream, products);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            return xmldoc;
        }
        static public string Serialize(List<Order> orders)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.txt", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(List<Order>));
            ser.Serialize(stream, orders);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            return xmldoc;
        }
        static public string Serialize(List<OrderExtended> orders)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.txt", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(List<OrderExtended>));
            ser.Serialize(stream, orders);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            return xmldoc;
        }
        static public string Serialize(List<Client> clients)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.txt", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(List<Client>));
            ser.Serialize(stream, clients);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            return xmldoc;
        }
        static public string Serialize(List<Person> workers)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.txt", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(List<Person>));
            ser.Serialize(stream, workers);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            return xmldoc;
        }
        static public string Serialize(List<Models.Action> actions)
        {
            var stream = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.txt", FileMode.OpenOrCreate);
            XmlSerializer ser = new XmlSerializer(typeof(List<Models.Action>));
            ser.Serialize(stream, actions);
            stream.Close();
            string xmldoc = System.IO.File.ReadAllText(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.txt");
            return xmldoc;
        }
        static public Person Deserialize(byte[] results)
        {
            string res = UTF8Encoding.UTF8.GetString(results);
            FileStream file = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.xml", FileMode.OpenOrCreate);
            file.Write(results, 0, results.Length);
            file.Close();
            file = new FileStream(@"C:\Users\Adons_\Desktop\log_pass1.xml", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            Person user = (Person)serializer.Deserialize(file);
            file.Close();
            System.IO.File.Delete(@"C:\Users\Adons_\Desktop\log_pass1.xml");
            return user;
        }

        private string Encryption(string xmldoc)
        {
            string hash = "qpmzalbt";
            
            byte[] data = UTF8Encoding.UTF8.GetBytes(xmldoc);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider triDES = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = triDES.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }

        }
        static public byte[] Decryption(byte[] data)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                string hash = "qpmzalbt";
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider triDES = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = triDES.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return results;
                }

            }
        }

        private static void SignXmlDocumentWithCertificate(XmlDocument doc, X509Certificate2 cert)
        {
            SignedXml signedXml = new SignedXml(doc);
            signedXml.SigningKey = cert.PrivateKey;
            Reference reference = new Reference();
            reference.Uri = "";
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(reference);

            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(cert));

            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlSig = signedXml.GetXml();

            doc.DocumentElement.AppendChild(doc.ImportNode(xmlSig, true));
        }


        DBaseContext context = new DBaseContext();
        public string Hello(string log_pass_enc)
        {
            Person user = new Person();
           
            byte[] data = Convert.FromBase64String(log_pass_enc);
            byte[] results = Decryption(data);
            user = Deserialize(results);
           
            
            var person = context.People
                       .Where(s => s.Login == user.Login && s.Password == user.Password).Select(s => new { s.Name, s.Login, s.Password, s.PersonId, s.Status }).ToList();
            if (person.Count != 0)
            {

                user.PersonId = person[0].PersonId;
                user.Name = person[0].Name;
                user.Login = person[0].Login;
                user.Password = person[0].Password;
                user.Status = person[0].Status;

                // ActionId=0 =>вход
                Models.Action action = new Models.Action();
                int peron = context.Actions
                      .Select(s => new { s.idAction, s.ActionId, s.ActionDescription, s.Date, s.PersonId }).ToList().Count;
                action.idAction = peron+1;
                action.ActionId = 0;
                action.ActionDescription = "Пользователь " + user.Login + " вошёл в систему";
                action.Date = DateTime.Now;
                action.PersonId = user.PersonId;
                Global.pers = user;
                context.Actions.Add(action); 
                context.SaveChanges();

                return Encryption(Serialize(user));
            }
            else
            {
                return "access_denied";
            }

        }

        public string GetProducts()
        {
            Product prod = new Product();
            List<Product> productes_list = new List<Product>();

            var productes = context.Products.Select(s => new {s.ProductId, s.ProductName, s.Addres, s.Price, s.Weigth }).ToList();
            for(int i=0; i<productes.Count;++i)
            {
                productes_list.Add(new Product());
                productes_list[i].ProductId = productes[i].ProductId;
                productes_list[i].ProductName = productes[i].ProductName;
                productes_list[i].Addres = productes[i].Addres;
                productes_list[i].Price = productes[i].Price;
                productes_list[i].Weigth = productes[i].Weigth;
            }

            // ActionId=1 =>вывод списка продуктов
            Models.Action action = new Models.Action();
            int peron = context.Actions
                  .Select(s => new { s.idAction, s.ActionId, s.ActionDescription, s.Date, s.PersonId }).ToList().Count;
            action.idAction = peron + 1;
            action.ActionId = 1;
            action.ActionDescription = "Пользователь " + Global.pers.Login + "вывел список продуктов";
            action.Date = DateTime.Now;
            action.PersonId = Global.pers.PersonId;

            context.Actions.Add(action);
            context.SaveChanges();
            string xmldoc = Serialize(productes_list);
           

            string pfxPath = @"C:\Users\Adons_\Desktop\ceritif-cert.pfx";
            XmlDocument doc = new XmlDocument();
            X509Certificate2 cert = new X509Certificate2(System.IO.File.ReadAllBytes(pfxPath), "1234");
            doc.LoadXml(xmldoc);
            SignXmlDocumentWithCertificate(doc, cert);
            string out_xml = doc.OuterXml;
            //out_xml = out_xml.Insert(1005,"32145");
            string encrypted_xmldoc = Encryption(out_xml);
            return encrypted_xmldoc;
        }
        public string GetOrders()
        {
            Order prod = new Order();
            List<OrderExtended> orders_list = new List<OrderExtended>();

            var orders = context.Orders.Select(s => new { s.OrderId, s.ProductId, s.ClientId, s.Date, s.OrderStatus }).ToList();
            var ord = context.Orders.
                Join(
                context.Clients,
                Orders => Orders.ClientId,
                Clients => Clients.ClientId,
                (Orders, Clients) => new
                {
                    OrderId = Orders.ClientId,
                    ClientId = Clients.ClientId,
                    ClientName = Clients.ClientName,
                    OrderDate = Orders.Date,
                    ProductId=Orders.ProductId
                })
                .Join(context.Products,
                    Orders_Clients => Orders_Clients.ProductId,
                    Products =>Products.ProductId,
                    (Orders_Clients, Products) =>new
                    {
                        OrderId = Orders_Clients.OrderId,
                        ClientId = Orders_Clients.ClientId,
                        ProductId = Orders_Clients.ProductId,
                        ClientName = Orders_Clients.ClientName,
                        OrderDate = Orders_Clients.OrderDate,
                        ProductName = Products.ProductName,
                        Price=Products.Price
                    }
                ).ToList();


            // ActionId=2 =>вывод списка продуктов
            Models.Action action = new Models.Action();
            int peron = context.Actions
                  .Select(s => new { s.idAction, s.ActionId, s.ActionDescription, s.Date, s.PersonId }).ToList().Count;
            action.idAction = peron + 1;
            action.ActionId = 2;
            action.ActionDescription = "Пользователь " + Global.pers.Login + "вывел список заказов";
            action.Date = DateTime.Now;
            action.PersonId = Global.pers.PersonId;
            context.Actions.Add(action);
            context.SaveChanges();



            for (int i = 0; i < ord.Count/2; ++i)
            {
                orders_list.Add(new OrderExtended());
                orders_list[i].ProductId = ord[i].ProductId;
                orders_list[i].ClientId = ord[i].ClientId;
                orders_list[i].OrderId = ord[i].OrderId;
                orders_list[i].ClientName = ord[i].ClientName;
                orders_list[i].OrderDate = ord[i].OrderDate;
                orders_list[i].ProductName = ord[i].ProductName;
                orders_list[i].Price = ord[i].Price;
            }
            string xmldoc = Serialize(orders_list);

            string pfxPath = @"C:\Users\Adons_\Desktop\ceritif-cert.pfx";
            XmlDocument doc = new XmlDocument();
            X509Certificate2 cert = new X509Certificate2(System.IO.File.ReadAllBytes(pfxPath), "1234");
            doc.LoadXml(xmldoc);
            SignXmlDocumentWithCertificate(doc, cert);
            string out_xml = doc.OuterXml;
            //out_xml = out_xml.Insert(1005,"32145");
            string encrypted_xmldoc = Encryption(out_xml);
            return encrypted_xmldoc;
        }
        public string GetClients()
        {
            List<Client> client_list = new List<Client>();
            var clients = context.Clients.Select
                (
                    s => new {s.ClientId,s.ClientName,s.PhoneNumber}
                ).ToList();

            // ActionId=3 =>вывод списка клиентов
            Models.Action action = new Models.Action();
            int peron = context.Actions
                  .Select(s => new { s.idAction, s.ActionId, s.ActionDescription, s.Date, s.PersonId }).ToList().Count;
            action.idAction = peron + 1;
            action.ActionId = 3;
            action.ActionDescription = "Пользователь " + Global.pers.Login + "вывел список клиентов";
            action.Date = DateTime.Now;
            action.PersonId = Global.pers.PersonId;
            context.Actions.Add(action);
            context.SaveChanges();


            for (int i = 0; i < clients.Count; ++i)
            {
                client_list.Add(new Client());
                client_list[i].ClientId = clients[i].ClientId;
                client_list[i].ClientName = clients[i].ClientName;
                client_list[i].PhoneNumber = clients[i].PhoneNumber;
            }
            string xmldoc = Serialize(client_list);
            string pfxPath = @"C:\Users\Adons_\Desktop\ceritif-cert.pfx";
            XmlDocument doc = new XmlDocument();
            X509Certificate2 cert = new X509Certificate2(System.IO.File.ReadAllBytes(pfxPath), "1234");
            doc.LoadXml(xmldoc);
            SignXmlDocumentWithCertificate(doc, cert);
            string out_xml = doc.OuterXml;
            //out_xml = out_xml.Insert(1005,"32145");
            string encrypted_xmldoc = Encryption(out_xml);
            return encrypted_xmldoc;
        }
        public string GetWorkers()
        {
            List<Person> worker_list = new List<Person>();
            var workers = context.People.Select
                (
                    s => new { s.PersonId, s.Name, s.Login , s.Password, s.Status }
                ).ToList();

            // ActionId=4 =>вывод списка работников
            Models.Action action = new Models.Action();
            int peron = context.Actions
                  .Select(s => new { s.idAction, s.ActionId, s.ActionDescription, s.Date, s.PersonId }).ToList().Count;
            action.idAction = peron + 1;
            action.ActionId = 4;
            action.ActionDescription = "Пользователь " + Global.pers.Login + "вывел список работников";
            action.Date = DateTime.Now;
            action.PersonId = Global.pers.PersonId;
            context.Actions.Add(action);
            context.SaveChanges();


            for (int i = 0; i < workers.Count; ++i)
            {
                worker_list.Add(new Person());
                worker_list[i].PersonId = workers[i].PersonId;
                worker_list[i].Name = workers[i].Name;
                worker_list[i].Login = workers[i].Login;
                worker_list[i].Password = workers[i].Password;
                worker_list[i].Status = workers[i].Status;
            }
            string xmldoc = Serialize(worker_list);
            string pfxPath = @"C:\Users\Adons_\Desktop\ceritif-cert.pfx";
            XmlDocument doc = new XmlDocument();
            X509Certificate2 cert = new X509Certificate2(System.IO.File.ReadAllBytes(pfxPath), "1234");
            doc.LoadXml(xmldoc);
            SignXmlDocumentWithCertificate(doc, cert);
            string out_xml = doc.OuterXml;
            //out_xml = out_xml.Insert(1005,"32145");
            string encrypted_xmldoc = Encryption(out_xml);
            return encrypted_xmldoc;
        }
        public string GetActions()
        {
            List<Models.Action> actions_list = new List<Models.Action>();
            var actions = context.Actions.Select
                (
                    s => new { s.idAction, s.ActionId, s.ActionDescription, s.PersonId, s.Date }
                ).ToList();

            // ActionId=5 =>вывод списка действий
            Models.Action action = new Models.Action();
            int peron = context.Actions
                  .Select(s => new { s.idAction, s.ActionId, s.ActionDescription, s.Date, s.PersonId }).ToList().Count;
            action.idAction = peron + 1;
            action.ActionId = 5;
            action.ActionDescription = "Пользователь " + Global.pers.Login + "вывел список действий";
            action.Date = DateTime.Now;
            action.PersonId = Global.pers.PersonId;
            context.Actions.Add(action);
            context.SaveChanges();

            for (int i = 0; i < actions.Count; ++i)
            {
                actions_list.Add(new Models.Action());
                actions_list[i].idAction = actions[i].idAction;
                actions_list[i].ActionId = actions[i].ActionId;
                actions_list[i].ActionDescription = actions[i].ActionDescription;
                actions_list[i].PersonId = actions[i].PersonId;
                actions_list[i].Date = actions[i].Date;
            }
            string xmldoc = Serialize(actions_list);
            string pfxPath = @"C:\Users\Adons_\Desktop\ceritif-cert.pfx";
            XmlDocument doc = new XmlDocument();
            X509Certificate2 cert = new X509Certificate2(System.IO.File.ReadAllBytes(pfxPath), "1234");
            doc.LoadXml(xmldoc);
            SignXmlDocumentWithCertificate(doc, cert);
            string out_xml = doc.OuterXml;
            //out_xml = out_xml.Insert(1005,"32145");
            string encrypted_xmldoc = Encryption(out_xml);
            return encrypted_xmldoc;
        }
    }
}
