namespace Players.PowerUps
{
    public class Shield:PowerUp
    {
        private PlayerHealth comp;
        public override void Act()
        {
            if(CompareTag("consumable")) return;

            comp=GetComponent<PlayerHealth>();
            comp._canReceiveDamage = false;
            Invoke("dest",5);
            
        }
        public void dest()
        {
            comp._canReceiveDamage = true;
            Destroy (this);
        }
    }
}