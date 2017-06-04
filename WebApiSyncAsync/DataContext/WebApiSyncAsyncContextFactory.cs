namespace WebApiSyncAsync.DataContext
{
	public class WebApiSyncAsyncContextFactory : IWebApiSyncAsyncContextFactory
	{
		public WebApiSyncAsyncContext Create()
		{
			return new WebApiSyncAsyncContext();
		}
	}
}