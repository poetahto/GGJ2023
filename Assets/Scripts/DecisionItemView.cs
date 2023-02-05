using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Effects
{
    public class DecisionItemView : MonoBehaviour
    {
        public TMP_Text display;

        public void UpdateData(IEnumerable<Effect> pacts, IEnumerable<Effect> curses)
        {
            var sb = new StringBuilder();

            foreach (var pact in pacts)
            {
                sb.AppendLine($"<color=green>{pact.GetName()} - <i>{pact.GetDescription()}</i></color>");
            }
            
            foreach (var curse in curses)
            {
                sb.AppendLine($"<color=red>{curse.GetName()} - <i>{curse.GetDescription()}</i></color>");
            }

            display.text = sb.ToString();
        }
    }
}