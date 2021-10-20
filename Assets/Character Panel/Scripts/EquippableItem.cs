using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Juma.CharacterStats;

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
    public int StrengthPercentBonus;
    public int DexterityPercentBonus;
    public int IntelligencePercentBonus;
    public int DefencePercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public void Equip(CharacterPanelManager character)
    {
        if (StrengthBonus != 0)
        {
            character.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        }

        if (DexterityBonus != 0)
        {
            character.Dexterity.AddModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));
        }

        if (IntelligenceBonus != 0)
        {
            character.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        }

        if (DefenceBonus != 0)
        {
            character.Defence.AddModifier(new StatModifier(DefenceBonus, StatModType.Flat, this));
        }


        if (StrengthPercentBonus != 0)
        {
            character.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        }

        if (DexterityPercentBonus != 0)
        {
            character.Dexterity.AddModifier(new StatModifier(DexterityPercentBonus, StatModType.PercentMult, this));
        }

        if (IntelligencePercentBonus != 0)
        {
            character.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        }

        if (DefencePercentBonus != 0)
        {
            character.Defence.AddModifier(new StatModifier(DefencePercentBonus, StatModType.PercentMult, this));
        }

    }

    public void Unequip(CharacterPanelManager character)
    {
        character.Strength.RemoveAllModifiersFromSource(this);
        character.Dexterity.RemoveAllModifiersFromSource(this);
        character.Intelligence.RemoveAllModifiersFromSource(this);
        character.Defence.RemoveAllModifiersFromSource(this);
    }
}
