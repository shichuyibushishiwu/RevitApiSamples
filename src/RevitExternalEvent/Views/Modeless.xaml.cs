using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RevitExternalEvent.Views
{
    /// <summary>
    /// Modeless.xaml 的交互逻辑
    /// </summary>
    public partial class Modeless : Window
    {
        private static Modeless _modeless = new Modeless();
        private Document? _document;

        private Modeless()
        {
            InitializeComponent();
        }

        public static Modeless GetInstance() => _modeless;

        public void Initial(Document document)
        {
            _document = document;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (_document == null)
            {
                return;
            }

            App.Current?._service?.Raise((uiapp) =>
            {
                using (Transaction ts = new Transaction(_document))
                {
                    ts.Start("test");
                    Autodesk.Revit.DB.Line line = Autodesk.Revit.DB.Line.CreateBound(XYZ.Zero, new XYZ(0, 50, 0));
                    _document.Create.NewDetailCurve(_document.ActiveView, line);
                    ts.Commit();
                }
            });

           
        }
    }
}
