﻿using System.Collections.Generic;
using EC.BusinessLogic.Services.Interfaces;
using EC.Entities.Entities;
using System.Web.Mvc;

namespace EC.Web.Controllers
{
    public class PhoneController : Controller
    {
        private readonly IPhoneService _phoneService;
        private readonly IUserService _userService;

        public PhoneController(IPhoneService phoneService, IUserService userService)
        {
            _phoneService = phoneService;
            _userService = userService;
        }

        [HttpPost]
        public ActionResult AddPhone(Phone phone)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserByLogin(User.Identity.Name);

                phone.UserId = user.Id;

                _phoneService.CreatePhone(phone);

                return RedirectToAction("EditUserContacts", new { login = User.Identity.Name });
            }

            return PartialView("CreatePhone");
        }

        [HttpPost]
        public ActionResult UpdatePhone(Phone phone)
        {
            if (ModelState.IsValid)
            {
                _phoneService.UpdatePhone(phone);

                return RedirectToAction("EditUserContacts", new { login = User.Identity.Name });
            }

            return PartialView(phone);
        }

        [HttpPost]
        public ActionResult DeletePhone(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            _phoneService.DeletePhone(id);

            return RedirectToAction("EditUserContacts", new { login = User.Identity.Name });
        }

        [HttpGet]
        public ActionResult UserPhones(string login)
        {
            var userPhones = _phoneService.GetUserContacts(login);

            if (userPhones == null)
            {
                return View("NotFound");
            }

            return View(userPhones);
        }

        [HttpGet]
        public ActionResult EditUserContacts(string login)
        {
            var phones = _phoneService.GetUserContacts(login);

            return View(phones);

        }

        [HttpPost]
        public ActionResult EditUserContacts(ICollection<Phone> phones)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in phones)
                {
                    _phoneService.UpdatePhone(item);
                }
            }

            return View(phones);
        }
    }
}