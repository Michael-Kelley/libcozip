using System;
using System.Runtime.InteropServices;

namespace COZip {
	public static class Zlib {
		public enum CompressionMethod {
			Deflated = 8
		}

		public enum CompressionStrategy {
			Default
		}

		public enum Result {
			VersionError = -6,
			BufError,
			MemError,
			DataError,
			StreamError,
			ErrNo,
			OK,
			StreamEnd,
			NeedDict
		}

		public const int MAXWBITS = 15;
		public const int ZFINISH = 4;
		public const int ZNOFLUSH = 0;

		[StructLayout(LayoutKind.Sequential)]
		public struct ZStream {
			public IntPtr NextIn;
			public uint AvailIn;
			public uint TotalIn;
			public IntPtr NextOut;
			public uint AvailOut;
			public uint TotalOut;
			public IntPtr Msg;
			public IntPtr State;
			public IntPtr ZAlloc;
			public IntPtr ZFree;
			public IntPtr Opaque;
			public int DataType;
			public uint Adler;
			public uint Reserved;
		}

		[DllImport("zlib1.dll", EntryPoint = "deflateInit2_", CallingConvention = CallingConvention.Cdecl)]
		public static extern int DeflateInit2(ref ZStream strm, int level, CompressionMethod method, int windowBits, int memLevel, CompressionStrategy strategy, IntPtr version, int streamSize);

		[DllImport("zlib1.dll", EntryPoint = "deflate", CallingConvention = CallingConvention.Cdecl)]
		public static extern int Deflate(ref ZStream strm, int flush);

		[DllImport("zlib1.dll", EntryPoint = "deflateEnd", CallingConvention = CallingConvention.Cdecl)]
		public static extern int DeflateEnd(ref ZStream strm);

		[DllImport("zlib1.dll", EntryPoint = "inflateInit2_", CallingConvention = CallingConvention.Cdecl)]
		public static extern int InflateInit2(ref ZStream strm, int windowBits, IntPtr version, int streamSize);

		[DllImport("zlib1.dll", EntryPoint = "inflate", CallingConvention = CallingConvention.Cdecl)]
		public static extern int Inflate(ref ZStream strm, int flush);

		[DllImport("zlib1.dll", EntryPoint = "inflateEnd", CallingConvention = CallingConvention.Cdecl)]
		public static extern int InflateEnd(ref ZStream strm);

		[DllImport("zlib1.dll", EntryPoint = "zlibVersion", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Version();
	}
}