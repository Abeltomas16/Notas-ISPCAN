using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IspcaNotas.View.Grafico
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfessorGrafico : ContentPage
    {
        public ProfessorGrafico()
        {
            InitializeComponent();
            List<ChartEntry> entries = new List<ChartEntry>();
            entries.Add(new ChartEntry(20)
            {
                ValueLabel = "20",
                Label = "Aprovados",
                Color = SKColor.Parse("#00FEFE"),
                TextColor = SKColors.Black
            });
            entries.Add(new ChartEntry(10)
            {
                ValueLabel = "10",
                Label = "Reprovados",
                Color = SKColors.Red,
                TextColor = SKColors.Black
            });
            grafico1.Chart = new DonutChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                LabelTextSize = 25,
                AnimationDuration = TimeSpan.FromMilliseconds(200),
                IsAnimated = true
            };


            grafico2.Chart = new DonutChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                LabelTextSize = 25,
                AnimationDuration = TimeSpan.FromMilliseconds(200),
                IsAnimated = true
            };

            grafico3.Chart = new BarChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                LabelTextSize = 25,
                AnimationDuration = TimeSpan.FromMilliseconds(200),
                IsAnimated = true
            };
        }
    }
}