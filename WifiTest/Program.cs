using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WifiTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IntPtr clientHandle = IntPtr.Zero;
            uint negotiatedVersion;

            try
            {
                var error = Interop.WlanOpenHandle(Interop.WLAN_CLIENT_VERSION_LONGHORN, IntPtr.Zero,
                    out negotiatedVersion,
                    out clientHandle);
                if (error != 0)
                {
                    Console.WriteLine("Wifi is not available");
                    Console.WriteLine("Press any key for exit...");
                    Console.ReadLine();
                    return;
                }

                IntPtr interfaceList;
                Interop.WlanEnumInterfaces(clientHandle, IntPtr.Zero, out interfaceList);
                WlanInterfaceInfoListHeader header =
                    (WlanInterfaceInfoListHeader) Marshal.PtrToStructure(interfaceList,
                        typeof(WlanInterfaceInfoListHeader));

                Int64 listIterator = interfaceList.ToInt64() + Marshal.SizeOf(header);

                for (int i = 0; i < header.numberOfItems; i++)
                {
                    WlanInterfaceInfo info =
                        (WlanInterfaceInfo) Marshal.PtrToStructure(new IntPtr(listIterator), typeof(WlanInterfaceInfo));
                    listIterator += Marshal.SizeOf(info);
                    IntPtr availableNetworks = IntPtr.Zero;
                    try
                    {
                        error = Interop.WlanGetAvailableNetworkList(clientHandle, info.interfaceGuid,
                            WlanGetAvailableNetworkFlags.IncludeAllManualHiddenProfiles, IntPtr.Zero, out availableNetworks);
                        if (error != 0)
                        {
                            continue;
                        }
                        WlanAvailableNetworkListHeader availNetListHeader = (WlanAvailableNetworkListHeader)Marshal.PtrToStructure(availableNetworks, typeof(WlanAvailableNetworkListHeader));
                        long availNetListIt = availableNetworks.ToInt64() + Marshal.SizeOf(typeof(WlanAvailableNetworkListHeader));			
                        for (int j = 0; j < availNetListHeader.numberOfItems; j++)
                        {
                            var availableNetwork = (WlanAvailableNetwork)Marshal.PtrToStructure(new IntPtr(availNetListIt), typeof(WlanAvailableNetwork));
                            availNetListIt += Marshal.SizeOf(typeof(WlanAvailableNetwork));
                            var name = new String(Encoding.ASCII.GetChars(availableNetwork.dot11Ssid.SSID, 0,
                                (int) availableNetwork.dot11Ssid.SSIDLength));
                            
                            Console.WriteLine($"Network with SSID {name} and signal quality {availableNetwork.wlanSignalQuality}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        if (availableNetworks != IntPtr.Zero)
                        {
                            Interop.WlanFreeMemory(availableNetworks);
                        }
                    }
                }

                Interop.WlanFreeMemory(interfaceList);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (clientHandle != IntPtr.Zero)
                {
                    Interop.WlanCloseHandle(clientHandle, IntPtr.Zero);
                }
            }
            Console.WriteLine("Press any key for exit...");
            Console.ReadLine();
        }
    }
}