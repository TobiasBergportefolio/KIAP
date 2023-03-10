using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;
using System.Collections.ObjectModel;

namespace KiAP_projekt.ViewModel
{
    public class ChangeMemberViewModel : INotifyPropertyChanged
    {
        private MemberRepository memberRepository;

        //event that updates which notifies the viewLayer when properties are changed 
        public event PropertyChangedEventHandler PropertyChanged;

        //list of MemberViewModel (artificial layer of member class) which is updated instantly in the view 
        public ObservableCollection<MemberViewModel> MemberViewModels { get; set; }

        //SelectedClusterMeeting is created as a fully implemented property
        //to use the INotifyPropertyChanged method "OnPropertyChanged"
        private MemberViewModel selectedMember;
 
        public MemberViewModel SelectedMember
        {
            get { return selectedMember; }
            set { selectedMember = value;
                OnPropertyChanged("SelectedMember");
            }
        }       

        //This method is used to notify the view layer when changes are made to the properties
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Constructor that instantiates a MemberRepository and populates our list of MemberViewModels 
        public ChangeMemberViewModel()
        {
            memberRepository = new MemberRepository();
            MemberViewModels = new ObservableCollection<MemberViewModel>();

            //This retrieves a list of all the members by calling the GetAll Method from the repository.
            //The loop instantiates new objects of the MemberViewModel class for each member
            //and adds them to the MemberViewModels list
            foreach (Member member in memberRepository.GetAll())
            {
                MemberViewModels.Add(new MemberViewModel(member));
            }
            
        }
        
        //This method calls the Update method from the MemberRepository to update the the member in the database
        public void UpdateMember(int id, string name, string phoneNumber, string email)
        {
            memberRepository.Update(id, name, phoneNumber, email);
        }
    }
}
