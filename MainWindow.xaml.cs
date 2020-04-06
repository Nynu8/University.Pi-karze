using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lesson_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string storageFileName = "storage.json";

        public MainWindow()
        {
            TextBoxEmpty.DefaultBrush = Brushes.Red;
            InitializeComponent();
            InitializeWeight();
            name.SetFocus();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ageSliderLabel.Content = $"Age: {Math.Floor(e.NewValue)}";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(IsNotEmpty(name) & IsNotEmpty(surname))
            {
                listBoxNames.Items.Add(new Person(name.Text, surname.Text, (uint)ageSlider.Value, uint.Parse(weight.Text)));
                ClearForm();
                name.SetFocus();
            }
        }

        private void LoadPerson(Person person)
        {
            name.Text = person.Name;
            surname.Text = person.Surname;
            ageSlider.Value = person.Age;
            weight.Text = person.Weight.ToString();
            Edit.IsEnabled = true;
            Delete.IsEnabled = true;
        }

        private void DeletePerson(Person person)
        {
            listBoxNames.Items.Remove(person);
        }

        private bool IsNotEmpty(TextBoxEmpty tb)
        {
            if(tb.Text.Trim() == "")
            {
                tb.SetError("Field cannot be empty");
                return false;
            }

            tb.SetError("");
            return true;
        }

        private void ClearForm()
        {
            name.Text = "";
            surname.Text = "";
            ageSlider.Value = 18;
            weight.SelectedIndex = 0;
            Edit.IsEnabled = false;
            Delete.IsEnabled = false;
            listBoxNames.SelectedIndex = -1;
            name.SetFocus();
        }


        private void InitializeWeight()
        {
            for(int i = 30; i < 250; i++)
            {
                weight.Items.Add(i);
            }

            weight.SelectedIndex = 0;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (IsNotEmpty(name) & IsNotEmpty(surname))
            {
                var newPerson = new Person(name.Text, surname.Text, (uint)ageSlider.Value, uint.Parse(weight.Text));
                bool alreadyOnList = false;
                foreach (var item in listBoxNames.Items)
                {
                    var tmp = item as Person;
                    if (tmp.Equals(newPerson))
                    {
                        alreadyOnList = true;
                        break;
                    }
                }

                if (alreadyOnList)
                {
                    var dialog = MessageBox.Show($"{newPerson.ToString()} is already on the list\nDo you want to clear the form?", "Warning", MessageBoxButton.OKCancel);
                    if (dialog == MessageBoxResult.OK)
                    {
                        ClearForm();
                    }
                }
                else
                {
                    var currentPerson = (Person)listBoxNames.SelectedItem;
                    currentPerson.Name = newPerson.Name;
                    currentPerson.Surname = newPerson.Surname;
                    currentPerson.Weight = newPerson.Weight;
                    currentPerson.Age = newPerson.Age;
                    listBoxNames.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedPerson = (Person)listBoxNames.SelectedItem;
            var dialog = MessageBox.Show($"You're about to delete {selectedPerson.ToString()}\nAre you sure?", "Warning", MessageBoxButton.OKCancel);
            if(dialog == MessageBoxResult.OK)
            {
                DeletePerson(selectedPerson);
                ClearForm();
            }
        }

        private void listBoxNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxNames.SelectedIndex > -1)
            {
                LoadPerson((Person)listBoxNames.SelectedItem);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var toLoad = Storage.LoadFromFile(storageFileName);
            foreach (var item in toLoad)
            {
                listBoxNames.Items.Add(item);   
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            List<Person> list = new List<Person>();
            foreach (var item in listBoxNames.Items)
            {
                list.Add((Person)item);
            }

            Storage.SaveToFile(storageFileName, list.ToArray());
        }
    }
}
