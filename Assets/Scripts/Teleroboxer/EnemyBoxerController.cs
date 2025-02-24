using System.Collections;
using UnityEngine;

public class EnemyBoxerController : MonoBehaviour
{
    private EnemyHandController leftHand, rightHand;
    [SerializeField] private float actionCooldown = 2.0f; // Tiempo entre acciones

    private void Start()
    {
        leftHand = GetComponent<BoxerStats>().leftHand.GetComponent<EnemyHandController>();
        rightHand = GetComponent<BoxerStats>().rightHand.GetComponent<EnemyHandController>();

        StartCoroutine(EnemyAI());
    }

    private IEnumerator EnemyAI()
    {
        while (true)
        {
            yield return new WaitForSeconds(actionCooldown);
            int actionLeft = Random.Range(0, 3);  // Acción de la mano izquierda
            int actionRight = Random.Range(0, 3); // Acción de la mano derecha

            ExecuteAction(leftHand, actionLeft);
            ExecuteAction(rightHand, actionRight);
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
            case 2:
                // No hacer nada (reposo)
                break;
        }
    }
}
