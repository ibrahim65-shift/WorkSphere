using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WorkSphere.Global_Classes;
using WorkSphere_Entities;

namespace WorkSphere_DataAccess
{
    public class clsSystemRecordsData
    {
        public static clsSystemRecordEntity GetSystemRecordInfoByID(int ID)
        {
            string query = "Select * From SystemRecords Where ID=@ID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ID;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToSystemRecord(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetSystemRecordInfoByID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetSystemRecordInfoByID (General)", ex);
            }

            return null;
        }
        public static clsSystemRecordEntity GetSystemRecordInfoByUserID(int UserID)
        {
            string query = "Select * From SystemRecords Where UserID=@UserID";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = UserID;

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReaderToSystemRecord(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetSystemRecordInfoByUserID (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetSystemRecordInfoByUserID (General)", ex);
            }

            return null;
        }
        public static int AddNew(clsSystemRecordEntity entity)
        {
            string query = @"insert into SystemRecords 
            (ActionType,DeviceName,MachinID,Title,Description,CreatedDate,UserID)
            Values (@ActionType,@DeviceName,@MachinID,@Title,@Description,@CreatedDate,@UserID);
            Select SCOPE_IDENTITY();";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.Add("@ActionType", System.Data.SqlDbType.NVarChar).Value = entity.ActionType;
                        command.Parameters.Add("@DeviceName", System.Data.SqlDbType.NVarChar).Value = entity.DeviceName;
                        command.Parameters.Add("@MachinID", System.Data.SqlDbType.NVarChar).Value = entity.MachinID;
                        command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar).Value = entity.Title;
                        command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = entity.Description;
                        command.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime).Value = entity.CreatedDate;
                        command.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = entity.UserID;

                        connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int newID))
                        {
                            return newID;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.AddNew (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.AddNew (General)", ex);
            }

            return -1;
        }
        public static bool Update(clsSystemRecordEntity entity)
        {
            string query = @"Update SystemRecords
                            set ActionType =@ActionType ,
                                DeviceName=@DeviceName ,
                            	MachinID=@MachinID ,
                            	Title= @Title, 
                            	Description= @Description,
                            	CreatedDate=@CreatedDate , 
                            	UserID =@UserID 
                            	Where ID=@ID;";

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = entity.ID;
                        command.Parameters.Add("@ActionType", System.Data.SqlDbType.NVarChar).Value = entity.ActionType;
                        command.Parameters.Add("@DeviceName", System.Data.SqlDbType.NVarChar).Value = entity.DeviceName;
                        command.Parameters.Add("@MachinID", System.Data.SqlDbType.NVarChar).Value = entity.MachinID;
                        command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar).Value = entity.Title;
                        command.Parameters.Add("@Description", System.Data.SqlDbType.NVarChar).Value = entity.Description;
                        command.Parameters.Add("@CreatedDate", System.Data.SqlDbType.DateTime).Value = entity.CreatedDate;
                        command.Parameters.Add("@UserID", System.Data.SqlDbType.Int).Value = entity.UserID;

                        connection.Open();
                        int rows = command.ExecuteNonQuery();

                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.Update (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.Update (General)", ex);
                return false;
            }
        }
        public static bool Delete(int ID)
        {
            string query = "Delete SystemRecords Where ID=@ID;";

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = ID;

                        connection.Open();
                        int rows = command.ExecuteNonQuery();

                        return rows > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.Delete (SQL)", ex);
                return false;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.Delete (General)", ex);
                return false;
            }
        }
        public static List<clsSystemRecordEntity> GetAllSystemRecords()
        {
            var list = new List<clsSystemRecordEntity>();
            string query = "Select * From SystemRecords";

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
                            {
                                list.Add(MapReaderToSystemRecord(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetAllSystemRecords (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetAllSystemRecords (General)", ex);
            }

            return list;
        }
        public static async Task<(List<clsSystemRecordEntity> list , int TotalPages)> GetSystemRecordsPageAsync(int PageNumber , int PageSize ,string searchText=null)
        {
            int totalPage = 0;
            var list = new List<clsSystemRecordEntity>();

            try
            {


                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_GetSystemRecordPage", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@PageNumber",SqlDbType.Int).Value=PageNumber;
                        command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
                        command.Parameters.Add("@SearchText", SqlDbType.NVarChar,200).Value = string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : searchText;

                        await connection.OpenAsync().ConfigureAwait(false);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                        {
                            int totalCount = 0;
                            while (await reader.ReadAsync().ConfigureAwait(false))
                            {
                                list.Add(MapReaderToSystemRecord(reader));
                                
                                if(totalCount==0)
                                {
                                    totalCount = Convert.ToInt32(reader["TotalCount"]);
                                }
                            }

                            totalPage = (int)Math.Ceiling((double)totalCount/PageSize);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetSystemRecordsPage (SQL)", ex);
            }
            catch (Exception ex)
            {
                clsEventLogger.LogException("clsSystemRecordsData.GetSystemRecordsPage (General)", ex);
            }

            return (list,totalPage);
        }
        public static clsSystemRecordEntity MapReaderToSystemRecord(SqlDataReader reader)
        {

            return new clsSystemRecordEntity
            {
                ID = (int)reader["ID"],
                ActionType = reader["ActionType"].ToString(),
                DeviceName = reader["DeviceName"].ToString(),
                MachinID = reader["MachinID"].ToString(),
                Title = reader["Title"].ToString(),
                Description = reader["Description"].ToString(),
                CreatedDate = (DateTime)reader["CreatedDate"],
                UserID = (int)reader["UserID"]
            };

        }
    }
}
