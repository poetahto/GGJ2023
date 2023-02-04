using System.Text;
using Collectables;
using TMPro;
using UnityEngine;

namespace Debug
{
    public class DebuggingStorageView : View<Storage>
    {
        [SerializeField] private TMP_Text text;
        
        public override void UpdateValue(Storage value)
        {
            var sb = new StringBuilder();

            foreach (var storable in value.Storable)
            {
                sb.AppendLine($"{storable.name}");
            }

            text.text = sb.ToString();
        }
    }
}