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

namespace Marian_Bianca_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //lab4part3.2
            //creare obiect binding pentru comanda 
            CommandBinding cmd1 = new CommandBinding();
            //asociere comanda
            cmd1.Command = ApplicationCommands.Print;
            //lab4part1.2
            //inout gesture: I + Alt
            ApplicationCommands.Print.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));
            //asociem un handler
            cmd1.Executed += new ExecutedRoutedEventHandler(CtrlP_CommandHandler);
            //adaugam la colectia CommandBinding
            this.CommandBindings.Add(cmd1);
            //lab4part5.3
            //Doughnut>Stop
            //comanda custom
            CommandBinding cmd2 = new CommandBinding();
            cmd2.Command = CustomCommands.StopCommand.Launch;
            cmd2.Executed += new ExecutedRoutedEventHandler(CtrlS_CommandHandler);//asociem handler
            this.CommandBindings.Add(cmd2);
        }

        //lab3part13
        private DoughnutMachine myDoughnutMachine;
        //lab3part14
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            myDoughnutMachine = new DoughnutMachine();
            myDoughnutMachine.DoughnutComplete += new DoughnutMachine.DoughnutCompleteDelegate(DoughnutCompleteHandler);//lab3part19

            //lab4part12
            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key";
            cmbType.SelectedValuePath = "Value";
        }
        //lab3part15
        private int mRaisedGlazed;
        private int mRaisedSugar;
        private int mFilledLemon;
        private int mFilledChocolate;
        private int mFilledVanilla;
        //lab3part16
        private void exitToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//lab3part22
        }
        
        private void glazedToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            glazedToolStripMenuItem.IsChecked = true;//lab3part17
            sugarToolStripMenuItem.IsChecked = false;//lab3part17
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Glazed);//lab3part17
        }
        private void sugarToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            glazedToolStripMenuItem.IsChecked = false;//lab3part17
            sugarToolStripMenuItem.IsChecked = true;//lab3part17
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Sugar);//lab3part17
        }
        //lab3part18
        private void DoughnutCompleteHandler()
        {
            switch (myDoughnutMachine.Flavor)
            {
                case DoughnutType.Glazed:
                    mRaisedGlazed++;
                    txtGlazedRaised.Text = mRaisedGlazed.ToString();
                    break;
                case DoughnutType.Sugar:
                    mRaisedSugar++;
                    txtSugarRaised.Text = mRaisedSugar.ToString();
                    break;
                case DoughnutType.Lemon://lab4part3
                    mFilledLemon++;
                    txtLemonFilled.Text = mFilledLemon.ToString();
                    break;
                case DoughnutType.Chocolate://lab4part3
                    mFilledChocolate++;
                    txtChocolateFilled.Text = mFilledChocolate.ToString();
                    break;
                case DoughnutType.Vanilla://lab4part3
                    mFilledVanilla++;
                    txtVanillaFilled.Text = mFilledVanilla.ToString();
                    break;
             }
        }
        //lab3part20
        private void stopToolStripMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myDoughnutMachine.Enabled = false;//lab3part21
        }
        //lab3part23
        private void txtQuantity_KeyPress(object sender, KeyEventArgs e)
        {
            if(!(e.Key>=Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //lab4part5
        private void FilledItems_Click(object sender, RoutedEventArgs e)
        {
            string DoughnutFlavour;

            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            DoughnutFlavour = SelectedItem.Header.ToString();

            Enum.TryParse(DoughnutFlavour, out DoughnutType myFlavour);
            myDoughnutMachine.MakeDoughnuts(myFlavour);
        }
        //lab4part6
        private void FilledItemsShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;

            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            mesaj = SelectedItem.Header.ToString() + " doughnuts are being cooked";
            this.Title = mesaj;
        }
        //lab4part10
        KeyValuePair<DoughnutType, double>[] PriceList =
        {
            new KeyValuePair<DoughnutType, double>(DoughnutType.Sugar, 2.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Glazed, 3),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Chocolate, 4.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Vanilla, 4),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Lemon, 3.5)
        };
        //lab4part11
        DoughnutType selectedDoughnut;
        //lab4part14
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<DoughnutType, double> selectedEntry = (KeyValuePair<DoughnutType, double>)cmbType.SelectedItem;
            selectedDoughnut = selectedEntry.Key;
        }
       //lab4part15
        private int ValidateQuantity(DoughnutType selectedDoughnut)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;
            
            switch(selectedDoughnut)
            {
                case DoughnutType.Glazed:
                    if (q > mRaisedGlazed)
                        r = 0;
                    break;
                case DoughnutType.Sugar:
                    if (q > mRaisedSugar)
                        r = 0;
                    break;
                case (DoughnutType.Chocolate):
                    if (q > mFilledChocolate)
                        r = 0;
                    break;
                case (DoughnutType.Lemon):
                    if (q > mFilledLemon)
                        r = 0;
                    break;
                case (DoughnutType.Vanilla):
                    if (q > mFilledVanilla)
                        r = 0;
                    break;
            }
            return r;
        }
        //lab4part16
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateQuantity(selectedDoughnut) > 0)
            {
                lstSale.Items.Add(txtQuantity.Text + " " + selectedDoughnut.ToString() + ":" + txtPrice.Text + " " + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text));
            }
            else
            {
                MessageBox.Show("Cantitatea introdusa nu este disponibila in stoc");
            }
        }
        //lab4part19
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }
        //lab4part21
        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = (double.Parse(txtTotal.Text) + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text)).ToString();

            foreach(string s in lstSale.Items)
            {
                switch (s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") -1))
                {
                    case "Glazed":
                        mRaisedGlazed = mRaisedGlazed - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtGlazedRaised.Text = mRaisedGlazed.ToString();
                        break;
                    case "Sugar":
                        mRaisedSugar = mRaisedSugar - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtSugarRaised.Text = mRaisedSugar.ToString();
                        break;
                    case "Chocolate":
                        mFilledChocolate = mFilledChocolate - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtChocolateFilled.Text = mFilledChocolate.ToString();
                        break;
                    case "Lemon":
                        mFilledLemon = mFilledLemon - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtLemonFilled.Text = mFilledLemon.ToString();
                        break;
                    case "Vanilla":
                        mFilledVanilla = mFilledVanilla - Int32.Parse(s.Substring(0,
                       s.IndexOf(" ")));
                        txtVanillaFilled.Text = mFilledVanilla.ToString();
                        break;
                }
            }
        }
        //lab4part4.2
        private void CtrlP_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("You have in stock:" + mRaisedGlazed + " Glazed," + mRaisedSugar + " Sugar, "+mFilledLemon+" Lemon, "+mFilledChocolate+" Chocolate, "+mFilledVanilla+" Vanilla" );
        }
        //lab4part6.3
        private void CtrlS_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //handler pentru comanda Ctrl+S -> se va executa stopToolStripMenuItem_Click
            MessageBox.Show("Ctrl+S was pressed! The doughnut machine will stop!");
            this.stopToolStripMenuItem_Click(sender, e);
        }
    }
}