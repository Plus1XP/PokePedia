
using SkiaSharp;

using System.Collections.Generic;

namespace PokePedia.Models
{
    class PokemonCharts
    {
        public List<Microcharts.ChartEntry> chartEntries;

        public List<Microcharts.ChartEntry>GetChartEntries()
        {
            return chartEntries;
        }

        public Microcharts.ChartEntry AddChartEntries(string name, int? value, string colour)
        {
            return new Microcharts.ChartEntry((float)value)
            {
                Label = name,
                ValueLabel = value.ToString(),
                Color = SKColor.Parse(colour)
            };
        }
    }
}
