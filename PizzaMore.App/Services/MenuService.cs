using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaMore.App.BindingModels;
using PizzaMore.App.Data;
using PizzaMore.App.Models;
using PizzaMore.App.ViewModels;
using SimpleHttpServer.Models;

namespace PizzaMore.App.Services
{
    public class MenuService : Service
    {
        public MenuService(PizzaMoreContext context) : base(context)
        {
        }

        public void AddPizza(AddProductBindingModel model, User user)
        {
            var pizza = new Pizza()
            {
                Title = model.Title,
                Recipe = model.Recipe,
                ImageUrl = model.ImageUrl,
                User = user,
                UpVotes = 0,
                DownVotes = 0
            };

            this.context.Pizzas.Add(pizza);
            this.context.SaveChanges();
        }

        public PizzaViewModel GetAllPizzas(HttpSession session)
        {
            var viewModels = new List<PizzaViewModel>();

            var pizzas = this.context.Pizzas.ToList();
            var viewModel = new PizzaViewModel();
            viewModel.Email = this.context.Logins.First(x => x.sessionId == session.Id).User.Email;

            foreach (var pizza in pizzas)
            {    
                viewModel.Pizzas.Add(pizza);
            }

            return viewModel;
        }
    }
}
