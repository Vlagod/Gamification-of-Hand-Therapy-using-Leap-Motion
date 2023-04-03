using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace WristSniper.Scripts
{
    public class WristSniperGameManager : MonoBehaviour
    {
        public float timerTimeMax = 30f;
        public float timerTimeLeft;

        public int points;

        [SerializeField] private TextMeshProUGUI timerTimeLeftText;
        [SerializeField] private TextMeshProUGUI pointsText;

        public static UnityEvent AddPoint = new UnityEvent ();

        private void OnEnable()
        {
            AddPoint.AddListener(AddPoints);
        }

        private void OnDisable()
        {
            AddPoint.RemoveListener(AddPoints);
        }

        private void Start()
        {
            timerTimeLeft = timerTimeMax;
        }

        private void Update()
        {
            UpdateUI();
            timerTimeLeft -= Time.deltaTime;
            if (timerTimeLeft < 0)
            {
                //game end
            }
        }

        void UpdateUI()
        {
            timerTimeLeftText.text = timerTimeLeft.ToString() + "s";
            pointsText.text = points.ToString();
        }

        void AddPoints()
        {
            points += 1;
        }
        
    }
}