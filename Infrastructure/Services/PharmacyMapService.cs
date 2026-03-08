using Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json.Linq;
using SerpApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PharmacyMapService : IMapService
    {
        private readonly string _apiKey = "27d10abcfb8c381717ad4ff617cbbf6282524b0e3c54f32016e673defdc9eb58";

        public async Task<(double Lat, double Lng)?> GetCoordinatesAsync(string address)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("engine", "google_maps");
                ht.Add("q", address);
                ht.Add("api_key", _apiKey);

                GoogleSearch search = new GoogleSearch(ht, _apiKey);
                JObject data = search.GetJson();

                
                var location = data["place_results"]?["gps_coordinates"];

                if (location != null)
                {
                    double lat = (double)location["latitude"];
                    double lng = (double)location["longitude"];
                    return (lat, lng);
                }
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"SerpApi greška: {ex.Message}");
            }

            return null;
        }
    }
}
