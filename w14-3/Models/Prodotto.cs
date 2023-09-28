using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;

namespace w14_3.Models
{
    public class Prodotto
    {

        public int IdProdotto { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string NomeProdotto { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public decimal Prezzo { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Descrizione { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string ImgeBox { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Image1 { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public string Image2 { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        public bool Vetrina { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]

        public static List<Prodotto> ListProdotto = new List<Prodotto>();
        public static List<Prodotto> listDettaglio = new List<Prodotto>();
        public Prodotto() { }
        public Prodotto(string nome, decimal prezzo, string descrizione, string imagebox, string image1, string image2, bool vetrina, int id)
        {
            NomeProdotto = nome;
            Prezzo = prezzo;
            Descrizione = descrizione;
            ImgeBox = imagebox;
            Image1 = image1;
            Image2 = image2;
            Vetrina = vetrina;
            IdProdotto = id;

        }

        public static void SelectProdotti()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
          .ConnectionString.ToString();
            Prodotto.ListProdotto.Clear();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("select * from Prodotto ", conn);

            SqlDataReader sqlreader;
            conn.Open();

            sqlreader = cmd.ExecuteReader();


            while (sqlreader.Read())
            {

                Prodotto prod = new Prodotto();
                prod.NomeProdotto = sqlreader["NomeArticolo"].ToString();
                prod.Prezzo = Convert.ToInt32(sqlreader["Prezzo"]);
                prod.Descrizione = sqlreader["Descrizione"].ToString();
                prod.ImgeBox = sqlreader["ImageBox"].ToString();
                prod.Image1 = sqlreader["Image1"].ToString();
                prod.Image2 = sqlreader["Image2"].ToString();
                prod.Vetrina = Convert.ToBoolean(sqlreader["Vetrina"]);
                prod.IdProdotto = Convert.ToInt32(sqlreader["IdScarpa"]);
                Prodotto.ListProdotto.Add(prod);
            }
        }
        public static void SelectWhereId()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
         .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd1 = new SqlCommand("select * from Prodotto where IdScarpa=@id", conn);
            SqlDataReader sqlreader1;
            conn.Open();

            cmd1.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["IdProd"]);
            sqlreader1 = cmd1.ExecuteReader();
           
            while (sqlreader1.Read())
            {
                Prodotto.listDettaglio.Clear();
                Prodotto p = new Prodotto();
                p.NomeProdotto = sqlreader1["NomeArticolo"].ToString();
                p.Descrizione = sqlreader1["Descrizione"].ToString();
                p.Prezzo = Convert.ToDecimal(sqlreader1["Prezzo"].ToString());
                p.ImgeBox = sqlreader1["ImageBox"].ToString();
                p.Image1 = sqlreader1["Image1"].ToString();
                p.Image2 = sqlreader1["Image2"].ToString();
                p.Vetrina = Convert.ToBoolean(sqlreader1["Vetrina"]);
                Prodotto.listDettaglio.Add(p);

            }
        }
        public static void Insert(Prodotto p, string messaggio)
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
                cmd.Parameters.AddWithValue("ImageBox", p.ImgeBox);
                cmd.Parameters.AddWithValue("Image1", p.Image1);
                cmd.Parameters.AddWithValue("Image2", p.Image2);
                cmd.Parameters.AddWithValue("Vetrina", p.Vetrina);

             
                int inserimentoEffettuato = cmd.ExecuteNonQuery();

                if (inserimentoEffettuato > 0)
                {
                    messaggio = "Inserimento effetuato con successo";
                }

            }
            catch (Exception ex) { messaggio = $"{ex}"; }
            finally { conn.Close(); }
        }
        public static void SelectModifica(Prodotto p)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["IdProd"]))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
                SqlConnection conn2 = new SqlConnection(connectionString);
                SqlCommand cmd2 = new SqlCommand("select * from Prodotto WHERE IdScarpa=@id", conn2);
                cmd2.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["IdProd"]);
                conn2.Open();
                SqlDataReader sqlreader;
                sqlreader = cmd2.ExecuteReader();
                while (sqlreader.Read())
                {
                    p.NomeProdotto = sqlreader["NomeArticolo"].ToString();
                    p.Descrizione = sqlreader["Descrizione"].ToString();
                    p.Prezzo = Convert.ToDecimal(sqlreader["Prezzo"].ToString());
                    p.ImgeBox = sqlreader["ImageBox"].ToString();
                    p.Image1 = sqlreader["Image1"].ToString();
                    p.Image2 = sqlreader["Image2"].ToString();
                    p.Vetrina = Convert.ToBoolean(sqlreader["Vetrina"]);

                }
            }
        }
        public static void Modifica(Prodotto p)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionDB"].ConnectionString.ToString();
            SqlConnection conn2 = new SqlConnection(connectionString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = conn2;
            cmd2.CommandText = "UPDATE Prodotto SET NomeArticolo=@nome,Prezzo =@prezzo, Descrizione=@Descrizone, ImageBox=@Imagebox,Image1=@image1,Image2=@Image2, Vetrina=@Vetrina where IdScarpa=@id";
            cmd2.Parameters.AddWithValue("nome", p.NomeProdotto);
            cmd2.Parameters.AddWithValue("prezzo", p.Prezzo);
            cmd2.Parameters.AddWithValue("Descrizone", p.Descrizione);
            cmd2.Parameters.AddWithValue("ImageBox", p.ImgeBox);
            cmd2.Parameters.AddWithValue("Image1", p.Image1);
            cmd2.Parameters.AddWithValue("Image2", p.Image2);
            cmd2.Parameters.AddWithValue("Vetrina", p.Vetrina);
            cmd2.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["IdProd"]);


            conn2.Open();

            cmd2.ExecuteNonQuery();

            conn2.Close();
        }
        public static void Elimina()
        {
            string connection = ConfigurationManager.ConnectionStrings["ConnectionDB"]
            .ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM Prodotto where IdScarpa=@id";
            cmd.Parameters.AddWithValue("id", HttpContext.Current.Request.QueryString["IdProd"]);

            conn.Open();

            cmd.ExecuteNonQuery();

            conn.Close();
        }

    
    }
}