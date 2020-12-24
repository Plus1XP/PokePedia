using Microsoft.VisualStudio.TestTools.UnitTesting;

using PokeDex.Models;

using System.Collections.Generic;

namespace PokeDex.UnitTests
{
    [TestClass]
    public class ElementalColoursTests
    {
        [TestMethod]
        public void CanParseElementalData_TypeIsNull_ReturnsFalse()
        {
            //var elementalColours = new ElementalColours();

            //var result = elementalColours.GetElementalColour("Fire", null);

            //var fire = "#F08030";

            //Assert.IsTrue(result.Item2 == fire);
        }

        [TestMethod]

        public void ReturnsCorrectTypeColour_TypeIsString_ReturnsTrue()
        {
            ElementalColours elementalColours = new ElementalColours();
            List<string> list1 = new List<string>() { "fire", "water" };
            List<string> list2 = new List<string>() { "fire" };

            System.Tuple<string, string> result1 = elementalColours.GetElementalColour(list1);

            Assert.IsTrue(result1.Item1 == elementalColours.typeColour["fire"] && result1.Item2 == elementalColours.typeColour["water"]);

            System.Tuple<string, string> result2 = elementalColours.GetElementalColour(list1);

            Assert.IsTrue(result1.Item1 == elementalColours.typeColour["fire"]);

            System.Tuple<string, string> result3 = elementalColours.GetElementalBackgroundColour(list1);

            Assert.IsTrue(result1.Item1 == elementalColours.typeColour["fire"] && result1.Item2 == elementalColours.typeColour["water"]);

            System.Tuple<string, string> result4 = elementalColours.GetElementalBackgroundColour(list1);

            Assert.IsTrue(result1.Item1 == elementalColours.typeColour["fire"]);

        }
    }
}
