using System;
using Grasshopper.Kernel;

namespace BoolFrog
{
	public class TogEdge : GH_Component
	{
		#region inputs
		private const string PInInput = "Input";
		#endregion inputs

		#region outputs
		private const string POutUpEdge = "Up Edge";
		private const string POutDownEdge = "Down Edge";
		#endregion outputs

		private bool FWasUp;

		public TogEdge()
			: base("TogEdge", "TogEdge", "When toggle equal true sends UpEdge true for one frame", "BoolFrog", "Animation")
		{

		}

		public override Guid ComponentGuid
		{
			get { return new Guid("1cb7144a-74a9-4cfd-9c9e-14a27a30e94a"); }
		}

		protected override void RegisterInputParams(GH_InputParamManager pManager)
		{
			pManager.Register_BooleanParam(PInInput, "I", "Input value", false);
		}

		protected override void RegisterOutputParams(GH_OutputParamManager pManager)
		{
			pManager.Register_BooleanParam(POutUpEdge, "Up", "True for one frame");
			pManager.Register_BooleanParam(POutDownEdge, "Down", "True for one frame");
		}

		protected override void SolveInstance(IGH_DataAccess da)
		{
			GH_Document document = OnPingDocument();
			if (document == null) return;

			da.SetData(POutUpEdge, false);
			da.SetData(POutDownEdge, false);

			bool input = false;
			da.GetData(PInInput, ref input);

			if (!FWasUp && input)
			{
				FWasUp = true;
				da.SetData(POutUpEdge, true);
				document.ScheduleSolution(10, CallBack);
			}
			else if (!input && FWasUp)
			{
				FWasUp = false;
				da.SetData(POutDownEdge, true);
				document.ScheduleSolution(10, CallBack);
			}
		}

		private void CallBack(GH_Document doc)
		{
			ExpireSolution(false);
		}
	}
}
