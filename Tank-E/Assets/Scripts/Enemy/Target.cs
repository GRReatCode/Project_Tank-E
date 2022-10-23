using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable, IBurnable //en estos script se llama la corutina startburning y stopburning del lanzallamas
{
    [SerializeField]
    private bool _IsBurning;
    public bool IsBurning { get => _IsBurning; set => _IsBurning = value; }//para el lanzallamas
    public float health = 100f;
    private Coroutine BurnCoroutine;
    private float _Health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage (float ammount)
    {
        health -= ammount;
        if (health <= 0f)
        {
            Die();
        }
    }

    public void StartBurning(int DamagePerSecond)
    {
        IsBurning = true;
        if (BurnCoroutine != null)
        {
            StopCoroutine(BurnCoroutine);
        }

        BurnCoroutine = StartCoroutine(Burn(DamagePerSecond));
    }
    private IEnumerator Burn(int DamagePerSecond)
    {
        float minTimeToDamage = 1f / DamagePerSecond;//aplica cantidad de daño por segundo
        WaitForSeconds wait = new WaitForSeconds(minTimeToDamage);
        int damagePerTick = Mathf.FloorToInt(minTimeToDamage) + 2;

        TakeDamage(damagePerTick);
        while (IsBurning)
        {
            yield return wait;
            TakeDamage(damagePerTick);
        }
    }
    public void StopBurning()
    {
        IsBurning = false;
        if (BurnCoroutine != null)
        {
            StopCoroutine(BurnCoroutine);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
