using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Configuration;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
namespace WpfApp1
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ReactiveCommand<Unit, string> AddNumbers { get; }
        public MainWindowViewModel()
        {
            AddNumbers = ReactiveCommand.CreateFromTask(AddNumbersHandler);
            AddNumbers.ToPropertyEx(this, y => y.Sum);
            //AddNumbers.ThrownExceptions.Subscribe(ex => this.Log().Warn("Error!", ex));

            this.WhenAnyValue(
                x => x.Input1, x => x.Input2,
                (input1, input2) =>
                    !string.IsNullOrWhiteSpace(input1) &&
                    !string.IsNullOrWhiteSpace(input2)
                )
                .Select(_ => Unit.Default)
                .InvokeCommand(AddNumbers);
        }

        private async Task<string> AddNumbersHandler()
        {
            var sum = await Task.Run(() => SimpleMathService(Input1, Input2));

            return sum > 0 ? $"{sum}" : "";
        }

        private int SimpleMathService(string input1, string input2)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));

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