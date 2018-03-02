using System;

namespace iOS_Multiscreen
{
    class Incer
    
		
		public Incer()
		{			
		}
		
        public void runMe()
        {
            Counter c = new Counter(new Random().Next(10));

            // Add our own method to the event pointer in Counter.
            c.ThresholdReached += c_ThresholdReached;
        }

        // Function which will be exectuted whne the event fires. 
        static void c_ThresholdReached(Object sender, ThresholdReachedEventArgs e)
        {
            Console.WriteLine("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);
            Environment.Exit(0);
        }
    }
    
    
    class Counter
    {
        private int threshold;
        private int total;

        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x)
        {
            total += x;
            if (total >= threshold)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
        }

        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            ThresholdReachedEventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        // Create the event - event "Name the same as the delegate" "Name of event"
        public event ThresholdReachedEventHandler ThresholdReached;
    }
    

    // Create an arguments class if needed
    public class ThresholdReachedEventArgs : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }
    
    // Create the delegate for our event. This takes a ref to the sender and our custom eventargs class
    public delegate void ThresholdReachedEventHandler(Object sender, ThresholdReachedEventArgs e);
}

