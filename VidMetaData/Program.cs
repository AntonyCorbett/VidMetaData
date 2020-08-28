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

                //var extractor = new TagLibExtractor();
                var extractor = new WinShellExtractor();

                var outputFile = app.Execute(extractor, Environment.CurrentDirectory);
                Console.WriteLine($"Output = {outputFile}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
