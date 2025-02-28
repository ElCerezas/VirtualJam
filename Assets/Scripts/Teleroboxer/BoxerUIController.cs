using UnityEngine;
using UnityEngine.UI;

public class BoxerUIController : MonoBehaviour
{
    [SerializeField] BoxerStats playerStats, enemyStats;
    [SerializeField] Slider playerLifebar, enemyLifebar;
    [SerializeField] AudioSource PlayerHit, EnemyHit, Blocked;

    [SerializeField] int damage;
    BoxingDamageManager FXManager;
    private void Awake()
    {
        FXManager = GetComponent<BoxingDamageManager>();
    }
    public void OnHit(bool blocked, bool isPlayerHit)
    {
        if (!blocked)
        {
            //Cuando se da un golpe
            if (isPlayerHit)
            {
                playerStats.health -= damage;
                playerLifebar.value = playerStats.health;
                FXManager.OnDamageRecived();
                PlayerHit.Play();
            }
            else
            {
                enemyStats.health -= damage;
                enemyLifebar.value = enemyStats.health;
                FXManager.OnJustShake(0.2f, 0.4f);
                EnemyHit.Play();
            }
        }
        else
        {
            if(!Blocked.isPlaying)
            {
                Blocked.Play();
            }
            Debug.Log("Block Shake");
            FXManager.OnJustShake(0.2f, 0.2f);
        }

        if (playerStats.health <= 0)
        {
            GameManager.GameWin();
        }
        if (enemyStats.health <= 0)
        {
            GameManager.GameLose();
        }
    }

}
