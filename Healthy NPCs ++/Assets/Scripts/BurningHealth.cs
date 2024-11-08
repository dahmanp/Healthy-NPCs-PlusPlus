using System;
using UnityEngine;
using System.Collections;

public class BurningHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int startingHealth = 100;
    public int duration = 4;
    private int index = 0;
    public ParticleSystem fire;

    private int currentHealth;

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public float CurrentHpPct
    {
        get { return (float)currentHealth / (float)startingHealth; }
    }

    public void TakeDamage(int dmg)
    {
        if (dmg <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + dmg);

        fire.Play();
        Burning(dmg);

        if (CurrentHpPct <= 0)
            Die();
    }

    private void Burning(int dmg)
    {
        StartCoroutine(BurnTheNPC(dmg));
    }

    IEnumerator BurnTheNPC(int dmg)
    {
        for (index = 0; index <= duration; index++)
        {
            currentHealth -= dmg;
            OnHPPctChanged(CurrentHpPct);
            Debug.Log(currentHealth);
            Debug.Log("increment " + index);
            yield return StartCoroutine(Wait());
        }
        fire.Stop();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void Die()
    {
        OnDied();
        GameObject.Destroy(this.gameObject);
    }
}