using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsSalaryGradeStepsData
    {

        public static clsSalaryGradeStepsEntity GetSalaryGradeStepsByID(int ID)
        {
            const string query = "SELECT * FROM SalaryGradeSteps WHERE StepID = @ID";

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
                                return _MapToSalaryGradeStepsEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetSalaryGradeStepsByID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetSalaryGradeStepsByID (General)", ex);
            }
            return null;
        }
        public static List<clsSalaryGradeStepsEntity> GetSalaryGradeStepsByGradeID(int gradeID)
        {
            const string query = "SELECT * FROM SalaryGradeSteps WHERE GradeID = @GradeID ORDER BY StepNumber";
            var list = new List<clsSalaryGradeStepsEntity>();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@GradeID", SqlDbType.Int).Value = gradeID;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                list.Add(_MapToSalaryGradeStepsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetSalaryGradeStepsByGradeID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetSalaryGradeStepsByGradeID (General)", ex);
            }

            return list;
        }
        public static int AddNew(clsSalaryGradeStepsEntity entity)
        {
            const string query = @"
               INSERT INTO SalaryGradeSteps(GradeID,StepNumber,BaseSalary,AnnualRaisePercent,MinYearsInStep)
               VALUES (@GradeID,@StepNumber,@BaseSalary,@AnnualRaisePercent,@MinYearsInStep);
               SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var pGrade = command.Parameters.Add("@GradeID", SqlDbType.Int);
                        pGrade.Value = entity.GradeID;

                        var pStep = command.Parameters.Add("@StepNumber", SqlDbType.TinyInt);
                        pStep.Value = entity.StepNumber;

                        // BaseSalary: assume DECIMAL(18,2)
                        var pBase = command.Parameters.Add("@BaseSalary", SqlDbType.Decimal);
                        pBase.Precision = 18;
                        pBase.Scale = 2;
                        pBase.Value = entity.BaseSalary;

                        // AnnualRaisePercent: assume DECIMAL(5,2) (nullable)
                        var pRaise = command.Parameters.Add("@AnnualRaisePercent", SqlDbType.Decimal);
                        pRaise.Precision = 5;
                        pRaise.Scale = 2;
                        pRaise.Value = (object)entity.AnnualRaisePercent ?? DBNull.Value;

                        var pMin = command.Parameters.Add("@MinYearsInStep", SqlDbType.TinyInt);
                        pMin.Value = entity.MinYearsInStep;

                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int newID))
                            return newID;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.AddNew (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.AddNew (General)", ex);
            }

            return -1;
        }
        public static bool Update(clsSalaryGradeStepsEntity entity)
        {

            const string query = @"
               UPDATE SalaryGradeSteps
               SET GradeID = @GradeID,
                   StepNumber = @StepNumber,
                   BaseSalary = @BaseSalary,
                   AnnualRaisePercent = @AnnualRaisePercent,
                   MinYearsInStep = @MinYearsInStep
               WHERE StepID = @StepID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@StepID", SqlDbType.Int).Value = entity.StepID;
                        command.Parameters.Add("@GradeID", SqlDbType.Int).Value = entity.GradeID;
                        command.Parameters.Add("@StepNumber", SqlDbType.TinyInt).Value = entity.StepNumber;

                        var pBase = command.Parameters.Add("@BaseSalary", SqlDbType.Decimal);
                        pBase.Precision = 18;
                        pBase.Scale = 2;
                        pBase.Value = entity.BaseSalary;

                        var pRaise = command.Parameters.Add("@AnnualRaisePercent", SqlDbType.Decimal);
                        pRaise.Precision = 5;
                        pRaise.Scale = 2;
                        pRaise.Value = (object)entity.AnnualRaisePercent ?? DBNull.Value;

                        command.Parameters.Add("@MinYearsInStep", SqlDbType.TinyInt).Value = entity.MinYearsInStep;

                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.Update (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.Update (General)", ex);
                return false;
            }
        }
        public static bool Delete(int ID)
        {
            const string query = "DELETE FROM SalaryGradeSteps WHERE StepID = @StepID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@StepID", SqlDbType.Int).Value = ID;
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.Delete (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.Delete (General)", ex);
                return false;
            }
        }
        public static bool IsStepNumberExistsByGradeIDAndStepNumber(int GradeID, byte StepNumber)
        {
            string query = "Select found=1 From SalaryGradeSteps Where GradeID=@GradeID and StepNumber =@StepNumber;";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@GradeID", SqlDbType.Int).Value = GradeID;
                        command.Parameters.Add("@StepNumber", SqlDbType.TinyInt).Value = StepNumber;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            return reader.HasRows;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.IsStepNumberExistsByGradeIDAndStepNumber (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.IsStepNumberExistsByGradeIDAndStepNumber (General)", ex);
                return false;
            }
        }
        public static List<clsSalaryGradeStepsEntity> GetAllSalaryGradeSteps()
        {
            var list = new List<clsSalaryGradeStepsEntity>();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetAllSalaryGradeSteps", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                list.Add(_MapToSalaryGradeStepsEntity(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetAllSalaryGradeSteps (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetAllSalaryGradeSteps (General)", ex);
            }

            return list;
        }
        public static int GetStepNumber(int GradeID)
        {
            string query = @"Select top 1 StepNumber From SalaryGradeSteps Where GradeID =@GradeID
                          Order By StepNumber Desc";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand commmand = new SqlCommand(query, connection))
                    {
                        commmand.Parameters.Add("@GradeID", SqlDbType.Int).Value = GradeID;

                        connection.Open();
                        object result = commmand.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int stepNumber))
                        {
                            return stepNumber;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetStepNumber (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSalaryGradeStepsData.GetStepNumber (General)", ex);
            }

            return 0;
        }
        private static clsSalaryGradeStepsEntity _MapToSalaryGradeStepsEntity(SqlDataReader reader)
        {
            // safer mapping using ordinals & IsDBNull checks
            int iStepID = reader.GetOrdinal("StepID");
            int iGradeID = reader.GetOrdinal("GradeID");
            int iStepNumber = reader.GetOrdinal("StepNumber");
            int iBaseSalary = reader.GetOrdinal("BaseSalary");
            int iAnnual = reader.GetOrdinal("AnnualRaisePercent");
            int iMinYears = reader.GetOrdinal("MinYearsInStep");

            return new clsSalaryGradeStepsEntity
            {
                StepID = reader.IsDBNull(iStepID) ? 0 : reader.GetInt32(iStepID),
                GradeID = reader.IsDBNull(iGradeID) ? 0 : reader.GetInt32(iGradeID),
                StepNumber = reader.IsDBNull(iStepNumber) ? (byte)0 : reader.GetByte(iStepNumber),
                BaseSalary = reader.IsDBNull(iBaseSalary) ? 0m : reader.GetDecimal(iBaseSalary),
                AnnualRaisePercent = reader.IsDBNull(iAnnual) ? (decimal?)null : reader.GetDecimal(iAnnual),
                MinYearsInStep = reader.IsDBNull(iMinYears) ? (byte)0 : reader.GetByte(iMinYears)
            };
        }
    }
}