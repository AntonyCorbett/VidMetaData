using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VidMetaData.Models;

namespace VidMetaData.FileHandling
{
    internal class OutputWriter
    {
        private const string Separator = "\t";

        public int Execute(string outputFilePath, IReadOnlyCollection<AbstractMediaMetaData> metaDataCollection)
        {
            if (!metaDataCollection.Any())
            {
                return 0;
            }

            var sb = new StringBuilder();
            sb.AppendLine(metaDataCollection.First().ToDelimitedHeaderText(Separator));
            foreach (var item in metaDataCollection)
            {
                sb.AppendLine(item.ToDelimitedText(Separator));
            }

            File.WriteAllText(outputFilePath, sb.ToString(), Encoding.UTF8);

            return metaDataCollection.Count;
        }
    }
}
