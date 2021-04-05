using BugReport.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugReport.ViewModels
{
    public class ChildViewModel : Screen
    {
        private AdviceModel _advice = new AdviceModel();
        private BindableCollection<string> _revision = new BindableCollection<string>();
        private string selectedValue;

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        public ChildViewModel()
        {
            Revision.Add("Game is bad");
            Revision.Add("Game unplayable");
            Revision.Add("Game has potential");
            Revision.Add("Not bad game");
            Revision.Add("Awesome game");

            _advice = AdviceModel.returnAdvice();

            if (_advice == null)
            {
                _advice = new AdviceModel();
            }
        }

        public BindableCollection<string> Revision
        {
            get 
            { 
                return _revision; 
            }
            set 
            { 
                _revision = value; 
            }
        }

        public string SelectedRevision
        {
            get 
            { 
                return _advice.Header; 
            }
            set 
            {
                _advice.Header = value;
                selectedValue = value;
                NotifyOfPropertyChange(() => SelectedRevision);
            }
        }


        public string AdviceDescription
        {
            get 
            { 
                return _advice.Description; 
            }
            set 
            {
                _advice.Description = value;
                NotifyOfPropertyChange(() => AdviceDescription);
            }
        }


        public int Mark
        {
            get 
            { 
                return _advice.Rate; 
            }
            set 
            { 
                _advice.Rate = value;
                NotifyOfPropertyChange(() => Mark);
            }
        }


        public void Return()
        {
            var manager = new WindowManager();
            var newWindow = new ShellViewModel();
            manager.ShowWindow(newWindow);
            this.TryClose();
        }

        public bool CanSave(string selectedRevision, string adviceDescription)
        {
            if (String.IsNullOrWhiteSpace(selectedValue) ||
                String.IsNullOrWhiteSpace(adviceDescription))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Save(string selectedRevision, string adviceDescription)
        {
            AdviceModel.saveAdvice(_advice);
            var manager = new WindowManager();
            var newWindow = new ShellViewModel();
            manager.ShowWindow(newWindow);
            this.TryClose();
        }

    }
}
