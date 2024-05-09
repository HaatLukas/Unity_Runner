using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Stwórz element do wyboru w assetach
[CreateAssetMenu (menuName = "Powerup/Powerup Stats", fileName = "NewPowerupStat")]
public class PowerupStats : ScriptableObject
{
    // Serialize Field pozwala nam na edycjê pomimo zmiennej private
    [SerializeField]
    private float[] value; // [0] [1] [2] [3] [4] [5] [6]

    public float GetValue(int level = 1) // Domyœlny poziom dla ka¿dego powerupa to bêdzie 1
    {
        if (level < 1) // Je¿eli level wpisany jest za niski
        {
            return value[0];
        }
        else if (level >= value.Length) // Je¿eli level wpisany jest zbyt du¿y
        {
            return value[value.Length - 1];
        }
        else // Je¿eli level jest dobrze wpisany
        {
            return value[level-1];
        }
        
    }


}
