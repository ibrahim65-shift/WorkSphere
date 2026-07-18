using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkSphere_Buisness;

namespace WorkSphere.Global_Classes
{
    public static class clsUtil
    {
        private static readonly string RegistryKeyPath = @"Software\WorkSphere";
        private static readonly byte[] Entropy = Encoding.UTF8.GetBytes("Ibrahim-Mohammed-WorkSphere");

        public static bool RememberUserNameAndPassword(string userName, string password)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
                {
                    if (key == null)
                    {
                        // Event Loger
                        return false;
                    }

                    key.SetValue("Username", userName ?? string.Empty);

                    if (!string.IsNullOrEmpty(password.Trim()))
                    {
                        byte[] encryptedPassword = ProtectedData.Protect
                            (
                              Encoding.UTF8.GetBytes(password), Entropy, DataProtectionScope.CurrentUser
                            );

                        key.SetValue("Password", encryptedPassword, RegistryValueKind.Binary);
                    }
                    else
                    {
                        if (key.GetValue("Password") != null)
                            key.DeleteValue("Password", false);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUtil.RememberUserNameAndPassword (General)", ex);
                return false;
            }
        }
        public static bool GetStoredCredential(ref string userName, ref string password)
        {
            userName = "";
            password = "";

            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
                {
                    if (key == null)
                        return false;

                    object usernameValue = key.GetValue("Username");
                    if (usernameValue != null)
                        userName = usernameValue.ToString();

                    object passwordValue = key.GetValue("Password");

                    if (passwordValue is byte[] encrptyedPassword)
                    {
                        byte[] decrptyedPassword = ProtectedData.Unprotect
                            (
                              encrptyedPassword, Entropy, DataProtectionScope.CurrentUser
                            );

                        password = Encoding.UTF8.GetString(decrptyedPassword);
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUtil.GetStoredCredential (General)", ex);
                return false;
            }
        }
        public static void DeleteCredentialsFromRegistry()
        {
            try
            {
                using (var root = Registry.CurrentUser)
                {
                    using (var sub = root.OpenSubKey(RegistryKeyPath))
                    {
                        if (sub != null)
                        {
                            sub.DeleteSubKey(RegistryKeyPath, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUtil.DeleteCredentialsFromRegistry (General)", ex);
            }
        }
        public static async Task<bool> CheckDatabaseConnection()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["WorkSphereDB"].ConnectionString))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlCommand command = new SqlCommand("Select 1", connection))
                    {
                        var result = await command.ExecuteScalarAsync().ConfigureAwait(false);

                        return result?.ToString() == "1";
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUtil.CheckDatabaseConnection (General)", ex);
                return false;
            }
        }
        public static bool IsDataGridViewEmpty(DataGridView dataGridView)
        {
            return (dataGridView.Rows.Count > 0) ? false : true;
        }
        public static string NormalizeArabic(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            string normalized = text;

            // 1- توحيد الألف والهمزات
            normalized = normalized.Replace("أ", "ا")
                                   .Replace("إ", "ا")
                                   .Replace("آ", "ا");

            // 2- توحيد الهاء/التاء المربوطة
            normalized = normalized.Replace("ة", "ه");

            // 3- توحيد الياء/الألف المقصورة
            normalized = normalized.Replace("ى", "ي")
                                   .Replace("ي", "ي"); // للتأكيد فقط

            // 4- إزالة التشكيل (فتحة، ضمة، كسرة، تنوين، شدة، سكون …)
            string tashkeel = @"ًٌٍَُِّْ";
            foreach (char mark in tashkeel)
                normalized = normalized.Replace(mark.ToString(), "");

            // 5- إزالة المسافات الزائدة
            normalized = normalized.Trim();

            return normalized;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // 1) التحقق من الصياغة باستخدام MailAddress
                var addr = new MailAddress(email);
                if (addr.Address != email)
                    return false;

                // 2) Regex للتأكد من النطاق
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                                           RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (!emailRegex.IsMatch(email))
                    return false;

                // 3) التحقق من وجود الدومين (DNS check)
                string domain = email.Substring(email.IndexOf('@') + 1);
                try
                {
                    var hostEntry = Dns.GetHostEntry(domain);
                    if (hostEntry.AddressList.Length == 0)
                        return false;
                }
                catch (SocketException)
                {
                    return false; // الدومين غير موجود
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string FormatDate(object dateValue)
        {
            if (dateValue == DBNull.Value || dateValue == null)
                return string.Empty;

            // هذه الطريقة تعمل مع جميع الأنواع
            if (DateTime.TryParse(dateValue.ToString(), out DateTime date))
                return date.ToString("dd/MM/yyyy");

            return string.Empty;
        }
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            DataTable dt = new DataTable();

            if (list != null && list.Count > 0)
            {
                var properties = typeof(T).GetProperties();

                foreach (var prop in properties)
                    dt.Columns.Add(prop.Name);

                foreach (var item in list)
                    dt.Rows.Add(properties.Select(p => p.GetValue(item)).ToArray());
            }

            return dt;
        }
        public static bool AddNewSystemRecord(string ActionType, string title, string description)
        {
            clsSystemRecords record = new clsSystemRecords();
            record.ActionType = ActionType;
            record.DeviceName = Environment.MachineName;
            record.MachinID = clsMachineInfo.GetShortMachineId();
            record.Title = title;
            record.Description = description;
            record.CreatedDate = DateTime.Now;
            record.UserID = clsCurrentUser.User.UserID;

            if (record.Save())
            {
                return true;
            }

            return false;

        }
    }
}
