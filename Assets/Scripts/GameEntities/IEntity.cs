namespace GameEntities
{
    public interface IEntity
    {
        public void TakeHit(int dmgTaken);

        public void Died();
        public bool IsAlive();
    }
}