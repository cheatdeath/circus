using System.Collections.Generic;
using Circus.OrderBook;

namespace Circus.DataProducers
{
    public interface IDataProducer
    {
        void Process(IOrderBook book, IEnumerable<OrderBookEvent> events);
    }
}