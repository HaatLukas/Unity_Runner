using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoving : MonoBehaviour
{

    public float Speed = 2f;
    public float distance = 2f;
    private float direction = 1f;
    private Vector3 StartPosition;

    void Start()
    {
        // Na starcie gry mówimy gdzie jesteœmy
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.settings.inGame == false) { return; }

        // Liczymy nowa pozycje dla nas
        Vector3 newPosition = transform.position + 
            Vector3.up * Mathf.Sin(Time.deltaTime*Speed) * distance * direction;

        // Wrzucamy j¹ do naszego obiektu
        transform.position = newPosition;

        if (transform.position.y >= StartPosition.y + distance || 
            transform.position.y <= StartPosition.y - distance)
        {
            direction = direction * -1;
            // direction*=-1;
        }
    }
}
