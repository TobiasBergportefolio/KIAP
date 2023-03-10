using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiAP_projekt.Model;

namespace KiAP_projekt.ViewModel
{
    public class MainViewModel
    {
        //This class is used to handle user inputs from the MainWindow.

        //We create new memberRepository and ClusterMeetingRepository in private fields
        private ClusterMeetingRepository clusterMeetingRepository;
        private MemberRepository memberRepository;

        //We have an Observable collection of meetings which only include the upcoming meetings as a property.
        public ObservableCollection<ClusterMeetingViewModel> UpcomingClusterMeetingViewModels { get; set; }

        //This is an observable collection of all the members as a property
        public ObservableCollection<MemberViewModel> MemberViewModels { get; set; }

        //we instanciate the fields and properties and then we get the upcomming meetings
        public MainViewModel()
        {
            clusterMeetingRepository = new ClusterMeetingRepository();
            UpcomingClusterMeetingViewModels = new ObservableCollection<ClusterMeetingViewModel>();
            memberRepository = new MemberRepository();
            MemberViewModels = new ObservableCollection<MemberViewModel>();
            
            //In the following loops we cycle through lists of members and meetings
            //and then instantiate their corresponding ViewModel versions of the classes
            //and add them to our lists of MemberViewModels and ClusterMEetingViewModels
            foreach (ClusterMeeting cm in clusterMeetingRepository.GetUpcomingMeetings())
            {
                UpcomingClusterMeetingViewModels.Add(new ClusterMeetingViewModel(cm));
            }
            foreach (Member member in memberRepository.GetAll())
            {
                MemberViewModels.Add(new MemberViewModel(member));
            }
        }

        //first we get the clustermeeting by id then delete it.
        public void DeleteClusterMeeting(int id)
        {
            ClusterMeeting clusterMeeting = clusterMeetingRepository.GetById(id);
            clusterMeetingRepository.Delete(clusterMeeting);
        }

        //first we get the member by id then delete it.
        public void DeleteMember(int id)
        {
            Member member = memberRepository.GetById(id);
            memberRepository.Delete(member);
        }

        //This method updates the upcomingMeetings list by first clearing it and then
        //repopulating it by getting a list of upcoming meetings from the repository
        public void UpdateUpcomingMeetingList()
        {
            UpcomingClusterMeetingViewModels.Clear();
            foreach (ClusterMeeting cm in clusterMeetingRepository.GetUpcomingMeetings())
            {
                UpcomingClusterMeetingViewModels.Add(new ClusterMeetingViewModel(cm));
            }
        }

        //This method updates the members list by first clearing it and then
        //repopulating it by getting a list of members from the repository
        public void UpdateMemberList()
        {
            MemberViewModels.Clear();
            foreach (Member member in memberRepository.GetAll())
            {
                MemberViewModels.Add(new MemberViewModel(member));
            }
        }

        //a method for getting a role for a member and returning the result.
        public string GetRole()
        {
            string getRole = memberRepository.GetRole();
            return getRole;
        }
    }
}
