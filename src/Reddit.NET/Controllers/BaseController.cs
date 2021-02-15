using Reddit.Controllers.Internal;

namespace Reddit.Controllers
{
    /// <summary>
    /// The base Controller.
    /// </summary>
    public abstract class BaseController : Validators
    {
        /// <summary>
        /// List-handling.
        /// </summary>
        public Lists Lists { get; set; }

        /// <summary>
        /// Create a new Controller instance.
        /// </summary>
        public BaseController()
        {
            Lists = new Lists();
        }
    }
}
