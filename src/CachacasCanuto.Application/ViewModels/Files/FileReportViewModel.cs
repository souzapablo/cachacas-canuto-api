namespace CachacasCanuto.Application.ViewModels.Files
{
    public class FileReportViewModel
    {
        public FileReportViewModel(byte[] content, string fileReportName, string fileReportType)
        {
            Content = content;
            FileReportName = fileReportName;
            FileReportType = fileReportType;
        }

        public byte[] Content { get; set; }
        public string FileReportName { get; set; }
        public string FileReportType { get; set; }
    }
}
