using CommunityToolkit.Mvvm.Input;
using FirestoreMaui.Models;
using FirestoreMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirestoreMaui.ViewModels
{
    public partial class SampleVM
    {
        FirestoreService firestoreService;
        public ObservableCollection<SampleModel> SampleData { get; set; } = [];
        public SampleVM(FirestoreService firestoreService)
        {
            this.firestoreService = firestoreService;
        }
        [RelayCommand]
        public async Task GetSampleData()
        {
            SampleData.Clear();
            var x = await firestoreService.GetSampleModels();
            foreach (var item in x)
            {
                SampleData.Add(item);
            }
        }
        [RelayCommand]
        public async Task SaveSampleData()
        {
            var sample = new SampleModel
            {
                Name = "Sample",
                Description = "Sample Description",
                CreatedAt = DateTime.Now
            };
            await firestoreService.InsertSampleModel(sample);
        }
    }
}
