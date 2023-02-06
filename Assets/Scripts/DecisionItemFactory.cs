using System.Collections.Generic;
using Effects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DecisionItemFactory : MonoBehaviour
{
    public DecisionItemView viewPrefab;
    public GameObject eruptParticles;

    public List<Effect> PotentialPacts;
        
    public List<Effect> PotentialCurses;

    private void Awake()
    {
        PotentialPacts = new List<Effect>
        {
            new HigherBulletDamage(1),
            new IncreaseBulletSpeed(1),
            new IncreaseFireRate(1),
            new HealOnHit(0.25f),
            new BiggerBullets(0.25f),
            new PiercingEffect(),
            new ExplodeOnImpact(1, 1, eruptParticles),
            new IncreaseBulletsPerShot(1),
        };
        
        PotentialCurses = new List<Effect>
        {
            // new LowFrictionEffect(1),
            new StandStillDamage(0.5f, 1),
            new TakeMoreDamage(0.5f),
            new LowerVisibility(0.25f),
            new LongerRound(15),
            new MoreEnemies(1),
            new Slowdown(0.8f),
        };
    }

    public GameObject Create()
    {
        Shuffle(PotentialCurses);
        var result = Instantiate(viewPrefab);

        var pactList = new List<Effect>
        {
            GetRandom(PotentialPacts)
        };

        var curseList = new List<Effect>
        {
            PotentialCurses[0],
             PotentialCurses[1],
        };
            
        var trueCurse = curseList[0];
        Shuffle(curseList);
        result.UpdateData(pactList[0], trueCurse, curseList);
            
        result.collectable.onCollect.AddListener(data =>
        {
            if (data.collector.TryGetComponent(out PlayerEffectManager effectManager))
            {
                effectManager.AddEffect(pactList[0]);
                effectManager.AddEffect(trueCurse);
            }
        });
            
        return result.gameObject;
    }

    public static void Shuffle(List<Effect> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var rand = Random.Range(0, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

    public static Effect GetRandom(List<Effect> list)
    {
        return list[Random.Range(0, list.Count)];
    }
}