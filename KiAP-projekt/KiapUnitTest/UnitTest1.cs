using Microsoft.VisualStudio.TestTools.UnitTesting;
using KiAP_projekt;
using System;
using KiAP_projekt.ViewModel;
using KiAP_projekt.Model;

namespace KiapUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        ChangeMeetingViewModel changeMeetingVM = new ChangeMeetingViewModel();
        CreateMeetingViewModel createMeetingVM = new CreateMeetingViewModel();
        ClusterMeetingRepository clusterMeetingRepository = new ClusterMeetingRepository();

        //Creates a new cluster meeting and checks if number of clustermeetings in database has increased by 1
        [TestMethod]
        public void TestMeetingCount()
        {
            //Arrange
            DateTime date = new DateTime(2022,06,20);
            DateTime time = new DateTime(2022, 01, 01, 12, 30, 00);
            Double duration = 3;
            string clusterPackageName = "Diabetes";
            DateTime registrationDeadline = new DateTime(2022, 05, 15);
            bool online = true;
            int count = clusterMeetingRepository.GetAll().Count;
            
            //Act
            createMeetingVM.CreateClusterMeeting(date, time, duration, clusterPackageName, registrationDeadline, online);

            //Assert
            Assert.AreEqual(count + 1, clusterMeetingRepository.GetAll().Count);
        }

        //Tests if UpdateClusterMeeting method works 
        [TestMethod]
        public void TestUpdateClusterPackageName()
        {
            ClusterMeeting clusterMeeting = clusterMeetingRepository.GetById(65);
            string clusterPackageName = "KOL - behandling";
            changeMeetingVM.UpdateClusterMeeting(clusterMeeting.ClusterMeetingID, clusterMeeting.Date, clusterMeeting.Time, clusterMeeting.Duration,clusterPackageName, clusterMeeting.RegistrationDeadline, clusterMeeting.Online, clusterMeeting.Address, clusterMeeting.PostalCode,clusterMeeting.City);
            Assert.AreEqual(clusterPackageName, clusterMeetingRepository.GetById(65).ClusterPackageName);

        }
    }
}
