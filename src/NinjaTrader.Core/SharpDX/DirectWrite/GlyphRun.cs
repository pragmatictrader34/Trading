using System;
using System.Runtime.InteropServices;

// ReSharper disable CheckNamespace

namespace SharpDX.DirectWrite
{
    public class GlyphRun : IDisposable
    {
        internal IntPtr FontFacePointer;
        public float FontSize;
        internal int GlyphCount;
        internal IntPtr GlyphIndicesPointer;
        internal IntPtr GlyphAdvancesPointer;
        internal IntPtr GlyphOffsetsPointer;
        public bool IsSideways;
        public int BidiLevel;

        public short[] Indices { get; set; }

        public float[] Advances { get; set; }

        internal void __MarshalFree(ref GlyphRun.__Native @ref) => @ref.__MarshalFree();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal struct __Native
        {
            public IntPtr FontFace;
            public float FontEmSize;
            public int GlyphCount;
            public IntPtr GlyphIndices;
            public IntPtr GlyphAdvances;
            public IntPtr GlyphOffsets;
            public int BidiLevel;

            internal void __MarshalFree()
            {
                if (this.GlyphIndices != IntPtr.Zero)
                    Marshal.FreeHGlobal(this.GlyphIndices);
                if (this.GlyphAdvances != IntPtr.Zero)
                    Marshal.FreeHGlobal(this.GlyphAdvances);
                if (!(this.GlyphOffsets != IntPtr.Zero))
                    return;
                Marshal.FreeHGlobal(this.GlyphOffsets);
            }
        }
    }
}