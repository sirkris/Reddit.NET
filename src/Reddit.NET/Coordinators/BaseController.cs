using Reddit.Coordinators.Internal;

namespace Reddit.Coordinators
{
    public abstract class BaseCoordinator : Validators
    {
        public Lists Lists;

        public BaseCoordinator()
        {
            Lists = new Lists();
        }
    }
}
