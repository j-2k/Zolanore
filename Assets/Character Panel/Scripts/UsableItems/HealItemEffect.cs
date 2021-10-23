using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS LAYOUT CAN BE USED WITH MULTIPLE OTHER EFFECTS EVEN IN A SPELL SYSTEM FOR EXAMPLE
[CreateAssetMenu]
public class HealItemEffect : UsableItemEffect
{
    public int healthAmount;

    public override void ExecuteEffect(UsableItem parentItem, CharacterPanelManager characterPanelManager)
    {
        characterPanelManager.health += healthAmount;
    }

    public override string GetDescription()
    {
        return "Heals for " + healthAmount + " health.";
    }

    public override string GetDescriptionLore()
    {
        return "Lore stuff.";
    }
}
