using System.Collections;
using System.Collections.Generic;
using Collectables;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DecisionItemView : MonoBehaviour
{
    public float delayTime = 2;
    // public TMP_Text displayPact;
    private List<TMP_Text> displayCurses = new List<TMP_Text>();
    public Collectable collectable;
    public TMP_Text effectDisplayPrefab;
    public float offset;
    public float baseHeight;
    public Color pactColor = Color.green;
    public Color curseColor = Color.red;

    private TMP_Text _trueCurseText;

    public void CollectAnimation()
    {
        StartCoroutine(CollectAnimCoroutine());
    }

    private IEnumerator CollectAnimCoroutine()
    {
        GetComponent<Collider>().enabled = false;
        
        foreach (var displayCurse in displayCurses)
        {
            if (displayCurse != _trueCurseText)
            {
                displayCurse.enabled = false;
            }
        }

        yield return new WaitForSeconds(delayTime);

        gameObject.SetActive(false);
    }

    public TMP_Text AddEffectDisplay(Effect effect, Color color, float instanceOffset)
    {
        var instance = Instantiate(effectDisplayPrefab, transform);
        instance.rectTransform.position = transform.position;
        instance.rectTransform.position += Vector3.up * (instanceOffset + baseHeight);
        instance.text = $"<b><color=#{color.ToHexString()}>{effect.Name}</b> - <size=75%>{effect.Description}</color></size>";
        return instance;
    }

    public void UpdateData(Effect pact, Effect trueCurse, IList<Effect> cursesRandom)
    {
        // displayPact.text = $"<color=green>{pact.Name} - <i>{pact.Description}</i></color>";
        AddEffectDisplay(pact, pactColor, 0);

        for (int i = 0; i < cursesRandom.Count; i++)
        {
            var instance = AddEffectDisplay(cursesRandom[i], curseColor, (i + 1) * offset);
            // displayCurses[i].text = $"<color=red>{cursesRandom[i].Name} - <i>{cursesRandom[i].Description}</i></color>";
            if (cursesRandom[i] == trueCurse)
            {
                _trueCurseText = instance;
            }
            displayCurses.Add(instance);
        }
    }
}