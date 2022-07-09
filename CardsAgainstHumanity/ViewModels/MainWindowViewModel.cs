using System;
using System.IO;
using Avalonia;
using Avalonia.Platform;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;


namespace CardsAgainstHumanity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    { 
        public string? Input { get; set; }

     
        
        public void CreateCards()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;
            var cardNames = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cardName in cardNames) CreateCard(cardName);
        }

        private static void CreateCard(string text)
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            var img = assets.Open(new Uri("avares://CardsAgainstHumanity/Assets/cardsAgainstHumanity.png"));
            var font = SystemFonts.CreateFont("Arial", 25, FontStyle.Bold);
            var image =  Image.Load(img);

            TextOptions options = new(font)
            {
                Origin = new PointF(20, 20),
                TabWidth = 8,
                WrappingLength = 220,
                HorizontalAlignment = HorizontalAlignment.Left,
                LineSpacing = 1
            };
            image.Mutate(m => m.DrawText(options, text, Color.Black));
            
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Cards Of Humanity");
            Directory.CreateDirectory(path);
            image.SaveAsPng($"{path}/{GenerateName(text)}.png");
        }

        private static string GenerateName(string name)
        {
            var cardName = "";
            var ts = name.Split(" ");
            for (var i = 0; i < ts.Length; i++)
            {
                if (i < ts.Length) cardName += " ";
                cardName += $"{ts[i]}";
            }
            return cardName;
        }
    }
}