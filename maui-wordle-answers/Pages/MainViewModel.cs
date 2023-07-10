using maui_wordle_answers.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Input;

namespace maui_wordle_answers.Pages
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string title = "Yesterday's Wordle";
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
            YesterdayCommand = new Command(execute: async () => await FindSolution("Yesterday's wordle", -1));
            TodayCommand = new Command(execute: async () => await FindSolution("Today's wordle", 0));
            TomorrowCommand = new Command(execute: async () => await FindSolution("Tomorrow's wordle", 1));
            YesterdayCommand.Execute(this);
        }

        public void LoadFromText(string json)
        {
            Solution = JsonSerializer.Deserialize<WordleSolution>(json);
        }

        public ICommand YesterdayCommand { private set; get; }
        public ICommand TodayCommand { private set; get; }
        public ICommand TomorrowCommand { private set; get; }

        public async Task FindSolution(string title, int offset)
        {
            Title = title;
            var day = DateTime.Today.AddDays(offset);
            var stamp = day.ToString("yyyy-MM-dd");
            var url =  $"https://www.nytimes.com/svc/wordle/v2/{stamp}.json";
            Solution = null;
            var json = await new HttpClient().GetStringAsync(url);
            LoadFromText(json);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
