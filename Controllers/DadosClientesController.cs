using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CadastroTempus.Data;
using CadastroTempus.Models;

namespace CadastroTempus.Controllers
{
    public class DadosClientesController : Controller
    {
        private CadastroTempusContext db = new CadastroTempusContext();
        SqlConnection conn = null;
        SqlDataReader reader = null;
        // GET: dadosClientes
        public ActionResult Index()
        {
           
            return View(db.dadosClientes.ToList());
        }

        // GET: dadosClientes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dadosCliente dadosCliente = db.dadosClientes.Find(id);
            if (dadosCliente == null)
            {
                return HttpNotFound();
            }
            return View(dadosCliente);
        }

        // GET: dadosClientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: dadosClientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TxtCpf,Nome,DtaNascimento,Renda")] dadosCliente dadosCliente)
        {
            if (ModelState.IsValid)
            {
                try {
                    dadosCliente.DtaCadastro = DateTime.Now;
                    db.dadosClientes.Add(dadosCliente);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                    
                }
                catch
                {
                   
                    throw new NotImplementedException("CPF JÁ CADASTRADO");
                }
            }
           
            return View(dadosCliente);
        }

        // GET: dadosClientes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dadosCliente dadosCliente = db.dadosClientes.Find(id);
            if (dadosCliente == null)
            {
                return HttpNotFound();
            }
            return View(dadosCliente);
        }

        //public ActionResult Filtro(string Nome)
        //{
        //    if (Nome == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    dadosCliente dadosCliente = db.dadosClientes.Find(Nome);
            
            
        //    return View(dadosCliente);
        //}

        // POST: dadosClientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TxtCpf,Nome,DtaNascimento,DtaCadastro,Renda")] dadosCliente dadosCliente)
        {
            if (ModelState.IsValid)
            {
                dadosCliente.DtaCadastro = DateTime.Now;
                db.Entry(dadosCliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dadosCliente);
        }

        // GET: dadosClientes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dadosCliente dadosCliente = db.dadosClientes.Find(id);
            if (dadosCliente == null)
            {
                return HttpNotFound();
            }
            return View(dadosCliente);
        }

        // POST: dadosClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            dadosCliente dadosCliente = db.dadosClientes.Find(id);
            db.dadosClientes.Remove(dadosCliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //[HttpGet]
        //public ActionResult Relatorio(string id)

        //{
        //    try
        //    {
        //        string consulta = "";
        //        if (id == "Hoje")
        //        {
        //            consulta = "SELECT (SELECT COUNT(*) FROM dadosClientes WHERE Renda <= 980 AND DAY(NOW())-DAY(DtaCadastro) = 0 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS 'Classe A'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE Renda >= 980.01 AND Renda <= 2500 AND DAY(NOW())-DAY(DtaCadastro) = 0 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS 'Classe B'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE Renda > 2500 AND DAY(NOW())-DAY(DtaCadastro) = 0 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS 'Classe C'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE TIMESTAMPDIFF(YEAR, DtaNascimento, CURDATE()) >= 18 AND Renda > (SELECT AVG(Renda) FROM dadosClientes) AND DAY(NOW())-DAY(DtaCadastro) = 0 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS '18Media'";
        //        }
        //        else if (id == "Mes")
        //        {
        //            consulta = "SELECT (SELECT COUNT(*) FROM dadosClientes WHERE Renda <= 980 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS 'Classe A'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE Renda >= 980.01 AND Renda <= 2500 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS 'Classe B'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE Renda > 2500 AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS 'Classe C'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE TIMESTAMPDIFF(YEAR, DtaNascimento, CURDATE()) >= 18 AND Renda > (SELECT AVG(Renda) FROM dadosClientes) AND MONTH(NOW())-MONTH(DtaCadastro) = 0 AND YEAR(NOW())-YEAR(DtaCadastro) = 0) AS '18Media'";
        //        }
        //        else if (id == "Semana")
        //        {
        //            consulta = "SELECT (SELECT COUNT(*) FROM dadosClientes WHERE Renda <= 980 AND YEARWEEK(DtaCadastro) = YEARWEEK(NOW())) AS 'Classe A'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE Renda >= 980.01 AND Renda <= 2500 AND YEARWEEK(DtaCadastro) = YEARWEEK(NOW())) AS 'Classe B'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE Renda > 2500 AND YEARWEEK(DtaCadastro) = YEARWEEK(NOW())) AS 'Classe C'," +
        //                "(SELECT COUNT(*) FROM dadosClientes WHERE TIMESTAMPDIFF(YEAR, DtaNascimento, CURDATE()) >= 18 AND Renda > (SELECT AVG(Renda) FROM dadosClientes) AND YEARWEEK(DtaCadastro) = YEARWEEK(NOW())) AS '18Media'";
        //        }
        //        conn = new
        //        SqlConnection("Server=(localdb)\\mssqllocaldb;Database=CU-1;Trusted_Connection=True;MultipleActiveResultSets=true");
                
        //        SqlCommand cmd = new SqlCommand(consulta, conn);
        //        conn.Open();
        //        reader = cmd.ExecuteReader();
                
        //        if (reader.Read())
        //        {
        //                   id = reader["Classe A"].ToString();
        //                  id = reader["Classe B"].ToString();
        //                   id = reader["Classe C"].ToString();
        //                   id = reader["18Media"].ToString();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }



        //    return View(id);
        //}

        }
        }

