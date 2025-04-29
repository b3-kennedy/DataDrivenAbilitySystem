using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toolTipPrefab;
    TextMeshProUGUI nameText;
    TextMeshProUGUI descriptionText;

    GameObject spawnedTooltip;

    

    void Start()
    {



    }


    public void SetTooltipVisibility(bool value)
    {
        spawnedTooltip.SetActive(value);
    }



    public void SetUpToolTip(AbilityData ability)
    {
        spawnedTooltip = Instantiate(toolTipPrefab, transform);
        spawnedTooltip.transform.localPosition = new Vector3(0, 339.89f, 0);
        nameText = spawnedTooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = spawnedTooltip.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        SetTooltipVisibility(false);
        nameText.text = ability.abilityName;
        descriptionText.text = FormatDescription(ability);
    }

    private string FormatDescription(AbilityData ability)
    {
        // Prepare dictionary for replacements
        Dictionary<string, string> values = new Dictionary<string, string>
        {
            {"damage", ability.damage.ToString() },
            {"cooldown", ability.cooldown.ToString()},
            {"radius", ability.radius.ToString()}
        };

        foreach (var sv in ability.specialValues)
        {
            values[sv.name] = sv.value;
        }

        string description = ability.abilityDescription;

        foreach (var kvp in values)
        {
            description = description.Replace("{" + kvp.Key + "}", kvp.Value);
        }

        return description;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTooltipVisibility(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTooltipVisibility(false);
    }
}
