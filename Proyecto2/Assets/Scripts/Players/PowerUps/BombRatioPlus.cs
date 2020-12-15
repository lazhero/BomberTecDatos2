namespace Players.PowerUps
{
    public class BombRatioPlus:PowerUp
    {
        public override void Act()
        {
            if(CompareTag("consumable")) return;

            GetComponent<PlayerHealth>().ModifyStats(0,1,0);
            Destroy (this);
        }

        public override void Dest()
        {
            throw new System.NotImplementedException();
        }
    }
}