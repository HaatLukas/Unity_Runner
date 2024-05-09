using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class FloorController : MonoBehaviour
{

    public GameObject FloorTile_1;
    public GameObject FloorTile_2;

    // Dziêki temu, mo¿emy dorzucaæ tyle przeszkód ile chcemy
    public GameObject[] tiles;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (GameManager.settings.inGame == false) { return; }

        FloorTile_1.transform.position -= new Vector3(GameManager.settings.worldSpeed, 0, 0);
        FloorTile_2.transform.position -= new Vector3(GameManager.settings.worldSpeed, 0, 0);


        if (FloorTile_2.transform.position.x < 0f)
        {
            // Zamiast 32f wpisujecie ca³kowit¹ d³ugoœæ waszych pod³óg
            // FloorTile_1.transform.position += new Vector3(32f, 0f, 0f);

            // Stworzyc jeden wylosowany przez nas kawa³ek poziomu
            var newTile = Instantiate(tiles[Random.Range(0, tiles.Length)],
                FloorTile_2.transform.position + new Vector3(16f, 0f, 0f), Quaternion.identity);
            Destroy(FloorTile_1);
            FloorTile_1 = FloorTile_2;
            FloorTile_2 = newTile;
        }

       


    }





}
