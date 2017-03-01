﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMore.App.BindingModels;
using PizzaMore.App.Services;
using PizzaMore.App.Utillities;
using PizzaMore.App.ViewModels;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace PizzaMore.App.Controllers
{
    public class MenuController : Controller
    {
        [HttpGet]
        public IActionResult<PizzaViewModel> Index(HttpSession session, HttpResponse response)
        {
            var user = AuthenticationManager.IsAuthenticated(session);
            if (user == null)
            {
                this.Redirect(response, "/users/signin");
                return null;
            }

            var service = new MenuService(Data.Data.Context);

            var viewModel = service.GetAllPizzas(session);

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Add(HttpSession session, HttpResponse response)
        {
            var user = AuthenticationManager.IsAuthenticated(session);
            if (user == null)
            {
                this.Redirect(response, "/users/signin");
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(HttpSession session, AddProductBindingModel model,
            HttpResponse response)
        {
            var user = AuthenticationManager.IsAuthenticated(session);

            var service = new MenuService(Data.Data.Context);

            service.AddPizza(model, user);

            this.Redirect(response, "/menu/index");
            return null;
        }
    }
}
