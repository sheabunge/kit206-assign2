using HRIS.Control;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HRIS {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        StaffController staffController;
        public MainWindow() {
            staffController = (StaffController)Application.Current.FindResource("StaffController");
            InitializeComponent();
        }

        private void SelectCategory(object sender, SelectionChangedEventArgs e) {
            Teaching.Category currentCategory = (Teaching.Category) e.AddedItems[0];
            Console.WriteLine();
           staffController.FilterBy(currentCategory);
        }
    }
}
