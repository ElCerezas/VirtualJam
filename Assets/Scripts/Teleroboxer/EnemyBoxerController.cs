using System.Collections;
using UnityEngine;

public class EnemyBoxerController : MonoBehaviour
{
    private EnemyHandController leftHand, rightHand;
    [SerializeField] private float actionCooldown = 2.0f; // Tiempo base entre acciones

    private void Start()
    {
        leftHand = GetComponent<BoxerStats>().leftHand.GetComponent<EnemyHandController>();
        rightHand = GetComponent<BoxerStats>().rightHand.GetComponent<EnemyHandController>();

        //Cada mano tiene su propia IA independiente
        StartCoroutine(EnemyAI(leftHand));
        StartCoroutine(EnemyAI(rightHand));
    }

    private IEnumerator EnemyAI(EnemyHandController hand)
    {
        while (true)
        {
            //Tiempo de espera independiente para cada mano
            yield return new WaitForSeconds(Random.Range(actionCooldown - 0.2f, actionCooldown + 0.2f));

            int action = Random.Range(0, 2);  // Acción aleatoria (0 = golpe, 1 = bloqueo)
            ExecuteAction(hand, action);
        }
    }

    private void ExecuteAction(EnemyHandController hand, int action)
    {
        switch (action)
        {
            case 0:
                hand.StartChargeAndHit();
                break;
            case 1:
                hand.StartBlock();
                break;
            default:
                // No hacer nada
                break;
        }
    }
}
