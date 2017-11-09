using System;
using InMemD8.Data;
using InMemD8.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace XUnitPizzieriaTest
{

    public class XUnitPizzieriaTest
    {
        private readonly IServiceProvider _service;

        [Fact]
        public void SortedIngrediant()
        {
            var ingredient = _service.GetService<IngredientService>();
            var ingredients = ingredient.GetIngredients();
            Assert.Empty(ingredients);
        }

        public XUnitPizzieriaTest()
        {
            var efService = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase("Db").UseInternalServiceProvider(efService));
            serviceCollection.AddTransient<IngredientService>();

            _service = serviceCollection.BuildServiceProvider();

        }
    }
}

