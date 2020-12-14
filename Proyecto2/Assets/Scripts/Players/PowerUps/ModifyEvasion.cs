namespace Players.PowerUps
{
    public class ModifyEvasion:PowerUp
    {
        public override void Act()
        {
            if(CompareTag("consumable")) return;
            GetComponent<PlayerHealth>().Evasion += 10;
            Destroy (this);
        }

        public override void Dest()
        {
            throw new System.NotImplementedException();
        }
    }
}