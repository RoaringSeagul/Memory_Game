using System;
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
using System.Windows.Shapes;

namespace Memory_Game
{
    /// <summary>
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        public NewGame()
        {
            InitializeComponent();
        }

        private void cbxNbrJoueurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxNbrJoueurItem1.IsSelected)
            {
                cbxDebutePartieItemJoueur2.IsEnabled = false;
                cbxDebutePartieItemOrdinateur.IsEnabled = true;
            }
            else if (cbxNbrJoueurItem2.IsSelected)
            {
                if (cbxDebutePartieItemJoueur2 == null)
                    cbxDebutePartieItemJoueur2 = new ComboBoxItem();
                if (cbxDebutePartieItemOrdinateur == null)
                    cbxDebutePartieItemOrdinateur = new ComboBoxItem();

                cbxDebutePartieItemJoueur2.IsEnabled = true;
                cbxDebutePartieItemOrdinateur.IsEnabled = false;
            }
        }
    }
}
