using System;
using UnityEngine;

namespace DefaultNamespace.VFX
{
    public class SpriteScaleAndAlpha : MonoBehaviour
    {
        public float speed = 1;
        public float alphaSpeed = 1;
        public SpriteRenderer sprite;

        private void Start()
        {
            var col = sprite.color;
            col.a = 1;
            sprite.color = col;

            sprite.transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            var col = sprite.color;
            col.a -= alphaSpeed * Time.deltaTime;
            sprite.transform.localScale += Vector3.one * (speed * Time.deltaTime);
            sprite.color = col;
        }
    }
}