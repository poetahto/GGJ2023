using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerEffectManager : MonoBehaviour, IResettable
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
        if (_effects.Count <= 0)
            return;
        
        for (int i = _effects.Count - 1; i >= 0; i--)
        {
            RemoveEffect(_effects[i]);
        }
    }

    private void Update()
    {
        if (_effects.Count <= 0)
            return;
        
        for (int i = _effects.Count - 1; i >= 0; i--)
        {
            _effects[i].Update();
        }
    }

    public bool showDebug = false;
    
    private void OnGUI()
    {
        if (showDebug)
        {
            foreach (var effect in _effects)
            {
                GUILayout.Label(effect.Name);
            }
        
            if (GUILayout.Button("Remove all"))
                RemoveAllEffects();
        }
    }

    public void ResetObject()
    {
        RemoveAllEffects();
        _effects.Clear();
    }
}