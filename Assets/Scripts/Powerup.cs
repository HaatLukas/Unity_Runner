using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Powerup : ScriptableObject
{
    [SerializeField]
    protected int[] UpgradeCosts; // Koszt ulepszeñ
    [SerializeField]
    protected int currentLevel = 1;
    [SerializeField]
    protected int maxLevel = 5;
  //  [SerializeField]
   // protected string name;

    private void Awake()
    {
        LoadPowerupLevel();
    }
    private void LoadPowerupLevel()
    {
        string key = name + "Level";
        if(PlayerPrefs.HasKey(key)) 
        {
            PlayerPrefs.GetInt(key);
        }
      
    }
    private void SavePowerupLevel()
    {
        string key = name + "Level";
        PlayerPrefs.SetInt(key, currentLevel);
    }
    private void OnValidate() // Sprawdzacz danych
    {
        currentLevel = Mathf.Min(currentLevel, maxLevel);
        currentLevel = Mathf.Max(currentLevel, 1);
    }
    public bool IsMaxedOut() // Sprawdzamy czy jesteœmy na max poziomie
    {
        return currentLevel == maxLevel;
    }
    public int GetNextUpgradeCost()
    {
        if (!IsMaxedOut()) // ! -- NIE
        {
            return UpgradeCosts[currentLevel - 1];
        }
        return -1;
    }
    public void Upgrade()
    {
        if (IsMaxedOut()) 
        { 
            return;
        }
        currentLevel++;
        SavePowerupLevel();
    }
    // Override pozwala nam nadpisaæ istniej¹c¹ funkcjê
    public override string ToString()
    {
        string text = $" {name} LVL. {currentLevel} ";     // 1. Bateria LVL. 2 
        if (IsMaxedOut())    // 2. Bateria LVL. 2 (MAX)
        {
            text += "(MAX)";
        }
        return text;
    }
    public string UpgradeCostString()
    {
        if (!IsMaxedOut())
        {
            return $"UPGRADE \n COST: {GetNextUpgradeCost()}";
        }
        else
        {
            return "MAXED OUT";
        }
    }

    public bool isActive;

    [SerializeField]
    protected PowerupStats duration;
    public float getDuration()
    {
        return duration.GetValue(currentLevel);
    }

    // private - Mo¿na je u¿yæ tylko w skrypcie, który tworzymy
    // protected - Mo¿na je u¿yc tylko w skrypcie oraz we wszystkich które je dziedzicz¹
    // public - Mo¿na je u¿yæ wszêdzie w projekcie


}
