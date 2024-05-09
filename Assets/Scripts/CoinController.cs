using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Transform PlayerTransform;
    private GameManager gm;
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        if (gm == null)  // Je�eli ustawienia nie s� podgrane
        {
            gm = GameManager.settings;
        }
        if (gm.magnet.isActive != false) // Je�eli nie jest aktywne 
        {
            return; // To wychodzimy
        }
        else // A jak jest aktywne
        {
            if (Vector3.Distance(transform.position, PlayerTransform.position) 
                < gm.magnet.GetRange())
            {
                // Normalizacja wektora ( w skr�cie ustawiamy d�ugo�� krok�w na 1 cm
                var direction = (PlayerTransform.position
                    - transform.position).normalized;

                // transoform.position to mam na my�li pozycj� monety
                transform.position += direction * gm.magnet.GetSpeed();
            }
        }
    }
}
