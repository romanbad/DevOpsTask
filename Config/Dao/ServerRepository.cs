using System.Configuration;
using System.IO;
using Config.Model;

namespace Config.Dao
{
	public class ServerRepository
	{
		private static readonly string ServersFolder =
			Path.Combine(Path.GetFullPath(ConfigurationManager.AppSettings["GitFolder"]), "servers");

		public Server GetServer(string id, bool fetch = true)
		{
			if (fetch)
				GitClient.Fetch();
			var serverFile = Path.Combine(ServersFolder, id + ".json.cfg");
			return Base.FromJson<Server>(File.ReadAllText(serverFile));
		}
	}
}