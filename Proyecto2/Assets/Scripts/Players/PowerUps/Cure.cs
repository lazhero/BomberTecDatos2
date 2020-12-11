namespace Players.PowerUps
{
    public class Cure:PowerUp
    {
        public override void Act()
        {
            if(CompareTag("consumable")) return;
            GetComponent<PlayerHealth>().ModifyStats(1,0,0);
            Destroy (this);
        }
    }
}