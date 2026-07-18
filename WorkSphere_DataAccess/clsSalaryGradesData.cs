using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsSalaryGradesData
    {

        public static clsSalaryGradesEntity GetSalaryGradesByID(int ID)
        {
            const string query = "SELECT * FROM SalaryGrades WHERE GradeID=@ID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return _MapToSalaryGradesEntity(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetSalaryGradesByID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetSalaryGradesByID (General)", ex);
            }

            return null;
        }
        public static async Task<List<clsSalaryGradesEntity>> GetSalaryGradesByDepartmentID(int DepID)
        {
            string query = "Select * From SalaryGrades Where DepartmentID=@DepID";
            var list = new List<clsSalaryGradesEntity>();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@DepID", SqlDbType.Int).Value = DepID;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(_MapToSalaryGradesEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetSalaryGradesByDepartmentID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetSalaryGradesByDepartmentID (General)", ex);
            }
            return list;
        }
        public static int AddNew(clsSalaryGradesEntity entity)
        {
            const string query = @"
                INSERT INTO SalaryGrades
                (JobGrade, Description,DepartmentID, EffectiveFrom, EffectiveTo, IsActive,
                 CreatedDate, CreatedByUserID, EditedDate, EditedByUserID)
                VALUES
                (@JobGrade, @Description,@DepartmentID, @EffectiveFrom, @EffectiveTo, @IsActive,
                 @CreatedDate, @CreatedByUserID, @EditedDate, @EditedByUserID);
                SELECT SCOPE_IDENTITY();";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add("@JobGrade", SqlDbType.SmallInt).Value = entity.JobGrade;
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = entity.Description;
                    command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = entity.DepartmentID;
                    command.Parameters.Add("@EffectiveFrom", SqlDbType.Date).Value = entity.EffectiveFrom;
                    command.Parameters.Add("@EffectiveTo", SqlDbType.Date).Value = (object)entity.EffectiveTo ?? DBNull.Value;
                    command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;
                    command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = entity.CreatedDate;
                    command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = (object)entity.CreatedByUserID ?? DBNull.Value;
                    command.Parameters.Add("@EditedDate", SqlDbType.DateTime2).Value = (object)entity.EditedDate ?? DBNull.Value;
                    command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                    connection.Open();
                    object result = command.ExecuteScalar();

                    // ✅ تأكدت من التحويل بشكل آمن
                    if (result != null && int.TryParse(result.ToString(), out int newID))
                        return newID;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.AddNew (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.AddNew (General)", ex);
            }

            return -1;
        }
        public static bool Update(clsSalaryGradesEntity entity)
        {
            const string query = @"
                UPDATE SalaryGrades 
                SET JobGrade = @JobGrade,
                    Description = @Description,
                    DepartmentID=@DepartmentID,
                    EffectiveFrom = @EffectiveFrom,
                    EffectiveTo = @EffectiveTo,
                    IsActive = @IsActive,
                    EditedDate = @EditedDate,
                    EditedByUserID = @EditedByUserID
                WHERE GradeID = @GradeID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@GradeID", SqlDbType.Int).Value = entity.GradeID;
                        command.Parameters.Add("@JobGrade", SqlDbType.SmallInt).Value = entity.JobGrade;
                        command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = entity.Description;
                        command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = entity.DepartmentID;
                        command.Parameters.Add("@EffectiveFrom", SqlDbType.Date).Value = entity.EffectiveFrom;
                        command.Parameters.Add("@EffectiveTo", SqlDbType.Date).Value = (object)entity.EffectiveTo ?? DBNull.Value;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = entity.CreatedDate;
                        command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = (object)entity.CreatedByUserID ?? DBNull.Value;
                        command.Parameters.Add("@EditedDate", SqlDbType.DateTime2).Value = (object)entity.EditedDate ?? DBNull.Value;
                        command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.Update (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.Update (General)", ex);
                return false;
            }
        }
        public static bool Delete(int ID)
        {
            const string query = "DELETE FROM SalaryGrades WHERE GradeID=@GradeID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@GradeID", SqlDbType.Int).Value = ID;

                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.Delete (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.Delete (General)", ex);
                return false;
            }
        }
        public static DataTable GetAllSalaryGrades()
        {
            string query = "Select * From vw_FullSalaryGradesInfo";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetAllSalaryGrades (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetAllSalaryGrades (General)", ex);
            }

            return dt;
        }
        public static async Task<(DataTable dt , int TotalPages)> GetSalaryGradesPageAsync(int PageNumber , int PageSize  , string searchText=null)
        {
            int totalPage = 0;
            DataTable dt = new DataTable();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetSalaryGradesPage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar,200).Value = string.IsNullOrWhiteSpace(searchText)?(object)DBNull.Value : searchText;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using(SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        dt.Load(reader);
                    }

                    if(dt.Rows.Count > 0)
                    {
                        totalPage = (int)Math.Ceiling(Convert.ToInt32(dt.Rows[0]["TotalCount"]) / (double)PageSize);
                    }
                    dt.Columns.Remove("TotalCount");
                }

            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetSalaryGradesPage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.GetSalaryGradesPage (General)", ex);
            }

            return (dt, totalPage);
        }
        public static bool IsGobExists(int DepID, short jobGrade, string description)
        {
            string query = @"
    SELECT COUNT(1) 
    FROM SalaryGrades
    WHERE DepartmentID = @DepartmentID
    AND (
        JobGrade = @JobGrade
        OR 
        REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
            REPLACE(REPLACE(REPLACE(
                LTRIM(RTRIM(Description)),
                '  ', ' '),
                '   ', ' '),
                '    ', ' '),
            N'أ', N'ا'),
            N'إ', N'ا'),  
            N'آ', N'ا'),
            N'ئ', N'ا'),
            N'ء', N'ا'),
            N'ة', N'ه'),
            N'ى', N'ا') = 
        REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
            REPLACE(REPLACE(REPLACE(
                LTRIM(RTRIM(@Description)),
                '  ', ' '),
                '   ', ' '),
                '    ', ' '),
            N'أ', N'ا'),
            N'إ', N'ا'),
            N'آ', N'ea'), 
            N'ئ', N'ا'),
            N'ء', N'ا'),
            N'ة', N'ه'),
            N'ى', N'ا')
    );";


            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = DepID;
                    command.Parameters.Add("@JobGrade", SqlDbType.SmallInt).Value = jobGrade;
                    command.Parameters.AddWithValue("@Description", SqlDbType.NVarChar).Value = description;

                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    return (count > 0);
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.IsGobExists (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradesData.IsGobExists (General)", ex);
                return false;
            }

        }
        private static clsSalaryGradesEntity _MapToSalaryGradesEntity(SqlDataReader reader)
        {
            return new clsSalaryGradesEntity
            {
                GradeID = (int)reader["GradeID"],
                JobGrade = (short)reader["JobGrade"],
                Description = reader["Description"].ToString(),
                DepartmentID = (int)reader["DepartmentID"],
                EffectiveFrom = (DateTime)reader["EffectiveFrom"],
                EffectiveTo = reader["EffectiveTo"] != DBNull.Value ? (DateTime?)reader["EffectiveTo"] : null,
                IsActive = (bool)reader["IsActive"],
                CreatedDate = (DateTime)reader["CreatedDate"],
                CreatedByUserID = reader["CreatedByUserID"] != DBNull.Value ? (int?)reader["CreatedByUserID"] : null,
                EditedDate = reader["EditedDate"] != DBNull.Value ? (DateTime?)reader["EditedDate"] : null,
                EditedByUserID = reader["EditedByUserID"] != DBNull.Value ? (int?)reader["EditedByUserID"] : null
            };
        }



    }
}