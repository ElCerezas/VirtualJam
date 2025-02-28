using UnityEngine;
using UnityEngine.UI;

public class BoxerUIController : MonoBehaviour
{
    [SerializeField] BoxerStats playerStats, enemyStats;
    [SerializeField] Slider playerLifebar, enemyLifebar;

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
            }
            else
            {
                enemyStats.health -= damage;
                enemyLifebar.value = enemyStats.health;
            }
        }
    }

}
