using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsDepartmentData
    {
        public static async Task<clsDepartmentEntity> GetDepartmentInfoByIDAsync(int ID)
        {
            string query = "Select * From Departments Where DepartmentID=@ID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return _MapToDepartmentEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetDepartmentInfoByIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetDepartmentInfoByIDAsync (General)", ex);
            }
            return null;
        }
        public static async Task<clsDepartmentEntity> GetDepartmentInfoByDepartmentNameAsync(string DepartmentName)
        {
            string query = "Select * From Departments Where DepartmentName=@DepartmentName";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = DepartmentName;
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return _MapToDepartmentEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetDepartmentInfoByDepartmentNameAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetDepartmentInfoByDepartmentNameAsync (General)", ex);
            }

            return null;
        }
        public static async Task<int> AddNewAsync(clsDepartmentEntity entity)
        {
            string query = @"insert into Departments(DepartmentName,Description,CreatedDate)
             Values (@DepartmentName,@Description,@CreatedDate);
             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = entity.DepartmentName;
                        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)entity.Description ?? DBNull.Value;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = entity.CreatedDate;

                        await connection.OpenAsync().ConfigureAwait(false);

                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        if (result != null && int.TryParse(result.ToString(), out int newID))
                        {
                            return newID;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.AddNewAsync (General)", ex);
            }
            return -1;
        }
        public static async Task<bool> UpdateAsync(clsDepartmentEntity entity)
        {
            string query = @"Update Departments

              set DepartmentName=@DepartmentName,
              Description=@Description,
              CreatedDate=@CreatedDate
              
              Where DepartmentID=@ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = entity.DepartmentID;
                        command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = entity.DepartmentName;
                        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)entity.Description ?? DBNull.Value;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = entity.CreatedDate;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From Departments Where DepartmentID=@ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.DeleteAsync (General)", ex);
                return false;
            }

        }
        public static async Task<List<clsDepartmentEntity>> GetAllDepartments()
        {
            string query = "Select * From Departments;";
            var list = new List<clsDepartmentEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync().ConfigureAwait(false);
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(_MapToDepartmentEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetAllDepartments (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetAllDepartments (General)", ex);
            }
            return list;
        }
        public static async Task<(List<clsDepartmentEntity> list , int TotalPages)> GetDepartmentsPageAsync(int PageNumber, int PageSize , string searchText=null)
        {
            int totalPages = 0;
            var list = new List<clsDepartmentEntity>();

            try
            {
                using(SqlConnection connection =new SqlConnection(clsDataAccessSettings.ConnectionString))
                using(SqlCommand command = new SqlCommand("SP_GetDepartmentsPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : searchText;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using(SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        int totalCount = 0;
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToDepartmentEntity(reader));

                            if (totalCount == 0)
                                totalCount = Convert.ToInt32(reader["TotalCount"]);
                        }

                        totalPages = (int)Math.Ceiling((double)totalCount / PageSize);
                    }
                }
            }
            catch(SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetDepartmentsPageAsync (SQL)", ex);
            }
            catch(Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetDepartmentsPageAsync (General)", ex);
            }

            return (list,totalPages);
        }
        public static async Task<bool> IsDepartmentExists(string DepartmentName)
        {
            string query = "Select Count(1) From Departments Where DepartmentName=@DepartmentName";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@DepartmentName", SqlDbType.NVarChar).Value = DepartmentName;

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        int count = Convert.ToInt32(result ?? 0);
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.IsDepartmentExists (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.IsDepartmentExists (General)", ex);
                return false;
            }
        }
        private static clsDepartmentEntity _MapToDepartmentEntity(SqlDataReader reader)
        {
            int iDepartmentID = reader.GetOrdinal("DepartmentID");
            int iDepartmentName = reader.GetOrdinal("DepartmentName");
            int iDescription = reader.GetOrdinal("Description");
            int iCreatedDate = reader.GetOrdinal("CreatedDate");

            return new clsDepartmentEntity
            {
                DepartmentID = reader.IsDBNull(iDepartmentID) ? 0 : reader.GetInt32(iDepartmentID),
                DepartmentName = reader.IsDBNull(iDepartmentName) ? "" : reader.GetString(iDepartmentName),
                Description = reader.IsDBNull(iDescription) ? null : reader.GetString(iDescription),
                CreatedDate = reader.IsDBNull(iCreatedDate) ? DateTime.MinValue : reader.GetDateTime(iCreatedDate)
            };
        }
    }
}
