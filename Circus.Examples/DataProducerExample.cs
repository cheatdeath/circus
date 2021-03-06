using System;
using System.Collections.Generic;
using Circus.DataProducers;
using Circus.OrderBook;
using Circus.TimeProviders;

namespace Circus.Examples
{
    public class MarketDataProducerExample
    {
        public static void Run()
        {
            var time = new UtcTimeProvider();

            var sec1 = new Security("GCZ6", SecurityType.Future, 10, 10);
            var sec2 = new Security("SIZ6", SecurityType.Future, 10, 10);

            IOrderBook book1 = new InMemoryOrderBook(sec1, time);
            IOrderBook book2 = new InMemoryOrderBook(sec2, time);

            var tradeDataProducer = new TradeDataProducer();
            var levelDataProducer = new LevelDataProducer(5);

            void Publish(IOrderBook book, IList<OrderBookEvent> events)
            {
                Print(tradeDataProducer.Process(book, events));
                Print(levelDataProducer.Process(book, events));
            }

            Publish(book1, book1.UpdateStatus(OrderBookStatus.Open));
            Publish(book1, book1.CreateOrder(Guid.NewGuid(), Guid.NewGuid(), OrderValidity.Day, Side.Buy, 3, 100));
            Publish(book1, book1.CreateOrder(Guid.NewGuid(), Guid.NewGuid(), OrderValidity.Day, Side.Sell, 5, 100));

            Publish(book2, book2.UpdateStatus(OrderBookStatus.Open));
            Publish(book2, book2.CreateOrder(Guid.NewGuid(), Guid.NewGuid(), OrderValidity.Day, Side.Buy, 3, 100));
            Publish(book2, book2.CreateOrder(Guid.NewGuid(), Guid.NewGuid(), OrderValidity.Day, Side.Sell, 5, 100));
        }

        private static void Print(IEnumerable<TradedDataEvent> events)
        {
            foreach (var @event in events)
            {
                Console.WriteLine(@event);
            }
        }

        private static void Print(IEnumerable<LevelsDataEvent> events)
        {
            foreach (var @event in events)
            {
                Console.WriteLine(@event);
            }
        }
    }
}