using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;

namespace PdfMerge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Check if command-line arguments are provided
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: PDFMergeTool <output_file_path> <input_file1_path> <input_file2_path> ...");
                return;
            }

            string outputFile = args[0];
            List<string> inputFiles = [];

            // Add input files to the list
            for (int i = 1; i < args.Length; i++)
            {
                inputFiles.Add(args[i]);
            }

            MergePDFs(outputFile, inputFiles);

            Console.WriteLine("PDF files merged successfully!");
        }

        static void MergePDFs(string outputFile, List<string> inputFiles)
        {
            PdfDocument outputDocument = new();

            foreach (string inputFile in inputFiles)
            {
                using PdfDocument inputDocument = PdfReader.Open(inputFile, PdfDocumentOpenMode.Import);
                int pageCount = inputDocument.PageCount;
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    PdfPage page = inputDocument.Pages[pageIndex];
                    outputDocument.AddPage(page);
                }
            }

            outputDocument.Save(outputFile);
        }
    }
}
