using Reddit.NET.Exceptions;
using Reddit.NET.Controllers.Internal;
using Reddit.NET.Controllers.Structures;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public abstract class BaseController : Validators
    {
        public Listings Listings;

        public BaseController()
        {
            Listings = new Listings();
        }
    }
}
