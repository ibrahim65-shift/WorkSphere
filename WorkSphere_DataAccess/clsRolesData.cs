using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsRolesData
    {
        public static async Task<clsRolesEntity> GetRoleByroleIDAsync(int roleID)
        {
            string query = "Select * From Roles Where RoleID=@roleID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@roleID", SqlDbType.Int).Value = roleID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return _MapToRolesEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.GetRoleByroleIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.GetRoleByroleIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<clsRolesEntity> GetRoleByroleNameAsync(string roleName)
        {
            string query = "Select * From Roles Where RoleName=@roleName";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@roleName", SqlDbType.NVarChar).Value = roleName;
                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return _MapToRolesEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.GetRoleByroleNameAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.GetRoleByroleNameAsync (General)", ex);
            }

            return null;
        }
        public static async Task<int> AddNewAsync(clsRolesEntity entity)
        {
            string query = @"insert into Roles(RoleName,Description,IsActive)
             values (@RoleName,@Description,@IsActive);
             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = entity.RoleName;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)entity.Description ?? DBNull.Value;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;

                    await connection.OpenAsync().ConfigureAwait(false);

                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    if (result != null && int.TryParse(result.ToString(), out int newID))
                        return newID;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsRolesEntity entity)
        {
            string query = @"Update Roles
             set RoleName=@RoleName , Description=@Description , IsActive=@IsActive
             Where RoleID=@RoleID ";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@RoleID", SqlDbType.Int).Value = entity.RoleID;
                    command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = entity.RoleName;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)entity.Description ?? DBNull.Value;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;
                    await connection.OpenAsync().ConfigureAwait(false);

                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int roleID)
        {
            string query = "Delete Roles Where RoleID=@roleID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@roleID", SqlDbType.Int).Value = roleID;
                    await connection.OpenAsync().ConfigureAwait(false);

                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<List<clsRolesEntity>> GetAllAsync()
        {
            string query = "Select * From Roles;";
            var list = new List<clsRolesEntity>();

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
                            list.Add(_MapToRolesEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.GetAllAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.GetAllAsync (General)", ex);
            }

            return list;
        }
        public static async Task<(List<clsRolesEntity> list, int TotalPages)> GetRolesPageAsync(int PageNumber , int PageSize,string searchText=null)
        {
            int totalPages = 0;
            var list = new List<clsRolesEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetRolesPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar,200).Value = string.IsNullOrWhiteSpace(searchText)?(object)DBNull.Value:searchText;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        int totalCount = 0;
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToRolesEntity(reader));

                            if (totalCount == 0)
                            {
                                totalCount = Convert.ToInt32(reader["TotalCount"]);
                            }
                        }

                        totalPages = (int)Math.Ceiling((double)totalCount/PageSize);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.GetRolesPage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.GetRolesPage (General)", ex);
            }

            return (list,totalPages);
        }
        public static async Task<bool> IsRoleNameExists(string roleName)
        {
            string query = "Select COUNT(1) From Roles Where RoleName=@roleName;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@roleName", SqlDbType.NVarChar).Value = roleName;
                    await connection.OpenAsync().ConfigureAwait(false);

                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    int count = Convert.ToInt32(result ?? 0);
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsRolesData.IsRoleNameExists (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsRolesData.IsRoleNameExists (General)", ex);
                return false;
            }
        }
        private static clsRolesEntity _MapToRolesEntity(SqlDataReader reader)
        {
            int iRoleID = reader.GetOrdinal("RoleID");
            int iRoleName = reader.GetOrdinal("RoleName");
            int iDescription = reader.GetOrdinal("Description");
            int iIsActive = reader.GetOrdinal("IsActive");

            return new clsRolesEntity
            {
                RoleID = reader.GetInt32(iRoleID),
                RoleName = reader.GetString(iRoleName),
                Description = reader.IsDBNull(iDescription) ? null : reader.GetString(iDescription),
                IsActive = reader.GetBoolean(iIsActive)
            };

        }
    }
}
