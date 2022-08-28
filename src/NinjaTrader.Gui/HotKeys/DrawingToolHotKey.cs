using System;
using NinjaTrader.Gui.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Serialization;

namespace NinjaTrader.Gui.HotKeys
{
    public class DrawingToolHotKey : NotifyPropertyChangedBase
    {
        private KeyGesture keyGesture;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DrawingToolHotKey Clone() => (DrawingToolHotKey)null;

        public DrawingToolHotKey()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public DrawingToolHotKey(string fullTypeName, KeyGesture keyGesture)
        {
        }

        [Browsable(false)]
        public string FullTypeName { get; set; }

        [XmlIgnore]
        public KeyGesture KeyGesture
        {
            get => this.keyGesture;
            [MethodImpl(MethodImplOptions.NoInlining)]
            set
            {
            }
        }

        public string KeyGestureSerialize
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static DrawingToolHotKey()
        {
        }
    }
}