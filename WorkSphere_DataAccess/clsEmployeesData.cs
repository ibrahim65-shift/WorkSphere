using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsEmployeesData
    {
        public static async Task<clsEmployeeEntity> GetEmployeeInfoByIDAsync(int ID)
        {
            string query = "Select * from Employees Where EmployeeID =@ID";

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
                                return _MapToEmployeeEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeInfoByIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeInfoByIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<clsEmployeeEntity> GetEmployeeInfoByNationalIDAsync(string NationalID)
        {
            string query = "Select * from Employees Where NationalID =@NationalID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@NationalID", SqlDbType.NVarChar).Value = NationalID;
                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            if (await reader.ReadAsync().ConfigureAwait(false))
                                return _MapToEmployeeEntity(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeInfoByNationalIDAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeInfoByNationalIDAsync (General)", ex);
            }

            return null;
        }
        public static async Task<int> AddNewAsync(clsEmployeeEntity entity)
        {
            string query = @"insert into Employees(NationalID,FirstName,SecondName,ThirdName,LastName,Gender
             ,BirthDate,Email,Phone,Address,HireDate,IsActive,StepID,DepartmentID,CreatedDate,CreatedByUserID,
             EditDate,EditedByUserID)

             Values (@NationalID,@FirstName,@SecondName,@ThirdName,@LastName,@Gender,@BirthDate,@Email,@Phone
             ,@Address,@HireDate,@IsActive,@StepID,@DepartmentID,@CreatedDate,@CreatedByUserID,@EditDate,
              @EditedByUserID);
             
             Select SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@NationalID", SqlDbType.NVarChar).Value = entity.NationalID;
                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = entity.FirstName;
                        command.Parameters.Add("@SecondName", SqlDbType.NVarChar).Value = entity.SecondName;
                        command.Parameters.Add("@ThirdName", SqlDbType.NVarChar).Value = (object)entity.ThirdName ?? DBNull.Value;
                        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = entity.LastName;
                        command.Parameters.Add("@Gender", SqlDbType.Bit).Value = entity.Gender;
                        command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = entity.BirthDate;
                        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)entity.Email ?? DBNull.Value;
                        command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = (object)entity.Phone ?? DBNull.Value;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = (object)entity.Address ?? DBNull.Value;
                        command.Parameters.Add("@HireDate", SqlDbType.Date).Value = entity.HireDate;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;
                        command.Parameters.Add("@StepID", SqlDbType.Int).Value = entity.StepID;
                        command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = entity.DepartmentID;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = entity.CreatedDate;
                        command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = entity.CreatedByUserID;
                        command.Parameters.Add("@EditDate", SqlDbType.DateTime2).Value = (object)entity.EditDate ?? DBNull.Value;
                        command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;
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
                clsEventLogger.LogException("clsEmployeesData.AddNewAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.AddNewAsync (General)", ex);
            }
            return -1;
        }
        public static async Task<bool> UpdateAsync(clsEmployeeEntity entity)
        {
            string query = @"Update Employees
            set NationalID=@NationalID , 
            FirstName  =@FirstName,
            SecondName =@SecondName,
            ThirdName=@ThirdName,
            LastName=@LastName,
            Gender=@Gender,
            BirthDate=@BirthDate,
            Email=@Email,
            Phone=@Phone,
            Address=@Address,
            HireDate=@HireDate,
            IsActive=@IsActive,
            StepID=@StepID,
            DepartmentID=@DepartmentID,
            CreatedDate=@CreatedDate,
            CreatedByUserID=@CreatedByUserID,
            EditDate=@EditDate,
            EditedByUserID=@EditedByUserID
            
            Where EmployeeID=@EmployeeID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = entity.EmployeeID;
                        command.Parameters.Add("@NationalID", SqlDbType.NVarChar).Value = entity.NationalID;
                        command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = entity.FirstName;
                        command.Parameters.Add("@SecondName", SqlDbType.NVarChar).Value = entity.SecondName;
                        command.Parameters.Add("@ThirdName", SqlDbType.NVarChar).Value = (object)entity.ThirdName ?? DBNull.Value;
                        command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = entity.LastName;
                        command.Parameters.Add("@Gender", SqlDbType.Bit).Value = entity.Gender;
                        command.Parameters.Add("@BirthDate", SqlDbType.Date).Value = entity.BirthDate;
                        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)entity.Email ?? DBNull.Value;
                        command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = (object)entity.Phone ?? DBNull.Value;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = (object)entity.Address ?? DBNull.Value;
                        command.Parameters.Add("@HireDate", SqlDbType.Date).Value = entity.HireDate;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = entity.IsActive;
                        command.Parameters.Add("@StepID", SqlDbType.Int).Value = entity.StepID;
                        command.Parameters.Add("@DepartmentID", SqlDbType.Int).Value = entity.DepartmentID;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime2).Value = entity.CreatedDate;
                        command.Parameters.Add("@CreatedByUserID", SqlDbType.Int).Value = entity.CreatedByUserID;
                        command.Parameters.Add("@EditDate", SqlDbType.DateTime2).Value = (object)entity.EditDate ?? DBNull.Value;
                        command.Parameters.Add("@EditedByUserID", SqlDbType.Int).Value = (object)entity.EditedByUserID ?? DBNull.Value;

                        await connection.OpenAsync().ConfigureAwait(false);
                        int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.UpdateAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.UpdateAsync (General)", ex);
                return false;
            }
        }
        public static async Task<bool> DeleteAsync(int ID)
        {
            string query = "Delete From Employees Where EmployeeID=@ID;";

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
                clsEventLogger.LogException("clsEmployeesData.DeleteAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.DeleteAsync (General)", ex);
                return false;
            }
        }
        public static async Task<List<clsEmployeeEntity>> GetAllEmployees()
        {
            string query = "Select * From Employees;";
            var list = new List<clsEmployeeEntity>();

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
                                list.Add(_MapToEmployeeEntity(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetAllEmployees (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetAllEmployees (General)", ex);
            }

            return list;
        }
        public static async Task<bool> IsNationalIDExistsAsync(string nationalID)
        {
            string query = "SELECT COUNT(1) From Employees Where NationalID=@NationalID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NationalID", nationalID);

                        await connection.OpenAsync().ConfigureAwait(false);
                        object result = await command.ExecuteScalarAsync().ConfigureAwait(false);
                        int count = Convert.ToInt32(result ?? 0);
                        return count > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.IsNationalIDExistsAsync (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.IsNationalIDExistsAsync (General)", ex);
                return false;
            }
        }
        public static async Task<DataTable> GetEmployeeFullInfo()
        {
            string query = "Select * From vw_EmployeeFullInfo;";
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await connection.OpenAsync();
                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            dt.Load(reader);
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeFullInfo (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeeFullInfo (General)", ex);
            }

            return dt;
        }
        public static async Task<bool> SetNewStepID(int empID, int newStepID)
        {
            string query = @"update Employees
                 set StepID=@StepID
                 Where EmployeeID = @EmployeeID";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@StepID", SqlDbType.Int).Value = newStepID;
                    command.Parameters.Add("@EmployeeID", SqlDbType.Int).Value = empID;

                    await connection.OpenAsync().ConfigureAwait(false);
                    int rows = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
                    return rows > 0;
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsEmployeesData.SetNewStepID (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.SetNewStepID (General)", ex);
                return false;
            }
        }
        public static async Task<int> GetNumberOfEmployeesAsync()
        {
            string query = "Select COUNT(*) From Employees Where IsActive=1;";

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
                clsEventLogger.LogException("clsEmployeesData.NumberOfEmployees (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.NumberOfEmployees (General)", ex);
            }
            return 0;
        }
        public static async Task<(DataTable dt , int totalPages)> GetEmployeesPageAsync(int PageNumber, int PageSize,  string searchText = null)
        {
            DataTable dt = new DataTable();
            int totalPages = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand("SP_GetEmployeesPage",connection))
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
                clsEventLogger.LogException("clsEmployeesData.GetEmployeePage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsEmployeesData.GetEmployeePage (General)", ex);
            }

            return (dt,totalPages);
        }
        private static clsEmployeeEntity _MapToEmployeeEntity(SqlDataReader reader)
        {
            int iEmployeeID = reader.GetOrdinal("EmployeeID");
            int iNationaID = reader.GetOrdinal("NationalID");
            int iFirstName = reader.GetOrdinal("FirstName");
            int iSecondName = reader.GetOrdinal("SecondName");
            int iThirdName = reader.GetOrdinal("ThirdName");
            int iLastName = reader.GetOrdinal("LastName");
            int iGender = reader.GetOrdinal("Gender");
            int iBirthDate = reader.GetOrdinal("BirthDate");
            int iEmail = reader.GetOrdinal("Email");
            int iPhone = reader.GetOrdinal("Phone");
            int iAddress = reader.GetOrdinal("Address");
            int iHireDate = reader.GetOrdinal("HireDate");
            int iIsActive = reader.GetOrdinal("IsActive");
            int iStepID = reader.GetOrdinal("StepID");
            int iDepartmentID = reader.GetOrdinal("DepartmentID");
            int iCreatedDate = reader.GetOrdinal("CreatedDate");
            int iCreatedByUserID = reader.GetOrdinal("CreatedByUserID");
            int iEditDate = reader.GetOrdinal("EditDate");
            int iEditedByUserID = reader.GetOrdinal("EditedByUserID");

            return new clsEmployeeEntity
            {
                EmployeeID = reader.IsDBNull(iEmployeeID) ? 0 : reader.GetInt32(iEmployeeID),
                NationalID = reader.IsDBNull(iNationaID) ? "" : reader.GetString(iNationaID),
                FirstName = reader.IsDBNull(iFirstName) ? "" : reader.GetString(iFirstName),
                SecondName = reader.IsDBNull(iSecondName) ? "" : reader.GetString(iSecondName),
                ThirdName = reader.IsDBNull(iThirdName) ? null : reader.GetString(iThirdName),
                LastName = reader.IsDBNull(iLastName) ? "" : reader.GetString(iLastName),
                Gender = reader.IsDBNull(iGender) ? false : reader.GetBoolean(iGender),
                BirthDate = reader.IsDBNull(iBirthDate) ? DateTime.MinValue : reader.GetDateTime(iBirthDate),
                Email = reader.IsDBNull(iEmail) ? null : reader.GetString(iEmail),
                Phone = reader.IsDBNull(iPhone) ? null : reader.GetString(iPhone),
                Address = reader.IsDBNull(iAddress) ? null : reader.GetString(iAddress),
                HireDate = reader.IsDBNull(iHireDate) ? DateTime.Now : reader.GetDateTime(iHireDate),
                IsActive = reader.IsDBNull(iIsActive) ? true : reader.GetBoolean(iIsActive),
                StepID = reader.IsDBNull(iStepID) ? 0 : reader.GetInt32(iStepID),
                DepartmentID = reader.IsDBNull(iDepartmentID) ? 0 : reader.GetInt32(iDepartmentID),
                CreatedDate = reader.IsDBNull(iCreatedDate) ? DateTime.MinValue : reader.GetDateTime(iCreatedDate),
                CreatedByUserID = reader.IsDBNull(iCreatedByUserID) ? 0 : reader.GetInt32(iCreatedByUserID),
                EditDate = reader.IsDBNull(iEditDate) ? (DateTime?)null : reader.GetDateTime(iEditDate),
                EditedByUserID = reader.IsDBNull(iEditedByUserID) ? (int?)null : reader.GetInt32(iEditedByUserID)
            };
        }

    }
}
