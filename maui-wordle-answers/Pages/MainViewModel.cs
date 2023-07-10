using maui_wordle_answers.ViewModels;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Input;

namespace maui_wordle_answers.Pages
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Dictionary<string, string> cache = new Dictionary<string, string>();

        private string title;
        public string Title
        {
            get => title;
            set
            {
                if (title == value) return;
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        private WordleSolution solution = null;
        public WordleSolution Solution
        {
            get => solution;
            set
            {
                if (solution == value) return;
                solution = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Solution)));
            }
        }

        public MainViewModel()
        {
            YesterdayCommand = new Command(execute: async () => await Solve("Yesterday's Wordle was:", -1));
            TodayCommand = new Command(execute: async () => await Solve("Today's Wordle is:", 0));
            TomorrowCommand = new Command(execute: async () => await Solve("Tomorrow's Wordle prediction:", 1));
            YesterdayCommand.Execute(this);
        }

        public void LoadFromText(string json)
        {
            Solution = JsonSerializer.Deserialize<WordleSolution>(json);
        }

        public ICommand YesterdayCommand { private set; get; }
        public ICommand TodayCommand { private set; get; }
        public ICommand TomorrowCommand { private set; get; }

        public async Task Solve(string title, int offset)
        {
            Title = title;
            var datestamp = DateTime.Today.AddDays(offset).ToString("yyyy-MM-dd");
            string json = "";
            if (cache.ContainsKey(datestamp))
            {
                json = cache.GetValueOrDefault(datestamp);
            }
            else
            {
                var url = $"https://www.nytimes.com/svc/wordle/v2/{datestamp}.json";
                Solution = null;
                await Task.Delay(200);
                json = await new HttpClient().GetStringAsync(url);
                cache.Add(datestamp, json);
            }
            LoadFromText(json);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
