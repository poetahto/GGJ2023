using System.Collections;
using System.Collections.Generic;
using Collectables;
using TMPro;
using UnityEngine;

public class DecisionItemView : MonoBehaviour
{
    public float delayTime = 2;
    public TMP_Text displayPact;
    public List<TMP_Text> displayCurses;
    public Collectable collectable;

    private TMP_Text _trueCurseText;

    public void CollectAnimation()
    {
        StartCoroutine(CollectAnimCoroutine());
    }

    private IEnumerator CollectAnimCoroutine()
    {
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

    public void UpdateData(Effect pact, Effect trueCurse, IList<Effect> cursesRandom)
    {
        displayPact.text = $"<color=green>{pact.Name} - <i>{pact.Description}</i></color>";

        for (int i = 0; i < cursesRandom.Count; i++)
        {
            displayCurses[i].text = $"<color=red>{cursesRandom[i].Name} - <i>{cursesRandom[i].Description}</i></color>";
            if (cursesRandom[i] == trueCurse)
            {
                _trueCurseText = displayCurses[i];
            }
        }
    }
}