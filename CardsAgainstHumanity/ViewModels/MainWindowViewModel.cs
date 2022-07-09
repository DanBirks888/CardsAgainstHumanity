using System;
using Avalonia;
using Avalonia.Platform;
using Avalonia.Shared.PlatformSupport;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;


namespace CardsAgainstHumanity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    { 
        private static string cardsOfHumanityPath =  $"../../../Assets/cardsAgainstHumanity.png";
        public string? Input { get; set; }
        
        public void CreateCards()
        {
            if (string.IsNullOrWhiteSpace(Input)) return;
            var cardNames = Input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var cardName in cardNames) CreateCard(cardName);
        }

        public static void CreateCard(string text)
        {
            var font = SystemFonts.CreateFont("Arial", 25, FontStyle.Bold);
            var image = Image.Load(cardsOfHumanityPath);

            TextOptions options = new(font)
            {
                Origin = new PointF(20, 20),
                TabWidth = 8, // A tab renders as 8 spaces wide
                WrappingLength = 220,
                HorizontalAlignment = HorizontalAlignment.Left,
                LineSpacing = 1
            };
            image.Mutate(m => m.DrawText(options, text, Color.Black));
            image.SaveAsPng($"../../../../../../Desktop/{GenerateName(text)}.png");
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