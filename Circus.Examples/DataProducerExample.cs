using System;
using Circus.DataProducers;
using Circus.OrderBook;

namespace Circus.Examples
{
    public class MarketDataProducerExample
    {
        public static void Run()
        {
            var time = new UtcTimeProvider();

            var tradeDataProducer = new TradeDataProducer();
            tradeDataProducer.Traded += (_, args) => Console.WriteLine(args);
            var levelDataProducer = new LevelDataProducer(5);
            levelDataProducer.LevelsUpdated += (_, args) => Console.WriteLine(args);

            var sec1 = new Security("GCZ6", SecurityType.Future, 10, 10);
            IOrderBook book1 = new InMemoryOrderBook(sec1, time);
            book1.OrderBookEvent += (_, args) =>
            {
                tradeDataProducer.Process(book1, args.Events);
                levelDataProducer.Process(book1, args.Events);
            };
            book1.SetStatus(OrderBookStatus.Open);
            book1.CreateLimitOrder(Guid.NewGuid(), TimeInForce.Day, Side.Buy, 100, 3);
            book1.CreateLimitOrder(Guid.NewGuid(), TimeInForce.Day, Side.Sell, 100, 5);

            var sec2 = new Security("SIZ6", SecurityType.Future, 10, 10);
            IOrderBook book2 = new InMemoryOrderBook(sec2, time);
            book2.OrderBookEvent += (_, args) =>
            {
                tradeDataProducer.Process(book2, args.Events);
                levelDataProducer.Process(book2, args.Events);
            };
            book2.SetStatus(OrderBookStatus.Open);
            book2.CreateLimitOrder(Guid.NewGuid(), TimeInForce.Day, Side.Buy, 100, 3);
            book2.CreateLimitOrder(Guid.NewGuid(), TimeInForce.Day, Side.Sell, 100, 5);
        }
    }
}