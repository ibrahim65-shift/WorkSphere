using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;
using WrokSphere_Shared;

namespace WorkSphere_DataAccess
{
    public class clsRolePermissionsData
    {
        public static async Task<clsRolePermissionsEntity> GetRolePermissionsByRolePermIDAsync(int rolePermID)
        {
            string query = "Select * From RolePermissions Where RolePermissionID=@rolePermID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@rolePermID", SqlDbType.Int).Value = rolePermID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return _MapToRolePermissionsEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetRolePermissionsByRolePermIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetRolePermissionsByRolePermIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<List<clsRolePermissionsEntity>> GetListRolePermissionsByRoleIDAsync(int roleID)
        {
            string query = "Select * From RolePermissions Where RoleID=@roleID;";
            var list = new List<clsRolePermissionsEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@roleID", SqlDbType.Int).Value = roleID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToRolePermissionsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetListRolePermissionsByRoleIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetListRolePermissionsByRoleIDAsync (General)", ex);
            }

            return list;
        }
        public static async Task<List<clsRolePermissionsEntity>> GetListRolePermissionsBypermIDAsync(int permID)
        {
            string query = "Select * From RolePermissions Where PermissionID=@permID;";
            var list = new List<clsRolePermissionsEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@permID", SqlDbType.Int).Value = permID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToRolePermissionsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetListRolePermissionsBypermIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetListRolePermissionsBypermIDAsync (General)", ex);
            }

            return list;
        }
        public static async Task<int> AddNewAsync(clsRolePermissionsEntity entity)
        {
            string query = @"insert into RolePermissions(RoleID,PermissionID,IsAllowed)
             values (@RoleID,@PermissionID,@IsAllowed);
             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = entity.RoleID;
                    command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = entity.PermissionID;
                    command.Parameters.Add("@IsAllowed", SqlDbType.Bit).Value = entity.IsAllowed;
                    await connection.OpenAsync().ConfigureAwait(false);

                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    if (result != null && int.TryParse(result.ToString(), out int newID))
                        return newID;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsRolePermissionsEntity entity)
        {
            string query = @"Update RolePermissions
             set RoleID=@RoleID , PermissionID=@PermissionID, IsAllowed=@IsAllowed 
             Where RolePermissionID=@RolePermissionID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@RolePermissionID", SqlDbType.Int).Value = entity.RolePermissionID;
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = entity.RoleID;
                    command.Parameters.Add("@PermissionID", SqlDbType.Int).Value = entity.PermissionID;
                    command.Parameters.Add("@IsAllowed", SqlDbType.Bit).Value = entity.IsAllowed;
                    await connection.OpenAsync().ConfigureAwait(false);

                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int rolePermID)
        {
            string query = "Delete RolePermissions Where RolePermissionID=@rolePermID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@rolePermID", SqlDbType.Int).Value = rolePermID;
                    await connection.OpenAsync().ConfigureAwait(false);
                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<DataTable> GetAllRolePermissionInfoAsync()
        {
            string query = "Select * From vw_RolePermissions";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetAllRolePermissionInfoAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetAllRolePermissionInfoAsync (General)", ex);
            }

            return dt;
        }
        public static async Task<(DataTable dt , int TotalPages)> GetRolePermissionsPageAsync(int PageNumber ,int PageSize , string searchText=null)
        {
            int totalPages = 0;
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using(SqlCommand command = new SqlCommand("SP_GetRolePermissionsPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value= PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value= PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar,200).Value=string.IsNullOrWhiteSpace(searchText)?(object)DBNull.Value : searchText;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using(SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                    }

                    if(dt.Rows.Count>0)
                    {
                        totalPages = (int)Math.Ceiling(Convert.ToInt32(dt.Rows[0]["TotalCount"]) / (double)PageSize);
                    }

                    dt.Columns.Remove("TotalCount");
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetRolePermissionsPage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.GetRolePermissionsPage (General)", ex);
            }

            return (dt, totalPages);
        }
        public static async Task<bool> SaveRolePermissionsBulkAsync(int roleID, List<clsRolePermissionItem> permissions)
        {
            try
            {
                string query = "SP_SaveRolePermissionsBulk";

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;

                    DataTable permissionsTable = new DataTable();

                    permissionsTable.Columns.Add("PermissionID", typeof(int));
                    permissionsTable.Columns.Add("IsAllowed", typeof(bool));

                    foreach (var perm in permissions)
                    {
                        permissionsTable.Rows.Add(perm.PermissionID, perm.IsAllowed);
                    }

                    SqlParameter permissionsParam = command.Parameters.AddWithValue("@Permissions", permissionsTable);

                    permissionsParam.SqlDbType = SqlDbType.Structured;
                    permissionsParam.TypeName = "RolePermissionListType";

                    await connection.OpenAsync().ConfigureAwait(false);

                    int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.SaveRolePermissionsBulkAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.SaveRolePermissionsBulkAsync (General)", ex);
                return false;
            }

        }
        public static async Task<bool> IsRoleAlreadyHasThisPermissionAsync(int roleID, int permID)
        {
            string query = "Select COUNT(1) From RolePermissions Where RoleID=@roleID and PermissionID=@premID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@roleID", SqlDbType.Int).Value = roleID;
                    command.Parameters.Add("@premID", SqlDbType.Int).Value = permID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    int count = Convert.ToInt32(result ?? 0);
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.IsRoleAlreadyHasThisPermissionAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolePermissionsData.IsRoleAlreadyHasThisPermissionAsync (General)", ex);
                return false;
            }
        }
        private static clsRolePermissionsEntity _MapToRolePermissionsEntity(SqlDataReader reader)
        {
            int iRolePermissionID = reader.GetOrdinal("RolePermissionID");
            int iRoleID = reader.GetOrdinal("RoleID");
            int iPermissionID = reader.GetOrdinal("PermissionID");
            int iIsAllowed = reader.GetOrdinal("IsAllowed");

            return new clsRolePermissionsEntity
            {
                RolePermissionID = reader.GetInt32(iRolePermissionID),
                RoleID = reader.GetInt32(iRoleID),
                PermissionID = reader.GetInt32(iPermissionID),
                IsAllowed = reader.GetBoolean(iIsAllowed)
            };

        }
    }
}
