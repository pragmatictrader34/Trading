// ReSharper disable CheckNamespace

using System;

namespace NinjaTrader.Cbi
{
    /// <summary>
    /// The Account class can be used to subscribe to account related events as well as accessing account related information.
    /// </summary>
    public class Account : ISnapShotSerializable
    {
        public void SnapShotPersist(bool updateVersion)
        {
            throw new System.NotImplementedException();
        }

        public void AccountItemUpdateCallback(
          AccountItem itemType,
          Currency currency,
          double value,
          DateTime time)
        {
        }

        public void PositionUpdateCallback(
          Instrument instrument,
          MarketPosition marketPosition,
          int quantity,
          double averagePrice,
          Operation operation)
        {
        }

    }
}