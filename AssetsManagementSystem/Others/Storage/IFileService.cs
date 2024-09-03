namespace AssetsManagementSystem.Others.Storage
{
    public interface IFileService
    {
        Task<string> UploadPdfAsync(Stream fileStream, string fileName);
        Task<Stream> DownloadPdfAsync(string filePath);
        FileInfo GetPdfFileInfo(string filePath);
        void RemovePdf(string filePath);
    }
}
