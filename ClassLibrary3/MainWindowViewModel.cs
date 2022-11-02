using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Configuration;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
namespace WpfApp1
{
    public class MainWindowViewModel : ReactiveObject
    {
        public bool ShowBusy { [ObservableAsProperty]get; }
        public ReactiveCommand<Unit, string> AddNumbers { get; }
        public MainWindowViewModel()
        {
            AddNumbers = ReactiveCommand.CreateFromObservable(AddNumbersHandler);
            AddNumbers.ToPropertyEx(this, y => y.Sum);
            //AddNumbers.ThrownExceptions.Subscribe(ex => this.Log().Warn("Error!", ex));

            this.WhenAnyObservable(x => x.AddNumbers.IsExecuting)
                .ToPropertyEx(this, x => x.ShowBusy);

            this.WhenAnyValue(x => x.Input1, x => x.Input2,
                (a, b) => !string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b))
                .Where(x => x)
                .Do(_ => this.Log().Debug($"Rodney-Input1={Input1}"))
                .Select(_ => Unit.Default)
                .InvokeCommand(AddNumbers);
        }

        private IObservable<string> AddNumbersHandler()
        {
            return Observable.StartAsync(async () =>
            {
                var sum = await Task.Run(() => SimpleMathService(Input1, Input2));

                return sum > 0 ? $"{sum}" : "";

            }).Delay(TimeSpan.FromSeconds(5));

        }

        private int SimpleMathService(string input1, string input2)
        {
            if (string.IsNullOrWhiteSpace(input1) || string.IsNullOrWhiteSpace(input2)) return default;

            using (var client = new WebClient())
            {
                try
                {
                    var jsonString = client.DownloadString($"{ConfigurationManager.AppSettings["BaseUrl"]}/simplemath/{input1}/{input2}");

                    var sum = JsonSerializer.Deserialize<int>(jsonString);

                    return sum;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [Reactive] public string Input1 { get; set; }
        [Reactive] public string Input2 { get; set; }
        public string Sum { [ObservableAsProperty] get; }
    }
}