public interface IHealth
{
    event System.Action<float> OnHPPctChanged;
    event System.Action OnDied;
    void TakeDamage(int dmg);
}