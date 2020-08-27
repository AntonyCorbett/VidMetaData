namespace VidMetaData.Models
{
    internal class ProgressEventArgs
    {
        public string FileName { get; set; }

        public string ProgressText { get; set; }

        public bool Error { get; set; }
    }
}
