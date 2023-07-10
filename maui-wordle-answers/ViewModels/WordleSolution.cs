using System.ComponentModel;
using System.Text.Json.Serialization;

namespace maui_wordle_answers.ViewModels
{
    public class WordleSolution : INotifyPropertyChanged
    {
        private int id = 0;

        [JsonPropertyName("id")]
        public int Id
        {
            get => id;
            set
            {
                if (id == value) return;
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        private string solution;

        [JsonPropertyName("solution")]
        public string Solution
        {
            get => solution;
            set
            {
                if (solution == value) return;
                solution = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Solution)));
            }
        }

        private string printDate;

        [JsonPropertyName("print_date")]
        public string PrintDate
        {
            get => printDate;
            set
            {
                if (printDate == value) return;
                printDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrintDate)));
            }
        }

        private int daysSinceLaunch;

        [JsonPropertyName("days_since_launch")]
        public int DaysSinceLaunch
        {
            get => daysSinceLaunch;
            set
            {
                if (daysSinceLaunch == value) return;
                daysSinceLaunch = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DaysSinceLaunch)));
            }
        }

        private string editor;

        [JsonPropertyName("editor")]
        public string Editor
        {
            get => editor;
            set
            {
                if (editor == value) return;
                editor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Editor)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}