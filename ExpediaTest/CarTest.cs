using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;
using System.Collections.Generic;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = ObjectMother.BMW();
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = ObjectMother.Saab();
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarGetsCarLocation()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            int carNumber = 10;
            String carLocation = "Home";

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(carNumber);
                LastCall.Return(carLocation);
            }

            var target = new Car(10);
            target.Database = mockDatabase;

            String result;
            result = target.getCarLocation(carNumber);
            Assert.AreEqual(result, carLocation);
        }

        [Test()]
        public void TestThatCarHasCorrectMileage()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 152000;
            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;
            Int32 result = target.Mileage;
            Assert.AreEqual(result, Miles);
        }
	}
}
