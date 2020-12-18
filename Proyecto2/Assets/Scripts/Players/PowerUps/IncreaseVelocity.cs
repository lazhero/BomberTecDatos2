namespace Players.PowerUps
{
    public class IncreaseVelocity:PowerUp
    {
        public override void Act()
        {
            GetComponent<PlayerHealth>().Velocity += 1;
            Destroy (this);
        }

        public override void Dest()
        {
            throw new System.NotImplementedException();
        }
        
    }
    
}