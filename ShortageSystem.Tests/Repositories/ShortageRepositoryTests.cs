using FluentAssertions;
using ShortageSystem.Identity;
using ShortageSystem.Models;
using ShortageSystem.Repositories;
using Xunit;

namespace ShortageSystem.Tests.Repositories
{
    public class ShortageRepositoryTests
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Shortages.json");

        IShortageRepository _repo;
        public ShortageRepositoryTests() {
            _repo = new ShortageRepository();
            //Reset test Shortages.json file before each test
            ResetFile();
        }

        private void ResetFile()
        {
            File.WriteAllText(filePath, "[]");
        }


        [Fact]
        public async Task Add_NewShortage_SameTitle_SameRoom_LowerPriority_NotAdded()
        {
            //Arrange
            var existingShortage = new Shortage("New Shortage", "John Doe", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var lowerPriorityShortage = new Shortage("New Shortage", "John Doe", Category.Electronics, Room.MeetingRoom, 3, new DateOnly(2024, 09, 10), "admin");

            await _repo.AddShortageAsync(existingShortage);
            //Act
            //await _repo.AddShortageAsync(lowerPriorityShortage);
            var allShortages = await _repo.GetAllShortagesAsync();
            //Assert
            allShortages.Should().HaveCount(1);
            allShortages.Should().Contain(existingShortage);
            allShortages.Should().NotContain(lowerPriorityShortage);
        }

        [Fact]
        public async Task Add_NewShortage_SameTitle_SameRoom_HigherPriority_Added()
        {
            //Arrange
            var existingShortage = new Shortage("New Shortage", "John Doe", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var higherPriorityShortage = new Shortage("New Shortage", "John Doe", Category.Electronics, Room.MeetingRoom, 7, new DateOnly(2024, 09, 10), "admin");

            await _repo.AddShortageAsync(existingShortage);
            //Act
            await _repo.AddShortageAsync(higherPriorityShortage);
            var allShortages = await _repo.GetAllShortagesAsync();
            //Assert
            allShortages.Should().HaveCount(1);
            allShortages.Should().Contain(higherPriorityShortage);
            allShortages.Should().NotContain(existingShortage);
        }

        [Fact]
        public async Task Get_ShortagesByCreator_NotAdmin_CorrectCount_CorrectData()
        {
            //Arrange
            var shortage1 = new Shortage("New Shortage1", "John Doe", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("New Shortage2", "John Doe", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "adminsFriend");
            var shortage3 = new Shortage("New Shortage3", "John Doe", Category.Food, Room.MeetingRoom, 3, new DateOnly(2024, 09, 10), "admin");
            var shortage4 = new Shortage("New Shortage4", "John Doe", Category.Electronics, Room.Bathroom, 7, new DateOnly(2024, 09, 10), "adminsFriend");

            //Act
            await _repo.AddShortageAsync(shortage1);
            await _repo.AddShortageAsync(shortage2);
            await _repo.AddShortageAsync(shortage3);
            await _repo.AddShortageAsync(shortage4);

            var allShortagesByUser = await _repo.GetShortagesByUserAsync("adminsFriend");

            //Assert
            allShortagesByUser.Should().HaveCount(2);
            allShortagesByUser.Should().Contain(shortage2);
            allShortagesByUser.Should().Contain(shortage4);
            allShortagesByUser.Should().NotContain(shortage1);
            allShortagesByUser.Should().NotContain(shortage3);

        }

        [Fact]
        public async Task Get_ShortagesByAnyCreator_Admin_CorrectCount_CorrectData()
        {
            //Arrange
            var shortage1 = new Shortage("New Shortage1", "John Doe", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("New Shortage2", "John Doe", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "adminsFriend");
            var shortage3 = new Shortage("New Shortage3", "John Doe", Category.Food, Room.MeetingRoom, 3, new DateOnly(2024, 09, 10), "admin");
            var shortage4 = new Shortage("New Shortage4", "John Doe", Category.Electronics, Room.Bathroom, 7, new DateOnly(2024, 09, 10), "adminsOtherFriend");

            //Act
            await _repo.AddShortageAsync(shortage1);
            await _repo.AddShortageAsync(shortage2);
            await _repo.AddShortageAsync(shortage3);
            await _repo.AddShortageAsync(shortage4);

            var allShortagesByUser = await _repo.GetShortagesByUserAsync("admin");

            //Assert
            allShortagesByUser.Should().HaveCount(4);

            allShortagesByUser.Should().Contain(shortage2);
            allShortagesByUser.Should().Contain(shortage4);
            allShortagesByUser.Should().Contain(shortage1);
            allShortagesByUser.Should().Contain(shortage3);

        }

        [Fact]
        public async Task Delete_Shortage_Admin_CorrectCount_CorrectShortageDeleted()
        {
            //Arrange
            var shortage1 = new Shortage("New Shortage1", "John Doe", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("New Shortage2", "John Doe", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "admin");

            //Act
            await _repo.AddShortageAsync(shortage1);
            await _repo.AddShortageAsync(shortage2);
            await _repo.DeleteShortageAsync(shortage2);
            var allShortages = await _repo.GetAllShortagesAsync();

            //Assert
            allShortages.Should().HaveCount(1);
            allShortages.Should().Contain(shortage1);
            allShortages.Should().NotContain(shortage2);
        }

        [Fact]
        public async Task Delete_Shortage_NotAdmin_NotCreator_ShortageNotDeleted()
        {
            //Arrange
            var shortage1 = new Shortage("New Shortage1", "John Doe", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "adminsfriend");
            var shortage2 = new Shortage("New Shortage2", "John Doe", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "adminsfriend");

            ApplicationUser.UserName = "NotAdmin";

            //Act
            await _repo.AddShortageAsync(shortage1); 
            await _repo.AddShortageAsync(shortage2);
            await _repo.DeleteShortageAsync(shortage2);
            var allShortages = await _repo.GetAllShortagesAsync();

            //Assert
            allShortages.Should().HaveCount(2);
            allShortages.Should().Contain(shortage1);
            allShortages.Should().Contain(shortage2);
        }
    }
}
