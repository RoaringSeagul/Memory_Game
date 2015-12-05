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
        internal bool clicked = false;
        int positionX;
        int positionY;
        public Carte(int PosX, int PosY, int JeuxGrandeur, MainWindow main)
        {
            InitializeComponent();

            positionX = PosX;
            positionY = PosY;

            btn.Height = 500 / JeuxGrandeur;
            btn.Width = 500 / JeuxGrandeur;

            btn.Background = Brushes.Aqua;

            btn.Click += (sender, e) =>
            {
                main.update(positionX, positionY);
            };
        }

        internal void SetBackground(Brush brush)
        {
            btn.Background = brush;
        }

        internal void ShowBackground(MainWindow main)
        {
            clicked = true;
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(main.logique.GetImage(positionX, positionY).imageName, UriKind.Relative));
            btn.Background = brush;
        }

        internal void Disable()
        {
            btn.IsEnabled = false;
        }
        internal void Enable()
        {
            btn.IsEnabled = true;
        }
    }
}
