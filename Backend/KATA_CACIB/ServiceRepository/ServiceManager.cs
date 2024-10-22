namespace KATA_CACIB.ServiceRepository
{
    using Microsoft.Extensions.Caching.Memory;
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    public class ServiceManager
    {
        // Concurrent dictionary to store the UUID and corresponding MemoryStream
        private readonly ConcurrentDictionary<Guid, MemoryStream> _streams = new();
        private readonly IMemoryCache _cache;

        public ServiceManager(IMemoryCache cache)
        {
            _cache = cache;
        }
        public Guid CallService(string action)
        {
            // Generate a new UUID
            var uuid = Guid.NewGuid();

            // Create a new MemoryStream
            var memoryStream = new MemoryStream();

            // Stocker le MemoryStream dans le cache avec un UUID comme clé
            _cache.Set(uuid, memoryStream);

            // Start the background task
            _ = Task.Run(() => WriteRandomTextToStream(uuid, memoryStream , action));

            // Return the UUID to the caller immediately
            return uuid;
        }

        // Method to retrieve the MemoryStream associated with a UUID
        public MemoryStream GetStreamByUUID(Guid uuid)
        {
            if (_cache.TryGetValue(uuid, out MemoryStream memoryStream))
            {
                if(memoryStream != null)
                {
                    Console.WriteLine("Stream content found in MemoryCache: " + memoryStream.Length);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return memoryStream;
                }
                   
            }
            Console.WriteLine("Stream content not found in MemoryCache: ");
            return null;
        }

        // Background task that writes random text to the stream every second for 3 minutes
        private async Task WriteRandomTextToStream(Guid uuid, MemoryStream memoryStream , string action)
        {
            var random = new Random();
            var stopTime = DateTime.Now.AddMinutes(3); // Stop after 3 minutes
            var writer = new StreamWriter(memoryStream, Encoding.UTF8, leaveOpen: true);
            writer.WriteLine($"Lancement de l'action : {action} at {DateTime.Now}");
            Console.WriteLine($"Lancement de l'action : {action} at {DateTime.Now}");
            await writer.FlushAsync();

            while (DateTime.Now < stopTime)
            {
                // Generate a random text (for example, a random string of 10 characters)
                var randomText = GenerateRandomString(random, 10);

                // Write the text to the MemoryStream
                await writer.WriteLineAsync($"Génération d'un text  : [{randomText}] at {DateTime.Now}");
                await writer.FlushAsync(); // Ensure the text is written to the stream
                 
                // Debug: check the content of the stream at this point
                //memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position for reading
                //using (var reader = new StreamReader(memoryStream, Encoding.UTF8, leaveOpen: true))
                //{
                //    var contentSoFar = await reader.ReadToEndAsync();
                //    Console.WriteLine("Stream content at this point: " + contentSoFar);
                //}
                // Wait for 1 second
                await Task.Delay(1000);
            }

            // Close the writer and complete the task
            writer.WriteLine($"End of stream : {action} at {DateTime.Now}");
            await writer.FlushAsync();

            // Debug: check the content of the stream at this point
            memoryStream.Seek(0, SeekOrigin.Begin); // Reset stream position for reading
            using (var reader = new StreamReader(memoryStream, Encoding.UTF8, leaveOpen: true))
            {
                var contentSoFar = await reader.ReadToEndAsync();
                Console.WriteLine("Stream content at this point: " + contentSoFar);
            }

            writer.Close();


        }

        // Helper method to generate a random string of given length
        private static string GenerateRandomString(Random random, int length)
        {
            Random rand = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string result = new string(Enumerable.Repeat(chars, 10).Select(s => s[rand.Next(s.Length)]).ToArray());
            return result;
        }
    }
}
