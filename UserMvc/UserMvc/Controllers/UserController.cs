using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using UserMvc.Models;
using UserMvc.Repository;
using Aes = System.Security.Cryptography.Aes;

namespace UserMvc.Controllers
{
    
    public class UserController : Controller
    {
        private IGenericRepository<User> _context;
        private readonly AppDbContext _db;
       

        public UserController(IGenericRepository<User> context, AppDbContext db)
        {
            _context = context;
            _db = db;
        }

        [HttpGet]
 
        public IActionResult Index()
        {
            try
            {
                var users = _context.GetAll();
                return View(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        [HttpGet]
       
        public IActionResult Edit(int Id)
        {
            try
            {
                ViewBag.Date = DateTime.Now;
                var user = _db.users.Find(Id);
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            try
            {
                _context.Edit(user);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel user)
        {
            try
            {
                var log = _db.users.Where(x => x.UserName.Equals(user.UserName) && x.Password.Equals(user.Password));
                if (log.Any())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["Err"] = "invalid user name/password";
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            try
            {
                //ViewBag.Date = DateTime.Now;
                var user = new User();
                user.CreationDate = DateTime.Now;
                return View(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(User user)
        {
            //ViewBag.Encrypt = Encrypt(user.Password); 
            //ViewBag.RfcEncrypt-Encrypt.EncryptPassword(Password);
            //ViewBag.Password = user.Password;
            //Encrypt(user.Password);
            try
            {
                _context.Add(user);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }
        //private string Encrypt(string clearText)
        //{
        //    string encryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }

        //    return clearText;
        //}

        //private string Decrypt(string cipherText)
        //{
        //    string encryptionKey = "MAKV2SPBNI99212";
        //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //    using (Aes encryptor = Aes.Create())
        //    {
        //        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //        encryptor.Key = pdb.GetBytes(32);
        //        encryptor.IV = pdb.GetBytes(16);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                cs.Close();
        //            }
        //            cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //        }
        //    }

        //    return cipherText;
        //}
    }
}
