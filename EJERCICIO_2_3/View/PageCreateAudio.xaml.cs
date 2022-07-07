using EJERCICIO_2_3.Model;
using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EJERCICIO_2_3.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateAudio : ContentPage
    {

        private bool isRecording = false;
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService()
        {
            StopRecordingOnSilence = false,
            StopRecordingAfterTimeout = false
        };

        private readonly AudioPlayer audioPlayer = new AudioPlayer();

        public PageCreateAudio()
        {
            InitializeComponent();
        }

        private async void BtnRecordAudio_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
            if (status != PermissionStatus.Granted)
            {
                return;
            }
            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();

                lblIsRecording.Text = "Aun no esta grabando, presione el boton para comenzar";
                BtnRecordAudio.Text = "Comenzar a Grabar Audio";
                isRecording = true;

            }
            else
            {
                await audioRecorderService.StartRecording();

                lblIsRecording.Text = "Grabando Audio ...";
                BtnRecordAudio.Text = "Detener Grabacion";
            }
        }

        private async void BtnSaveAudio_Clicked(object sender, EventArgs e)
        {
            if (!isRecording)
            {
                await DisplayAlert("Advertencia", "Aun no ha grabando un audio", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                await DisplayAlert("Advertencia", "Debes ingrear un descripcion de tu audio.", "Ok");
                return;
            }

            string path = SaveAudioToDevice();

            Audio audio = new Audio()
            {
                description = txtDescription.Text,
                pathAudio = path,
                dateCreation = DateTime.Now
            };

            var result = await App.DBase.CreateAudio(audio);

            if (result > 0)
            {
                await DisplayAlert("Confirmacion", "Audio Guardado Correctamente.", "Ok");
                isRecording = false;
                txtDescription.Text = "";
            } else
            {
                await DisplayAlert("Confirmacion", "Se produjo un error al guardar el audio", "Ok");
            }

        }

        private string SaveAudioToDevice()
        {
            string path = "/storage/emulated/0/Android/data/com.companyname.ejercicio_2_3/files/Audios";
            MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(audioRecorderService.GetAudioFilePath()));
            Byte[] bytes = memoryStream.ToArray();

            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
                
            var nameAudio = DateTime.Now.ToString("yyyyMMddhhmmss") + ".wav";
            var fullPath = Path.Combine(path, nameAudio);
            File.WriteAllBytes(fullPath, bytes);


            return fullPath;
        }

        private async void BtnListAudio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageListAudio());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            audioPlayer.Pause();
        }
    }
}