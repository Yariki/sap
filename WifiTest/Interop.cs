using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WifiTest
{
    public static class Interop
    {
        public const uint WLAN_CLIENT_VERSION_XP_SP2 = 1;
		public const uint WLAN_CLIENT_VERSION_LONGHORN = 2;
 
	    
		#region [functions]	    
	    
		[DllImport("wlanapi.dll")]
		public static extern int WlanOpenHandle(
			[In] UInt32 clientVersion,
			[In, Out] IntPtr pReserved,
			[Out] out UInt32 negotiatedVersion,
			[Out] out IntPtr clientHandle);

		[DllImport("wlanapi.dll")]
		public static extern int WlanCloseHandle(
			[In] IntPtr clientHandle,
			[In, Out] IntPtr pReserved);

		[DllImport("wlanapi.dll")]
		public static extern int WlanEnumInterfaces(
			[In] IntPtr clientHandle,
			[In, Out] IntPtr pReserved,
			[Out] out IntPtr ppInterfaceList);

		[DllImport("wlanapi.dll")]
		public static extern int WlanQueryInterface(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] WlanIntfOpcode opCode,
			[In, Out] IntPtr pReserved,
			[Out] out int dataSize,
			[Out] out IntPtr ppData,
			[Out] out WlanOpcodeValueType wlanOpcodeValueType);

		[DllImport("wlanapi.dll")]
		public static extern int WlanSetInterface(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] WlanIntfOpcode opCode,
			[In] uint dataSize,
			[In] IntPtr pData,
			[In, Out] IntPtr pReserved);

		/// <param name="pDot11Ssid">Not supported on Windows XP SP2: must be a <c>null</c> reference.</param>
		/// <param name="pIeData">Not supported on Windows XP SP2: must be a <c>null</c> reference.</param>
		[DllImport("wlanapi.dll")]
		public static extern int WlanScan(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] IntPtr pDot11Ssid,
			[In] IntPtr pIeData,
			[In, Out] IntPtr pReserved);

		[DllImport("wlanapi.dll")]
		public static extern int WlanGetAvailableNetworkList(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] WlanGetAvailableNetworkFlags flags,
			[In, Out] IntPtr reservedPtr,
			[Out] out IntPtr availableNetworkListPtr);


		[DllImport("wlanapi.dll")]
		public static extern int WlanSetProfile(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] WlanProfileFlags flags,
			[In, MarshalAs(UnmanagedType.LPWStr)] string profileXml,
			[In, Optional, MarshalAs(UnmanagedType.LPWStr)] string allUserProfileSecurity,
			[In] bool overwrite,
			[In] IntPtr pReserved,
			[Out] out WlanReasonCode reasonCode);

		/// <param name="flags">Not supported on Windows XP SP2: must be a <c>null</c> reference.</param>
		[DllImport("wlanapi.dll")]
		public static extern int WlanGetProfile(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In, MarshalAs(UnmanagedType.LPWStr)] string profileName,
			[In] IntPtr pReserved,
			[Out] out IntPtr profileXml,
			[Out, Optional] out WlanProfileFlags flags,
			[Out, Optional] out WlanAccess grantedAccess);

		[DllImport("wlanapi.dll")]
		public static extern int WlanGetProfileList(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] IntPtr pReserved,
			[Out] out IntPtr profileList
		);

		[DllImport("wlanapi.dll")]
		public static extern void WlanFreeMemory(IntPtr pMemory);

		[DllImport("wlanapi.dll")]
		public static extern int WlanReasonCodeToString(
			[In] WlanReasonCode reasonCode,
			[In] int bufferSize,
			[In, Out] StringBuilder stringBuffer,
			IntPtr pReserved
		);		

		/// <summary>
		/// Defines the callback function which accepts WLAN notifications.
		/// </summary>
		public delegate void WlanNotificationCallbackDelegate(ref WlanNotificationData notificationData, IntPtr context);

		[DllImport("wlanapi.dll")]
		public static extern int WlanRegisterNotification(
			[In] IntPtr clientHandle,
			[In] WlanNotificationSource notifSource,
			[In] bool ignoreDuplicate,
			[In] WlanNotificationCallbackDelegate funcCallback,
			[In] IntPtr callbackContext,
			[In] IntPtr reserved,
			[Out] out WlanNotificationSource prevNotifSource);
		
		[DllImport("wlanapi.dll")]
		public static extern int WlanConnect(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] ref WlanConnectionParameters connectionParameters,
			IntPtr pReserved);

		[DllImport("wlanapi.dll")]
		public static extern int WlanDeleteProfile(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In, MarshalAs(UnmanagedType.LPWStr)] string profileName,
			IntPtr reservedPtr
		);

		[DllImport("wlanapi.dll")]
		public static extern int WlanDisconnect(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			IntPtr pReserved);

		[DllImport("wlanapi.dll")]
		public static extern int WlanGetNetworkBssList(
			[In] IntPtr clientHandle,
			[In, MarshalAs(UnmanagedType.LPStruct)] Guid interfaceGuid,
			[In] IntPtr dot11SsidInt,
			[In] Dot11BssType dot11BssType,
			[In] bool securityEnabled,
			IntPtr reservedPtr,
			[Out] out IntPtr wlanBssList
		);
	    
	    #endregion
	    
    }
}