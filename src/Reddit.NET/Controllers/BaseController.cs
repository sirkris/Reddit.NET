using Reddit.Controllers.Internal;

namespace Reddit.Controllers
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
