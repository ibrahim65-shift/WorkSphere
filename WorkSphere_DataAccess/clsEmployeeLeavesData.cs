using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsEmployeeLeavesData
    {
        public static async Task<clsEmployeeLeavesEntity> GetEmployeeLeavesInfoByLeaveIDAsync(int LeaveID)
        {
            string query = "Select * From EmployeeLeaves Where LeaveID=@LeaveID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveID", SqlDbType.Int).Value = LeaveID;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                return _MapToEmployeeLeavesEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetEmployeeLeavesInfoByLeaveIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetEmployeeLeavesInfoByLeaveIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<List<clsEmployeeLeavesEntity>> GetEmployeeLeavesInfoByEmployeeIDAsync(int EmpID)
        {
            string query = "Select * From EmployeeLeaves Where EmployeeID=@EmployeeID";
            var list = new List<clsEmployeeLeavesEntity>();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = EmpID;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(_MapToEmployeeLeavesEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetEmployeeLeavesInfoByEmployeeIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetEmployeeLeavesInfoByEmployeeIDAsync (General)", ex);
            }

            return list;
        }
        public static async Task<List<clsEmployeeLeavesEntity>> GetEmployeeLeavesInfoByLeaveTypeIDAsync(int LeaveTyepID)
        {
            string query = "Select * From EmployeeLeaves Where LeaveTypeID=@LeaveTypeID";
            var list = new List<clsEmployeeLeavesEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = LeaveTyepID;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(_MapToEmployeeLeavesEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetEmployeeLeavesInfoByLeaveTypeIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetEmployeeLeavesInfoByLeaveTypeIDAsync (General)", ex);
            }

            return list;
        }
        public static async Task<int> AddNewAsync(clsEmployeeLeavesEntity entity)
        {
            string query = @"insert into EmployeeLeaves(EmployeeID,LeaveTypeID,StartDate,EndDate,Reason,Status
            ,ApprovedByUserID ,CreatedDate,CreatedByUserID,EditedDate,EditedByUserID)
            
            Values(@EmployeeID,@LeaveTypeID,@StartDate,@EndDate,@Reason,@Status,@ApprovedByUserID
            ,@CreatedDate,@CreatedByUserID,@EditedDate,@EditedByUserID);
            
            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = entity.LeaveTypeID;
                        command.Parameters.Add("@StartDate", SqlDbType.Date).Value = entity.StartDate;
                        command.Parameters.Add("@EndDate", SqlDbType.Date).Value = entity.EndDate;
                        command.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = (object)entity.Reason ?? DBNull.Value;
                        command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = entity.Status;
                        command.Parameters.Add("@ApprovedByUserID", SqlDbType.Int).Value = (object)entity.ApprovedByUserID ?? DBNull.Value;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;
                        command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = entity.CreatedByUserID;
                        command.Parameters.Add("@EditedDate", SqlDbType.DateTime).Value = (object)entity.EditedDate ?? DBNull.Value;
                        command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        if (result != null && int.TryParse(result.ToString(), out int newID))
                            return newID;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsEmployeeLeavesEntity entity)
        {
            string query = @"
             Update EmployeeLeaves 

             set EmployeeID=@EmployeeID , 
             LeaveTypeID =@LeaveTypeID ,
             StartDate =@StartDate ,
             EndDate =@EndDate ,
             Reason =@Reason ,
             Status =@Status ,
             ApprovedByUserID = @ApprovedByUserID,
             EditedDate=@EditedDate,
             EditedByUserID=@EditedByUserID 
             
             Where LeaveID=@LeaveID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveID", SqlDbType.Int).Value = entity.LeaveID;
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                        command.Parameters.Add("@LeaveTypeID", SqlDbType.Int).Value = entity.LeaveTypeID;
                        command.Parameters.Add("@StartDate", SqlDbType.Date).Value = entity.StartDate;
                        command.Parameters.Add("@EndDate", SqlDbType.Date).Value = entity.EndDate;
                        command.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = (object)entity.Reason ?? DBNull.Value;
                        command.Parameters.Add("@Status", SqlDbType.TinyInt).Value = entity.Status;
                        command.Parameters.Add("@ApprovedByUserID", SqlDbType.Int).Value = (object)entity.ApprovedByUserID ?? DBNull.Value;
                        command.Parameters.Add("@EditedDate", SqlDbType.DateTime).Value = (object)entity.EditedDate ?? DBNull.Value;
                        command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.UpdateAsync (General)", ex);
                return false;
            }

        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From EmployeeLeaves Where LeaveID=@LeaveID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@LeaveID", SqlDbType.Int).Value = ID;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<DataTable> GetAllAsync()
        {
            string query = "select * From  vw_EmployeeLeavesInfo;";
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
                clsEventLogger.LogException("clsEmployeeLeavesData.GetAllAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.GetAllAsync (General)", ex);
            }

            return dt;
        }
        public static async Task<(DataTable dt , int TotalPages)> GetEmployeeLeavesInfoPageAsync(int PageNumber , int PageSize , string searchText=null)
        {
            DataTable dt = new DataTable();
            int totalPages = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetEmployeeLeavesInfoPage", connection))
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
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeLeavesInfoPage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeLeavesInfoPage (General)", ex);
            }

            return (dt, totalPages);
        }
        public static async Task<bool> HasOverlappingLeaveAsync(int empID, DateTime startDate, DateTime endDate, int? excludeLeaveID = null)
        {
            const string query = @"
        SELECT COUNT(1)
        FROM EmployeeLeaves
        WHERE EmployeeID = @EmployeeID
          AND Status IN (1,2) -- 1:Pending, 2:Approved (considered blocking)
          AND @StartDate <= EndDate
          AND @EndDate   >= StartDate
          AND (@ExcludeLeaveID IS NULL OR LeaveID <> @ExcludeLeaveID);";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = empID;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime2).Value = startDate;
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime2).Value = endDate;
                    cmd.Parameters.Add("@ExcludeLeaveID", SqlDbType.Int).Value = (object)excludeLeaveID ?? DBNull.Value;

                    await conn.OpenAsync().ConfigureAwait(false);
                    object result = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                    int cnt = Convert.ToInt32(result ?? 0);
                    return cnt > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.HasOverlappingLeaveAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.HasOverlappingLeaveAsync (General)", ex);
                return false;
            }
        }
        public static async Task<int> GetNumberOfCurrentLeavesAsync()
        {
            string query = "Select COUNT(*) From EmployeeLeaves Where Status = 2 and (GETDATE() Between StartDate and EndDate);";

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    if (result != null && int.TryParse(result.ToString(), out int number))
                        return number;
                }

            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.NumberOfCurrentLeaves (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.NumberOfCurrentLeaves (General)", ex);
            }

            return 0;
        }
        public static async Task<int> GetNumberOfPendingLeavesAsync()
        {
            string query = "Select COUNT(*) From EmployeeLeaves Where Status = 1 and (GETDATE() Between StartDate and EndDate);";

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync().ConfigureAwait(false);
                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    if (result != null && int.TryParse(result.ToString(), out int number))
                        return number;
                }

            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.NumberOfPendingLeaves (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeLeavesData.NumberOfPendingLeaves (General)", ex);
            }

            return 0;
        }

        private static clsEmployeeLeavesEntity _MapToEmployeeLeavesEntity(SqlDataReader reader)
        {
            int iLeaveID = reader.GetOrdinal("LeaveID");
            int iEmployeeID = reader.GetOrdinal("EmployeeID");
            int iLeaveTypeID = reader.GetOrdinal("LeaveTypeID");
            int iStartDate = reader.GetOrdinal("StartDate");
            int iEndDate = reader.GetOrdinal("EndDate");
            int iReason = reader.GetOrdinal("Reason");
            int iStatus = reader.GetOrdinal("Status");
            int iApprovedByUserID = reader.GetOrdinal("ApprovedByUserID");
            int iCreatedDate = reader.GetOrdinal("CreatedDate");
            int iCreatedByUserID = reader.GetOrdinal("CreatedByUserID");
            int iEditedDate = reader.GetOrdinal("EditedDate");
            int iEditedByUserID = reader.GetOrdinal("EditedByUserID");


            return new clsEmployeeLeavesEntity
            {
                LeaveID = reader.IsDBNull(iLeaveID) ? 0 : reader.GetInt32(iLeaveID),
                EmployeeID = reader.IsDBNull(iEmployeeID) ? 0 : reader.GetInt32(iEmployeeID),
                LeaveTypeID = reader.IsDBNull(iLeaveTypeID) ? 0 : reader.GetInt32(iLeaveTypeID),
                StartDate = reader.IsDBNull(iStartDate) ? DateTime.MinValue : reader.GetDateTime(iStartDate),
                EndDate = reader.IsDBNull(iEndDate) ? DateTime.MinValue : reader.GetDateTime(iEndDate),
                Reason = reader.IsDBNull(iReason) ? null : reader.GetString(iReason),
                Status = reader.IsDBNull(iStatus) ? (byte)0 : reader.GetByte(iStatus),
                ApprovedByUserID = reader.IsDBNull(iApprovedByUserID) ? (int?)null : reader.GetInt32(iApprovedByUserID),
                CreatedDate = reader.IsDBNull(iCreatedDate) ? DateTime.MinValue : reader.GetDateTime(iCreatedDate),
                CreatedByUserID = reader.IsDBNull(iCreatedByUserID) ? 0 : reader.GetInt32(iCreatedByUserID),
                EditedDate = reader.IsDBNull(iEditedDate) ? (DateTime?)null : reader.GetDateTime(iEditedDate),
                EditedByUserID = reader.IsDBNull(iEditedByUserID) ? (int?)null : reader.GetInt32(iEditedByUserID)

            };
        }

    }
}
