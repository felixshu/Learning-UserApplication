namespace User.Data.Model
{
	public enum WorkOrderStatus
	{
		Created = 0,
		Inprocess = 10,
		Rework = 15,
		Submitted = 20,
		Approved = 30,
		Canceled = -10,
		Rejected = -20
	}
}