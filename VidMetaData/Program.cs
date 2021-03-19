using System;
using VidMetaData.Extractor;
using VidMetaData.Models;

namespace VidMetaData
{
    static class Program
    {
        private static ConsoleColor DefaultForegroundColor;

        static void Main()
        {
            try
            {
                DefaultForegroundColor = Console.ForegroundColor;

                var app = new MainApp();
                ConfigureProgress(app);
                ProcessVideoFiles(app);
                ProcessAudioFiles(app);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void ProcessVideoFiles(MainApp app)
        {
            var extractor = new VideoExtractor();
            var outputFile = app.Execute(extractor, Environment.CurrentDirectory);
            Console.WriteLine($"Video File Output = {outputFile}");
        }

        private static void ProcessAudioFiles(MainApp app)
        {
            var extractor = new AudioExtractor();
            var outputFile = app.Execute(extractor, Environment.CurrentDirectory);
            Console.WriteLine($"Audio File Output = {outputFile}");
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
