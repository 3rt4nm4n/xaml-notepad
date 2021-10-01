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
using System.IO;

namespace xaml_notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string reserved_words_path= "C:\\Users\\nigbu\\source\\repos\\xaml-notepad\\reserved_words.txt";
        public string filepath=null;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myGrid.Width = e.NewSize.Width;
            myGrid.Height = e.NewSize.Height;

            double xc = 1, yc = 1;
            if (e.PreviousSize.Width != 0)
            {
                xc = (e.NewSize.Width / e.PreviousSize.Width);
            }
            if (e.PreviousSize.Height != 0)
            {
                yc = (e.NewSize.Height / e.PreviousSize.Height);
            }
            ScaleTransform scale = new ScaleTransform(myGrid.LayoutTransform.Value.M11 * xc, myGrid.LayoutTransform.Value.M22 * yc);
            myGrid.LayoutTransform = scale;
            myGrid.UpdateLayout();
        }

        private void CodeTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //fetch input words to a text file and if a letter is typed advise prediction to the user
            /*string[] res=File.ReadAllLines(reserved_words_path);
            foreach(var r in res)
            {
                string[] rr = r.Split(',');
                TextRange range = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
                string codelines = range.Text;
                if ()
            }*/
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if reserved make it blue
            //CodeTextBox.Selection.ApplyPropertyValue(RichTextBox.ForegroundProperty, Brushes.Blue);
            
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
             
                Microsoft.Win32.SaveFileDialog dg = new Microsoft.Win32.SaveFileDialog();
                dg.FileName = "new.xaml";
                dg.DefaultExt = ".xaml";
                dg.Filter = "eXtensible Markup Language .xaml|*.xaml";
                Nullable<bool> result = dg.ShowDialog();
                if (result == true)
                {
                    string filename = dg.FileName;
                    TextRange range = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
                    string codelines = range.Text;
                    filepath = Path.GetFullPath(dg.FileName);
                    File.WriteAllText(filename, codelines);
                

                }
                CodeTextBox.Document.Blocks.Clear();
            
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog og = new Microsoft.Win32.OpenFileDialog();
           
            if (og.ShowDialog() == true)
            {
                
                FlowDocument content = new FlowDocument(new Paragraph(new Run(File.ReadAllText(og.FileName))));
                CodeTextBox.Document = content;
                filepath = Path.GetFullPath(og.FileName);

            }
            
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult dr = MessageBox.Show("Would you like to save your file?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (dr == MessageBoxResult.Yes)
            {
                
                SaveAsButton_Click(sender, e);
                CodeTextBox.Document.Blocks.Clear();
            }
            else
                CodeTextBox.Document.Blocks.Clear();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            string filename = Path.GetFileName(filepath);
            TextRange range = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
            string codelines = range.Text;
            File.WriteAllText(filename, codelines);
        }
    }
}
