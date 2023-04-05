using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

namespace FromForGame
{
    public class Sota : MonoBehaviour
    {

        public GameObject _FX;
        public GameObject _sota;

        bool click;
        bool _damp;
        bool _destroy;

        Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _sota.SetActive(true);

            transform.eulerAngles = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click();
            }
        }


        private void Click()
        {
            Debug.Log("Click");

            click = true;
            _sota.SetActive(false);

            Instantiate(_FX, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.EulerRotation(-90, 0, 0));


            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);

            foreach (Collider objects in colliders)
            {
                if (objects.GetComponent<Sota>())
                {
                    objects.GetComponent<Sota>().Damp();
                }
            }

            MMVibrationManager.Haptic(HapticTypes.MediumImpact);

            Destroy(gameObject, 0.3f);
        }


        public void Damp()
        {
            if (_damp) return;

            StartCoroutine(DampAnim());
        }

        IEnumerator DampAnim()
        {
            _damp = true;

            _animator.SetTrigger("active");

            yield return new WaitForSeconds(0.2f);

            _damp = false;
        }
    }
}