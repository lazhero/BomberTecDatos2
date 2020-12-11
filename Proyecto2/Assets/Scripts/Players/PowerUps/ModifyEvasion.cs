namespace Players.PowerUps
{
    public class ModifyEvasion:PowerUp
    {
        public override void Act()
        {
            if(CompareTag("consumable")) return;
            GetComponent<PlayerHealth>().ModifyStats(0,0,1);
            Destroy (this);
        }
    }
}