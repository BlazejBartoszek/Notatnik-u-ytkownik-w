using Microsoft.AspNetCore.Mvc;
using Notatnik_użytkowników.Models;
using Notatnik_użytkowników.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Notatnik_użytkowników.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPdfService _pdfService;

        public UserController(IUserRepository userRepository, IPdfService pdfService)
        {
            _userRepository = userRepository;
            _pdfService = pdfService;
        }

        // GET: User        
        public ActionResult Index()
        {
            return View(_userRepository.GetAllActive());
        }

        // GET: User/Create        
        public ActionResult Create()
        {
            return View(new UserModel());
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel userModel)
        {
            _userRepository.Add(userModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: User/Edit/5        
        public ActionResult Edit(int id)
        {
            return View(_userRepository.Get(id));
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserModel userModel)
        {
            _userRepository.Update(id, userModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: User/Delete/5        
        public ActionResult Delete(int id)
        {
            return View(_userRepository.Get(id));
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserModel userModel)
        {
            _userRepository.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult AddToRaportList(int id)
        {
            _userRepository.AddToRaportList(id);

            return RedirectToAction(nameof(Index));
        }
        
        public ActionResult Download()
        {
            var allUserList = _userRepository.GetAllActive();
            List<UserModel> userToPrintRaport = new List<UserModel>();
            userToPrintRaport.AddRange(allUserList.Where(x => x.UserRaport == true));

            if (userToPrintRaport.Count != 0)
            {
                using (var stream = new MemoryStream())
                {
                    _pdfService.GeneratePdf(stream, userToPrintRaport);
                    stream.Seek(0, SeekOrigin.Begin);
                    return File(stream.ToArray(), "application/pdf", $"{DateTime.Now}.pdf");
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }            
        }
    }
}