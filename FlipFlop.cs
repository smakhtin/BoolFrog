using System;
using Grasshopper.Kernel;

namespace BoolFrog
{
	public class FlipFlop :GH_Component
	{
		#region inputs
		private const string PInSet = "Set";
		private const string PInReset = "Reset";
		#endregion inputs

		#region outputs
		private const string POutOutput = "Output";
		private const string POutInverseOutput = "Inverse Output";
		#endregion outputs

		private bool FOutput;

		public FlipFlop():base("FlipFlop", "FlipFlop", "If set equal true, output equal true. If reset equal true, output equal false", "BoolFrog", "Animation")
		{
			
		}

		public override Guid ComponentGuid
		{
			get { return new Guid("f9f6c978-4479-463a-af3a-0af6ee370a9a"); }
		}

		protected override void RegisterInputParams(GH_InputParamManager pManager)
		{
			pManager.Register_BooleanParam(PInSet, "Set", "Set bang", false);
			pManager.Register_BooleanParam(PInReset, "Res", "Reset bang", false);
		}

		protected override void RegisterOutputParams(GH_OutputParamManager pManager)
		{
			pManager.Register_BooleanParam(POutOutput, "Out", "Ouput value");
			pManager.Register_BooleanParam(POutInverseOutput, "InvOut", "Inversed output value");
		}

		protected override void SolveInstance(IGH_DataAccess da)
		{
			bool set = false;
			da.GetData(PInSet, ref set);
			bool reset = false;
			da.GetData(PInReset, ref reset);

			if (set) FOutput = true;
			else if (reset) FOutput = false;

			da.SetData(POutOutput, FOutput);
			da.SetData(POutInverseOutput, !FOutput);
		}
	}
}
