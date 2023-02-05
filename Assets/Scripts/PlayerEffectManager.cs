using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private List<Effect> _effects = new List<Effect>();
        
    public void AddEffect(Effect effect)
    {
        effect.Player = player;
        effect.Initialize();
            
        _effects.Add(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        effect.Shutdown();

        _effects.Remove(effect);
    }

    public void RemoveAllEffects()
    {
        for (int i = _effects.Count - 1; i >= 0; i--)
        {
            RemoveEffect(_effects[i]);
        }
    }

    private void Update()
    {
        foreach (var effect in _effects)
        {
            effect.Update();
        }
    }

    private void OnGUI()
    {
        foreach (var effect in _effects)
        {
            GUILayout.Label(effect.Name);
        }
        
        if (GUILayout.Button("Remove all"))
            RemoveAllEffects();
    }
}