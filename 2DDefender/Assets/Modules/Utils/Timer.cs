namespace Modules.Utils
{   
    public class Timer
    {
        protected float _delay;
        private readonly float _deltaTime;
        protected float _timeTillEnd;

        public bool IsTimerStarted { get; private set; }

        public Timer(float delay, float deltaTime)
        {
            _delay = delay;
            _deltaTime = deltaTime;
            IsTimerStarted = false;
        }

        public bool IsTimeFinish()
        {
            /*
             * Запускает обратный таймер на время _delay, если он не запущен.
             * Если запущен уменьшает время осташееся время на _deltaTime
             * Возвращает true если заданной время прошло
             */
            if (IsTimerStarted)
                Tick();
            else
                StartTimer();

            return _timeTillEnd < 0;
        }

        public bool IsTimeNotFinish()
        {
            if (!IsTimerStarted) return false;

            Tick();
            return _timeTillEnd > 0;
        }
        
        public void ResetTimer()
        {
            /*
             * сбрасывает таймер в начальное состояние
             */
            IsTimerStarted = false;
        }

        public virtual void StartTimer()
        {
            _timeTillEnd = _delay;
            IsTimerStarted = true;
        }

        private void Tick() => _timeTillEnd -= _deltaTime;
    }
}