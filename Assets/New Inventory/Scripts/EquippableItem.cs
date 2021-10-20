using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{ 
    Weapon,
    Shield,
    Helmet,
    Chestplate,
    Platelegs,
    Boots,
    Accessory1,
    Accessory2,
    Accessory3,
    Accessory4,
}


[CreateAssetMenu]
public class EquippableItem : Item
{
    public int StrengthBonus;
    public int DexterityBonus;
    public int IntelligenceBonus;
    public int DefenceBonus;
    [Space]
    public EquipmentType EquipmentType;
    //??? no % prob
    /*
    public int StrengthPercentBonus;
    public int DexterityPercentBonus;
    public int IntelligencePercentBonus;
    public int DefencePercentBonus;
    */
}
