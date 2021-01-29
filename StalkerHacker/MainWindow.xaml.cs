using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ablak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database database;
        double progres = 0;
        double sec = 30;
        bool animate = false;
        int keypressed = 0;
        Queue<Key> pressedkeys = new Queue<Key>();

        int iT = 0;
        string hackingstring = "";
        string spaceholder = "";
        string outputstring = "";




        public MainWindow()
        {
            InitializeComponent();

            pressedkeys.Enqueue(Key.A);
            pressedkeys.Enqueue(Key.B);
            pressedkeys.Enqueue(Key.C);
            pressedkeys.Enqueue(Key.D);
            pressedkeys.Enqueue(Key.E);

            try
            {
            hackingstring = System.IO.File.ReadAllText("hackingstring.txt");
            database = new Database("pass.txt");
            }catch(Exception e)
            {
                outputbox.Text = "Hacking Mainframe cant be loaded!!!";
            }
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += timer_Tick;
            timer.Start();




        }

        private void timer_Tick(object sender, EventArgs e)
        {
            progres = (double)keypressed/5.0;
            progresbar.Value = progres;

            //double step = 100/sec;
            if(animate)
            {
                //  progres += step;
                outputbox.Text = spaceholder+outputstring; 
                
                //if (iT >= flipbook.Length)
                 //   iT = 0;
                if (progres>100)
                {
                    gomb.IsEnabled = true;
                    string key = inputbox.Text;
                    string value = database.getPass(key);
                    if(value!=null)
                        outputbox.Text = "Password found:\n"+value;
                    else
                        outputbox.Text = "!!!CRACKING FAILED!!!";
                    animate = false;
                    inputbox.Focusable = true;
                }
                outputbox.ScrollToEnd();
            }
        }

        private void gomb_Click(object sender, RoutedEventArgs e)
        {
            //todo anime
            gomb.IsEnabled = false;
            progres = 0;
            outputbox.Clear();
            iT = 0;
            spaceholder = "";
            keypressed = 0;
            animate = true;
            inputbox.Focusable = false;


        }

        private void inputbox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter && !animate)
            {
                gomb.IsEnabled = false;
                progres = 0;
                outputbox.Clear();
                iT = 0;
                spaceholder = "";
                keypressed = 0;
                animate = true;
                inputbox.Focusable = false;
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string name = Regex.Replace(files[0], @"\w*\:\\(.)*\\", "");
            name = name.Split('.')[0];
            inputbox.Text = name;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (!pressedkeys.Contains(e.Key))
            {
                pressedkeys.Dequeue();
                keypressed++;
                pressedkeys.Enqueue(e.Key);
                iT += 5;
                if(iT>hackingstring.Length)
                {
                    spaceholder = hackingstring;
                    iT = 0;
                }
                outputstring = hackingstring.Substring(0, iT);
            }

        }
    }
}
