namespace Players.PowerUps
{
    public class Cure:PowerUp
    {
        public override void Act()
        {
            GetComponent<PlayerHealth>().ModifyStats(1,0,0);
            Destroy (this);
        }

        public override void Dest()
        {
            throw new System.NotImplementedException();
        }
    }
}