using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Server
{
	public class SolveMazeCommand : ICommand
	{
		private IModel model;
		public SolveMazeCommand(IModel newModel)
		{
			model = newModel;
		}

		public ConnectionInfo Execute(string[] args, TcpClient client = null)
		{
			string name = args[0];
			string algo = args[1];

			SolveInfo solveInfo = model.SolveMaze(name, algo);
			SolutionAdapter solutionAdapter = new SolutionAdapter(solveInfo.Solution);
			string strSolution = solutionAdapter.CreateString();

			JObject solutionObj = new JObject();
			solutionObj["Name"] = name;
			solutionObj["Solution"] = strSolution;
			solutionObj["NodesEvaluated"] = solveInfo.NodesEvaluated;

			ConnectionInfo connectionInfo = new ConnectionInfo();
			connectionInfo.Answer = solutionObj.ToString();
			connectionInfo.CloseConnection = true;

			return connectionInfo;
		}
	}
}