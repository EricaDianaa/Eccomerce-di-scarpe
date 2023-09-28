using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using w14_3.Models;

namespace w14_3.Controllers
{
  
    public class HomeController : Controller
    {   
        string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
           .ConnectionString.ToString();
           
        public ActionResult Index()
        {
            Prodotto.SelectProdotti();
            return View(Prodotto.ListProdotto);
        }

        public ActionResult Dettagli() 
        {
          Prodotto.SelectWhereId();

            return View(Prodotto.listDettaglio);
        }

        public ActionResult Admin()
        {
        
            Prodotto.SelectProdotti();
            return View(Prodotto.ListProdotto);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Prodotto p, HttpPostedFileBase ImgeBox, HttpPostedFileBase Image1, HttpPostedFileBase Image2)
        {
            if (ModelState.IsValid)
            {
                string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connection);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Prodotto VALUES(@Nome,@Prezzo,@Descrizione,@ImageBox,@Image1,@Image2,@Vetrina)";
                    cmd.Parameters.AddWithValue("Nome", p.NomeProdotto);
                    cmd.Parameters.AddWithValue("Prezzo", p.Prezzo);
                    cmd.Parameters.AddWithValue("Descrizione", p.Descrizione);
                
                 
                    cmd.Parameters.AddWithValue("Vetrina", p.Vetrina);

                if (ImgeBox.ContentLength > 0)
                {
                    string nameFile1 = ImgeBox.FileName;  
                  
                    string path=Path.Combine(Server.MapPath("~/Content/img"),nameFile1);
                    ImgeBox.SaveAs(path); 
                    cmd.Parameters.AddWithValue("ImageBox", nameFile1);
                }
                if (Image1.ContentLength > 0)
                {
                    string nameFile2 = Image1.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/img"), nameFile2);
                    Image1.SaveAs(path);
                        cmd.Parameters.AddWithValue("Image1", nameFile2);
                }
                if (Image2.ContentLength > 0)
                {
                    string nameFile3 = Image2.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/img"), nameFile3);
                    Image2.SaveAs(path);   
                    cmd.Parameters.AddWithValue("Image2", nameFile3);
                }
                    int inserimentoEffettuato = cmd.ExecuteNonQuery();

                    if (inserimentoEffettuato > 0)
                    {
                       ViewBag.Messaggio  = "Inserimento effetuato con successo";
                    }

                }
                catch (Exception ex) { ViewBag.Messaggio = $"{ex}"; }
                finally { conn.Close(); }
             //   Prodotto.Insert(p, );
            }

            return Redirect("index");
        }
        [HttpGet]
        public ActionResult Delete(int idProd) 
        {

            foreach (Prodotto item in Prodotto.ListProdotto)
            {
                int idProdo =Convert.ToInt32(Request.QueryString["IdProd"]) ;
                Prodotto.SelectWhereId();
                if (item.IdProdotto == idProd)
                {

                  //  ToReturn = item;
                    Prodotto.Elimina(); 
                   
                }
            }

            return Redirect("index");
        }

        public ActionResult Edit(int idProd)
        {
            Prodotto prod = new Prodotto();
            foreach (Prodotto item in Prodotto.ListProdotto)
            {
               
                Prodotto.SelectWhereId();
                if (item.IdProdotto == idProd)
                {

                    prod = item;
                    break;
                  //  Prodotto.Modifica();

                }
               
            }
            return View(prod);
        }

        [HttpPost]
        public ActionResult Edit(Prodotto p, HttpPostedFileBase ImgeBox, HttpPostedFileBase Image1, HttpPostedFileBase Image2)
        {
            Prodotto prod = new Prodotto();
            
            foreach (Prodotto item in Prodotto.ListProdotto)
            {
             //   Prodotto.SelectWhereId();
                if (p.IdProdotto == item.IdProdotto)
                {
                    item.IdProdotto=p.IdProdotto;
                    item.NomeProdotto = p.NomeProdotto;
                    item.Prezzo = p.Prezzo;
                    item.Descrizione = p.Descrizione;
                    item.ImgeBox=p.ImgeBox;
                    item.Image1 = p.Image1;
                    item.Image2 = p.Image2;
                    item.Vetrina = p.Vetrina;
                 

                    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
                    SqlConnection conn2 = new SqlConnection(connectionString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.Connection = conn2;
                    cmd2.CommandText = "UPDATE Prodotto SET NomeArticolo=@nome,Prezzo =@prezzo, Descrizione=@Descrizone, ImageBox=@Imagebox,Image1=@image1,Image2=@Image2, Vetrina=@Vetrina where IdScarpa=@id";
                    cmd2.Parameters.AddWithValue("nome", p.NomeProdotto);
                    cmd2.Parameters.AddWithValue("prezzo", p.Prezzo);
                    cmd2.Parameters.AddWithValue("Descrizone", p.Descrizione);
                    cmd2.Parameters.AddWithValue("Vetrina", p.Vetrina);
                    cmd2.Parameters.AddWithValue("id",p.IdProdotto);
                    if (ImgeBox.ContentLength > 0)
                    {
                        string nameFile1 = ImgeBox.FileName;

                        string path = Path.Combine(Server.MapPath("~/Content/img"), nameFile1);
                        ImgeBox.SaveAs(path);
                        cmd2.Parameters.AddWithValue("ImageBox", nameFile1);
                    }
                    else if(ImgeBox.ContentLength < 0)
                    {
                        cmd2.Parameters.AddWithValue("ImageBox", item.ImgeBox);
                    }
                    if (Image1.ContentLength > 0)
                    {
                        string nameFile2 = Image1.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/img"), nameFile2);
                        Image1.SaveAs(path);
                        cmd2.Parameters.AddWithValue("Image1", nameFile2);
                    }
                    else if (Image1.ContentLength < 0)
                    {
                        cmd2.Parameters.AddWithValue("ImageBox", item.Image1);
                    }
                    if (Image2.ContentLength > 0)
                    {
                        string nameFile3 = Image2.FileName;
                        string path = Path.Combine(Server.MapPath("~/Content/img"), nameFile3);
                        Image2.SaveAs(path);
                        cmd2.Parameters.AddWithValue("Image2", nameFile3);
                    }
                    else if (Image2.ContentLength < 0)
                    {
                        cmd2.Parameters.AddWithValue("ImageBox", item.Image2);
                    }

                    conn2.Open();

                    cmd2.ExecuteNonQuery();

                    conn2.Close();

                  //  Prodotto.Modifica(p);

                }

            }
            return Redirect("index");
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Utente u)
        {
            FormsAuthentication.SetAuthCookie(u.Username, false);
            return RedirectToAction("Index", "Home");
        }
    }
}