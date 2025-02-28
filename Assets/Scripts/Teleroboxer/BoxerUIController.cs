using UnityEngine;
using UnityEngine.UI;

public class BoxerUIController : MonoBehaviour
{
    [SerializeField] BoxerStats playerStats, enemyStats;
    [SerializeField] Slider playerLifebar, enemyLifebar;
    [SerializeField] AudioSource PlayerHit, EnemyHit, Blocked;

    [SerializeField] int damage;
    public void OnHit(bool blocked, bool isPlayerHit)
    {
        if (!blocked)
        {
            //Cuando se da un golpe
            if (isPlayerHit)
            {
                playerStats.health -= damage;
                playerLifebar.value = playerStats.health;
                PlayerHit.Play();
            }
            else
            {
                enemyStats.health -= damage;
                enemyLifebar.value = enemyStats.health;
                EnemyHit.Play();
            }
        }
        else
        {
            if(!Blocked.isPlaying)
            {
                Blocked.Play();
            }
        }

        if (playerStats.health < 0)
        {
            GameManager.GameWin();
        }
        if (enemyStats.health < 0)
        {
            GameManager.GameLose();
        }
    }

}
