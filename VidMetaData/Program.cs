using System;
using System.Linq;
using VidMetaData.Extractor;
using VidMetaData.Models;

namespace VidMetaData
{
    static class Program
    {
        private static ConsoleColor DefaultForegroundColor;

        static void Main(string[] args)
        {
            try
            {
                DefaultForegroundColor = Console.ForegroundColor;

                var app = new MainApp();
                ConfigureProgress(app);

                ShowUsage();

                var folder = Environment.CurrentDirectory;
                var includeSubFolders = args.Contains("/s");
                var useParallelProcessing = args.Contains("/p");

                ProcessVideoFiles(app, folder, includeSubFolders, useParallelProcessing);
                ProcessAudioFiles(app, folder, includeSubFolders, useParallelProcessing);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ShowUsage()
        {
            Console.WriteLine("VidMetaData extracts MP3 and MP4 meta data from the current folder");
            Console.WriteLine("storing it in tab-delimited files with '.tsv' extensions.");
            Console.WriteLine();
            Console.WriteLine("USAGE");
            Console.WriteLine("=====");
            Console.WriteLine(" Copy the VidMetaData.exe file to your media folder and run it with");
            Console.WriteLine(" the following optional command-line switches:");
            Console.WriteLine();
            Console.WriteLine("   /s (also includes sub-directories)");
            Console.WriteLine("   /p (uses parallel processing to improve performance)");
            Console.WriteLine();
        }

        private static void ProcessVideoFiles(MainApp app, string folder, bool includeSubFolders, bool useParallelProcessing)
        {
            var extractor = new VideoExtractor();
            var result = app.Execute(extractor, folder, includeSubFolders, useParallelProcessing);

            if (result.count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{result.count} video files. Output = {result.outputPath}");
                Console.WriteLine();
            }
        }

        private static void ProcessAudioFiles(MainApp app, string folder, bool includeSubFolders, bool useParallelProcessing)
        {
            var extractor = new AudioExtractor();
            var result = app.Execute(extractor, folder, includeSubFolders, useParallelProcessing);

            if (result.count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{result.count} audio files. Output = {result.outputPath}");
                Console.WriteLine();
            }
        }

        private static void ConfigureProgress(MainApp app)
        {
            app.ProgressEvent += delegate (object sender, ProgressEventArgs args)
            {
                Console.ForegroundColor = args.Error
                    ? ConsoleColor.Red
                    : DefaultForegroundColor;

                Console.WriteLine(args.ProgressText);
            };
        }
    }
}
