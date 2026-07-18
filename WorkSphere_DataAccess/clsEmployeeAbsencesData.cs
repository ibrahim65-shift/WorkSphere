using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsEmployeeAbsencesData
    {
        public static async Task<clsEmployeeAbsencesEntity> GetEmployeeAbsencesInfoByIDAsync(int ID)
        {
            string query = "Select * From EmployeeAbsences Where AbsenceID=@ID";

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
                            return _MapToEmployeeAbsences(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetEmployeeAbsencesInfoByIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetEmployeeAbsencesInfoByIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<List<clsEmployeeAbsencesEntity>> GetEmployeeAbsencesInfoByEmployeeIDAsync(int empID)
        {
            string query = "Select * From EmployeeAbsences Where EmployeeID=@ID";
            var list = new List<clsEmployeeAbsencesEntity>();

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
                            list.Add(_MapToEmployeeAbsences(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetEmployeeAbsencesInfoByEmployeeIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetEmployeeAbsencesInfoByEmployeeIDAsync (General)", ex);
            }
            return list;
        }
        public static async Task<int> AddNewAsync(clsEmployeeAbsencesEntity entity)
        {
            string query = @"insert into EmployeeAbsences(EmployeeID,AbsenceDate,IsExcused,Reason
            ,CreatedDate,CreatedByUserID,EditedDate,EditedByUserID)
             Values (@EmployeeID,@AbsenceDate,@IsExcused,@Reason,@CreatedDate,@CreatedByUserID
             ,@EditedDate,@EditedByUserID);
             SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int).Value = entity.EmployeeID;
                    command.Parameters.Add("@AbsenceDate", System.Data.SqlDbType.Date).Value = entity.AbsenceDate;
                    command.Parameters.Add("@IsExcused", System.Data.SqlDbType.Bit).Value = entity.IsExcused;
                    command.Parameters.Add("@Reason", System.Data.SqlDbType.NVarChar).Value = (object)entity.Reason ?? DBNull.Value;
                    command.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime).Value = entity.CreatedDate;
                    command.Parameters.Add("@CreatedByUserID", System.Data.SqlDbType.Int).Value = entity.CreatedByUserID;
                    command.Parameters.Add("@EditedDate", System.Data.SqlDbType.DateTime).Value = (object)entity.EditedDate ?? DBNull.Value;
                    command.Parameters.Add("@EditedByUserID", System.Data.SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                    await connection.OpenAsync().ConfigureAwait(false);
                    object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                    if (result != null && int.TryParse(result.ToString(), out int newID))
                        return newID;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.AddNewAsync (General)", ex);
            }

            return -1;
        }
        public static async Task<bool> UpdateAsync(clsEmployeeAbsencesEntity entity)
        {
            string query = @"
             update EmployeeAbsences
             
             set EmployeeID=@EmployeeID,
             AbsenceDate   =@AbsenceDate,
             IsExcused     =@IsExcused,
             Reason        =@Reason,
             EditedDate    =@EditedDate,
             EditedByUserID=@EditedByUserID
             
             Where AbsenceID=@AbsenceID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@AbsenceID", System.Data.SqlDbType.Int).Value = entity.AbsenceID;
                    command.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int).Value = entity.EmployeeID;
                    command.Parameters.Add("@AbsenceDate", System.Data.SqlDbType.Date).Value = entity.AbsenceDate;
                    command.Parameters.Add("@IsExcused", System.Data.SqlDbType.Bit).Value = entity.IsExcused;
                    command.Parameters.Add("@Reason", System.Data.SqlDbType.NVarChar).Value = (object)entity.Reason ?? DBNull.Value;
                    command.Parameters.Add("@EditedDate", System.Data.SqlDbType.DateTime).Value = (object)entity.EditedDate ?? DBNull.Value;
                    command.Parameters.Add("@EditedByUserID", System.Data.SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                    await connection.OpenAsync().ConfigureAwait(false);
                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From EmployeeAbsences Where AbsenceID=@ID;";

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
                clsEventLogger.LogException("clsEmployeeAbsencesData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<DataTable> GetAllEmployeeAbsencesInfo()
        {
            string query = "Select * From vw_EmployeeAbsencesInfo;";
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
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetAllEmployeeAbsencesInfo (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetAllEmployeeAbsencesInfo (General)", ex);
            }
            return dt;
        }
        public static async Task<(DataTable dt, int totalPages)> GetEmployeeAbsencesInfoPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            DataTable dt = new DataTable();
            int totalPages = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetEmployeeAbsencesInfoPage", connection))
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
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeAbsencesInfoPageAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeAbsencesInfoPageAsync (General)", ex);
            }

            return (dt, totalPages);
        }
        public static async Task<bool> HasAbsenceOnDateAsync(int empID, DateTime date, int? excludeAbsenceID = null)
        {
            string query = @"SELECT COUNT(1) FROM EmployeeAbsences 
         WHERE EmployeeID = @EmployeeID AND AbsenceDate = @Date  AND (@Exclude IS NULL OR AbsenceID <> @Exclude)";

            try
            {
                using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = empID;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;
                    cmd.Parameters.Add("@Exclude", SqlDbType.Int).Value = (object)excludeAbsenceID ?? DBNull.Value;

                    await conn.OpenAsync().ConfigureAwait(false);
                    object result = await cmd.ExecuteScalarAsync().ConfigureAwait(false);
                    int count = Convert.ToInt32(result ?? 0);
                    return count > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.HasAbsenceOnDateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.HasAbsenceOnDateAsync (General)", ex);
                return false;

            }
        }
        public static async Task<List<int>> GetEmployeeIDsForAbsenceAsync(DateTime date)
        {
            string query = @"SELECT e.EmployeeID
             FROM Employees e
             WHERE e.IsActive = 1 
               AND e.EmployeeID NOT IN (
                   SELECT EmployeeID FROM EmployeeAttendance 
                   WHERE CONVERT(DATE, AttendanceDate) = @Date
               )
               AND e.EmployeeID NOT IN (
                   SELECT EmployeeID FROM EmployeeAbsences 
                   WHERE CONVERT(DATE, AbsenceDate) =  @Date
               )
             ORDER BY e.EmployeeID;";

            var employeeIDs = new List<int>();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Date", SqlDbType.Date).Value = date.Date;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {

                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            if (!reader.IsDBNull(0))
                            {
                                employeeIDs.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetEmployeeIDsForAbsenceAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.GetEmployeeIDsForAbsenceAsync (General)", ex);
            }

            return employeeIDs;
        }
        public static async Task<bool> BulkInsertAbsencesAsync(List<int> employeeIDs, DateTime date,
            bool isExcused, string reason, int createdByUserID)
        {
            if (!employeeIDs.Any())
                return false;

            DataTable AbsencesTable = new DataTable();
            
            AbsencesTable.Columns.Add("EmployeeID", typeof(int));
            AbsencesTable.Columns.Add("AbsenceDate", typeof(DateTime));
            AbsencesTable.Columns.Add("IsExcused", typeof(bool));
            AbsencesTable.Columns.Add("Reason", typeof(string));

            foreach (int empID in employeeIDs)
                AbsencesTable.Rows.Add(empID, date, isExcused, reason);

            try
            {
                using(SqlConnection connection =new SqlConnection(clsDataAccessSettings.ConnectionString))
                using(SqlCommand command = new SqlCommand("SP_BulkInsertAbsences",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter = command.Parameters.AddWithValue("@Absences",AbsencesTable);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "RoleAbsenceListType";

                    command.Parameters.AddWithValue("@CreatedByUserID" , createdByUserID);

                    SqlParameter returnParam = new SqlParameter();
                    returnParam.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(returnParam);

                    await connection.OpenAsync().ConfigureAwait(false);
                    await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                    int result = (int)returnParam.Value;
                    return result == 1;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.BulkInsertAbsencesAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.BulkInsertAbsencesAsync (General)", ex);
                return false;

            }
        }
        public static async Task<int> GetNumberOfAbsenceEmployeesAsync()
        {
            string query = "Select COUNT(*) From EmployeeAbsences Where AbsenceDate =  CAST(GETDATE() as date);";

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
                clsEventLogger.LogException("clsEmployeeAbsencesData.NumberOfAttendanceEmployees (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeeAbsencesData.NumberOfAttendanceEmployees (General)", ex);
            }

            return 0;
        }
        private static clsEmployeeAbsencesEntity _MapToEmployeeAbsences(SqlDataReader reader)
        {
            int iAbsenceID = reader.GetOrdinal("AbsenceID");
            int iEmployeeID = reader.GetOrdinal("EmployeeID");
            int iAbsenceDate = reader.GetOrdinal("AbsenceDate");
            int iIsExcused = reader.GetOrdinal("IsExcused");
            int iReason = reader.GetOrdinal("Reason");
            int iCreatedDate = reader.GetOrdinal("CreatedDate");
            int iCreatedByUserID = reader.GetOrdinal("CreatedByUserID");
            int iEditedDate = reader.GetOrdinal("EditedDate");
            int iEditedByUserID = reader.GetOrdinal("EditedByUserID");

            return new clsEmployeeAbsencesEntity
            {
                AbsenceID = reader.IsDBNull(iAbsenceID) ? 0 : reader.GetInt32(iAbsenceID),
                EmployeeID = reader.IsDBNull(iEmployeeID) ? 0 : reader.GetInt32(iEmployeeID),
                AbsenceDate = reader.IsDBNull(iAbsenceDate) ? DateTime.MinValue : reader.GetDateTime(iAbsenceDate),
                IsExcused = reader.IsDBNull(iIsExcused) ? false : reader.GetBoolean(iIsExcused),
                Reason = reader.IsDBNull(iReason) ? null : reader.GetString(iReason),
                CreatedDate = reader.IsDBNull(iCreatedDate) ? DateTime.MinValue : reader.GetDateTime(iCreatedDate),
                CreatedByUserID = reader.IsDBNull(iCreatedByUserID) ? 0 : reader.GetInt32(iCreatedByUserID),
                EditedDate = reader.IsDBNull(iEditedDate) ? (DateTime?)null : reader.GetDateTime(iEditedDate),
                EditedByUserID = reader.IsDBNull(iEditedByUserID) ? (int?)null : reader.GetInt32(iEditedByUserID)

            };

        }


    }
}
