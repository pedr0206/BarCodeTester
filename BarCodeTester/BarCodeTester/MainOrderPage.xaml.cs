using Android.Graphics.Pdf;
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

namespace BarCodeTester
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainOrderPage : ContentPage
    {
        public static string root = "";
        private void Sendemail(MemoryStream memoryStream)
        {
            try
            {

                //Mail Body
                MailMessage message = new MailMessage();
                message.To.Add("hymatikorders@gmail.com");
                message.From = new MailAddress("hymatikorders@gmail.com");
                message.Subject = "Orders from mobile app";
                Attachment attachment = new Attachment(root + @"/Output.pdf");//memoryStream, MediaTypeNames.Application.Pdf);
                message.Attachments.Add(attachment);
                message.Body = "";

                SmtpClient mailClient = new SmtpClient("smtp.gmail.com");
                mailClient.Port = 587;
                mailClient.EnableSsl = true;
                //mailClient.Credentials = new NetworkCredential("hymatikodense@gmail.com", "hymatik123.");
                mailClient.Credentials = new NetworkCredential("hymatikorders@gmail.com", "marketing123.");


                mailClient.Send(message);
            }
            catch (Exception e) { }


        }
        public MainOrderPage()
        {
            InitializeComponent();

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
        }

        private void Ulp_OnUserSelected(DbUser result)
        {
            UserName.Text = result.Name;
        }

        private void NewOrderBTN_Clicked(object sender, EventArgs e)
        {
            //GetInputValues
            string Clientnumber = clientnumber.Text;
            string phoneNumber = phonenumber.Text;
            string Company = company.Text;
            string Cvrnumber = cvrnumber.Text;
            string Deliveryaddress = deliveryaddress.Text;
            string Order = order.Text;

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
            graphics.DrawString(string.Format(" {0}" + Environment.NewLine + "{1}" + Environment.NewLine + "{2}" + Environment.NewLine + "{3}" + Environment.NewLine + "{4}" + Environment.NewLine + "{5}", Clientnumber, phoneNumber, Company, Cvrnumber, Deliveryaddress, Order), font, PdfBrushes.Black, new PointF(0, 0));

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            Task generatingDoc = Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Output.pdf", "application/pdf", stream);
            Sendemail(stream);

            while (generatingDoc.Status != TaskStatus.RanToCompletion)
            {
                //
            }

            string g = "";
        }
    }
}