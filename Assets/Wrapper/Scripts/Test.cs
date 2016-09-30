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

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {

            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                
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
