using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsPermissionsData
    {
        public static async Task<clsPermissionsEntity> GetPermissionByPermissionIDAsync(int permID)
        {
            string query = "Select * From Permissions Where PermissionID = @permID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@permID", SqlDbType.Int).Value = permID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return _MapToPermissionsEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.GetPermissionByPermissionIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.GetPermissionByPermissionIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<clsPermissionsEntity> GetPermissionByPermissionCodeAsync(string permCode)
        {
            string query = "Select * From Permissions Where PermissionCode = @permCode;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@permCode", SqlDbType.NVarChar).Value = permCode;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return _MapToPermissionsEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.GetPermissionByPermissionCodeAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.GetPermissionByPermissionCodeAsync (General)", ex);
            }

            return null;
        }
        public static async Task<int> AddNewAsync(clsPermissionsEntity entity)
        {
            string query = @"
             Insert into Permissions(PermissionCode,PermissionName)
             Values(@PermissionCode,@PermissionName);
             Select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@PermissionCode", SqlDbType.NVarChar).Value = entity.PermissionCode;
                    command.Parameters.Add("@PermissionName", SqlDbType.NVarChar).Value = entity.PermissionName;
                    await connection.OpenAsync().ConfigureAwait(false);

                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    if (result != null && int.TryParse(result.ToString(), out int newID))
                        return newID;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsPermissionsEntity entity)
        {
            string query = @"update Permissions
            Set PermissionCode=@PermissionCode , PermissionName=@PermissionName
            Where PermissionID = @PermissionID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = entity.PermissionID;
                    command.Parameters.Add("@PermissionCode", SqlDbType.NVarChar).Value = entity.PermissionCode;
                    command.Parameters.Add("@PermissionName", SqlDbType.NVarChar).Value = entity.PermissionName;
                    await connection.OpenAsync().ConfigureAwait(false);

                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return (rows > 0);
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int permID)
        {
            string query = @"Delete Permissions Where PermissionID=@PermissionID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = permID;

                    await connection.OpenAsync().ConfigureAwait(false);

                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return (rows > 0);
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<List<clsPermissionsEntity>> GetAllAsync()
        {
            string query = "Select * From Permissions";
            var list = new List<clsPermissionsEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToPermissionsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.GetAllAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.GetAllAsync (General)", ex);
            }

            return list;
        }
        public static async Task<bool> IsPermissionCodeExists(string permissionCode)
        {
            string query = "Select COUNT(1) From Permissions Where PermissionCode=@permCode;";
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@permCode", SqlDbType.NVarChar).Value = permissionCode;
                    await connection.OpenAsync().ConfigureAwait(false);
                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    int count = Convert.ToInt32(result ?? 0);
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsPermissionsData.IsPermissionCodeExists (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsPermissionsData.IsPermissionCodeExists (General)", ex);
                return false;
            }
        }
        private static clsPermissionsEntity _MapToPermissionsEntity(SqlDataReader reader)
        {
            int iPermissionID = reader.GetOrdinal("PermissionID");
            int iPermissionCode = reader.GetOrdinal("PermissionCode");
            int iPermissionName = reader.GetOrdinal("PermissionName");

            return new clsPermissionsEntity
            {
                PermissionID = reader.GetInt32(iPermissionID),
                PermissionCode = reader.GetString(iPermissionCode),
                PermissionName = reader.GetString(iPermissionName),
            };
        }
    }
}
