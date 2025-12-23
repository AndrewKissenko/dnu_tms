using HtmlAgilityPack;
using tms.DataAccess;
using tms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms.DataSeeders
{
    public class CitiesSeeder : IDataSeeder
    {
        private PortalContext context;

        public CitiesSeeder(PortalContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (context.Cities.Count() > 100) 
                return;

            List<City> cities = new List<City>();
            try
            {
                var url = "https://www.britannica.com/topic/list-of-cities-and-towns-in-the-United-States-2023068";

                // Optional but helps avoid occasional blocking
                var web = new HtmlWeb();

                var doc = web.Load(url);

                // Narrow the search to the article content
                var article = doc.DocumentNode.SelectSingleNode("//main//article") ?? doc.DocumentNode;

                // Each state is an H2; the cities are in the first UL that follows it
                var stateHeaders = article.SelectNodes(".//h2") ?? new HtmlNodeCollection(null);

                foreach (var h2 in stateHeaders)
                {
                    var state = HtmlEntity.DeEntitize(h2.InnerText).Trim();
                    if (string.IsNullOrWhiteSpace(state))
                        continue;

                    // The cities list is the next UL after the H2
                    var ul = h2.SelectSingleNode("following-sibling::ul[1]");
                    if (ul == null)
                        continue;

                    // Cities are links inside the list items (fallback to li text if no <a>)
                    var cityNodes = ul.SelectNodes(".//li") ?? new HtmlNodeCollection(null);

                    foreach (var li in cityNodes)
                    {
                        var a = li.SelectSingleNode(".//a");
                        var cityName = HtmlEntity.DeEntitize((a ?? li).InnerText).Trim();

                        if (!string.IsNullOrWhiteSpace(cityName))
                        {
                            cities.Add(new City { State = state, CityName = cityName });
                        }
                    }
                }

                await context.Cities.AddRangeAsync(cities);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // todo: log error (ex)
                throw;
            }

        }
    }
}
