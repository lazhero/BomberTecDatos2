using Players.Behaviors;

namespace Players
{
    public class LeaveBomb:AiBehavior
    {
        public override void Act()
        {
            controller.GenerateBomb();
        }
    }
}