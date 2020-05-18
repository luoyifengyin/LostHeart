using MyGameApplication.CarRacing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyGameApplication.Item.Effect {
    public class P2 : PropEffect {
        private float explosionPower = 1.5e6f;
        private float explosionRadius = 5f;
        private Vector3 rotateForce = new Vector3(0, 1e5f, 1e5f);

        public override bool Condition() {
            return SceneManager.GetActiveScene().name == "CarRacing";
        }

        public override void Operation() {
            int rank = Ranking.Instance.Rank;
            if (rank == 1) return;
            GameObject target = Ranking.Instance.GetRacer(rank - 1);
            EnemyCar enemyCar = target.GetComponent<EnemyCar>();
            if (enemyCar && enemyCar.Visible) {
                var helper = target.GetComponent<P2_EffectHelper>();
                if (!helper) target.AddComponent<P2_EffectHelper>();
                Explode(target);
            }
        }

        private void Explode(GameObject target) {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            Vector3 explosionLocalPos = new Vector3(0, -0.5f, -2f);
            rb.AddExplosionForce(explosionPower, target.transform.TransformPoint(explosionLocalPos), explosionRadius, 1f);
            rb.AddTorque(rotateForce);
        }
    }

    public class P2_EffectHelper : MonoBehaviour {
        private Rigidbody rb;
        private RigidbodyConstraints originConstraints;

        private void Awake() {
            rb = GetComponent<Rigidbody>();
            originConstraints = rb.constraints;
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
                rb.constraints = originConstraints;
            }
        }
    }
}
