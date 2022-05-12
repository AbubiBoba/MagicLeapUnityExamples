public interface IShooting
{
    public void OnGunTriggerPressed(byte controllerId, float value);
    public void ShootParticles();
    public void ShootSleeve();
    public void GiveDamage();
    //private void TryToShoot();
}
