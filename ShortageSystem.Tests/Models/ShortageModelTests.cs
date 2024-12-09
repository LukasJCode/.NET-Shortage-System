using ShortageSystem.Models;
using Xunit;

namespace ShortageSystem.Tests.Models
{
    public class ShortageModelTests
    {
        [Fact]
        public void Shortage_Creation_With_Valid_Properties_Should_Be_Successful()
        {
            // Arrange
            string title = "Projector";
            string name = "Conference Room Projector";
            Room room = Room.MeetingRoom;
            Category category = Category.Electronics;
            int priority = 8;
            DateOnly dateOnly = new DateOnly(2024, 09, 10);
            string creator = "admin";

            // Act
            Shortage shortage = new Shortage(title, name, category, room, priority, dateOnly, creator);

            // Assert
            Assert.Equal(title, shortage.Title);
            Assert.Equal(name, shortage.Name);
            Assert.Equal(room, shortage.Room);
            Assert.Equal(category, shortage.Category);
            Assert.Equal(priority, shortage.Priority);
            Assert.Equal(dateOnly, shortage.CreatedOn);
            Assert.Equal(creator, shortage.CreatedBy);
        }
    }
}
