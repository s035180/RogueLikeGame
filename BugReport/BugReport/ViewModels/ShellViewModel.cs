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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace BugReport.ViewModels
{
    public class ShellViewModel : Screen
    {
        private PersonDataModel _person { get; set; }
        private string _title = "RogueLikeGame";
        private string _name = "Bug Report";
        private BindableCollection<string> _listOfPhoto = new BindableCollection<string>();
        private string selectedValue;
        private AdviceModel _advice = new AdviceModel();


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
                    _person.Images.Add(filename);
                    _listOfPhoto.Add(Path.GetFileName(filename));
                    NotifyOfPropertyChange(() => Images);
                    NotifyOfPropertyChange(() => ListOfPhoto);

                }
            }
        }
        public void DeleteImage(BindableCollection<string> ListOfPhoto)
        {
            _person.Images.Remove(selectedValue);
            NotifyOfPropertyChange(() => Images);
        }
        public string SelectedPhoto
        {
            set
            {
                    selectedValue = value;
            }
        }
        public bool CanDeleteImage(BindableCollection<string> ListOfPhoto)
        {
            if (_person.Images.Count > 0  && !String.IsNullOrWhiteSpace(selectedValue))
            {
               
                    return true;
            }
            else
                return false;
        }

        public BindableCollection<string> ListOfPhoto
        {
            get 
            { 
                return _person.Images;
            }
            set
            {
                _listOfPhoto = value;
            }
        }




        public void SendReport(string firstname, string lastname, string email, string problemTitle, string description)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("noskoruslan1999@gmail.com");
                mail.To.Add("gh7891gh@gmail.com");
                mail.Subject = _person.ProblemTitle;
                _advice = AdviceModel.returnAdvice();
                StringBuilder sbBody = new StringBuilder();
                sbBody.AppendLine("Name: " + _person.FirstName);
                sbBody.AppendLine("Surname: " + _person.LastName);
                sbBody.AppendLine("Email: " + _person.Email);
                sbBody.AppendLine("Description: " + _person.Description);
                if(_advice != null)
                {
                    sbBody.AppendLine("Game Advice");
                    sbBody.AppendLine("Game Revision: " + _advice.Header);
                    sbBody.AppendLine("Game Rate: " + _advice.Rate);
                    sbBody.AppendLine("Advice Description: " + _advice.Description);
                }
                System.Net.Mail.Attachment attachment;
                if (_person.Images != null)
                {
                    foreach (string photo in _person.Images)
                    {
                        attachment = new System.Net.Mail.Attachment(photo);
                        mail.Attachments.Add(attachment);
                    }
                }
                mail.Body = sbBody.ToString();
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("noskoruslan1999@gmail.com", "2447892asd");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                MessageBox.Show("Report have been sent");

            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
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
