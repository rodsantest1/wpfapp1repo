using ReactiveUI;
using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainWindowViewModel();

            this.WhenActivated(disposables =>
            {
                Input1.CaretBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                Input2.CaretBrush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

                this.OneWayBind(ViewModel,
                    vm => vm.ShowBusy,
                    v => v.BusyTextBlock.Visibility)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                    vm => vm.Input1,
                    v => v.Input1.Text)
                    .DisposeWith(disposables);
                this.Bind(ViewModel,
                    vm => vm.Input2,
                    v => v.Input2.Text)
                    .DisposeWith(disposables);
                this.OneWayBind(ViewModel,
                    vm => vm.Sum,
                    v => v.Sum.Text)
                    .DisposeWith(disposables);
            });
        }
    }
}