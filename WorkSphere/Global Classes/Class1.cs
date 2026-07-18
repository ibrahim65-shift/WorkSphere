using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;

public static class clsMachineInfo
{
    public static string GetShortMachineId()
    {
        string cpuId = GetCpuId();
        string diskId = GetDiskId();
        string machineName = Environment.MachineName;

        // نجمع البيانات
        string rawId = cpuId + diskId + machineName;

        // نعمل Hash بالـ MD5 (أقصر من SHA256)
        using (MD5 md5 = MD5.Create())
        {
            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(rawId));

            // ناخذ أول 16 رقم (hex)
            return BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, 16);
        }
    }

    private static string GetCpuId()
    {
        try
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select ProcessorId from Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["ProcessorId"]?.ToString() ?? string.Empty;
                }
            }
        }
        catch { }
        return string.Empty;
    }

    private static string GetDiskId()
    {
        try
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_DiskDrive"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["SerialNumber"]?.ToString() ?? string.Empty;
                }
            }
        }
        catch { }
        return string.Empty;
    }
}