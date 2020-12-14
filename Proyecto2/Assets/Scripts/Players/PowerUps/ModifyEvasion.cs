namespace Players.PowerUps
{
    public class ModifyEvasion:PowerUp
    {
        public override void Act()
        {
            GetComponent<PlayerHealth>().Evasion += 10;
            Destroy (this);
        }

        public override void Dest()
        {
            throw new System.NotImplementedException();
        }
    }
}