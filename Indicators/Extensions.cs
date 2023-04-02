#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
using NinjaTrader.NinjaScript.Indicators;

#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript
{
    public static class Extensions
    {
        public static int ValueInPips(this ATR indicator, int barsAgo)
        {
            var value = (int)Math.Round(indicator[barsAgo] * 10000, 0);
            return value;
        }

        public static void DrawVerticalMarkerLine(this NinjaScriptBase script, MarketPosition? positionType = null)
        {
            var BarToLineOffsetInTicks = 100;
            var LineDashStyle = DashStyleHelper.Dot;
            var LINE_LENGTH_TICKS = 100000;
            var TAG_SUFFIX = "_VertLineAtTime";
            var tag = script.Time[0] + TAG_SUFFIX + "AboveBar";
            var startY = script.High[0] + BarToLineOffsetInTicks * script.TickSize;
            var endY = script.High[0] + LINE_LENGTH_TICKS * script.TickSize;

            var lineThickness = positionType == null ? 1 : 3;
            var brush = positionType == null ? Brushes.Blue : Brushes.Green;

            if (positionType == MarketPosition.Long || positionType == null)
                Draw.Line(script, tag, false, 0, startY, 0, endY, brush, LineDashStyle, lineThickness);

            tag = script.Time[0] + TAG_SUFFIX + "BelowBar";
            startY = 0;
            endY = script.Low[0] - BarToLineOffsetInTicks * script.TickSize;

            if (positionType == MarketPosition.Short || positionType == null)
                Draw.Line(script, tag, false, 0, startY, 0, endY, brush, LineDashStyle, lineThickness);
        }

        /// <summary>
        /// Format: dd.MM.yyyy HH:mm, e.g. 3.12.2020 14:00
        /// </summary>
        public static void BreakAtTimestamp(this NinjaScriptBase script, string timestamp)
        {
            var dateTime = timestamp.ToDateTime();

            if (script.Time[0] == dateTime)
                System.Diagnostics.Debugger.Break();
        }

        public static ConsoleOutputService ConsoleOutput(this NinjaScriptBase script)
        {
            return new ConsoleOutputService(script);
        }

        public static DateTime ToDateTime(this string text)
        {
            DateTime dateTime;

            var format = "d.M.yyyy HH:mm";
            var culture = CultureInfo.InvariantCulture;

            if (!DateTime.TryParseExact(text, format, culture, DateTimeStyles.None, out dateTime))
                throw new InvalidOperationException("Invalid timestamp string: " + text);

            return dateTime;
        }

        public class ConsoleOutputService
        {
            private Predicate<NinjaScriptBase> _predicate;
            private readonly NinjaScriptBase _script;

            private bool CanExecute
            {
                get
                {
                    if (_predicate == null)
                        return true;

                    var canExecute = _predicate.Invoke(_script);

                    return canExecute;
                }
            }

            public ConsoleOutputService(NinjaScriptBase script)
            {
                _script = script;
            }

            public ConsoleOutputService If(Predicate<NinjaScriptBase> predicate)
            {
                if (_predicate == null)
                {
                    _predicate = predicate;
                }
                else
                {
                    var previousPredicate = _predicate;
                    _predicate = x => previousPredicate(x) && predicate(x);
                }

                return this;
            }

            public ConsoleOutputService IfBetween(string startTimestamp, string endTimestamp)
            {
                var start = startTimestamp.ToDateTime();
                var end = endTimestamp.ToDateTime();
                return If(_ => _.Time[0] >= start).If(_ => _.Time[0] <= end);
            }

            public ConsoleOutputService IfAfter(string timestamp)
            {
                return If(_ => _.Time[0] > timestamp.ToDateTime());
            }

            public ConsoleOutputService IfBefore(string timestamp)
            {
                return If(_ => _.Time[0] < timestamp.ToDateTime());
            }

            public void PrintBar(int barsAgo)
            {
                if (!CanExecute)
                    return;

                var text =
                    "bar      " +
                    "time=" + _script.Time[barsAgo].ToString("dd.MM.yyyy HH:mm") + ",   " +
                    "O=" + _script.Open[barsAgo] + ",   " +
                    "H=" + _script.High[barsAgo] + ",   " +
                    "L=" + _script.Low[barsAgo] + ",   " +
                    "C=" + _script.Close[barsAgo];

                System.Diagnostics.Debug.Print(text);
            }

            public void Print(Position position)
            {
                if (!CanExecute)
                    return;

                var text =
                    "position " +
                    "time=" + _script.Time[0].ToString("dd.MM.yyyy HH:mm") + ",   " +
                    "type=" + position.MarketPosition + ",   " +
                    "price=" + position.AveragePrice + ",   " +
                    "quantity=" + position.Quantity;

                System.Diagnostics.Debug.Print(text);
            }

            public void Print(Order order)
            {
                if (!CanExecute)
                    return;

                var text =
                    "order    " +
                    "time=" + _script.Time[0].ToString("dd.MM.yyyy HH:mm") + ",   " +
                    "id=" + order.OrderId + ",   " +
                    "name=" + order.Name + ",   " +
                    "fromEntry=" + order.FromEntrySignal + ",   " +
                    "type=" + order.OrderType + ",   " +
                    "price=" + order.AverageFillPrice + ",   " +
                    "quantity=" + order.Quantity;

                System.Diagnostics.Debug.Print(text);
            }
        }
    }
    }
