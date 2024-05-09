using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup / Magnet", fileName = "Magnet") ]
public class Magnet : Powerup
{
    [SerializeField]
    private PowerupStats range; // Zasi�g dzia�ania
    public float GetRange()
    {
        return range.GetValue(currentLevel);
    }
    
    [SerializeField]
    private PowerupStats speed; // Si�� przyci�gania monet
    public float GetSpeed() 
    {
        return speed.GetValue(currentLevel);
    }
}
