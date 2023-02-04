using TMPro;

namespace DefaultNamespace
{
    public class DebuggingHealthView : View<Health>
    {
        public TMP_Text text;

        public override void UpdateValue(Health value)
        {
            text.text = $"{value.CurrentHealth}/{value.MaxHealth}";
        }
    }
}