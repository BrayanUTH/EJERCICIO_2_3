using EJERCICIO_2_3.Model;
using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EJERCICIO_2_3.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListAudio : ContentPage
    {
        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public PageListAudio()
        {
            InitializeComponent();
        }

        private void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var audio = item.BindingContext as Audio;

            audioPlayer.Play(audio.pathAudio);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            audioPlayer.Pause();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListAudio.ItemsSource = await App.DBase.GetAllAudio();
        }
    }
}