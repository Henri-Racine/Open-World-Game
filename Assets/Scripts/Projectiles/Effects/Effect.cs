using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public class Effect : MonoBehaviour
    {
        public float destroyDelay = 5f;
        public int damage = 2;
        [Tooltip("what Visual effects to spawn as a child to the thing we hit")]
        public GameObject visualEffectsPrefab;
        [HideInInspector] public Transform hitObject;

        private float destroyTimer = 0f;
        // Start is called before the first frame update

        protected virtual void Start()
        {
            GameObject clone = Instantiate(visualEffectsPrefab, hitObject.transform);
            clone.transform.position = transform.position;
            clone.transform.rotation = transform.rotation;
        }
        protected virtual void Update()
        {
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= destroyDelay)
            {
                Kill();
            }
        }


        public virtual void Kill()
        {
            Destroy(gameObject);
        }
    }
}
