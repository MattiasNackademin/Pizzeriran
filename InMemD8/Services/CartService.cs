using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InMemD8.Data;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;

namespace InMemD8.Services
{

    public class CartService
    {
        private IHttpContextAccessor _accessor;

        public CartService(IHttpContextAccessor accessor, ApplicationDbContext context)
        {
            _accessor = accessor;
        }

       

    }
}
