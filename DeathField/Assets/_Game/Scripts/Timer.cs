using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeathField
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private Text _timeText;
        [SerializeField] private int _maxGameTime;

        private void Start()
        {
            StartCoroutine(ExecuteAfterTime(_maxGameTime));
        }

        IEnumerator ExecuteAfterTime(float timeInSec)
        {
            var time = timeInSec;
            while (time > 0)
            {
                yield return new WaitForSeconds(1f);
                time--;
                //_timeText.text = ((int)(time / 60)).ToString() + ":" + ((int)(time % 60)).ToString();
                _timeText.text = string.Format("{0}:{1:00}", (int)time / 60, (int)time % 60);
            }
            //end game
        }
    }
}
