using Reddit.Controllers.Internal;

namespace Reddit.Controllers
{
    public abstract class BaseController : Validators
    {
        public Lists Lists { get; set; }

        public BaseController()
        {
            Lists = new Lists();
        }
    }
}
