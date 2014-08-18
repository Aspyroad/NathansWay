/*
It's one of those things that's much easier to understand once you do it.
For producer consumer, let's have two objects, Producer and Consumer. 
They both share a queue they're given when constructed, so they can write between it.
Adding in a producer consumer is pretty familiar, just with the CompleteAdding a little different:
*/

    public class Producer{
       private BlockingCollection<string> _queue;
       public Producer(BlockingCollection<string> queue){_queue = queue;}  

       //a method to do something
       public MakeStuff()
       {
           for(var i=0;i<Int.MaxValue;i++)
           {
                _queue.Add("a string!");
           }

           _queue.CompleteAdding();
       }
}

/*
The consumer doesn't seem to make sense...

UNTIL YOU REALIZE THAT THE FOREACH WILL NOT STOP LOOPING UNTIL THE QUEUE HAS COMPLETED ADDING. 

Until then, if there's no items, it will just go back to sleep. 
And since it's the same instance of the collection in the producer and consumer.
You can have the consumer ONLY taking up cycles when there's actually things to do, and not have to worry about stopping it, restarting it, etc.
*/

public class Consumer()
{
      private BlockingCollection<string> _queue;
      public Consumer(BlockingCollection<string> queue)
      {
           _queue = queue;
      }

      public void WriteStuffToFile()
      {
          //we'll hold until our queue is done.  If we get stuff in the queue, we'll start processing it then
          foreach(var s in _queue.GetConsumingEnumerable())
          {
             WriteToFile(s);
          }
      }
}

/*
So you wire them together by using the collection.
*/

var queue = new BlockingCollection<string>();
var producer = new Producer(queue);
var consumer = new Consumer(queue);

producer.MakeStuff();
producer.WriteStuffToFile();