using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for Carte.xaml
    /// </summary>
    public partial class Carte : UserControl
    {
        private int x;
        private int y;
        public Carte(int PosX, int PosY, int JeuxGrandeur, ImageCarte img)
        {
            InitializeComponent();

            btn.Height = 500 / JeuxGrandeur;
            btn.Width = 500 / JeuxGrandeur;

            btn.Background = Brushes.Aqua;

            btn.Click += (sender, e) =>
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri((Directory.GetCurrentDirectory() + img.imageName + ".png"), UriKind.Relative));
                btn.Background = brush;
            };
        }
    }
}
