namespace Players
{
    public class LeaveBomb:Behavior
    {
        public override void Act()
        {
            controller.GenerateBomb();
        }
    }
}