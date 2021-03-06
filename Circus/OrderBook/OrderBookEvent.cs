using System;
using System.Collections.Generic;

namespace Circus.OrderBook
{
    public record OrderBookEvent(Security Security, DateTime Time);

    public record StatusChanged(Security Security, DateTime Time, OrderBookStatus Status)
        : OrderBookEvent(Security, Time);

    public record OrderEvent(Security Security, DateTime Time, Guid ClientId, Guid OrderId)
        : OrderBookEvent(Security, Time);

    public record OrderConfirmedEvent(Security Security, DateTime Time, Guid ClientId, Order Order)
        : OrderEvent(Security, Time, ClientId, Order.OrderId);

    public record CreateOrderConfirmed(Security Security, DateTime Time, Guid ClientId, Order Order)
        : OrderConfirmedEvent(Security, Time, ClientId, Order);

    public record UpdateOrderConfirmed(Security Security, DateTime Time, Guid ClientId, Order Order)
        : OrderConfirmedEvent(Security, Time, ClientId, Order);

    public record CancelOrderConfirmed(Security Security, DateTime Time, Guid ClientId, Order Order,
            OrderCancelledReason Reason)
        : OrderConfirmedEvent(Security, Time, ClientId, Order);

    public record ExpireOrderConfirmed(Security Security, DateTime Time, Guid ClientId, Order Order)
        : OrderConfirmedEvent(Security, Time, ClientId, Order);

    public record FillOrderConfirmed(Security Security, DateTime Time, Guid ClientId, Order Order, decimal Price,
            int Quantity, bool IsResting)
        : OrderConfirmedEvent(Security, Time, ClientId, Order);

    public record OrderRejectedEvent(Security Security, DateTime Time, Guid ClientId, Guid OrderId,
            OrderRejectedReason Reason)
        : OrderEvent(Security, Time, ClientId, OrderId);

    public record CreateOrderRejected(Security Security, DateTime Time, Guid ClientId, Guid OrderId,
            OrderRejectedReason Reason)
        : OrderRejectedEvent(Security, Time, ClientId, OrderId, Reason);

    public record UpdateOrderRejected(Security Security, DateTime Time, Guid ClientId, Guid OrderId,
            OrderRejectedReason Reason)
        : OrderRejectedEvent(Security, Time, ClientId, OrderId, Reason);

    public record CancelOrderRejected(Security Security, DateTime Time, Guid ClientId, Guid OrderId,
            OrderRejectedReason Reason)
        : OrderRejectedEvent(Security, Time, ClientId, OrderId, Reason);

    public record OrdersMatched(Security Security, DateTime Time, decimal Price, int Quantity,
            IList<FillOrderConfirmed> Fills)
        : OrderBookEvent(Security, Time);
}