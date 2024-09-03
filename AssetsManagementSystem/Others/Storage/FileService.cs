namespace AssetsManagementSystem.Others.Storage
{
    public class FileService:IFileService
    {
        private readonly string _fileStoragePath;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _fileStoragePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
        }

        #region Upload PDF
        public async Task<string> UploadPdfAsync(Stream fileStream, string fileName)
        {
            if (fileStream == null || fileStream.Length == 0)
                throw new ArgumentException("Invalid file stream.");

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Invalid file name.");

            var filePath = Path.Combine(_fileStoragePath, fileName);

            // Ensure the directory exists
            if (!Directory.Exists(_fileStoragePath))
            {
                Directory.CreateDirectory(_fileStoragePath);
            }

            // Save the file to the specified path
            using (var fileStreamOutput = new FileStream(filePath, FileMode.Create))
            {
                await fileStream.CopyToAsync(fileStreamOutput);
            }

            return filePath;
        }
        #endregion

        #region Download PDF
        public async Task<Stream> DownloadPdfAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            var fullPath = Path.Combine(_fileStoragePath, filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("File not found.", fullPath);

            return new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        }
        #endregion

        #region Get PDF File Info
        public FileInfo GetPdfFileInfo(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            var fullPath = Path.Combine(_fileStoragePath, filePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("File not found.", fullPath);

            return new FileInfo(fullPath);
        }
        #endregion

        #region Remove PDF
        public void RemovePdf(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or empty.");

            var fullPath = Path.Combine(_fileStoragePath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            else
            {
                throw new FileNotFoundException("File not found.", fullPath);
            }
        }
        #endregion
    }
}
