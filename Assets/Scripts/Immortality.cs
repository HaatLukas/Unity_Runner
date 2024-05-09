using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Powerup/Immortality", fileName = "Immortality") ]
public class Immortality : Powerup
{
    [SerializeField]
    private PowerupStats speed;

    public float GetSpeed()
    {
        return speed.GetValue(currentLevel);
    }

}
