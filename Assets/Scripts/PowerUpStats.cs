using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Stw�rz element do wyboru w assetach
[CreateAssetMenu (menuName = "Powerup/Powerup Stats", fileName = "NewPowerupStat")]
public class PowerupStats : ScriptableObject
{
    // Serialize Field pozwala nam na edycj� pomimo zmiennej private
    [SerializeField]
    private float[] value; // [0] [1] [2] [3] [4] [5] [6]

    public float GetValue(int level = 1) // Domy�lny poziom dla ka�dego powerupa to b�dzie 1
    {
        if (level < 1) // Je�eli level wpisany jest za niski
        {
            return value[0];
        }
        else if (level >= value.Length) // Je�eli level wpisany jest zbyt du�y
        {
            return value[value.Length - 1];
        }
        else // Je�eli level jest dobrze wpisany
        {
            return value[level-1];
        }
        
    }


}
