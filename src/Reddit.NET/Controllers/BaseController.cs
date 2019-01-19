using Reddit.Controllers.Internal;

namespace Reddit.Controllers
{
    public abstract class BaseController : Validators
    {
        public Lists Lists;

        public BaseController()
        {
            Lists = new Lists();
        }
    }
}
