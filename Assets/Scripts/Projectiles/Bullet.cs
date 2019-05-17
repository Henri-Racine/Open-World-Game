using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Projectiles.Effects;
namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : Projectile
    {
        public float speed = 50f;
        [BoxGroup("References")] public GameObject effectPrefab;
        [BoxGroup("References")] public Transform line;
        bool hit = false;

        private Rigidbody rigid;

        //public GameObject cube;

        Vector3 start, end;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
            start = transform.position;
        }


        public static float AngleOffAroundAxis(Vector3 v, Vector3 forward, Vector3 axis)
        {
            Vector3 right = Vector3.Cross(axis, forward);
            return Mathf.Atan2(Vector3.Dot(v, right), Vector3.Dot(v, forward)) * Mathf.Rad2Deg;
        }

        // Sorting method to get largest float magnitude in normal Vector NOTE only works well for surfaces oriented to world axis
        Vector3 Displacement(Vector3 _displacement, Vector3 normal)
        {
            if (Mathf.Abs(normal.x) > Mathf.Abs(normal.z))
            {
                if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
                {
                    _displacement = new Vector3(_displacement.x, 0, 0);
                }
            }
            else if (Mathf.Abs(normal.z) > Mathf.Abs(normal.x))
            {
                if (Mathf.Abs(normal.z) > Mathf.Abs(normal.y))
                {
                    _displacement = new Vector3(0, 0, _displacement.z);
                }
            }
            else
            {
                _displacement = new Vector3(0, _displacement.y, 0);
            }

            return _displacement;
        }

        void OnCollisionEnter(Collision col)
        {
            end = transform.position;
            // Get contact point from collision
            ContactPoint contact = col.contacts[0];

            // Get bulletDirection
            Vector3 bulletDir = end - start;

            Quaternion lookRotation = Quaternion.LookRotation(bulletDir);
            Quaternion rotation = lookRotation * Quaternion.AngleAxis(-90, Vector3.right);
            // Spawn a BulletHole on that contact point
            GameObject clone = Instantiate(effectPrefab, contact.point, rotation);
            // Get angle between normal and bullet dir
            float impactAngle = 180 - Vector3.Angle(bulletDir, contact.normal);
            clone.transform.localScale = clone.transform.localScale / (1 + impactAngle / 45);

            // Effect script
            Effect effect = clone.GetComponent<Effect>();
            effect.damage += damage;
            effect.hitObject = col.transform;

            // Destroy self
            Destroy(gameObject);
        }
        public override void Fire(Vector3 lineOrigin, Vector3 direction)
        {
            // Set line position to origin
            line.position = lineOrigin;
            // Set bullet flying in direction with speed
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }
    }
}

