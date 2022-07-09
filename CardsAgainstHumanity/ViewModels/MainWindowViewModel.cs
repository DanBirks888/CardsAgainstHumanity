using System.IO;
using System.Media;
using Avalonia.Controls;
using Avalonia.Data;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using Tmds.DBus;
using Image = SixLabors.ImageSharp.Image;


namespace CardsAgainstHumanity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Bakky => "transparent";
        public string Title => "Cards Of Humanity";
        public string Title2 => "Cards Of Humanity";
        private static string btnClickPath = "CardsAgainstHumanity/Assets/MH - Item Found.mp3";
        private static string cardsOfHumanityPath = "CardsAgainstHumanity/Assets/cardsAgainstHumanity.png";


        public void CreateCards()
        {
            var player = new SoundPlayer(btnClickPath);
        }

        public static void CreateAllCards(string path)
        {
            var cardNames = File.ReadAllLines(path);
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
            image.SaveAsPng($"../../../../../Desktop/Test Cards/{GenerateName(text)}.png");
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