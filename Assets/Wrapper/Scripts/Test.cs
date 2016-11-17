using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Wrapper.Scripts
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private Button _startButton;

		[SerializeField]
		private Image _gImg;

		[SerializeField]
		private Image _bImg;

		[SerializeField]
		private GameObject _pointer;


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
				_gImg.fillAmount = 0.4f;
				_bImg.fillAmount = 0.8f;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
				var xPos = 0f;
				if (_gImg.fillAmount > 0.5f) {
					xPos = _gImg.rectTransform.position.x + (_gImg.rectTransform.rect.width * (_gImg.fillAmount - 0.5f));
				} else if (_gImg.fillAmount < 0.5f) {
					xPos = _gImg.rectTransform.position.x - (_gImg.rectTransform.rect.width * (0.5f - _gImg.fillAmount));
				} else {
					xPos = _gImg.rectTransform.position.x;
				}

				_pointer.transform.parent = _gImg.transform;
				_pointer.transform.position = Vector3.zero;
				_pointer.transform.position = new Vector3(xPos, _gImg.transform.position.y, _gImg.transform.position.z);
				_pointer.transform.rotation = _gImg.transform.rotation;
            }
        }

        void Start()
        {
            _startButton.onClick.AddListener(OnStartClick);
        }

        private void OnStartClick()
        {
            Debug.Log("start");
        }
    }
}
