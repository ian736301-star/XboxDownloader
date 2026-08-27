XBOX DOWNLOADER — UWP / Xbox Dev Mode

O projeto foi preparado para ser compilado em Windows e gerar um MSIX x64.
Também inclui .github/workflows/build-msix.yml para compilar e assinar
automaticamente em GitHub Actions usando um certificado de desenvolvimento.

O workflow cria:
- XboxDownloader_*.msix
- XboxDownloader.cer
- um ZIP com os dois

A assinatura é de desenvolvimento. O certificado .cer correspondente precisa
ser confiado no ambiente onde o pacote for instalado.

IMPORTANTE:
O workflow precisa ser executado em uma conta/repositório GitHub com GitHub
Actions habilitado. Não é necessário ter PC local para a compilação: o runner
windows-latest faz a compilação.

O Downloader usa HttpClient e FileSavePicker. Ele funciona melhor com URLs
diretas de arquivos. Sites que exigem login, Cloudflare ou JavaScript para
gerar o arquivo podem exigir tratamento adicional.
