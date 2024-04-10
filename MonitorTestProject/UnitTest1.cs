using System.Text.RegularExpressions;

namespace MonitorTestProject
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void RegexReturnsFileNameCorrectly()
		{
			Regex regex = new(@"\w*\.exe*$", RegexOptions.IgnoreCase);

			Match match = regex.Match("C:\\Users\\Vinnie\\Downloads\\regextester\\RegexTester\\RegexTester\\bin\\Debug\\RegexTester.exe");
			string temp = match.Value.Split(".")[0];
			Assert.IsTrue(temp == "RegexTester");
		}
	}
}