using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsEmployeePromotionsData
    {
        public static async Task<clsEmployeePromotionsEntity> GetEmployeePromotionsInfoByIDAsync(int ID)
        {
            string query = "select * From EmployeePromotions Where PromotionID=@ID;";

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
                                return _MapToEmployeePromotionsEntity(reader);

                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetEmployeePromotionsInfoByIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetEmployeePromotionsInfoByIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<List<clsEmployeePromotionsEntity>> GetEmployeePromotionsInfoByEmployeeIDAsync(int ID)
        {
            string query = "Select * From EmployeePromotions Where EmployeeID=@ID;";
            var list = new List<clsEmployeePromotionsEntity>();

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
                            while (await reader.ReadAsync().ConfigureAwait(false))
                                list.Add(_MapToEmployeePromotionsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetEmployeePromotionsInfoByEmployeeIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetEmployeePromotionsInfoByEmployeeIDAsync (General)", ex);
            }
            return list;
        }
        public static async Task<int> AddNewAsync(clsEmployeePromotionsEntity entity)
        {
            string query = @"insert into EmployeePromotions(EmployeeID,OldStepID,NewStepID,PromotionDate,Notes,CreatedByUserID)
             values (@EmployeeID,@OldStepID,@NewStepID,@PromotionDate,@Notes,@CreatedByUserID);
             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                        command.Parameters.Add("@OldStepID", SqlDbType.Int).Value = entity.OldStepID;
                        command.Parameters.Add("@NewStepID", SqlDbType.Int).Value = entity.NewStepID;
                        command.Parameters.Add("@PromotionDate", SqlDbType.Date).Value = entity.PromotionDate;
                        command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = (object)entity.Notes ?? DBNull.Value;
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
                clsEventLogger.LogException("clsEmployeePromotionsData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsEmployeePromotionsEntity entity)
        {
            string query = @"update EmployeePromotions

             set EmployeeID=@EmployeeID,
             OldStepID=@OldStepID,
             NewStepID=@NewStepID,
             PromotionDate=@PromotionDate,
             Notes=@Notes,
             CreatedByUserID=@CreatedByUserID
             
             Where PromotionID=@ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = entity.PromotionID;
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                        command.Parameters.Add("@OldStepID", SqlDbType.Int).Value = entity.OldStepID;
                        command.Parameters.Add("@NewStepID", SqlDbType.Int).Value = entity.NewStepID;
                        command.Parameters.Add("@PromotionDate", SqlDbType.Date).Value = entity.PromotionDate;
                        command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = (object)entity.Notes ?? DBNull.Value;
                        command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = entity.CreatedByUserID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From EmployeePromotions Where PromotionID=@ID;";

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
                clsEventLogger.LogException("clsEmployeePromotionsData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<List<clsEmployeePromotionsEntity>> GetAllEmployeePromotions()
        {
            string query = "Select * From EmployeePromotions";
            var list = new List<clsEmployeePromotionsEntity>();

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
                                list.Add(_MapToEmployeePromotionsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetAllEmployeePromotions (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetAllEmployeePromotions (General)", ex);
            }

            return list;
        }
        public static async Task<DataTable> GetFullEmployeeInfoPromotions()
        {
            string query = "Select * From vw_FullEmployeePromotions";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false) )
                        {
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetFullEmployeeInfoPromotions (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.GetFullEmployeeInfoPromotions (General)", ex);
            }

            return dt;
        }
        public static async Task<(DataTable dt , int TotalPages)> GetEmployeePromotionsPageAsync(int PageNumber , int PageSize , string searchText=null)
        {
            DataTable dt = new DataTable();
            int totalPages = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetEmployeePromotionsPage", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : searchText;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                        if (dt.Rows.Count > 0)
                        {
                            totalPages = (int)Math.Ceiling(Convert.ToInt32(dt.Rows[0]["TotalCount"]) / (double)PageSize);
                        }
                    }

                    dt.Columns.Remove("TotalCount");
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeePromotionsPageAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeePromotionsPageAsync (General)", ex);
            }

            return (dt, totalPages);
        }
        public static async Task<bool> HasEmployeeReachedStepAsync(int EmployeeID, int NewStepID)
        {
            string query = "Select Count(1) From EmployeePromotions Where EmployeeID =@EmployeeID and NewStepID=@NewStepID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmployeeID;
                        command.Parameters.Add("@NewStepID", SqlDbType.Int).Value = NewStepID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.HasEmployeeReachedStepAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeePromotionsData.HasEmployeeReachedStepAsync (General)", ex);
                return false;
            }
        }
        private static clsEmployeePromotionsEntity _MapToEmployeePromotionsEntity(SqlDataReader reader)
        {
            int iPromotionID = reader.GetOrdinal("PromotionID");
            int iEmployeeID = reader.GetOrdinal("EmployeeID");
            int iOldStepID = reader.GetOrdinal("OldStepID");
            int iNewStepID = reader.GetOrdinal("NewStepID");
            int iPromotionDate = reader.GetOrdinal("PromotionDate");
            int iNotes = reader.GetOrdinal("Notes");
            int iCreatedByUserID = reader.GetOrdinal("CreatedByUserID");

            return new clsEmployeePromotionsEntity
            {
                PromotionID = reader.IsDBNull(iPromotionID) ? 0 : reader.GetInt32(iPromotionID),
                EmployeeID = reader.IsDBNull(iEmployeeID) ? 0 : reader.GetInt32(iEmployeeID),
                OldStepID = reader.IsDBNull(iOldStepID) ? 0 : reader.GetInt32(iOldStepID),
                NewStepID = reader.IsDBNull(iNewStepID) ? 0 : reader.GetInt32(iNewStepID),
                PromotionDate = reader.IsDBNull(iPromotionDate) ? DateTime.MinValue : reader.GetDateTime(iPromotionDate),
                Notes = reader.IsDBNull(iNotes) ? null : reader.GetString(iNotes),
                CreatedByUserID = reader.IsDBNull(iCreatedByUserID) ? 0 : reader.GetInt32(iCreatedByUserID),
            };
        }
    }
}
