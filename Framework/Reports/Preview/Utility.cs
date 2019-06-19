using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace KyoeiSystem.Framework.Common
{
	/// <summary>
	/// ウィンドウハンドルを列挙するクラスの共通機能
	/// </summary>
	public abstract class WindowHandles : IEnumerable<IntPtr>
	{
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal delegate bool EnumWindowsProcDelegate(IntPtr windowHandle, IntPtr lParam);

		internal List<IntPtr> handles;

		public WindowHandles()
		{
			handles = new List<IntPtr>();
		}

		public IEnumerator<IntPtr> GetEnumerator()
		{
			return handles.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return handles.GetEnumerator();
		}

		internal bool EnumWindowProc(IntPtr handle, IntPtr lParam)
		{
			handles.Add(handle);
			return true;
		}
	}

	/// <summary>
	/// トップレベルウィンドウのウィンドウハンドルを列挙する機能を提供します。
	/// </summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public sealed class TopLevelWindowHandles : WindowHandles
	{
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool EnumWindows(
				[MarshalAs(UnmanagedType.FunctionPtr)] EnumWindowsProcDelegate enumProc,
				IntPtr lParam);
		}

		/// <summary>
		/// トップレベルウィンドウのウィンドウハンドルを列挙します。
		/// </summary>
		public TopLevelWindowHandles()
			: base()
		{
			handles = new List<IntPtr>();
			NativeMethods.EnumWindows(EnumWindowProc, default(IntPtr));
		}
	}

	/// <summary>
	/// 親ウィンドウの子ウィンドウのウィンドウハンドルを列挙する機能を提供します。
	/// </summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public sealed class ChildWindowHandles : WindowHandles
	{
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool EnumChildWindows(
				IntPtr handle,
				[MarshalAs(UnmanagedType.FunctionPtr)] EnumWindowsProcDelegate enumProc,
				IntPtr lParam);
		}

		IntPtr windowHandle;

		/// <summary>
		/// 親ウィンドウの子ウィンドウのウィンドウハンドルを取得します。
		/// </summary>
		/// <param name="windowHandle">親ウィンドウのウィンドウハンドルです。</param>
		public ChildWindowHandles(IntPtr windowHandle)
			: base()
		{
			this.windowHandle = windowHandle;
			NativeMethods.EnumChildWindows(windowHandle, EnumWindowProc, default(IntPtr));
		}

		/// <summary>
		/// 親ウィンドウのウィンドウハンドルです。
		/// </summary>
		public IntPtr WindowHandle { get { return windowHandle; } }
	}

	/// <summary>
	/// スレッドに所属するトップレベルウィンドウのウィンドウハンドルを列挙する機能を提供します。
	/// </summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public sealed class ThreadWindowHandles : WindowHandles
	{
		[SuppressUnmanagedCodeSecurity]
		private static class NativeMethods
		{
			[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool EnumThreadWindows(
				uint threadId,
				[MarshalAs(UnmanagedType.FunctionPtr)] EnumWindowsProcDelegate enumProc,
				IntPtr lParam);
		}

		uint threadId;

		/// <summary>
		/// スレッドに所属するトップレベルウィンドウのウィンドウハンドルを取得します。
		/// </summary>
		/// <param name="threadId">スレッドIDです。</param>
		public ThreadWindowHandles(uint threadId)
			: base()
		{
			this.threadId = threadId;
			NativeMethods.EnumThreadWindows(threadId, EnumWindowProc, default(IntPtr));
		}

		/// <summary>
		/// スレッドIDです。
		/// </summary>
		public uint ThreadID { get { return threadId; } }
	}
}
