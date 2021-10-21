using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemSlotText;
    [SerializeField] TextMeshProUGUI ItemStatsText;
    [SerializeField] TextMeshProUGUI ItemDescriptionText;

    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip (EquippableItem equippableItem)
    {
        ItemNameText.text = equippableItem.ItemName;
        ItemSlotText.text = equippableItem.EquipmentType.ToString();

        sb.Length = 0;
        AddStat(equippableItem.StrengthBonus, "Strength");
        AddStat(equippableItem.DexterityBonus, "Dexterity");
        AddStat(equippableItem.IntelligenceBonus, "Intelligence");
        AddStat(equippableItem.DefenceBonus, "Defence");

        AddStat(equippableItem.StrengthPercentBonus, "Strength", isPercentMult: true);
        AddStat(equippableItem.DexterityPercentBonus, "Dexterity", isPercentMult: true);
        AddStat(equippableItem.IntelligencePercentBonus, "Intelligence", isPercentMult: true);
        AddStat(equippableItem.DefencePercentBonus, "Defence", isPercentMult: true);

        /*
         * this allocates so much memory especially if ran many times
         * creating a new copy of a string with the concatenation
         * we will allocate tons of strings & will be very bad for performance 
        ItemStatsText.text = equippableItem.StrengthBonus + " Strength";
        ItemStatsText.text += "\n" + equippableItem.IntelligenceBonus + " Intelligence";
        */

        ItemStatsText.text = sb.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName, bool isPercentMult = false)
    {
        if (value!=0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (value > 0)
            {
                sb.Append("+");
            }

            if (isPercentMult)
            {
                sb.Append(value);
                //sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            //sb.Append(value);
            //sb.Append(" ");
            sb.Append(statName);
        }
    }
}
