//// Event Subscription memory Leak Demo
//var publisher = new EventPublisher();
//var subscriber = new EventSubscriber();

//// Subscriber attaches to the event
//subscriber.Subscribe(publisher);

//for (int i = 0; i < 15; i++)
//{
//    // Simulate event triggering
//    publisher.TriggerEvent();

//    // Optionally unsubscribe
//    // Uncomment the following line to test memory management with unsubscribing
//    // subscriber.Unsubscribe(publisher);
//}

//// Keep the console window open
//Console.WriteLine("Press any key to exit...");
//Console.ReadKey();

using System.Diagnostics.Tracing;


SampleEventSource.Log.AppStarted("Application Started!");
SampleEventSource.Log.DebugMessage("Process 1");
SampleEventSource.Log.DebugMessage("Process 1 Finished");
SampleEventSource.Log.RequestStart(3);
SampleEventSource.Log.RequestStop(3);

[EventSource(Name = "SampleEventSource")]
class SampleEventSource : EventSource
{
    public static SampleEventSource Log { get; } = new SampleEventSource();

    [Event(1, Keywords = Keywords.Startup)]
    public void AppStarted(string message) => WriteEvent(1, message);
    [Event(2, Keywords = Keywords.Requests)]
    public void RequestStart(int requestId) => WriteEvent(2, requestId);
    [Event(3, Keywords = Keywords.Requests)]
    public void RequestStop(int requestId) => WriteEvent(3, requestId);
    [Event(4, Keywords = Keywords.Startup, Level = EventLevel.Verbose)]
    public void DebugMessage(string message) => WriteEvent(4, message);


    public class Keywords
    {
        public const EventKeywords Startup = (EventKeywords)0x0001;
        public const EventKeywords Requests = (EventKeywords)0x0002;
    }
}

