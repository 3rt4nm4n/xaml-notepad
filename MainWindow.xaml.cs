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
using System.Windows.Controls.Primitives;

namespace xaml_notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string reserved_words_path= "C:\\Users\\nigbu\\source\\repos\\xaml-notepad\\reserved_words.txt";
        public string filepath=null;
        public string filename;
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

            Microsoft.Win32.SaveFileDialog dg = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "new.xaml",
                DefaultExt = ".xaml",
                Filter = "eXtensible Markup Language .xaml|*.xaml"
            };
            Nullable<bool> result = dg.ShowDialog();
                if (result == true)
                {
                    filename = dg.FileName;
                    TextRange range = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
                    string codelines = range.Text;
                    filepath = Path.GetFullPath(dg.FileName);
                    File.WriteAllText(filename, codelines);
                

                }
            this.Title = "XAML Notepad - " + filename;

        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog og = new Microsoft.Win32.OpenFileDialog();
           
            if (og.ShowDialog() == true)
            {
                filename = og.FileName; 
                FlowDocument content = new FlowDocument(new Paragraph(new Run(File.ReadAllText(og.FileName))));
                CodeTextBox.Document = content;
                filepath = Path.GetFullPath(og.FileName);
                this.Title = "XAML Notepad - " + filename;
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
            if (filename != null)
            {

                TextRange range = new TextRange(CodeTextBox.Document.ContentStart, CodeTextBox.Document.ContentEnd);
                string codelines = range.Text;
                //MessageBox.Show("Saving to " + filepath, "Information", MessageBoxButton.OK, MessageBoxImage.Information); //gives notification about saving
                File.WriteAllText(filename, codelines);
            }
            else
                SaveAsButton_Click(sender,e);
            this.Title = "XAML Notepad - " + filename;

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = (ToolBoxList.SelectedItem as ListBoxItem).Content.ToString();
            
            
            if (obj != null)
            {
                switch (obj.ToString()) {
                    case "Pointer":
                        break;
                    case "Border":
                        CodeTextBox.AppendText("<Border BorderBrush=\"Black\" BorderThickness=\"1\" HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Button":
                        CodeTextBox.AppendText("<Button Content=\"Button\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\" Width=\"75\"/>");
                        break;
                    case "Calendar":
                        CodeTextBox.AppendText("<Calendar HorizontalAlignment=\"Left\" VerticalAlignment =\"Top\" />");
                        break;
                    case "Canvas":
                        CodeTextBox.AppendText("<Canvas HorizontalAlignment=\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "CheckBox":
                        CodeTextBox.AppendText("<CheckBox Content=\"CheckBox\" HorizontalAlignment =\"Left\" VerticalAlignment =\"Top\" />");
                        break;
                    case "ComboBox":
                        CodeTextBox.AppendText("<ComboBox HorizontalAlignment=\"Left\" VerticalAlignment =\"Top\" Width =\"120\" />");
                        break;
                    case "Comment":
                        CodeTextBox.AppendText("<!---->");
                        break;
                    case "ContentController":
                        CodeTextBox.AppendText("<ContentControl Content=\"ContentControl\" HorizontalAlignment =\"Left\" VerticalAlignment =\"Top\" />");
                        break;
                    case "DataGrid":
                        CodeTextBox.AppendText("<DataGrid HorizontalAlignment=\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "DatePicker":
                        CodeTextBox.AppendText("<DatePicker HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\"/>");
                        break;
                    case "DockPanel":
                        CodeTextBox.AppendText("<DockPanel HorizontalAlignment=\"Left\" Height=\"100\" LastChildFill=\"False\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "DocumentViewer":
                        CodeTextBox.AppendText("<DocumentViewer HorizontalAlignment=\"Left\" Margin =\"0, 0, -0.4, 0\" VerticalAlignment =\"Top\"/>");
                        break;
                    case "Ellipse":
                        CodeTextBox.AppendText("<Ellipse Fill=\"#FFF4F4F5\" HorizontalAlignment=\"Left\" Height=\"100\" Stroke=\"Black\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Expander":
                        CodeTextBox.AppendText("<Expander Header=\"Expander\" HorizontalAlignment =\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\">\n< Grid Background = \"#FFE5E5E5\" />\n</ Expander > ");
                        break;
                    case "Frame":
                        CodeTextBox.AppendText("<Frame Content=\"Frame\" HorizontalAlignment =\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "Grid":
                        CodeTextBox.AppendText("<Grid HorizontalAlignment=\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "GridSplitter":
                        CodeTextBox.AppendText("<GridSplitter HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"5\"/>");
                        break;
                    case "GroupBox":
                        CodeTextBox.AppendText("<GroupBox Header=\"GroupBox\" HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Image":
                        CodeTextBox.AppendText("<Image HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Label":
                        CodeTextBox.AppendText("<Label Content=\"Label\" HorizontalAlignment =\"Left\" VerticalAlignment =\"Top\" />");
                        break;
                    case "ListBox":
                        CodeTextBox.AppendText("<ListBox HorizontalAlignment=\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "ListView":
                        CodeTextBox.AppendText("<ListView HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\">\n<ListView.View>\n<GridView>\n<GridViewColumn/>\n</GridView>\n</ListView.View>\n</ListView>");
                        break;
                    case "MediaElement":
                        CodeTextBox.AppendText("<MediaElement HorizontalAlignment=\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "Menu":
                        CodeTextBox.AppendText("<Menu HorizontalAlignment=\"Left\" Height =\"100\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "PasswordBox":
                        CodeTextBox.AppendText("<PasswordBox HorizontalAlignment=\"Left\" VerticalAlignment =\"Top\" />");
                        break;
                    case "ProgressBar":
                        CodeTextBox.AppendText("<ProgressBar HorizontalAlignment=\"Left\" Height=\"10\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "RadioButton":
                        CodeTextBox.AppendText("<RadioButton Content=\"RadioButton\" HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\"/>");
                        break;
                    case "Rectangle":
                        CodeTextBox.AppendText("<Rectangle Fill=\"#FFF4F4F5\" HorizontalAlignment =\"Left\" Height =\"100\" Stroke =\"Black\" VerticalAlignment =\"Top\" Width =\"100\" />");
                        break;
                    case "RichTextBox":
                        CodeTextBox.AppendText("<RichTextBox HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\">\n<FlowDocument>\n<Paragraph>\n<Run Text=\"RichTextBox\"/>\n</Paragraph>\n</FlowDocument>\n</RichTextBox>");
                        break;
                    case "ScrollBar":
                        CodeTextBox.AppendText("<ScrollBar HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\"/>");
                        break;
                    case "ScrollViewer":
                        CodeTextBox.AppendText("<ScrollViewer HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Separator":
                        CodeTextBox.AppendText("<Separator HorizontalAlignment=\"Left\" Height=\"100\" Margin=\"0\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Slider":
                        CodeTextBox.AppendText("<Slider HorizontalAlignment=\"Left\" VerticalAlignment=\"Top\"/>");
                        break;
                    case "StackPanel":
                        CodeTextBox.AppendText("<StackPanel HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "StatusBar":
                        CodeTextBox.AppendText("<StatusBar HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "TabControl":
                        CodeTextBox.AppendText("<TabControl HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\">\n<TabItem Header=\"TabItem\">\n<Grid Background=\"#FFE5E5E5\"/>\n</TabItem>\n<TabItem Header=\"TabItem\">\n<Grid Background=\"#FFE5E5E5\"/>\n</TabItem>\n</TabControl>");
                        break;
                    case "TextBlock":
                        CodeTextBox.AppendText("<TextBlock HorizontalAlignment=\"Left\" TextWrapping=\"Wrap\" Text=\"TextBlock\" VerticalAlignment=\"Top\"/>");
                        break;
                    case "TextBox":
                        CodeTextBox.AppendText("<TextBox HorizontalAlignment=\"Left\" Height=\"23\" TextWrapping=\"Wrap\" Text=\"TextBox\" VerticalAlignment=\"Top\" Width=\"120\"/>");
                        break;
                    case "Toolbar":
                        CodeTextBox.AppendText("<ToolBar HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "ToolBarPanel":
                        CodeTextBox.AppendText("<ToolBarPanel HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "ToolBarTray":
                        CodeTextBox.AppendText("<ToolBarTray HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "TreeView":
                        CodeTextBox.AppendText("<TreeView HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "Viewbox":
                        CodeTextBox.AppendText("<Viewbox HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "WebBrowser":
                        CodeTextBox.AppendText("<WebBrowser HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "WindowsFormsHost":
                        CodeTextBox.AppendText("<WindowsFormsHost HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    case "WrapPanel":
                        CodeTextBox.AppendText("<WrapPanel HorizontalAlignment=\"Left\" Height=\"100\" VerticalAlignment=\"Top\" Width=\"100\"/>");
                        break;
                    default:
                        MessageBox.Show("Something went wrong!","Error",MessageBoxButton.OK,MessageBoxImage.Hand);
                        break;
                    
                }
            }
        }
    }
}
