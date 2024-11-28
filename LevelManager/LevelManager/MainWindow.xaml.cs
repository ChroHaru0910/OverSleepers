using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace VariableManagerApp
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<VariableInfo> Variables { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Variables = new ObservableCollection<VariableInfo>();
            VariableDataGrid.ItemsSource = Variables;

            // 起動時にデータを読み込む
            LoadVariables();
        }

        private void LoadVariables_Click(object sender, RoutedEventArgs e) => LoadVariables();

        private void LoadVariables()
        {
            string path = "Variables.json";

            if (File.Exists(path))
            {
                try
                {
                    // JSONを読み込み、デシリアライズ
                    string json = File.ReadAllText(path);
                    var imported = JsonSerializer.Deserialize<ObservableCollection<VariableInfo>>(json);

                    Variables.Clear();
                    foreach (var variable in imported)
                    {
                        Variables.Add(variable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"データの読み込み中にエラーが発生しました: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("指定されたJSONファイルが見つかりません。");
            }
        }

        private void SaveVariables_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize(Variables, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("Variables.json", json);

            MessageBox.Show("変数が保存されました。");
        }
    }

    public class VariableInfo : INotifyPropertyChanged
    {
        private string _type;
        private string _value;

        public string Name { get; set; }

        public string Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));

                    // 型変更時に値を初期化
                    UpdateValueByType();
                }
            }
        }

        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public string Comment { get; set; }

        private void UpdateValueByType()
        {
            switch (_type)
            {
                case "int":
                    Value = "0"; // 初期値を整数として設定
                    break;
                case "float":
                    Value = "0.0"; // 初期値を浮動小数点として設定
                    break;
                case "string":
                    Value = ""; // 初期値を空文字列として設定
                    break;
                default:
                    Value = null; // 不明な型の場合はnull
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
