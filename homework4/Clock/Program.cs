using System;

namespace Clock
{
    class Clock
    {
        private int hour, minute, second;
        public int ClockHour  { set; get; }
        public int ClockMinute { set; get; }
        public void SetAlarm(int hour, int minute)
        {
            if (hour >= 24 || hour < 0 || minute >= 60 || minute < 0)
                throw new ArgumentOutOfRangeException("invalid input");
            ClockHour = hour;
            ClockMinute = minute;
        }
        public event Action<int, int, int> OnTick;
        public event Action<int, int> OnAlarm;
        public Clock()
        {
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;
            second = DateTime.Now.Second;
        }
        public void Start()
        {
            while (true)
            {
                ++second;
                if (second == 60)
                {
                    second = 0;
                    ++minute;
                    if (minute == 60)
                    {
                        minute = 0;
                        hour=(hour+1)%24;
                    }
                }
                if (hour == ClockHour && minute == ClockMinute)
                {
                    OnAlarm(hour, minute);//in this way it would alarm during HOUR:MIN, so there should jump out of the loop
                }
                else OnTick(hour, minute,second);
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
    
    class Program
    {
        void OnTick(int hour, int minute, int second)
        {
            Console.WriteLine("Tick at {0}:{1}:{2}", hour.ToString("00"), minute.ToString("00"), second.ToString("00"));
        }
        void OnAlarm(int hour, int minute)
        {
            Console.WriteLine("Alarm at {0}:{1}", hour.ToString("00"), minute.ToString("00"));
        }
        static void Main()
        {
            Program p = new Program();
            Clock c1 = new Clock();
            c1.OnTick += p.OnTick;
            c1.OnAlarm += p.OnAlarm;
            c1.SetAlarm(13, 02);
            c1.Start();
        }
    }

    
}
