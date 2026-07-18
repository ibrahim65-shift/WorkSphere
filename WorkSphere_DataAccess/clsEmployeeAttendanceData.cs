using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsEmployeeAttendanceData
    {
        public static async Task<clsEmployeeAttendanceEntity> GetEmployeeAttendanceInfoByIDAsync(int ID)
        {
            string query = "Select * From EmployeeAttendance Where AttendanceID=@ID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ID;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            return _MapToEmployeeAttendanceEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.GetEmployeeAttendanceInfoByIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.GetEmployeeAttendanceInfoByIDAsync (General)", ex);
            }
            return null;
        }
        public static async Task<List<clsEmployeeAttendanceEntity>> GetEmployeeAttendanceInfoByEmployeeIDAsync(int empID)
        {
            string query = "Select * From EmployeeAttendance Where EmployeeID=@ID";
            var list = new List<clsEmployeeAttendanceEntity>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = empID;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(_MapToEmployeeAttendanceEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.GetEmployeeAttendanceInfoByEmployeeIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.GetEmployeeAttendanceInfoByEmployeeIDAsync (General)", ex);
            }
            return list;
        }
        public static async Task<int> AddNewAsync(clsEmployeeAttendanceEntity entity)
        {
            string query = @"insert into EmployeeAttendance(EmployeeID,AttendanceDate,CheckInTime,CheckOutTime
            ,IsLate,Notes,CreatedDate,CreatedByUserID,EditedDate,EditedByUserID)
            
            Values(@EmployeeID,@AttendanceDate,@CheckInTime,@CheckOutTime,@IsLate,@Notes
            ,@CreatedDate,@CreatedByUserID,@EditedDate,@EditedByUserID);
            
            SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                    command.Parameters.Add("@AttendanceDate", SqlDbType.Date).Value = entity.AttendanceDate;
                    command.Parameters.Add("@CheckInTime", SqlDbType.Time).Value = (object)entity.CheckInTime ?? DBNull.Value;
                    command.Parameters.Add("@CheckOutTime", SqlDbType.Time).Value = (object)entity.CheckOutTime ?? DBNull.Value;
                    command.Parameters.Add("@IsLate", SqlDbType.Bit).Value = entity.IsLate;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = (object)entity.Notes ?? DBNull.Value;
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
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsEmployeeAttendanceEntity entity)
        {
            string query = @"update EmployeeAttendance

             set EmployeeID =@EmployeeID,
             AttendanceDate =@AttendanceDate,
             CheckInTime =@CheckInTime ,
             CheckOutTime =@CheckOutTime ,
             IsLate =@IsLate ,
             Notes =@Notes ,
             EditedDate =@EditedDate ,
             EditedByUserID = @EditedByUserID
             
             Where AttendanceID = @AttendanceID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@AttendanceID", SqlDbType.Int).Value = entity.AttendanceID;
                    command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                    command.Parameters.Add("@AttendanceDate", SqlDbType.Date).Value = entity.AttendanceDate;
                    command.Parameters.Add("@CheckInTime", SqlDbType.Time).Value = (object)entity.CheckInTime ?? DBNull.Value;
                    command.Parameters.Add("@CheckOutTime", SqlDbType.Time).Value = (object)entity.CheckOutTime ?? DBNull.Value;
                    command.Parameters.Add("@IsLate", SqlDbType.Bit).Value = entity.IsLate;
                    command.Parameters.Add("@Notes", SqlDbType.NVarChar).Value = (object)entity.Notes ?? DBNull.Value;
                    command.Parameters.Add("@EditedDate", SqlDbType.DateTime).Value = (object)entity.EditedDate ?? DBNull.Value;
                    command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                    await connection.OpenAsync().ConfigureAwait(false);
                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From EmployeeAttendance Where AttendanceID=@ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                    await connection.OpenAsync().ConfigureAwait(false);
                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<DataTable> GetEmployeeAttendanceInfo()
        {
            string query = "Select * From vw_EmployeeAttendance;";
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
                clsEventLogger.LogException("clsEmployeeAttendanceData.GetEmployeeAttendanceInfo (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.GetEmployeeAttendanceInfo (General)", ex);
            }
            return dt;
        }
        public static async Task<(DataTable dt, int totalPages)> GetEmployeeAttendancePageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            DataTable dt = new DataTable();
            int totalPages = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetEmployeeAttendancePage", connection))
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
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeAttendancePage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeAttendancePage (General)", ex);
            }

            return (dt, totalPages);
        }
        public static async Task<bool> HasAttendanceForDateAsync(int empID, DateTime attendanceDate, int? excludeAttendanceID = null)
        {
            string query = @"
             SELECT COUNT(1)
             FROM EmployeeAttendance
             WHERE EmployeeID = @EmployeeID
             AND AttendanceDate = @AttendanceDate
             AND (@Exclude IS NULL OR AttendanceID <> @Exclude);";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = empID;
                    command.Parameters.Add("@AttendanceDate", SqlDbType.Date).Value = attendanceDate.Date;
                    command.Parameters.Add("@Exclude", SqlDbType.Int).Value = (object)excludeAttendanceID ?? DBNull.Value;

                    await connection.OpenAsync().ConfigureAwait(false);
                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    int count = Convert.ToInt32(result ?? 0);
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.HasAttendanceForDateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.HasAttendanceForDateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<int> GetNumberOfAttendanceEmployeesAsync()
        {
            string query = "Select COUNT(*) From EmployeeAttendance Where AttendanceDate= CAST(GETDATE() as date);";

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
                clsEventLogger.LogException("clsEmployeeAttendanceData.NumberOfAttendanceEmployees (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.NumberOfAttendanceEmployees (General)", ex);
            }

            return 0;
        }
        public static async Task<int> GetNumberOfLateEmployeesAsync()
        {
            string query = "Select COUNT(*) From EmployeeAttendance Where AttendanceDate= CAST(GETDATE() as date) and IsLate=1;";

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
                clsEventLogger.LogException("clsEmployeeAttendanceData.NumberOfLateEmployees (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAttendanceData.NumberOfLateEmployees (General)", ex);
            }

            return 0;
        }
        private static clsEmployeeAttendanceEntity _MapToEmployeeAttendanceEntity(SqlDataReader reader)
        {
            int iAttendanceID = reader.GetOrdinal("AttendanceID");
            int iEmployeeID = reader.GetOrdinal("EmployeeID");
            int iAttendanceDate = reader.GetOrdinal("AttendanceDate");
            int iCheckInTime = reader.GetOrdinal("CheckInTime");
            int iCheckOutTime = reader.GetOrdinal("CheckOutTime");
            int iIsLate = reader.GetOrdinal("IsLate");
            int iNotes = reader.GetOrdinal("Notes");
            int iCreatedDate = reader.GetOrdinal("CreatedDate");
            int iCreatedByUserID = reader.GetOrdinal("CreatedByUserID");
            int iEditedDate = reader.GetOrdinal("EditedDate");
            int iEditedByUserID = reader.GetOrdinal("EditedByUserID");

            return new clsEmployeeAttendanceEntity
            {
                AttendanceID = reader.IsDBNull(iAttendanceID) ? 0 : reader.GetInt32(iAttendanceID),
                EmployeeID = reader.IsDBNull(iEmployeeID) ? 0 : reader.GetInt32(iEmployeeID),
                AttendanceDate = reader.IsDBNull(iAttendanceDate) ? DateTime.MinValue : reader.GetDateTime(iAttendanceDate),
                CheckInTime = reader.IsDBNull(iCheckInTime) ? (TimeSpan?)null : reader.GetTimeSpan(iCheckInTime),
                CheckOutTime = reader.IsDBNull(iCheckOutTime) ? (TimeSpan?)null : reader.GetTimeSpan(iCheckOutTime),
                IsLate = reader.IsDBNull(iIsLate) ? false : reader.GetBoolean(iIsLate),
                Notes = reader.IsDBNull(iNotes) ? null : reader.GetString(iNotes),
                CreatedDate = reader.IsDBNull(iCreatedDate) ? DateTime.MinValue : reader.GetDateTime(iCreatedDate),
                CreatedByUserID = reader.IsDBNull(iCreatedByUserID) ? 0 : reader.GetInt32(iCreatedByUserID),
                EditedDate = reader.IsDBNull(iEditedDate) ? (DateTime?)null : reader.GetDateTime(iEditedDate),
                EditedByUserID = reader.IsDBNull(iEditedByUserID) ? (int?)null : reader.GetInt32(iEditedByUserID)

            };
        }
    }
}
