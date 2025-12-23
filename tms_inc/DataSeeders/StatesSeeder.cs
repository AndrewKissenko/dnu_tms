using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using tms.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tms.DataSeeders
{
    public class StatesSeeder : IDataSeeder
    {
        private PortalContext context;
        public StatesSeeder(PortalContext context)
        {
            this.context = context;
        }
        public async Task SeedAsync()
        {
            var cities = await context.Cities.ToListAsync();

            if (cities.Count == 0)
                return;

            var fullNameCities = cities.Where(x => string.IsNullOrEmpty(x.State) || x.State.Length > 2).ToList();

            if (fullNameCities.Count == 0)
            {
                return;
            }

            try
            {
                List<State> states = new List<State>();

                var url = "https://www.faa.gov/air_traffic/publications/atpubs/cnt_html/appendix_a.html";

                var doc = new HtmlWeb().Load(url);

                var rows = doc.DocumentNode.SelectNodes("//table//tr");
                if (rows == null || rows.Count == 0)
                    throw new Exception("FAA table rows not found.");

                foreach (var row in rows)
                {
                    var cells = row.SelectNodes("./th|./td");
                    if (cells == null) continue;

                    // Skip header rows (any <th> present)
                    if (row.SelectSingleNode("./th") != null) continue;

                    // Read pairs across the row
                    for (int i = 0; i + 1 < cells.Count; i += 2)
                    {
                        var stateFull = HtmlEntity.DeEntitize(cells[i].InnerText).Trim();
                        var stateShort = HtmlEntity.DeEntitize(cells[i + 1].InnerText).Trim();

                        if (string.IsNullOrWhiteSpace(stateFull) || string.IsNullOrWhiteSpace(stateShort))
                            continue;

                        // FAA often renders like "Arizona," -> remove trailing commas
                        stateFull = stateFull.TrimEnd(',', ' ');

                        states.Add(new State { StateFull = stateFull, StateShort = stateShort });
                    }
                }

                var dict = states
                    .GroupBy(s => Normalize(s.StateFull))
                    .ToDictionary(g => g.Key, g => g.First().StateShort);

                foreach (var city in fullNameCities)
                {
                    var key = Normalize(city.State);
                    if (dict.TryGetValue(key, out var abbr))
                        city.State = abbr;
                }

                await context.SaveChangesAsync();

                static string Normalize(string s)
                {
                    if (string.IsNullOrWhiteSpace(s)) return "";
                    s = s.Replace('\u00A0', ' ');                 // NBSP
                    s = System.Text.RegularExpressions.Regex.Replace(s, @"\s+", " ");
                    return s.Trim().TrimEnd(',').ToUpperInvariant();
                }

            }
            catch (System.Exception ex)
            {
                //log exception
                throw;
            }


        }
    }

    public class State
    {
        public string StateFull { get; set; }
        public string StateShort { get; set; }
    }
}
