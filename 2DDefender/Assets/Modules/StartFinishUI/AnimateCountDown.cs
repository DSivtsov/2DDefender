using System;
using System.Collections;
using Asyncoroutine;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Modules.StartStopGameUI
{
    public class AnimateCountDown
    {

        private readonly TMP_Text _textCount;
        private readonly Transform _transformText;
        private readonly float _countInterval;
        private readonly int _beginCountingNumber = 3;
        private readonly int _endCountingNumber = 0;

        public event Action OnCountDownFinished;

        public AnimateCountDown(TMP_Text textCount, int beginCountingNumber, int endCountingNumber,
            float countInterval = 1f)
        {
            _textCount = textCount;
            _transformText = textCount.transform;
            _countInterval = countInterval;
            _beginCountingNumber = beginCountingNumber;
            _endCountingNumber = endCountingNumber;
        }

        public async void StartCountDown()
        {
            await Countdown();
            OnCountDownFinished?.Invoke();;
        }
        
        private IEnumerator Countdown()
        {

            for (int i = _beginCountingNumber; i > _endCountingNumber; i--)
            {
                AnimationCountDown(i);
                yield return new WaitForSeconds(_countInterval);
            }
        }

        private void AnimationCountDown(int count)
        {
            _transformText.localScale = Vector3.one;
            _textCount.text = count.ToString();
            _transformText.DOScale(endValue: 5f, duration: 1f);
        }
    }
}