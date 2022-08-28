namespace NinjaTrader.Cbi
{
    public class Bracket
    {
        public int Quantity { get; set; }

        public double StopLoss { get; set; }

        public StopStrategy StopStrategy { get; set; }

        public double Target { get; set; }
    }
}