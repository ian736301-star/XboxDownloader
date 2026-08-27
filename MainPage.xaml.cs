using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace XboxDownloader
{
    public sealed partial class MainPage : Page
    {
        readonly HttpClient client = new HttpClient();

        public MainPage() { InitializeComponent(); }

        async void Download_Click(object sender, RoutedEventArgs e)
        {
            if (!Uri.TryCreate(UrlBox.Text.Trim(), UriKind.Absolute, out var uri))
            {
                Status.Text = "URL inválida.";
                return;
            }

            try
            {
                Status.Text = "Escolha onde salvar o arquivo...";
                var picker = new FileSavePicker();
                picker.SuggestedStartLocation = PickerLocationId.Downloads;
                picker.FileTypeChoices.Add("Arquivo", new[] { ".zip", ".7z", ".rar", ".iso", ".7zip", ".bin", ".txt", ".exe", ".msix", ".appx" });
                picker.SuggestedFileName = Path.GetFileName(uri.LocalPath);
                var file = await picker.PickSaveFileAsync();
                if (file == null) { Status.Text = "Download cancelado."; return; }

                Status.Text = "Baixando...";
                using (var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var total = response.Content.Headers.ContentLength;
                    using (var input = await response.Content.ReadAsStreamAsync())
                    using (var output = await file.OpenStreamForWriteAsync())
                    {
                        var buffer = new byte[1024 * 256];
                        long done = 0;
                        int n;
                        while ((n = await input.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await output.WriteAsync(buffer, 0, n);
                            done += n;
                            if (total.HasValue)
                                Progress.Value = done * 100.0 / total.Value;
                        }
                    }
                }
                Status.Text = "Download concluído.";
                Progress.Value = 100;
            }
            catch (Exception ex)
            {
                Status.Text = "Erro: " + ex.Message;
            }
        }
    }
}
