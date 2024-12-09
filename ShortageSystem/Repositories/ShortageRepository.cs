using ShortageSystem.Identity;
using ShortageSystem.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace ShortageSystem.Repositories
{
    public class ShortageRepository : IShortageRepository
    {
        //Setting Json serializer options
        JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        //Setting storage file path
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shortages.json");

        public async Task AddShortageAsync(Shortage shortage)
        {
            List<Shortage> shortages = await GetAllShortagesAsync();

            //Getting shortage with same title and room
            var existingShortage = shortages.FirstOrDefault(s =>
                s.Title.Equals(shortage.Title, StringComparison.OrdinalIgnoreCase) &&
                s.Room == shortage.Room);

            //If such shortage exists with lower priority, its updated
            if (existingShortage != null)
            {
                if (existingShortage.Priority < shortage.Priority)
                {
                    shortages.Remove(existingShortage);
                    shortages.Add(shortage);
                    Console.WriteLine("Existing shortage updated due to higher priority.");
                }
                else
                {
                    Console.WriteLine("Shortage already exists with equal or higher priority.");
                    return;
                }
            }
            //Otherwise shortage is added
            else
            {
                shortages.Add(shortage);
            }

            //Shortage saved to file
            await SaveShortagesToFileAsync(shortages);
        }

        public async Task DeleteShortageAsync(Shortage shortage)
        {
            if(shortage != null)
            {
                List<Shortage> shortages = await GetAllShortagesAsync();
                //Only user who created the shortage or admin can delete the shortage
                if (shortage.CreatedBy.Equals("admin", StringComparison.OrdinalIgnoreCase) || shortage.CreatedBy.Equals(ApplicationUser.UserName, StringComparison.OrdinalIgnoreCase))
                {
                    shortages.Remove(shortage);
                    await SaveShortagesToFileAsync(shortages);
                }
                else
                    Console.WriteLine("You do not have permission to delete this shortage.");
            }
        }

        public async Task<List<Shortage>> GetAllShortagesAsync()
        {
            //If file doesnt exist at given path, its created
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }

            try
            { 
                //Reading all json data from file
                string json = await File.ReadAllTextAsync(filePath);
                //Deserializing json data and returning deserialized list of shortages
                return JsonSerializer.Deserialize<List<Shortage>>(json, options);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File I/O error: {ex.Message}");
            }
            return new List<Shortage>();

        }


        public async Task<List<Shortage>> GetShortagesByUserAsync(string userName)
        {
            List<Shortage> shortages = await GetAllShortagesAsync();

            //If user is admin returning all shorages, otherwise only shortages given user created
            if (userName.Equals("admin", StringComparison.OrdinalIgnoreCase))
                return shortages;
            else
                return shortages.Where(s => s.CreatedBy.Equals(userName, StringComparison.OrdinalIgnoreCase)).ToList();

        }

        private async Task SaveShortagesToFileAsync(List<Shortage> shortages)
        {
            try
            {
                //Serializing the list
                var serializedObject = JsonSerializer.Serialize(shortages, options);
                using (StreamWriter swl = new StreamWriter(filePath, false))
                {
                    //Asynchorously writing serialized data to file
                    await swl.WriteLineAsync(serializedObject);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occurred while saving shortages: {e.Message}");
            }
        }

    }
}
