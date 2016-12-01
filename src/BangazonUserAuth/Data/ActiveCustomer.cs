using BangazonUserAuth.Models;

namespace BangazonUserAuth.Data
{
	//Class Name: ActiveCustomer
	//Author: Grant Regnier
	//Purpose of the class: This class should follow the singleton patern and only have one instance in existance at a time. It should also have a property of customer that is accessable wherever the instance is called.
	//Methods in Class: None.
	public class ActiveCustomer
	{
		// Make the class a singleton to maintain state across all uses
		private static ActiveCustomer _instance;
		public static ActiveCustomer Instance
		{
			get {
				// First time an instance of this class is requested
				if (_instance == null) {
					_instance = new ActiveCustomer ();
				}
				return _instance;
			}
		}

		// To track the currently active customer - selected by user
		public Customer Customer { get; set; }

	}
}
