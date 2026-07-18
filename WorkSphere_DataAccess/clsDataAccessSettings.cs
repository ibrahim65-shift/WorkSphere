using System.Configuration;


namespace WorkSphere_DataAccess
{
    public static class clsDataAccessSettings
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["WorkSphereDB"].ConnectionString;
            }
        }
    }


}
