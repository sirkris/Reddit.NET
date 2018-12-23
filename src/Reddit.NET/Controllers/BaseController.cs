using Reddit.NET.Controllers.Internal;

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
