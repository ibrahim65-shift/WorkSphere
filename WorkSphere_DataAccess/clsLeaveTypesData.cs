using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsLeaveTypesData
    {
        public static async Task<clsLeaveTypesEntity> GetLeaveTypesInfoByIDAsync(int ID)
        {
            string query = "Select * From LeaveTypes Where LeaveTypeID =@ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return _MapToLeaveTypesEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.GetLeaveTypesInfoByIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.GetLeaveTypesInfoByIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<clsLeaveTypesEntity> GetLeaveTypesInfoByNameAsync(string Name)
        {
            string query = "Select * From LeaveTypes Where Name =@Name;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return _MapToLeaveTypesEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.GetLeaveTypesInfoByNameAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.GetLeaveTypesInfoByNameAsync (General)", ex);
            }

            return null;
        }
        public static async Task<int> AddNewAsync(clsLeaveTypesEntity entity)
        {
            string query = @"insert into LeaveTypes (Name,Description,MaxDaysPerYear,IsPaid,CreatedDate,CreatedByUserID)
            values (@Name,@Description,@MaxDaysPerYear,@IsPaid,@CreatedDate,@CreatedByUserID);
            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
                        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)entity.Description ?? DBNull.Value;
                        command.Parameters.Add("@MaxDaysPerYear", SqlDbType.Int).Value = (object)entity.MaxDaysPerYear ?? DBNull.Value;
                        command.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = entity.IsPaid;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                        command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = entity.CreatedByUserID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        if (result != null && int.TryParse(result.ToString(), out int newID))
                            return newID;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsLeaveTypesEntity entity)
        {
            string query = @"
              Update LeaveTypes
              
              set Name = @Name, 
              Description = @Description, 
              MaxDaysPerYear = @MaxDaysPerYear ,
              IsPaid = @IsPaid 
              Where LeaveTypeID=@LeaveTypeID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = entity.LeaveTypeID;
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
                        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = (object)entity.Description ?? DBNull.Value;
                        command.Parameters.Add("@MaxDaysPerYear", SqlDbType.Int).Value = (object)entity.MaxDaysPerYear ?? DBNull.Value;
                        command.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = entity.IsPaid;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.UpdateAsync (General)", ex);
                return false;
            }

        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From LeaveTypes Where LeaveTypeID =@LeaveTypeID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = ID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<List<clsLeaveTypesEntity>> GetAllAsync()
        {
            string query = "Select * From LeaveTypes";
            var list = new List<clsLeaveTypesEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(_MapToLeaveTypesEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.GetAllAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.GetAllAsync (General)", ex);
            }

            return list;
        }
        public static async Task<(List<clsLeaveTypesEntity> list, int TotalPages)> GetLeaveTypesPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            int totalPages = 0;
            var list = new List<clsLeaveTypesEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetLeaveTypesPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : searchText;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        int totalCount = 0;
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToLeaveTypesEntity(reader));

                            if (totalCount == 0)
                                totalCount = Convert.ToInt32(reader["TotalCount"]);
                        }

                        totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetLeaveTypesPageAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsDepartmentData.GetLeaveTypesPageAsync (General)", ex);
            }

            return (list, totalPages);
        }
        public static async Task<bool> IsLeavePaidAsync(int LeaveTypeID)
        {
            string query = "Select Count(1) From LeaveTypes Where LeaveTypeID=@LeaveTypeID and IsPaid=1;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = LeaveTypeID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.IsLeavePaidAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.IsLeavePaidAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> IsLeaveTypeNameExistsAsync(string LeaveTypeName)
        {
            string query = "Select Count(1) From LeaveTypes Where Name=@Name";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = LeaveTypeName;

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.IsLeaveTypeNameExistsAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsLeaveTypesData.IsLeaveTypeNameExistsAsync (General)", ex);
                return false;
            }
        }

        private static clsLeaveTypesEntity _MapToLeaveTypesEntity(SqlDataReader reader)
        {
            int iLeaveTypeID = reader.GetOrdinal("LeaveTypeID");
            int iName = reader.GetOrdinal("Name");
            int iDescription = reader.GetOrdinal("Description");
            int iMaxDaysPerYear = reader.GetOrdinal("MaxDaysPerYear");
            int iIsPaid = reader.GetOrdinal("IsPaid");
            int iCreatedDate = reader.GetOrdinal("CreatedDate");
            int iCreatedByUserID = reader.GetOrdinal("CreatedByUserID");

            return new clsLeaveTypesEntity
            {
                LeaveTypeID = reader.IsDBNull(iLeaveTypeID) ? 0 : reader.GetInt32(iLeaveTypeID),
                Name = reader.IsDBNull(iName) ? null : reader.GetString(iName),
                Description = reader.IsDBNull(iDescription) ? null : reader.GetString(iDescription),
                MaxDaysPerYear = reader.IsDBNull(iMaxDaysPerYear) ? (int?)null : reader.GetInt32(iMaxDaysPerYear),
                IsPaid = reader.IsDBNull(iIsPaid) ? false : reader.GetBoolean(iIsPaid),
                CreatedDate = reader.IsDBNull(iCreatedDate) ? DateTime.Now : reader.GetDateTime(iCreatedDate),
                CreatedByUserID = reader.IsDBNull(iCreatedByUserID) ? 0 : reader.GetInt32(iCreatedByUserID)

            };
        }
    }
}
