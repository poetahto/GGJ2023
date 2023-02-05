using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class DecisionItemFactory : MonoBehaviour
    {
        public DecisionItemView viewPrefab;
        public List<Effect> potentialPacts = new List<Effect>();
        public List<Effect> potentialCurses = new List<Effect>();

        private void OnGUI()
        {
            if (GUILayout.Button("Create item"))
            {
                Create();
            }
        }

        public GameObject Create()
        {
            Shuffle(potentialCurses);
            var result = Instantiate(viewPrefab);

            var pactList = new List<Effect>
            {
                GetRandom(potentialPacts)
            };

            var curseList = new List<Effect>
            {
                potentialCurses[0],
                potentialCurses[1],
                potentialCurses[2],
            };
            
            Instantiate(pactList[0], result.transform);
            Instantiate(curseList[0], result.transform);
            var trueCurse = curseList[0];
            Shuffle(curseList);
            result.UpdateData(pactList[0], trueCurse, curseList);
            
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
}