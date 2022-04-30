using System;
using NUnit.Framework;
using Hearts_of_Iron_IV_Tools.Country;

namespace Hearts_of_Iron_IV_Tools_Test.Country
{
    [TestFixture]
    public class StateTest
    {
        private const string TEST =
                        "state= {\n\tid=2\n\tname=\"STATE_2\" # Latium\n\tmanpower = 5781971\n\t\n\tstate_category = megalopolis\n\n\thistory=\n\t{\n\t\towner = LAT\n\t\tvictory_points = { 9904 40 }\n\t\tvictory_points = { 11846 5 }\n\t\tbuildings = {\n\t\t\tinfrastructure = 4\n\t\t\tair_base = 8\n\t\t\tarms_factory = 1\n\t\t\tindustrial_complex = 3\n\t\t\t11751 = {\n\t\t\t\tnaval_base = 5\n\t\t\t}\n\t\t\t11846 = {\n\t\t\t\tnaval_base = 2\n\t\t\t}\n\t\t}\n\t\tadd_core_of = LAT  \n\tadd_core_of = GER\n\t}\n\n\tprovinces= {\n\t\t923 6862 9794 9904 11751 11846    11882\n \t}\n\t\n\tlocal_supplies=0.0\n}";
        private static readonly State _state = new State(TEST);

        [Test]
        public void GetterTest()
        {
            Assert.AreEqual(2, _state.Id);
            Assert.AreEqual("STATE_2", _state.Name);
            Assert.AreEqual(StateCategory.MEGALOPOLIS, _state.Type);
            Assert.AreEqual("LAT", _state.Owner);
            Assert.AreEqual(5781971, _state.Manpower);
        }

        [Test]
        public void GetGetAllProvinceIdTest()
        {
            Assert.That(new int[] {923, 6862, 9794, 9904, 11751, 11846, 11882},
                Is.EquivalentTo(_state.GetAllProvinceId()));
        }

        [Test]
        public void GetHasCoreTagsTest()
        {
            Assert.That(new string[] {"LAT", "GER"},
                Is.EquivalentTo(_state.GetHasCoreTags()));
        }

        [Test]
        public void GetBuildingNumberTest()
        {
            Assert.AreEqual(8, _state.GetBuildingNumber(BuildingType.AIR_BASE));
            Assert.AreEqual(4, _state.GetBuildingNumber(BuildingType.INFRASTRUCTURE));
            Assert.AreEqual(3, _state.GetBuildingNumber(BuildingType.INDUSTRIAL_COMPLEX));
            Assert.AreEqual(1, _state.GetBuildingNumber(BuildingType.ARMS_FACTORY));
            Assert.AreEqual(0, _state.GetBuildingNumber(BuildingType.SYNTHETIC_REFINERY));
            Assert.AreEqual(0, _state.GetBuildingNumber(BuildingType.NUCLEAR_REACTOR));
            Assert.AreEqual(0, _state.GetBuildingNumber(BuildingType.DOCKYARD));
            Assert.AreEqual(0, _state.GetBuildingNumber(BuildingType.FUEL_SILO));
            Assert.AreEqual(0, _state.GetBuildingNumber(BuildingType.ROCKET_SITE));
        }

        [Test]
        public void GetResourcesNumberTest()
        {
            Assert.AreEqual(0, _state.GetResourcesNumber(ResourcesType.OIL));
            Assert.AreEqual(0, _state.GetResourcesNumber(ResourcesType.TUNGSTEN));
            Assert.AreEqual(0, _state.GetResourcesNumber(ResourcesType.RUBBER));
            Assert.AreEqual(0, _state.GetResourcesNumber(ResourcesType.STEEL));
            Assert.AreEqual(0, _state.GetResourcesNumber(ResourcesType.CHROMIUM));
            Assert.AreEqual(0, _state.GetResourcesNumber(ResourcesType.ALUMINIUM));
        }

        [Test]
        public void StateExceptionTest()
        {
            Assert.Throws<ArgumentNullException>(() => new State(null));
            Assert.Throws<ArgumentNullException>(() => State.ByPath(null));
        }
    }
}