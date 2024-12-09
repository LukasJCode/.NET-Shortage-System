using ShortageSystem.Repositories;
using FluentAssertions;
using ShortageSystem.Filters;
using ShortageSystem.Models;


namespace ShortageSystem.Tests.Filters
{
    
    public class ShortageFiltersTests
    {
        ShortageFilters shortageFilters = new ShortageFilters();

        [Fact]
        public void Filter_Shortages_ByTitle_InputMatchesExactly_CorrectShortages()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "admin");
            
            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            //Act
            var filteredShortages = shortageFilters.FilterByTitle(shortages, "Change Lightbulb");

            //Assert
            filteredShortages.Should().HaveCount(1);
            filteredShortages.Should().Contain(shortage1);
            filteredShortages.Should().NotContain(shortage2);
        }

        [Fact]
        public void Filter_Shortages_ByTitle_InputMatchesPartly_CorrectShortages()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "admin");

            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            //Act
            var filteredShortages = shortageFilters.FilterByTitle(shortages, "Lightbulb");

            //Assert
            filteredShortages.Should().HaveCount(1);
            filteredShortages.Should().Contain(shortage1);
            filteredShortages.Should().NotContain(shortage2);
        }

        [Fact]
        public void Filter_Shortages_ByTitle_InputDoesNotMatch_NoShortages()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2024, 09, 10), "admin");

            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            //Act
            var filteredShortages = shortageFilters.FilterByTitle(shortages, "Beans");

            //Assert
            filteredShortages.Should().HaveCount(0);
            filteredShortages.Should().NotContain(shortage1);
            filteredShortages.Should().NotContain(shortage2);
        }

        [Fact]
        public void Filter_Shortages_ByCreatedOn_CorrectShortages()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2023, 09, 10), "admin");

            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            DateInterval dateInterval = new DateInterval(new DateOnly(2023, 01, 10) , new DateOnly(2023, 12, 12));

            //Act
            var filteredShortages = shortageFilters.FilterByCreatedOn(shortages, dateInterval);

            //Assert
            filteredShortages.Should().HaveCount(1);
            filteredShortages.Should().Contain(shortage2);
            filteredShortages.Should().NotContain(shortage1);
        }

        [Fact]
        public void Filter_Shortages_ByCreatedOn_StartDateMoreRecentThanEndDate_EmptyArray()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2023, 09, 10), "admin");

            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            DateInterval dateInterval = new DateInterval(new DateOnly(2024, 01, 10), new DateOnly(2023, 12, 12));

            //Act
            var filteredShortages = shortageFilters.FilterByCreatedOn(shortages, dateInterval);

            //Assert
            filteredShortages.Should().HaveCount(0);
        }

        [Fact]
        public void Filter_Shortages_ByCategory_CorrectShortages()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2023, 09, 10), "admin");

            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            //Act
            var filteredShortages = shortageFilters.FilterByCategory(shortages, Category.Other);

            //Assert
            filteredShortages.Should().HaveCount(1);
            filteredShortages.Should().Contain(shortage2);
            filteredShortages.Should().NotContain(shortage1);
        }

        [Fact]
        public void Filter_Shortages_ByRoom_CorrectShortages()
        {
            //Arrange
            var shortage1 = new Shortage("Change Lightbulb", "Lightbulb", Category.Electronics, Room.MeetingRoom, 5, new DateOnly(2024, 09, 10), "admin");
            var shortage2 = new Shortage("Buy Apples", "Apples", Category.Other, Room.Kitchen, 1, new DateOnly(2023, 09, 10), "admin");

            List<Shortage> shortages = new List<Shortage>();
            shortages.Add(shortage1);
            shortages.Add(shortage2);

            //Act
            var filteredShortages = shortageFilters.FilterByRoom(shortages, Room.Kitchen);

            //Assert
            filteredShortages.Should().HaveCount(1);
            filteredShortages.Should().Contain(shortage2);
            filteredShortages.Should().NotContain(shortage1);
        }
    }
}
