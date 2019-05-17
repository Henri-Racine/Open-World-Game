using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Effects
{
    public class Impact : Effect
    {
        public override void Kill()
        {
            IHealth health = hitObject.GetComponent<IHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
                print(damage);
            }

            base.Kill();
        }
    }
}
