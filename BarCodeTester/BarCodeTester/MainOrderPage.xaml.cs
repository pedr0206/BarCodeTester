using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.Drawing;

namespace BarCodeTester
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainOrderPage : ContentPage
    {
        public static string root = "";
        DbUser currentUser = null;
        DbAddress currentAddress = null;
        //private void Sendemail(MemoryStream memoryStream)
        //{
        //    try
        //    {

        //        //Mail Body
        //        MailMessage message = new MailMessage();
        //        message.To.Add("hymatikorders@gmail.com");
        //        message.From = new MailAddress("hymatikorders@gmail.com");
        //        message.Subject = "Orders from mobile app";
        //        Attachment attachment = new Attachment(root + @"/Output.pdf");//memoryStream, MediaTypeNames.Application.Pdf);
        //        message.Attachments.Add(attachment);
        //        message.Body = "hola burro";

        //        SmtpClient mailClient = new SmtpClient("smtp.gmail.com");
        //        mailClient.Port = 587;
        //        mailClient.EnableSsl = true;
        //        //mailClient.Credentials = new NetworkCredential("hymatikodense@gmail.com", "hymatik123.");
        //        mailClient.Credentials = new NetworkCredential("hymatikorders@gmail.com", "marketing123.");


        //        mailClient.Send(message);
        //    }
        //    catch (Exception e) { }


        //}
        public MainOrderPage()
        {
            InitializeComponent();

            OrderNow.Clicked += OrderNowBTN_Clicked;

            UserName.Clicked += async delegate
            {
                UserListPage ulp = await UserListPage.CreateUserListPageAsync();
                ulp.OnUserSelected += Ulp_OnUserSelected;
                await Navigation.PushModalAsync(ulp);
            };

            DeliveryAddress.Clicked += async delegate
            {
                DeliveryAddressListPage dalp = await DeliveryAddressListPage.CreateDeliveryAddressListPageAsync();
                dalp.OnAddressSelected += Dalp_OnUserSelected;
                await Navigation.PushModalAsync(dalp);
            };


            ManageOrder.Clicked += NewOrderBTN_Clicked;
        }

        private void Dalp_OnUserSelected(DbAddress result)
        {
            DeliveryAddress.Text = result.Address;
            currentAddress = result;
        }

        private void Ulp_OnUserSelected(DbUser result)
        {
            UserName.Text = result.Name;
            currentUser = result;
        }

        private void NewOrderBTN_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
            
        }
        private void OrderNowBTN_Clicked(object sender, EventArgs e)
        {
            //ADICIONAR USERS A DATABASE
            //App.Database.SaveUserAsync(dbuser);
            
            EmailTester email = new EmailTester();


            //string Company = "teste";//company.Text;
            //string Cvrnumber = "teste";//cvrnumber.Text;
            //string Order = "teste";//order.Text;
            //string phoneNumber = "teste";//phonenumber.Text;
            StringBuilder products = new StringBuilder();

            foreach(var product in ProductRepository.Instance.GetAllProducts())
            {
                products.AppendLine("\t * " + product);
            }

            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add page settings


            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            PdfGraphics graphics = page.Graphics;


            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text
            graphics.DrawString(string.Format(" " +
                "{0}" + Environment.NewLine + 
                "{1}" + Environment.NewLine + 
                "{2}" + Environment.NewLine, currentUser.ID + " - " + currentUser.Name, currentAddress.Address + " - " + currentAddress.ZipCode + " - " + currentAddress.City, products), font, PdfBrushes.Black, new PointF(0, 0));

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            Task generatingDoc = Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application/pdf", stream);

            email.EmailSender(new MemoryStream(stream.ToArray()));

            //while (generatingDoc.Status != TaskStatus.RanToCompletion)
            //{
            //    //
            //}

            string g = "";

        }
    }

}