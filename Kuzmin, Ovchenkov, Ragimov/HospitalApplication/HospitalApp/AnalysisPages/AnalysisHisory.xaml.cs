﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using HospitalApp.UserPages;
using HospitalLib.Data;
using HospitalLib.Providers;

namespace HospitalApp.AnalysisPages
{
    /// <summary>
    /// Interaction logic for AnalysisHisory.xaml
    /// </summary>
    public partial class AnalysisHisory
    {
        private readonly IDictionary<string, Analysis> _analyzesDictionary;

        public AnalysisHisory()
        {
            InitializeComponent();
            _analyzesDictionary = new Dictionary<string, Analysis>();
            LoadAllAnalyzes();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Switcher.PageSwitcher.Navigate(new UserLoaded());
        }

        private void LoadAllAnalyzes()
        {
            var analysisProvider = new AnalysisProvider(new DatabaseProvider());
            var analyzes = analysisProvider.Load(CurrentState.CurrentPerson);
            AddToDictionary(analyzes);
        }

        private void AddToDictionary(IEnumerable<Analysis> analyzes)
        {
            var list = new ObservableCollection<string>();

            foreach (var analysis in analyzes)
            {
                var name = analysis.GetTemplateName() + " " + analysis.CreationTime;
                _analyzesDictionary[name] = analysis;
                list.Add(name);
            }

            AnalyzesComboBox.ItemsSource = list;
            if (list.Count == 0)
                throw new InvalidDataException("Analyzes Database is empty");

            AnalyzesComboBox.SelectedIndex = 0;
        }

        private void CreateNewButton_Click(object sender, RoutedEventArgs e)
        {
            var analysis = _analyzesDictionary[AnalyzesComboBox.SelectedValue.ToString()];
            CurrentState.CurrentAnalysis = new Analysis(analysis.Template, analysis.Person, new NewIdProvider(new DatabaseProvider()));
            CurrentState.CurrentTemplate = CurrentState.CurrentAnalysis.Template;
            Switcher.PageSwitcher.Navigate(new AnalysisLoaded());
        }

        private void ChangeCurrentlButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AnalyzesComboBox.SelectedValue.ToString()))
            {
                MessageBox.Show("Вам следует выбрать анализ");
                return;
            }

            CurrentState.CurrentAnalysis = _analyzesDictionary[AnalyzesComboBox.SelectedValue.ToString()];
            CurrentState.CurrentAnalysis.New = false;
            CurrentState.CurrentTemplate = CurrentState.CurrentAnalysis.Template;
            Switcher.PageSwitcher.Navigate(new AnalysisLoaded());
        }
    }
}
