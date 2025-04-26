namespace EmpManagement.Data
{
    public static class AppConstant
    {
        public static IWebHostEnvironment hostEnvironment { get;set; }
        public static  void writeLog(string Exception)
        {
            string fileName = DateTime.Now.ToString("dd/MM/yyyy");

            // Ensure the logs directory exists
            string logsDir = Path.Combine(AppConstant.hostEnvironment.WebRootPath, "Logs");
            if (!Directory.Exists(logsDir))
            {
                Directory.CreateDirectory(logsDir);
            }

            // Full file path
            string filePath = Path.Combine(logsDir, $"Log_{fileName}.txt");

            // Check if the file exists, and create it if not
            if (!File.Exists(filePath))
            {
                try
                {
                    // Create and close the file (ensure it's created with appropriate permissions)
                    File.Create(filePath).Close();
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Handle the access exception, possibly log it or notify the user
                    Console.WriteLine($"Access denied to file path: {filePath}. {ex.Message}");
                }
            }

            StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine(Exception);
                 sw.Dispose();

            

        }
    }
}
