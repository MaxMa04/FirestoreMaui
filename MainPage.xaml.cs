using FirestoreMaui.Services;
using FirestoreMaui.ViewModels;

namespace FirestoreMaui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(SampleVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }

}
