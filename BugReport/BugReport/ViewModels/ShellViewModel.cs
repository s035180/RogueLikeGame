using BugReport.Models;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Text;
using System.Net;

namespace BugReport.ViewModels
{
    public class ShellViewModel : Screen
    {
        private PersonDataModel _person { get; set; }
        private string _title = "RogueLikeGame";
        private string _name = "Bug Report";

        public ShellViewModel()
        {
            _person = PersonDataModel.returnPerson();

            if (_person == null)
            {
                _person = new PersonDataModel();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        public string FirstName
        {
            get
            {
                return _person.FirstName;
            }
            set
            {

                if (value.Length < 2)
                {
                    _person.FirstName = null;
                    throw new Exception("");
                }
                else
                {
                    _person.FirstName = value;
                    NotifyOfPropertyChange(() => FirstName);
                    //NotifyOfPropertyChange(() => Full);
                }

            }
        }

        public string LastName
        {
            get
            {
                return _person.LastName;
            }
            set
            {
                if (value.Length < 2)
                {
                    _person.LastName = null;
                    throw new Exception("");
                }
                else
                {
                    _person.LastName = value;
                    NotifyOfPropertyChange(() => LastName);
                    NotifyOfPropertyChange(() => Full);
                }

            }
        }

        public string Email
        {
            get
            {
                return _person.Email;
            }
            set
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                RegexOptions.CultureInvariant | RegexOptions.Singleline);
                if (!regex.IsMatch(value))
                {
                    _person.Email = null;
                    throw new Exception("");
                }
                else
                {
                    _person.Email = value;
                    NotifyOfPropertyChange(() => Email);
                    NotifyOfPropertyChange(() => Full);
                }

            }
        }

        public string ProblemTitle
        {
            get
            {
                return _person.ProblemTitle;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _person.ProblemTitle = null;
                    throw new Exception("");
                }
                else
                {
                    _person.ProblemTitle = value;
                    NotifyOfPropertyChange(() => ProblemTitle);
                    NotifyOfPropertyChange(() => Full);
                }

            }
        }

        public string Description
        {
            get
            {
                return _person.Description;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _person.Description = null;
                    throw new Exception("");
                }
                else
                {
                    _person.Description = value;
                    NotifyOfPropertyChange(() => ProblemTitle);
                    NotifyOfPropertyChange(() => Full);
                }
            }
        }

        public string Full
        {
            get
            {
                return $"{FirstName} {LastName} {Email} {ProblemTitle} {Description}";
            }
        }

        public string Images
        {
            get 
            {
               return "Image Count: " + _person.Images.Count().ToString();
            }
        }

        public bool CanNewWindow(string firstName, string lastName, string email, string problemTitle, string description)
        {
            if (String.IsNullOrWhiteSpace(firstName) ||
               String.IsNullOrWhiteSpace(lastName) ||
               String.IsNullOrWhiteSpace(email) ||
               String.IsNullOrWhiteSpace(problemTitle) ||
               String.IsNullOrWhiteSpace(description))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void NewWindow(string firstName, string lastName, string email, string problemTitle, string description)
        {
            PersonDataModel.savePerson(_person);
            var manager = new WindowManager();
            var newWindow = new ChildViewModel();
            manager.ShowWindow(newWindow);
            this.TryClose();
        }

        public void AddImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "jpg files (*.jpg)|*.jpg";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filename in openFileDialog.FileNames)
                {
                    _person.Images.Add(Path.GetFileName(filename));
                    NotifyOfPropertyChange(() => Images);
                }
            }
        }

        public void SendReport(string firstname, string lastname, string email, string problemTitle, string description)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //put your SMTP address and port here.
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                //Put the email address
                mail.From = new MailAddress("noskoruslan1999@gmail.com");
                //Put the email where you want to send.
                mail.To.Add("gh7891gh@gmail.com");

                mail.Subject = "CheckoutPOS Exception Log";

                StringBuilder sbBody = new StringBuilder();

                sbBody.AppendLine("Hi Dev Team,");

                sbBody.AppendLine("Something went wrong with CheckoutPOS");

                sbBody.AppendLine("Here is the error log:");

                sbBody.AppendLine("Exception: Object reference not set to an instance of an object....");

                sbBody.AppendLine("Thanks,");

                mail.Body = sbBody.ToString();

                //Your log file path
                //System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(@"C:\Logs\CheckoutPOS.log");

                //mail.Attachments.Add(attachment);

                //Your username and password!
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("noskoruslan1999@gmail.com", "2447892asd");
                SmtpServer.EnableSsl = true;
                //SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("The exception has been sent! :)");

                /*var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("noskoruslan1999@gmail.com", "2447892asd"),
                    EnableSsl = true,
                };

                smtpClient.Send("noskoruslan1999@gmail.com", "gh7891gh@gmail.com", "subject", "body");*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool CanSendReport(string firstname, string lastname, string email, string problemTitle, string description)
        {
            if (String.IsNullOrWhiteSpace(_person.FirstName) ||
               String.IsNullOrWhiteSpace(_person.LastName) ||
               String.IsNullOrWhiteSpace(_person.Email) ||
               String.IsNullOrWhiteSpace(_person.ProblemTitle) ||
               String.IsNullOrWhiteSpace(_person.Description))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
