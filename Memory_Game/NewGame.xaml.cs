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
                txtbNomJoueur2.IsEnabled = false;
            }
            else if (cbxNbrJoueurItem2.IsSelected)
            {
                if (cbxDebutePartieItemJoueur2 == null)
                    cbxDebutePartieItemJoueur2 = new ComboBoxItem();
                if (cbxDebutePartieItemOrdinateur == null)
                    cbxDebutePartieItemOrdinateur = new ComboBoxItem();
                if (txtbNomJoueur2 == null)
                    txtbNomJoueur2 = new TextBox();

                txtbNomJoueur2.IsEnabled = true;

                cbxDebutePartieItemJoueur2.IsEnabled = true;
                cbxDebutePartieItemOrdinateur.IsEnabled = false;
            }
        }

        private void txtbNomJoueur1_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbxDebutePartieItemJoueur1.Content = txtbNomJoueur1.Text;
        }

        private void txtbNomJoueur2_TextChanged(object sender, TextChangedEventArgs e)
        {
            cbxDebutePartieItemJoueur2.Content = txtbNomJoueur2.Text;
        }
    }
}
