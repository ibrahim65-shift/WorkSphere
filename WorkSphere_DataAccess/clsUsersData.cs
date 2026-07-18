using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsUsersData
    {
        public static clsUsersEntities GetUserByID(int userID)
        {
            string query = "SELECT * FROM Users WHERE UserID=@UserID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapReaderToUser(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserByID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserByID (General)", ex);
            }

            return null;
        }
        public static clsUsersEntities GetUserByUserName(string userName)
        {
            string query = "SELECT * FROM Users WHERE UserName=@UserName";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapReaderToUser(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserByUserName (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserByUserName (General)", ex);
            }

            return null;
        }
        public static clsUsersEntities GetUserByUserNameAndPassword(string userName, string password)
        {
            string query = "SELECT * FROM Users WHERE UserName=@UserName AND Password=@Password";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
                        command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapReaderToUser(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserByUserNameAndPassword (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserByUserNameAndPassword (General)", ex);
            }

            return null;
        }
        public static int AddNew(clsUsersEntities user)
        {
            string query = @"
                INSERT INTO Users(UserName,Password,FullName,Email,Phone,Address,IsActive,RoleID,CreatedDate,EditDate)
                VALUES (@UserName,@Password,@FullName,@Email,@Phone,@Address,@IsActive,@RoleID,@CreatedDate,@EditDate);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                        command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;
                        command.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)user.Email ?? DBNull.Value;
                        command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = user.Phone;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = (object)user.Address ?? DBNull.Value;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
                        command.Parameters.Add("@RoleID", SqlDbType.Int).Value = user.RoleID;
                        command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                        command.Parameters.Add("@EditDate", SqlDbType.DateTime).Value = user.EditDate;

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int newID))
                            return newID;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.AddNew (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.AddNew (General)", ex);
            }

            return -1;
        }
        public static bool Update(clsUsersEntities user)
        {
            string query = @"
                UPDATE Users SET 
                    UserName=@UserName,
                    FullName=@FullName,
                    Email=@Email,
                    Phone=@Phone,
                    Address=@Address,
                    IsActive=@IsActive,
                    RoleID=@RoleID,
                    EditDate=@EditDate
                   WHERE UserID=@UserID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = user.UserID;
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = user.UserName;
                        command.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = user.FullName;
                        command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object)user.Email ?? DBNull.Value;
                        command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = user.Phone;
                        command.Parameters.Add("@Address", SqlDbType.NVarChar).Value = (object)user.Address ?? DBNull.Value;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
                        command.Parameters.Add("@RoleID", SqlDbType.Int).Value = user.RoleID;
                        command.Parameters.Add("@EditDate", SqlDbType.DateTime).Value = DateTime.Now;

                        if (!string.IsNullOrEmpty(user.Password))
                            command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;

                        connection.Open();
                        int rows = command.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.Update (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.Update (General)", ex);
                return false;
            }
        }
        public static bool Delete(int userID)
        {
            string query = "DELETE FROM Users WHERE UserID=@UserID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                        connection.Open();
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.Delete (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.Delete (General)", ex);
                return false;
            }
        }
        public static List<clsUsersEntities> GetAllUsers()
        {
            var list = new List<clsUsersEntities>();
            string query = "select * From UsersView Order by UserID desc;";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                list.Add(MapReaderToUserForView(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetAllUsers (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetAllUsers (General)", ex);
            }

            return list;
        }
        public static async Task<(List<clsUsersEntities> List, int TotalPages)> GetUsersPageAsync(int PageNumber, int PageSize, string searchText = null)
        {
            int totalPages = 0;
            var list = new List<clsUsersEntities>();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using(SqlCommand command = new SqlCommand("SP_GetUsersPage",connection))
                {
                    
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                    command.Parameters.Add("@SearchText", SqlDbType.NVarChar, 200).Value = string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : searchText;

                    await connection.OpenAsync();

                    using(SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        int TotalCount = 0;
                        while(await reader.ReadAsync().ConfigureAwait(false))
                        {
                            list.Add(MapReaderToUserForView(reader));

                            if(TotalCount==0)
                            {
                                TotalCount = Convert.ToInt32(reader["TotalCount"]);
                            }
                        }

                        if(TotalCount>0)
                        {
                            totalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
                        }
                    }

                    
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUsersPage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUsersPage (General)", ex);
            }

            return (list, totalPages);
        }
        public static bool IsFullNameExists(string fullName)
        {
            string query = "SELECT Found=1 FROM Users WHERE FullName=@FullName";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@FullName", SqlDbType.NVarChar).Value = fullName;
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
                clsEventLogger.LogException("clsUsersData.IsFullNameExists (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.IsFullNameExists (General)", ex);
                return false;
            }

        }
        public static bool IsUserNameExists(string userName)
        {
            string query = "SELECT 1 FROM Users WHERE UserName=@UserName";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = userName;
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
                clsEventLogger.LogException("clsUsersData.IsUserNameExists (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.IsUserNameExists (General)", ex);
                return false;
            }
        }
        public static bool UpdatePassword(string password, int userID)
        {
            string query = @"
             Update Users 
               set Password = @Password
               Where UserID=@UserID;";

            try
            {

                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                        command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                        connection.Open();
                        int rows = command.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.UpdatePassword (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.UpdatePassword (General)", ex);
                return false;
            }
        }
        public static string GetPasswordByUserID(int userID)
        {
            string password = null;
            string query = "select Password From Users Where UserID=@UserID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                        connection.Open();

                        var result = command.ExecuteScalar();
                        if (result != null)
                            password = result.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetPasswordByUserID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetPasswordByUserID (General)", ex);
            }

            return password;
        }
        public static async Task<List<string>> GetUserPermissionsAsync(int userID)
        {
            var PermissionList = new List<string>();
            string query = "SP_GetUserPermissions";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                    await connection.OpenAsync().ConfigureAwait(false);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        while (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            PermissionList.Add(reader["PermissionCode"].ToString());
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserPermissionsAsync (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsUsersData.GetUserPermissionsAsync (General)", ex);
            }

            return PermissionList;
        }
        private static clsUsersEntities MapReaderToUser(SqlDataReader reader)
        {
            return new clsUsersEntities
            {
                UserID = (int)reader["UserID"],
                UserName = reader["UserName"].ToString(),
                Password = reader["Password"].ToString(),
                FullName = reader["FullName"].ToString(),
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                Phone = reader["Phone"].ToString(),
                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                IsActive = (bool)reader["IsActive"],
                RoleID = (int)reader["RoleID"],
                CreatedDate = (DateTime)reader["CreatedDate"],
                EditDate = (DateTime)reader["EditDate"]
            };
        }
        private static clsUsersEntities MapReaderToUserForView(SqlDataReader reader)
        {
            return new clsUsersEntities
            {
                UserID = (int)reader["UserID"],
                UserName = reader["UserName"].ToString(),
                Password = reader["Password"].ToString(),
                FullName = reader["FullName"].ToString(),
                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                Phone = reader["Phone"].ToString(),
                Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : null,
                IsActive = (bool)reader["IsActive"],
                RoleID = (int)reader["RoleID"],
                RoleName = reader["RoleName"].ToString(),
                CreatedDate = (DateTime)reader["CreatedDate"],
                EditDate = (DateTime)reader["EditDate"]
            };
        }
    }
}