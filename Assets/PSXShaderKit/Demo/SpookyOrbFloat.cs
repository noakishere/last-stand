using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSXShaderKit
{
    public class SpookyOrbFloat : MonoBehaviour
    {
        [SerializeField]
        private float MinHeight;
        [SerializeField]
        private float MaxHeight;
        [SerializeField]
        private float RotationSpeed;

        [SerializeField] 
        private string text;

        [SerializeField]
        private AudioClip clip;

        [SerializeField] private bool doesItEnd;


        private void Start()
        {
            MinHeight = transform.position.y;
            MaxHeight = MinHeight + 2f;
        }

        // Update is called once per frame
        void Update()
        {
            float yPos = Mathf.Lerp(MinHeight, MaxHeight, (Mathf.Sin(Time.time * 0.65f) + 1) * 0.5f);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            transform.Rotate(new Vector3(0, RotationSpeed * Time.deltaTime, 0));
        }

        private void Interact()
        {
            if(text != "")
            {
                TextManager.Instance.UpdateText(text);
            }

            if(clip != null)
            {
                SoundManager.Instance.PlayEffectAudio(clip);
            }

            if (doesItEnd)
            {
                StartCoroutine(End());
            }

            else
            {
                Destroy(gameObject);
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                Interact();
            }
        }

        private IEnumerator End()
        {
            yield return new WaitForSeconds(10f);

            SceneBehaviour.Instance.MainMenu();
        }
    }
}
