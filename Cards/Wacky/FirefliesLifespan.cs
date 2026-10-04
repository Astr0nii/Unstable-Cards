using System.Collections;
using UnityEngine;

namespace UnstableCards.Cards.Wacky
{
    /// <summary>
    /// Gives Fireflies bullets a "lifespan": the firefly glows for a moment, phases through
    /// whatever it wants, and then simply gets tired and dies. This is what stops the
    /// wall-phasing swarm from living forever and chewing through an entire level unsupervised.
    /// Runs locally on every client so bullet despawns stay in sync.
    /// </summary>
    internal class FirefliesLifespan : MonoBehaviour
    {
        public float lifespan = 2.0f;

        private SpriteRenderer spriteRenderer;
        private Color baseColor = Color.white;
        private bool handled;

        void Start()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                baseColor = spriteRenderer.color;
            }

            StartCoroutine(LiveABriefAndBrightLife());
        }

        IEnumerator LiveABriefAndBrightLife()
        {
            float elapsed = 0f;

            while (elapsed < lifespan)
            {
                // Time is frozen (pause menu / round end): the firefly holds its breath and waits.
                if (Time.timeScale <= 0f)
                {
                    yield return null;
                    continue;
                }

                elapsed += Time.deltaTime;

                if (spriteRenderer != null)
                {
                    float flicker = Mathf.PingPong(elapsed * 18f, 1f);
                    float fade = Mathf.Clamp01(1f - (elapsed / lifespan));
                    spriteRenderer.color = new Color(baseColor.r, baseColor.g, baseColor.b, fade * (0.55f + 0.45f * flicker));
                }

                yield return null;
            }

            Die();
        }

        void Die()
        {
            if (handled) return;
            handled = true;

            // The firefly's time has come. Poof.
            Destroy(gameObject);
        }
    }
}
