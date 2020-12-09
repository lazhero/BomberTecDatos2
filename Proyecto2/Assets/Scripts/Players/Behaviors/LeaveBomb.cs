using Players.Behaviors;

namespace Players
{
    public class LeaveBomb:AiBehavior
    {
        public override void Act()
        {
            controller.GenerateBomb();
            Invoke("RunAway",0.5f);
        }

        private void RunAway()
        {
            gameObject.GetComponent<Hide>().Act();;
        }
    }
}